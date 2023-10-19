using System;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.AdminApi
{
    interface IEconomyClientParserHelper
    {
        (object, Type) GetClientRequestFromEconomyResource(IEconomyResource economyResource);

        EconomyResource GetEconomyResourceFromListResponse(GetResourcesResponseResultsInner resource);
    }
}
