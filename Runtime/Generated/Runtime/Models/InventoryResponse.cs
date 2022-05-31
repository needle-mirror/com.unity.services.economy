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
    /// InventoryResponse model
    /// </summary>
    [Preserve]
    [DataContract(Name = "inventory-response")]
    internal class InventoryResponse
    {
        /// <summary>
        /// Creates an instance of InventoryResponse.
        /// </summary>
        /// <param name="playersInventoryItemId">ID of the player&#39;s inventory item.</param>
        /// <param name="inventoryItemId">Resource ID of the inventory item configuration associated with this instance.</param>
        /// <param name="writeLock">The write lock for the inventory item instance.</param>
        /// <param name="created">created param</param>
        /// <param name="modified">modified param</param>
        /// <param name="instanceData">Instance data. Max size when serialized 5 KB.</param>
        [Preserve]
        public InventoryResponse(string playersInventoryItemId, string inventoryItemId, string writeLock, ModifiedMetadata created, ModifiedMetadata modified, object instanceData = default)
        {
            PlayersInventoryItemId = playersInventoryItemId;
            InventoryItemId = inventoryItemId;
            InstanceData = new JsonObject(instanceData);
            WriteLock = writeLock;
            Created = created;
            Modified = modified;
        }

        /// <summary>
        /// ID of the player&#39;s inventory item.
        /// </summary>
        [Preserve]
        [DataMember(Name = "playersInventoryItemId", IsRequired = true, EmitDefaultValue = true)]
        public string PlayersInventoryItemId{ get; }
        
        /// <summary>
        /// Resource ID of the inventory item configuration associated with this instance.
        /// </summary>
        [Preserve]
        [DataMember(Name = "inventoryItemId", IsRequired = true, EmitDefaultValue = true)]
        public string InventoryItemId{ get; }
        
        /// <summary>
        /// Instance data. Max size when serialized 5 KB.
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "instanceData", EmitDefaultValue = false)]
        public JsonObject InstanceData{ get; }
        
        /// <summary>
        /// The write lock for the inventory item instance.
        /// </summary>
        [Preserve]
        [DataMember(Name = "writeLock", IsRequired = true, EmitDefaultValue = true)]
        public string WriteLock{ get; }
        
        /// <summary>
        /// Parameter created of InventoryResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "created", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Created{ get; }
        
        /// <summary>
        /// Parameter modified of InventoryResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "modified", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Modified{ get; }
    
        /// <summary>
        /// Formats a InventoryResponse into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        public string SerializeAsPathParam()
        {
            var serializedModel = "";
            if (PlayersInventoryItemId != null)
            {
                var playersInventoryItemIdStringValue = PlayersInventoryItemId;
                serializedModel += "playersInventoryItemId," + playersInventoryItemIdStringValue + ",";
            }
            if (InventoryItemId != null)
            {
                var inventoryItemIdStringValue = InventoryItemId;
                serializedModel += "inventoryItemId," + inventoryItemIdStringValue + ",";
            }
            if (InstanceData != null)
            {
                var instanceDataStringValue = InstanceData.ToString();
                serializedModel += "instanceData," + instanceDataStringValue + ",";
            }
            if (WriteLock != null)
            {
                var writeLockStringValue = WriteLock;
                serializedModel += "writeLock," + writeLockStringValue + ",";
            }
            if (Created != null)
            {
                var createdStringValue = Created.ToString();
                serializedModel += "created," + createdStringValue + ",";
            }
            if (Modified != null)
            {
                var modifiedStringValue = Modified.ToString();
                serializedModel += "modified," + modifiedStringValue;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a InventoryResponse as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        public Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();
            
            if (PlayersInventoryItemId != null)
            {
                var playersInventoryItemIdStringValue = PlayersInventoryItemId.ToString();
                dictionary.Add("playersInventoryItemId", playersInventoryItemIdStringValue);
            }
            
            if (InventoryItemId != null)
            {
                var inventoryItemIdStringValue = InventoryItemId.ToString();
                dictionary.Add("inventoryItemId", inventoryItemIdStringValue);
            }
            
            if (WriteLock != null)
            {
                var writeLockStringValue = WriteLock.ToString();
                dictionary.Add("writeLock", writeLockStringValue);
            }
            
            return dictionary;
        }
    }
}