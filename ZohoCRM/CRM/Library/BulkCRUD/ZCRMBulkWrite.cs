using System;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Api;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.BulkAPI.Handler;
using ZCRMSDK.CRM.Library.BulkAPI.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.BulkCRUD
{
    public class ZCRMBulkWrite : ZCRMEntity
    {
        private long jobId;
        private string character_encoding;
        private string operation;
        private ZCRMBulkCallBack callback;
        private ZCRMUser created_by;
        private string created_time;
        private string status;
        private List<ZCRMBulkWriteResource> resources = new List<ZCRMBulkWriteResource>();
        private ZCRMBulkResult result;
        private string moduleAPIName;

        private ZCRMBulkWrite() { }

        /// <summary>
        /// Constructor to set the operation
        /// </summary>
        /// <param name="operation"></param>
        private ZCRMBulkWrite(string operation)
        {
            Operation = operation;
        }

        /// <summary>
        /// Constructor to set the jobId and module API name
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="moduleAPIName"></param>
        private ZCRMBulkWrite(long jobId, string moduleAPIName)
        {
            JobId = jobId;
            ModuleAPIName = moduleAPIName;
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWrite class
        /// </summary>
        /// <returns>Instance of ZCRMBulkWrite</returns>
        public static ZCRMBulkWrite GetInstance()
        {
            return new ZCRMBulkWrite();
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWrite class
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>Instance of ZCRMBulkWrite</returns>
        public static ZCRMBulkWrite GetInstance(string operation)
        {
            return new ZCRMBulkWrite(operation);
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkWrite class
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="moduleAPIName"></param>
        /// <returns>Instance of ZCRMBulkWrite</returns>
        public static ZCRMBulkWrite GetInstance(long jobId, string moduleAPIName = null)
        {
            return new ZCRMBulkWrite(jobId, moduleAPIName);
        }

        /// <summary>
        /// Gets or Sets the module API name of the bulk write job.
        /// </summary>
        public string ModuleAPIName
        {
            get { return moduleAPIName; }
            set { moduleAPIName = value; }
        }

        /// <summary>
        /// Gets or Sets the unique identifier of the bulk write job.
        /// </summary>
        public long JobId
        {
            get { return jobId; }
            set { jobId = value; }
        }

        /// <summary>
        /// Gets or Sets the character encoding of the uploaded file.
        /// </summary>
        public string CharacterEncoding
        {
            get { return character_encoding; }
            set { character_encoding = value; }
        }

        /// <summary>
        /// Gets or Sets the type of operation you want to perform on the bulk write job(values are[insert, update, upsert]).
        /// </summary>
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        /// <summary>
        /// Gets or Sets the ZCRMBulkCallBack object.
        /// </summary>
        public ZCRMBulkCallBack Callback
        {
            get { return callback; }
            set { callback = value; }
        }

        /// <summary>
        /// Gets or sets the user who created the bulk write job.
        /// </summary>
        public ZCRMUser CreatedBy
        {
            get { return created_by; }
            set { created_by = value; }
        }

        /// <summary>
        /// Gets or sets the created time of/for the bulk write job. 
        /// </summary>
        public string CreatedTime
        {
            get { return created_time; }
            set { created_time = value; }
        }

        /// <summary>
        /// Gets or Sets the current status of the bulk write job.
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets or Sets the List ZCRMBulkWriteResource object
        /// </summary>
        public List<ZCRMBulkWriteResource> Resources
        {
            get { return resources; }
            set { resources = value; }
        }

        /// <summary>
        /// To set the ZCRMBulkWriteResource object
        /// </summary>
        /// <param name="resource">ZCRMBulkWriteResource object</param>
        public void SetResource(ZCRMBulkWriteResource resource)
        {
            this.resources.Add(resource);
        }

        /// <summary>
        /// Gets or Sets the ZCRMBulkResult object.
        /// </summary>
        public ZCRMBulkResult Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// Method to upload a CSV file in ZIP format for bulk write API.
        /// </summary>
        /// <param name="filePath">uploaded zip file path</param>
        /// <param name="headers">uploaded file request headers</param>
        /// <returns>APIResponse - APIResponse instance of the APIResponse class which holds the API response.</returns>
        public APIResponse UploadFile(string filePath, Dictionary<string, string> headers)
        {
            return BulkWriteAPIHandler.GetInstance(this).UploadFile(filePath, headers);
        }

        /// <summary>
        /// Method to create a bulk write job.
        /// </summary>
        /// <returns>APIResponse - APIResponse instance of the APIResponse class which holds the API response.</returns>
        public APIResponse CreateBulkWriteJob()
        {
            return BulkWriteAPIHandler.GetInstance(this).CreateBulkWriteJob();
        }

        /// <summary>
        /// Method to get the details of a bulk write job performed previously.
        /// </summary>
        /// <returns>APIResponse - APIResponse instance of the APIResponse class which holds the API response.</returns>
        public APIResponse GetBulkWriteJobDetails()
        {
            return BulkWriteAPIHandler.GetInstance(this).GetBulkWriteJobDetails();
        }

        /// <summary>
        /// Method to download the result of the bulk write job as a CSV file.
        /// </summary>
        /// <param name="downloadedFileURL">the download URL from which you can download the result(CSV file) of the bulk write job.</param>
        /// <returns>FileAPIResponse - FileAPIResponse instance of the FileAPIResponse class which holds the response.</returns>
        public FileAPIResponse DownloadBulkWriteResult(string downloadedFileURL)
        {
            return BulkWriteAPIHandler.GetInstance(this).DownloadBulkWriteResult(downloadedFileURL);
        }

        /// <summary>
        /// Method to get download the result of the bulk write job and get CSV file as ZCRMRecord instance.
        /// </summary>
        /// <param name="filePath">file path to store the downloaded file.</param>
        /// <param name="downloadFileURL">The download URL from which you can download the result(CSV file) of the bulk write job.</param>
        /// <returns>CSVFileResponse - CSVFileResponse instance of the CSVFileResponse class which holds the response.</returns>
        public BulkResponse DownloadANDGetRecords(string filePath, string downloadFileURL)
        {
            if (downloadFileURL == null)
            {
                throw new ZCRMException("Download File URL must not be null for download and get records operation.");
            }
            if (filePath == null)
            {
                throw new ZCRMException("File Path must not be null for download and get records operation.");
            }
            return BulkAPIHandler.GetInstance(this).ProcessZip(filePath, true, null, APIConstants.WRITE, downloadFileURL, false);
        }

        /// <summary>
        /// Method to get CSV file as ZCRMRecord instance.
        /// </summary>
        /// <param name="filePath">file path of the downloaded file.</param>
        /// <param name="fileName">file name of the downloaded file.</param>
        /// <returns>CSVFileResponse - CSVFileResponse instance of the CSVFileResponse class which holds the response.</returns>
        public BulkResponse GetRecords(string filePath, string fileName)
        {
            if (fileName == null)
            {
                throw new ZCRMException("File Name must not be null for get records operation.");
            }
            if (filePath == null)
            {
                throw new ZCRMException("File Path must not be null for get records operation.");
            }
            return BulkAPIHandler.GetInstance(this).ProcessZip(filePath, false, fileName, APIConstants.WRITE, null, false);
        }

        /// <summary>
        /// Method to get download the result of the bulk write job and get CSV file as failed ZCRMRecord instance.
        /// </summary>
        /// <param name="filePath">file path to store the downloaded file.</param>
        /// <param name="downloadFileURL">The download URL from which you can download the result(CSV file) of the bulk write job.</param>
        /// <returns>CSVFileResponse - CSVFileResponse instance of the CSVFileResponse class which holds the response.</returns>
        public BulkResponse DownloadANDGetFailedRecords(string filePath, string downloadFileURL)
        {
            if (downloadFileURL == null)
            {
                throw new ZCRMException("Download File URL must not be null for download and get failed records operation.");
            }
            if (filePath == null)
            {
                throw new ZCRMException("File Path must not be null for download and get failed records operation.");
            }
            return BulkAPIHandler.GetInstance(this).ProcessZip(filePath, true, null, APIConstants.WRITE, downloadFileURL, true);
        }

        /// <summary>
        /// Method to get CSV file as failed ZCRMRecord instance.
        /// </summary>
        /// <param name="filePath">file path of the downloaded file.</param>
        /// <param name="fileName">file name of the downloaded file.</param>
        /// <returns>CSVFileResponse - CSVFileResponse instance of the CSVFileResponse class which holds the response.</returns>
        public BulkResponse GetFailedRecords(string filePath, string fileName)
        {
            if (fileName == null)
            {
                throw new ZCRMException("File Name must not be null for get failed records operation.");
            }
            if (filePath == null)
            {
                throw new ZCRMException("File Path must not be null for get failed records operation.");
            }
            return BulkAPIHandler.GetInstance(this).ProcessZip(filePath, false, fileName, APIConstants.WRITE, null, true);
        }
    }
}
