using System.Net;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;


namespace ZCRMSDK.CRM.Library.Api.Response
{
    public class APIResponse : CommonAPIResponse
    {

        private ZCRMEntity data;
        private string status;
        private string message;


        public APIResponse() { }

        public APIResponse(int statusCode, string responseString, WebHeaderCollection headers) : base(statusCode, responseString, headers) { }

        public ZCRMEntity Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        public string Message
        {
            get
            {
                return message;
            }
            private set
            {
                message = value;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            internal set
            {
                status = value;
            }
        }


        protected override void ProcessDataResponse()
        {
            JObject msgJSON = ResponseJSON;
            if(msgJSON.ContainsKey(APIConstants.DATA))
            {
                JArray recordsArray = (JArray)ResponseJSON.GetValue(APIConstants.DATA);
                msgJSON = (JObject)recordsArray[0];
            }
            if (msgJSON.ContainsKey(APIConstants.TAGS))
            {
                JArray tagsArray = (JArray)ResponseJSON.GetValue(APIConstants.TAGS);
                msgJSON = (JObject)tagsArray[0];
            }
            if (msgJSON.ContainsKey(APIConstants.USERS))
            {
                JArray usersArray = (JArray)ResponseJSON.GetValue(APIConstants.USERS);
                msgJSON = (JObject)usersArray[0];
            }
            if (msgJSON.ContainsKey(APIConstants.MESSAGE))
            {
                Message = msgJSON.GetValue(APIConstants.MESSAGE).ToString();
            }

            if(msgJSON.ContainsKey(APIConstants.STATUS))
            {
                Status = msgJSON.GetValue(APIConstants.STATUS).ToString();
                if(Status.Equals(APIConstants.CODE_ERROR))
                {
                    if(msgJSON.ContainsKey(APIConstants.DETAILS))
                    {
                        //TODO: Inspect the working of this part;
                        throw new ZCRMException(true, (int)HttpStatusCode.Value, msgJSON.GetValue(APIConstants.CODE).ToString(), Message, msgJSON.GetValue(APIConstants.DETAILS) as JObject);
                    }
                    throw new ZCRMException(true, (int)HttpStatusCode.Value, msgJSON.GetValue(APIConstants.CODE).ToString(), Message);
                }
            }
            //NOTE: For the user photo download method.
            else if(msgJSON.ContainsKey("status_code"))
            {
                Status = msgJSON["status_code"].ToString();
                if (msgJSON.ContainsKey("errors"))
                {
                    JArray errorDetails = (JArray)msgJSON["errors"];
                    throw new ZCRMException(true, (int)HttpStatusCode.Value, HttpStatusCode.Value.ToString(), "Status_code :" + (string)msgJSON["status_code"] + ", Error details: " + errorDetails[0]["code"] + ", Resource :" + errorDetails[0]["resource"]);
                }
                throw new ZCRMException(true, (int)HttpStatusCode.Value, HttpStatusCode.Value.ToString(), "Status_code :" + (string)msgJSON["status_code"] + msgJSON.ToString());
            }
            //NOTE : while uploading the file if the org id is wrong the below else-if block will handle it.
            else if(msgJSON.ContainsKey("x-error"))
            {
                ZCRMLogger.LogInfo(ToString());
                JToken infoMessageToken = msgJSON.GetValue("info");
                string infoMessage = null;
                if(infoMessageToken != null && infoMessageToken.Type != JTokenType.Null)
                {
                    infoMessage = infoMessageToken.ToString();
                }
                throw new ZCRMException(true, (int)HttpStatusCode.Value, HttpStatusCode.Value.ToString(), infoMessage , msgJSON);
            }
        }

        protected override void HandleFaultyResponse()
        {
            if ((HttpStatusCode == APIConstants.ResponseCode.NO_CONTENT) || (HttpStatusCode == APIConstants.ResponseCode.NOT_MODIFIED))
            {
                throw new ZCRMException(true, (int)HttpStatusCode.Value, HttpStatusCode.Value.ToString());
            }
            ZCRMLogger.LogError(ResponseJSON[APIConstants.CODE] + " " + ResponseJSON[APIConstants.MESSAGE]);
            throw new ZCRMException(true, (int)HttpStatusCode.Value, ResponseJSON.GetValue(APIConstants.CODE).ToString(), ResponseJSON.GetValue(APIConstants.MESSAGE).ToString(), ResponseJSON);
        }
    }
}
