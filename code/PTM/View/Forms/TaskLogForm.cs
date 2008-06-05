using System;
using System.ComponentModel;
using System.Windows.Forms;
using PTM.Framework;
using PTM.Framework.Infos;
using PTM.View.Controls;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for TaskNotificationForm.
	/// </summary>
	internal class TaskLogForm : Form
	{
		private Button okButton;
		private Button cancelButton;
		private TasksTreeViewControl tasksTree;
		private Button editButton;
		private Button deleteButton;
		private Button newButton;
		private GroupBox groupBox1;
		private Button propertiesButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		internal TaskLogForm()
		{
			InitializeComponent();

			tasksTree.Initialize();

			this.tasksTree.SelectedTaskChanged += new EventHandler(taskTree_SelectedTaskChanged);
			if (Tasks.CurrentTask != null)
			{
				tasksTree.SelectedTaskId = Tasks.CurrentTask.ParentId;
			}
			else
			{
				tasksTree.SelectedTaskId = Tasks.RootTask.Id;
			}
			tasksTree.DoubleClick += new EventHandler(tasksTree_DoubleClick);
		}

		internal TaskLogForm(int editTaskId)
		{
			InitializeComponent();
			tasksTree.Initialize();
			this.tasksTree.SelectedTaskChanged += new EventHandler(taskTree_SelectedTaskChanged);
			tasksTree.DoubleClick += new EventHandler(tasksTree_DoubleClick);
			Task task;
			task = Tasks.FindById(editTaskId);

			this.tasksTree.SelectedTaskId = task.Id;

			//SetChildTask(row);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskLogForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tasksTree = new PTM.View.Controls.TasksTreeViewControl();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.propertiesButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(256, 252);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Ok";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(344, 252);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            // 
            // tasksTree
            // 
            this.tasksTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tasksTree.Location = new System.Drawing.Point(6, 18);
            this.tasksTree.Name = "tasksTree";
            this.tasksTree.Size = new System.Drawing.Size(318, 216);
            this.tasksTree.TabIndex = 0;
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Location = new System.Drawing.Point(344, 94);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "Edit";
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(344, 134);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // newButton
            // 
            this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newButton.Location = new System.Drawing.Point(344, 54);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 3;
            this.newButton.Text = "New";
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tasksTree);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 240);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose a task";
            // 
            // propertiesButton
            // 
            this.propertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesButton.Location = new System.Drawing.Point(344, 174);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(75, 23);
            this.propertiesButton.TabIndex = 9;
            this.propertiesButton.Text = "Properties";
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
            // 
            // TaskLogForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(432, 289);
            this.Controls.Add(this.propertiesButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 318);
            this.Name = "TaskLogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tasks";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private int selectedTaskId;

		internal int SelectedTaskId
		{
			get { return selectedTaskId; }
		}

		private void taskTree_SelectedTaskChanged(object sender, EventArgs e)
		{
			if (this.tasksTree.SelectedTaskId == -1)
				return;

			if (tasksTree.SelectedTaskId == Tasks.RootTask.Id)
			{
				this.editButton.Enabled = false;
				this.deleteButton.Enabled = false;
				this.propertiesButton.Enabled = false;
			}
			else
			{
				this.editButton.Enabled = true;
				this.deleteButton.Enabled = true;
				this.propertiesButton.Enabled = true;
			}

			selectedTaskId = this.tasksTree.SelectedTaskId;

		}

		private void newButton_Click(object sender, EventArgs e)
		{
			tasksTree.AddNewTask();
		}

		private void editButton_Click(object sender, EventArgs e)
		{
			tasksTree.EditSelectedTaskDescription();
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			tasksTree.DeleteSelectedTask();
		}

		private void propertiesButton_Click(object sender, EventArgs e)
		{
			tasksTree.ShowPropertiesSelectedTask();
		}

		private void tasksTree_DoubleClick(object sender, EventArgs e)
		{
			if (this.tasksTree.SelectedTaskId == Tasks.RootTask.Id)
				return;
			this.selectedTaskId = tasksTree.SelectedTaskId;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}