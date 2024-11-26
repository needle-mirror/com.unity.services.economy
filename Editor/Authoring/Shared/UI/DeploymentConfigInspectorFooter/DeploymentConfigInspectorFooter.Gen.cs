// WARNING: Auto generated code. Modifications will be lost!
// Original source 'com.unity.services.shared' @0.0.12.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        const string k_ViewInDeploymentAnalyticsKey = "clicked_view_in_deployment_window_btn";
        const string k_ViewInDashboardAnalyticsKey = "clicked_view_in_dashboard_btn";
        string m_ServiceName;
        string m_FilePath;
        Func<Task<string>> m_DashboardUrlGetter;
        ICommonAnalytics m_CommonAnalyticsSender;

        public Func<Task<string>> DashboardLinkUrlGetter
        {
            set
            {
                m_DashboardUrlGetter = value;
                SetupBtnViewInDashboard(this);
            }
        }

        public void BindGUI(string filePath, ICommonAnalytics analyticsSender, string serviceName = "")
        {
            SetupFooterVisual();
            SetupBtnViewInDeploymentWindow(this);
            SetupBtnViewInDashboard(this);
            m_ServiceName = serviceName ?? ReadPackageInfo().displayName;
            m_FilePath = filePath;
            m_CommonAnalyticsSender = analyticsSender;
        }

        void SetupFooterVisual([CallerFilePath] string sourceFilePath = "")
        {
            var basePath = GetBasePath(sourceFilePath);
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

        string GetBasePath(string sourceFilePath)
        {
            var packageInfo = ReadPackageInfo();
            var dirFullPath = Path.GetFullPath(Path.GetDirectoryName(sourceFilePath) !);
            var editorIx = dirFullPath.IndexOf("Editor");
            var dirRelativePath = dirFullPath.Substring(editorIx);

            var basePath = Path.Combine("Packages",
                packageInfo.name,
                dirRelativePath,
"Assets");
            return basePath;
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

        void SetupBtnViewInDashboard(VisualElement myInspector)
        {
            var viewInDashboardBtn = myInspector.Q<Button>("view-in-dashboard-btn");
            if (viewInDashboardBtn != null)
            {
                var enabled = m_DashboardUrlGetter != null;
                viewInDashboardBtn.SetEnabled(enabled);

                if (enabled)
                {
                    // if we don't do this we might trigger the link more than once when clicked
                    viewInDashboardBtn.clicked -= OnViewInDashboardClicked;
                    viewInDashboardBtn.clicked += OnViewInDashboardClicked;
                }
            }
        }

        async void OnViewInDashboardClicked()
        {
            SendAnalyticsEvent(k_ViewInDashboardAnalyticsKey);
            var url = await m_DashboardUrlGetter();
            Application.OpenURL(url);
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
                SendAnalyticsEvent(k_ViewInDeploymentAnalyticsKey);
#elif DEPLOYMENT_API_AVAILABLE_V1_0
                Logging.Logger.Log("Please update your Deployment package to use this feature. A minimum version of 1.4.0 is required.");
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
        void SendAnalyticsEvent(string key)
        {
            m_CommonAnalyticsSender.Send(new ICommonAnalytics.CommonEventPayload
            {
                action = key,
                context = m_ServiceName
            });
        }

#if !UNITY_2023_3_OR_NEWER
        new class UxmlFactory : UxmlFactory<DeploymentConfigInspectorFooter> {}
#endif
    }
}
