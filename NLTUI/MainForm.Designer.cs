namespace NLTUI
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.tbxTranslations = new System.Windows.Forms.TabControl();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.txtLocaleSelector = new System.Windows.Forms.ToolStripLabel();
			this.cbxLocales = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnNew = new System.Windows.Forms.ToolStripButton();
			this.btnNewLocale = new System.Windows.Forms.ToolStripButton();
			this.btnOpen = new System.Windows.Forms.ToolStripButton();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.btnSaveAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.btnCut = new System.Windows.Forms.ToolStripButton();
			this.btnCopy = new System.Windows.Forms.ToolStripButton();
			this.btnPaste = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnHelp = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cleanupLocaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 485);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(764, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuTools,
            this.mnuHelp});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(764, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(37, 20);
			this.mnuFile.Text = "&File";
			// 
			// mnuEdit
			// 
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(39, 20);
			this.mnuEdit.Text = "&Edit";
			// 
			// mnuTools
			// 
			this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanupLocaleToolStripMenuItem});
			this.mnuTools.Name = "mnuTools";
			this.mnuTools.Size = new System.Drawing.Size(48, 20);
			this.mnuTools.Text = "&Tools";
			// 
			// mnuHelp
			// 
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.Size = new System.Drawing.Size(44, 20);
			this.mnuHelp.Text = "&Help";
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tbxTranslations);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(764, 411);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(764, 461);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// tbxTranslations
			// 
			this.tbxTranslations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbxTranslations.Location = new System.Drawing.Point(0, 0);
			this.tbxTranslations.Name = "tbxTranslations";
			this.tbxTranslations.SelectedIndex = 0;
			this.tbxTranslations.Size = new System.Drawing.Size(764, 411);
			this.tbxTranslations.TabIndex = 0;
			// 
			// toolStrip2
			// 
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtLocaleSelector,
            this.cbxLocales});
			this.toolStrip2.Location = new System.Drawing.Point(81, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(69, 25);
			this.toolStrip2.TabIndex = 1;
			// 
			// txtLocaleSelector
			// 
			this.txtLocaleSelector.Name = "txtLocaleSelector";
			this.txtLocaleSelector.Size = new System.Drawing.Size(44, 22);
			this.txtLocaleSelector.Text = "Locale:";
			// 
			// cbxLocales
			// 
			this.cbxLocales.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.cbxLocales.Image = ((System.Drawing.Image)(resources.GetObject("cbxLocales.Image")));
			this.cbxLocales.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cbxLocales.Name = "cbxLocales";
			this.cbxLocales.Size = new System.Drawing.Size(13, 22);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnNewLocale,
            this.btnOpen,
            this.btnSave,
            this.btnSaveAll,
            this.toolStripSeparator,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.toolStripSeparator1,
            this.btnHelp,
            this.toolStripSeparator2});
			this.toolStrip1.Location = new System.Drawing.Point(3, 25);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(237, 25);
			this.toolStrip1.TabIndex = 0;
			// 
			// btnNew
			// 
			this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnNew.Image = global::NLTUI.Properties.Resources.page_white;
			this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(23, 22);
			this.btnNew.Text = "Ne&w Project";
			this.btnNew.Click += new System.EventHandler(this.newToolStripButton_Click);
			// 
			// btnNewLocale
			// 
			this.btnNewLocale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnNewLocale.Image = global::NLTUI.Properties.Resources.page_white_copy;
			this.btnNewLocale.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNewLocale.Name = "btnNewLocale";
			this.btnNewLocale.Size = new System.Drawing.Size(23, 22);
			this.btnNewLocale.Text = "New &Locale";
			this.btnNewLocale.Click += new System.EventHandler(this.btnNewLocale_Click);
			// 
			// btnOpen
			// 
			this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnOpen.Image = global::NLTUI.Properties.Resources.folder;
			this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(23, 22);
			this.btnOpen.Text = "&Open";
			this.btnOpen.Click += new System.EventHandler(this.openToolStripButton_Click);
			// 
			// btnSave
			// 
			this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSave.Image = global::NLTUI.Properties.Resources.disk;
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnSaveAll
			// 
			this.btnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSaveAll.Image = global::NLTUI.Properties.Resources.disk_multiple;
			this.btnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSaveAll.Name = "btnSaveAll";
			this.btnSaveAll.Size = new System.Drawing.Size(23, 22);
			this.btnSaveAll.Text = "Save All";
			this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// btnCut
			// 
			this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCut.Image = global::NLTUI.Properties.Resources.cut;
			this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCut.Name = "btnCut";
			this.btnCut.Size = new System.Drawing.Size(23, 22);
			this.btnCut.Text = "C&ut";
			// 
			// btnCopy
			// 
			this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCopy.Image = global::NLTUI.Properties.Resources.page_copy;
			this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(23, 22);
			this.btnCopy.Text = "&Copy";
			// 
			// btnPaste
			// 
			this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPaste.Image = global::NLTUI.Properties.Resources.paste_plain;
			this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPaste.Name = "btnPaste";
			this.btnPaste.Size = new System.Drawing.Size(23, 22);
			this.btnPaste.Text = "&Paste";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnHelp
			// 
			this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnHelp.Image = global::NLTUI.Properties.Resources.help;
			this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(23, 22);
			this.btnHelp.Text = "He&lp";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// cleanupLocaleToolStripMenuItem
			// 
			this.cleanupLocaleToolStripMenuItem.Name = "cleanupLocaleToolStripMenuItem";
			this.cleanupLocaleToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.cleanupLocaleToolStripMenuItem.Text = "&Cleanup Locale";
			this.cleanupLocaleToolStripMenuItem.Click += new System.EventHandler(this.cleanupLocaleToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(764, 507);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NeoSmart Localization Toolkit";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.TabControl tbxTranslations;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem mnuTools;
		private System.Windows.Forms.ToolStripMenuItem mnuHelp;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnNew;
		private System.Windows.Forms.ToolStripButton btnOpen;
		private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripButton btnCut;
		private System.Windows.Forms.ToolStripButton btnCopy;
		private System.Windows.Forms.ToolStripButton btnPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripLabel txtLocaleSelector;
		private System.Windows.Forms.ToolStripDropDownButton cbxLocales;
		private System.Windows.Forms.ToolStripButton btnNewLocale;
		private System.Windows.Forms.ToolStripButton btnSaveAll;
		private System.Windows.Forms.ToolStripMenuItem cleanupLocaleToolStripMenuItem;
	}
}

