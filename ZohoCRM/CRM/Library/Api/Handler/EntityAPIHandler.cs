using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZCRMSDK.CRM.Library.CRUD;
using ZCRMSDK.CRM.Library.Setup.Users;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.CRMException;
using ZCRMSDK.CRM.Library.Common;
using Newtonsoft.Json;
using System.IO;

namespace ZCRMSDK.CRM.Library.Api.Handler
{
    public class EntityAPIHandler : CommonAPIHandler, IAPIHandler
    {
        //NOTE:Property not used;
        protected ZCRMRecord record = null;


        protected EntityAPIHandler(ZCRMRecord zcrmRecord)
        {
            record = zcrmRecord;
        }

        public static EntityAPIHandler GetInstance(ZCRMRecord zcrmRecord)
        {
            return new EntityAPIHandler(zcrmRecord);
        }

        public APIResponse GetRecord()
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = record.ModuleAPIName + "/" + record.EntityId;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject recordDetails = (JObject)responseDataArray[0];
                SetRecordProperties(recordDetails);
                response.Data = record;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse CreateRecord(List<string> trigger, string lar_id)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = record.ModuleAPIName;
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                dataArray.Add(GetZCRMRecordAsJSON());
                requestBodyObject.Add(APIConstants.DATA, dataArray);
                if (trigger != null && trigger.Count > 0)
                {
                    requestBodyObject.Add("trigger", JArray.FromObject(trigger));
                }
                if (lar_id != null)
                {
                    requestBodyObject.Add("lar_id", lar_id);
                }
                requestBody = requestBodyObject;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject responseData = (JObject)responseDataArray[0];
                JObject recordDetails = (JObject)responseData[APIConstants.DETAILS];
                SetRecordProperties(recordDetails);
                response.Data = record;
                return response;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse UpdateRecord(List<string> trigger)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.PUT;
                urlPath = record.ModuleAPIName + "/" + record.EntityId;
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                dataArray.Add(GetZCRMRecordAsJSON());
                requestBodyObject.Add(APIConstants.DATA, dataArray);
                if (trigger != null && trigger.Count > 0)
                {
                    requestBodyObject.Add("trigger", JArray.FromObject(trigger));
                }
                requestBody = requestBodyObject;

                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();

                JArray responseDataArray = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject responseData = (JObject)responseDataArray[0];
                JObject responseDetails = (JObject)responseData[APIConstants.DETAILS];
                SetRecordProperties(responseDetails);
                response.Data = record;
                return response;

            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse DeleteRecord()
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.DELETE;
                urlPath = record.ModuleAPIName + "/" + record.EntityId;

