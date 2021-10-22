using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;

public class InventoriesBasicExample : MonoBehaviour
{
   
    async void FetchInventoryItemsConfig()
    {
        List<InventoryItemDefinition> items = await Economy.Configuration.GetInventoryItemsAsync();
        
        foreach (var item in items)
        {
            Debug.Log($"Items | ID: {item.Id} - Name: {item.Name}");
        }
    }

    async void FetchInventoryItemFromConfig()
    {
        InventoryItemDefinition item = await Economy.Configuration.GetInventoryItemAsync("item_id");
        
        Debug.Log($"Item | ID: {item.Id} - name: {item.Name}");
    }
    
    async void FetchPlayersInventoryItems()
    {
        PlayerInventory.GetInventoryOptions options = new PlayerInventory.GetInventoryOptions
        {
            ItemsPerFetch = 5
        };
        GetInventoryResult inventoryResult = await Economy.PlayerInventory.GetInventoryAsync(options);

        foreach (var playersInventoryItem in inventoryResult.PlayersInventoryItems)
        {
            Debug.Log($"Players item | Players Inventory Item ID: {playersInventoryItem.PlayersInventoryItemId} - Inventory Item ID: {playersInventoryItem.InventoryItemId}");
        }

        GetInventoryResult nextPage = await inventoryResult.GetNextAsync();
    }

    async void AddInventoryItemInstance()
    {
        Dictionary<string, object> instanceData = new Dictionary<string, object>();
        instanceData.Add("isRare", true);
        instanceData.Add("color", "silver");
        
        PlayerInventory.AddInventoryItemOptions options = new PlayerInventory.AddInventoryItemOptions
        {
          PlayersInventoryItemId  = "playersInventoryItemId",
          InstanceData = instanceData
        };
        PlayersInventoryItem playersItem = await Economy.PlayerInventory.AddInventoryItemAsync("inventoryItemId", options);
        
        Debug.Log($"Players item | Players Inventory Item ID: {playersItem.PlayersInventoryItemId} - Inventory Item ID: {playersItem.InventoryItemId}");
    }
    
    async void UpdatePlayersInventoryItem()
    {
        Dictionary<string, object> instanceData = new Dictionary<string, object>();
        instanceData.Add("isRare", true);
        instanceData.Add("color", "silver");
        
        PlayersInventoryItem playersItem = await Economy.PlayerInventory.UpdatePlayersInventoryItemAsync("playersInventoryItemId", 
            instanceData);
        
        Debug.Log($"Players item | Players Inventory Item ID: {playersItem.PlayersInventoryItemId} - Inventory Item ID: {playersItem.InventoryItemId}");
    }
    
    async void UpdatePlayersInventoryItemUsingWriteLock()
    {
        GetInventoryResult inventoryResult = await Economy.PlayerInventory.GetInventoryAsync();
        PlayersInventoryItem playersInventoryItem = inventoryResult.PlayersInventoryItems.First();
        
        Dictionary<string, object> instanceData = new Dictionary<string, object>();
        instanceData.Add("isRare", true);
        instanceData.Add("color", "silver");

        PlayerInventory.UpdatePlayersInventoryItemOptions options = new PlayerInventory.UpdatePlayersInventoryItemOptions
        {
            WriteLock = playersInventoryItem.WriteLock
        };
        PlayersInventoryItem updatedItem = await Economy.PlayerInventory.UpdatePlayersInventoryItemAsync(playersInventoryItem.PlayersInventoryItemId, 
            instanceData, options);
        
        Debug.Log($"Players item | Players Inventory Item ID: {updatedItem.PlayersInventoryItemId} - Inventory Item ID: {updatedItem.InventoryItemId}");
    }

    async void DeleteInventoryItem()
    {
        await Economy.PlayerInventory.DeletePlayersInventoryItemAsync("playersInventoryItemId");
    }
}
