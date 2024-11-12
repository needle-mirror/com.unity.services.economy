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
    class EconomyPackageInitializer : IInitializablePackageV2
    {
        const string k_CloudEnvironmentKey = "com.unity.services.core.cloud-environment";
        const string k_StagingEnvironment = "staging";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeOnLoad()
        {
            var initializer = new EconomyPackageInitializer();
            initializer.Register(CorePackageRegistry.Instance);
        }

        public void Register(CorePackageRegistry registry)
        {
            registry.Register(this)
                .DependsOn<IAccessToken>()
                .DependsOn<ICloudProjectId>()
                .DependsOn<IPlayerId>()
                .DependsOn<IInstallationId>()
                .DependsOn<IProjectConfiguration>()
                .DependsOn<IExternalUserId>();
        }

        public Task Initialize(CoreRegistry registry)
        {
            EconomyService.instance = InitializeService(registry);

            // Used to support the old static interface
            // Note: CI transforms warning into errors, and this code is there to explicitly support legacy systems, let's ignore this one
#pragma warning disable CS0618
            Economy.Configuration = new Configuration();
            Economy.PlayerBalances = new PlayerBalances();
            Economy.PlayerInventory = new PlayerInventory();
            Economy.Purchases = new Purchases();
#pragma warning restore CS0618

            return Task.CompletedTask;
        }

        public Task InitializeInstanceAsync(CoreRegistry registry)
        {
            InitializeService(registry);
            return Task.CompletedTask;
        }

        IEconomyService InitializeService(CoreRegistry registry)
        {
            var projectConfiguration = registry.GetServiceComponent<IProjectConfiguration>();
            var cloudProjectId = registry.GetServiceComponent<ICloudProjectId>();
            var installationId = registry.GetServiceComponent<IInstallationId>().GetOrCreateIdentifier();
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
            var service = InitializeEconomy(cloudProjectId, accessToken, registry.GetServiceComponent<IPlayerId>(), configurationApiClient, currenciesApiClient, inventoryApiClient, purchasesApiClient, installationId, analyticsUserId);
            registry.RegisterService<IEconomyService>(service);
            return service;
        }

        internal EconomyInstance InitializeEconomy(ICloudProjectId cloudProjectId, IAccessToken accessToken, IPlayerId playerId, IInternalConfigurationApiClient configurationApiClient, ICurrenciesApiClient currenciesApiClient, IInventoryApiClient inventoryApiClient, IPurchasesApiClient purchasesApiClient, string unityProjectId, string analyticsUserId)
        {
            IEconomyAuthentication economyAuth = new EconomyAuthentication(playerId, accessToken, unityProjectId, analyticsUserId);

            ConfigurationInternal configurationInternal = new ConfigurationInternal(cloudProjectId, configurationApiClient, economyAuth);
            PlayerBalancesInternal playerBalancesInternal = new PlayerBalancesInternal(cloudProjectId, currenciesApiClient, economyAuth);
            PlayerInventoryInternal playerInventoryInternal = new PlayerInventoryInternal(cloudProjectId, inventoryApiClient, economyAuth);
            PurchasesInternal purchasesInternal = new PurchasesInternal(cloudProjectId, purchasesApiClient, economyAuth, playerBalancesInternal, playerInventoryInternal);

            return new EconomyInstance(configurationInternal, playerBalancesInternal, playerInventoryInternal, purchasesInternal);
        }

        static string GetAnalyticsUserId(CoreRegistry registry)
        {
            var externalUserId = registry.GetServiceComponent<IExternalUserId>();

            if (!string.IsNullOrEmpty(externalUserId.UserId))
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
