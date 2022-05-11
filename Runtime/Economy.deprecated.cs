using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy.Model;

namespace Unity.Services.Economy
{
    [Obsolete("The interface provided by Economy has moved to EconomyService.Instance, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public static class Economy
    {
        [Obsolete("The interface provided by Economy.Configuration has been replaced by EconomyService.Instance.Configuration, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public static Configuration Configuration;

        [Obsolete("The interface provided by Economy.PlayerBalances has been replaced by EconomyService.Instance.PlayerBalances, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public static PlayerBalances PlayerBalances;

        [Obsolete("The interface provided by Economy.PlayerInventory has been replaced by EconomyService.Instance.PlayerInventory, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public static PlayerInventory PlayerInventory;

        [Obsolete("The interface provided by Economy.Purchases has been replaced by EconomyService.Instance.Purchases, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public static Purchases Purchase;
    }

    [Obsolete("The interface provided by Economy.Configuration has been replaced by EconomyService.Instance.Configuration, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public class Configuration
    {
        [Obsolete("The interface provided by Economy.Configuration.GetCurrenciesAsync() has been replaced by EconomyService.Instance.Configuration.GetCurrenciesAsync(), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<List<CurrencyDefinition>> GetCurrenciesAsync() => await EconomyService.Instance.Configuration.GetCurrenciesAsync();

        [Obsolete("The interface provided by Economy.Configuration.GetCurrencyAsync(string id) has been replaced by EconomyService.Instance.Configuration.GetCurrencyAsync(string id), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<CurrencyDefinition> GetCurrencyAsync(string id) => await EconomyService.Instance.Configuration.GetCurrencyAsync(id);

        [Obsolete("The interface provided by Economy.Configuration.GetInventoryItemsAsync() has been replaced by EconomyService.Instance.Configuration.GetInventoryItemsAsync(), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<List<InventoryItemDefinition>> GetInventoryItemsAsync() => await EconomyService.Instance.Configuration.GetInventoryItemsAsync();

        [Obsolete("The interface provided by Economy.Configuration.GetInventoryItemAsync(string id) has been replaced by EconomyService.Instance.Configuration.GetInventoryItemAsync(string id), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<InventoryItemDefinition> GetInventoryItemAsync(string id) => await EconomyService.Instance.Configuration.GetInventoryItemAsync(id);

        [Obsolete("The interface provided by Economy.Configuration.GetVirtualPurchasesAsync() has been replaced by EconomyService.Instance.Configuration.GetVirtualPurchasesAsync(), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync() => await EconomyService.Instance.Configuration.GetVirtualPurchasesAsync();

        [Obsolete("The interface provided by Economy.Configuration.GetVirtualPurchaseAsync(string id) has been replaced by EconomyService.Instance.Configuration.GetVirtualPurchaseAsync(string id), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<VirtualPurchaseDefinition> GetVirtualPurchaseAsync(string id) => await EconomyService.Instance.Configuration.GetVirtualPurchaseAsync(id);

        [Obsolete("The interface provided by Economy.Configuration.GetRealMoneyPurchasesAsync() has been replaced by EconomyService.Instance.Configuration.GetRealMoneyPurchasesAsync(), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<List<RealMoneyPurchaseDefinition>> GetRealMoneyPurchasesAsync() => await EconomyService.Instance.Configuration.GetRealMoneyPurchasesAsync();

        [Obsolete("The interface provided by Economy.Configuration.GetRealMoneyPurchaseAsync(string id) has been replaced by EconomyService.Instance.Configuration.GetRealMoneyPurchaseAsync(string id), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<RealMoneyPurchaseDefinition> GetRealMoneyPurchaseAsync(string id) => await EconomyService.Instance.Configuration.GetRealMoneyPurchaseAsync(id);
    }

    [Obsolete("The interface provided by Economy.PlayerBalances has been replaced by EconomyService.Instance.PlayerBalances, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public class PlayerBalances
    {
        [Obsolete("The interface provided by Economy.PlayerBalances.GetBalancesAsync(GetBalancesOptions options) has been replaced by EconomyService.Instance.PlayerBalances.GetBalancesAsync(GetBalancesOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<GetBalancesResult> GetBalancesAsync(GetBalancesOptions options = null) => await EconomyService.Instance.PlayerBalances.GetBalancesAsync(options);

        [Obsolete("The interface provided by Economy.PlayerBalances.IncrementBalanceAsync(string currencyId, int amount, IncrementBalanceOptions options) has been replaced by EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(string currencyId, int amount, IncrementBalanceOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayerBalance> IncrementBalanceAsync(string currencyId, int amount, IncrementBalanceOptions options = null) => await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(currencyId, amount, options);

        [Obsolete("The interface provided by Economy.PlayerBalances.DecrementBalanceAsync(string currencyId, int amount, DecrementBalanceOptions options) has been replaced by EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(string currencyId, int amount, DecrementBalanceOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayerBalance> DecrementBalanceAsync(string currencyId, int amount, DecrementBalanceOptions options = null) => await EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(currencyId, amount, options);

        [Obsolete("The interface provided by Economy.PlayerBalances.SetBalanceAsync(string currencyId, long balance, SetBalanceOptions options) has been replaced by EconomyService.Instance.PlayerBalances.SetBalanceAsync(string currencyId, long balance, SetBalanceOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayerBalance> SetBalanceAsync(string currencyId, long balance, SetBalanceOptions options = null) => await EconomyService.Instance.PlayerBalances.SetBalanceAsync(currencyId, balance, options);

        [Obsolete("The model provided by Economy.PlayerBalances.GetBalancesOptions has been moved to EconomyService.Instance.GetBalancesOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class GetBalancesOptions : Unity.Services.Economy.GetBalancesOptions {}

        [Obsolete("The model provided by Economy.PlayerBalances.IncrementBalanceOptions has been moved to EconomyService.Instance.IncrementBalanceOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class IncrementBalanceOptions : Unity.Services.Economy.IncrementBalanceOptions {}

        [Obsolete("The model provided by Economy.PlayerBalances.DecrementBalanceOptions has been moved to EconomyService.Instance.DecrementBalanceOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class DecrementBalanceOptions : Unity.Services.Economy.DecrementBalanceOptions {}

        [Obsolete("The model provided by Economy.PlayerBalances.SetBalanceOptions has been moved to EconomyService.Instance.SetBalanceOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class SetBalanceOptions : Unity.Services.Economy.SetBalanceOptions {}
    }

    [Obsolete("The interface provided by Economy.PlayerInventory has been replaced by EconomyService.Instance.PlayerInventory, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public class PlayerInventory
    {
        [Obsolete("The interface provided by Economy.PlayerInventory.GetInventoryAsync(GetInventoryOptions options) has been replaced by EconomyService.Instance.PlayerInventory.GetInventoryAsync(GetInventoryOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<GetInventoryResult> GetInventoryAsync(GetInventoryOptions options = null) => await EconomyService.Instance.PlayerInventory.GetInventoryAsync(options);

        [Obsolete("The interface provided by Economy.PlayerInventory.AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options) has been replaced by EconomyService.Instance.PlayerInventory.AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayersInventoryItem> AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options = null) => await EconomyService.Instance.PlayerInventory.AddInventoryItemAsync(inventoryItemId, options);

        [Obsolete("The interface provided by Economy.PlayerInventory.DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options) has been replaced by EconomyService.Instance.PlayerInventory.DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options = null) => await EconomyService.Instance.PlayerInventory.DeletePlayersInventoryItemAsync(playersInventoryItemId, options);

        [Obsolete("The interface provided by Economy.PlayerInventory.UpdatePlayersInventoryItemAsync(string playersInventoryItemId, Dictionary<string, object> instanceData, UpdatePlayersInventoryItemOptions options) has been replaced by EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync(string playersInventoryItemId, Dictionary<string, object> instanceData, UpdatePlayersInventoryItemOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayersInventoryItem> UpdatePlayersInventoryItemAsync(string playersInventoryItemId, Dictionary<string, object> instanceData, UpdatePlayersInventoryItemOptions options = null) => await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync(playersInventoryItemId, instanceData, options = null);

        [Obsolete("The model provided by Economy.PlayerInventory.GetInventoryOptions has been moved to EconomyService.Instance.GetInventoryOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class GetInventoryOptions : Unity.Services.Economy.GetInventoryOptions {}

        [Obsolete("The model provided by Economy.PlayerInventory.AddInventoryItemOptions has been moved to EconomyService.Instance.AddInventoryItemOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class AddInventoryItemOptions : Unity.Services.Economy.AddInventoryItemOptions {}

        [Obsolete("The model provided by Economy.PlayerInventory.DeletePlayersInventoryItemOptions has been moved to EconomyService.Instance.DeletePlayersInventoryItemOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class DeletePlayersInventoryItemOptions : Unity.Services.Economy.DeletePlayersInventoryItemOptions {}

        [Obsolete("The model provided by Economy.PlayerInventory.UpdatePlayersInventoryItemOptions has been moved to EconomyService.Instance.UpdatePlayersInventoryItemOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class UpdatePlayersInventoryItemOptions : Unity.Services.Economy.UpdatePlayersInventoryItemOptions {}
    }

    [Obsolete("The interface provided by Economy.Purchases has been replaced by EconomyService.Instance.Purchases, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public class Purchases
    {
        [Obsolete("The interface provided by Economy.Purchases.MakeVirtualPurchaseAsync(string virtualPurchaseId, Services.Economy.MakeVirtualPurchaseOptions options) has been replaced by EconomyService.Instance.Purchases.MakeVirtualPurchaseAsync(string virtualPurchaseId, Services.Economy.MakeVirtualPurchaseOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<MakeVirtualPurchaseResult> MakeVirtualPurchaseAsync(string virtualPurchaseId, Services.Economy.MakeVirtualPurchaseOptions options = null) => await EconomyService.Instance.Purchases.MakeVirtualPurchaseAsync(virtualPurchaseId, options);

        [Obsolete("The interface provided by Economy.Purchases.RedeemAppleAppStorePurchaseAsync(RedeemAppleAppStorePurchaseArgs args) has been replaced by EconomyService.Instance.Purchases.RedeemAppleAppStorePurchaseAsync(RedeemAppleAppStorePurchaseArgs args), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<RedeemAppleAppStorePurchaseResult> RedeemAppleAppStorePurchaseAsync(RedeemAppleAppStorePurchaseArgs args) => await EconomyService.Instance.Purchases.RedeemAppleAppStorePurchaseAsync(args);

        [Obsolete("The interface provided by Economy.Purchases.RedeemGooglePlayPurchaseAsync(RedeemGooglePlayStorePurchaseArgs args) has been replaced by EconomyService.Instance.Purchases.RedeemGooglePlayPurchaseAsync(RedeemGooglePlayStorePurchaseArgs args), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<RedeemGooglePlayPurchaseResult> RedeemGooglePlayPurchaseAsync(RedeemGooglePlayStorePurchaseArgs args) => await EconomyService.Instance.Purchases.RedeemGooglePlayPurchaseAsync(args);

        [Obsolete("The model provided by Economy.Purchases.MakeVirtualPurchaseOptions has been moved to EconomyService.Instance.MakeVirtualPurchaseOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class MakeVirtualPurchaseOptions : Unity.Services.Economy.MakeVirtualPurchaseOptions {}

        [Obsolete("The model provided by Economy.Purchases.RedeemAppleAppStorePurchaseArgs has been moved to EconomyService.Instance.RedeemAppleAppStorePurchaseArgs, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class RedeemAppleAppStorePurchaseArgs : Unity.Services.Economy.RedeemAppleAppStorePurchaseArgs
        {
            public RedeemAppleAppStorePurchaseArgs(string realMoneyPurchaseId, string receipt, int localCost, string localCurrency)
                : base(realMoneyPurchaseId, receipt, localCost, localCurrency) {}
        }

        [Obsolete("The model provided by Economy.Purchases.RedeemGooglePlayStorePurchaseArgs has been moved to EconomyService.Instance.RedeemGooglePlayStorePurchaseArgs, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class RedeemGooglePlayStorePurchaseArgs : Unity.Services.Economy.RedeemGooglePlayStorePurchaseArgs
        {
            public RedeemGooglePlayStorePurchaseArgs(string realMoneyPurchaseId, string purchaseData, string purchaseDataSignature, int localCost, string localCurrency)
                : base(realMoneyPurchaseId, purchaseData, purchaseDataSignature, localCost, localCurrency) {}
        }
    }
}
