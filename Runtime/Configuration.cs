using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Economy.Internal;
using Unity.Services.Economy.Internal.Apis.InternalConfiguration;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Economy.Internal.InternalConfiguration;
using Unity.Services.Economy.Internal.Models;
using Unity.Services.Economy.Model;
using UnityEngine;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Tests.Runtime")]

namespace Unity.Services.Economy
{
    /// <summary>
    /// This class allows you to retrieve items from the global economy configuration as it is set up in the Unity Dashboard.
    /// </summary>
    public interface IEconomyConfigurationApiClient
    {
        /// <summary>
        /// Gets the currently published Economy configuration and caches it.
        /// </summary>
        /// <returns>A list of ConfigurationItemDefinition</returns>
        /// <exception cref="EconomyException">>Thrown if request is unsuccessful</exception>
        Task<List<ConfigurationItemDefinition>> SyncConfigurationAsync();

        /// <summary>
        /// Gets the currencies from the cached configuration.
        /// </summary>
        /// <returns>A list of CurrencyDefinition</returns>
        /// <exception cref="EconomyException">>Thrown if request is unsuccessful</exception>
        List<CurrencyDefinition> GetCurrencies();

        /// <summary>
        /// Gets the inventory items from the cached configuration.
        /// </summary>
        /// <returns>A list of InventoryItemDefinition</returns>
        /// <exception cref="EconomyException">>Thrown if request is unsuccessful</exception>
        List<InventoryItemDefinition> GetInventoryItems();

        /// <summary>
        /// Gets the virtual purchases from the cached configuration.
        /// </summary>
        /// <returns>A list of VirtualPurchaseDefinition</returns>
        /// <exception cref="EconomyException">>Thrown if request is unsuccessful</exception>
        List<VirtualPurchaseDefinition> GetVirtualPurchases();

        /// <summary>
        /// Gets the real money purchases from the cached configuration.
        /// </summary>
        /// <returns>A list of RealMoneyPurchaseDefinition</returns>
        /// <exception cref="EconomyException">>Thrown if request is unsuccessful</exception>
        List<RealMoneyPurchaseDefinition> GetRealMoneyPurchases();

        /// <summary>
        /// Gets a specific currency from the cached config.
        /// </summary>
        /// <param name="id">The ID of the currency to fetch.</param>
        /// <returns>A CurrencyDefinition or null if the currency doesn't exist.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        CurrencyDefinition GetCurrency(string id);

        /// <summary>
        /// Gets a specific inventory item from the cached config.
        /// </summary>
        /// <param name="id">The ID of the inventory item to fetch.</param>
        /// <returns>A InventoryItemDefinition or null if the currency doesn't exist.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        InventoryItemDefinition GetInventoryItem(string id);

        /// <summary>
        /// Gets a specific virtual purchase from the cached config.
        /// </summary>
        /// <param name="id">The ID of the virtual purchase to fetch.</param>
        /// <returns>A VirtualPurchaseDefinition or null if the currency doesn't exist.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        VirtualPurchaseDefinition GetVirtualPurchase(string id);

        /// <summary>
        /// Gets a specific real money purchase from the cached config.
        /// </summary>
        /// <param name="id">The ID of the real money purchase to fetch.</param>
        /// <returns>A RealMoneyPurchaseDefinition or null if the currency doesn't exist.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        RealMoneyPurchaseDefinition GetRealMoneyPurchase(string id);

        /// <summary>
        /// Returns the cached config assignment hash.
        /// </summary>
        /// <returns>A config assignment hash or null if Economy doesn't have one</returns>
        string GetConfigAssignmentHash();

        /// <summary>
        /// Gets the Currencies that have been configured and published in the Unity Dashboard.
        /// </summary>
        /// <returns>A list of CurrencyDefinition</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("GetCurrenciesAsync has been replaced by first caching your configuration using SyncConfigurationAsync and then using the GetCurrencies method. This API will be removed in an upcoming release.", false)]
        Task<List<CurrencyDefinition>> GetCurrenciesAsync();

        /// <summary>
        /// Gets a Currency Definition for a specific currency.
        /// </summary>
        /// <param name="id">The configuration ID of the currency to fetch.</param>
        /// <returns>A CurrencyDefinition for the specified currency, or null if the currency doesn't exist.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("GetCurrencyAsync has been replaced by first caching your configuration using SyncConfigurationAsync and then using the GetCurrency method. This API will be removed in an upcoming release.", false)]
        Task<CurrencyDefinition> GetCurrencyAsync(string id);

