
namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMEventParticipant
    {
        private string email;
        private string name;
        private long id;
        private string type;
        private bool isInvited;
        private string status;


        private ZCRMEventParticipant(string type, long id)
        {
            Type = type;
            Id = id;
        }

        public static ZCRMEventParticipant GetInstance(string type, long id)
        {
            return new ZCRMEventParticipant(type, id);
        }

        public long Id { get => id; private set => id = value; }

        public string Type { get => type; private set => type = value; }

        public string Name { get => name; set => name = value; }

        public string Email { get => email; set => email = value; }

        public bool IsInvited { get => isInvited; set => isInvited = value; }

        public string Status { get => status; set => status = value; }
    }
}
