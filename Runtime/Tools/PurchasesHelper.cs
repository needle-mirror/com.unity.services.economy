using UnityEngine;

namespace Unity.Services.Economy.Tools
{
    [CreateAssetMenu(fileName = "PurchasesHelper", menuName = "Economy Tools/Purchases Helper")]
    public class PurchasesHelper : ScriptableObject
    {
        [Header("Make Purchase")]
        public string purchaseId;

        /// <summary>
        /// Used to trigger the call to the Economy Service using the options set in the inspector.
        /// </summary>
        public async void InvokeAsync()
        {
            try
            {
                await EconomyService.Instance.Purchases.MakeVirtualPurchaseAsync(purchaseId);
            }
            catch (EconomyValidationException e)
            {
                Debug.LogError(e);
            }
            catch (EconomyRateLimitedException e)
            {
                Debug.LogError(e);
            }
            catch (EconomyException e)
            {
                Debug.LogError(e);
            }
        }
    }
}
