using System;
using Unity.Services.Economy.Internal.Models;

namespace Unity.Services.Economy.Model
{
    public class ConfigurationResource
    {
        public object Value { get; }
        public Type Type { get; }

        internal ConfigurationResource(object value, Type type)
        {
            Value = value;
            Type = type;
        }
    }
}
