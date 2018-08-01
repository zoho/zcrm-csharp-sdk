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

        public static ZCRMField GetInstance(string fieldAPIName)
        {
            return new ZCRMField(fieldAPIName);
        }

        public string ApiName { get => apiName; set => apiName = value; }

        public long Id { get => id; set => id = value; }

        public string DisplayName { get => displayName; set => displayName = value; }

        public string DataType { get => dataType; set => dataType = value; }

        public bool ReadOnly { get => readOnly; set => readOnly = value; }

        public bool Visible { get => visible; set => visible = value; }

        public bool? Mandatory { get => mandatory; set => mandatory = value; }

        public bool CustomField { get => customField; set => customField = value; }

        public object DefaultValue { get => defaultValue; set => defaultValue = value; }

        public int? SequenceNo { get => sequenceNo; set => sequenceNo = value; }

        public int? MaxLength { get => maxLength; set => maxLength = value; }

        public int? Precision { get => precision; set => precision = value; }

        public List<string> SubLayoutsPresent { private get => subLayoutsPresent; set => subLayoutsPresent = value; }

        public string FormulaReturnType { get => formulaReturnType; set => formulaReturnType = value; }

        public bool Webhook { get => webhook; set => webhook = value; }

        public string ToolTip { get => toolTip; set => toolTip = value; }

        public string CreatedSource { get => createdSource; set => createdSource = value; }

        public long SubFormTabId { get => subFormTabId; set => subFormTabId = value; }

        public Dictionary<string, object> SubformDetails { get => subformDetails; private set => subformDetails = value; }

        public Dictionary<string, object> LookupDetails { get => lookupDetails; private  set => lookupDetails = value; }

        public Dictionary<string, object> MultiselectLookup { get => multiselectLookup; private set => multiselectLookup = value; }

        public List<ZCRMPickListValue> PickListValues { get => pickListValues; private set => pickListValues = value; }

        public void AddPickListValue(ZCRMPickListValue value)
        {
            PickListValues.Add(value);
        }

        public bool IsPresentInCreateLayout()
        {
            return SubLayoutsPresent.Contains("CREATE");
        }

        public bool IsPresentInViewLayout()
        {
            return SubLayoutsPresent.Contains("VIEW");
        }

        public bool IsPresentInQuickCreateLayout()
        {
            return SubLayoutsPresent.Contains("QUICK_CREATE");
        }

        public void SetSubformDetails(string fieldName, object value)
        {
            SubformDetails.Add(fieldName,value);
        }

        public void SetMultiselectLookup(string fieldName, object value)
        {
            MultiselectLookup.Add(fieldName, value);
        }

        public void SetLookupDetails(string fieldName, object value)
        {
            LookupDetails.Add(fieldName, value);
        }

    }
}
