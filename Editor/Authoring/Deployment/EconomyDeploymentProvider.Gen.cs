using System.Collections.ObjectModel;
using Unity.Services.DeploymentApi.Editor;
using UnityEditor;

namespace Unity.Services.Economy.Editor.Authoring.Deployment
{
    class EconomyDeploymentProvider : DeploymentProvider
    {
        public override string Service => L10n.Tr("Economy");

        public override Command DeployCommand { get; }

        public EconomyDeploymentProvider(
            DeployCommand deployCommand,
            OpenEconomyDashboardCommand openEconomyDashboardCommand,
            ObservableCollection<IDeploymentItem> deploymentItems)
            : base(deploymentItems)
        {
            DeployCommand = deployCommand;
            Commands.Add(openEconomyDashboardCommand);
        }
    }
}
