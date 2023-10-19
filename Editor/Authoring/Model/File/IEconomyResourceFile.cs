using Newtonsoft.Json;

namespace Unity.Services.Economy.Editor.Authoring.Model.File
{
    interface IEconomyResourceFile
    {
        public string Id { get; set; }
        [JsonRequired]
        public string Name { get; set; }

        [JsonIgnore] //specialized class will set this field, it should not be stored as it is inferred from the file-type
        public string Type { get; set; }
        public object CustomData { get; set; }
    }
}
