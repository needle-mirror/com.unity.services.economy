using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.DeploymentApi.Editor;
using Unity.Services.Economy.Editor.Authoring.Core.IO;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
using Unity.Services.Economy.Editor.Authoring.Model.File;
using Cost = Unity.Services.Economy.Editor.Authoring.Core.Model.Cost;
using RealMoneyReward = Unity.Services.Economy.Editor.Authoring.Core.Model.RealMoneyReward;
using StoreIdentifiers = Unity.Services.Economy.Editor.Authoring.Core.Model.StoreIdentifiers;

namespace Unity.Services.Economy.Editor.Authoring.Deployment
{
    class EconomyResourcesLoader : IEconomyResourcesLoader
    {
        readonly IEconomyJsonConverter m_EconomyJsonConverter;
        readonly IFileSystem m_FileSystem;

        public EconomyResourcesLoader(
            IEconomyJsonConverter economyJsonConverter,
            IFileSystem fileSystem)
        {
            m_EconomyJsonConverter = economyJsonConverter;
            m_FileSystem = fileSystem;
        }

        public string CreateAndSerialize(IEconomyResource resource)
        {
            EconomyResourceFile resourceFile = null;

            var resourceId = Path.GetFileNameWithoutExtension(resource.Path) == resource.Id ? null : resource.Id;

            switch (resource)
            {
                case EconomyCurrency economyCurrency:
                    resourceFile = new EconomyCurrencyFile()
                    {
                        Id = resourceId,
                        Name = economyCurrency.Name,
                        Initial = economyCurrency.Initial,
                        Max = economyCurrency.Max,
                        CustomData = economyCurrency.CustomData,
                        Type = economyCurrency.Type,
                    };
                    break;
                case EconomyInventoryItem economyInventoryItem:
                    resourceFile = new EconomyInventoryItemFile()
                    {
                        Id = resourceId,
                        Name = economyInventoryItem.Name,
                        CustomData = economyInventoryItem.CustomData,
                        Type = economyInventoryItem.Type,
                    };
                    break;
                case EconomyVirtualPurchase economyVirtualPurchase:
                    resourceFile = new EconomyVirtualPurchaseFile
                    {
                        Id = resourceId,
                        Name = economyVirtualPurchase.Name,
                        CustomData = economyVirtualPurchase.CustomData,
                        Costs = economyVirtualPurchase.Costs
                            .Select(cost =>
                                new Cost()
                                {
                                    ResourceId = cost.ResourceId,
                                    Amount = cost.Amount
                                })
                            .ToArray(),
                        Rewards = economyVirtualPurchase.Rewards,
                        Type = economyVirtualPurchase.Type,
                    };
                    break;
                case EconomyRealMoneyPurchase economyRealMoneyPurchase:
                    resourceFile = new EconomyRealMoneyPurchaseFile
                    {
                        Id = resourceId,
                        Name = economyRealMoneyPurchase.Name,
                        CustomData = economyRealMoneyPurchase.CustomData,
                        Rewards = economyRealMoneyPurchase.Rewards.Select(
                            realMoneyReward => new RealMoneyReward()
                            {
                                ResourceId = realMoneyReward.ResourceId,
                                Amount = realMoneyReward.Amount
                            }).ToArray(),
                        StoreIdentifiers = new()
                        {
                            AppleAppStore = economyRealMoneyPurchase.StoreIdentifiers?.AppleAppStore,
                            GooglePlayStore = economyRealMoneyPurchase.StoreIdentifiers?.GooglePlayStore
                        },
                        Type = economyRealMoneyPurchase.Type,
                    };
                    break;
            }

            if (resourceFile == null)
            {
                throw new SerializationException($"Error - {resource.Id} is not a valid resource.");
            }

            return m_EconomyJsonConverter.SerializeObject(resourceFile);
        }

