# PlayerBalances

The player balances namespace contains all the methods for fetching and updating a player's currency balances.

These methods will return the balances for the currently signed in player from the Authentication SDK.

> **All the methods in this namespace can throw an `EconomyException` as described [here](./index.md#EconomyException)**

#### GetBalances

Retrieve the currency balances for the current user.

When getting balances, you can set an optional limit on the number of balances to fetch (between 1 and 100 inclusive). This is to help with pagination. The default number is 20.

Note: This will return all balances for the user, including those for currencies that have since been deleted.

```cs
// Will retrieve the default maximum of 20 balances
GetBalancesResult playerBalancesResponse = await Economy.PlayerBalances.GetBalancesAsync();

List<PlayerBalanceDefinition> listOfBalances = playerBalancesResponse.Balances;

if (playerBalancesResponse.HasNext) {
    playerBalancesResponse.GetNext();
}
List<PlayerBalanceDefinition> updatedBalanceList = playerBalancesResponse.Balances;

// ... etc
```

Retrieve the first 5 balances for the current user, and then retrieve the next 5.
```cs
GetBalancesResult playerBalancesResponse = await Economy.PlayerBalances.GetBalancesAsync(5);

List<PlayerBalanceDefinition> listOfBalances = playerBalancesResponse.Balances;

if (playerBalancesResponse.HasNext) {
    GetBalancesResult nextPlayerBalancesResponse = await playerBalancesResponse.GetNext(5);
}

// ... etc
```

These methods return a `GetBalancesResult`. This object handles the pagination for you (see below for details)

#### SetBalance

Sets the balance of the specified currency to the specified value.

This method will throw an exception if the specified value is below the minimum or above the maximum allowed for the currency. It will also throw an exception if the specified currency ID does not exist.

This method optionally takes a writeLock string. If provided, then an exception will be thrown unless the writeLock matches the writeLock received by a previous read, in order to provide optimistic concurrency. If not provided, the transaction will proceed regardless of any existing writeLock in the data.

This method returns the current balance after the update has been applied, if the operation is successful.

```cs
string currencyID = "GOLD_BARS";
int newAmount = 1000;
string writeLock = "someLockValueFromPreviousRequest";

PlayerBalanceDefinition newBalance = await Economy.PlayerBalances.SetBalanceAsync(currencyID, newAmount);
// OR
PlayerBalanceDefinition otherNewBalance = await Economy.PlayerBalances.SetBalanceAsync(currencyID, newAmount, writeLock);
```

#### IncrementBalance

Increments the balance of the specified currency by the specified value.

This method will throw an exception if the resulting value is above the maximum allowed for the currency. It will also throw an exception if the specified currency ID does not exist.

This method optionally takes a writeLock string. If provided, then an exception will be thrown unless the writeLock matches the writeLock received by a previous read, in order to provide optimistic concurrency. If not provided, the transaction will proceed regardless of any existing writeLock in the data.

This method returns the current balance after the update has been applied, if the operation is successful.

```cs
string currencyID = "GOLD_BARS";
int incrementAmount = 1000;
string writeLock = "someLockValueFromPreviousRequest";

PlayerBalanceDefinition newBalance = await Economy.PlayerBalances.IncrementBalanceAsync(currencyID, newAmount);
// OR
PlayerBalanceDefinition otherNewBalance = await Economy.PlayerBalances.IncrementBalanceAsync(currencyID, newAmount, writeLock);
```

#### DecrementBalance

Decrements the balance of the specified currency by the specified value.

This method will throw an exception if the resulting value is below the minimum allowed for the currency. It will also throw an exception if the specified currency ID does not exist.

This method optionally takes a writeLock string. If provided, then an exception will be thrown unless the writeLock matches the writeLock received by a previous read, in order to provide optimistic concurrency. If not provided, the transaction will proceed regardless of any existing writeLock in the data.

This method returns the current balance after the update has been applied, if the operation is successful.

```cs
string currencyID = "GOLD_BARS";
int decrementAmount = 1000;
string writeLock = "someLockValueFromPreviousRequest";

PlayerBalanceDefinition newBalance = await Economy.PlayerBalances.DecrementBalanceAsync(currencyID, newAmount);
// OR
PlayerBalanceDefinition otherNewBalance = await Economy.PlayerBalances.DecrementBalanceAsync(currencyID, newAmount, writeLock);
```

#### BalanceUpdated

This event can be subscribed to in order to be notified when the SDK updates the balance of a particular currency.
The subscriber will be passed the currency ID of the balance that was updated.

**Note**: This event will only be called for SDK initiated actions (e.g. updating player's balances, making purchases etc). 
It will _not_ be called for any updates from other devices / service side changes.

```cs
Economy.PlayerBalances.BalanceUpdated += currencyID => {
    Debug.Log($"The currency that was updated was {currencyID}");
};
```

### GetBalancesResult

A `GetBalancesResult` provides paginated access to the list of balances retrieved. It has the following fields:

- `Balances`: A `List<PlayerBalanceDefinition>` with the currently fetched balances

It has the following methods:

- `GetNextAsync(int itemsToFetch = 20)`: This method asynchronously fetches more results. It has one optional parameter to limit the amount
of results fetched (this can be between 1 and 100 inclusive, default is 20). It will return a new result, which contains both the original items and
the newly fetched items in it's `Balances` list.

### PlayerBalance

A player balance represents a single currency balance for a player. It has the following fields:

- `CurrencyId`: The ID of the currency this balance represents
- `Balance`: The integer amount of this currency the player has
- `WriteLock`: The current writeLock string
- `Created`: The date this balance was created. It is an `EconomyDate` object (see [here](./index.md#EconomyDate)).
- `Modified`: The date this balance was modified. It is an `EconomyDate` object (see [here](./index.md#EconomyDate)).

It also has the following helper methods

#### GetCurrencyDefinition

This is a convenience method to get the currency definition for the currency associated with this balance. 
It returns a `CurrencyDefinition`, as described [here](./configuration.md)

```cs
PlayerBalance myPlayerBalance = ... // Get a player balance from one of the above methods
CurrencyDefinition currencyDefForMyPlayerBalance = myPlayerBalance.GetCurrencyDefinitionAsync();
```



