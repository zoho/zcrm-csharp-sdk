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
    public class BulkReadAPIHandler : CommonAPIHandler, IAPIHandler
    {
        //NOTE:Property not used;
        protected ZCRMBulkRead readRecord;
        private int index;

        protected BulkReadAPIHandler(ZCRMBulkRead readRecord)
        {
            this.readRecord = readRecord;
        }

        public static BulkReadAPIHandler GetInstance(ZCRMBulkRead readRecord)
        {
            return new BulkReadAPIHandler(readRecord);
        }

        public APIResponse GetBulkReadJobDetails()
        {
            try
            {
                if (this.readRecord.JobId == null)
                {
                    throw new ZCRMException("JOB ID must not be null for get operation.");
                }
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = APIConstants.READ + "/" + this.readRecord.JobId;
                isBulk = true;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject recordDetails = (JObject)responseDataArray[0];
                this.SetBulkReadRecordProperties(recordDetails);
                response.Data = this.readRecord;
                return response;
            }
            catch (Exception e) when ((e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse CreateBulkReadJob()
        {
            try
            {
                if (this.readRecord.JobId != null)
                {
                    throw new ZCRMException("JOB ID must be null for create operation.");
                }
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = APIConstants.READ;
                requestBody = this.GetZCRMBulkQueryAsJSON();
                isBulk = true;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject responseData = (JObject)responseDataArray[0];
                JObject recordDetails = (JObject)responseData[APIConstants.DETAILS];
                this.SetBulkReadRecordProperties(recordDetails);
                response.Data = this.readRecord;
                return response;
            }
            catch (Exception e) when ((e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public FileAPIResponse DownloadBulkReadResult()
        {
            try
            {
                if (this.readRecord.JobId == null)
                {
                    throw new ZCRMException("JOB ID must not be null for get bulk read result operation.");
                }
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = APIConstants.READ + "/" + this.readRecord.JobId + "/" + APIConstants.RESULT;
                isBulk = true;
                return APIRequest.GetInstance(this).DownloadFile();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        private void SetBulkReadRecordProperties(JObject recordJSON)
        {
            foreach (KeyValuePair<string, JToken> token in recordJSON)
            {
                string fieldAPIName = token.Key;
                if (fieldAPIName.Equals("id") && token.Value.Type != JTokenType.Null)
                {
                    this.readRecord.JobId = Convert.ToInt64(token.Value);
                }
                else if (fieldAPIName.Equals("operation") && token.Value.Type != JTokenType.Null)
                {
                    this.readRecord.Operation = token.Value.ToString();
                }
                else if (fieldAPIName.Equals("state") && token.Value.Type != JTokenType.Null)
                {
                    this.readRecord.State = token.Value.ToString();
                }
                else if (fieldAPIName.Equals("created_by") && token.Value.Type != JTokenType.Null)
                {
                    JObject createdObject = (JObject)token.Value;
                    ZCRMUser createdUser = ZCRMUser.GetInstance(Convert.ToInt64(createdObject["id"]), (string)createdObject["name"]);
                    this.readRecord.CreatedBy = createdUser;
                }
                else if (fieldAPIName.Equals("created_time") && token.Value.Type != JTokenType.Null)
                {
                    this.readRecord.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(token.Value));
                }
                else if (fieldAPIName.Equals("result") && token.Value.Type != JTokenType.Null)
                {
                    this.readRecord.Result = this.SetZCRMResultObject((JObject)token.Value);
                }
                else if (fieldAPIName.Equals("query") && token.Value.Type != JTokenType.Null)
                {
                    this.readRecord.Query = this.SetZCRMBulkQueryObject((JObject)token.Value);
                }
                else if (fieldAPIName.Equals("callback") && token.Value.Type != JTokenType.Null)
                {
                    this.readRecord.CallBack = this.SetZCRMBulkCallBackObject((JObject)token.Value);
                }
                else if (fieldAPIName.Equals("file_type") && token.Value.Type != JTokenType.Null)
                {
                    this.readRecord.FileType = token.Value.ToString();
                }
            }
        }

        private ZCRMBulkQuery SetZCRMBulkQueryObject(JObject queryValue)
        {
            ZCRMBulkQuery query = ZCRMBulkQuery.GetInstance();
            if (queryValue.ContainsKey("module") && queryValue["module"].Type != JTokenType.Null)
            {
                this.readRecord.ModuleAPIName = (string)queryValue["module"];
                query.ModuleAPIName = (string)queryValue["module"];
            }
            if (queryValue.ContainsKey("page") && queryValue["page"].Type != JTokenType.Null)
            {
                query.Page = (int)queryValue["page"];
            }
            if (queryValue.ContainsKey("cvid") && queryValue["cvid"].Type != JTokenType.Null)
            {
                query.CvId = (long)queryValue["cvid"];
            }
            if (queryValue.ContainsKey("fields") && queryValue["fields"].Type != JTokenType.Null)
            {
                JArray jarr = (JArray)queryValue["fields"];
                List<string> fields = new List<string>();
                foreach (string fieldValue in jarr)
                {
                    fields.Add(fieldValue);
                }
                query.Fields = fields;
            }
            if (queryValue.ContainsKey("criteria") && queryValue["criteria"].Type != JTokenType.Null)
            {
                index = 1;
                query.Criteria = this.SetZCRMCriteriaObject((JObject)queryValue["criteria"]);
                query.CriteriaPattern = query.Criteria.Pattern;
                query.CriteriaCondition = query.Criteria.Criteria;
            }
            return query;
        }

        private ZCRMCriteria SetZCRMCriteriaObject(JObject criteriaJSON)
        {
            ZCRMCriteria recordCriteria = ZCRMCriteria.GetInstance();
            if (criteriaJSON.ContainsKey("api_name") && criteriaJSON["api_name"].Type != JTokenType.Null)
            {
                recordCriteria.FieldAPIName = criteriaJSON["api_name"].ToString();
            }
            if (criteriaJSON.ContainsKey("comparator") && criteriaJSON["comparator"].Type != JTokenType.Null)
            {
                recordCriteria.Comparator = criteriaJSON["comparator"].ToString();
            }
            if (criteriaJSON.ContainsKey("value") && criteriaJSON["value"].Type != JTokenType.Null)
            {
                recordCriteria.Value = criteriaJSON["value"];
                recordCriteria.Index = index;
                recordCriteria.Pattern = Convert.ToString(index);
                index++;
                recordCriteria.Criteria = "(" + criteriaJSON["api_name"].ToString() + ":" + criteriaJSON["comparator"].ToString() + ":" + criteriaJSON["value"].ToString() + ")";
            }
            List<ZCRMCriteria> recordData = new List<ZCRMCriteria>();
            if (criteriaJSON.ContainsKey("group") && criteriaJSON["group"].Type != JTokenType.Null)
            {
                JArray jarr = (JArray)criteriaJSON["group"];
                foreach (JObject groupJSON in jarr)
                {
                    recordData.Add(SetZCRMCriteriaObject(groupJSON));
                }
                recordCriteria.Group = recordData;
            }
            if (criteriaJSON.ContainsKey("group_operator") && criteriaJSON["group_operator"].Type != JTokenType.Null)
            {
                string criteria = "(", pattern = "(";
                recordCriteria.GroupOperator = criteriaJSON["group_operator"].ToString();
                int count = recordData.Count, i = 0;
                foreach (ZCRMCriteria criteriaObj in recordData)
                {
                    i++;
                    criteria +=  criteriaObj.Criteria;
                    pattern +=  criteriaObj.Pattern;
                    if (i < count)
                    {
                        criteria += recordCriteria.GroupOperator;
                        pattern += recordCriteria.GroupOperator;
                    }
                }
                recordCriteria.Criteria = criteria + ")";
                recordCriteria.Pattern = pattern + ")";
            }
            return recordCriteria;
        }

        private JObject GetZCRMBulkQueryAsJSON()
        {
            JObject requestBodyObject = new JObject();
            JObject recordJSON = new JObject();
            if (this.readRecord.ModuleAPIName != null)
            {
                recordJSON.Add("module", this.readRecord.ModuleAPIName);
            }
            if (this.readRecord.Query != null)
            {
                ZCRMBulkQuery query = this.readRecord.Query;
                if (query.Fields != null)
                {
                    recordJSON.Add("fields", this.GetFieldsAsJSONArray(query.Fields));
                }
                if (query.Page > 0)
                {
                    recordJSON.Add("page", query.Page);
                }
                if (query.Criteria != null)
                {
                    recordJSON.Add("criteria", this.GetCriteriaAsJSONObject(query.Criteria));
                }
                if (query.CvId > 0)
                {
                    recordJSON.Add("cvid", query.CvId);
                }
            }
            if (this.readRecord.CallBack != null)
            {
                requestBodyObject.Add(APIConstants.CALLBACK, this.GetCallBackAsJSONObject(this.readRecord.CallBack));
            }
            if (this.readRecord.FileType != null)
            {
                requestBodyObject[APIConstants.FILETYPE] = this.readRecord.FileType;
            }
            requestBodyObject.Add(APIConstants.QUERY, recordJSON);
            return requestBodyObject;
        }

        private JArray GetFieldsAsJSONArray(List<string> fieldsValue)
        {
            if (fieldsValue.Count == 0)
            {
                return null;
            }
            JArray fields = new JArray();
            foreach (string field in fieldsValue)
            {
                fields.Add(field);
            }
            return fields;
        }

        private JObject GetCriteriaAsJSONObject(ZCRMCriteria criteria)
        {
            JObject recordCriteria = new JObject();
            if (criteria.FieldAPIName != null)
            {
                recordCriteria.Add("api_name", criteria.FieldAPIName);
            }
            if (criteria.Value != null)
            {
                recordCriteria.Add("value", JToken.FromObject(criteria.Value));
            }
            if (criteria.GroupOperator != null)
            {
                recordCriteria.Add("group_operator", criteria.GroupOperator);
            }
            if (criteria.Comparator != null)
            {
                recordCriteria.Add("comparator", criteria.Comparator);
            }
            if (criteria.Group != null && criteria.Group.Count > 0)
            {
                JArray recordData = new JArray();
                foreach (ZCRMCriteria data in criteria.Group)
                {
                    recordData.Add(this.GetCriteriaAsJSONObject(data));
                }
                recordCriteria.Add("group", recordData);
            }
            return recordCriteria;
        }

        private JObject GetCallBackAsJSONObject(ZCRMBulkCallBack callback)
        {
            JObject callbackJSON = new JObject();
            if (callback.Url != null)
            {
                callbackJSON.Add("url", callback.Url);
            }
            if (callback.Method != null)
            {
                callbackJSON.Add("method", callback.Method);
            }
            return callbackJSON;
        }

        private ZCRMBulkCallBack SetZCRMBulkCallBackObject(JObject callbackJSON)
        {
            ZCRMBulkCallBack callback = ZCRMBulkCallBack.GetInstance();
            if (callbackJSON.ContainsKey("url") && callbackJSON["url"].Type != JTokenType.Null)
            {
                callback.Url = (string)callbackJSON["url"];
            }
            if (callbackJSON.ContainsKey("method") && callbackJSON["method"].Type != JTokenType.Null)
            {
                callback.Method = (string)callbackJSON["method"];
            }
            return callback;
        }

        private ZCRMBulkResult SetZCRMResultObject(JObject resultJSON)
        {
            ZCRMBulkResult result = ZCRMBulkResult.GetInstance();
            if (resultJSON.ContainsKey("download_url") && resultJSON["download_url"].Type != JTokenType.Null)
            {
                result.DownloadUrl = (string)resultJSON["download_url"];
            }
            if (resultJSON.ContainsKey("page") && resultJSON["page"].Type != JTokenType.Null)
            {
                result.Page = (int)resultJSON["page"];
            }
            if (resultJSON.ContainsKey("count") && resultJSON["count"].Type != JTokenType.Null)
            {
                result.Count = (int)resultJSON["count"];
            }
            if (resultJSON.ContainsKey("per_page") && resultJSON["per_page"].Type != JTokenType.Null)
            {
                result.PerPage = (int)resultJSON["per_page"];
            }
            if (resultJSON.ContainsKey("more_records") && resultJSON["more_records"].Type != JTokenType.Null)
            {
                result.MoreRecords = (bool)resultJSON["more_records"];
            }
            return result;
        }
    }
}
