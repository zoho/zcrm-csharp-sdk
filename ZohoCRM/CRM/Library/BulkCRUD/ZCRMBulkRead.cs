using System;
using ZCRMSDK.CRM.Library.Api;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.BulkAPI.Handler;
using ZCRMSDK.CRM.Library.BulkAPI.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.BulkCRUD
{
    public class ZCRMBulkRead : ZCRMEntity, ICloneable
    {
        private long? jobId;
        private string operation;
        private string state;
        private ZCRMBulkResult result;
        private ZCRMBulkQuery query;
        private ZCRMBulkCallBack callback;
        private ZCRMUser createdBy;
        private string createdTime;
        private string moduleAPIName;
        private string fileType;

        /// <summary>
        /// constructor to set the module API name and job id
        /// </summary>
        /// <param name="moduleAPIName"></param>
        /// <param name="jobId"></param>
        private ZCRMBulkRead(string moduleAPIName, long? jobId)
        {
            ModuleAPIName = moduleAPIName;
            JobId = jobId;
        }

        /// <summary>
        ///  constructor to set the module API name.
        /// </summary>
        /// <param name="moduleAPIName"></param>
        private ZCRMBulkRead(string moduleAPIName)
        {
            ModuleAPIName = moduleAPIName;
        }

        /// <summary>
        /// constructor to set the job id.
        /// </summary>
        /// <param name="jobId"></param>
        private ZCRMBulkRead(long? jobId)
        {
            JobId = jobId;
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkRead class
        /// </summary>
        /// <param name="moduleAPIName"></param>
        /// <param name="jobId"></param>
        /// <returns>Instance of ZCRMBulkRead.</returns>
        public static ZCRMBulkRead GetInstance(string moduleAPIName, long? jobId = null)
        {
            return new ZCRMBulkRead(moduleAPIName, jobId);
        }

        /// <summary>
        /// Method to get the instance of the ZCRMBulkRead class
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns>Instance of ZCRMBulkRead.</returns>
        public static ZCRMBulkRead GetInstance(long? jobId)
        {
            return new ZCRMBulkRead(jobId);
        }

        /// <summary>
        /// Gets or Sets the API Name of the module to be read.
        /// </summary>
        public string ModuleAPIName
        {
            get { return moduleAPIName; }
            set { moduleAPIName = value; }
        }

        /// <summary>
        /// Gets or Sets the unique identifier of the bulk read job.
        /// </summary>
        public long? JobId
        {
            get { return jobId; }
            set { jobId = value; }
        }

        /// <summary>
        /// Gets or sets the user who created the bulk read job.
        /// </summary>
        public ZCRMUser CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        /// <summary>
        /// Gets or sets the created time of/for the bulk read job.
        /// </summary>
        public string CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }

        /// <summary>
        /// Gets or Sets the current status of the bulk read job.
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Gets or Sets the type of action the API completed.
        /// </summary>
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        /// <summary>
        /// Gets the Sets the callback details to the bulk read job.
        /// </summary>
        public ZCRMBulkCallBack CallBack
        {
            get { return callback; }
            set { callback = value; }
        }

        /// <summary>
        /// Gets or Sets the result details to the bulk read job. 
        /// </summary>
        public ZCRMBulkResult Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// Gets or Sets query details to the bulk read job.
        /// </summary>
        public ZCRMBulkQuery Query
        {
            get { return query; }
            set { query = value; }
        }
        /// <summary>
        /// Gets or Sets file_type value for this key as "ics" to export all records in the Events module as an ICS file.
        /// </summary>
        public string FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }
        /// <summary>
        /// Method to create a bulk read job to export records.
        /// </summary>
        /// <returns>APIResponse - APIResponse instance of the APIResponse class which holds the API response.</returns>
        public APIResponse CreateBulkReadJob()
        {
            return BulkReadAPIHandler.GetInstance(this).CreateBulkReadJob();
        }

        /// <summary>
        /// Method to get the details of a bulk read job performed previously.
        /// </summary>
        /// <returns>APIResponse - APIResponse instance of the APIResponse class which holds the API response.</returns>
        public APIResponse GetBulkReadJobDetails()
        {
            return BulkReadAPIHandler.GetInstance(this).GetBulkReadJobDetails();
        }

        /// <summary>
        /// Method download the bulk read job as a CSV file.
        /// </summary>
        /// <returns>FileAPIResponse - FileAPIResponse instance of the FileAPIResponse class which holds the response.</returns>
        public FileAPIResponse DownloadBulkReadResult()
        {
            return BulkReadAPIHandler.GetInstance(this).DownloadBulkReadResult();
        }

        /// <summary>
        /// Method to get download the result of the bulk read job and get CSV file as ZCRMRecord instance.
        /// </summary>
        /// <param name="filePath">file path to store the Downloaded file.</param>
        /// <returns>BulkResponse - BulkResponse instance of the BulkResponse class which holds the response.</returns>
        public BulkResponse DownloadANDGetRecords(string filePath)
        {
            if (filePath == null)
            {
                throw new ZCRMException("File Path must not be null for download and get records operation.");
            }
            return BulkAPIHandler.GetInstance(this).ProcessZip(filePath, true, null, APIConstants.READ, null, false);
        }

        /// <summary>
        /// Method to get CSV file as ZCRMRecord instance.
        /// </summary>
        /// <param name="filePath">file path of the downloaded file.</param>
        /// <param name="fileName">file name of the downloaded file.</param>
        /// <returns>BulkResponse instance of the BulkResponse class which holds the response.</returns>
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
            return BulkAPIHandler.GetInstance(this).ProcessZip(filePath, false, fileName, APIConstants.READ, null, false);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}