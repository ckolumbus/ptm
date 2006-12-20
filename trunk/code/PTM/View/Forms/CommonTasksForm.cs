using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using PTM.Framework;
using PTM.View.Controls.TreeListViewComponents;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for CommonTasksForm.
	/// </summary>
	public class CommonTasksForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private PTM.View.Controls.TreeListViewComponents.TreeListView list;
		private System.Windows.Forms.CheckBox chkIsActive;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.PictureBox picture;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnLeft;
		private System.Windows.Forms.Button btnRigth;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CommonTasksForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.list.SmallImageList = IconsManager.IconsList;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CommonTasksForm));
			this.list = new PTM.View.Controls.TreeListViewComponents.TreeListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnRigth = new System.Windows.Forms.Button();
			this.btnLeft = new System.Windows.Forms.Button();
			this.chkIsActive = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.picture = new System.Windows.Forms.PictureBox();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.AutoArrange = false;
			this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																										 this.columnHeader1,
																										 this.columnHeader2});
			this.list.HideSelection = false;
			this.list.Location = new System.Drawing.Point(8, 16);
			this.list.MultiSelect = false;
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(336, 136);
			this.list.Sorting = System.Windows.Forms.SortOrder.None;
			this.list.TabIndex = 0;
			this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Description";
			this.columnHeader1.Width = 271;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Is Active";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.list);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 160);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Available common tasks";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnRigth);
			this.groupBox2.Controls.Add(this.btnLeft);
			this.groupBox2.Controls.Add(this.chkIsActive);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.txtDescription);
			this.groupBox2.Controls.Add(this.picture);
			this.groupBox2.Controls.Add(this.btnSave);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(8, 200);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(352, 88);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Preferences";
			// 
			// btnRigth
			// 
			this.btnRigth.Image = ((System.Drawing.Image)(resources.GetObject("btnRigth.Image")));
			this.btnRigth.Location = new System.Drawing.Point(40, 64);
			this.btnRigth.Name = "btnRigth";
			this.btnRigth.Size = new System.Drawing.Size(18, 18);
			this.btnRigth.TabIndex = 6;
			this.btnRigth.Click += new System.EventHandler(this.btnRigth_Click);
			// 
			// btnLeft
			// 
			this.btnLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnLeft.Image")));
			this.btnLeft.Location = new System.Drawing.Point(16, 64);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(18, 18);
			this.btnLeft.TabIndex = 5;
			this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			// 
			// chkIsActive
			// 
			this.chkIsActive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkIsActive.Location = new System.Drawing.Point(136, 56);
			this.chkIsActive.Name = "chkIsActive";
			this.chkIsActive.Size = new System.Drawing.Size(112, 24);
			this.chkIsActive.TabIndex = 4;
			this.chkIsActive.Text = "Is an active tasks";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Description:";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(136, 24);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(208, 20);
			this.txtDescription.TabIndex = 2;
			this.txtDescription.Text = "";
			// 
			// picture
			// 
			this.picture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picture.Location = new System.Drawing.Point(16, 24);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(40, 40);
			this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picture.TabIndex = 0;
			this.picture.TabStop = false;
			// 
			// btnNew
			// 
			this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNew.Location = new System.Drawing.Point(192, 176);
			this.btnNew.Name = "btnNew";
			this.btnNew.TabIndex = 3;
			this.btnNew.Text = "New";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemove.Location = new System.Drawing.Point(280, 176);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(280, 296);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(192, 296);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(272, 56);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 8;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// CommonTasksForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(370, 328);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CommonTasksForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Common Tasks";
			this.Load += new System.EventHandler(this.CommonTasksForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void CommonTasksForm_Load(object sender, System.EventArgs e)
		{
			foreach (DefaultTask defaultTask in DefaultTasks.Table.Values)
			{				
				if(defaultTask.DefaultTaskId == DefaultTasks.IdleTaskId)
					continue;
				if(defaultTask.Hidden)
					continue;
				TreeListViewItem item = new TreeListViewItem(defaultTask.Description, new string[]{defaultTask.IsActive.ToString()});
				item.ImageIndex = defaultTask.IconId;
				item.Tag = defaultTask;
				this.list.Items.Add(item);				
			}
			
			New();
			
		}

		private void list_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(list.SelectedItems.Count==0)
				return;
			TreeListViewItem item;
			item = list.SelectedItems[0];
			DefaultTask defaultTask = (DefaultTask) item.Tag;
			this.picture.Image = ((Icon)IconsManager.CommonTaskIconsTable[defaultTask.IconId]).ToBitmap();
			this.picture.Tag = defaultTask.IconId;
			this.txtDescription.Text = item.Text;
			this.chkIsActive.Checked = bool.Parse(item.SubItems[1].Text);
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if(list.SelectedItems.Count==0)
				return;
			
			TreeListViewItem item;
			item = list.SelectedItems[0];
			
//			if(MessageBox.Show("The logs associated with this common tasks will be deleted too.\nAre you sure you want to delete '"
//				+ item.Text + "'?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
//				return;
			
			this.list.Items.Remove(item);
			New();
		}

		private void btnNew_Click(object sender, System.EventArgs e)
		{
			New();			
		}

		private void New()
		{
			this.list.SelectedItems.Clear();
			this.txtDescription.Text = String.Empty;
			this.chkIsActive.Checked = false;
			this.picture.Tag = 1;
			this.picture.Image = ((Icon)IconsManager.CommonTaskIconsTable[(int)this.picture.Tag]).ToBitmap();			
			this.txtDescription.Focus();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if(this.list.SelectedItems.Count ==0)
			{
				DefaultTask defaultTask = new DefaultTask();
				defaultTask.DefaultTaskId = -1;
				defaultTask.Description = this.txtDescription.Text;
				defaultTask.IconId = (int) this.picture.Tag;
				defaultTask.IsActive = this.chkIsActive.Checked;
				TreeListViewItem item = new TreeListViewItem(defaultTask.Description, new string[]{defaultTask.IsActive.ToString()});
				item.ImageIndex = defaultTask.IconId;
				item.Tag = defaultTask;
				this.list.Items.Add(item);
			}
			else
			{
				DefaultTask defaultTask = (DefaultTask) this.list.SelectedItems[0].Tag;
				defaultTask.Description = this.txtDescription.Text;
				defaultTask.IconId = (int) this.picture.Tag;
				defaultTask.IsActive = this.chkIsActive.Checked;
				this.list.SelectedItems[0].SubItems[0].Text = defaultTask.Description;
				this.list.SelectedItems[0].SubItems[1].Text = defaultTask.IsActive.ToString();
				this.list.SelectedItems[0].ImageIndex = defaultTask.IconId;
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnLeft_Click(object sender, System.EventArgs e)
		{
			this.picture.Tag = (int)this.picture.Tag - 1;
			if((int)this.picture.Tag < 1)
				this.picture.Tag = IconsManager.CommonTaskIconsTable.Count - 1;
			this.picture.Image = ((Icon)IconsManager.CommonTaskIconsTable[this.picture.Tag]).ToBitmap();
			
		}
		
		private void btnRigth_Click(object sender, System.EventArgs e)
		{
			this.picture.Tag = (int)this.picture.Tag + 1;
			if((int)this.picture.Tag > IconsManager.CommonTaskIconsTable.Count-1)
				this.picture.Tag = 1;
			this.picture.Image = ((Icon)IconsManager.CommonTaskIconsTable[this.picture.Tag]).ToBitmap();
	
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				foreach (DefaultTask defaulTask in DefaultTasks.Table.Values)
				{
					if(defaulTask.DefaultTaskId == DefaultTasks.IdleTaskId)
						continue;
					bool deleted = true;
					foreach (ListViewItem item in this.list.Items)
					{
						DefaultTask dt = (DefaultTask) item.Tag;
						if(defaulTask.DefaultTaskId == dt.DefaultTaskId)
						{
							deleted = false;
							break;
						}
					}
					if(deleted)
					{
						DefaultTasks.Delete(defaulTask.DefaultTaskId);
					}
				}
				
				foreach (ListViewItem item in this.list.Items)
				{
					DefaultTask dt = (DefaultTask) item.Tag;
					if(dt.DefaultTaskId==-1)
						DefaultTasks.Add(dt.Description, dt.IsActive, dt.IconId);
					else
						DefaultTasks.Update(dt.DefaultTaskId, dt.Description, dt.IsActive, dt.IconId);
				}
				this.Close();
			}
			catch(ApplicationException aex)
			{
				Cursor.Current = Cursors.Default;
				MessageBox.Show(aex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

	}
}
