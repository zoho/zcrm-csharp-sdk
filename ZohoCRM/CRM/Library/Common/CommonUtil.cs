using System;
using System.IO;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Api;
using ZCRMSDK.CRM.Library.Common.ConfigFileHandler;
using System.Configuration;

namespace ZCRMSDK.CRM.Library.Common
{
    
    public class CommonUtil
    {

        public enum SortOrder { asc, desc }

        public enum PhotoSize { stamp, thumb, original, favicon, medium }

        public static Dictionary<string, string> GetConfigFileAsDict(string sectionName)
        {
            Dictionary<string, string> retDict = new Dictionary<string, string>();
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigFileSection section = configuration.GetSection(sectionName) as ConfigFileSection;
                if (section != null)
                {
                    foreach (ConfigFileElement element in section.Settings)
                    {
                        if(element.Value != null && element.Value != "")
                        retDict.Add(element.Key, element.Value);
                    }
                }

            }
            catch (Exception e)
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
            return retDict;
        }



        public static Dictionary<string, string> GetFileAsDict(Stream inputStream)
        {
            try{
                Dictionary<string, string> outDict = new Dictionary<string, string>();
                using(StreamReader reader = new StreamReader(inputStream))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split('=');
                        if (!values[0].StartsWith("#", StringComparison.CurrentCulture))
                        {
                            string val = null;
                            if (values.Length == 2 && values[1] != null && values[1] != "")
                            {
                                val = values[1];
                            }
                            else{
                                continue;
                            }
                            outDict.Add(values[0], val);
                        }
                    }
                }
                return outDict;
            }
            catch(Exception e)
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(e);
            }
        }


        internal static string CollectionToCommaDelimitedString<T>(List<T> fields)
        {
            return string.Join(",", fields);
        }


        public static void SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
        {
            DirectoryInfo directory = new DirectoryInfo(filePath);
            if(!directory.Exists)
            {
                directory.Create();
            }

            string fullFilePath = Path.Combine(filePath, fileName);
            using(FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
            {
                inputStream.CopyTo(outputFileStream);
            }
        }

        public static void CheckIsPhotoSupported(string moduleAPIName)
        {

            if(APIConstants.PHOTO_NOTSUPPORTED_MODULES.Contains(moduleAPIName))
            {
                throw new ZCRMException("Given module is not a photo supported module");
            }
        }

        public static void ValidateFile(string filePath)
        {
            if(!File.Exists(filePath))
            {
                throw new ZCRMException("No such file or directory");
            }
            FileInfo fileInfo = new FileInfo(filePath);
            if(fileInfo.Length/1048576L > 20L)
            {
                throw new ZCRMException("File size is more than allowed size " + APIConstants.MAX_ALLOWED_FILE_SIZE_IN_MB + "MB.");
            }
        }

        public static string DictToString(Dictionary<string, string> dict)
        {
            string dictString = "{";
            foreach(KeyValuePair<string, string> keyValues in dict)
            {
                dictString += keyValues.Key + " : " + keyValues.Value + ", ";
            }
            return dictString.TrimEnd(',', ' ')+"}";
        }
        public static string RemoveEscaping(string input)
        {
            input = input.Replace("\\", "");
            input = input.Replace("\"", "");
            return input;
        }


    }
}
