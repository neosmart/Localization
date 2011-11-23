using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
