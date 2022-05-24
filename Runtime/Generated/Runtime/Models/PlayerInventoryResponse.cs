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
    /// PlayerInventoryResponse model
    /// </summary>
    [Preserve]
    [DataContract(Name = "player_inventory_response")]
    internal class PlayerInventoryResponse
    {
        /// <summary>
        /// Creates an instance of PlayerInventoryResponse.
        /// </summary>
        /// <param name="results">List of player&#39;s inventory items.</param>
        /// <param name="links">links param</param>
        [Preserve]
        public PlayerInventoryResponse(List<InventoryResponse> results, PlayerCurrencyBalanceResponseLinks links)
        {
            Results = results;
            Links = links;
        }

        /// <summary>
        /// List of player&#39;s inventory items.
        /// </summary>
        [Preserve]
        [DataMember(Name = "results", IsRequired = true, EmitDefaultValue = true)]
        public List<InventoryResponse> Results{ get; }
        
        /// <summary>
        /// Parameter links of PlayerInventoryResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "links", IsRequired = true, EmitDefaultValue = true)]
        public PlayerCurrencyBalanceResponseLinks Links{ get; }
    
        /// <summary>
        /// Formats a PlayerInventoryResponse into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        public string SerializeAsPathParam()
        {
            var serializedModel = "";
            if (Results != null)
            {
                var resultsStringValue = Results.ToString();
                serializedModel += "results," + resultsStringValue + ",";
            }
            if (Links != null)
            {
                var linksStringValue = Links.ToString();
                serializedModel += "links," + linksStringValue;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a PlayerInventoryResponse as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        public Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();
            
            return dictionary;
        }
    }
}
