using System;
using System.Collections.Generic;

namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMProfileCategory
    {

        private string name;
        private string module;
        private string displayLabel;
        private List<long> permissionIds = new List<long>();

        private ZCRMProfileCategory(string name)
        {
            Name = name;
        }

        public static ZCRMProfileCategory GetInstance(string name)
        {
            return new ZCRMProfileCategory(name);
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

        public List<long> PermissionIds
        {
            get
            {
                return permissionIds;
            }
            set
            {
                permissionIds = value;
            }
        }
    }
}
