using Newtonsoft.Json;
using Unity.Services.Economy.Internal.Models;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents an amount of currency/inventory items associated with a purchase. Each one relates to a single currency/inventory item type (for example 4 swords, 10 gold etc.).
    /// </summary>
    [Preserve]
    public class PurchaseItemQuantity
    {
        /// <summary>Creates an instance of PurchaseItemQuantity</summary>
        [Preserve]
        public PurchaseItemQuantity()
        {
        }

        [Preserve]
        internal PurchaseItemQuantity(Reward reward)
        {
            Amount = reward.Amount;
            ResourceId = reward.ResourceId;
        }

        [Preserve]
        internal PurchaseItemQuantity(Cost cost)
        {
            Amount = cost.Amount;
            ResourceId = cost.ResourceId;
        }

        /// <summary>The amount of the purchase </summary>
        [Preserve]
        [JsonRequired]
        [JsonProperty("amount")]
        public int Amount;

        /// <summary>Reference to the item</summary>
        [Preserve]
        [JsonRequired]
        [JsonProperty("itemId")]
        public EconomyReference Item;

        internal string ResourceId;
    }
}
