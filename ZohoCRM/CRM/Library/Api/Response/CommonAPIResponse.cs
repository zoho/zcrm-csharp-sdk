using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using ZCRMSDK.CRM.Library.CRMException;

namespace ZCRMSDK.CRM.Library.Api.Response
{
    public class CommonAPIResponse
    {

        protected JObject responseJSON;
        protected APIConstants.ResponseCode? httpStatusCode;
        protected ResponseHeaders responseHeaders;
        string responseString;


        //NOTE: Because of naming collision, the properties have been changed to properties;
        public ResponseHeaders GetResponseHeaders() 
        {
            return responseHeaders; 
        }

        protected void SetResponseHeaders(WebHeaderCollection headers)
        {
            responseHeaders = new ResponseHeaders(headers);
        }

        public APIConstants.ResponseCode? HttpStatusCode { get => httpStatusCode; private set => httpStatusCode = value; }

        public JObject ResponseJSON { get => responseJSON; set => responseJSON = value; }

        public CommonAPIResponse() { }

        public CommonAPIResponse(int statusCode, string responseString, WebHeaderCollection headers)
        {
            Init(statusCode, responseString);
            ProcessResponse();
            SetResponseHeaders(headers);
            ZCRMLogger.LogInfo(ToString());
        }

        protected void Init(int statusCode, string responseString)
        {
            HttpStatusCode = APIConstants.GetEnum(statusCode);
            SetResponseJSON(responseString);
        }


        protected void ProcessResponse()
        {
            if (APIConstants.FaultyResponseCodes.Contains(HttpStatusCode))
            {
                HandleFaultyResponse();
            }
            else if ((HttpStatusCode == APIConstants.ResponseCode.ACCEPTED) || (HttpStatusCode == APIConstants.ResponseCode.OK) ||
                    (HttpStatusCode == APIConstants.ResponseCode.CREATED))
            {
                ProcessDataResponse();
            }
        }

        protected virtual void SetResponseJSON(string responseString)
        {
            if((APIConstants.ResponseCode.NO_CONTENT == HttpStatusCode) || (APIConstants.ResponseCode.NOT_MODIFIED == HttpStatusCode))
            {
                ResponseJSON = new JObject();
            }

            else
            {
                this.responseString = responseString;
                ResponseJSON = JObject.Parse(responseString);    
            }
        }

        protected virtual void HandleFaultyResponse() { }

        protected virtual void ProcessDataResponse() { }




        public override string ToString()
        {
            return APIConstants.STATUS_CODE + "=" + HttpStatusCode + "," + APIConstants.RESPONSE_JSON + "=" + responseString + "," + APIConstants.RESPONSE_HEADERS + "=" +  GetResponseHeaders();
        }



        //TODO: Inspect the usage of static modifier in the below class;
        public class ResponseHeaders
        {
            private int allowedAPICallsPerMinute;
            private int remainingCountForThisWindow;
            private long remainingTimeForThisWindowReset;
            private string contentDisposition;

            public ResponseHeaders(WebHeaderCollection responseHeaders)
            {
                WebHeaderCollection collection = responseHeaders;
                string header = collection[APIConstants.ALLOWED_API_CALLS_PER_MINUTE];
                if(header != null && header != "")
                {
                    AllowedAPICallsPerMinute = Convert.ToInt32(header);
                    RemainingAPICountForThisWindow = Convert.ToInt32(collection[APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW]);
                    RemainingTimeForThisWindowReset = Convert.ToInt64(collection[APIConstants.REMAINING_TIME_FOR_WINDOW__RESET]);
                }
                ContentDisposition = collection["Content-Disposition"];
            }

            public int AllowedAPICallsPerMinute { get => allowedAPICallsPerMinute; private set => allowedAPICallsPerMinute = value; }
            public int RemainingAPICountForThisWindow { get => remainingCountForThisWindow; private set => remainingCountForThisWindow = value; }
            public long RemainingTimeForThisWindowReset { get => remainingTimeForThisWindowReset; private set => remainingTimeForThisWindowReset = value; }
            public string ContentDisposition { get => contentDisposition; private set => contentDisposition = value; }

            public override string ToString()
            {
                return APIConstants.ALLOWED_API_CALLS_PER_MINUTE + "=" + AllowedAPICallsPerMinute + ";" + APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW + "=" + RemainingAPICountForThisWindow + ";" + APIConstants.REMAINING_TIME_FOR_WINDOW__RESET + "=" + RemainingTimeForThisWindowReset + ";ContentDisposition=" + ContentDisposition;
            }
        }
    }
}
