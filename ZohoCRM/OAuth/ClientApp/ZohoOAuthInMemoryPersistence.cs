using ZCRMSDK.OAuth.Client;
using ZCRMSDK.OAuth.Common;
using ZCRMSDK.OAuth.Contract;

namespace ZCRMSDK.OAuth.ClientApp
{
    public class ZohoOAuthInMemoryPersistence : IZohoPersistenceHandler
    {

        public void DeleteOAuthTokens(string paramName)
        {
            InMemoryStorage tokenStorage = InMemoryStorage.GetInstance();
            tokenStorage.UserIdentifier = null;
        }

        public ZohoOAuthTokens GetOAuthTokens(string userMailId)
        {
            InMemoryStorage tokenStorage = InMemoryStorage.GetInstance();
            if(!userMailId.Equals(tokenStorage.UserIdentifier))
            {
                throw new ZohoOAuthException("Given User not found in configuration");
            }
            ZohoOAuthTokens tokens = new ZohoOAuthTokens();
            tokens.UserMaiilId = tokenStorage.UserIdentifier;
            tokens.AccessToken = tokenStorage.AccessToken;
            tokens.RefreshToken = tokenStorage.RefreshToken;
            tokens.ExpiryTime = System.Convert.ToInt64(tokenStorage.ExpiryTime);
            return tokens;
        }

        public void SaveOAuthData(ZohoOAuthTokens zohoOAuthTokens)
        {
            InMemoryStorage tokenStorage = InMemoryStorage.GetInstance();
            tokenStorage.AccessToken = zohoOAuthTokens.AccessToken;
            tokenStorage.RefreshToken = zohoOAuthTokens.RefreshToken;
            tokenStorage.UserIdentifier = zohoOAuthTokens.UserMaiilId;
            tokenStorage.ExpiryTime = zohoOAuthTokens.ExpiryTime.ToString();
        }

        private class InMemoryStorage
        {
            private static InMemoryStorage tokenStorage = null;

            private string userIdentifier;
            private string accessToken;
            private string refreshToken;
            private string expiryTime;

            private InMemoryStorage() { }

            internal string UserIdentifier { get => userIdentifier; set => userIdentifier = value; }
            public string AccessToken { get => accessToken; set => accessToken = value; }
            public string RefreshToken { get => refreshToken; set => refreshToken = value; }
            public string ExpiryTime { get => expiryTime; set => expiryTime = value; }

            internal static InMemoryStorage GetInstance()
            {
                if(tokenStorage == null)
                {
                    tokenStorage = new InMemoryStorage();
                }
                return tokenStorage;
            }
        }

    }
}
