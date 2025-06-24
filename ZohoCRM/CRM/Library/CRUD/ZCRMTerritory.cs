using System;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMTerritory : ZCRMEntity
    {
        private string createdTime;
        private string modifiedTime;
        private ZCRMUser manager;
        private string parent_id;
        private ZCRMCriteria criteria;
        private string name;
        private ZCRMUser modifiedBy;
        private string description;
        private long id;
        private ZCRMUser createdBy;
        private bool isManager;
        private bool subordinates;

        private ZCRMTerritory(long id)
        {
            Id = id;
        }

        public static ZCRMTerritory GetInstance(long id)
        {
            return new ZCRMTerritory(id);
        }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>The created time.</value>
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
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>The modified time.</value>
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
        /// Gets or sets the manager.
        /// </summary>
        /// <value>The manager.</value>
        public ZCRMUser Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        public string ParentId
        {
            get
            {
                return parent_id;
            }
            set
            {
                parent_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        public ZCRMCriteria Criteria
        {
            get
            {
                return criteria;
            }
            set
            {
                criteria = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
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
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
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
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
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
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
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
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
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
        /// Gets or sets a value indicating whether this <see cref="T:ZCRMSDK.CRM.Library.CRUD.ZCRMTerritory"/> is manager.
        /// </summary>
        /// <value><c>true</c> if is manager; otherwise, <c>false</c>.</value>
        public bool IsManager
        {
            get
            {
                return isManager;
            }
            set
            {
                isManager = value;
            }
        }

        public bool Subordinates
        {
            get
            {
                return subordinates;
            }
            set
            {
                subordinates = value;
            }
        }
    }
}