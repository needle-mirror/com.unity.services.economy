using System;
using Unity.Services.Economy;
using UnityEngine;

namespace Unity.Services.Economy.Tools
{
    /// <summary>Helper to work with player balances </summary>
    [CreateAssetMenu(fileName = "PlayerBalancesHelper", menuName = "Economy Tools/Player Balances Helper")]
    public class PlayerBalancesHelper : ScriptableObject
    {
        /// <summary>CurrencyAction enum</summary>
        public enum CurrencyAction
        {
            /// <summary> Set the currency </summary>
            Set,
            /// <summary> Increment the currency </summary>
            Increment,
            /// <summary> Decrement the currency </summary>
            Decrement
        }

        /// <summary> Action to apply to the currency </summary>
        [Header("Currencies Helper")]
        public CurrencyAction action;
        /// <summary>ID of the currency to modify </summary>
        public string currencyId;
        /// <summary>Amount to apply to the currency </summary>
        public int amount;

        /// <summary>
        /// Used to trigger the call to the Economy Service using the options set in the inspector.
        /// </summary>
        public async void InvokeAsync()
        {
            if (string.IsNullOrEmpty(currencyId))
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Unity.Services.Core.CommonErrorCodes.Unknown, "The currency ID on the player balances helper scriptable object is empty. Please enter an ID.");
            }

            switch (action)
            {
                case CurrencyAction.Set:
                    try
                    {
                        await EconomyService.Instance.PlayerBalances.SetBalanceAsync(currencyId, amount);
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
                    break;
                case CurrencyAction.Increment:
                    try
                    {
                        await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(currencyId, amount);
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
                    break;
                case CurrencyAction.Decrement:
                    try
                    {
                        await EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(currencyId, amount);
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
                    break;
            }
        }
    }
}
