using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy.Internal.Models;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a single currency configuration.
    /// </summary>
    [Preserve]
    public class CurrencyDefinition : ConfigurationItemDefinition
    {
        /// <summary>
        /// The amount of currency a player initially is given.
        /// </summary>
        [Preserve][JsonProperty("initial")][JsonRequired] public int Initial;
        /// <summary>
        /// (Optional, a value of 0 indicates no maximum) The maximum amount of this currency a player can own.
        /// </summary>
        [Preserve][JsonProperty("max")] public int Max;

        /// <summary>Creates an instance of currence definition</summary>
        [Preserve]
        public CurrencyDefinition()
        {
        }

        [Preserve]
        internal CurrencyDefinition(CurrencyResource resource)
        {
            Id = resource.Id;
            Name = resource.Name;
            Type = ConfigurationInternal.CurrencyType;
            Created = EconomyDate.From(resource.Created);
            Modified = EconomyDate.From(resource.Modified);
            Initial = resource.Initial;
            Max = resource.Max;
            CustomDataDeserializable = resource.CustomData;
#pragma warning disable CS0618
            // obsolete member
            CustomData = JsonConvert.DeserializeObject<Dictionary<string, object>>(resource.CustomData.GetAsString());
#pragma warning disable CS0618
        }

        /// <summary>
        /// Gets the current balance of this currency for the currently signed in player.
        /// It is equivalent to the balance for this currency retrieved from EconomyService.Internal.PlayerBalances.GetBalancesAsync()
        /// </summary>
        /// <returns>A PlayerBalance object containing the currency balance for this currency.</returns>
        public async Task<PlayerBalance> GetPlayerBalanceAsync()
        {
            // Note: Could be simplified and performance improved by a "GetBalance" method being available on the API
            GetBalancesResult result = await EconomyService.Instance.PlayerBalances.GetBalancesAsync();
            return result.Balances.Find(b => b.CurrencyId == Id);
        }
    }
}
