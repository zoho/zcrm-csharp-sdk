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

        public static ZCRMOrganization GetInstance()
        {
            return new ZCRMOrganization(null, null);
        }

        public static ZCRMOrganization GetInstance(string orgName, long orgId)
        {
            return new ZCRMOrganization(orgName, orgId);
        }

        public string CompanyName { get => companyName; private set => companyName = value; }
        public string Alias { get => alias; set => alias = value; }
        public long? OrgId { get => orgId; set => orgId = value; }
        public long PrimaryZuid { get => primaryZuid; set => primaryZuid = value; }
        public long Zgid { get => zgid; set => zgid = value; }
        public string PrimaryEmail { get => primaryEmail; set => primaryEmail = value; }
        public string Website { get => website; set => website = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Fax { get => fax; set => fax = value; }
        public int EmployeeCount { get => employeeCount; set => employeeCount = value; }
        public string Description { get => description; set => description = value; }
        public string Timezone { get => timezone; set => timezone = value; }
        public string IsoCode { get => isoCode; set => isoCode = value; }
        public string Street { get => street; set => street = value; }
        public string State { get => state; set => state = value; }
        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public string ZipCode { get => zipCode; set => zipCode = value; }
        public string CountryCode { get => countryCode; set => countryCode = value; }
        public bool McStatus { get => mcStatus; set => mcStatus = value; }
        public bool GappsEnabled { get => gappsEnabled; set => gappsEnabled = value; }
        public string CurrencyLocale { get => currencyLocale; set => currencyLocale = value; }
        public string CurrencySymbol { get => currencySymbol; set => currencySymbol = value; }





        public BulkAPIResponse<ZCRMUser> GetAllUsers()
        {
            return GetAllUsers(null);
        }


        public BulkAPIResponse<ZCRMUser> GetAllUsers(string modifiedSince)
        {
            return OrganizationAPIHandler.GetInstance().GetAllUsers(modifiedSince, 1, 200);
        }

        public BulkAPIResponse<ZCRMUser> GetAllUsers(string modifiedSince, int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllUsers(modifiedSince, page, perPage);
        }



        public BulkAPIResponse<ZCRMUser> GetAllConfirmedUsers()
        {
            return GetAllConfirmedUsers(1, 100);
        }

        public BulkAPIResponse<ZCRMUser> GetAllConfirmedUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllConfirmedUsers(page, perPage);
        }



        public BulkAPIResponse<ZCRMUser> GetAllUnConfirmedUsers()
        {
            return GetAllUnConfirmedUsers(1, 100);
        }

        public BulkAPIResponse<ZCRMUser> GetAllUnConfirmedUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllUnConfirmedUsers(page, perPage);
        }


        public BulkAPIResponse<ZCRMUser> GetAllDeletedUsers()
        {
            return GetAllDeletedUsers(1, 200);
        }

        public BulkAPIResponse<ZCRMUser> GetAllDeletedUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllDeletedUsers(page, perPage);
        }


        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedUsers()
        {
            return OrganizationAPIHandler.GetInstance().GetAllActiveConfirmedUsers(1, 200);
        }

        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllActiveConfirmedUsers(page, perPage);
        }

       
        public BulkAPIResponse<ZCRMUser> GetAllAdminUsers()
        {
            return OrganizationAPIHandler.GetInstance().GetAllAdminUsers(1, 200);
        }

        public BulkAPIResponse<ZCRMUser> GetAllAdminUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllAdminUsers(page, perPage);
        }


        public BulkAPIResponse<ZCRMUser> GetAllActiveUsers()
        {
            return OrganizationAPIHandler.GetInstance().GetAllActiveUsers(1, 200);
        }

        public BulkAPIResponse<ZCRMUser> GetAllActiveUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllActiveUsers(page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllInActiveUsers()
        {
            return OrganizationAPIHandler.GetInstance().GetAllDeactivatedUsers(1, 200);
        }

        public BulkAPIResponse<ZCRMUser> GetAllInActiveUsers(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllDeactivatedUsers(page, perPage);
        }

        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedAdmins()
        {
            return GetAllActiveConfirmedAdmins(1, 100);
        }

        public BulkAPIResponse<ZCRMUser> GetAllActiveConfirmedAdmins(int page, int perPage)
        {
            return OrganizationAPIHandler.GetInstance().GetAllActiveConfirmedAdmins(page, perPage);
        }

        public APIResponse GetCurrentUser()
        {
            return OrganizationAPIHandler.GetInstance().GetCurrentUser();
        }


        public APIResponse GetUser(long userId)
        {
            return OrganizationAPIHandler.GetInstance().GetUser(userId);
        }


        public BulkAPIResponse<ZCRMRole> GetAllRoles()
        {
            return OrganizationAPIHandler.GetInstance().GetAllRoles();
        }



        public APIResponse GetRole(long roleId)
        {
           return OrganizationAPIHandler.GetInstance().GetRole(roleId);
        }

        public BulkAPIResponse<ZCRMProfile> GetAllProfiles()
        {
            return OrganizationAPIHandler.GetInstance().GetAllProfiles();
        }



        public APIResponse GetProfile(long profileId)
        {
            return OrganizationAPIHandler.GetInstance().GetProfile(profileId);
        }


        public BulkAPIResponse<ZCRMOrgTax> GetAllTaxes()
        {
            return OrganizationAPIHandler.GetInstance().GetAllTaxes();
        }



        public APIResponse GetTax(long taxId)
        {
            return OrganizationAPIHandler.GetInstance().GetTax(taxId);
        }


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
       
        public BulkAPIResponse<ZCRMOrgTax> DeleteTaxes(List<long> taxIds)
        {
            if(taxIds == null || taxIds.Count == 0)
            {
                throw new ZCRMException("Tax ID list MUST NOT be null/empty for delete operation");
            }
            return OrganizationAPIHandler.GetInstance().DeleteTaxes(taxIds);
        }

    }
}
