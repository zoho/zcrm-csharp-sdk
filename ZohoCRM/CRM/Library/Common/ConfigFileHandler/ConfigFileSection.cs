using System.Configuration;

namespace ZCRMSDK.CRM.Library.Common.ConfigFileHandler
{
    public class ConfigFileSection : ConfigurationSection
    {
        public ConfigFileSection() { }

        [ConfigurationProperty("settings", IsDefaultCollection = false)]
        public ConfigFileCollection Settings
        {
            get
            {
                ConfigFileCollection configFileCollection =
                    (ConfigFileCollection)base["settings"];

                return configFileCollection;
            }

            set
            {
                base["settings"] = value;
            }
        }
    }
}
