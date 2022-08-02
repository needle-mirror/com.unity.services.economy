//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------

using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.Economy.Internal.Models;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Economy.Internal;
using Unity.Services.Authentication.Internal;

namespace Unity.Services.Economy.Internal.Apis.Configuration
{
    /// <summary>
    /// Interface for the ConfigurationApiClient
    /// </summary>
    internal interface IConfigurationApiClient
    {
            /// <summary>
            /// Async Operation.
            /// Get player&#39;s configuration.
            /// </summary>
            /// <param name="request">Request object for GetPlayerConfiguration.</param>
            /// <param name="operationConfiguration">Configuration for GetPlayerConfiguration.</param>
            /// <returns>Task for a Response object containing status code, headers, and PlayerConfigurationResponse object.</returns>
            /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<PlayerConfigurationResponse>> GetPlayerConfigurationAsync(GetPlayerConfigurationRequest request, Internal.Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IConfigurationApiClient"/>
    internal class ConfigurationApiClient : BaseApiClient, IConfigurationApiClient
    {
        private IAccessToken _accessToken;
        private const int _baseTimeout = 10;
        private Internal.Configuration _configuration;
        /// <summary>
        /// Accessor for the client configuration object. This returns a merge
        /// between the current configuration and the global configuration to
        /// ensure the correct combination of headers and a base path (if it is
        /// set) are returned.
        /// </summary>
        public Internal.Configuration Configuration
        {
            get {
                // We return a merge between the current configuration and the
                // global configuration to ensure we have the correct
                // combination of headers and a base path (if it is set).
                Internal.Configuration globalConfiguration = new Internal.Configuration("https://economy.services.api.unity.com", 10, 4, null);
                return Internal.Configuration.MergeConfigurations(_configuration, globalConfiguration);
            }
            set { _configuration = value; }
        }

        /// <summary>
        /// ConfigurationApiClient Constructor.
        /// </summary>
        /// <param name="httpClient">The HttpClient for ConfigurationApiClient.</param>
        /// <param name="accessToken">The Authentication token for the client.</param>
        /// <param name="configuration"> ConfigurationApiClient Configuration object.</param>
        public ConfigurationApiClient(IHttpClient httpClient,
            IAccessToken accessToken,
            Internal.Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;

            _accessToken = accessToken;
        }


        /// <summary>
        /// Async Operation.
        /// Get player&#39;s configuration.
        /// </summary>
        /// <param name="request">Request object for GetPlayerConfiguration.</param>
        /// <param name="operationConfiguration">Configuration for GetPlayerConfiguration.</param>
        /// <returns>Task for a Response object containing status code, headers, and PlayerConfigurationResponse object.</returns>
        /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<PlayerConfigurationResponse>> GetPlayerConfigurationAsync(GetPlayerConfigurationRequest request,
            Internal.Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(PlayerConfigurationResponse)   },{"400", typeof(MakeVirtualPurchase400OneOf)   },{"403", typeof(BasicErrorResponse)   },{"404", typeof(BasicErrorResponse)   },{"429", typeof(BasicErrorResponse)   },{"503", typeof(BasicErrorResponse)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Internal.Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(_accessToken, finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var responseDes = System.Text.Encoding.UTF8.GetString(response.Data);
             var handledResponse = ResponseHandler.HandleAsyncResponse<PlayerConfigurationResponse>(response, statusCodeToTypeMap);
            return new Response<PlayerConfigurationResponse>(response, handledResponse);
        }

    }
}