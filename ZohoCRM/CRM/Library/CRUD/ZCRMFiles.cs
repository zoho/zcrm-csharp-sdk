using System;
using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMFiles : ZCRMEntity
    {

        private string extn;

        private bool isPreviewAvailable;

        private string downloadUrl;

        private string deleteUrl;

        private long entityId;

        private string mode;

        private long originalSizeByte;

        private string previewUrl;

        private string fileName;

        private string fileId;

        private string attachmentId;

        private string fileSize;

        private long creatorId;

        private int linkDocs;

        private bool delete = false;

        private Dictionary<string, object> keyValues = new Dictionary<string, object>();

        private ZCRMFiles(string fileId, string fileName)
        {
            this.fileId = fileId;

            this.fileName = fileName;
        }

        public static ZCRMFiles GetInstance(string id, string fileName)
        {
            return new ZCRMFiles(id, fileName);
        }

        public string Extn
        {
            get
            {
                return this.extn;
            }
            set
            {
                this.extn = value;
            }
        }

        public bool IsPreviewAvailable
        {
            get
            {
                return this.isPreviewAvailable;
            }
            set
            {
                this.isPreviewAvailable = value;
            }
        }

        public string DownloadUrl
        {
            get
            {
                return this.downloadUrl;
            }
            set
            {
                this.downloadUrl = value;
            }
        }

        public string DeleteUrl
        {
            get
            {
                return this.deleteUrl;
            }
            set
            {
                this.deleteUrl = value;
            }
        }

        public long EntityId
        {
            get
            {
                return this.entityId;
            }
            set
            {
                this.entityId = value;
            }
        }

        public string Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }

        public long OriginalSizeByte
        {
            get
            {
                return this.originalSizeByte;
            }
            set
            {
                this.originalSizeByte = value;
            }
        }

        public string PreviewUrl
        {
            get
            {
                return this.previewUrl;
            }
            set
            {
                this.previewUrl = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public string FileId
        {
            get
            {
                return this.fileId;
            }
            set
            {
                this.fileId = value;
            }
        }

        public string AttachmentId
        {
            get
            {
                return this.attachmentId;
            }
            set
            {
                this.attachmentId = value;
            }
        }

        public string FileSize
        {
            get
            {
                return this.fileSize;
            }
            set
            {
                this.fileSize = value;
            }
        }

        public long CreatorId
        {
            get
            {
                return this.creatorId;
            }
            set
            {
                this.creatorId = value;
            }
        }

        public int LinkDocs
        {
            get
            {
                return this.linkDocs;
            }
            set
            {
                this.linkDocs = value;
            }
        }

        public bool Delete
        {
            get
            {
                return this.delete;
            }
            set
            {
                this.delete = value;
            }
        }

        public Dictionary<string,object> KeyValues
        {
            get
            {
                return this.keyValues;
            }
            set
            {
                this.keyValues = value;
            }
        }

        public void AddKeyValues(string keyName, object keyValue)
        {
            KeyValues[keyName] = keyValue;
        }
    }
}
