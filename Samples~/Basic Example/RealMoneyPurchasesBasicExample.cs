using System.Collections.Generic;
using Unity.Services.Economy.Model;
using UnityEngine;

namespace Unity.Services.Economy
{
    public class RealMoneyPurchasesExample: MonoBehaviour
    {
        async void FetchPurchasesConfig()
        {
            List<RealMoneyPurchaseDefinition> definitions = await Economy.Configuration.GetRealMoneyPurchasesAsync();
            
            foreach (var definition in definitions)
            {
                Debug.Log($"Purchases | ID: {definition.Id} - Name: {definition.Name}");
            }
        }

        async void RedeemAppleAppStorePurchase()
        {
            try
            {
                Purchases.RedeemAppleAppStorePurchaseArgs args = new Purchases.RedeemAppleAppStorePurchaseArgs("PURCHASE_ID", "RECEIPT_FROM_APP_STORE", 0, "USD");
                RedeemAppleAppStorePurchaseResult purchaseResult = await Economy.Purchases.RedeemAppleAppStorePurchaseAsync(args);

                Debug.Log($"Purchase was successful");

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
