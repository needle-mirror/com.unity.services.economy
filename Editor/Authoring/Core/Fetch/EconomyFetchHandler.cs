using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Core.IO;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Core.Service;
using Unity.Services.Economy.Editor.Authoring.Core.Validations;

namespace Unity.Services.Economy.Editor.Authoring.Core.Fetch
{
    class EconomyFetchHandler : IEconomyFetchHandler
    {
        readonly IEconomyClient m_Client;
        readonly IEconomyResourcesLoader m_EconomyResourcesLoader;
        readonly IFileSystem m_FileSystem;

        public EconomyFetchHandler(
            IEconomyClient client,
            IEconomyResourcesLoader economyResourcesLoader,
            IFileSystem fileSystem)
        {
            m_Client = client;
            m_EconomyResourcesLoader = economyResourcesLoader;
            m_FileSystem = fileSystem;
        }

        public async Task<FetchResult> FetchAsync(string rootDirectory,
            IReadOnlyList<IEconomyResource> localResources,
            bool dryRun = false,
            bool reconcile = false,
            CancellationToken cancellationToken = default)
        {
            var res = new FetchResult();

            localResources = DuplicateResourceValidation.FilterDuplicateResources(
                localResources, out var duplicateGroups);

            var remoteResources = await GetRemoteResources(rootDirectory, localResources, cancellationToken);

            var toUpdate = remoteResources
                .Intersect(localResources)
                .ToList();

            var toDelete = localResources
                .Except(remoteResources)
                .ToList();

            var toCreate = new List<IEconomyResource>();
            if (reconcile)
            {
                toCreate = remoteResources
                    .Except(localResources)
                    .ToList();
            }

            res.Created = toCreate;
            res.Deleted = toDelete;
            res.Updated = toUpdate;
            res.Fetched = new List<IEconomyResource>();
            res.Failed = new List<IEconomyResource>();

            UpdateDuplicateResourceStatus(res, duplicateGroups);

            if (dryRun)
            {
                UpdateResourcesStatus(res);
                return res;
            }

            var updateTasks = new List<(IEconomyResource, Task)>();
            var deleteTasks = new List<(IEconomyResource, Task)>();
            var createTasks = new List<(IEconomyResource, Task)>();

            foreach (var resource in toUpdate)
            {
                var task = m_FileSystem.WriteAllText(
                    resource.Path,
                    m_EconomyResourcesLoader.CreateAndSerialize(resource),
                    cancellationToken);
                updateTasks.Add((resource, task));
            }

            foreach (var resource in toDelete)
            {
                var task = m_FileSystem.Delete(
                    resource.Path,
                    cancellationToken);
                deleteTasks.Add((resource, task));
            }

            foreach (var resource in toCreate)
            {
                var task = m_FileSystem.WriteAllText(
                    resource.Path,
                    m_EconomyResourcesLoader.CreateAndSerialize(resource),
                    cancellationToken);
                createTasks.Add((resource, task));
            }

            await UpdateResult(updateTasks, res, new DeploymentStatus(Statuses.Fetched, "Updated locally", SeverityLevel.Success));
            await UpdateResult(deleteTasks, res, new DeploymentStatus(Statuses.Fetched, "Deleted locally", SeverityLevel.Success));
            await UpdateResult(createTasks, res, new DeploymentStatus(Statuses.Fetched, "Created locally", SeverityLevel.Success));

            return res;
        }

        async Task<List<IEconomyResource>> GetRemoteResources(string rootDirectory, IReadOnlyList<IEconomyResource> localResources, CancellationToken cancellationToken)
        {
            var remoteResources = await m_Client.List(cancellationToken);

            foreach (var remote in remoteResources)
            {
                var local = localResources.FirstOrDefault(l => Equals(l, remote));

                ((EconomyResource)remote).Path = local != null ? local.Path : CreateRemoteResourceName(rootDirectory, remote);
            }
            return remoteResources;
        }

        static string CreateRemoteResourceName(string rootDirectory, IEconomyResource remote)
        {
            return Path.Combine(rootDirectory, remote.Id) + ((EconomyResource)remote).EconomyExtension;
        }

        protected virtual void UpdateStatus(
            IEconomyResource economyResource,
            DeploymentStatus status)
        {
            // clients can override this to provide user feedback on progress
            ((EconomyResource)economyResource).Status = status;
        }

        protected virtual void UpdateProgress(
            IEconomyResource economyResource,
            float progress)
        {
            // clients can override this to provide user feedback on progress
            ((EconomyResource)economyResource).Progress = progress;
        }

        void UpdateDuplicateResourceStatus(
            FetchResult result,
            IReadOnlyList<IGrouping<string, IEconomyResource>> duplicateGroups)
        {
            foreach (var group in duplicateGroups)
            {
                foreach (var res in group)
                {
                    result.Failed.Add(res);
                    var(shortMessage, _) = DuplicateResourceValidation.GetDuplicateResourceErrorMessages(res, group.ToList());
                    UpdateStatus(res, new DeploymentStatus(Statuses.FailedToFetch, shortMessage));
                }
            }
        }

        void UpdateResourcesStatus(FetchResult result)
        {
            foreach (var created in result.Created)
            {
                UpdateStatus(created, new DeploymentStatus(Statuses.Fetched, "Created locally", SeverityLevel.Success));
            }

            foreach (var updated in result.Updated)
            {
                UpdateStatus(updated, new DeploymentStatus(Statuses.Fetched, "Updated locally", SeverityLevel.Success));
            }

            foreach (var deleted in result.Deleted)
            {
                UpdateStatus(deleted, new DeploymentStatus(Statuses.Fetched, "Deleted locally", SeverityLevel.Success));
            }
        }

        async Task UpdateResult(
            List<(IEconomyResource, Task)> tasks,
            FetchResult res,
            DeploymentStatus status)
        {
            foreach (var(resource, task) in tasks)
            {
                try
                {
                    await task;
                    res.Fetched.Add(resource);
                    UpdateStatus(resource, status);
                    UpdateProgress(resource, 100);
                }
                catch (Exception e)
                {
                    res.Failed.Add(resource);
                    UpdateStatus(resource, new DeploymentStatus(Statuses.FailedToFetch, e.Message));
                }
            }
        }
    }
}
