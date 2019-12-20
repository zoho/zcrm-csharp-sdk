using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using ZCRMSDK.CRM.Library.Api;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.BulkAPI.Response;
using ZCRMSDK.CRM.Library.BulkCRUD;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.Users;
using Newtonsoft.Json;
using System.Reflection;

namespace ZCRMSDK.CRM.Library.BulkAPI.Handler
{
    public class BulkAPIHandler
    {
        protected ZCRMBulkRead bulkReadRecordIns;
        protected ZCRMBulkWrite bulkWriteRecordIns;
        protected ZCRMRecord recordIns;
        private string fileName;

        private BulkAPIHandler(ZCRMBulkRead zcrmRecord)
        {
            this.bulkReadRecordIns = zcrmRecord;
        }

        public static BulkAPIHandler GetInstance(ZCRMBulkRead zcrmRecord)
        {
            return new BulkAPIHandler(zcrmRecord);
        }

        private BulkAPIHandler(ZCRMBulkWrite zcrmRecord)
        {
            this.bulkWriteRecordIns = zcrmRecord;
        }

        public static BulkAPIHandler GetInstance(ZCRMBulkWrite zcrmRecord)
        {
            return new BulkAPIHandler(zcrmRecord);
        }

        public BulkResponse ProcessZip(string filePath, bool download, string fileName, string operationType, string fileURL, bool checkFailed)
        {
            StreamReader streamReader = null;
            string fileType = "";
            List<string> fieldAPINames = new List<string>();
            Dictionary<string, object> EventsData = new Dictionary<string, object>();
            string ModuleAPIName;
            try
            {
                if (operationType.Equals(APIConstants.READ))
                {
                    if (download && fileName == null)
                    {
                        FileAPIResponse fileResponse = BulkReadAPIHandler.GetInstance(this.bulkReadRecordIns).DownloadBulkReadResult();
                        if (!fileResponse.HttpStatusCode.Equals(APIConstants.ResponseCode.OK))
                        {
                            throw new ZCRMException("Zip file not downloaded");
                        }
                        else
                        {
                            if (!this.WriteStreamtoZipFile(fileResponse, filePath + "/"))
                            {
                                throw new ZCRMException("Error while writing file in the file path specified: " + filePath + "/");
                            }
                            if (!this.Unzip(filePath + "/" + fileResponse.GetFileName(), filePath + "/"))
                            {
                                throw new ZCRMException("Error occurred while unzipping the file: " + filePath + "/" + fileResponse.GetFileName());
                            }
                            streamReader = new StreamReader(File.OpenRead(filePath + "/" + this.fileName));
                        }
                        fileResponse = null;
                    }
                    ModuleAPIName = this.bulkReadRecordIns.ModuleAPIName;
                }
                else
                {
                    if (download && fileName == null && fileURL != null)
                    {
                        FileAPIResponse fileResponse = BulkWriteAPIHandler.GetInstance(this.bulkWriteRecordIns).DownloadBulkWriteResult(fileURL);
                        if (!fileResponse.HttpStatusCode.Equals(APIConstants.ResponseCode.OK))
                        {
                            throw new ZCRMException("Zip file not downloaded");
                        }
                        else
                        {
                            if (!this.WriteStreamtoZipFile(fileResponse, filePath + "/"))
                            {
                                throw new ZCRMException("Error while writing file in the file path specified: " + filePath + "/");
                            }

                            if (!this.Unzip(filePath + "/" + fileResponse.GetFileName(), filePath + "/"))
                            {
                                throw new ZCRMException("Error occurred while unzipping the file: " + filePath + "/" + fileResponse.GetFileName());
                            }
                            streamReader = new StreamReader(File.OpenRead(filePath + "/" + this.fileName));
                        }
                        fileResponse = null;
                    }
                    ModuleAPIName = this.bulkWriteRecordIns.ModuleAPIName;
                }
                if (streamReader == null && fileName != null)
                {
                    if (!Unzip(filePath + "/" + fileName, filePath + "/"))
                    {
                        throw new ZCRMException("Error occurred while unzipping the file: " + filePath + "/" + fileName);
                    }
                    streamReader = new StreamReader(File.OpenRead(filePath + "/" + this.fileName));
                }
                try
                {
                    if(this.fileName.Contains(".ics"))
                    {
                        fileType = "ics";
                        long cur_pos = 0;
                        int length = 0;
                        while (!streamReader.EndOfStream)
                        {
                            string line = streamReader.ReadLine();
                            cur_pos = cur_pos + line.Length;
                            var value = line.Split(new[] { ':' }, 2);
                            if (!EventsData.ContainsKey(value[0]))
                            {
                                EventsData[value[0]] = value[1];
                            }
                            else
                            {
                                break;
                            }
                            length = value.Length;
                        }
                        streamReader.BaseStream.Seek(cur_pos, SeekOrigin.Begin);
                        streamReader.DiscardBufferedData();
                    }
                    else
                    {
                        string header = streamReader.ReadLine();
                        if (header != null)
                        {
                            fieldAPINames = this.ParseCsvRow(header);
                        }
                    }

                }
                catch (Exception e) when (!(e is ZCRMException))
                {
                    ZCRMLogger.LogError(e);
                    throw new ZCRMException(APIConstants.SDK_ERROR, e);
                }
                this.fileName = null;
                BulkResponse bulkResponse = new BulkResponse(ModuleAPIName, streamReader, checkFailed, fileType);
                bulkResponse.FieldAPINames = fieldAPINames;
                bulkResponse.APIHandlerIns = this;
                if(fileType.Equals("ics"))
                {
                    EventsData["EventsData"] = bulkResponse;
                    EventsData["END"] = (string)EventsData["BEGIN"];
                    bulkResponse.Data = EventsData;
                }
                return bulkResponse;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        protected bool WriteStreamtoZipFile(FileAPIResponse fileResponse, string filePath)
        {
            try
            {
                Stream file = fileResponse.GetFileAsStream(); //To get attachment file content
                CommonUtil.SaveStreamAsFile(filePath, file, fileResponse.GetFileName()); //To write a new file using the same attachment name
                file.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public ZCRMRecord SetRecordProperties(string ModuleAPIName, Dictionary<string, object> fieldvsValue, int rowNumber)
        {
            recordIns = null;
            recordIns = new ZCRMRecord(ModuleAPIName);
            recordIns.RowNumber = rowNumber;
            foreach (KeyValuePair<string, object> data in fieldvsValue)
            {
                if (data.Key.Equals("Id") || data.Key.Equals("RECORD_ID"))
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        recordIns.EntityId = Convert.ToInt64(data.Value);
                    }
                }
                else if (data.Key.Equals("Created_By"))
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        ZCRMUser createdBy = recordIns.CreatedBy ?? ZCRMUser.GetInstance();
                        createdBy.Id = Convert.ToInt64(data.Value);
                        recordIns.CreatedBy = createdBy;
                    }
                }
                else if (data.Key.Equals("Modified_By"))
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        ZCRMUser modifiedBy = recordIns.ModifiedBy ?? ZCRMUser.GetInstance();
                        modifiedBy.Id = Convert.ToInt64(data.Value);
                        recordIns.ModifiedBy = modifiedBy;
                    }
                }
                else if (data.Key.Equals("Created_Time"))
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        recordIns.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(data.Value));
                    }
                }
                else if (data.Key.Equals("Modified_Time"))
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        recordIns.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(data.Value));
                    }
                }
                else if (data.Key.Equals("Owner"))
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        ZCRMUser owner = recordIns.Owner != null ? recordIns.Owner : ZCRMUser.GetInstance();
                        owner.Id = Convert.ToInt64(data.Value);
                        recordIns.Owner = owner;
                    }
                }
                else if (data.Key.Equals("STATUS"))
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        recordIns.Status = (string)data.Value;
                    }
                }
                else if (data.Key.Equals("ERRORS"))
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        recordIns.Error = (string)data.Value;
                    }
                }
                else if (data.Key.StartsWith("$", StringComparison.Ordinal))
                {
                    var regex = new Regex(Regex.Escape("$"));
                    string fieldName = regex.Replace(data.Key, "", 1);
                    //string fieldName = fieldAPINames[i].re("\\$", "");
                    if (APIConstants.PROPERTIES_AS_FILEDS.Contains(fieldName))
                    {
                        if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                        {
                            recordIns.SetFieldValue(fieldName, data.Value);
                        }
                    }
                    else
                    {
                        if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                        {
                            recordIns.SetProperty(fieldName, data.Value);
                        }
                    }
                }
                else
                {
                    if (data.Value != null && !String.IsNullOrEmpty(Convert.ToString(data.Value)) && !String.IsNullOrWhiteSpace(Convert.ToString(data.Value)))
                    {
                        recordIns.SetFieldValue(data.Key, data.Value);
                    }
                }
            }
            return recordIns;
        }

        private bool Unzip(string zipFilePath, string destDir)
        {
            this.fileName = null;
            try
            {
                if (File.Exists(zipFilePath + ".zip") || File.Exists(zipFilePath + ".csv") || File.Exists(zipFilePath))
                {
                    string filePath = File.Exists(zipFilePath + ".zip") ? zipFilePath + ".zip" : zipFilePath;
                    if (filePath.Contains("zip"))
                    {
                        ZipArchive archive = ZipFile.OpenRead(filePath);
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            char[] remove = { '/' };
                            string file_name = entry.FullName.TrimStart(remove);
                            string destinationPath = Path.GetFullPath(Path.Combine(destDir, file_name));
                            if (destinationPath.StartsWith(destDir, StringComparison.Ordinal))
                            {
                                this.fileName = file_name;
                                entry.ExtractToFile(destinationPath, true);
                            }
                        }
                        archive.Dispose();
                    }
                    else
                    {
                        this.fileName = File.Exists(zipFilePath + ".csv") ? Path.GetFileName(zipFilePath + ".csv") : Path.GetFileName(zipFilePath);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ZCRMLogger.LogError(ex);
                return false;
            }
            return true;
        }


        public List<string> ParseCsvRow(string value)
        {
            string[] c;
            List<string> resp = new List<string>();
            bool cont = false;
            string cs = "";
            c = value.Split(new char[] { ',' }, StringSplitOptions.None);
            foreach (string y in c)
            {
                string x = y;
                if (cont)
                {
                    // End of field
                    if (x.EndsWith("\"", StringComparison.Ordinal))
                    {
                        cs += "," + x.Substring(0, x.Length - 1);
                        resp.Add(cs);
                        cs = "";
                        cont = false;
                        continue;
                    }
                    else
                    {
                        // Field still not ended
                        cs += "," + x;
                        continue;
                    }
                }
                // Fully encapsulated with no comma within
                if (x.StartsWith("\"", StringComparison.Ordinal) && x.EndsWith("\"", StringComparison.Ordinal))
                {
                    if ((x.EndsWith("\"\"", StringComparison.Ordinal) && !x.EndsWith("\"\"\"", StringComparison.Ordinal)) && x != "\"\"")
                    {
                        cont = true;
                        cs = x;
                        continue;
                    }
                    resp.Add(x.Substring(1, x.Length - 2));
                    continue;
                }
                // Start of encapsulation but comma has split it into at least next field
                if (x.StartsWith("\"", StringComparison.Ordinal) && !x.EndsWith("\"", StringComparison.Ordinal))
                {
                    cont = true;
                    cs += x.Substring(1);
                    continue;
                }
                // Non encapsulated complete field
                resp.Add(x);
            }
            return resp;
        }

        public ZCRMRecord Next(string ModuleAPIName, Dictionary<string, object> fieldvsValue, int rowNumber)
        {
            if (fieldvsValue != null)
            {
                return SetRecordProperties(ModuleAPIName, fieldvsValue, rowNumber);
            }
            else
            {
                return ZCRMRecord.GetInstance(ModuleAPIName, null);
            }
        }
    }

    public static class StreamReaderExtensions
    {
        readonly static FieldInfo charPosField = typeof(StreamReader).GetField("charPos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | BindingFlags.DeclaredOnly);
        readonly static FieldInfo byteLenField = typeof(StreamReader).GetField("byteLen", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | BindingFlags.DeclaredOnly);
        readonly static FieldInfo charBufferField = typeof(StreamReader).GetField("charBuffer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | BindingFlags.DeclaredOnly);

        public static long GetPosition(this StreamReader reader)
        {
            //shift position back from BaseStream.Position by the number of bytes read
            //into internal buffer.
            int byteLen = (int)byteLenField.GetValue(reader);
            var position = reader.BaseStream.Position - byteLen;

            //if we have consumed chars from the buffer we need to calculate how many
            //bytes they represent in the current encoding and add that to the position.
            int charPos = (int)charPosField.GetValue(reader);
            if (charPos > 0)
            {
                var charBuffer = (char[])charBufferField.GetValue(reader);
                var encoding = reader.CurrentEncoding;
                var bytesConsumed = encoding.GetBytes(charBuffer, 0, charPos).Length;
                position += bytesConsumed;
            }

            return position;
        }

        public static void SetPosition(this StreamReader reader, long position)
        {
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(position, SeekOrigin.Begin);
        }
    }
}