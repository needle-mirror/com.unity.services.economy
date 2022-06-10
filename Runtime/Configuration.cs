using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Unity.Services.Economy.Model;
using UnityEngine;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Tests")]

namespace Unity.Services.Economy
{
    /// <summary>
    /// This class allows you to retrieve items from the global economy configuration as it is set up in the Unity Dashboard.
    /// </summary>
    public interface IEconomyConfigurationApiClient
    {
        /// <summary>
        /// Gets the Currencies that have been configured and published in the Unity Dashboard.
        /// </summary>
        /// <returns>A list of CurrencyDefinition</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        Task<List<CurrencyDefinition>> GetCurrenciesAsync();

        /// <summary>
        /// Gets a Currency Definition for a specific currency.
        /// </summary>
        /// <param name="id">The configuration ID of the currency to fetch.</param>
        /// <returns>A CurrencyDefinition for the specified currency, or null if the currency doesn't exist.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        Task<CurrencyDefinition> GetCurrencyAsync(string id);

        /// <summary>
        /// Gets the Inventory Items that have been configured and published in the Unity Dashboard.
        /// </summary>
        /// <returns>A list of InventoryItemDefinition</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        Task<List<InventoryItemDefinition>> GetInventoryItemsAsync();

        /// <summary>
        /// Gets a InventoryItemDefinition for a specific currency.
        /// </summary>
        /// <param name="id">The configuration ID of the item to fetch.</param>
        /// <returns>A InventoryItemDefinition for the specified currency, or null if the currency doesn't exist.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        Task<InventoryItemDefinition> GetInventoryItemAsync(string id);

        /// <summary>
        /// Gets all the virtual purchases currently configured and published in the Economy Dashboard.
        ///
        /// Note that this will also fetch all associated Inventory Items/Currencies associated with the purchase.
        /// </summary>
        /// <returns>A list of VirtualPurchaseDefinition</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync();

        /// <summary>
        /// Gets a VirtualPurchaseDefinition for a specific virtual purchase.
        ///
        /// Note that this will also fetch the associated Inventory Items/Currencies associated with this purchase.
        /// </summary>
        /// <param name="id">The ID of the purchase to retrieve</param>
        /// <returns>A VirtualPurchaseDefinition for the specified purchase if it exists, or null otherwise.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        Task<VirtualPurchaseDefinition> GetVirtualPurchaseAsync(string id);

        /// <summary>
        /// Gets all the real money purchases currently configured and published in the Economy Dashboard.
        /// </summary>
        /// <returns>A list of RealMoneyPurchaseDefinition</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        Task<List<RealMoneyPurchaseDefinition>> GetRealMoneyPurchasesAsync();

        /// <summary>
        /// Gets a RealMoneyPurchaseDefinition for a specific real money purchase.
        /// </summary>
        /// <param name="id">The ID of the purchase to retrieve</param>
        /// <returns>A RealMoneyPurchaseDefinition for the specified purchase if it exists, or null otherwise.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        Task<RealMoneyPurchaseDefinition> GetRealMoneyPurchaseAsync(string id);

        /// <summary>
        /// Returns the most up to date config assignment hash that Economy has.
        /// </summary>
        /// <returns>A config assignment hash or null if Economy doesn't have one</returns>
        string GetConfigAssignmentHash();
    }

    class ConfigurationInternal : IEconomyConfigurationApiClient
    {
        internal EconomyRemoteConfig remoteConfig;

        internal ConfigurationInternal(IEconomyAuthentication economyAuthHandler)
        {
            remoteConfig = new EconomyRemoteConfig(new RemoteConfigRuntimeNonStaticWrapper(), economyAuthHandler);
        }

        public async Task<List<CurrencyDefinition>> GetCurrenciesAsync()
        {
            return await remoteConfig.GetObjectsFromRemoteConfigAsync<CurrencyDefinition>(EconomyRemoteConfig.currencyTypeString);
        }

        public async Task<CurrencyDefinition> GetCurrencyAsync(string id)
        {
            CurrencyDefinition currency = (CurrencyDefinition)await remoteConfig.GetEconomyItemWithKeyUsingRefreshAsync(id);
            if (currency == null)
            {
                return null;
            }
            return currency;
        }

        public async Task<List<InventoryItemDefinition>> GetInventoryItemsAsync()
        {
            return await remoteConfig.GetObjectsFromRemoteConfigAsync<InventoryItemDefinition>(EconomyRemoteConfig.inventoryItemTypeString);
        }

        public async Task<InventoryItemDefinition> GetInventoryItemAsync(string id)
        {
            InventoryItemDefinition inventoryItem = (InventoryItemDefinition)await remoteConfig.GetEconomyItemWithKeyUsingRefreshAsync(id);
            if (inventoryItem == null)
            {
                return null;
            }
            return inventoryItem;
        }

        public async Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync()
        {
            return await remoteConfig.GetObjectsFromRemoteConfigAsync<VirtualPurchaseDefinition>(EconomyRemoteConfig.virtualPurchaseTypeString);
        }

        public async Task<VirtualPurchaseDefinition> GetVirtualPurchaseAsync(string id)
        {
            return (VirtualPurchaseDefinition)await remoteConfig.GetEconomyItemWithKeyUsingRefreshAsync(id);
        }

        public async Task<List<RealMoneyPurchaseDefinition>> GetRealMoneyPurchasesAsync()
        {
            return await remoteConfig.GetObjectsFromRemoteConfigAsync<RealMoneyPurchaseDefinition>(EconomyRemoteConfig.realMoneyPurchaseTypeString);
        }

        public async Task<RealMoneyPurchaseDefinition> GetRealMoneyPurchaseAsync(string id)
        {
            return (RealMoneyPurchaseDefinition)await remoteConfig.GetEconomyItemWithKeyUsingRefreshAsync(id);
        }

        public string GetConfigAssignmentHash()
        {
            return remoteConfig.economyAuthHandler.configAssignmentHash;
        }
    }
}
