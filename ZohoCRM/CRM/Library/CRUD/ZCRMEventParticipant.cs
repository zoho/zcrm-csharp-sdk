
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
        /// Gets the participant Id.
        /// </summary>
        /// <value>The Id of the participant.</value>
        /// <returns>Long</returns>
        public long Id { get => id; private set => id = value; }

        /// <summary>
        /// Gets the participant type.
        /// </summary>
        /// <value>The type of the participant.</value>
        /// <returns>String</returns>
        public string Type { get => type; private set => type = value; }

        /// <summary>
        /// Gets or sets the name of/for the participant.
        /// </summary>
        /// <value>The name of the participant.</value>
        /// <returns>String</returns>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Gets or sets the email of/for the participant.
        /// </summary>
        /// <value>The email of the participant.</value>
        /// <returns>String</returns>
        public string Email { get => email; set => email = value; }

        /// <summary>
        /// Gets or sets a value indicating whether this participant is invited.
        /// </summary>
        /// <value><c>true</c> if is invited; otherwise, <c>false</c>.</value>
        /// <returns>Boolean</returns>
        public bool IsInvited { get => isInvited; set => isInvited = value; }

        /// <summary>
        /// Gets or sets the status of/for the participant.
        /// </summary>
        /// <value>The status of the participant.</value>
        /// <returns>String</returns>
        public string Status { get => status; set => status = value; }
    }
}
