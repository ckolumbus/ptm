using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Timers;
using System.Windows.Forms;
using PTM.Addin;
using PTM.Framework;
using PTM.Framework.Helpers;
using PTM.Framework.Infos;
using PTM.View.Controls.TreeListViewComponents;
using PTM.View.Forms;
using PopupControl;
using Timer=System.Timers.Timer;

namespace PTM.View.Controls
{
	/// <summary>
	/// Summary description for TasksLog.
	/// </summary>
	internal class TasksLogControl : AddinTabPage
	{
		private Button editButton;
		private Button addTaskButton;
		private ColumnHeader TaskDescriptionHeader;
        private ColumnHeader DurationTaskHeader;
		private ContextMenu notifyContextMenu;
		private Timer notifyTimer;
		private NotifyIcon notifyIcon;
		private IContainer components;
		private TreeListView taskList;
		private Button propertiesButton;
		private Button deleteButton;
		private ColumnHeader StartTimeHeader;
		private DateTimePicker logDate;
		private Label label1;
		private ContextMenu rigthClickMenu;
		private ToolTip shortcutToolTip;
		private CheckBox pathCheckBox;
		private DateTime currentDay;
        private BackgroundWorker worker = new BackgroundWorker();
        private CodeProject.SystemHotkey.SystemHotkey hotKey;
        private Popup popup;

        internal TasksLogControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

            hotKey = new CodeProject.SystemHotkey.SystemHotkey(this.components);
            hotKey.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftT;
            hotKey.Pressed += new System.EventHandler(hotKey_Pressed);

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            notifyIcon.MouseDown+=new MouseEventHandler(notifyIcon_MouseDown);
			notifyTimer.Elapsed += new ElapsedEventHandler(notifyTimer_Elapsed);
            notifyIcon.Click+=new EventHandler(notifyIcon_Click);
			addTaskButton.Click += new EventHandler(addTaskButton_Click);
			this.taskList.DoubleClick += new EventHandler(taskList_DoubleClick);
            this.logDate.CloseUp += new EventHandler(logDate_CloseUp);
            this.logDate.DropDown += new EventHandler(logDate_DropDown);

			Tasks.TaskChanged += new Tasks.TaskChangeEventHandler(TasksDataTable_TasksRowChanged);
			Tasks.TaskDeleting += new Tasks.TaskChangeEventHandler(TasksDataTable_TasksRowDeleting);
			Tasks.TaskDeleted += new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleted);
			Logs.LogChanged += new Logs.LogChangeEventHandler(TasksLog_LogChanged);
            Logs.CurrentLogDurationChanged += new ElapsedEventHandler(Logs_CurrentLogDurationChanged);
			ApplicationsLog.ApplicationsLogChanged +=
				new ApplicationsLog.ApplicationLogChangeEventHandler(ApplicationsLog_ApplicationsLogChanged);
			this.taskList.SmallImageList = IconsManager.IconsList;
			this.Load += new EventHandler(TasksLogControl_Load);

			Configuration config;
			config = ConfigurationHelper.GetConfiguration(ConfigurationKey.ShowTasksFullPath);

			if (config.Value.ToString().CompareTo("1") == 0)
				this.pathCheckBox.Checked = true;
			else
				this.pathCheckBox.Checked = false;

			this.Status = String.Empty;

