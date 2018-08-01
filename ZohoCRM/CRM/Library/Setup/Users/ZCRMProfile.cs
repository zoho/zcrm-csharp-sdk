using ZCRMSDK.CRM.Library.Common;


namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMProfile : ZCRMEntity
    {

        private long id;
        private string name;
        private ZCRMUser createdBy;
        private ZCRMUser modifiedBy;
        private string createdTime;
        private string modifiedTime;
        private bool category;
        private string description;

        private ZCRMProfile(long profileId, string profileName)
        {
            Id = profileId;
            Name = profileName;
        }

        public static ZCRMProfile GetInstance(long profileId, string profileName)
        {
            return new ZCRMProfile(profileId, profileName);
        }


        public long Id { get => id; private set => id = value; }

        public string Name { get => name; private set => name = value; }

        public ZCRMUser CreatedBy { get => createdBy; set => createdBy = value; }

        public ZCRMUser ModifiedBy { get => modifiedBy; set => modifiedBy = value; }

        public string CreatedTime { get => createdTime; set => createdTime = value; }

        public string ModifiedTime { get => modifiedTime; set => modifiedTime = value; }

        public bool Category { get => category; set => category = value; }

        public string Description { get => description; set => description = value; }
    }
}
