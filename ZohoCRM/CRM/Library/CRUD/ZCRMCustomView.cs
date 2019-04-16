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

        /// <summary>
        /// To get ZCRMCustomView instance by passing modules' APIName and custom view Id.
        /// </summary>
        /// <returns>ZCRMCustomView class instance.</returns>
        /// <param name="moduleAPIName">Modules' API Name (String)</param>
        /// <param name="customViewId">Module custom view Id (Long)</param>
        public static ZCRMCustomView GetInstance(string moduleAPIName, long customViewId)
        {
            return new ZCRMCustomView(moduleAPIName, customViewId);
        }

        /// <summary>
        /// Gets the modules' API name.
        /// </summary>
        /// <value>The modules' API name.</value>
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
        /// Gets or sets the custom view Id.
        /// </summary>
        /// <value>The custom view Id.</value>
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
        /// Gets or sets the name of/for the custom view.
        /// </summary>
        /// <value>The name of custom view.</value>
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
        /// Gets or sets the system name of/for the custom view.
        /// </summary>
        /// <value>The system name of the custom view.</value>
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
        /// Gets or sets the display name of/for the custom view.
        /// </summary>
        /// <value>The display name of the custom view.</value>
        /// <returns>String</returns>
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        /// <summary>
        /// Gets or sets the fields of/for the custom view.
        /// </summary>
        /// <value>The fields of the custom view.</value>
        /// <returns>List of string</returns>
        public List<string> Fields
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
        /// Gets or sets the sort order of/for the custom view.
        /// </summary>
        /// <value>The sort order preference - asc/desc.</value>
        /// <returns>CommonUtil.SortOrder(asc, desc) response </returns>
        public CommonUtil.SortOrder SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                sortOrder = value;
            }
        }

        /// <summary>
        /// Gets or sets the sort value of/for custom view.
        /// </summary>
        /// <value>The sort value of/for custom view.</value>
        /// <returns>String</returns>
        public string SortBy
        {
            get
            {
                return sortBy;
            }
            set
            {
                sortBy = value;
            }
        }

        /// <summary>
        /// Gets or sets the category of/for the custom view.
        /// </summary>
        /// <value>The category of the custom view.</value>
        /// <returns>String</returns>
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this custom view is default.
        /// </summary>
        /// <value><c>true</c> if isdefault; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Isdefault
        {
            get
            {
                return isdefault;
            }
            set
            {
                isdefault = value;
            }
        }

        /// <summary>
        /// Gets or sets the favourite of/for the custom view.
        /// </summary>
        /// <value>The favourite of the custom view.</value>
        /// <returns>Integer</returns>
        public int Favourite
        {
            get
            {
                return favourite;
            }
            set
            {
                favourite = value;
            }
        }

        /// <summary>
        /// To get records of the custom view.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        public BulkAPIResponse<ZCRMRecord> GetRecords()
        {
            return GetRecords(null);
        }

        /// <summary>
        /// To get records of the custom view with the selected fields information alone.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="recordFields">List of field API names (String).</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(List<string> recordFields)
        {
            return GetRecords(1, 200, recordFields);
        }

        /// <summary> 
        /// To get records of the custom view based on page, perPage and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="recordFields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(int page, int perPage, List<string> recordFields)
        {
            return GetRecords(null, null, page, perPage, null, recordFields);   
        }

        /// <summary>
        /// To get records of the custom view based on sortByField, sortOrder and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="recordFields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(string sortByField, CommonUtil.SortOrder? sortOrder, List<string> recordFields)
        {
            return GetRecords(sortByField, sortOrder, 1, 200, null, recordFields);
        }

        /// <summary>
        /// To get records of the custom view based on sortByField, sortOrderField,page, perPage, modifiedSince(Header) and selected fields information.
        /// </summary>
        /// <returns>BulkAPIResponse&lt;ZCRMRecord&gt; class instance.</returns>
        /// <param name="sortByField">To sort the records based on this field (String)</param>
        /// <param name="sortOrder">Order preference - CommonUtil.SortOrder {asc/desc} <example>CommonUtil.SortOrder.asc</example></param>
        /// <param name="page">Starting page index (Integer)</param>
        /// <param name="perPage">Number of records per page (Integer)</param>
        /// <param name="modifiedSince">DateTime(ISO8601 format) to display records which are modified after the given input datetime (String)</param>
        /// <param name="recordFields">List of field API names (String)</param>
        public BulkAPIResponse<ZCRMRecord> GetRecords(string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, List<string> recordFields)
        {
            return ZCRMModule.GetInstance(moduleAPIName).GetRecords(Convert.ToInt64(Id), sortByField, sortOrder, page, perPage, modifiedSince, recordFields);   
        }
    }
}
