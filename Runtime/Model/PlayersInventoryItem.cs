using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy.Internal.Http;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Represents a single player's inventory item. This is an inventory item unique to a specific player.
    /// </summary>
    [Preserve]
    public class PlayersInventoryItem
    {
        /// <summary>Creates an instance of the PlayersInventoryItem</summary>
        /// <param name="playersInventoryItemId">The ID of the unique item specific to this player's inventory.</param>
        /// <param name="inventoryItemId">The configuration ID of the inventory item.</param>
        /// <param name="instanceData">Any instance data specific to this unique item in the player's inventory.</param>
        /// <param name="writeLock">The current WriteLock string.</param>
        /// <param name="created">The date this players inventory item was created as an EconomyDate object.</param>
        /// <param name="modified">The date this players inventory item was modified as an EconomyDate object.</param>
        [Preserve]
        public PlayersInventoryItem(string playersInventoryItemId = default(string), string inventoryItemId = default(string), IDeserializable instanceData = default(IDeserializable),
                                    string writeLock = default(string), EconomyDate created = default(EconomyDate), EconomyDate modified = default(EconomyDate))
        {
            PlayersInventoryItemId = playersInventoryItemId;
            InventoryItemId = inventoryItemId;
            InstanceData = instanceData;
            WriteLock = writeLock;
            Created = created;
            Modified = modified;
        }

        /// <summary>
        /// The ID of the unique item specific to this player's inventory.
        /// </summary>
        [Preserve] public string PlayersInventoryItemId;
        /// <summary>
        /// The configuration ID of the inventory item.
        /// </summary>
        [Preserve] public string InventoryItemId;
        /// <summary>
        /// Any instance data specific to this unique item in the player's inventory.
        /// </summary>
        [Preserve] public IDeserializable InstanceData;
        /// <summary>
        /// The current WriteLock string.
        /// </summary>
        [Preserve] public string WriteLock;
        /// <summary>
        /// The date this players inventory item was created as an EconomyDate object.
        /// </summary>
        [Preserve] public EconomyDate Created;
        /// <summary>
        /// The date this players inventory item was modified as an EconomyDate object.
        /// </summary>
        [Preserve] public EconomyDate Modified;

        /// <summary>
        /// Gets the configuration definition associated with this player's inventory item.
        /// </summary>
        /// <returns>The InventoryItemDefinition associated with this player's inventory item</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        public InventoryItemDefinition GetItemDefinition()
        {
            return EconomyService.Instance.Configuration.GetInventoryItem(InventoryItemId);
        }

        /// <summary>
        /// Gets the configuration definition associated with this player's inventory item.
        /// </summary>
        /// <returns>The InventoryItemDefinition associated with this player's inventory item</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Obsolete("This has been replaced with GetItemDefinition which is not asynchronous and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<InventoryItemDefinition> GetItemDefinitionAsync()
        {
            return await EconomyService.Instance.Configuration.GetInventoryItemAsync(InventoryItemId);
        }
    }
}
