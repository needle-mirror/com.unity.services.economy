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

        [Preserve]
        [JsonRequired]
        [JsonProperty("amount")]
        public int Amount;

        [Preserve]
        [JsonRequired]
        [JsonProperty("itemId")]
        public EconomyReference Item;

        internal string ResourceId;
    }
}
