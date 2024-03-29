//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Scripting;
using Unity.Services.Economy.Internal.Models;
using Unity.Services.Economy.Internal.Scheduler;
using Unity.Services.Authentication.Internal;

namespace Unity.Services.Economy.Internal.Purchases
{
    internal static class JsonSerialization
    {
        public static byte[] Serialize<T>(T obj)
        {
            return Encoding.UTF8.GetBytes(SerializeToString(obj));
        }

        public static string SerializeToString<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings{ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore});
        }
    }

    /// <summary>
    /// PurchasesApiBaseRequest class
    /// </summary>
    [Preserve]
    internal class PurchasesApiBaseRequest
    {
        /// <summary>
        /// Helper function to add a provided key and value to the provided
        /// query params and to escape the values correctly if it is a URL.
        /// </summary>
        /// <param name="queryParams">A `List/<string/>` of the query parameters.</param>
        /// <param name="key">The key to be added.</param>
        /// <param name="value">The value to be added.</param>
        /// <returns>Returns a `List/<string/>` with the `key` and `value` added to the provided `queryParams`.</returns>
        [Preserve]
        public List<string> AddParamsToQueryParams(List<string> queryParams, string key, string value)
        {
            key = UnityWebRequest.EscapeURL(key);
            value = UnityWebRequest.EscapeURL(value);
            queryParams.Add($"{key}={value}");

            return queryParams;
        }

        /// <summary>
        /// Helper function to add a provided key and list of values to the
        /// provided query params and to escape the values correctly if it is a
        /// URL.
        /// </summary>
        /// <param name="queryParams">A `List/<string/>` of the query parameters.</param>
        /// <param name="key">The key to be added.</param>
        /// <param name="values">List of values to be added.</param>
        /// <param name="style">string for defining the style, currently unused.</param>
        /// <param name="explode">True if query params should be escaped and added separately.</param>
        /// <returns>Returns a `List/<string/>`</returns>
        [Preserve]
        public List<string> AddParamsToQueryParams(List<string> queryParams, string key, List<string> values, string style, bool explode)
        {
            if (explode)
            {
                foreach(var value in values)
                {
                    string escapedValue = UnityWebRequest.EscapeURL(value);
                    queryParams.Add($"{UnityWebRequest.EscapeURL(key)}={escapedValue}");
                }
            }
            else
            {
                string paramString = $"{UnityWebRequest.EscapeURL(key)}=";
                foreach(var value in values)
                {
                    paramString += UnityWebRequest.EscapeURL(value) + ",";
                }
                paramString = paramString.Remove(paramString.Length - 1);
                queryParams.Add(paramString);
            }

            return queryParams;
        }

        /// <summary>
        /// Helper function to add a provided map of keys and values, representing a model, to the
        /// provided query params.
        /// </summary>
        /// <param name="queryParams">A `List/<string/>` of the query parameters.</param>
        /// <param name="modelVars">A `Dictionary` representing the vars of the model</param>
        /// <returns>Returns a `List/<string/>`</returns>
        [Preserve]
        public List<string> AddParamsToQueryParams(List<string> queryParams, Dictionary<string, string> modelVars)
        {
            foreach(var key in modelVars.Keys)
            {
                string escapedValue = UnityWebRequest.EscapeURL(modelVars[key]);
                queryParams.Add($"{UnityWebRequest.EscapeURL(key)}={escapedValue}");
            }

            return queryParams;
        }

        /// <summary>
        /// Helper function to add a provided key and value to the provided
        /// query params and to escape the values correctly if it is a URL.
        /// </summary>
        /// <param name="queryParams">A `List/<string/>` of the query parameters.</param>
        /// <param name="key">The key to be added.</param>
        /// <typeparam name="T">The type of the value to be added.</typeparam>
        /// <param name="value">The value to be added.</param>
        /// <returns>Returns a `List/<string/>`</returns>
        [Preserve]
        public List<string> AddParamsToQueryParams<T>(List<string> queryParams, string key, T value)
        {
            if (queryParams == null)
            {
                queryParams = new List<string>();
            }

            key = UnityWebRequest.EscapeURL(key);
            string valueString = UnityWebRequest.EscapeURL(value.ToString());
            queryParams.Add($"{key}={valueString}");
            return queryParams;
        }

        /// <summary>
        /// Constructs a string representing an array path parameter.
        /// </summary>
        /// <param name="pathParam">The list of values to convert to string.</param>
        /// <returns>String representing the param.</returns>
        [Preserve]
        public string GetPathParamString(List<string> pathParam)
        {
            string paramString = "";
            foreach(var value in pathParam)
            {
                paramString += UnityWebRequest.EscapeURL(value) + ",";
            }
            paramString = paramString.Remove(paramString.Length - 1);
            return paramString;
        }

        /// <summary>
        /// Constructs the body of the request based on IO stream.
        /// </summary>
        /// <param name="stream">The IO stream to use.</param>
        /// <returns>Byte array representing the body.</returns>
        public byte[] ConstructBody(System.IO.Stream stream)
        {
            if (stream != null)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }

        /// <summary>
        /// Construct the request body based on string value.
        /// </summary>
        /// <param name="s">The input body.</param>
        /// <returns>Byte array representing the body.</returns>
        public byte[] ConstructBody(string s)
        {
            return System.Text.Encoding.UTF8.GetBytes(s);
        }

        /// <summary>
        /// Construct request body based on generic object.
        /// </summary>
        /// <param name="o">The object to use.</param>
        /// <returns>Byte array representing the body.</returns>
        public byte[] ConstructBody(object o)
        {
            return JsonSerialization.Serialize(o);
        }

        /// <summary>
        /// Generate an accept header.
        /// </summary>
        /// <param name="accepts">list of accepts objects.</param>
        /// <returns>The generated accept header.</returns>
        public string GenerateAcceptHeader(string[] accepts)
        {
            if (accepts.Length == 0)
            {
                return null;
            }
            for (int i = 0; i < accepts.Length; ++i)
            {
                if (string.Equals(accepts[i], "application/json", System.StringComparison.OrdinalIgnoreCase))
                {
                    return "application/json";
                }
            }
            return string.Join(", ", accepts);
        }

        private static readonly Regex JsonRegex = new Regex(@"application\/json(;\s)?((charset=utf8|q=[0-1]\.\d)(\s)?)*");

        /// <summary>
        /// Generate Content Type Header.
        /// </summary>
        /// <param name="contentTypes">The content types.</param>
        /// <returns>The Content Type Header.</returns>
        public string GenerateContentTypeHeader(string[] contentTypes)
        {
            if (contentTypes.Length == 0)
            {
                return null;
            }

            for(int i = 0; i < contentTypes.Length; ++i)
            {
                if (!string.IsNullOrWhiteSpace(contentTypes[i]) && JsonRegex.IsMatch(contentTypes[i]))
                {
                    return contentTypes[i];
                }
            }
            return contentTypes[0];
        }

        /// <summary>
        /// Generate multipart form file section.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="stream">The file stream to use.</param>
        /// <param name="contentType">The content type.</param>
        /// <returns>Returns a multipart form section.</returns>
        public IMultipartFormSection GenerateMultipartFormFileSection(string paramName, System.IO.FileStream stream, string contentType)
        {
            return new MultipartFormFileSection(paramName, ConstructBody(stream), GetFileName(stream.Name), contentType);
        }

        /// <summary>
        /// Generate multipart form file section.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="stream">The IO stream to use.</param>
        /// <param name="contentType">The content type.</param>
        /// <returns>Returns a multipart form section.</returns>
        public IMultipartFormSection GenerateMultipartFormFileSection(string paramName, System.IO.Stream stream, string contentType)
        {
            return new MultipartFormFileSection(paramName, ConstructBody(stream), Guid.NewGuid().ToString(), contentType);
        }

        private string GetFileName(string filePath)
        {
            return System.IO.Path.GetFileName(filePath);
        }
    }

    /// <summary>
    /// MakeVirtualPurchaseRequest
    /// Make virtual purchase
    /// </summary>
    [Preserve]
    internal class MakeVirtualPurchaseRequest : PurchasesApiBaseRequest
    {
        /// <summary>Accessor for projectId </summary>
        [Preserve]
        public string ProjectId { get; }
        /// <summary>Accessor for playerId </summary>
        [Preserve]
        public string PlayerId { get; }
        /// <summary>Accessor for playerPurchaseVirtualRequest </summary>
        [Preserve]
        public Unity.Services.Economy.Internal.Models.PlayerPurchaseVirtualRequest PlayerPurchaseVirtualRequest { get; }
        /// <summary>Accessor for configAssignmentHash </summary>
        [Preserve]
        public string ConfigAssignmentHash { get; }
        /// <summary>Accessor for unityInstallationId </summary>
        [Preserve]
        public string UnityInstallationId { get; }
        /// <summary>Accessor for analyticsUserId </summary>
        [Preserve]
        public string AnalyticsUserId { get; }
        string PathAndQueryParams;

        /// <summary>
        /// MakeVirtualPurchase Request Object.
        /// Make virtual purchase
        /// </summary>
        /// <param name="projectId">ID of the project.</param>
        /// <param name="playerId">ID of the player.</param>
        /// <param name="playerPurchaseVirtualRequest">PlayerPurchaseVirtualRequest param</param>
        /// <param name="configAssignmentHash">Configuration assignment hash that should be used when processing this request.</param>
        /// <param name="unityInstallationId">Unique identifier that identifies an installation on the client’s device. The same player can have different installationIds if they have the game installed on different devices. It is available to all Unity packages that integrate with the Services SDK Core package.</param>
        /// <param name="analyticsUserId">A unique string that identifies the player and is consistent across their subsequent play sessions for analytics purposes. It is the primary user identifier and it comes from the Core package.</param>
        [Preserve]
        public MakeVirtualPurchaseRequest(string projectId, string playerId, Unity.Services.Economy.Internal.Models.PlayerPurchaseVirtualRequest playerPurchaseVirtualRequest, string configAssignmentHash = default(string), string unityInstallationId = default(string), string analyticsUserId = default(string))
        {
            ProjectId = projectId;

            PlayerId = playerId;

            PlayerPurchaseVirtualRequest = playerPurchaseVirtualRequest;
            ConfigAssignmentHash = configAssignmentHash;
            UnityInstallationId = unityInstallationId;
            AnalyticsUserId = analyticsUserId;
            PathAndQueryParams = $"/v2/projects/{projectId}/players/{playerId}/purchases/virtual";

            List<string> queryParams = new List<string>();

            if(!string.IsNullOrEmpty(ConfigAssignmentHash))
            {
                queryParams = AddParamsToQueryParams(queryParams, "configAssignmentHash", ConfigAssignmentHash);
            }
            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Helper function for constructing URL from request base path and
        /// query params.
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Helper for constructing the request body.
        /// </summary>
        /// <returns>A list of IMultipartFormSection representing the request body.</returns>
        public byte[] ConstructBody()
        {
            return ConstructBody(PlayerPurchaseVirtualRequest);
        }

        /// <summary>
        /// Helper function for constructing the headers.
        /// </summary>
        /// <param name="accessToken">The auth access token to use.</param>
        /// <param name="operationConfiguration">The operation configuration to use.</param>
        /// <returns>A dictionary representing the request headers.</returns>
        public Dictionary<string, string> ConstructHeaders(IAccessToken accessToken,
            Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();
            if(!string.IsNullOrEmpty(accessToken.AccessToken))
            {
                headers.Add("authorization", "Bearer " + accessToken.AccessToken);
            }

            // Analytics headers
            headers.Add("Unity-Client-Version", Application.unityVersion);
            headers.Add("Unity-Client-Mode", Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");

            string[] contentTypes = {
                "application/json"
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var httpMethod = "POST";
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }
            else if (httpMethod == "POST" || httpMethod == "PATCH")
            {
                headers.Add("Content-Type", "application/json");
            }

            if(!string.IsNullOrEmpty(UnityInstallationId))
            {
                headers.Add("Unity-installation-id", UnityInstallationId);
            }
            if(!string.IsNullOrEmpty(AnalyticsUserId))
            {
                headers.Add("Analytics-user-id", AnalyticsUserId);
            }

            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }
    /// <summary>
    /// RedeemAppleAppStorePurchaseRequest
    /// Redeem Apple App Store purchase
    /// </summary>
    [Preserve]
    internal class RedeemAppleAppStorePurchaseRequest : PurchasesApiBaseRequest
    {
        /// <summary>Accessor for projectId </summary>
        [Preserve]
        public string ProjectId { get; }
        /// <summary>Accessor for playerId </summary>
        [Preserve]
        public string PlayerId { get; }
        /// <summary>Accessor for playerPurchaseAppleappstoreRequest </summary>
        [Preserve]
        public Unity.Services.Economy.Internal.Models.PlayerPurchaseAppleappstoreRequest PlayerPurchaseAppleappstoreRequest { get; }
        /// <summary>Accessor for configAssignmentHash </summary>
        [Preserve]
        public string ConfigAssignmentHash { get; }
        /// <summary>Accessor for unityInstallationId </summary>
        [Preserve]
        public string UnityInstallationId { get; }
        /// <summary>Accessor for analyticsUserId </summary>
        [Preserve]
        public string AnalyticsUserId { get; }
        string PathAndQueryParams;

        /// <summary>
        /// RedeemAppleAppStorePurchase Request Object.
        /// Redeem Apple App Store purchase
        /// </summary>
        /// <param name="projectId">ID of the project.</param>
        /// <param name="playerId">ID of the player.</param>
        /// <param name="playerPurchaseAppleappstoreRequest">PlayerPurchaseAppleappstoreRequest param</param>
        /// <param name="configAssignmentHash">Configuration assignment hash that should be used when processing this request.</param>
        /// <param name="unityInstallationId">Unique identifier that identifies an installation on the client’s device. The same player can have different installationIds if they have the game installed on different devices. It is available to all Unity packages that integrate with the Services SDK Core package.</param>
        /// <param name="analyticsUserId">A unique string that identifies the player and is consistent across their subsequent play sessions for analytics purposes. It is the primary user identifier and it comes from the Core package.</param>
        [Preserve]
        public RedeemAppleAppStorePurchaseRequest(string projectId, string playerId, Unity.Services.Economy.Internal.Models.PlayerPurchaseAppleappstoreRequest playerPurchaseAppleappstoreRequest, string configAssignmentHash = default(string), string unityInstallationId = default(string), string analyticsUserId = default(string))
        {
            ProjectId = projectId;

            PlayerId = playerId;

            PlayerPurchaseAppleappstoreRequest = playerPurchaseAppleappstoreRequest;
            ConfigAssignmentHash = configAssignmentHash;
            UnityInstallationId = unityInstallationId;
            AnalyticsUserId = analyticsUserId;
            PathAndQueryParams = $"/v2/projects/{projectId}/players/{playerId}/purchases/appleappstore";

            List<string> queryParams = new List<string>();

            if(!string.IsNullOrEmpty(ConfigAssignmentHash))
            {
                queryParams = AddParamsToQueryParams(queryParams, "configAssignmentHash", ConfigAssignmentHash);
            }
            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Helper function for constructing URL from request base path and
        /// query params.
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Helper for constructing the request body.
        /// </summary>
        /// <returns>A list of IMultipartFormSection representing the request body.</returns>
        public byte[] ConstructBody()
        {
            return ConstructBody(PlayerPurchaseAppleappstoreRequest);
        }

        /// <summary>
        /// Helper function for constructing the headers.
        /// </summary>
        /// <param name="accessToken">The auth access token to use.</param>
        /// <param name="operationConfiguration">The operation configuration to use.</param>
        /// <returns>A dictionary representing the request headers.</returns>
        public Dictionary<string, string> ConstructHeaders(IAccessToken accessToken,
            Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();
            if(!string.IsNullOrEmpty(accessToken.AccessToken))
            {
                headers.Add("authorization", "Bearer " + accessToken.AccessToken);
            }

            // Analytics headers
            headers.Add("Unity-Client-Version", Application.unityVersion);
            headers.Add("Unity-Client-Mode", Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");

            string[] contentTypes = {
                "application/json"
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var httpMethod = "POST";
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }
            else if (httpMethod == "POST" || httpMethod == "PATCH")
            {
                headers.Add("Content-Type", "application/json");
            }

            if(!string.IsNullOrEmpty(UnityInstallationId))
            {
                headers.Add("Unity-installation-id", UnityInstallationId);
            }
            if(!string.IsNullOrEmpty(AnalyticsUserId))
            {
                headers.Add("Analytics-user-id", AnalyticsUserId);
            }

            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }
    /// <summary>
    /// RedeemGooglePlayPurchaseRequest
    /// Redeem Google Play purchase
    /// </summary>
    [Preserve]
    internal class RedeemGooglePlayPurchaseRequest : PurchasesApiBaseRequest
    {
        /// <summary>Accessor for projectId </summary>
        [Preserve]
        public string ProjectId { get; }
        /// <summary>Accessor for playerId </summary>
        [Preserve]
        public string PlayerId { get; }
        /// <summary>Accessor for playerPurchaseGoogleplaystoreRequest </summary>
        [Preserve]
        public Unity.Services.Economy.Internal.Models.PlayerPurchaseGoogleplaystoreRequest PlayerPurchaseGoogleplaystoreRequest { get; }
        /// <summary>Accessor for configAssignmentHash </summary>
        [Preserve]
        public string ConfigAssignmentHash { get; }
        /// <summary>Accessor for unityInstallationId </summary>
        [Preserve]
        public string UnityInstallationId { get; }
        /// <summary>Accessor for analyticsUserId </summary>
        [Preserve]
        public string AnalyticsUserId { get; }
        string PathAndQueryParams;

        /// <summary>
        /// RedeemGooglePlayPurchase Request Object.
        /// Redeem Google Play purchase
        /// </summary>
        /// <param name="projectId">ID of the project.</param>
        /// <param name="playerId">ID of the player.</param>
        /// <param name="playerPurchaseGoogleplaystoreRequest">PlayerPurchaseGoogleplaystoreRequest param</param>
        /// <param name="configAssignmentHash">Configuration assignment hash that should be used when processing this request.</param>
        /// <param name="unityInstallationId">Unique identifier that identifies an installation on the client’s device. The same player can have different installationIds if they have the game installed on different devices. It is available to all Unity packages that integrate with the Services SDK Core package.</param>
        /// <param name="analyticsUserId">A unique string that identifies the player and is consistent across their subsequent play sessions for analytics purposes. It is the primary user identifier and it comes from the Core package.</param>
        [Preserve]
        public RedeemGooglePlayPurchaseRequest(string projectId, string playerId, Unity.Services.Economy.Internal.Models.PlayerPurchaseGoogleplaystoreRequest playerPurchaseGoogleplaystoreRequest, string configAssignmentHash = default(string), string unityInstallationId = default(string), string analyticsUserId = default(string))
        {
            ProjectId = projectId;

            PlayerId = playerId;

            PlayerPurchaseGoogleplaystoreRequest = playerPurchaseGoogleplaystoreRequest;
            ConfigAssignmentHash = configAssignmentHash;
            UnityInstallationId = unityInstallationId;
            AnalyticsUserId = analyticsUserId;
            PathAndQueryParams = $"/v2/projects/{projectId}/players/{playerId}/purchases/googleplaystore";

            List<string> queryParams = new List<string>();

            if(!string.IsNullOrEmpty(ConfigAssignmentHash))
            {
                queryParams = AddParamsToQueryParams(queryParams, "configAssignmentHash", ConfigAssignmentHash);
            }
            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Helper function for constructing URL from request base path and
        /// query params.
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Helper for constructing the request body.
        /// </summary>
        /// <returns>A list of IMultipartFormSection representing the request body.</returns>
        public byte[] ConstructBody()
        {
            return ConstructBody(PlayerPurchaseGoogleplaystoreRequest);
        }

        /// <summary>
        /// Helper function for constructing the headers.
        /// </summary>
        /// <param name="accessToken">The auth access token to use.</param>
        /// <param name="operationConfiguration">The operation configuration to use.</param>
        /// <returns>A dictionary representing the request headers.</returns>
        public Dictionary<string, string> ConstructHeaders(IAccessToken accessToken,
            Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();
            if(!string.IsNullOrEmpty(accessToken.AccessToken))
            {
                headers.Add("authorization", "Bearer " + accessToken.AccessToken);
            }

            // Analytics headers
            headers.Add("Unity-Client-Version", Application.unityVersion);
            headers.Add("Unity-Client-Mode", Scheduler.EngineStateHelper.IsPlaying ? "play" : "edit");

            string[] contentTypes = {
                "application/json"
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var httpMethod = "POST";
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }
            else if (httpMethod == "POST" || httpMethod == "PATCH")
            {
                headers.Add("Content-Type", "application/json");
            }

            if(!string.IsNullOrEmpty(UnityInstallationId))
            {
                headers.Add("Unity-installation-id", UnityInstallationId);
            }
            if(!string.IsNullOrEmpty(AnalyticsUserId))
            {
                headers.Add("Analytics-user-id", AnalyticsUserId);
            }

            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }
}
