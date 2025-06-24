

namespace ZCRMSDK.OAuth.Common
{
    internal static class ZohoOAuthConstants
    {
        internal static readonly string IAM_URL = "iamURL";
        internal static readonly string SCOPES = "scope";
        internal static readonly string STATE = "state";
        internal static readonly string STATE_OBTAINING_GRANT_TOKEN = "OBTAIN_GRANT_TOKEN";
        internal static readonly string RESPONSE_TYPE = "response_type";
        internal static readonly string RESPONSE_TYPE_CODE = "code";
        internal static readonly string USER_MAIL_ID = "user_mail_id";
        internal static readonly string OAUTH_TOKENS_FILE_PATH = "oauth_tokens_file_path";
        internal static readonly string CURRENT_USER_EMAIL = "currentUserEmail";
        internal static readonly string CLIENT_ID = "client_id";
        internal static readonly string CLIENT_SECRET = "client_secret";
        internal static readonly string REDIRECT_URL = "redirect_uri";
        internal static readonly string ACCESS_TYPE = "access_type";
        internal static readonly string ACCESS_TYPE_OFFLINE = "offline";
        internal static readonly string ACCESS_TYPE_ONLINE = "online";
        internal static readonly string PROMPT = "prompt";
        internal static readonly string PROMPT_CONSENT = "consent";
        internal static readonly string GRANT_TYPE = "grant_type";
        internal static readonly string GRANT_TYPE_AUTH_CODE = "authorization_code";
        internal static readonly string CODE = "code";
        internal static readonly string GRANT_TYPE_REFRESH = "refresh_token";
        internal static readonly string GRANT_TOKEN = "grant_token";
        internal static readonly string ACCESS_TOKEN = "access_token";
        internal static readonly string REFRESH_TOKEN = "refresh_token";
        internal static readonly string EXPIRES_IN = "expires_in";
        internal static readonly string EXPIRY_TIME = "expiry_time";
        internal static readonly string PERSISTENCE_HANDLER_CLASS = "persistence_handler_class";
        internal static readonly string TOKEN = "token";
        internal static readonly string DISPATCH_TO = "dispatchTo";
        internal static readonly string OAUTH_TOKENS_PARAM = "oauth_tokens";
        internal static readonly string AuthHeaderPrefix = "Zoho-oauthtoken ";
        internal static readonly string IAM_SCOPE = "AaaServer.profile.Read";
        internal static readonly string MYSQL_USERNAME = "mysql_username";
        internal static readonly string MYSQL_PASSWORD = "mysql_password";
        internal static readonly string MYSQL_SERVER = "mysql_server";
        internal static readonly string MYSQL_DATABASE = "mysql_database";
        internal static readonly string MYSQL_PORT = "mysql_port";
        internal static readonly string DEFAULT_PERSISTENCE_HANDLER = "ZCRMSDK.OAuth.ClientApp.ZohoOAuthInMemoryPersistence, ZCRMSDK";
    }
}
