using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Economy.Internal.Models;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary> EconomyDate data </summary>
    [Preserve]
    public class EconomyDate
    {
        /// <summary> The date </summary>
        [Preserve] [JsonProperty("date")] public DateTime Date;

        internal static EconomyDate From(ModifiedMetadata data) =>
            data.Date.HasValue ? new EconomyDate() { Date = data.Date.Value } : null;
    }

    /// <summary>
    /// The base class for the more specific configuration types, e.g. CurrencyDefinition. These are used to define
    /// the resources that you create in the Unity Dashboard.
    /// </summary>
    [Preserve]
    public class ConfigurationItemDefinition
    {
        /// <summary>
        /// The configuration ID of the resource.
        /// </summary>
        [Preserve] [JsonProperty("id")] [JsonRequired] public string Id;
        /// <summary>
        /// The name of the resource.
        /// </summary>
        [Preserve] [JsonProperty("name")] [JsonRequired] public string Name;
        /// <summary>
        /// Resource type as it appears in the Unity dashboard.
        /// </summary>
        [Preserve] [JsonProperty("type")] [JsonRequired] public string Type;
        /// <summary>
        /// Any custom data associated with this resource definition.
        /// </summary>
        [Obsolete("The interface provided by CustomData has been replaced by CustomDataDeserializable, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        [Preserve] [JsonProperty("customData")] public Dictionary<string, object> CustomData;
        /// <summary>
        /// Any custom data associated with this resource definition in a deserializable format.
        /// </summary>
        public IDeserializable CustomDataDeserializable;
        /// <summary>
        /// The date this resource was created.
        /// </summary>
        [Preserve] [JsonProperty("created")] public EconomyDate Created;
        /// <summary>
        /// The date this resource was last modified.
        /// </summary>
        [Preserve] [JsonProperty("modified")] public EconomyDate Modified;
    }
}
