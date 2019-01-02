using System;
using System.IO;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Setup.RestClient;
using ZCRMSDK.OAuth.Client;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Api;
using System.Reflection;
using ZCRMSDK.OAuth.Contract;
using ZCRMSDK.OAuth.Common;
using System.Threading;
using System.Diagnostics;

namespace ZCRMSDK.CRM.Library.Common
{
    public class ZCRMConfigUtil
    {
        private static Boolean handleAuthentication;

        public static Boolean HandleAuthentication { get => handleAuthentication; private set => handleAuthentication = value; }

        public static Dictionary<string, string> ConfigProperties { get; private set; } = new Dictionary<string, string>();

        public static Dictionary<string, ZohoOAuthParams> LoggedUsers = new Dictionary<string, ZohoOAuthParams>();

        public static void InitializeMobile(bool initOAuth)
        {
            Initialize(initOAuth, null, null);
            HandleAuthentication = true;
        } 

        public static void CustomInitialize(Stream inputStream)
        {
            Initialize(true, inputStream, null);
        }

        public static void CustomInitialize(Dictionary<string, string> configDict)
        {
            Initialize(true, null, configDict);
        }


        public static void Initialize(bool initOAuth, Stream configStream, Dictionary<string, string> configData) {

            Assembly assembly = Assembly.GetAssembly(typeof(ZCRMConfigUtil));
            ConfigProperties = CommonUtil.GetFileAsDict(assembly.GetManifestResourceStream(assembly.GetName().Name + ".Resources.configuration.txt"));
            Dictionary<string, string> keyValuePairs = CommonUtil.GetConfigFileAsDict("zcrm_configuration");

            foreach (KeyValuePair<string, string> keyValues in keyValuePairs)
            {
                ConfigProperties[keyValues.Key] = keyValues.Value;
            }

            if(configStream != null)
            {
                configData = CommonUtil.GetFileAsDict(configStream);
                configStream = null;
            }
            if(configData != null)
            {
                AddConfigurationData(configData);
            }
            ZCRMLogger.Init();

            if (initOAuth)
            {
                HandleAuthentication = true;
                try
                {
                    ZohoOAuth.Initialize(ConfigProperties.ContainsKey(APIConstants.DOMAIN_SUFFIX) ? (string)ConfigProperties[APIConstants.DOMAIN_SUFFIX] : null, configData);
                    if(ConfigProperties.ContainsKey(APIConstants.DOMAIN_SUFFIX))
                    {
                        SetAPIBaseUrl(ConfigProperties[APIConstants.DOMAIN_SUFFIX]);
                    }
                }
                catch (Exception e)
                {
                    throw new ZCRMException(e);
                }
            }
            ZCRMLogger.LogInfo("C# Client Library Configuration Properties : " + CommonUtil.DictToString(ConfigProperties));
        }

        private static void AddConfigurationData(Dictionary<string, string> configDict)
        {
            foreach (KeyValuePair<string, string> data in configDict)
            {
                if (!ZohoOAuth.OAUTH_CONFIG_KEYS.Contains(data.Key))
                {
                    if(!string.IsNullOrEmpty(configDict[data.Key]) && !string.IsNullOrWhiteSpace(configDict[data.Key]))
                    {
                        ConfigProperties[data.Key] = data.Value;
                    }
                }
            }
        }

        private static void SetAPIBaseUrl(string domainSuffix)
        {
            domainSuffix = domainSuffix ?? "com";
            switch (domainSuffix)
            {
                case "eu":
                    ConfigProperties[APIConstants.APIBASEURL] = "https://www.zohoapis.eu";
                    break;
                case "cn":
                    ConfigProperties[APIConstants.APIBASEURL] = "https://www.zohoapis.com.cn";
                    break;
                default:
                    ConfigProperties[APIConstants.APIBASEURL] = "https://www.zohoapis.com";
                    break;
            }
        }        

        public static string GetAccessToken()
        {
            string userMailId = ZCRMRestClient.GetCurrentUserEmail();

            if ((userMailId == null) && (!(ConfigProperties.ContainsKey("currentUserEmail")) || (ConfigProperties["currentUserEmail"] == null)))
            {
                throw new ZCRMException("Current user must be either set in ZCRMRestClient or zcrm_configuration section in zoho_configuration.config");
            }
            if (userMailId == null)
            {
                userMailId = ConfigProperties["currentUserEmail"];
            }
            ZohoOAuthClient client = ZohoOAuthClient.GetInstance();
            return client.GetAccessToken(userMailId);
        }

        public static void SetConfigValue(string config, string value){
            ConfigProperties.Add(config, value);
        }

        public static string GetConfigValue(string config)
        {
            if(ConfigProperties.ContainsKey(config))
            {
                return (string)ConfigProperties[config];   
            }
            return null;
        }

        public static string GetApiBaseURL(){
            return GetConfigValue("apiBaseUrl");
        }

        public static string GetApiVersion(){
            return GetConfigValue("apiVersion");
        }

        public static string GetAuthenticationClass(){
            return GetConfigValue("loginAuthClass");
        }

        public static string GetPhotoUrl(){
            return GetConfigValue("photoUrl");
        }
    }

}
