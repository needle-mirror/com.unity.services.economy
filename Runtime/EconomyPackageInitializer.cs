using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy.Internal.Apis.Currencies;
using Unity.Services.Economy.Internal.Apis.InternalConfiguration;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Apis.Purchases;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Device.Internal;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Economy
{
    class EconomyPackageInitializer : IInitializablePackage
    {
        const string k_CloudEnvironmentKey = "com.unity.services.core.cloud-environment";
        const string k_StagingEnvironment = "staging";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            CoreRegistry.Instance.RegisterPackage(new EconomyPackageInitializer())
                .DependsOn<IAccessToken>()
                .DependsOn<ICloudProjectId>()
                .DependsOn<IPlayerId>()
                .DependsOn<IInstallationId>()
                .DependsOn<IProjectConfiguration>()
                .DependsOn<IExternalUserId>();
        }

        public Task Initialize(CoreRegistry registry)
        {
            var projectConfiguration = registry.GetServiceComponent<IProjectConfiguration>();
            var cloudProjectId = registry.GetServiceComponent<ICloudProjectId>();
            var externalUserId = registry.GetServiceComponent<IExternalUserId>();
            var httpClient = new HttpClient();

            var configuration = new Internal.Configuration(GetHost(projectConfiguration), null, null, null);
            externalUserId.UserIdChanged += id => UpdateExternalUserId(configuration, id);

            IAccessToken accessToken = registry.GetServiceComponent<IAccessToken>();

            IInternalConfigurationApiClient configurationApiClient = new InternalConfigurationApiClient(httpClient, accessToken, configuration);
            ICurrenciesApiClient currenciesApiClient = new CurrenciesApiClient(httpClient, accessToken, configuration);
            IInventoryApiClient inventoryApiClient = new InventoryApiClient(httpClient, accessToken, configuration);
            IPurchasesApiClient purchasesApiClient = new PurchasesApiClient(httpClient, accessToken, configuration);

            var analyticsUserId = GetAnalyticsUserId(registry);
            EconomyService.InitializeEconomy(cloudProjectId, accessToken, registry.GetServiceComponent<IPlayerId>(), configurationApiClient, currenciesApiClient, inventoryApiClient, purchasesApiClient, registry.GetServiceComponent<IInstallationId>().GetOrCreateIdentifier(), analyticsUserId);

            return Task.CompletedTask;
        }

        static string GetAnalyticsUserId(CoreRegistry registry)
        {
            var externalUserId = registry.GetServiceComponent<IExternalUserId>();

            if (!String.IsNullOrEmpty(externalUserId.UserId))
            {
                return externalUserId.UserId;
            }

            return null;
        }

        static void UpdateExternalUserId(Internal.Configuration configuration, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                configuration.Headers.Remove("analytics-user-id");
            }
            else
            {
                configuration.Headers["analytics-user-id"] = userId;
            }
        }

        string GetHost(IProjectConfiguration projectConfiguration)
        {
            var cloudEnvironment = projectConfiguration?.GetString(k_CloudEnvironmentKey);

            switch (cloudEnvironment)
            {
                case k_StagingEnvironment:
                    return "https://economy-stg.services.api.unity.com";
                default:
                    return "https://economy.services.api.unity.com";
            }
        }
    }
}
