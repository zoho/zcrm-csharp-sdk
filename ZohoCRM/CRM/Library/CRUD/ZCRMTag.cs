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
        public static ZCRMTag GetInstance(long? entityId = null,string moduleApiName= null)
        {
            return new ZCRMTag(entityId, moduleApiName);
        }

        public long? Id { get => id; set => id = value; }

        public string Name { get => name; set => name = value; }

        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        public string CreatedTime { get => createdTime; set => createdTime = value; }

        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        public int Count { get => count; set =>count = value; }

        public string ModuleApiName { get => moduleApiName; set => moduleApiName = value; }

        public APIResponse Delete()
        {
            if (this.id == null || this.id == 0)
            {
                throw new ZCRMException("Tag ID MUST NOT be null/empty for Delete operation");
            }
            return TagAPIHandler.GetInstance().Delete(this.id);
        }

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
