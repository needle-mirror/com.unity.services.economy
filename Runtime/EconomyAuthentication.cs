using Unity.Services.Authentication.Internal;

namespace Unity.Services.Economy
{
    interface IEconomyAuthentication
    {
        string GetPlayerId();
        string GetAccessToken();
        string GetUnityInstallationId();
        string GetAnalyticsUserId();
        void CheckSignedIn();
        string configAssignmentHash { get; set; }
    }

    class EconomyAuthentication : IEconomyAuthentication
    {
        IPlayerId m_PlayerIdComponent;
        IAccessToken m_AccessTokenComponent;
        string m_unityInstallationId;
        string m_analyticsUserId;

        public string configAssignmentHash { get; set; }

        public EconomyAuthentication(IPlayerId playerIdComponent, IAccessToken accessTokenComponent, string unityInstallationId, string analyticsUserId)
        {
            m_AccessTokenComponent = accessTokenComponent;
            m_PlayerIdComponent = playerIdComponent;
            m_unityInstallationId = unityInstallationId;
            m_analyticsUserId = analyticsUserId;
        }

        public string GetPlayerId()
        {
            return m_PlayerIdComponent.PlayerId;
        }

        public string GetAccessToken()
        {
            return m_AccessTokenComponent.AccessToken;
        }

        public string GetUnityInstallationId()
        {
            return m_unityInstallationId;
        }

        public string GetAnalyticsUserId()
        {
            return m_analyticsUserId;
        }

        public void CheckSignedIn()
        {
            // When sign in completes, the access token is set. If sign out is called or the session expires, it is cleared.
            if (GetAccessToken() == null)
            {
                throw new EconomyException(EconomyExceptionReason.Unauthorized, Core.CommonErrorCodes.Forbidden, "You are not signed in to the Authentication Service. Please sign in.");
            }
        }
    }
}
