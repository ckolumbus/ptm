using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PTM.Framework.Helpers;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for AddinForm.
	/// </summary>
	internal class AddinForm : Form
	{
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ListView addinList;
		private ColumnHeader columnHeader3;
		private ColumnHeader columnHeader4;
		private Button addButton;
		private Button removeButton;
		private Button closeButton;
		private OpenFileDialog openAddinDialog;
		private Label label1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		internal AddinForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.addinList = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.addButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.openAddinDialog = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Available Add-ins";
			this.columnHeader1.Width = 342;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Startup";
			this.columnHeader2.Width = 67;
			// 
			// addinList
			// 
			this.addinList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
			                                	{
			                                		this.columnHeader3,
			                                		this.columnHeader4
			                                	});
			this.addinList.FullRowSelect = true;
			this.addinList.GridLines = true;
			this.addinList.HideSelection = false;
			this.addinList.Location = new System.Drawing.Point(8, 8);
			this.addinList.MultiSelect = false;
			this.addinList.Name = "addinList";
			this.addinList.Size = new System.Drawing.Size(328, 176);
			this.addinList.TabIndex = 4;
			this.addinList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Available Add-ins";
			this.columnHeader3.Width = 156;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Location";
			this.columnHeader4.Width = 600;
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(344, 48);
			this.addButton.Name = "addButton";
			this.addButton.TabIndex = 6;
			this.addButton.Text = "Add...";
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Location = new System.Drawing.Point(344, 88);
			this.removeButton.Name = "removeButton";
			this.removeButton.TabIndex = 7;
			this.removeButton.Text = "Remove";
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Location = new System.Drawing.Point(344, 200);
			this.closeButton.Name = "closeButton";
			this.closeButton.TabIndex = 8;
			this.closeButton.Text = "Close";
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// openAddinDialog
			// 
			this.openAddinDialog.Filter = "Assembly Files (*.exe; *.dll)|*.exe; *.dll|All Files (*.*)|*.*";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 192);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(328, 32);
			this.label1.TabIndex = 9;
			this.label1.Text = "* You need to restart the application before your changes take effect";
			// 
			// AddinForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(426, 232);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.removeButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.addinList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddinForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add-in Manager";
			this.Load += new System.EventHandler(this.AddinForm_Load);
			this.ResumeLayout(false);
		}

		#endregion

		private void AddinForm_Load(object sender, EventArgs e)
		{
			ArrayList addins = AddinHelper.GetAddins();
			foreach (string path in addins)
			{
				AddAddinToList(path);
			}
		}

		private void AddAddinToList(string path)
		{
			this.addinList.Items.Add(new ListViewItem(new string[] {AddinHelper.GetAddinDescription(path), path}));
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			if (this.openAddinDialog.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					AddinHelper.AddAddinAssembly(this.openAddinDialog.FileName);
					AddAddinToList(this.openAddinDialog.FileName);
				}
				catch (ApplicationException aex)
				{
					MessageBox.Show(aex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			if (this.addinList.SelectedItems.Count == 0)
				return;

			AddinHelper.DeleteAddinAssembly(this.addinList.SelectedItems[0].SubItems[1].Text);
			this.addinList.Items.Remove(this.addinList.SelectedItems[0]);
		}
	}
}