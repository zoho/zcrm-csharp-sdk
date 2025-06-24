using System;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.CRUD;

namespace ZCRMSDK.CRM.Library.BulkCRUD
{
    public class ZCRMBulkQuery
    {
        private string module;
        private long cvId;
        private List<string> fields;
        private int page;
        private ZCRMCriteria criteria;
        private string criteriaPattern;
        private string criteriaCondition;

        /// <summary>
        /// Method to get instance of ZCRMBulkQuery class.
        /// </summary>
        /// <returns></returns>
        public static ZCRMBulkQuery GetInstance()
        {
            return new ZCRMBulkQuery();
        }

        /// <summary>
        /// Gets or Sets the index of the record to be obtained (default is 1).
        /// </summary>
        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        /// <summary>
        /// Gets or Sets the API Name of the module to be read.
        /// </summary>
        public string ModuleAPIName
        {
            get { return module; }
            set { module = value; }
        }

        /// <summary>
        /// Gets or Sets the unique ID of the custom view whose records you want to export.
        /// </summary>
        public long CvId
        {
            get { return cvId; }
            set { cvId = value; }
        }

        /// <summary>
        /// Gets or Sets the filter(ZCRMBulkCriteria object) the records to be exported.
        /// </summary>
        public ZCRMCriteria Criteria
        {
            get { return criteria; }
            set { criteria = value; }
        }



        public string CriteriaPattern
        {
            get
            {
                return criteriaPattern;
            }
            set
            {
                criteriaPattern = value;
            }
        }

        public string CriteriaCondition
        {
            get
            {
                return criteriaCondition;
            }
            set
            {
                criteriaCondition = value;
            }
        }
    }
}