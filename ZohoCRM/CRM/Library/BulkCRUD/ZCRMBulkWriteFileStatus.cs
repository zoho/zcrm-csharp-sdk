using System;
namespace ZCRMSDK.CRM.Library.BulkCRUD
{
    public class ZCRMBulkWriteFileStatus
    {
        private string status;
        private string name;
        private int added_count;
        private int skipped_count;
        private int updated_count;
        private int total_count;

        private ZCRMBulkWriteFileStatus() { }

        /// <summary>
        /// Constructor to set the file name
        /// </summary>
        /// <param name="fileName"></param>
        private ZCRMBulkWriteFileStatus(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWriteFile class
        /// </summary>
        /// <returns>Instance of ZCRMBulkWriteFile</returns>
        public static ZCRMBulkWriteFileStatus GetInstance()
        {
            return new ZCRMBulkWriteFileStatus();
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWriteFile class
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ZCRMBulkWriteFileStatus GetInstance(string fileName)
        {
            return new ZCRMBulkWriteFileStatus(fileName);
        }

        /// <summary>
        /// Gets or Sets the status of the bulk write job for that file.
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets or Sets the name of the CSV file which will get downloaded.
        /// </summary>
        public string FileName
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or Sets the number of records added or imported.
        /// </summary>
        public int AddedCount
        {
            get { return added_count; }
            set { added_count = value; }
        }

        /// <summary>
        /// Gets or Sets the number of records skipped due to some issues.
        /// </summary>
        public int SkippedCount
        {
            get { return skipped_count; }
            set { skipped_count = value; }
        }

        /// <summary>
        /// Gets or Sets the number of records updated during bulk update.
        /// </summary>
        public int UpdatedCount
        {
            get { return updated_count; }
            set { updated_count = value; }
        }

        /// <summary>
        /// Gets or Sets the total number of records inserted, updated, or skipped during bulk import.
        /// </summary>
        public int TotalCount
        {
            get { return total_count; }
            set { total_count = value; }
        }
    }
}