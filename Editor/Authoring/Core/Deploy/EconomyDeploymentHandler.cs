using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Core.Service;
using Unity.Services.Economy.Editor.Authoring.Core.Validations;
using ILogger = Unity.Services.Economy.Editor.Authoring.Core.Logging.ILogger;

namespace Unity.Services.Economy.Editor.Authoring.Core.Deploy
{
    class EconomyDeploymentHandler : IEconomyDeploymentHandler
    {
        readonly IEconomyClient m_Client;
        readonly ILogger m_Logger;
        const double k_DelayBetweenBatches = 1;
        const int k_BatchSize = 10;
        readonly object m_ResultLock = new();
        readonly object m_ToPublishLock = new();

        internal EconomyDeploymentHandler(IEconomyClient client, ILogger logger)
        {
            m_Client = client;
            m_Logger = logger;
        }

        /*
         * We have two types of resources: Basic ones (like Currency and Inventory Item)
         * and Purchase ones (like Virtual Purchase and Real Money Purchase)
         * Purchase resources depend on basic ones, so we need to follow a specific
         * behavior to avoid conflicts:
         * - Create basic resources then create purchase resources
         * Ensuring no purchase resource is created before the basic ones it uses
         * - Delete purchase resources then delete basic resources
         * Avoiding any resource to be deleted while another one depends on it
         */
        public async Task<DeployResult> DeployAsync(
            IReadOnlyList<IEconomyResource> localResources,
            bool dryRun = false,
            bool reconcile = false,
            CancellationToken cancellationToken = default)
        {
            var res = new DeployResult();
            res.Deployed = new List<IEconomyResource>();
            res.Failed = new List<IEconomyResource>();

            localResources = DuplicateResourceValidation.FilterDuplicateResources(
                localResources, out var duplicateGroups);
            UpdateDuplicateResourceStatus(res, duplicateGroups);

            localResources = localResources.Where(r => !string.IsNullOrEmpty(r.EconomyType)).ToList();

            List<IEconomyResource> remoteResources;
            try
            {
                remoteResources = await m_Client.List(cancellationToken);
            }
            catch (Exception e)
            {
                foreach (var resource in localResources)
                {
                    resource.Status =
                        new DeploymentStatus(
                            Statuses.FailedToDeploy,
                            "Failed to fetch remote resources: " + e.Message,
                            SeverityLevel.Error);
                }
                throw;
            }

            var toCreate = localResources
                .Except(remoteResources)
                .ToList();

            var toUpdate = localResources
                .Except(toCreate)
                .ToList();

            var toDelete = new List<IEconomyResource>();
            if (reconcile && localResources.Count > 0)
            {
                toDelete = remoteResources
                    .Except(localResources)
                    .ToList();
            }

            res.Created = toCreate;
            res.Deleted = toDelete;
            res.Updated = toUpdate;


            foreach (var economyResource in localResources)
            {
                UpdateResourceProgress(economyResource, 33);
            }

            if (reconcile && localResources.Count == 0)
            {
                if (remoteResources.Count > 0)
                {
                    m_Logger.LogWarning("Economy service deployment cannot be used in an empty folder " +
                                        "while using reconcile option. You cannot have an empty published configuration.");
                }
                return res;
            }

            if (dryRun)
            {
                res.Created = res.Created.Except(res.Failed).ToList();
                res.Updated = res.Updated.Except(res.Failed).ToList();
                res.Deleted = res.Deleted.Except(res.Failed).ToList();

                UpdateStatusAndProgress(res);
                return res;
            }

            var toPublish = new List<IEconomyResource>();

            // Sort resources
            var basicResourcesToCreate = toCreate
                .FindAll(r => !IsResourcePurchaseType(r.EconomyType));
            var complexResourcesToDelete = toDelete
                .FindAll(r => IsResourcePurchaseType(r.EconomyType));
            var complexResourcesToCreate = toCreate
                .FindAll(r => IsResourcePurchaseType(r.EconomyType));
            var basicResourcesToDelete = toDelete
                .FindAll(r => !IsResourcePurchaseType(r.EconomyType));

            // Deployment tasks (order is important)
            var createBasicResourceTasks =
                GetTasks(basicResourcesToCreate, m_Client.Create, toPublish, res, cancellationToken);
            var updateResourceTasks =
                GetTasks(toUpdate, m_Client.Update, toPublish, res, cancellationToken);
            IEnumerable<Task> deleteComplexResourceTasks = null;
            var createComplexResourceTasks =
                GetTasks(complexResourcesToCreate, m_Client.Create, toPublish, res, cancellationToken);
            IEnumerable<Task> deleteBasicResourceTasks = null;

            if (reconcile)
            {
                deleteComplexResourceTasks =
                    GetTasks(complexResourcesToDelete, m_Client.Delete, toPublish, res, cancellationToken);
                deleteBasicResourceTasks =
                    GetTasks(basicResourcesToDelete, m_Client.Delete, toPublish, res, cancellationToken);
            }

            var allTaskListsQueue = new Queue<(IEnumerable<Task> tasks, int nbTasks)>(new []
            {
                (createBasicResourceTasks, basicResourcesToCreate.Count),
                (updateResourceTasks, toUpdate.Count),
                (deleteComplexResourceTasks, complexResourcesToDelete.Count),
                (createComplexResourceTasks, complexResourcesToCreate.Count),
                (deleteBasicResourceTasks, basicResourcesToDelete.Count)
            });

            // Execute all tasks (in batches and in order)
            while (allTaskListsQueue.Count > 0)
            {
                var taskList = allTaskListsQueue.Dequeue();

                if (taskList.tasks == null || taskList.nbTasks == 0)
                {
                    continue;
                }

                await Batching.Batching.ExecuteInBatchesAsync(
                    taskList.tasks,
                    cancellationToken,
                    k_BatchSize,
                    k_DelayBetweenBatches);

                if (allTaskListsQueue.Count != 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(k_DelayBetweenBatches), cancellationToken);
                }
            }

