using System;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Api.Handler;

namespace ZCRMSDK.CRM.Library.CRUD
{

    //TODO:<IMPORTANT> Learn about ICloneable;
    public class ZCRMRecord : ZCRMEntity, ICloneable
    {

        private long? entityId;
        private string moduleAPIName;
        private Dictionary<string, object> fieldNameVsValue = new Dictionary<string, object>();
        private Dictionary<string, object> properties = new Dictionary<string, object>();
        private List<ZCRMInventoryLineItem> lineItems = new List<ZCRMInventoryLineItem>();
        private List<ZCRMEventParticipant> participants = new List<ZCRMEventParticipant>();
        private List<ZCRMPriceBookPricing> priceDetails = new List<ZCRMPriceBookPricing>();
        private string lookupLabel;
        private ZCRMUser owner;
        private ZCRMUser createdBy;
        private ZCRMUser modifiedBy;
        private string createdTime;
        private string modifiedTime;
        private ZCRMLayout layout;
        private List<ZCRMTax> taxList = new List<ZCRMTax>();


        private ZCRMRecord(string module, long? id)
        {
            ModuleAPIName = module;
            EntityId = id;
        }

        public ZCRMRecord(string module) : this(module, null) { }


        public static ZCRMRecord GetInstance(string moduleAPIName, long? entityId)
        {
            return new ZCRMRecord(moduleAPIName, entityId);
        }

        public string ModuleAPIName { get => moduleAPIName; private set => moduleAPIName = value; }

        public long? EntityId { get => entityId; set => entityId = value; }

        public string LookupLabel { get => lookupLabel; set => lookupLabel = value; }

        public ZCRMLayout Layout { get => layout; set => layout = value; }

        public ZCRMUser Owner { get => owner; set => owner = value; }

        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        public string CreatedTime { get => createdTime; set => createdTime = value; }

        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        public Dictionary<string, object> Properties {  get => properties;  private set => properties = value; }

        public Dictionary<string, object> Data { get => fieldNameVsValue; set => fieldNameVsValue = value; }

        public List<ZCRMInventoryLineItem> LineItems { get => lineItems; private set => lineItems = value; }

        public List<ZCRMEventParticipant> Participants { get => participants; private set => participants = value; }

        public List<ZCRMPriceBookPricing> PriceDetails { get => priceDetails; private set => priceDetails = value; }

        public List<ZCRMTax> TaxList { get => taxList; private set => taxList = value; }

        public void SetProperty(string name, object value)
        {
            Properties.Add(name,value);
        }

        public object GetProperty(string propertyName)
        {
            return Properties[propertyName];
        }

        public void SetFieldValue(string fieldAPIName, object value)
        {
            Data.Add(fieldAPIName, value);
        }

        public object GetFieldValue(string fieldAPIName)
        {
            if(Data.ContainsKey(fieldAPIName))
            {
                if(Data[fieldAPIName] == null) { return null; }
                return Data[fieldAPIName];
            }
            throw new ZCRMException("The given field is not present in this record - " + fieldAPIName);
        }


        public void AddLineItem(ZCRMInventoryLineItem newLineItem)
        {
            LineItems.Add(newLineItem);
        }

        public void AddParticipant(ZCRMEventParticipant participant)
        {
            Participants.Add(participant);
        }

        public void AddPriceDetail(ZCRMPriceBookPricing priceDetail)
        {
            PriceDetails.Add(priceDetail);
        }

        public void AddTax(ZCRMTax tax)
        {
            TaxList.Add(tax);
        }


        public APIResponse Create()
        {
           if (EntityId != null) { throw new ZCRMException("Entity ID MUST be NUL for Create Operation"); }
            return EntityAPIHandler.GetInstance(this).CreateRecord();
        }

        public APIResponse Update()
        {
            if (EntityId == null) { throw new ZCRMException("Entity ID MUST NOT be NULl for update operation"); }
            return EntityAPIHandler.GetInstance(this).UpdateRecord();
        }

        public APIResponse Delete()
        {
            if (EntityId == null) { throw new ZCRMException("Entity ID MUST NOT be NULL for Delete Operation"); }
            return EntityAPIHandler.GetInstance(this).DeleteRecord();
        }

        public Dictionary<string, long> Convert()
        {
            return Convert(null);
        }

        private Dictionary<string, long> Convert(ZCRMRecord potential)
        {
            return Convert(potential, null);
        }

        private Dictionary<string, long> Convert(ZCRMRecord potential, ZCRMUser assignToUser)
        {
            return EntityAPIHandler.GetInstance(this).ConvertRecord(potential, assignToUser);
        }