                return APIRequest.GetInstance(this).GetAPIResponse();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public Dictionary<string, long> ConvertRecord(ZCRMRecord potential, ZCRMUser assignToUser)
        {
            try
            {
                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = record.ModuleAPIName + "/" + record.EntityId + "/actions/convert";
                JObject requestBodyObject = new JObject();
                JArray dataArray = new JArray();
                JObject dataObject = new JObject();
                if (assignToUser != null)
                {
                    dataObject.Add("assign_to", assignToUser.Id.ToString());
                }
                if (potential != null)
                {
                    dataObject.Add(APIConstants.DEALS, GetInstance(potential).GetZCRMRecordAsJSON());
                }
                dataArray.Add(dataObject);
                requestBodyObject.Add(APIConstants.DATA, dataArray);
                requestBody = requestBodyObject;
                APIResponse response = APIRequest.GetInstance(this).GetAPIResponse();
                JArray responseJson = (JArray)response.ResponseJSON[APIConstants.DATA];
                JObject convertedIdsJSON = (JObject)responseJson[0];

                Dictionary<string, long> convertedIds = new Dictionary<string, long>();
                convertedIds.Add(APIConstants.CONTACTS, Convert.ToInt64(convertedIdsJSON[APIConstants.CONTACTS]));
                if (convertedIdsJSON[APIConstants.ACCOUNTS].Type != JTokenType.Null)
                {
                    convertedIds.Add(APIConstants.ACCOUNTS, Convert.ToInt64(convertedIdsJSON[APIConstants.ACCOUNTS]));
                }
                if (convertedIdsJSON[APIConstants.DEALS].Type != JTokenType.Null)
                {
                    convertedIds.Add(APIConstants.DEALS, Convert.ToInt64(convertedIdsJSON[APIConstants.DEALS]));
                }
                return convertedIds;
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse UploadPhoto(string filePath)
        {
            try
            {
                CommonUtil.CheckIsPhotoSupported(record.ModuleAPIName);
                CommonUtil.ValidateFile(filePath);

                requestMethod = APIConstants.RequestMethod.POST;
                urlPath = record.ModuleAPIName + "/" + record.EntityId + "/photo";

                FileInfo fileInfo = new FileInfo(filePath);
                return APIRequest.GetInstance(this).UploadFile(fileInfo.OpenRead(), fileInfo.Name);
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public FileAPIResponse DownloadPhoto()
        {
            try
            {
                CommonUtil.CheckIsPhotoSupported(record.ModuleAPIName);

                requestMethod = APIConstants.RequestMethod.GET;
                urlPath = record.ModuleAPIName + "/" + record.EntityId + "/photo";

                return APIRequest.GetInstance(this).DownloadFile();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }

        public APIResponse DeletePhoto()
        {
            try
            {
                CommonUtil.CheckIsPhotoSupported(record.ModuleAPIName);

                requestMethod = APIConstants.RequestMethod.DELETE;
                urlPath = record.ModuleAPIName + "/" + record.EntityId + "/photo";

                return APIRequest.GetInstance(this).GetAPIResponse();
            }
            catch (Exception e) when (!(e is ZCRMException))
            {
                ZCRMLogger.LogError(e);
                throw new ZCRMException(APIConstants.SDK_ERROR, e);
            }
        }



        public void SetRecordProperties(JObject recordDetails)
        {
            SetRecordProperties(recordDetails, record);
        }


        public void SetRecordProperties(JObject recordJSON, ZCRMRecord record)
        {
            foreach (KeyValuePair<string, JToken> token in recordJSON)
            {
                string fieldAPIName = token.Key;
                if (fieldAPIName.Equals("id"))
                {
                    record.EntityId = Convert.ToInt64(token.Value);
                }
                else if (fieldAPIName.Equals("Product_Details") && token.Value.Type != JTokenType.Null)
                {
                    SetInventoryLineItems(token.Value);
                }
                else if (fieldAPIName.Equals("Participants") && token.Value.Type != JTokenType.Null)
                {
                    SetParticipants(token.Value);
                }
                else if (fieldAPIName.Equals("Pricing_Details") && token.Value.Type != JTokenType.Null)
                {
                    SetPriceDetails((JArray)token.Value);
                }
                else if (fieldAPIName.Equals("Created_By") && token.Value.Type != JTokenType.Null)
                {
                    JObject createdObject = (JObject)token.Value;
                    ZCRMUser createdUser = ZCRMUser.GetInstance(Convert.ToInt64(createdObject["id"]), (string)createdObject["name"]);
                    record.CreatedBy = createdUser;
                }
                else if (fieldAPIName.Equals("Modified_By") && token.Value.Type != JTokenType.Null)
                {
                    JObject modifiedObject = (JObject)token.Value;
                    ZCRMUser modifiedBy = ZCRMUser.GetInstance(Convert.ToInt64(modifiedObject["id"]), (string)modifiedObject["name"]);
                    record.ModifiedBy = modifiedBy;
                }
                else if (fieldAPIName.Equals("Created_Time"))
                {
                    record.CreatedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(token.Value));
                }
                else if (fieldAPIName.Equals("Modified_Time"))
                {
                    record.ModifiedTime = CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(token.Value));
                }
                else if (fieldAPIName.Equals("Owner") && token.Value.Type != JTokenType.Null)
                {
                    JObject ownerObject = (JObject)token.Value;
                    ZCRMUser ownerUser = ZCRMUser.GetInstance(Convert.ToInt64(ownerObject["id"]), (string)ownerObject["name"]);
                    record.Owner = ownerUser;
                }
                else if (fieldAPIName.Equals("Layout") && token.Value.Type != JTokenType.Null)
                {
                    JObject layoutObject = (JObject)token.Value;
                    ZCRMLayout layout = ZCRMLayout.GetInstance(Convert.ToInt64(layoutObject["id"]));
                    layout.Name = (string)layoutObject["name"];
                }
                else if (fieldAPIName.Equals("Handler") && token.Value.Type != JTokenType.Null)
                {
                    JObject handlerObject = (JObject)token.Value;
                    ZCRMUser handler = ZCRMUser.GetInstance(Convert.ToInt64(handlerObject["id"]), (string)handlerObject["name"]);
                    record.SetFieldValue(fieldAPIName, handler);
                }

                else if (fieldAPIName.Equals("Remind_At") && token.Value.Type != JTokenType.Null)
                {
                    if (token.Value is JObject)
                    {
                        JObject remindObject = (JObject)token.Value;
                        record.SetFieldValue(fieldAPIName, remindObject["ALARM"]);
                    }
                    else
                    {
                        record.SetFieldValue(fieldAPIName, token.Value);
                    }
                }
                else if (fieldAPIName.Equals("Recurring_Activity") && token.Value.Type != JTokenType.Null)
                {
                    JObject recurringActivityObject = (JObject)token.Value;
                    record.SetFieldValue(fieldAPIName, recurringActivityObject["RRULE"]);
                }
                else if (fieldAPIName.Equals("$line_tax") && token.Value.Type != JTokenType.Null)
                {
                    JArray taxDetails = (JArray)token.Value;
                    foreach (JObject taxDetail in taxDetails)
                    {
                        ZCRMTax tax = ZCRMTax.GetInstance((string)taxDetail["name"]);
                        tax.Percentage = Convert.ToDouble(taxDetail["percentage"]);
                        tax.Value = Convert.ToDouble(taxDetail["value"]);
                        record.AddTax(tax);
                    }
                }
                else if (fieldAPIName.Equals("Tax") && token.Value.Type != JTokenType.Null)
                {
                    var taxNames = token.Value;
                    foreach (string data in taxNames)
                    {
                        ZCRMTax tax = ZCRMTax.GetInstance(data);
                        record.AddTax(tax);
                    }
                }
                else if (fieldAPIName.Equals("tags") && token.Value.Type != JTokenType.Null)
                {
                    JArray jsonArray = (JArray)token.Value;
                    List<string> tags = new List<string>();
                    foreach (string tag in jsonArray)
                    {
                        tags.Add(tag);
                    }
                    record.TagNames = tags;
                }
                else if (fieldAPIName.Equals("Tag") && token.Value.Type != JTokenType.Null)
                {
                    JArray jsonArray = (JArray)token.Value;
                    foreach (JObject tag in jsonArray)
                    {
                        ZCRMTag tagIns = ZCRMTag.GetInstance(Convert.ToInt64(tag.GetValue("id")));
                        tagIns.Name = tag.GetValue("name").ToString();
                        record.Tags.Add(tagIns);

                    }
                }
                else if (fieldAPIName.StartsWith("$", StringComparison.CurrentCulture))
                {
                    fieldAPIName = fieldAPIName.TrimStart('\\', '$');
                    if (APIConstants.PROPERTIES_AS_FILEDS.Contains(fieldAPIName))
                    {
                        record.SetFieldValue(fieldAPIName, token.Value);
                    }
                    else
                    {
                        record.SetProperty(fieldAPIName, token.Value);
                    }
                }
                else if (token.Value is JObject)
                {
                    JObject lookupDetails = (JObject)token.Value;
                    ZCRMRecord lookupRecord = ZCRMRecord.GetInstance(fieldAPIName, Convert.ToInt64(lookupDetails["id"]));
                    lookupRecord.LookupLabel = (string)lookupDetails["name"];
                    record.SetFieldValue(fieldAPIName, lookupRecord);
                }
                else if (token.Value is JArray)
                {
                    JArray jsonArray = (JArray)token.Value;

                    List<ZCRMFiles> fileValues = new List<ZCRMFiles>();

                    List<object> values = new List<object>();

                    foreach (Object obj in jsonArray)
                    {
                        if (obj is JObject)
                        {
                            JObject value = (JObject)obj;

                            if (value.ContainsKey("file_Id") && value.ContainsKey("attachment_Id"))
                            {
                                fileValues.Add(this.GetZCRMFileObject(value));
                            }
                            else
                            {
                                values.Add(values);
                            }
                        }
                        else
                        {
                            values.Add(obj);
                        }
                    }
                    if(fileValues.Count > 0)
                    {
                        record.SetFieldValue(fieldAPIName, fileValues);
                    }
                    else
                    {
                        record.SetFieldValue(fieldAPIName, values);
                    }
                   
                }
                else
                {
                    if (token.Value.Type.ToString().Equals("Date"))
                    {
                        record.SetFieldValue(fieldAPIName, CommonUtil.RemoveEscaping((string)JsonConvert.SerializeObject(token.Value)));
                    }
                    else
                    {
                        record.SetFieldValue(fieldAPIName, token.Value);
                    }
                }
            }
        }

        private ZCRMFiles GetZCRMFileObject(JObject file)
        {
            ZCRMFiles files = ZCRMFiles.GetInstance(null,null);

            if (file.ContainsKey("extn") && file["extn"].Type != JTokenType.Null)
            {
                files.Extn = file["extn"].ToString();
            }
            if (file.ContainsKey("is_Preview_Available") && file["is_Preview_Available"].Type != JTokenType.Null)
            {
                files.IsPreviewAvailable = (bool)file["is_Preview_Available"];
            }
            if (file.ContainsKey("download_Url") && file["download_Url"].Type != JTokenType.Null)
            {
                files.DownloadUrl = file["download_Url"].ToString();
            }
            if (file.ContainsKey("delete_Url") && file["delete_Url"].Type != JTokenType.Null)
            {
                files.DeleteUrl = file["delete_Url"].ToString();
            }
            if (file.ContainsKey("entity_Id") && file["entity_Id"].Type != JTokenType.Null)
            {
                files.EntityId = Convert.ToInt64(file["entity_Id"]);
            }
            if (file.ContainsKey("mode") && file["mode"].Type != JTokenType.Null)
            {
                files.Mode = file["mode"].ToString();
            }
            if (file.ContainsKey("original_Size_Byte") && file["original_Size_Byte"].Type != JTokenType.Null)
            {
                files.OriginalSizeByte = Convert.ToInt64(file["original_Size_Byte"]);
            }
            if (file.ContainsKey("preview_Url") && file["preview_Url"].Type != JTokenType.Null)
            {
                files.PreviewUrl = file["preview_Url"].ToString();
            }
            if (file.ContainsKey("file_Name") && file["file_Name"].Type != JTokenType.Null)
            {
                files.FileName = file["file_Name"].ToString();
            }
            if (file.ContainsKey("file_Id") && file["file_Id"].Type != JTokenType.Null)
            {
                files.FileId = file["file_Id"].ToString();
            }
            if (file.ContainsKey("attachment_Id") && file["attachment_Id"].Type != JTokenType.Null)
            {
                files.AttachmentId = file["attachment_Id"].ToString();
            }
            if (file.ContainsKey("file_Size") && file["file_Size"].Type != JTokenType.Null)
            {
                files.FileSize = file["file_Size"].ToString();
            }
            if (file.ContainsKey("creator_Id") && file["creator_Id"].Type != JTokenType.Null)
            {
                files.CreatorId = Convert.ToInt64(file["creator_Id"]);
            }
            if (file.ContainsKey("link_Docs") && file["link_Docs"].Type != JTokenType.Null)
            {
                files.LinkDocs = (int)file["link_Docs"];
            }

            return files;
        }

        private void SetParticipants(JToken participants)
        {
            foreach (JObject participantDetails in participants)
            {
                record.AddParticipant(GetZCRMParticipant(participantDetails));
            }
        }


        private void SetInventoryLineItems(JToken lineItems)
        {
            foreach (JObject lineItem in lineItems)
            {
                record.AddLineItem(GetZCRMInventoryLineItem(lineItem));
            }
        }

        private void SetPriceDetails(JArray priceDetails)
        {
            foreach (JObject priceDetail in priceDetails)
            {
                record.AddPriceDetail(GetZCRMPriceDetail(priceDetail));
            }
        }


        public ZCRMInventoryLineItem GetZCRMInventoryLineItem(JObject lineItemJSON)
        {
            JObject productDetails = (JObject)lineItemJSON["product"];
            long lineItemId = Convert.ToInt64(lineItemJSON["id"]);
            ZCRMInventoryLineItem lineItem = ZCRMInventoryLineItem.GetInstance(lineItemId);

            ZCRMRecord product = ZCRMRecord.GetInstance("Products", Convert.ToInt64(productDetails["id"]));
            product.LookupLabel = (string)productDetails["name"];
            if(productDetails.ContainsKey("Product_Code") && productDetails["Product_Code"].Type != JTokenType.Null)
            {
                product.SetFieldValue("Product_Code", (string)productDetails["name"]);
            }
            lineItem.Product = product;
            lineItem.Quantity = Convert.ToDouble(lineItemJSON["quantity"]);
            lineItem.Discount = Convert.ToDouble(lineItemJSON["Discount"]);
            lineItem.TotalAfterDiscount = Convert.ToDouble(lineItemJSON["total_after_discount"]);
            lineItem.NetTotal = Convert.ToDouble(lineItemJSON["net_total"]);
            lineItem.TaxAmount = Convert.ToDouble(lineItemJSON["Tax"]);
            lineItem.ListPrice = Convert.ToDouble(lineItemJSON["list_price"]);
            if(lineItemJSON.ContainsKey("unit_price") && lineItemJSON["unit_price"].Type != JTokenType.Null)
            {
                lineItem.UnitPrice = Convert.ToDouble(lineItemJSON["unit_price"]);
            }
            lineItem.QuantityInStock = Convert.ToInt32(lineItemJSON["quantity_in_stock"]);
            lineItem.Total = Convert.ToDouble(lineItemJSON["total"]);
            lineItem.Description = (string)lineItemJSON["product_description"];
            JArray lineTaxes = (JArray)lineItemJSON["line_tax"];
            foreach (JObject lineTax in lineTaxes)
            {
                ZCRMTax tax = ZCRMTax.GetInstance((string)lineTax["name"]);
                tax.Percentage = Convert.ToDouble(lineTax["percentage"]);
                tax.Value = Convert.ToDouble(lineTax["value"]);
                lineItem.AddLineTax(tax);
            }
            return lineItem;
        }

        private ZCRMEventParticipant GetZCRMParticipant(JObject participantDetails)
        {
            long Id = Convert.ToInt64(participantDetails["id"]);
            string type = (string)participantDetails["type"];
            ZCRMEventParticipant participant = ZCRMEventParticipant.GetInstance(type, Id);
            participant.Name = (string)participantDetails["name"];
            participant.Email = (string)participantDetails["Email"];
            participant.IsInvited = (bool)participantDetails["invited"];
            participant.Status = (string)participantDetails["status"];
            participant.Participant = (string)participantDetails["participant"];
            return participant;
        }

        private ZCRMPriceBookPricing GetZCRMPriceDetail(JObject priceDetail)
        {
            long id = Convert.ToInt64(priceDetail["id"]);
            ZCRMPriceBookPricing pricing = ZCRMPriceBookPricing.GetInstance(id);
            pricing.Discount = Convert.ToDouble(priceDetail["discount"]);
            pricing.ToRange = Convert.ToDouble(priceDetail["to_range"]);
            pricing.FromRange = Convert.ToDouble(priceDetail["from_range"]);

            return pricing;
        }


        public JObject GetZCRMRecordAsJSON()
        {
            JObject recordJSON = new JObject();

            Dictionary<string, object> recordData = record.Data;

            if (record.Owner != null)
            {
                recordJSON.Add("Owner", record.Owner.Id);
            }

            if (record.Layout != null)
            {
                recordJSON.Add("Layout", record.Layout.Id);
            }

            MapAsJSON(recordData, recordJSON);

            if (record.LineItems != null)
            {
                recordJSON.Add("Product_Details", GetLineItemsAsJSONArray(record.LineItems));
            }
                
            if (record.Participants != null)
            {
                recordJSON.Add("Participants", GetParticipantsAsJSONArray(record.Participants));
            }

            if (record.PriceDetails != null)
            {
                recordJSON.Add("Pricing_Details", GetPriceDetailsAsJSONArray(record.PriceDetails));
            }

            if (record.TaxList != null && record.ModuleAPIName.Equals("Products"))
            {
                recordJSON.Add("Tax", GetTaxAsJSONArray(record.TaxList));
            }

            if (record.TaxList != null)
            {
                recordJSON.Add("$line_tax", GetTaxAsJSONArray(record.TaxList));
            }

            return recordJSON;
        }

        private void MapAsJSON(Dictionary<string, object> recordData, JObject recordJSON)
        {
            foreach (KeyValuePair<string, object> data in recordData)
            {
                recordJSON.Add(data.Key, JToken.FromObject(ObjectToJSONData(data.Value)));
            }
        }

        public object ObjectToJSONData(object value)
        {
            if (value == null)
            {
                value = null;
            }
            else if (value is ZCRMRecord)
            {
                value = ((ZCRMRecord)value).EntityId;
            }
            else if (value is ZCRMUser)
            {
                value = ((ZCRMUser)value).Id;
            }
            else if (value is List<ZCRMFiles>)
            {
                JArray jsonArray = new JArray();

                List<ZCRMFiles> files = (List<ZCRMFiles>)value;

                foreach (ZCRMFiles valueObject in files)
                {
                    jsonArray.Add(GetFilesAsJSON(valueObject));
                }

                value = jsonArray;
            }
            else if(value is List<ZCRMInventoryLineItem>)
            {
                value = GetLineItemsAsJSONArray((List<ZCRMInventoryLineItem>)value);
            }
            else if(value is List<ZCRMEventParticipant>)
            {
                value = GetParticipantsAsJSONArray((List<ZCRMEventParticipant>)value);
            }
            else if(value is List<ZCRMPriceBookPricing>)
            {
                value = GetPriceDetailsAsJSONArray((List<ZCRMPriceBookPricing>)value);
            }
            else if(value is List<ZCRMTax>)
            {
                value = GetTaxAsJSONArray((List<ZCRMTax>)value);
            }
            else if (value is List<object>)
            {
                JArray jsonArray = new JArray();

                foreach (object valueObject in (List<object>)value)
                {
                    if (valueObject is ZCRMSubform)
                    {
                        jsonArray.Add(GetSubformAsJSON((ZCRMSubform)valueObject));
                    }
                    else
                    {
                        jsonArray.Add(valueObject);
                    }
                }

                value = jsonArray;
            }

            return value;
        }

        private JObject GetFilesAsJSON(ZCRMFiles files)
        {
            JObject fileJSON = new JObject();

            if(!string.IsNullOrEmpty(files.FileId) && !string.IsNullOrWhiteSpace(files.FileId))
            {
                fileJSON.Add("file_id", files.FileId);
            }
            if (!string.IsNullOrEmpty(files.AttachmentId) && !string.IsNullOrWhiteSpace(files.AttachmentId))
            {
                fileJSON.Add("attachment_id", files.AttachmentId);
            }
            if(files.Delete)
            {
                fileJSON.Add("_delete", null);
            }

            return fileJSON;
        }

        private JObject GetSubformAsJSON(ZCRMSubform subform)
        {
            JObject subformJSON = new JObject();

            Dictionary<string, object> subformData = subform.Data;
            if (subform.EntityId != null)
            {
                subformJSON.Add("id", subform.EntityId.ToString());
            }
            if (subform.Owner != null)
            {
                subformJSON.Add("Owner", subform.Owner.Id.ToString());
            }
            if (subform.Layout != null)
            {
                subformJSON.Add("Layout", subform.Layout.Id.ToString());
            }

            MapAsJSON(subformData, subformJSON);

            return subformJSON;
        }

        private JArray GetLineItemsAsJSONArray(List<ZCRMInventoryLineItem> lineItemsList )
        {
            if (lineItemsList.Count == 0)
            {
                return null;
            }
            JArray lineItems = new JArray();
            foreach (ZCRMInventoryLineItem inventoryLineItem in lineItemsList)
            {
                lineItems.Add(GetZCRMInventoryLineItemAsJSON(inventoryLineItem));
            }
            return lineItems;
        }

        private JObject GetZCRMInventoryLineItemAsJSON(ZCRMInventoryLineItem inventoryLineItem)
        {
            JObject lineItem = new JObject();
            if (inventoryLineItem.Id != null)
            {
                lineItem.Add("id", inventoryLineItem.Id.ToString());
            }
            if (inventoryLineItem.Product != null)
            {
                lineItem.Add("product", inventoryLineItem.Product.EntityId);
            }

            lineItem.Add("product_description", inventoryLineItem.Description);
            lineItem.Add("list_price", inventoryLineItem.ListPrice);
            lineItem.Add("quantity", inventoryLineItem.Quantity);

            if (inventoryLineItem.DiscountPercentage == null)
            {
                lineItem.Add("Discount", inventoryLineItem.Discount);
            }
            else
            {
                lineItem.Add("Discount", inventoryLineItem.DiscountPercentage + "%");
            }

            JArray lineTaxArray = new JArray();
            List<ZCRMTax> taxes = inventoryLineItem.LineTax;
            foreach (ZCRMTax tax in taxes)
            {
                JObject lineTax = new JObject();
                lineTax.Add("name", tax.TaxName);
                lineTax.Add("value", tax.Value);
                lineTax.Add("percentage", tax.Percentage);
                lineTaxArray.Add(lineTax);
            }
            lineItem.Add("line_tax", lineTaxArray);

            return lineItem;
        }

        private JArray GetParticipantsAsJSONArray(List<ZCRMEventParticipant> participantsList)
        {
            if (participantsList.Count == 0)
            {
                return null;
            }
            JArray participants = new JArray();
            
            foreach (ZCRMEventParticipant participant in participantsList)
            {
                participants.Add(GetZCRMParticipantsAsJSON(participant));
            }
            return participants;
        }

        private JObject GetZCRMParticipantsAsJSON(ZCRMEventParticipant participant)
        {
            JObject participantJSON = new JObject
            {
                { "participant", participant.Id == 0?participant.Participant:participant.Id.ToString()},
                { "type", participant.Type },
                { "name", participant.Name },
                { "Email", participant.Email },
                { "invited", participant.IsInvited },
                { "status", participant.Status }
            };

            return participantJSON;
        }

        private JArray GetPriceDetailsAsJSONArray(List<ZCRMPriceBookPricing> priceDetailsList)
        {
            if (priceDetailsList.Count == 0) { return null; }

            JArray priceDetails = new JArray();

            foreach (ZCRMPriceBookPricing priceDetail in priceDetailsList)
            {
                priceDetails.Add(GetZCRMPriceDetailAsJSON(priceDetail));
            }
            return priceDetails;

        }

        private JObject GetZCRMPriceDetailAsJSON(ZCRMPriceBookPricing priceDetail)
        {
            JObject priceDetailJSON = new JObject();
            if (priceDetail.Id != null)
            {
                priceDetailJSON.Add("id", priceDetail.Id.ToString());
            }
            priceDetailJSON.Add("discount", priceDetail.Discount);
            priceDetailJSON.Add("to_range", priceDetail.ToRange);
            priceDetailJSON.Add("from_range", priceDetail.FromRange);

            return priceDetailJSON;
        }

        private JArray GetTaxAsJSONArray(List<ZCRMTax> taxList)
        {
            if (taxList.Count == 0)
            {
                return null;
            }

            JArray taxes = new JArray();

            if (record != null && record.ModuleAPIName.Equals("Products"))
            {
                foreach (ZCRMTax tax in taxList)
                {
                    taxes.Add(tax.TaxName);
                }
            }
            else
            {
                foreach (ZCRMTax tax in taxList)
                {
                    JObject taxObject = new JObject
                    {
                        { "percentage", tax.Percentage },
                        { "name", tax.TaxName }
                    };

                    taxes.Add(taxObject);
                }
            }

            return taxes;
        }
    }
}
