using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
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
        private NumericUpDown hrsNumericUpDown;
        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown minsNumericUpDown;


		private int taskId;

		public TaskPropertiesForm(int taskId)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


            this.taskId = taskId;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskPropertiesForm));
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnRigth = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.picture = new System.Windows.Forms.PictureBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.hrsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.minsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hrsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // chkIsActive
            // 
            this.chkIsActive.Location = new System.Drawing.Point(72, 70);
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
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(240, 101);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(152, 101);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "Ok";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // hrsNumericUpDown
            // 
            this.hrsNumericUpDown.Location = new System.Drawing.Point(158, 44);
            this.hrsNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.hrsNumericUpDown.Name = "hrsNumericUpDown";
            this.hrsNumericUpDown.Size = new System.Drawing.Size(49, 20);
            this.hrsNumericUpDown.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Estimation-Goal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "hrs.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "mins.";
            // 
            // minsNumericUpDown
            // 
            this.minsNumericUpDown.Location = new System.Drawing.Point(239, 44);
            this.minsNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minsNumericUpDown.Name = "minsNumericUpDown";
            this.minsNumericUpDown.Size = new System.Drawing.Size(36, 20);
            this.minsNumericUpDown.TabIndex = 17;
            // 
            // TaskPropertiesForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(320, 129);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.minsNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hrsNumericUpDown);
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
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hrsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private void TaskPropertiesForm_Load(object sender, EventArgs e)
		{
            Task task = Tasks.FindById(taskId);
			this.picture.Image = ((Icon) IconsManager.CommonTaskIconsTable[task.IconId]).ToBitmap();
			this.picture.Tag = task.IconId;
			this.txtDescription.Text = task.Description;
			this.chkIsActive.Checked = task.IsActive;
		    this.minsNumericUpDown.Value = task.Estimation % 60;
            this.hrsNumericUpDown.Value = (task.Estimation - task.Estimation % 60)/60;
            if(task.Id == Tasks.IdleTask.Id)
            {
                this.txtDescription.Enabled = false;
                this.chkIsActive.Enabled = false;
                this.minsNumericUpDown.Enabled = false;
                this.hrsNumericUpDown.Enabled = false;
                this.btnRigth.Enabled = false;
                this.btnLeft.Enabled = false;
                this.okButton.Enabled = false;
            }
            
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
			Task task;
			task = Tasks.FindById(taskId);
			try
			{
				task.IsActive = this.IsActive;
				task.IconId = this.IconId;
				task.Description = this.Description;
			    task.Estimation = (int) (this.minsNumericUpDown.Value + this.hrsNumericUpDown.Value*60);
				Tasks.UpdateTask(task);
			}
			catch (ApplicationException aex)
			{
				MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			this.Close();
		}
	}
}