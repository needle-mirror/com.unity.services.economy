using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.Core.Editor;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Configuration;
using Unity.Services.Economy.Client.Apis.EconomyAdmin;
using Unity.Services.Economy.Client.EconomyAdmin;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Core.Service;
using Unity.Services.Economy.Editor.Authoring.Model;
using Unity.Services.Economy.Editor.Authoring.Shared.Clients;
using AddConfigResourceRequest = Unity.Services.Economy.Client.EconomyAdmin.AddConfigResourceRequest;

namespace Unity.Services.Economy.Editor.Authoring.AdminApi
{
    class EconomyClient : IEconomyClient
    {
        readonly IEconomyAdminApiClient m_Client;
        readonly IEnvironmentProvider m_EnvironmentProvider;
        readonly IProjectIdProvider m_ProjectIdProvider;
        readonly IAccessTokens m_AccessTokens;
        readonly IEconomyClientParserHelper m_ParserHelper;
        internal const float k_PostOpProgress = 66;
        const string k_EnvironmentNotFoundMsg =
            "Environment not found. Please set your environment through the Services window under \"environments\".";

        public EconomyClient(
            IEnvironmentProvider environmentProvider,
            IProjectIdProvider projectIdProvider,
            IEconomyAdminApiClient client,
            IAccessTokens accessTokens,
            IEconomyClientParserHelper parserHelper)
        {
            m_EnvironmentProvider = environmentProvider;
            m_ProjectIdProvider = projectIdProvider;
            m_Client = client;
            m_AccessTokens = accessTokens;
            m_ParserHelper = parserHelper;
        }

        public async Task Update(IEconomyResource economyResource, CancellationToken token = default)
        {
            await UpdateToken();
            (object obj, Type type) requestData = m_ParserHelper.GetClientRequestFromEconomyResource(economyResource);

            var request = new EditConfigResourceRequest(
                m_ProjectIdProvider.ProjectId,
                Guid.Parse(m_EnvironmentProvider.Current),
                economyResource.Id,
                new Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models.AddConfigResourceRequest(
                    requestData.obj,
                    requestData.type));

            await m_Client.EditConfigResourceAsync(request);
            economyResource.Progress = k_PostOpProgress;
        }

        public async Task Create(IEconomyResource economyResource, CancellationToken token = default)
        {
            await UpdateToken();
            (object obj, Type type) requestData = m_ParserHelper.GetClientRequestFromEconomyResource(economyResource);

            var request = new AddConfigResourceRequest(
                m_ProjectIdProvider.ProjectId,
                Guid.Parse(m_EnvironmentProvider.Current),
                new Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models.AddConfigResourceRequest(
                    requestData.obj,
                    requestData.type));

            await m_Client.AddConfigResourceAsync(request);
            economyResource.Progress = k_PostOpProgress;
        }

        public async Task Publish(CancellationToken token = default)
        {
            await UpdateToken();
            var request = new PublishEconomyRequest(
                    m_ProjectIdProvider.ProjectId,
                    Guid.Parse(m_EnvironmentProvider.Current), new PublishBody(true));

            await m_Client.PublishEconomyAsync(request);
        }

        public async Task Delete(string resourceId, CancellationToken token = default)
        {
            await UpdateToken();
            var request = new DeleteConfigResourceRequest(
                    m_ProjectIdProvider.ProjectId,
                    Guid.Parse(m_EnvironmentProvider.Current),
                    resourceId);

            await m_Client.DeleteConfigResourceAsync(request);
        }

        public async Task<List<IEconomyResource>> List(CancellationToken token = default)
        {
            await UpdateToken();
            var request = new GetResourcesRequest(
                    m_ProjectIdProvider.ProjectId,
                    Guid.Parse(m_EnvironmentProvider.Current));

            var response = await m_Client.GetResourcesAsync(request);

            if (response?.Result?.Results == null)
            {
                return new List<IEconomyResource>();
            }

            return GetResourcesFromList(response.Result.Results);
        }

        List<IEconomyResource> GetResourcesFromList(
            List<GetResourcesResponseResultsInner> remoteResources,
            CancellationToken token = default)
        {
            var resources = new List<IEconomyResource>();

            foreach (var remoteResource in remoteResources)
            {
                resources.Add(m_ParserHelper.GetEconomyResourceFromListResponse(remoteResource));

                if (token.IsCancellationRequested)
                {
                    break;
                }
            }

            return resources;
        }

        async Task UpdateToken()
        {
            var client = m_Client as EconomyAdminApiClient;
            if (client == null)
                return;

            string token = await m_AccessTokens.GetServicesGatewayTokenAsync();
            var headers = new AdminApiHeaders<EconomyClient>(token);

            client.Configuration = new Configuration(
                null,
                null,
                null,
                headers.ToDictionary());
        }
    }
}
