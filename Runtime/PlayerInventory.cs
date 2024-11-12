using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Services.Economy.Internal;
using Unity.Services.Economy.Internal.Apis.Inventory;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Economy.Internal.Inventory;
using Unity.Services.Economy.Internal.Models;
using Unity.Services.Economy.Model;
using Unity.Services.Core.Configuration.Internal;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Tests")]

namespace Unity.Services.Economy
{
    /// <summary>
    /// The PlayerInventory methods provide access to the current player's inventory items, and allow you to update them.
    /// </summary>
    public interface IEconomyPlayerInventoryApiClient
    {
        /// <summary>
        /// Fires when the SDK updates a player's inventory item (e.g. by editing the custom data). The called function will be passed the player inventory item ID
        /// that was updated. (Note: this is the ID of the individual inventory item owned by the player, not the item configuration).
        /// Note that this will NOT fire for balance changes from elsewhere not in this instance of the SDK, for example other
        /// server-side updates or updates from other devices.
        /// </summary>
        event Action<string> PlayersInventoryItemUpdated;

        /// <summary>
        /// Gets the inventory items in the inventory of the player that is currently signed in.
        /// The players items are available on the returned object using the <c>PlayersInventoryItems</c> property.
        /// The results are paginated - the first set of results are initially returned, and more can be requested with the <c>GetNextAsync</c> method.
        /// The <c>HasNext</c> property indicates whether there are more results to be returned.
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="options">(Optional) Use to set request options. See GetInventoryOptions for more details.</param>
        /// <returns>A GetInventoryResult object, with properties as specified above.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        /// <exception cref="EconomyRateLimitedException">Thrown if the service returned rate limited error.</exception>
        Task<GetInventoryResult> GetInventoryAsync(GetInventoryOptions options = null);

        /// <summary>
        /// Adds an inventory item to the player's inventory.
        ///
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="inventoryItemId">The item ID to add</param>
        /// <param name="options">(Optional) Use to set the PlayersInventoryItem ID for the created instance and instance data.</param>
        /// <returns>The created player inventory item.</returns>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        /// <exception cref="EconomyValidationException">Thrown if the service returned validation error.</exception>
        /// <exception cref="EconomyRateLimitedException">Thrown if the service returned rate limited error.</exception>
        Task<PlayersInventoryItem> AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options = null);

