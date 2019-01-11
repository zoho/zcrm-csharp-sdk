using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public class MetaDataAPIHandler : CommonAPIHandler, IAPIHandler
    {

        private MetaDataAPIHandler() { }

        public static MetaDataAPIHandler GetInstance()
        {
            return new MetaDataAPIHandler();
        }


        public BulkAPIResponse<ZCRMModule> GetAllModules(string modifiedSince)
        {
            requestMethod = APIConstants.RequestMethod.GET;
            urlPath = "settings/modules";
            if(modifiedSince != null && modifiedSince != "")
            {
                requestHeaders.Add("If-Modified-Since", modifiedSince);   
            }

            BulkAPIResponse<ZCRMModule> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMModule>();

            JObject responseJSON = response.ResponseJSON;
            List<ZCRMModule> allModules = new List<ZCRMModule>();

            if(responseJSON.ContainsKey("modules"))
            {
                JArray modulesArray = (JArray)responseJSON["modules"];
                foreach (JObject moduleDetails in modulesArray)
                {
                    allModules.Add(GetZCRMModule(moduleDetails));
                }
                response.BulkData = allModules;
            }
            return response;
        }


        public APIResponse GetModule(string apiName)
        {
            requestMethod = APIConstants.RequestMethod.GET;
            urlPath = "settings/modules/" + apiName;

            APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

            JArray modulesArray = (JArray)response.ResponseJSON["modules"];
            JObject moduleDetails = (JObject)modulesArray[0];
            response.Data = GetZCRMModule(moduleDetails);

            return response;
        }


        private ZCRMModule GetZCRMModule(JObject moduleDetails) 
        {
            try
            {
                ZCRMModule module = ZCRMModule.GetInstance((string)moduleDetails["api_name"]);
                module.Id = Convert.ToInt64(moduleDetails["id"]);
                module.SystemName = (string)moduleDetails["module_name"];
                module.SingularLabel = (string)moduleDetails["singular_label"];
                module.PluralLabel = (string)moduleDetails["plural_label"];
                module.Creatable = (bool)moduleDetails["creatable"];
                module.Viewable = (bool)moduleDetails["viewable"];
                module.Editable = (bool)moduleDetails["editable"];
                module.Convertable = (bool)moduleDetails["convertable"];
                module.Deletable = (bool)moduleDetails["deletable"];
                module.CustomModule = (bool)(moduleDetails["generated_type"].ToString().Equals("custom"));
                module.ApiSupported = (bool)(moduleDetails["api_supported"]);
                JArray accessibleProfilesArray = (JArray)moduleDetails["profiles"];
                foreach (JObject accessibleProfiles in accessibleProfilesArray)
                {
                    ZCRMProfile profile = ZCRMProfile.GetInstance(Convert.ToInt64(accessibleProfiles["id"]), (string)accessibleProfiles["name"]);
                    module.AddAccessibleProfile(profile);
                }
                if (moduleDetails["modified_by"].HasValues)
                {
                    JObject modifiedByObject = (JObject)moduleDetails["modified_by"];
                    ZCRMUser modifiedUser = ZCRMUser.GetInstance(Convert.ToInt64(modifiedByObject["id"]), (string)modifiedByObject["name"]);
                    module.ModifiedBy = modifiedUser;
                    module.ModifiedTime = (string)moduleDetails["modified_time"];
                }
                if (moduleDetails.ContainsKey("related_lists") && moduleDetails["related_lists"].Type != JTokenType.Null)
                {
                    List<ZCRMModuleRelation> relatedLists = new List<ZCRMModuleRelation>();
                    JArray relatedListsArray = (JArray)moduleDetails["related_lists"];
                    foreach (JObject relatedListDetails in relatedListsArray)
                    {
                        ZCRMModuleRelation relatedList = ZCRMModuleRelation.GetInstance(module.ApiName, (string)relatedListDetails["api_name"]);
                        SetRelatedListProperties(relatedList, relatedListDetails);
                        relatedLists.Add(relatedList);
                    }
                    module.RelatedLists = relatedLists;
                }
                if (moduleDetails.ContainsKey("business_card_fields") && moduleDetails.Type != JTokenType.Null)
                {
                    List<string> bcFields = new List<string>();
                    JArray bcFieldsArray = (JArray)moduleDetails["business_card_fields"];
                    foreach (JObject bcField in bcFieldsArray)
                    {
                        bcFields.Add(bcField.ToString());
                    }
                    module.BussinessCardFields = bcFields;
                }
                if (moduleDetails.ContainsKey("layouts"))
                {
                    module.Layouts = ModuleAPIHandler.GetInstance(module).GetAllLayouts(moduleDetails);
                }
                return module;

            }
            catch (Exception e) when(!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        private void SetRelatedListProperties(ZCRMModuleRelation relatedList, JObject relatedListDetails)
        {
            relatedList.Id = Convert.ToInt64(relatedListDetails["id"]);
            relatedList.Visible = (bool)relatedListDetails["visible"];
            relatedList.Label = (string)relatedListDetails["display_label"];
        }
    }
}
