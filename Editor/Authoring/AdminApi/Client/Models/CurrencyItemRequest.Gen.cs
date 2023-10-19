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
    [DataContract(Name = "currency-item-request")]
    internal class CurrencyItemRequest
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">ID of the item.</param>
        /// <param name="name">A descriptive name for the item.</param>
        /// <param name="type">type param</param>
        /// <param name="initial">The initial amount of currency that a player is credited upon first interaction. When &#x60;max&#x60; is used, &#x60;initial&#x60; must be less than &#x60;max&#x60;.</param>
        /// <param name="max">The maximum currency balance that a player can have. Calls that would result in the maximum being exceeded return an error.</param>
        /// <param name="customData">Max size when serialized 5 kilobits.</param>
        [Preserve]
        public CurrencyItemRequest(string id, string name, TypeOptions type, long initial = 0, long max = default, object customData = default)
        {
            Id = id;
            Name = name;
            Type = type;
            Initial = initial;
            Max = max;
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
        /// Parameter type of CurrencyItemRequest
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public TypeOptions Type{ get; }

        /// <summary>
        /// The initial amount of currency that a player is credited upon first interaction. When &#x60;max&#x60; is used, &#x60;initial&#x60; must be less than &#x60;max&#x60;.
        /// </summary>
        [Preserve]
        [DataMember(Name = "initial", EmitDefaultValue = true)]
        public long Initial{ get; }

        /// <summary>
        /// The maximum currency balance that a player can have. Calls that would result in the maximum being exceeded return an error.
        /// </summary>
        [Preserve]
        [DataMember(Name = "max", EmitDefaultValue = true)]
        public long Max{ get; }

        /// <summary>
        /// Max size when serialized 5 kilobits.
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "customData", EmitDefaultValue = true)]
        public IDeserializable CustomData { get; }

        /// <summary>
        /// Defines Type
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeOptions
        {
            /// <summary>
            /// Enum CURRENCY for value: CURRENCY
            /// </summary>
            [EnumMember(Value = "CURRENCY")]
            CURRENCY = 1
        }

        /// <summary>
        /// Formats a CurrencyItemRequest into a string of key-value pairs for use as a path parameter.
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
            serializedModel += "initial," + Initial.ToString() + ",";
            serializedModel += "max," + Max.ToString() + ",";
            if (CustomData != null)
            {
                serializedModel += "customData," + CustomData.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a CurrencyItemRequest as a dictionary of key-value pairs for use as a query parameter.
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

            var initialStringValue = Initial.ToString();
            dictionary.Add("initial", initialStringValue);

            var maxStringValue = Max.ToString();
            dictionary.Add("max", maxStringValue);

            return dictionary;
        }
    }
}