using System.Collections.ObjectModel;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core.Editor;
using Unity.Services.Core.Editor.Environments;
using Unity.Services.Core.Editor.OrganizationHandler;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Configuration;
using Unity.Services.Economy.Client.Apis.EconomyAdmin;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Http;
using Unity.Services.Economy.Editor.Authoring.AdminApi;
using Unity.Services.Economy.Editor.Authoring.Analytics;
using Unity.Services.Economy.Editor.Authoring.Core.Deploy;
using Unity.Services.Economy.Editor.Authoring.Core.IO;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Core.Service;
using Unity.Services.Economy.Editor.Authoring.Deployment;
using Unity.Services.Economy.Editor.Authoring.IO;
using Unity.Services.Economy.Editor.Authoring.Model;
using Unity.Services.Economy.Editor.Authoring.Shared.Analytics;
using Unity.Services.Economy.Editor.Authoring.Shared.DependencyInversion;
using UnityEditor;
using UnityEngine;
using static Unity.Services.Economy.Editor.Authoring.Shared.DependencyInversion.Factories;
using ICoreLogger = Unity.Services.Economy.Editor.Authoring.Core.Logging.ILogger;
using Logger = Unity.Services.Economy.Editor.Authoring.Logging.Logger;

namespace Unity.Services.Economy.Editor.Authoring
{
    class EconomyAuthoringServices : AbstractRuntimeServices<EconomyAuthoringServices>
    {
        [InitializeOnLoadMethod]
        static void Initialize()
        {
            Instance.Initialize(new ServiceCollection());
            var deploymentItemProvider = Instance.GetService<DeploymentProvider>();
            Deployments.Instance.DeploymentProviders.Add(deploymentItemProvider);
        }

        protected override void Register(ServiceCollection collection)
        {
            collection.Register(Default<ICommonAnalytics, CommonAnalytics>);
#if UNITY_2023_2_OR_NEWER
            collection.Register(Default<ICommonAnalyticProvider, CommonAnalyticProvider>);
#endif
            collection.RegisterSingleton(Default<ObservableCollection<IDeploymentItem>, ObservableEconomyAssets>);
            collection.Register(_ => Debug.unityLogger);
            collection.Register(col => (ObservableEconomyAssets)col.GetService(typeof(ObservableCollection<IDeploymentItem>)));
            collection.Register(Default<DeployCommand>);
            collection.Register(Default<IEconomyDeploymentHandler, EditorEconomyDeploymentHandler>);
            collection.Register(Default<IEnvironmentProvider, EnvironmentProvider>);
            collection.Register(Default<IProjectIdProvider, ProjectIdProvider>);
            collection.Register(Default<IAccessTokens, AccessTokens>);
            collection.Register(Default<IAccessToken, CoreAccessToken>);
            collection.Register(_ => EnvironmentsApi.Instance);
            collection.Register(Default<IEconomyEditorAnalytics, EconomyEditorAnalytics>);
            collection.RegisterStartupSingleton(Default<DeploymentProvider, EconomyDeploymentProvider>);
            collection.Register(Default<ICoreLogger, Logger>);
            collection.Register(Default<IHttpClient, HttpClient>);
            collection.Register(Default<IFileSystem, FileSystem>);
            collection.Register(Default<IEconomyJsonConverter, EconomyJsonConverter>);
            collection.Register(Default<IEconomyResourcesLoader, EconomyResourcesLoader>);
            collection.Register(Default<IEconomyAdminApiClient, EconomyAdminApiClient>);
            collection.Register(_ => new Configuration(null, null, null, null));
            collection.Register(Default<IEconomyClientParserHelper, EconomyClientParserHelper>);
            collection.Register(Default<IEconomyClient, EconomyClient>);
            collection.Register(_ => OrganizationProvider.Organization);
            collection.Register(Default<OpenEconomyDashboardCommand>);
            collection.Register(Default<IEconomyDashboardUrlResolver, EconomyDashboardUrlResolver>);
        }
    }
}
