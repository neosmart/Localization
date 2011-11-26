using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NLTUI
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			AddDocument("Document 1");
		}

		private void AddDocument(string title)
		{
			var tabPage = new TabPage();
			tabPage.Text = title;

			var translator = new Translator();

			tabPage.Controls.Add(translator);
			translator.Dock = DockStyle.Fill;
			
			tbxTranslations.TabPages.Add(tabPage);
		}
	}
}
