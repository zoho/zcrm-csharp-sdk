
namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMSubform : ZCRMRecord
    {
        private long? id = null;
        private string name = null;

        private ZCRMSubform(string name, long? id) : base(name)
        {
            Id = id;
            Name = name;
        }
        /// <summary>
        /// To get ZCRMSubform instance by passing subform name.
        /// </summary>
        /// <param name="name">Name (String) of the subform</param>
        public ZCRMSubform(string name) : this(name, null) { }

        /// <summary>
        /// To get ZCRMSubform instance by passing subform name and Id.
        /// </summary>
        /// <returns>ZCRMSubform class instance.</returns>
        /// <param name="name">Name (String) of the subform</param>
        /// <param name="id">Id (Long) of the subform</param>
        public static new ZCRMSubform GetInstance(string name, long? id)
        {
            return new ZCRMSubform(name, id);
        }
        private long? Id { get => id; set => id = value; }
        private string Name { get => name; set => name = value; }
    }
}
