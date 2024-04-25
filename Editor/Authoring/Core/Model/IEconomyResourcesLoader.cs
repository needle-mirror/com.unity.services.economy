using System.Threading;
using System.Threading.Tasks;

namespace Unity.Services.Economy.Editor.Authoring.Core.Model
{
    interface IEconomyResourcesLoader
    {
        Task<IEconomyResource> LoadResourceAsync(
            string path,
            CancellationToken cancellationToken);

        string CreateAndSerialize(IEconomyResource resource);
    }
}
