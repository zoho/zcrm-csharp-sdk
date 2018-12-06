using System.Collections.Generic;


namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMInventoryLineItem
    {

        private long? id;
        private ZCRMRecord product;
        private double listPrice;
        private double unitPrice;
        private double quantity;
        private string description;
        private double total;
        private double? discount;
        private double? discountPercentage;
        private double totalAfterDiscount;
        private double taxAmount;
        private double netTotal;
        private List<ZCRMTax> lineTax = new List<ZCRMTax>();


        public ZCRMInventoryLineItem(ZCRMRecord product)
        {
            Product = product;    
        }

        private ZCRMInventoryLineItem(long lineItemId)
        {
            Id = lineItemId;
        }

        public static ZCRMInventoryLineItem GetInstance(long lineItemId)
        {
            return new ZCRMInventoryLineItem(lineItemId);
        }


        public ZCRMRecord Product { get => product; set => product = value; }

        public long? Id { get => id; set => id = value; }

        public string Description { get => description; set => description = value; }

        public double Quantity { get => quantity; set => quantity = value; }

        public double ListPrice { get => listPrice; set => listPrice = value; }

        public double UnitPrice { get => unitPrice; set => unitPrice = value; }

        public double Total { get => total; set => total = value; }

        public double? DiscountPercentage 
        { 
            get => discountPercentage; 
            set 
            { 
                discountPercentage = value;
                discount = 0.0D;
            } 
        }

        public double? Discount
        {
            get => discount;
            set
            {
                discount = value;
                discountPercentage = 0.0D;
            }
        }

        public double TotalAfterDiscount { get => totalAfterDiscount; set => totalAfterDiscount = value; }

        public double NetTotal { get => netTotal; set => netTotal = value; }

        public List<ZCRMTax> LineTax { get => lineTax; private set => lineTax = value; }

        public double TaxAmount { get => taxAmount; set => taxAmount = value; }


        public void AddLineTax(ZCRMTax lineTax)
        {
            LineTax.Add(lineTax);
        }
    }
}
