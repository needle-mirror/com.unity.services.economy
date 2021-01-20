using Unity.Services.Economy.Internal;
using Unity.Services.Authentication;
using Unity.Services.Authentication.Internal;

namespace Unity.Services.Economy
{
    internal interface IEconomyAuthentication
    {
        string GetPlayerId();
        string GetAccessToken();
        void CheckSignedIn();
    }
    
    internal class EconomyAuthentication : IEconomyAuthentication
    {
        IPlayerId m_PlayerIdComponent;
        IAccessToken m_AccessTokenComponent;

        public EconomyAuthentication(IPlayerId playerIdComponent, IAccessToken accessTokenComponent)
        {
            m_AccessTokenComponent = accessTokenComponent;
            m_PlayerIdComponent = playerIdComponent;
        }
        
        public string GetPlayerId()
        {
            return m_PlayerIdComponent.PlayerId;
        }

        public string GetAccessToken()
        {
            return m_AccessTokenComponent.AccessToken;
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
