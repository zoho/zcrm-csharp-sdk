using ZCRMSDK.CRM.Library.Common;


namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMOrgTax : ZCRMEntity
    {
        private long? id;
        private string name;
        private string displayName;
        private double value;
        private int sequence;

        private ZCRMOrgTax(string taxName, long? id)
        {
            Name = taxName;
            Id = id;
        }

        public ZCRMOrgTax(string taxName) : this(taxName, null)
        {
            
        }


        public static ZCRMOrgTax GetInstance(long id)
        {
            return new ZCRMOrgTax(null, id);
        }

        public long? Id { get => id; set => id = value; }

        public string Name { get => name; set => name = value; }

        public string DisplayName { get => displayName; set => displayName = value; }

        public double Value { get => value; set => this.value = value; }

        public int Sequence { get => sequence; set => sequence = value; }
    }
}
