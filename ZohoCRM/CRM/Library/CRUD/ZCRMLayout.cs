using System.Collections.Generic;
using ZCRMSDK.CRM.Library.Common;
using ZCRMSDK.CRM.Library.Setup.Users;

namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMLayout : ZCRMEntity
    {

        private long id;
        private string name;
        private ZCRMUser createdBy;
        private ZCRMUser modifiedBy;
        private string createdTime;
        private string modifiedTime;
        private bool visible;
        private int status;
        private List<ZCRMSection> sections = new List<ZCRMSection>();
        private List<ZCRMProfile> accessibleProfies = new List<ZCRMProfile>();

        private ZCRMLayout(long layoutId)
        {
            Id = layoutId;
        }

        public static ZCRMLayout GetInstance(long layoutId)
        {
            return new ZCRMLayout(layoutId);    
        }



        public long Id { get => id; private set => id = value; }

        public string Name { get => name; set => name = value; }

        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        public string CreatedTime { get => createdTime; set => createdTime = value; }

        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        public bool Visible { get => visible; set => visible = value; }

        public int Status { get => status; set => status = value; }

        public List<ZCRMProfile> AccessibleProfies { get => accessibleProfies; private set => accessibleProfies = value; }

        public List<ZCRMSection> Sections { get => sections; set => sections = value; }

        public void AddAccessibleProfiles(ZCRMProfile profile)
        {
            AccessibleProfies.Add(profile);
        }

        public void AddSection(ZCRMSection section)
        {
            Sections.Add(section);
        }
    }
}
