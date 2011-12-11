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

				switch (_localeManager.GetStringStatus(_locale, collectionKey, key.Key))
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

			chkMinorUpdate.Visible = _parentLocale == null;
			chkDerived.Enabled = _parentLocale != null;
			chkUpToDate.Visible = _parentLocale != null;
			btnSetModified.Visible = _parentLocale == null;
		}

		private void StoreCurrentData()
		{
			if (_lastTranslation == null)
				return;

			_lastTranslation.DeriveFromParent = chkDerived.Checked;
			_lastTranslation.BumpVersion = !chkMinorUpdate.Checked;

			if (!_lastTranslation.DeriveFromParent)
			{
				_lastTranslation.Value = txtNew.Text;
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

			if(_forceModified)
			{
				chkMinorUpdate.Checked = true;
			}
			else
			{
				chkMinorUpdate.Checked = !_lastTranslation.BumpVersion;
			}
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

		public void Save()
		{
			StoreCurrentData();
			_locale.Save(btnSetModified.Checked);
		}

		private void btnSetModified_Click(object sender, EventArgs e)
		{
			_forceModified = btnSetModified.Checked;
			chkMinorUpdate.Enabled = !btnSetModified.Checked;
		}
	}
}
