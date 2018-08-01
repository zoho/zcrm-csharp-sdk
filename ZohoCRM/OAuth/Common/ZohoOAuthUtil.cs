using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Common.ConfigFileHandler;

namespace ZCRMSDK.OAuth.Common
{
    public static class ZohoOAuthUtil
    {
        internal static Dictionary<string, string> GetFileAsDict(Stream inputStream)
        {
            try
            {
                Dictionary<string, string> outDict = new Dictionary<string, string>();
                using (StreamReader reader = new StreamReader(inputStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split('=');
                        if (!values[0].StartsWith("#", StringComparison.CurrentCulture))
                        {
                            string val = null;
                            if (values.Length == 2 && values[1] != null && values[1] != "" ) 
                            {
                                val = values[1];
                            }
                            outDict.Add(values[0], val);

                        }
                    }
                }
                return outDict;
            }
            catch(ArgumentNullException e)
            {
                ZCRMLogger.LogError("Exception while initializing Zoho OAuth Client .. Essential configuration data not found");
                throw new ZohoOAuthException(e);
            }
        }

        internal static Dictionary<string, string> GetConfigFileAsDict(string sectionName)
        {
            Dictionary<string, string> retDict = new Dictionary<string, string>();
            try{
                ExeConfigurationFileMap configurationFileMap = new ExeConfigurationFileMap();
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigFileSection section = configuration.GetSection(sectionName) as ConfigFileSection;
                if(section != null)
                {
                    foreach(ConfigFileElement element in section.Settings)
                    {
                        retDict.Add(element.Key, element.Value);
                    }
                }

            }catch(Exception e)
            {
                ZCRMLogger.LogError(e);
                throw new ZohoOAuthException(e);
            }
            return retDict;
        }


    }
}
