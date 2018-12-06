using System.Collections.Generic;


namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMSection
    {

        private string name;
        private string displayName;
        private int columnCount;
        private int sequence;
        private List<ZCRMField> fields = new List<ZCRMField>();

        private ZCRMSection(string name)
        {
            Name = name;
        }

        public static ZCRMSection GetInstance(string sectionName)
        {
            return new ZCRMSection(sectionName);
        }

        public string Name { get => name; private set => name = value; }

        public string DisplayName { get => displayName; set => displayName = value; }

        public int ColumnCount { get => columnCount; set => columnCount = value; }

        public int Sequence { get => sequence; set => sequence = value; }

        public List<ZCRMField> Fields { get => fields; set => fields = value; }

        public void AddField(ZCRMField field)
        {
            Fields.Add(field);
        }
    }
}
