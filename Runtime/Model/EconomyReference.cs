using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// A reference to another resource definition from within a purchase.
    /// </summary>
    [Preserve]
    public class EconomyReference
    {
        [Preserve]
        ConfigurationItemDefinition m_ReferencedItem;

        [Preserve]
        [JsonConstructor]
        public EconomyReference([JsonProperty("$ref_economy")] string reference)
        {
            // We need to access the internal property "remoteConfig" - so we cast to a Configuration object to access
            // internal properties
            ConfigurationInternal configurationInternal = (ConfigurationInternal)EconomyService.Instance.Configuration;

            string referenceWithoutLeadingHash = reference.Substring(1);
            m_ReferencedItem = configurationInternal.remoteConfig.GetEconomyItemWithKeyWithoutRefresh(referenceWithoutLeadingHash);
        }

        /// <summary>
        /// Gets the referenced configuration item, which will automatically be deserialized to its target type.
        ///
        /// You can cast to this type by checking the <c>Type</c> parameter of the returned ConfigurationItemDefinition,
        /// and then casting to either InventoryItemDefinition or CurrencyDefinition as appropriate.
        /// </summary>
        /// <returns>Either a CurrencyDefinition or an InventoryItemDefinition, depending on the underlying item Type.</returns>
        [Preserve]
        public ConfigurationItemDefinition GetReferencedConfigurationItem()
        {
            return m_ReferencedItem;
        }
    }
}
