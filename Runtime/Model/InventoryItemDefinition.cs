using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy.Internal.Models;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a single inventory item configuration.
    /// </summary>
    [Preserve]
    public class InventoryItemDefinition : ConfigurationItemDefinition
    {
        /// <summary>Creates an instance of the InventoryItemDefinition </summary>
        [Preserve]
        public InventoryItemDefinition()
        {
        }

        [Preserve]
        internal InventoryItemDefinition(InventoryItemResource resource)
        {
            Id = resource.Id;
            Name = resource.Name;
            Type = ConfigurationInternal.InventoryItemType;
            Created = EconomyDate.From(resource.Created);
            Modified = EconomyDate.From(resource.Modified);
            CustomDataDeserializable = resource.CustomData;
#pragma warning disable CS0618
            // obsolete member
            CustomData = JsonConvert.DeserializeObject<Dictionary<string, object>>(resource.CustomData.GetAsString());
#pragma warning disable CS0618
        }

        /// <summary>
        /// Gets all the PlayersInventoryItems of this inventory item for the currently signed in player
        /// </summary>
        /// <returns>A GetInventoryResult with all the PlayersInventoryItems of this inventory item</returns>
        public async Task<GetInventoryResult> GetAllPlayersInventoryItemsAsync()
        {
            GetInventoryOptions options = new GetInventoryOptions
            {
                InventoryItemIds = new List<string> { Id }
            };
            return await EconomyService.Instance.PlayerInventory.GetInventoryAsync(options);
        }
    }
}
