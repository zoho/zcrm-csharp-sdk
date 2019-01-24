
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

        /// <summary>
        /// To get ZCRMTax instance by passing tax name.
        /// </summary>
        /// <returns>The ZCRMTax class instance.</returns>
        /// <param name="taxName">Name (String) of the tax.</param>
        public static ZCRMTax GetInstance(string taxName)
        {
            return new ZCRMTax(taxName);
        }

        /// <summary>
        /// Gets or sets the name of/for the tax.
        /// </summary>
        /// <value>The name of the tax.</value>
        /// <returns>String</returns>
        public string TaxName { get => taxName; set => taxName = value; }

        /// <summary>
        /// Gets or sets the percentage of/for the tax.
        /// </summary>
        /// <value>The percentage of the tax.</value>
        /// <returns>Double</returns>
        public double Percentage { get => percentage; set => percentage = value; }

        /// <summary>
        /// Gets or sets the value of/for the tax.
        /// </summary>
        /// <value>The value of the tax.</value>
        /// <returns>Double</returns>
        public double Value { get => value; set => this.value = value; }
    }
}
