using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMTrashRecord : ZCRMEntity
    {
        private long? entityId;
        private string displayName;
        private string type;
        private string deletedTime;
        private ZCRMUser createdBy;
        private ZCRMUser deletedBy;


        private ZCRMTrashRecord(string type, long? id)
        {
            Type = type;
            EntityId = id;
        }

        /// <summary>
        /// To get ZCRMTrashRecord class instance.
        /// </summary>
        /// <param name="type">Type (String) of the trash record.</param>
        public ZCRMTrashRecord(string type) : this(type, null) {}

        /// <summary>
        /// To get ZCRMTrashRecord instance by passing Trash record type and entity(record) Id.
        /// </summary>
        /// <returns>ZCRMTrashRecord class instance.</returns>
        /// <param name="type">Type (String) of the trash record.</param>
        /// <param name="entityId">Entity(record) Id (Long) of the trash record </param>
        public static ZCRMTrashRecord GetInstance(string type, long entityId)
        {
            return new ZCRMTrashRecord(type, entityId);
        }

        /// <summary>
        /// Gets or sets the entity(record) Id of/for the trash record.
        /// </summary>
        /// <value>The entity(record) Id (Long) of the trash record.</value>
        /// <returns>Long</returns>
        public long? EntityId
        {
            get
            {
                return entityId;
            }
            set
            {
                entityId = value;
            }
        }

        /// <summary>
        /// Gets or sets the display name of/for the trash record.
        /// </summary>
        /// <value>The display name of the trash record.</value>
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
        /// Gets the type of the trash record.
        /// </summary>
        /// <value>The type (String) of the trash record</value>
        /// <returns>String</returns>
        public string Type
        {
            get
            {
                return type;
            }
            private set
            {
                type = value;
            }
        }

        /// <summary>
        /// Gets or sets the deleted time of/for the trash record.
        /// </summary>
        /// <value>The deleted time of the trash record.</value>
        /// <returns>String</returns>
        public string DeletedTime
        {
            get
            {
                return deletedTime;
            }
            set
            {
                deletedTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who create the trash record.
        /// </summary>
        /// <value>The user who create the trash record.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who deleted the record.
        /// </summary>
        /// <value>The user who deleted the record.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser DeletedBy
        {
            get
            {
                return deletedBy;
            }
            set
            {
                deletedBy = value;
            }
        }
    }
}
