using ZCRMSDK.CRM.Library.Api.Handler;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMUser : ZCRMEntity
    {

        private long id;
        private long? zuId;
        private string firstName;
        private string lastName;
        private string fullName;
        private string alias;
        private string dateOfBirth;
        private string mobile;
        private string phone;
        private string emailId;
        private string fax;
        private string street;
        private string city;
        private string state;
        private string country;
        private string language;
        private string locale;
        private string countryLocale;
        private string nameFormat;
        private string dateFormat;
        private string timeFormat;
        private string timeZone;
        private string website;
        private bool confirm;
        private string status;
        private ZCRMRole role;
        private ZCRMProfile profile;
        private ZCRMUser createdBy;
        private ZCRMUser modifiedBy;
        private string createdTime;
        private string modifiedTime;
        private ZCRMUser reportingTo;


        private ZCRMUser(long userId, string fullName)
        {
            Id = userId;
            FullName = fullName;

        }

        public static ZCRMUser GetInstance(long userId, string fullName)
        {
            return new ZCRMUser(userId, fullName);
        }

        public static ZCRMUser GetInstance(long userId)
        {
            return new ZCRMUser(userId, null);
        }

        public long Id { get => id; private set => id = value; }

        public long? ZuId { get => zuId; set => zuId = value; }

        public string FirstName { get => firstName; set => firstName = value; }

        public string LastName { get => lastName; set => lastName = value; }

        public string FullName { get => fullName; set => fullName = value; }

        public string EmailId { get => emailId; set => emailId = value; }

        public string Mobile { get => mobile; set => mobile = value; }

        public string Language { get => language; set => language = value; }

        public string Alias { get => alias; set => alias = value; }

        public string City { get => city; set => city = value; }

        public string Country { get => country; set => country = value; }

        public string CountryLocale { get => countryLocale; set => countryLocale = value; }

        public string Fax { get => fax; set => fax = value; }

        public string DateFormat { get => dateFormat; set => dateFormat = value; }

        public string TimeFormat { get => timeFormat; set => timeFormat = value; }

        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        public string Locale { get => locale; set => locale = value; }

        public string NameFormat { get => nameFormat; set => nameFormat = value; }

        public string Phone { get => phone; set => phone = value; }

        public string State { get => state; set => state = value; }

        public bool Confirm { get => confirm; set => confirm = value; }

        public string Street { get => street; set => street = value; }

        public string TimeZone { get => timeZone; set => timeZone = value; }

        public string Website { get => website; set => website = value; }

        public string Status { get => status; set => status = value; }

        public ZCRMProfile Profile { get => profile; set => profile = value; }

        public ZCRMRole Role { get => role; set => role = value; }
       
        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        public string CreatedTime { get => createdTime; set => createdTime = value; }

        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        public ZCRMUser ReportingTo { get => reportingTo; set => reportingTo = value; }


        public FileAPIResponse DownloadProfilePic()
        {
            return DownloadProfilePic(null);
        }

        public FileAPIResponse DownloadProfilePic(CommonUtil.PhotoSize? photoSize)
        {
            return OrganizationAPIHandler.GetInstance().DownloadUserPhoto(photoSize);
        }
    }
}