			CreateRigthClickMenu();
			CreateNotifyMenu();
		}

        private void hotKey_Pressed(object sender, System.EventArgs e)
        {
            NewTaskLog(false);
        }

	    private DateTime dateBeforeDropDown;
        void logDate_DropDown(object sender, EventArgs e)
        {
            dateBeforeDropDown = logDate.Value;
            this.logDate.ValueChanged -= new EventHandler(this.logDate_ValueChanged);
        }

        void logDate_CloseUp(object sender, EventArgs e)
        {
            this.logDate.ValueChanged += new EventHandler(this.logDate_ValueChanged);
            if (!logDate.Value.Equals(dateBeforeDropDown))
                GetLogsAsync();
        }
        
		protected override void OnHandleDestroyed(EventArgs e)
		{
			Tasks.TaskChanged -= new Tasks.TaskChangeEventHandler(TasksDataTable_TasksRowChanged);
			Tasks.TaskDeleting -= new Tasks.TaskChangeEventHandler(TasksDataTable_TasksRowDeleting);
			Tasks.TaskDeleted -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleted);
			Logs.LogChanged -= new Logs.LogChangeEventHandler(TasksLog_LogChanged);
            Logs.CurrentLogDurationChanged -= new ElapsedEventHandler(Logs_CurrentLogDurationChanged);
			ApplicationsLog.ApplicationsLogChanged -=
				new ApplicationsLog.ApplicationLogChangeEventHandler(ApplicationsLog_ApplicationsLogChanged);
			base.OnHandleDestroyed(e);
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.notifyIcon.Dispose();
				if (components != null)
				{
					components.Dispose();
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TasksLogControl));
            PTM.View.Controls.TreeListViewComponents.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new PTM.View.Controls.TreeListViewComponents.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            this.editButton = new System.Windows.Forms.Button();
            this.addTaskButton = new System.Windows.Forms.Button();
            this.notifyContextMenu = new System.Windows.Forms.ContextMenu();
            this.TaskDescriptionHeader = new System.Windows.Forms.ColumnHeader();
            this.StartTimeHeader = new System.Windows.Forms.ColumnHeader();
            this.DurationTaskHeader = new System.Windows.Forms.ColumnHeader();
            this.notifyTimer = new System.Timers.Timer();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
            this.rigthClickMenu = new System.Windows.Forms.ContextMenu();
            this.propertiesButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.logDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.shortcutToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pathCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.notifyTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Location = new System.Drawing.Point(152, 280);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(72, 23);
            this.editButton.TabIndex = 3;
            this.editButton.Text = "Edit...";
            this.shortcutToolTip.SetToolTip(this.editButton, "Enter");
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addTaskButton
            // 
            this.addTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addTaskButton.Location = new System.Drawing.Point(312, 280);
            this.addTaskButton.Name = "addTaskButton";
            this.addTaskButton.Size = new System.Drawing.Size(72, 23);
            this.addTaskButton.TabIndex = 5;
            this.addTaskButton.Text = "New...";
            this.shortcutToolTip.SetToolTip(this.addTaskButton, "Ins");
            // 
            // TaskDescriptionHeader
            // 
            this.TaskDescriptionHeader.Text = "Task Description";
            this.TaskDescriptionHeader.Width = 226;
            // 
            // StartTimeHeader
            // 
            this.StartTimeHeader.Text = "Start Time";
            this.StartTimeHeader.Width = 80;
            // 
            // DurationTaskHeader
            // 
            this.DurationTaskHeader.Text = "Duration";
            this.DurationTaskHeader.Width = 65;
            // 
            // notifyTimer
            // 
            this.notifyTimer.Interval = 1000;
            this.notifyTimer.SynchronizingObject = this;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenu = this.notifyContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Tag = null;
            this.notifyIcon.Text = "PTM";
            this.notifyIcon.Visible = true;
            // 
            // taskList
            // 
            this.taskList.AllowColumnReorder = true;
            this.taskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.taskList.AutoArrange = false;
            this.taskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskDescriptionHeader,
            this.DurationTaskHeader,
            this.StartTimeHeader});
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.None;
            this.taskList.Comparer = treeListViewItemCollectionComparer1;
            this.taskList.ContextMenu = this.rigthClickMenu;
            this.taskList.HideSelection = false;
            this.taskList.Location = new System.Drawing.Point(8, 32);
            this.taskList.Name = "taskList";
            this.taskList.Size = new System.Drawing.Size(376, 240);
            this.taskList.Sorting = System.Windows.Forms.SortOrder.None;
            this.taskList.TabIndex = 1;
            this.taskList.UseCompatibleStateImageBehavior = false;
            this.taskList.SelectedIndexChanged += new System.EventHandler(this.taskList_SelectedIndexChanged);
            this.taskList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.taskList_KeyDown);
            // 
            // propertiesButton
            // 
            this.propertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesButton.Location = new System.Drawing.Point(232, 280);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(72, 23);
            this.propertiesButton.TabIndex = 4;
            this.propertiesButton.Text = "Properties...";
            this.shortcutToolTip.SetToolTip(this.propertiesButton, "Ctrl + P");
            this.propertiesButton.Click += new System.EventHandler(this.switchToButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(72, 280);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(72, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.shortcutToolTip.SetToolTip(this.deleteButton, "Del");
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // logDate
            // 
            this.logDate.Location = new System.Drawing.Point(48, 8);
            this.logDate.Name = "logDate";
            this.logDate.Size = new System.Drawing.Size(248, 20);
            this.logDate.TabIndex = 0;
            this.logDate.ValueChanged += new System.EventHandler(this.logDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pathCheckBox
            // 
            this.pathCheckBox.Location = new System.Drawing.Point(304, 8);
            this.pathCheckBox.Name = "pathCheckBox";
            this.pathCheckBox.Size = new System.Drawing.Size(80, 24);
            this.pathCheckBox.TabIndex = 8;
            this.pathCheckBox.Text = "Show path";
            this.pathCheckBox.CheckedChanged += new System.EventHandler(this.pathCheckBox_CheckedChanged);
            // 
            // TasksLogControl
            // 
            this.Controls.Add(this.pathCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logDate);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.propertiesButton);
            this.Controls.Add(this.taskList);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addTaskButton);
            this.Name = "TasksLogControl";
            this.Size = new System.Drawing.Size(392, 312);
            this.shortcutToolTip.SetToolTip(this, "Shift + P");
            ((System.ComponentModel.ISupportInitialize)(this.notifyTimer)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		#region TaskLog

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetLogDay((GetLogsResult) e.Result);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetLogs();
        }

		private void TasksLogControl_Load(object sender, EventArgs e)
		{
			this.currentDay = DateTime.Today;
            GetLogsResult result = this.GetLogs();
            SetLogDay(result);
			AddIdleTaskLog();
		}

		internal void NewTaskLog(bool mustAddATask)
		{
			notifyTimer.Stop();
			TaskSelectForm tasklog = new TaskSelectForm();
			if (tasklog.ShowDialog(this) == DialogResult.OK)
			{
				AddTaskLog(tasklog.SelectedTaskId,
				           (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value);
			}
			else if (mustAddATask)
			{
				AddIdleTaskLog();
			}
		}

		private void AddIdleTaskLog()
		{
			Logs.AddIdleTaskLog();
			ResetNotifyTimer((int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value);
		}

		private void AddTaskLog(int taskId, int defaultMins)
		{
			Logs.AddLog(taskId);
			ResetNotifyTimer(defaultMins);
		}

		private void ResetNotifyTimer(int defaultMins)
		{
			notifyTimer.Stop();
		    notifyTimer.Interval = 1000*60*defaultMins;
            //notifyTimer.Interval = 1000 * 20;
			notifyTimer.Start();
		}

		private void EditSelectedTaskLog()
		{
            if (taskList.SelectedItems.Count == 0)
                return;
            if (!isValidEditableLog(taskList.SelectedItems[0]))
				return;
			int taskId = ((Log) taskList.SelectedItems[0].Tag).TaskId;

			TaskSelectForm taskSelectForm = new TaskSelectForm(taskId);
			if (taskSelectForm.ShowDialog(this.Parent) == DialogResult.OK)
			{
				for (int i = 0; i < taskList.SelectedItems.Count; i++)
				{
					int taskLogId = ((Log) taskList.SelectedItems[i].Tag).Id;
					Logs.UpdateLogTaskId(taskLogId, taskSelectForm.SelectedTaskId);
				}
			}
		}

        private void ShowTaskProperties()
        {
            if (taskList.SelectedItems.Count == 0)
                return;
            int taskId = ((Log)taskList.SelectedItems[0].Tag).TaskId;
            TaskPropertiesForm pf;
            pf = new TaskPropertiesForm(taskId);
            pf.ShowDialog(this);
        }

		private void DeleteSelectedTaskLog()
		{
			for (int i = 0; i < taskList.SelectedItems.Count; i++)
			{
                if (!isValidEditableLog(taskList.SelectedItems[i]))
                    continue;
				int taskLogId = ((Log) taskList.SelectedItems[i].Tag).Id;
				Logs.DeleteLog(taskLogId);
			}
		}

	    private int copiedTaskId = -1;
        private void CopySelectedTaskLog()
        {
            for (int i = 0; i < taskList.SelectedItems.Count; i++)
            {
                if (!isValidEditableLog(taskList.SelectedItems[i]))
                    continue;
                copiedTaskId = ((Log)taskList.SelectedItems[i].Tag).TaskId;
                break;
            }
        }
        private void PasteSelectedTaskLog()
        {
            if(copiedTaskId==-1)
                return;
            for (int i = 0; i < taskList.SelectedItems.Count; i++)
            {
                if (!isValidEditableLog(taskList.SelectedItems[i]))
                    continue;
                int taskLogId = ((Log)taskList.SelectedItems[i].Tag).Id;
                Logs.UpdateLogTaskId(taskLogId, copiedTaskId);
            }
        }


        private void DeleteSelectedAppLog()
        {
            for (int i = 0; i < taskList.SelectedItems.Count; i++)
            {
                if (taskList.SelectedItems[i].Parent==null)
                    continue;
                int appId = ((ApplicationLog)taskList.SelectedItems[i].Tag).Id;
                try
                {
                    ApplicationsLog.DeleteApplicationLog(appId);
                }
                catch(ApplicationException aex)
                {
                    MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

	    private void addTaskButton_Click(object sender, EventArgs e)
		{
			NewTaskLog(false);
		}

		private void editButton_Click(object sender, EventArgs e)
		{
			EditSelectedTaskLog();
		}

		private void taskList_DoubleClick(object sender, EventArgs e)
		{
			EditSelectedTaskLog();
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			DeleteSelectedTaskLog();
            DeleteSelectedAppLog();
		}

		private static bool isValidEditableLog(TreeListViewItem item)
		{
            if (item.Parent != null)
				return false;

			return true;
		}

		private void switchToButton_Click(object sender, EventArgs e)
		{
            ShowTaskProperties();
		}

		private void taskList_SelectedIndexChanged(object sender, EventArgs e)
		{
            DisplaySelectedItemStatus();

			if (this.taskList.SelectedItems.Count <= 0)
			{
				SetNoEditable();
			    return;
			}

		    if (this.taskList.SelectedItems.Count == 1)
			{
				if (taskList.SelectedItems[0].Parent == null)
				{
					SetEditable();
				}
				else
				{
					SetNoEditable();
				}
			}
			else
			{
				if (taskList.SelectedItems[0].Parent != null)
				{
					SetNoEditable();
					return;
				}
				for (int i = 1; i < this.taskList.SelectedItems.Count; i++)
				{
					if (taskList.SelectedItems[i].Parent != null)
					{
						SetNoEditable();
						return;
					}
				}
				SetEditable();
			}
		}

	    private IDictionary taskExecutedTimeCache = new HybridDictionary();
        private int GetCachedExecutedTime(int taskId)
        {
            if (taskExecutedTimeCache.Contains(taskId))
            {
                return (int)taskExecutedTimeCache[taskId];
            }
            else
            {
                int executedTime = TasksSummaries.GetExecutedTime(taskId);
                taskExecutedTimeCache.Add(taskId, executedTime);
                return executedTime;
            }            
        }


	    private void DisplaySelectedItemStatus()
	    {
	        if (this.taskList.SelectedItems.Count <= 0)
	        {
	            this.Status = String.Empty;
	            return;
	        }
            if (taskList.SelectedItems[0].Parent != null)
            {
                this.Status = String.Empty;
                return;
            }
            else
            {
                int taskId = ((Log) taskList.SelectedItems[0].Tag).TaskId;

                if(taskId == Tasks.IdleTask.Id)
                {
                    this.Status = String.Empty;
                    return;
                }

                int executedTime = GetCachedExecutedTime(taskId);
                TimeSpan executedTimeSpan = new TimeSpan(0, 0, executedTime);

                Task task = Tasks.FindById(taskId);

                if(task.Estimation>0)
                {
                    TimeSpan estimatedTimeSpan = new TimeSpan(0, task.Estimation, 0);
                    double percentGoal = executedTime / (task.Estimation*60.0);
                    this.Status = "Time elapsed: " + ViewHelper.TimeSpanToTimeString(executedTimeSpan) +
                        "     Estimated:" + ViewHelper.TimeSpanToTimeString(estimatedTimeSpan) +
                    "     % Elapsed:" + percentGoal.ToString("0.0%", CultureInfo.InvariantCulture);
                }
                else
                {
                    this.Status = "Time elapsed: " + ViewHelper.TimeSpanToTimeString(executedTimeSpan) +
                    "     Not estimated.";
                }
                
            }
	    }

	    private void SetEditable()
		{
			this.editButton.Enabled = true;
			this.propertiesButton.Enabled = true;
			this.taskList.ContextMenu = this.rigthClickMenu;
		}

		private void SetNoEditable()
		{
			this.editButton.Enabled = false;
			this.propertiesButton.Enabled = false;
			this.taskList.ContextMenu = null;
		}

		private void logDate_ValueChanged(object sender, EventArgs e)
		{
		    GetLogsAsync();
		}

	    private void mnuEdit_Click(object sender, EventArgs e)
		{
			EditSelectedTaskLog();
		}

		private void mnuShowProperties_Click(object sender, EventArgs e)
		{
			ShowTaskProperties();
		}

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			DeleteSelectedTaskLog();
		}

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            CopySelectedTaskLog();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            PasteSelectedTaskLog();
        }

		private void CreateRigthClickMenu()
		{
			MenuItem mnuEdit = new MenuItem();
			MenuItem mnuShowProperties = new MenuItem();
			MenuItem mnuDelete = new MenuItem();
            MenuItem mnuCopy = new MenuItem();
            MenuItem mnuPaste = new MenuItem();
			MenuItem menuItem11 = new MenuItem();
            
            TaskMenuItem idleMenuItem = new TaskMenuItem(Tasks.IdleTask.Id);
            idleMenuItem.Text = Tasks.IdleTask.Description;
            idleMenuItem.Pick += new EventHandler(mnuTaskSetTo_Click);

			this.rigthClickMenu.MenuItems.Clear();
			this.rigthClickMenu.MenuItems.AddRange(new MenuItem[]
			                                       	{
			                                       		mnuEdit,
			                                       		mnuShowProperties,
			                                       		mnuDelete,
                                                        mnuCopy,
                                                        mnuPaste,
			                                       		menuItem11,
                                                        idleMenuItem
			                                       	});

			mnuEdit.DefaultItem = true;
			mnuEdit.Index = 0;
			mnuEdit.Text = "Edit...";
			mnuEdit.Click += new EventHandler(this.mnuEdit_Click);

			mnuShowProperties.Index = 1;
			mnuShowProperties.Shortcut = Shortcut.CtrlP;
			mnuShowProperties.Text = "Show task properties...";
			mnuShowProperties.Click += new EventHandler(this.mnuShowProperties_Click);

			mnuDelete.Index = 2;
			mnuDelete.Shortcut = Shortcut.Del;
			mnuDelete.Text = "Delete";
			mnuDelete.Click += new EventHandler(this.mnuDelete_Click);

            mnuCopy.Index = 3;
            mnuCopy.Shortcut = Shortcut.CtrlC;
            mnuCopy.Text = "Copy";
            mnuCopy.Click += new EventHandler(this.mnuCopy_Click);

            mnuPaste.Index = 4;
            mnuPaste.Shortcut = Shortcut.CtrlV;
            mnuPaste.Text = "Paste";
            mnuPaste.Click += new EventHandler(this.mnuPaste_Click);

			menuItem11.Index = 5;
			menuItem11.Text = "-";

			ArrayList a = new ArrayList();
			Task[] tasks;
			tasks = Tasks.GetChildTasks(Tasks.RootTask.Id);
			foreach (Task task in tasks)
			{
                if (task.Id == Tasks.IdleTask.Id)
                    continue;
                if (task.Hidden) continue;
			    
				TaskMenuItem menuItem = new TaskMenuItem(task.Id);
				menuItem.Text = task.Description;
				menuItem.Pick += new EventHandler(mnuTaskSetTo_Click);

				a.Add(menuItem);
				AddSubTasks(task, menuItem, new EventHandler(mnuTaskSetTo_Click));
			}
			ContextMenu defaultTasksMenu = new ContextMenu((TaskMenuItem[]) a.ToArray(typeof (TaskMenuItem)));
			this.rigthClickMenu.MergeMenu(defaultTasksMenu);
		}

		private void mnuTaskSetTo_Click(object sender, EventArgs e)
		{
			TaskMenuItem mnu = (TaskMenuItem) sender;
			foreach (ListViewItem item in this.taskList.SelectedItems)
			{
                if (item.Tag is Log) //see bug 2253519
                {
                    Log log = (Log)item.Tag;
                    Logs.UpdateLogTaskId(log.Id, mnu.TaskId);                   
                }
			}
		}


		private void taskList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				EditSelectedTaskLog();
			}
			if (e.KeyData == Keys.Insert)
			{
				NewTaskLog(false);
			}
		}

		private void pathCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				for (int i = 0; i < this.taskList.Items.Count; i++)
				{
					TreeListViewItem item = this.taskList.Items[i];
					if (((Log) item.Tag).TaskId == Tasks.IdleTask.Id)
						continue;
					if (this.pathCheckBox.Checked)
					{
						item.Text = Tasks.GetFullPath(((Log) item.Tag).TaskId);
					}
					else
					{
						item.Text = Tasks.FindById(((Log) item.Tag).TaskId).Description;
					}
				}
				Configuration config;
				config = ConfigurationHelper.GetConfiguration(ConfigurationKey.ShowTasksFullPath);
				if (this.pathCheckBox.Checked)
					config.Value = "1";
				else
					config.Value = "0";
				ConfigurationHelper.SaveConfiguration(config);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

        private void SetLogDay(GetLogsResult result)
		{
			try
			{
				taskList.BeginUpdate();
                foreach (Log log in result.LogList)
				{
					Task taskRow = Tasks.FindById(log.TaskId);
					TreeListViewItem itemA = new TreeListViewItem("", new string[] {"", ""});
					SetListItemValues(itemA, log, taskRow);
					taskList.Items.Insert(0, itemA);
					foreach (ApplicationLog applicationLog in log.ApplicationsLog)
					{
						UpdateApplicationsList(applicationLog, DataRowAction.Add);
					}
				}
			}
			finally
			{
				taskList.EndUpdate();
				SetReadyState();
			}
		}

		#endregion

		#region Notifications

		private NotifyForm notifyForm;

		private void notifyIcon_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.Visible == false)
				{
					this.ParentForm.Activate();
					this.ParentForm.Visible = true;
				}
				if (this.ParentForm.WindowState == FormWindowState.Minimized)
					this.ParentForm.WindowState = FormWindowState.Normal;
			}
		}

		private void notifyTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
