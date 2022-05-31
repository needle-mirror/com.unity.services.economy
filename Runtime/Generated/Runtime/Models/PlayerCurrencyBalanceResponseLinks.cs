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
    /// PlayerCurrencyBalanceResponseLinks model
    /// </summary>
    [Preserve]
    [DataContract(Name = "player_currency_balance_response_links")]
    internal class PlayerCurrencyBalanceResponseLinks
    {
        /// <summary>
        /// Creates an instance of PlayerCurrencyBalanceResponseLinks.
        /// </summary>
        /// <param name="next">Contains the URL path for requesting the next page of results. This value is &#x60;null&#x60; when there are no pages remaining.</param>
        [Preserve]
        public PlayerCurrencyBalanceResponseLinks(string next)
        {
            Next = next;
        }

        /// <summary>
        /// Contains the URL path for requesting the next page of results. This value is &#x60;null&#x60; when there are no pages remaining.
        /// </summary>
        [Preserve]
        [DataMember(Name = "next", IsRequired = true, EmitDefaultValue = true)]
        public string Next{ get; }
    
        /// <summary>
        /// Formats a PlayerCurrencyBalanceResponseLinks into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        public string SerializeAsPathParam()
        {
            var serializedModel = "";
            if (Next != null)
            {
                var nextStringValue = Next;
                serializedModel += "next," + nextStringValue;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a PlayerCurrencyBalanceResponseLinks as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        public Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();
            
            if (Next != null)
            {
                var nextStringValue = Next.ToString();
                dictionary.Add("next", nextStringValue);
            }
            
            return dictionary;
        }
    }
}