        public BulkAPIResponse<ZCRMRecord> GetRelatedListRecords(string relatedListAPIName)
        {
            return GetRelatedListRecords(relatedListAPIName, 1, 20);
        }

        public BulkAPIResponse<ZCRMRecord> GetRelatedListRecords(string relatedListAPIName, int page, int perPage)
        {
            return GetRelatedListRecords(relatedListAPIName, null, null, page, perPage, null);
        }

        public BulkAPIResponse<ZCRMRecord> GetRelatedListRecords(string relatedListAPIName, string sortByField, CommonUtil.SortOrder? sortOrder)
        {
            return GetRelatedListRecords(relatedListAPIName, sortByField, sortOrder, 1, 20, null);
       }

        public BulkAPIResponse<ZCRMRecord> GetRelatedListRecords(string relatedListAPIName, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince)
        {
            return ZCRMModuleRelation.GetInstance(this, relatedListAPIName).GetRecords(sortByField, sortOrder, page, perPage, modifiedSince);
        }


        public BulkAPIResponse<ZCRMNote> GetNotes()
        {
            return GetNotes(1, 20);
        }


        public BulkAPIResponse<ZCRMNote> GetNotes(int page, int perPage)
        {
            return GetNotes(null, null, page, perPage, null);
        }


        public BulkAPIResponse<ZCRMNote> GetNotes(string sortByField, CommonUtil.SortOrder? sortOrder)
        {
            return GetNotes(sortByField, sortOrder, 1, 20, null);
        }

        public BulkAPIResponse<ZCRMNote> GetNotes(string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince)
        {
            return ZCRMModuleRelation.GetInstance(this, "Notes").GetNotes(sortByField, sortOrder, page, perPage, modifiedSince);
        }


        public APIResponse AddNote(ZCRMNote note)
        {
            if (note.Id != null) { throw new ZCRMException("Note ID MUST be null for Create Operation"); }
            return ZCRMModuleRelation.GetInstance(this, "Notes").AddNote(note);
        }


        public APIResponse UpdateNote(ZCRMNote note)
        {
            if (note.Id == null) { throw new ZCRMException("Note ID MUST NOT be null for Update Operation"); }
            return ZCRMModuleRelation.GetInstance(this, "Notes").UpdateNote(note);
        }

        public APIResponse DeleteNote(ZCRMNote note)
        {
            if (note.Id == null) { throw new ZCRMException("Note ID MUST be null for Delete Operation"); }
            return ZCRMModuleRelation.GetInstance(this, "Notes").DeleteNote(note);
        }



        public BulkAPIResponse<ZCRMAttachment> GetAllAttachmentsDetails()
        {
            return GetAllAttachmentsDetails(0, 20, null);
        }


        public BulkAPIResponse<ZCRMAttachment> GetAllAttachmentsDetails(int page, int perPage, string modifiedSince)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").GetAllAttachmentsDetails(page, perPage, modifiedSince);
        }




        public APIResponse UploadAttachment(String filePath)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").UploadAttachment(filePath);
        }



        public APIResponse UploadLinkAsAttachment(String attachmentUrl)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").UploadLinkAsAttachment(attachmentUrl);
        }

        public FileAPIResponse DownloadAttachment(long attachmentId)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").DownloadAttachment(attachmentId);
        }



        public APIResponse DeleteAttachment(long attachmentId)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").DeleteAttachment(attachmentId);
        }



        public APIResponse UploadPhoto(string filePath)
        {
            return EntityAPIHandler.GetInstance(this).UploadPhoto(filePath);
        }

        public FileAPIResponse DownloadPhoto()
        {
            return EntityAPIHandler.GetInstance(this).DownloadPhoto();
        }

        public APIResponse DeletePhoto()
        {
            return EntityAPIHandler.GetInstance(this).DeletePhoto();
        }


        public APIResponse AddRelation(ZCRMJunctionRecord junctionRecord)
        {
            return ZCRMModuleRelation.GetInstance(this, junctionRecord).AddRelation();
        }


        public APIResponse DeleteRelation(ZCRMJunctionRecord junctionRecord)
        {
            return ZCRMModuleRelation.GetInstance(this, junctionRecord).DeleteRelation();
        }



        //TODO<IMPORTANT>:Learn about the method completely before implementing the method;
        public object Clone()
        {
            ZCRMRecord newRecord = this.Clone() as ZCRMRecord;
            newRecord.EntityId = null;
            newRecord.Properties = null;
            return newRecord; 
        }
    }
}
