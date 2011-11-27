using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NLTUI
{
	public partial class NewLocaleDialog : Form
	{
		public DialogResult Result { get; private set; }
		public string LocaleName { get; private set; }
		public string LocaleKey { get; private set; }

		public NewLocaleDialog()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Result = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if(txtKey.Text.Length == 0)
			{
				errorKey.SetError(lblBlank, "This is a required field!");
				return;
			}

			if(txtKey.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
			{
				var invalidChars = string.Empty;
				foreach(var c in Path.GetInvalidFileNameChars())
					invalidChars += c;

				errorKey.SetError(lblBlank, string.Format("{0} {1}", "Locale key may not contain any of these characters:", invalidChars));
				return;
			}

			LocaleKey = txtKey.Text;
			LocaleName = txtName.Text;

			Result = DialogResult.OK;
			Close();
		}
	}
}
