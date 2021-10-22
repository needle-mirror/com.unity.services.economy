//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using Unity.GameBackend.Economy.Apis.Currencies;
using Unity.GameBackend.Economy.Apis.Inventory;
using Unity.GameBackend.Economy.Apis.Purchases;


namespace Unity.GameBackend.Economy
{
    /// <summary>
    /// EconomyService
    /// </summary>
    internal static class EconomyService
    {
        /// <summary>
        /// The static instance of EconomyService.
        /// </summary>
        public static IEconomyService Instance { get; internal set; }
    }

    /// <summary> Interface for EconomyService</summary>
    internal interface IEconomyService
    {
        
        /// <summary> Accessor for CurrenciesApi methods.</summary>
        ICurrenciesApiClient CurrenciesApi { get; set; }
        
        /// <summary> Accessor for InventoryApi methods.</summary>
        IInventoryApiClient InventoryApi { get; set; }
        
        /// <summary> Accessor for PurchasesApi methods.</summary>
        IPurchasesApiClient PurchasesApi { get; set; }
        

        /// <summary> Configuration properties for the service.</summary>
        Configuration Configuration { get; set; }
    }
}
