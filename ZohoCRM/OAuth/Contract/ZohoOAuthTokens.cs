using System;
using ZCRMSDK.OAuth.Common;

namespace ZCRMSDK.OAuth.Contract
{
    public class ZohoOAuthTokens
    {

        private string userMailId;
        private string accessToken;
        private string refreshToken;
        private long expiryTime;

        public string UserMaiilId { get => userMailId; set => userMailId = value; }

        public string RefreshToken { get => refreshToken; set => refreshToken = value; }

        public long ExpiryTime { get => expiryTime; set => expiryTime = value; }

        public string AccessToken { 
            get {
                 if(IsAccessTokenValid()){
                    return accessToken;
                }
                throw new ZohoOAuthException("Access token expired");
            }
            set => accessToken = value; 
        }

        private bool IsAccessTokenValid(){
            if(GetExpiryLapseInMillis() > 10L){
                return true;
            }
            return false;
        }

        public long GetExpiryLapseInMillis() {
            long time = (ExpiryTime - (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            return (time);
        }

        //NOTE: Omitted JsonObject to Json Method();

    }
}
