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

        private HttpWebResponse Response { get => response; }

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
            if(HttpStatusCode == APIConstants.ResponseCode.OK || HttpStatusCode == APIConstants.ResponseCode.NO_CONTENT)
            {
                ResponseJSON = new JObject();
                if (HttpStatusCode == APIConstants.ResponseCode.OK && !string.IsNullOrEmpty(responseHeaders.ContentDisposition)) 
                {
                    Status = APIConstants.CODE_SUCCESS;
                }
                else
                {
                    if (responseString != null && responseString != "")
                    {
                        ResponseJSON = JObject.Parse(responseString);
                    }
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
