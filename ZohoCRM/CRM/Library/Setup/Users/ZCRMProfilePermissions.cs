using System;
namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMProfilePermissions
    {
        private string displayLabel;
        private string module;
        private string name;
        private long id;
        private bool enabled;

        private ZCRMProfilePermissions(){}

        public static ZCRMProfilePermissions GetInstance()
        {
            return new ZCRMProfilePermissions();
        }

        public string DisplayLabel
        {
            get
            {
                return displayLabel;
            }
            set
            {
                displayLabel = value;
            }
        }

        public string Module
        {
            get
            {
                return module;
            }
            set
            {
                module = value;
            }
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

        public long Id
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

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }
    }
}
