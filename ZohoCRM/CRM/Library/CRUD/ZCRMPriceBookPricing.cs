
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

        public static ZCRMPriceBookPricing GetInstance(long id)
        {
            return new ZCRMPriceBookPricing(id);
        }

        public long? Id { get => id; set => id = value; }

        public double ToRange { get => toRange; set => toRange = value; }

        public double FromRange { get => fromRange; set => fromRange = value; }

        public double Discount { get => discount; set => discount = value; }
    }
}
