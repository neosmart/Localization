﻿using System;
using System.Collections.Generic;
using System.IO;

namespace NeoSmart.Localization
{
    public class LocaleManager
    {
        private static string _transFolder = @"";
        private static string _defaultLocale = @"";
        private static string _propertiesXml = @"";
        private static readonly Dictionary<string, Locale> LocalesMap = new Dictionary<string, Locale>();

        private string _currentLocale;
        private bool _defaultLoaded;

        public static string DefaultLocale
        {
            get { return _defaultLocale; }
            set { lock (_defaultLocale) _defaultLocale = value; }
        }

        public static IEnumerable<string> Locales
        {
            get { return LocalesMap.Keys; }
        }

        public LocaleManager(string localizationFolder = @"lang", string propertiesXml = @"properties.xml",
                             string defaultLocale = @"en-US")
        {
            lock (_transFolder)
            {
                _transFolder = Path.IsPathRooted(localizationFolder)
                                   ? localizationFolder
                                   : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localizationFolder);
            }

            lock (_propertiesXml)
            {
                _propertiesXml = propertiesXml;
            }

            lock (_defaultLocale)
            {
                _defaultLocale = defaultLocale;
            }
        }

        public static void LoadLocales()
        {
            lock (Locales)
            {
                LocalesMap.Clear();
                var directories = Directory.GetDirectories(_transFolder);
                foreach (var directory in directories)
                {
                    var localeKey = Path.GetFileName(directory);
                    if (string.IsNullOrEmpty(localeKey))
                        continue;

                    if (!File.Exists(Path.Combine(directory, _propertiesXml)))
                        continue;

                    LocalesMap.Add(localeKey, new Locale(localeKey));
                }
            }
        }

        public bool SetLocale(string localeKey)
        {
            lock (Locales)
            {
                if (!LocalesMap.ContainsKey(localeKey))
                    return false;
            }

            _currentLocale = localeKey;
            _defaultLoaded = false;

            LoadLocale(localeKey);
            if (!LoadLocale(_defaultLocale))
                throw new DefaultLocaleNotFoundException();

            return true;
        }

        private bool LoadLocale(string localeKey)
        {
            if (localeKey == _defaultLocale)
            {
                if (_defaultLoaded)
                    return true;
                _defaultLoaded = true;
            }

            var localeFolder = Path.Combine(_transFolder, localeKey);
            if (!Directory.Exists(localeFolder))
                return false;

            return LocalesMap[localeKey].Load(Path.Combine(localeFolder, _propertiesXml));
        }

        public string GetString(string key, string fallback = null)
        {
            if (string.IsNullOrEmpty(_currentLocale))
            {
                LoadLocale(_defaultLocale);

                if (string.IsNullOrEmpty(_currentLocale))
                    return fallback;
            }

            var locale = LocalesMap[_currentLocale];

            while (true)
            {
                try
                {
                    return locale.GetString(key);
                }
                catch (KeyNotFoundException)
                {
                    if (!string.IsNullOrEmpty(locale.ParentLocale))
                    {
                        locale = LocalesMap[locale.ParentLocale];
                        continue;
                    }

                    if (fallback != null)
                    {
                        return fallback;
                    }

                    throw new StringNotFoundException();
                }
            }
        }

        public string SetText(System.Windows.Forms.Control control)
        {
            return control.Text = GetString(control.Name);
        }
    }
}
