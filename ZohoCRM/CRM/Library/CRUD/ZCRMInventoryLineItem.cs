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

        private int quantityInStock;

        /// <summary>
        /// To get ZCRMInventoryLineItem instance by passing ZCRMRecord class instance.
        /// </summary>
        /// <param name="product">ZCRMRecord class instance.</param>
        public ZCRMInventoryLineItem(ZCRMRecord product)
        {
            Product = product;    
        }

        private ZCRMInventoryLineItem(long lineItemId)
        {
            Id = lineItemId;
        }

        /// <summary>
        /// To get ZCRMInventoryLineItem instance by passing line item Id.
        /// </summary>
        /// <returns>ZCRMInventoryLineItem class instance.</returns>
        /// <param name="lineItemId">Id (Long) of the line item</param>
        public static ZCRMInventoryLineItem GetInstance(long lineItemId)
        {
            return new ZCRMInventoryLineItem(lineItemId);
        }

        /// <summary>
        /// Gets or sets the product of/for the line item.
        /// </summary>
        /// <value>The product of the line item.</value>
        /// <returns>ZCRMRecord class instance</returns>
        public ZCRMRecord Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
            }
        }

        /// <summary>
        /// Gets or sets the line item Id.
        /// </summary>
        /// <value>Id of the line item.</value>
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
        /// Gets or sets the description of/for the line item.
        /// </summary>
        /// <value>The description of the line item.</value>
        /// <returns>String</returns>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// Gets or sets the quantity of/for the line item.
        /// </summary>
        /// <value>The quantity of the line item.</value>
        /// <returns>Double</returns>
        public double Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        /// <summary>
        /// Gets or sets the list price of/for the line item.
        /// </summary>
        /// <value>The list price of the line item.</value>
        /// <returns>Double</returns>
        public double ListPrice
        {
            get
            {
                return listPrice;
            }
            set
            {
                listPrice = value;
            }
        }

        /// <summary>
        /// Gets or sets the unit price of/for the line item.
        /// </summary>
        /// <value>The unit price of the line item.</value>
        /// <returns>Double</returns>
        public double UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                unitPrice = value;
            }
        }

        /// <summary>
        /// Gets or sets the total of/for the line item.
        /// </summary>
        /// <value>The total of the line item.</value>
        /// <returns>Double</returns>
        public double Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
            }
        }

        /// <summary>
        /// Gets or sets the discount percentage of/for the line item.
        /// </summary>
        /// <value>The discount percentage of the line item.</value>
        /// <returns>Double</returns>
        public double? DiscountPercentage 
        { 
            get
            {
                return discountPercentage;
            }
            set 
            { 
                discountPercentage = value;
                discount = 0.0D;
            } 
        }

        /// <summary>
        /// Gets or sets the discount of/for the line item.
        /// </summary>
        /// <value>The discount of the line item.</value>
        /// <returns>Double</returns>
        public double? Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
                discountPercentage = 0.0D;
            }
        }

        /// <summary>
        /// Gets or sets the total after discount of/for the line item.
        /// </summary>
        /// <value>The total after discount of the line item.</value>
        /// <returns>Double</returns>
        public double TotalAfterDiscount
        {
            get
            {
                return totalAfterDiscount;
            }
            set
            {
                totalAfterDiscount = value;
            }
        }

        /// <summary>
        /// Gets or sets the net total of/for the line item.
        /// </summary>
        /// <value>The net total of the line item.</value>
        /// <returns>Double</returns>
        public double NetTotal
        {
            get
            {
                return netTotal;
            }
            set
            {
                netTotal = value;
            }
        }

        /// <summary>
        /// Gets the line tax of the line item.
        /// </summary>
        /// <value>The line tax of the line item.</value>
        /// <returns>List of ZCRMTax class instance</returns>
        public List<ZCRMTax> LineTax
        {
            get
            {
                return lineTax;
            }
            private set
            {
                lineTax = value;
            }
        }

        /// <summary>
        /// Gets or sets the tax amount of/for the line item.
        /// </summary>
        /// <value>The tax amount of the line item.</value>
        /// <returns>Double</returns>
        public double TaxAmount
        {
            get
            {
                return taxAmount;
            }
            set
            {
                taxAmount = value;
            }
        }

        /// <summary>
        /// To add the line tax based on ZCRMTax class instance.
        /// </summary>
        /// <param name="lineTax">ZCRMTax class instance</param>
        public void AddLineTax(ZCRMTax lineTax)
        {
            LineTax.Add(lineTax);
        }


        public int QuantityInStock
        {
            get
            {
                return quantityInStock;
            }
            set
            {
                quantityInStock = value;
            }
        }
    }
}
