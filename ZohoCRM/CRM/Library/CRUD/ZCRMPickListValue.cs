using Newtonsoft.Json.Linq;
namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMPickListValue
    {
        private string displayName;
        private string actualName;
        private int sequenceNumber;
        private JArray maps;

        private ZCRMPickListValue() { }

        /// <summary>
        /// To get ZCRMPickListValue instance.
        /// </summary>
        /// <returns>ZCRMPickListValue class instance.</returns>
        public static ZCRMPickListValue GetInstance()
        {
            return new ZCRMPickListValue();
        }
        /// <summary>
        /// Gets or sets the display name of/for the pick list.
        /// </summary>
        /// <value>The display name of the pick list.</value>
        /// <returns>String</returns>
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        /// <summary>
        /// Gets or sets the actual name of/for the pick list.
        /// </summary>
        /// <value>The actual name of the pick list.</value>
        /// <returns>String</returns>
        public string ActualName
        {
            get
            {
                return actualName;
            }
            set
            {
                actualName = value;
            }
        }

        /// <summary>
        /// Gets or sets the sequence number of/for the pick list.
        /// </summary>
        /// <value>The sequence number of the pick list.</value>
        /// <returns>Integer</returns>
        public int SequenceNumber { get
            {
                return sequenceNumber;
            }
            set
            {
                sequenceNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the maps of/for the pick list.
        /// </summary>
        /// <value>The maps of the pick list.</value>
        /// <returns>JSON Array(JArray)</returns>
        public JArray Maps
        {
            get
            {
                return maps;
            }
            set
            {
                maps = value;
            }
        }
    }
}
