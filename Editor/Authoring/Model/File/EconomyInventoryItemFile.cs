using Newtonsoft.Json;
using Unity.Services.Economy.Editor.Authoring.Core.Model;

namespace Unity.Services.Economy.Editor.Authoring.Model.File
{
    class EconomyInventoryItemFile : EconomyResourceFile
    {
        [JsonProperty("$schema")]
        public string Value = "https://ugs-config-schemas.unity3d.com/v1/economy/economy-inventory.schema.json";

        [JsonIgnore]
        public string Extension => EconomyResourcesExtensions.InventoryItem;

        [JsonIgnore]
        public string FileBodyText
        {
            get
            {
                var inventory = new EconomyInventoryItemFile();
                inventory.Name = "My Item";
                return JsonConvert.SerializeObject(inventory, JsonSerializerSettings);
            }
        }

        [JsonConstructor]
        public EconomyInventoryItemFile()
            : base(EconomyResourceTypes.InventoryItem)
        {
        }
    }
}
