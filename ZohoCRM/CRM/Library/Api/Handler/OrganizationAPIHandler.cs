using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Setup.MetaData;
using ZCRMSDK.CRM.Library.Setup.Users;
using ZCRMSDK.CRM.Library.Common;
using Newtonsoft.Json;

namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public class OrganizationAPIHandler : CommonAPIHandler, IAPIHandler
    {

        private OrganizationAPIHandler() { }

        public static OrganizationAPIHandler GetInstance()
        {
            return new OrganizationAPIHandler();
        }

        public APIResponse GetOrganizationDetails()
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "org";

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JObject responseJSON = response.ResponseJSON;
                JArray orgArray = (JArray)responseJSON["org"];
                response.Data = GetZCRMOrganization((JObject)orgArray[0]);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        //TOOD: Handle exceptions;
        private ZCRMOrganization GetZCRMOrganization(JObject orgDetails)
        {
            ZCRMOrganization organization = ZCRMOrganization.GetInstance((string)orgDetails["company_name"], (long)orgDetails["id"]);
            organization.Country = (string)orgDetails["country"];
            organization.PhotoId = (string)orgDetails["photo_id"];
            organization.City = (string)orgDetails["city"];
            organization.Description = (string)orgDetails["description"];
            organization.McStatus = (bool)orgDetails["mc_status"];
            organization.GappsEnabled = (bool)orgDetails["gapps_enabled"];
            organization.Street = (string)orgDetails["street"];
            organization.Alias = (string)orgDetails["alias"];
            organization.Currency = (string)orgDetails["currency"];
            organization.State = (string)orgDetails["state"];
            organization.Fax = (string)orgDetails["fax"];
            organization.EmployeeCount = Convert.ToInt32(orgDetails["employee_count"].Type != JTokenType.Null ? orgDetails["employee_count"] : 0); //check the value is null/empty
            organization.ZipCode = (string)orgDetails["zip"];
            organization.Website = (string)orgDetails["website"];
            organization.CurrencySymbol = (string)orgDetails["currency_symbol"];
            organization.Mobile = (string)orgDetails["mobile"];
            organization.CurrencyLocale = (string)orgDetails["currency_locale"];
            organization.PrimaryZuid = (long)orgDetails["primary_zuid"];
            organization.ZiaPortalId = (string)orgDetails["zia_portal_id"];
            organization.Timezone = (string)orgDetails["time_zone"];
            organization.Zgid = (long)orgDetails["zgid"];
            organization.CountryCode = (string)orgDetails["country_code"];
            if (orgDetails.ContainsKey("license_details") && orgDetails["license_details"].Type != JTokenType.Null)
            {
                JObject organizationJobj = (JObject)orgDetails["license_details"];
                if (organizationJobj.ContainsKey("paid_expiry") && organizationJobj["paid_expiry"].Type != JTokenType.Null)
                {
                    organization.PaidExpiry = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(organizationJobj["paid_expiry"]));
                }
                if (organizationJobj.ContainsKey("users_license_purchased") && organizationJobj["users_license_purchased"].Type != JTokenType.Null)
                {
                    organization.UsersLicensePurchased = (int)organizationJobj["users_license_purchased"];
                }
                if (organizationJobj.ContainsKey("trial_type") && organizationJobj["trial_type"].Type != JTokenType.Null)
                {
                    organization.TrialType = (string)organizationJobj["trial_type"];
                }
                if (organizationJobj.ContainsKey("trial_expiry") && organizationJobj["trial_expiry"].Type != JTokenType.Null)
                {
                    organization.TrialType = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(organizationJobj["trial_expiry"]));
                }
                if (organizationJobj.ContainsKey("paid") && organizationJobj["paid"].Type != JTokenType.Null)
                {
                    organization.PaidAccount = (bool)organizationJobj["paid"];
                }
                if (organizationJobj.ContainsKey("paid_type") && organizationJobj["paid_type"].Type != JTokenType.Null)
                {
                    organization.PaidType = (string)organizationJobj["paid_type"];
                }
            }
            organization.Phone = (string)orgDetails["phone"];
            organization.PrivacySettings = (bool)orgDetails["privacy_settings"];
            organization.PrimaryEmail = (string)orgDetails["primary_email"];
            organization.IsoCode = (string)orgDetails["iso_code"];
            return organization;
        }


        public APIResponse GetUser(long? userId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                if (userId != null)
                {
                    urlPath = "users/" + userId;
                }
                else
                {
                    urlPath = "users";
                    requestQueryParams.Add("type", "CurrentUser");
                }

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray usersArray = (JArray)response.ResponseJSON["users"];
                response.Data = GetZCRMUser((JObject)usersArray[0]);

                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }


        private BulkAPIResponse<ZCRMUser> GetUsers(string type, string modifiedSince, int page, int perPage)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "users";
                requestQueryParams.Add("type", type);
                requestQueryParams.Add(APIConstants.PAGE, page.ToString());
                requestQueryParams.Add(APIConstants.PER_PAGE, perPage.ToString());
                if (modifiedSince != null && modifiedSince != "")
                {
                    requestHeaders.Add("If-Modified-Since", modifiedSince);
                }

                BulkAPIResponse<ZCRMUser> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMUser>();

                List<ZCRMUser> allUsers = new List<ZCRMUser>();
                JObject responseJSON = response.ResponseJSON;
                if (responseJSON.ContainsKey("users"))
                {
                    JArray usersArray = (JArray)responseJSON["users"];
                    foreach (JObject userJSON in usersArray)
                    {
                        allUsers.Add(GetZCRMUser(userJSON));
                    }
                }
                response.BulkData = allUsers;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse GetCurrentUser()
        {
            return GetUser(null);
        }


        public BulkAPIResponse<ZCRMUser> GetAllUsers(string modifiedSince, int page, int perPage)
        {
            return GetUsers(null, modifiedSince, page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllActiveUsers(int page, int perPage)
        {
            return GetUsers("ActiveUsers", null, page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllDeactivatedUsers(int page, int perPage)
        {
            return GetUsers("DeactiveUsers", null, page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllConfirmedUsers(int page, int perPage)
        {
            return GetUsers("ConfirmedUsers", null, page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllUnConfirmedUsers(int page, int perPage)
        {
            return GetUsers("NotConfirmedUsers", null, page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllDeletedUsers(int page, int perPage)
        {
            return GetUsers("DeletedUsers", null, page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedUsers(int page, int perPage)
        {
            return GetUsers("ActiveConfirmedUsers", null, page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllAdminUsers(int page, int perPage)
        {
            return GetUsers("AdminUsers", null, page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedAdmins(int page, int perPage)
        {
            return GetUsers("ActiveConfirmedAdmins", null, page, perPage);
        }

        private ZCRMUser GetZCRMUser(JObject userDetails)
        {
            ZCRMUser user = ZCRMUser.GetInstance((long)userDetails["id"], (string)userDetails["full_name"]);
            user.EmailId = (string)userDetails["email"];
            user.FirstName = (string)userDetails["first_name"];
            user.LastName = (string)userDetails["last_name"];
            user.Language = (string)userDetails["language"];
            user.Mobile = (string)userDetails["mobile"];
            user.Status = (string)userDetails["status"];
            user.ZuId = (long?)userDetails["zuid"];
            if (userDetails.ContainsKey("profile"))
            {
                JObject profileObject = (JObject)userDetails["profile"];
                ZCRMProfile profile = ZCRMProfile.GetInstance((long)profileObject["id"], (string)profileObject["name"]);
                user.Profile = profile;
            }
            if (userDetails.ContainsKey("role"))
            {
                JObject roleObject = (JObject)userDetails["role"];
                ZCRMRole role = ZCRMRole.GetInstance((long)roleObject["id"], (string)roleObject["name"]);
                user.Role = role;
            }

            user.Alias = (string)userDetails["alias"];
            user.City = (string)userDetails["city"];
            user.Confirm = (bool)userDetails["confirm"];
            user.CountryLocale = (string)userDetails["country_locale"];
            user.DateFormat = (string)userDetails["date_format"];
            user.TimeFormat = (string)userDetails["time_format"];
            user.DateOfBirth = (string)userDetails["dob"];
            user.Country = (string)userDetails["country"];
            user.Fax = (string)userDetails["fax"];
            user.Locale = (string)userDetails["locale"];
            user.NameFormat = (string)userDetails["name_format"];
            user.Website = (string)userDetails["website"];
            user.TimeZone = (string)userDetails["time_zone"];
            user.Street = (string)userDetails["street"];
            user.State = (string)userDetails["state"];
            user.MicrosoftAppUser = (bool)userDetails["microsoft"];
            user.Phone = (string)userDetails["phone"];
            if (userDetails.ContainsKey("created_by") && userDetails["created_by"].Type != JTokenType.Null)
            {
                JObject createdByObject = (JObject)userDetails["created_by"];
                ZCRMUser createdUser = ZCRMUser.GetInstance((long)createdByObject["id"], (string)createdByObject["name"]);
                user.CreatedBy = createdUser;
                user.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(userDetails["created_time"]));
            }
            if (userDetails.ContainsKey("Modified_By") && userDetails["Modified_By"].Type != JTokenType.Null)
            {
                JObject modifiedByObject = (JObject)userDetails["Modified_By"];
                ZCRMUser modifiedByUser = ZCRMUser.GetInstance((long)modifiedByObject["id"], (string)modifiedByObject["name"]);
                user.ModifiedBy = modifiedByUser;
                user.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(userDetails["Modified_Time"]));
            }
            if (userDetails.ContainsKey("Reporting_To") && userDetails["Reporting_To"].Type != JTokenType.Null)
            {
                JObject reportingToObject = (JObject)userDetails["Reporting_To"];
                ZCRMUser reportingTo = ZCRMUser.GetInstance((long)reportingToObject["id"], (string)reportingToObject["name"]);
                user.ReportingTo = reportingTo;
            }
            if (userDetails.ContainsKey("signature") && userDetails["signature"].Type != JTokenType.Null)
            {
                user.Signature = (string)userDetails["signature"];
            }
            if (userDetails.ContainsKey("number") && userDetails["number"].Type != JTokenType.Null)
            {
                user.Number = (int)userDetails["number"];
            }
            if (userDetails.ContainsKey("offset") && userDetails["offset"].Type != JTokenType.Null)
            {
                user.OffSet = (long)userDetails["offset"];
            }
            if (userDetails.ContainsKey("customize_info") && userDetails["customize_info"].Type != JTokenType.Null)
            {
                user.CustomizeInfo = GetZCRMUserCustomizeInfo((JObject)userDetails["customize_info"]);
            }
            if (userDetails.ContainsKey("personal_account") && userDetails["personal_account"].Type != JTokenType.Null)
            {
                user.IsPersonalAccount = (bool)userDetails["personal_account"];
            }
            if (userDetails.ContainsKey("default_tab_group") && userDetails["default_tab_group"].Type != JTokenType.Null)
            {
                user.DefaultTabGroup = (string)userDetails["default_tab_group"];
            }
            if (userDetails.ContainsKey("theme") && userDetails["theme"].Type != JTokenType.Null)
            {
                user.Theme = GetZCRMUserTheme((JObject)userDetails["theme"]);
            }
            if (userDetails.ContainsKey("zip") && userDetails["zip"].Type != JTokenType.Null)
            {
                user.Zip = (string)userDetails["zip"];
            }
            if (userDetails.ContainsKey("decimal_separator") && userDetails["decimal_separator"].Type != JTokenType.Null)
            {
                user.DecimalSeparator = (string)userDetails["decimal_separator"];
            }
            if (userDetails.ContainsKey("territories") && userDetails["territories"].Type != JTokenType.Null)
            {
                JArray jsonArray = (JArray)userDetails["territories"];
                List<ZCRMTerritory> territories = new List<ZCRMTerritory>();
                foreach (JObject territory in jsonArray)
                {
                    territories.Add(GetZCRMTerritory(territory));
                }
                user.Territories = territories;
            }
            if (userDetails.ContainsKey("Isonline") && userDetails["Isonline"].Type != JTokenType.Null)
            {
                user.IsOnline = (bool)userDetails["Isonline"];
            }
            if (userDetails.ContainsKey("Currency") && userDetails["Currency"].Type != JTokenType.Null)
            {
                user.Currency = (string)userDetails["Currency"];
            }
            foreach (KeyValuePair<string, JToken> token in userDetails)
            {
                if (!ZCRMUser.defaultKeys.Contains(token.Key))
                {
                    user.SetFieldValue(token.Key, (object)token.Value);
                }
            }
            return user;
        }

        private ZCRMUserCustomizeInfo GetZCRMUserCustomizeInfo(JObject customizeInfo)
        {
            ZCRMUserCustomizeInfo customizeInfoInstance = ZCRMUserCustomizeInfo.GetInstance();
            if (customizeInfo["notes_desc"] != null)
            {
                customizeInfoInstance.NotesDesc = (string)customizeInfo["notes_desc"];
            }
            if (customizeInfo["show_right_panel"] != null)
            {
                customizeInfoInstance.IsToShowRightPanel = (string)customizeInfo["show_right_panel"];
            }
            if (customizeInfo["bc_view"] != null)
            {
                customizeInfoInstance.IsBcView = (string)customizeInfo["bc_view"];
            }
            if (customizeInfo["show_home"] != null)
            {
                customizeInfoInstance.IsToShowHome = (string)customizeInfo["show_home"];
            }
            if (customizeInfo["show_detail_view"] != null)
            {
                customizeInfoInstance.IsToShowDetailView = (string)customizeInfo["show_detail_view"];
            }
            if (customizeInfo["unpin_recent_item"] != null)
            {
                customizeInfoInstance.UnpinRecentItem = (string)customizeInfo["unpin_recent_item"];
            }
            return customizeInfoInstance;
        }

        private ZCRMUserTheme GetZCRMUserTheme(JObject themeDetails)
        {
            ZCRMUserTheme themeInstance = ZCRMUserTheme.GetInstance();
            themeInstance.NormalTabFontColor = (string)themeDetails["normal_tab"]["font_color"];
            themeInstance.NormalTabBackground = (string)themeDetails["normal_tab"]["background"];
            themeInstance.SelectedTabFontColor = (string)themeDetails["selected_tab"]["font_color"];
            themeInstance.SelectedTabBackground = (string)themeDetails["selected_tab"]["background"];
            themeInstance.New_background = (string)themeDetails["new_background"];
            themeInstance.Background = (string)themeDetails["background"];
            themeInstance.Screen = (string)themeDetails["screen"];
            themeInstance.Type = (string)themeDetails["type"];
            return themeInstance;
        }

        private ZCRMTerritory GetZCRMTerritory(JObject territory)
        {
            ZCRMTerritory territoryIns = ZCRMTerritory.GetInstance(Convert.ToInt64(territory["id"]));
            if (territory.ContainsKey("created_time") && territory["created_time"].Type != JTokenType.Null)
            {
                territoryIns.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(territory["created_time"]));
            }
            if (territory.ContainsKey("modified_time") && territory["modified_time"].Type != JTokenType.Null)
            {
                territoryIns.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(territory["modified_time"]));
            }
            if (territory.ContainsKey("manager") && territory["manager"].Type != JTokenType.Null)
            {
                if (territory["manager"] is JObject)
                {
                    JObject managerByObject = (JObject)territory["created_by"];
                    ZCRMUser managerUser = ZCRMUser.GetInstance((long)managerByObject["id"], (string)managerByObject["name"]);
                    territoryIns.Manager = managerUser;
                }
                if (territory["manager"].Type.ToString().Equals("Boolean"))
                {
                    territoryIns.IsManager = (bool)(territory["manager"]);
                }
            }
            if (territory.ContainsKey("parent_id") && territory["parent_id"].Type != JTokenType.Null)
            {
                territoryIns.ParentId = territory["parent_id"].ToString();
            }
            if (territory.ContainsKey("criteria") && territory["criteria"].Type != JTokenType.Null)
            {
                JObject jobj = (JObject)territory["criteria"];
                territoryIns.Criteria = SetZCRMCriteriaObject(jobj);
            }
            if (territory.ContainsKey("name") && territory["name"].Type != JTokenType.Null)
            {
                territoryIns.Name = territory["name"].ToString();
            }
            if (territory.ContainsKey("modified_by") && territory["modified_by"].Type != JTokenType.Null)
            {
                JObject modifiedByObject = (JObject)territory["modified_by"];
                ZCRMUser modifiedUser = ZCRMUser.GetInstance((long)modifiedByObject["id"], (string)modifiedByObject["name"]);
                territoryIns.ModifiedBy = modifiedUser;
            }
            if (territory.ContainsKey("description") && territory["description"].Type != JTokenType.Null)
            {
                territoryIns.Description = territory["description"].ToString();
            }
            if (territory.ContainsKey("created_by") && territory["created_by"].Type != JTokenType.Null)
            {
                JObject createdByObject = (JObject)territory["created_by"];
                ZCRMUser createdUser = ZCRMUser.GetInstance((long)createdByObject["id"], (string)createdByObject["name"]);
                territoryIns.CreatedBy = createdUser;
            }
            return territoryIns;

        }

        private ZCRMCriteria SetZCRMCriteriaObject(JObject criteria)
        {
            ZCRMCriteria recordCriteria = ZCRMCriteria.GetInstance();
            if (criteria.ContainsKey("field") && criteria["field"].Type != JTokenType.Null)
            {
                recordCriteria.FieldAPIName = criteria["field"].ToString();
            }
            if (criteria.ContainsKey("value") && criteria["value"].Type != JTokenType.Null)
            {
                recordCriteria.Value = criteria["value"];
            }
            if (criteria.ContainsKey("group_operator") && criteria["group_operator"].Type != JTokenType.Null)
            {
                recordCriteria.GroupOperator = criteria["group_operator"].ToString();
            }
            if (criteria.ContainsKey("comparator") && criteria["comparator"].Type != JTokenType.Null)
            {
                recordCriteria.Comparator = criteria["comparator"].ToString();
            }
            if (criteria.ContainsKey("group") && criteria["group"].Type != JTokenType.Null)
            {
                List<ZCRMCriteria> recordData = new List<ZCRMCriteria>();
                JArray jarr = (JArray)criteria["group"];
                for (int i = 0; i < jarr.Count; i++)
                {
                    recordData.Add(SetZCRMCriteriaObject((JObject)jarr[i]));
                }
                recordCriteria.Group = recordData;
            }
            return recordCriteria;
        }

        public APIResponse CreateUser(ZCRMUser userInstance)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = "users";
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                if (userInstance.Id != 0)
                {
                    throw new ZCRMException("User ID MUST be null for CreateUser operation.");
                }
                dataArray.Add(ConstructJSONForUser(userInstance));
                requestBodyObject.Add(APIConstants.USERS, dataArray);
                requestBody = requestBodyObject;
                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse UpdateUser(ZCRMUser userInstance)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.PUT;
                if (userInstance.Id == 0)
                {
                    throw new ZCRMException("User ID MUST not be null for UpdateUser operation.");
                }
                urlPath = "users/" + userInstance.Id;
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                dataArray.Add(ConstructJSONForUser(userInstance));
                requestBodyObject.Add(APIConstants.USERS, dataArray);
                requestBody = requestBodyObject;
                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse DeleteUser(long userId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.DELETE;
                urlPath = "users/" + userId;
                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        private JObject ConstructJSONForUser(ZCRMUser user)
        {
            JObject userJSON = new JObject();
            ZCRMRole role = user.Role;
            if (role != null)
            {
                userJSON["role"] = role.Id.ToString();
            }
            ZCRMProfile profile = user.Profile;
            if (profile != null)
            {
                userJSON["profile"] = profile.Id.ToString();
            }
            if (user.Country != null)
            {
                userJSON["country"] = user.Country;
            }
            if (user.City != null)
            {
                userJSON["city"] = user.City;
            }
            if (user.Signature != null)
            {
                userJSON["signature"] = user.Signature;
            }
            if (user.NameFormat != null)
            {
                userJSON["name_format"] = user.NameFormat;
            }
            if (user.Language != null)
            {
                userJSON["language"] = user.Language;
            }
            if (user.Locale != null)
            {
                userJSON["locale"] = user.Locale;
            }
            if (user.DefaultTabGroup != null)
            {
                userJSON["default_tab_group"] = user.DefaultTabGroup;
            }
            if (user.Street != null)
            {
                userJSON["street"] = user.Street;
            }
            if (user.Alias != null)
            {
                userJSON["alias"] = user.Alias;
            }
            if (user.State != null)
            {
                userJSON["state"] = user.State;
            }
            if (user.CountryLocale != null)
            {
                userJSON["country_locale"] = user.CountryLocale;
            }
            if (user.Fax != null)
            {
                userJSON["fax"] = user.Fax;
            }
            if (user.FirstName != null)
            {
                userJSON["first_name"] = user.FirstName;
            }
            if (user.EmailId != null)
            {
                userJSON["email"] = user.EmailId;
            }
            if (user.Zip != null)
            {
                userJSON["zip"] = user.Zip;
            }
            if (user.DecimalSeparator != null)
            {
                userJSON["decimal_separator"] = user.DecimalSeparator;
            }
            if (user.Website != null)
            {
                userJSON["website"] = user.Website;
            }
            if (user.TimeFormat != null)
            {
                userJSON["time_format"] = user.TimeFormat;
            }
            if (user.Mobile != null)
            {
                userJSON["mobile"] = user.Mobile;
            }
            if (user.LastName != null)
            {
                userJSON["last_name"] = user.LastName;
            }
            if (user.TimeZone != null)
            {
                userJSON["time_zone"] = user.TimeZone;
            }
            if (user.Phone != null)
            {
                userJSON["phone"] = user.Phone;
            }
            if (user.DateOfBirth != null)
            {
                userJSON["dob"] = user.DateOfBirth;
            }
            if (user.DateFormat != null)
            {
                userJSON["date_format"] = user.DateFormat;
            }
            if (user.Status != null)
            {
                userJSON["status"] = user.Status;
            }

            foreach (KeyValuePair<string, object> token in user.Data)
            {
                userJSON[token.Key] = JToken.FromObject(token.Value);
            }

            userJSON["personal_account"] = user.IsPersonalAccount;
            return userJSON;
        }

        public APIResponse GetRole(long roleId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/roles/" + roleId;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JObject responseJSON = response.ResponseJSON;
                JArray rolesArray = (JArray)responseJSON["roles"];
                response.Data = GetZCRMRole((JObject)rolesArray[0]);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMRole> GetAllRoles()
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/roles";

                BulkAPIResponse<ZCRMRole> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMRole>();

                List<ZCRMRole> allRoles = new List<ZCRMRole>();
                JObject responseJSON = response.ResponseJSON;
                JArray rolesArray = (JArray)responseJSON["roles"];
                foreach (JObject roleDetails in rolesArray)
                {
                    allRoles.Add(GetZCRMRole(roleDetails));
                }
                response.BulkData = allRoles;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        //TODO: Handle exceptions;
        private ZCRMRole GetZCRMRole(JObject roleDetails)
        {
            ZCRMRole role = ZCRMRole.GetInstance((long)roleDetails["id"], (string)roleDetails["name"]);
            role.Label = (string)roleDetails["display_label"];
            if (roleDetails.ContainsKey("forecast_manager") && roleDetails["forecast_manager"].Type != JTokenType.Null)
            {
                JObject forecast_manager = (JObject)roleDetails["forecast_manager"];
                ZCRMUser user = ZCRMUser.GetInstance();
                if (forecast_manager.ContainsKey("id") && forecast_manager["id"].Type != JTokenType.Null)
                {
                    user.Id = (long)forecast_manager["id"];
                }
                if (forecast_manager.ContainsKey("name") && forecast_manager["name"].Type != JTokenType.Null)
                {
                    user.FullName = (string)forecast_manager["name"];
                }
                role.ForecastManager = user;
            }
            if (roleDetails.ContainsKey("share_with_peers") && roleDetails["share_with_peers"].Type != JTokenType.Null)
            {
                role.ShareWithPeers = (bool)roleDetails["share_with_peers"];
            }
            if (roleDetails.ContainsKey("description") && roleDetails["description"].Type != JTokenType.Null)
            {
                role.Description = (string)roleDetails["description"];
            }
            ZCRMRole reportingTo = null;
            if (roleDetails.ContainsKey("reporting_to") && roleDetails["reporting_to"].Type != JTokenType.Null)
            {
                JObject reportingToObject = (JObject)roleDetails["reporting_to"];
                reportingTo = ZCRMRole.GetInstance((long)reportingToObject["id"], (string)reportingToObject["name"]);
            }
            role.ReportingTo = reportingTo;
            role.AdminUser = (bool)roleDetails["admin_user"];
            return role;
        }

        public APIResponse GetProfile(long profileId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/profiles/" + profileId;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JObject responseJSON = response.ResponseJSON;
                JArray rolesArray = (JArray)responseJSON["profiles"];
                response.Data = GetZCRMProfile((JObject)rolesArray[0]);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMProfile> GetAllProfiles()
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "settings/profiles";

                BulkAPIResponse<ZCRMProfile> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMProfile>();

                List<ZCRMProfile> allProfiles = new List<ZCRMProfile>();
                JObject responseJSON = response.ResponseJSON;
                JArray profilesArray = (JArray)responseJSON["profiles"];
                foreach (JObject profileDetails in profilesArray)
                {
                    allProfiles.Add(GetZCRMProfile(profileDetails));
                }
                response.BulkData = allProfiles;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        private ZCRMProfile GetZCRMProfile(JObject profileDetails)
        {
            ZCRMProfile profile = ZCRMProfile.GetInstance((long)profileDetails["id"], (string)profileDetails["name"]);
            profile.Category = (bool)profileDetails["category"];
            profile.Description = (string)profileDetails["description"];
            profile.CreatedBy = null;
            //TODO: Check with HasValues method;
            if (profileDetails["created_by"].Type != JTokenType.Null)
            {
                JObject createdByObject = (JObject)profileDetails["created_by"];
                ZCRMUser createdBy = ZCRMUser.GetInstance((long)createdByObject["id"], (string)createdByObject["name"]);
                profile.CreatedBy = createdBy;
                profile.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(profileDetails["created_time"]));
            }
            profile.ModifiedBy = null;
            if (profileDetails["modified_by"].Type != JTokenType.Null)
            {
                JObject modifiedByObject = (JObject)profileDetails["modified_by"];
                ZCRMUser modifiedBy = ZCRMUser.GetInstance((long)modifiedByObject["id"], (string)modifiedByObject["name"]);
                profile.ModifiedBy = modifiedBy;
                profile.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(profileDetails["modified_time"]));
            }
            if (profileDetails.ContainsKey("permissions_details") && profileDetails["permissions_details"].Type != JTokenType.Null)
            {
                JArray permissionsJArr = (JArray)profileDetails["permissions_details"];
                foreach (JObject permissionsJObj in permissionsJArr)
                {
                    ZCRMProfilePermissions profilePermissions = ZCRMProfilePermissions.GetInstance();
                    if (permissionsJObj.ContainsKey("display_label") && permissionsJObj["display_label"].Type != JTokenType.Null)
                    {
                        profilePermissions.DisplayLabel = (string)permissionsJObj["display_label"];
                    }
                    if (permissionsJObj.ContainsKey("module") && permissionsJObj["module"].Type != JTokenType.Null)
                    {
                        profilePermissions.Module = (string)permissionsJObj["module"];
                    }
                    if (permissionsJObj.ContainsKey("name") && permissionsJObj["name"].Type != JTokenType.Null)
                    {
                        profilePermissions.Name = (string)permissionsJObj["name"];
                    }
                    if (permissionsJObj.ContainsKey("id") && permissionsJObj["id"].Type != JTokenType.Null)
                    {
                        profilePermissions.Id = (long)permissionsJObj["id"];
                    }
                    if (permissionsJObj.ContainsKey("enabled") && permissionsJObj["enabled"].Type != JTokenType.Null)
                    {
                        profilePermissions.Enabled = (bool)permissionsJObj["enabled"];
                    }
                    profile.SetPermissionsDetails(profilePermissions);
                }
            }
            if (profileDetails.ContainsKey("sections") && profileDetails["sections"].Type != JTokenType.Null)
            {
                JArray sectionsJArr = (JArray)profileDetails["sections"];
                foreach (JObject sectionsJObj in sectionsJArr)
                {
                    ZCRMProfileSection sectionIns = ZCRMProfileSection.GetInstance((string)sectionsJObj["name"]);
                    if (sectionsJObj.ContainsKey("categories") && sectionsJObj["categories"].Type != JTokenType.Null)
                    {
                        JArray categoriesJArr = (JArray)sectionsJObj["categories"];
                        foreach (JObject categoriesJObj in categoriesJArr)
                        {
                            ZCRMProfileCategory categoryIns = ZCRMProfileCategory.GetInstance((string)categoriesJObj["name"]);
                            if (categoriesJObj.ContainsKey("display_label") && categoriesJObj["display_label"].Type != JTokenType.Null)
                            {
                                categoryIns.DisplayLabel = (string)categoriesJObj["display_label"];
                            }
                            if (categoriesJObj.ContainsKey("module") && categoriesJObj["module"].Type != JTokenType.Null)
                            {
                                categoryIns.Module = (string)categoriesJObj["module"];
                            }
                            if (categoriesJObj.ContainsKey("permissions_details") && categoriesJObj["permissions_details"].Type != JTokenType.Null && categoriesJObj["permissions_details"].HasValues)
                            {
                                List<long> permissionsIds = new List<long>();
                                foreach (long id in categoriesJObj["permissions_details"])
                                {
                                    permissionsIds.Add(id);
                                }
                                categoryIns.PermissionIds = permissionsIds;
                            }
                            sectionIns.SetCategories(categoryIns);
                        }
                    }
                    profile.SetProfileSection(sectionIns);
                }
            }
            return profile;
        }

        public APIResponse GetTax(long taxId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "org/taxes/" + taxId;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JObject responseJSON = response.ResponseJSON;
                JArray taxArray = (JArray)responseJSON["taxes"];
                response.Data = GetZCRMOrgTax((JObject)taxArray[0]);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMOrgTax> GetAllTaxes()
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = "org/taxes";

                BulkAPIResponse<ZCRMOrgTax> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMOrgTax>();

                JObject responseJSON = response.ResponseJSON;
                response.BulkData = GetAllZCRMOrgTax(responseJSON);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        private List<ZCRMOrgTax> GetAllZCRMOrgTax(JObject responseJSON)
        {
            List<ZCRMOrgTax> allOrgTaxes = new List<ZCRMOrgTax>();
            JArray taxArray = (JArray)responseJSON["taxes"];
            foreach (JObject taxDetails in taxArray)
            {
                allOrgTaxes.Add(GetZCRMOrgTax(taxDetails));
            }
            return allOrgTaxes;
        }

        private ZCRMOrgTax GetZCRMOrgTax(JObject taxDetails)
        {
            ZCRMOrgTax tax = ZCRMOrgTax.GetInstance((long)taxDetails["id"]);
            if (taxDetails.ContainsKey("name"))
            {
                tax.Name = (string)taxDetails["name"];
            }
            if (taxDetails.ContainsKey("display_label"))
            {
                tax.DisplayName = (string)taxDetails["display_label"];
            }
            if (taxDetails.ContainsKey("value"))
            {
                tax.Value = (double)taxDetails["value"];
            }
            return tax;
        }

        private JObject GetZCRMOrgTaxAsJSON(ZCRMOrgTax tax)
        {
            JObject taxJSON = new JObject
            {
                { "name", tax.Name },
                { "id", tax.Id },
                { "display_label", tax.DisplayName },
                { "value", tax.Value },
            };
            return taxJSON;

        }

        public BulkAPIResponse<ZCRMOrgTax> CreateTaxes(List<ZCRMOrgTax> taxes)
        {
            try
            {
                if (taxes.Count > 100)
                {
                    throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
                }
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = "org/taxes";
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                foreach (ZCRMOrgTax tax in taxes)
                {
                    dataArray.Add(GetZCRMOrgTaxAsJSON(tax));
                }
                requestBodyObject.Add("taxes", dataArray);
                requestBody = requestBodyObject;

                BulkAPIResponse<ZCRMOrgTax> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMOrgTax>();

                List<ZCRMOrgTax> createdTaxes = new List<ZCRMOrgTax>();
                List<EntityResponse> responses = response.BulkEntitiesResponse;
                foreach (EntityResponse entityResponse in responses)
                {
                    if (entityResponse.Status.Equals(APIConstants.CODE_SUCCESS))
                    {
                        JObject responseData = entityResponse.ResponseJSON;
                        JObject responseDetails = (JObject)responseData[APIConstants.DETAILS];
                        ZCRMOrgTax tax = GetZCRMOrgTax(responseDetails);
                        createdTaxes.Add(tax);
                        entityResponse.Data = tax;
                    }
                    else
                    {
                        entityResponse.Data = null;
                    }
                }
                response.BulkData = createdTaxes;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMOrgTax> UpdateTaxes(List<ZCRMOrgTax> taxes)
        {
            try
            {
                if (taxes.Count > 100)
                {
                    throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
                }
                requestMethod = APIConstants.RequestMethod.PUT;
                urlPath = "org/taxes";
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                foreach (ZCRMOrgTax tax in taxes)
                {
                    dataArray.Add(GetZCRMOrgTaxAsJSON(tax));
                }
                requestBodyObject.Add("taxes", dataArray);
                requestBody = requestBodyObject;

                BulkAPIResponse<ZCRMOrgTax> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMOrgTax>();
                List<ZCRMOrgTax> updateTaxes = new List<ZCRMOrgTax>();
                List<EntityResponse> responses = response.BulkEntitiesResponse;
                foreach (EntityResponse entityResponse in responses)
                {
                    if (entityResponse.Status.Equals(APIConstants.CODE_SUCCESS))
                    {
                        JObject responseData = entityResponse.ResponseJSON;
                        JObject responseDetails = (JObject)responseData[APIConstants.DETAILS];
                        ZCRMOrgTax tax = GetZCRMOrgTax(responseDetails);
                        updateTaxes.Add(tax);
                        entityResponse.Data = tax;
                    }
                    else
                    {
                        entityResponse.Data = null;
                    }
                }
                response.BulkData = updateTaxes;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public BulkAPIResponse<ZCRMOrgTax> DeleteTaxes(List<long> taxIds)
        {
            try
            {
                if (taxIds.Count > 100)
                {
                    throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
                }
                requestMethod = APIConstants.RequestMethod.DELETE;
                urlPath = "org/taxes";
                requestQueryParams.Add("ids", CommonUtil.CollectionToCommaDelimitedString(taxIds));

                return APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMOrgTax>();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public FileAPIResponse DownloadUserPhoto(CommonUtil.PhotoSize? photoSize)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = ZCRMConfigUtil.GetPhotoUrl();
                if (photoSize != null)
                {
                    requestQueryParams.Add("photo_size", photoSize.ToString());
                }
                return APIRequest.GetInstance(this).DownloadFile();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }
    }
}