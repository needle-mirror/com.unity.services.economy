using System;
using Unity.Services.Economy;
using UnityEngine;

namespace Unity.Services.Economy.Tools
{
    [CreateAssetMenu(fileName = "PlayerBalancesHelper", menuName = "Economy Tools/Player Balances Helper")]
    public class PlayerBalancesHelper : ScriptableObject
    {
        public enum CurrencyAction
        {
            Set,
            Increment,
            Decrement
        }

        [Header("Currencies Helper")]
        public CurrencyAction action;
        public string currencyId;
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
