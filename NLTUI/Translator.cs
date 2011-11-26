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
		private string _collectionKey;
		private LocaleManager _localeManager;
		private Locale _parentLocale;
		private Locale _locale;

		public Translator(LocaleManager localeManager, Locale parentLocale = null)
		{
			_localeManager = localeManager;
			_parentLocale = parentLocale;
			InitializeComponent();

			imageList1.Images.Add(@"red", Properties.Resources.red);
			imageList1.Images.Add(@"orange", Properties.Resources.orange);
			imageList1.Images.Add(@"green", Properties.Resources.green);
		}

		public void LoadKeys(Locale loadFrom, string collectionKey)
		{
			_collectionKey = collectionKey;
			_locale = _localeManager.Locales[_localeManager.CurrentLocale];

			var enumerable = loadFrom.StringCollections[collectionKey].StringsTable.Keys;
			foreach (var key in enumerable)
			{
				var item = new ListViewItem(key);

				switch (_localeManager.GetStringStatus(_locale, _collectionKey, key))
				{
					case StringStatus.UpToDate:
						item.ImageKey = @"green";
						break;
					case StringStatus.Outdated:
						item.ImageKey = @"orange";
						break;
					case StringStatus.Missing:
						item.ImageKey = @"red";
						break;
				}

				lstKeys.Items.Add(item);
			}

			colKey.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstKeys.SelectedItems.Count == 0)
				return;

			txtOld.Text = _parentLocale != null ? _parentLocale.GetString(_collectionKey, lstKeys.SelectedItems[0].Text) : string.Empty;
			txtNew.Text = _localeManager.GetString(_collectionKey, lstKeys.SelectedItems[0].Text);
		}
	}
}
