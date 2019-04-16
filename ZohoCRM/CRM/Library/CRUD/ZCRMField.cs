using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;


namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMField : ZCRMEntity
    {
        private long id;
        private string apiName;
        private string displayName;
        private string dataType;
        private bool readOnly;
        private bool visible;
        private bool? mandatory;
        private bool customField;
        private bool webhook;
        private object defaultValue;
        private string jsonType;
        private int? maxLength;
        private int? precision;
        private int? sequenceNo;
        private string toolTip;
        private string createdSource;
        private List<string> subLayoutsPresent = new List<string>();
        private List<ZCRMPickListValue> pickListValues = new List<ZCRMPickListValue>();
        private string formulaReturnType;
        private long subFormTabId;
        private Dictionary<string, object> subformDetails = new Dictionary<string, object>();
        private Dictionary<string, object> lookupDetails = new Dictionary<string, object>();
        private Dictionary<string, object> multiselectLookup = new Dictionary<string, object>();

        private ZCRMField(string fieldAPIName)
        {
            ApiName = fieldAPIName;
        }

        /// <summary>
        /// To get ZCRMField instance by using field API name.
        /// </summary>
        /// <returns>ZCRMField class instance.</returns>
        /// <param name="fieldAPIName">APIName (String) of the fields.</param>
        public static ZCRMField GetInstance(string fieldAPIName)
        {
            return new ZCRMField(fieldAPIName);
        }

        /// <summary>
        /// Gets or sets the APIName of/for the fields.
        /// </summary>
        /// <value>The APIName of the fields.</value>
        /// <returns>String</returns>
        public string ApiName
        {
            get
            {
                return apiName;
            }
            set
            {
                apiName = value;
            }
        }

        /// <summary>
        /// Gets or sets the fields' Id.
        /// </summary>
        /// <value>The Id of the fields.</value>
        /// <returns>Long</returns>
        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the json type of/for the fields.
        /// </summary>
        /// <value>The json type of the fields.</value>
        /// <returns>String</returns>
        public string JsonType
        {
            get
            {
                return jsonType;
            }
            set
            {
                jsonType = value;
            }
        }

        /// <summary>
        /// Gets or sets the display name of/for the fields.
        /// </summary>
        /// <value>The display name of the fields.</value>
        /// <returns>String</returns>
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        /// <summary>
        /// Gets or sets the data type of/for the fields.
        /// </summary>
        /// <value>The data type of the fields.</value>
        /// <returns>String</returns>
        public string DataType
        {
            get
            {
                return dataType;
            }
            set
            {
                dataType = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this field is read only.
        /// </summary>
        /// <value><c>true</c> if read only; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this field is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        /// <summary>
        /// Gets or sets the mandatory fields.
        /// </summary>
        /// <value>The mandatory fields.</value>
        /// <returns>Boolean</returns>
        public bool? Mandatory
        {
            get
            {
                return mandatory;
            }
            set
            {
                mandatory = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this field is custom field.
        /// </summary>
        /// <value><c>true</c> if custom field; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool CustomField
        {
            get
            {
                return customField;
            }
            set
            {
                customField = value;
            }
        }

        /// <summary>
        /// Gets or sets the field default value.
        /// </summary>
        /// <value>The default value of the field.</value>
        /// <returns>Object</returns>
        public object DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the sequence no of/for the fields.
        /// </summary>
        /// <value>The sequence no of the fields.</value>
        /// <returns>Integer</returns>
        public int? SequenceNo
        {
            get
            {
                return sequenceNo;
            }
            set
            {
                sequenceNo = value;
            }
        }

        /// <summary>
        /// Gets or sets the max length of/for the fields.
        /// </summary>
        /// <value>The max length of the fields.</value>
        /// <returns>Integer</returns>
        public int? MaxLength
        {
            get
            {
                return maxLength;
            }
            set
            {
                maxLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the precision of/for the fields.
        /// </summary>
        /// <value>The precision of the fields.</value>
        /// <returns>Integer</returns>
        public int? Precision
        {
            get
            {
                return precision;
            }
            set
            {
                precision = value;
            }
        }

        /// <summary>
        /// Sets the sub layouts of the field.
        /// </summary>
        /// <value>The sub layouts present in the field.</value>
        /// <returns>List of string</returns>
        public List<string> SubLayoutsPresent
        {
            private get
            {
                return subLayoutsPresent;
            }
            set
            {
                subLayoutsPresent = value;
            }
        }

        /// <summary>
        /// Gets or sets the formula return type of/for the fields.
        /// </summary>
        /// <value>The formula return type of the fields.</value>
        /// <returns>String</returns>
        public string FormulaReturnType
        {
            get
            {
                return formulaReturnType;
            }
            set
            {
                formulaReturnType = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this field is webhook.
        /// </summary>
        /// <value><c>true</c> if webhook; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool Webhook
        {
            get
            {
                return webhook;
            }
            set
            {
                webhook = value;
            }
        }

        /// <summary>
        /// Gets or sets the tool tip of/for the fields.
        /// </summary>
        /// <value>The tool tip of the fields.</value>
        /// <returns>String</returns>
        public string ToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                toolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets the created source of/for the fields.
        /// </summary>
        /// <value>The created source of the fields.</value>
        /// <returns>String</returns>
        public string CreatedSource
        {
            get
            {
                return createdSource;
            }
            set
            {
                createdSource = value;
            }
        }

        /// <summary>
        /// Gets or sets the sub form tab Id of/for the field.
        /// </summary>
        /// <value>The sub form tab Id of the field.</value>
        /// <returns>Long</returns>
        public long SubFormTabId
        {
            get
            {
                return subFormTabId;
            }
            set
            {
                subFormTabId = value;
            }
        }

        /// <summary>
        /// Gets the subform details of the fields.
        /// </summary>
        /// <value>The subform details of the fields.</value>
        /// <returns>Dictionary(String,Object)</returns>
        public Dictionary<string, object> SubformDetails
        {
            get
            {
                return subformDetails;
            }
            private set
            {
                subformDetails = value;
            }
        }

        /// <summary>
        /// Gets the lookup details of the fields.
        /// </summary>
        /// <value>The lookup details of the fields.</value>
        /// <returns>Dictionary(String,Object)</returns>
        public Dictionary<string, object> LookupDetails
        {
            get
            {
                return lookupDetails;
            }
            private set
            {
                lookupDetails = value;
            }
        }

        /// <summary>
        /// Gets the multiselect lookup of the fields.
        /// </summary>
        /// <value>The multiselect lookup of the fields.</value>
        /// <returns>Dictionary(String,Object)</returns>
        public Dictionary<string, object> MultiselectLookup
        {
            get
            {
                return multiselectLookup;
            }
            private set
            {
                multiselectLookup = value;
            }
        }

        /// <summary>
        /// Gets the pick list values of the fields.
        /// </summary>
        /// <value>The pick list values of the fields.</value>
        /// <returns>List of ZCRMPickListValue class instance</returns>
        public List<ZCRMPickListValue> PickListValues
        {
            get
            {
                return pickListValues;
            }
            private set
            {
                pickListValues = value;
            }
        }

        /// <summary>
        /// To add the pick list value based on ZCRMPickListValue class instance.
        /// </summary>
        /// <param name="value">ZCRMPickListValue class instance</param>
        public void AddPickListValue(ZCRMPickListValue value)
        {
            PickListValues.Add(value);
        }

        /// <summary>
        /// To get present in create layout of the field.
        /// </summary>
        /// <returns><c>true</c>, if present in create layout, <c>false</c> otherwise.</returns>
        public bool IsPresentInCreateLayout()
        {
            return SubLayoutsPresent.Contains("CREATE");
        }

        /// <summary>
        /// To get present in view layout of the field.
        /// </summary>
        /// <returns><c>true</c>, if present in view layout, <c>false</c> otherwise.</returns>
        public bool IsPresentInViewLayout()
        {
            return SubLayoutsPresent.Contains("VIEW");
        }

        /// <summary>
        /// To get present in quick create layout of the field.
        /// </summary>
        /// <returns><c>true</c>, if present in quick create layout, <c>false</c> otherwise.</returns>
        public bool IsPresentInQuickCreateLayout()
        {
            return SubLayoutsPresent.Contains("QUICK_CREATE");
        }

        /// <summary>
        /// To set the subform details based on field APIName and value.
        /// </summary>
        /// <param name="fieldName">Name (String) of the field</param>
        /// <param name="value">value (Object) of the field</param>
        public void SetSubformDetails(string fieldName, object value)
        {
            SubformDetails.Add(fieldName,value);
        }

        /// <summary>
        /// To sets the multiselect lookup based on field APIName and value.
        /// </summary>
        /// <param name="fieldName">Name (String) of the field</param>
        /// <param name="value">value (Object) of the field</param>
        public void SetMultiselectLookup(string fieldName, object value)
        {
            MultiselectLookup.Add(fieldName, value);
        }

        /// <summary>
        /// To set the lookup details of the field.
        /// </summary>
        /// <param name="fieldName">Name (String) of the field</param>
        /// <param name="value">value (Object) of the field</param>
        public void SetLookupDetails(string fieldName, object value)
        {
            LookupDetails.Add(fieldName, value);
        }

    }
}
