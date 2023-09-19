using System.Collections.Generic;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.Core.Fetch
{
    class FetchResult
    {
        public List<IEconomyResource> Created { get; set; }
        public List<IEconomyResource> Updated { get; set; }
        public List<IEconomyResource> Deleted { get; set; }
        public List<IEconomyResource> Fetched { get; set; }
        public List<IEconomyResource> Failed { get; set; }
    }
}
