using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PTM.Data;
using PTM.Framework;
using PTM.Framework.Infos;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for TaskPropertiesForm.
	/// </summary>
	public class TaskPropertiesForm : Form
	{
		private CheckBox chkIsActive;
		private Button btnRigth;
		private Button btnLeft;
		private PictureBox picture;
		private Button cancelButton;
		private Button okButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private TextBox txtDescription;


		private Task task;

		public TaskPropertiesForm(int taskId)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			task = Tasks.FindById(taskId);
		}

		public bool IsActive
		{
			get { return this.chkIsActive.Checked; }
		}

		public int IconId
		{
			get { return (int) this.picture.Tag; }
		}

		private string Description
		{
			get { return this.txtDescription.Text; }
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TaskPropertiesForm));
			this.chkIsActive = new System.Windows.Forms.CheckBox();
			this.btnRigth = new System.Windows.Forms.Button();
			this.btnLeft = new System.Windows.Forms.Button();
			this.picture = new System.Windows.Forms.PictureBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// chkIsActive
			// 
			this.chkIsActive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkIsActive.Location = new System.Drawing.Point(72, 56);
			this.chkIsActive.Name = "chkIsActive";
			this.chkIsActive.Size = new System.Drawing.Size(96, 24);
			this.chkIsActive.TabIndex = 10;
			this.chkIsActive.Text = "Is active task";
			// 
			// btnRigth
			// 
			this.btnRigth.Image = ((System.Drawing.Image)(resources.GetObject("btnRigth.Image")));
			this.btnRigth.Location = new System.Drawing.Point(40, 56);
			this.btnRigth.Name = "btnRigth";
			this.btnRigth.Size = new System.Drawing.Size(18, 18);
			this.btnRigth.TabIndex = 9;
			this.btnRigth.Click += new System.EventHandler(this.btnRigth_Click);
			// 
			// btnLeft
			// 
			this.btnLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnLeft.Image")));
			this.btnLeft.Location = new System.Drawing.Point(16, 56);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(18, 18);
			this.btnLeft.TabIndex = 8;
			this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			// 
			// picture
			// 
			this.picture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picture.Location = new System.Drawing.Point(16, 16);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(40, 40);
			this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picture.TabIndex = 7;
			this.picture.TabStop = false;
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(72, 16);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(240, 20);
			this.txtDescription.TabIndex = 11;
			this.txtDescription.Text = "";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(240, 88);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 13;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(152, 88);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 12;
			this.okButton.Text = "Ok";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// TaskPropertiesForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(320, 116);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.btnLeft);
			this.Controls.Add(this.picture);
			this.Controls.Add(this.chkIsActive);
			this.Controls.Add(this.btnRigth);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TaskPropertiesForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Task Properties";
			this.Load += new System.EventHandler(this.TaskPropertiesForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private void TaskPropertiesForm_Load(object sender, EventArgs e)
		{
			this.picture.Image = ((Icon) IconsManager.CommonTaskIconsTable[task.IconId]).ToBitmap();
			this.picture.Tag = task.IconId;
			this.txtDescription.Text = task.Description;
			this.chkIsActive.Checked = task.IsActive;
		}

		private void btnLeft_Click(object sender, EventArgs e)
		{
			this.picture.Tag = (int) this.picture.Tag - 1;
			if ((int) this.picture.Tag < 1)
				this.picture.Tag = IconsManager.CommonTaskIconsTable.Count - 1;
			this.picture.Image = ((Icon) IconsManager.CommonTaskIconsTable[this.picture.Tag]).ToBitmap();
		}

		private void btnRigth_Click(object sender, EventArgs e)
		{
			this.picture.Tag = (int) this.picture.Tag + 1;
			if ((int) this.picture.Tag > IconsManager.CommonTaskIconsTable.Count - 1)
				this.picture.Tag = 1;
			this.picture.Image = ((Icon) IconsManager.CommonTaskIconsTable[this.picture.Tag]).ToBitmap();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Task row;
			row = Tasks.FindById(task.Id);
			try
			{
				row.IsActive = this.IsActive;
				row.IconId = this.IconId;
				row.Description = this.Description;
				Tasks.UpdateTask(row);
			}
			catch (ApplicationException aex)
			{
				MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			this.Close();
		}
	}
}