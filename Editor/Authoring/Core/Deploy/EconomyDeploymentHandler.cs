using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        internal EconomyDeploymentHandler(IEconomyClient client, ILogger logger)
        {
            m_Client = client;
            m_Logger = logger;
        }

        public async Task<DeployResult> DeployAsync(
            IReadOnlyList<IEconomyResource> localResources,
            bool dryRun = false,
            bool reconcile = false,
            CancellationToken cancellationToken = default)
        {
            var res = new DeployResult();

            localResources = DuplicateResourceValidation.FilterDuplicateResources(
                localResources, out var duplicateGroups);



            var remoteResources = await m_Client.List(cancellationToken);

            var toCreate = localResources
                .Except(remoteResources)
                .ToList();

            var upToDate = localResources
                .Where(r => IsLocalResourceUpToDateWithRemote(r, remoteResources))
                .ToList();

            var toUpdate = localResources
                .Except(toCreate)
                .Except(upToDate)
                .ToList();

            upToDate.ForEach(r =>
                r.Status = new DeploymentStatus(Statuses.UpToDate, "", SeverityLevel.Success));

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
            res.Deployed = new List<IEconomyResource>();
            res.Failed = new List<IEconomyResource>();

            foreach (var economyResource in localResources)
            {
                UpdateResourceProgress(economyResource, 33);
            }

            UpdateDuplicateResourceStatus(res, duplicateGroups);

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

            var createBasicResourceTasks = new List<(IEconomyResource, Task)>();
            var updateTasks = new List<(IEconomyResource, Task)>();
            var deletePurchaseTasks = new List<(IEconomyResource, Task)>();


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

            // Create Basic Resources
            var basicResources = toCreate.FindAll(r => !IsResourcePurchaseType(r.EconomyType));
            foreach (var resource in basicResources)
            {
                createBasicResourceTasks.Add((resource, m_Client.Create(resource, cancellationToken)));
            }

            foreach (var resource in toUpdate)
            {
                updateTasks.Add((resource, m_Client.Update(resource, cancellationToken)));
            }

            if (reconcile)
            {
                // Delete Virtual and Money Purchase Resources
                var purchaseToDeleteResources = toDelete.FindAll(r => IsResourcePurchaseType(r.EconomyType));
                foreach (var resource in purchaseToDeleteResources)
                {
                    deletePurchaseTasks.Add((resource, m_Client.Delete(resource.Id, cancellationToken)));
                }
            }

            var toPublish = new List<IEconomyResource>();

            Task[] firstBatchOfTasks = new Task[3]
            {
                UpdateResult(createBasicResourceTasks, toPublish, res),
                UpdateResult(updateTasks, toPublish, res),
                UpdateResult(deletePurchaseTasks, toPublish, res)
            };

            // Update, Create and Delete first batch of resources
            await Task.WhenAll(firstBatchOfTasks);

            var createPurchaseTasks = new List<(IEconomyResource, Task)>();
            var deleteResourceTasks = new List<(IEconomyResource, Task)>();

            if (reconcile)
            {
                // Delete Non-Purchase Resources
                var basicResourceToDelete = toDelete.FindAll(r => !IsResourcePurchaseType(r.EconomyType));
                foreach (var resource in basicResourceToDelete)
                {
                    deleteResourceTasks.Add((resource, m_Client.Delete(resource.Id, cancellationToken)));
                }
            }

            // Create Virtual and Money Purchase Resources
            var purchaseResources = toCreate.FindAll(r => IsResourcePurchaseType(r.EconomyType));
            foreach (var localResource in purchaseResources)
            {
                createPurchaseTasks.Add((localResource, m_Client.Create(localResource, cancellationToken)));
            }

            Task[] secondBatchOfTasks = new Task[2]
            {
                UpdateResult(createPurchaseTasks, toPublish, res),
                UpdateResult(deleteResourceTasks, toPublish, res)
            };

            // Create and Delete second batch of resources
            await Task.WhenAll(secondBatchOfTasks);

            await PublishAndUpdateResult(toPublish, res, cancellationToken);

            res.Created = res.Created.Except(res.Failed).ToList();
            res.Updated = res.Updated.Except(res.Failed).ToList();
            res.Deleted = res.Deleted.Except(res.Failed).ToList();

            UpdateStatusAndProgress(res);
            return res;
        }

        static bool IsResourcePurchaseType(string resourceType)
        {
            return resourceType.Equals(EconomyResourceTypes.VirtualPurchase) || resourceType.Equals(EconomyResourceTypes.MoneyPurchase);
        }

        void UpdateDuplicateResourceStatus(
            DeployResult result,
            IReadOnlyList<IGrouping<string, IEconomyResource>> duplicateGroups)
        {
            foreach (var group in duplicateGroups)
            {
                foreach (var res in group)
                {
                    result.Failed.Add(res);
                    var (shortMessage, _) = DuplicateResourceValidation.GetDuplicateResourceErrorMessages(res, group.ToList());
                    res.Status = new DeploymentStatus(Statuses.FailedToDeploy, shortMessage);
                }
            }
        }

        async Task UpdateResult(
            List<(IEconomyResource, Task)> tasks,
            List<IEconomyResource> toPublish,
            DeployResult res)
        {
            foreach (var (resource, task) in tasks)
            {
                try
                {
                    await task;
                    if (task.Exception != null)
                    {
                        throw task.Exception;
                    }
                    toPublish.Add(resource);
                }
                catch (Exception e)
                {
                    HandleException(e, resource, res);
                }
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
                failed.Status = new DeploymentStatus(
                    Statuses.FailedToDeploy,
                    failed.Status.MessageDetail,
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
            result.Failed.Add(resource);
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
