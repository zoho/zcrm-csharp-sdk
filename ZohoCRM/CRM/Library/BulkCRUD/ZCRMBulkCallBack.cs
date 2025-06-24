using System;
namespace ZCRMSDK.CRM.Library.BulkCRUD
{
    public class ZCRMBulkCallBack
    {
        private string url;
        private string method;

        private ZCRMBulkCallBack() { }

        /// <summary>
        /// Constructor to set the callback URL and method
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        private ZCRMBulkCallBack(string url, string method)
        {
            Url = url;
            Method = method;
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkCallBack class.
        /// </summary>
        /// <returns>Instance of ZCRMBulkCallBack.</returns>
        public static ZCRMBulkCallBack GetInstance()
        {
            return new ZCRMBulkCallBack();
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkCallBack class.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns>Instance of ZCRMBulkCallBack.</returns>
        public static ZCRMBulkCallBack GetInstance(string url, string method)
        {
            return new ZCRMBulkCallBack(url, method);
        }

        /// <summary>
        /// Gets or Sets the valid URL, which should allow HTTP POST method.
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        /// Gets or Sets the HTTP method of the callback url. Only HTTP POST method is supported.
        /// </summary>
        public string Method
        {
            get { return method; }
            set { method = value; }
        }
    }
}