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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button importButton;
		private System.Windows.Forms.Button button1;
		private IContainer components;
		

		private PTMDataset.TasksRow selectedTaskRow = null;

		public PTMDataset.TasksRow SelectedTaskRow
		{
			get { return selectedTaskRow; }
		}

		public TasksHierarchyForm()
		{
			InitializeComponent();
			tasksTreeViewControl.AfterSelect+=new TreeViewEventHandler(TreeView_AfterSelect);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TasksHierarchyForm));
			this.newButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.deleteButton = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.importButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.tasksTreeViewControl = new PTM.View.Controls.TasksTreeViewControl();
			this.SuspendLayout();
			// 
			// newButton
			// 
			this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.newButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.newButton.Location = new System.Drawing.Point(264, 48);
			this.newButton.Name = "newButton";
			this.newButton.Size = new System.Drawing.Size(80, 23);
			this.newButton.TabIndex = 1;
			this.newButton.Text = "New Task...";
			this.newButton.Click += new System.EventHandler(this.newButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(264, 288);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(80, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Ok";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Choose a Detail Level:";
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.deleteButton.Location = new System.Drawing.Point(264, 128);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(80, 23);
			this.deleteButton.TabIndex = 5;
			this.deleteButton.Text = "Delete";
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.editButton.Location = new System.Drawing.Point(264, 88);
			this.editButton.Name = "editButton";
			this.editButton.TabIndex = 6;
			this.editButton.Text = "Edit";
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// importButton
			// 
			this.importButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.importButton.Location = new System.Drawing.Point(264, 168);
			this.importButton.Name = "importButton";
			this.importButton.Size = new System.Drawing.Size(80, 23);
			this.importButton.TabIndex = 7;
			this.importButton.Text = "Import...";
			this.importButton.Visible = false;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(264, 208);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "Save As...";
			this.button1.Visible = false;
			// 
			// tasksTreeViewControl1
			// 
			this.tasksTreeViewControl.Location = new System.Drawing.Point(8, 32);
			this.tasksTreeViewControl.Name = "tasksTreeViewControl";
			this.tasksTreeViewControl.Size = new System.Drawing.Size(248, 280);
			this.tasksTreeViewControl.TabIndex = 9;
			// 
			// TasksHierarchyForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 318);
			this.Controls.Add(this.tasksTreeViewControl);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.importButton);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.newButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "TasksHierarchyForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tasks Tree";
			this.Load += new System.EventHandler(this.TasksGroupsForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		//private PTMDataset.TasksDataTable tasksTable = null;
		
		private void TasksGroupsForm_Load(object sender, EventArgs e)
		{
			
		}

		

		private void newButton_Click(object sender, EventArgs e)
		{
			tasksTreeViewControl.AddNewTask();		
		}

		private void editButton_Click(object sender, System.EventArgs e)
		{
			tasksTreeViewControl.EditSelectedTaskDescription();
		}

		private void deleteButton_Click(object sender, System.EventArgs e)
		{	
			if(MessageBox.Show("All tasks and sub-groups assigned to this group will be deleted too. \nAre you sure you want to delete '" + tasksTreeViewControl.SelectedNode.Text + "'?", 
				this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly)
				== DialogResult.OK)
			{
				try
				{
					tasksTreeViewControl.DeleteSelectedTask();	
				}
				catch(ApplicationException aex)
				{
					MessageBox.Show(aex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if(tasksTreeViewControl.SelectedNode == tasksTreeViewControl.TopNode)
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

		private void okButton_Click(object sender, System.EventArgs e)
		{
			this.selectedTaskRow = Tasks.FindById(
			(int) tasksTreeViewControl.SelectedNode.Tag);
			this.Close();
		}
	}
}
