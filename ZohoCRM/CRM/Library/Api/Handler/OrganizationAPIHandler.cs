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
            organization.Alias = (string)orgDetails["alias"];
            organization.PrimaryZuid = (long)orgDetails["primary_zuid"];
            organization.Zgid = (long)orgDetails["zgid"];
            organization.Phone = (string)orgDetails["phone"];
            organization.Mobile = (string)orgDetails["mobile"];
            organization.Website = (string)orgDetails["website"];
            organization.PrimaryEmail = (string)orgDetails["primary_email"];
            organization.EmployeeCount = Convert.ToInt32(orgDetails["employee_count"].Type!= JTokenType.Null? orgDetails["employee_count"]: 0); //check the value is null/empty
            organization.Description = (string)orgDetails["description"];
            organization.Timezone = (string)orgDetails["time_zone"];
            organization.IsoCode = (string)orgDetails["iso_code"];
            organization.CurrencyLocale = (string)orgDetails["currency_locale"];
            organization.CurrencySymbol = (string)orgDetails["currency_symbol"];
            organization.Street = (string)orgDetails["street"];
            organization.State = (string)orgDetails["state"];
            organization.City = (string)orgDetails["city"];
            organization.Country = (string)orgDetails["country"];
            organization.CountryCode = (string)orgDetails["country_code"];
            organization.ZipCode = (string)orgDetails["zip"];
            organization.McStatus = (bool)orgDetails["mc_status"];
            organization.GappsEnabled = (bool)orgDetails["gapps_enabled"];

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
                if(modifiedSince != null && modifiedSince != "")
                {
                    requestHeaders.Add("If-Modified-Since", modifiedSince);   
                }

                BulkAPIResponse<ZCRMUser> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMUser>();

                List<ZCRMUser> allUsers = new List<ZCRMUser>();
                JObject responseJSON = response.ResponseJSON;
                if(responseJSON.ContainsKey("users"))
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
            if (userDetails.ContainsKey("profile") )
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
            if (userDetails.ContainsKey("created_by") && userDetails["created_by"].Type != JTokenType.Null)
            {
                JObject createdByObject = (JObject)userDetails["created_by"];
                ZCRMUser createdUser = ZCRMUser.GetInstance((long)createdByObject["id"], (string)createdByObject["name"]);
                user.CreatedBy = createdUser;
                user.CreatedTime = CommonUtil.removeEscaping((string)JsonConvert.SerializeObject(userDetails["created_time"]));
            }
            if (userDetails.ContainsKey("Modified_By") && userDetails["Modified_By"].Type != JTokenType.Null)
            {
                JObject modifiedByObject = (JObject)userDetails["Modified_By"];
                ZCRMUser modifiedByUser = ZCRMUser.GetInstance((long)modifiedByObject["id"], (string)modifiedByObject["name"]);
                user.ModifiedBy = modifiedByUser;
                user.ModifiedTime = CommonUtil.removeEscaping((string)JsonConvert.SerializeObject(userDetails["Modified_Time"]));
            }
            if (userDetails.ContainsKey("Reporting_To") && userDetails["Reporting_To"].Type != JTokenType.Null)
            {
                JObject reportingToObject = (JObject)userDetails["Reporting_To"];
                ZCRMUser reportingTo = ZCRMUser.GetInstance((long)reportingToObject["id"], (string)reportingToObject["name"]);
                user.ReportingTo = reportingTo;
            }

            return user;
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
            role.AdminUser = (bool)roleDetails["admin_user"];
            ZCRMRole reportingTo = null;
            if (roleDetails["reporting_to"].Type != JTokenType.Null)
            {
                JObject reportingToObject = (JObject)roleDetails["reporting_to"];
                reportingTo = ZCRMRole.GetInstance((long)reportingToObject["id"], (string)reportingToObject["name"]);
            }
            role.ReportingTo = reportingTo;
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
                profile.CreatedTime = (string)profileDetails["created_time"];
            }
            profile.ModifiedBy = null;
            if (profileDetails["modified_by"].Type != JTokenType.Null)
            {
                JObject modifiedByObject = (JObject)profileDetails["modified_by"];
                ZCRMUser modifiedBy = ZCRMUser.GetInstance((long)modifiedByObject["id"], (string)modifiedByObject["name"]);
                profile.ModifiedBy = modifiedBy;
                profile.ModifiedTime = (string)profileDetails["modified_time"];
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
            if (taxes.Count > 100)
            {
                throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
            }
            try
            {
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
            if (taxes.Count > 100)
            {
                throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
            }
            try
            {
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

                BulkAPIResponse<ZCRMOrgTax> response =  APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMOrgTax>();
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
            if(taxIds.Count > 100)
            {
                throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
            }

            try{
                requestMethod = APIConstants.RequestMethod.DELETE;
                urlPath = "org/taxes";
                requestQueryParams.Add("ids", CommonUtil.CollectionToCommaDelimitedString(taxIds));

                return APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMOrgTax>();
            }
            catch(Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public FileAPIResponse DownloadUserPhoto(CommonUtil.PhotoSize? photoSize)
        {
            try{
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = ZCRMConfigUtil.GetPhotoUrl();
                if(photoSize != null)
                {
                    requestQueryParams.Add("photo_size", photoSize.ToString());
                }
                return APIRequest.GetInstance(this).DownloadFile();
            }
            catch(Exception e) when(!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }
    }
}
