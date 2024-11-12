using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a currency that was part of a purchase.
    /// </summary>
    [Preserve]
    public class CurrencyExchangeItem
    {
        /// <summary>Creates an instance of the CurrencyExchangeItem </summary>
        /// <param name="id">Currency ID</param>
        /// <param name="amount">Amount of currency</param>
        [Preserve]
        [JsonConstructor]
        public CurrencyExchangeItem(string id, int amount)
        {
            Id = id;
            Amount = amount;
        }
        /// <summary>
        /// The ID of the currency.
        /// </summary>
        [Preserve] public string Id;
        /// <summary>
        /// The amount of this currency that was used/rewarded in the purchase.
        /// </summary>
        [Preserve] public int Amount;
    }
}
