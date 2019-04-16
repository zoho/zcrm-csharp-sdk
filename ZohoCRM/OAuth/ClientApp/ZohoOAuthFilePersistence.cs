using System;
using System.Collections.Generic;
using ZCRMSDK.OAuth.Client;
using ZCRMSDK.OAuth.Contract;
using ZCRMSDK.OAuth.Common;
using ZCRMSDK.CRM.Library.CRMException;
using System.IO;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.OAuth.ClientApp
{
    public class ZohoOAuthFilePersistence : IZohoPersistenceHandler
    {
        public void DeleteOAuthTokens(string userMailId)
        {
            try
            {
                string persistencePath = GetPersistenceHandlerFilePath();
                Dictionary<string, string> oauthTokens = ZohoOAuthUtil.GetFileAsDict(new FileStream(persistencePath, FileMode.Open));
                if (!oauthTokens["useridentifier"].Equals(userMailId))
                {
                    throw new ZohoOAuthException("Given User not found in configuration");
                }

                File.WriteAllText(persistencePath, String.Empty);

            }
            catch (FileNotFoundException e)
            {
                ZCRMLogger.LogError("Exception while deleting tokens from file." + e);
                throw new ZohoOAuthException(e);
            }

        }

        public ZohoOAuthTokens GetOAuthTokens(string userMailId)
        {
            ZohoOAuthTokens tokens;
            try
            {
                tokens = new ZohoOAuthTokens();
                Dictionary<string, string> oauthTokens = ZohoOAuthUtil.GetFileAsDict(new FileStream(GetPersistenceHandlerFilePath(), FileMode.Open));
                //Dictionary<string, string> oauthTokens = ZohoOAuthUtil.ConfigFileSectionToDict("tokens", "oauth_tokens.config");
                if (!oauthTokens["useridentifier"].Equals(userMailId))
                {
                    throw new ZohoOAuthException("Given User not found in configuration");
                }
                tokens.UserMaiilId = oauthTokens["useridentifier"];
                tokens.AccessToken = oauthTokens["accesstoken"];
                tokens.RefreshToken = oauthTokens["refreshtoken"];
                tokens.ExpiryTime = Convert.ToInt64(oauthTokens["expirytime"]);
                return tokens;
            }
            catch (FileNotFoundException e)
            {
                ZCRMLogger.LogError("Exception while fetching tokens from configuration." + e);
                throw new ZohoOAuthException(e);
            }

        }

        public void SaveOAuthTokens(ZohoOAuthTokens zohoOAuthTokens)
        {
            try
            {
                FileStream outFile = new FileStream(GetPersistenceHandlerFilePath(), FileMode.Create);
                using(StreamWriter writer = new StreamWriter(outFile))
                {
                    writer.WriteLine("useridentifier=" + zohoOAuthTokens.UserMaiilId);
                    writer.WriteLine("accesstoken=" + zohoOAuthTokens.AccessToken);
                    writer.WriteLine("refreshtoken=" + zohoOAuthTokens.RefreshToken);
                    writer.WriteLine("expirytime=" + zohoOAuthTokens.ExpiryTime);
                }
            }catch(Exception e) when(e is UnauthorizedAccessException || e is DirectoryNotFoundException)
            {
                ZCRMLogger.LogError("Exception while inserting tokens to config file " + e);
                throw new ZohoOAuthException(e);
            }
        }

        public static string GetPersistenceHandlerFilePath()
        {
            return ZohoOAuth.GetConfigValue("oauth_tokens_file_path");
        }
    }
}
