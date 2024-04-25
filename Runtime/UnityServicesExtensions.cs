using Unity.Services.Economy;

namespace Unity.Services.Core
{
    /// <summary>
    /// Economy Services Extension Methods
    /// </summary>
    public static class UnityServicesExtensions
    {
        /// <summary>
        /// Retrieve the economy service from the core service registry
        /// </summary>
        /// <param name="unityServices">The core services instance</param>
        /// <returns>The economy service instance</returns>
        public static IEconomyService GetEconomyService(this IUnityServices unityServices)
        {
            return unityServices.GetService<IEconomyService>();
        }
    }
}
