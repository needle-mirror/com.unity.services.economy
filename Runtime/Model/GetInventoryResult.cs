using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Provides paginated access to the list of retrieved player's inventory items.
    /// </summary>
    [Preserve]
    public class GetInventoryResult : PageableResult<PlayersInventoryItem, GetInventoryResult>
    {
        [Preserve] List<string> m_PlayersInventoryItemIds;
        [Preserve] List<string> m_InventoryItemIds;

        readonly PlayerInventoryInternal m_PlayerInventoryInternal;

        /// <summary>
        /// The currently fetched items.
        /// </summary>
        [Preserve]
        public List<PlayersInventoryItem> PlayersInventoryItems => m_Results;

        [Preserve]
        internal GetInventoryResult(List<PlayersInventoryItem> results, bool hasNext, List<string> inventoryItemIds,
                                    List<string> playersInventoryItemIds, PlayerInventoryInternal playerInventoryInternal)
            : base(results, hasNext)
        {
            m_InventoryItemIds = inventoryItemIds;
            m_PlayersInventoryItemIds = playersInventoryItemIds;
            m_PlayerInventoryInternal = playerInventoryInternal;
        }

        /// <summary>
        /// Retrieves the next page of the players inventory items.
        /// </summary>
        /// <param name="itemsPerFetch">The number of items to fetch. Can be between 1-100 inclusive and defaults to 20.</param>
        /// <returns>A new GetInventoryResult</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Preserve]
        protected override async Task<GetInventoryResult> GetNextResultsAsync(int itemsPerFetch)
        {
            GetInventoryOptions options = new GetInventoryOptions
            {
                PlayersInventoryItemIds = m_PlayersInventoryItemIds,
                InventoryItemIds = m_InventoryItemIds,
                ItemsPerFetch = itemsPerFetch
            };

            return await m_PlayerInventoryInternal.GetNextInventory(m_Results.Last().PlayersInventoryItemId, options);
        }
    }
}
