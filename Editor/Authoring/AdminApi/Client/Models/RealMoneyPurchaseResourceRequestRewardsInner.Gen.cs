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
    /// RealMoneyPurchaseResourceRequestRewardsInner model
    /// </summary>
    [Preserve]
    [DataContract(Name = "real_money_purchase_resource_request_rewards_inner")]
    internal class RealMoneyPurchaseResourceRequestRewardsInner
    {
        /// <summary>
        /// Creates an instance of RealMoneyPurchaseResourceRequestRewardsInner.
        /// </summary>
        /// <param name="resourceId">The ID of the currency or inventory item used as a reward.</param>
        /// <param name="amount">The amount of the resource credited as part of the purchase.</param>
        /// <param name="defaultInstanceData">Instance data saved against the new inventory item. Max size when serialised 5 kilobits. This property has been deprecated and will only return \&quot;null\&quot;.</param>
        [Preserve]
        public RealMoneyPurchaseResourceRequestRewardsInner(string resourceId, long amount, object defaultInstanceData = default)
        {
            ResourceId = resourceId;
            Amount = amount;
            DefaultInstanceData = (IDeserializable) JsonObject.GetNewJsonObjectResponse(defaultInstanceData);
        }

        /// <summary>
        /// The ID of the currency or inventory item used as a reward.
        /// </summary>
        [Preserve]
        [DataMember(Name = "resourceId", IsRequired = true, EmitDefaultValue = true)]
        public string ResourceId{ get; }

        /// <summary>
        /// The amount of the resource credited as part of the purchase.
        /// </summary>
        [Preserve]
        [DataMember(Name = "amount", IsRequired = true, EmitDefaultValue = true)]
        public long Amount{ get; }

        /// <summary>
        /// Instance data saved against the new inventory item. Max size when serialised 5 kilobits. This property has been deprecated and will only return \&quot;null\&quot;.
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "defaultInstanceData", EmitDefaultValue = false)]
        public IDeserializable DefaultInstanceData{ get; }

        /// <summary>
        /// Formats a RealMoneyPurchaseResourceRequestRewardsInner into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (ResourceId != null)
            {
                serializedModel += "resourceId," + ResourceId + ",";
            }
            serializedModel += "amount," + Amount.ToString() + ",";
            if (DefaultInstanceData != null)
            {
                serializedModel += "defaultInstanceData," + DefaultInstanceData.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a RealMoneyPurchaseResourceRequestRewardsInner as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (ResourceId != null)
            {
                var resourceIdStringValue = ResourceId.ToString();
                dictionary.Add("resourceId", resourceIdStringValue);
            }

            var amountStringValue = Amount.ToString();
            dictionary.Add("amount", amountStringValue);

            return dictionary;
        }
    }
}
