using System.Collections.Generic;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// Provides access to the configuration retrieved.
    /// </summary>
    [Preserve]
    internal class GetConfigurationResult
    {
        /// <summary>
        /// The configuration resource definitions.
        /// </summary>
        [Preserve]
        public List<ConfigurationItemDefinition> Results;

        /// <summary>
        /// Contains information about the results.
        /// </summary>
        [Preserve]
        public ConfigurationMetadata Metadata { get; }

        [Preserve]
        internal GetConfigurationResult(List<ConfigurationItemDefinition> results, ConfigurationMetadata metadata)
        {
            Results = results;
            Metadata = metadata;
        }
    }
}
