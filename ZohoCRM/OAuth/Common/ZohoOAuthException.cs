using System;

//TODO: Learn about exception and complete the class
namespace ZCRMSDK.OAuth.Common
{
    public class ZohoOAuthException : Exception
    {
        private string message;
        private Exception originalException = null;

        //TODO: Inspect this class and learn about Exception class;

        public new string Message { get => message; set => message = value; }
        public Exception OriginalException { get => originalException; set => originalException = value; }

        public ZohoOAuthException(string errorMessage){
            Message = errorMessage;
        } 

        public ZohoOAuthException(Exception e){
            OriginalException = e;
        }

        public override string ToString()
        {
            string returnMessage = typeof(ZohoOAuthException).Name + ".Caused by :" ;
            if(OriginalException != null){
                returnMessage = returnMessage + OriginalException;
            }
            else {
                returnMessage += Message;
            }

            return returnMessage;
        }
    }
}
