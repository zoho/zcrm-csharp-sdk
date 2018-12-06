using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Api.Response;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMNote : ZCRMEntity
    {

        private long? id;
        private string title;
        private string content;
        private ZCRMRecord parentRecord;
        private ZCRMUser notesOwner;
        private ZCRMUser createdBy;
        private string createdTime;
        private ZCRMUser modifiedBy;
        private string modifiedTime;
        private List<ZCRMAttachment> attachments = new List<ZCRMAttachment>();


        private ZCRMNote(ZCRMRecord parentRecord, long noteId)
        {
            ParentRecord = parentRecord;
            Id = noteId;

        }

        public ZCRMNote(ZCRMRecord parentRecord)
        {
            ParentRecord = parentRecord;
        }

        public static ZCRMNote GetInstance(ZCRMRecord parentRecord, long noteId)
        {
            return new ZCRMNote(parentRecord, noteId);
        }


        public ZCRMRecord ParentRecord { get => parentRecord; private set => parentRecord = value; }

        public long? Id { get => id; set => id = value; }

        public string Title { get => title; set => title = value; }

        public string Content { get => content; set => content = value; }

        public ZCRMUser NotesOwner { get => notesOwner; set => notesOwner = value; }

        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        public string CreatedTime { get => createdTime; set => createdTime = value; }

        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        public List<ZCRMAttachment> Attachments { get => attachments; private set => attachments = value; }



        public void AddAttachment(ZCRMAttachment attachment)
        {
            Attachments.Add(attachment);
        }


        public APIResponse UploadAttachment(string filePath)
        {
            return ZCRMModuleRelation.GetInstance(ZCRMRecord.GetInstance("Notes", id), "Attachments").UploadAttachment(filePath);
        }

        public APIResponse DownloadAttachment(long attachmentId)
        {
            return ZCRMModuleRelation.GetInstance(ZCRMRecord.GetInstance("Notes", id), "Attachments").DownloadAttachment(attachmentId);
        }

        public APIResponse DeleteAttachment(long attachmentId)
        {
            return ZCRMModuleRelation.GetInstance(ZCRMRecord.GetInstance("Notes", id), "Attachments").DeleteAttachment(attachmentId);
        }

    }
}
