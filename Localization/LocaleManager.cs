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
		private static string _currentLocale = @"";
		private static readonly Dictionary<string, Locale> LocalesMap = new Dictionary<string, Locale>();

		public string CurrentLocale { get { return _currentLocale; } private set { _currentLocale = value; } }

		public string DefaultCollectionKey { get; set; }

		public string DefaultLocale
		{
			get { return _defaultLocale; }
			set { lock (_defaultLocale) _defaultLocale = value; }
		}

		public Dictionary<string, Locale> Locales
		{
			get { return LocalesMap; }
		}

		public string PropertiesXml { get { return _propertiesXml; } }

		public string LocaleRoot
		{
			get { return _transFolder; }
			set
			{
				lock (_transFolder)
				{
					_transFolder = Path.IsPathRooted(value) ? value : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value);
				}
			}
		}

		public LocaleManager(string defaultCollectionKey = null, string defaultLocale = @"en-US", string localizationFolder = @"lang",
		                     string propertiesXml = @"properties.xml")
		{
			LocaleRoot = localizationFolder;

			lock (_propertiesXml)
			{
				_propertiesXml = propertiesXml;
			}

			lock (_defaultLocale)
			{
				_defaultLocale = defaultLocale;
			}

			DefaultCollectionKey = defaultCollectionKey;
		}

		public void LoadLocales()
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

					var propertiesXml = Path.Combine(directory, _propertiesXml);

					if (!File.Exists(propertiesXml))
						continue;

					var locale = new Locale(localeKey);
					locale.Load(propertiesXml);

					LocalesMap.Add(localeKey, locale);
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

			CurrentLocale = localeKey;

			LoadLocale(localeKey);

			if (localeKey != _defaultLocale)
			{
				if (!LoadLocale(_defaultLocale))
					throw new DefaultLocaleNotFoundException();

				if(string.IsNullOrEmpty(Locales[CurrentLocale].ParentLocale))
				{
					Locales[CurrentLocale].ParentLocale = DefaultLocale;
				}
			}

			return true;
		}

		private bool LoadLocale(string localeKey)
		{
			var localeFolder = Path.Combine(_transFolder, localeKey);
			if (!Directory.Exists(localeFolder))
				return false;

			if (Locales.Count == 0)
			{
				LoadLocales();
			}

			bool result = LocalesMap[localeKey].Load(Path.Combine(localeFolder, _propertiesXml));

			return result;
		}

		public StringStatus GetStringStatus(Locale locale, string collectionKey, string key)
		{
			StringTranslation translation;

			if (!locale.StringCollections[collectionKey].StringsTable.TryGetValue(key, out translation))
				return StringStatus.Missing;

			if (translation.DeriveFromParent)
				return StringStatus.UpToDate;

			if (translation.Version == LocalesMap[_defaultLocale].StringCollections[collectionKey].StringsTable[key].Version)
				return StringStatus.UpToDate;

			return StringStatus.Outdated;
		}

		public string GetString(string key, bool useFallback = false, string fallback = null)
		{
			CheckDefaultCollectionKey();
			return GetString(DefaultCollectionKey, key, useFallback ? fallback : null);
		}

		public string GetString(string collectionKey, string key, string fallback = null)
		{
			var localeKey = string.IsNullOrEmpty(CurrentLocale) ? _defaultLocale : CurrentLocale;
			
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
				catch (StringNotFoundException)
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

		private void CheckDefaultCollectionKey()
		{
			if(DefaultCollectionKey == null)
			{
				throw new DefaultCollectionKeyNotSet("An attempt to load keys without specifying the collection key was made, and no default collection key was previously set.");
			}
		}

		public string [] GetStrings(string [] keys)
		{
			CheckDefaultCollectionKey();
			return GetStrings(DefaultCollectionKey, keys);
		}

		public string [] GetStrings(string collectionKey, string [] keys)
		{
			var results = new List<string>(keys.Length);

			foreach(string key in keys)
			{
				results.Add(GetString(collectionKey, key));
			}

			return results.ToArray();
		}
	}
}
