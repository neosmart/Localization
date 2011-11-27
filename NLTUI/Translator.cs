﻿using System;
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
		private LocaleManager _localeManager;
		private Locale _parentLocale;
		private Locale _locale;
		private StringTranslation _lastTranslation;
		private StringCollection _collection;

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
			_locale = _localeManager.Locales[_localeManager.CurrentLocale];
			_collection = _locale.StringCollections[collectionKey];

			var enumerable = loadFrom.StringCollections[collectionKey].StringsTable.Keys;
			foreach (var key in enumerable)
			{
				var item = new ListViewItem(key);

				switch (_localeManager.GetStringStatus(_locale, collectionKey, key))
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

			var selectedKey = lstKeys.SelectedItems[0].Text;

			if(_lastTranslation != null)
			{
				_lastTranslation.DeriveFromParent = chkDerived.Checked;
				_lastTranslation.BumpVersion = !chkMinorUpdate.Checked;

				if(!_lastTranslation.DeriveFromParent)
				{
					_lastTranslation.Value = txtNew.Text;
				}
			}

			txtOld.Text = _parentLocale != null ? _parentLocale.GetString(_collection.Key, selectedKey) : string.Empty;
			txtNew.Text = _collection[selectedKey];

			_lastTranslation = _collection.StringsTable[selectedKey];

			chkDerived.Checked = _lastTranslation.DeriveFromParent;
			chkMinorUpdate.Checked = !_lastTranslation.BumpVersion;
		}

		private void chkDerived_CheckedChanged(object sender, EventArgs e)
		{
			txtNew.Enabled = !chkDerived.Checked;
			if(chkDerived.Checked)
			{
				txtNew.Text = txtOld.Text;
			}
			else
			{
				txtNew.Text = _lastTranslation.Value;
			}
		}
	}
}
