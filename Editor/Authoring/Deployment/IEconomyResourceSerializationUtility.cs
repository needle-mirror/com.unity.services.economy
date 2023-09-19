using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.Deployment
{
    interface IEconomyResourceSerializationUtility
    {
        EconomyResource GetEconomyResourceFromJson(string fileName, string json, string type, out string message, out string details);
        EconomyResource GetEconomyResourceFromGetResourcesResponseResultsInner(
            GetResourcesResponseResultsInner resource);
    }
}
