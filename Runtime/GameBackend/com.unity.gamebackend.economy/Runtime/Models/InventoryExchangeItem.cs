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
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.GameBackend.Economy.Http;



namespace Unity.GameBackend.Economy.Models
{
    /// <summary>
    /// InventoryExchangeItem model
    /// <param name="id">ID of the inventory item.</param>
    /// <param name="amount">Number of player inventory items.</param>
    /// <param name="playersInventoryItemIds">Players inventory item IDs of the players items to be added or removed.</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "inventory-exchange-item")]
    public class InventoryExchangeItem
    {
        /// <summary>
        /// Creates an instance of InventoryExchangeItem.
        /// </summary>
        /// <param name="id">ID of the inventory item.</param>
        /// <param name="amount">Number of player inventory items.</param>
        /// <param name="playersInventoryItemIds">Players inventory item IDs of the players items to be added or removed.</param>
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
        /// Players inventory item IDs of the players items to be added or removed.
        /// </summary>
        [Preserve]
        [DataMember(Name = "playersInventoryItemIds", IsRequired = true, EmitDefaultValue = true)]
        public List<string> PlayersInventoryItemIds{ get; }
    
    }
}

