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

        /// <summary>
        /// To get attachment instance by passing ZCRMRecord instance.
        /// </summary>
        /// <param name="parentRecord">ZCRMRecord class instance.</param>
        public ZCRMAttachment(ZCRMRecord parentRecord)
        {
            ParentRecord = parentRecord;
        }

        /// <summary>
        /// To get attachment instance by passing ZCRMRecord instance and attachment Id.
        /// </summary>
        /// <returns>ZCRMAttachment class instance.</returns>
        /// <param name="parentRecord">ZCRMRecord class instance</param>
        /// <param name="attachmentId">Id (Long) of the attachment </param>
        public static ZCRMAttachment GetInstance(ZCRMRecord parentRecord, long attachmentId)
        {
            return new ZCRMAttachment(parentRecord, attachmentId);
        }

        /// <summary>
        /// Gets the parent record of the current attachment.
        /// </summary>
        /// <value>The parent record of the current attachment.</value>
        /// <returns>ZCRMRecord class instance</returns>
        public ZCRMRecord ParentRecord { get => parentRecord; private set => parentRecord = value; }

        /// <summary>
        /// Gets or sets the attachment Id.
        /// </summary>
        /// <value>The attachment Id.</value>
        /// <returns>Long</returns>
        public long Id { get => id; set => id = value; }

        /// <summary>
        /// Gets or sets the name of/for the attachment file.
        /// </summary>
        /// <value>The name of the attachment file.</value>
        /// <returns>String</returns>
        public string FileName { get => fileName; set => fileName = value; }

        /// <summary>
        /// Gets or sets the type of/for the attachment file.
        /// </summary>
        /// <value>The type of the attachment file.</value>
        /// <returns>String</returns>
        public string FileType { get => fileType; set => fileType = value; }

        /// <summary>
        /// Gets or sets the size of/for the attachment.
        /// </summary>
        /// <value>The size of the attachment.</value>
        /// <returns>Long</returns>
        public long Size { get => size; set => size = value; }

        /// <summary>
        /// Gets or sets the user who owner of the attachment.
        /// </summary>
        /// <value>The user who upload the attachment.</value>
        /// <return>ZCRMUser class instance</return>
        public ZCRMUser Owner { get => owner; set => owner = value; }

        /// <summary>
        /// Gets or sets the user who created the attachment.
        /// </summary>
        /// <value>The user who created the attachment.</value>
        /// <return>ZCRMUser class instance</return>
        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        /// <summary>
        /// Gets or sets the created time of/for the attachment.
        /// </summary>
        /// <value>The created time of the attachment.</value>
        /// <returns>String</returns>
        public string CreatedTime { get => createdTime; set => createdTime = value; }

        /// <summary>
        /// Gets or sets the user who modified the attachment.
        /// </summary>
        /// <value>The user who modified the attachment.</value>
        /// <return>ZCRMUser class instance</return>
        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        /// <summary>
        /// Gets or sets the modified time of/for the attachment.
        /// </summary>
        /// <value>The modified time of the attachment.</value>
        /// <returns>String</returns>
        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        /// <summary>
        /// To download the attachment file.
        /// </summary>
        /// <returns>FileAPIResponse class instance</returns>
        public FileAPIResponse DownloadFile()
        {
           return ZCRMModuleRelation.GetInstance(parentRecord, "Attachments").DownloadAttachment(Id);
        }

    }
}
