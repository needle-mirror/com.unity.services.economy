# Purchases

The methods in the Purchases namespace allow you to make purchases for the currently signed in user.

> **All the methods in this namespace can throw an `EconomyException` as described [here](./index.md#EconomyException)**

### MakeVirtualPurchaseAsync

Makes a virtual purchase specified by ID. Optionally takes a list of instance IDs of items to use in that purchase, for example when trading one item for another. If the list of instance IDs is not provided, instances to use are chosen at random.

Example:

```cs 
string purchaseID = "BUY_A_SWORD";
MakeVirtualPurchaseResult purchaseResult = await Economy.Purchases.MakeVirtualPurchaseAsync(purchaseID);
```

Alternative Example using instance IDs:

```cs
string purchaseID = "BUY_A_SWORD_2";
List<string> instanceIDs= new List<string> { "myItem1", "myItem2" };
MakeVirtualPurchaseResult purchaseResult = await Economy.Purchases.MakeVirtualPurchaseAsync(purchaseID, instanceIDs);
```

This method returns a `MakeVirtualPurchaseResult`, which contains the following data:

- `Costs`: A `Costs` object representing the costs that were spent in this purchase. This in turn has two fields:
    - `Currency`: A list of `CurrencyExchangeItem` describing the currencies used to make this purchase.
    - `Inventory`: A list of `InventoryExchangeItem` describing the items used as a cost in order to make this purchase.
- `Rewards`: A `Rewards` object representing the rewards given in exchange for this purchase. This also has two fields as above:
    - `Currency`: A list of `CurrencyExchangeItem` describing the currencies rewarded as part of this purchase.
    - `Inventory`: A list of `InventoryExchangeItem` describing the items rewarded as part of this purchase.

##### CurrencyExchangeItem

This object represents a currency that was part of a purchase. It has two fields

- `Id`: The ID of the currency
- `Amount`: The amount of this currency used in the purchase.

##### InventoryExchangeItem

This object represents a inventory item that was part of a purchase. It has three fields

- `Id`: The ID of the currency
- `Amount`: The amount of this inventory item used/rewarded in the purchase.
- `InstanceIds`: A list of instance IDs that were used/rewarded in the purchase.

### RedeemAppleAppStorePurchaseAsync
Redeems a real money purchase by submitting a receipt from the Apple App Store. This is validated and if valid the rewards as defined in the configuration are applied to the player’s inventory and currency balances.

The `localCost` passed into this method needs to be an integer in the minor currency format, e.g. $1.99 USD would be `199`, and the `localCurrency` must be the ISO-4217 code of the currency.

Example:

```cs 
string purchaseID = "BUY_GEMS";
string receipt = "<receipt_string_from_Apple>";
PlayerPurchaseAppleAppStoreResult purchaseResult = await Economy.Purchases.RedeemAppleAppStorePurchaseAsync(purchaseId, receipt, 199, "USD");
```

This method returns a `PlayerPurchaseAppleAppStoreResult`, which contains the following data:

- `Verification`: The receipt verification details from the validation service.
    - `Status`: Status of the receipt verification. This will be one of:
        - `VALID`: The purchase was valid. 
        - `VALID_NOT_REDEEMED`: The purchase was valid but seen before, but had not yet been redeemed. 
        - `INVALID_ALREADY_REDEEMED`: The purchase has already been redeemed. 
        - `INVALID_VERIFICATION_FAILED`: The receipt verification service returned that the receipt data was not valid. 
        - `INVALID_ANOTHER_PLAYER`: The receipt has previously been used by a different player and validated. 
        - `INVALID_CONFIGURATION`: The service configuration is invalid, further information in the details section of the response. 
        - `INVALID_PRODUCT_ID_MISMATCH`: The purchase configuration store product identifier does not match the one in the receipt.
    - `Store`: Details from the receipt validation service. This has three fields:
        - `Code`: The status code sent back from the Apple App Store verification service.
        - `Message`: A textual description of the returned status code.
        - `Receipt`: The full response from the verification service as a JSON encoded string.
- `Rewards`: A `Rewards` object representing the rewards given in exchange for this purchase. This has two fields:
    - `Currency`: A list of `CurrencyExchangeItem` describing the currencies rewarded as part of this purchase.
    - `Inventory`: A list of `InventoryExchangeItem` describing the items rewarded as part of this purchase.
    
#### EconomyAppleAppStorePurchaseFailedException

`RedeemAppleAppStorePurchaseAsync` may throw an exception of type `EconomyAppleAppStorePurchaseFailedException`. This inherits from `EconomyException` and contains one additional field called `Data`.

The `Data` field has the same structure as the `PlayerPurchaseAppleAppStoreResult` class, and contains further exception details. Specifically, you can look at the `Message` field in the `Store` object.

Please note, this `Data` field is different from the `Data` field in the base `Exception` class.