using System;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;
using System.Collections.Generic;

namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public class ModuleAPIHandler : CommonAPIHandler, IAPIHandler
    {
        private ZCRMModule module;

        private ModuleAPIHandler(ZCRMModule zcrmModule)
        {
            module = zcrmModule;
        }

        public static ModuleAPIHandler GetInstance(ZCRMModule zcrmModule)
        {
            return new ModuleAPIHandler(zcrmModule);
        }


        public void GetModuleDetails()
        {
            module = (ZCRMModule)MetaDataAPIHandler.GetInstance().GetModule(module.ApiName).Data;
        }

        public APIResponse GetLayoutDetails(long layoutId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/layouts/" + layoutId;
                requestQueryParams.Add("module", module.ApiName);

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JObject responseJSON = response.ResponseJSON;
                JArray layoutsArray = (JArray)responseJSON["layouts"];
                response.Data = GetZCRMLayout((JObject)layoutsArray[0]);
                return response;
            }
            catch (Exception e) when(!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMLayout> GetAllLayouts(string modifiedSince)
        {
            try{

                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/layouts";
                requestQueryParams.Add("module", module.ApiName);
                if (modifiedSince != null && modifiedSince != "")
                {
                    requestHeaders.Add("If-Modified-Since", modifiedSince);
                }

                BulkAPIResponse<ZCRMLayout> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMLayout>();

                response.BulkData = GetAllLayouts(response.ResponseJSON);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMField> GetAllFields(string modifiedSince)
        {
            try
            {

                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/fields";
                requestQueryParams.Add("module", module.ApiName);
                if (modifiedSince != null && modifiedSince != "")
                {
                    requestHeaders.Add("If-Modified-Since", modifiedSince);
                }

                BulkAPIResponse<ZCRMField> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMField>();

                response.BulkData = GetAllFields(response.ResponseJSON);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }

        }

        public BulkAPIResponse<ZCRMCustomView> GetAllCustomViews(string modifiedSince)
        {
            try
            {

                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/custom_views";
                requestQueryParams.Add("module", module.ApiName);
                requestHeaders.Add("If-Modified-Since", modifiedSince);

                BulkAPIResponse<ZCRMCustomView> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMCustomView>();

                response.BulkData = GetAllCustomViews(response.ResponseJSON);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }

        }

        public APIResponse GetCustomView(long cvId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/custom_views/" + cvId;
                requestQueryParams.Add("module", module.ApiName);

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JObject responseJSON = response.ResponseJSON;
                JArray layoutsArray = (JArray)responseJSON["custom_views"];
                response.Data = GetZCRMCustomView((JObject)layoutsArray[0]);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMModuleRelation> GetAllRelatedLists()
        {
            try
            {

                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/related_lists";
                requestQueryParams.Add("module", module.ApiName);

                BulkAPIResponse<ZCRMModuleRelation> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMModuleRelation>();

                response.BulkData = GetAllRelatedLists(response.ResponseJSON);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }

        }

        private ZCRMLayout GetZCRMLayout(JObject layoutDetails)
        {
            ZCRMLayout layout = ZCRMLayout.GetInstance(Convert.ToInt64(layoutDetails["id"]));
            layout.Name = Convert.ToString(layoutDetails["name"]);
            layout.Visible = Convert.ToBoolean(layoutDetails["visible"]);
            layout.Status = Convert.ToInt32(layoutDetails["status"]);
            if (layoutDetails["created_by"].HasValues)
            {
                JObject createdByObject = (JObject)layoutDetails["created_by"];
                ZCRMUser createdUser = ZCRMUser.GetInstance(Convert.ToInt64(createdByObject["id"]), Convert.ToString(createdByObject["name"]));
                layout.CreatedBy = createdUser;
                layout.CreatedTime = (string)layoutDetails["created_time"];
            }
            if (layoutDetails["modified_by"].HasValues)
            {
                JObject modifiedByObject = (JObject)layoutDetails["modified_by"];
                ZCRMUser modifiedUser = ZCRMUser.GetInstance(Convert.ToInt64(modifiedByObject["id"]), Convert.ToString(modifiedByObject["name"]));
                layout.ModifiedBy = modifiedUser;
                layout.ModifiedTime = (string)layoutDetails["modified_time"];
            }
            JArray accessibleProfilesArray = (JArray)layoutDetails["profiles"];
            foreach (JObject profileObject in accessibleProfilesArray)
            {
                ZCRMProfile profile = ZCRMProfile.GetInstance(Convert.ToInt64(profileObject["id"]), Convert.ToString(profileObject["name"]));
                layout.AddAccessibleProfiles(profile);
            }
            layout.Sections = GetAllSectionsofLayout(layoutDetails);
            return layout;
        }

        private ZCRMField GetZCRMField(JObject fieldJSON)
        {
            ZCRMField field = ZCRMField.GetInstance(Convert.ToString(fieldJSON["api_name"]));
            field.DefaultValue = (object)fieldJSON["default_value"];
            field.Mandatory = (bool?)fieldJSON["required"];
            field.Id = Convert.ToInt64(fieldJSON["id"]);
            field.CustomField = Convert.ToBoolean(fieldJSON["custom_field"]);
            field.DataType = Convert.ToString(fieldJSON["data_type"]);
            field.DisplayName = Convert.ToString(fieldJSON["field_label"]);
            field.MaxLength = (int?)fieldJSON["length"];
            field.Precision = (int?)fieldJSON["decimal_place"];
            field.ReadOnly = Convert.ToBoolean(fieldJSON["read_only"]);
            field.Visible = Convert.ToBoolean(fieldJSON["visible"]);
            field.SequenceNo = (int?)fieldJSON["sequence_number"];
            field.ToolTip = Convert.ToString(fieldJSON["tooltip"]);
            field.Webhook = Convert.ToBoolean(fieldJSON["webhook"]);
            field.CreatedSource = Convert.ToString(fieldJSON["created_source"]);
            JToken tempJSONObect = fieldJSON["formula"];
            if (tempJSONObect.HasValues)
            {
                field.FormulaReturnType = Convert.ToString(fieldJSON["return_type"]);
            }
            tempJSONObect = fieldJSON["currency"];
            if (tempJSONObect.HasValues)
            {
                field.Precision = (int?)fieldJSON["precision"];
            }
            JObject subLayouts = (JObject)fieldJSON["view_type"];
            if (subLayouts.Type != JTokenType.Null)
            {
                List<string> layoutsPresent = new List<string>();
                if ((bool)subLayouts["create"])
                {
                    layoutsPresent.Add("CREATE");
                }
                if ((bool)subLayouts["view"])
                {
                    layoutsPresent.Add("VIEW");
                }
                if ((bool)subLayouts["quick_create"])
                {
                    layoutsPresent.Add("QUICK_CREATE");
                }
                field.SubLayoutsPresent = layoutsPresent;
            }

            JArray pickList = (JArray)fieldJSON["pick_list_values"];
            foreach (JObject pickListObject in pickList)
            {
                ZCRMPickListValue pickListValue = ZCRMPickListValue.GetInstance();
                pickListValue.DisplayName = (string)fieldJSON["display_value"];
                pickListValue.ActualName = (string)fieldJSON["actual_value"];
                pickListValue.SequenceNumber = Convert.ToInt32(fieldJSON["sequence_number"]);
                pickListValue.Maps = (JArray)fieldJSON["maps"];
                field.AddPickListValue(pickListValue);
            }
            JObject lookup = (JObject)fieldJSON["lookup"];
            foreach (KeyValuePair<string, JToken> lookupObject in lookup)
            {
                field.SetLookupDetails(lookupObject.Key, (object)lookupObject.Value);
            }
            JObject multilookup = (JObject)fieldJSON["multiselectlookup"];
            foreach (KeyValuePair<string, JToken> multiLookupObject in multilookup)
            {
                field.SetMultiselectLookup(multiLookupObject.Key, (object)multiLookupObject.Value);
            }
            if (fieldJSON.ContainsKey("subformtabId") && fieldJSON["subformtabId"].Type != JTokenType.Null)
            {
                field.SubFormTabId = Convert.ToInt64(fieldJSON["subformtabid"]);
            }
            if (fieldJSON.ContainsKey("subform") && fieldJSON["subform"].Type != JTokenType.Null)
            {
                JObject subformDetails = (JObject)fieldJSON["subform"];
                foreach (KeyValuePair<string, JToken> subformDetail in subformDetails)
                {
                    field.SetSubformDetails(subformDetail.Key, (object)subformDetail.Value);
                }
            }
            return field;
        }

        private ZCRMSection GetZCRMSection(JObject sectionJSON)
        {
            ZCRMSection section = ZCRMSection.GetInstance(Convert.ToString(sectionJSON["name"]));
            section.ColumnCount = Convert.ToInt32(sectionJSON["column_count"]);
            section.DisplayName = (string)sectionJSON["display_label"];
            section.Sequence = Convert.ToInt32(sectionJSON["sequence_number"]);
            section.Fields = GetAllFields(sectionJSON);
            return section;
        }

        private ZCRMCustomView GetZCRMCustomView(JObject customViewObject)
        {
            ZCRMCustomView customView = ZCRMCustomView.GetInstance(module.ApiName, Convert.ToInt64(customViewObject["id"]));
            customView.DisplayName = (string)customViewObject["display_value"];
            customView.Isdefault = (bool)customViewObject["default"];
            customView.SystemName = (string)customViewObject["system_name"];
            customView.Category = (string)customViewObject["category"];
            if (customViewObject["favorite"].Type != JTokenType.Null)
            {
                customView.Favourite = Convert.ToInt32(customViewObject["favourite"]);
            }
            customView.Name = (string)customViewObject["name"];
            customView.SortBy = (string)customViewObject["sort_by"];
            if (customViewObject.ContainsKey("sort_order") && customViewObject["sort_order"].Type != JTokenType.Null)
            {
                customView.SortOrder = (CommonUtil.SortOrder)Enum.Parse(typeof(CommonUtil.SortOrder), (string)customViewObject["sort_order"]);
            }
            List<string> fields = new List<string>();
            if (customViewObject.ContainsKey("fields") && customViewObject["fields"].Type != JTokenType.Null)
            {
                JArray fieldsArray = (JArray)customViewObject["fields"];
                foreach (string fieldObject in fieldsArray)
                {
                    fields.Add(fieldObject);
                }
            }
            customView.Fields = fields;
            return customView;
        }

        private ZCRMModuleRelation GetZCRMModuleRelation(JObject relatedList)
        {
            ZCRMModuleRelation moduleRelation = ZCRMModuleRelation.GetInstance(module.ApiName, Convert.ToInt64(relatedList["id"]));
            moduleRelation.ApiName = (string)relatedList["api_name"];
            moduleRelation.Label = (string)relatedList["display_label"];
            moduleRelation.Module = (string)relatedList["module"];
            moduleRelation.Type = (string)relatedList["type"];
            return moduleRelation;
        }

        private List<ZCRMSection> GetAllSectionsofLayout(JObject layoutDetails)
        {
            List<ZCRMSection> sections = new List<ZCRMSection>();
            if (layoutDetails.ContainsKey("sections"))
            {
                JArray sectionsArray = (JArray)layoutDetails["sections"];
                foreach (JObject section in sectionsArray)
                {
                    sections.Add(GetZCRMSection(section));
                }
            }
            return sections;
        }

        public List<ZCRMLayout> GetAllLayouts(JObject layoutsJSONObject)
        {
            List<ZCRMLayout> allLayouts = new List<ZCRMLayout>();
            if (layoutsJSONObject.ContainsKey("layouts"))
            {
                JArray layoutsArray = (JArray)layoutsJSONObject["layouts"];
                foreach (JObject layoutObject in layoutsArray)
                {
                    allLayouts.Add(GetZCRMLayout(layoutObject));
                }
            }
            return allLayouts;
        }

        private List<ZCRMField> GetAllFields(JObject sectionJSON)
        {
            List<ZCRMField> fields = new List<ZCRMField>();
            if (sectionJSON.ContainsKey("fields") && sectionJSON["fields"].Type != JTokenType.Null)
            {
                JArray fieldsArray = (JArray)sectionJSON["fields"];
                foreach (JObject fieldObject in fieldsArray)
                {
                    fields.Add(GetZCRMField(fieldObject));
                }
            }
            return fields;
        }

        private List<ZCRMCustomView> GetAllCustomViews(JObject customviewJSON)
        {
            List<ZCRMCustomView> allCustomViews = new List<ZCRMCustomView>();
            if (customviewJSON.ContainsKey("custom_views") && customviewJSON["custom_views"].Type != JTokenType.Null)
            {
                JArray customViewsArray = (JArray)customviewJSON["custom_views"];
                foreach (JObject customViewObject in customViewsArray)
                {
                    allCustomViews.Add(GetZCRMCustomView(customViewObject));
                }
            }
            return allCustomViews;
        }

        private List<ZCRMModuleRelation> GetAllRelatedLists(JObject responseJSON)
        {
                List<ZCRMModuleRelation> relatedLists = new List<ZCRMModuleRelation>();
            if (responseJSON.ContainsKey("related_lists") && responseJSON.Type != JTokenType.Null)
                {
                    JArray relatedListArray = (JArray)responseJSON["related_lists"];
                    foreach (JObject relatedList in relatedListArray)
                    {
                        relatedLists.Add(GetZCRMModuleRelation(relatedList));
                    }
                }
                return relatedLists;
        }

    }
}
