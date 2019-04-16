using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.Api.Response
{
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

        public JObject ResponseJSON
        {
            get
            {
                return responseJSON;
            }
            private set
            {
                responseJSON = value;
            }
        }
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
        public string Status
        {
            get
            {
                return status;
            }
            private set
            {
                status = value;
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
        public string Code
        {
            get
            {
                return code;
            }
            private set
            {
                code = value;
            }
        }
        public JObject ErrorDetails
        {
            get
            {
                return errorDetails;
            }
            private set
            {
                errorDetails = value;
            }
        }
    }
}
