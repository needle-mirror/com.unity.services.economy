//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.GameBackend.Economy.Http
{
    /// <summary>
    /// JsonObjectConverter overrides behaviour from JsonConverter to allow
    /// encapsulation of raw object types and conversion between different
    /// types.
    /// </summary>
    [Preserve]
    internal class JsonObjectConverter : JsonConverter
    {
        ///<inheritdoc cref="JsonConverter"/>
        /// <summary>Convert a JsonObject to JToken.</summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JsonObject jobj = (JsonObject) value;

            if (jobj.obj == null)
            {
                writer.WriteNull();
                return;
            }

            JToken t = JToken.FromObject(jobj.obj);
            t.WriteTo(writer);
        }

        ///<inheritdoc cref="JsonConverter"/>
        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        ///<inheritdoc cref="JsonConverter"/>
        public override bool CanConvert(System.Type objectType)
        {
            throw new System.NotImplementedException();
        }
    }
}