        /// <summary>
        /// Gets the Inventory Items that have been configured and published in the Unity Dashboard.
        /// </summary>
        /// <returns>A list of InventoryItemDefinition</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("GetInventoryItemsAsync has been replaced by first caching your configuration using SyncConfigurationAsync and then using the GetInventoryItems method. This API will be removed in an upcoming release.", false)]
        Task<List<InventoryItemDefinition>> GetInventoryItemsAsync();

        /// <summary>
        /// Gets a InventoryItemDefinition for a specific currency.
        /// </summary>
        /// <param name="id">The configuration ID of the item to fetch.</param>
        /// <returns>A InventoryItemDefinition for the specified currency, or null if the currency doesn't exist.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("GetInventoryItemAsync has been replaced by first caching your configuration using SyncConfigurationAsync and then using the GetInventoryItem method. This API will be removed in an upcoming release.", false)]
        Task<InventoryItemDefinition> GetInventoryItemAsync(string id);

        /// <summary>
        /// Gets all the virtual purchases currently configured and published in the Economy Dashboard.
        ///
        /// Note that this will also fetch all associated Inventory Items/Currencies associated with the purchase.
        /// </summary>
        /// <returns>A list of VirtualPurchaseDefinition</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("GetVirtualPurchasesAsync has been replaced by first caching your configuration using SyncConfigurationAsync and then using the GetVirtualPurchases method. This API will be removed in an upcoming release.", false)]
        Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync();

        /// <summary>
        /// Gets a VirtualPurchaseDefinition for a specific virtual purchase.
        ///
        /// Note that this will also fetch the associated Inventory Items/Currencies associated with this purchase.
        /// </summary>
        /// <param name="id">The ID of the purchase to retrieve</param>
        /// <returns>A VirtualPurchaseDefinition for the specified purchase if it exists, or null otherwise.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("GetVirtualPurchaseAsync has been replaced by first caching your configuration using SyncConfigurationAsync and then using the GetVirtualPurchase method. This API will be removed in an upcoming release.", false)]
        Task<VirtualPurchaseDefinition> GetVirtualPurchaseAsync(string id);

        /// <summary>
        /// Gets all the real money purchases currently configured and published in the Economy Dashboard.
        /// </summary>
        /// <returns>A list of RealMoneyPurchaseDefinition</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("GetRealMoneyPurchasesAsync has been replaced by first caching your configuration using SyncConfigurationAsync and then using the GetRealMoneyPurchases method. This API will be removed in an upcoming release.", false)]
        Task<List<RealMoneyPurchaseDefinition>> GetRealMoneyPurchasesAsync();

        /// <summary>
        /// Gets a RealMoneyPurchaseDefinition for a specific real money purchase.
        /// </summary>
        /// <param name="id">The ID of the purchase to retrieve</param>
        /// <returns>A RealMoneyPurchaseDefinition for the specified purchase if it exists, or null otherwise.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("GetRealMoneyPurchaseAsync has been replaced by first caching your configuration using SyncConfigurationAsync and then using the GetRealMoneyPurchase method. This API will be removed in an upcoming release.", false)]
        Task<RealMoneyPurchaseDefinition> GetRealMoneyPurchaseAsync(string id);
    }

    class ConfigurationInternal : IEconomyConfigurationApiClient
    {
        private List<ConfigurationItemDefinition> m_CachedConfig;
        private bool m_HasSynced;

        private readonly ICloudProjectId m_CloudProjectId;
        private readonly IInternalConfigurationApiClient m_InternalConfigurationApiClient;
        private readonly IEconomyAuthentication m_EconomyAuthentication;

        // Names matching the Type property on a PlayerConfigurationResponseResultsOneOf
        private const string k_CurrencyTypeString = "CurrencyResource";
        private const string k_InventoryItemTypeString = "InventoryItemResource";
        private const string k_VirtualPurchaseTypeString = "VirtualPurchaseResource";
        private const string k_RealMoneyPurchaseTypeString = "RealMoneyPurchaseResource";

        // Names matching the Type property on a ConfigurationItemDefinition
        internal const string CurrencyType = "CURRENCY";
        internal const string InventoryItemType = "INVENTORY_ITEM";
        internal const string VirtualPurchaseType = "VIRTUAL_PURCHASE";
        internal const string RealMoneyPurchaseType = "MONEY_PURCHASE";