        public async Task<IEconomyResource> LoadResourceAsync(
            string path,
            CancellationToken cancellationToken)
        {
            var fileId = Path.GetFileNameWithoutExtension(path);

            IEconomyResource resource = new EconomyResource(fileId)
            {
                Path = path,
                Name = fileId
            };

            try
            {
                var fileText = await m_FileSystem.ReadAllText(path, cancellationToken);
                var fileExtension = Path.GetExtension(path);

                switch (fileExtension)
                {
                    case EconomyResourcesExtensions.Currency:
                        resource = LoadEconomyCurrencyResource(path, fileId, fileText);
                        break;
                    case EconomyResourcesExtensions.InventoryItem:
                        resource = LoadEconomyInventoryItemResource(path, fileId, fileText);
                        break;
                    case EconomyResourcesExtensions.VirtualPurchase:
                        resource = LoadEconomyVirtualPurchaseResource(path, fileId, fileText);
                        break;
                    case EconomyResourcesExtensions.MoneyPurchase:
                        resource = LoadEconomyRealMoneyPurchaseResource(path, fileId, fileText);
                        break;
                    default:
                        throw new InvalidOperationException($"Error - File : {path} - does not match any valid economy resource extension");
                }

                resource.Status = new DeploymentStatus(Statuses.Loaded, "");
            }
            catch (Exception ex) when (ex is SerializationException or FileNotFoundException or
                                       InvalidOperationException or JsonSerializationException or JsonReaderException)
            {
                resource.Status = new DeploymentStatus(Statuses.FailedToRead, ex.Message, SeverityLevel.Error);
            }

            return resource;
        }

        IEconomyResource LoadEconomyRealMoneyPurchaseResource(string path, string fileId, string resourceFileText)
        {
            var economyFile = m_EconomyJsonConverter.DeserializeObject<EconomyRealMoneyPurchaseFile>(resourceFileText);

            if (economyFile == null)
            {
                throw new SerializationException($"Error - File : {path} - does not match valid economy real money purchase resource");
            }

            return new EconomyRealMoneyPurchase(economyFile.Id ?? fileId)
            {
                Name = economyFile.Name,
                Rewards = economyFile.Rewards.Select(
                    economyFileReward => new RealMoneyReward()
                    {
                        ResourceId = economyFileReward.ResourceId,
                        Amount = economyFileReward.Amount
                    }).ToArray(),
                StoreIdentifiers = new StoreIdentifiers()
                {
                    AppleAppStore = economyFile.StoreIdentifiers?.AppleAppStore,
                    GooglePlayStore = economyFile.StoreIdentifiers?.GooglePlayStore
                },
                CustomData = economyFile.CustomData,
                Path = path
            };
        }

        IEconomyResource LoadEconomyVirtualPurchaseResource(string path, string fileId, string resourceFileText)
        {
            var economyFile = m_EconomyJsonConverter.DeserializeObject<EconomyVirtualPurchaseFile>(resourceFileText);

            if (economyFile == null)
            {
                throw new SerializationException($"Error - File : {path} - does not match valid economy virtual purchase resource");
            }

            return new EconomyVirtualPurchase(economyFile.Id ?? fileId)
            {
                Name = economyFile.Name,
                Costs = economyFile.Costs.Select(
                    economyFileCost => new Cost
                    {
                        ResourceId = economyFileCost.ResourceId,
                        Amount = economyFileCost.Amount
                    }).ToArray(),
                Rewards = economyFile.Rewards,
                CustomData = economyFile.CustomData,
                Path = path
            };
        }

        IEconomyResource LoadEconomyInventoryItemResource(string path, string fileId, string resourceFileText)
        {
            var economyFile = m_EconomyJsonConverter.DeserializeObject<EconomyInventoryItemFile>(resourceFileText);

            if (economyFile == null)
            {
                throw new SerializationException($"Error - File : {path} - does not match valid economy inventory item resource");
            }

            return new EconomyInventoryItem(economyFile.Id ?? fileId)
            {
                Name = economyFile.Name,
                CustomData = economyFile.CustomData,
                Path = path
            };
        }

        IEconomyResource LoadEconomyCurrencyResource(string path, string fileId, string resourceFileText)
        {
            var economyFile = m_EconomyJsonConverter.DeserializeObject<EconomyCurrencyFile>(resourceFileText);

            if (economyFile == null)
            {
                throw new SerializationException($"Error - File : {path} - does not match valid economy currency resource");
            }

            return new EconomyCurrency(economyFile.Id ?? fileId)
            {
                Name = economyFile.Name,
                Initial = economyFile.Initial,
                Max = economyFile.Max ?? 0,
                CustomData = economyFile.CustomData,
                Path = path
            };
        }
    }
}
