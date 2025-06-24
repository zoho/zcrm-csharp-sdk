using System;
using System.Collections.Generic;

namespace ZCRMSDK.CRM.Library.BulkCRUD
{
    public class ZCRMBulkWriteFieldMapping
    {
        private string api_name;
        private int? index;
        private string find_by;
        private string format;
        private Dictionary<string, object> default_value = new Dictionary<string, object>();

        private ZCRMBulkWriteFieldMapping() { }

        /// <summary>
        /// Constructor to set the field API name and field index
        /// </summary>
        /// <param name="api_name"></param>
        /// <param name="index"></param>
        private ZCRMBulkWriteFieldMapping(string api_name, int? index)
        {
            FieldAPIName = api_name;
            Index = index;
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWriteFieldMapping class
        /// </summary>
        /// <returns>Instance of ZCRMBulkWriteFieldMapping</returns>
        public static ZCRMBulkWriteFieldMapping GetInstance()
        {
            return new ZCRMBulkWriteFieldMapping();
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWriteFieldMapping class
        /// </summary>
        /// <param name="api_name"></param>
        /// <param name="index"></param>
        /// <returns>Instance of ZCRMBulkWriteFieldMapping</returns>
        public static ZCRMBulkWriteFieldMapping GetInstance(string api_name, int? index)
        {
            return new ZCRMBulkWriteFieldMapping(api_name, index);
        }

        /// <summary>
        /// Gets or Sets the API name of the field present in Zoho module object that you want to import.
        /// </summary>
        public string FieldAPIName
        {
            get { return api_name; }
            set { api_name = value; }
        }

        /// <summary>
        /// Gets or Sets the column index of the field you want to map to the CRM field.
        /// </summary>
        public int? Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// Gets or Sets the API name of the unique field or primary field(record ID) in the module.
        /// </summary>
        public string FindBy
        {
            get { return find_by; }
            set { find_by = value; }
        }

        /// <summary>
        /// Gets or Sets the format of field value.
        /// </summary>
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        /// <summary>
        /// Gets or Sets the default value with which the system replaces any partial or empty file column in the CSV file.
        /// </summary>
        public Dictionary<string, object> DefaultValue
        {
            get { return default_value; }
            set { default_value = value; }
        }

        /// <summary>
        /// To set the default value of the field.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetDefaultValue(string key, object value)
        {
            DefaultValue.Add(key, value);
        }
    }
}