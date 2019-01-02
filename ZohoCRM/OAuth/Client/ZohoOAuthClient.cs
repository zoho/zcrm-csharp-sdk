using System;
using ZCRMSDK.OAuth.Contract;
using ZCRMSDK.OAuth.Common;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.CRMException;
using System.Net;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.RestClient;

namespace ZCRMSDK.OAuth.Client
{
    public class ZohoOAuthClient
    {
        //TODO: Get Logger

        private static ZohoOAuthClient client = null;
        private ZohoOAuthParams oAuthParams;

        public ZohoOAuthClient(ZohoOAuthParams oAuthParams)
        {
            this.oAuthParams = oAuthParams;
        }

        public static ZohoOAuthClient GetInstance(ZohoOAuthParams oAuthParams){
            client = new ZohoOAuthClient(oAuthParams);
            return client;
        }

        public static ZohoOAuthClient GetInstance(){
            return client;
        }

        //TODO: Throw exceptions;
        public static void Initialize() { }


        public string GetAccessToken(string userMailId) {
            IZohoPersistenceHandler persistenceHandler = ZohoOAuth.GetPersistenceHandlerInstance();

            ZohoOAuthTokens tokens;
            try
            {
                ZCRMLogger.LogInfo("Retreiving access token..");
                tokens = persistenceHandler.GetOAuthTokens(userMailId);
            }
            catch (Exception e)when(!(e is ZohoOAuthException))
            {
                ZCRMLogger.LogError("Exception while fetching tokens from persistence" + e);
                throw new ZohoOAuthException(e); 
            }
            try
            {
                return tokens.AccessToken;
            }
            catch (ZohoOAuthException)
            {
                ZCRMLogger.LogInfo("Access Token expired, Refreshing Access token");
                tokens = RefreshAccessToken(tokens.RefreshToken, userMailId);
            }
            return tokens.AccessToken;
         }

        /// <summary>
        /// To get access token from grantToken.
        /// </summary>
        /// <returns>ZohoOAuthTokens class instance.</returns>
        /// <param name="grantToken">Grant token (String) of the oauth client</param>
        public ZohoOAuthTokens GenerateAccessToken(string grantToken)
        {
            if (grantToken == null || grantToken.Length == 0)
            {
                throw new ZohoOAuthException("Grant Type is not provided");
            }

            try
            {
                var conn = GetZohoConnector(ZohoOAuth.GetTokenURL());
                conn.AddParam(ZohoOAuthConstants.GRANT_TYPE, ZohoOAuthConstants.GRANT_TYPE_AUTH_CODE);
                conn.AddParam(ZohoOAuthConstants.CODE, grantToken);
                string response = conn.Post();

                JObject responseJSON = JObject.Parse(response);

                if (responseJSON.ContainsKey(ZohoOAuthConstants.ACCESS_TOKEN))
                {
                    ZohoOAuthTokens tokens = GetTokensFromJSON(responseJSON);
                    tokens.UserMaiilId = GetUserMailId(tokens.AccessToken);
                    ZohoOAuth.GetPersistenceHandlerInstance().SaveOAuthTokens(tokens);
                    return tokens;
                }
                throw new ZohoOAuthException("Exception while fetching Access Token from grant token" + response);
            }
            catch (WebException e)
            {
                ZCRMLogger.LogError(e);
                throw new ZohoOAuthException(e);
            }
        }

        /// <summary>
        /// To generates the access token from refresh token.
        /// </summary>
        /// <returns>ZohoOAuthTokens class instance.</returns>
        /// <param name="refreshToken">Refresh token (String) of the oauth client</param>
        /// <param name="userMailId">User mail Id (String)</param>
        public ZohoOAuthTokens GenerateAccessTokenFromRefreshToken(string refreshToken, string userMailId)
        {
            return RefreshAccessToken(refreshToken, userMailId);
        }

        //TODO: Create ZohoOAuthException class and change the throw exception class;
        private ZohoOAuthTokens RefreshAccessToken(string refreshToken, string userMailId){
            if(refreshToken == null){
                throw new ZohoOAuthException("Refresh token is not provided");
            }
            try{
                ZohoHTTPConnector conn = GetZohoConnector(ZohoOAuth.GetRefreshTokenURL());
                conn.AddParam(ZohoOAuthConstants.GRANT_TYPE, ZohoOAuthConstants.REFRESH_TOKEN);
                conn.AddParam(ZohoOAuthConstants.REFRESH_TOKEN, refreshToken);
                string response = conn.Post();
                JObject responseJSON = JObject.Parse(response);
                if(responseJSON.ContainsKey(ZohoOAuthConstants.ACCESS_TOKEN)){
                    ZohoOAuthTokens tokens = GetTokensFromJSON(responseJSON);
                    tokens.RefreshToken = refreshToken;
                    tokens.UserMaiilId = userMailId;
                    ZohoOAuth.GetPersistenceHandlerInstance().SaveOAuthTokens(tokens);
                    return tokens;
                }
                throw new ZohoOAuthException("Exception while fetching access tokens from Refresh Token" + response);
            }catch(WebException e){
                ZCRMLogger.LogError(e);
                throw new ZohoOAuthException(e);
            }
        }


        //TODO: the method throws three exceptions and check for null exception on access_token.
        private string GetUserMailId(string accessToken)
        {
            try
            {
                ZohoHTTPConnector conn = new ZohoHTTPConnector() { Url = ZohoOAuth.GetUserInfoURL() };
                conn.AddHeader("Authorization", ZohoOAuthConstants.AuthHeaderPrefix + accessToken);
                string response = conn.Get();
                JObject responseJSON = JObject.Parse(response);

                return responseJSON["Email"].ToString();
            }
            catch (WebException) { throw; }
        }

        private ZohoHTTPConnector GetZohoConnector(string url)
        {
            ZohoHTTPConnector conn = new ZohoHTTPConnector() { Url = url };
            conn.AddParam(ZohoOAuthConstants.CLIENT_ID, oAuthParams.ClientId);
            conn.AddParam(ZohoOAuthConstants.CLIENT_SECRET, oAuthParams.ClientSecret);
            conn.AddParam(ZohoOAuthConstants.REDIRECT_URL, oAuthParams.RedirectURL);
            return conn;
        }

        private ZohoOAuthTokens GetTokensFromJSON(JObject responseJSON){
            try
            {
                ZohoOAuthTokens tokens = new ZohoOAuthTokens();
                long expiresIn = Convert.ToInt64(responseJSON[ZohoOAuthConstants.EXPIRES_IN]);
                tokens.ExpiryTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + expiresIn - 600000;
                tokens.AccessToken = (string)responseJSON[ZohoOAuthConstants.ACCESS_TOKEN];
                if (responseJSON.ContainsKey(ZohoOAuthConstants.REFRESH_TOKEN))
                {
                    tokens.RefreshToken = (string)responseJSON[ZohoOAuthConstants.REFRESH_TOKEN];
                }
                //CRM.Library.Common.ZCRMConfigUtil.ConfigProperties["apiBaseUrl"] = (string)responseJSON["api_domain"];
                return tokens;
            }
            catch (Exception) { throw; }
        }
    }

}
