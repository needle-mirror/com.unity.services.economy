using System.Collections.Generic;
using Unity.Services.Economy.Model;
using UnityEngine;

namespace Unity.Services.Economy
{
    public class PurchasesBasicExample: MonoBehaviour
    {
        async void FetchPurchasesConfig()
        {
            List<VirtualPurchaseDefinition> definitions = await Economy.Configuration.GetVirtualPurchasesAsync();
            
            foreach (var definition in definitions)
            {
                Debug.Log($"Purchases | ID: {definition.Id} - Name: {definition.Name}");
                Debug.Log($"-- Costs:");
                foreach (var cost in definition.Costs)
                {
                    // Can use CurrencyDefinition, InventoryItemDefinition etc instead of ConfigurationItemDefinition to get specific fields
                    Debug.Log($"--- Cost {cost.Amount} {cost.Item.GetReferencedConfigurationItem().Name}");
                }
                foreach (var reward in definition.Rewards)
                {
                    // Can use CurrencyDefinition, InventoryItemDefinition etc instead of ConfigurationItemDefinition to get specific fields
                    Debug.Log($"--- Cost {reward.Amount} {reward.Item.GetReferencedConfigurationItem().Name}");
                }
            }
        }

        async void MakePurchase()
        {
            try
            {
                MakeVirtualPurchaseResult purchaseResult = await Economy.Purchases.MakeVirtualPurchaseAsync("A_PURCHASE_ID");

                Debug.Log($"Purchase was successful");
                foreach (var cost in purchaseResult.Costs.Currency)
                {
                    Debug.Log($"- purchase cost {cost.Amount} {cost.Id}");
                }

                foreach (var cost in purchaseResult.Costs.Inventory)
                {
                    Debug.Log($"- purchase cost {cost.Amount} {cost.Id}");
                }

                foreach (var reward in purchaseResult.Rewards.Currency)
                {
                    Debug.Log($"- purchase rewarded {reward.Amount} {reward.Id}");
                }

                foreach (var reward in purchaseResult.Rewards.Inventory)
                {
                    Debug.Log($"- purchase rewarded {reward.Amount} {reward.Id}");
                }
            }
            catch (EconomyException e)
            {
                Debug.LogError($"Purchase failed because {e.Message} with code {e.Reason}");
            }
        }

        async void MakePurchaseWithInstanceIDs()
        {
            List<string> playersInventoryItemIds = new List<string> { "myInstance" };
            try
            {
                Purchases.MakeVirtualPurchaseOptions options = new Purchases.MakeVirtualPurchaseOptions
                {
                    PlayersInventoryItemIds = playersInventoryItemIds
                };
                MakeVirtualPurchaseResult purchaseResult = await Economy.Purchases.MakeVirtualPurchaseAsync("A_PURCHASE_ID", options);

                Debug.Log($"Purchase was successful");
                foreach (var cost in purchaseResult.Costs.Currency)
                {
                    Debug.Log($"- purchase cost {cost.Amount} {cost.Id}");
                }

                foreach (var cost in purchaseResult.Costs.Inventory)
                {
                    Debug.Log($"- purchase cost {cost.Amount} {cost.Id}");
                }

                foreach (var reward in purchaseResult.Rewards.Currency)
                {
                    Debug.Log($"- purchase rewarded {reward.Amount} {reward.Id}");
                }

                foreach (var reward in purchaseResult.Rewards.Inventory)
                {
                    Debug.Log($"- purchase rewarded {reward.Amount} {reward.Id}");
                }
            }
            catch (EconomyException e)
            {
                Debug.LogError($"Purchase failed because {e.Message} with code {e.Reason}");
            }
        }
    }
}
