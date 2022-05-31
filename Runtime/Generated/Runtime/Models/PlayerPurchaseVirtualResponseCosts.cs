//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.Economy.Internal.Http;



namespace Unity.Services.Economy.Internal.Models
{
    /// <summary>
    /// Costs of the purchase.
    /// </summary>
    [Preserve]
    [DataContract(Name = "player_purchase_virtual_response_costs")]
    internal class PlayerPurchaseVirtualResponseCosts
    {
        /// <summary>
        /// Costs of the purchase.
        /// </summary>
        /// <param name="inventory">Inventory that was deducted in the purchase.</param>
        /// <param name="currency">Currency that was deducted in the purchase.</param>
        [Preserve]
        public PlayerPurchaseVirtualResponseCosts(List<InventoryExchangeItem> inventory, List<CurrencyExchangeItem> currency = default)
        {
            Currency = currency;
            Inventory = inventory;
        }

        /// <summary>
        /// Currency that was deducted in the purchase.
        /// </summary>
        [Preserve]
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public List<CurrencyExchangeItem> Currency{ get; }
        
        /// <summary>
        /// Inventory that was deducted in the purchase.
        /// </summary>
        [Preserve]
        [DataMember(Name = "inventory", IsRequired = true, EmitDefaultValue = true)]
        public List<InventoryExchangeItem> Inventory{ get; }
    
        /// <summary>
        /// Formats a PlayerPurchaseVirtualResponseCosts into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        public string SerializeAsPathParam()
        {
            var serializedModel = "";
            if (Currency != null)
            {
                var currencyStringValue = Currency.ToString();
                serializedModel += "currency," + currencyStringValue + ",";
            }
            if (Inventory != null)
            {
                var inventoryStringValue = Inventory.ToString();
                serializedModel += "inventory," + inventoryStringValue;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a PlayerPurchaseVirtualResponseCosts as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        public Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();
            
            return dictionary;
        }
    }
}