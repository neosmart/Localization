using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NeoSmart.Localization;

namespace NLTUI
{
	public partial class Translator : UserControl
	{
		private Locale _locale;
		private string _collectionKey;
		private Locale _parentLocale;

		public Translator(Locale locale, Locale parentLocale = null)
		{
			_locale = locale;
			_parentLocale = parentLocale;
			InitializeComponent();
		}

		public void LoadKeys(Locale loadFrom, string collectionKey)
		{
			_collectionKey = collectionKey;

			var enumerable = loadFrom.StringCollections[collectionKey].StringsTable.Keys;
			foreach (var key in enumerable)
			{
				lstKeys.Items.Add(new ListViewItem(key));
			}
		}

		private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstKeys.SelectedItems.Count == 0)
				return;

			txtOld.Text = _parentLocale != null ? _parentLocale.GetString(_collectionKey, lstKeys.SelectedItems[0].Text) : string.Empty;
			txtNew.Text = _locale.GetString(_collectionKey, lstKeys.SelectedItems[0].Text);
		}
	}
}
