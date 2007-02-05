using System;
using System.Collections;
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
using NotifyIcon=HansBlomme.Windows.Forms.NotifyIcon;
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
		private Timer notifyAnswerTimer;
		private ContextMenu notifyContextMenu;
		private Timer notifyTimer;
		private NotifyIcon notifyIcon;
		private IContainer components;
		private TreeListView taskList;
		private Button switchToButton;
		private Button deleteButton;
		private ColumnHeader StartTimeHeader;
		private DateTimePicker logDate;
		private Label label1;
		private ContextMenu rigthClickMenu;
		private ToolTip shortcutToolTip;
		private CheckBox pathCheckBox;
		private DateTime currentDay;
		private AsyncWorker worker;

		internal TasksLogControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			worker = new AsyncWorker();
			worker.OnBeforeDoWork += new AsyncWorker.OnBeforeDoWorkDelegate(worker_OnBeforeDoWork);
			worker.OnWorkDone += new AsyncWorker.OnWorkDoneDelegate(worker_OnWorkDone);

			notifyIcon.MouseDown += new NotifyIcon.MouseDownEventHandler(notifyIcon_MouseDown);
			notifyTimer.Elapsed += new ElapsedEventHandler(notifyTimer_Elapsed);
			notifyAnswerTimer.Elapsed += new ElapsedEventHandler(notifyAnswerTimer_Elapsed);
			notifyIcon.Click += new NotifyIcon.ClickEventHandler(notifyIcon_Click);
			addTaskButton.Click += new EventHandler(addTaskButton_Click);
			this.taskList.DoubleClick += new EventHandler(taskList_DoubleClick);

			Tasks.TaskChanged += new Tasks.TaskChangeEventHandler(TasksDataTable_TasksRowChanged);
			Tasks.TaskDeleting += new Tasks.TaskChangeEventHandler(TasksDataTable_TasksRowDeleting);
			Tasks.TaskDeleted += new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleted);
			Logs.LogChanged += new Logs.LogChangeEventHandler(TasksLog_LogChanged);
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

		protected override void OnHandleDestroyed(EventArgs e)
		{
			Tasks.TaskChanged -= new Tasks.TaskChangeEventHandler(TasksDataTable_TasksRowChanged);
			Tasks.TaskDeleting -= new Tasks.TaskChangeEventHandler(TasksDataTable_TasksRowDeleting);
			Tasks.TaskDeleted -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleted);
			Logs.LogChanged -= new Logs.LogChangeEventHandler(TasksLog_LogChanged);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (TasksLogControl));
			this.editButton = new System.Windows.Forms.Button();
			this.addTaskButton = new System.Windows.Forms.Button();
			this.notifyContextMenu = new System.Windows.Forms.ContextMenu();
			this.TaskDescriptionHeader = new System.Windows.Forms.ColumnHeader();
			this.StartTimeHeader = new System.Windows.Forms.ColumnHeader();
			this.DurationTaskHeader = new System.Windows.Forms.ColumnHeader();
			this.notifyAnswerTimer = new System.Timers.Timer();
			this.notifyTimer = new System.Timers.Timer();
			this.notifyIcon = new NotifyIcon(this.components);
			this.taskList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
			this.rigthClickMenu = new System.Windows.Forms.ContextMenu();
			this.switchToButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.logDate = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.shortcutToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.pathCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize) (this.notifyAnswerTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.notifyTimer)).BeginInit();
			this.SuspendLayout();
			// 
			// editButton
			// 
			this.editButton.Anchor =
				((System.Windows.Forms.AnchorStyles)
				 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.addTaskButton.Anchor =
				((System.Windows.Forms.AnchorStyles)
				 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.addTaskButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.addTaskButton.Location = new System.Drawing.Point(312, 280);
			this.addTaskButton.Name = "addTaskButton";
			this.addTaskButton.Size = new System.Drawing.Size(72, 23);
			this.addTaskButton.TabIndex = 5;
			this.addTaskButton.Text = "New Log...";
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
			// notifyAnswerTimer
			// 
			this.notifyAnswerTimer.SynchronizingObject = this;
			// 
			// notifyTimer
			// 
			this.notifyTimer.Interval = 1000;
			this.notifyTimer.SynchronizingObject = this;
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenu = this.notifyContextMenu;
			this.notifyIcon.Icon = ((System.Drawing.Icon) (resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Current task";
			this.notifyIcon.Visible = true;
			// 
			// taskList
			// 
			this.taskList.Anchor =
				((System.Windows.Forms.AnchorStyles)
				 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				   | System.Windows.Forms.AnchorStyles.Right)));
			this.taskList.AutoArrange = false;
			this.taskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
			                               	{
			                               		this.TaskDescriptionHeader,
			                               		this.DurationTaskHeader,
			                               		this.StartTimeHeader
			                               	});
			this.taskList.ContextMenu = this.rigthClickMenu;
			this.taskList.HideSelection = false;
			this.taskList.Location = new System.Drawing.Point(8, 32);
			this.taskList.Name = "taskList";
			this.taskList.Size = new System.Drawing.Size(376, 240);
			this.taskList.Sorting = System.Windows.Forms.SortOrder.None;
			this.taskList.TabIndex = 1;
			this.taskList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.taskList_KeyDown);
			this.taskList.SelectedIndexChanged += new System.EventHandler(this.taskList_SelectedIndexChanged);
			// 
			// switchToButton
			// 
			this.switchToButton.Anchor =
				((System.Windows.Forms.AnchorStyles)
				 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.switchToButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.switchToButton.Location = new System.Drawing.Point(232, 280);
			this.switchToButton.Name = "switchToButton";
			this.switchToButton.Size = new System.Drawing.Size(72, 23);
			this.switchToButton.TabIndex = 4;
			this.switchToButton.Text = "Switch To";
			this.shortcutToolTip.SetToolTip(this.switchToButton, "Shift+Ins");
			this.switchToButton.Click += new System.EventHandler(this.switchToButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor =
				((System.Windows.Forms.AnchorStyles)
				 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.Controls.Add(this.switchToButton);
			this.Controls.Add(this.taskList);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.addTaskButton);
			this.Name = "TasksLogControl";
			this.Size = new System.Drawing.Size(392, 312);
			((System.ComponentModel.ISupportInitialize) (this.notifyAnswerTimer)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.notifyTimer)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		#region TaskLog

		private void TasksLogControl_Load(object sender, EventArgs e)
		{
			this.currentDay = DateTime.Today;
			ArrayList logs = (ArrayList) this.GetLogs(this.currentDay);
			SetLogDay(logs);
			AddIdleTaskLog();
		}

		internal void NewTaskLog(bool mustAddATask)
		{
			notifyTimer.Stop();
			TaskLogForm tasklog = new TaskLogForm();
			if (tasklog.ShowDialog(this) == DialogResult.OK)
			{
				AddTaskLog(tasklog.SelectedTaskRow.Id,
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
			notifyTimer.Start();
		}

		private void EditSelectedTaskLog()
		{
			if (!isValidEditableLog())
				return;
			int taskId = ((Log) taskList.SelectedItems[0].Tag).TaskId;

			TaskLogForm taskLogForm = new TaskLogForm(taskId);
			if (taskLogForm.ShowDialog(this.Parent) == DialogResult.OK)
			{
				for (int i = 0; i < taskList.SelectedItems.Count; i++)
				{
					int taskLogId = ((Log) taskList.SelectedItems[i].Tag).Id;
					Logs.UpdateLogTaskId(taskLogId, taskLogForm.SelectedTaskRow.Id);
				}
			}
		}

		private void SwitchToSelectedLog()
		{
			if (!isValidEditableLog())
				return;
			int taskId = ((Log) taskList.SelectedItems[0].Tag).TaskId;
			AddTaskLog(taskId,
			           (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value);
		}

		private void DeleteSelectedTaskLog()
		{
			if (!isValidEditableLog())
				return;

			for (int i = 0; i < taskList.SelectedItems.Count; i++)
			{
				int taskLogId = ((Log) taskList.SelectedItems[i].Tag).Id;
				Logs.DeleteLog(taskLogId);
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
		}

		private bool isValidEditableLog()
		{
			if (this.taskList.SelectedItems.Count == 0)
				return false;
			if (taskList.SelectedItems[0].Parent != null)
				return false;

			return true;
		}

		private void switchToButton_Click(object sender, EventArgs e)
		{
			SwitchToSelectedLog();
		}

		private void taskList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.taskList.SelectedItems.Count <= 0)
			{
				SetNoEditable();
			}
			else if (this.taskList.SelectedItems.Count == 1)
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
//				int taskId = ((Log) taskList.SelectedItems[0].Tag).TaskId;
				for (int i = 1; i < this.taskList.SelectedItems.Count; i++)
				{
					if (taskList.SelectedItems[i].Parent != null)
					{
						SetNoEditable();
						return;
					}
//					if (((Log) taskList.SelectedItems[i].Tag).TaskId != taskId)
//					{
//						this.editButton.Enabled = false;
//						return;
//					}
				}
				SetEditable();
			}
		}

		private void SetEditable()
		{
			this.editButton.Enabled = true;
			this.deleteButton.Enabled = true;
			this.switchToButton.Enabled = true;
			this.taskList.ContextMenu = this.rigthClickMenu;
		}

		private void SetNoEditable()
		{
			this.editButton.Enabled = false;
			this.deleteButton.Enabled = false;
			this.switchToButton.Enabled = false;
			this.taskList.ContextMenu = null;
		}

		private void logDate_ValueChanged(object sender, EventArgs e)
		{
			if (logDate.Value.Date != DateTime.Today)
			{
				this.addTaskButton.Enabled = false;
				this.switchToButton.Enabled = false;
			}
			else
			{
				this.addTaskButton.Enabled = true;
				this.switchToButton.Enabled = true;
			}
			this.currentDay = logDate.Value.Date;
			worker.DoWork((int) TasksLogCotrolWorks.GetLogs, new AsyncWorker.AsyncWorkerDelegate(GetLogs), new object[] {null});
		}

		private void mnuEdit_Click(object sender, EventArgs e)
		{
			EditSelectedTaskLog();
		}

		private void mnuSwitchTo_Click(object sender, EventArgs e)
		{
			SwitchToSelectedLog();
		}

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			DeleteSelectedTaskLog();
		}

		private void CreateRigthClickMenu()
		{
			MenuItem mnuEdit = new MenuItem();
			MenuItem mnuSwitchTo = new MenuItem();
			MenuItem mnuDelete = new MenuItem();
			MenuItem menuItem11 = new MenuItem();
			this.rigthClickMenu.MenuItems.Clear();
			this.rigthClickMenu.MenuItems.AddRange(new MenuItem[]
			                                       	{
			                                       		mnuEdit,
			                                       		mnuSwitchTo,
			                                       		mnuDelete,
			                                       		menuItem11
			                                       	});

			mnuEdit.DefaultItem = true;
			mnuEdit.Index = 0;
			mnuEdit.Text = "Edit...";
			mnuEdit.Click += new EventHandler(this.mnuEdit_Click);

			mnuSwitchTo.Index = 1;
			mnuSwitchTo.Shortcut = Shortcut.ShiftIns;
			mnuSwitchTo.Text = "Switch To";
			mnuSwitchTo.Click += new EventHandler(this.mnuSwitchTo_Click);

			mnuDelete.Index = 2;
			mnuDelete.Shortcut = Shortcut.Del;
			mnuDelete.Text = "Delete";
			mnuDelete.Click += new EventHandler(this.mnuDelete_Click);

			menuItem11.Index = 3;
			menuItem11.Text = "-";

			ArrayList a = new ArrayList();
			Task[] tasks;
			tasks = Tasks.GetChildTasks(Tasks.RootTasksRow.Id);
			foreach (Task task in tasks)
			{
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
				Log log = (Log) item.Tag;
				Logs.UpdateLogTaskId(log.Id, mnu.TaskId);
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
					if (((Log) item.Tag).TaskId == Tasks.IdleTasksRow.Id)
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
			catch
			{
				throw;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void SetLogDay(ArrayList logs)
		{
			try
			{
				taskList.BeginUpdate();
				foreach (Log log in logs)
				{
					Task taskRow = Tasks.FindById(log.TaskId);
					TreeListViewItem itemA = new TreeListViewItem("", new string[] {"", ""});
					SetListItemValues(itemA, log, taskRow);
					taskList.Items.Insert(0, itemA);
					foreach (ApplicationLog applicationLog in log.ApplicationsLog)
					{
						UpdateApplicationsList(applicationLog);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex.Message);
				Logger.Write(ex.StackTrace);
				throw;
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
			notifyForm = new NotifyForm(this.notifyIcon.Text);
			notifyForm.Show();
			notifyAnswerTimer.Start();
		}

		private void notifyAnswerTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
//			Thread th = new Thread(new ThreadStart(GetAnswer));
//			th.Priority = ThreadPriority.BelowNormal;
//			th.Start();
			GetAnswer();
		}

		private void notifyIcon_Click(object sender, EventArgs e)
		{
			this.ParentForm.Activate();
		}

		private void GetAnswer()
		{
			notifyAnswerTimer.Stop();
			if (notifyForm.Result == NotifyForm.NotifyResult.Waiting)
			{
				notifyAnswerTimer.Start();
				return;
			}
			else if (notifyForm.Result == NotifyForm.NotifyResult.Cancel)
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
				           (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value);
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		#region NotifyContextMenu

		internal event EventHandler Exit;

		private void CreateNotifyMenu()
		{
			MenuItem exitContextMenuItem = new MenuItem();
			MenuItem menuItem1 = new MenuItem();
			this.notifyContextMenu = new ContextMenu();
			this.notifyContextMenu.MenuItems.AddRange(new MenuItem[]
			                                          	{
			                                          		exitContextMenuItem,
			                                          		menuItem1
			                                          	});

			exitContextMenuItem.Index = 0;
			exitContextMenuItem.Text = "Exit";
			exitContextMenuItem.Click += new EventHandler(exitContextMenuItem_Click);

			menuItem1.Index = 1;
			menuItem1.Text = "-";


			ArrayList a = new ArrayList();
			Task[] tasks;
			tasks = Tasks.GetChildTasks(Tasks.RootTasksRow.Id);
			foreach (Task task in tasks)
			{
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
			this.notifyAnswerTimer.Stop();
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
						item.SubItems[TaskDescriptionHeader.Index].Text = e.Task.Description;
						item.ImageIndex = e.Task.IconId;
					}
				}
			}
			CreateNotifyMenu();
			CreateRigthClickMenu();
		}

		private void TasksDataTable_TasksRowDeleting(Tasks.TaskChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Delete)
			{
				foreach (ListViewItem item in this.taskList.Items)
				{
					if (((Log) item.Tag).TaskId == e.Task.Id)
					{
						item.Remove();
					}
				}
			}
		}

		private void Tasks_TasksRowDeleted(Tasks.TaskChangeEventArgs e)
		{
			CreateNotifyMenu();
			CreateRigthClickMenu();
		}

		private void TasksLog_LogChanged(Logs.LogChangeEventArgs e)
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
					taskRow = Tasks.FindById(e.Log.TaskId);
					TreeListViewItem itemA = new TreeListViewItem("", new string[] {"", ""});
					SetListItemValues(itemA, e.Log, taskRow);
					taskList.Items.Insert(0, itemA);
				}
			}
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
//			if (item.SubItems[TaskDescriptionHeader.Index].Text != taskRow.Description)
//			{
//				item.Text = taskRow.Description;
//			}
			if (this.pathCheckBox.Checked && taskRow.Id != Tasks.IdleTasksRow.Id)
				item.Text = Tasks.GetFullPath(taskRow.Id);
			else
				item.Text = taskRow.Description;

			item.SubItems[DurationTaskHeader.Index].Text = ViewHelper.Int32ToTimeString(log.Duration);
			//item.SubItems[StartTimeHeader.Index].Text = log.InsertTime.ToShortTimeString();
			CultureInfo cultureInfo;
			cultureInfo = (CultureInfo) CultureInfo.CurrentCulture.Clone();
			cultureInfo.DateTimeFormat.ShortTimePattern = "hh:mm tt";
			cultureInfo.DateTimeFormat.AMDesignator = "a.m.";
			cultureInfo.DateTimeFormat.PMDesignator = "p.m.";
			item.SubItems[StartTimeHeader.Index].Text = log.InsertTime.ToString("t", cultureInfo);

			item.ImageIndex = taskRow.IconId;
			if (Logs.CurrentLog != null && log.Id == Logs.CurrentLog.Id)
			{
				notifyIcon.Text = taskRow.Description;
				notifyIcon.Icon = (Icon) IconsManager.CommonTaskIconsTable[taskRow.IconId];
			}
		}

		private void UpdateApplicationsList(ApplicationLog applicationLog)
		{
			if (applicationLog == null)
			{
				return;
			}


			TimeSpan active = new TimeSpan(0, 0, applicationLog.ActiveTime);
			string activeTime = ViewHelper.TimeSpanToTimeString(active);
			string caption = applicationLog.Caption.Length != 0 ? applicationLog.Caption : applicationLog.Name;
			foreach (TreeListViewItem logItem in this.taskList.Items)
			{
				if (((Log) logItem.Tag).Id == applicationLog.TaskLogId)
				{
					foreach (TreeListViewItem appItem in logItem.Items)
					{
						if (((ApplicationLog) appItem.Tag).Id == applicationLog.Id)
						{
							appItem.Tag = applicationLog;
							appItem.SubItems[TaskDescriptionHeader.Index].Text = caption;
							appItem.SubItems[DurationTaskHeader.Index].Text = activeTime;
							return;
						}
					}
					TreeListViewItem lvi =
						new TreeListViewItem(caption,
						                     new string[] {activeTime, "", applicationLog.Id.ToString(CultureInfo.InvariantCulture)});
					lvi.Tag = applicationLog;
					lvi.ImageIndex = IconsManager.GetIconFromFile(applicationLog.ApplicationFullPath);
					logItem.Items.Add(lvi);
				}
			}
		}

		private void ApplicationsLog_ApplicationsLogChanged(ApplicationsLog.ApplicationLogChangeEventArgs e)
		{
			this.UpdateApplicationsList(e.ApplicationLog);
		}

		#endregion

		#region AsyncWork

		private enum TasksLogCotrolWorks : int
		{
			GetLogs
		}

		private object GetLogs(object p)
		{
			ArrayList list = Logs.GetLogsByDay(this.currentDay.Date);
			foreach (Log log in list)
			{
				ArrayList applicationLogs = ApplicationsLog.GetApplicationsLog(log.Id);
				log.ApplicationsLog = applicationLogs;
			}
			return list;
		}

		private void worker_OnBeforeDoWork(AsyncWorker.OnBeforeDoWorkEventArgs e)
		{
			switch (e.WorkId)
			{
				case (int) TasksLogCotrolWorks.GetLogs:
					SetWaitState();
					break;
			}
		}

		private void worker_OnWorkDone(AsyncWorker.OnWorkDoneEventArgs e)
		{
			switch (e.WorkId)
			{
				case (int) TasksLogCotrolWorks.GetLogs:
					SetLogDayDelegate del = new SetLogDayDelegate(SetLogDay);

					this.Invoke(del, new object[] {e.Result});
					//this.SetLogDay((ArrayList) e.Result);
					break;
			}
		}

		private delegate void SetLogDayDelegate(ArrayList logs);

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

		#endregion
	}
}