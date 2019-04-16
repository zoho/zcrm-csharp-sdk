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

        /// <summary>
        /// To get ZCRMNote instance by passing ZCRMRecord instance.
        /// </summary>
        /// <param name="parentRecord">ZCRMRecord class instance</param>
        public ZCRMNote(ZCRMRecord parentRecord)
        {
            ParentRecord = parentRecord;
        }

        /// <summary>
        /// To get ZCRMNote instance by passing ZCRMRecord class instance and note Id.
        /// </summary>
        /// <returns>ZCRMNote class instance.</returns>
        /// <param name="parentRecord">ZCRMRecord class instance</param>
        /// <param name="noteId">Id (Long) of the record note.</param>
        public static ZCRMNote GetInstance(ZCRMRecord parentRecord, long noteId)
        {
            return new ZCRMNote(parentRecord, noteId);
        }

        /// <summary>
        /// Gets the parent record of the note.
        /// </summary>
        /// <value>The parent record of the note.</value>
        /// <returns>ZCRMRecord class instance</returns>
        public ZCRMRecord ParentRecord
        {
            get
            {
                return parentRecord;
            }
            private set
            {
                parentRecord = value;
            }
        }

        /// <summary>
        /// Gets or sets the note Id.
        /// </summary>
        /// <value>The identifier.</value>
        /// <returns>Long</returns>
        public long? Id
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
        /// Gets or sets the title of/for the note.
        /// </summary>
        /// <value>The title of the note.</value>
        /// <returns>String</returns>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// Gets or sets the content of/for the note.
        /// </summary>
        /// <value>The content of the note.</value>
        /// <returns>String</returns>
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who owner of the notes.
        /// </summary>
        /// <value>The user who owner of the notes.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser NotesOwner
        {
            get
            {
                return notesOwner;
            }
            set
            {
                notesOwner = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who created the notes.
        /// </summary>
        /// <value>The user who created the notes.</value>
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
        /// Gets or sets the created time of/for the notes.
        /// </summary>
        /// <value>The created time of the notes.</value>
        /// <returns>String</returns>
        public string CreatedTime
        {
            get
            {
                return createdTime;
            }
            set
            {
                createdTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who modified the notes.
        /// </summary>
        /// <value>The user who modified the notes.</value>
        /// <returns>ZCRMUser class instance</returns>
        public ZCRMUser ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
            }
        }

        /// <summary>
        /// Gets or sets the modified time of/for the notes.
        /// </summary>
        /// <value>The modified time of the notes.</value>
        /// <returns>String</returns>
        public string ModifiedTime
        {
            get
            {
                return modifiedTime;
            }
            set
            {
                modifiedTime = value;
            }
        }

        /// <summary>
        /// Gets the attachments of the notes.
        /// </summary>
        /// <value>The attachments of the notes.</value>
        /// <returns>List of ZCRMAttachment class instance</returns>
        public List<ZCRMAttachment> Attachments
        {
            get
            {
                return attachments;
            }
            private set
            {
                attachments = value;
            }
        }

        /// <summary>
        /// To add the attachment of the notes based on ZCRMAttachment class instance.
        /// </summary>
        /// <param name="attachment">ZCRMAttachment class instance</param>
        public void AddAttachment(ZCRMAttachment attachment)
        {
            Attachments.Add(attachment);
        }

        /// <summary>
        /// To upload the attachment of the notes based on file path.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="filePath">File path (String) of the note attachment</param>
        public APIResponse UploadAttachment(string filePath)
        {
            return ZCRMModuleRelation.GetInstance(ZCRMRecord.GetInstance("Notes", id), "Attachments").UploadAttachment(filePath);
        }

        /// <summary>
        /// To download the attachment of the notes based on attachment Id.
        /// </summary>
        /// <returns>FileAPIResponse class instance.</returns>
        /// <param name="attachmentId">Attachment Id (Long) of the notes</param>
        public FileAPIResponse DownloadAttachment(long attachmentId)
        {
            return ZCRMModuleRelation.GetInstance(ZCRMRecord.GetInstance("Notes", id), "Attachments").DownloadAttachment(attachmentId);
        }

        /// <summary>
        /// To delete the attachment of the notes based on attachment Id.
        /// </summary>
        /// <returns>APIResponse class instance.</returns>
        /// <param name="attachmentId">Attachment Id (Long) of the notes</param>
        public APIResponse DeleteAttachment(long attachmentId)
        {
            return ZCRMModuleRelation.GetInstance(ZCRMRecord.GetInstance("Notes", id), "Attachments").DeleteAttachment(attachmentId);
        }

    }
}
