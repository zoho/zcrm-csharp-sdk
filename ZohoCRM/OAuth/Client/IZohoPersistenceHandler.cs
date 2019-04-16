using ZCRMSDK.OAuth.Contract;


namespace ZCRMSDK.OAuth.Client
{
    public interface IZohoPersistenceHandler
    {
        void SaveOAuthTokens(ZohoOAuthTokens zohoOAuthTokens);

        ZohoOAuthTokens GetOAuthTokens(string paramString);

        void DeleteOAuthTokens(string paramName);

    }
}
