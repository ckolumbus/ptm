using System;
using System.ComponentModel;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;
using PTM.View.Controls;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for TasksGroupsForm.
	/// </summary>
	public class TasksHierarchyForm : Form
	{
		private TasksTreeViewControl tasksTreeViewControl;
		private Button newButton;
		private Button okButton;
		private Button deleteButton;
		private Button editButton;
		private IContainer components;


		private PTMDataset.TasksRow selectedTaskRow = null;

		public PTMDataset.TasksRow SelectedTaskRow
		{
			get { return selectedTaskRow; }
		}

		public TasksHierarchyForm()
		{
			InitializeComponent();
			tasksTreeViewControl.SelectedTaskChanged += new EventHandler(TreeView_AfterSelect);
			this.tasksTreeViewControl.Initialize(true);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TasksHierarchyForm));
			this.newButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.tasksTreeViewControl = new PTM.View.Controls.TasksTreeViewControl();
			this.SuspendLayout();
			// 
			// newButton
			// 
			this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.newButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.newButton.Location = new System.Drawing.Point(264, 16);
			this.newButton.Name = "newButton";
			this.newButton.Size = new System.Drawing.Size(80, 23);
			this.newButton.TabIndex = 1;
			this.newButton.Text = "New Task...";
			this.newButton.Click += new System.EventHandler(this.newButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(264, 288);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(80, 23);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "Ok";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.deleteButton.Location = new System.Drawing.Point(264, 96);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(80, 23);
			this.deleteButton.TabIndex = 3;
			this.deleteButton.Text = "Delete";
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.editButton.Location = new System.Drawing.Point(264, 56);
			this.editButton.Name = "editButton";
			this.editButton.TabIndex = 2;
			this.editButton.Text = "Edit";
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// tasksTreeViewControl
			// 
			this.tasksTreeViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tasksTreeViewControl.Location = new System.Drawing.Point(8, 8);
			this.tasksTreeViewControl.Name = "tasksTreeViewControl";
			this.tasksTreeViewControl.SelectedTaskId = -1;
			this.tasksTreeViewControl.Size = new System.Drawing.Size(248, 304);
			this.tasksTreeViewControl.TabIndex = 0;
			// 
			// TasksHierarchyForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 318);
			this.Controls.Add(this.tasksTreeViewControl);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.newButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "TasksHierarchyForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tasks Explorer";
			this.ResumeLayout(false);

		}

		#endregion

		private void newButton_Click(object sender, EventArgs e)
		{
			tasksTreeViewControl.AddNewTask();
		}

		private void editButton_Click(object sender, EventArgs e)
		{
			tasksTreeViewControl.EditSelectedTaskDescription();
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			tasksTreeViewControl.DeleteSelectedTask();
		}

		private void TreeView_AfterSelect(object sender, EventArgs e)
		{
			if (tasksTreeViewControl.SelectedTaskId == Tasks.RootTasksRow.Id)
			{
				this.editButton.Enabled = false;
				this.deleteButton.Enabled = false;
			}
			else
			{
				this.editButton.Enabled = true;
				this.deleteButton.Enabled = true;
			}
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			this.selectedTaskRow = Tasks.FindById(
				(int) tasksTreeViewControl.SelectedTaskId);
			this.Close();
		}
	}
}