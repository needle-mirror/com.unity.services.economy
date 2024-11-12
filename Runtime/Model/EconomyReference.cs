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

        /// <summary>Creates an instance of EconomyReference</summary>
        /// <param name="configItem">Config item</param>
        [Preserve]
        [JsonConstructor]
        public EconomyReference(ConfigurationItemDefinition configItem)
        {
            m_ReferencedItem = configItem;
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
