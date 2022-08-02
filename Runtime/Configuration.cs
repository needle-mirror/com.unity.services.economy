using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.Services.Economy.Internal;
using Unity.Services.Economy.Internal.Apis.Configuration;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Economy.Internal.Models;
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
        // Names matching the Type property on a PlayerConfigurationResponseResultsOneOf
        private const string k_CurrencyTypeString = "CurrencyResource";
        private const string k_InventoryItemTypeString = "InventoryItemResource";
        private const string k_VirtualPurchaseTypeString = "VirtualPurchaseResource";
        private const string k_RealMoneyPurchaseTypeString = "RealMoneyPurchaseResource";

        // Names matching the Type property on a ConfigurationItemDefinition
        internal const string currencySdkModelTypeString = "CURRENCY";
        internal const string inventoryItemSdkModelTypeString = "INVENTORY_ITEM";
        internal const string virtualPurchaseSdkModelTypeString = "VIRTUAL_PURCHASE";
        internal const string realMoneyPurchaseSdkModelTypeString = "MONEY_PURCHASE";

        internal enum PurchaseItemType
        {
            Costs,
            Rewards
        }

        readonly IConfigurationApiClient m_ConfigurationApiClient;
        readonly IEconomyAuthentication m_EconomyAuthentication;

        internal ConfigurationInternal(IConfigurationApiClient configurationApiClient, IEconomyAuthentication economyAuthWrapper)
        {
            m_ConfigurationApiClient = configurationApiClient;
            m_EconomyAuthentication = economyAuthWrapper;
        }

        /// <summary>
        /// Gets the all resources in the Economy configuration.
        /// </summary>
        /// <returns>A GetConfigurationResult</returns>
        /// <exception cref="EconomyException"></exception>
        private async Task<GetConfigurationResult> GetConfigurationAsync()
        {
            m_EconomyAuthentication.CheckSignedIn();

            GetPlayerConfigurationRequest request = new GetPlayerConfigurationRequest(
                Application.cloudProjectId,
                m_EconomyAuthentication.GetPlayerId(),
                m_EconomyAuthentication.configAssignmentHash
            );

            try
            {
                Response<PlayerConfigurationResponse> response = await m_ConfigurationApiClient.GetPlayerConfigurationAsync(request);

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
            GetConfigurationResult config = await GetConfigurationAsync();

            List<CurrencyDefinition> currencies = new List<CurrencyDefinition>();
            foreach (var configItem in config.Results)
            {
                if (configItem.Type == currencySdkModelTypeString)
                {
                    currencies.Add((CurrencyDefinition)configItem);
                }
            }

            return currencies;
        }

        public async Task<List<InventoryItemDefinition>> GetInventoryItemsAsync()
        {
            GetConfigurationResult config = await GetConfigurationAsync();

            List<InventoryItemDefinition> inventoryItems = new List<InventoryItemDefinition>();
            foreach (var configItem in config.Results)
            {
                if (configItem.Type == inventoryItemSdkModelTypeString)
                {
                    inventoryItems.Add((InventoryItemDefinition)configItem);
                }
            }

            return inventoryItems;
        }

        public async Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync()
        {
            GetConfigurationResult config = await GetConfigurationAsync();

            List<VirtualPurchaseDefinition> virtualPurchases = new List<VirtualPurchaseDefinition>();
            foreach (var configItem in config.Results)
            {
                if (configItem.Type == virtualPurchaseSdkModelTypeString)
                {
                    virtualPurchases.Add((VirtualPurchaseDefinition)configItem);
                }
            }

            return virtualPurchases;
        }

        public async Task<List<RealMoneyPurchaseDefinition>> GetRealMoneyPurchasesAsync()
        {
            GetConfigurationResult config = await GetConfigurationAsync();

            List<RealMoneyPurchaseDefinition> realMoneyPurchases = new List<RealMoneyPurchaseDefinition>();
            foreach (var configItem in config.Results)
            {
                if (configItem.Type == realMoneyPurchaseSdkModelTypeString)
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
            GetConfigurationResult config = await GetConfigurationAsync();

            foreach (var configItem in config.Results)
            {
                if (configItem.Id == id)
                {
                    return (T)Convert.ChangeType(configItem, typeof(T));
                }
            }

            Debug.Log("The provided economy item key doesn't exist in the fetched configuration.");
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
                string serializedResourceValue = JsonConvert.SerializeObject(resource.Value);
                switch (resource.Type.Name)
                {
                    case k_CurrencyTypeString:
                        CurrencyDefinition convertedCurrency = JsonConvert.DeserializeObject<CurrencyDefinition>(serializedResourceValue);
                        SetCustomDataDeserializable(convertedCurrency);
                        convertedResources.Add(convertedCurrency);
                        break;
                    case k_InventoryItemTypeString:
                        InventoryItemDefinition convertedInventoryItem = JsonConvert.DeserializeObject<InventoryItemDefinition>(serializedResourceValue);
                        SetCustomDataDeserializable(convertedInventoryItem);
                        convertedResources.Add(convertedInventoryItem);
                        break;
                    case k_VirtualPurchaseTypeString:
                        VirtualPurchaseDefinition convertedVirtualPurchase = ConvertVirtualPurchaseRawResponse(serializedResourceValue, results);
                        SetCustomDataDeserializable(convertedVirtualPurchase);
                        convertedResources.Add(convertedVirtualPurchase);
                        break;
                    case k_RealMoneyPurchaseTypeString:
                        RealMoneyPurchaseDefinition convertedRealMoneyPurchase = ConvertRealMoneyPurchaseRawResponse(serializedResourceValue, results);
                        SetCustomDataDeserializable(convertedRealMoneyPurchase);
                        convertedResources.Add(convertedRealMoneyPurchase);
                        break;
                }
            }

            return convertedResources;
        }

        /// <summary>
        /// Sets the CustomDataDeserializable property on a config item.
        /// </summary>
        /// <param name="configItem">A ConfigurationItemDefinition</param>
        /// <returns>A ConfigurationItemDefinition</returns>
        private static void SetCustomDataDeserializable(ConfigurationItemDefinition configItem)
        {
            if (configItem == null) return;
            configItem.CustomDataDeserializable = new JsonObject(configItem.CustomData);
        }

        /// <summary>
        /// Converts a single virtual purchase received from the service and converts it into the equivalent SDK model.
        /// </summary>
        /// <param name="purchaseRawResponse">The serialized response from the service for a single virtual purchase</param>
        /// <param name="rawConfig">The entire raw config response received from the service.
        /// This is required to set up the Costs and Rewards of the purchase.</param>
        /// <returns>A VirtualPurchaseDefinition</returns>
        private VirtualPurchaseDefinition ConvertVirtualPurchaseRawResponse(string purchaseRawResponse, List<PlayerConfigurationResponseResultsOneOf> rawConfig)
        {
            VirtualPurchaseDefinition virtualPurchaseDefinition = new VirtualPurchaseDefinition();
            virtualPurchaseDefinition.Costs = new List<PurchaseItemQuantity>();
            virtualPurchaseDefinition.Rewards = new List<PurchaseItemQuantity>();

            JObject jObject = JObject.Parse(purchaseRawResponse);
            SetBasicConfigItemProperties(virtualPurchaseDefinition, jObject);
            virtualPurchaseDefinition.Costs = GetPurchaseItemQuantityList(jObject, PurchaseItemType.Costs, rawConfig);
            virtualPurchaseDefinition.Rewards = GetPurchaseItemQuantityList(jObject, PurchaseItemType.Rewards, rawConfig);

            return virtualPurchaseDefinition;
        }

        /// <summary>
        /// Converts a single real money purchase received from the service and converts it into the equivalent SDK model.
        /// </summary>
        /// <param name="purchaseRawResponse">The serialized response from the service for a single real money purchase</param>
        /// <param name="rawConfig">The entire raw config response received from the service.
        /// This is required to set up the Rewards of the purchase.</param>
        /// <returns></returns>
        private RealMoneyPurchaseDefinition ConvertRealMoneyPurchaseRawResponse(string purchaseRawResponse, List<PlayerConfigurationResponseResultsOneOf> rawConfig)
        {
            RealMoneyPurchaseDefinition realMoneyPurchaseDefinition = new RealMoneyPurchaseDefinition();
            realMoneyPurchaseDefinition.Rewards = new List<PurchaseItemQuantity>();

            JObject jObject = JObject.Parse(purchaseRawResponse);
            SetBasicConfigItemProperties(realMoneyPurchaseDefinition, jObject);
            realMoneyPurchaseDefinition.Rewards = GetPurchaseItemQuantityList(jObject, PurchaseItemType.Rewards, rawConfig);

            return realMoneyPurchaseDefinition;
        }

        /// <summary>
        /// Takes the a config item received from the service in JSON format and sets the base properties for a ConfigurationItemDefinition.
        /// </summary>
        /// <param name="itemDefinition">The ConfigurationItemDefinition to set</param>
        /// <param name="itemJson">The config item JSON received from the service</param>
        private static void SetBasicConfigItemProperties(ConfigurationItemDefinition itemDefinition, JObject itemJson)
        {
            itemDefinition.Id = itemJson["id"]?.Value<string>();
            itemDefinition.Name = itemJson["name"]?.Value<string>();
            itemDefinition.Type = itemJson["type"]?.Value<string>();
            itemDefinition.Created = itemJson["created"]?.ToObject<EconomyDate>();
            itemDefinition.Modified = itemJson["modified"]?.ToObject<EconomyDate>();
            itemDefinition.CustomData = itemJson["customData"]?.ToObject<Dictionary<string, object>>();
            itemDefinition.CustomDataDeserializable = new JsonObject(itemDefinition.CustomData);
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
        private List<PurchaseItemQuantity> GetPurchaseItemQuantityList(JObject purchaseJson, PurchaseItemType purchaseItemType,
            List<PlayerConfigurationResponseResultsOneOf> rawConfig)
        {
            List<PurchaseItemQuantity> purchaseItemQuantities = new List<PurchaseItemQuantity>();
            foreach (var purchaseItem in purchaseJson[purchaseItemType.ToString().ToLower()].Children())
            {
                string purchaseItemId = purchaseItem["resourceId"]?.ToString();
                int purchaseItemAmount = purchaseItem["amount"] !.ToObject<int>();
                PurchaseItemQuantity purchaseItemQuantity = GetPurchaseItemQuantity(purchaseItemId, purchaseItemAmount, rawConfig);
                if (purchaseItemQuantity == null)
                {
                    throw new EconomyException(EconomyExceptionReason.UnprocessableTransaction, 422, "Failed to find the relevant PurchaseItemQuantity in the Economy configuration.");
                }
                purchaseItemQuantities.Add(purchaseItemQuantity);
            }

            return purchaseItemQuantities;
        }

        /// <summary>
        /// This takes a resource Id and finds that resource in the raw config received from the service. It then returns
        /// this resource as a PurchaseItemQuantity, so it can be used in a purchases' Costs/Rewards.
        /// </summary>
        /// <param name="resourceId">The config item Id of the Cost/Reward</param>
        /// <param name="amount">The amount of the Cost/Reward</param>
        /// <param name="rawConfig">The entire raw config response received from the service.
        /// This is required to set up the Costs/Rewards of the purchase.</param>
        /// <returns></returns>
        private static PurchaseItemQuantity GetPurchaseItemQuantity(string resourceId, int amount, List<PlayerConfigurationResponseResultsOneOf> rawConfig)
        {
            PurchaseItemQuantity purchaseItemQuantity = new PurchaseItemQuantity { Amount = amount };

            foreach (var resource in rawConfig)
            {
                string serializedResourceValue = JsonConvert.SerializeObject(resource.Value);
                switch (resource.Type.Name)
                {
                    case k_CurrencyTypeString:
                    {
                        CurrencyDefinition convertedCurrency = JsonConvert.DeserializeObject<CurrencyDefinition>(serializedResourceValue);
                        if (convertedCurrency == null) return null;

                        if (convertedCurrency.Id == resourceId)
                        {
                            convertedCurrency.CustomDataDeserializable = new JsonObject(convertedCurrency.CustomData);
                            purchaseItemQuantity.Item = new EconomyReference(convertedCurrency);
                            return purchaseItemQuantity;
                        }

                        break;
                    }
                    case k_InventoryItemTypeString:
                    {
                        InventoryItemDefinition convertedItem = JsonConvert.DeserializeObject<InventoryItemDefinition>(serializedResourceValue);
                        if (convertedItem == null) return null;

                        if (convertedItem.Id == resourceId)
                        {
                            convertedItem.CustomDataDeserializable = new JsonObject(convertedItem.CustomData);
                            purchaseItemQuantity.Item = new EconomyReference(convertedItem);
                            return purchaseItemQuantity;
                        }

                        break;
                    }
                }
            }

            return null;
        }
    }
}
