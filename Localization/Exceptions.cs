using System;
using System.Collections.Generic;

namespace NeoSmart.Localization
{
	public class LocalizationException : Exception
	{
		public LocalizationException(string message = "")
			: base(message)
		{
		}
	}

	public class DefaultLocaleNotFoundException : LocalizationException
	{
	}

	public class IncompleteLocaleException : LocalizationException
	{
		public IncompleteLocaleException(string message = "")
			: base(message)
		{
		}
	}

	public class MalformedStringException : LocalizationException
	{
		public MalformedStringException(string message = "")
			: base(message)
		{
		}
	}

	public class DuplicateKeyException : LocalizationException
	{
		public DuplicateKeyException(string message = "")
			: base(message)
		{
		}
	}

	public class DefaultCollectionKeyNotSet : LocalizationException
	{
		public DefaultCollectionKeyNotSet(string message = "")
			: base(message)
		{
		}
	}

	public class StringNotFoundException : LocalizationException
	{
	    public string Key;
	    public string Collection;
		public StringNotFoundException(string key, string collection, string language = "")
			: base(string.Format("String \"{0}\" was not found in collection \"{1}\" for language {2}!", key, collection, language))
		{
		    Key = key;
		    Collection = collection;
		}
	}
}