        /// <summary>
        /// Deletes an item in the player's inventory.
        ///
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="playersInventoryItemId">PlayersInventoryItem ID for the created inventory item</param>
        /// <param name="options">(Optional) Use to set a write lock for optimistic concurrency</param>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        /// <exception cref="EconomyValidationException">Thrown if the service returned validation error.</exception>
        /// <exception cref="EconomyRateLimitedException">Thrown if the service returned rate limited error.</exception>
        /// <returns>Object representing the continuation of the task</returns>
        Task DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options = null);

        /// <summary>
        /// Updates the instance data of an item in the player's inventory.
        ///
        /// Throws a EconomyException with a reason code and explanation if the request is badly formed, unauthorized or uses a missing resource.
        /// </summary>
        /// <param name="playersInventoryItemId">PlayersInventoryItem ID for the created inventory item</param>
        /// <param name="instanceData">Instance data</param>
        /// <param name="options">(Optional) Use to set a write lock for optimistic concurrency</param>
        /// <exception cref="EconomyException">Thrown if request is unsuccessful</exception>
        /// <exception cref="EconomyValidationException">Thrown if the service returned validation error.</exception>
        /// <exception cref="EconomyRateLimitedException">Thrown if the service returned rate limited error.</exception>
        /// <returns>The updated item</returns>
        Task<PlayersInventoryItem> UpdatePlayersInventoryItemAsync(string playersInventoryItemId,
            object instanceData, UpdatePlayersInventoryItemOptions options = null);
    }

    /// <inheritdoc/>
    class PlayerInventoryInternal : IEconomyPlayerInventoryApiClient
    {
        readonly IInventoryApiClient m_InventoryApiClient;
        readonly IEconomyAuthentication m_EconomyAuthentication;
        readonly ICloudProjectId m_CloudProjectId;

        internal PlayerInventoryInternal(ICloudProjectId cloudProjectId, IInventoryApiClient inventoryApiClient, IEconomyAuthentication economyAuthentication)
        {
            m_CloudProjectId = cloudProjectId;
            m_InventoryApiClient = inventoryApiClient;
            m_EconomyAuthentication = economyAuthentication;
        }

        /// <inheritdoc/>
        public event Action<string> PlayersInventoryItemUpdated;

        /// <inheritdoc/>
        public async Task<GetInventoryResult> GetInventoryAsync(GetInventoryOptions options = null)
        {
            return await GetNextInventory(null, options);
        }

        internal async Task<GetInventoryResult> GetNextInventory(string afterPlayersInventoryItemId, GetInventoryOptions options = null)
        {
            if (options == null)
            {
                options = new GetInventoryOptions();
            }

            m_EconomyAuthentication.CheckSignedIn();

            EconomyAPIErrorHandler.HandleItemsPerFetchExceptions(options.ItemsPerFetch);

            GetPlayerInventoryRequest request = new GetPlayerInventoryRequest(
                m_CloudProjectId.GetCloudProjectId(),
                m_EconomyAuthentication.GetPlayerId(),
                m_EconomyAuthentication.configAssignmentHash,
                m_EconomyAuthentication.GetUnityInstallationId(),
                m_EconomyAuthentication.GetAnalyticsUserId(),
                afterPlayersInventoryItemId,
                options.ItemsPerFetch,
                options.PlayersInventoryItemIds,
                options.InventoryItemIds
            );

            try
            {
                Response<PlayerInventoryResponse> response = await m_InventoryApiClient.GetPlayerInventoryAsync(request);

                List<PlayersInventoryItem> playersInventoryItems = ConvertToPlayersInventoryItems(response.Result.Results);

                return new GetInventoryResult(playersInventoryItems, ResponseHasNextLinks(response.Result), options.PlayersInventoryItemIds, options.InventoryItemIds, this);
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }

        /// <inheritdoc/>
        public async Task<PlayersInventoryItem> AddInventoryItemAsync(string inventoryItemId, AddInventoryItemOptions options = null)
        {
            m_EconomyAuthentication.CheckSignedIn();

            AddInventoryItemRequest request = new AddInventoryItemRequest(
                m_CloudProjectId.GetCloudProjectId(),
                m_EconomyAuthentication.GetPlayerId(),
                new AddInventoryRequest(inventoryItemId, options?.PlayersInventoryItemId, options?.InstanceData),
                m_EconomyAuthentication.configAssignmentHash,
                m_EconomyAuthentication.GetUnityInstallationId(),
                m_EconomyAuthentication.GetAnalyticsUserId());

            try
            {
                Response<InventoryResponse> response = await m_InventoryApiClient.AddInventoryItemAsync(request);

                PlayersInventoryItem playersInventoryItem = ConvertToPlayersInventoryItem(response.Result);
                FireInventoryItemUpdated(playersInventoryItem.PlayersInventoryItemId);

                return playersInventoryItem;
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException<ValidationErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }

        /// <inheritdoc/>
        public async Task DeletePlayersInventoryItemAsync(string playersInventoryItemId, DeletePlayersInventoryItemOptions options = null)
        {
            m_EconomyAuthentication.CheckSignedIn();

            DeleteInventoryItemRequest request = new DeleteInventoryItemRequest(
                m_CloudProjectId.GetCloudProjectId(),
                m_EconomyAuthentication.GetPlayerId(),
                playersInventoryItemId,
                m_EconomyAuthentication.configAssignmentHash,
                m_EconomyAuthentication.GetUnityInstallationId(),
                m_EconomyAuthentication.GetAnalyticsUserId(),
                options?.WriteLock,
                new InventoryDeleteRequest(options?.WriteLock));

            try
            {
                await m_InventoryApiClient.DeleteInventoryItemAsync(request);
                FireInventoryItemUpdated(playersInventoryItemId);
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException<ValidationErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }

        /// <inheritdoc/>
        public async Task<PlayersInventoryItem> UpdatePlayersInventoryItemAsync(string playersInventoryItemId, object instanceData, UpdatePlayersInventoryItemOptions options = null)
        {
            m_EconomyAuthentication.CheckSignedIn();

            UpdateInventoryItemRequest request = new UpdateInventoryItemRequest(
                m_CloudProjectId.GetCloudProjectId(),
                m_EconomyAuthentication.GetPlayerId(),
                playersInventoryItemId,
                new InventoryRequestUpdate(instanceData, options?.WriteLock),
                m_EconomyAuthentication.configAssignmentHash,
                m_EconomyAuthentication.GetUnityInstallationId(),
                m_EconomyAuthentication.GetAnalyticsUserId());

            try
            {
                Response<InventoryResponse> response = await m_InventoryApiClient.UpdateInventoryItemAsync(request);

                PlayersInventoryItem playersInventoryItem = ConvertToPlayersInventoryItem(response.Result);

                FireInventoryItemUpdated(playersInventoryItemId);

                return playersInventoryItem;
            }
            catch (HttpException<BasicErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException<ValidationErrorResponse> e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
            catch (HttpException e)
            {
                throw EconomyAPIErrorHandler.HandleException(e);
            }
        }

        internal static bool ResponseHasNextLinks(PlayerInventoryResponse response)
        {
            return !string.IsNullOrEmpty(response.Links?.Next);
        }

        static List<PlayersInventoryItem> ConvertToPlayersInventoryItems(List<InventoryResponse> responses)
        {
            List<PlayersInventoryItem> playersInventoryItems = new List<PlayersInventoryItem>(responses.Count);
            foreach (var response in responses)
            {
                playersInventoryItems.Add(ConvertToPlayersInventoryItem(response));
            }

            return playersInventoryItems;
        }

        internal static PlayersInventoryItem ConvertToPlayersInventoryItem(InventoryResponse response)
        {
            return new PlayersInventoryItem
            {
                PlayersInventoryItemId = response.PlayersInventoryItemId,
                InventoryItemId = response.InventoryItemId,
                InstanceData = response.InstanceData,
                WriteLock = response.WriteLock,
                Modified = response.Modified.Date == null ? null : new EconomyDate {Date = response.Modified.Date.Value},
                Created = response.Created.Date == null ? null : new EconomyDate {Date = response.Created.Date.Value}
            };
        }

        internal void FireInventoryItemUpdated(string playersInventoryItemId)
        {
            PlayersInventoryItemUpdated?.Invoke(playersInventoryItemId);
        }
    }

    /// <summary>
    /// Options for a GetInventoryAsync call.
    /// </summary>
    public class GetInventoryOptions
    {
        /// <summary>
        /// The PlayersInventoryItem IDs of the items in the players inventory that you want to retrieve.
        /// </summary>
        public List<string> PlayersInventoryItemIds = null;

        /// <summary>
        /// The configuration IDs of the items you want to retrieve.
        /// </summary>
        public List<string> InventoryItemIds = null;

        /// <summary>
        /// Used to specify the number of items to fetch per request. Defaults to 20 items.
        /// </summary>
        public int ItemsPerFetch = 20;
    }

    /// <summary>
    /// Options for a AddInventoryItemAsync call.
    /// </summary>
    public class AddInventoryItemOptions
    {
        /// <summary>
        /// Sets the ID of the created PlayersInventoryItem. If not supplied, one will be generated.
        /// </summary>
        public string PlayersInventoryItemId = null;

        /// <summary>
        /// Dictionary of instance data.
        /// </summary>
        public object InstanceData = null;
    }

    /// <summary>
    /// Options for a DeletePlayersInventoryItemAsync call.
    /// </summary>
    public class DeletePlayersInventoryItemOptions
    {
        /// <summary>
        /// A write lock for optimistic concurrency.
        /// </summary>
        public string WriteLock = null;
    }

    /// <summary>
    /// Options for a UpdatePlayersInventoryItemAsync call.
    /// </summary>
    public class UpdatePlayersInventoryItemOptions
    {
        /// <summary>
        /// A write lock for optimistic concurrency.
        /// </summary>
        public string WriteLock = null;
    }
}
