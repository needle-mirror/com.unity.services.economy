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
    /// InventoryExchangeItem model
    /// </summary>
    [Preserve]
    [DataContract(Name = "inventory-exchange-item")]
    internal class InventoryExchangeItem
    {
        /// <summary>
        /// Creates an instance of InventoryExchangeItem.
        /// </summary>
        /// <param name="id">ID of the inventory item.</param>
        /// <param name="amount">Number of player inventory items.</param>
        /// <param name="playersInventoryItemIds">The &#x60;playersInventoryItemIds&#x60; for the player&#39;s items to be added or removed.</param>
        [Preserve]
        public InventoryExchangeItem(string id, int amount, List<string> playersInventoryItemIds)
        {
            Id = id;
            Amount = amount;
            PlayersInventoryItemIds = playersInventoryItemIds;
        }

        /// <summary>
        /// ID of the inventory item.
        /// </summary>
        [Preserve]
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id{ get; }
        
        /// <summary>
        /// Number of player inventory items.
        /// </summary>
        [Preserve]
        [DataMember(Name = "amount", IsRequired = true, EmitDefaultValue = true)]
        public int Amount{ get; }
        
        /// <summary>
        /// The &#x60;playersInventoryItemIds&#x60; for the player&#39;s items to be added or removed.
        /// </summary>
        [Preserve]
        [DataMember(Name = "playersInventoryItemIds", IsRequired = true, EmitDefaultValue = true)]
        public List<string> PlayersInventoryItemIds{ get; }
    
        /// <summary>
        /// Formats a InventoryExchangeItem into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        public string SerializeAsPathParam()
        {
            var serializedModel = "";
            if (Id != null)
            {
                var idStringValue = Id;
                serializedModel += "id," + idStringValue + ",";
            }
            if (Amount != null)
            {
                var amountStringValue = Amount.ToString();
                serializedModel += "amount," + amountStringValue + ",";
            }
            if (PlayersInventoryItemIds != null)
            {
                var playersInventoryItemIdsStringValue = PlayersInventoryItemIds.ToString();
                serializedModel += "playersInventoryItemIds," + playersInventoryItemIdsStringValue;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a InventoryExchangeItem as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        public Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();
            
            if (Id != null)
            {
                var idStringValue = Id.ToString();
                dictionary.Add("id", idStringValue);
            }
            
            if (Amount != null)
            {
                var amountStringValue = Amount.ToString();
                dictionary.Add("amount", amountStringValue);
            }
            
            if (PlayersInventoryItemIds != null)
            {
                var playersInventoryItemIdsStringValue = PlayersInventoryItemIds.ToString();
                dictionary.Add("playersInventoryItemIds", playersInventoryItemIdsStringValue);
            }
            
            return dictionary;
        }
    }
}
