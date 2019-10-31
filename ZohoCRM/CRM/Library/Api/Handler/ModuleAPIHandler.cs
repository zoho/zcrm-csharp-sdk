using System;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public class ModuleAPIHandler : CommonAPIHandler, IAPIHandler
    {
        private ZCRMModule module;
        private int index;

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
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMLayout> GetAllLayouts(string modifiedSince)
        {
            try
            {

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

        public APIResponse GetField(long fieldId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/fields/" + fieldId;
                requestQueryParams.Add("module", module.ApiName);

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JObject responseJSON = response.ResponseJSON;
                JArray fieldArray = (JArray)responseJSON["fields"];
                response.Data = GetZCRMField((JObject)fieldArray[0]);
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
                response.Data = GetZCRMCustomView((JObject)layoutsArray[0], (JObject)responseJSON["info"]["translation"]);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse GetRelatedList(long relatedListId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/related_lists/" + relatedListId;
                requestQueryParams.Add("module", module.ApiName);

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JObject responseJSON = response.ResponseJSON;
                JArray relatedListArray = (JArray)responseJSON["related_lists"];
                response.Data = GetZCRMModuleRelation((JObject)relatedListArray[0]);
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
                layout.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(layoutDetails["created_time"]));
            }
            if (layoutDetails["modified_by"].HasValues)
            {
                JObject modifiedByObject = (JObject)layoutDetails["modified_by"];
                ZCRMUser modifiedUser = ZCRMUser.GetInstance(Convert.ToInt64(modifiedByObject["id"]), Convert.ToString(modifiedByObject["name"]));
                layout.ModifiedBy = modifiedUser;
                layout.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(layoutDetails["modified_time"]));
            }
            JArray accessibleProfilesArray = (JArray)layoutDetails["profiles"];
            foreach (JObject profileObject in accessibleProfilesArray)
            {
                ZCRMProfile profile = ZCRMProfile.GetInstance(Convert.ToInt64(profileObject["id"]), Convert.ToString(profileObject["name"]));
                if (profileObject.ContainsKey("default") && profileObject["default"].Type != JTokenType.Null)
                {
                    profile.IsDefault = Convert.ToBoolean(profileObject["default"]);
                }
                layout.AddAccessibleProfiles(profile);
            }
            layout.Sections = GetAllSectionsofLayout(layoutDetails);

            if (layoutDetails.ContainsKey("convert_mapping") && layoutDetails["convert_mapping"].Type != JTokenType.Null)
            {
                List<string> convertModules = new List<string>() { "Contacts", "Deals", "Accounts" };
                Dictionary<string, ZCRMLeadConvertMapping> convertMapDic = new Dictionary<string, ZCRMLeadConvertMapping>();
                foreach (string convertModule in convertModules)
                {
                    if (((JObject)layoutDetails["convert_mapping"]).ContainsKey(convertModule) && ((JObject)layoutDetails["convert_mapping"])[convertModule].Type != JTokenType.Null)
                    {
                        JObject contactsMap = (JObject)layoutDetails["convert_mapping"][convertModule];
                        ZCRMLeadConvertMapping convertMapIns = ZCRMLeadConvertMapping.GetInstance(contactsMap["name"].ToString(), Convert.ToInt64(contactsMap["id"]));
                        if (contactsMap.ContainsKey("fields") && contactsMap["fields"].Type != JTokenType.Null)
                        {
                            List<ZCRMLeadConvertMappingField> ConvertMappingFields = new List<ZCRMLeadConvertMappingField>();
                            JArray fields = (JArray)contactsMap["fields"];
                            foreach (JObject field in fields)
                            {
                                ZCRMLeadConvertMappingField convertMappingFieldIns = ZCRMLeadConvertMappingField.GetInstance(field["api_name"].ToString(), Convert.ToInt64(field["id"]));
                                convertMappingFieldIns.FieldLabel = field["field_label"].ToString();
                                convertMappingFieldIns.Required = Convert.ToBoolean(field["required"]);
                                ConvertMappingFields.Add(convertMappingFieldIns);
                            }
                            convertMapIns.Fields = ConvertMappingFields;
                        }
                        convertMapDic.Add(convertModule, convertMapIns);
                    }
                }
                layout.ConvertMapping = convertMapDic;
            }
            return layout;
        }

        private ZCRMField GetZCRMField(JObject fieldJSON)
        {
            ZCRMField field = ZCRMField.GetInstance(Convert.ToString(fieldJSON["api_name"]));
            field.Mandatory = (bool?)fieldJSON["system_mandatory"];
            field.Webhook = (bool)(fieldJSON["webhook"]);
            field.JsonType = (string)(fieldJSON["json_type"]);
            if (fieldJSON["crypt"].HasValues)
            {
                foreach (KeyValuePair<string, JToken> crypt in (JObject)fieldJSON["crypt"])
                {
                    field.SetEncrypt(crypt.Key, Convert.ToString(crypt.Value));
                }
            }
            field.DisplayName = (string)(fieldJSON["field_label"]);
            field.ToolTip = (string)(fieldJSON["tooltip"]);
            field.CreatedSource = (string)(fieldJSON["created_source"]);
            field.FieldReadOnly = (bool)(fieldJSON["field_read_only"]);
            field.DisplayLabel = (string)(fieldJSON["display_label"]);
            field.ReadOnly = (bool)(fieldJSON["read_only"]);
            if (fieldJSON["association_details"].HasValues)
            {
                foreach (KeyValuePair<string, JToken> association_details in (JObject)fieldJSON["association_details"])
                {
                    if (association_details.Key.Equals("lookup_field"))
                    {
                        JObject data = (JObject)association_details.Value;
                        ZCRMField field_obj = ZCRMField.GetInstance(Convert.ToString(data["api_name"]));
                        field_obj.Id = Convert.ToInt64(data["id"]);
                        field.SetAssociationDetails(association_details.Key, field_obj);
                    }
                    if (association_details.Key.Equals("related_field"))
                    {
                        JObject data = (JObject)association_details.Value;
                        ZCRMField field_obj = ZCRMField.GetInstance(Convert.ToString(data["api_name"]));
                        field_obj.Id = Convert.ToInt64(data["id"]);
                        field.SetAssociationDetails(association_details.Key, field_obj);
                    }
                }
            }
            if(fieldJSON.ContainsKey("businesscard_supported"))
            {
                field.BusinesscardSupported = (bool)(fieldJSON["businesscard_supported"]);
            }
            if (fieldJSON["currency"].HasValues)
            {
                JObject tempJSONObject = (JObject)fieldJSON["currency"];
                if (tempJSONObject.HasValues)
                {
                    if (tempJSONObject.ContainsKey("precision"))
                    {
                        field.Precision = (int?)tempJSONObject["precision"];
                    }
                    if (tempJSONObject.ContainsKey("rounding_option"))
                    {
                        field.RoundingOption = Convert.ToString(tempJSONObject["rounding_option"]);
                    }
                }
            }
            field.Id = (long)(fieldJSON["id"]);
            field.CustomField = (bool)(fieldJSON["custom_field"]);
            if (fieldJSON["lookup"].HasValues)
            {
                JObject lookup = (JObject)fieldJSON["lookup"];
                foreach (KeyValuePair<string, JToken> lookupObject in lookup)
                {
                    field.SetLookupDetails(lookupObject.Key, (object)lookupObject.Value);
                }
            }
            field.Visible = (bool)(fieldJSON["visible"]);
            field.MaxLength = (int?)fieldJSON["length"];
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
                if ((bool)subLayouts["edit"])
                {
                    layoutsPresent.Add("EDIT");
                }
                field.SubLayoutsPresent = layoutsPresent;
            }
            if (fieldJSON["subform"].HasValues)
            {
                JObject subformDetails = (JObject)fieldJSON["subform"];
                foreach (KeyValuePair<string, JToken> subformDetail in subformDetails)
                {
                    field.SetSubformDetails(subformDetail.Key, (object)subformDetail.Value);
                }
            }
            if (fieldJSON["unique"].HasValues)
            {
                field.UniqueField = true;
                JObject casesensitive = (JObject)fieldJSON["unique"];
                if(casesensitive != null)
                {
                    field.CaseSensitive = (string)(casesensitive["casesensitive"]);
                }
            }
            if (fieldJSON.ContainsKey("history_tracking") && fieldJSON["history_tracking"].Type != JTokenType.Null)
            {
                field.HistoryTracking = (bool)(fieldJSON["history_tracking"]);
            }
            field.DataType = (string)(fieldJSON["data_type"]);
            if (fieldJSON["formula"].HasValues)
            {
                JObject tempJSONObject = (JObject)fieldJSON["formula"];
                if (tempJSONObject.HasValues)
                {
                    if (tempJSONObject.ContainsKey("return_type"))
                    {
                        field.FormulaReturnType = (string)(tempJSONObject["return_type"]);
                    }
                    if (tempJSONObject.ContainsKey("expression"))
                    {
                        field.FormulaExpression = (string)(tempJSONObject["expression"]);
                    }
                }
            }
            field.DecimalPlace = (int?)fieldJSON["decimal_place"];
            if(fieldJSON.ContainsKey("mass_update"))
            {
                field.MassUpdate = (bool)(fieldJSON["mass_update"]);
            }
            if (fieldJSON.ContainsKey("blueprint_supported") && fieldJSON["blueprint_supported"].Type != JTokenType.Null)
            {
                field.BluePrintSupported = (bool)(fieldJSON["blueprint_supported"]);
            }
            if (fieldJSON["pick_list_values"].HasValues)
            {
                JArray pickList = (JArray)fieldJSON["pick_list_values"];
                foreach (JObject pickListObject in pickList)
                {
                    ZCRMPickListValue pickListValue = ZCRMPickListValue.GetInstance();
                    pickListValue.DisplayName = (string)pickListObject["display_value"];
                    pickListValue.ActualName = (string)pickListObject["actual_value"];
                    pickListValue.SequenceNumber = Convert.ToInt32(pickListObject["sequence_number"]);
                    pickListValue.Maps = (JArray)pickListObject["maps"];
                    field.AddPickListValue(pickListValue);
                }
            }
            if (fieldJSON["multiselectlookup"].HasValues)
            {
                JObject multilookup = (JObject)fieldJSON["multiselectlookup"];
                foreach (KeyValuePair<string, JToken> multiLookupObject in multilookup)
                {
                    field.SetMultiselectLookup(multiLookupObject.Key, (object)multiLookupObject.Value);
                }
            }
            if (fieldJSON["auto_number"].HasValues)
            {
                field.AutoNumber = true;
                JObject auto_number = (JObject)fieldJSON["auto_number"];
                if (auto_number.ContainsKey("prefix") && auto_number["prefix"].Type != JTokenType.Null)
                {
                    field.Prefix = Convert.ToString(auto_number["prefix"]);
                }
                if (auto_number.ContainsKey("suffix") && auto_number["suffix"].Type != JTokenType.Null)
                {
                    field.Suffix = Convert.ToString(auto_number["suffix"]);
                }
                if (auto_number.ContainsKey("start_number") && auto_number["start_number"].Type != JTokenType.Null)
                {
                    field.StartNumber = Convert.ToInt32(auto_number["start_number"]);
                }
            }
            if (fieldJSON.ContainsKey("default_value") && fieldJSON["default_value"].Type != JTokenType.Null)
            {
                field.DefaultValue = (object)fieldJSON["default_value"];
            }
            if (fieldJSON.ContainsKey("sequence_number") && fieldJSON["sequence_number"].Type != JTokenType.Null)
            {
                field.SequenceNo = (int?)fieldJSON["sequence_number"];
            }
            if (fieldJSON.ContainsKey("subformtabId") && fieldJSON["subformtabId"].Type != JTokenType.Null)
            {
                field.SubFormTabId = Convert.ToInt64(fieldJSON["subformtabid"]);
            }
            return field;
        }

        private ZCRMSection GetZCRMSection(JObject sectionJSON)
        {
            ZCRMSection section = ZCRMSection.GetInstance(Convert.ToString(sectionJSON["name"]));
            section.ColumnCount = Convert.ToInt32(sectionJSON["column_count"]);
            section.DisplayName = (string)sectionJSON["display_label"];
            section.Sequence = Convert.ToInt32(sectionJSON["sequence_number"]);
            if (sectionJSON.ContainsKey("isSubformSection"))
            {
                section.IsSubformSection = Convert.ToBoolean(sectionJSON["isSubformSection"]);
            }
            if (sectionJSON.ContainsKey("tab_traversal"))
            {
                section.TabTraversal = Convert.ToInt32(sectionJSON["tab_traversal"]);
            }
            if (sectionJSON.ContainsKey("api_name"))
            {
                section.ApiName = Convert.ToString(sectionJSON["api_name"]);
            }
            if (sectionJSON.ContainsKey("properties") && sectionJSON["properties"].Type != JTokenType.Null)
            {
                section.Properties = (JObject)sectionJSON["properties"];
            }
            section.Fields = GetAllFields(sectionJSON);
            return section;
        }

        private ZCRMCustomView GetZCRMCustomView(JObject customViewObject, JObject categoriesArr)
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
            if (customViewObject.ContainsKey("shared_type") && customViewObject["shared_type"].Type != JTokenType.Null)
            {
                customView.SharedType = (string)customViewObject["shared_type"];
            }
            if (customViewObject.ContainsKey("shared_details") && customViewObject["shared_details"].Type != JTokenType.Null)
            {
                customView.SharedDetails = customViewObject["shared_details"].ToString();
            }
            if (customViewObject.ContainsKey("offline") && customViewObject["offline"].Type != JTokenType.Null)
            {
                customView.IsOffline = (bool)customViewObject["offline"];
            }
            if (customViewObject.ContainsKey("system_defined") && customViewObject["system_defined"].Type != JTokenType.Null)
            {
                customView.IsSystemDefined = (bool)customViewObject["system_defined"];
            }
            if (customViewObject.ContainsKey("criteria") && customViewObject["criteria"].Type != JTokenType.Null)
            {
                JObject jobj = (JObject)customViewObject["criteria"];
                index = 1;
                customView.Criteria = SetZCRMCriteriaObject(jobj);
                customView.CriteriaPattern = customView.Criteria.Pattern;
                customView.CriteriaCondition = customView.Criteria.Criteria;
            }

            if (categoriesArr.Count > 0)
            {
                List<ZCRMCustomViewCategory> categoryInstanceArray = new List<ZCRMCustomViewCategory>();
                foreach (KeyValuePair<string, JToken> token in categoriesArr)
                {
                    ZCRMCustomViewCategory customViewCategoryIns = ZCRMCustomViewCategory.GetInstance();
                    customViewCategoryIns.DisplayValue = token.Value.ToString();
                    customViewCategoryIns.ActualValue = token.Key;
                    categoryInstanceArray.Add(customViewCategoryIns);
                }
                customView.CategoriesList = categoryInstanceArray;
            }
            return customView;
        }

        private ZCRMCriteria SetZCRMCriteriaObject(JObject criteria)
        {
            ZCRMCriteria recordCriteria = ZCRMCriteria.GetInstance();
            if (criteria.ContainsKey("field") && criteria["field"].Type != JTokenType.Null)
            {
                recordCriteria.FieldAPIName = criteria["field"].ToString();
            }
            if (criteria.ContainsKey("comparator") && criteria["comparator"].Type != JTokenType.Null)
            {
                recordCriteria.Comparator = criteria["comparator"].ToString();
            }
            if (criteria.ContainsKey("value") && criteria["value"].Type != JTokenType.Null)
            {
                recordCriteria.Value = criteria["value"];
                recordCriteria.Index = index;
                recordCriteria.Pattern = Convert.ToString(index);
                index++;
                recordCriteria.Criteria = "(" + criteria["field"].ToString() + ":" + criteria["comparator"].ToString() + ":" + criteria["value"].ToString() + ")";
            }
            List<ZCRMCriteria> recordData = new List<ZCRMCriteria>();
            if (criteria.ContainsKey("group") && criteria["group"].Type != JTokenType.Null)
            {
                JArray jarr = (JArray)criteria["group"];
                foreach (JObject groupJSON in jarr)
                {
                    recordData.Add(SetZCRMCriteriaObject(groupJSON));
                }
                recordCriteria.Group = recordData;
            }
            if (criteria.ContainsKey("group_operator") && criteria["group_operator"].Type != JTokenType.Null)
            {
                string criteriavalue = "(", pattern = "(";
                recordCriteria.GroupOperator = criteria["group_operator"].ToString();
                int count = recordData.Count, i = 0;
                foreach (ZCRMCriteria criteriaObj in recordData)
                {
                    i++;
                    criteriavalue += criteriaObj.Criteria;
                    pattern += criteriaObj.Pattern;
                    if (i < count)
                    {
                        criteriavalue += recordCriteria.GroupOperator;
                        pattern += recordCriteria.GroupOperator;
                    }
                }
                recordCriteria.Criteria = criteriavalue + ")";
                recordCriteria.Pattern = pattern + ")";
            }
            return recordCriteria;
        }

        private ZCRMModuleRelation GetZCRMModuleRelation(JObject relatedList)
        {
            ZCRMModuleRelation moduleRelation = ZCRMModuleRelation.GetInstance(module.ApiName, Convert.ToInt64(relatedList["id"]));
            moduleRelation.SequenceNumber = (int)relatedList["sequence_number"];
            moduleRelation.Label = (string)relatedList["display_label"];
            moduleRelation.ApiName = (string)relatedList["api_name"];
            moduleRelation.Module = (string)relatedList["module"];
            moduleRelation.Name = (string)relatedList["name"];
            moduleRelation.Action = (string)relatedList["action"];
            moduleRelation.Href = (string)relatedList["href"];
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
                    allCustomViews.Add(GetZCRMCustomView(customViewObject, (JObject)customviewJSON["info"]["translation"]));
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

        public List<ZCRMField> GetFields(JArray fieldArray)
        {
            List<ZCRMField> fieldLists = new List<ZCRMField>();
            foreach (JObject fieldetails in fieldArray)
            {
                fieldLists.Add(GetZCRMField(fieldetails));
            }
            return fieldLists;
        }
    }
}