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
    /// The definition for a economy resource that represents a real money purchase.
    /// </summary>
    [Preserve]
    [DataContract(Name = "real-money-purchase-resource")]
    internal class RealMoneyPurchaseResource
    {
        /// <summary>
        /// The definition for a economy resource that represents a real money purchase.
        /// </summary>
        /// <param name="id">ID of the resource.</param>
        /// <param name="name">A descriptive name for the resource.</param>
        /// <param name="type">Type of the item, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.</param>
        /// <param name="storeIdentifiers">storeIdentifiers param</param>
        /// <param name="rewards">The rewards credited to the player when making the purchase. A reward is composed of the ID of a currency or inventory item, the amount of that currency or item, and the default instance data (for inventory items).</param>
        /// <param name="customData">Max size when serialised 5 KB.</param>
        /// <param name="created">created param</param>
        /// <param name="modified">modified param</param>
        [Preserve]
        public RealMoneyPurchaseResource(string id, string name, TypeOptions type, RealMoneyPurchaseResourceStoreIdentifiers storeIdentifiers, List<Reward> rewards, object customData, ModifiedMetadata created, ModifiedMetadata modified)
        {
            Id = id;
            Name = name;
            Type = type;
            StoreIdentifiers = storeIdentifiers;
            Rewards = rewards;
            CustomData = new JsonObject(customData);
            Created = created;
            Modified = modified;
        }

        /// <summary>
        /// ID of the resource.
        /// </summary>
        [Preserve]
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id{ get; }
        
        /// <summary>
        /// A descriptive name for the resource.
        /// </summary>
        [Preserve]
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name{ get; }
        
        /// <summary>
        /// Type of the item, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public TypeOptions Type{ get; }
        
        /// <summary>
        /// Parameter storeIdentifiers of RealMoneyPurchaseResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "storeIdentifiers", IsRequired = true, EmitDefaultValue = true)]
        public RealMoneyPurchaseResourceStoreIdentifiers StoreIdentifiers{ get; }
        
        /// <summary>
        /// The rewards credited to the player when making the purchase. A reward is composed of the ID of a currency or inventory item, the amount of that currency or item, and the default instance data (for inventory items).
        /// </summary>
        [Preserve]
        [DataMember(Name = "rewards", IsRequired = true, EmitDefaultValue = true)]
        public List<Reward> Rewards{ get; }
        
        /// <summary>
        /// Max size when serialised 5 KB.
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "customData", IsRequired = true, EmitDefaultValue = true)]
        public JsonObject CustomData{ get; }
        
        /// <summary>
        /// Parameter created of RealMoneyPurchaseResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "created", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Created{ get; }
        
        /// <summary>
        /// Parameter modified of RealMoneyPurchaseResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "modified", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Modified{ get; }
    
        /// <summary>
        /// Type of the item, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.
        /// </summary>
        /// <value>Type of the item, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.</value>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeOptions
        {
            /// <summary>
            /// Enum MONEYPURCHASE for value: MONEY_PURCHASE
            /// </summary>
            [EnumMember(Value = "MONEY_PURCHASE")]
            MONEYPURCHASE = 1
        }

        /// <summary>
        /// Formats a RealMoneyPurchaseResource into a string of key-value pairs for use as a path parameter.
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
            if (Name != null)
            {
                var nameStringValue = Name;
                serializedModel += "name," + nameStringValue + ",";
            }
            if (Type != null)
            {
                var typeStringValue = Type;
                serializedModel += "type," + typeStringValue + ",";
            }
            if (StoreIdentifiers != null)
            {
                var storeIdentifiersStringValue = StoreIdentifiers.ToString();
                serializedModel += "storeIdentifiers," + storeIdentifiersStringValue + ",";
            }
            if (Rewards != null)
            {
                var rewardsStringValue = Rewards.ToString();
                serializedModel += "rewards," + rewardsStringValue + ",";
            }
            if (CustomData != null)
            {
                var customDataStringValue = CustomData.ToString();
                serializedModel += "customData," + customDataStringValue + ",";
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
        /// Returns a RealMoneyPurchaseResource as a dictionary of key-value pairs for use as a query parameter.
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
            
            if (Name != null)
            {
                var nameStringValue = Name.ToString();
                dictionary.Add("name", nameStringValue);
            }
            
            if (Type != null)
            {
                var typeStringValue = Type.ToString();
                dictionary.Add("type", typeStringValue);
            }
            
            return dictionary;
        }
    }
}