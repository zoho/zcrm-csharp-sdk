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

        internal string Code
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
        internal string ErrorMsg { 
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

        internal JObject ErrorDetails
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

        public ZCRMException(string code, string message, JObject errorDetails) : base(message)
        {
            Code = code;
            ErrorMsg = message;
            ErrorDetails = errorDetails;
        }

        public ZCRMException(string code, string message) : this(code: code, message: message, errorDetails: null) { }

        public ZCRMException(string message) : this(code: null, message: message, errorDetails: null) { }

        public ZCRMException(string code, Exception ex) : base(code, ex) 
        {
            originalException = ex;
            Code = code;
        }

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
