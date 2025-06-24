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

        /// <summary>
        /// To get ZCRMOrgTax instance by passing tax name.
        /// </summary>
        /// <param name="taxName">Name (String) of the tax.</param>
        public ZCRMOrgTax(string taxName) : this(taxName, null){}

        /// <summary>
        /// To get ZCRMOrgTax instance by passing tax Id.
        /// </summary>
        /// <returns>ZCRMOrgTax class instance.</returns>
        /// <param name="id">Id (Long) of the tax.</param>
        public static ZCRMOrgTax GetInstance(long id)
        {
            return new ZCRMOrgTax(null, id);
        }

        /// <summary>
        /// Gets or sets the tax Id.
        /// </summary>
        /// <value>The Id of the tax.</value>
        /// <returns>Long</returns>
        public long? Id
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
        /// Gets or sets the name of/for the tax.
        /// </summary>
        /// <value>The name of the tax.</value>
        /// <returns>String</returns>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the display name of/for the tax.
        /// </summary>
        /// <value>The display name of the tax.</value>
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
        /// Gets or sets the value of/for the tax.
        /// </summary>
        /// <value>The value of the tax.</value>
        /// <returns>Double</returns>
        public double Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets the sequence of/for the tax.
        /// </summary>
        /// <value>The sequence of the tax.</value>
        /// <returns>Integer</returns>
        public int Sequence
        {
            get
            {
                return sequence;
            }
            set
            {
                sequence = value;
            }
        }
    }
}
