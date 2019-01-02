using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Api.Handler;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Setup.RestClient;
using ZCRMSDK.CRM.Library.CRMException;
using System.Reflection;

namespace ZCRMSDK.CRM.Library.Api
{
    public class APIRequest
    {
        private string url = ZCRMConfigUtil.GetApiBaseURL() + "/crm/" + ZCRMConfigUtil.GetApiVersion()+"/";
        private APIConstants.RequestMethod requestMethod;
        private Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
        private Dictionary<string, string> requestParams = new Dictionary<string, string>();
        private JObject requestBody;
        private HttpWebResponse response;
        private static string boundary = "----FILEBOUNDARY----";
        private Stream fileRequestBody;
        private Stream requestStream = null;

        private Dictionary<string, string> RequestHeaders { get => requestHeaders; set => requestHeaders = value; }
        private Dictionary<string, string> RequestParams { get => requestParams; set => requestParams = value; }
        private JObject RequestBody { get => requestBody; set => requestBody = value; }


        private APIRequest(IAPIHandler handler)
        {
            url = handler.GetUrlPath().Contains("http") ? handler.GetUrlPath() : url + handler.GetUrlPath();
            requestMethod = handler.GetRequestMethod();
            RequestHeaders = handler.GetRequestHeaders();
            RequestParams = handler.GetRequestQueryParams();
            RequestBody = handler.GetRequestBody();
        }

        public static APIRequest GetInstance(IAPIHandler handler){
            return new APIRequest(handler);
        }

        public void SetHeader(string name, string value)
        {
            RequestHeaders.Add(name, value);    
        }

        public void SetRequestParam(string name, string value)
        {
            RequestParams.Add(name, value);
        }

        private string GetHeader(string name){
            return RequestHeaders[name];
        }

        private string GetRequestParam(string name)
        {
            return RequestParams[name];
        }

        private void SetQueryParams()
        {
            if(RequestParams.Count == 0) { return; }
            url += "?";
            foreach(KeyValuePair<string, string> keyValuePairs in RequestParams)
            {
                if (keyValuePairs.Value != null && keyValuePairs.Value != "")
                {
                    url = url.EndsWith("?", StringComparison.InvariantCulture) ? url + keyValuePairs.Key + "=" + keyValuePairs.Value : url + "&" + keyValuePairs.Key + "=" + keyValuePairs.Value; 
                }
            }
            url = url.EndsWith("?", StringComparison.InvariantCulture) ? url.TrimEnd('?') : url;
        }

        private void SetHeaders(ref HttpWebRequest request)
        {
            foreach(KeyValuePair<string, string> keyValuePairs in RequestHeaders)
            {
                if(keyValuePairs.Value != null && keyValuePairs.Value != "")
                {
                    request.Headers[keyValuePairs.Key] = keyValuePairs.Value;   
                }
            }
         
        }

        private void AuthenticateRequest()
        {
            try
            {
                if (ZCRMConfigUtil.HandleAuthentication)
                {
                    string accessToken = (string)(Type.GetType(ZCRMConfigUtil.GetAuthenticationClass()).GetMethod("GetAccessToken", BindingFlags.Static | BindingFlags.Public).Invoke(null, null));
                    if (accessToken == null)
                    {
                        ZCRMLogger.LogError("Access token is not set");
                        throw new ZCRMException(APIConstants.AUTHENTICATION_FAILURE, "Access token is not set");
                    }
                    SetHeader("Authorization", APIConstants.authHeaderPrefix + accessToken);
                    ZCRMLogger.LogInfo("Token fetched successfully..");
                }
            }
            catch (TargetInvocationException e)
            {
                ZCRMLogger.LogError(e.GetBaseException());
                if (e.GetBaseException() is ZCRMException)
                {
                    throw (ZCRMException)(e.GetBaseException());
                }
                throw new ZCRMException(e.GetBaseException());
            }
            catch(Exception e) when(e is TargetException || e is MethodAccessException)
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
        }

        public APIResponse GetAPIResponse()
        {
            GetResponseFromServer();
            return new APIResponse(response);
        }

        public BulkAPIResponse<T> GetBulkAPIResponse<T>() where T : ZCRMEntity
        {
            try
            {
                GetResponseFromServer();
                return new BulkAPIResponse<T>(response);
            }
            catch (Exception e) when(!(e is ZCRMException))
            {
                if (e is ZCRMException) { throw; }
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
        }

        private void GetResponseFromServer()
        {
            try
            {
                PopulateRequestHeaders(ZCRMRestClient.StaticHeaders);
                if (ZCRMRestClient.DYNAMIC_HEADERS.IsValueCreated)
                {
                    PopulateRequestHeaders(ZCRMRestClient.GetDynamicHeaders());
                }
                else
                { 
                    AuthenticateRequest();
                }
                SetQueryParams();
                HttpWebRequest request = GetHttpWebRequestClient();
                SetHeaders(ref request);
                SetRequestMethod(request);
             
                ZCRMLogger.LogInfo(ToString());
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }catch(WebException e)
                {
                    if (e.Response == null) { throw; }
                    response = (HttpWebResponse)e.Response;
                }
				APIStats.UpdateStats(response);
            }catch(WebException e)
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
        }



