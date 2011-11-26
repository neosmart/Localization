using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NeoSmart.Localization;
using System.IO;

namespace NLTUI
{
	public partial class MainForm : Form
	{
		private LocaleManager _localeManager;

		public MainForm()
		{
			InitializeComponent();
		}

		private void LoadLocales()
		{
			foreach(var locale in _localeManager.Locales.Values)
			{
				var entry = new ToolStripMenuItem(string.IsNullOrEmpty(locale.Name) ? locale.Key : locale.Name);
				entry.CheckOnClick = true;
				entry.CheckStateChanged += LocaleChanged;
				
				if(locale.Key == _localeManager.DefaultLocale)
				{
					entry.Checked = true;
				}

				cbxLocales.DropDownItems.Add(entry);
			}
		}

		void LocaleChanged(object sender, EventArgs e)
		{
			var item = ((ToolStripMenuItem) sender);
			if(!item.Checked)
				return;

			cbxLocales.Text = item.Text;
			
			foreach(var other in cbxLocales.DropDownItems)
			{
				if (other == sender)
					continue;

				((ToolStripMenuItem)other).Checked = false;
			}
		}

		private void ResetWorkspace()
		{
			tbxTranslations.TabPages.Clear();
			cbxLocales.DropDownItems.Clear();
			cbxLocales.Text = string.Empty;
		}

		private void CreateDocuments()
		{
			var defaultLocale = _localeManager.Locales[_localeManager.DefaultLocale];
			_localeManager.SetLocale(_localeManager.DefaultLocale);

			foreach(var stringTable in defaultLocale.StringCollections)
			{
				var translator = CreateTranslator(defaultLocale, stringTable.Key);
				translator.LoadKeys(defaultLocale, stringTable.Key);
			}
		}

		private Translator CreateTranslator(Locale locale, string displayName)
		{
			var tabPage = new TabPage();
			tabPage.Text = displayName;

			var translator = new Translator(_localeManager, !string.IsNullOrEmpty(locale.ParentLocale) ? _localeManager.Locales[locale.ParentLocale] : null);

			tabPage.Controls.Add(translator);
			translator.Dock = DockStyle.Fill;
			
			tbxTranslations.TabPages.Add(tabPage);

			return translator;
		}

		private LoadFolderResult OpenFolder(string localeFolder)
		{
			_localeManager = new LocaleManager
			                 	{
			                 		LocaleRoot = localeFolder
			                 	};
			_localeManager.LoadLocales();

			if(_localeManager.Locales.Count == 0)
			{
				var result =
					MessageBox.Show(
						"No valid locales were detected in the selected folder. This is either an empty project, or the wrong folder was selected.\r\n\r\nDo you want to continue loading this project?",
						"No Locales Found", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

				if(result == DialogResult.Yes)
					return LoadFolderResult.Success;

				_localeManager = null;

				if(result == DialogResult.No)
					return LoadFolderResult.Retry;

				if(result == DialogResult.Cancel)
					return LoadFolderResult.Cancel;
			}

			return LoadFolderResult.Success;
		}

		private void newToolStripButton_Click(object sender, EventArgs e)
		{

		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			LoadFolderResult loadResult;
			var folderDialog = new FolderBrowserDialog();
			folderDialog.Description = "Choose a folder containing all the various locale folders to load a project. Typically, this folder will be called \"lang\"";

			if (!string.IsNullOrEmpty(Properties.Settings.Default.LastBrowse))
			{
				if (Directory.Exists(Properties.Settings.Default.LastBrowse))
					folderDialog.SelectedPath = Properties.Settings.Default.LastBrowse;
			}

			do
			{
				var result = folderDialog.ShowDialog();
				if (result == DialogResult.Cancel)
					return;

				loadResult = OpenFolder(folderDialog.SelectedPath);
			} while (loadResult == LoadFolderResult.Retry);

			if (loadResult == LoadFolderResult.Cancel)
				return;

			Properties.Settings.Default.LastBrowse = folderDialog.SelectedPath;
			Properties.Settings.Default.Save();

			ResetWorkspace();

			LoadLocales();
			CreateDocuments();
		}
	}
}
