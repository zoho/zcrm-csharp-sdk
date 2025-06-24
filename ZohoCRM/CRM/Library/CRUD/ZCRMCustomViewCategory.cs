using System;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMCustomViewCategory : ZCRMEntity
    {
        private string displayValue;
        private string actualValue;

        private ZCRMCustomViewCategory()
        {
        }

        public static ZCRMCustomViewCategory GetInstance()
        {
            return new ZCRMCustomViewCategory();
        }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <value>The display value.</value>
        public string DisplayValue
        {
            get
            {
                return displayValue;
            }
            set
            {
                displayValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the actual value.
        /// </summary>
        /// <value>The actual value.</value>
        public string ActualValue
        {
            get
            {
                return actualValue;
            }
            set
            {
                actualValue = value;
            }
        }
    }
}