        private void SetRequestMethod(HttpWebRequest request)
        {
            switch(requestMethod)
            {
                case APIConstants.RequestMethod.GET:
                    request.Method = APIConstants.RequestMethod.GET.ToString();
                    return;
                case APIConstants.RequestMethod.DELETE:
                    request.Method = APIConstants.RequestMethod.DELETE.ToString();
                    return;
                case APIConstants.RequestMethod.POST:
                    request.Method = APIConstants.RequestMethod.POST.ToString();
                    break;
                case APIConstants.RequestMethod.PUT:
                    request.Method = APIConstants.RequestMethod.PUT.ToString();
                    break;
            }
            PopulateRequestStream(request);
        }

        private void PopulateRequestStream(HttpWebRequest request)
        {
            try
            {
                //If JSON Payload
                if (RequestBody.Type != JTokenType.Null && RequestBody.Count > 0)
                {
                    string dataString = RequestBody.ToString();
                    var data = Encoding.ASCII.GetBytes(dataString);
                    int dataLength = data.Length;
                    request.ContentType = "application/json";
                    request.ContentLength = dataLength;
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(data, 0, dataLength);
                    }
                }
                //If either file Payload or empty request body
                else
                {
                    requestStream = new MemoryStream();
                    String multiPartHeader = "\r\n--"+boundary;
                    byte[] multiPartHeaderBytes = Encoding.UTF8.GetBytes(multiPartHeader);
                    requestStream.Write(multiPartHeaderBytes, 0, multiPartHeaderBytes.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    if (fileRequestBody != null && fileRequestBody.Length > 0)
                    {
                        while ((bytesRead = fileRequestBody.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }
                    }
                    requestStream.Position = 0;

                    request.ContentType = "multipart/form-data; boundary="+boundary;
                    request.ContentLength = requestStream.Length;
                    using (var writer = request.GetRequestStream())
                    {
                        while ((bytesRead = requestStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            writer.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
        }


        private HttpWebRequest GetHttpWebRequestClient()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (ZCRMConfigUtil.ConfigProperties.ContainsKey("timeout"))
            {
                int? timeoutPeriod = Convert.ToInt32(ZCRMConfigUtil.GetConfigValue("timeout"));
                if (timeoutPeriod != null)
                {
                    request.Timeout = (int)timeoutPeriod;
                }

            }
            request.UserAgent = APIConstants.USER_AGENT;
            return request;
        }

        private void PopulateRequestHeaders(Dictionary<string, string> dict)
        {
            foreach(KeyValuePair<string, string> keyValuePair in dict)
            {
                RequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public APIResponse UploadFile(string filePath)
        {
            try
            {
                fileRequestBody = GetFileRequestBodyStream(filePath);
                GetResponseFromServer();
                return new APIResponse(response);
            }
            catch(Exception e) when(!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
        }

        public FileAPIResponse DownloadFile()
        {
            try{
                GetResponseFromServer();
                return new FileAPIResponse(response);
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
        }


        private Stream GetFileRequestBodyStream(string filePath)
        {
            try
            {
                Stream fileDataStream = new MemoryStream();

                FileInfo fileInfo = new FileInfo(filePath);

                //File Content-Disposition header excluding the boundary header;
                string fileHeader = string.Format("\r\nContent-Disposition: form-data; name=\"file\"; filename=\""+fileInfo.Name+"\"\r\nContent-Type: application/octet-stream\r\n\r\n");
                byte[] fileHeaderBytes = Encoding.UTF8.GetBytes(fileHeader);
                fileDataStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);

                //File content 
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                using (FileStream fileStream = fileInfo.OpenRead())
                {
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        fileDataStream.Write(buffer, 0, bytesRead);
                    }
                }

                //Footer
                byte[] fileFooterBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--");
                fileDataStream.Write(fileFooterBytes, 0, fileFooterBytes.Length);
                fileDataStream.Position = 0;
                return fileDataStream;
            }
            catch(IOException e){
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
        }


        public override string ToString()
        {
            Dictionary<string, string> reqHeaders = new Dictionary<string, string>(RequestHeaders);
            reqHeaders["Authorization"] = "## can't disclose ##";
            return APIConstants.URL + "=" + url + " ," + APIConstants.HEADERS + " = " + CommonUtil.DictToString(reqHeaders) + " ," + APIConstants.PARAMS + " = " + CommonUtil.DictToString(RequestParams) + ".";
        }
    }
}
