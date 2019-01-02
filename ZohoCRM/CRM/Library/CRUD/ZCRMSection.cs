using System.Collections.Generic;


namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMSection
    {

        private string name;
        private string displayName;
        private int columnCount;
        private int sequence;
        private List<ZCRMField> fields = new List<ZCRMField>();

        private ZCRMSection(string name)
        {
            Name = name;
        }

        /// <summary>
        /// To get ZCRMSection instance.
        /// </summary>
        /// <returns>ZCRMSection class instance.</returns>
        /// <param name="sectionName">Name (String) of the section.</param>
        public static ZCRMSection GetInstance(string sectionName)
        {
            return new ZCRMSection(sectionName);
        }

        /// <summary>
        /// To get the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        /// <returns>String</returns>
        public string Name { get => name; private set => name = value; }

        /// <summary>
        /// Gets or sets the display name of/for the section.
        /// </summary>
        /// <value>The display name of the section.</value>
        /// <returns>String</returns>
        public string DisplayName { get => displayName; set => displayName = value; }

        /// <summary>
        /// Gets or sets the column count of/for the section.
        /// </summary>
        /// <value>The column count of the section.</value>
        /// <returns>Integer</returns>
        public int ColumnCount { get => columnCount; set => columnCount = value; }

        /// <summary>
        /// Gets or sets the sequence of/for the section.
        /// </summary>
        /// <value>The sequence of the section.</value>
        /// <returns>Integer</returns>
        public int Sequence { get => sequence; set => sequence = value; }

        /// <summary>
        /// Gets or sets the fields of/for the section.
        /// </summary>
        /// <value>The fields.</value>
        /// <returns>List of ZCRMField class instance</returns>
        public List<ZCRMField> Fields { get => fields; set => fields = value; }

        /// <summary>
        /// To add the field of the section based on ZCRMField class instance.
        /// </summary>
        /// <param name="field">ZCRMField class instance.</param>
        public void AddField(ZCRMField field)
        {
            Fields.Add(field);
        }
    }
}
