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
        private List<ZCRMTag> tags = new List<ZCRMTag>();
        private List<string> tagnames = new List<string>();

        /** Bulk write API fields */
        private string status;
        private string error;
        private int rowNumber;

        private ZCRMRecord(string module, long? id)
        {
            ModuleAPIName = module;
            EntityId = id;
        }

        /// <summary>
        /// To get ZCRMRecord instance by passing module's APIName. 
        /// </summary>
        /// <param name="module">APIName (String) of the module</param>
        public ZCRMRecord(string module) : this(module, null) { }

        /// <summary>
        /// To get ZCRMRecord instance by passing modules' APIName and entity(record) Id.
        /// </summary>
        /// <returns>ZCRMRecord class instance.</returns>
        /// <param name="moduleAPIName">APIName (String) of the module</param>
        /// <param name="entityId">Id (Long) of the module record</param>
        public static ZCRMRecord GetInstance(string moduleAPIName, long? entityId)
        {
            return new ZCRMRecord(moduleAPIName, entityId);
        }

        /// <summary>
        /// Gets the APIName of the module.
        /// </summary>
        /// <value>The APIName of the module.</value>
        /// <returns>String</returns>
        public string ModuleAPIName
        {
            get
            {
                return moduleAPIName;
            }
            private set
            {
                moduleAPIName = value;
            }
        }

        /// <summary>
        /// Gets or sets the Id of/for the record.
        /// </summary>
        /// <value>The entity(record) Id of the record.</value>
        /// <returns>Long</returns>
        public long? EntityId
        {
            get
            {
                return entityId;
            }
            set
            {
                entityId = value;
            }
        }

        /// <summary>
        /// Gets or sets the lookup label of/for the record.
        /// </summary>
        /// <value>The lookup label of the record.</value>
        /// <returns>String</returns>
        public string LookupLabel
        {
            get
            {
                return lookupLabel;
            }
            set
            {
                lookupLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets the layout of/for the record.
        /// </summary>
        /// <value>The layout of the record.</value>
        /// <returns>ZCRMLayout class instance</returns>
        public ZCRMLayout Layout
        {
            get
            {
                return layout;
            }
            set
            {
                layout = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who owner of the record.
        /// </summary>
        /// <value>The owner.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who created the record.
        /// </summary>
        /// <value>The created by.</value>
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
        /// Gets or sets the created time of/for the record.
        /// </summary>
        /// <value>The created time.</value>
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
        /// Gets or sets the user who modified the record.
        /// </summary>
        /// <value>The modified by.</value>
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
        /// Gets or sets the modified time of/for the record.
        /// </summary>
        /// <value>The modified time.</value>
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
        /// Gets the properties of the record.
        /// </summary>
        /// <value>The properties of the record.</value>
        /// <returns>Dictionary(String,Object)</returns>
        public Dictionary<string, object> Properties
        {
            get
            {
                return properties;
            }
            private set
            {
                properties = value;
            }
        }

        /// <summary>
        /// Gets or sets the data of/for the record.
        /// </summary>
        /// <value>The data of the record.</value>
        /// <returns>Dictionary(String,Object)</returns>
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
        /// Gets the line items of the record.
        /// </summary>
        /// <value>The line items of the record.</value>
        /// <returns>List of ZCRMInventoryLineItem class instance</returns>
        public List<ZCRMInventoryLineItem> LineItems
        {
            get
            {
                return lineItems;
            }
            private set
            {
                lineItems = value;
            }
        }

        /// <summary>
        /// Gets the participants of the record.
        /// </summary>
        /// <value>The participants of the record.</value>
        /// <returns>List of ZCRMEventParticipant class instance</returns>
        public List<ZCRMEventParticipant> Participants
        {
            get
            {
                return participants;
            }
            private set
            {
                participants = value;
            }
        }

        /// <summary>
        /// Gets the price details of the record.
        /// </summary>
        /// <value>The price details of the record.</value>
        /// <returns>List of ZCRMPriceBookPricing class instance</returns>
        public List<ZCRMPriceBookPricing> PriceDetails
        {
            get
            {
                return priceDetails;
            }
            private set
            {
                priceDetails = value;
            }
        }

        /// <summary>
        /// Gets the tax list of the record.
        /// </summary>
        /// <value>The tax list of the record.</value>
        /// <returns>List of ZCRMTax class instance</returns>
        public List<ZCRMTax> TaxList
        {
            get
            {
                return taxList;
            }
            private set
            {
                taxList = value;
            }
        }

        /// <summary>
        /// Gets or sets the tags of/for the record.
        /// </summary>
        /// <value>The tags of the record.</value>
        /// <returns>List of String</returns>
        public List<ZCRMTag> Tags
        {
            get
            {
                return tags;
            }
            set
            {
                tags = value;
            }
        }

        /// <summary>
        /// Gets or sets the tag names.
        /// </summary>
        /// <value>The tag names.</value>
        public List<string> TagNames
        {
            get
            {
                return tagnames;
            }
            set
            {
                tagnames = value;
            }
        }

        /// <summary>
        /// To set the property based on Property name and value.
        /// </summary>
        /// <param name="name">Name (String) of the record property.</param>
        /// <param name="value">value (Object) of the record property.</param>
        public void SetProperty(string name, object value)
        {
            Properties.Add(name, value);
        }

        /// <summary>
        /// To get the property value of the record based on Property name and value.
        /// </summary>
        /// <returns>Property value of the record.</returns>
        /// <param name="propertyName">name (String) of the record property.</param>
        public object GetProperty(string propertyName)
        {
            return Properties[propertyName];
        }

        /// <summary>
        /// To set fields based on field APIName and value.
        /// </summary>
        /// <param name="fieldAPIName">APIName (String) of the record field.</param>
        /// <param name="value">value (Object) of the record field.</param>
        public void SetFieldValue(string fieldAPIName, object value)
        {
            Data[fieldAPIName] =  value;
        }

		/// <summary>
		/// To get field value of the record based on field APIName.
		/// </summary>
		/// <returns>value (Object) of record field.</returns>
		/// <param name="fieldAPIName">APIName (String) of the record field.</param>
		public object GetFieldValue(string fieldAPIName)
        {
            if (Data.ContainsKey(fieldAPIName))
            {
                if (Data[fieldAPIName] == null) { return null; }
                return Data[fieldAPIName];
            }
            throw new ZCRMException("The given field is not present in this record - " + fieldAPIName);
        }

        /// <summary>
        /// To add line item of the record based on ZCRMInventoryLineItem class instance.
        /// </summary>
        /// <param name="newLineItem">ZCRMInventoryLineItem class instance</param>
        public void AddLineItem(ZCRMInventoryLineItem newLineItem)
        {
            LineItems.Add(newLineItem);
        }

        /// <summary>
        /// To add participant of the record based on ZCRMEventParticipant class instance.
        /// </summary>
        /// <param name="participant">ZCRMEventParticipant class instance</param>
        public void AddParticipant(ZCRMEventParticipant participant)
        {
            Participants.Add(participant);
        }

        /// <summary>
        /// To add price detail of the record based on ZCRMPriceBookPricing class instance.
        /// </summary>
        /// <param name="priceDetail">ZCRMPriceBookPricing class instance</param>
        public void AddPriceDetail(ZCRMPriceBookPricing priceDetail)
        {
            PriceDetails.Add(priceDetail);
        }

        /// <summary>
        /// To add tax of the record ZCRMTax class instance.
        /// </summary>
        /// <param name="tax">ZCRMTax class instance.</param>
        public void AddTax(ZCRMTax tax)
        {
            TaxList.Add(tax);
        }

        /// <summary>
        /// Gets or sets the record status 
        /// </summary>
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
        /// Gets or sets the record error
        /// </summary>
        public string Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
            }
        }

        /// <summary>
        /// Gets or sets the record row number
        /// </summary>
        public int RowNumber
        {
            get
            {
                return rowNumber;
            }
            set
            {
                rowNumber = value;
            }
        }


        /// <summary>
        /// To create the record.
        /// </summary>
        /// <returns>APIResponse class instance</returns>
        public APIResponse Create(List<string> trigger = null, string lar_id = null, List<string> process = null)
        {
            if (EntityId != null) { throw new ZCRMException("Entity ID MUST be NUL for Create Operation"); }
            return EntityAPIHandler.GetInstance(this).CreateRecord(trigger, lar_id, process);
        }

        /// <summary>
        /// To update the record.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        public APIResponse Update(List<string> trigger = null, List<string> process = null)
        {
            if (EntityId == null) { throw new ZCRMException("Entity ID MUST NOT be NULl for update operation"); }
            return EntityAPIHandler.GetInstance(this).UpdateRecord(trigger, process);
        }

        /// <summary>
        /// To delete the record.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        public APIResponse Delete()
        {
            if (EntityId == null) { throw new ZCRMException("Entity ID MUST NOT be NULL for Delete Operation"); }
            return EntityAPIHandler.GetInstance(this).DeleteRecord();
        }

        /// <summary>
        /// To convert the record.
        /// </summary>
        /// <returns>Dictionary(String,Long).</returns>
        public Dictionary<string, long> Convert()
        {
            return Convert(null);
        }

        /// <summary>
        /// To convert specified potential record.
        /// </summary>
        /// <returns>Dictionary(String,Long).</returns>
        /// <param name="potential">ZCRMRecord class instance</param>
        public Dictionary<string, long> Convert(ZCRMRecord potential)
        {
            return Convert(potential, null);
        }

        /// <summary>
        /// To convert the record based on potential record  and ZCRMUser class instance.
        /// </summary>
        /// <returns>Dictionary(String,Long).</returns>
        /// <param name="potential">ZCRMRecord class instance</param>
        /// <param name="assignToUser">ZCRMUser class instance</param>
        public Dictionary<string, long> Convert(ZCRMRecord potential, ZCRMUser assignToUser)
        {
            return EntityAPIHandler.GetInstance(this).ConvertRecord(potential, assignToUser);
        }

        /// <summary>
        /// To get related list records based on related List APIName.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="relatedListAPIName">APIName (String) of the record related list.</param>
        public BulkAPIResponse<ZCRMRecord> GetRelatedListRecords(string relatedListAPIName)
        {
            return GetRelatedListRecords(relatedListAPIName, 1, 20);
        }

        /// <summary>
        /// To get all related list records based on related List APIName, page and perPage
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="relatedListAPIName">APIName (String) of the record related list</param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMRecord> GetRelatedListRecords(string relatedListAPIName, int page, int perPage)
        {
            return GetRelatedListRecords(relatedListAPIName, null, null, page, perPage, null);
        }

        /// <summary>
        /// To get all related list records based on related List APIName, sortByField and sortOrder
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="relatedListAPIName">APIName (String) of the record related list.</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        public BulkAPIResponse<ZCRMRecord> GetRelatedListRecords(string relatedListAPIName, string sortByField, CommonUtil.SortOrder? sortOrder)
        {
            return GetRelatedListRecords(relatedListAPIName, sortByField, sortOrder, 1, 20, null);
        }

        /// <summary>
        /// To get all related list records based on related List APIName, sortByField, sortOrder, page, perPage and modifiedSince.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="relatedListAPIName">APIName (String) of the record related list.</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime to display records which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRelatedListRecords(string relatedListAPIName, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince)
        {
            return ZCRMModuleRelation.GetInstance(this, relatedListAPIName).GetRecords(sortByField, sortOrder, page, perPage, modifiedSince);
        }

        /// <summary>
        /// To get all notes of the record based on ZCRMRecord class instance.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMNote> GetNotes()
        {
            return GetNotes(1, 20);
        }

        /// <summary>
        /// To get all notes of the records based on page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMNote> GetNotes(int page, int perPage)
        {
            return GetNotes(null, null, page, perPage, null);
        }

        /// <summary>
        /// To get all notes of the records based on sortByField and sortOrder.
        /// </summary>
        /// <returns>BulkAPIResponse class instance.</returns>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        public BulkAPIResponse<ZCRMNote> GetNotes(string sortByField, CommonUtil.SortOrder? sortOrder)
        {
            return GetNotes(sortByField, sortOrder, 1, 20, null);
        }

        /// <summary>
        /// To get all notes of the records based on sortByField, sortOrder, page, perPage and modifiedSince(Header).
        /// </summary>
        /// <returns>BulkAPIResponse class instance.</returns>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display notes which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMNote> GetNotes(string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince)
        {
            return ZCRMModuleRelation.GetInstance(this, "Notes").GetNotes(sortByField, sortOrder, page, perPage, modifiedSince);
        }

        /// <summary>
        /// To add the note of the records based on ZCRMNote class instance.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="note">ZCRMNote class instance</param>
        public APIResponse AddNote(ZCRMNote note)
        {
            if (note.Id != null) { throw new ZCRMException("Note ID MUST be null for Create Operation"); }
            return ZCRMModuleRelation.GetInstance(this, "Notes").AddNote(note);
        }

        /// <summary>
        /// To update the note of the records.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="note">ZCRMNote class instance</param>
        public APIResponse UpdateNote(ZCRMNote note)
        {
            if (note.Id == null) { throw new ZCRMException("Note ID MUST NOT be null for Update Operation"); }
            return ZCRMModuleRelation.GetInstance(this, "Notes").UpdateNote(note);
        }

        /// <summary>
        /// To deletes the note of the records.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="note">ZCRMNote class instance</param>
        public APIResponse DeleteNote(ZCRMNote note)
        {
            if (note.Id == null) { throw new ZCRMException("Note ID MUST be null for Delete Operation"); }
            return ZCRMModuleRelation.GetInstance(this, "Notes").DeleteNote(note);
        }

        /// <summary>
        /// To get all attachments.
        /// </summary>
        /// <returns>BulkAPIResponse class instance.</returns>
        public BulkAPIResponse<ZCRMAttachment> GetAttachments()
        {
            return GetAttachments(0, 20, null);
        }

        /// <summary>
        /// To get all attachments of the records based on page, perPage and modifiedSince(Header).
        /// </summary>
        /// <returns>BulkAPIResponse class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display attachments which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMAttachment> GetAttachments(int page, int perPage, string modifiedSince)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").GetAttachments(page, perPage, modifiedSince);
        }

        /// <summary>
        /// To upload the attachment of the records based on attachment file path.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="filePath">file path (String) of the record attachment.</param>
        public APIResponse UploadAttachment(String filePath)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").UploadAttachment(filePath);
        }

        /// <summary>
        /// To upload the link as attachment of the records based on attachment URL.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="attachmentUrl">Attachment URL (String) of the record</param>
        public APIResponse UploadLinkAsAttachment(String attachmentUrl)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").UploadLinkAsAttachment(attachmentUrl);
        }

        /// <summary>
        /// To download the attachment of the records based on attachment Id.
        /// </summary>
        /// <returns>FileAPIResponse class instance.</returns>
        /// <param name="attachmentId">Id (Long) of the record attachment.</param>
        public FileAPIResponse DownloadAttachment(long attachmentId)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").DownloadAttachment(attachmentId);
        }

        /// <summary>
        /// To delete the attachment of the records based on attachment Id.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="attachmentId">Id (Long) of the record attachment.</param>
        public APIResponse DeleteAttachment(long attachmentId)
        {
            return ZCRMModuleRelation.GetInstance(this, "Attachments").DeleteAttachment(attachmentId);
        }

        /// <summary>
        /// To upload the record photo based on file path
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="filePath">Upload photo file path (String)</param>
        public APIResponse UploadPhoto(string filePath)
        {
            return EntityAPIHandler.GetInstance(this).UploadPhoto(filePath);
        }

        /// <summary>
        /// To download the record photo.
        /// </summary>
        /// <returns>FileAPIResponse class instance.</returns>
        public FileAPIResponse DownloadPhoto()
        {
            return EntityAPIHandler.GetInstance(this).DownloadPhoto();
        }

        /// <summary>
        /// To deletes the record photo.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        public APIResponse DeletePhoto()
        {
            return EntityAPIHandler.GetInstance(this).DeletePhoto();
        }

        /// <summary>
        /// To add the relation of the record based on ZCRMJunctionRecord class instance.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="junctionRecord">ZCRMJunctionRecord class instance</param>
        public APIResponse AddRelation(ZCRMJunctionRecord junctionRecord)
        {
            return ZCRMModuleRelation.GetInstance(this, junctionRecord).AddRelation();
        }

        /// <summary>
        /// To delete the relation of the record based on ZCRMJunctionRecord class instance.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="junctionRecord">ZCRMJunctionRecord class instance</param>
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

        /// <summary>
        /// To add the tags to record.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="tagNames">List of tag names (String)</param>
        public APIResponse AddTags(List<string> tagNames)
        {
            if (this.entityId == null || this.entityId == 0)
            {
                throw new ZCRMException("Record ID MUST NOT be null/empty for Add Tags to a Specific record operation");
            }
            if (string.IsNullOrEmpty(this.moduleAPIName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for Add Tags to a Specific record operation");
            }
            if (tagNames.Count <= 0)
            {
                throw new ZCRMException("Tag Name list MUST NOT be null/empty for Add Tags to a Specific record operation");
            }
            return TagAPIHandler.GetInstance().AddTags(this, tagNames);
        }

        /// <summary>
        /// To remove the tags from record.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="tagNames">List of tag names (String)</param>
        public APIResponse RemoveTags(List<string> tagNames)
        {
            if (this.entityId == null || this.entityId == 0)
            {
                throw new ZCRMException("Record ID MUST NOT be null/empty for Remove Tags from a Specific record operation");
            }
            if (string.IsNullOrEmpty(this.moduleAPIName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for Remove Tags from a Specific record operation");
            }
            if (tagNames.Count <= 0)
            {
                throw new ZCRMException("Tag Name list MUST NOT be null/empty for Remove Tags from a Specific record operation");
            }
            return TagAPIHandler.GetInstance().RemoveTags(this, tagNames);
        }
    }
}
