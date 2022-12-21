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
    /// The definition for a economy resource that represents a currency.
    /// </summary>
    [Preserve]
    [DataContract(Name = "currency-resource")]
    internal class CurrencyResource
    {
        /// <summary>
        /// The definition for a economy resource that represents a currency.
        /// </summary>
        /// <param name="id">Identifier for the resource.</param>
        /// <param name="name">Name of the resource.</param>
        /// <param name="type">Type of the resource, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.</param>
        /// <param name="created">created param</param>
        /// <param name="modified">modified param</param>
        /// <param name="initial">initial param</param>
        /// <param name="max">max param</param>
        /// <param name="customData">customData param</param>
        [Preserve]
        public CurrencyResource(string id, string name, TypeOptions type, ModifiedMetadata created, ModifiedMetadata modified, int initial = default, int max = default, object customData = default)
        {
            Id = id;
            Name = name;
            Type = type;
            Created = created;
            Modified = modified;
            Initial = initial;
            Max = max;
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
        /// Type of the resource, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public TypeOptions Type{ get; }

        /// <summary>
        /// Parameter created of CurrencyResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "created", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Created{ get; }

        /// <summary>
        /// Parameter modified of CurrencyResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "modified", IsRequired = true, EmitDefaultValue = true)]
        public ModifiedMetadata Modified{ get; }

        /// <summary>
        /// Parameter initial of CurrencyResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "initial", EmitDefaultValue = true)]
        public int Initial{ get; }

        /// <summary>
        /// Parameter max of CurrencyResource
        /// </summary>
        [Preserve]
        [DataMember(Name = "max", EmitDefaultValue = true)]
        public int Max{ get; }

        /// <summary>
        /// Parameter customData of CurrencyResource
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "customData", EmitDefaultValue = false)]
        public IDeserializable CustomData{ get; }

        /// <summary>
        /// Type of the resource, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.
        /// </summary>
        /// <value>Type of the resource, for example &#x60;CURRENCY&#x60;, &#x60;INVENTORY_ITEM&#x60;, &#x60;VIRTUAL_PURCHASE&#x60;, &#x60;MONEY_PURCHASE&#x60;.</value>
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
        /// Formats a CurrencyResource into a string of key-value pairs for use as a path parameter.
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
            serializedModel += "initial," + Initial.ToString() + ",";
            serializedModel += "max," + Max.ToString() + ",";
            if (CustomData != null)
            {
                serializedModel += "customData," + CustomData.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a CurrencyResource as a dictionary of key-value pairs for use as a query parameter.
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
