using ZCRMSDK.CRM.Library.Common;

namespace ZCRMSDK.CRM.Library.Setup.Users
{
    public class ZCRMRole : ZCRMEntity
    {

        private long id;
        private string name;
        private string label;
        private bool isAdminUser;
        private ZCRMRole reportingTo;


        private ZCRMRole(long roleId, string roleName)
        {
            Id = roleId;
            Name = roleName;
        }

        public static ZCRMRole GetInstance(long roleId, string roleName)
        {
            return new ZCRMRole(roleId, roleName);
        }


        public long Id { get => id; private set => id = value; }

        public string Name { get => name; private set => name = value; }

        public string Label { get => label; set => label = value; }

        public bool AdminUser { get => isAdminUser; set => isAdminUser = value; }

        public ZCRMRole ReportingTo { get => reportingTo; set => reportingTo = value; }
    }
}
