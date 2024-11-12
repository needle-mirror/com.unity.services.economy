using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy.Model;

namespace Unity.Services.Economy
{
    /// <summary>Obsolete, please use EconomyService.Instance </summary>
    [Obsolete("The interface provided by Economy has moved to EconomyService.Instance, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public static class Economy
    {
        /// <summary>Obsolete, please use EconomyService.Instance </summary>
        [Obsolete("The interface provided by Economy.Configuration has been replaced by EconomyService.Instance.Configuration, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public static Configuration Configuration;

        /// <summary>Obsolete, please use EconomyService.Instance </summary>
        [Obsolete("The interface provided by Economy.PlayerBalances has been replaced by EconomyService.Instance.PlayerBalances, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public static PlayerBalances PlayerBalances;

        /// <summary>Obsolete, please use EconomyService.Instance </summary>
        [Obsolete("The interface provided by Economy.PlayerInventory has been replaced by EconomyService.Instance.PlayerInventory, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public static PlayerInventory PlayerInventory;

        /// <summary>Obsolete, please use EconomyService.Instance </summary>
        [Obsolete("The interface provided by Economy.Purchases has been replaced by EconomyService.Instance.Purchases, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public static Purchases Purchases;
    }

    /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
    [Obsolete("The interface provided by Economy.Configuration has been replaced by EconomyService.Instance.Configuration, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public class Configuration
    {
        /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Configuration.GetCurrenciesAsync() has been replaced by EconomyService.Instance.Configuration.GetCurrenciesAsync(), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<List<CurrencyDefinition>> GetCurrenciesAsync() => await EconomyService.Instance.Configuration.GetCurrenciesAsync();

        /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
        /// <param name="id">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Configuration.GetCurrencyAsync(string id) has been replaced by EconomyService.Instance.Configuration.GetCurrencyAsync(string id), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<CurrencyDefinition> GetCurrencyAsync(string id) => await EconomyService.Instance.Configuration.GetCurrencyAsync(id);

        /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Configuration.GetInventoryItemsAsync() has been replaced by EconomyService.Instance.Configuration.GetInventoryItemsAsync(), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<List<InventoryItemDefinition>> GetInventoryItemsAsync() => await EconomyService.Instance.Configuration.GetInventoryItemsAsync();

        /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
        /// <param name="id">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Configuration.GetInventoryItemAsync(string id) has been replaced by EconomyService.Instance.Configuration.GetInventoryItemAsync(string id), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<InventoryItemDefinition> GetInventoryItemAsync(string id) => await EconomyService.Instance.Configuration.GetInventoryItemAsync(id);

        /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Configuration.GetVirtualPurchasesAsync() has been replaced by EconomyService.Instance.Configuration.GetVirtualPurchasesAsync(), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync() => await EconomyService.Instance.Configuration.GetVirtualPurchasesAsync();

        /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
        /// <param name="id">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Configuration.GetVirtualPurchaseAsync(string id) has been replaced by EconomyService.Instance.Configuration.GetVirtualPurchaseAsync(string id), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<VirtualPurchaseDefinition> GetVirtualPurchaseAsync(string id) => await EconomyService.Instance.Configuration.GetVirtualPurchaseAsync(id);

        /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Configuration.GetRealMoneyPurchasesAsync() has been replaced by EconomyService.Instance.Configuration.GetRealMoneyPurchasesAsync(), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<List<RealMoneyPurchaseDefinition>> GetRealMoneyPurchasesAsync() => await EconomyService.Instance.Configuration.GetRealMoneyPurchasesAsync();

        /// <summary>Obsolete, please use EconomyService.Instance.Configuration </summary>
        /// <param name="id">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Configuration.GetRealMoneyPurchaseAsync(string id) has been replaced by EconomyService.Instance.Configuration.GetRealMoneyPurchaseAsync(string id), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<RealMoneyPurchaseDefinition> GetRealMoneyPurchaseAsync(string id) => await EconomyService.Instance.Configuration.GetRealMoneyPurchaseAsync(id);
    }

    /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
    [Obsolete("The interface provided by Economy.PlayerBalances has been replaced by EconomyService.Instance.PlayerBalances, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public class PlayerBalances
    {
        /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.PlayerBalances.GetBalancesAsync(GetBalancesOptions options) has been replaced by EconomyService.Instance.PlayerBalances.GetBalancesAsync(GetBalancesOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<GetBalancesResult> GetBalancesAsync(GetBalancesOptions options = null) => await EconomyService.Instance.PlayerBalances.GetBalancesAsync(options);

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
        /// <param name="currencyId">Obsolete</param>
        /// <param name="amount">Obsolete</param>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.PlayerBalances.IncrementBalanceAsync(string currencyId, int amount, IncrementBalanceOptions options) has been replaced by EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(string currencyId, int amount, IncrementBalanceOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayerBalance> IncrementBalanceAsync(string currencyId, int amount, IncrementBalanceOptions options = null) => await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(currencyId, amount, options);

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
        /// <param name="currencyId">Obsolete</param>
        /// <param name="amount">Obsolete</param>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.PlayerBalances.DecrementBalanceAsync(string currencyId, int amount, DecrementBalanceOptions options) has been replaced by EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(string currencyId, int amount, DecrementBalanceOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayerBalance> DecrementBalanceAsync(string currencyId, int amount, DecrementBalanceOptions options = null) => await EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(currencyId, amount, options);

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
        /// <param name="currencyId">Obsolete</param>
        /// <param name="balance">Obsolete</param>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.PlayerBalances.SetBalanceAsync(string currencyId, long balance, SetBalanceOptions options) has been replaced by EconomyService.Instance.PlayerBalances.SetBalanceAsync(string currencyId, long balance, SetBalanceOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayerBalance> SetBalanceAsync(string currencyId, long balance, SetBalanceOptions options = null) => await EconomyService.Instance.PlayerBalances.SetBalanceAsync(currencyId, balance, options);

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
        [Obsolete("The model provided by Economy.PlayerBalances.GetBalancesOptions has been moved to EconomyService.Instance.GetBalancesOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class GetBalancesOptions : Unity.Services.Economy.GetBalancesOptions {}

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
        [Obsolete("The model provided by Economy.PlayerBalances.IncrementBalanceOptions has been moved to EconomyService.Instance.IncrementBalanceOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class IncrementBalanceOptions : Unity.Services.Economy.IncrementBalanceOptions {}

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
        [Obsolete("The model provided by Economy.PlayerBalances.DecrementBalanceOptions has been moved to EconomyService.Instance.DecrementBalanceOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class DecrementBalanceOptions : Unity.Services.Economy.DecrementBalanceOptions {}

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerBalances </summary>
        [Obsolete("The model provided by Economy.PlayerBalances.SetBalanceOptions has been moved to EconomyService.Instance.SetBalanceOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class SetBalanceOptions : Unity.Services.Economy.SetBalanceOptions {}
    }

    /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
    [Obsolete("The interface provided by Economy.PlayerInventory has been replaced by EconomyService.Instance.PlayerInventory, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public class PlayerInventory
    {
        /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.PlayerInventory.GetInventoryAsync(GetInventoryOptions options) has been replaced by EconomyService.Instance.PlayerInventory.GetInventoryAsync(GetInventoryOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<GetInventoryResult> GetInventoryAsync(GetInventoryOptions options = null) => await EconomyService.Instance.PlayerInventory.GetInventoryAsync(options);

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
        /// <param name="inventoryItemId">Obsolete</param>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.PlayerInventory.AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options) has been replaced by EconomyService.Instance.PlayerInventory.AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayersInventoryItem> AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options = null) => await EconomyService.Instance.PlayerInventory.AddInventoryItemAsync(inventoryItemId, options);

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
        /// <param name="playersInventoryItemId">Obsolete</param>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.PlayerInventory.DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options) has been replaced by EconomyService.Instance.PlayerInventory.DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options = null) => await EconomyService.Instance.PlayerInventory.DeletePlayersInventoryItemAsync(playersInventoryItemId, options);

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
        /// <param name="playersInventoryItemId">Obsolete</param>
        /// <param name="instanceData">Obsolete</param>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.PlayerInventory.UpdatePlayersInventoryItemAsync(string playersInventoryItemId, Dictionary<string, object> instanceData, UpdatePlayersInventoryItemOptions options) has been replaced by EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync(string playersInventoryItemId, Dictionary<string, object> instanceData, UpdatePlayersInventoryItemOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<PlayersInventoryItem> UpdatePlayersInventoryItemAsync(string playersInventoryItemId, Dictionary<string, object> instanceData, UpdatePlayersInventoryItemOptions options = null) => await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync(playersInventoryItemId, instanceData, options = null);

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
        [Obsolete("The model provided by Economy.PlayerInventory.GetInventoryOptions has been moved to EconomyService.Instance.GetInventoryOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class GetInventoryOptions : Unity.Services.Economy.GetInventoryOptions {}

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
        [Obsolete("The model provided by Economy.PlayerInventory.AddInventoryItemOptions has been moved to EconomyService.Instance.AddInventoryItemOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class AddInventoryItemOptions : Unity.Services.Economy.AddInventoryItemOptions {}

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
        [Obsolete("The model provided by Economy.PlayerInventory.DeletePlayersInventoryItemOptions has been moved to EconomyService.Instance.DeletePlayersInventoryItemOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class DeletePlayersInventoryItemOptions : Unity.Services.Economy.DeletePlayersInventoryItemOptions {}

        /// <summary>Obsolete, please use EconomyService.Instance.PlayerInventory </summary>
        [Obsolete("The model provided by Economy.PlayerInventory.UpdatePlayersInventoryItemOptions has been moved to EconomyService.Instance.UpdatePlayersInventoryItemOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class UpdatePlayersInventoryItemOptions : Unity.Services.Economy.UpdatePlayersInventoryItemOptions {}
    }

    /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
    [Obsolete("The interface provided by Economy.Purchases has been replaced by EconomyService.Instance.Purchases, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
    public class Purchases
    {
        /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
        /// <param name="virtualPurchaseId">Obsolete</param>
        /// <param name="options">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Purchases.MakeVirtualPurchaseAsync(string virtualPurchaseId, Services.Economy.MakeVirtualPurchaseOptions options) has been replaced by EconomyService.Instance.Purchases.MakeVirtualPurchaseAsync(string virtualPurchaseId, Services.Economy.MakeVirtualPurchaseOptions options), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<MakeVirtualPurchaseResult> MakeVirtualPurchaseAsync(string virtualPurchaseId, Services.Economy.MakeVirtualPurchaseOptions options = null) => await EconomyService.Instance.Purchases.MakeVirtualPurchaseAsync(virtualPurchaseId, options);

        /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
        /// <param name="args">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Purchases.RedeemAppleAppStorePurchaseAsync(RedeemAppleAppStorePurchaseArgs args) has been replaced by EconomyService.Instance.Purchases.RedeemAppleAppStorePurchaseAsync(RedeemAppleAppStorePurchaseArgs args), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<RedeemAppleAppStorePurchaseResult> RedeemAppleAppStorePurchaseAsync(RedeemAppleAppStorePurchaseArgs args) => await EconomyService.Instance.Purchases.RedeemAppleAppStorePurchaseAsync(args);

        /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
        /// <param name="args">Obsolete</param>
        /// <returns>Obsolete</returns>
        [Obsolete("The interface provided by Economy.Purchases.RedeemGooglePlayPurchaseAsync(RedeemGooglePlayStorePurchaseArgs args) has been replaced by EconomyService.Instance.Purchases.RedeemGooglePlayPurchaseAsync(RedeemGooglePlayStorePurchaseArgs args), and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public async Task<RedeemGooglePlayPurchaseResult> RedeemGooglePlayPurchaseAsync(RedeemGooglePlayStorePurchaseArgs args) => await EconomyService.Instance.Purchases.RedeemGooglePlayPurchaseAsync(args);

        /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
        [Obsolete("The model provided by Economy.Purchases.MakeVirtualPurchaseOptions has been moved to EconomyService.Instance.MakeVirtualPurchaseOptions, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class MakeVirtualPurchaseOptions : Unity.Services.Economy.MakeVirtualPurchaseOptions {}

        /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
        [Obsolete("The model provided by Economy.Purchases.RedeemAppleAppStorePurchaseArgs has been moved to EconomyService.Instance.RedeemAppleAppStorePurchaseArgs, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class RedeemAppleAppStorePurchaseArgs : Unity.Services.Economy.RedeemAppleAppStorePurchaseArgs
        {
            /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
            /// <param name="realMoneyPurchaseId">Obsolete</param>
            /// <param name="receipt">Obsolete</param>
            /// <param name="localCost">Obsolete</param>
            /// <param name="localCurrency">Obsolete</param>
            public RedeemAppleAppStorePurchaseArgs(string realMoneyPurchaseId, string receipt, int localCost, string localCurrency)
                : base(realMoneyPurchaseId, receipt, localCost, localCurrency) {}
        }

        /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
        [Obsolete("The model provided by Economy.Purchases.RedeemGooglePlayStorePurchaseArgs has been moved to EconomyService.Instance.RedeemGooglePlayStorePurchaseArgs, and should be accessed from there instead. This API will be removed in an upcoming release.", false)]
        public class RedeemGooglePlayStorePurchaseArgs : Unity.Services.Economy.RedeemGooglePlayStorePurchaseArgs
        {
            /// <summary>Obsolete, please use EconomyService.Instance.Purchases </summary>
            /// <param name="realMoneyPurchaseId">Obsolete</param>
            /// <param name="purchaseData">Obsolete</param>
            /// <param name="purchaseDataSignature">Obsolete</param>
            /// <param name="localCost">Obsolete</param>
            /// <param name="localCurrency">Obsolete</param>
            public RedeemGooglePlayStorePurchaseArgs(string realMoneyPurchaseId, string purchaseData, string purchaseDataSignature, int localCost, string localCurrency)
                : base(realMoneyPurchaseId, purchaseData, purchaseDataSignature, localCost, localCurrency) {}
        }
    }
}
