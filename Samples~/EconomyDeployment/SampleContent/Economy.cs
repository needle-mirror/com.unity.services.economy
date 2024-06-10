// NOTE: You need to deploy the MYCURRENCY.ecc currency file before being able to run this sample,
// to do so, open the deployment window and deploy the MYCURRENCY.ecc file.
// See README.md for more details.

using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine.EventSystems;
#if INPUT_SYSTEM_PRESENT
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.UI;
#endif

namespace Sample.Deployment.EconomySample
{
    public class Economy : MonoBehaviour
    {
        public string CurrencyId = "MYCURRENCY";
        CurrencyDefinition m_Currency;
        [SerializeField]
        StandaloneInputModule m_DefaultInputModule;

        async void Awake()
        {
#if INPUT_SYSTEM_PRESENT
            m_DefaultInputModule.enabled = false;
            m_DefaultInputModule.gameObject.AddComponent<InputSystemUIInputModule>();
            TouchSimulation.Enable();
#endif
            
            // Economy needs to be initialized and then the user must sign in.
            await InitializeServices();
            await SignInAnonymously();

            // Cache the Economy configuration
            await EconomyService.Instance.Configuration.SyncConfigurationAsync();
            UpdateCurrencyData();
        }

        static async Task InitializeServices()
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync();
            }
        }

        static async Task SignInAnonymously()
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        public async void UpdateCurrencyData()
        {
            try
            {
                await EconomyService.Instance.Configuration.SyncConfigurationAsync();
                m_Currency = EconomyService.Instance.Configuration.GetCurrency(CurrencyId);
                Debug.Log($"Currency name: {m_Currency.Name}");
                Debug.Log($"Currency id: {m_Currency.Id}");
                Debug.Log($"Currency initial balance: {m_Currency.Initial}");
                Debug.Log($"Currency max balance: {m_Currency.Max}");
                Debug.Log($"Currency last modified: {m_Currency.Modified.Date}");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async void IncreaseBalance()
        {
            try
            {
                PlayerBalance newBalance = await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(CurrencyId, 1);
                Debug.Log($"New balance for {newBalance.CurrencyId} is {newBalance.Balance}.");
            }
            catch (EconomyRateLimitedException e)
            {
                Debug.LogError($"Rate limit reached - Retry after {e.RetryAfter} seconds.");
            }
            catch (EconomyException e)
            {
                if (e.Reason == EconomyExceptionReason.UnprocessableTransaction)
                {
                    Debug.LogError($"Error while increasing balance: balance maximum reached, please click the reset balance button.");
                    return;
                }

                Debug.LogException(e);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async void ResetBalance()
        {
            try
            {
                PlayerBalance newBalance = await EconomyService.Instance.PlayerBalances.SetBalanceAsync(CurrencyId, m_Currency.Initial);
                Debug.Log($"Balance for {newBalance.CurrencyId} was reset.");
            }
            catch (EconomyRateLimitedException e)
            {
                Debug.LogError($"Rate limit reached - Retry after {e.RetryAfter} seconds.");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
