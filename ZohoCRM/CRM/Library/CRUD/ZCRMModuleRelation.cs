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

        /// <summary>
        /// To get ZCRMModuleRelation instance by passing parent module APIName and related List APIName.
        /// </summary>
        /// <returns>ZCRMModuleRelation class instance.</returns>
        /// <param name="parentModuleAPIName">APIName (String) of the parent module.</param>
        /// <param name="relatedListAPIName">APIName (String) of the related list.</param>
        public static ZCRMModuleRelation GetInstance(string parentModuleAPIName, string relatedListAPIName)
        {
            return new ZCRMModuleRelation(parentModuleAPIName, relatedListAPIName);
        }

        /// <summary>
        /// To get ZCRMModuleRelation instance by passing parent module APIName and related List Id.
        /// </summary>
        /// <returns>ZCRMModuleRelation class instance.</returns>
        /// <param name="parentModuleAPIName">APIName (String) of the parent module</param>
        /// <param name="relatedListId">Id (Long) of the related list.)</param>
        public static ZCRMModuleRelation GetInstance(string parentModuleAPIName, long relatedListId)
        {
            return new ZCRMModuleRelation(parentModuleAPIName, relatedListId);
        }

        /// <summary>
        /// To get ZCRMModuleRelation instance by passing ZCRMRecord class instance and related List APIName.
        /// </summary>
        /// <returns>ZCRMModuleRelation class instance.</returns>
        /// <param name="parentRecord">ZCRMRecord class instance</param>
        /// <param name="relatedListAPIName">APIName (String) of the related list.</param>
        public static ZCRMModuleRelation GetInstance(ZCRMRecord parentRecord, string relatedListAPIName)
        {
            return new ZCRMModuleRelation(parentRecord, relatedListAPIName);
        }

        /// <summary>
        /// To get ZCRMModuleRelation instance by passing ZCRMRecord class instance and ZCRMJunctionRecord class instance.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="parentRecord">ZCRMRecord class instance</param>
        /// <param name="junctionRecord">ZCRMJunctionRecord class instance</param>
        public static ZCRMModuleRelation GetInstance(ZCRMRecord parentRecord, ZCRMJunctionRecord junctionRecord)
        {
            return new ZCRMModuleRelation(parentRecord, junctionRecord);
        }

        /// <summary>
        /// Gets or sets the APIName of/for the module relation.
        /// </summary>
        /// <value>The APIName of the module relation.</value>
        /// <returns>String</returns>
        public string ApiName
        {
            get
            {
                return apiName;
            }
            set
            {
                apiName = value;
            }
        }

        /// <summary>
        /// Gets or sets the Id of/for the module relation.
        /// </summary>
        /// <value>The Id of the module relation.</value>
        /// <returns>Long</returns>
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
        /// Gets the parent module API Name of the module relation.
        /// </summary>
        /// <value>The parent module API Name of the module relation.</value>
        /// <returns>String</returns>
        public string ParentModuleAPIName
        {
            get
            {
                return parentModuleAPIName;
            }
            private set
            {
                parentModuleAPIName = value;
            }
        }

        /// <summary>
        /// Gets the parent record of the module relation.
        /// </summary>
        /// <value>The parent record of the module relation.</value>
        /// <returns>ZCRMRecord class instance</returns>
        public ZCRMRecord ParentRecord
        {
            get
            {
                return parentRecord;
            }
            private set
            {
                parentRecord = value;
            }
        }

        /// <summary>
        /// Gets or sets the junction record of/for the module relation.
        /// </summary>
        /// <value>The junction record of the module relation.</value>
        /// <returns>ZCRMJunctionRecord class instance</returns>
        private ZCRMJunctionRecord JunctionRecord
        {
            get
            {
                return junctionRecord;
            }
            set
            {
                junctionRecord = value;
            }
        }

        /// <summary>
        /// Gets or sets the module name of/for the module relation.
        /// </summary>
        /// <value>The module name of the module relation.</value>
        /// <returns>String</returns>
        public string Module
        {
            get
            {
                return module;
            }
            set
            {
                module = value;
            }
        }

        /// <summary>
        /// Gets or sets the label of/for the module relation.
        /// </summary>
        /// <value>The label of the module relation.</value>
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
        /// Gets or sets the name of/for the module relation.
        /// </summary>
        /// <value>The name of the module relation.</value>
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
        /// Gets or sets a value indicating whether module relation is visible.
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
        /// Gets or sets the type of/for the module relation.
        /// </summary>
        /// <value>The type.</value>
        /// <returns>String</returns>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// To get all records of the module relation.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMRecord> GetRecords()
        {
            return GetRecords(null, null, 1, 20, null);
        }

        /// <summary>
        /// To get all records of the module relation based on sortByField, sortOrder, page, perPage and modifiedSince(Header).
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display records which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince)
        {
            return RelatedListAPIHandler.GetInstance(ParentRecord, this).GetRecords(sortByField, sortOrder, page, perPage, modifiedSince);
        }

        /// <summary>
        /// To get all notes of the module relation records.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMNote> GetNotes()
        {
            return GetNotes(null, null, 0, 20, null);
        }

        /// <summary>
        /// To get all notes of the module relation records based on sortByField, sortOrder, page, perPage and modifiedSince(Header).
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display notes which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMNote> GetNotes(String sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, String modifiedSince)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).GetNotes(sortByField, sortOrder, page, perPage, modifiedSince);
        }

        /// <summary>
        /// To add the note of the module relation records based on ZCRMNote class instance.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="note">ZCRMNote class instance</param>
        public APIResponse AddNote(ZCRMNote note)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).AddNote(note);
        }

        /// <summary>
        /// To update the note of the module relation records based on ZCRMNote class instance.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="note">ZCRMNote class instance</param>
        public APIResponse UpdateNote(ZCRMNote note)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).UpdateNote(note);
        }

        /// <summary>
        /// To delete the note of the module relation records based on ZCRMNote class instance.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="note">ZCRMNote class instance</param>
        public APIResponse DeleteNote(ZCRMNote note)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).DeleteNote(note);
        }

        /// <summary>
        /// To get all attachments of the module related list.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMAttachment&gt; class instance</returns>
        public BulkAPIResponse<ZCRMAttachment> GetAttachments()
        {
            return GetAttachments(0, 20, null);
        }

        /// <summary>
        /// To get all attachments of the module related list based on page, perPage and modifiedSince(Header).
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime to display attachments which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMAttachment> GetAttachments(int page, int perPage, String modifiedSince)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).GetAttachments(page, perPage, modifiedSince);
        }

        /// <summary>
        /// To upload attachment of the module related list based on attachment file path.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="filePath">Attachment file path (String) of the module related list</param>
        public APIResponse UploadAttachment(String filePath)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).UploadAttachment(filePath);
        }

        /// <summary>
        /// To upload the link as attachment of the module related list based on attachment URL.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="attachmentUrl">Attachment URL (String) of the module related list</param>
        public APIResponse UploadLinkAsAttachment(String attachmentUrl)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).UploadLinkAsAttachment(attachmentUrl);
        }

        /// <summary>
        /// To download attachment of the module related list based on attachment Id.
        /// </summary>
        /// <returns>FileAPIResponse class instance.</returns>
        /// <param name="attachmentId">Attachment Id (Long) of the module related list</param>
        public FileAPIResponse DownloadAttachment(long attachmentId)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).DownloadAttachment(attachmentId);
        }

        /// <summary>
        /// To delete attachment of the module related list based on attachment Id.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="attachmentId">Attachment Id (Long) of the module related list</param>
        public APIResponse DeleteAttachment(long attachmentId)
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, this).DeleteAttachment(attachmentId);
        }

        /// <summary>
        /// To add the relation of parent record and junction record.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        public APIResponse AddRelation()
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, junctionRecord).AddRelation();
        }

        /// <summary>
        /// To delete the relation of parent record and junction record.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        public APIResponse DeleteRelation()
        {
            return RelatedListAPIHandler.GetInstance(parentRecord, junctionRecord).DeleteRelation();
        }
        
    }
}
