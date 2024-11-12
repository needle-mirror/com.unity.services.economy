// WARNING: Auto generated code. Modifications will be lost!
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Shared.Analytics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Unity.Services.Economy.Editor.Authoring.Shared.UI.DeploymentConfigInspectorFooter
{
#if UNITY_2023_3_OR_NEWER
    [UxmlElement]
#endif
    partial class DeploymentConfigInspectorFooter : BindableElement
    {
        string m_ServiceName;
        string m_FilePath;
        ICommonAnalytics m_CommonAnalyticsSender;

        public void BindGUI(string filePath, ICommonAnalytics analyticsSender, string serviceName = "")
        {
            SetupFooterVisual();
            SetupBtnViewInDeploymentWindow(this);
            m_ServiceName = serviceName ?? ReadPackageInfo().displayName;
            m_FilePath = filePath;
            m_CommonAnalyticsSender = analyticsSender;
        }

        void SetupFooterVisual([CallerFilePath] string sourceFilePath = "")
        {
            var basePath = Path.Combine("Packages",
                RemovePathBeforePattern(Directory.GetParent(sourceFilePath) !.FullName, ReadPackageInfo().name),
                "Assets");
            var uxmlPath = Path.Combine(basePath, "DeploymentConfigInspectorFooter.uxml");
            var ussPath = Path.Combine(basePath, "DeploymentConfigInspectorFooter.uss");
            var ussDarkPath = Path.Combine(basePath, "DeploymentConfigInspectorFooterDark.uss");
            var ussLightPath = Path.Combine(basePath, "DeploymentConfigInspectorFooterLight.uss");

            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(ussPath));
            styleSheets.Add(
                EditorGUIUtility.isProSkin ? AssetDatabase.LoadAssetAtPath<StyleSheet>(ussDarkPath) :
                AssetDatabase.LoadAssetAtPath<StyleSheet>(ussLightPath));

            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            visualTreeAsset.CloneTree(this);
        }

        static string RemovePathBeforePattern(string path, string pattern)
        {
            int patternIndex = path.LastIndexOf(pattern);
            if (patternIndex >= 0)
            {
                return path.Substring(patternIndex);
            }
            return path;
        }

        PackageInfo ReadPackageInfo()
        {
            return PackageInfo.FindForAssembly(GetType().Assembly);
        }

        void SetupBtnViewInDeploymentWindow(VisualElement myInspector)
        {
            var viewInDeployBtn = myInspector.Q<Button>("view-in-deployment-window-btn");
            if (viewInDeployBtn != null)
            {
                viewInDeployBtn.clickable.clicked += SelectFileInDeploymentWindow;
            }
        }

        void SelectFileInDeploymentWindow()
        {
            if (File.Exists(m_FilePath))
            {
#if DEPLOYMENT_API_AVAILABLE_V1_1
                Deployments.Instance.DeploymentWindow.OpenWindow();
                Deployments.Instance.DeploymentWindow.ClearSelection();

                var deploymentItems = GetDeploymentItems();
                Deployments.Instance.DeploymentWindow.Select(deploymentItems);
                SendAnalyticsEvent();
#elif DEPLOYMENT_API_AVAILABLE_V1_0
                Logging.Logger.LogError("Please update your Deployment package to use this feature. A minimum version of 1.4.0 is required.");
#endif
            }
        }

#if DEPLOYMENT_API_AVAILABLE_V1_1
        List<IDeploymentItem> GetDeploymentItems()
        {
            var deploymentItems = Deployments.Instance.DeploymentWindow.GetFromFiles(new List<string> { m_FilePath });

            var simpleItems = deploymentItems.FindAll(x => x is not ICompositeItem);
            var compositeItems = deploymentItems.FindAll(x => x is ICompositeItem);

            // if any item is composite unwinds its children and add to the list
            if (compositeItems.Any())
            {
                foreach (var item in compositeItems)
                {
                    simpleItems.AddRange(((ICompositeItem)item).Children);
                }
            }

            return simpleItems;
        }

#endif
        void SendAnalyticsEvent()
        {
            m_CommonAnalyticsSender.Send(new ICommonAnalytics.CommonEventPayload
            {
                action = "clicked_view_in_deployment_window_btn",
                context = m_ServiceName
            });
        }

#if !UNITY_2023_3_OR_NEWER
        new class UxmlFactory : UxmlFactory<DeploymentConfigInspectorFooter> {}
#endif
    }
}
