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

        public ZCRMTrashRecord(string type) : this(type, null) {}

       
        public static ZCRMTrashRecord GetInstance(string type, long entityId)
        {
            return new ZCRMTrashRecord(type, entityId);
        }


        public long? EntityId { get => entityId; set => entityId = value; }

        public string DisplayName { get => displayName; set => displayName = value; }

        public string Type { get => type; private set => type = value; }

        public string DeletedTime { get => deletedTime; set => deletedTime = value; }

        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        public ZCRMUser DeletedBy { get => deletedBy; set => deletedBy = value; }
    }
}
