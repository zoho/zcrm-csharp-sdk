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

        public static ZCRMJunctionRecord GetInstance(string apiName, long id)
        {
            return new ZCRMJunctionRecord(apiName, id);
        }

        public string ApiName { get => apiName; private set => apiName = value; }

        public long Id { get => id; private set => id = value; }

        public Dictionary<string, object> RelatedDetails { get => relatedDetails; private set => relatedDetails = value; }

        public void SetRelatedDetails(string fieldAPIName, object value)
        {
            RelatedDetails.Add(fieldAPIName, value);
        }

    }
}
