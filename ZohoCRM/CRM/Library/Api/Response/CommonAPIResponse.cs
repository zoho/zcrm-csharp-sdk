using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using ZCRMSDK.CRM.Library.CRMException;

namespace ZCRMSDK.CRM.Library.Api.Response
{
    public class CommonAPIResponse
    {

        protected HttpWebResponse response;
        protected JObject responseJSON;
        protected APIConstants.ResponseCode? httpStatusCode;
        protected ResponseHeaders responseHeaders;
        string responseString;

        protected HttpWebResponse Response
        {
            get
            {
                return response;
            }
            set
            {
                response = value;
            }
        }

        //NOTE: Because of naming collision, the properties have been changed to properties;
        public ResponseHeaders GetResponseHeaders() 
        {
            return responseHeaders; 
        }

        protected void SetResponseHeaders()
        {
            responseHeaders = new ResponseHeaders(Response);
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

        public CommonAPIResponse(HttpWebResponse response)
        {
            try
            {
                Response = response;
                Init();
                ProcessResponse();
                SetResponseHeaders();
                ZCRMLogger.LogInfo(ToString());
            }
            catch (Exception)
            {
                ZCRMLogger.LogInfo(ToString());
            }
        }

        protected void Init()
        {
            HttpStatusCode = APIConstants.GetEnum((int)response.StatusCode);
            SetResponseJSON();
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

        protected virtual void SetResponseJSON()
        {
            if((APIConstants.ResponseCode.NO_CONTENT == HttpStatusCode) || (APIConstants.ResponseCode.NOT_MODIFIED == HttpStatusCode))
            {
                ResponseJSON = new JObject();
            }

            else
            {
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
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

            public ResponseHeaders(HttpWebResponse response)
            {
                WebHeaderCollection collection = response.Headers;
                string header = response.GetResponseHeader(APIConstants.ALLOWED_API_CALLS_PER_MINUTE);
                if(header != null && header != "")
                {
                    AllowedAPICallsPerMinute = Convert.ToInt32(header);
                    RemainingAPICountForThisWindow = Convert.ToInt32(response.GetResponseHeader(APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW));
                    RemainingTimeForThisWindowReset = Convert.ToInt64(response.GetResponseHeader(APIConstants.REMAINING_TIME_FOR_WINDOW__RESET));
                }
            }

            public int AllowedAPICallsPerMinute
            {
                get
                {
                    return allowedAPICallsPerMinute;
                }
                private set
                {
                    allowedAPICallsPerMinute = value;
                }
            }
            public int RemainingAPICountForThisWindow
            {
                get
                {
                    return remainingCountForThisWindow;
                }
                private set
                {
                    remainingCountForThisWindow = value;
                }
            }
            public long RemainingTimeForThisWindowReset
            {
                get
                {
                    return remainingTimeForThisWindowReset;
                }
                private set
                {
                    remainingTimeForThisWindowReset = value;
                }
            }

            public override string ToString()
            {
                return APIConstants.ALLOWED_API_CALLS_PER_MINUTE + "=" + AllowedAPICallsPerMinute + ";" + APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW + "=" + RemainingAPICountForThisWindow + ";" + APIConstants.REMAINING_TIME_FOR_WINDOW__RESET + "=" + RemainingTimeForThisWindowReset + ";";
            }
        }
    }
}
