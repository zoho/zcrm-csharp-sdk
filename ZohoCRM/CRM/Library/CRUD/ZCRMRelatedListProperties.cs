using System;
using System.Collections.Generic;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMRelatedListProperties
    {
        private string sortBy;
        private string sortOrder;
        private List<string> fields = new List<string>();

        private ZCRMRelatedListProperties()
        {
        }

        public static ZCRMRelatedListProperties GetInstance()
        {
            return new ZCRMRelatedListProperties();
        }

        public string SortBy
        {
            get
            {
                return sortBy;
            }
            set
            {
                sortBy = value;
            }
        }

        public string SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                sortOrder = value;
            }
        }

        public List<string> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
            }
        }
    }
}
