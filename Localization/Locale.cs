using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NeoSmart.Localization
{
    public class Locale : IComparable<Locale>
    {
        private readonly Dictionary<string, StringCollection> _stringMap = new Dictionary<string, StringCollection>();

        public IEnumerable<StringCollection> StringCollections
        {
            get { return _stringMap.Values; }
        }

        public string Key { get; private set; }
        public string Name { get; internal set; }
        public bool Loaded { get; internal set; }
        public bool RightToLeft { get; internal set; }
        public string ParentLocale { get; private set; }

        public int CompareTo(Locale other)
        {
            return Name.CompareTo(other.Name);
        }

        public Locale(string localeKey)
        {
            Key = localeKey;
        }

        public string GetString(string key)
        {
            return _stringMap[key].StringsTable[key];
        }

        private void LoadPropertiesXml(string xmlPath)
        {
            XmlNode node;
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            node = xmlDocument.SelectSingleNode(@"/localization/locale");
            if (node == null)
                throw new IncompleteLocaleException("The required locale element 'locale' was not found.");

            node = xmlDocument.SelectSingleNode(@"/localization/locale/name");
            if (node == null)
                throw new IncompleteLocaleException("The required locale element 'name' was not found.");
            Name = node.InnerText;

            node = xmlDocument.SelectSingleNode(@"/localization/locale/rtl");
            RightToLeft = node != null && node.InnerText == "true";

            node = xmlDocument.SelectSingleNode(@"/localization/locale/parentLocale");
            ParentLocale = node != null ? node.InnerText : string.Empty;
        }

        public bool LoadFromFile(string xmlPath)
        {
            if (!File.Exists(xmlPath))
                return false;

            LoadPropertiesXml(xmlPath);

            foreach (var stringFile in Directory.GetFiles(Path.GetDirectoryName(xmlPath), @"*.xml"))
            {
                if (string.Compare(stringFile, xmlPath, true) == 0)
                    continue;

                var stringKey = Path.GetFileNameWithoutExtension(stringFile);

                var stringCollection = new StringCollection(stringKey);
                stringCollection.LoadFromFile(stringFile);

                _stringMap[stringKey] = stringCollection;
            }

            return true;
        }

        public bool ExportToFile(string path)
        {

            return true;
        }
    }
}
