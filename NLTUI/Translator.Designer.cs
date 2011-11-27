namespace NLTUI
{
	partial class Translator
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lstKeys = new System.Windows.Forms.ListView();
			this.colKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtOld = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.chkDerived = new System.Windows.Forms.CheckBox();
			this.chkMinorUpdate = new System.Windows.Forms.CheckBox();
			this.txtNew = new System.Windows.Forms.TextBox();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lstKeys);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(485, 284);
			this.splitContainer1.SplitterDistance = 115;
			this.splitContainer1.TabIndex = 0;
			// 
			// lstKeys
			// 
			this.lstKeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colKey});
			this.lstKeys.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstKeys.FullRowSelect = true;
			this.lstKeys.HideSelection = false;
			this.lstKeys.Location = new System.Drawing.Point(0, 0);
			this.lstKeys.Name = "lstKeys";
			this.lstKeys.Size = new System.Drawing.Size(115, 284);
			this.lstKeys.SmallImageList = this.imageList1;
			this.lstKeys.TabIndex = 1;
			this.lstKeys.UseCompatibleStateImageBehavior = false;
			this.lstKeys.View = System.Windows.Forms.View.Details;
			this.lstKeys.SelectedIndexChanged += new System.EventHandler(this.lstKeys_SelectedIndexChanged);
			// 
			// colKey
			// 
			this.colKey.Text = "Translation Key";
			this.colKey.Width = 93;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer2.Size = new System.Drawing.Size(366, 284);
			this.splitContainer2.SplitterDistance = 97;
			this.splitContainer2.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtOld);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(360, 91);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Source Text";
			// 
			// txtOld
			// 
			this.txtOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOld.BackColor = System.Drawing.Color.White;
			this.txtOld.Location = new System.Drawing.Point(7, 19);
			this.txtOld.Multiline = true;
			this.txtOld.Name = "txtOld";
			this.txtOld.ReadOnly = true;
			this.txtOld.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtOld.Size = new System.Drawing.Size(347, 66);
			this.txtOld.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.flowLayoutPanel1);
			this.groupBox2.Controls.Add(this.txtNew);
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(360, 174);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Translated Text";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.chkDerived);
			this.flowLayoutPanel1.Controls.Add(this.chkMinorUpdate);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(7, 151);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(346, 17);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// chkDerived
			// 
			this.chkDerived.AutoSize = true;
			this.chkDerived.Location = new System.Drawing.Point(207, 3);
			this.chkDerived.Name = "chkDerived";
			this.chkDerived.Size = new System.Drawing.Size(136, 17);
			this.chkDerived.TabIndex = 1;
			this.chkDerived.Text = "Use parent locale\'s text";
			this.chkDerived.UseVisualStyleBackColor = true;
			this.chkDerived.CheckedChanged += new System.EventHandler(this.chkDerived_CheckedChanged);
			// 
			// chkMinorUpdate
			// 
			this.chkMinorUpdate.AutoSize = true;
			this.chkMinorUpdate.Location = new System.Drawing.Point(113, 3);
			this.chkMinorUpdate.Name = "chkMinorUpdate";
			this.chkMinorUpdate.Size = new System.Drawing.Size(88, 17);
			this.chkMinorUpdate.TabIndex = 2;
			this.chkMinorUpdate.Text = "Minor update";
			this.chkMinorUpdate.UseVisualStyleBackColor = true;
			// 
			// txtNew
			// 
			this.txtNew.AcceptsReturn = true;
			this.txtNew.AcceptsTab = true;
			this.txtNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNew.Location = new System.Drawing.Point(7, 19);
			this.txtNew.Multiline = true;
			this.txtNew.Name = "txtNew";
			this.txtNew.Size = new System.Drawing.Size(347, 126);
			this.txtNew.TabIndex = 0;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(485, 284);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(485, 309);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// Translator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "Translator";
			this.Size = new System.Drawing.Size(485, 309);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtOld;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtNew;
		private System.Windows.Forms.ListView lstKeys;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ColumnHeader colKey;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.CheckBox chkDerived;
		private System.Windows.Forms.CheckBox chkMinorUpdate;
	}
}
