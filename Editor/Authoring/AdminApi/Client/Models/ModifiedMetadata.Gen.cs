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
    /// ModifiedMetadata model
    /// </summary>
    [Preserve]
    [DataContract(Name = "modified-metadata")]
    internal class ModifiedMetadata
    {
        /// <summary>
        /// Creates an instance of ModifiedMetadata.
        /// </summary>
        /// <param name="date">Date time in ISO 8601 format. &#x60;null&#x60; if there is no associated value.</param>
        [Preserve]
        public ModifiedMetadata(DateTime? date)
        {
            Date = date;
        }

        /// <summary>
        /// Date time in ISO 8601 format. &#x60;null&#x60; if there is no associated value.
        /// </summary>
        [Preserve]
        [DataMember(Name = "date", IsRequired = true, EmitDefaultValue = true)]
        public DateTime? Date{ get; }

        /// <summary>
        /// Formats a ModifiedMetadata into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (Date != null)
            {
                serializedModel += "date," + Date.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a ModifiedMetadata as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (Date != null)
            {
                var dateStringValue = Date.ToString();
                dictionary.Add("date", dateStringValue);
            }

            return dictionary;
        }
    }
}
