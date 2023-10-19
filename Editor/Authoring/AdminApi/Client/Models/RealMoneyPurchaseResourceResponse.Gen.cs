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
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Http;


namespace Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models
{
    /// <summary>
    ///
    /// </summary>
    [Preserve]
    [DataContract(Name = "real-money-purchase-resource-response")]
    internal class RealMoneyPurchaseResourceResponse
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">ID of the resource.</param>
        /// <param name="name">A descriptive name for the resource.</param>
        /// <param name="type">type param</param>
        /// <param name="storeIdentifiers">storeIdentifiers param</param>
        /// <param name="rewards">The rewards credited when making the purchase. A reward is an ID of a currency or inventory item, an amount and default instance data for inventory items. An item can be used in more than one reward line.</param>
        /// <param name="customData">Max size when serialised 5 kilobits.</param>
        /// <param name="created">created param</param>
        /// <param name="modified">modified param</param>
        [Preserve]
        public RealMoneyPurchaseResourceResponse(string id, string name, TypeOptions type, RealMoneyPurchaseItemResponseStoreIdentifiers storeIdentifiers, List<VirtualPurchaseResourceResponseRewardsInner> rewards, object customData, ModifiedMetadata created, ModifiedMetadata modified)
        {
            Id = id;
            Name = name;
            Type = type;
            StoreIdentifiers = storeIdentifiers;
            Rewards = rewards;
            CustomData = (IDeserializable) JsonObject.GetNewJsonObjectResponse(customData);
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
        /// Parameter type of RealMoneyPurchaseResourceResponse
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public TypeOptions Type{ get; }

        /// <summary>
        /// Parameter storeIdentifiers of RealMoneyPurchaseResourceResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "storeIdentifiers", IsRequired = true, EmitDefaultValue = true)]
        public RealMoneyPurchaseItemResponseStoreIdentifiers StoreIdentifiers{ get; }

        /// <summary>
        /// The rewards credited when making the purchase. A reward is an ID of a currency or inventory item, an amount and default instance data for inventory items. An item can be used in more than one reward line.
        /// </summary>
        [Preserve]
        [DataMember(Name = "rewards", IsRequired = true, EmitDefaultValue = true)]
        public List<VirtualPurchaseResourceResponseRewardsInner> Rewards{ get; }

        /// <summary>
        /// Max size when serialised 5 kilobits.
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "customData", IsRequired = true, EmitDefaultValue = true)]
        public IDeserializable CustomData{ get; }

        /// <summary>
        /// Parameter created of RealMoneyPurchaseResourceResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "created", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Created{ get; }

        /// <summary>
        /// Parameter modified of RealMoneyPurchaseResourceResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "modified", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Modified{ get; }

        /// <summary>
        /// Defines Type
        /// </summary>
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
        /// Formats a RealMoneyPurchaseResourceResponse into a string of key-value pairs for use as a path parameter.
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
            if (StoreIdentifiers != null)
            {
                serializedModel += "storeIdentifiers," + StoreIdentifiers.ToString() + ",";
            }
            if (Rewards != null)
            {
                serializedModel += "rewards," + Rewards.ToString() + ",";
            }
            if (CustomData != null)
            {
                serializedModel += "customData," + CustomData.ToString() + ",";
            }
            if (Created != null)
            {
                serializedModel += "created," + Created.ToString() + ",";
            }
            if (Modified != null)
            {
                serializedModel += "modified," + Modified.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a RealMoneyPurchaseResourceResponse as a dictionary of key-value pairs for use as a query parameter.
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