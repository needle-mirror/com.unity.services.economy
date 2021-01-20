using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy.Internal.Apis.Currencies;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Apis.Purchases;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core.Device.Internal;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Economy
{
    internal class EconomyPackageInitializer : IInitializablePackage
    {
        private static GameObject _gameObjectFactory;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            CoreRegistry.Instance.RegisterPackage(new EconomyPackageInitializer())
                .DependsOn<IAccessToken>()
                .DependsOn<IPlayerId>()
                .DependsOn<IInstallationId>();
        }
        
        public Task Initialize(CoreRegistry registry)
        {
            var httpClient = new HttpClient();
            
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                {"unity-installation-id", registry.GetServiceComponent<IInstallationId>().GetOrCreateIdentifier()}
            };
            var configuration = new Unity.Services.Economy.Internal.Configuration(null, null, null, headers);

            IAccessToken accessToken = registry.GetServiceComponent<IAccessToken>();

            ICurrenciesApiClient currenciesApiClient = new CurrenciesApiClient(httpClient, accessToken, configuration);
            IInventoryApiClient inventoryApiClient = new InventoryApiClient(httpClient, accessToken, configuration);
            IPurchasesApiClient purchasesApiClient = new PurchasesApiClient(httpClient, accessToken, configuration);

            Economy.InitializeEconomy(accessToken, registry.GetServiceComponent<IPlayerId>(), currenciesApiClient, inventoryApiClient, purchasesApiClient);
            
            return Task.CompletedTask;
        }
    }
}
