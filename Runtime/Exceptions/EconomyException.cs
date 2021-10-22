using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Unity.Services.Economy.Editor")]
namespace Unity.Services.Economy
{        
    /// <summary>
    /// An enum of possible reasons that Economy would throw an exception. These are mapped to particular HTTP status
    /// codes.
    /// </summary>
    public enum EconomyExceptionReason : long
    {
        Unknown = 0,
        NetworkError = 1,
        
        InvalidArgument = 400,
        Unauthorized = 401,
        Forbidden = 403,
        EntityNotFound = 404,
        RequestTimeOut = 408,
        Conflict = 409,
        UnprocessableTransaction = 422,
        RateLimited = 429,
        
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
    }

    /// <summary>
    /// An exception specific to the Economy service.
    /// </summary>
    public class EconomyException : Core.RequestFailedException
    {
        static readonly string unknownError = "An unknown error occurred in the Economy SDK.";
        
        /// <summary>
        /// The reason the exception was thrown, selected from the EconomyExceptionReason enum.
        /// </summary>
        public EconomyExceptionReason Reason { get; private set; }
        
        internal EconomyException(EconomyExceptionReason reason, int serviceErrorCode, string description) 
            : base(serviceErrorCode, description ?? unknownError)
        {
            Reason = reason;
        }
        
        internal EconomyException(EconomyExceptionReason reason, int serviceErrorCode, string description, Exception inner) 
            : base(serviceErrorCode, description ?? unknownError, inner)
        {
            Reason = reason;
        }

        internal EconomyException(long httpStatusCode, int serviceErrorCode, string description, Exception inner)
            : base(serviceErrorCode, description ?? unknownError, inner)
        {
            if (Enum.IsDefined(typeof(EconomyExceptionReason), httpStatusCode))
            {
                Reason = (EconomyExceptionReason)httpStatusCode;
            }
            else
            {
                Reason = EconomyExceptionReason.Unknown;
            }
        }
    }
}
