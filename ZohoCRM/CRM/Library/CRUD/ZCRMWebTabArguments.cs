using System;
namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMWebTabArguments
    {

        private string name;
        private string value;

        private ZCRMWebTabArguments(){}

        public static ZCRMWebTabArguments GetInstance()
        {
            return new ZCRMWebTabArguments();
        }

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
