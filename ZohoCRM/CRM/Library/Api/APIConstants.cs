using System.Collections.Generic;
using System.Diagnostics;
namespace ZCRMSDK.CRM.Library.Api
{
    public static class APIConstants
    {
        public static readonly string ALLOWED_API_CALLS_PER_MINUTE = "X-RATELIMIT-LIMIT";
        public static readonly string REMAINING_COUNT_FOR_THIS_WINDOW = "X-RATELIMIT-REMAINING";
        public static readonly string REMAINING_TIME_FOR_WINDOW__RESET = "X-RATELIMIT-RESET";
        public static readonly string URL = "URL";
        public static readonly string HEADERS = "HEADERS";
        public static readonly string PARAMS = "PARAMS";
        public static readonly string STATUS_CODE = "STATUS_CODE";
        public static readonly string RESPONSE_JSON = "RESPONSE_JSON";
        public static readonly string RESPONSE_HEADERS = "RESPONSE_HEADERS";
        public static readonly string EXCEPTION_LOG_MSG = "ZCRM - ";
        public static readonly string SDK_ERROR = "ZCRM_INTERNAL_ERROR";
        public static readonly string AUTHENTICATION_FAILURE = "AUTHENTICATION_FAILURE";
        public static readonly string authHeaderPrefix = "Zoho-oauthtoken ";
        public static readonly string CODE_ERROR = "error";
        public static readonly string CODE_SUCCESS = "success";
        public static readonly string MESSAGE = "message";
        public static readonly string CODE = "code";
        public static readonly string STATUS = "status";
        public static readonly string DETAILS = "details";
        public static readonly string DATA = "data";
        public static readonly string TAGS = "tags";
        public static readonly string TAXES = "taxes";
        public static readonly string INFO = "info";
        public static readonly string PER_PAGE = "per_page";
        public static readonly string PAGE = "page";
        public static readonly string COUNT = "count";
        public static readonly string MORE_RECORDS = "more_records";
        public static readonly string ALLOWED_COUNT = "allowed_count";
        public static readonly string LEADS = "Leads";
        public static readonly string ACCOUNTS = "Accounts";
        public static readonly string CONTACTS = "Contacts";
        public static readonly string DEALS = "Deals";
        public static readonly string QUOTES = "Quotes";
        public static readonly string SALESORDERS = "SalesOrders";
        public static readonly string INVOICES = "Invoices";
        public static readonly string PURCHASEORDERS = "PurchaseOrders";
        public static readonly string INVALID_ID_MSG = "The given id seems to be invalid.";
        public static readonly string INVALID_DATA = "INVALID_DATA";
        public static readonly string API_MAX_RECORDS_MSG = "Cannot process more than 100 records at a time.";
        public static readonly string API_MAX_TAGS_MSG = "Cannot process more than 50 tags at a time.";
        public static readonly string API_MAX_RECORD_TAGS_MSG = "Cannot process more than 10 tags at a time.";
        public static readonly string ACTION = "action";
        public static readonly string DUPLICATE_FIELD = "duplicate_field";
        public static readonly string GMT = "GMT";
        public static readonly string MIN_LOG_LEVEL = "minLogLevel";
        public static readonly string APIBASEURL = "apiBaseUrl";
        public static readonly string DOMAIN_SUFFIX = "domainSuffix";
        public static readonly string USER_AGENT = "ZOHOCRM C# SDK";
        public static readonly int MAX_ALLOWED_FILE_SIZE_IN_MB = 20;
        public static readonly List<string> CONVERTIBLE_MODULES = new List<string>() { "Leads", "Quotes", "SalesOrders" };
        public static readonly List<string> PHOTO_NOTSUPPORTED_MODULES = new List<string>() { "Deals", "Activities", "Quotes", "Sales_Orders",
                                "Purchase_Orders", "Invoices", "Campaigns","Price_Books","Cases","Solutions","Tasks","Events","Calls","Notes","Attachments" };
        public static readonly List<string> PROPERTIES_AS_FILEDS = new List<string>() { "se_module", "gclid" };

