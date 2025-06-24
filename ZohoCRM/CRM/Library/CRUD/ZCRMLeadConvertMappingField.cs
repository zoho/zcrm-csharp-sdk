using System;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMLeadConvertMappingField : ZCRMEntity
    {
        private string apiName;
        private long id;
        private string fieldLabel;
        private bool required;

        private ZCRMLeadConvertMappingField(string apiName, long id)
        {
            ApiName = apiName;
            Id = id;
        }

        public static ZCRMLeadConvertMappingField GetInstance(string apiName, long id)
        {
            return new ZCRMLeadConvertMappingField(apiName, id);
        }

        /// <summary>
        /// Gets or sets the name of the API.
        /// </summary>
        /// <value>The name of the API.</value>
        public string ApiName
        {
            get
            {
                return apiName;
            }
            set
            {
                apiName = value;
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
        /// Gets or sets the field label.
        /// </summary>
        /// <value>The field label.</value>
        public string FieldLabel
        {
            get
            {
                return fieldLabel;
            }
            set
            {
                fieldLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:ZCRMSDK.CRM.Library.CRUD.ZCRMLeadConvertMappingField"/> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        public bool Required
        {
            get
            {
                return required;
            }
            set
            {
                required = value;
            }
        }
    }
}
