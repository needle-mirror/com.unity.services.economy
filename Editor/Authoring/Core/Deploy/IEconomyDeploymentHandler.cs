using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.Core.Deploy
{
    interface IEconomyDeploymentHandler
    {
        Task<DeployResult> DeployAsync(IReadOnlyList<IEconomyResource> localResources,
            bool dryRun = false,
            bool reconcile = false,
            CancellationToken cancellationToken = default);
    }
}
