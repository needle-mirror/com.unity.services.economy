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
    /// PlayerConfigurationResponse model
    /// </summary>
    [Preserve]
    [DataContract(Name = "player-configuration-response")]
    internal class PlayerConfigurationResponse
    {
        /// <summary>
        /// Creates an instance of PlayerConfigurationResponse.
        /// </summary>
        /// <param name="results">Array of resource definitions.</param>
        /// <param name="metadata">metadata param</param>
        [Preserve]
        public PlayerConfigurationResponse(List<PlayerConfigurationResponseResultsOneOf> results, PlayerConfigurationResponseMetadata metadata = default)
        {
            Metadata = metadata;
            Results = results;
        }

        /// <summary>
        /// Parameter metadata of PlayerConfigurationResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public PlayerConfigurationResponseMetadata Metadata{ get; }
        
        /// <summary>
        /// Array of resource definitions.
        /// </summary>
        [Preserve]
        [DataMember(Name = "results", IsRequired = true, EmitDefaultValue = true)]
        public List<PlayerConfigurationResponseResultsOneOf> Results{ get; }
    
        /// <summary>
        /// Formats a PlayerConfigurationResponse into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        public string SerializeAsPathParam()
        {
            var serializedModel = "";
            if (Metadata != null)
            {
                var metadataStringValue = Metadata.ToString();
                serializedModel += "metadata," + metadataStringValue + ",";
            }
            if (Results != null)
            {
                var resultsStringValue = Results.ToString();
                serializedModel += "results," + resultsStringValue;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a PlayerConfigurationResponse as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        public Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();
            
            return dictionary;
        }
    }
}