            await PublishAndUpdateResult(toPublish, res, cancellationToken);

            res.Created = res.Created.Except(res.Failed).ToList();
            res.Updated = res.Updated.Except(res.Failed).ToList();
            res.Deleted = res.Deleted.Except(res.Failed).ToList();

            UpdateStatusAndProgress(res);
            return res;
        }

        IEnumerable<Task> GetTasks(
            List<IEconomyResource> resources,
            Func<IEconomyResource, CancellationToken, Task> resourceOperation,
            List<IEconomyResource> toPublish,
            DeployResult result,
            CancellationToken token)
            => resources.Select(r => GetResourceTask(
                () => resourceOperation(r, token),
                r,
                toPublish,
                result));

        IEnumerable<Task> GetTasks(
            List<IEconomyResource> resources,
            Func<string, CancellationToken, Task> resourceOperation,
            List<IEconomyResource> toPublish,
            DeployResult result,
            CancellationToken token)
            => resources.Select(r => GetResourceTask(
                () => resourceOperation(r.Id, token),
                r,
                toPublish,
                result));

        static bool IsResourcePurchaseType(string resourceType)
            => resourceType.Equals(EconomyResourceTypes.VirtualPurchase) ||
               resourceType.Equals(EconomyResourceTypes.MoneyPurchase);

        static void UpdateDuplicateResourceStatus(
            DeployResult result,
            IReadOnlyList<IGrouping<string, IEconomyResource>> duplicateGroups)
        {
            foreach (var group in duplicateGroups)
            {
                foreach (var r in group)
                {
                    result.Failed.Add(r);
                    var (shortMessage, _) = DuplicateResourceValidation.GetDuplicateResourceErrorMessages(r, group.ToList());
                    r.Status = new DeploymentStatus(Statuses.FailedToDeploy, shortMessage, SeverityLevel.Error);
                }
            }
        }

        async Task GetResourceTask(
            Func<Task> resourceOperation,
            IEconomyResource resource,
            List<IEconomyResource> toPublish,
            DeployResult result)
        {
            try
            {
                await resourceOperation();
                lock (m_ToPublishLock)
                {
                    toPublish.Add(resource);
                }
            }
            catch (Exception e)
            {
                HandleException(e, resource, result);
            }
        }

        void UpdateStatusAndProgress(DeployResult result)
        {
            foreach (var updated in result.Updated)
            {
                updated.Status = new DeploymentStatus(Statuses.Deployed, "Updated remotely", SeverityLevel.Success);
                UpdateResourceProgress(updated, 100);
            }

            foreach (var created in result.Created)
            {
                created.Status = new DeploymentStatus(Statuses.Deployed, "Created remotely", SeverityLevel.Success);
                UpdateResourceProgress(created, 100);
            }

            foreach (var deleted in result.Deleted)
            {
                deleted.Status = new DeploymentStatus(Statuses.Deployed, "Deleted remotely", SeverityLevel.Success);
                UpdateResourceProgress(deleted, 100);
            }

            foreach (var failed in result.Failed)
            {
                var msg = string.Join(" ", failed.Status.Message, failed.Status.MessageDetail);
                failed.Status = new DeploymentStatus(
                    Statuses.FailedToDeploy,
                    msg,
                    SeverityLevel.Error);
            }
        }
        async Task PublishAndUpdateResult(
            List<IEconomyResource> toPublishList,
            DeployResult res,
            CancellationToken cancellationToken)
        {
            try
            {
                await m_Client.Publish(cancellationToken);
                foreach (var resource in toPublishList)
                {
                    res.Deployed.Add(resource);
                    resource.Status = new DeploymentStatus(Statuses.Deployed, "", SeverityLevel.Success);
                }
            }
            catch (Exception e)
            {
                foreach (var resource in toPublishList)
                {
                    HandleException(e, resource, res);
                }
            }
        }

        internal virtual void UpdateResourceProgress(IEconomyResource resource, float progress)
        {
            resource.Progress = progress;
        }

        internal virtual void HandleException(Exception exception, IEconomyResource resource, DeployResult result)
        {
            lock (m_ResultLock)
            {
                result.Failed.Add(resource);
            }
            resource.Status = new DeploymentStatus(
                Statuses.FailedToDeploy,
                exception.Message,
                SeverityLevel.Error);
        }

        internal virtual bool IsLocalResourceUpToDateWithRemote(
            IEconomyResource resource,
            List<IEconomyResource> remoteResources)
            => throw new NotImplementedException();
    }
}
