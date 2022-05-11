using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Provides paginated access to the list of balances retrieved and allows you to retrieve the next page.
    /// </summary>
    [Preserve]
    public class GetBalancesResult : PageableResult<PlayerBalance, GetBalancesResult>
    {
        /// <summary>
        /// The list of currently fetched balances.
        /// </summary>
        [Preserve] public List<PlayerBalance> Balances => m_Results;

        readonly PlayerBalancesInternal m_PlayerBalancesInternal;

        [Preserve]
        internal GetBalancesResult(List<PlayerBalance> results, bool hasNext, PlayerBalancesInternal playerBalancesInternal)
            : base(results, hasNext)
        {
            m_PlayerBalancesInternal = playerBalancesInternal;
        }

        /// <summary>
        /// Fetches the next set of results.
        /// </summary>
        /// <param name="itemsPerFetch">The number of items to fetch. Can be between 1-100 inclusive and defaults to 20.</param>
        /// <returns>A new GetBalancesResult</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        [Preserve]
        protected override async Task<GetBalancesResult> GetNextResultsAsync(int itemsPerFetch)
        {
            return await m_PlayerBalancesInternal.GetNextBalancesAsync(Balances.Last().CurrencyId, itemsPerFetch);
        }
    }
}
