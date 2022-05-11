using System;

namespace Unity.Services.Economy
{
    /// <summary>
    /// An exception that is thrown when the client has been rate limited.
    /// </summary>
    public class EconomyRateLimitedException : EconomyException
    {
        /// <summary>
        /// The number of seconds until the client is no longer rate limited.
        /// </summary>
        public int RetryAfter { get; private set; }

        internal EconomyRateLimitedException(EconomyExceptionReason reason, int serviceErrorCode, string description, int retryAfter, Exception e)
            : base(reason, serviceErrorCode, description, e)
        {
            RetryAfter = retryAfter;
        }
    }
}