        public static readonly List<ResponseCode?> FaultyResponseCodes;
        public static readonly Dictionary<string, TraceLevel> LOGGER_LEVELS;
        public static readonly Dictionary<string, string> ACCESS_TYPE;

        static APIConstants()
        {
            FaultyResponseCodes = new List<ResponseCode?>
            {
                ResponseCode.NO_CONTENT,
                ResponseCode.NOT_FOUND,
                ResponseCode.AUTHORIZATION_ERROR,
                ResponseCode.BAD_REQUEST,
                ResponseCode.FORBIDDEN,
                ResponseCode.INTERNAL_SERVER_ERROR,
                ResponseCode.METHOD_NOT_ALLOWED,
                ResponseCode.MOVED_PERMANENTLY,
                ResponseCode.MOVED_TEMPORARILY,
                ResponseCode.REQUEST_ENTITY_TOO_LARGE,
                ResponseCode.TOO_MANY_REQUEST,
                ResponseCode.UNSUPPORTED_MEDIA_TYPE
            };


            LOGGER_LEVELS = new Dictionary<string, TraceLevel>()
            {
                {"ALL", TraceLevel.Verbose},
                {"INFO", TraceLevel.Info},
                {"WARNING", TraceLevel.Warning},
                {"ERROR", TraceLevel.Error},
                {"OFF", TraceLevel.Off}
            };

            ACCESS_TYPE = new Dictionary<string, string>
            {
                { "Production", "www" },
                { "Development", "developer" },
                { "Sandbox", "sandbox" }
            };
        }

        public enum RequestMethod { GET, POST, PUT, DELETE }

        public enum ResponseCode
        {
            OK = 200, CREATED = 201, ACCEPTED = 202, NO_CONTENT = 204, MOVED_PERMANENTLY = 301,
            MOVED_TEMPORARILY = 302, NOT_MODIFIED = 304, BAD_REQUEST = 400, AUTHORIZATION_ERROR = 401, FORBIDDEN = 403, NOT_FOUND = 404,
            METHOD_NOT_ALLOWED = 405, REQUEST_ENTITY_TOO_LARGE = 413, UNSUPPORTED_MEDIA_TYPE = 415,
            TOO_MANY_REQUEST = 429, INTERNAL_SERVER_ERROR = 500 
        
        }

        public static ResponseCode? GetEnum(int code)
        {
            switch(code)
            {
                case 200:
                    return ResponseCode.OK;
                case 201:
                    return ResponseCode.CREATED;
                case 202:
                    return ResponseCode.ACCEPTED;
                case 204:
                    return ResponseCode.NO_CONTENT;
                case 301:
                    return ResponseCode.MOVED_PERMANENTLY;
                case 302:
                    return ResponseCode.MOVED_TEMPORARILY;
                case 304:
                    return ResponseCode.NOT_MODIFIED;
                case 400:
                    return ResponseCode.BAD_REQUEST;
                case 401:
                    return ResponseCode.AUTHORIZATION_ERROR;
                case 403:
                    return ResponseCode.FORBIDDEN;
                case 404:
                    return ResponseCode.NOT_FOUND;
                case 405:
                    return ResponseCode.METHOD_NOT_ALLOWED;
                case 413:
                    return ResponseCode.REQUEST_ENTITY_TOO_LARGE;
                case 415:
                    return ResponseCode.UNSUPPORTED_MEDIA_TYPE;
                case 429:
                    return ResponseCode.TOO_MANY_REQUEST;
                case 500:
                    return ResponseCode.INTERNAL_SERVER_ERROR;
                default:
                    return null;
            }

        }

       
        //TODO: Write enum class for RequestMethod and ResponseCode and implement their functions;

    }

 
}
