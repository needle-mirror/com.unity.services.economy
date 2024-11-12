using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.Services.Economy.Internal.Models;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// The store identifiers.
    /// </summary>
    [Preserve]
    public class StoreIdentifiers
    {
        /// <summary>Create an instance of the StoreIdentifiers class </summary>
        [Preserve]
        public StoreIdentifiers() { }

        [Preserve]
        internal StoreIdentifiers(RealMoneyPurchaseResourceStoreIdentifiers data)
        {
            AppleAppStore = data.AppleAppStore;
            GooglePlayStore = data.GooglePlayStore;
        }

        /// <summary>
        /// Apple App Store identifier
        /// </summary>
        [Preserve]
        [JsonProperty("appleAppStore")]
        public string AppleAppStore;

        /// <summary>
        /// Google Play Store identifier
        /// </summary>
        [Preserve]
        [JsonProperty("googlePlayStore")]
        public string GooglePlayStore;
    }

    /// <summary>
    /// Represents a single real money purchase configuration.
    /// </summary>
    [Preserve]
    public class RealMoneyPurchaseDefinition : ConfigurationItemDefinition
    {
        /// <summary>Create an instance of the RealMoneyPurchaseDefinition class </summary>
        [Preserve]
        public RealMoneyPurchaseDefinition()
        {
        }

        [Preserve]
        internal RealMoneyPurchaseDefinition(RealMoneyPurchaseResource resource)
        {
            Id = resource.Id;
            Name = resource.Name;
            Type = ConfigurationInternal.RealMoneyPurchaseType;
            Created = EconomyDate.From(resource.Created);
            Modified = EconomyDate.From(resource.Modified);
            CustomDataDeserializable = resource.CustomData;
            StoreIdentifiers = new StoreIdentifiers(resource.StoreIdentifiers);
            Rewards = new List<PurchaseItemQuantity>();
            #pragma warning disable CS0618
            // obsolete member
            CustomData = JsonConvert.DeserializeObject<Dictionary<string, object>>(resource.CustomData.GetAsString());
            #pragma warning disable CS0618

            if (resource.Rewards != null)
            {
                foreach (var reward in resource.Rewards)
                {
                    Rewards.Add(new PurchaseItemQuantity(reward));
                }
            }
        }

        /// <summary>
        /// The store identifiers for this purchase.
        /// </summary>
        [Preserve]
        [JsonRequired]
        [JsonProperty("storeIdentifiers")]
        public StoreIdentifiers StoreIdentifiers;

        /// <summary>
        /// The rewards associated with this purchase
        /// </summary>
        [Preserve]
        [JsonRequired]
        [JsonProperty("rewards")]
        public List<PurchaseItemQuantity> Rewards;
    }
}
