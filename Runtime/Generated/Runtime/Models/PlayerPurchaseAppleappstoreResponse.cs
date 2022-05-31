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
    /// PlayerPurchaseAppleappstoreResponse model
    /// </summary>
    [Preserve]
    [DataContract(Name = "player-purchase-appleappstore-response")]
    internal class PlayerPurchaseAppleappstoreResponse
    {
        /// <summary>
        /// Creates an instance of PlayerPurchaseAppleappstoreResponse.
        /// </summary>
        /// <param name="verification">verification param</param>
        /// <param name="rewards">rewards param</param>
        [Preserve]
        public PlayerPurchaseAppleappstoreResponse(PlayerPurchaseAppleappstoreResponseVerification verification, PlayerPurchaseVirtualResponseRewards rewards)
        {
            Verification = verification;
            Rewards = rewards;
        }

        /// <summary>
        /// Parameter verification of PlayerPurchaseAppleappstoreResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "verification", IsRequired = true, EmitDefaultValue = true)]
        public PlayerPurchaseAppleappstoreResponseVerification Verification{ get; }
        
        /// <summary>
        /// Parameter rewards of PlayerPurchaseAppleappstoreResponse
        /// </summary>
        [Preserve]
        [DataMember(Name = "rewards", IsRequired = true, EmitDefaultValue = true)]
        public PlayerPurchaseVirtualResponseRewards Rewards{ get; }
    
        /// <summary>
        /// Formats a PlayerPurchaseAppleappstoreResponse into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        public string SerializeAsPathParam()
        {
            var serializedModel = "";
            if (Verification != null)
            {
                var verificationStringValue = Verification.ToString();
                serializedModel += "verification," + verificationStringValue + ",";
            }
            if (Rewards != null)
            {
                var rewardsStringValue = Rewards.ToString();
                serializedModel += "rewards," + rewardsStringValue;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a PlayerPurchaseAppleappstoreResponse as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        public Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();
            
            return dictionary;
        }
    }
}