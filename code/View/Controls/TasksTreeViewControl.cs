using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;

namespace PTM.View.Controls
{

	public class TasksTreeViewControl : UserControl
	{
		private IContainer components;

		public TasksTreeViewControl()
		{
			InitializeComponent();		
		}

		public event EventHandler SelectedTaskChanged;

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TasksTreeViewControl));
			this.treeView = new System.Windows.Forms.TreeView();
			this.groupsImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.AllowDrop = true;
			this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.HotTracking = true;
			this.treeView.ImageIndex = -1;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = -1;
			this.treeView.Size = new System.Drawing.Size(120, 104);
			this.treeView.TabIndex = 0;
			// 
			// groupsImageList
			// 
			this.groupsImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.groupsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("groupsImageList.ImageStream")));
			this.groupsImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// TasksTreeViewControl
			// 
			this.Controls.Add(this.treeView);
			this.Name = "TasksTreeViewControl";
			this.Size = new System.Drawing.Size(120, 104);
			this.ResumeLayout(false);

		}
		#endregion

		private ImageList groupsImageList;
		private TreeView treeView;
		private int currentSelectedTask = -1;
		public bool includeDefaultTask;
		public const string NEW_TASK = "New Task";

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			treeView.ImageList = this.groupsImageList;
			treeView.ImageIndex = 0;
			treeView.SelectedImageIndex = 1;
			treeView.LabelEdit = false;
			treeView.HideSelection = false;
			treeView.AfterSelect+=new TreeViewEventHandler(treeView_AfterSelect);
			treeView.AfterLabelEdit+=new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
			this.treeView.AfterLabelEdit+=new NodeLabelEditEventHandler(treeView_AfterLabelEdit);
		}

		public void Initialize(bool includeDefaultTask)
		{
			this.includeDefaultTask = includeDefaultTask;
			LoadTree();
			Tasks.TasksRowChanged+=new PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TasksRowDeleting+=new PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowDeleting);

		}
		
		public void AddNewTask()
		{
			PTMDataset.TasksRow row = Tasks.NewTasksRow();
			row.Description = NEW_TASK;
			row.ParentId = (int) treeView.SelectedNode.Tag;
			row.IsDefaultTask = false;

			try
			{
				row.Id = Tasks.AddTasksRow(row);
			}
			catch(ApplicationException aex)
			{
				MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			
			treeView.LabelEdit = true;
			TreeNode node = FindTaskNode(row.Id);
			node.EnsureVisible();
			treeView.SelectedNode = node;
			node.BeginEdit();
		}

		public void EditSelectedTaskDescription()
		{
			treeView.LabelEdit = true;
			treeView.SelectedNode.BeginEdit();
		}
		
		public void DeleteSelectedTask()
		{
			
			if(MessageBox.Show("All tasks and sub-tasks assigned to this task will be deleted too. \nAre you sure you want to delete '" + this.treeView.SelectedNode.Text + "'?", 
				this.ParentForm.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly)
				== DialogResult.OK)
			{
				PTMDataset.TasksRow row;
				row = Tasks.FindById((int) treeView.SelectedNode.Tag);
				try
				{
					Tasks.DeleteTaskRow(row);
				}
				catch(ApplicationException aex)
				{
					MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}				
		}


		private void LoadTree()
		{
			treeView.Nodes.Clear();
			TreeNode nodeParent = CreateNode(Tasks.RootTasksRow);
			this.treeView.Nodes.Add(nodeParent);
			AddChildNodes(Tasks.RootTasksRow, nodeParent);
		}

		private void AddChildNodes(PTMDataset.TasksRow parentRow, TreeNode nodeParent)
		{
			DataRow[] childsRows = Tasks.GetChildTasks(parentRow.Id);
			foreach (PTMDataset.TasksRow row in childsRows)
			{
				if(!this.includeDefaultTask && row.IsDefaultTask)
					continue;
				TreeNode nodeChild = CreateNode(row);
				nodeParent.Nodes.Add(nodeChild);
				AddChildNodes(row, nodeChild);
			}
		}

		private TreeNode CreateNode(PTMDataset.TasksRow row)
		{
			TreeNode node = new TreeNode(row.Description, this.treeView.ImageIndex,this.treeView.SelectedImageIndex);
			node.Tag = row.Id;
			return node;
		}

		private TreeNode FindTaskNode(int taskId)
		{
			return FindNode(taskId, this.treeView.Nodes);
		}

		private TreeNode FindNode(int taskId, TreeNodeCollection nodes)
		{
			foreach (TreeNode node in nodes)
			{
				if ((int)node.Tag==taskId)
				{
					return node;
				}
				else
				{
					if(node.Nodes.Count>0)
					{
						TreeNode childnode = FindNode(taskId, node.Nodes);
						if(childnode!=null)
							return childnode;
					}
				}
			}
			return null;
		}

		
		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			PTMDataset.TasksRow row = Tasks.FindById(Convert.ToInt32(e.Node.Tag));
			if(row!=null)
			{
				if(e.Label!=null &&  e.Label!=String.Empty)
				{
					row.Description = e.Label;
					try
					{
						Tasks.UpdateTaskRow(row);						
					}
					catch(ApplicationException aex)
					{
						MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
		}

		private void Tasks_TasksRowChanged(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			if(e.Action == DataRowAction.Add)
			{
				TreeNode nodeParent = FindTaskNode(e.Row.ParentId);
				TreeNode nodeChild = CreateNode(e.Row);
				nodeParent.Nodes.Add(nodeChild);
				return;
			}
			else if(e.Action == DataRowAction.Change)
			{
				TreeNode node = FindTaskNode(e.Row.Id);
				node.Text = e.Row.Description;
			}
		}

		private void Tasks_TasksRowDeleting(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			TreeNode node = FindTaskNode(e.Row.Id);
			if(node!=null && node.TreeView != null)
				node.Remove();	
		}
		private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			treeView.LabelEdit = false;
		}


		public int SelectedTaskId
		{
			get
			{
				return currentSelectedTask;
			}
			set
			{
				if(currentSelectedTask==value)
					return;
				TreeNode node;
				node = FindTaskNode(value);
				if(node== null)
					return;
				currentSelectedTask = value;
				treeView.SelectedNode = node;
				if(this.SelectedTaskChanged!=null)
				{
					this.SelectedTaskChanged(this, new EventArgs());
				}
			}
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if(currentSelectedTask != (int)e.Node.Tag)
			{
				currentSelectedTask = (int) e.Node.Tag;
				if(this.SelectedTaskChanged!=null)
				{
					this.SelectedTaskChanged(sender, e);
				}
			}
		}
	}

}
