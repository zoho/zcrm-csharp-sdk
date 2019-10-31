using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.Users;
using Newtonsoft.Json;

namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public class MetaDataAPIHandler : CommonAPIHandler, IAPIHandler
    {

        private int index;
        private MetaDataAPIHandler() { }

        public static MetaDataAPIHandler GetInstance()
        {
            return new MetaDataAPIHandler();
        }


        public BulkAPIResponse<ZCRMModule> GetAllModules(string modifiedSince)
        {
            requestMethod = APIConstants.RequestMethod.GET;
            urlPath = "settings/modules";
            if (!string.IsNullOrEmpty(modifiedSince))
            {
                requestHeaders.Add("If-Modified-Since", modifiedSince);
            }

            BulkAPIResponse<ZCRMModule> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMModule>();

            JObject responseJSON = response.ResponseJSON;
            List<ZCRMModule> allModules = new List<ZCRMModule>();

            if (responseJSON.ContainsKey("modules"))
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
                if (moduleDetails.ContainsKey("global_search_supported") && moduleDetails["global_search_supported"].Type != JTokenType.Null)
                {
                    module.GlobalSearchSupported = (bool)moduleDetails["global_search_supported"];
                }
                if (moduleDetails.ContainsKey("kanban_view") && moduleDetails["kanban_view"].Type != JTokenType.Null)
                {
                    module.KanbanView = (bool)moduleDetails["kanban_view"];
                }
                module.Deletable = (bool)moduleDetails["deletable"];
                module.Creatable = (bool)moduleDetails["creatable"];
                if (moduleDetails.ContainsKey("filter_status") && moduleDetails["filter_status"].Type != JTokenType.Null)
                {
                    module.FilterStatus = (bool)moduleDetails["filter_status"];
                }
                if (moduleDetails.ContainsKey("inventory_template_supported") && moduleDetails["inventory_template_supported"].Type != JTokenType.Null)
                {
                    module.InventoryTemplateSupported = (bool)moduleDetails["inventory_template_supported"];
                }
                if (moduleDetails.ContainsKey("modified_time") && moduleDetails["modified_time"].Type != JTokenType.Null)
                {
                    module.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(moduleDetails["modified_time"]));
                }
                module.PluralLabel = (string)moduleDetails["plural_label"];
                if (moduleDetails.ContainsKey("presence_sub_menu") && moduleDetails["presence_sub_menu"].Type != JTokenType.Null)
                {
                    module.PresenceSubMenu = (bool)moduleDetails["presence_sub_menu"];
                }
                module.Id = Convert.ToInt64(moduleDetails["id"]);
                if (moduleDetails.ContainsKey("related_list_properties") && moduleDetails["related_list_properties"].Type != JTokenType.Null)
                {
                    module.RelatedListProperties = GetRelatedListProperties((JObject)moduleDetails["related_list_properties"]);
                }
                if (moduleDetails.ContainsKey("$properties") && moduleDetails["$properties"].Type != JTokenType.Null)
                {
                    List<string> properties = new List<string>();
                    foreach (string property in (JArray)moduleDetails["$properties"])
                    {
                        properties.Add(property);
                    }
                    module.Properties = properties;
                }
                if (moduleDetails.ContainsKey("per_page") && moduleDetails["per_page"].Type != JTokenType.Null)
                {
                    module.PerPage = (int)moduleDetails["per_page"];
                }
                if (moduleDetails.ContainsKey("visibility") && moduleDetails["visibility"].Type != JTokenType.Null)
                {
                    module.Visibility = (int)moduleDetails["visibility"];
                }
                module.Convertable = (bool)moduleDetails["convertable"];
                module.Editable = (bool)moduleDetails["editable"];
                if (moduleDetails.ContainsKey("emailTemplate_support") && moduleDetails["emailTemplate_support"].Type != JTokenType.Null)
                {
                    module.EmailTemplateSupport = (bool)moduleDetails["emailTemplate_support"];
                }
                if (moduleDetails["profiles"].HasValues)
                {
                    JArray accessibleProfilesArray = (JArray)moduleDetails["profiles"];
                    foreach (JObject accessibleProfiles in accessibleProfilesArray)
                    {
                        ZCRMProfile profile = ZCRMProfile.GetInstance(Convert.ToInt64(accessibleProfiles["id"]), (string)accessibleProfiles["name"]);
                        module.AddAccessibleProfile(profile);
                    }
                }
                if (moduleDetails.ContainsKey("filter_supported") && moduleDetails["filter_supported"].Type != JTokenType.Null)
                {
                    module.FilterSupported = (bool)moduleDetails["filter_supported"];
                }
                if (moduleDetails.ContainsKey("display_field") && moduleDetails["display_field"].Type != JTokenType.Null)
                {
                    module.DisplayField = (string)moduleDetails["display_field"];
                }
                if (moduleDetails.ContainsKey("search_layout_fields") && moduleDetails["search_layout_fields"].Type != JTokenType.Null)
                {
                    List<string> layout_Fields = new List<string>();
                    foreach (string layout_Field in (JArray)moduleDetails["search_layout_fields"])
                    {
                        layout_Fields.Add(layout_Field);
                    }
                    module.SearchLayoutFields = layout_Fields;
                }
                if (moduleDetails.ContainsKey("kanban_view_supported") && moduleDetails["kanban_view_supported"].Type != JTokenType.Null)
                {
                    module.KanbanViewSupported = (bool)moduleDetails["kanban_view_supported"];
                }
                module.WebLink = (string)moduleDetails["web_link"];
                module.SequenceNumber = (int)moduleDetails["sequence_number"];
                module.SingularLabel = (string)moduleDetails["singular_label"];
                module.Viewable = (bool)moduleDetails["viewable"];
                module.ApiSupported = (bool)(moduleDetails["api_supported"]);
                if (moduleDetails.ContainsKey("quick_create") && moduleDetails["quick_create"].Type != JTokenType.Null)
                {
                    module.QuickCreate = (bool)(moduleDetails["quick_create"]);
                }
                if (moduleDetails["modified_by"].HasValues)
                {
                    JObject modifiedByObject = (JObject)moduleDetails["modified_by"];
                    ZCRMUser modifiedUser = ZCRMUser.GetInstance(Convert.ToInt64(modifiedByObject["id"]), (string)modifiedByObject["name"]);
                    module.ModifiedBy = modifiedUser;
                }
                module.CustomModule = (bool)(moduleDetails["generated_type"].ToString().Equals("custom"));
                if (moduleDetails.ContainsKey("feeds_required") && moduleDetails["feeds_required"].Type != JTokenType.Null)
                {
                    module.FeedsRequired = (bool)(moduleDetails["feeds_required"]);
                }
                if (moduleDetails.ContainsKey("scoring_supported") && moduleDetails["scoring_supported"].Type != JTokenType.Null)
                {
                    module.ScoringSupported = (bool)(moduleDetails["scoring_supported"]);
                }
                if (moduleDetails.ContainsKey("webform_supported") && moduleDetails["webform_supported"].Type != JTokenType.Null)
                {
                    module.WebformSupported = (bool)(moduleDetails["webform_supported"]);
                }
                if (moduleDetails.ContainsKey("arguments") && moduleDetails["arguments"].Type != JTokenType.Null)
                {
                    if (moduleDetails["arguments"].HasValues)
                    {
                        List<ZCRMWebTabArguments> argumentsList = new List<ZCRMWebTabArguments>();
                        JArray argumentsJArr = (JArray)moduleDetails["arguments"];
                        foreach (JObject argumentsJObj in argumentsJArr)
                        {
                            ZCRMWebTabArguments argumentsIns = ZCRMWebTabArguments.GetInstance();
                            if (argumentsJObj.ContainsKey("name") && argumentsJObj["name"].Type != JTokenType.Null)
                            {
                                argumentsIns.Name = (string)argumentsJObj["name"];
                            }
                            if (argumentsJObj.ContainsKey("value") && argumentsJObj["value"].Type != JTokenType.Null)
                            {
                                argumentsIns.Value = (string)argumentsJObj["value"];
                            }
                            argumentsList.Add(argumentsIns);
                        }
                        module.WebTabArguments = argumentsList;
                    }
                }
                module.SystemName = (string)moduleDetails["module_name"];
                if (moduleDetails.ContainsKey("business_card_field_limit") && moduleDetails["business_card_field_limit"].Type != JTokenType.Null)
                {
                    module.BusinessCardFieldLimit = (int)moduleDetails["business_card_field_limit"];
                }
                if (moduleDetails.ContainsKey("custom_view") && moduleDetails["custom_view"].Type != JTokenType.Null)
                {
                    module.CustomView = GetZCRMCustomView((string)moduleDetails["api_name"], (JObject)moduleDetails["custom_view"]);
                }
                if (moduleDetails.ContainsKey("parent_module") && moduleDetails["parent_module"].Type != JTokenType.Null)
                {
                    if (moduleDetails["parent_module"].HasValues)
                    {
                        JObject parent_module = (JObject)moduleDetails["parent_module"];
                        ZCRMModule parentModuleIns = ZCRMModule.GetInstance((string)parent_module["api_name"]);
                        parentModuleIns.Id = Convert.ToInt64(parent_module["id"]);
                        module.ParentModule = parentModuleIns;
                    }
                }
                if (moduleDetails.ContainsKey("territory") && moduleDetails["territory"].Type != JTokenType.Null)
                {
                    JObject territoryJobj = (JObject)moduleDetails["territory"];
                    ZCRMTerritory territoryIns = ZCRMTerritory.GetInstance(Convert.ToInt64(territoryJobj["id"]));
                    territoryIns.Name = (string)territoryJobj["name"];
                    territoryIns.Subordinates = (bool)territoryJobj["subordinates"];
                    module.Territory = territoryIns;
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

                if (moduleDetails.ContainsKey("fields") && moduleDetails["fields"].Type != JTokenType.Null)
                {
                    JArray fieldArray = (JArray)moduleDetails["fields"];
                    module.Fields = ModuleAPIHandler.GetInstance(ZCRMModule.GetInstance((string)moduleDetails["api_name"])).GetFields(fieldArray);
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
            catch (Exception e) when (!(e is ZCRMException))
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

        private ZCRMRelatedListProperties GetRelatedListProperties(JObject relatedListProperties)
        {
            ZCRMRelatedListProperties relatedListPropInstance = ZCRMRelatedListProperties.GetInstance();
            if (relatedListProperties.ContainsKey("sort_by") && relatedListProperties["sort_by"].Type != JTokenType.Null)
            {
                relatedListPropInstance.SortBy = (string)relatedListProperties["sort_by"];
            }
            if (relatedListProperties.ContainsKey("sort_order") && relatedListProperties["sort_order"].Type != JTokenType.Null)
            {
                relatedListPropInstance.SortOrder = (string)relatedListProperties["sort_order"];
            }
            if (relatedListProperties.ContainsKey("fields") && relatedListProperties["fields"].Type != JTokenType.Null)
            {
                List<string> fields = new List<string>();
                foreach (string field in (JArray)relatedListProperties["fields"])
                {
                    fields.Add(field);
                }
                relatedListPropInstance.Fields = fields;
            }
            return relatedListPropInstance;
        }

        private ZCRMCustomView GetZCRMCustomView(string ModuleAPIName, JObject customViewObject)
        {
            ZCRMCustomView customView = ZCRMCustomView.GetInstance(ModuleAPIName, Convert.ToInt64(customViewObject["id"]));
            customView.DisplayName = (string)customViewObject["display_value"];
            if (customViewObject.ContainsKey("shared_type") && customViewObject["shared_type"].Type != JTokenType.Null)
            {
                customView.SharedType = (string)customViewObject["shared_type"];
            }
            if (customViewObject.ContainsKey("criteria") && customViewObject["criteria"].Type != JTokenType.Null)
            {
                JObject jobj = (JObject)customViewObject["criteria"];
                index = 1;
                customView.Criteria = SetZCRMCriteriaObject(jobj);
                customView.CriteriaPattern = customView.Criteria.Pattern;
                customView.CriteriaCondition = customView.Criteria.Criteria;
            }
            customView.SystemName = (string)customViewObject["system_name"];
            if (customViewObject.ContainsKey("shared_details") && customViewObject["shared_details"].Type != JTokenType.Null)
            {
                customView.SharedDetails = customViewObject["shared_details"].ToString();
            }
            customView.SortBy = (string)customViewObject["sort_by"];
            if (customViewObject.ContainsKey("offline") && customViewObject["offline"].Type != JTokenType.Null)
            {
                customView.IsOffline = (bool)customViewObject["offline"];
            }
            customView.Isdefault = (bool)customViewObject["default"];
            if (customViewObject.ContainsKey("system_defined") && customViewObject["system_defined"].Type != JTokenType.Null)
            {
                customView.IsSystemDefined = (bool)customViewObject["system_defined"];
            }
            customView.Name = (string)customViewObject["name"];
            customView.Category = (string)customViewObject["category"];
            if (customViewObject.ContainsKey("fields") && customViewObject["fields"].Type != JTokenType.Null)
            {
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
            }
            if (customViewObject.ContainsKey("favorite") && customViewObject["favorite"].Type != JTokenType.Null)
            {
                customView.Favourite = Convert.ToInt32(customViewObject["favourite"]);
            }
            if (customViewObject.ContainsKey("sort_order") && customViewObject["sort_order"].Type != JTokenType.Null)
            {
                customView.SortOrder = (CommonUtil.SortOrder)Enum.Parse(typeof(CommonUtil.SortOrder), (string)customViewObject["sort_order"]);
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
    }
}
