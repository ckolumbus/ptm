using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
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
			InitCommonControls();
			this.treeView.ItemDrag+=new ItemDragEventHandler(treeView_ItemDrag);
			this.treeView.DragDrop+=new DragEventHandler(treeView_DragDrop);
			this.treeView.DragOver+=new DragEventHandler(treeView_DragOver);
			this.treeView.DragEnter+=new DragEventHandler(treeView_DragEnter);
			this.treeView.DragLeave+=new EventHandler(treeView_DragLeave);
			this.treeView.GiveFeedback+=new GiveFeedbackEventHandler(treeView_GiveFeedback);
			this.timer.Tick+=new EventHandler(timer_Tick);
			timer.Interval = 200;
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


		#region Drag And Drop
		private Timer timer = new Timer();
		private System.Windows.Forms.ImageList imageListDrag = new ImageList();
		private TreeNode dragNode = null;
		private TreeNode tempDropNode = null;

		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControls();

		[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
		public static extern bool ImageList_BeginDrag(IntPtr himlTrack, int
			iTrack, int dxHotspot, int dyHotspot);

		[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
		public static extern bool ImageList_DragMove(int x, int y);

		[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
		public static extern void ImageList_EndDrag();

		[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
		public static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);

		[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
		public static extern bool ImageList_DragLeave(IntPtr hwndLock);

		[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
		public static extern bool ImageList_DragShowNolock(bool fShow);

		private void treeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			// Get drag node and select it
			this.dragNode = (TreeNode)e.Item;
			this.treeView.SelectedNode = this.dragNode;

			// Reset image list used for drag image
			this.imageListDrag.Images.Clear();
			this.imageListDrag.ImageSize = new Size(this.dragNode.Bounds.Size.Width + this.treeView.Indent, this.dragNode.Bounds.Height);

			// Create new bitmap
			// This bitmap will contain the tree node image to be dragged
			Bitmap bmp = new Bitmap(this.dragNode.Bounds.Width + this.treeView.Indent, this.dragNode.Bounds.Height);

			// Get graphics from bitmap
			using(Graphics gfx = Graphics.FromImage(bmp))
			{
				// Draw node icon into the bitmap
				gfx.DrawImage(this.groupsImageList.Images[0], 0, 0);

				// Draw node label into bitmap
				gfx.DrawString(this.dragNode.Text,
					this.treeView.Font,
					new SolidBrush(this.treeView.ForeColor),
					(float)this.treeView.Indent, 1.0f);
			}

			// Add bitmap to imagelist
			this.imageListDrag.Images.Add(bmp);

			// Get mouse position in client coordinates
			Point p = this.treeView.PointToClient(Control.MousePosition);

			// Compute delta between mouse position and node bounds
			//			int dx = p.X + this.treeView.Indent - this.dragNode.Bounds.Left;
			//			int dy = p.Y - this.dragNode.Bounds.Top;
			int dx = p.X + this.treeView.Indent - this.dragNode.Bounds.Left - this.treeView.Location.X;
			int dy = p.Y - this.dragNode.Bounds.Top - this.treeView.Location.Y;

			// Begin dragging image
			if (ImageList_BeginDrag(this.imageListDrag.Handle, 0, dx, dy))
			{
				// Begin dragging
				this.treeView.DoDragDrop(bmp, DragDropEffects.Move);
				// End dragging image
				ImageList_EndDrag();
			}		
		
		}

		private void treeView_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// Compute drag position and move image
			Point formP = this.PointToClient(new Point(e.X, e.Y));
			ImageList_DragMove(formP.X - this.treeView.Left, formP.Y - this.treeView.Top);

			// Get actual drop node
			TreeNode dropNode = this.treeView.GetNodeAt(this.treeView.PointToClient(new Point(e.X, e.Y)));
			if(dropNode == null)
			{
				e.Effect = DragDropEffects.None;
				return;
			}
			
			e.Effect = DragDropEffects.Move;

			// if mouse is on a new node select it
			if(this.tempDropNode != dropNode)
			{
				ImageList_DragShowNolock(false);
				this.treeView.SelectedNode = dropNode;
				ImageList_DragShowNolock(true);
				tempDropNode = dropNode;
			}
			
			// Avoid that drop node is child of drag node 
			TreeNode tmpNode = dropNode;
			while(tmpNode.Parent != null)
			{
				if(tmpNode.Parent == this.dragNode) e.Effect = DragDropEffects.None;
				tmpNode = tmpNode.Parent;
			}
		}

		private void treeView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// Unlock updates
			ImageList_DragLeave(this.treeView.Handle);

			// Get drop node
			TreeNode dropNode = this.treeView.GetNodeAt(this.treeView.PointToClient(new Point(e.X, e.Y)));

			// If drop node isn't equal to drag node, add drag node as child of drop node
			if(this.dragNode != dropNode)
			{
				// Remove drag node from parent
				if(this.dragNode.Parent == null)
				{
					this.treeView.Nodes.Remove(this.dragNode);
				}
				else
				{
					this.dragNode.Parent.Nodes.Remove(this.dragNode);
				}

				// Add drag node to drop node
				dropNode.Nodes.Add(this.dragNode);
				dropNode.ExpandAll();
				
				Tasks.UpdateParentTask((int)this.dragNode.Tag, (int)dropNode.Tag);

				// Set drag node to null
				this.dragNode = null;

				// Disable scroll timer
				this.timer.Enabled = false;
			}
		}

		private void treeView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			ImageList_DragEnter(this.treeView.Handle, e.X - this.treeView.Left,
				e.Y - this.treeView.Top);

			// Enable timer for scrolling dragged item
			this.timer.Enabled = true;
		}

		private void treeView_DragLeave(object sender, System.EventArgs e)
		{
			ImageList_DragLeave(this.treeView.Handle);

			// Disable timer for scrolling dragged item
			this.timer.Enabled = false;
		}

		private void treeView_GiveFeedback(object sender, System.Windows.Forms.GiveFeedbackEventArgs e)
		{
			if(e.Effect == DragDropEffects.Move) 
			{
				// Show pointer cursor while dragging
				e.UseDefaultCursors = false;
				this.treeView.Cursor = Cursors.Default;
			}
			else e.UseDefaultCursors = true;
			
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			// get node at mouse position
			Point pt = treeView.PointToClient(Control.MousePosition);
			TreeNode node = this.treeView.GetNodeAt(pt);

			if(node == null) return;

			// if mouse is near to the top, scroll up
			if(pt.Y < 30)
			{
				// set actual node to the upper one
				if (node.PrevVisibleNode!= null) 
				{
					node = node.PrevVisibleNode;
				
					// hide drag image
					ImageList_DragShowNolock(false);
					// scroll and refresh
					node.EnsureVisible();
					this.treeView.Refresh();
					// show drag image
					ImageList_DragShowNolock(true);
					
				}
			}
				// if mouse is near to the bottom, scroll down
			else if(pt.Y > this.treeView.Size.Height - 30)
			{
				if (node.NextVisibleNode!= null) 
				{
					node = node.NextVisibleNode;
				
					ImageList_DragShowNolock(false);
					node.EnsureVisible();
					this.treeView.Refresh();
					ImageList_DragShowNolock(true);
				}
			} 

		}


		#endregion
	}

}
