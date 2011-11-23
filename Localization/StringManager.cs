using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NeoSmart.Localization
{
    public class StringManager
    {
        private static string _transFolder = "";
        private static string _defaultLocale = "en-US";

        public static string DefaultLocale
        {
            get { return _defaultLocale; }
            set { lock (_defaultLocale) _defaultLocale = value; }
        }

        public string Name { get; private set; }
        public bool RightToLeft { get; private set; }

        private string _currentLocale;
        private bool _propertiesLoaded;
        private bool _defaultLoaded;
        private readonly string _key;
        private readonly Dictionary<string, string> _strings = new Dictionary<string, string>();

        private static readonly List<String> Locales = new List<string>();

        public static void LoadLocales(string localizationFolder = @"trans")
        {
            lock(_transFolder)
            {
                _transFolder = Path.IsPathRooted(localizationFolder)
                                   ? localizationFolder
                                   : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localizationFolder);
            }

            lock (Locales)
            {
                Locales.Clear();
                var directories = Directory.GetDirectories(_transFolder);
                foreach(var directory in directories)
                {
                    Locales.Add(Path.GetFileName(directory));
                }
            }
        }

        public StringManager(string key)
        {
            _key = key;
        }

        public bool SetLocale(string locale)
        {
            lock (Locales)
            {
                if (!Locales.Contains(locale))
                    return false;
            }

            _currentLocale = locale;
            _defaultLoaded = false;
            _propertiesLoaded = false;

            _strings.Clear();

            LoadLocale(locale);
            if(!LoadLocale(_defaultLocale))
                throw new DefaultLocaleNotFoundException();

            return true;
        }

        private bool LoadLocale(string locale)
        {
            while (!string.IsNullOrEmpty(locale))
            {
                if (locale == _defaultLocale)
                {
                    if (_defaultLoaded)
                        return true;
                    _defaultLoaded = true;
                }

                var localeFolder = Path.Combine(_transFolder, locale);
                if (!Directory.Exists(localeFolder))
                    return false;

                var localeFile = Path.Combine(localeFolder, _key + @".xml");
                if (!File.Exists(localeFile))
                    return false;

                XmlNode node;
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(localeFile);

                if (!_propertiesLoaded)
                {
                    node = xmlDocument.SelectSingleNode(@"/localization/name");
                    if(node == null)
                        throw new IncompleteLocaleException("The required locale element 'name' was not found.");
                    Name = node.InnerText;

                    node = xmlDocument.SelectSingleNode(@"/localization/rtl");
                    RightToLeft = node != null && node.InnerText == "true";

                    node = xmlDocument.SelectSingleNode(@"/localization/locale/formProperties");

                    _propertiesLoaded = true;
                    //TODO: Add any form properties here
                }

                node = xmlDocument.SelectSingleNode(@"/localization/parentLocale");
                locale = node != null ? node.InnerText : string.Empty;

                node = xmlDocument.SelectSingleNode(@"/localization/strings");
                if(node == null)
                    throw new IncompleteLocaleException("The required locale element 'strings' was not found.");

                var strings = xmlDocument.SelectNodes(@"/localization/strings/string");
                if(strings != null)
                {
                    foreach(XmlNode text in strings)
                    {
                        if(text.Attributes == null)
                            throw new MalformedStringException("Invalid translation string found. Attributes 'name' and 'key' are required.");

                        if(!_strings.ContainsKey(text.Attributes["key"].InnerText))
                            _strings.Add(text.Attributes["key"].InnerText, text.Attributes["value"].InnerText);
                    }
                }
            }

            return true;
        }

        public string GetString(string key, string fallback = null)
        {
            if(string.IsNullOrEmpty(_currentLocale))
            {
                LoadLocale(_defaultLocale);
            }

            try
            {
                return _strings[key];
            }
            catch (KeyNotFoundException)
            {
                if (fallback != null)
                    return fallback;

                throw new StringNotFoundException();
            }
        }

        public string SetText(System.Windows.Forms.Control control)
        {
            return control.Text = GetString(control.Name);
        }
    }
}
