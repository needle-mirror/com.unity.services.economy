using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy.Internal.Apis.Currencies;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Apis.Purchases;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Device.Internal;
using Unity.Services.Core.Internal;
using Unity.Services.Economy.Internal.Apis.Configuration;
using UnityEngine;

namespace Unity.Services.Economy
{
    class EconomyPackageInitializer : IInitializablePackage
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            CoreRegistry.Instance.RegisterPackage(new EconomyPackageInitializer())
                .DependsOn<IAccessToken>()
                .DependsOn<ICloudProjectId>()
                .DependsOn<IPlayerId>()
                .DependsOn<IInstallationId>()
                .DependsOn<IProjectConfiguration>();
        }

        public Task Initialize(CoreRegistry registry)
        {
            var cloudProjectId = registry.GetServiceComponent<ICloudProjectId>();
            var httpClient = new HttpClient();

            var configuration = new Unity.Services.Economy.Internal.Configuration(null, null, null, GetServiceHeaders(registry));

            IAccessToken accessToken = registry.GetServiceComponent<IAccessToken>();

            IConfigurationApiClient configurationApiClient = new ConfigurationApiClient(httpClient, accessToken, configuration);
            ICurrenciesApiClient currenciesApiClient = new CurrenciesApiClient(httpClient, accessToken, configuration);
            IInventoryApiClient inventoryApiClient = new InventoryApiClient(httpClient, accessToken, configuration);
            IPurchasesApiClient purchasesApiClient = new PurchasesApiClient(httpClient, accessToken, configuration);

            EconomyService.InitializeEconomy(cloudProjectId, accessToken, registry.GetServiceComponent<IPlayerId>(), configurationApiClient, currenciesApiClient, inventoryApiClient, purchasesApiClient);

            return Task.CompletedTask;
        }

        static Dictionary<string, string> GetServiceHeaders(CoreRegistry registry)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            string installationId = registry.GetServiceComponent<IInstallationId>().GetOrCreateIdentifier();
            string analyticsUserId = registry.GetServiceComponent<IProjectConfiguration>().GetString("com.unity.services.core.analytics-user-id");

            // If analytics user id is set, use that, otherwise fallback on the installation id
            if (!String.IsNullOrEmpty(analyticsUserId))
            {
                headers.Add("analytics-user-id", analyticsUserId);
            }
            else
            {
                headers.Add("unity-installation-id", installationId);
            }

            return headers;
        }
    }
}
