using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NeoSmart.Localization
{
    public class StringCollection
    {
        public string Key
        {
            get; private set;
        }

        public Dictionary<string, string> StringsTable
        {
            get { return _strings; }
        }

        private readonly Dictionary<string, string> _strings = new Dictionary<string, string>(); 

        public StringCollection(string key)
        {
            Key = key;
        }

        public void ExportToFile(string xmlPath)
        {

        }

        public void LoadFromFile(string xmlPath)
        {
            Key = Path.GetFileNameWithoutExtension(xmlPath);
            string localeKey = Path.GetFileName(Path.GetDirectoryName(xmlPath));

            XmlNode node;
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            node = xmlDocument.SelectSingleNode(@"/localization/strings");
            if (node == null)
                throw new IncompleteLocaleException("The required locale element 'strings' was not found.");

            var strings = xmlDocument.SelectNodes(@"/localization/strings/string");
            if (strings != null)
            {
                foreach (XmlNode text in strings)
                {
                    if (text.Attributes == null)
                        throw new MalformedStringException("Invalid translation string found. Attributes 'name' and 'key' are required.");

                    string key = text.Attributes["key"].InnerText;
                    if(string.IsNullOrEmpty(key))
                        continue;

                    if (_strings.ContainsKey(key))
                        throw new DuplicateKeyException(string.Format("Key {0} in {1}\\{2} was defined more than once.", key, localeKey, Key));

                    _strings.Add(key, text.Attributes["value"].InnerText);
                }
            }
        }
    }
}
