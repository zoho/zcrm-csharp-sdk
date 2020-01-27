using System;
namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMToolTip
    {
        private string name;
        private string value;

        private ZCRMToolTip(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public static ZCRMToolTip GetInstance(string name, string value)
        {
            return new ZCRMToolTip(name, value);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}
