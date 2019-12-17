using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.Users;
using Newtonsoft.Json;

namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public class MassEntityAPIHandler : CommonAPIHandler, IAPIHandler
    {
        private ZCRMModule module;
        private ZCRMTrashRecord trashRecord = null;


        private MassEntityAPIHandler(ZCRMModule zcrmModule)
        {
            module = zcrmModule;
        }

        public static MassEntityAPIHandler GetInstance(ZCRMModule zcrmModule)
        {
            return new MassEntityAPIHandler(zcrmModule);
        }


        public BulkAPIResponse<ZCRMRecord> CreateRecords(List<ZCRMRecord> records, List<string> trigger, string lar_id)
        {
            if (records.Count > 100)
            {
                throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
            }
            requestMethod = APIConstants.RequestMethod.POST;
            urlPath = module.ApiName;
            JObject requestBodyObject = new JObject();
            JArray dataArray = new JArray();
            foreach (ZCRMRecord record in records)
            {
                if (record.EntityId == null)
                {
                    dataArray.Add(EntityAPIHandler.GetInstance(record).GetZCRMRecordAsJSON());
                }
                else
                {
                    throw new ZCRMException("Entity ID Must be null/empty for CreateRecords operation.");
                }
            }
            requestBodyObject.Add(APIConstants.DATA, dataArray);
            if (trigger != null && trigger.Count > 0)
            {
                requestBodyObject.Add("trigger", JArray.FromObject(trigger));
            }
            if (lar_id != null)
            {
                requestBodyObject.Add("lar_id" , lar_id);
            }
            requestBody = requestBodyObject;

            BulkAPIResponse<ZCRMRecord> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMRecord>();

            List<ZCRMRecord> createdRecords = new List<ZCRMRecord>();
            List<EntityResponse> responses = response.BulkEntitiesResponse;
            int responseSize = responses.Count;
            for (int i = 0; i < responseSize; i++)
            {
                EntityResponse entityResponse = responses[i];
                if (entityResponse.Status.Equals(APIConstants.CODE_SUCCESS))
                {
                    JObject responseData = entityResponse.ResponseJSON;
                    JObject recordDetails = (JObject)responseData[APIConstants.DETAILS];
                    ZCRMRecord newRecord = records[i];
                    EntityAPIHandler.GetInstance(newRecord).SetRecordProperties(recordDetails);
                    createdRecords.Add(newRecord);
                    entityResponse.Data = newRecord;
                }
                else
                {
                    entityResponse.Data = null;
                }
            }
            response.BulkData = createdRecords;
            return response;
        }

        public BulkAPIResponse<ZCRMRecord> UpdateRecords(List<ZCRMRecord> records, List<string> trigger)
        {
            if (records.Count > 100)
            {
                throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
            }
            requestMethod = APIConstants.RequestMethod.PUT;
            urlPath = module.ApiName;
            JObject requestBodyObject = new JObject();
            JArray dataArray = new JArray();
            foreach (ZCRMRecord record in records)
            {
                if (record.GetFieldValue("id") == null || (long)record.GetFieldValue("id") <= 0)
                {
                    throw new ZCRMException("Entity ID Must Not be null/empty for UpdateRecords operation.");
                }
                dataArray.Add(EntityAPIHandler.GetInstance(record).GetZCRMRecordAsJSON());
            }
            requestBodyObject.Add(APIConstants.DATA, dataArray);
            if (trigger != null && trigger.Count > 0)
            {
                requestBodyObject.Add("trigger", JArray.FromObject(trigger));
            }
            requestBody = requestBodyObject;

            BulkAPIResponse<ZCRMRecord> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMRecord>();
            List<ZCRMRecord> updatedRecords = new List<ZCRMRecord>();
            List<EntityResponse> responses = response.BulkEntitiesResponse;
            int responseSize = responses.Count;
            for (int i = 0; i < responseSize; i++)
            {
                EntityResponse entityResponse = responses[i];
                if (entityResponse.Status.Equals(APIConstants.CODE_SUCCESS))
                {
                    JObject responseData = entityResponse.ResponseJSON;
                    JObject recordDetails = (JObject)responseData[APIConstants.DETAILS];
                    ZCRMRecord newRecord = records[i];
                    EntityAPIHandler.GetInstance(newRecord).SetRecordProperties(recordDetails);
                    updatedRecords.Add(newRecord);
                    entityResponse.Data = newRecord;
                }
                else
                {
                    entityResponse.Data = null;
                }
            }
            response.BulkData = updatedRecords;
            return response;
        }
        public BulkAPIResponse<ZCRMRecord> MassUpdateRecords(List<long> entityIds, string fieldAPIName, Object value)
        {
            if (entityIds.Count > 100)
            {
                throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
            }
            //NOTE: null value is not converted to JObject of type null;
            requestMethod = APIConstants.RequestMethod.PUT;
            urlPath = module.ApiName;
            JObject requestBodyObject = new JObject();
            JArray dataArray = new JArray();
            foreach (long id in entityIds)
            {
                JObject updateJSON = new JObject
                {
                    { fieldAPIName, value.ToString() },
                    { "id", Convert.ToString(id) }
                };
                dataArray.Add(updateJSON);
            }
            requestBodyObject.Add(APIConstants.DATA, dataArray);
            requestBody = requestBodyObject;

            BulkAPIResponse<ZCRMRecord> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMRecord>();

            List<ZCRMRecord> updatedRecords = new List<ZCRMRecord>();
            List<EntityResponse> responses = response.BulkEntitiesResponse;
            foreach (EntityResponse entityResponse in responses)
            {
                if (entityResponse.Status.Equals(APIConstants.CODE_SUCCESS))
                {
                    JObject responseData = entityResponse.ResponseJSON;
                    JObject recordDetails = (JObject)responseData[APIConstants.DETAILS];
                    ZCRMRecord updatedRecord = ZCRMRecord.GetInstance(module.ApiName, Convert.ToInt64(recordDetails["id"]));
                    EntityAPIHandler.GetInstance(updatedRecord).SetRecordProperties(recordDetails);
                    updatedRecords.Add(updatedRecord);
                    entityResponse.Data = updatedRecord;
                }
                else
                {
                    entityResponse.Data = null;
                }
            }
            response.BulkData = updatedRecords;
            return response;

        }

        public BulkAPIResponse<ZCRMRecord> UpsertRecords(List<ZCRMRecord> records, List<string> trigger, string lar_id, List<string> duplicate_check_fields)
        {
            if (records.Count > 100)
            {
                throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
            }
            requestMethod = APIConstants.RequestMethod.POST;
            urlPath = module.ApiName + "/upsert";
            JObject requestBodyObject = new JObject();
            JArray dataArray = new JArray();
            foreach (ZCRMRecord record in records)
            {
                JObject recordJSON = EntityAPIHandler.GetInstance(record).GetZCRMRecordAsJSON();
                dataArray.Add(recordJSON);
            }
            requestBodyObject.Add(APIConstants.DATA, dataArray);
            if (trigger != null && trigger.Count > 0)
            {
                requestBodyObject.Add("trigger", JArray.FromObject(trigger));
            }
            if (lar_id != null)
            {
                requestBodyObject.Add("lar_id", lar_id);
            }
            if (duplicate_check_fields != null && duplicate_check_fields.Count > 0)
            {
                requestBodyObject.Add("duplicate_check_fields", JArray.FromObject(duplicate_check_fields));
            }
            requestBody = requestBodyObject;

            BulkAPIResponse<ZCRMRecord> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMRecord>();

            List<ZCRMRecord> upsertedRecords = new List<ZCRMRecord>();
            List<EntityResponse> responses = response.BulkEntitiesResponse;
            int responseSize = responses.Count;
            for (int i = 0; i < responseSize; i++)
            {
                EntityResponse entityResponse = responses[i];
                if (entityResponse.Status.Equals(APIConstants.CODE_SUCCESS))
                {
                    JObject responseData = entityResponse.ResponseJSON;
                    JObject recordDetails = (JObject)responseData[APIConstants.DETAILS];
                    ZCRMRecord record = records[i];
                    EntityAPIHandler.GetInstance(record).SetRecordProperties(recordDetails);
                    upsertedRecords.Add(record);
                    entityResponse.Data = record;
                }
                else
                {
                    entityResponse.Data = null;
                }
            }
            response.BulkData = upsertedRecords;
            return response;
        }

        public BulkAPIResponse<ZCRMRecord> GetRecords(long? cvId, string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince, string isConverted, string isApproved, List<string> fields)
        {
            requestMethod = APIConstants.RequestMethod.GET;
            urlPath = module.ApiName;
            if (cvId != null)
            {
                requestQueryParams.Add("cvid", cvId.ToString());
            }
            if (sortByField != null)
            {
                requestQueryParams.Add("sort_by", sortByField);
            }
            if (sortOrder != null)
            {
                requestQueryParams.Add("sort_order", sortOrder.ToString());
            }
            requestQueryParams.Add(APIConstants.PAGE, page.ToString());
            requestQueryParams.Add(APIConstants.PER_PAGE, perPage.ToString());
            if (isApproved != null && isApproved != "")
            {
                requestQueryParams.Add("approved", isApproved);
            }
            if (isConverted != null && isConverted != "")
            {
                requestQueryParams.Add("converted", isConverted);
            }
            if (fields != null)
            {
                requestQueryParams.Add("fields", CommonUtil.CollectionToCommaDelimitedString(fields));
            }
            if (modifiedSince != null && modifiedSince != "")
            {
                requestHeaders.Add("If-Modified-Since", modifiedSince);
            }

            BulkAPIResponse<ZCRMRecord> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMRecord>();

            List<ZCRMRecord> records = new List<ZCRMRecord>();
            JObject responseJSON = response.ResponseJSON;
            if (responseJSON.ContainsKey(APIConstants.DATA))
            {
                JArray recordsArray = (JArray)responseJSON[APIConstants.DATA];
                foreach (JObject recordDetails in recordsArray)
                {
                    ZCRMRecord record = ZCRMRecord.GetInstance(module.ApiName, Convert.ToInt64(recordDetails["id"]));
                    EntityAPIHandler.GetInstance(record).SetRecordProperties(recordDetails);
                    records.Add(record);
                }
            }
            response.BulkData = records;
            return response;
        }

        public BulkAPIResponse<ZCRMEntity> DeleteRecords(List<long> entityIds)
        {
            if (entityIds.Count > 100)
            {
                throw new ZCRMException(APIConstants.API_MAX_RECORDS_MSG);
            }
            requestMethod = APIConstants.RequestMethod.DELETE;
            urlPath = module.ApiName;
            requestQueryParams.Add("ids", CommonUtil.CollectionToCommaDelimitedString(entityIds));

            BulkAPIResponse<ZCRMEntity> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMEntity>();

            List<EntityResponse> responses = response.BulkEntitiesResponse;
            foreach (EntityResponse entityResponse in responses)
            {
                JObject entityResponseJSON = entityResponse.ResponseJSON;
                JObject recordJSON = (JObject)entityResponseJSON[APIConstants.DETAILS];
                ZCRMRecord record = ZCRMRecord.GetInstance(module.ApiName, Convert.ToInt64(recordJSON["id"]));
                entityResponse.Data = record;
            }
            return response;
        }

        public BulkAPIResponse<ZCRMTrashRecord> GetAllDeletedRecords()
        {
            return GetDeletedRecords("all");
        }

        public BulkAPIResponse<ZCRMTrashRecord> GetRecycleBinRecords()
        {
            return GetDeletedRecords("recycle");
        }

        public BulkAPIResponse<ZCRMTrashRecord> GetPermanentlyDeletedRecords()
        {
            return GetDeletedRecords("permanent");
        }

        private BulkAPIResponse<ZCRMTrashRecord> GetDeletedRecords(string type)
        {
            requestMethod = APIConstants.RequestMethod.GET;
            urlPath = module.ApiName + "/deleted";
            requestQueryParams.Add("type", type);

            BulkAPIResponse<ZCRMTrashRecord> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMTrashRecord>();

            List<ZCRMTrashRecord> trashRecordList = new List<ZCRMTrashRecord>();
            JObject responseJSON = response.ResponseJSON;
            if (responseJSON.ContainsKey(APIConstants.DATA))
            {
                JArray trashRecordsArray = (JArray)responseJSON[APIConstants.DATA];
                foreach (JObject trashRecordDetails in trashRecordsArray)
                {
                    trashRecord = ZCRMTrashRecord.GetInstance((string)trashRecordDetails["type"], Convert.ToInt64(trashRecordDetails["id"]));
                    SetTrashRecordProperties(trashRecordDetails);
                    trashRecordList.Add(trashRecord);
                }
            }
            response.BulkData = trashRecordList;
            return response;
        }

        public BulkAPIResponse<ZCRMRecord> SearchByWord(string searchText, Dictionary<string,string> methodParams)
        {
            return SearchRecords("word", searchText, methodParams);
        }

        public BulkAPIResponse<ZCRMRecord> SearchByCriteria(string searchCriteria, Dictionary<string, string> methodParams)
        {
            return SearchRecords("criteria", searchCriteria, methodParams);
        }


        public BulkAPIResponse<ZCRMRecord> SearchByEmail(string searchValue, Dictionary<string, string> methodParams)
        {
            return SearchRecords("email", searchValue, methodParams);
        }


        public BulkAPIResponse<ZCRMRecord> SearchByPhone(string searchValue, Dictionary<string, string> methodParams)
        {
            return SearchRecords("phone", searchValue, methodParams);
        }

        private BulkAPIResponse<ZCRMRecord> SearchRecords(string searchKey, string searchValue, Dictionary<string,string> methodParams)
        {
            requestMethod = APIConstants.RequestMethod.GET;
            urlPath = module.ApiName + "/search";
            requestQueryParams.Add(searchKey, searchValue);
            foreach(KeyValuePair<string,string> methodParam in methodParams)
            {
                requestQueryParams.Add(methodParam.Key, methodParam.Value);
            }
            
            BulkAPIResponse<ZCRMRecord> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMRecord>();

            List<ZCRMRecord> recordsList = new List<ZCRMRecord>();
            JObject responseJSON = response.ResponseJSON;
            if (responseJSON.ContainsKey(APIConstants.DATA))
            {
                JArray recordsArray = (JArray)responseJSON[APIConstants.DATA];
                foreach (JObject recordDetails in recordsArray)
                {
                    ZCRMRecord record = ZCRMRecord.GetInstance(module.ApiName, Convert.ToInt64(recordDetails["id"]));
                    EntityAPIHandler.GetInstance(record).SetRecordProperties(recordDetails);
                    recordsList.Add(record);
                }
            }
            response.BulkData = recordsList;
            return response;
        }

        public void SetTrashRecordProperties(JObject trashRecordDetails)
        {
            foreach (KeyValuePair<string, JToken> trashRecordDetail in trashRecordDetails)
            {
                string fieldAPIName = Convert.ToString(trashRecordDetail.Key);
                if (fieldAPIName.Equals("created_by") && trashRecordDetail.Value.Type != JTokenType.Null)
                {
                    JObject createdByObject = (JObject)trashRecordDetail.Value;
                    ZCRMUser createdUser = ZCRMUser.GetInstance(Convert.ToInt64(createdByObject["id"]), (string)createdByObject["name"]);
                    trashRecord.CreatedBy = createdUser;
                }
                else if (fieldAPIName.Equals("deleted_by") && trashRecordDetail.Value.Type != JTokenType.Null)
                {
                    JObject modifiedByObject = (JObject)trashRecordDetail.Value;
                    ZCRMUser DeletedByUser = ZCRMUser.GetInstance(Convert.ToInt64(modifiedByObject["id"]), (string)modifiedByObject["name"]);
                    trashRecord.DeletedBy = DeletedByUser;
                }
                else if (fieldAPIName.Equals("display_name"))
                {
                    trashRecord.DisplayName = Convert.ToString(trashRecordDetail.Value);
                }
                else if (fieldAPIName.Equals("deleted_time"))
                {
                    trashRecord.DeletedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(trashRecordDetail.Value));
                }
            }
        }
    }
}
