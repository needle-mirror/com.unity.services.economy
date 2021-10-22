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
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.GameBackend.Economy.Http;



namespace Unity.GameBackend.Economy.Models
{
    /// <summary>
    /// CurrencyBalanceRequest model
    /// <param name="currencyId">Resource ID for the currency.</param>
    /// <param name="balance">The player&#39;s balance.</param>
    /// <param name="writeLock">The write lock for the currency balance.</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "currency-balance-request")]
    public class CurrencyBalanceRequest
    {
        /// <summary>
        /// Creates an instance of CurrencyBalanceRequest.
        /// </summary>
        /// <param name="currencyId">Resource ID for the currency.</param>
        /// <param name="balance">The player&#39;s balance.</param>
        /// <param name="writeLock">The write lock for the currency balance.</param>
        [Preserve]
        public CurrencyBalanceRequest(string currencyId, long balance, string writeLock = default)
        {
            CurrencyId = currencyId;
            Balance = balance;
            WriteLock = writeLock;
        }

        /// <summary>
        /// Resource ID for the currency.
        /// </summary>
        [Preserve]
        [DataMember(Name = "currencyId", IsRequired = true, EmitDefaultValue = true)]
        public string CurrencyId{ get; }
        /// <summary>
        /// The player&#39;s balance.
        /// </summary>
        [Preserve]
        [DataMember(Name = "balance", IsRequired = true, EmitDefaultValue = true)]
        public long Balance{ get; }
        /// <summary>
        /// The write lock for the currency balance.
        /// </summary>
        [Preserve]
        [DataMember(Name = "writeLock", EmitDefaultValue = false)]
        public string WriteLock{ get; }
    
    }
}

