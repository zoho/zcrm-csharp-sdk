using System;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMCustomView : ZCRMEntity
    {
        private string moduleAPIName;
        private string displayName;
        private string name;
        private string systemName;
        private long id;
        private string sortBy;
        private CommonUtil.SortOrder sortOrder;
        private string category;
        private List<string> fields = new List<string>();
        private int favourite;
        private bool isdefault;


        private ZCRMCustomView(string moduleAPIName, long customViewId)
        {
            ModuleAPIName = moduleAPIName;
            Id = customViewId;
        }

        public static ZCRMCustomView GetInstance(string moduleAPIName, long customViewId)
        {
            return new ZCRMCustomView(moduleAPIName, customViewId);
        }

        public string ModuleAPIName { get => moduleAPIName; private set => moduleAPIName = value; }

        public long Id { get => id; set => id = value; }

        public string Name { get => name; set => name = value; }

        public string SystemName { get => systemName; set => systemName = value; }

        public string DisplayName { get => displayName; set => displayName = value; }

        public List<string> Fields { get => fields; set => fields = value; }

        public CommonUtil.SortOrder SortOrder { get => sortOrder; set => sortOrder = value; }

        public string SortBy { get => sortBy; set => sortBy = value; }

        public string Category { get => category; set => category = value; }

        public bool Isdefault { get => isdefault; set => isdefault = value; }

        public int Favourite { get => favourite; set => favourite = value; }



        public BulkAPIResponse<ZCRMRecord> GetRecords()
        {
            return GetRecords(null);
        }

        public BulkAPIResponse<ZCRMRecord> GetRecords(List<string> recordFields)
        {

            return GetRecords(1, 200, recordFields);
        }

        public BulkAPIResponse<ZCRMRecord> GetRecords(int page, int perPage, List<string> recordFields)
        {
            return GetRecords(null, null, page, perPage, null, recordFields);   
        }

        public BulkAPIResponse<ZCRMRecord> GetRecords(string sortByField, CommonUtil.SortOrder? sortOrdrer, List<string> recordFields)
        {
            return GetRecords(sortByField, sortOrdrer, 1, 200, null, recordFields);
        }


        public BulkAPIResponse<ZCRMRecord> GetRecords(string sortByField, CommonUtil.SortOrder? sortOrderField, int page, int perPage, string modifiedSince, List<string> recordFields)
        {
            return ZCRMModule.GetInstance(moduleAPIName).GetRecords(Convert.ToInt64(Id), sortByField, sortOrderField, page, perPage, modifiedSince, recordFields);   
        }
    }
}
