using System;
using System.Collections.Generic;

namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMProfileSection
    {
        private string name;
        private List<ZCRMProfileCategory> categories = new List<ZCRMProfileCategory>();

        private ZCRMProfileSection(string name)
        {

        }

        public static ZCRMProfileSection GetInstance(string name)
        {
            return new ZCRMProfileSection(name);
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

        public List<ZCRMProfileCategory> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
            }
        }

        public void SetCategories(ZCRMProfileCategory category)
        {
            Categories.Add(category);
        }
    }
}
