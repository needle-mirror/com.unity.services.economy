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
    [DataContract(Name = "inventory-item-request")]
    internal class InventoryItemRequest
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">ID of the item.</param>
        /// <param name="name">A descriptive name for the item.</param>
        /// <param name="type">type param</param>
        /// <param name="customData">Max size when serialized 5 kilobits.</param>
        [Preserve]
        public InventoryItemRequest(string id, string name, TypeOptions type, object customData = default)
        {
            Id = id;
            Name = name;
            Type = type;
            CustomData = (IDeserializable)(customData is null ? new JsonObject(new object()) : JsonObject.GetNewJsonObjectResponse(customData));
        }

        /// <summary>
        /// ID of the item.
        /// </summary>
        [Preserve]
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id{ get; }

        /// <summary>
        /// A descriptive name for the item.
        /// </summary>
        [Preserve]
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name{ get; }

        /// <summary>
        /// Parameter type of InventoryItemRequest
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public TypeOptions Type{ get; }

        /// <summary>
        /// Max size when serialized 5 kilobits.
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "customData", EmitDefaultValue = true)]
        public IDeserializable CustomData{ get; }

        /// <summary>
        /// Defines Type
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeOptions
        {
            /// <summary>
            /// Enum INVENTORYITEM for value: INVENTORY_ITEM
            /// </summary>
            [EnumMember(Value = "INVENTORY_ITEM")]
            INVENTORYITEM = 1
        }

        /// <summary>
        /// Formats a InventoryItemRequest into a string of key-value pairs for use as a path parameter.
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
            if (CustomData != null)
            {
                serializedModel += "customData," + CustomData.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a InventoryItemRequest as a dictionary of key-value pairs for use as a query parameter.
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