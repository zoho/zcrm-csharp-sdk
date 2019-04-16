using System;
using ZCRMSDK.CRM.Library.Setup.Users;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Api.Handler;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.CRMException;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMTag : ZCRMEntity
    {
        private long? id;
        private string name;
        private ZCRMUser createdBy;
        private string createdTime;
        private ZCRMUser modifiedBy;
        private string modifiedTime;
        private int count;
        private string moduleApiName;

        public ZCRMTag(long? entityId, string moduleApiName)
        {
            this.id = entityId;
            this.moduleApiName = moduleApiName;
        }

        /// <summary>
        /// To get ZCRMTag instance by passing tag Id and modules' APIName.
        /// </summary>
        /// <returns>ZCRMTag class instance.</returns>
        /// <param name="Id">Id (Long) of the tag.</param>
        /// <param name="moduleApiName">APIName (String) of the module</param>
        public static ZCRMTag GetInstance(long? Id = null,string moduleApiName= null)
        {
            return new ZCRMTag(Id, moduleApiName);
        }

        /// <summary>
        /// Gets or sets the Id of/for the tag.
        /// </summary>
        /// <value>The Id of the tag.</value>
        public long? Id
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
        /// Gets or sets the name of/for the tag.
        /// </summary>
        /// <value>The name of the tag.</value>
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
        /// Gets or sets the user who created the tag.
        /// </summary>
        /// <value>The user who created the tag.</value>
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
        /// Gets or sets the created time of/for the tag.
        /// </summary>
        /// <value>The created time of the tag.</value>
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
        /// Gets or sets the user who modified the tag.
        /// </summary>
        /// <value>The user who modified the tag.</value>
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
        /// Gets or sets the modified time of/for the tag.
        /// </summary>
        /// <value>The modified time of the tag.</value>
        /// <returns>ZCRMUser class instance</returns>
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
        /// Gets or sets the count of/for the tag.
        /// </summary>
        /// <value>The count of the tag.</value>
        /// <returns>Integer</returns>
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        /// <summary>
        /// Gets or set the APIName of/for the module.
        /// </summary>
        /// <value>The name of the module API.</value>
        public string ModuleApiName
        {
            get
            {
                return moduleApiName;
            }
            set
            {
                moduleApiName = value;
            }
        }

        /// <summary>
        /// To delete tag specified tag Id.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        public APIResponse Delete()
        {
            if (this.id == null || this.id == 0)
            {
                throw new ZCRMException("Tag ID MUST NOT be null/empty for Delete operation");
            }
            return TagAPIHandler.GetInstance().Delete(this.id);
        }

        /// <summary>
        /// To merge specified ZCRMTag class instance.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="mergetag">ZCRMTag class instance</param>
        public APIResponse Merge(ZCRMTag mergetag)
        {
            if (this.id == null || this.id == 0)
            {
                throw new ZCRMException("Tag ID MUST NOT be null/empty for Merge operation");
            }
            if (mergetag.id == null || mergetag.id == 0)
            {
                throw new ZCRMException("Merge Tag ID MUST NOT be null/empty for Merge operation");
            }
            return TagAPIHandler.GetInstance().Merge(this.Id, mergetag.Id);
        }

        /// <summary>
        /// To update ZCRMTag.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        public APIResponse Update()
        {
            if (this.id == null || this.id == 0)
            {
                throw new ZCRMException("Tag ID MUST NOT be null/empty for Update operation");
            }
            if (string.IsNullOrEmpty(this.moduleApiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for Update operation");
            }
            if (string.IsNullOrEmpty(this.name))
            {
                throw new ZCRMException("Tag Name MUST NOT be null/empty for Update operation");
            }
            return TagAPIHandler.GetInstance().Update(this);
        }
    }
}
