#nullable enable
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Unity.Services.Economy.Editor.Authoring.Core.IO;

namespace Unity.Services.Economy.Editor.Authoring.IO
{
    class EconomyJsonConverter : IEconomyJsonConverter
    {
        readonly JsonSerializerSettings m_JsonSerializerSettings = new()
        {
            Converters = { new StringEnumConverter() },
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public T? DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string SerializeObject(object? value)
        {
            return JsonConvert.SerializeObject(value, m_JsonSerializerSettings);
        }
    }
}
