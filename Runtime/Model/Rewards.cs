using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represent the rewards given in exchange for a purchase.
    /// </summary>
    [Preserve]
    public class Rewards
    {
        /// <summary>Create an instance of the Rewards class </summary>
        /// <param name="currencies">A list of CurrencyExchangeItem describing the currencies rewarded as part of this purchase.</param>
        /// <param name="inventory">A list of InventoryExchangeItem describing the items rewarded as part of this purchase.</param>
        [Preserve]
        [JsonConstructor]
        public Rewards(List<CurrencyExchangeItem> currencies, List<InventoryExchangeItem> inventory)
        {
            Currency = currencies;
            Inventory = inventory;
        }

        /// <summary>
        /// A list of CurrencyExchangeItem describing the currencies rewarded as part of this purchase.
        /// </summary>
        [Preserve] public List<CurrencyExchangeItem> Currency;

        /// <summary>
        /// A list of InventoryExchangeItem describing the items rewarded as part of this purchase.
        /// </summary>
        [Preserve] public List<InventoryExchangeItem> Inventory;
    }
}
