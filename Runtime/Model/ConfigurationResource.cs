using System;
using Unity.Services.Economy.Internal.Models;

namespace Unity.Services.Economy.Model
{
    /// <summary>Represents a configuration resource </summary>
    public class ConfigurationResource
    {
        /// <summary>Value of the resource </summary>
        public object Value { get; }
        /// <summary>Type of the resource</summary>
        public Type Type { get; }

        internal ConfigurationResource(object value, Type type)
        {
            Value = value;
            Type = type;
        }
    }
}
