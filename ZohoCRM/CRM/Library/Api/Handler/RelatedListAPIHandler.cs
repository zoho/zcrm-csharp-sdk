using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.Users;
using Newtonsoft.Json;
using System.IO;

namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public class RelatedListAPIHandler : CommonAPIHandler, IAPIHandler
    {

        //NOTE: Properties are not used. Instead fields are used since publicly they are not accessed;

        private ZCRMRecord parentRecord;
        private ZCRMModuleRelation relatedList;
        private ZCRMJunctionRecord junctionRecord;

        private RelatedListAPIHandler(ZCRMRecord parentRecord, ZCRMModuleRelation relatedList)
        {
            this.parentRecord = parentRecord;
            this.relatedList = relatedList;
        }

        public static RelatedListAPIHandler GetInstance(ZCRMRecord parentRecord, ZCRMModuleRelation relatedList)
        {
            return new RelatedListAPIHandler(parentRecord, relatedList);
        }

        private RelatedListAPIHandler(ZCRMRecord parentRecord, ZCRMJunctionRecord junctionRecord)
        {
            this.parentRecord = parentRecord;
            this.junctionRecord = junctionRecord;
        }

        public static RelatedListAPIHandler GetInstance(ZCRMRecord parentRecord, ZCRMJunctionRecord junctionRecord)
        {
            return new RelatedListAPIHandler(parentRecord, junctionRecord);
        }

        public BulkAPIResponse<ZCRMRecord> GetRecords(string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName;
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
                if (modifiedSince != null && modifiedSince != "")
                {
                    requestHeaders.Add("If-Modified-Since", modifiedSince);
                }

                BulkAPIResponse<ZCRMRecord> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMRecord>();

                List<ZCRMRecord> recordsList = new List<ZCRMRecord>();
                JObject responseJSON = response.ResponseJSON;
                if (responseJSON.ContainsKey(APIConstants.DATA))
                {
                    JArray recordsArray = (JArray)responseJSON[APIConstants.DATA];
                    foreach (JObject recordDetails in recordsArray)
                    {
                        ZCRMRecord record = ZCRMRecord.GetInstance(relatedList.ApiName, Convert.ToInt64(recordDetails["id"]));
                        EntityAPIHandler.GetInstance(record).SetRecordProperties(recordDetails);
                        recordsList.Add(record);
                    }
                }
                response.BulkData = recordsList;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }


        public BulkAPIResponse<ZCRMNote> GetNotes(string sortByField, CommonUtil.SortOrder? sortOrder, int page, int perPage, string modifiedSince)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName;
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
                if (modifiedSince != null && modifiedSince != "")
                {
                    requestHeaders.Add("If-Modified-Since", modifiedSince);
                }

                BulkAPIResponse<ZCRMNote> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMNote>();

                List<ZCRMNote> allNotes = new List<ZCRMNote>();
                JObject responseJSON = response.ResponseJSON;
                if (responseJSON.ContainsKey(APIConstants.DATA))
                {
                    JArray notesArray = (JArray)responseJSON[APIConstants.DATA];
                    foreach (JObject noteDetails in notesArray)
                    {
                        allNotes.Add(GetZCRMNote(noteDetails, null));
                    }
                }
                response.BulkData = allNotes;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse AddNote(ZCRMNote note)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName;
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                dataArray.Add(GetZCRMNoteAsJSON(note));
                requestBodyObject.Add(APIConstants.DATA, dataArray);
                requestBody = requestBodyObject;


                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject responseData = (JObject)responseDataArray[0];
                JObject responseDetails = (JObject)responseData[APIConstants.DETAILS];
                note = GetZCRMNote(responseDetails, note);
                response.Data = note;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }

        }


        public APIResponse UpdateNote(ZCRMNote note)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.PUT;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName + "/" + note.Id;
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                dataArray.Add(GetZCRMNoteAsJSON(note));
                requestBodyObject.Add(APIConstants.DATA, dataArray);
                requestBody = requestBodyObject;


                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject responseData = (JObject)responseDataArray[0];
                JObject responseDetails = (JObject)responseData[APIConstants.DETAILS];
                note = GetZCRMNote(responseDetails, note);
                response.Data = note;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }


        public APIResponse DeleteNote(ZCRMNote note)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.DELETE;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName + "/" + note.Id;

                return APIRequest.GetInstance(this).GetAPIResponse();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }


        public BulkAPIResponse<ZCRMAttachment> GetAttachments(int page, int perPage, string modifiedSince)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName;
                requestQueryParams.Add(APIConstants.PAGE, page.ToString());
                requestQueryParams.Add(APIConstants.PER_PAGE, perPage.ToString());
                if (modifiedSince != null && modifiedSince != "")
                {
                    requestHeaders.Add("If-Modified-Since", modifiedSince);
                }

                BulkAPIResponse<ZCRMAttachment> response = APIRequest.GetInstance(this).GetBulkAPIResponse<ZCRMAttachment>();

                List<ZCRMAttachment> allAttachments = new List<ZCRMAttachment>();
                JObject responseJSON = response.ResponseJSON;
                if (responseJSON.ContainsKey(APIConstants.DATA))
                {
                    JArray attachmentsArray = (JArray)responseJSON[APIConstants.DATA];
                    foreach (JObject attachmentDetails in attachmentsArray)
                    {
                        allAttachments.Add(GetZCRMAttachment(attachmentDetails));
                    }
                }
                response.BulkData = allAttachments;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse UploadAttachment(string filePath)
        {
            try
            {
                CommonUtil.ValidateFile(filePath);
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName;

                FileInfo fileInfo = new FileInfo(filePath);
                APIResponse response = APIRequest.GetInstance(this).UploadFile(fileInfo.OpenRead(), fileInfo.Name);

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject responseData = (JObject)responseDataArray[0];
                JObject responseDetails = (JObject)responseData[APIConstants.DETAILS];
                response.Data = GetZCRMAttachment(responseDetails);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse UploadLinkAsAttachment(string attachmentUrl)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName;
                requestQueryParams.Add("attachmentUrl", attachmentUrl);

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject responseData = (JObject)responseDataArray[0];
                JObject responseDetails = (JObject)responseData[APIConstants.DETAILS];

                response.Data = GetZCRMAttachment(responseDetails);
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public FileAPIResponse DownloadAttachment(long attachmentId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName + "/" + attachmentId;

                return APIRequest.GetInstance(this).DownloadFile();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }


        public APIResponse DeleteAttachment(long attachmentId)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.DELETE;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + relatedList.ApiName + "/" + attachmentId;
                return APIRequest.GetInstance(this).GetAPIResponse();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        private ZCRMNote GetZCRMNote(JObject noteDetails, ZCRMNote note)
        {
            if (note == null)
            {
                note = ZCRMNote.GetInstance(parentRecord, Convert.ToInt64(noteDetails["id"]));
            }
            note.Id = Convert.ToInt64(noteDetails["id"]);
            if (noteDetails["Note_Title"] != null && noteDetails["Note_Title"].Type != JTokenType.Null)
            {
                note.Title = (string)noteDetails["Note_Title"];
            }
            if (noteDetails["Note_Content"] != null && noteDetails["Note_Content"].Type != JTokenType.Null)
            {
                note.Content = (string)noteDetails["Note_Content"];
            }
            JObject createdByObject = (JObject)noteDetails["Created_By"];
            ZCRMUser createdBy = ZCRMUser.GetInstance(Convert.ToInt64(createdByObject["id"]), (string)createdByObject["name"]);
            note.CreatedBy = createdBy;
            note.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(noteDetails["Created_Time"]));

            JObject modifiedByObject = (JObject)noteDetails["Modified_By"];
            ZCRMUser modifiedBy = ZCRMUser.GetInstance(Convert.ToInt64(modifiedByObject["id"]), (string)modifiedByObject["name"]);
            note.ModifiedBy = modifiedBy;
            note.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(noteDetails["Modified_Time"]));

            if (noteDetails["Owner"] != null && noteDetails["Owner"].Type != JTokenType.Null)
            {
                JObject ownerObject = (JObject)noteDetails["Owner"];
                ZCRMUser owner = ZCRMUser.GetInstance(Convert.ToInt64(ownerObject["id"]), (string)ownerObject["name"]);
                note.NotesOwner = owner;
            }
            else
            {
                note.NotesOwner = createdBy;
            }
            if (noteDetails["$attachments"] != null && noteDetails["$attachments"].Type != JTokenType.Null)
            {
                JArray attachmentsArray = (JArray)noteDetails["$attachments"];
                foreach (JObject attachmentDetails in attachmentsArray)
                {
                    note.AddAttachment(GetZCRMAttachment(attachmentDetails));
                }
            }
            return note;
        }

        private JObject GetZCRMNoteAsJSON(ZCRMNote note)
        {
            JObject noteJSON = new JObject();
            if (note.Title != null)
            {
                noteJSON.Add("Note_Title", note.Title);
            }
            else
            {
                noteJSON.Add("Note_Title", null);
            }
            if (note.Content != null)
            {
                noteJSON.Add("Note_Content", note.Content);
            }
            return noteJSON;
        }

        private ZCRMAttachment GetZCRMAttachment(JObject attachmentDetails)
        {
            ZCRMAttachment attachment = ZCRMAttachment.GetInstance(parentRecord, Convert.ToInt64(attachmentDetails["id"]));
            string fileName = (string)attachmentDetails["File_Name"];
            if (fileName != null)
            {
                attachment.FileName = fileName;
                attachment.FileType = fileName.Substring(fileName.LastIndexOf('.') + 1);
            }
            if (attachmentDetails.ContainsKey("Size"))
            {
                attachment.Size = Convert.ToInt64(attachmentDetails["Size"]);
            }
            if (attachmentDetails.ContainsKey("Created_By") && attachmentDetails["Created_By"].Type != JTokenType.Null)
            {
                JObject createdByObject = (JObject)attachmentDetails["Created_By"];
                ZCRMUser createdBy = ZCRMUser.GetInstance(Convert.ToInt64(createdByObject["id"]), (string)createdByObject["name"]);
                attachment.CreatedBy = createdBy;
                attachment.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(attachmentDetails["Created_Time"]));

                if (attachmentDetails["Owner"] != null && attachmentDetails["Owner"].Type != JTokenType.Null)
                {
                    JObject ownerObject = (JObject)attachmentDetails["Owner"];
                    ZCRMUser owner = ZCRMUser.GetInstance(Convert.ToInt64(ownerObject["id"]), (string)ownerObject["name"]);
                    attachment.Owner = owner;
                }
                else
                {
                    attachment.Owner = createdBy;
                }
            }
            if (attachmentDetails.ContainsKey("Modified_By") && attachmentDetails["Modified_By"].Type != JTokenType.Null)
            {
                JObject modifiedByObject = (JObject)attachmentDetails["Modified_By"];
                ZCRMUser modifiedBy = ZCRMUser.GetInstance(Convert.ToInt64(modifiedByObject["id"]), (string)modifiedByObject["name"]);
                attachment.ModifiedBy = modifiedBy;
                attachment.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(attachmentDetails["Modified_Time"]));
            }

            if (attachmentDetails.ContainsKey("$editable"))
            {
                if (attachmentDetails["$editable"] != null)
                {
                    attachment.Editable = Convert.ToBoolean(attachmentDetails["$editable"]);
                }
            }
            if (attachmentDetails.ContainsKey("$file_id"))
            {
                if (attachmentDetails["$file_id"] != null)
                {
                    attachment.FieldId = Convert.ToString(attachmentDetails["$file_id"]);
                }
            }
            if (attachmentDetails.ContainsKey("$type"))
            {
                if (attachmentDetails["$type"] != null)
                {
                    attachment.Type = Convert.ToString(attachmentDetails["$type"]);
                }
            }
            if (attachmentDetails.ContainsKey("$se_module"))
            {
                if (attachmentDetails["$se_module"] != null)
                {
                    attachment.Se_module = Convert.ToString(attachmentDetails["$se_module"]);
                }
            }
            if (attachmentDetails.ContainsKey("$link_url"))
            {
                if (attachmentDetails["$link_url"] != null)
                {
                    attachment.Link_url = Convert.ToString(attachmentDetails["$link_url"]);
                }
            }
            return attachment;
        }

        public APIResponse AddRelation()
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.PUT;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + junctionRecord.ApiName + "/" + junctionRecord.Id;
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                dataArray.Add(GetRelationDetailsAsJSON(junctionRecord.RelatedDetails));
                requestBodyObject.Add(APIConstants.DATA, dataArray);
                requestBody = requestBodyObject;

                return APIRequest.GetInstance(this).GetAPIResponse();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        private JObject GetRelationDetailsAsJSON(Dictionary<string, object> relatedDetails)
        {
            JObject relatedDetailsJSON = new JObject();
            foreach (KeyValuePair<string, object> keyValuePairs in relatedDetails)
            {
                //TODO: Didn't check null value;
                object value = keyValuePairs.Value;
                if (value == null)
                {
                    relatedDetailsJSON.Add(keyValuePairs.Key, JToken.Parse(null));
                }
                else
                {
                    relatedDetailsJSON.Add(keyValuePairs.Key, JToken.FromObject(value));
                }

            }
            return relatedDetailsJSON;
        }

        public APIResponse DeleteRelation()
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.DELETE;
                urlPath = parentRecord.ModuleAPIName + "/" + parentRecord.EntityId + "/" + junctionRecord.ApiName + "/" + junctionRecord.Id;

                return APIRequest.GetInstance(this).GetAPIResponse();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }
    }
}
