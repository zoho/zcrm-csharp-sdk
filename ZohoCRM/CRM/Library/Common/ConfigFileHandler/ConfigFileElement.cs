using System.Configuration;

namespace ZCRMSDK.CRM.Library.Common.ConfigFileHandler
{
    public class ConfigFileElement : ConfigurationElement
    {
        public ConfigFileElement() { }

        public ConfigFileElement(string key, string name)
        {
            this.Key = key;
            this.Value = name;
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value", DefaultValue = null)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}
