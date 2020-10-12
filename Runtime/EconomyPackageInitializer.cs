using System.Threading.Tasks;
using Unity.Services.Economy.Internal.Apis.Currencies;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Apis.Purchases;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Authentication.Internal;
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
                .DependsOn<IPlayerId>();
        }
        
        public Task Initialize(CoreRegistry registry)
        {
            var httpClient = new HttpClient();

            ICurrenciesApiClient currenciesApiClient = new CurrenciesApiClient(httpClient);
            IInventoryApiClient inventoryApiClient = new InventoryApiClient(httpClient);
            IPurchasesApiClient purchasesApiClient = new PurchasesApiClient(httpClient);

            Economy.InitializeEconomy(registry.GetServiceComponent<IAccessToken>(), registry.GetServiceComponent<IPlayerId>(), currenciesApiClient, inventoryApiClient, purchasesApiClient);
            
            return Task.CompletedTask;
        }
    }
}
