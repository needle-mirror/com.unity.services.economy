using System;
using System.IO;
using System.Linq;
using System.Threading;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Deployment;
using Unity.Services.Economy.Editor.Authoring.Model;
using Unity.Services.Economy.Editor.Authoring.Shared.Analytics;
using Unity.Services.Economy.Editor.Authoring.Shared.UI.DeploymentConfigInspectorFooter;
using UnityEditor;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Unity.Services.Economy.Editor.Authoring.UI
{
    [CustomEditor(typeof(EconomyAsset))]
    [CanEditMultipleObjects]
    class EconomyResourceInspector : UnityEditor.Editor
    {
        const int k_MaxLines = 75;
        const string k_Template = "Packages/com.unity.services.economy/Editor/Authoring/UI/Assets/EconomyResourceInspector.uxml";

        public override VisualElement CreateInspectorGUI()
        {
            var myInspector = new VisualElement();
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(k_Template);
            visualTree.CloneTree(myInspector);

            ShowResourceBody(myInspector);
            SetupConfigFooter(myInspector);

            return myInspector;
        }

        void SetupConfigFooter(VisualElement myInspector)
        {
            var deploymentConfigInspectorFooter = myInspector.Q<DeploymentConfigInspectorFooter>();
            deploymentConfigInspectorFooter.BindGUI(
                AssetDatabase.GetAssetPath(target),
                EconomyAuthoringServices.Instance.GetService<ICommonAnalytics>(),
                "economy");

            var loader = EconomyAuthoringServices.Instance.GetService<IEconomyResourcesLoader>();

            // nb different instance than at import time, dont know why
            var economyAsset = target as EconomyAsset;

            // cannot null-coalesce, because its a unity object
            if (economyAsset != null)
            {
                economyAsset.BuildAndValidateEconomyResource(loader, CancellationToken.None)?.Wait();
                deploymentConfigInspectorFooter.DashboardLinkUrlGetter = () => EconomyAuthoringServices.Instance
                    .GetService<IEconomyDashboardUrlResolver>()
                    .EconomyResource(economyAsset.Resource?.Id);
            }
        }

        void ShowResourceBody(VisualElement myInspector)
        {
            var body = myInspector.Q<TextField>();
            if (targets.Length == 1)
            {
                body.visible = true;
                body.value = ReadResourceBody(targets[0]);
            }
            else
            {
                body.visible = false;
            }
        }

        static string ReadResourceBody(Object resource)
        {
            var path = AssetDatabase.GetAssetPath(resource);
            var lines = File.ReadLines(path).Take(k_MaxLines).ToList();
            if (lines.Count == k_MaxLines)
            {
                lines.Add("...");
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}
