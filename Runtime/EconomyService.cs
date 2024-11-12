using System.Runtime.CompilerServices;
using Unity.Services.Economy.Internal.Apis.Currencies;
using Unity.Services.Economy.Internal.Apis.InternalConfiguration;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Apis.Purchases;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core;
using Unity.Services.Core.Configuration.Internal;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Tests")]

// This allows the Economy test to see the generated SDK classes/models etc.
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Unity.Services.Economy
{
    /// <summary> Interface to the economy service </summary>
    public interface IEconomyService
    {
        /// <summary> The Configuration methods allow you to access the global Economy configuration for your game. </summary>
        IEconomyConfigurationApiClient Configuration { get; }
        /// <summary> The PlayerBalances methods provide access to the current player's balances, and allow you to update them.</summary>
        IEconomyPlayerBalancesApiClient PlayerBalances { get; }
        /// <summary> The PlayerInventory methods provide access to the current player's inventory items, and allow you to update them. </summary>
        IEconomyPlayerInventoryApiClient PlayerInventory { get; }
        /// <summary> The Purchases methods allow you to make virtual and real world purchases. </summary>
        IEconomyPurchasesApiClientApi Purchases { get; }
    }

    /// <inheritdoc />
    public class EconomyInstance : IEconomyService
    {
        /// <inheritdoc />
        public IEconomyConfigurationApiClient Configuration { get; internal set; }

        /// <inheritdoc />
        public IEconomyPlayerBalancesApiClient PlayerBalances { get; }

        /// <inheritdoc />
        public IEconomyPlayerInventoryApiClient PlayerInventory { get; }

        /// <inheritdoc />
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

    /// <summary>Provides access to the economy service</summary>
    public static class EconomyService
    {
        internal static IEconomyService instance;

        /// <summary>Singleton of the Economy Service </summary>
        /// <exception cref="ServicesInitializationException">Throws ServicesInitializationException if the service has not been initailized</exception>
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
    }
}
