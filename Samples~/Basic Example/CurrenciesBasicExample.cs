using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;

public class CurrenciesBasicExample : MonoBehaviour
{
    async void FetchCurrenciesConfig()
    {
        List<CurrencyDefinition> currencies = await Economy.Configuration.GetCurrenciesAsync();

        foreach (var currency in currencies)
        {
            Debug.Log($"Currencies | ID: {currency.Id} - Name: {currency.Name}");
        }
    }

    async void FetchCurrencyFromConfig()
    {
        CurrencyDefinition currency = await Economy.Configuration.GetCurrencyAsync("currency_id");

        Debug.Log($"Currency | ID: {currency.Id} - name: {currency.Name}");
    }

    async void FetchPlayerBalances()
    {
        PlayerBalances.GetBalancesOptions options = new PlayerBalances.GetBalancesOptions
        {
            ItemsPerFetch = 5
        };
        GetBalancesResult balancesResult = await Economy.PlayerBalances.GetBalancesAsync(options);

        foreach (var balance in balancesResult.Balances)
        {
            Debug.Log($"Balances | ID: {balance.CurrencyId} - Balance: {balance.Balance}");
        }

        GetBalancesResult nextPage = await balancesResult.GetNextAsync();
    }

    async void AdjustPlayerBalance()
    {
        PlayerBalance setBalance = await Economy.PlayerBalances.SetBalanceAsync("currency_id", 10);
        Debug.Log($"Set | ID: {setBalance.CurrencyId} - Balance: {setBalance.Balance}");

        PlayerBalance incrementBalance = await Economy.PlayerBalances.IncrementBalanceAsync("currency_id", 5);
        Debug.Log($"Increment | ID: {incrementBalance.CurrencyId} - Balance: {incrementBalance.Balance}");

        PlayerBalance decrementBalance = await Economy.PlayerBalances.DecrementBalanceAsync("currency_id", 5);
        Debug.Log($"Decrement | ID: {decrementBalance.CurrencyId} - Balance: {decrementBalance.Balance}");
    }
}