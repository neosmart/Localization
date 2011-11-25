using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NeoSmart.Localization
{
	public class StringCollection
	{
		public string Key { get; private set; }

		public Dictionary<string, string> StringsTable
		{
			get { return _strings; }
		}

		private readonly Dictionary<string, string> _strings = new Dictionary<string, string>();

		public StringCollection(string key)
		{
			Key = key;
		}

		public void Save(string xmlPath)
		{
			var xmlDocument = new XmlDocument();

			var xmlDeclaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"utf-8", null);

			xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);

			var xmlNode = xmlDocument.AppendChild(xmlDocument.CreateElement(@"localization"));
			xmlNode = xmlNode.AppendChild(xmlDocument.CreateElement(@"strings"));

			foreach (var entry in _strings)
			{
				var stringNode = xmlDocument.CreateElement(@"string");

				stringNode.SetAttribute(@"key", entry.Key);
				stringNode.SetAttribute(@"value", entry.Value);

				xmlNode.AppendChild(stringNode);
			}

			xmlDocument.Save(xmlPath);
		}

		public void Load(string xmlPath)
		{
			Key = Path.GetFileNameWithoutExtension(xmlPath);
			string localeKey = Path.GetFileName(Path.GetDirectoryName(xmlPath));

			var xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlPath);

			var node = xmlDocument.SelectSingleNode(@"/localization/strings");
			if (node == null)
				throw new IncompleteLocaleException("The required locale element 'strings' was not found.");

			var strings = xmlDocument.SelectNodes(@"/localization/strings/string");
			if (strings != null)
			{
				foreach (XmlNode text in strings)
				{
					if (text.Attributes == null)
						throw new MalformedStringException("Invalid translation string found. Attributes 'key' and 'value' are required.");

					string key = text.Attributes["key"].InnerText;
					if (string.IsNullOrEmpty(key))
						continue;

					if (_strings.ContainsKey(key))
						throw new DuplicateKeyException(string.Format("Key {0} in {1}\\{2} was defined more than once.", key, localeKey, Key));

					_strings.Add(key, text.Attributes["value"].InnerText);
				}
			}
		}

		public void Merge(StringCollection newStrings, bool overwriteExisting = true)
		{
			Merge(newStrings.StringsTable, overwriteExisting);

			return;
		}

		public void Merge(Dictionary<string, string> newStrings, bool overwriteExisting = true)
		{
			foreach (var entry in newStrings)
			{
				if (StringsTable.ContainsKey(entry.Key) && !overwriteExisting)
					continue;

				StringsTable[entry.Key] = entry.Value;
			}
		}
	}
}
