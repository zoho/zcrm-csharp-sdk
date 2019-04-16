using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public interface IAPIHandler
    {
        APIConstants.RequestMethod GetRequestMethod();

        string GetUrlPath();

        JObject GetRequestBody();

        Dictionary<string, string> GetRequestHeaders();

        Dictionary<string, string> GetRequestQueryParams();
    }
}