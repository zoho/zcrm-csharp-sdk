using System.Collections.Generic;


namespace ZCRMSDK.CRM.Library.CRUD
{
    public class ZCRMJunctionRecord
    {

        private string apiName;
        private long id;
        private Dictionary<string, object> relatedDetails = new Dictionary<string, object>();

        private ZCRMJunctionRecord(string apiName, long id)
        {
            ApiName = apiName;
            Id = id;
        }

        /// <summary>
        /// To get ZCRMJunctionRecord instance by passing JunctionRecord APIName and record Id
        /// </summary>
        /// <returns>ZCRMJunctionRecord class instance.</returns>
        /// <param name="apiName">APIName (String) of the JunctionRecord</param>
        /// <param name="id">Id (Long) of the JunctionRecord</param>
        public static ZCRMJunctionRecord GetInstance(string apiName, long id)
        {
            return new ZCRMJunctionRecord(apiName, id);
        }

        /// <summary>
        /// Gets the APIName of the JunctionRecord.
        /// </summary>
        /// <value>The APIName of the JunctionRecord.</value>
        /// <returns>String</returns>
        public string ApiName { get => apiName; private set => apiName = value; }

        /// <summary>
        /// Gets the JunctionRecord Id.
        /// </summary>
        /// <value>The Id of the JunctionRecord.</value>
        /// <returns>Long</returns>
        public long Id { get => id; private set => id = value; }

        /// <summary>
        /// Gets the related details of the JunctionRecord.
        /// </summary>
        /// <value>The related details of the JunctionRecord.</value>
        /// <returns>Dictionary(String,Object)</returns>
        public Dictionary<string, object> RelatedDetails { get => relatedDetails; private set => relatedDetails = value; }

        /// <summary>
        /// To set the related details of the JunctionRecord based on field APIName and value.
        /// </summary>
        /// <param name="fieldAPIName">APIName (String) of the field</param>
        /// <param name="value">value (Object) of the field </param>
        public void SetRelatedDetails(string fieldAPIName, object value)
        {
            RelatedDetails.Add(fieldAPIName, value);
        }

    }
}
