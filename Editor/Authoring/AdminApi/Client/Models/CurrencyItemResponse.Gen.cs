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
    [DataContract(Name = "currency-item-response")]
    internal class CurrencyItemResponse
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">ID of the item.</param>
        /// <param name="name">A descriptive name for the item.</param>
        /// <param name="type">type param</param>
        /// <param name="initial">The initial amount of currency that a player is credited with upon first interaction. When &#x60;max&#x60; is used, &#x60;initial&#x60; must be less than &#x60;max&#x60;.</param>
        /// <param name="max">The maximum currency balance that a player can have. Calls that would result in the maximum being exceeded return an error.</param>
        /// <param name="customData">Max size when serialised 5 kilobits.</param>
        /// <param name="created">created param</param>
        /// <param name="modified">modified param</param>
        [Preserve]
        public CurrencyItemResponse(string id, string name, TypeOptions type, long initial, long max, object customData, ModifiedMetadata created, ModifiedMetadata modified)
        {
            Id = id;
            Name = name;
            Type = type;
            Initial = initial;
            Max = max;
            CustomData = (IDeserializable) JsonObject.GetNewJsonObjectResponse(customData);
            Created = created;
            Modified = modified;
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
        /// Parameter type of CurrencyItemResponse
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public TypeOptions Type{ get; }

        /// <summary>
        /// The initial amount of currency that a player is credited with upon first interaction. When &#x60;max&#x60; is used, &#x60;initial&#x60; must be less than &#x60;max&#x60;.
        /// </summary>
        [Preserve]
        [DataMember(Name = "initial", IsRequired = true, EmitDefaultValue = true)]
        public long Initial{ get; }

        /// <summary>
        /// The maximum currency balance that a player can have. Calls that would result in the maximum being exceeded return an error.
        /// </summary>
        [Preserve]
        [DataMember(Name = "max", IsRequired = true, EmitDefaultValue = true)]
        public long Max{ get; }

        /// <summary>
        /// Max size when serialised 5 kilobits.
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "customData", IsRequired = true, EmitDefaultValue = true)]
        public IDeserializable CustomData{ get; }

        /// <summary>
        /// Parameter created of CurrencyItemResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "created", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Created{ get; }

        /// <summary>
        /// Parameter modified of CurrencyItemResponse
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
            /// Enum CURRENCY for value: CURRENCY
            /// </summary>
            [EnumMember(Value = "CURRENCY")]
            CURRENCY = 1
        }

        /// <summary>
        /// Formats a CurrencyItemResponse into a string of key-value pairs for use as a path parameter.
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
        /// Returns a CurrencyItemResponse as a dictionary of key-value pairs for use as a query parameter.
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
