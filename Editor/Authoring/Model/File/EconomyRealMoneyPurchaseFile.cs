using System;
using Newtonsoft.Json;
using Unity.Services.Economy.Editor.Authoring.Core.Model;
namespace Unity.Services.Economy.Editor.Authoring.Model.File
{
    class EconomyRealMoneyPurchaseFile : EconomyResourceFile
    {
        [JsonProperty("$schema")]
        public string Value = "https://ugs-config-schemas.unity3d.com/v1/economy/economy-real-purchase.schema.json";

        [JsonRequired]
        public StoreIdentifiers StoreIdentifiers;
        [JsonRequired]
        public RealMoneyReward[] Rewards;

        [JsonIgnore]
        public string Extension => EconomyResourcesExtensions.MoneyPurchase;

        [JsonIgnore]
        public string FileBodyText
        {
            get
            {
                var virtualPurchase = new EconomyRealMoneyPurchaseFile
                {
                    Name = "My Real Money Purchase",
                    StoreIdentifiers = new()
                    {
                        GooglePlayStore = "123"
                    },
                    Rewards = new[]
                    {
                        new RealMoneyReward
                        {
                            Amount = 6,
                            ResourceId = "MY_RESOURCE_ID"
                        }
                    }
                };

                return JsonConvert.SerializeObject(virtualPurchase, JsonSerializerSettings);
            }
        }

        [JsonConstructor]
        public EconomyRealMoneyPurchaseFile()
            : base(EconomyResourceTypes.MoneyPurchase)
        {
            StoreIdentifiers = new StoreIdentifiers();
            Rewards = Array.Empty<RealMoneyReward>();
        }
    }
}
