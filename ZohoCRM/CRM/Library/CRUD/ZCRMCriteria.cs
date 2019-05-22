using System;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMCriteria : ZCRMEntity
    {
        private string comparator;
        private string field;
        private string value;
        private string groupOperator;
        private string pattern;
        private int index;
        private string criteria;
        private List<ZCRMCriteria> group;

        private ZCRMCriteria()
        {

        }

        public static ZCRMCriteria GetInstance()
        {
            return new ZCRMCriteria();
        }

        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>The pattern.</value>
        public string Pattern
        {
            get
            {
                return pattern;
            }
            set
            {
                pattern = value;
            }
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        public string Criteria
        {
            get
            {
                return criteria;
            }
            set
            {
                criteria = value;
            }
        }


        /// <summary>
        /// Gets or sets the comparator.
        /// </summary>
        /// <value>The comparator.</value>
        public string Comparator
        {
            get
            {
                return comparator;
            }
            set
            {
                comparator = value;
            }
        }

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>The field.</value>
        public string Field
        {
            get
            {
                return field;
            }
            set
            {
                field = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public string GroupOperator
        {
            get
            {
                return groupOperator;
            }
            set
            {
                groupOperator = value;
            }
        }

        public List<ZCRMCriteria> Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
            }
        }
    }
}