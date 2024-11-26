using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Model;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.Economy.Editor.Authoring.Deployment
{
    class OpenEconomyDashboardCommand : Command
    {
        readonly IEconomyDashboardUrlResolver m_DashboardUrlResolver;
        readonly IEconomyResourcesLoader m_ResourceLoader;
        public override string Name => L10n.Tr("Open in Dashboard");

        public OpenEconomyDashboardCommand(
            IEconomyDashboardUrlResolver dashboardUrlResolver,
            IEconomyResourcesLoader resourceLoader)
        {
            m_DashboardUrlResolver = dashboardUrlResolver;
            m_ResourceLoader = resourceLoader;
        }

        public override async Task ExecuteAsync(IEnumerable<IDeploymentItem> items, CancellationToken cancellationToken = default)
        {
            var ids = new List<string>();
            foreach (var asset in items.OfType<EconomyAsset>())
            {
                await asset.BuildAndValidateEconomyResource(m_ResourceLoader, cancellationToken);
                if (asset.Resource?.Id != null)
                    ids.Add(asset.Resource.Id);
            }

            foreach (var id in ids)
            {
                Application.OpenURL(await m_DashboardUrlResolver.EconomyResource(id));
            }
        }
    }
}
