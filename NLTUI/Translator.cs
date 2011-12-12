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
		private LocaleManager _localeManager;
		private Locale _parentLocale;
		private Locale _locale;
		private StringTranslation _lastTranslation;
		private StringCollection _collection;
		private bool _forceModified;

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

			if(_locale.StringCollections.ContainsKey(collectionKey) == false)
			{
				_locale.StringCollections.Add(collectionKey, new StringCollection(collectionKey));
			}
			_collection = _locale.StringCollections[collectionKey];
			txtNew.RightToLeft = _locale.RightToLeft ? RightToLeft.Yes : RightToLeft.No;

			var enumerable = loadFrom.StringCollections[collectionKey].StringsTable.Values;
			foreach (var key in enumerable)
			{
				if (key.AliasedKey)
					continue;

				var item = new ListViewItem(key.Key);

				item.ImageKey = GetStatusIcon(collectionKey, key.Key);
				
				lstKeys.Items.Add(item);
			}

			colKey.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

			chkMinorUpdate.Visible = _parentLocale == null;
			chkDerived.Enabled = _parentLocale != null;
			chkUpToDate.Visible = _parentLocale != null;
			btnSetModified.Visible = _parentLocale == null;
		}

		private string GetStatusIcon(string collectionKey, string key)
		{
			switch (_localeManager.GetStringStatus(_locale, collectionKey, key))
			{
				case StringStatus.UpToDate:
					return @"green";
				case StringStatus.Outdated:
					return @"orange";
				case StringStatus.Missing:
					return @"red";
			}

			return string.Empty;
		}
		private void StoreCurrentData()
		{
			if (_lastTranslation == null)
				return;

			if (chkDerived.Visible)
			{
				_lastTranslation.DeriveFromParent = chkDerived.Checked;
			}

			if(chkMinorUpdate.Visible)
			{
				_lastTranslation.BumpVersion = !chkMinorUpdate.Checked;
			}

			if (!_lastTranslation.DeriveFromParent)
			{
				_lastTranslation.Value = txtNew.Text;
			}

			if (chkUpToDate.Visible)
			{
				_lastTranslation.BumpVersion = chkUpToDate.Checked;
			}
		}

		private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstKeys.SelectedItems.Count == 0)
				return;

			var selectedKey = lstKeys.SelectedItems[0].Text;

			if(_lastTranslation != null)
			{
				StoreCurrentData();
			}

			txtOld.Text = _parentLocale != null ? _parentLocale.GetString(_collection.Key, selectedKey) : string.Empty;
			try
			{
				txtNew.Text = _collection[selectedKey];
			}
			catch (StringNotFoundException)
			{
				_collection.StringsTable.Add(selectedKey, new StringTranslation(selectedKey, string.Empty));
				txtNew.Text = _collection[selectedKey];
			}

			_lastTranslation = _collection.StringsTable[selectedKey];

			chkDerived.Checked = _lastTranslation.DeriveFromParent;

			if (chkMinorUpdate.Visible)
			{
				if (_forceModified)
				{
					chkMinorUpdate.Checked = true;
				}
				else
				{
					chkMinorUpdate.Checked = !_lastTranslation.BumpVersion;
				}
			}
			else if(chkUpToDate.Visible)
			{
				chkUpToDate.Checked = _lastTranslation.BumpVersion;
			}
		}

		private void chkDerived_CheckedChanged(object sender, EventArgs e)
		{
			txtNew.Enabled = !chkDerived.Checked;
			txtNew.Text = chkDerived.Checked ? txtOld.Text : _lastTranslation.Value;
		}

		public void Save()
		{
			StoreCurrentData();
			_locale.Save(_collection, btnSetModified.Checked);
		}

		private void btnSetModified_Click(object sender, EventArgs e)
		{
			_forceModified = btnSetModified.Checked;
			chkMinorUpdate.Enabled = !btnSetModified.Checked;
		}

		private void chkUpToDate_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUpToDate.Visible && _lastTranslation != null)
			{
				//Need to update icons
				lstKeys.SelectedItems[0].ImageKey = chkUpToDate.Checked ? @"green" : GetStatusIcon(_collection.Key, lstKeys.SelectedItems[0].Text);
			}
		}
	}
}
