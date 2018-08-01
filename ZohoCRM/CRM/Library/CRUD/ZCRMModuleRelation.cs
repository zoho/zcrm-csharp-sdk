using System;
using ZCRMSDK.CRM.Library.Api.Handler;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMModuleRelation : ZCRMEntity
    {

        private string label;
        private string apiName;
        private string module;
        private string name;
        private long id;
        private string parentModuleAPIName;
        private bool visible;
        private string type;

        private ZCRMRecord parentRecord;
        private ZCRMJunctionRecord junctionRecord;

        private ZCRMModuleRelation(string parentModuleAPIName, string relatedListAPIName)
        {
            ParentModuleAPIName = parentModuleAPIName;
            ApiName = relatedListAPIName;
        }

        private ZCRMModuleRelation(string parentModuleAPIName, long relatedListId)
        {
            ParentModuleAPIName = parentModuleAPIName;
            Id = relatedListId;
        }

        private ZCRMModuleRelation(ZCRMRecord parentRecord, string relatedListAPIName)
        {
            ParentRecord = parentRecord;
            ApiName = relatedListAPIName;
        }

        private ZCRMModuleRelation(ZCRMRecord parentRecord, ZCRMJunctionRecord junctionRecord)
        {
            ParentRecord = parentRecord;
            JunctionRecord = junctionRecord;
        }
       

        public static ZCRMModuleRelation GetInstance(string parentModuleAPIName, string relatedListAPIName)
        {
            return new ZCRMModuleRelation(parentModuleAPIName, relatedListAPIName);
        }

        public static ZCRMModuleRelation GetInstance(string parentModuleAPIName, long relatedListId)
        {
            return new ZCRMModuleRelation(parentModuleAPIName, relatedListId);
        }

        public static ZCRMModuleRelation GetInstance(ZCRMRecord parentRecord, string relatedListAPIName)
        {
            return new ZCRMModuleRelation(parentRecord, relatedListAPIName);
        }

        public static ZCRMModuleRelation GetInstance(ZCRMRecord parentRecord, ZCRMJunctionRecord junctionRecord)
        {
            return new ZCRMModuleRelation(parentRecord, junctionRecord);
        }

        public string ApiName { get => apiName; set => apiName = value; }

        public long Id { get => id; set => id = value; }

        public string ParentModuleAPIName { get => parentModuleAPIName; private set => parentModuleAPIName = value; }

        public ZCRMRecord ParentRecord { get => parentRecord; private set => parentRecord = value; }

        private ZCRMJunctionRecord JunctionRecord { get => junctionRecord; set => junctionRecord = value; }

        public string Module { get => module; set => module = value; }

        public string Label { get => label; set => label = value; }

        public string Name { get => name; set => name = value; }

        public bool Visible { get => visible; set => visible = value; }

        public string Type { get => type; set => type = value; }



        public BulkAPIResponse<ZCRMRecord> GetRecords()
        {
            return GetRecords(null, null, 1, 20, null);
        }


        public BulkAPIResponse<ZCRMRecord> GetRecords(string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince)
        {
            return RelatedListAPIHandler.GetInstance(ParentRecord, this).GetRecords(sortByField, sortOrder, page, perPage, modifiedSince);
        }


        public BulkAPIResponse<ZCRMNote> GetNotes()
        {
            return GetNotes(null, null, 0, 20, null);
        }
  
        public BulkAPIResponse<ZCRMNote> GetNotes(String sortByField, CommonUtil.SortOrder? sortOrder, int page, int per_page, String modifiedSince)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).GetNotes(sortByField, sortOrder, page, per_page, modifiedSince);
        }

        public APIResponse AddNote(ZCRMNote note)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).AddNote(note);
        }
  

        public APIResponse UpdateNote(ZCRMNote note)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).UpdateNote(note);
        }


        public APIResponse DeleteNote(ZCRMNote note)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).DeleteNote(note);
        }
  

        public BulkAPIResponse<ZCRMAttachment> GetAllAttachmentsDetails()
        {
            return GetAllAttachmentsDetails(0, 20, null);
        }


        public BulkAPIResponse<ZCRMAttachment> GetAllAttachmentsDetails(int page, int per_page, String modifiedSince)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).GetAllAttachmentsDetails(page, per_page, modifiedSince);
        }


        public APIResponse UploadAttachment(String filePath)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).UploadAttachment(filePath);
        }




        public APIResponse UploadLinkAsAttachment(String attachmentUrl)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).UploadLinkAsAttachment(attachmentUrl);
        }


        public FileAPIResponse DownloadAttachment(long attachmentId)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).DownloadAttachment(attachmentId);
        }


        public APIResponse DeleteAttachment(long attachmentId)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).DeleteAttachment(attachmentId);
        }


        public APIResponse AddRelation()
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, junctionRecord).AddRelation();
        }


        public APIResponse DeleteRelation()
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, junctionRecord).DeleteRelation();
        }
        
    }
}
