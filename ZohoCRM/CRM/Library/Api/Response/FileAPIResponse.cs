using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.CRMException;

namespace ZCRMSDK.CRM.Library.Api.Response
{
    public class FileAPIResponse : APIResponse
    {
        public FileAPIResponse(HttpWebResponse response) : base(response) { }

        public string GetFileName()
        {
            string fileMetaData = Response.Headers["Content-Disposition"];
            string fileName = fileMetaData.Split(new string[] { "=" }, StringSplitOptions.None)[1];
            if(fileName.Contains("''"))
            {
                fileName = fileName.Split(new string[] {"''"}, StringSplitOptions.None)[1];

            }
            fileName = fileName.Trim('\"');
            return fileName;
        }

        protected override void SetResponseJSON()
        {
            string contentType = Response.GetResponseHeader("Content-Type");
            if (!String.IsNullOrEmpty(contentType) && !String.IsNullOrWhiteSpace(contentType) && contentType.Contains("json"))
            {
                string responseString = new StreamReader(Response.GetResponseStream()).ReadToEnd();
                if (responseString != null && responseString != "")
                {
                    ResponseJSON = JObject.Parse(responseString);
                }
            }
            else
            {
                ResponseJSON = new JObject();
                string contentDisposition = Response.GetResponseHeader("Content-Disposition");
                if (HttpStatusCode == APIConstants.ResponseCode.OK && !string.IsNullOrEmpty(contentDisposition)) 
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
    }
}
