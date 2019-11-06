using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Api;
using ZCRMSDK.CRM.Library.Api.Handler;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.BulkCRUD;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.BulkAPI.Handler
{
    public class BulkWriteAPIHandler : CommonAPIHandler, IAPIHandler
    {
        protected ZCRMBulkWrite writeRecord;

        protected BulkWriteAPIHandler(ZCRMBulkWrite writeRecord)
        {
            this.writeRecord = writeRecord;
        }

        public static BulkWriteAPIHandler GetInstance(ZCRMBulkWrite writeRecord)
        {
            return new BulkWriteAPIHandler(writeRecord);
        }

        public APIResponse UploadFile(string filePath, Dictionary<string, string> headers)
        {
            if (filePath == null)
            {
                throw new ZCRMException("File path must not be null for file upload operation.");
            }
            CommonUtil.ValidateFile(filePath);
            try
            {
                if (headers.Count <= 0)
                {
                    throw new ZCRMException("Headers must not be null for file upload operation.");
                }
                //Process Request JSON
                this.requestMethod = APIConstants.RequestMethod.POST;
                this.urlPath = ZCRMConfigUtil.GetFileUploadURL() + "/crm/" + ZCRMConfigUtil.GetApiVersion() + "/" + APIConstants.UPLOAD;
                foreach (KeyValuePair<string, string> header in headers)
                {
                    requestHeaders.Add(header.Key, header.Value);
                }

                //Fire Request
                APIResponse response = APIRequest.GetInstance(this).UploadFile(filePath);
                JObject responseData = response.ResponseJSON;
                JObject details = (JObject)responseData[APIConstants.DETAILS];
                response.Data = this.GetZCRMAttachmentObject(details);
                return response;
            }
            catch (Exception e) when ((e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse CreateBulkWriteJob()
        {
            try
            {
                if (this.writeRecord.JobId > 0)
                {
                    throw new ZCRMException("JOB ID must be null for create operation.");
                }
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = APIConstants.WRITE;
                requestBody = this.GetZCRMBulkWriteAsJSON();
                isBulk = true;
                
                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();
                JObject responseData = response.ResponseJSON;
                JObject recordDetails = (JObject)responseData[APIConstants.DETAILS];
                this.SetBulkWriteRecordProperties(recordDetails);
                response.Data = this.writeRecord;
                return response;
            }
            catch (Exception e) when ((e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse GetBulkWriteJobDetails()
        {
            try
            {
                if (this.writeRecord.JobId <= 0)
                {
                    throw new ZCRMException("JOB ID must not be null for get operation.");
                }
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = APIConstants.WRITE + "/" + this.writeRecord.JobId;
                isBulk = true;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();
                JObject recordDetails = response.ResponseJSON;
                this.SetBulkWriteRecordProperties(recordDetails);
                response.Data = this.writeRecord;
                return response;
            }
            catch (Exception e) when ((e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public FileAPIResponse DownloadBulkWriteResult(string downloadURL)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = downloadURL ?? throw new ZCRMException("Download File URL must not be null for download operation.");
                return APIRequest.GetInstance(this).DownloadFile();
            }
            catch (Exception e) when ((e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }

        }

        private ZCRMAttachment GetZCRMAttachmentObject(JObject attachmentJson)
        {
            ZCRMAttachment attachment = ZCRMAttachment.GetInstance();
            if (attachmentJson.ContainsKey("file_id") && attachmentJson["file_id"].Type != JTokenType.Null)
            {
                attachment.Id = Convert.ToInt64(attachmentJson["file_id"]);
            }
            if (attachmentJson.ContainsKey("created_time") && attachmentJson["created_time"].Type != JTokenType.Null)
            {
                attachment.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(attachmentJson["created_time"]));
            }
            return attachment;
        }

        private JObject GetZCRMBulkWriteAsJSON()
        {
            JObject recordJSON = new JObject();
            if (this.writeRecord.CharacterEncoding != null)
            {
                recordJSON.Add("character_encoding", this.writeRecord.CharacterEncoding);
            }
            if (this.writeRecord.Operation != null)
            {
                recordJSON.Add("operation", this.writeRecord.Operation);
            }
            if (this.writeRecord.Callback != null)
            {
                ZCRMBulkCallBack callback = this.writeRecord.Callback;
                JObject callBackJSON = new JObject();
                if (callback.Url != null)
                {
                    callBackJSON.Add("url", callback.Url);
                }
                if (callback.Method != null)
                {
                    callBackJSON.Add("method", callback.Method);
                }
                recordJSON.Add("callback", callBackJSON);
            }
            if (this.writeRecord.Resources != null && this.writeRecord.Resources.Count > 0)
            {
                recordJSON.Add("resource", this.GetZCRMBulkWriteResourceAsJSONArray());
            }
            return recordJSON;
        }

        private JArray GetZCRMBulkWriteResourceAsJSONArray()
        {
            JArray resource = new JArray();
            foreach (ZCRMBulkWriteResource resourceObj in writeRecord.Resources)
            {
                resource.Add(this.GetZCRMBulkWriteResourceAsJSONObject(resourceObj));
            }
            return resource;
        }

        private JObject GetZCRMBulkWriteResourceAsJSONObject(ZCRMBulkWriteResource resourceObj)
        {
            JObject resourceJSON = new JObject();
            if (resourceObj.Type != null)
            {
                resourceJSON.Add("type", resourceObj.Type);
            }
            if (resourceObj.ModuleAPIName != null)
            {
                resourceJSON.Add("module", resourceObj.ModuleAPIName);
            }
            if (resourceObj.FileId > 0)
            {
                resourceJSON.Add("file_id", resourceObj.FileId);
            }
            if ("true".Equals(resourceObj.IgnoreEmpty) || "false".Equals(resourceObj.IgnoreEmpty))
            {
                resourceJSON.Add("ignore_empty", resourceObj.IgnoreEmpty);
            }
            if (resourceObj.FindBy != null)
            {
                resourceJSON.Add("find_by", resourceObj.FindBy);
            }
            if (resourceObj.FieldMapping != null && resourceObj.FieldMapping.Count > 0)
            {
                resourceJSON.Add("field_mappings", this.GetZCRMBulkWriteFieldMappingAsJSONArray(resourceObj.FieldMapping));
            }
            return resourceJSON;
        }

        private JArray GetZCRMBulkWriteFieldMappingAsJSONArray(List<ZCRMBulkWriteFieldMapping> fieldMapping)
        {
            JArray fieldMappingJSONArray = new JArray();
            foreach (ZCRMBulkWriteFieldMapping fieldMappingObj in fieldMapping)
            {
                fieldMappingJSONArray.Add(this.GetZCRMBulkWriteFieldMappingJSONObject(fieldMappingObj));
            }
            return fieldMappingJSONArray;
        }

        private JObject GetZCRMBulkWriteFieldMappingJSONObject(ZCRMBulkWriteFieldMapping fieldMappingObj)
        {
            JObject fieldMappingJSON = new JObject();
            if (fieldMappingObj.FieldAPIName != null)
            {
                fieldMappingJSON.Add("api_name", fieldMappingObj.FieldAPIName);
            }
            if (fieldMappingObj.Index != null)
            {
                fieldMappingJSON.Add("index", fieldMappingObj.Index);
            }
            if (fieldMappingObj.DefaultValue != null && fieldMappingObj.DefaultValue.Count > 0)
            {
                fieldMappingJSON.Add("default_value", this.GetDefaultValueAsJSON(fieldMappingObj.DefaultValue));
            }
            if (fieldMappingObj.FindBy != null)
            {
                fieldMappingJSON.Add("find_by", fieldMappingObj.FindBy);
            }
            if (fieldMappingObj.Format != null)
            {
                fieldMappingJSON.Add("format", fieldMappingObj.Format);
            }
            return fieldMappingJSON;
        }

        private JObject GetDefaultValueAsJSON(Dictionary<string, object> defaultValue)
        {
            JObject defaultValueJSON = new JObject();
            foreach (KeyValuePair<string, object> defaultObj in defaultValue)
            {
                defaultValueJSON.Add(defaultObj.Key, JToken.FromObject(defaultObj.Value));
            }
            return defaultValueJSON;
        }

        private void SetBulkWriteRecordProperties(JObject recordJSON)
        {
            foreach (KeyValuePair<string, JToken> bulkRecord in recordJSON)
            {
                if (bulkRecord.Key.Equals("id") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    this.writeRecord.JobId = (long)bulkRecord.Value;
                }
                else if (bulkRecord.Key.Equals("created_by") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    JObject createdByObj = (JObject)bulkRecord.Value;
                    ZCRMUser createdBy = ZCRMUser.GetInstance((long)createdByObj["id"], (string)createdByObj["name"]);
                    this.writeRecord.CreatedBy = createdBy;
                }
                else if (bulkRecord.Key.Equals("created_time") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    this.writeRecord.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(bulkRecord.Value));
                }
                else if (bulkRecord.Key.Equals("status") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    this.writeRecord.Status = (string)bulkRecord.Value;
                }
                else if (bulkRecord.Key.Equals("character_encoding") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    this.writeRecord.CharacterEncoding = (string)bulkRecord.Value;
                }
                else if (bulkRecord.Key.Equals("resource") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    this.SetZCRMBulkWriteResourceListofObject((JArray)bulkRecord.Value);
                }
                else if (bulkRecord.Key.Equals("result") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    JObject createdObject = (JObject)bulkRecord.Value;
                    ZCRMBulkResult result = ZCRMBulkResult.GetInstance();
                    if (createdObject.ContainsKey("download_url"))
                    {
                        result.DownloadUrl = (string)createdObject["download_url"];
                    }
                    this.writeRecord.Result = result;
                }
                else if (bulkRecord.Key.Equals("operation") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    this.writeRecord.Operation = (string)bulkRecord.Value;
                }
                else if (bulkRecord.Key.Equals("callback") && bulkRecord.Value.Type != JTokenType.Null)
                {
                    ZCRMBulkCallBack callback = ZCRMBulkCallBack.GetInstance();
                    JObject jsonObj = (JObject)bulkRecord.Value;
                    if (jsonObj.ContainsKey("url"))
                    {
                        callback.Url = (string)jsonObj["url"];
                    }
                    if (jsonObj.ContainsKey("method"))
                    {
                        callback.Method = (string)jsonObj["method"];
                    }
                    this.writeRecord.Callback = callback;
                }
            }
        }

        private void SetZCRMBulkWriteResourceListofObject(JArray resourceJSONArray)
        {
            List<ZCRMBulkWriteResource> resources = new List<ZCRMBulkWriteResource>();
            foreach (JObject resourceJSON in resourceJSONArray)
            {
                resources.Add(SetZCRMBulkWriteResourceObject(resourceJSON));
            }
            this.writeRecord.Resources = resources;
        }

        private ZCRMBulkWriteResource SetZCRMBulkWriteResourceObject(JObject resourceJSON)
        {
            ZCRMBulkWriteResource resourceObj = ZCRMBulkWriteResource.GetInstance();
            if (resourceJSON.ContainsKey("status") && resourceJSON["status"].Type != JTokenType.Null)
            {
                resourceObj.Status = (string)resourceJSON["status"];
            }
            if (resourceJSON.ContainsKey("message") && resourceJSON["message"].Type != JTokenType.Null)
            {
                resourceObj.Message = (string)resourceJSON["message"];
            }
            if (resourceJSON.ContainsKey("type") && resourceJSON["type"].Type != JTokenType.Null)
            {
                resourceObj.Type = (string)resourceJSON["type"];
            }
            if (resourceJSON.ContainsKey("module") && resourceJSON["module"].Type != JTokenType.Null)
            {
                resourceObj.ModuleAPIName = (string)resourceJSON["module"];
            }
            if (resourceJSON.ContainsKey("field_mappings") && resourceJSON["field_mappings"].Type != JTokenType.Null)
            {
                List<ZCRMBulkWriteFieldMapping> fieldMappings = new List<ZCRMBulkWriteFieldMapping>();
                JArray jarr = (JArray)resourceJSON["field_mappings"];
                foreach (JObject fieldJSON in jarr)
                {
                    fieldMappings.Add(this.SetZCRMBulkWriteFieldMappingObject(fieldJSON));
                }
                resourceObj.FieldMapping = fieldMappings;
            }
            if (resourceJSON.ContainsKey("file") && resourceJSON["file"].Type != JTokenType.Null)
            {
                resourceObj.FileStatus = SetZCRMBulkWriteFileObject((JObject)resourceJSON["file"]);
            }
            if (resourceJSON.ContainsKey("ignore_empty") && resourceJSON["ignore_empty"].Type != JTokenType.Null)
            {
                resourceObj.IgnoreEmpty = (bool)resourceJSON["ignore_empty"];
            }
            if (resourceJSON.ContainsKey("find_by") && resourceJSON["find_by"].Type != JTokenType.Null)
            {
                resourceObj.FindBy = (string)resourceJSON["find_by"];
            }
            return resourceObj;
        }

        private ZCRMBulkWriteFieldMapping SetZCRMBulkWriteFieldMappingObject(JObject fieldMappingJSON)
        {
            ZCRMBulkWriteFieldMapping fieldMappingObj = ZCRMBulkWriteFieldMapping.GetInstance();
            if (fieldMappingJSON.ContainsKey("api_name") && fieldMappingJSON["api_name"].Type != JTokenType.Null)
            {
                fieldMappingObj.FieldAPIName = (string)fieldMappingJSON["api_name"];
            }
            if (fieldMappingJSON.ContainsKey("index") && fieldMappingJSON["index"].Type != JTokenType.Null)
            {
                fieldMappingObj.Index = Convert.ToInt32(fieldMappingJSON["index"]);
            }
            if (fieldMappingJSON.ContainsKey("find_by") && fieldMappingJSON["find_by"].Type != JTokenType.Null)
            {
                fieldMappingObj.FindBy = (string)fieldMappingJSON["find_by"];
            }
            if (fieldMappingJSON.ContainsKey("format") && fieldMappingJSON["format"].Type != JTokenType.Null)
            {
                fieldMappingObj.Format = (string)fieldMappingJSON["format"];
            }
            if (fieldMappingJSON.ContainsKey("default_value") && fieldMappingJSON["default_value"].Type != JTokenType.Null)
            {
                JObject defaultValueJSON = (JObject)fieldMappingJSON["default_value"];
                foreach (KeyValuePair<string, JToken> defaultValue in defaultValueJSON)
                {
                    fieldMappingObj.SetDefaultValue(defaultValue.Key, (object)defaultValue.Value);
                }
            }
            return fieldMappingObj;
        }

        private ZCRMBulkWriteFileStatus SetZCRMBulkWriteFileObject(JObject fileJSON)
        {
            ZCRMBulkWriteFileStatus fileObj = ZCRMBulkWriteFileStatus.GetInstance();
            if (fileJSON.ContainsKey("status") && fileJSON["status"].Type != JTokenType.Null)
            {
                fileObj.Status = (string)fileJSON["status"];
            }
            if (fileJSON.ContainsKey("name") && fileJSON["name"].Type != JTokenType.Null)
            {
                fileObj.FileName = (string)fileJSON["name"];
            }
            if (fileJSON.ContainsKey("added_count") && fileJSON["added_count"].Type != JTokenType.Null)
            {
                fileObj.AddedCount = (int)fileJSON["added_count"];
            }
            if (fileJSON.ContainsKey("skipped_count") && fileJSON["skipped_count"].Type != JTokenType.Null)
            {
                fileObj.SkippedCount = (int)fileJSON["skipped_count"];
            }
            if (fileJSON.ContainsKey("updated_count") && fileJSON["updated_count"].Type != JTokenType.Null)
            {
                fileObj.UpdatedCount = (int)fileJSON["updated_count"];
            }
            if (fileJSON.ContainsKey("total_count") && fileJSON["total_count"].Type != JTokenType.Null)
            {
                fileObj.TotalCount = (int)fileJSON["total_count"];
            }
            return fileObj;
        }
    }
}