        private enum PurchaseItemType
        {
            Costs,
            Rewards
        }

        internal ConfigurationInternal(ICloudProjectId cloudProjectId, IInternalConfigurationApiClient configurationApiClient, IEconomyAuthentication economyAuthWrapper)
        {
            m_CloudProjectId = cloudProjectId;
            m_InternalConfigurationApiClient = configurationApiClient;
            m_EconomyAuthentication = economyAuthWrapper;
        }

        public async Task<List<ConfigurationItemDefinition>> SyncConfigurationAsync()
        {
            m_EconomyAuthentication.CheckSignedIn();

            GetPlayerConfigurationRequest request = new GetPlayerConfigurationRequest(
                m_CloudProjectId.GetCloudProjectId(),
                m_EconomyAuthentication.GetPlayerId(),
                null,
                m_EconomyAuthentication.GetUnityInstallationId(),
                m_EconomyAuthentication.GetAnalyticsUserId()
            );

            try
            {
                Response<PlayerConfigurationResponse> response = await m_InternalConfigurationApiClient.GetPlayerConfigurationAsync(request);

                List<ConfigurationItemDefinition> convertedResources = ConvertResources(response.Result.Results);

                ConfigurationMetadata convertedMetadata = new ConfigurationMetadata(response.Result.Metadata.ConfigAssignmentHash);

                // Update the config assignment hash stored in the SDK every time we fetch a config
                m_EconomyAuthentication.configAssignmentHash = convertedMetadata.ConfigAssignmentHash;
                GetConfigurationResult result = new GetConfigurationResult(convertedResources, convertedMetadata);
                m_CachedConfig = result.Results;
                m_HasSynced = true;

                return result.Results;
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }

        public List<CurrencyDefinition> GetCurrencies()
        {
            CheckHasSynced();

            List<CurrencyDefinition> currencies = new List<CurrencyDefinition>();
            foreach (var configItem in m_CachedConfig)
            {
                if (configItem.Type == CurrencyType)
                {
                    currencies.Add((CurrencyDefinition)configItem);
                }
            }

            return currencies;
        }

        public List<InventoryItemDefinition> GetInventoryItems()
        {
            CheckHasSynced();

            List<InventoryItemDefinition> inventoryItems = new List<InventoryItemDefinition>();
            foreach (var configItem in m_CachedConfig)
            {
                if (configItem.Type == InventoryItemType)
                {
                    inventoryItems.Add((InventoryItemDefinition)configItem);
                }
            }

            return inventoryItems;
        }

        public List<VirtualPurchaseDefinition> GetVirtualPurchases()
        {
            CheckHasSynced();

            List<VirtualPurchaseDefinition> virtualPurchases = new List<VirtualPurchaseDefinition>();
            foreach (var configItem in m_CachedConfig)
            {
                if (configItem.Type == VirtualPurchaseType)
                {
                    virtualPurchases.Add((VirtualPurchaseDefinition)configItem);
                }
            }

            return virtualPurchases;
        }

        public List<RealMoneyPurchaseDefinition> GetRealMoneyPurchases()
        {
            CheckHasSynced();

            List<RealMoneyPurchaseDefinition> realMoneyPurchases = new List<RealMoneyPurchaseDefinition>();
            foreach (var configItem in m_CachedConfig)
            {
                if (configItem.Type == RealMoneyPurchaseType)
                {
                    realMoneyPurchases.Add((RealMoneyPurchaseDefinition)configItem);
                }
            }

            return realMoneyPurchases;
        }

        public CurrencyDefinition GetCurrency(string id)
        {
            return GetConfigurationItemFromCache<CurrencyDefinition>(id);
        }

        public InventoryItemDefinition GetInventoryItem(string id)
        {
            return GetConfigurationItemFromCache<InventoryItemDefinition>(id);
        }

        public VirtualPurchaseDefinition GetVirtualPurchase(string id)
        {
            return GetConfigurationItemFromCache<VirtualPurchaseDefinition>(id);
        }

        public RealMoneyPurchaseDefinition GetRealMoneyPurchase(string id)
        {
            return GetConfigurationItemFromCache<RealMoneyPurchaseDefinition>(id);
        }

        private T GetConfigurationItemFromCache<T>(string id)
        {
            CheckHasSynced();

            foreach (var configItem in m_CachedConfig)
            {
                if (configItem.Id == id)
                {
                    return (T)Convert.ChangeType(configItem, typeof(T));
                }
            }

            return default;
        }

        private void CheckHasSynced()
        {
            if (m_HasSynced) return;

            throw new EconomyException(EconomyExceptionReason.ConfigNotSynced, 4, "You have not synced your configuration yet. Call SyncConfigurationAsync() at least once before calling the other configuration methods.");
        }

        /// <summary>
        /// Gets the all resources in the Economy configuration.
        /// </summary>
        /// <returns>A GetConfigurationResult</returns>
        /// <exception cref="EconomyException"></exception>
        [Obsolete("GetConfigurationAsync should not be used anymore, instead you should use the SyncConfigurations workflow. " +
            "This method is still needed for other deprecated methods. This API will be removed in an upcoming release.", false)]
        private async Task<GetConfigurationResult> GetConfigurationAsync()
        {
            m_EconomyAuthentication.CheckSignedIn();

            GetPlayerConfigurationRequest request = new GetPlayerConfigurationRequest(
                m_CloudProjectId.GetCloudProjectId(),
                m_EconomyAuthentication.GetPlayerId(),
                m_EconomyAuthentication.configAssignmentHash,
                m_EconomyAuthentication.GetUnityInstallationId(),
                m_EconomyAuthentication.GetAnalyticsUserId()
            );

            try
            {
                Response<PlayerConfigurationResponse> response = await m_InternalConfigurationApiClient.GetPlayerConfigurationAsync(request);

                List<ConfigurationItemDefinition> convertedResources = ConvertResources(response.Result.Results);
                ConfigurationMetadata convertedMetadata = new ConfigurationMetadata(response.Result.Metadata.ConfigAssignmentHash);
                // Update the config assignment hash stored in the SDK every time we fetch a config
                m_EconomyAuthentication.configAssignmentHash = convertedMetadata.ConfigAssignmentHash;
                GetConfigurationResult result = new GetConfigurationResult(convertedResources, convertedMetadata);

                return result;
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }

        public async Task<List<CurrencyDefinition>> GetCurrenciesAsync()
        {
            var configItems = await SyncConfigurationAsync();

            List<CurrencyDefinition> currencies = new List<CurrencyDefinition>();
            foreach (var configItem in configItems)
            {
                if (configItem.Type == CurrencyType)
                {
                    currencies.Add((CurrencyDefinition)configItem);
                }
            }

            return currencies;
        }

        public async Task<List<InventoryItemDefinition>> GetInventoryItemsAsync()
        {
            var configItems = await SyncConfigurationAsync();

            List<InventoryItemDefinition> inventoryItems = new List<InventoryItemDefinition>();
            foreach (var configItem in configItems)
            {
                if (configItem.Type == InventoryItemType)
                {
                    inventoryItems.Add((InventoryItemDefinition)configItem);
                }
            }

            return inventoryItems;
        }

        public async Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync()
        {
            var configItems = await SyncConfigurationAsync();

            List<VirtualPurchaseDefinition> virtualPurchases = new List<VirtualPurchaseDefinition>();
            foreach (var configItem in configItems)
            {
                if (configItem.Type == VirtualPurchaseType)
                {
                    virtualPurchases.Add((VirtualPurchaseDefinition)configItem);
                }
            }

            return virtualPurchases;
        }

        public async Task<List<RealMoneyPurchaseDefinition>> GetRealMoneyPurchasesAsync()
        {
            var configItems = await SyncConfigurationAsync();

            List<RealMoneyPurchaseDefinition> realMoneyPurchases = new List<RealMoneyPurchaseDefinition>();
            foreach (var configItem in configItems)
            {
                if (configItem.Type == RealMoneyPurchaseType)
                {
                    realMoneyPurchases.Add((RealMoneyPurchaseDefinition)configItem);
                }
            }

            return realMoneyPurchases;
        }

        public async Task<CurrencyDefinition> GetCurrencyAsync(string id)
        {
            return await GetConfigurationItem<CurrencyDefinition>(id);
        }

        public async Task<InventoryItemDefinition> GetInventoryItemAsync(string id)
        {
            return await GetConfigurationItem<InventoryItemDefinition>(id);
        }

        public async Task<VirtualPurchaseDefinition> GetVirtualPurchaseAsync(string id)
        {
            return await GetConfigurationItem<VirtualPurchaseDefinition>(id);
        }

        public async Task<RealMoneyPurchaseDefinition> GetRealMoneyPurchaseAsync(string id)
        {
            return await GetConfigurationItem<RealMoneyPurchaseDefinition>(id);
        }

        private async Task<T> GetConfigurationItem<T>(string id)
        {
            var configItems = await SyncConfigurationAsync();

            foreach (var configItem in configItems)
            {
                if (configItem.Id == id)
                {
                    return (T)Convert.ChangeType(configItem, typeof(T));
                }
            }

            return default;
        }

        public string GetConfigAssignmentHash()
        {
            return m_EconomyAuthentication.configAssignmentHash;
        }

        /// <summary>
        /// Takes the response we get from the service when retrieving the Economy config and converts it into the
        /// equivalent SDK models.
        /// </summary>
        /// <param name="results">The list of configuration items received from the service</param>
        /// <returns>A list of ConfigurationItemDefinitions</returns>
        internal List<ConfigurationItemDefinition> ConvertResources(List<PlayerConfigurationResponseResultsOneOf> results)
        {
            List<ConfigurationItemDefinition> convertedResources = new List<ConfigurationItemDefinition>();

            foreach (var resource in results)
            {
                switch (resource.Type.Name)
                {
                    case k_CurrencyTypeString:
                        {
                            var baseResource = (CurrencyResource)resource.Value;
                            var convertedResource = new CurrencyDefinition(baseResource);
                            convertedResources.Add(convertedResource);
                        }
                        break;
                    case k_InventoryItemTypeString:
                        {
                            var baseResource = (InventoryItemResource)resource.Value;
                            var convertedResource = new InventoryItemDefinition(baseResource);
                            convertedResources.Add(convertedResource);
                        }
                        break;
                    case k_VirtualPurchaseTypeString:
                        {
                            var baseResource = (VirtualPurchaseResource)resource.Value;
                            var convertedResource = new VirtualPurchaseDefinition(baseResource);
                            convertedResources.Add(convertedResource);
                        }
                        break;
                    case k_RealMoneyPurchaseTypeString:
                        {
                            var baseResource = (RealMoneyPurchaseResource)resource.Value;
                            var convertedResource = new RealMoneyPurchaseDefinition(baseResource);
                            convertedResources.Add(convertedResource);
                        }
                        break;
                }
            }

            ProcessReferences(convertedResources);

            return convertedResources;
        }


        /// <summary>
        /// Takes the virtual/real money purchase received from the service in JSON format and returns its Costs/Rewards which is a list
        /// of PurchaseItemQuantity.
        /// </summary>
        /// <param name="purchaseJson">The virtual/real money purchase received from the service in JSON format</param>
        /// <param name="purchaseItemType">Specifies whether you are setting up Costs or Rewards</param>
        /// <param name="rawConfig">The entire raw config response received from the service.
        /// This is required to set up the Costs/Rewards of the purchase.</param>
        /// <returns></returns>
        /// <exception cref="EconomyException"></exception>
        private void ProcessReferences(List<ConfigurationItemDefinition> definitions)
        {
            foreach (var definition in definitions)
            {
                switch (definition.Type)
                {
                    case VirtualPurchaseType:
                        var virtualPurchaseDefinition = (VirtualPurchaseDefinition)definition;
                        ProcessReferences(definitions, virtualPurchaseDefinition.Rewards);
                        ProcessReferences(definitions, virtualPurchaseDefinition.Costs);
                        break;
                    case RealMoneyPurchaseType:
                        var realMoneyPurchaseDefinition = (RealMoneyPurchaseDefinition)definition;
                        ProcessReferences(definitions, realMoneyPurchaseDefinition.Rewards);
                        break;
                }
            }
        }

        private void ProcessReferences(List<ConfigurationItemDefinition> definitions, List<PurchaseItemQuantity> items)
        {
            foreach (var item in items)
            {
                item.Item = new EconomyReference(GetReference(item.ResourceId, definitions));
            }
        }

        private ConfigurationItemDefinition GetReference(string resourceId, List<ConfigurationItemDefinition> definitions)
        {
            foreach (var definition in definitions)
            {
                if (definition.Id == resourceId)
                    return definition;
            }

            throw new EconomyException(EconomyExceptionReason.UnprocessableTransaction, 422, "Failed to find the reference in the Economy configuration.");
        }
    }
}
