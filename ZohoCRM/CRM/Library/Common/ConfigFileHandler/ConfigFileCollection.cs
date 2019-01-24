using System.Configuration;

namespace ZCRMSDK.CRM.Library.Common.ConfigFileHandler
{
    [ConfigurationCollection(itemType: typeof(ConfigFileElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class ConfigFileCollection : ConfigurationElementCollection
    {

        static ConfigFileCollection()
        {

            collectionProperties = new ConfigurationPropertyCollection();

        }


        private static ConfigurationPropertyCollection collectionProperties;

        protected ConfigurationPropertyCollection CollectionProperties
        {
            get { return collectionProperties; }
        }

        protected new ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }


        public ConfigFileElement this[int index]
        {
            get { return (ConfigFileElement)base.BaseGet(index); }

            set
            {
                if (base.BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }


        public new ConfigFileElement this[string key]
        {
            get { return (ConfigFileElement)base.BaseGet(key); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigFileElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ConfigFileElement).Key;
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Clear()
        {
            BaseClear();
        }

        public void Add(ConfigFileElement element)
        {
            base.BaseAdd(element);
        }
    }
}
