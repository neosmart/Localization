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
		private List<Translator> _translators;
		private string _folder;

		public MainForm()
		{
			InitializeComponent();
		    FixFonts(this);

			toolStrip1.Location = new Point(0, 0);
			toolStrip2.Location = new Point(1, 0);
		}

	    private static void FixFonts(Control c)
	    {
            c.Font = SystemFonts.MessageBoxFont;
            foreach (var child in c.Controls)
            {
                FixFonts((Control)child);
            }
        }

		private void FillLocalesMenu(string selectedKey)
		{
			foreach(var locale in _localeManager.Locales.Values)
			{
				var entry = new ToolStripMenuItem(locale.Key);
				entry.Checked = locale.Key == selectedKey;
				entry.CheckOnClick = true;
				entry.CheckStateChanged += LocaleChanged;

				cbxLocales.DropDownItems.Add(entry);
			}
			cbxLocales.Text = selectedKey;
		}

		void LocaleChanged(object sender, EventArgs e)
		{
			var item = ((ToolStripMenuItem) sender);
			ChangeLocale(_localeManager.Locales[item.Text]);
		}

		private void ResetTranslators()
		{
			tbxTranslations.TabPages.Clear();
			_translators = new List<Translator>();
		}

		private void ResetWorkspace()
		{
			ResetTranslators();
			cbxLocales.DropDownItems.Clear();
			cbxLocales.Text = string.Empty;
		}

		private void ChangeLocale(Locale locale)
		{
			var dialogResult = MessageBox.Show("Do you want to save your changes to this locale's strings?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Cancel)
				return;
			if (dialogResult == DialogResult.Yes)
				Save();

			_localeManager.LoadLocales();
			_localeManager.SetLocale(locale.Key);
			locale = _localeManager.Locales[locale.Key];

			ResetWorkspace();
			FillLocalesMenu(locale.Key);
			CreateDocuments(locale);
		}

		private void CreateDocuments(Locale newLocale = null)
		{
			var defaultLocale = _localeManager.Locales[_localeManager.DefaultLocale];

			if(newLocale == null)
			{
				newLocale = defaultLocale;
			}

			foreach(var stringTable in defaultLocale.StringCollections)
			{
				var translator = CreateTranslator(newLocale, stringTable.Key);
				translator.LoadKeys(defaultLocale, stringTable.Key);
				_translators.Add(translator);
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

		private void Open()
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

			_folder = folderDialog.SelectedPath;
			Properties.Settings.Default.LastBrowse = folderDialog.SelectedPath;
			Properties.Settings.Default.Save();

			ResetWorkspace();
			_localeManager.SetLocale(_localeManager.DefaultLocale);
			FillLocalesMenu(_localeManager.DefaultLocale);
			CreateDocuments();
		}

		private void NewLocale()
		{
			var dialog = new NewLocaleDialog();
			dialog.ShowDialog();

			if (dialog.Result == DialogResult.Cancel)
				return;

			var newLocale = new Locale(dialog.LocaleKey);
			newLocale.ParentLocale = _localeManager.DefaultLocale;
			newLocale.Name = dialog.LocaleName;

			var newFolder = Path.Combine(_folder, dialog.LocaleKey);
			if (!Directory.Exists(newFolder))
				Directory.CreateDirectory(newFolder);

			newLocale.Save(Path.Combine(newFolder, _localeManager.PropertiesXml));

			ChangeLocale(newLocale);
		}

		private void Save()
		{
			if (tbxTranslations.SelectedIndex == -1)
				return;

			_translators[tbxTranslations.SelectedIndex].Save();
		}

		private void SaveAll()
		{
			foreach (var translator in _translators)
			{
				translator.Save();
			}
		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			Open();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void btnNewLocale_Click(object sender, EventArgs e)
		{
			NewLocale();
		}

		private void btnSaveAll_Click(object sender, EventArgs e)
		{
			SaveAll();
		}

		private void cleanupLocaleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(_localeManager.CurrentLocale))
				return;

			_localeManager.Locales[_localeManager.CurrentLocale].Cleanup();
		}

        private void findMissingStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder missing = new StringBuilder();
            foreach (var translation in _localeManager.Locales.Values)
            {
                if (translation.Key == _localeManager.DefaultLocale)
                    continue;

                translation.Load(translation.XmlPath);
                
                foreach (var sCollection in _localeManager.Locales[_localeManager.DefaultLocale].StringCollections)
                {
                    int found = 0;
                    foreach(var sPair in sCollection.Value.StringsTable)
                    {
                        if (sPair.Value.AliasedKey || sPair.Key == @"MinimumWidth" || sPair.Key == @"MinimumHeight")
                            continue;

                        if (translation.StringCollections.ContainsKey(sCollection.Key))
                        {
                            if (translation.StringCollections[sCollection.Key].StringsTable.ContainsKey(sPair.Key))
                            {
                                continue;
                            }
                        }

                        if (found++ == 0)
                        {
                            missing.AppendFormat("{0}:{1}\n", translation.Key, sCollection.Key);
                        }
                        missing.AppendFormat("\t{0}\n", sPair.Key);
                    }
                }
            }

            MessageBox.Show(missing.ToString());
        }

        private void findoutdatedStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder outdated = new StringBuilder();
            foreach (var translation in _localeManager.Locales.Values)
            {
                if (translation.Key == _localeManager.DefaultLocale)
                    continue;

                translation.Load(translation.XmlPath);

                foreach (var sCollection in _localeManager.Locales[_localeManager.DefaultLocale].StringCollections)
                {
                    int found = 0;
                    foreach (var sPair in sCollection.Value.StringsTable)
                    {
                        if (sPair.Value.AliasedKey || sPair.Key == @"MinimumWidth" || sPair.Key == @"MinimumHeight")
                            continue;

                        if (translation.StringCollections.ContainsKey(sCollection.Key))
                        {
                            if (translation.StringCollections[sCollection.Key].StringsTable.ContainsKey(sPair.Key))
                            {
                                if (translation.StringCollections[sCollection.Key].StringsTable[sPair.Key].Version < sPair.Value.Version
                                    && !translation.StringCollections[sCollection.Key].StringsTable[sPair.Key].DeriveFromParent)
                                {
                                    if (found++ == 0)
                                    {
                                        outdated.AppendFormat("{0}:{1}\n", translation.Key, sCollection.Key);
                                    }
                                    outdated.AppendFormat("\t{0}\n", sPair.Key);
                                }
                            }
                        }
                    }
                }
            }

            MessageBox.Show(outdated.ToString());
        }
	}
}
