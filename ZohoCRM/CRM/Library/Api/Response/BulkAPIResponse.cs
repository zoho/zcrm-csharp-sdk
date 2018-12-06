using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;


namespace ZCRMSDK.CRM.Library.Api.Response
{
    
    public class BulkAPIResponse<T> : CommonAPIResponse where T : ZCRMEntity
    {
        
        private List<T> bulkData;
        private ResponseInfo info;
        private List<EntityResponse> bulkEntitiesResponse;

        public BulkAPIResponse() { }

        public BulkAPIResponse(HttpWebResponse response) : base(response)
        {
            SetInfo();
        }



        public List<T> BulkData { get => bulkData; set => bulkData = value; }
       
        public ResponseInfo Info { get => info; private set => info = value; }
     
        public List<EntityResponse> BulkEntitiesResponse { get => bulkEntitiesResponse; private set => bulkEntitiesResponse = value; }

        private void SetInfo()
        {
            if (ResponseJSON.ContainsKey(APIConstants.INFO))
            {
                Info = new ResponseInfo((JObject)ResponseJSON[APIConstants.INFO]);
            }
        }

        protected override void ProcessDataResponse()
        {
            BulkEntitiesResponse = new List<EntityResponse>();
            JArray recordsArray = new JArray();
            if (ResponseJSON.ContainsKey(APIConstants.DATA))
            {
                recordsArray = (JArray)ResponseJSON[APIConstants.DATA];
                foreach(JObject recordJSON in recordsArray)
                {
                    if(recordJSON.ContainsKey(APIConstants.STATUS))
                    {
                        EntityResponse individualResponse = new EntityResponse(recordJSON);
                        BulkEntitiesResponse.Add(individualResponse);
                    }
                }
            }
            else if(ResponseJSON.ContainsKey(APIConstants.TAGS))
            {
                recordsArray = (JArray)ResponseJSON[APIConstants.TAGS];
                foreach(JObject recordJSON in recordsArray)
                {
                    if(recordJSON.ContainsKey(APIConstants.STATUS))
                    {
                        EntityResponse individualResponse = new EntityResponse(recordJSON);
                        BulkEntitiesResponse.Add(individualResponse);
                    }
                }
            }
        }


        protected override void HandleFaultyResponse()
        {
            if((HttpStatusCode == APIConstants.ResponseCode.NO_CONTENT) || (HttpStatusCode == APIConstants.ResponseCode.NOT_MODIFIED))
            {
                ResponseJSON = new JObject();
                BulkData = new List<T>();
            }
            else
            {
                ZCRMLogger.LogError(ResponseJSON[APIConstants.CODE]+" "+ResponseJSON[APIConstants.MESSAGE]);
                throw new ZCRMException((string)ResponseJSON[APIConstants.CODE], (string)ResponseJSON[APIConstants.MESSAGE]);     
            }
        }

        //TODO: Inspect the usage of this class and learn about static classes and about access-modifiers;
        public class ResponseInfo
        {
            private bool moreRecords;
            private int recordCount;
            private int pageNo;
            private int perPage;
            private int allowedCount;

            internal ResponseInfo(JObject info)
            {

                if(info.ContainsKey(APIConstants.MORE_RECORDS))
                {
                    MoreRecords = (bool)info[APIConstants.MORE_RECORDS];
                }
                if (info.ContainsKey(APIConstants.COUNT))
                {
                    RecordCount = Convert.ToInt32(info[APIConstants.COUNT]);
                }
                if (info.ContainsKey(APIConstants.PAGE))
                {
                    PageNo = Convert.ToInt32(info[APIConstants.PAGE]);
                }
                if (info.ContainsKey(APIConstants.PER_PAGE))
                {
                    PerPage = Convert.ToInt32(info[APIConstants.PER_PAGE]);
                }
                if (info.ContainsKey(APIConstants.ALLOWED_COUNT))
                {
                    AllowedCount = Convert.ToInt32(info[APIConstants.ALLOWED_COUNT]);
                }

            }

            public bool MoreRecords { get => moreRecords; private set => moreRecords = value; }
            public int RecordCount { get => recordCount; private set => recordCount = value; }
            public int PageNo { get => pageNo; private set => pageNo = value; }
            public int PerPage { get => perPage; private set => perPage = value; }
            public int AllowedCount { get => allowedCount; private set => allowedCount = value; }
        }
    }

    public class EntityResponse
    {
        private JObject responseJSON;
        private ZCRMEntity data;
        private string status;
        private string message;
        private string code;
        private JObject errorDetails;
        private Dictionary<string, string> upsertedDetails = new Dictionary<string, string>();

        public EntityResponse(JObject entityResponseJSON)
        {
            ResponseJSON = entityResponseJSON;
            Status = (string)entityResponseJSON[APIConstants.STATUS];
            Code = (string)entityResponseJSON[APIConstants.CODE];
            Message = (string)entityResponseJSON[APIConstants.MESSAGE];
            if ((ResponseJSON.ContainsKey(APIConstants.DETAILS)) && (Status.Equals(APIConstants.CODE_ERROR)))
            {
                ErrorDetails = (JObject)ResponseJSON[APIConstants.DETAILS];
            }
            if (entityResponseJSON.ContainsKey(APIConstants.ACTION))
            {
                upsertedDetails.Add(APIConstants.ACTION, (string)entityResponseJSON[APIConstants.ACTION]);
            }
            if (entityResponseJSON.ContainsKey(APIConstants.DUPLICATE_FIELD))
            {
                upsertedDetails.Add(APIConstants.DUPLICATE_FIELD, (string)entityResponseJSON[APIConstants.DUPLICATE_FIELD]);
            }
        }

        public JObject ResponseJSON { get => responseJSON; private set => responseJSON = value; }
        public ZCRMEntity Data { get => data; set => data = value; }
        public string Status { get => status; private set => status = value; }
        public string Message { get => message; private set => message = value; }
        public string Code { get => code; private set => code = value; }
        public JObject ErrorDetails { get => errorDetails; private set => errorDetails = value; }
    }
        
}
