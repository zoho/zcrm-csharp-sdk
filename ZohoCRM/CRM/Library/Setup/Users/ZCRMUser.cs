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

        /// <summary>
        /// To get ZohoCRM user instance by passing user Id and user full name
        /// </summary>
        /// <returns>ZCRMUser class instance.</returns>
        /// <param name="userId">Id (Long) of the user</param>
        /// <param name="fullName">Name (String) of the user</param>
        public static ZCRMUser GetInstance(long userId, string fullName)
        {
            return new ZCRMUser(userId, fullName);
        }

        /// <summary>
        /// To get ZohoCRM user instance by passing user Id.
        /// </summary>
        /// <returns>ZCRMUser class instance.</returns>
        /// <param name="userId">Id (Long) of the user</param>
        public static ZCRMUser GetInstance(long userId)
        {
            return new ZCRMUser(userId, null);
        }

        /// <summary>
        /// Gets the user Id.
        /// </summary>
        /// <value>Id of the user.</value>
        /// <returns>Long</returns>
        public long Id { get => id; private set => id = value; }

        /// <summary>
        /// Gets or sets the Zoho Unique Id of/for the user.
        /// </summary>
        /// <value>The Zoho Unique Id of the user.</value>
        /// <returns>Long</returns>
        public long? ZuId { get => zuId; set => zuId = value; }

        /// <summary>
        /// Gets or sets the first name of/for the user.
        /// </summary>
        /// <value>the first name for the user.</value>
        /// <returns>String</returns>
        public string FirstName { get => firstName; set => firstName = value; }

        /// <summary>
        /// Gets or sets the last name of/for the user.
        /// </summary>
        /// <value>The last name of the user.</value>
        /// <returns>String</returns>
        public string LastName { get => lastName; set => lastName = value; }

        /// <summary>
        /// Gets or sets the full name of/for the user.
        /// </summary>
        /// <value>The full name of the user.</value>
        /// <returns>String</returns>
        public string FullName { get => fullName; set => fullName = value; }

        /// <summary>
        /// Gets or sets the email Id of/for the user.
        /// </summary>
        /// <value>The email Id of the user.</value>
        /// <returns>String</returns>
        public string EmailId { get => emailId; set => emailId = value; }

        /// <summary>
        /// Gets or sets the mobile number of/for the user.
        /// </summary>
        /// <value>The mobile number of the user.</value>
        /// <returns>String</returns>
        public string Mobile { get => mobile; set => mobile = value; }

        /// <summary>
        /// Gets or sets the language of/for the user.
        /// </summary>
        /// <value>The language of the user.</value>
        /// <returns>String</returns>
        public string Language { get => language; set => language = value; }

        /// <summary>
        /// Gets or sets the alias name of/for the user.
        /// </summary>
        /// <value>The alias name of the user.</value>
        /// <returns>String</returns>
        public string Alias { get => alias; set => alias = value; }

        /// <summary>
        /// Gets or sets the city of/for the user.
        /// </summary>
        /// <value>The city of the user.</value>
        /// <returns>String</returns>
        public string City { get => city; set => city = value; }

        /// <summary>
        /// Gets or sets the country of/for the user.
        /// </summary>
        /// <value>The country of the user.</value>
        /// <returns>String</returns>
        public string Country { get => country; set => country = value; }

        /// <summary>
        /// Gets or sets the country locale of/for the user .
        /// </summary>
        /// <value>The country locale of the user.</value>
        /// <returns>String</returns>
        public string CountryLocale { get => countryLocale; set => countryLocale = value; }

        /// <summary>
        /// Gets or sets the fax of/for the user.
        /// </summary>
        /// <value>The fax of the user.</value>
        /// <returns>String</returns>
        public string Fax { get => fax; set => fax = value; }

        /// <summary>
        /// Gets or sets the date format of/for the user.
        /// </summary>
        /// <value>The date format of the user.</value>
        /// <returns>String</returns>
        public string DateFormat { get => dateFormat; set => dateFormat = value; }

        /// <summary>
        /// Gets or sets the time format of/for the user.
        /// </summary>
        /// <value>The time format of the user.</value>
        /// <returns>String</returns>
        public string TimeFormat { get => timeFormat; set => timeFormat = value; }

        /// <summary>
        /// Gets or sets the date of birth of/for the user.
        /// </summary>
        /// <value>The date of birth of the user.</value>
        /// <returns>String</returns>
        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        /// <summary>
        /// Gets or sets the locale of/for the user.
        /// </summary>
        /// <value>The locale of the user.</value>
        /// <returns>String</returns>
        public string Locale { get => locale; set => locale = value; }

        /// <summary>
        /// Gets or sets the name format of/for the user.
        /// </summary>
        /// <value>The name format of the user.</value>
        /// <returns>String</returns>
        public string NameFormat { get => nameFormat; set => nameFormat = value; }

        /// <summary>
        /// Gets or sets the phone number of/for the user.
        /// </summary>
        /// <value>The phone number of the user.</value>
        /// <returns>String</returns>
        public string Phone { get => phone; set => phone = value; }

        /// <summary>
        /// Gets or sets the state of/for the user.
        /// </summary>
        /// <value>The state of the user.</value>
        /// <returns>String</returns>
        public string State { get => state; set => state = value; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is confirm.
        /// </summary>
        /// <value><c>true</c> if confirm; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Confirm { get => confirm; set => confirm = value; }

        /// <summary>
        /// Gets or sets the street of/for the user.
        /// </summary>
        /// <value>The street of the user.</value>
        /// <returns>String</returns>
        public string Street { get => street; set => street = value; }

        /// <summary>
        /// Gets or sets the time zone of/for the user.
        /// </summary>
        /// <value>The time zone of the user.</value>
        /// <returns>String</returns>
        public string TimeZone { get => timeZone; set => timeZone = value; }

        /// <summary>
        /// Gets or sets the website of/for the user.
        /// </summary>
        /// <value>The website of the user.</value>
        /// <returns>String</returns>
        public string Website { get => website; set => website = value; }

        /// <summary>
        /// Gets or sets the status of/for the user.
        /// </summary>
        /// <value>The status of the user.</value>
        /// <returns>String</returns>
        public string Status { get => status; set => status = value; }

        /// <summary>
        /// Gets or sets the profile of/for the user.
        /// </summary>
        /// <value>The profile of the user.</value>
        /// <returns>ZCRMProfile class instance</returns>
        public ZCRMProfile Profile { get => profile; set => profile = value; }

        /// <summary>
        /// Gets or sets the role of/for the user.
        /// </summary>
        /// <value>The role of the user.</value>
        /// <returns>ZCRMRole class instance</returns>
        public ZCRMRole Role { get => role; set => role = value; }

        /// <summary>
        /// Gets or sets the user to which the current user created.
        /// </summary>
        /// <value>The user to which the current user created.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        /// <summary>
        /// Gets or sets the created time of/for the user.
        /// </summary>
        /// <value>The created time of the user.</value>
        /// <returns>String</returns>
        public string CreatedTime { get => createdTime; set => createdTime = value; }

        /// <summary>
        /// Gets or sets the user to which the current user modified.
        /// </summary>
        /// <value>The user to which the current user modified.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        /// <summary>
        /// Gets or sets the modified time of/for the user.
        /// </summary>
        /// <value>The modified time of the user.</value>
        /// <returns>String</returns>
        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        /// <summary>
        /// Gets or sets the user to which the current user reporting to.
        /// </summary>
        /// <value>The user to which the current user reporting to.</value>
        /// <value>ZCRMUser class instance</value>
        public ZCRMUser ReportingTo { get => reportingTo; set => reportingTo = value; }

        /// <summary>
        /// To downloads the profile pic of the user.
        /// </summary>
        /// <returns>FileAPIResponse class instance</returns>
        public FileAPIResponse DownloadProfilePic()
        {
            return DownloadProfilePic(null);
        }

        /// <summary>
        /// To downloads the profile pic of the user based on photo size.
        /// </summary>
        /// <returns>FileAPIResponse class instance.</returns>
        /// <param name="photoSize">Photo size support (CommonUtil.PhotoSize {stamp, thumb, original, favicon, medium)} <example>CommonUtil.PhotoSize.original</example></param>
        public FileAPIResponse DownloadProfilePic(CommonUtil.PhotoSize? photoSize)
        {
            return OrganizationAPIHandler.GetInstance().DownloadUserPhoto(photoSize);
        }
    }
}
