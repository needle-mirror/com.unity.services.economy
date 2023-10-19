#nullable enable
namespace Unity.Services.Economy.Editor.Authoring.Core.IO
{
    interface IEconomyJsonConverter
    {
        public T? DeserializeObject<T>(string value);

        public string SerializeObject(object? value);
    }
}