//			notifyForm = new NotifyForm(this.notifyIcon.Text);
//			notifyForm.ShowNoActivate();
//			notifyForm.Closed += new EventHandler(notifyForm_Closed);
            notifyForm = new NotifyForm(this.notifyIcon.Text);

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int clientWidth = notifyForm.Width;
            int clientHeight = notifyForm.Height;
            Rectangle area = new Rectangle(screenWidth - clientWidth, screenHeight - clientHeight,
                clientWidth, clientHeight);

            popup = new Popup(this.notifyForm);
            popup.Top = screenHeight - clientHeight;
            popup.Left = screenWidth - clientWidth;

            popup.Closed += new ToolStripDropDownClosedEventHandler(notifyPopup_Closed);
            notifyForm.Prepare();
            popup.Show(new Point(screenWidth - clientWidth, screenHeight - clientHeight));
		}

        void notifyPopup_Closed(object sender, EventArgs e)
        {
            if (notifyForm.Result == NotifyForm.NotifyResult.Cancel || 
                notifyForm.Result == NotifyForm.NotifyResult.Waiting)
            {
                this.AddIdleTaskLog();
            }
            else if (notifyForm.Result == NotifyForm.NotifyResult.No)
            {
                NewTaskLog(true);
            }
            else if (notifyForm.Result == NotifyForm.NotifyResult.Yes)
            {
                AddTaskLog(Tasks.CurrentTask.Id,
                           (int)ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

		private void notifyIcon_Click(object sender, EventArgs e)
		{
			this.ParentForm.Activate();
		}

		#endregion

		#region NotifyContextMenu

		internal event EventHandler Exit;

		private void CreateNotifyMenu()
		{
			MenuItem exitContextMenuItem = new MenuItem();
			MenuItem menuItem1 = new MenuItem();
            menuItem1.Text = "-";
            TaskMenuItem idleMenuItem = new TaskMenuItem(Tasks.IdleTask.Id);
            idleMenuItem.Text = Tasks.IdleTask.Description;
            idleMenuItem.Pick += new EventHandler(mnuTaskAdd_Click);

			this.notifyContextMenu = new ContextMenu();
			this.notifyContextMenu.MenuItems.AddRange(new MenuItem[]
			                                          	{
			                                          		exitContextMenuItem,
			                                          		menuItem1,
                                                            idleMenuItem
			                                          	});

			exitContextMenuItem.Index = 0;
			exitContextMenuItem.Text = "Exit";
			exitContextMenuItem.Click += new EventHandler(exitContextMenuItem_Click);			


			ArrayList a = new ArrayList();
			Task[] tasks;
			tasks = Tasks.GetChildTasks(Tasks.RootTask.Id);
			foreach (Task task in tasks)
			{
                if (task.Id == Tasks.IdleTask.Id)
                    continue;
                if(task.Hidden)
                    continue;
				TaskMenuItem menuItem = new TaskMenuItem(task.Id);
				menuItem.Text = task.Description;
				menuItem.Pick += new EventHandler(mnuTaskAdd_Click);
				a.Add(menuItem);
				AddSubTasks(task, menuItem, new EventHandler(mnuTaskAdd_Click));
			}

			ContextMenu defaultTasksMenu = new ContextMenu((TaskMenuItem[]) a.ToArray(typeof (TaskMenuItem)));
			this.notifyContextMenu.MergeMenu(defaultTasksMenu);
			this.notifyIcon.ContextMenu = this.notifyContextMenu;
		}

		private void AddSubTasks(Task parentTask, TaskMenuItem menuItem, EventHandler handler)
		{
			//ArrayList a = new ArrayList();
			Task[] tasks;
			tasks = Tasks.GetChildTasks(parentTask.Id);
			if (tasks.Length == 0)
				return;
			foreach (Task task in tasks)
			{
				TaskMenuItem subMenu = new TaskMenuItem(task.Id);
				subMenu.Text = task.Description;

				subMenu.Pick += handler;
				//a.Add(subMenu);
				menuItem.MenuItems.Add(subMenu);
				AddSubTasks(task, subMenu, handler);
			}
			//menuItem.MenuItems.AddRange((TaskMenuItem[]) a.ToArray(typeof (TaskMenuItem)));
		}

		private void exitContextMenuItem_Click(object sender, EventArgs e)
		{
			this.notifyTimer.Stop();
			this.notifyIcon.Visible = false;
			this.Exit(this, e);
		}

		private void mnuTaskAdd_Click(object sender, EventArgs e)
		{
			TaskMenuItem mnu = (TaskMenuItem) sender;
			AddTaskLog(mnu.TaskId, (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value);
		}

		#endregion

		#region Framework events

		private void TasksDataTable_TasksRowChanged(Tasks.TaskChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Change)
			{
				foreach (ListViewItem item in this.taskList.Items)
				{
					if (((Log) item.Tag).TaskId == e.Task.Id)
					{
                        if (this.pathCheckBox.Checked)
                            item.SubItems[TaskDescriptionHeader.Index].Text = Tasks.GetFullPath(e.Task.Id);
                        else
						    item.SubItems[TaskDescriptionHeader.Index].Text = e.Task.Description;
						item.ImageIndex = e.Task.IconId;
					}
				}
			}
            if(e.Task.Id == Tasks.CurrentTask.Id)
		        UpdateNotifyIcon();
			CreateNotifyMenu();
			CreateRigthClickMenu();
		    DisplaySelectedItemStatus();
		}

		private void TasksDataTable_TasksRowDeleting(Tasks.TaskChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Delete)
			{
				for (int i =0; i<this.taskList.Items.Count;i++)
				{
					if (((Log) this.taskList.Items[i].Tag).TaskId == e.Task.Id)
					{
						this.taskList.Items.RemoveAt(i);
						return;
					}
				}
			}
		}

		private void Tasks_TasksRowDeleted(Tasks.TaskChangeEventArgs e)
		{
			CreateNotifyMenu();
			CreateRigthClickMenu();
            DisplaySelectedItemStatus();
		}

        delegate void TasksLog_LogChangedDelegate(Logs.LogChangeEventArgs e);
		private void TasksLog_LogChanged(Logs.LogChangeEventArgs e)
		{
            if (this.InvokeRequired)
            {
                TasksLog_LogChangedDelegate d = new TasksLog_LogChangedDelegate(TasksLog_LogChanged);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                Task taskRow;
                if (e.Action == DataRowAction.Change)
                {
                    if (e.Log.InsertTime.Date != this.currentDay)
                        return;

                    taskRow = Tasks.FindById(e.Log.TaskId);
                    foreach (TreeListViewItem item in this.taskList.Items)
                    {
                        if (((Log) item.Tag).Id == e.Log.Id)
                        {
                            SetListItemValues(item, e.Log, taskRow);
                            break;
                        }
                    }
                }
                else if (e.Action == DataRowAction.Add)
                {
                    CheckCurrentDayChanged();
                    if (this.logDate.Value.Date == currentDay)
                    {
                        //unbold no current log font.
                        foreach (TreeListViewItem item in this.taskList.Items)
                        {
                            if (item.Font.Bold && Logs.CurrentLog != null && ((Log)item.Tag).Id != Logs.CurrentLog.Id)
                            {
                                item.Font = new Font(item.Font, FontStyle.Regular);
                                break;
                            }
                        }

                        taskRow = Tasks.FindById(e.Log.TaskId);
                        TreeListViewItem itemA = new TreeListViewItem("", new string[] {"", ""});
                        itemA.Font = new Font(itemA.Font, FontStyle.Bold);
                        SetListItemValues(itemA, e.Log, taskRow);
                        taskList.Items.Insert(0, itemA);
                    }
                }
                if(e.Log.Id == Logs.CurrentLog.Id)
                    UpdateNotifyIcon();
                DisplaySelectedItemStatus();
            }
		}

	    private delegate void Logs_CurrentLogDurationChangedDelegate(object sender, ElapsedEventArgs e);
        private void Logs_CurrentLogDurationChanged(object sender, ElapsedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Logs_CurrentLogDurationChangedDelegate d = new Logs_CurrentLogDurationChangedDelegate(Logs_CurrentLogDurationChanged);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {

                foreach (TreeListViewItem item in this.taskList.Items)
                {
                    if (Logs.CurrentLog != null && ((Log)item.Tag).Id == Logs.CurrentLog.Id)
                    {
                        item.SubItems[DurationTaskHeader.Index].Text = ViewHelper.Int32ToTimeString(Logs.CurrentLog.Duration);
                        if (!item.Font.Bold)
                            item.Font = new Font(item.Font, FontStyle.Bold);                        
                        break;
                    }
                }

                //update cache
                if (taskExecutedTimeCache.Contains(Tasks.CurrentTask.Id))
                    taskExecutedTimeCache[Tasks.CurrentTask.Id] = (int) taskExecutedTimeCache[Tasks.CurrentTask.Id]+1;

                if (this.taskList.SelectedItems.Count > 0 && this.taskList.SelectedItems[0].Parent == null
                    && ((Log)this.taskList.SelectedItems[0].Tag).TaskId == Tasks.CurrentTask.Id)
                    DisplaySelectedItemStatus();

            }
        }

	    private void UpdateNotifyIcon()
	    {
	        notifyIcon.Text = Tasks.CurrentTask.Description.Substring(0, Math.Min(Tasks.CurrentTask.Description.Length, 63)); //notifyIcon supports 64 chars
	        notifyIcon.Icon = (Icon)IconsManager.CommonTaskIconsTable[Tasks.CurrentTask.IconId];
	        notifyIcon.Tag = Tasks.CurrentTask.Id;
	    }

	    private void CheckCurrentDayChanged()
		{
			if (currentDay != DateTime.Today)
			{
				this.taskList.Items.Clear();
				currentDay = DateTime.Today;
				this.logDate.Value = currentDay;
			}
		}

		private void SetListItemValues(ListViewItem item, Log log, Task taskRow)
		{
			item.Tag = log;
            if (taskRow!=null)
            {
                if (this.pathCheckBox.Checked && taskRow.Id != Tasks.IdleTask.Id)
                    item.Text = Tasks.GetFullPath(taskRow.Id);
                else
                    item.Text = taskRow.Description;

                item.ImageIndex = taskRow.IconId;
            }

			item.SubItems[DurationTaskHeader.Index].Text = ViewHelper.Int32ToTimeString(log.Duration);
			//item.SubItems[StartTimeHeader.Index].Text = log.InsertTime.ToShortTimeString();
			CultureInfo cultureInfo;
			cultureInfo = (CultureInfo) CultureInfo.CurrentCulture.Clone();
			cultureInfo.DateTimeFormat.ShortTimePattern = "hh:mm tt";
			cultureInfo.DateTimeFormat.AMDesignator = "a.m.";
			cultureInfo.DateTimeFormat.PMDesignator = "p.m.";
			item.SubItems[StartTimeHeader.Index].Text = log.InsertTime.ToString("t", cultureInfo);		
		}

		private void UpdateApplicationsList(ApplicationLog applicationLog, DataRowAction action)
		{
			if (applicationLog == null)
			{
				return;
			}

            string activeTime = null;
            string caption = null;

            if(action == DataRowAction.Add || action == DataRowAction.Change)
            {
                TimeSpan active = new TimeSpan(0, 0, applicationLog.ActiveTime);
                activeTime = ViewHelper.TimeSpanToTimeString(active);
                caption = applicationLog.Caption;
            }

			foreach (TreeListViewItem logItem in this.taskList.Items)
			{
				if (((Log) logItem.Tag).Id == applicationLog.TaskLogId)
				{
                    if (action == DataRowAction.Add)
                    {
                        TreeListViewItem lvi = new TreeListViewItem(caption,
                         new string[] { activeTime, "", applicationLog.Id.ToString(CultureInfo.InvariantCulture) });
                        lvi.Tag = applicationLog;
                        lvi.ImageIndex = IconsManager.GetIconFromFile(applicationLog.ApplicationFullPath);
                        logItem.Items.Add(lvi);
                    }
                    else
                    {
                        for (int i = 0; i < logItem.Items.Count; i++)
                        {
                            TreeListViewItem appItem = logItem.Items[i];
                            if (((ApplicationLog)appItem.Tag).Id == applicationLog.Id)
                            {
                                if (action == DataRowAction.Change)
                                {
                                    appItem.Tag = applicationLog;
                                    appItem.SubItems[TaskDescriptionHeader.Index].Text = caption;
                                    appItem.SubItems[DurationTaskHeader.Index].Text = activeTime;
                                    return;
                                }
                                if (action == DataRowAction.Delete)
                                {
                                    logItem.Items.RemoveAt(i);
                                    return;
                                }
                            }
                        }
                    }
				}
			}
		}

        delegate void ApplicationsLog_ApplicationsLogChangedDelegate(ApplicationsLog.ApplicationLogChangeEventArgs e);
		private void ApplicationsLog_ApplicationsLogChanged(ApplicationsLog.ApplicationLogChangeEventArgs e)
		{
            if (this.InvokeRequired)
            {
                ApplicationsLog_ApplicationsLogChangedDelegate d = new ApplicationsLog_ApplicationsLogChangedDelegate(ApplicationsLog_ApplicationsLogChanged);
                this.Invoke(d, new object[] { e});
            }
            else
            {
                this.UpdateApplicationsList(e.ApplicationLog, e.Action);
            }            
		}

		#endregion

		#region AsyncWork
        
        private void GetLogsAsync()
        {
            if (logDate.Value.Date != DateTime.Today)
            {
                this.addTaskButton.Enabled = false;
                this.propertiesButton.Enabled = false;
            }
            else
            {
                this.addTaskButton.Enabled = true;
                this.propertiesButton.Enabled = true;
            }
            this.currentDay = logDate.Value.Date;
            SetWaitState();
            worker.RunWorkerAsync();
        }

        private GetLogsResult GetLogs()
		{
            GetLogsResult result = new GetLogsResult();
            			result.LogList = Logs.GetLogsByDay(this.currentDay.Date);
            foreach (Log log in result.LogList)
			{
				ArrayList applicationLogs = ApplicationsLog.GetApplicationsLog(log.Id);
				log.ApplicationsLog = applicationLogs;
			}
            return result;
		}

		private void SetWaitState()
		{
			this.Status = "Retrieving data...";
			this.logDate.Enabled = false;
			taskList.Items.Clear();
			this.Refresh();
			this.Cursor = Cursors.WaitCursor;
			foreach (Control control in this.Controls)
			{
				control.Cursor = Cursors.WaitCursor;
			}
		}

		private void SetReadyState()
		{
			this.Status = "";
			this.Cursor = Cursors.Default;
			foreach (Control control in this.Controls)
			{
				control.Cursor = Cursors.Default;
			}
			this.logDate.Enabled = true;
		}

        public class GetLogsResult
        {
            public ArrayList LogList;
        }

		#endregion
	}
}
