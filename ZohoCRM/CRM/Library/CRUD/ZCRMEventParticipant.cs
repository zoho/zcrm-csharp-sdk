
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
        private string participant;

        private ZCRMEventParticipant(string type, long id)
        {
            Type = type;
            Id = id;
        }

        private ZCRMEventParticipant(string type, string participant)
        {
            Type = type;
            Participant = participant;
        }

        /// <summary>
        /// To get ZCRMEventParticipant instance by using participant type and Id.
        /// </summary>
        /// <returns>ZCRMEventParticipant class instance.</returns>
        /// <param name="type">Type (String) of the participant.</param>
        /// <param name="id">Id (Long) of the participant.</param>
        public static ZCRMEventParticipant GetInstance(string type, long id)
        {
            return new ZCRMEventParticipant(type, id);
        }

        /// <summary>
        /// To get ZCRMEventParticipant instance by using participant type and Id(email).
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="type">Type.</param>
        /// <param name="participant">Participant.</param>
        public static ZCRMEventParticipant GetInstance(string type, string participant)
        {
            return new ZCRMEventParticipant(type, participant);
        }

        /// <summary>
        /// Gets the participant Id.
        /// </summary>
        /// <value>The Id of the participant.</value>
        /// <returns>Long</returns>
        public long Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
            }
        }

        /// <summary>
        /// Gets the participant type.
        /// </summary>
        /// <value>The type of the participant.</value>
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
        /// Gets or sets the name of/for the participant.
        /// </summary>
        /// <value>The name of the participant.</value>
        /// <returns>String</returns>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the email of/for the participant.
        /// </summary>
        /// <value>The email of the participant.</value>
        /// <returns>String</returns>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this participant is invited.
        /// </summary>
        /// <value><c>true</c> if is invited; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool IsInvited
        {
            get
            {
                return isInvited;
            }
            set
            {
                isInvited = value;
            }
        }

        /// <summary>
        /// Gets or sets the status of/for the participant.
        /// </summary>
        /// <value>The status of the participant.</value>
        /// <returns>String</returns>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// Gets or sets the participant.
        /// </summary>
        /// <value>The participant.</value>
        public string Participant
        {
            get
            {
                return participant;
            }
            set
            {
                participant = value;
            }
        }
    }
}
