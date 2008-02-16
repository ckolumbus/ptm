using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Aga.Controls.Tree;
using PTM.Framework;
using PTM.Framework.Infos;
using PTM.View.Forms;

namespace PTM.View.Controls
{
	public class TasksTreeViewControl : UserControl
	{
		private IContainer components;
        private Aga.Controls.Tree.NodeControls.NodeIcon iconNode;
        private Aga.Controls.Tree.NodeControls.NodeStateIcon nodeStateIcon1;

        private TreeModel _model;

        public TasksTreeViewControl()
		{
			InitializeComponent();
            //InitCommonControls();
            //this.treeView.ItemDrag += new ItemDragEventHandler(treeView_ItemDrag);
            //this.treeView.DragDrop += new DragEventHandler(treeView_DragDrop);
            //this.treeView.DragOver += new DragEventHandler(treeView_DragOver);
            //this.treeView.DragEnter += new DragEventHandler(treeView_DragEnter);
            //this.treeView.DragLeave += new EventHandler(treeView_DragLeave);
            //this.treeView.GiveFeedback += new GiveFeedbackEventHandler(treeView_GiveFeedback);
            //this.treeView.DoubleClick += new EventHandler(treeView_DoubleClick);
            //this.timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = 200;

            _model = new TreeModel();
            treeView.Model = _model;
		}

