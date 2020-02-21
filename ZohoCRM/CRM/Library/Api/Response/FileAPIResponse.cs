using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.CRMException;

namespace ZCRMSDK.CRM.Library.Api.Response
{
    public class FileAPIResponse : APIResponse, IDisposable
    {
        private readonly HttpWebResponse response;


        private HttpWebResponse Response
        {
            get
            {
                return this.response;
            }
        }
        public FileAPIResponse(HttpWebResponse response)
        {
            this.response = response;
        }

        public FileAPIResponse(HttpWebResponse response, int statusCode, string responseString, WebHeaderCollection headers) : base(statusCode, responseString, headers)
        {
            this.response = response;
        }

        public string GetFileName()
        {
            string fileMetaData = responseHeaders.ContentDisposition;
            string fileName = fileMetaData.Split(new string[] { "=" }, StringSplitOptions.None)[1];
            if(fileName.Contains("''"))
            {
                fileName = fileName.Split(new string[] {"''"}, StringSplitOptions.None)[1];
            }
            fileName = fileName.Trim('\"');
            return fileName;
        }

        protected override void SetResponseJSON(string responseString)
        {
            string contentType = responseHeaders.ContentType;
            if (!string.IsNullOrEmpty(contentType) && !string.IsNullOrWhiteSpace(contentType) && contentType.Contains("json"))
            {
                if (!string.IsNullOrEmpty(responseString) && !string.IsNullOrWhiteSpace(responseString))
                {
                    ResponseJSON = JObject.Parse(responseString);
                }
            }
            else
            {
                ResponseJSON = new JObject();
                if (HttpStatusCode == APIConstants.ResponseCode.OK && !string.IsNullOrEmpty(responseHeaders.ContentDisposition))
                {
                    Status = APIConstants.CODE_SUCCESS;
                }
            }
        }

        public Stream GetFileAsStream()
        {
            Stream outStream = Response.GetResponseStream();
            return outStream;
        }

        public void Dispose()
        {
            response.Dispose();
        }
    }
}
