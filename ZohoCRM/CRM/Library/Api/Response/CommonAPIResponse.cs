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

        public APIConstants.ResponseCode? HttpStatusCode
        {
            get
            {
                return httpStatusCode;
            }
            private set
            {
                httpStatusCode = value;
            }
        }

        public JObject ResponseJSON
        {
            get
            {
                return responseJSON;
            }
            set
            {
                responseJSON = value;
            }
        }

        public CommonAPIResponse() { }

        public CommonAPIResponse(int statusCode, string responseString, WebHeaderCollection headers)
        {
            SetResponseHeaders(headers);
            Init(statusCode, responseString);
            ProcessResponse();
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
            if ((APIConstants.ResponseCode.NO_CONTENT == HttpStatusCode) || (APIConstants.ResponseCode.NOT_MODIFIED == HttpStatusCode))
            {
                ResponseJSON = new JObject();
            }
            else
            {
                this.responseString = responseString;
                ResponseJSON = new JObject();
                if (!string.IsNullOrEmpty(responseString) && !string.IsNullOrWhiteSpace(responseString))
                {
                    ResponseJSON = JObject.Parse(responseString);
                }
            }
        }

        protected virtual void HandleFaultyResponse() { }

        protected virtual void ProcessDataResponse() { }




        public override string ToString()
        {
            return APIConstants.STATUS_CODE + "=" + HttpStatusCode + "," + APIConstants.RESPONSE_JSON + "=" + responseString + "," + APIConstants.RESPONSE_HEADERS + "=" + GetResponseHeaders();
        }



        //TODO: Inspect the usage of static modifier in the below class;
        public class ResponseHeaders
        {
            private int allowedAPICallsPerMinute;
            private int remainingCountForThisWindow;
            private long remainingTimeForThisWindowReset;
            private string contentDisposition;
            private string contentType;

            public ResponseHeaders(WebHeaderCollection responseHeaders)
            {
                WebHeaderCollection collection = responseHeaders;
                string header = collection[APIConstants.ALLOWED_API_CALLS_PER_MINUTE];
                if (!string.IsNullOrEmpty(header))
                {
                    AllowedAPICallsPerMinute = Convert.ToInt32(header);
                    RemainingAPICountForThisWindow = Convert.ToInt32(collection[APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW]);
                    RemainingTimeForThisWindowReset = Convert.ToInt64(collection[APIConstants.REMAINING_TIME_FOR_WINDOW__RESET]);
                }
                ContentDisposition = collection["Content-Disposition"];
                ContentType = collection["Content-Type"];
            }

            public int AllowedAPICallsPerMinute
            {
                get
                {
                    return this.allowedAPICallsPerMinute;
                }
                private set
                {
                    this.allowedAPICallsPerMinute = value;
                }
            }

            public int RemainingAPICountForThisWindow
            {
                get
                {
                    return this.remainingCountForThisWindow;
                }
                private set
                {
                    this.remainingCountForThisWindow = value;
                }
            }

            public long RemainingTimeForThisWindowReset
            {
                get
                {
                    return this.remainingTimeForThisWindowReset;
                }
                private set
                {
                    this.remainingTimeForThisWindowReset = value;
                }
            }

            public string ContentDisposition
            {
                get
                {
                    return this.contentDisposition;
                }
                private set
                {
                    this.contentDisposition = value;
                }
            }

            public string ContentType
            {
                get
                {
                    return this.contentType;
                }
                private set
                {
                    this.contentType = value;
                }
            }

            public override string ToString()
            {
                return APIConstants.ALLOWED_API_CALLS_PER_MINUTE + "=" + AllowedAPICallsPerMinute + ";" + APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW + "=" + RemainingAPICountForThisWindow + ";" + APIConstants.REMAINING_TIME_FOR_WINDOW__RESET + "=" + RemainingTimeForThisWindowReset + ";ContentDisposition=" + ContentDisposition;
            }
        }
    }
}
