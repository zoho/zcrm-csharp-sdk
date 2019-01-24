using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Api;

namespace ZCRMSDK.OAuth.Common
{
    //TODO: Add Logger to the File;
    class ZohoHTTPConnector
    {
        private string url;
        private Dictionary<string, string> requestParams = new Dictionary<string, string>();
        private Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

        public string Url { get => url; set => url = value; }
        public Dictionary<string, string> RequestHeaders { get => requestHeaders; set => requestHeaders = value; }

        public void AddParam(string key, string value)
        {
            requestParams.Add(key, value);
        }

        public void AddHeader(string key, string value)
        {
            RequestHeaders.Add(key, value);
        }

        //TODO: Change the access-modifier to internal;
        public string Post()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                string postData = null;
                if (requestParams.Count != 0)
                {
                    foreach (KeyValuePair<string, string> param in requestParams)
                    {
                        if (postData == null)
                        {
                            postData = param.Key + "=" + param.Value;
                        }
                        else
                        {
                            postData += "&" + param.Key + "=" + param.Value;
                        }
                    }
                }
                request.UserAgent = "Mozilla/5.0";
                var data = Encoding.ASCII.GetBytes(postData);

                if (RequestHeaders.Count != 0)
                {
                    foreach (KeyValuePair<string, string> header in RequestHeaders)
                    {
                        if (header.Value != null && string.IsNullOrEmpty(header.Value))
                            request.Headers[header.Key] = header.Value;
                    }
                }

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Method = "POST";


                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(data, 0, data.Length);
                }
                ZCRMLogger.LogInfo("POST - " + APIConstants.URL + " = " + url + ", " + APIConstants.HEADERS + " = " + CommonUtil.DictToString(RequestHeaders) + ", " + APIConstants.PARAMS + " = " + CommonUtil.DictToString(requestParams));
                var response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                ZCRMLogger.LogInfo(APIConstants.STATUS_CODE + " = " + response.StatusCode + ", " + APIConstants.RESPONSE_JSON + " = " + responseString);
                return responseString;
            }catch(WebException e)
            {
                ZCRMLogger.LogError(e);
                throw new ZohoOAuthException(e);
            }
        }

        //TODO: Throw Exceptions
        public string Get()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.UserAgent = "Mozilla/5.0";

                if (RequestHeaders != null && RequestHeaders.Count != 0)
                {
                    foreach (KeyValuePair<string, string> header in RequestHeaders)
                    {
                        request.Headers[header.Key] = header.Value;
                    }
                }

                ZCRMLogger.LogInfo("GET - " + APIConstants.URL + " = " + url + ", " + APIConstants.HEADERS + " = " + CommonUtil.DictToString(RequestHeaders) + ", " + APIConstants.PARAMS + " = " + CommonUtil.DictToString(requestParams));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string responseString = "";
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
                ZCRMLogger.LogInfo(APIConstants.STATUS_CODE + " = " + response.StatusCode + ", " + APIConstants.RESPONSE_JSON + " = " + responseString);
                return responseString;
            }catch(WebException e)
            {
                ZCRMLogger.LogError(e);
                throw new ZohoOAuthException(e);
            }
        }

    }
}
