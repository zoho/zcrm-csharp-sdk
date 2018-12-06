using System.Threading;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Api.Handler;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.MetaData;
using System.IO;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.OAuth.Client;
using ZCRMSDK.CRM.Library.Api;
using ZCRMSDK.OAuth.Common;
using ZCRMSDK.OAuth.Contract;
using System;

namespace ZCRMSDK.CRM.Library.Setup.Restclient
{
    public class ZCRMRestClient
    {

        private static readonly ThreadLocal<string> CURRENT_USER_EMAIL = new ThreadLocal<string>();

        public static readonly ThreadLocal<Dictionary<string, string>> DYNAMIC_HEADERS = new ThreadLocal<Dictionary<string, string>>();
        private static Dictionary<string, string> staticHeaders = new Dictionary<string, string>();

        public static Dictionary<string, string> StaticHeaders { get => staticHeaders; set => staticHeaders = value; }


        public static ZCRMRestClient GetInstance()
        {
            return new ZCRMRestClient();
        }

        public static void Initialize()
        {
            Initialize(true);
        }

        public static void Initialize(bool handleAuthentication)
        {
            ZCRMConfigUtil.Initialize(handleAuthentication, null, null);
        }

        public static void Initialize(Stream inputStream)
        {
            ZCRMConfigUtil.CustomInitialize(inputStream);
        }

        public static void Initialize(Dictionary<string, string> oauthConfig)
        {
            ZCRMConfigUtil.CustomInitialize(oauthConfig);
        }

        public static void SetCurrentUser(string userEmail)
        {
            CURRENT_USER_EMAIL.Value = userEmail;
        }

        public static string GetCurrentUserEmail()
        {
            return CURRENT_USER_EMAIL.Value;
        }

        public static void SetDynamicHeaders(Dictionary<string, string> headers)
        {
            DYNAMIC_HEADERS.Value = headers;
        } 

        public static Dictionary<string, string> GetDynamicHeaders()
        {
            return DYNAMIC_HEADERS.Value;
        }


        public ZCRMOrganization GetOrganizationInstance()
        {
            return ZCRMOrganization.GetInstance();
        }


        public APIResponse GetOrganizationDetails()
        {
            return OrganizationAPIHandler.GetInstance().GetOrganizationDetails();
        }


        public APIResponse GetCurrentUser()
        {
            return OrganizationAPIHandler.GetInstance().GetCurrentUser();
        }

        public ZCRMModule GetModuleInstance(string moduleAPIName)
        {
            return ZCRMModule.GetInstance(moduleAPIName);
        }


        public ZCRMRecord GetRecordInstance(string moduleAPIName, long entityId)
        {
            return ZCRMRecord.GetInstance(moduleAPIName, entityId);
        }

        public BulkAPIResponse<ZCRMModule> GetAllModules()
        {
            return GetAllModules(null);
        }

        public BulkAPIResponse<ZCRMModule> GetAllModules(string modifiedSince)
        {
            return MetaDataAPIHandler.GetInstance().GetAllModules(modifiedSince);
        }


        public APIResponse GetModule(string moduleName)
        {
            return MetaDataAPIHandler.GetInstance().GetModule(moduleName);
        }
    }
}
