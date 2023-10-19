using System;
using Newtonsoft.Json;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.Model.File
{
    class EconomyVirtualPurchaseFile : EconomyResourceFile
    {
        [JsonProperty("$schema")]
        public string Value = "https://ugs-config-schemas.unity3d.com/v1/economy/economy-virtual-purchase.schema.json";

        [JsonRequired]
        public Cost[] Costs;
        [JsonRequired]
        public Reward[] Rewards;

        [JsonIgnore]
        public string Extension => EconomyResourcesExtensions.VirtualPurchase;

        [JsonIgnore]
        public string FileBodyText
        {
            get
            {
                var virtualPurchase = new EconomyVirtualPurchaseFile
                {
                    Name = "My Virtual Purchase",
                    Costs = new[]
                    {
                        new Cost
                        {
                            Amount = 2,
                            ResourceId = "MY_RESOURCE_ID"
                        }
                    },
                    Rewards = new[]
                    {
                        new Reward
                        {
                            ResourceId = "MY_RESOURCE_ID_2",
                            Amount = 6,
                            DefaultInstanceData = null
                        }
                    }
                };
                return JsonConvert.SerializeObject(virtualPurchase, JsonSerializerSettings);
            }
        }

        [JsonConstructor]
        public EconomyVirtualPurchaseFile()
            : base(EconomyResourceTypes.VirtualPurchase)
        {
            Costs = Array.Empty<Cost>();
            Rewards = Array.Empty<Reward>();
        }
    }
}
