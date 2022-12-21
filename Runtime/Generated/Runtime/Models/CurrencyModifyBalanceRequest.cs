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
    /// CurrencyModifyBalanceRequest model
    /// </summary>
    [Preserve]
    [DataContract(Name = "currency-modify-balance-request")]
    internal class CurrencyModifyBalanceRequest
    {
        /// <summary>
        /// Creates an instance of CurrencyModifyBalanceRequest.
        /// </summary>
        /// <param name="amount">The value by which to increment or decrement. Zero is allowed but results in no change to the currency balance.</param>
        /// <param name="currencyId">Resource ID of the currency.</param>
        /// <param name="writeLock">The write lock for the currency balance.</param>
        [Preserve]
        public CurrencyModifyBalanceRequest(long amount, string currencyId = default, string writeLock = default)
        {
            CurrencyId = currencyId;
            Amount = amount;
            WriteLock = writeLock;
        }

        /// <summary>
        /// Resource ID of the currency.
        /// </summary>
        [Preserve]
        [DataMember(Name = "currencyId", EmitDefaultValue = false)]
        public string CurrencyId{ get; }
        
        /// <summary>
        /// The value by which to increment or decrement. Zero is allowed but results in no change to the currency balance.
        /// </summary>
        [Preserve]
        [DataMember(Name = "amount", IsRequired = true, EmitDefaultValue = true)]
        public long Amount{ get; }
        
        /// <summary>
        /// The write lock for the currency balance.
        /// </summary>
        [Preserve]
        [DataMember(Name = "writeLock", EmitDefaultValue = false)]
        public string WriteLock{ get; }
    
        /// <summary>
        /// Formats a CurrencyModifyBalanceRequest into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (CurrencyId != null)
            {
                serializedModel += "currencyId," + CurrencyId + ",";
            }
            serializedModel += "amount," + Amount.ToString() + ",";
            if (WriteLock != null)
            {
                serializedModel += "writeLock," + WriteLock;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a CurrencyModifyBalanceRequest as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (CurrencyId != null)
            {
                var currencyIdStringValue = CurrencyId.ToString();
                dictionary.Add("currencyId", currencyIdStringValue);
            }
            
            var amountStringValue = Amount.ToString();
            dictionary.Add("amount", amountStringValue);
            
            if (WriteLock != null)
            {
                var writeLockStringValue = WriteLock.ToString();
                dictionary.Add("writeLock", writeLockStringValue);
            }
            
            return dictionary;
        }
    }
}
