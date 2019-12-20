using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Api.Handler;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;

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

        private bool microsoft;
        private int number;
        private long offset;
        private string signature;
        private ZCRMUserCustomizeInfo customizeInfo;
        private bool isPersonalAccount;
        private string defaultTabGroup;
        private ZCRMUserTheme theme;
        private string zip;
        private string decimalSeparator;
        private List<ZCRMTerritory> territories;
        private bool isOnline;
        private string currency;
        private Dictionary<string, object> fieldNameVsValue = new Dictionary<string, object>();
        public static List<string> defaultKeys = new List<string>() { "Reporting_To", "Currency", "Modified_Time", "created_time", "territories", "reporting_to", "Isonline", "created_by", "Modified_By", "country", "id", "name", "role", "customize_info", "city", "signature", "name_format", "language", "locale", "personal_account", "default_tab_group", "alias", "street", "theme", "state", "country_locale", "fax", "first_name", "email", "zip", "decimal_separator", "website", "time_format", "profile", "mobile", "last_name", "time_zone", "zuid", "confirm", "full_name", "phone", "dob", "date_format", "status", "microsoft" };

        private ZCRMUser() { }

        private ZCRMUser(long userId, string fullName)
        {
            Id = userId;
            FullName = fullName;

        }
        private ZCRMUser(string last_name, string email)
        {
            LastName = last_name;
            EmailId = email;

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
        /// To get ZohoCRM user instance
        /// </summary>
        /// <returns>ZCRMUser class instance.</returns>
        public static ZCRMUser GetInstance()
        {
            return new ZCRMUser();
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
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="last_name">Last name.</param>
        /// <param name="email">Email.</param>
        public static ZCRMUser GetInstance(string last_name, string email)
        {
            return new ZCRMUser(last_name, email);
        }

        /// <summary>
        /// Gets the user Id.
        /// </summary>
        /// <value>Id of the user.</value>
        /// <returns>Long</returns>
        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the Zoho Unique Id of/for the user.
        /// </summary>
        /// <value>The Zoho Unique Id of the user.</value>
        /// <returns>Long</returns>
        public long? ZuId
        {
            get
            {
                return zuId;
            }
            set
            {
                zuId = value;
            }
        }

        /// <summary>
        /// Gets or sets the first name of/for the user.
        /// </summary>
        /// <value>the first name for the user.</value>
        /// <returns>String</returns>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        /// <summary>
        /// Gets or sets the last name of/for the user.
        /// </summary>
        /// <value>The last name of the user.</value>
        /// <returns>String</returns>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        /// <summary>
        /// Gets or sets the full name of/for the user.
        /// </summary>
        /// <value>The full name of the user.</value>
        /// <returns>String</returns>
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
            }
        }

        /// <summary>
        /// Gets or sets the email Id of/for the user.
        /// </summary>
        /// <value>The email Id of the user.</value>
        /// <returns>String</returns>
        public string EmailId
        {
            get
            {
                return emailId;
            }
            set
            {
                emailId = value;
            }
        }

        /// <summary>
        /// Gets or sets the mobile number of/for the user.
        /// </summary>
        /// <value>The mobile number of the user.</value>
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
        /// Gets or sets the language of/for the user.
        /// </summary>
        /// <value>The language of the user.</value>
        /// <returns>String</returns>
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }

        /// <summary>
        /// Gets or sets the alias name of/for the user.
        /// </summary>
        /// <value>The alias name of the user.</value>
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
        /// Gets or sets the city of/for the user.
        /// </summary>
        /// <value>The city of the user.</value>
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
        /// Gets or sets the country of/for the user.
        /// </summary>
        /// <value>The country of the user.</value>
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
        /// Gets or sets the country locale of/for the user .
        /// </summary>
        /// <value>The country locale of the user.</value>
        /// <returns>String</returns>
        public string CountryLocale
        {
            get
            {
                return countryLocale;
            }
            set
            {
                countryLocale = value;
            }
        }

        /// <summary>
        /// Gets or sets the fax of/for the user.
        /// </summary>
        /// <value>The fax of the user.</value>
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
        /// Gets or sets the date format of/for the user.
        /// </summary>
        /// <value>The date format of the user.</value>
        /// <returns>String</returns>
        public string DateFormat
        {
            get
            {
                return dateFormat;
            }
            set
            {
                dateFormat = value;
            }
        }

        /// <summary>
        /// Gets or sets the time format of/for the user.
        /// </summary>
        /// <value>The time format of the user.</value>
        /// <returns>String</returns>
        public string TimeFormat
        {
            get
            {
                return timeFormat;
            }
            set
            {
                timeFormat = value;
            }
        }

        /// <summary>
        /// Gets or sets the date of birth of/for the user.
        /// </summary>
        /// <value>The date of birth of the user.</value>
        /// <returns>String</returns>
        public string DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
            }
        }

        /// <summary>
        /// Gets or sets the locale of/for the user.
        /// </summary>
        /// <value>The locale of the user.</value>
        /// <returns>String</returns>
        public string Locale
        {
            get
            {
                return locale;
            }
            set
            {
                locale = value;
            }
        }

        /// <summary>
        /// Gets or sets the name format of/for the user.
        /// </summary>
        /// <value>The name format of the user.</value>
        /// <returns>String</returns>
        public string NameFormat
        {
            get
            {
                return nameFormat;
            }
            set
            {
                nameFormat = value;
            }
        }

        /// <summary>
        /// Gets or sets the phone number of/for the user.
        /// </summary>
        /// <value>The phone number of the user.</value>
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
        /// Gets or sets the state of/for the user.
        /// </summary>
        /// <value>The state of the user.</value>
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
        /// Gets or sets a value indicating whether this user is confirm.
        /// </summary>
        /// <value><c>true</c> if confirm; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Confirm
        {
            get
            {
                return confirm;
            }
            set
            {
                confirm = value;
            }
        }

        /// <summary>
        /// Gets or sets the street of/for the user.
        /// </summary>
        /// <value>The street of the user.</value>
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
        /// Gets or sets the time zone of/for the user.
        /// </summary>
        /// <value>The time zone of the user.</value>
        /// <returns>String</returns>
        public string TimeZone
        {
            get
            {
                return timeZone;
            }
            set
            {
                timeZone = value;
            }
        }

        /// <summary>
        /// Gets or sets the website of/for the user.
        /// </summary>
        /// <value>The website of the user.</value>
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
        /// Gets or sets the status of/for the user.
        /// </summary>
        /// <value>The status of the user.</value>
        /// <returns>String</returns>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// Gets or sets the profile of/for the user.
        /// </summary>
        /// <value>The profile of the user.</value>
        /// <returns>ZCRMProfile class instance</returns>
        public ZCRMProfile Profile
        {
            get
            {
                return profile;
            }
            set
            {
                profile = value;
            }
        }

        /// <summary>
        /// Gets or sets the role of/for the user.
        /// </summary>
        /// <value>The role of the user.</value>
        /// <returns>ZCRMRole class instance</returns>
        public ZCRMRole Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
            }
        }

        /// <summary>
        /// Gets or sets the user to which the current user created.
        /// </summary>
        /// <value>The user to which the current user created.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        /// <summary>
        /// Gets or sets the created time of/for the user.
        /// </summary>
        /// <value>The created time of the user.</value>
        /// <returns>String</returns>
        public string CreatedTime
        {
            get
            {
                return createdTime;
            }
            set
            {
                createdTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the user to which the current user modified.
        /// </summary>
        /// <value>The user to which the current user modified.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
            }
        }

        /// <summary>
        /// Gets or sets the modified time of/for the user.
        /// </summary>
        /// <value>The modified time of the user.</value>
        /// <returns>String</returns>
        public string ModifiedTime
        {
            get
            {
                return modifiedTime;
            }
            set
            {
                modifiedTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the user to which the current user reporting to.
        /// </summary>
        /// <value>The user to which the current user reporting to.</value>
        /// <value>ZCRMUser class instance</value>
        public ZCRMUser ReportingTo
        {
            get
            {
                return reportingTo;
            }
            set
            {
                reportingTo = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:ZCRMSDK.CRM.Library.Setup.Users.ZCRMUser"/> is microsoft.
        /// </summary>
        /// <value><c>true</c> if microsoft; otherwise, <c>false</c>.</value>
        public bool MicrosoftAppUser
        {
            get
            {
                return microsoft;
            }
            set
            {
                microsoft = value;
            }
        }

        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>The signature.</value>
        public string Signature
        {
            get
            {
                return signature;
            }
            set
            {
                signature = value;
            }
        }

        /// <summary>
        /// Gets or sets the customize info.
        /// </summary>
        /// <value>The customize info.</value>
        public ZCRMUserCustomizeInfo CustomizeInfo
        {
            get
            {
                return customizeInfo;
            }
            set
            {
                customizeInfo = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:ZCRMSDK.CRM.Library.Setup.Users.ZCRMUser"/> is
        /// personal account.
        /// </summary>
        /// <value><c>true</c> if is personal account; otherwise, <c>false</c>.</value>
        public bool IsPersonalAccount
        {
            get
            {
                return isPersonalAccount;
            }
            set
            {
                isPersonalAccount = value;
            }
        }

        /// <summary>
        /// Gets or sets the default tab group.
        /// </summary>
        /// <value>The default tab group.</value>
        public string DefaultTabGroup
        {
            get
            {
                return defaultTabGroup;
            }
            set
            {
                defaultTabGroup = value;
            }
        }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        /// <value>The theme.</value>
        public ZCRMUserTheme Theme
        {
            get
            {
                return theme;
            }
            set
            {
                theme = value;
            }
        }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>The zip.</value>
        public string Zip
        {
            get
            {
                return zip;
            }
            set
            {
                zip = value;
            }
        }

        /// <summary>
        /// Gets or sets the decimal separator.
        /// </summary>
        /// <value>The decimal separator.</value>
        public string DecimalSeparator
        {
            get
            {
                return decimalSeparator;
            }
            set
            {
                decimalSeparator = value;
            }
        }

        /// <summary>
        /// Gets or sets the territories.
        /// </summary>
        /// <value>The territories.</value>
        public List<ZCRMTerritory> Territories
        {
            get
            {
                return territories;
            }
            set
            {
                territories = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:ZCRMSDK.CRM.Library.Setup.Users.ZCRMUser"/> is online.
        /// </summary>
        /// <value><c>true</c> if is online; otherwise, <c>false</c>.</value>
        public bool IsOnline
        {
            get
            {
                return isOnline;
            }
            set
            {
                isOnline = value;
            }
        }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>The currency.</value>
        public string Currency
        {
            get
            {
                return currency;
            }
            set
            {
                currency = value;
            }
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public Dictionary<string, object> Data
        {
            get
            {
                return fieldNameVsValue;
            }
            set
            {
                fieldNameVsValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        /// <summary>
        /// Gets or sets the off set.
        /// </summary>
        /// <value>The off set.</value>
        public long OffSet
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }
        /// <summary>
        /// Sets the field value.
        /// </summary>
        /// <param name="fieldAPIName">Field APIN ame.</param>
        /// <param name="value">Value.</param>
        public void SetFieldValue(string fieldAPIName, object value)
        {
            Data.Add(fieldAPIName, value);
        }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <returns>The field value.</returns>
        /// <param name="fieldAPIName">Field APIN ame.</param>
        public object GetFieldValue(string fieldAPIName)
        {
            if (Data.ContainsKey(fieldAPIName))
            {
                if (Data[fieldAPIName] == null) { return null; }
                return Data[fieldAPIName];
            }
            throw new ZCRMException("The given field is not present in this user record - " + fieldAPIName);
        }
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