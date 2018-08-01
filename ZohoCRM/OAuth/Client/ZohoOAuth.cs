using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.OAuth.Common;
using ZCRMSDK.OAuth.Contract;

namespace ZCRMSDK.OAuth.Client
{
    public class ZohoOAuth
    {

        //TODO: Logger Decleration. Inspect the usage of it;

        private static Dictionary<string, string> configProperties = new Dictionary<string, string>();

        internal static List<string> OAUTH_CONFIG_KEYS = new List<string>{ "client_id", "client_secret", "redirect_uri", "scope", "access_type", "persistence_handler_class", "mysql_username", "mysql_password", "oauth_tokens_file_path"}; 
        public static Dictionary<string, string> ConfigProperties { get => configProperties; private set => configProperties = value; }

       
        public static void Initialize(string domainSuffix, Stream inputStream, Dictionary<string, string> configData)
        {
            try
            {
                SetIAMUrl(domainSuffix);
                AddConfigurationData("oauth_configuration");

                if (inputStream != null)
                {
                    AddConfigurationData(inputStream);
                }

                if (configData != null)
                {
                    AddConfigurationData(configData);
                }

                ZohoOAuthParams oAuthParams = new ZohoOAuthParams()
                {
                    ClientId = GetConfigValue(ZohoOAuthConstants.CLIENT_ID),
                    ClientSecret = GetConfigValue(ZohoOAuthConstants.CLIENT_SECRET),
                    AccessType = GetConfigValue(ZohoOAuthConstants.ACCESS_TYPE),
                    RedirectURL = GetConfigValue(ZohoOAuthConstants.REDIRECT_URL),
                    Scopes = GetConfigValue(ZohoOAuthConstants.SCOPES)
                };

                ZohoOAuthClient.GetInstance(oAuthParams);
                ZCRMLogger.LogInfo("Zoho OAuth Client Library configuration properties: "+ CommonUtil.DictToString(ConfigProperties));
            }
            catch(KeyNotFoundException e)
            {
                 ZCRMLogger.LogError("Exception while initializing Zoho OAuth Client .. Essential configuration data not found");
                 throw new ZohoOAuthException(e);
            }
        }

        private static void SetIAMUrl(string domainSuffix)
        {
            domainSuffix = domainSuffix ?? "com";
            switch (domainSuffix)
            {
                case "eu":
                    configProperties.Add(ZohoOAuthConstants.IAM_URL, "https://accounts.zoho.eu");
                    break;
                case "cn":
                    configProperties.Add(ZohoOAuthConstants.IAM_URL, "https://accounts.zoho.com.cn");
                    break;
                default:
                    configProperties.Add(ZohoOAuthConstants.IAM_URL, "https://accounts.zoho.com");
                    break;
            }
        }

        //Adds Configuration key value pairs specified by the argument to configProperties;
        private static void AddConfigurationData(Stream inputStream)
        {
            Dictionary<string, string> keyValuePairs;
            keyValuePairs = ZohoOAuthUtil.GetFileAsDict(inputStream);
            foreach (KeyValuePair<string, string> keyValues in keyValuePairs)
            {
                if (OAUTH_CONFIG_KEYS.Contains(keyValues.Key))
                {
                    ConfigProperties[keyValues.Key] = keyValues.Value;
                }
            }
        }

        private static void AddConfigurationData(Dictionary<string, string> configData)
        {
            foreach (KeyValuePair<string, string> keyValues in configData)
            {
                if (OAUTH_CONFIG_KEYS.Contains(keyValues.Key))
                {
                    ConfigProperties[keyValues.Key] = keyValues.Value;
                }
            }
        }

        private static void AddConfigurationData(string sectionName)
        {
            Dictionary<string, string> keyValuePairs = ZohoOAuthUtil.GetConfigFileAsDict(sectionName);
            foreach(KeyValuePair<string, string> configData in keyValuePairs)
            {
                if (OAUTH_CONFIG_KEYS.Contains(configData.Key))
                {
                    ConfigProperties[configData.Key] = configData.Value;
                }
            }
        }

        //AddConfigurationData overloading ends!.

        public static string GetConfigValue(string key){
            return configProperties[key];
        }

        public static string GetIAMUrl()
        {
            return GetConfigValue(ZohoOAuthConstants.IAM_URL);
        }

        public static string GetLoginWithZohoURL()
        {
            return GetIAMUrl() + "/oauth/v2/auth?"+ZohoOAuthConstants.SCOPES+"=" + GetCRMScope() + "&"+ZohoOAuthConstants.CLIENT_ID+"=" + GetClientID() + "&"+ZohoOAuthConstants.CLIENT_SECRET+"=" + GetClientSecret() + "&"+ZohoOAuthConstants.RESPONSE_TYPE+"="+ZohoOAuthConstants.RESPONSE_TYPE_CODE+"&"+ZohoOAuthConstants.ACCESS_TYPE+"=" + GetAccessType() + "&"+ZohoOAuthConstants.REDIRECT_URL+"=" + GetRedirectURL();
        }

        public static string GetTokenURL()
        {
            return GetIAMUrl() + "/oauth/v2/"+ZohoOAuthConstants.TOKEN;
        }

        public static string GetRefreshTokenURL()
        {
            return GetIAMUrl() + "/oauth/v2/"+ZohoOAuthConstants.TOKEN;
        }

        public static string GetUserInfoURL()
        {
            return GetIAMUrl() + "/oauth/user/info";
        }

        public static string GetRevokeTokenURL()
        {
            return GetIAMUrl() + "/oauth/v2/token/revoke";
        }

        public static string GetCRMScope()
        {
            return GetConfigValue(ZohoOAuthConstants.SCOPES);
        }

        public static string GetClientID()
        {
            return GetConfigValue(ZohoOAuthConstants.CLIENT_ID);
        }

        public static string GetClientSecret()
        {
            return GetConfigValue(ZohoOAuthConstants.CLIENT_SECRET);
        }

        public static string GetRedirectURL()
        {
            return GetConfigValue(ZohoOAuthConstants.REDIRECT_URL);
        }

        public static string GetAccessType()
        {
            return GetConfigValue(ZohoOAuthConstants.ACCESS_TYPE);
        }


        public static IZohoPersistenceHandler GetPersistenceHandlerInstance()
        {
            try
            {
                string classAssemblyName;
                try
                {
                    classAssemblyName = GetConfigValue(ZohoOAuthConstants.PERSISTENCE_HANDLER_CLASS);
                }
                catch (KeyNotFoundException) {
                    ZCRMLogger.LogInfo("Persistence Handler not specified. Hence using the default implementation.");
                    classAssemblyName = ZohoOAuthConstants.DEFAULT_PERSISTENCE_HANDLER;
                }
                return (IZohoPersistenceHandler)Activator.CreateInstance(Type.GetType(classAssemblyName));
            }
            catch (Exception e) when(e is ArgumentNullException){
                ZCRMLogger.LogError(e);
                throw new ZohoOAuthException(e);
            }
        }

    }
}
