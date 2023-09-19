using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.Core.Service
{
    interface IEconomyClient
    {
        Task Update(IEconomyResource economyResource, CancellationToken token = default);
        Task Create(IEconomyResource economyResource, CancellationToken token = default);
        Task Delete(string resourceId, CancellationToken token = default);
        Task<List<IEconomyResource>> List(CancellationToken token = default);
        Task Publish(CancellationToken token = default);
    }
}
