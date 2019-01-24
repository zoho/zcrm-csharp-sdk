
namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMPriceBookPricing
    {

        private long? id;
        private double toRange;
        private double fromRange;
        private double discount;

        public ZCRMPriceBookPricing() { }

        private ZCRMPriceBookPricing(long id)
        {
            Id = id;
        }

        /// <summary>
        /// To get ZCRMPriceBookPricing instance by passing price book Id.
        /// </summary>
        /// <returns>ZCRMPriceBookPricing class instance.</returns>
        /// <param name="id">Id (Long) of the price book</param>
        public static ZCRMPriceBookPricing GetInstance(long id)
        {
            return new ZCRMPriceBookPricing(id);
        }

        /// <summary>
        /// Gets or sets the price book Id.
        /// </summary>
        /// <value>The price book Id.</value>
        /// <returns>Long</returns>
        public long? Id { get => id; set => id = value; }

        /// <summary>
        /// Gets or sets to range of/for the price book.
        /// </summary>
        /// <value>To range of the price book</value>
        /// <returns>Double</returns>
        public double ToRange { get => toRange; set => toRange = value; }

        /// <summary>
        /// Gets or sets from range of/for the price book.
        /// </summary>
        /// <value>From range of the price book.</value>
        /// <returns>Double</returns>
        public double FromRange { get => fromRange; set => fromRange = value; }

        /// <summary>
        /// Gets or sets the discount of/for the price book.
        /// </summary>
        /// <value>The discount of the price book.</value>
        /// <returns>Double</returns>
        public double Discount { get => discount; set => discount = value; }
    }
}
