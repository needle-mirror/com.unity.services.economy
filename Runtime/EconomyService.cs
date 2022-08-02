using System.Runtime.CompilerServices;
using Unity.Services.Economy.Internal.Apis.Currencies;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Apis.Purchases;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core;
using Unity.Services.Economy.Internal.Apis.Configuration;
using Unity.Services.Core.Configuration.Internal;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Tests")]

// This allows the Economy test to see the generated SDK classes/models etc.
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Unity.Services.Economy
{
    public interface IEconomyService
    {
        IEconomyConfigurationApiClient Configuration { get; }
        IEconomyPlayerBalancesApiClient PlayerBalances { get; }
        IEconomyPlayerInventoryApiClient PlayerInventory { get; }
        IEconomyPurchasesApiClientApi Purchases { get; }
    }

    public class EconomyInstance : IEconomyService
    {
        /// <summary>
        /// The Configuration methods allow you to access the global Economy configuration for your game.
        /// </summary>
        public IEconomyConfigurationApiClient Configuration { get; internal set; }

        /// <summary>
        /// The PlayerBalances methods provide access to the current player's balances, and allow you to update them.
        /// </summary>
        public IEconomyPlayerBalancesApiClient PlayerBalances { get; }

        /// <summary>
        /// The PlayerInventory methods provide access to the current player's inventory items, and allow you to update them.
        /// </summary>
        public IEconomyPlayerInventoryApiClient PlayerInventory { get; }

        /// <summary>
        /// The Purchases methods allow you to make virtual and real world purchases.
        /// </summary>
        public IEconomyPurchasesApiClientApi Purchases { get; }

        internal EconomyInstance(ConfigurationInternal configuration, PlayerBalancesInternal playerBalances, PlayerInventoryInternal playerInventory,
                                 PurchasesInternal purchases)
        {
            Configuration = configuration;
            PlayerBalances = playerBalances;
            PlayerInventory = playerInventory;
            Purchases = purchases;
        }
    }

    public static class EconomyService
    {
        internal static IEconomyService instance;

        public static IEconomyService Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new ServicesInitializationException("The Economy service has not been initialized. Please initialize Unity Services.");
                }

                return instance;
            }
        }

        internal static void InitializeEconomy(ICloudProjectId cloudProjectId, IAccessToken accessToken, IPlayerId playerId, IConfigurationApiClient configurationApiClient, ICurrenciesApiClient currenciesApiClient, IInventoryApiClient inventoryApiClient, IPurchasesApiClient purchasesApiClient)
        {
            IEconomyAuthentication economyAuth = new EconomyAuthentication(playerId, accessToken);

            ConfigurationInternal configurationInternal = new ConfigurationInternal(configurationApiClient, economyAuth);
            PlayerBalancesInternal playerBalancesInternal = new PlayerBalancesInternal(cloudProjectId, currenciesApiClient, economyAuth);
            PlayerInventoryInternal playerInventoryInternal = new PlayerInventoryInternal(cloudProjectId, inventoryApiClient, economyAuth);
            PurchasesInternal purchasesInternal = new PurchasesInternal(cloudProjectId, purchasesApiClient, economyAuth, playerBalancesInternal, playerInventoryInternal);

            instance = new EconomyInstance(configurationInternal, playerBalancesInternal, playerInventoryInternal, purchasesInternal);

            // Used to support the old static interface
            Economy.Configuration = new Configuration();
            Economy.PlayerBalances = new PlayerBalances();
            Economy.PlayerInventory = new PlayerInventory();
            Economy.Purchases = new Purchases();
        }
    }
}
