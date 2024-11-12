using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Services.Economy;
using UnityEngine;

namespace Unity.Services.Economy.Tools
{
    /// <summary>Helper for the PlayerInventoriesHelper</summary>
    [CreateAssetMenu(fileName = "PlayerInventoriesHelper", menuName = "Economy Tools/Player Inventories Helper")]
    public class PlayerInventoriesHelper : ScriptableObject
    {
        /// <summary>InventoriesAction enum  </summary>
        public enum InventoriesAction
        {
            /// <summary>Add to the inventory</summary>
            Add,
            /// <summary>Update to the inventory</summary>
            Update,
            /// <summary>Delete to the inventory</summary>
            Delete
        }

        /// <summary>Action to take on the inventory </summary>
        [Header("Inventories Helper")]
        public InventoriesAction action;
        /// <summary>Inventory item to affect </summary>
        public string playersInventoryItemId;

        /// <summary>InventoryItem id to affect</summary>
        [Header("Add Instance Settings")]
        public string inventoryItemId;

        /// <summary>Instance json</summary>
        [Header("Update Instance Settings")]
        [TextArea]
        public string instanceDataJson;

        /// <summary>
        /// Used to trigger the call to the Economy Service using the options set in the inspector.
        /// </summary>
        public async void InvokeAsync()
        {
            switch (action)
            {
                case InventoriesAction.Add when string.IsNullOrEmpty(playersInventoryItemId):
                    ThrowExceptionIfItemIdNull();
                    try
                    {
                        await EconomyService.Instance.PlayerInventory.AddInventoryItemAsync(inventoryItemId);
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
                case InventoriesAction.Add:
                    ThrowExceptionIfItemIdNull();
                    AddInventoryItemOptions options = new AddInventoryItemOptions
                    {
                        PlayersInventoryItemId = playersInventoryItemId
                    };
                    try
                    {
                        await EconomyService.Instance.PlayerInventory.AddInventoryItemAsync(inventoryItemId, options);
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
                case InventoriesAction.Update:
                {
                    ThrowExceptionIfMissingInstanceId();
                    ThrowExceptionIfMissingPlayersInventoryItemId();

                    Dictionary<string, object> instanceData = JsonConvert.DeserializeObject<Dictionary<string, object>>(instanceDataJson);
                    try
                    {
                        await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync(playersInventoryItemId, instanceData);
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
                case InventoriesAction.Delete:
                {
                    ThrowExceptionIfMissingPlayersInventoryItemId();

                    try
                    {
                        await EconomyService.Instance.PlayerInventory.DeletePlayersInventoryItemAsync(playersInventoryItemId);
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

        void ThrowExceptionIfItemIdNull()
        {
            if (string.IsNullOrEmpty(inventoryItemId))
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Unity.Services.Core.CommonErrorCodes.Unknown, "The inventory item ID on the player inventories helper scriptable object is empty. Please enter an ID.");
            }
        }

        void ThrowExceptionIfMissingInstanceId()
        {
            if (string.IsNullOrEmpty(instanceDataJson))
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Unity.Services.Core.CommonErrorCodes.Unknown, "The custom data field on the player inventories helper scriptable object is empty. Please enter custom data.");
            }
        }

        void ThrowExceptionIfMissingPlayersInventoryItemId()
        {
            if (string.IsNullOrEmpty(playersInventoryItemId))
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Unity.Services.Core.CommonErrorCodes.Unknown, "The players inventory item ID on the player inventories helper scriptable object is empty. Please enter an ID.");
            }
        }
    }
}
