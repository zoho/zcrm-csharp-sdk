using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;


namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMAttachment : ZCRMEntity
    {
        private long id;
        private string fileName;
        private string fileType;
        private long size;
        private ZCRMRecord parentRecord;
        private ZCRMUser owner;
        private ZCRMUser createdBy;
        private string createdTime;
        private ZCRMUser modifiedBy;
        private string modifiedTime;

        private ZCRMAttachment(ZCRMRecord parentRecord, long attachmentId)
        {
            ParentRecord = parentRecord;
            Id = attachmentId;
        }

        public ZCRMAttachment(ZCRMRecord parentRecord)
        {
            ParentRecord = parentRecord;
        }


        public static ZCRMAttachment GetInstance(ZCRMRecord parentRecord, long attachmentId)
        {
            return new ZCRMAttachment(parentRecord, attachmentId);
        }


        public ZCRMRecord ParentRecord { get => parentRecord; private set => parentRecord = value; }

        public long Id { get => id; set => id = value; }

        public string FileName { get => fileName; set => fileName = value; }

        public string FileType { get => fileType; set => fileType = value; }

        public long Size { get => size; set => size = value; }

        public ZCRMUser Owner { get => owner; set => owner = value; }

        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        public string CreatedTime { get => createdTime; set => createdTime = value; }

        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        public FileAPIResponse DownloadFile()
        {
           return ZCRMModuleRelation.GetInstance(parentRecord, "Attachments").DownloadAttachment(Id);
        }

    }
}
