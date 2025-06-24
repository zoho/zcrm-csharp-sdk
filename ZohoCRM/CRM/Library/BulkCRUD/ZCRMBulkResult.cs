using System;
namespace ZCRMSDK.CRM.Library.BulkCRUD
{
    public class ZCRMBulkResult
    {
        private int page;
        private int count;
        private string downloadUrl;
        private long perPage;
        private bool moreRecords;

        /// <summary>
        /// Method to get instance of ZCRMBulkResult class
        /// </summary>
        /// <returns>Instance of ZCRMBulkResult</returns>
        public static ZCRMBulkResult GetInstance()
        {
            return new ZCRMBulkResult();
        }

        /// <summary>
        /// Gets or Sets the range of the number of records exported.
        /// </summary>
        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        /// <summary>
        /// Gets or Sets the actual number of records exported.
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        /// <summary>
        /// Gets or Sets the URL which contains the CSV file
        /// </summary>
        public string DownloadUrl
        {
            get { return downloadUrl; }
            set { downloadUrl = value; }
        }

        /// <summary>
        /// Gets or Sets the number of records in each page.
        /// </summary>
        public long PerPage
        {
            get { return perPage; }
            set { perPage = value; }
        }

        /// <summary>
        /// Gets or Sets the response can be used to detect if there are any further records.
        /// </summary>
        public bool MoreRecords
        {
            get { return moreRecords; }
            set { moreRecords = value; }
        }
    }
}
