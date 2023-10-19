using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Analytics;
using Unity.Services.Economy.Editor.Authoring.Core.Deploy;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Model;
using Unity.Services.Economy.Editor.Authoring.Shared.Infrastructure.Collections;
using UnityEditor;

namespace Unity.Services.Economy.Editor.Authoring.Deployment
{
    class DeployCommand : Command<EconomyAsset>
    {
        readonly IEconomyDeploymentHandler m_DeploymentHandler;
        readonly IEconomyResourcesLoader m_ResourcesLoader;
        readonly IEconomyEditorAnalytics m_EditorAnalytics;

        public override string Name => L10n.Tr("Deploy");

        public DeployCommand(
            IEconomyDeploymentHandler moduleDeploymentHandler,
            IEconomyResourcesLoader resourcesLoader,
            IEconomyEditorAnalytics editorAnalytics)
        {
            m_DeploymentHandler = moduleDeploymentHandler;
            m_ResourcesLoader = resourcesLoader;
            m_EditorAnalytics = editorAnalytics;
        }

        public override async Task ExecuteAsync(IEnumerable<EconomyAsset> items, CancellationToken cancellationToken = default)
        {
            var itemList = items.ToList();

            foreach (var economyAsset in itemList)
            {
                await economyAsset.BuildAndValidateEconomyResource(m_ResourcesLoader, cancellationToken);
            }

            ValidateEconomyRealMoneyPurchaseHasStoreIdentifier(itemList);

            var economyResources = itemList
                .Where(a => a.Resource != null)
                .Select(a => a.Resource)
                .ToList()
                .AsReadOnly();

            await m_DeploymentHandler.DeployAsync(economyResources, false, false, cancellationToken);

            economyResources.ForEach(r =>
            {
                m_EditorAnalytics.SendEvent(
                    "economy_file_deployed",
                    r.EconomyType,
                    default,
                    r.Status.MessageSeverity == SeverityLevel.Error
                        ? r.Status.MessageDetail 
                        : "");
            });
        }

        static void ValidateEconomyRealMoneyPurchaseHasStoreIdentifier(List<EconomyAsset> items)
        {
            items
                .Where(i => i.Resource?.EconomyType == EconomyResourceTypes.MoneyPurchase)
                .ForEach(i =>
                {
                    var realMoneyPurchase = i.Resource as EconomyRealMoneyPurchase;

                    if (realMoneyPurchase?.StoreIdentifiers.AppleAppStore == null &&
                        realMoneyPurchase?.StoreIdentifiers.GooglePlayStore == null)
                    {
                        i.Status = new DeploymentStatus(
                            "Missing Store Identifier",
                            "At least one store identifier has to be set for this type of resource " +
                            "(AppleAppStore or GooglePlayStore).",
                            SeverityLevel.Error);
                        i.Resource = null;
                    }
                });
        }
    }
}
