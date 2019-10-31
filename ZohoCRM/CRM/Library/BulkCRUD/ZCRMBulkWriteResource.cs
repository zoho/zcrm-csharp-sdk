using System;
using System.Collections.Generic;

namespace ZCRMSDK.CRM.Library.BulkCRUD
{
    public class ZCRMBulkWriteResource
    {
        private string status;
        private string message;
        private string type;
        private string module;
        private long fileId;
        private bool ignore_empty;
        private string findBy;
        private List<ZCRMBulkWriteFieldMapping> field_mappings = new List<ZCRMBulkWriteFieldMapping>();
        private ZCRMBulkWriteFileStatus fileStatus;

        private ZCRMBulkWriteResource() { }

        /// <summary>
        /// Constructor to set the module name and file id
        /// </summary>
        /// <param name="moduleAPIName"></param>
        /// <param name="fileId"></param>
	    private ZCRMBulkWriteResource(string moduleAPIName, long fileId)
        {
            ModuleAPIName = moduleAPIName;
            FileId = fileId;
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWriteResource class
        /// </summary>
        /// <returns>Instance of ZCRMBulkWriteResource</returns>
	    public static ZCRMBulkWriteResource GetInstance()
        {
            return new ZCRMBulkWriteResource();
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWriteResource class
        /// </summary>
        /// <param name="moduleAPIName"></param>
        /// <param name="file_id"></param>
        /// <returns>Instance of ZCRMBulkWriteResource</returns>
	    public static ZCRMBulkWriteResource GetInstance(string moduleAPIName, long file_id)
        {
            return new ZCRMBulkWriteResource(moduleAPIName, file_id);
        }

        /// <summary>
        /// Gets or Sets the status of the bulk write job for that module.
        /// </summary>
	    public string Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets or Sets the resource error message.
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// Gets or Sets the type of the module that you want to import. The value is data.
        /// </summary>
	    public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Gets or Sets the API name of the module that is imported.
        /// </summary>
        public string ModuleAPIName
        {
            get { return module; }
            set { module = value; }
        }

        /// <summary>
        /// Gets or Sets the file_id obtained from file upload API.
        /// </summary>
        public long FileId
        {
            get { return fileId; }
            set { fileId = value; }
        }

        /// <summary>
        /// Gets or Sets the IgnoreEmpty. True - Ignores the empty values. False or empty - with empty values . The default value is false.
        /// </summary>
        public bool IgnoreEmpty
        {
            get { return ignore_empty; }
            set { ignore_empty = value; }
        }

        /// <summary>
        /// Gets or Sets the API name of a unique field or ID of a record. 
        /// </summary>
        public string FindBy
        {
            get { return findBy; }
            set { findBy = value; }
        }

        /// <summary>
        /// Gets or Sets the field_mappings properties table for information on field_mappings object.
        /// </summary>
        public List<ZCRMBulkWriteFieldMapping> FieldMapping
        {
            get { return field_mappings; }
            set { field_mappings = value; }
        }

        /// <summary>
        /// Gets or Sets the file properties table for information on ZCRMBulkWriteFile object.
        /// </summary>
        public ZCRMBulkWriteFileStatus FileStatus
        {
            get { return fileStatus; }
            set { fileStatus = value; }
        }

        /// <summary>
        /// To set ZCRMBulkWriteFieldMapping object.
        /// </summary>
        /// <param name="field_mappings"></param>
        public void SetFieldMapping(ZCRMBulkWriteFieldMapping field_mappings)
        {
            this.field_mappings.Add(field_mappings);
        }
    }
}