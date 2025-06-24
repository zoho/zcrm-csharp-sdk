using System;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMLeadConvertMapping : ZCRMEntity
    {
        private string name;
        private long id;
        private List<ZCRMLeadConvertMappingField> fields = new List<ZCRMLeadConvertMappingField>();

        private ZCRMLeadConvertMapping(string name, long id)
        {
            Name = name;
            Id = id;
        }

        public static ZCRMLeadConvertMapping GetInstance(string name, long id)
        {
            return new ZCRMLeadConvertMapping(name, id);
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
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

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
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

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public List<ZCRMLeadConvertMappingField> Fields
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