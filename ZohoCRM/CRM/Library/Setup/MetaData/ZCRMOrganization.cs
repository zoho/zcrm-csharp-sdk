using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Api.Handler;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.Setup.MetaData
{
    public class ZCRMOrganization : ZCRMEntity
    {
        private string companyName;
        private string alias;
        private long? orgId;
        private long primaryZuid;
        private long zgid;
        private string primaryEmail;
        private string website;
        private string mobile;
        private string phone;
        private string fax;
        private int employeeCount;
        private string description;
        private string timezone;
        private string isoCode;
        private string currencyLocale;
        private string currencySymbol;
        private string street;
        private string state;
        private string city;
        private string country;
        private string zipCode;
        private string countryCode;
        private bool mcStatus;
        private bool gappsEnabled;


        private ZCRMOrganization(string orgName, long? orgId)
        {
            CompanyName = orgName;
            OrgId = orgId;
        }

        /// <summary>
        /// To get ZohoCRM Organization Instance.
        /// </summary>
        /// <returns>ZCRMOrganization class instance.</returns>
        public static ZCRMOrganization GetInstance()
        {
            return new ZCRMOrganization(null, null);
        }

        /// <summary>
        /// To get ZohoCRM Organization Instance by passing Organization name and Organization Id.
        /// </summary>
        /// <returns>ZCRMOrganization class instance</returns>
        /// <param name="orgName">Name (String) of the Organization.</param>
        /// <param name="orgId">Id (Long) of the Organization.</param>
        public static ZCRMOrganization GetInstance(string orgName, long orgId)
        {
            return new ZCRMOrganization(orgName, orgId);
        }

        /// <summary>
        /// Gets the company name of the Organization.
        /// </summary>
        /// <value>The company name of the Organization.</value>
        /// <returns>String</returns>
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            private set
            {
                companyName = value;
            }
        }

        /// <summary>
        /// Gets or sets the alias name of/for the Organization.
        /// </summary>
        /// <value>The alias name of the Organization.</value>
        /// <returns>String</returns>
        public string Alias
        {
            get
            {
                return alias;
            }
            set
            {
                alias = value;
            }
        }

        /// <summary>
        /// Gets or sets the Id of/for the Organization.
        /// </summary>
        /// <value>The Id of the Organization.</value>
        /// <returns>Long</returns>
        public long? OrgId
        {
            get
            {
                return orgId;
            }
            set
            {
                orgId = value;
            }
        }

        /// <summary>
        /// Gets or sets the primary Zoho Unique Id of/for the Organization.
        /// </summary>
        /// <value>The primary Zoho Unique Id of the Organization.</value>
        /// <returns>Long</returns>
        public long PrimaryZuid
        {
            get
            {
                return primaryZuid;
            }
            set
            {
                primaryZuid = value;
            }
        }

        /// <summary>
        /// Gets or sets the ZGID of/for the Organization.
        /// </summary>
        /// <value>The ZGID of the Organization.</value>
        /// <returns>Long</returns>
        public long Zgid
        {
            get
            {
                return zgid;
            }
            set
            {
                zgid = value;
            }
        }

        /// <summary>
        /// Gets or sets the primary email of/for the Organization.
        /// </summary>
        /// <value>The primary emai of the Organization.</value>
        /// <returns>String</returns>
        public string PrimaryEmail
        {
            get
            {
                return primaryEmail;
            }
            set
            {
                primaryEmail = value;
            }
        }

        /// <summary>
        /// Gets or sets the website of/for the Organization.
        /// </summary>
        /// <value>The website of the Organization.</value>
        /// <returns>String</returns>
        public string Website
        {
            get
            {
                return website;
            }
            set
            {
                website = value;
            }
        }

        /// <summary>
        /// Gets or sets the mobile number of/for the Organization.
        /// </summary>
        /// <value>The mobile number of the Organization.</value>
        /// <returns>String</returns>
        public string Mobile
        {
            get
            {
                return mobile;
            }
            set
            {
                mobile = value;
            }
        }

        /// <summary>
        /// Gets or sets the phone number of/for the Organization.
        /// </summary>
        /// <value>The phone number of the Organization.</value>
        /// <returns>String</returns>
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        /// <summary>
        /// Gets or sets the fax of/for the Organization.
        /// </summary>
        /// <value>The fax of the Organization.</value>
        /// <returns>String</returns>
        public string Fax
        {
            get
            {
                return fax;
            }
            set
            {
                fax = value;
            }
        }

        /// <summary>
        /// Gets or sets the employee count of/for the Organization.
        /// </summary>
        /// <value>The employee count of the Organization.</value>
        /// <returns>Integer</returns>
        public int EmployeeCount
        {
            get
            {
                return employeeCount;
            }
            set
            {
                employeeCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the description of/for the Organization.
        /// </summary>
        /// <value>The description of the Organization.</value>
        /// <returns>String</returns>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// Gets or sets the timezone of/for the Organization.
        /// </summary>
        /// <value>The timezone of the Organization.</value>
        /// <returns>String</returns>
        public string Timezone
        {
            get
            {
                return timezone;
            }
            set
            {
                timezone = value;
            }
        }

        /// <summary>
        /// Gets or sets the iso code of/for the Organization.
        /// </summary>
        /// <value>The iso code of the Organization.</value>
        /// <returns>String</returns>
        public string IsoCode
        {
            get
            {
                return isoCode;
            }
            set
            {
                isoCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the street of/for the Organization.
        /// </summary>
        /// <value>The street of the Organization.</value>
        /// <returns>String</returns>
        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                street = value;
            }
        }

        /// <summary>
        /// Gets or sets the state of/for the Organization.
        /// </summary>
        /// <value>The state of the Organization. </value>
        /// <returns>String</returns>
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        /// <summary>
        /// Gets or sets the city of/for the Organization.
        /// </summary>
        /// <value>The city of the Organization.</value>
        /// <returns>String</returns>
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        /// <summary>
        /// Gets or sets the country of/for the Organization.
        /// </summary>
        /// <value>The country of the Organization.</value>
        /// <returns>String</returns>
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        /// <summary>
        /// Gets or sets the zip code of/for the Organization.
        /// </summary>
        /// <value>The zip code of the Organization.</value>
        /// <returns>String</returns>
        public string ZipCode
        {
            get
            {
                return zipCode;
            }
            set
            {
                zipCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the country code of the Organization.
        /// </summary>
        /// <value>The country code of the Organization.</value>
        /// <returns>String</returns>
        public string CountryCode
        {
            get
            {
                return countryCode;
            }
            set
            {
                countryCode = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this Organization mc status.
        /// </summary>
        /// <value><c>true</c> if mc status; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool McStatus
        {
            get
            {
                return mcStatus;
            }
            set
            {
                mcStatus = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this Organization gapps enabled.
        /// </summary>
        /// <value><c>true</c> if gapps enabled; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool GappsEnabled
        {
            get
            {
                return gappsEnabled;
            }
            set
            {
                gappsEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the currency locale of/for the Organization.
        /// </summary>
        /// <value>The currency locale of the Organization.</value>
        /// <returns>String</returns>
        public string CurrencyLocale
        {
            get
            {
                return currencyLocale;
            }

            set
            {
                currencyLocale = value;
            }
        }

        /// <summary>
        /// Gets or sets the currency symbol of/for the Organization.
        /// </summary>
        /// <value>The currency symbol of the Organization.</value>
        /// <returns>String</returns>
        public string CurrencySymbol
        {
            get
            {
                return currencySymbol;
            }
            set
            {
                currencySymbol = value;
            }
        }

        /// <summary>
        /// Gets all users of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&l;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllUsers()
        {
            return GetAllUsers(null);
        }

        /// <summary>
        /// To get all user modified after the date-time specified in the If-Modified-Since Header.
        /// </summary>
        /// <returns>BulkAPIResponse&l;ZCRMUser&gt; class instance.</returns>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display users which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMUser> GetAllUsers(string modifiedSince)
        {
            return OrganizationAPIHandler.GetInstance().GetAllUsers(modifiedSince, 1, 200);
        }

        /// <summary>
        /// To get all user of the Organization based on modifiedSince(Header), page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&l;ZCRMUser&gt; class instance.</returns>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display users which are modified after the given input datetime (String)</param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllUsers(string modifiedSince, int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllUsers(modifiedSince, page, perPage);
        }

        /// <summary>
        /// To get all confirmed users of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&l;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllConfirmedUsers()
        {
            return GetAllConfirmedUsers(1, 100);
        }

        /// <summary>
        /// To get all confirmed users of the Organization based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&l;ZCRMUser&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllConfirmedUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllConfirmedUsers(page, perPage);
        }

        /// <summary>
        /// To get all un confirmed users of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&l;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllUnConfirmedUsers()
        {
            return GetAllUnConfirmedUsers(1, 100);
        }

        /// <summary>
        /// To get all un confirmed users of the Organization based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&l;ZCRMUser&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllUnConfirmedUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllUnConfirmedUsers(page, perPage);
        }

        /// <summary>
        /// To get all deleted users of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllDeletedUsers()
        {
            return GetAllDeletedUsers(1, 200);
        }

        /// <summary>
        /// To get all deleted users of the Organization based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllDeletedUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllDeletedUsers(page, perPage);
        }

        /// <summary>
        /// To get all active confirmed users of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedUsers()
        {
            return GetAllActiveConfirmedUsers(1, 200);
        }

        /// <summary>
        /// To get all active confirmed users of the Organization based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllActiveConfirmedUsers(page, perPage);
        }

        /// <summary>
        /// To get all admin users of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllAdminUsers()
        {
            return GetAllAdminUsers(1, 200);
        }

        /// <summary>
        /// To get all admin users of the Organization based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllAdminUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllAdminUsers(page, perPage);
        }

        /// <summary>
        /// To get all active users of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllActiveUsers()
        {
            return GetAllActiveUsers(1, 200);
        }

        /// <summary>
        /// To get all active users of the Organization based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllActiveUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllActiveUsers(page, perPage);
        }

        /// <summary>
        /// To get all in active users of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllInActiveUsers()
        {
            return GetAllInActiveUsers(1, 200);
        }

        /// <summary>
        /// To get all in active users of the Organization based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllInActiveUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllDeactivatedUsers(page, perPage);
        }

        /// <summary>
        /// To get all active confirmed admins of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedAdmins()
        {
            return GetAllActiveConfirmedAdmins(1, 100);
        }

        /// <summary>
        /// To get all active confirmed admins of the Organization based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMUser&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedAdmins(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllActiveConfirmedAdmins(page, perPage);
        }

        /// <summary>
        /// To get current user of the Organization.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        public APIResponse GetCurrentUser()
        {
            return OrganizationAPIHandler.GetInstance().GetCurrentUser();
        }

        /// <summary>
        /// To get user of the Organization based on used Id.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="userId">Id (Long) of the Organization user.</param>
        public APIResponse GetUser(long userId)
        {
            return OrganizationAPIHandler.GetInstance().GetUser(userId);
        }

        /// <summary>
        /// To get all roles of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRole&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMRole> GetAllRoles()
        {
            return OrganizationAPIHandler.GetInstance().GetAllRoles();
        }

        /// <summary>
        /// To get role of the Organization based on role Id.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="roleId">Id (Long) of the Organization role.</param>
        public APIResponse GetRole(long roleId)
        {
           return OrganizationAPIHandler.GetInstance().GetRole(roleId);
        }

        /// <summary>
        /// To get all profiles of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMProfile&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMProfile> GetAllProfiles()
        {
            return OrganizationAPIHandler.GetInstance().GetAllProfiles();
        }

        /// <summary>
        /// To get profile of the Organization based on profile Id.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="profileId">Id (Long) of the Organization profile.</param>
        public APIResponse GetProfile(long profileId)
        {
            return OrganizationAPIHandler.GetInstance().GetProfile(profileId);
        }

        /// <summary>
        /// To get all taxes of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMOrgTax&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMOrgTax> GetAllTaxes()
        {
            return OrganizationAPIHandler.GetInstance().GetAllTaxes();
        }

        /// <summary>
        /// To get tax of the Organization based on tax Id.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="taxId">Id (Long) of the Organization tax.</param>
        public APIResponse GetTax(long taxId)
        {
            return OrganizationAPIHandler.GetInstance().GetTax(taxId);
        }

        /// <summary>
        /// To creates the taxes of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMOrgTax&gt; class instance.</returns>
        /// <param name="taxes">List of ZCRMOrgTax instance.</param>
        public BulkAPIResponse<ZCRMOrgTax> CreateTaxes(List<ZCRMOrgTax> taxes)
        {
            foreach (ZCRMOrgTax tax in taxes)
            {
                if (tax.Id != null)
                {
                    throw new ZCRMException("Tax ID MUST be null for create operation.");
                }
            }
            return OrganizationAPIHandler.GetInstance().CreateTaxes(taxes);
        }

        /// <summary>
        /// To updates the taxes of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMOrgTax&gt; class instance.</returns>
        /// <param name="taxes">List of ZCRMOrgTax instance.</param>
        public BulkAPIResponse<ZCRMOrgTax> UpdateTaxes(List<ZCRMOrgTax> taxes)
        {
            foreach(ZCRMOrgTax tax in taxes)
            {
                if(tax.Id == null)
                {
                    throw new ZCRMException("Tax ID MUST NOT be null for update operation.");
                }
            }
            return OrganizationAPIHandler.GetInstance().UpdateTaxes(taxes);
        }

        /// <summary>
        /// To deletes the taxes of the Organization.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMOrgTax&gt; class instance.</returns>
        /// <param name="taxIds">List of tax Ids.</param>
        public BulkAPIResponse<ZCRMOrgTax> DeleteTaxes(List<long> taxIds)
        {
            if(taxIds == null || taxIds.Count == 0)
            {
                throw new ZCRMException("Tax ID list MUST NOT be null/empty for delete operation");
            }
            return OrganizationAPIHandler.GetInstance().DeleteTaxes(taxIds);
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <returns>The user instance.</returns>
        /// <param name="userInstance">User instance.</param>
        public APIResponse CreateUser(ZCRMUser userInstance)
        {
            return OrganizationAPIHandler.GetInstance().CreateUser(userInstance);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <returns>The user instance.</returns>
        /// <param name="userInstance">User instance.</param>
        public APIResponse UpdateUser(ZCRMUser userInstance)
        {
            return OrganizationAPIHandler.GetInstance().UpdateUser(userInstance);
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <returns>The user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public APIResponse DeleteUser(long userId)
        {
            return OrganizationAPIHandler.GetInstance().DeleteUser(userId);
        }

    }
}