        public event EventHandler SelectedTaskChanged;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TasksTreeViewControl));
            this.treeMenu = new System.Windows.Forms.ContextMenu();
            this.mnuProperties = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.mnuRename = new System.Windows.Forms.MenuItem();
            this.groupsImageList = new System.Windows.Forms.ImageList(this.components);
            this.treeView = new Aga.Controls.Tree.TreeViewAdv();
            this.taskNode = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.iconNode = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.nodeStateIcon1 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this.SuspendLayout();
            // 
            // treeMenu
            // 
            this.treeMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuProperties,
            this.menuItem5,
            this.mnuDelete,
            this.mnuRename});
            // 
            // mnuProperties
            // 
            this.mnuProperties.Index = 0;
            this.mnuProperties.Text = "Properties...";
            this.mnuProperties.Click += new System.EventHandler(this.mnuProperties_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "-";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 2;
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // mnuRename
            // 
            this.mnuRename.Index = 3;
            this.mnuRename.Text = "Rename";
            this.mnuRename.Click += new System.EventHandler(this.mnuRename_Click);
            // 
            // groupsImageList
            // 
            this.groupsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("groupsImageList.ImageStream")));
            this.groupsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.groupsImageList.Images.SetKeyName(0, "");
            this.groupsImageList.Images.SetKeyName(1, "");
            // 
            // treeView
            // 
            this.treeView.AllowColumnReorder = true;
            this.treeView.AllowDrop = true;
            this.treeView.BackColor = System.Drawing.SystemColors.Window;
            this.treeView.DefaultToolTipProvider = null;
            this.treeView.DisplayDraggingNodes = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.DragDropMarkColor = System.Drawing.Color.RoyalBlue;
            this.treeView.FullRowSelect = true;
            this.treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Model = null;
            this.treeView.Name = "treeView";
            this.treeView.NodeControls.Add(this.nodeStateIcon1);
            this.treeView.NodeControls.Add(this.iconNode);
            this.treeView.NodeControls.Add(this.taskNode);
            this.treeView.SelectedNode = null;
            this.treeView.Size = new System.Drawing.Size(230, 203);
            this.treeView.TabIndex = 1;
            this.treeView.Text = "treeViewAdv1";
            // 
            // taskNode
            // 
            this.taskNode.DataPropertyName = "Text";
            this.taskNode.IncrementalSearchEnabled = true;
            this.taskNode.LeftMargin = 3;
            this.taskNode.ParentColumn = null;
            this.taskNode.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.taskNode.UseCompatibleTextRendering = true;
            // 
            // iconNode
            // 
            this.iconNode.DataPropertyName = "Icon";
            this.iconNode.LeftMargin = 1;
            this.iconNode.ParentColumn = null;
            // 
            // nodeStateIcon1
            // 
            this.nodeStateIcon1.DataPropertyName = "Icon";
            this.nodeStateIcon1.LeftMargin = 0;
            this.nodeStateIcon1.ParentColumn = null;
            // 
            // TasksTreeViewControl
            // 
            this.Controls.Add(this.treeView);
            this.Name = "TasksTreeViewControl";
            this.Size = new System.Drawing.Size(230, 203);
            this.ResumeLayout(false);

		}

		#endregion

        private ImageList groupsImageList;
		private int currentSelectedTask = -1;
		private MenuItem menuItem5;
		private ContextMenu treeMenu;
		private MenuItem mnuDelete;
		private MenuItem mnuRename;
		private MenuItem mnuProperties;
        private Aga.Controls.Tree.TreeViewAdv treeView;
        private Aga.Controls.Tree.NodeControls.NodeTextBox taskNode;
        public const string NEW_TASK = "New Task";

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
            //treeView.ImageList = this.groupsImageList;
            //treeView.ImageIndex = 0;
            //treeView.SelectedImageIndex = 1;
            //treeView.LabelEdit = false;
			treeView.HideSelection = false;
            //treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
            //treeView.AfterLabelEdit += new NodeLabelEditEventHandler(treeView_AfterLabelEdit);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			Tasks.TaskChanged -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowChanged);
			//Tasks.TaskDeleting -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleting);
		}

		internal void Initialize()
		{
			LoadTree();
			Tasks.TaskChanged += new Tasks.TaskChangeEventHandler(Tasks_TasksRowChanged);
			//Tasks.TaskDeleting += new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleting);
		}

        private void LoadTree()
        {
            treeView.BeginUpdate();
            //_model.Nodes.Clear();
            Node nodeParent = CreateNode(Tasks.RootTask);
            _model.Nodes.Add(nodeParent);
            //AddChildNodes(Tasks.RootTask, nodeParent);
            treeView.EndUpdate();
        }

        private void AddChildNodes(Task parentRow, Node nodeParent)
        {
            Task[] childsRows = Tasks.GetChildTasks(parentRow.Id);
            foreach (Task task in childsRows)
            {
                if (task.Id == Tasks.IdleTask.Id)
                    continue;
                Node nodeChild = CreateNode(task);
                nodeParent.Nodes.Add(nodeChild);
                AddChildNodes(task, nodeChild);
            }
        }

		internal void AddNewTask()
		{
			int newId;
			try
			{
			    int parentId = (int) treeView.SelectedNode.Tag;
			    string newTaskName = GetNewTaskName(parentId);
                newId = Tasks.AddTask(newTaskName, parentId).Id;
			}
			catch (ApplicationException aex)
			{
				MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
			}
			Application.DoEvents();//first insert the new node (event fired)
			//treeView.LabelEdit = true;
			Node node = FindTaskNode(newId);
            
            //node.EnsureVisible();
            //treeView.SelectedNode = node;
            //node.BeginEdit();
            
		}

        private static string GetNewTaskName(int parentId)
        {
            if(Tasks.FindByParentIdAndDescription(parentId, NEW_TASK)==null)
               return NEW_TASK;
            
            int counter = 1;
            string newTaskName;
            do
            {
                newTaskName = NEW_TASK + counter;
                counter++;
            } while (Tasks.FindByParentIdAndDescription(parentId, newTaskName) != null);
            return newTaskName;
        }

        //internal void EditSelectedTaskDescription()
        //{
        //    treeView.LabelEdit = true;
        //    treeView.SelectedNode.BeginEdit();
        //}

		internal void DeleteSelectedTask()
		{
		    Node node = (Node) this.treeView.SelectedNode.Tag;
			if (MessageBox.Show(
			    	"All tasks and sub-tasks assigned to this task will be deleted too. \nAre you sure you want to delete '" +
                    node.Text + "'?",
			    	this.ParentForm.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
			    	MessageBoxOptions.DefaultDesktopOnly)
			    == DialogResult.OK)
			{
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					Tasks.DeleteTask((int) treeView.SelectedNode.Tag);
				}
				catch (ApplicationException aex)
				{
					Cursor.Current = Cursors.Default;
					MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				finally
				{
					Cursor.Current = Cursors.Default;
				}
			}
		}



		private static Node CreateNode(Task task)
		{
            Node node = new Node(task.Description);
		    node.Image = IconsManager.IconsList.Images[task.IconId];
			node.Tag = task.Id;
			return node;
		}

		private Node FindTaskNode(int taskId)
		{
            foreach (TreeNodeAdv nodeAdv in this.treeView.AllNodes)
		    {
		        Node node = (Node) nodeAdv.Tag;
                if ((int)node.Tag == taskId)
                    return node;
		    }
            //return FindNode(taskId, this.treeView.Nodes);
		    return null;
		}

        //private Node FindNode(int taskId, NodeCollection nodes)
        //{
        //    foreach (Node node in nodes)
        //    {
        //        if ((int) node.Tag == taskId)
        //        {
        //            return node;
        //        }
        //        else
        //        {
        //            if (node.Nodes.Count > 0)
        //            {
        //                Node childnode = FindNode(taskId, node.Nodes);
        //                if (childnode != null)
        //                    return childnode;
        //            }
        //        }
        //    }
        //    return null;
        //}


        //private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        //{
        //    treeView.LabelEdit = false;
        //    Task row = Tasks.FindById(Convert.ToInt32(e.Node.Tag));
        //    if (row != null)
        //    {
        //        if (e.Label == null || e.Label == String.Empty)
        //        {
        //            e.CancelEdit = true;
        //            return;
        //        }

        //        row.Description = e.Label;
        //        try
        //        {
        //            Tasks.UpdateTask(row);
        //        }
        //        catch (ApplicationException aex)
        //        {
        //            e.CancelEdit = true;
        //            MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("This task has been deleted.", this.ParentForm.Text, MessageBoxButtons.OK,
        //                        MessageBoxIcon.Information);
        //    }
        //}

		private void Tasks_TasksRowChanged(Tasks.TaskChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Add)
			{
				Node nodeParent = FindTaskNode(e.Task.ParentId);
				Node nodeChild = CreateNode(e.Task);
				nodeParent.Nodes.Add(nodeChild);
				return;
			}
			else if (e.Action == DataRowAction.Change)
			{
				Node node = FindTaskNode(e.Task.Id);
				node.Text = e.Task.Description;
			}
		}

        //private void Tasks_TasksRowDeleting(Tasks.TaskChangeEventArgs e)
        //{
        //    Node node = FindTaskNode(e.Task.Id);
        //    if (node != null && node.TreeView != null)
        //        node.Remove();
        //}


		internal int SelectedTaskId
		{
			get { return currentSelectedTask; }
			set
			{
				if (currentSelectedTask == value)
					return;
				Node node;
				node = FindTaskNode(value);
				if (node == null)
					return;
				currentSelectedTask = value;
				//treeView.SelectedNode = node;
				if (this.SelectedTaskChanged != null)
				{
					this.SelectedTaskChanged(this, new EventArgs());
				}
			}
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (currentSelectedTask != (int) e.Node.Tag)
			{
				currentSelectedTask = (int) e.Node.Tag;
				if (this.SelectedTaskChanged != null)
				{
					this.SelectedTaskChanged(sender, e);
				}
			}
		}

        //#region Drag And Drop

        //private Timer timer = new Timer();
        //private ImageList imageListDrag = new ImageList();
        //private Node dragNode = null;
        //private Node tempDropNode = null;

        //[DllImport("comctl32.dll")]
        //internal static extern bool InitCommonControls();

        //[DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool ImageList_BeginDrag(IntPtr himlTrack, int iTrack, int dxHotspot, int dyHotspot);

        //[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool ImageList_DragMove(int x, int y);

        //[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        //internal static extern void ImageList_EndDrag();

        //[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);

        //[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool ImageList_DragLeave(IntPtr hwndLock);

        //[DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool ImageList_DragShowNolock([MarshalAs(UnmanagedType.Bool)]bool fShow);

        //private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    // Get drag node and select it
        //    this.dragNode = (Node) e.Item;
        //    this.treeView.SelectedNode = this.dragNode;

        //    // Reset image list used for drag image
        //    this.imageListDrag.Images.Clear();
        //    this.imageListDrag.ImageSize =
        //    new Size(Math.Min(this.dragNode.Bounds.Size.Width + this.treeView.Indent, 256), this.dragNode.Bounds.Height);


        //    // Create new bitmap
        //    // This bitmap will contain the tree node image to be dragged
        //    Bitmap bmp = new Bitmap(Math.Min(this.dragNode.Bounds.Width + this.treeView.Indent, 256), this.dragNode.Bounds.Height);

        //    // Get graphics from bitmap
        //    using (Graphics gfx = Graphics.FromImage(bmp))
        //    {
        //        // Draw node icon into the bitmap
        //        gfx.DrawImage(this.groupsImageList.Images[0], 0, 0);

        //        // Draw node label into bitmap
        //        gfx.DrawString(this.dragNode.Text,
        //                       this.treeView.Font,
        //                       new SolidBrush(this.treeView.ForeColor),
        //                       this.treeView.Indent, 1.0f);
        //    }

        //    // Add bitmap to imagelist
        //    this.imageListDrag.Images.Add(bmp);

        //    // Get mouse position in client coordinates
        //    Point p = this.treeView.PointToClient(MousePosition);

        //    // Compute delta between mouse position and node bounds
        //    //			int dx = p.X + this.treeView.Indent - this.dragNode.Bounds.Left;
        //    //			int dy = p.Y - this.dragNode.Bounds.Top;
        //    int dx = p.X + this.treeView.Indent - this.dragNode.Bounds.Left - this.treeView.Location.X;
        //    int dy = p.Y - this.dragNode.Bounds.Top - this.treeView.Location.Y;

        //    // Begin dragging image
        //    if (ImageList_BeginDrag(this.imageListDrag.Handle, 0, dx, dy))
        //    {
        //        // Begin dragging
        //        this.treeView.DoDragDrop(bmp, DragDropEffects.Move);
        //        // End dragging image
        //        ImageList_EndDrag();
        //    }
        //}

        //private void treeView_DragOver(object sender, DragEventArgs e)
        //{
        //    // Compute drag position and move image
        //    Point formP = this.PointToClient(new Point(e.X, e.Y));
        //    ImageList_DragMove(formP.X - this.treeView.Left, formP.Y - this.treeView.Top);

        //    // Get actual drop node
        //    Node dropNode = this.treeView.GetNodeAt(this.treeView.PointToClient(new Point(e.X, e.Y)));
        //    if (dropNode == null)
        //    {
        //        e.Effect = DragDropEffects.None;
        //        return;
        //    }

        //    e.Effect = DragDropEffects.Move;

        //    // if mouse is on a new node select it
        //    if (this.tempDropNode != dropNode)
        //    {
        //        ImageList_DragShowNolock(false);
        //        this.treeView.SelectedNode = dropNode;
        //        ImageList_DragShowNolock(true);
        //        tempDropNode = dropNode;
        //    }

        //    // Avoid that drop node is child of drag node 
        //    Node tmpNode = dropNode;
        //    while (tmpNode.Parent != null)
        //    {
        //        if (tmpNode.Parent == this.dragNode) e.Effect = DragDropEffects.None;
        //        tmpNode = tmpNode.Parent;
        //    }
        //}

        //private void treeView_DragDrop(object sender, DragEventArgs e)
        //{
        //    // Unlock updates
        //    ImageList_DragLeave(this.treeView.Handle);

        //    // Get drop node
        //    Node dropNode = this.treeView.GetNodeAt(this.treeView.PointToClient(new Point(e.X, e.Y)));

        //    // If drop node isn't equal to drag node, add drag node as child of drop node
        //    if (this.dragNode != dropNode)
        //    {
        //        // Remove drag node from parent
        //        if (this.dragNode.Parent == null)
        //        {
        //            this.treeView.Nodes.Remove(this.dragNode);
        //        }
        //        else
        //        {
        //            this.dragNode.Parent.Nodes.Remove(this.dragNode);
        //        }

        //        // Add drag node to drop node
        //        dropNode.Nodes.Add(this.dragNode);
        //        dropNode.Expand();

        //        try
        //        {
        //            Cursor.Current = Cursors.WaitCursor;
        //            Tasks.UpdateParentTask((int) this.dragNode.Tag, (int) dropNode.Tag);
        //        }
        //        catch (ApplicationException aex)
        //        {
        //            Cursor.Current = Cursors.Default;
        //            MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        finally
        //        {
        //            Cursor.Current = Cursors.Default;
        //        }

        //        // Set drag node to null
        //        this.dragNode = null;

        //        // Disable scroll timer
        //        this.timer.Enabled = false;
        //    }
        //}

        //private void treeView_DragEnter(object sender, DragEventArgs e)
        //{
        //    ImageList_DragEnter(this.treeView.Handle, e.X - this.treeView.Left,
        //                        e.Y - this.treeView.Top);

        //    // Enable timer for scrolling dragged item
        //    this.timer.Enabled = true;
        //}

        //private void treeView_DragLeave(object sender, EventArgs e)
        //{
        //    ImageList_DragLeave(this.treeView.Handle);

        //    // Disable timer for scrolling dragged item
        //    this.timer.Enabled = false;
        //}

        //private void treeView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        //{
        //    if (e.Effect == DragDropEffects.Move)
        //    {
        //        // Show pointer cursor while dragging
        //        e.UseDefaultCursors = false;
        //        this.treeView.Cursor = Cursors.Default;
        //    }
        //    else e.UseDefaultCursors = true;
        //}

        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    // get node at mouse position
        //    Point pt = treeView.PointToClient(MousePosition);
        //    Node node = this.treeView.GetNodeAt(pt);
        //    if(node == null) return;

        //    // if mouse is near to the top, scroll up
        //    if (pt.Y < 30)
        //    {
        //        // set actual node to the upper one
        //        if (node.PrevVisibleNode != null)
        //        {
        //            node = node.PrevVisibleNode;

        //            // hide drag image
        //            ImageList_DragShowNolock(false);
        //            // scroll and refresh
        //            node.EnsureVisible();
        //            this.treeView.Refresh();
        //            // show drag image
        //            ImageList_DragShowNolock(true);
        //        }
        //    }
        //        // if mouse is near to the bottom, scroll down
        //    else if (pt.Y > this.treeView.Size.Height - 30)
        //    {
        //        if (node.NextVisibleNode != null)
        //        {
        //            node = node.NextVisibleNode;

        //            ImageList_DragShowNolock(false);
        //            node.EnsureVisible();
        //            this.treeView.Refresh();
        //            ImageList_DragShowNolock(true);
        //        }
        //    }
        //}

        //#endregion

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			this.DeleteSelectedTask();
		}

		private void mnuRename_Click(object sender, EventArgs e)
		{
			//this.EditSelectedTaskDescription();
		}

		public void ShowPropertiesSelectedTask()
		{
			TaskPropertiesForm pf;
			pf = new TaskPropertiesForm((int) treeView.SelectedNode.Tag);
			pf.ShowDialog(this);
		}

		private void mnuProperties_Click(object sender, EventArgs e)
		{
			ShowPropertiesSelectedTask();
		}

		private void treeView_DoubleClick(object sender, EventArgs e)
		{
			this.OnDoubleClick(e);
		}
	}
}