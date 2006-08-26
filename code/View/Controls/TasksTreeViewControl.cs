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
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ImageIndex = -1;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = -1;
			this.treeView.Size = new System.Drawing.Size(224, 384);
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
			this.Size = new System.Drawing.Size(224, 384);
			this.ResumeLayout(false);

		}
		#endregion

		private ImageList groupsImageList;
		private TreeView treeView;
		private int currentSelectedTask = -1;

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

		public void Initialize()
		{
			LoadTree();
			Tasks.TasksRowChanged+=new PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TasksRowDeleting+=new PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowDeleting);

			if (treeView.Nodes.Count > 0)
			{
				treeView.SelectedNode = treeView.Nodes[0];
			}
		}
		
//		public TreeView TreeView
//		{
//			get { return treeView; }
//		}
		public const string NEW_TASK = "New Task";
		public void AddNewTask()
		{
			treeView.LabelEdit = true;
			PTMDataset.TasksRow row = Tasks.NewTasksRow();
			row.Description = NEW_TASK;
			row.ParentId = (int) treeView.SelectedNode.Tag;
			row.IsDefaultTask = false;
			row.Id = Tasks.AddTasksRow(row);
			
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
			if(MessageBox.Show("All tasks and sub-groups assigned to this group will be deleted too. \nAre you sure you want to delete '" + this.treeView.SelectedNode.Text + "'?", 
				this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly)
				== DialogResult.OK)
			{
				PTMDataset.TasksRow row;
				row = Tasks.FindById((int) treeView.SelectedNode.Tag);
				Tasks.DeleteTaskRow(row);
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
			DataRow[] childsRows = Tasks.GetChildTasks(parentRow);
			foreach (PTMDataset.TasksRow row in childsRows)
			{
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

		private  TreeNode FindNode(int taskId, TreeNodeCollection nodes)
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
			PTMDataset.TasksDataTable tasksDataTable = new PTMDataset.TasksDataTable();
			PTMDataset.TasksRow row = Tasks.FindById(Convert.ToInt32(e.Node.Tag));
         if(row!=null)
			{
				row[tasksDataTable.DescriptionColumn.ColumnName] = e.Label;
				Tasks.UpdateTaskRow(row);
			}
			else
         {
         	throw new  ConstraintException("Value member should be a key.");
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
			if(node!=null || node.TreeView == null)
				node.Remove();	
		}
		private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			treeView.LabelEdit = false;
		}

//		public TreeNode SelectedNode
//		{
//			get { return this.treeView.SelectedNode; }
//		}
//
//		public TreeNode TopNode
//		{
//			get {  return this.treeView.TopNode; }
//		}

		public int SelectedTaskId
		{
			get
			{
				return currentSelectedTask;
			}
			set
			{
				foreach (TreeNode node in treeView.Nodes)
				{
					if((int)node.Tag == value)
					{
						currentSelectedTask = value;
						treeView.SelectedNode = node;
					}
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
