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
    /// Details from the receipt validation service.
    /// </summary>
    [Preserve]
    [DataContract(Name = "player_purchase_appleappstore_response_verification_store")]
    internal class PlayerPurchaseAppleappstoreResponseVerificationStore
    {
        /// <summary>
        /// Details from the receipt validation service.
        /// </summary>
        /// <param name="code">The status code sent back from the Apple App Store verification service.</param>
        /// <param name="message">A textual description of the returned status code.</param>
        /// <param name="receipt">The full response from the Apple App Store verification service as a JSON encoded string.</param>
        [Preserve]
        public PlayerPurchaseAppleappstoreResponseVerificationStore(string code, string message, string receipt)
        {
            Code = code;
            Message = message;
            Receipt = receipt;
        }

        /// <summary>
        /// The status code sent back from the Apple App Store verification service.
        /// </summary>
        [Preserve]
        [DataMember(Name = "code", IsRequired = true, EmitDefaultValue = true)]
        public string Code{ get; }
        
        /// <summary>
        /// A textual description of the returned status code.
        /// </summary>
        [Preserve]
        [DataMember(Name = "message", IsRequired = true, EmitDefaultValue = true)]
        public string Message{ get; }
        
        /// <summary>
        /// The full response from the Apple App Store verification service as a JSON encoded string.
        /// </summary>
        [Preserve]
        [DataMember(Name = "receipt", IsRequired = true, EmitDefaultValue = true)]
        public string Receipt{ get; }
    
        /// <summary>
        /// Formats a PlayerPurchaseAppleappstoreResponseVerificationStore into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (Code != null)
            {
                serializedModel += "code," + Code + ",";
            }
            if (Message != null)
            {
                serializedModel += "message," + Message + ",";
            }
            if (Receipt != null)
            {
                serializedModel += "receipt," + Receipt;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a PlayerPurchaseAppleappstoreResponseVerificationStore as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (Code != null)
            {
                var codeStringValue = Code.ToString();
                dictionary.Add("code", codeStringValue);
            }
            
            if (Message != null)
            {
                var messageStringValue = Message.ToString();
                dictionary.Add("message", messageStringValue);
            }
            
            if (Receipt != null)
            {
                var receiptStringValue = Receipt.ToString();
                dictionary.Add("receipt", receiptStringValue);
            }
            
            return dictionary;
        }
    }
}
