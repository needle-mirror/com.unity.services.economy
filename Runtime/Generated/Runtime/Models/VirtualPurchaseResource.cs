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
    /// The definition for a economy resource that represents a virtual purchase.
    /// </summary>
    [Preserve]
    [DataContract(Name = "virtual-purchase-resource")]
    internal class VirtualPurchaseResource
    {
        /// <summary>
        /// The definition for a economy resource that represents a virtual purchase.
        /// </summary>
        /// <param name="id">Identifier for the resource.</param>
        /// <param name="name">Name of the resource.</param>
        /// <param name="type">Type of the item, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.</param>
        /// <param name="created">created param</param>
        /// <param name="modified">modified param</param>
        /// <param name="costs">The costs deducted from the player when making the purchase. A cost is an ID of a currency or inventory item, plus an amount.</param>
        /// <param name="rewards">The rewards credited to the player when making the purchase. A reward is composed of the ID of a currency or inventory item, the amount of that currency or item, and the default instance data (for inventory items).</param>
        /// <param name="customData">customData param</param>
        [Preserve]
        public VirtualPurchaseResource(string id, string name, TypeOptions type, ModifiedMetadata created, ModifiedMetadata modified, List<Cost> costs = default, List<Reward> rewards = default, object customData = default)
        {
            Id = id;
            Name = name;
            Type = type;
            Created = created;
            Modified = modified;
            Costs = costs;
            Rewards = rewards;
            CustomData = (IDeserializable) JsonObject.GetNewJsonObjectResponse(customData);
        }

        /// <summary>
        /// Identifier for the resource.
        /// </summary>
        [Preserve]
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id{ get; }
        
        /// <summary>
        /// Name of the resource.
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
        /// Parameter created of VirtualPurchaseResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "created", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Created{ get; }
        
        /// <summary>
        /// Parameter modified of VirtualPurchaseResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "modified", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Modified{ get; }
        
        /// <summary>
        /// The costs deducted from the player when making the purchase. A cost is an ID of a currency or inventory item, plus an amount.
        /// </summary>
        [Preserve]
        [DataMember(Name = "costs", EmitDefaultValue = false)]
        public List<Cost> Costs{ get; }
        
        /// <summary>
        /// The rewards credited to the player when making the purchase. A reward is composed of the ID of a currency or inventory item, the amount of that currency or item, and the default instance data (for inventory items).
        /// </summary>
        [Preserve]
        [DataMember(Name = "rewards", EmitDefaultValue = false)]
        public List<Reward> Rewards{ get; }
        
        /// <summary>
        /// Parameter customData of VirtualPurchaseResource
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "customData", EmitDefaultValue = false)]
        public IDeserializable CustomData{ get; }
    
        /// <summary>
        /// Type of the item, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.
        /// </summary>
        /// <value>Type of the item, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.</value>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeOptions
        {
            /// <summary>
            /// Enum VIRTUALPURCHASE for value: VIRTUAL_PURCHASE
            /// </summary>
            [EnumMember(Value = "VIRTUAL_PURCHASE")]
            VIRTUALPURCHASE = 1
        }

        /// <summary>
        /// Formats a VirtualPurchaseResource into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (Id != null)
            {
                serializedModel += "id," + Id + ",";
            }
            if (Name != null)
            {
                serializedModel += "name," + Name + ",";
            }
            serializedModel += "type," + Type + ",";
            if (Created != null)
            {
                serializedModel += "created," + Created.ToString() + ",";
            }
            if (Modified != null)
            {
                serializedModel += "modified," + Modified.ToString() + ",";
            }
            if (Costs != null)
            {
                serializedModel += "costs," + Costs.ToString() + ",";
            }
            if (Rewards != null)
            {
                serializedModel += "rewards," + Rewards.ToString() + ",";
            }
            if (CustomData != null)
            {
                serializedModel += "customData," + CustomData.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a VirtualPurchaseResource as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
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
            
            var typeStringValue = Type.ToString();
            dictionary.Add("type", typeStringValue);
            
            return dictionary;
        }
    }
}
