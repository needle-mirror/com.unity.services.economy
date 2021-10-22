using System;
using System.Collections.Generic;
using Unity.GameBackend.Economy.Http;
using Unity.GameBackend.Economy.Models;

namespace Unity.Services.Economy.Exceptions
{
    static class EconomyAPIErrorHandler
    {
        static Dictionary<long, string> errorDetails = new Dictionary<long, string>
        {
            { 400, "Some of the arguments passed to the Economy request were invalid. Please check the requirements and try again." },
            { 401, "Permission denied when making a request to the Economy SDK. Have you signed in with the Authentication SDK?" },
            { 403, "Permission denied when making a request to the Economy SDK. Have you signed in with the Authentication SDK?" },
            { 404, "The requested entity was not found - please make sure it exists then try again." },
            { 409, "There was a conflict when updating the entity. Please make sure any write locks are using the correct values." },
            { 422, "Either the costs for a purchase were not met, you tried to exceed a currency maximum/minimum or this purchase has already been redeemed." },
            { 429, "Too many requests have been sent, so this device has been rate limited. Please try again later." }
        };

        internal static EconomyException HandleException(HttpException<BasicErrorResponse> e)
        {
            var reason = e.ActualError?.Detail;
            var httpStatusCode = e.Response.StatusCode;
            var errorCode = e.ActualError?.Code ?? 0;
            
            if (e.Response.IsNetworkError)
            {
                return new EconomyException(EconomyExceptionReason.NetworkError, Core.CommonErrorCodes.TransportError, reason ?? "The request to the Economy service failed - make sure you're connected to an internet connection and try again.", e);
            }

            if (reason == null)
            {
                if (errorDetails.ContainsKey(httpStatusCode))
                {
                    reason = errorDetails[httpStatusCode];
                }
                else
                {
                    reason = e.Message;
                }
            }

            return new EconomyException(httpStatusCode, errorCode, reason, e);
        }

        internal static EconomyException HandleException(HttpException e)
        {
            var reason = e.Response.ErrorMessage;
            var httpStatusCode = e.Response.StatusCode;
            var errorCode = (int)e.Response.StatusCode + 14000;
            
            if (e.Response.IsNetworkError)
            {
                return new EconomyException(EconomyExceptionReason.NetworkError, Core.CommonErrorCodes.TransportError, reason ?? "The request to the Economy service failed - make sure you're connected to an internet connection and try again.", e);
            }

            if (reason == null)
            {
                reason = errorDetails.ContainsKey(httpStatusCode) ? errorDetails[httpStatusCode] : e.Response.ErrorMessage;
            }
            return new EconomyException(httpStatusCode, errorCode, reason, e);
        }
        
        internal static EconomyAppleAppStorePurchaseFailedException HandleAppleAppStoreFailedExceptions(HttpException<ErrorResponsePurchaseAppleappstoreFailed> e)
        {
            return new EconomyAppleAppStorePurchaseFailedException(EconomyExceptionReason.UnprocessableTransaction, e.ActualError.Code, e.ActualError.Detail, e.ActualError.Data, e);
        }
        
        internal static EconomyGooglePlayStorePurchaseFailedException HandleGoogleStoreFailedExceptions(HttpException<ErrorResponsePurchaseGoogleplaystoreFailed> e)
        {
            return new EconomyGooglePlayStorePurchaseFailedException(EconomyExceptionReason.UnprocessableTransaction, e.ActualError.Code, e.ActualError.Detail, e.ActualError.Data, e);
        }

        internal static void HandleItemsPerFetchExceptions(int itemsPerFetch)
        {
            if (itemsPerFetch < 1 || itemsPerFetch > 100)
            {
                throw new EconomyException(EconomyExceptionReason.InvalidArgument, Core.CommonErrorCodes.Unknown, $"Items per fetch of {itemsPerFetch} is not valid. Please set it between 1 and 100 inclusive.");
            }
        }
    }
}
