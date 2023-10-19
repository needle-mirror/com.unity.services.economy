using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Shared.Infrastructure.Collections;

namespace Unity.Services.Economy.Editor.Authoring.AdminApi
{
    class EconomyClientParserHelper : IEconomyClientParserHelper
    {
        public (object, Type) GetClientRequestFromEconomyResource(IEconomyResource economyResource)
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
                    return (moneyPurchaseRequest, typeof(RealMoneyPurchaseResourceRequest));
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
                    return (virtualPurchaseRequest, typeof(VirtualPurchaseResourceRequest));
            }
            throw new InvalidCastException(errorMsg);
        }

        public EconomyResource GetEconomyResourceFromListResponse(GetResourcesResponseResultsInner resource)
        {
            switch (resource.Type)
            {
                case GetResourcesResponseResultsInner.TypeOptions.CURRENCY:
                    return new EconomyCurrency(resource.Id)
                    {
                        Name = resource.Name,
                        Initial = resource.Initial,
                        Max = resource.Max,
                        CustomData = resource.CustomData
                    };
                case GetResourcesResponseResultsInner.TypeOptions.INVENTORYITEM:
                    return new EconomyInventoryItem(resource.Id)
                    {
                        Name = resource.Name,
                        CustomData = resource.CustomData
                    };
                case GetResourcesResponseResultsInner.TypeOptions.MONEYPURCHASE:
                    var rmpRewards = new List<RealMoneyReward>();
                    resource.Rewards.ForEach(r =>
                        rmpRewards.Add(new RealMoneyReward
                        {
                            ResourceId = r.ResourceId,
                            Amount = r.Amount
                        }));

                    var storeIdentifiers = new StoreIdentifiers
                    {
                        AppleAppStore = resource.StoreIdentifiers?.AppleAppStore,
                        GooglePlayStore = resource.StoreIdentifiers?.GooglePlayStore
                    };

                    return new EconomyRealMoneyPurchase(resource.Id)
                    {
                        Name = resource.Name,
                        Rewards = rmpRewards.ToArray(),
                        StoreIdentifiers = storeIdentifiers,
                        CustomData = resource.CustomData
                    };
                case GetResourcesResponseResultsInner.TypeOptions.VIRTUALPURCHASE:
                    var costs = new List<Cost>();
                    var rewards = new List<Reward>();

                    resource.Costs.ForEach(c => costs.Add(
                        new Cost
                        {
                            ResourceId = c.ResourceId,
                            Amount = c.Amount
                        }));

                    resource.Rewards.ForEach(r => rewards.Add(
                        new Reward
                        {
                            ResourceId = r.ResourceId,
                            Amount = r.Amount
                        }));

                    return new EconomyVirtualPurchase(resource.Id)
                    {
                        Name = resource.Name,
                        Costs = costs.ToArray(),
                        Rewards = rewards.ToArray(),
                        CustomData = resource.CustomData
                    };
            }

            throw new SerializationException(
                "Could not map the contents of the remote resource with any local resource types.");
        }
    }
}
