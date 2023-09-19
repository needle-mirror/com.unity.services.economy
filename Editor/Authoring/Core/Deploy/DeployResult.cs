using System.Collections.Generic;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.Core.Deploy
{
    class DeployResult
    {
        public List<IEconomyResource> Created { get; set; }
        public List<IEconomyResource> Updated { get; set; }
        public List<IEconomyResource> Deleted { get; set; }
        public List<IEconomyResource> Deployed { get; set; }
        public List<IEconomyResource> Failed { get; set; }
    }
}
