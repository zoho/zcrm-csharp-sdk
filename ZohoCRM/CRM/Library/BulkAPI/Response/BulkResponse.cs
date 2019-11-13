using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ZCRMSDK.CRM.Library.Api;
using ZCRMSDK.CRM.Library.BulkAPI.Handler;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.CRUD;

namespace ZCRMSDK.CRM.Library.BulkAPI.Response
{
    public class BulkResponse
    {
        private StreamReader filePointer;
        private string moduleAPIName;
        private List<string> fieldAPINames = new List<string>();
        private Dictionary<string, object> fieldvsValue = new Dictionary<string, object>();
        private Dictionary<string, object> data = new Dictionary<string, object>();
        //private List<>
        private BulkAPIHandler apiHandlerIns;
        private int rowNumber;
        private bool checkFailedRecord;
        private string fileType;

        public BulkResponse() { }

        public BulkResponse(string moduleAPIName, StreamReader filePointer, bool checkFailedRecord,string fileType)
        {
            this.ModuleAPIName = moduleAPIName;
            this.FilePointer = filePointer;
            this.checkFailedRecord = checkFailedRecord;
            this.fileType = fileType;
        }

        public Dictionary<string, object> FieldvsValues
        {
            get
            {
                return this.fieldvsValue;
            }
            set
            {
                this.fieldvsValue = value;
            }
        }

        public List<string> FieldAPINames
        {
            get
            {
                return this.fieldAPINames;
            }
            set
            {
                this.fieldAPINames = value;
            }
        }

        public string ModuleAPIName
        {
            get
            {
                return this.moduleAPIName;
            }
            set
            {
                this.moduleAPIName = value;
            }
        }

        public StreamReader FilePointer
        {
            get
            {
                return this.filePointer;
            }
            set
            {
                this.filePointer = value;
            }
        }

        public BulkAPIHandler APIHandlerIns
        {
            get
            {
                return this.apiHandlerIns;
            }
            set
            {
                this.apiHandlerIns = value;
            }
        }

        public Dictionary<string, object> Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
        public void SetFieldValues(List<string> fieldValues)
        {
            if (fieldValues.Count == this.fieldAPINames.Count)
            {
                for (int index = 0; index < this.fieldAPINames.Count; index++)
                {
                    this.fieldvsValue[this.fieldAPINames[index]] = fieldValues[index];
                }
            }
        }

        public ZCRMRecord Next()
        {
            return this.APIHandlerIns.Next(this.ModuleAPIName, this.FieldvsValues, this.rowNumber);
        }

        public bool HasNext()
        {
            this.fieldvsValue = new Dictionary<string, object>(); string line = null;
            if(this.FilePointer.BaseStream == null)
            {
                return false;
            }
            try
            {
                if(this.fileType.Equals("ics"))
                {
                    while (!this.FilePointer.EndOfStream)
                    {
                        line = this.FilePointer.ReadLine();
                        if(line.Contains(":"))
                        {
                            var value = line.Split(new[] { ':' }, 2);
                            if (value[0].Equals("END") && this.FieldvsValues.Count > 0)
                            {
                                this.FieldvsValues[value[0]] = value[1];
                                return true;
                            }
                            else
                            {
                                this.FieldvsValues[value[0]] = value[1];
                            }
                        }
                    }
                    if(this.FilePointer.EndOfStream)
                    {
                        this.FilePointer.Close();
                    }
                }
                else
                {
                    line = this.FilePointer.ReadLine();
                    if (this.checkFailedRecord)
                    {
                        this.rowNumber++;
                        while (line != null)
                        {
                            List<string> fieldValues = this.APIHandlerIns.ParseCsvRow(line);
                            if (this.fieldAPINames.Contains(APIConstants.BULK_WRITE_STATUS))
                            {
                                int index = this.fieldAPINames.IndexOf(APIConstants.BULK_WRITE_STATUS);
                                if (!APIConstants.WRITE_STATUS.Contains(fieldValues[index]))
                                {
                                    this.SetFieldValues(fieldValues);
                                    return true;
                                }
                            }
                            line = this.FilePointer.ReadLine();
                        }
                        this.rowNumber = 0;
                        this.FilePointer.Close();
                    }
                    else
                    {
                        if (line != null)
                        {
                            List<string> fieldValues = this.APIHandlerIns.ParseCsvRow(line);
                            this.SetFieldValues(fieldValues);
                            this.rowNumber++;
                            return true;
                        }
                        else
                        {
                            this.rowNumber = 0;
                            this.FilePointer.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZCRMLogger.LogError(ex.Message);
                return false;
            }
            return false;
        }

        public void Close()
        {
            this.rowNumber = 0;
            this.FilePointer.Close();
        }
    }
}