using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Localization
{
	public class StringTranslation
	{
		public string Key { get; set; }

		public string Value { get; set; }

		public bool DeriveFromParent { get; set; }

		public int Version { get; set; }

		public StringTranslation(string key, string value)
		{
			Key = key;
			Value = value;
		}

		public int BumpVersion()
		{
			return ++Version;
		}
	}
}
