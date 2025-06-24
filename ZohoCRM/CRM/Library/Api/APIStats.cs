using System.Collections.Generic;
using System.Net;
using ZCRMSDK.CRM.Library.CRMException;

namespace ZCRMSDK.CRM.Library.Api
{
    public static class APIStats
    {
        private static Dictionary<string, string> apiCountStats = new Dictionary<string, string>();


        public static void UpdateStats(HttpWebResponse response)
        {
            UpdateCountStats(response);
        }

        private static void UpdateCountStats(HttpWebResponse response)
        {
            apiCountStats[APIConstants.ALLOWED_API_CALLS_PER_MINUTE] =  response.GetResponseHeader(APIConstants.ALLOWED_API_CALLS_PER_MINUTE);
            apiCountStats[APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW] = response.GetResponseHeader(APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW);
            apiCountStats[APIConstants.REMAINING_TIME_FOR_WINDOW__RESET] = response.GetResponseHeader(APIConstants.REMAINING_TIME_FOR_WINDOW__RESET);
        }

        public static string GetAllowedAPICallsPerMinute()
        {
            try
            {
                return apiCountStats[APIConstants.ALLOWED_API_CALLS_PER_MINUTE];
            }
            catch(KeyNotFoundException e)
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(true, "Stats not set!.", e);
            }
        }

        public static string GetRemainingCountForCurrentWindow()
        {
            try
            {
                return apiCountStats[APIConstants.REMAINING_COUNT_FOR_THIS_WINDOW];
            }
            catch (KeyNotFoundException e)
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(true, "Stats not set!.", e);
            }

        }
        public static string GetRemainingTimeForWindowReset()
        {
            try
            {
                return apiCountStats[APIConstants.REMAINING_TIME_FOR_WINDOW__RESET];
            }
            catch (KeyNotFoundException e)
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(true, "Stats not set!.", e);
            }
        }
    }
}
