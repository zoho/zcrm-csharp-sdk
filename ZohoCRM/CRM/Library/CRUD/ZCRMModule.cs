using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Api.Handler;
using ZCRMSDK.CRM.Library.Setup.Users;
using ZCRMSDK.CRM.Library.CRMException;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMModule : ZCRMEntity
    {
        private string systemName;
        private string apiName;
        private string singularLabel;
        private string pluralLabel;
        private long id;
        private bool customModule;
        private bool creatable;
        private bool viewable;
        private bool convertable;
        private bool deletable;
        private bool editable;
        private bool apiSupported;
        private ZCRMUser modifiedBy;
        private string modifiedTime;

        private List<ZCRMLayout> layouts = new List<ZCRMLayout>();
        private List<ZCRMModuleRelation> relatedLists = new List<ZCRMModuleRelation>();

        private List<string> bussinessCardFields = new List<string>();
        private List<ZCRMProfile> accessibleProfiles = new List<ZCRMProfile>();

        protected ZCRMModule(string apiName)
        {
            ApiName = apiName;
        }


        public static ZCRMModule GetInstance(string apiName)
        {
            return new ZCRMModule(apiName);
        }

        public string ApiName { get => apiName; private set => apiName = value; }

        public long Id { get => id; set => id = value; }

        public string SystemName { get => systemName; set => systemName = value; }

        public string SingularLabel { get => singularLabel; set => singularLabel = value; }

        public string PluralLabel { get => pluralLabel; set => pluralLabel = value; }

        public bool Convertable { get => convertable; set => convertable = value; }

        public bool Deletable { get => deletable; set => deletable = value; }

        public bool Editable { get => editable; set => editable = value; }

        public bool Viewable { get => viewable; set => viewable = value; }

        public bool CustomModule { get => customModule; set => customModule = value; }

        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        public List<string> BussinessCardFields { get => bussinessCardFields; set => bussinessCardFields = value; }

        public List<ZCRMLayout> Layouts { set => layouts = value; }

        public List<ZCRMModuleRelation> RelatedLists { get => relatedLists; set => relatedLists = value; }

        public bool Creatable { get => creatable; set => creatable = value; }

        public List<ZCRMProfile> AccessibleProfiles { get => accessibleProfiles; private set => accessibleProfiles = value; }
        public bool ApiSupported { get => apiSupported; set => apiSupported = value; }

        public BulkAPIResponse<ZCRMModuleRelation> GetRelatedLists()
        {
            return ModuleAPIHandler.GetInstance(this).GetAllRelatedLists();
        }


        public BulkAPIResponse<ZCRMField> GetAllFields()
        {
            return GetAllFields(null);
        }


        public BulkAPIResponse<ZCRMField> GetAllFields(string modifiedSince)
        {
            return ModuleAPIHandler.GetInstance(this).GetAllFields(modifiedSince);
        }


        public BulkAPIResponse<ZCRMLayout> GetLayouts()
        {
            return GetLayouts(null);
        }



        public BulkAPIResponse<ZCRMLayout> GetLayouts(string modifiedSince)
        {
            return ModuleAPIHandler.GetInstance(this).GetAllLayouts(modifiedSince);
        }

        public APIResponse GetLayoutDetails(long layoutId)
        {
            return ModuleAPIHandler.GetInstance(this).GetLayoutDetails(layoutId);
        }


        public BulkAPIResponse<ZCRMCustomView> GetCustomViews()
        {
            return GetCustomViews(null);
        }


        public BulkAPIResponse<ZCRMCustomView> GetCustomViews(string modifiedSince)
        {
            return ModuleAPIHandler.GetInstance(this).GetAllCustomViews(modifiedSince);
        }



        public APIResponse GetCustomView(long cvId)
        {
            return ModuleAPIHandler.GetInstance(this).GetCustomView(cvId);
        }



        public BulkAPIResponse<ZCRMRecord> CreateRecords(List<ZCRMRecord> records)
        {
            if (records == null || records.Count == 0)
            {
                throw new ZCRMException(" Records list MUST NOT be null for Create operation");
            }
            return MassEntityAPIHandler.GetInstance(this).CreateRecords(records);
        }


        public BulkAPIResponse<ZCRMRecord> MassUpdateRecords(List<long> entityIds, string fieldAPIName, object value)
        {
                if (entityIds == null || entityIds.Count == 0)
                {
                    throw new ZCRMException("Entity ID list MUST NOT be null/empty for update operation");
                }
                return MassEntityAPIHandler.GetInstance(this).MassUpdateRecords(entityIds, fieldAPIName, value);
        }

        public BulkAPIResponse<ZCRMRecord> UpdateRecords(List<ZCRMRecord> records)
        {
            if (records == null || records.Count == 0)
            {
                throw new ZCRMException("Entity ID list MUST NOT be null/empty for update operation");
            }
            return MassEntityAPIHandler.GetInstance(this).UpdateRecords(records);
        }

        public BulkAPIResponse<ZCRMRecord> UpsertRecords(List<ZCRMRecord> records)
        {
            if (records == null || records.Count == 0)
            {
                throw new ZCRMException("Record ID list MUST NOT be null/empty for upsert operation");
            }
            return MassEntityAPIHandler.GetInstance(this).UpsertRecords(records);
        }


        public BulkAPIResponse<ZCRMEntity> DeleteRecords(List<long> entityIds)
        {
            if (entityIds == null || entityIds.Count == 0)
            {
                throw new ZCRMException("Entity ID list MUST NOT be null/empty for delete operation");
            }
            return MassEntityAPIHandler.GetInstance(this).DeleteRecords(entityIds);
        }


        public APIResponse GetRecord(long entityId)
        {
            ZCRMRecord record = ZCRMRecord.GetInstance(ApiName, entityId);
            return EntityAPIHandler.GetInstance(record).GetRecord();
        }


        public BulkAPIResponse<ZCRMRecord> GetRecords()
        {
            return GetRecords(null, null);
        }


        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId)
        {
            return GetRecords(cvId, null);
        }



        public BulkAPIResponse<ZCRMRecord> GetRecords(List<string> fields)
        {
            return GetRecords(null, fields);
        }


        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, List<string> fields)
        {
            return GetRecords(cvId, 1, 200, fields);
        }


        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, int page, int perPage, List<string> fields)
        {
            return GetRecords(cvId, null, null, page, perPage, fields);
        }

        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, List<string> fields)
        {
            return GetRecords(cvId, sortByField, sortOrder, 1, 200, fields);
        }


        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, List<string> fields)
        {
            return GetRecords(cvId, sortByField, sortOrder, page, perPage, null, fields);
        }


        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, null, null, fields);
        }


        public BulkAPIResponse<ZCRMRecord> GetConvertedRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, "true", null, fields);
        }


        public BulkAPIResponse<ZCRMRecord> GetUnConvertedRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, "false", null, fields);
        }


        public BulkAPIResponse<ZCRMRecord> GetApprovedRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, null, "true", fields);
        }

        public BulkAPIResponse<ZCRMRecord> GetUnApprovedRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, null, "false", fields);
        }


        public BulkAPIResponse<ZCRMTrashRecord> GetAllDeletedRecords()
        {
            return MassEntityAPIHandler.GetInstance(this).GetAllDeletedRecords();
        }

        public BulkAPIResponse<ZCRMTrashRecord> GetRecycleBinRecords()
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecycleBinRecords();
        }

        public BulkAPIResponse<ZCRMTrashRecord> GetPermanentlyDeletedRecords()
        {
            return MassEntityAPIHandler.GetInstance(this).GetPermanentlyDeletedRecords();
        }


        public BulkAPIResponse<ZCRMRecord> SearchByWord(string value)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByWord(value, 1, 200);
        }

        public BulkAPIResponse<ZCRMRecord> SearchByWord(string value, int page, int perPage)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByWord(value, page, perPage);
        }



        public BulkAPIResponse<ZCRMRecord> SearchByCriteria(string value)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByCriteria(value, 1, 200);
        }


        public BulkAPIResponse<ZCRMRecord> SearchByCriteria(string value, int page, int perPage)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByCriteria(value, page, perPage);
        }


        public BulkAPIResponse<ZCRMRecord> SearchByPhone(string value)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByPhone(value, 1, 200);
        }


        public BulkAPIResponse<ZCRMRecord> SearchByPhone(string value, int page, int perPage)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByPhone(value, page, perPage);
        }


        public BulkAPIResponse<ZCRMRecord> SearchByEmail(string value)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByEmail(value, 1, 200);
        }

        public BulkAPIResponse<ZCRMRecord> SearchByEmail(string value, int page, int perPage)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByEmail(value, page, perPage);
        }

        public void AddAccessibleProfile(ZCRMProfile profile)
        {
            AccessibleProfiles.Add(profile);
        }

        public static ZCRMTag GetTagInstance()
        {
            return ZCRMTag.GetInstance();
        }

        public static ZCRMTag GetTagInstance(long? tagid)
        {
            return ZCRMTag.GetInstance(tagid);
        }
        public BulkAPIResponse<ZCRMTag> GetTags()
        {
            if (string.IsNullOrEmpty(this.apiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for GetTags operation");
            }
            return TagAPIHandler.GetInstance(this).GetTags();
        }

        public APIResponse GetTagCount(long? tagid)
        {
            if (string.IsNullOrEmpty(this.apiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for GetTagCount operation");
            }
            if (tagid == null || tagid == 0)
            {
                throw new ZCRMException("Tag ID MUST NOT be null/empty for GetTagCount operation");
            }
            return TagAPIHandler.GetInstance(this).GetTagCount(tagid);
        }

        public BulkAPIResponse<ZCRMTag> CreateTags(List<ZCRMTag> tags)
        {
            if (string.IsNullOrEmpty(this.apiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for CreateTags operation");
            }
            if (tags.Count<= 0)
            {
                throw new ZCRMException("Tag object list MUST NOT be null/empty for CreateTags operation");
            }
            return TagAPIHandler.GetInstance(this).CreateTags(tags);

        }

        public BulkAPIResponse<ZCRMTag> UpdateTags(List<ZCRMTag> tags)
        {
            if (string.IsNullOrEmpty(this.apiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for UpdateTags operation");
            }
            if (tags.Count <= 0)
            {
                throw new ZCRMException("Tag object list MUST NOT be null/empty for UpdateTags operation");
            }
            return TagAPIHandler.GetInstance(this).UpdateTags(tags);

        }

        public BulkAPIResponse<ZCRMRecord> AddTagsToRecords(List<long> recordIds, List<string> tagNames)
        {
            if (string.IsNullOrEmpty(this.apiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for Add Tags to Multiple records operation");
            }
            if (tagNames.Count <= 0)
            {
                throw new ZCRMException("Tag Name list MUST NOT be null/empty for Add Tags to Multiple records operation");
            }
            if (recordIds.Count <= 0)
            {
                throw new ZCRMException("Record ID list MUST NOT be null/empty for Add Tags to Multiple records operation");
            }
            return TagAPIHandler.GetInstance(this).AddTagsToRecords(recordIds, tagNames);
        }

        public BulkAPIResponse<ZCRMRecord> RemoveTagsFromRecords(List<long> recordIds, List<string> tagNames)
        {
            if (string.IsNullOrEmpty(this.apiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for Remove Tags from Multiple records operation");
            }
            if (tagNames.Count <= 0)
            {
                throw new ZCRMException("Tag Name list MUST NOT be null/empty for Remove Tags from Multiple records operation");
            }
            if (recordIds.Count <= 0)
            {
                throw new ZCRMException("Record ID list MUST NOT be null/empty for Remove Tags from Multiple records operation");
            }
            return TagAPIHandler.GetInstance(this).RemoveTagsFromRecords(recordIds, tagNames);
        }
    }
}
