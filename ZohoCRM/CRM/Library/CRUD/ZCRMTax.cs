
namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMTax
    {
        private string taxName;
        private double percentage;
        private double value;


        private ZCRMTax(string taxName)
        {
            TaxName = taxName;
        }

        public static ZCRMTax GetInstance(string taxName)
        {
            return new ZCRMTax(taxName);
        }

        public string TaxName { get => taxName; set => taxName = value; }

        public double Percentage { get => percentage; set => percentage = value; }

        public double Value { get => value; set => this.value = value; }
    }
}
