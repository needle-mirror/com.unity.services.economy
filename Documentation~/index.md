# Economy SDK Guide

This package helps you integrate the Economy service into your game. 

Feature specific documentation:
- [Configuration](./configuration.md)
- [Player Balances](./player_balances.md)
- [Player Inventory](./player_inventory.md)
- [Purchases](./purchases.md)

## Getting Started

To get started with the Economy SDK:

* Install the version of the package you wish using Package Manager
* Sign in to your cloud project using the Services window in Unity
* Initialize the Core SDK using `await UnityServices.InitializeAsync()`
* Sign into the authentication SDK, as mentioned below

**Note**: The Economy SDK requires that an authentication flow from the Authentication SDK has been completed prior to using any of the Economy APIs, as a valid player ID and access token are required to access the Economy services. This can be achieved with the following code snippet for anonymous authentication, or see the documentation for the Authentication SDK for more details and other sign in methods:

```cs
Authentication.SignInAnonymously();
```

## Using the Economy Dashboard

The functionality of the SDK is only available once you have published your first Economy configuration from the Economy Dashboard in uDash.

To get started:
* Sign into the Unity dashboard
* Navigate to the Economy section using the side menu
* Add some items to the configuration
* When you are ready for the configuration to be used in the SDK, click "Publish".

## Upgrading from Previous Versions

### 0.2.2
When using this version for the first time, be sure to republish your Economy configuration from the Dashboard, as the structure
of the Economy config changed at this release.

## Using the SDK

### Initial Setup

The Economy SDK is ready to use immediately once sign in with the Authentication SDK is complete. You may then call any of the below methods to start interacting with the Economy data.

### Quick Reference

Below is a quick reference for the methods available in the SDK. For a more detailed look, see the documentation for the individual namespaces.

#### Configuration

The methods in the `Configuration` namespace allow you to retrieve items from the global economy configuration.

For more details on these methods, see [the configuration documentation here.](./configuration.md)

The methods available are:
```cs
public Task<List<CurrencyDefinition>> GetCurrenciesAsync();
public Task<CurrencyDefinition> GetCurrencyAsync(string currencyID);
public Task<List<InventoryItemDefinition>> GetInventoryItemsAsync();
public Task<InventoryItemDefinition> GetInventoryItemAsync(string itemID);
public Task<List<VirtualPurchaseDefinition>> GetVirtualPurchasesAsync();
public Task<VirtualPurchaseDefinition> GetVirtualPurchaseAsync(string purchaseID);
```

#### Player Balances 

The methods in the PlayerBalances namespace allow you to retrieve and update the user's currency balances. These methods will return the balances for the currently signed in player from the Authentication SDK.

For more details on these methods, see [the player balance documentation here.](./player_balances.md)

The methods available are:
```cs
public Task<GetBalanceResponse> GetBalancesAsync(int itemsPerFetch = 20);
public Task<PlayerBalanceDefinition> SetBalanceAsync(string currencyID, int amount, string writeLock = null);
public Task<PlayerBalanceDefinition> IncrementBalanceAsync(string currencyID, int amount, string writeLock = null);
public Task<PlayerBalanceDefinition> DecrementBalanceAsync(string currencyID, int amount, string writeLock = null);
```

#### Player Inventory

The methods in the PlayerInventory namespace allow you to retrieve and update the player's inventory instances. These methods will return inventory data for the currently signed in player from the Authentication SDK.

For more details on these methods, see [the player inventory documentation here.](./player_inventory.md)

The methods available are:
```cs
public Task<GetInventoryResult> GetInventoryAsync(List<string> instanceIds = null, List<string> itemIds = null, int itemsPerFetch = 20);
public Task<PlayerInventoryInstance> AddInventoryInstanceAsync(string itemId, string instanceId = null, Dictionary<string, string> instanceData = null);
public Task<PlayerInventoryInstance> UpdateInventoryInstanceAsync(string instanceId, object instanceData, string writeLock = null);
public Task DeleteInventoryInstanceAsync(string instanceId, string writeLock = null);
```

#### Purchases

The methods in the Purchases namespace allow you to make virtual purchases for the player. These methods will make purchases for the player currently signed in with the Authentication SDK.

For more details on these methods, see [the purchases documentation here.](./purchases.md)

The methods available are:
```cs
public Task<MakeVirtualPurchaseResult> MakeVirtualPurchase(string virtualPurchaseId, List<string> instanceIds);
```

### Common Model Objects

#### EconomyDate

An economy date object is a wrapper for modified and created dates from the economy service. It currently has one parameter as shown below.

- `Date`: A `DateTime` representation of the wrapped date

#### EconomyException

An `EconomyException` will be thrown when there is a problem with one of the operations in the SDK. These exceptions should 
be handled by calling code. The methods that can throw these exceptions are clearly marked in documentation.

The `EconomyException` has the following field in addition to those normally provided by C# `Exception`:

- `Reason`: An `EconomyExceptionReason` is an enum value that describes what category of issue occurred. This is provided
to allow a code-friendly way of detecting and handling the different types of errors that can be thrown. The possible values
are:
    - `InvalidArgument`: One of the parameters passed was invalid or out of range. The `Exception` message will contain details
    of what to check.
    - `Unauthorized`: No auth token is provided, or the provided auth token is invalid. In most cases in the Economy SDK, this means
    that the Authentication SDK sign in process has not yet completed, or has expired. Ensure that SDK is signed in correctly before
    calling any Economy SDK methods.
    - `EntityNotFound`: The currency/item/etc. specified in the method call is not present in the Economy service. Check that the provided
    value is correct and try again.
    - `Conflict`: The write lock provided to the method do not align with the one held by the Economy service. Check another source isn't updating
    the same resource, you aren't trying to update the item twice, or that you've correctly read the write lock before trying to write it. If you 
    don't need optimistic concurrency protection, you can omit the write lock in order to force the transaction.
    - `RateLimited`: Too many requests have been made to the Economy service from this device. This usually indicates a logic problem in the calling
    code, so check the logic around the offending method call (for example, check you aren't calling the economy methods in an unlimited loop, etc).
    - `Unknown`: An error was returned that wasn't expected by the SDK. This is often a bug, and should be raised with the SDK team to debug.

### Common mechanics

#### Using the writeLock

The write lock is used to implement optimistic concurrency. It is optional.

For a code example, take a look at the method `UpdateInventoryItemUsingWriteLock` in our `InventoriesBasicExample` sample.

A `writeLock` is returned for each balance or inventory item instance when they are fetched, added or updated. 
The user can then pass the `writeLock` value back to the SDK when updating a currency balance or inventory instance. 
If it matches, the request is successful and will update. If it doesn't, then a error gets returned.

The `writeLock` can be any `string` value.
   
