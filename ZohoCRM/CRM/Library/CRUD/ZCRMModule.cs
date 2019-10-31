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

        private bool globalSearchSupported;
        private bool kanbanView;
        private bool filterStatus;
        private bool inventoryTemplateSupported;
        private bool presenceSubMenu;
        private ZCRMRelatedListProperties relatedListProperties;
        private List<string> properties = new List<string>();
        private int perPage;
        private int visibility;
        private bool emailTemplateSupport;
        private bool filterSupported;
        private string displayField;
        private List<string> searchLayoutFields = new List<string>();
        private bool kanbanViewSupported;
        private string webLink;
        private int sequenceNumber;
        private bool quickCreate;
        private bool feedsRequired;
        private bool scoringSupported;
        private bool webformSupported;
        private List<ZCRMWebTabArguments> webTabArguments = new List<ZCRMWebTabArguments>();
        private int businessCardFieldLimit;
        private ZCRMModule parentModule;
        private ZCRMTerritory territory;
        private ZCRMCustomView customView;

        private List<ZCRMField> fields = new List<ZCRMField>();


        protected ZCRMModule(string apiName)
        {
            ApiName = apiName;
        }

        /// <summary>
        /// To get module instance by passing modules' API name.
        /// </summary>
        /// <returns>ZCRMModule class instance.</returns>
        /// <param name="apiName">API name(String) of the module</param>
        public static ZCRMModule GetInstance(string apiName)
        {
            return new ZCRMModule(apiName);
        }

        /// <summary>
        /// To get API name of the module.
        /// </summary>
        /// <value>The API name of the module.</value>
        /// <returns>String</returns>
        public string ApiName
        {
            get
            {
                return apiName;
            }
            private set
            {
                apiName = value;
            }
        }

        /// <summary>
        /// Gets or sets the ZohoCRM modules' Id.
        /// </summary>
        /// <value>The modules' Id.</value>
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
        /// Gets or sets the name of/for the module.
        /// </summary>
        /// <value>The name of the module.</value>
        /// <returns>String</returns>
        public string SystemName
        {
            get
            {
                return systemName;
            }
            set
            {
                systemName = value;
            }
        }

        /// <summary>
        /// Gets or sets the singular name of/for the module.
        /// </summary>
        /// <value>The singular name of the module.</value>
        /// <returns>String</returns>
        public string SingularLabel
        {
            get
            {
                return singularLabel;
            }
            set
            {
                singularLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets the plural name of/for the module.
        /// </summary>
        /// <value>The plural name of the module.</value>
        /// <returns>String</returns>
        public string PluralLabel
        {
            get
            {
                return pluralLabel;
            }
            set
            {
                pluralLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this module is convertable.
        /// </summary>
        /// <value><c>true</c> if convertable; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Convertable
        {
            get
            {
                return convertable;
            }
            set
            {
                convertable = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this module is deletable.
        /// </summary>
        /// <value><c>true</c> if deletable; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Deletable
        {
            get
            {
                return deletable;
            }
            set
            {
                deletable = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this module is editable.
        /// </summary>
        /// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Editable
        {
            get
            {
                return editable;
            }
            set
            {
                editable = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this module is viewable.
        /// </summary>
        /// <value><c>true</c> if viewable; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Viewable
        {
            get
            {
                return viewable;
            }
            set
            {
                viewable = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this module is custom module.
        /// </summary>
        /// <value><c>true</c> if custom module; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool CustomModule
        {
            get
            {
                return customModule;
            }
            set
            {
                customModule = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who modified the module.
        /// </summary>
        /// <value>The user who modified the module.</value>
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
        /// Gets or sets the modified time of/for the module.
        /// </summary>
        /// <value>The modified time of/for the module.</value>
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
        /// Gets or sets the bussiness card fields of/for the module.
        /// </summary>
        /// <value>The bussiness card fields of the module.</value>
        /// <returns>List of string</returns>
        public List<string> BussinessCardFields
        {
            get
            {
                return bussinessCardFields;
            }
            set
            {
                bussinessCardFields = value;
            }
        }

        /// <summary>
        /// Sets the layouts of the module. 
        /// </summary>
        /// <value>The layouts of the module.</value>
        /// <returns>List of ZCRMLayout class instance</returns>
        public List<ZCRMLayout> Layouts { set { layouts = value; } }

        /// <summary>
        /// Gets or sets the related lists of the module.
        /// </summary>
        /// <value>The related lists of the module.</value>
        /// <returns>List of ZCRMModuleRelation class instance</returns>
        public List<ZCRMModuleRelation> RelatedLists
        {
            get
            {
                return relatedLists;
            }
            set
            {
                relatedLists = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this module is creatable.
        /// </summary>
        /// <value><c>true</c> if creatable; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Creatable
        {
            get
            {
                return creatable;
            }
            set
            {
                creatable = value;
            }
        }

        /// <summary>
        /// Gets the list of profiles which has access to the module.
        /// </summary>
        /// <value>list of profiles.</value>
        /// <returns>List of ZCRMProfile class instance</returns>
        public List<ZCRMProfile> AccessibleProfiles
        {
            get
            {
                return accessibleProfiles;
            }
            private set
            {
                accessibleProfiles = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this module is API supported.
        /// </summary>
        /// <value><c>true</c> if API supported; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool ApiSupported
        {
            get
            {
                return apiSupported;
            }
            set
            {
                apiSupported = value;
            }
        }

        public bool GlobalSearchSupported
        {
            get
            {
                return globalSearchSupported;
            }
            set
            {
                globalSearchSupported = value;
            }
        }

        public bool KanbanView
        {
            get
            {
                return kanbanView;
            }
            set
            {
                kanbanView = value;
            }
        }

        public bool FilterStatus
        {
            get
            {
                return filterStatus;
            }
            set
            {
                filterStatus = value;
            }
        }

        public bool InventoryTemplateSupported
        {
            get
            {
                return inventoryTemplateSupported;
            }
            set
            {
                inventoryTemplateSupported = value;
            }
        }

        public bool PresenceSubMenu
        {
            get
            {
                return presenceSubMenu;
            }
            set
            {
                presenceSubMenu = value;
            }
        }

        public ZCRMRelatedListProperties RelatedListProperties
        {
            get
            {
                return relatedListProperties;
            }
            set
            {
                relatedListProperties = value;
            }
        }

        public List<string> Properties
        {
            get
            {
                return properties;
            }
            set
            {
                properties = value;
            }
        }

        public int PerPage
        {
            get
            {
                return perPage;
            }
            set
            {
                perPage = value;
            }
        }

        public int Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
            }
        }

        public bool EmailTemplateSupport
        {
            get
            {
                return emailTemplateSupport;
            }
            set
            {
                emailTemplateSupport = value;
            }
        }

        public bool FilterSupported
        {
            get
            {
                return filterSupported;
            }
            set
            {
                filterSupported = value;
            }
        }

        public string DisplayField
        {
            get
            {
                return displayField;
            }
            set
            {
                displayField = value;
            }
        }

        public List<string> SearchLayoutFields
        {
            get
            {
                return searchLayoutFields;
            }
            set
            {
                searchLayoutFields = value;
            }
        }

        public bool KanbanViewSupported
        {
            get
            {
                return kanbanViewSupported;
            }
            set
            {
                kanbanViewSupported = value;
            }
        }

        public string WebLink
        {
            get
            {
                return webLink;
            }
            set
            {
                webLink = value;
            }
        }

        public int SequenceNumber
        {
            get
            {
                return sequenceNumber;
            }
            set
            {
                sequenceNumber = value;
            }
        }

        public bool QuickCreate
        {
            get
            {
                return quickCreate;
            }
            set
            {
                quickCreate = value;
            }
        }

        public bool FeedsRequired
        {
            get
            {
                return feedsRequired;
            }
            set
            {
                feedsRequired = value;
            }
        }

        public bool ScoringSupported
        {
            get
            {
                return scoringSupported;
            }
            set
            {
                scoringSupported = value;
            }
        }

        public bool WebformSupported
        {
            get
            {
                return webformSupported;
            }
            set
            {
                webformSupported = value;
            }
        }

        public List<ZCRMWebTabArguments> WebTabArguments
        {
            get
            {
                return webTabArguments;
            }
            set
            {
                webTabArguments = value;
            }
        }

        public int BusinessCardFieldLimit
        {
            get
            {
                return businessCardFieldLimit;
            }
            set
            {
                businessCardFieldLimit = value;
            }
        }

        public ZCRMModule ParentModule
        {
            get
            {
                return parentModule;
            }
            set
            {
                parentModule = value;
            }
        }

        public ZCRMTerritory Territory
        {
            get
            {
                return territory;
            }
            set
            {
                territory = value;
            }
        }

        public ZCRMCustomView CustomView
        {
            get
            {
                return customView;
            }
            set
            {
                customView = value;
            }
        }

        public List<ZCRMField> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
            }
        }

        /// <summary>
        /// To get related lists of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMModuleRelation&gt; class instance</returns>
        public BulkAPIResponse<ZCRMModuleRelation> GetRelatedLists()
        {
            return ModuleAPIHandler.GetInstance(this).GetAllRelatedLists();
        }

        /// <summary>
        /// To get RealtedList details based on RealtedList Id of the module.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="realtedListId">RealtedList Id (Long) of the module.</param>
        public APIResponse GetRelatedList(long realtedListId)
        {
            return ModuleAPIHandler.GetInstance(this).GetRelatedList(realtedListId);
        }

        /// <summary>
        /// To get all fields of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMField&gt; class instance</returns>
        public BulkAPIResponse<ZCRMField> GetAllFields()
        {
            return GetAllFields(null);
        }

        /// <summary>
        /// To get all fields of the module that are modified after the date-time specified in If-Modified-Since Header.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMField&gt; class instance.</returns>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display fields which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMField> GetAllFields(string modifiedSince)
        {
            return ModuleAPIHandler.GetInstance(this).GetAllFields(modifiedSince);
        }

        /// <summary>
        /// To get field details based on field Id of the module.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="fieldId">Field Id (Long) of the module.</param>
        public APIResponse GetField(long fieldId)
        {
            return ModuleAPIHandler.GetInstance(this).GetField(fieldId);
        }

        /// <summary>
        /// To get all layouts of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMLayout&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMLayout> GetAllLayouts()
        {
            return GetAllLayouts(null);
        }

        /// <summary>
        /// To get all layouts of the module that are modified after the date-time specified in the If-Modified-Since Header
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMLayout&gt; class instance.</returns>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display layouts which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMLayout> GetAllLayouts(string modifiedSince)
        {
            return ModuleAPIHandler.GetInstance(this).GetAllLayouts(modifiedSince);
        }

        /// <summary>
        /// To get layout details based on layout Id of the module.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="layoutId">Layout Id (Long) of the module.</param>
        public APIResponse GetLayoutDetails(long layoutId)
        {
            return ModuleAPIHandler.GetInstance(this).GetLayoutDetails(layoutId);
        }

        /// <summary>
        /// To get all custom views of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMCustomView&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMCustomView> GetAllCustomViews()
        {
            return GetAllCustomViews(null);
        }

        /// <summary>
        /// To get all custom views of the module that are modified after the date-time specified in the If-Modified-Since Header.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMCustomView&gt; class instance.</returns>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display custom views which are modified after the given input datetime (String)</param>
        public BulkAPIResponse<ZCRMCustomView> GetAllCustomViews(string modifiedSince)
        {
            return ModuleAPIHandler.GetInstance(this).GetAllCustomViews(modifiedSince);
        }

        /// <summary>
        /// To get the custom view based on custom view Id of the module.
        /// </summary>
        /// <returns>APIResponse class instance</returns>
        /// <param name="cvId">Custom view Id (Long) of module</param>
        public APIResponse GetCustomView(long cvId)
        {
            return ModuleAPIHandler.GetInstance(this).GetCustomView(cvId);
        }

        /// <summary>
        /// To create records of the module based on ZCRMRecord class instance.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="records">List of ZCRMRecord class instances</param>
        public BulkAPIResponse<ZCRMRecord> CreateRecords(List<ZCRMRecord> records, List<string> trigger = null, string lar_id = null)
        {
            if (records == null || records.Count == 0)
            {
                throw new ZCRMException(" Records list MUST NOT be null for Create operation");
            }
            return MassEntityAPIHandler.GetInstance(this).CreateRecords(records, trigger, lar_id);
        }

        /// <summary>
        /// To update the records based on List of record Id's, particular field API name and value of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="entityIds">List of entity(record) Ids (Long) of the module</param>
        /// <param name="fieldAPIName">Field API name (String) of the module</param>
        /// <param name="value">Field value (Object)</param>
        public BulkAPIResponse<ZCRMRecord> MassUpdateRecords(List<long> entityIds, string fieldAPIName, object value)
        {
            if (entityIds == null || entityIds.Count == 0)
            {
                throw new ZCRMException("Entity ID list MUST NOT be null/empty for update operation");
            }
            return MassEntityAPIHandler.GetInstance(this).MassUpdateRecords(entityIds, fieldAPIName, value);
        }

        /// <summary>
        /// To update records of the module based on List of ZCRMRecord class instance.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="records">List of ZCRMRecord class instance</param>
        public BulkAPIResponse<ZCRMRecord> UpdateRecords(List<ZCRMRecord> records, List<string> trigger = null)
        {
            if (records == null || records.Count == 0)
            {
                throw new ZCRMException("Entity ID list MUST NOT be null/empty for update operation");
            }
            return MassEntityAPIHandler.GetInstance(this).UpdateRecords(records, trigger);
        }

        /// <summary>
        /// To upsert the records of the module based on List of ZCRMRecord class instance.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="records">List of ZCRMRecord class instance.</param>
        public BulkAPIResponse<ZCRMRecord> UpsertRecords(List<ZCRMRecord> records,List<string> duplicate_check_fields = null, List<string> trigger = null, string lar_id = null)
        {
            if (records == null || records.Count == 0)
            {
                throw new ZCRMException("Record ID list MUST NOT be null/empty for upsert operation");
            }
            return MassEntityAPIHandler.GetInstance(this).UpsertRecords(records, trigger, lar_id, duplicate_check_fields);
        }

        /// <summary>
        /// To delete the records based on List of entity(record) Ids of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMEntity&gt; class instance.</returns>
        /// <param name="entityIds">List of entity(record) Ids (Long)</param>
        public BulkAPIResponse<ZCRMEntity> DeleteRecords(List<long> entityIds)
        {
            if (entityIds == null || entityIds.Count == 0)
            {
                throw new ZCRMException("Entity ID list MUST NOT be null/empty for delete operation");
            }
            return MassEntityAPIHandler.GetInstance(this).DeleteRecords(entityIds);
        }

        /// <summary>
        /// To get the record based on record Id of the module.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="entityId">Entity(record) Id (Long) of the module</param>
        public APIResponse GetRecord(long entityId)
        {
            ZCRMRecord record = ZCRMRecord.GetInstance(ApiName, entityId);
            return EntityAPIHandler.GetInstance(record).GetRecord();
        }

        /// <summary>
        /// To get all records of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMRecord> GetRecords()
        {
            return GetRecords(null, null);
        }

        /// <summary>
        /// To get all records of the module based on custom view Id of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId)
        {
            return GetRecords(cvId, null);
        }

        /// <summary>
        /// To get all records of the module with the selected fields information alone.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(List<string> fields)
        {
            return GetRecords(null, fields);
        }

        /// <summary>
        /// To get all records of the module based on custom view Id and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, List<string> fields)
        {
            return GetRecords(cvId, 1, 200, fields);
        }

        /// <summary>
        /// To get all records of the module based on custom view Id, page, perPage and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, int page, int perPage, List<string> fields)
        {
            return GetRecords(cvId, null, null, page, perPage, fields);
        }

        /// <summary>
        /// To get all records of the module based on custom view Id, sortByField, sortOrder and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, List<string> fields)
        {
            return GetRecords(cvId, sortByField, sortOrder, 1, 200, fields);
        }

        /// <summary>
        /// To get all records of the module based on custom view Id, sortByField, sortOrder, page, perPage and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, List<string> fields)
        {
            return GetRecords(cvId, sortByField, sortOrder, page, perPage, null, fields);
        }

        /// <summary>
        /// To get all records of the module based on custom view Id, sortByField, sortOrder, page, perPage, modifiedSince(Header) and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display records which are modified after the given input datetime (String)</param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, null, null, fields);
        }

        /// <summary>
        /// To get all converted records of the module based on custom view Id, sortByField, sortOrder, page, perPage, modifiedSince(Header) and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display records which are modified after the given input datetime (String)</param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetConvertedRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, "true", null, fields);
        }

        /// <summary>
        /// To get all un converted records of the module based on custom view Id, sortByField, sortOrder, page, perPage, modifiedSince(Header) and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display records which are modified after the given input datetime (String)</param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetUnConvertedRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, "false", null, fields);
        }

        /// <summary>
        /// To get all approved records of the module based on custom view Id, sortByField, sortOrder, page, perPage, modifiedSince(Header) and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display records which are modified after the given input datetime (String)</param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetApprovedRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, null, "true", fields);
        }

        /// <summary>
        /// To get all un approved records of the module based on custom view Id, sortByField, sortOrder, page, perPage, modifiedSince(Header) and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="cvId">Custom view Id (Long) of the module. Records only from the given custom view will be fetched</param>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display records which are modified after the given input datetime (String)</param>
        /// <param name="fields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetUnApprovedRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> fields)
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecords(cvId, sortByField, sortOrder, page, perPage, modifiedSince, null, "false", fields);
        }

        /// <summary>
        /// To get all deleted records of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMTrashRecord&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMTrashRecord> GetAllDeletedRecords()
        {
            return MassEntityAPIHandler.GetInstance(this).GetAllDeletedRecords();
        }

        /// <summary>
        /// To get recycle bin records of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMTrashRecord&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMTrashRecord> GetRecycleBinRecords()
        {
            return MassEntityAPIHandler.GetInstance(this).GetRecycleBinRecords();
        }

        /// <summary>
        /// To get permanently deleted records of the module.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMTrashRecord&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMTrashRecord> GetPermanentlyDeletedRecords()
        {
            return MassEntityAPIHandler.GetInstance(this).GetPermanentlyDeletedRecords();
        }

        /// <summary>
        /// To search the records based on word.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="value">Search word (String).This will return the record details based on word.</param>
        public BulkAPIResponse<ZCRMRecord> SearchByWord(string value)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByWord(value, 1, 200);
        }

        /// <summary>
        /// To search the records based on word with page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="value">Search word (String).This will return the record details based on word.</param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMRecord> SearchByWord(string value, int page, int perPage)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByWord(value, page, perPage);
        }

        /// <summary>
        /// To search the records based on criteria.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="value">Search criteria (String).This will return the record details based on criteria.</param>
        public BulkAPIResponse<ZCRMRecord> SearchByCriteria(string value)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByCriteria(value, 1, 200);
        }

        /// <summary>
        /// To search the records based on criteria with page and perPage. 
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="value">Search criteria (String). This will return the record details based on criteria.</param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMRecord> SearchByCriteria(string value, int page, int perPage)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByCriteria(value, page, perPage);
        }

        /// <summary>
        /// To search the record based on phone.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="value">Search phone number (String). This will return the record details based on phone number.</param>
        public BulkAPIResponse<ZCRMRecord> SearchByPhone(string value)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByPhone(value, 1, 200);
        }

        /// <summary>
        /// To search the record based on phone with page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="value">Search phone number (String). This will return the record details based on phone number.</param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMRecord> SearchByPhone(string value, int page, int perPage)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByPhone(value, page, perPage);
        }

        /// <summary>
        /// To search the record based on email.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance</returns>
        /// <param name="value">Search email id (String).This will return the record details based on email id.</param>
        public BulkAPIResponse<ZCRMRecord> SearchByEmail(string value)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByEmail(value, 1, 200);
        }

        /// <summary>
        /// To search the record based on email with page and perPage.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="value">Search email id (String). This will return the record details based on email id.</param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        public BulkAPIResponse<ZCRMRecord> SearchByEmail(string value, int page, int perPage)
        {
            return MassEntityAPIHandler.GetInstance(this).SearchByEmail(value, page, perPage);
        }

        /// <summary>
        /// To add the accessible profile of the module by passing ZCRMProfile class instance.
        /// </summary>
        /// <param name="profile">ZCRMProfile class instance</param>
        public void AddAccessibleProfile(ZCRMProfile profile)
        {
            AccessibleProfiles.Add(profile);
        }

        /// <summary>
        /// To get tag instance of the module.
        /// </summary>
        /// <returns>ZCRMTag class instance.</returns>
        public static ZCRMTag GetTagInstance()
        {
            return GetTagInstance(null);
        }

        /// <summary>
        /// To get tag instance of the module by passing tag Id.
        /// </summary>
        /// <returns>ZCRMTag class instance.</returns>
        /// <param name="tagid">Tag Id (Long) of the module.</param>
        public static ZCRMTag GetTagInstance(long? tagid)
        {
            return ZCRMTag.GetInstance(tagid);
        }

        /// <summary>
        /// To get all tags of the modules.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMTag&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMTag> GetTags()
        {
            if (string.IsNullOrEmpty(this.apiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for GetTags operation");
            }
            return TagAPIHandler.GetInstance(this).GetTags();
        }

        /// <summary>
        /// To returns the count of records with the given tag.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="tagid">Tag Id (Long) of the module</param>
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

        /// <summary>
        /// To create tags of the module based on ZCRMTag class instance.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMTag&gt; class instance.</returns>
        /// <param name="tags">List of ZCRMTag class instance</param>
        public BulkAPIResponse<ZCRMTag> CreateTags(List<ZCRMTag> tags)
        {
            if (string.IsNullOrEmpty(this.apiName))
            {
                throw new ZCRMException("Module Api Name MUST NOT be null/empty for CreateTags operation");
            }
            if (tags.Count <= 0)
            {
                throw new ZCRMException("Tag object list MUST NOT be null/empty for CreateTags operation");
            }
            return TagAPIHandler.GetInstance(this).CreateTags(tags);

        }

        /// <summary>
        /// To update tags of the module based on ZCRMTag class instance.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMTag&gt; class instance.</returns>
        /// <param name="tags">List of ZCRMTag class instance</param>
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

        /// <summary>
        /// To add tags to records based on List of record Ids and  List of tag names.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMTag&gt; class instance.</returns>
        /// <param name="recordIds">List of record Ids (Long) of the module</param>
        /// <param name="tagNames">List of tag names (String)of the module</param>
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

        /// <summary>
        /// To remove tags from records based on List of record Ids and List of tag names.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMTag&gt; class instance.</returns>
        /// <param name="recordIds">List of record Ids (Long) of the module</param>
        /// <param name="tagNames">List of tag names (String) of the module</param>
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
