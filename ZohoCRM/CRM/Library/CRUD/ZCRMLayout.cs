using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMLayout : ZCRMEntity
    {

        private long id;
        private string name;
        private ZCRMUser createdBy;
        private ZCRMUser modifiedBy;
        private string createdTime;
        private string modifiedTime;
        private bool visible;
        private int status;
        private List<ZCRMSection> sections = new List<ZCRMSection>();
        private List<ZCRMProfile> accessibleProfies = new List<ZCRMProfile>();
        private Dictionary<string, ZCRMLeadConvertMapping> convertMapping;

        private ZCRMLayout(long layoutId)
        {
            Id = layoutId;
        }

        /// <summary>
        /// To get ZCRMLayout instance by passing layout Id.
        /// </summary>
        /// <returns>ZCRMLayout class instance.</returns>
        /// <param name="layoutId">Id (Long) of the layout</param>
        public static ZCRMLayout GetInstance(long layoutId)
        {
            return new ZCRMLayout(layoutId);    
        }

        /// <summary>
        /// Gets the layout Id.
        /// </summary>
        /// <value>The Id of the layout.</value>
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
        /// Gets or sets the name of/for the layout.
        /// </summary>
        /// <value>The name of the layout.</value>
        /// <returns>String</returns>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who created the layout.
        /// </summary>
        /// <value>The user who created the layout.</value>
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
        /// Gets or sets the created time of/for the layout.
        /// </summary>
        /// <value>The created time of the layout.</value>
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
        /// Gets or sets the user who modified the layout.
        /// </summary>
        /// <value>The user who modified the layout.</value>
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
        /// Gets or sets the modified time of/for the layout.
        /// </summary>
        /// <value>The modified time of the layout.</value>
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
        /// Gets or sets a value indicating whether this layout is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        /// <summary>
        /// Gets or sets the status of/for the layout.
        /// </summary>
        /// <value>The status of the layout.</value>
        /// <returns>Integer</returns>
        public int Status
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
        /// Gets the accessible profies of the layout.
        /// </summary>
        /// <value>The accessible profies of the layout.</value>
        /// <returns>List of ZCRMProfile class instance</returns>
        public List<ZCRMProfile> AccessibleProfies
        {
            get
            {
                return accessibleProfies;
            }
            private set
            {
                accessibleProfies = value;
            }
        }

        /// <summary>
        /// Gets or sets the sections of/for the layout.
        /// </summary>
        /// <value>The sections of the layout.</value>
        /// <returns>List of ZCRMSection class instance</returns>
        public List<ZCRMSection> Sections
        {
            get
            {
                return sections;
            }
            set
            {
                sections = value;
            }
        }

        /// <summary>
        /// Gets or sets the convert mapping.
        /// </summary>
        /// <value>The convert mapping.</value>
        public Dictionary<string, ZCRMLeadConvertMapping> ConvertMapping
        {
            get
            {
                return convertMapping;
            }
            set
            {
                convertMapping = value;
            }
        }

        /// <summary>
        /// To add the accessible profiles of the layout based on ZCRMProfile class instance.
        /// </summary>
        /// <param name="profile">ZCRMProfile class instance</param>
        public void AddAccessibleProfiles(ZCRMProfile profile)
        {
            AccessibleProfies.Add(profile);
        }

        /// <summary>
        /// To add the section of the layout based on ZCRMSection class instance.
        /// </summary>
        /// <param name="section">ZCRMSection class instance</param>
        public void AddSection(ZCRMSection section)
        {
            Sections.Add(section);
        }
    }
}
