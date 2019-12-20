using System;
using Newtonsoft.Json.Linq;


namespace ZCRMSDK.CRM.Library.CRMException
{
    public class ZCRMException : Exception
    {

        private string code;
        private string errorMsg;

        private Exception originalException;
        private JObject errorDetails;

        private int? httpStatusCode;

        private bool isAPIException;

        public bool IsSDKException
        {
            get
            {
                return !isAPIException;
            }
            set
            {
                isAPIException = !value;
            }
        }

        public bool IsAPIException
        {
            get
            {
                return isAPIException;
            }
            set
            {
                isAPIException = value;
            }
        }

        public int? HttpStatusCode
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
        public string ErrorMsg { 
            get {
                if (originalException != null)
                    return originalException.ToString();
                return errorMsg;
            }
            private set
            {
                errorMsg = value;
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

        public ZCRMException(bool isAPIException, int? httpStatusCode, string code, string message, JObject errorDetails) : base(message)
        {
            IsAPIException = isAPIException;
            HttpStatusCode = httpStatusCode;
            Code = code;
            ErrorMsg = message;
            ErrorDetails = errorDetails;
        }

        public ZCRMException(bool isAPIException,int httpStatusCode, string code, string message) : this(isAPIException : isAPIException, httpStatusCode: httpStatusCode, code: code, message: message, errorDetails: null) { }

        public ZCRMException(bool isAPIException, int httpStatusCode, string code) : this(isAPIException: isAPIException, httpStatusCode: httpStatusCode, code: code, message: null) { }

        public ZCRMException(string code, string message, JObject errorDetails) : this(isAPIException : false, httpStatusCode: null, code: code, message: message, errorDetails: errorDetails) { }

        public ZCRMException(string code, string message) : this(code: code, message: message, errorDetails: null) { }

        public ZCRMException(string message) : this(code: null, message: message, errorDetails: null) { }

        public ZCRMException(bool isAPIException, string code, Exception ex) : base(code, ex)
        {
            IsAPIException = isAPIException;
            originalException = ex;
            Code = code;
        }

        public ZCRMException(string code, Exception ex) : this(isAPIException : false, code: code, ex : ex) { }

        public ZCRMException(Exception ex) : this(null, ex) { }

        public override string ToString()
        {
            string returnMsg = typeof(ZCRMException).FullName + ". Caused by : ";
            if(Code != null)
            {
                returnMsg += Code + " - " + ErrorMsg;
            }
            else{
                returnMsg += ErrorMsg;
            }
            return returnMsg;
        }
    }
}
