using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Unity.Services.Economy.Editor.Authoring.AdminApi.Client.Models;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using UnityEngine;

namespace Unity.Services.Economy.Editor.Authoring.Deployment
{
    class EconomyResourceSerializationUtility : IEconomyResourceSerializationUtility
    {
        internal const string k_ActionableDeserializationErrorMsg =
            "Make sure the file's content is valid JSON and follows the structure of Economy resources.";
        internal const string k_JsonDeserializationErrorMsg =
            "The economy resource file contains invalid or empty JSON.";
        internal const string k_NoTypeMatchErrorMsg =
            "Economy resource file contents did not match any valid Economy resource type.";

        [CanBeNull]
        public EconomyResource GetEconomyResourceFromJson(string fileName, string json, string type, out string message, out string details)
        {
            try
            {
                message = "";
                details = "";
                dynamic deserializedFileContent = JsonConvert.DeserializeObject(json);

                if (deserializedFileContent == null)
                {
                    message = "Deserialization Error";
                    details = string.Join(" ", k_JsonDeserializationErrorMsg + k_ActionableDeserializationErrorMsg);
                    return null;
                }

                EconomyResource resource = null;
                var serializerSettings = new JsonSerializerSettings()
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                switch (type)
                {
                    case EconomyResourcesExtensions.Currency:
                        resource = JsonConvert.DeserializeObject<EconomyCurrency>(json, serializerSettings);
                        break;
                    case EconomyResourcesExtensions.InventoryItem:
                        resource = JsonConvert.DeserializeObject<EconomyInventoryItem>(json, serializerSettings);
                        break;
                    case EconomyResourcesExtensions.MoneyPurchase:
                        resource = JsonConvert.DeserializeObject<EconomyRealMoneyPurchase>(json, serializerSettings);
                        break;
                    case EconomyResourcesExtensions.VirtualPurchase:
                        resource = JsonConvert.DeserializeObject<EconomyVirtualPurchase>(json, serializerSettings);
                        break;
                }

                if (resource == null)
                {
                    message = "Deserialization Error";
                    details = string.Join(" ", k_NoTypeMatchErrorMsg + k_ActionableDeserializationErrorMsg);
                    return null;
                }

                if (resource.Id == null)
                {
                    resource.Id = fileName;
                }

                return resource;
            }
            catch (Exception e)
            {
                message = "Serialization Error";
                details = e.Message;
                return null;
            }
        }

        public EconomyResource GetEconomyResourceFromGetResourcesResponseResultsInner(
            GetResourcesResponseResultsInner resource)
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
