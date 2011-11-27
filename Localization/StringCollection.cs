using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NeoSmart.Localization
{
	public class StringCollection
	{
		public string Key { get; private set; }

		public string this[string key]
		{
			get
			{
				try
				{
					return StringsTable[key].Value;
				}
				catch(KeyNotFoundException)
				{
					throw new StringNotFoundException();
				}
			}
			set
			{
				if (StringsTable.ContainsKey(key))
				{
					StringsTable[key] = new StringTranslation(key, value);
				}
				else
				{
					StringsTable[key].Value = value;
				}
			}
		}

		public Dictionary<string, StringTranslation> StringsTable
		{
			get { return _strings; }
		}

		private readonly Dictionary<string, StringTranslation> _strings = new Dictionary<string, StringTranslation>();

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

			foreach (var entry in _strings.Values)
			{
				var stringNode = xmlDocument.CreateElement(@"string");

				stringNode.SetAttribute(@"key", entry.Key);
				stringNode.SetAttribute(@"value", entry.Value);

				if (entry.BumpVersion)
					++entry.Version;

				if (entry.Version != 0)
					stringNode.SetAttribute(@"version", entry.Version.ToString());

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

					var key = text.Attributes["key"];
					if(key == null || string.IsNullOrEmpty(key.InnerText))
						throw new MalformedStringException("Invalid translation string found. Attributes 'key' and 'value' are required.");

					if (_strings.ContainsKey(key.InnerText))
						throw new DuplicateKeyException(string.Format("Key {0} in {1}\\{2} was defined more than once.", key.InnerText, localeKey, Key));

					var value = text.Attributes["value"];
					if (value == null)
						throw new MalformedStringException("Invalid translation string found. Attributes 'key' and 'value' are required.");

					var newString = new StringTranslation(key.InnerText, value.InnerText);
					_strings[newString.Key] = newString;

					var version = text.Attributes["version"];
					if (version != null)
					{
						newString.Version = uint.Parse(version.InnerText);
					}
				}
			}
		}

		public void Merge(StringCollection newStrings, bool overwriteExisting = true)
		{
			Merge(newStrings.StringsTable.Values, overwriteExisting);

			return;
		}

		public void Merge(IEnumerable<StringTranslation> newStrings, bool overwriteExisting = true)
		{
			foreach (var entry in newStrings)
			{
				if (StringsTable.ContainsKey(entry.Key) && !overwriteExisting)
					continue;

				StringsTable[entry.Key] = entry;
			}
		}
	}
}
