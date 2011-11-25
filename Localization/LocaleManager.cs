using System;
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

		public static string DefaultLocale
		{
			get { return _defaultLocale; }
			set { lock (_defaultLocale) _defaultLocale = value; }
		}

		public static IEnumerable<string> Locales
		{
			get { return LocalesMap.Keys; }
		}

		public LocaleManager(string localizationFolder = @"lang", string propertiesXml = @"properties.xml", string defaultLocale = @"en-US")
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

			LoadLocale(localeKey);
			if (!LoadLocale(_defaultLocale))
				throw new DefaultLocaleNotFoundException();

			return true;
		}

		private bool LoadLocale(string localeKey)
		{
			var localeFolder = Path.Combine(_transFolder, localeKey);
			if (!Directory.Exists(localeFolder))
				return false;

            if (LocalesMap.ContainsKey(localeKey))
                return true;

			LocalesMap.Add(localeKey, new Locale(localeKey));

			bool result = LocalesMap[localeKey].Load(Path.Combine(localeFolder, _propertiesXml));

			return result;
		}

		public string GetString(string collectionKey, string key, string fallback = null)
		{
			var localeKey = string.IsNullOrEmpty(_currentLocale) ? _defaultLocale : _currentLocale;
			
			if (!LocalesMap.ContainsKey(localeKey))
			{
				LoadLocale(localeKey);
			}

			var locale = LocalesMap[localeKey];

			while (true)
			{
				try
				{
					return locale.GetString(collectionKey, key);
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
	}
}
