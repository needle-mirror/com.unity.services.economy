using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Unity.Services.Economy.Editor.Authoring.Model.File
{
    abstract class EconomyResourceFile : IEconomyResourceFile
    {
        public string Id { get; set; }
        [JsonRequired]
        public string Name { get; set; }

        [JsonIgnore] //specialized class will set this field, it should not be stored as it is inferred from the file-type
        public string Type { get; set; }
        public object CustomData { get; set; }

        protected EconomyResourceFile(string type, string id = null, string name = "Display Name", object customData = null)
        {
            Id = id;
            Name = name;
            Type = type;
            CustomData = customData;
        }

        internal readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            Converters = { new StringEnumConverter() },
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }
}
