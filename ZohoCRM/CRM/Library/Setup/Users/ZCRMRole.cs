using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMRole : ZCRMEntity
    {

        private long id;
        private string name;
        private string label;
        private bool isAdminUser;
        private ZCRMRole reportingTo;

        private ZCRMUser forecastManager;
        private bool shareWithPeers;
        private string description;

        private ZCRMRole(long roleId, string roleName)
        {
            Id = roleId;
            Name = roleName;
        }

        /// <summary>
        /// To get ZohoCRM role instance by passing role Id and role name.
        /// </summary>
        /// <returns>ZCRMRole class instance.</returns>
        /// <param name="roleId">Id (Long) of the role</param>
        /// <param name="roleName">Name (String) of the role</param>
        public static ZCRMRole GetInstance(long roleId, string roleName)
        {
            return new ZCRMRole(roleId, roleName);
        }

        /// <summary>
        /// Gets the role Id.
        /// </summary>
        /// <value>Id of the role.</value>
        /// <returns>Long</returns>
        public long Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
            }
        }

        /// <summary>
        /// Gets the role name.
        /// </summary>
        /// <value>Name of the role.</value>
        /// <returns>String</returns>
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the display label of/for the role.
        /// </summary>
        /// <value>The display label of the role</value>
        /// <returns>String</returns>
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this role is admin user.
        /// </summary>
        /// <value><c>true</c> if admin user; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool AdminUser
        {
            get
            {
                return isAdminUser;
            }
            set
            {
                isAdminUser = value;
            }
        }

        /// <summary>
        /// Gets or sets the role to which the current role reporting to.
        /// </summary>
        /// <value>The role to which the current role reporting to.</value>
        /// <returns>ZCRMRole class instance</returns>
        public ZCRMRole ReportingTo
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

        public ZCRMUser ForecastManager
        {
            get
            {
                return forecastManager;
            }
            set
            {
                forecastManager = value;
            }
        }

        public bool ShareWithPeers
        {
            get
            {
                return shareWithPeers;
            }
            set
            {
                shareWithPeers = value;
            }
        }

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

    }
}
