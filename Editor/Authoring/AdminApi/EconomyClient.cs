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
using Unity.Services.Economy.Editor.Authoring.Deployment;
using Unity.Services.Economy.Editor.Authoring.Model;
using Unity.Services.Economy.Editor.Authoring.Shared.Clients;
using Unity.Services.Economy.Editor.Authoring.Shared.Infrastructure.Collections;
using AddConfigResourceRequest = Unity.Services.Economy.Client.EconomyAdmin.AddConfigResourceRequest;

namespace Unity.Services.Economy.Editor.Authoring.AdminApi
{
    class EconomyClient : IEconomyClient
    {
        readonly IEconomyAdminApiClient m_Client;
        readonly IEnvironmentProvider m_EnvironmentProvider;
        readonly IProjectIdProvider m_ProjectIdProvider;
        readonly IAccessTokens m_AccessTokens;
        readonly IEconomyResourceSerializationUtility m_SerializationUtility;
        internal const float k_PostOpProgress = 66;

        public EconomyClient(
            IEnvironmentProvider environmentProvider,
            IProjectIdProvider projectIdProvider,
            IEconomyAdminApiClient client,
            IAccessTokens accessTokens,
            IEconomyResourceSerializationUtility serializationUtility)
        {
            m_EnvironmentProvider = environmentProvider;
            m_ProjectIdProvider = projectIdProvider;
            m_Client = client;
            m_AccessTokens = accessTokens;
            m_SerializationUtility = serializationUtility;
        }

        public async Task Update(IEconomyResource economyResource, CancellationToken token = default)
        {
            await UpdateToken();
            (object obj, Type type) requestData = GetDataAndTypeFromEconomyResource(economyResource);

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
            (object obj, Type type) requestData = GetDataAndTypeFromEconomyResource(economyResource);

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
                resources.Add(m_SerializationUtility
                    .GetEconomyResourceFromGetResourcesResponseResultsInner(remoteResource));

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

        (object, Type) GetDataAndTypeFromEconomyResource(IEconomyResource economyResource)
        {
            var errorMsg = "Could not cast Economy Resource to Economy Resource type. Make sure the file " +
                           "contains valid JSON and follows the structure of valid economy files.";

            switch (economyResource.EconomyType)
            {
                case EconomyResourceTypes.Currency:
                    var currencyResource = economyResource as EconomyCurrency;

                    if (currencyResource == null)
                    {
                        throw new InvalidCastException(errorMsg);
                    }

                    var currencyRequest = new CurrencyItemRequest(
                        currencyResource.Id,
                        currencyResource.Name,
                        CurrencyItemRequest.TypeOptions.CURRENCY,
                        currencyResource.Initial,
                        currencyResource.Max ?? 0,
                        currencyResource.CustomData);
                    return (currencyRequest, typeof(CurrencyItemRequest));
                case EconomyResourceTypes.InventoryItem:
                    var inventoryResource = economyResource as EconomyInventoryItem;

                    if (inventoryResource == null)
                    {
                        throw new InvalidCastException(errorMsg);
                    }

                    var inventoryItemRequest = new InventoryItemRequest(
                        inventoryResource.Id,
                        inventoryResource.Name,
                        InventoryItemRequest.TypeOptions.INVENTORYITEM,
                        inventoryResource.CustomData);
                    return (inventoryItemRequest, typeof(InventoryItemRequest));
                case EconomyResourceTypes.MoneyPurchase:
                    var moneyPurchaseResource = economyResource as EconomyRealMoneyPurchase;

                    if (moneyPurchaseResource == null)
                    {
                        throw new InvalidCastException(errorMsg);
                    }

                    var storeIds = new RealMoneyPurchaseItemRequestStoreIdentifiers(
                        moneyPurchaseResource.StoreIdentifiers?.AppleAppStore,
                        moneyPurchaseResource.StoreIdentifiers?.GooglePlayStore);

                    var rmpRewards = new List<RealMoneyPurchaseResourceRequestRewardsInner>();

                    moneyPurchaseResource.Rewards?.ForEach(r =>
                        rmpRewards.Add(new RealMoneyPurchaseResourceRequestRewardsInner(r.ResourceId, r.Amount)));

                    var moneyPurchaseRequest = new RealMoneyPurchaseResourceRequest(
                        moneyPurchaseResource.Id,
                        moneyPurchaseResource.Name,
                        RealMoneyPurchaseResourceRequest.TypeOptions.MONEYPURCHASE,
                        storeIds,
                        rmpRewards,
                        moneyPurchaseResource.CustomData);
                    return (moneyPurchaseRequest, typeof(RealMoneyPurchaseItemRequest));
                case EconomyResourceTypes.VirtualPurchase:
                    var virtualPurchaseResource = economyResource as EconomyVirtualPurchase;

                    if (virtualPurchaseResource == null)
                    {
                        throw new InvalidCastException(errorMsg);
                    }

                    var costs = new List<VirtualPurchaseResourceRequestCostsInner>();
                    var vpRewards = new List<VirtualPurchaseResourceRequestRewardsInner>();

                    virtualPurchaseResource.Costs?.ForEach(c =>
                        costs.Add(new VirtualPurchaseResourceRequestCostsInner(c.ResourceId, c.Amount)));

                    virtualPurchaseResource.Rewards?.ForEach(r =>
                        vpRewards.Add(new VirtualPurchaseResourceRequestRewardsInner(r.ResourceId, r.Amount)));

                    var virtualPurchaseRequest = new VirtualPurchaseResourceRequest(
                        virtualPurchaseResource.Id,
                        virtualPurchaseResource.Name,
                        VirtualPurchaseResourceRequest.TypeOptions.VIRTUALPURCHASE,
                        costs,
                        vpRewards,
                        virtualPurchaseResource.CustomData);
                    return (virtualPurchaseRequest, typeof(VirtualPurchaseItemRequest));
            }
            throw new InvalidCastException(errorMsg);
        }
    }
}
