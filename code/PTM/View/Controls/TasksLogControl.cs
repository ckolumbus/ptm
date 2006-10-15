using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using PTM.Business;
using PTM.Business.Helpers;
using PTM.Data;
using PTM.Infos;
using PTM.View.Controls.TreeListViewComponents;
using PTM.View.Forms;
using Timer=System.Timers.Timer;

namespace PTM.View.Controls
{
	/// <summary>
	/// Summary description for TasksLog.
	/// </summary>
	internal class TasksLogControl : UserControl
	{
		private Button editButton;
		private Button addTaskButton;
		private ColumnHeader TaskDescriptionHeader;
		private ColumnHeader DurationTaskHeader;
		private Timer notifyAnswerTimer;
		private ContextMenu notifyContextMenu;
		private MenuItem exitContextMenuItem;
		private Timer notifyTimer;
		private NotifyIcon notifyIcon;
		private IContainer components;
		private MenuItem menuItem1;
		private MenuItem menuItem5;
		private MenuItem menuItem6;
		private MenuItem menuItem9;
		private MenuItem menuItem2;
		private MenuItem menuItem3;
		private MenuItem menuItem4;
		private MenuItem menuItem10;
		private TreeListView taskList;
		private Button switchToButton;
		private Button deleteButton;
		private ColumnHeader StartTimeHeader;
		private MenuItem menuItem7;
		private DateTimePicker logDate;
		private Label label1;
		private System.Windows.Forms.ContextMenu rigthClickMenu;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuSwitchTo;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem mnuLunchTime;
		private System.Windows.Forms.MenuItem mnuOtherPersonal;
		private System.Windows.Forms.MenuItem mnuJobPhoneCall;
		private System.Windows.Forms.MenuItem mnuCheckJobMail;
		private System.Windows.Forms.MenuItem mnuJobMeeting;
		private System.Windows.Forms.MenuItem mnuDelete;
		private System.Windows.Forms.ComboBox newDefaultTaskComboBox;
		private System.Windows.Forms.ToolTip shortcutToolTip;
		private DateTime currentDay;

		internal TasksLogControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			exitContextMenuItem.Click += new EventHandler(exitContextMenuItem_Click);
			notifyIcon.MouseDown += new MouseEventHandler(notifyIcon_MouseDown);
			notifyTimer.Elapsed += new ElapsedEventHandler(notifyTimer_Elapsed);
			notifyAnswerTimer.Elapsed += new ElapsedEventHandler(notifyAnswerTimer_Elapsed);
			notifyIcon.Click += new EventHandler(notifyIcon_Click);
			addTaskButton.Click += new EventHandler(addTaskButton_Click);
			this.taskList.DoubleClick += new EventHandler(taskList_DoubleClick);

			Tasks.TasksRowChanged += new PTMDataset.TasksRowChangeEventHandler(TasksDataTable_TasksRowChanged);
			Tasks.TasksRowDeleting += new PTMDataset.TasksRowChangeEventHandler(TasksDataTable_TasksRowDeleting);
			Logs.LogChanged += new Logs.LogChangeEventHandler(TasksLog_LogChanged);
			ApplicationsLog.ApplicationsLogChanged +=
				new ApplicationsLog.ApplicationLogChangeEventHandler(ApplicationsLog_ApplicationsLogChanged);

			this.taskList.SmallImageList = IconsManager.IconsList;
			this.Load += new EventHandler(TasksLogControl_Load);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			Tasks.TasksRowChanged -= new PTMDataset.TasksRowChangeEventHandler(TasksDataTable_TasksRowChanged);
			Tasks.TasksRowDeleting -= new PTMDataset.TasksRowChangeEventHandler(TasksDataTable_TasksRowDeleting);
			Logs.LogChanged -= new Logs.LogChangeEventHandler(TasksLog_LogChanged);
			ApplicationsLog.ApplicationsLogChanged -=
				new ApplicationsLog.ApplicationLogChangeEventHandler(ApplicationsLog_ApplicationsLogChanged);
			base.OnHandleDestroyed(e);
		}


		private void SetLogDay(DateTime date)
		{
			taskList.Items.Clear();
			ArrayList list = Logs.GetLogsByDay(date);
			foreach (Log log in list)
			{
				PTMDataset.TasksRow taskRow = Tasks.FindById(log.TaskId);
				TreeListViewItem itemA = new TreeListViewItem("", new string[] {"", ""});
				SetListItemValues(itemA, log, taskRow);
				taskList.Items.Insert(0, itemA);
				ArrayList applicationLogs = ApplicationsLog.GetApplicationsLog(log.Id);
				foreach (ApplicationLog applicationLog in applicationLogs)
				{
					UpdateApplicationsList(applicationLog);
				}
			}
		}

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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TasksLogControl));
			this.editButton = new System.Windows.Forms.Button();
			this.addTaskButton = new System.Windows.Forms.Button();
			this.notifyContextMenu = new System.Windows.Forms.ContextMenu();
			this.exitContextMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.TaskDescriptionHeader = new System.Windows.Forms.ColumnHeader();
			this.StartTimeHeader = new System.Windows.Forms.ColumnHeader();
			this.DurationTaskHeader = new System.Windows.Forms.ColumnHeader();
			this.notifyAnswerTimer = new System.Timers.Timer();
			this.notifyTimer = new System.Timers.Timer();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.taskList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
			this.rigthClickMenu = new System.Windows.Forms.ContextMenu();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuSwitchTo = new System.Windows.Forms.MenuItem();
			this.mnuDelete = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.mnuLunchTime = new System.Windows.Forms.MenuItem();
			this.mnuOtherPersonal = new System.Windows.Forms.MenuItem();
			this.mnuJobPhoneCall = new System.Windows.Forms.MenuItem();
			this.mnuCheckJobMail = new System.Windows.Forms.MenuItem();
			this.mnuJobMeeting = new System.Windows.Forms.MenuItem();
			this.switchToButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.logDate = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.newDefaultTaskComboBox = new System.Windows.Forms.ComboBox();
			this.shortcutToolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.notifyAnswerTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.notifyTimer)).BeginInit();
			this.SuspendLayout();
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.editButton.Location = new System.Drawing.Point(136, 280);
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
			this.addTaskButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.addTaskButton.Location = new System.Drawing.Point(296, 280);
			this.addTaskButton.Name = "addTaskButton";
			this.addTaskButton.Size = new System.Drawing.Size(72, 23);
			this.addTaskButton.TabIndex = 5;
			this.addTaskButton.Text = "New Log...";
			this.shortcutToolTip.SetToolTip(this.addTaskButton, "Ins");
			// 
			// notifyContextMenu
			// 
			this.notifyContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							  this.exitContextMenuItem,
																							  this.menuItem1,
																							  this.menuItem2,
																							  this.menuItem3,
																							  this.menuItem4,
																							  this.menuItem5,
																							  this.menuItem6,
																							  this.menuItem7,
																							  this.menuItem9,
																							  this.menuItem10});
			// 
			// exitContextMenuItem
			// 
			this.exitContextMenuItem.Index = 0;
			this.exitContextMenuItem.Text = "Exit";
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.Text = "-";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "Idle";
			this.menuItem2.Click += new System.EventHandler(this.menuIdle_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "Lunch Time";
			this.menuItem3.Click += new System.EventHandler(this.menuLunchTime_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "Other/Personal";
			this.menuItem4.Click += new System.EventHandler(this.menuOtherPersonal_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 5;
			this.menuItem5.Text = "Job Phone Call";
			this.menuItem5.Click += new System.EventHandler(this.menuJobPhoneCall_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 6;
			this.menuItem6.Text = "Checking Job Email";
			this.menuItem6.Click += new System.EventHandler(this.menuCheckingJobMail_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 7;
			this.menuItem7.Text = "Job Meeting";
			this.menuItem7.Click += new System.EventHandler(this.menuJobMeeting_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 8;
			this.menuItem9.Text = "-";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 9;
			this.menuItem10.Text = "About...";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
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
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Current task";
			this.notifyIcon.Visible = true;
			// 
			// taskList
			// 
			this.taskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.taskList.AutoArrange = false;
			this.taskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.TaskDescriptionHeader,
																					   this.DurationTaskHeader,
																					   this.StartTimeHeader});
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
			// rigthClickMenu
			// 
			this.rigthClickMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.mnuEdit,
																						   this.mnuSwitchTo,
																						   this.mnuDelete,
																						   this.menuItem11,
																						   this.mnuLunchTime,
																						   this.mnuOtherPersonal,
																						   this.mnuJobPhoneCall,
																						   this.mnuCheckJobMail,
																						   this.mnuJobMeeting});
			// 
			// mnuEdit
			// 
			this.mnuEdit.DefaultItem = true;
			this.mnuEdit.Index = 0;
			this.mnuEdit.Text = "Edit...";
			this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
			// 
			// mnuSwitchTo
			// 
			this.mnuSwitchTo.Index = 1;
			this.mnuSwitchTo.Shortcut = System.Windows.Forms.Shortcut.ShiftIns;
			this.mnuSwitchTo.Text = "Switch To";
			this.mnuSwitchTo.Click += new System.EventHandler(this.mnuSwitchTo_Click);
			// 
			// mnuDelete
			// 
			this.mnuDelete.Index = 2;
			this.mnuDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
			this.mnuDelete.Text = "Delete";
			this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 3;
			this.menuItem11.Text = "-";
			// 
			// mnuLunchTime
			// 
			this.mnuLunchTime.Index = 4;
			this.mnuLunchTime.Text = "Set to Lunch Time";
			this.mnuLunchTime.Click += new System.EventHandler(this.mnuLunchTime_Click);
			// 
			// mnuOtherPersonal
			// 
			this.mnuOtherPersonal.Index = 5;
			this.mnuOtherPersonal.Text = "Set to Other/Personal";
			this.mnuOtherPersonal.Click += new System.EventHandler(this.mnuOtherPersonal_Click);
			// 
			// mnuJobPhoneCall
			// 
			this.mnuJobPhoneCall.Index = 6;
			this.mnuJobPhoneCall.Text = "Set to Job Phone Call";
			this.mnuJobPhoneCall.Click += new System.EventHandler(this.mnuJobPhoneCall_Click);
			// 
			// mnuCheckJobMail
			// 
			this.mnuCheckJobMail.Index = 7;
			this.mnuCheckJobMail.Text = "Set to Checking Job Mail";
			this.mnuCheckJobMail.Click += new System.EventHandler(this.mnuCheckJobMail_Click);
			// 
			// mnuJobMeeting
			// 
			this.mnuJobMeeting.Index = 8;
			this.mnuJobMeeting.Text = "Set to Job Meeting";
			this.mnuJobMeeting.Click += new System.EventHandler(this.mnuJobMeeting_Click);
			// 
			// switchToButton
			// 
			this.switchToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.switchToButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.switchToButton.Location = new System.Drawing.Point(216, 280);
			this.switchToButton.Name = "switchToButton";
			this.switchToButton.Size = new System.Drawing.Size(72, 23);
			this.switchToButton.TabIndex = 4;
			this.switchToButton.Text = "Switch To";
			this.shortcutToolTip.SetToolTip(this.switchToButton, "Shift+Ins");
			this.switchToButton.Click += new System.EventHandler(this.switchToButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.deleteButton.Location = new System.Drawing.Point(56, 280);
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
			this.logDate.Size = new System.Drawing.Size(256, 20);
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
			// newDefaultTaskComboBox
			// 
			this.newDefaultTaskComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.newDefaultTaskComboBox.BackColor = System.Drawing.SystemColors.Window;
			this.newDefaultTaskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.newDefaultTaskComboBox.DropDownWidth = 104;
			this.newDefaultTaskComboBox.Items.AddRange(new object[] {
																		"Lunch Time",
																		"Other/Personal",
																		"Job Phone Call",
																		"Checking Job Email",
																		"Job Meeting"});
			this.newDefaultTaskComboBox.Location = new System.Drawing.Point(297, 281);
			this.newDefaultTaskComboBox.Name = "newDefaultTaskComboBox";
			this.newDefaultTaskComboBox.Size = new System.Drawing.Size(88, 21);
			this.newDefaultTaskComboBox.TabIndex = 7;
			this.newDefaultTaskComboBox.SelectedIndexChanged += new System.EventHandler(this.newDefaultTaskComboBox_SelectedIndexChanged);
			// 
			// TasksLogControl
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.logDate);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.switchToButton);
			this.Controls.Add(this.taskList);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.addTaskButton);
			this.Controls.Add(this.newDefaultTaskComboBox);
			this.Name = "TasksLogControl";
			this.Size = new System.Drawing.Size(392, 312);
			((System.ComponentModel.ISupportInitialize)(this.notifyAnswerTimer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.notifyTimer)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region TaskLog

		private void TasksLogControl_Load(object sender, EventArgs e)
		{
			currentDay = DateTime.Today;
			SetLogDay(currentDay);
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
				if (Tasks.CurrentTaskRow == null)
					AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTaskEnum.Idle);
				else
					AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTaskEnum.Idle);
			}
		}

		private void AddDefaultTaskLog(int taskParentId, DefaultTaskEnum defaultTaskEnum)
		{
			Logs.AddDefaultTaskLog(taskParentId, defaultTaskEnum);
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
			SetLogDay(logDate.Value.Date);
		}

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			EditSelectedTaskLog();
		}

		private void mnuSwitchTo_Click(object sender, System.EventArgs e)
		{
			SwitchToSelectedLog();
		}

		private void mnuDelete_Click(object sender, System.EventArgs e)
		{
			DeleteSelectedTaskLog();
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
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTaskEnum.Idle);
			}
			else if (notifyForm.Result == NotifyForm.NotifyResult.No)
			{
				NewTaskLog(true);
			}
			else if (notifyForm.Result == NotifyForm.NotifyResult.Yes)
			{
				AddTaskLog(Tasks.CurrentTaskRow.Id,
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

		private void exitContextMenuItem_Click(object sender, EventArgs e)
		{
			this.notifyTimer.Stop();
			this.notifyAnswerTimer.Stop();
			this.notifyIcon.Visible = false;
			this.Exit(this, e);
		}

		private void menuIdle_Click(object sender, EventArgs e)
		{
			if (Tasks.CurrentTaskRow == null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTaskEnum.Idle);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTaskEnum.Idle);
		}

		private void menuLunchTime_Click(object sender, EventArgs e)
		{
			if (Tasks.CurrentTaskRow == null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTaskEnum.LunchTime);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTaskEnum.LunchTime);
		}

		private void menuOtherPersonal_Click(object sender, EventArgs e)
		{
			if (Tasks.CurrentTaskRow == null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTaskEnum.OtherPersonal);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTaskEnum.OtherPersonal);
		}

		private void menuJobPhoneCall_Click(object sender, EventArgs e)
		{
			if (Tasks.CurrentTaskRow == null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTaskEnum.JobPhoneCall);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTaskEnum.JobPhoneCall);
		}

		private void menuCheckingJobMail_Click(object sender, EventArgs e)
		{
			if (Tasks.CurrentTaskRow == null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTaskEnum.CheckingJobMail);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTaskEnum.CheckingJobMail);
		}

		private void menuJobMeeting_Click(object sender, EventArgs e)
		{
			if (Tasks.CurrentTaskRow == null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTaskEnum.JobMeeting);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTaskEnum.JobMeeting);
		}

		private void menuItem10_Click(object sender, EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this.Parent);
		}

		#endregion

		#region Business events

		private void TasksDataTable_TasksRowChanged(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Change)
			{
				foreach (ListViewItem item in this.taskList.Items)
				{
					if (((Log) item.Tag).TaskId == e.Row.Id)
					{
						item.SubItems[TaskDescriptionHeader.Index].Text = e.Row.Description;
					}
				}
			}
		}

		private void TasksDataTable_TasksRowDeleting(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Delete)
			{
				foreach (ListViewItem item in this.taskList.Items)
				{
					if (((Log) item.Tag).TaskId == e.Row.Id)
					{
						item.Remove();
					}
				}
			}
		}

		private void TasksLog_LogChanged(Logs.LogChangeEventArgs e)
		{
			PTMDataset.TasksRow taskRow = Tasks.FindById(e.Log.TaskId);
			if (e.Action == DataRowAction.Change)
			{
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
					TreeListViewItem itemA = new TreeListViewItem("", new string[] {"", ""});
					SetListItemValues(itemA, e.Log, taskRow);
					taskList.Items.Insert(0, itemA);
				}
			}

			if (Logs.CurrentLog != null && Logs.CurrentLog.Id == e.Log.Id)
			{
				this.notifyIcon.Text = taskRow.Description;
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

		private void SetListItemValues(ListViewItem item, Log log, PTMDataset.TasksRow taskRow)
		{
			item.Tag = log;
			if (item.SubItems[TaskDescriptionHeader.Index].Text != taskRow.Description)
			{
				item.Text = taskRow.Description;
			}
			item.SubItems[DurationTaskHeader.Index].Text = ViewHelper.Int32ToTimeString(log.Duration);
			//item.SubItems[StartTimeHeader.Index].Text = log.InsertTime.ToShortTimeString();
			CultureInfo cultureInfo;
			cultureInfo = (CultureInfo) CultureInfo.CurrentCulture.Clone();
			cultureInfo.DateTimeFormat.ShortTimePattern = "hh:mm tt";
			cultureInfo.DateTimeFormat.AMDesignator = "a.m.";
			cultureInfo.DateTimeFormat.PMDesignator = "p.m.";
			item.SubItems[StartTimeHeader.Index].Text = log.InsertTime.ToString("t", cultureInfo);
			
			if (taskRow.IsDefaultTask)
			{
				item.ImageIndex = IconsManager.GetIndex(taskRow.DefaultTaskId.ToString(CultureInfo.InvariantCulture));
				if (Logs.CurrentLog != null && log.Id == Logs.CurrentLog.Id)
					notifyIcon.Icon = IconsManager.GetIcon(taskRow.DefaultTaskId.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				item.ImageIndex = IconsManager.GetIndex("0");
				if (Logs.CurrentLog != null && log.Id == Logs.CurrentLog.Id)
					notifyIcon.Icon = IconsManager.GetIcon("0");
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
					lvi.ImageIndex = IconsManager.AddIconFromFile(applicationLog.ApplicationFullPath);
					logItem.Items.Add(lvi);
				}
			}
		}

		private void ApplicationsLog_ApplicationsLogChanged(ApplicationsLog.ApplicationLogChangeEventArgs e)
		{
			this.UpdateApplicationsList(e.ApplicationLog);
		}

		#endregion


		private void taskList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
			{
				EditSelectedTaskLog();
			}
			if(e.KeyData == Keys.Insert)
			{
				NewTaskLog(false);
			}
		}

		private void newDefaultTaskComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.newDefaultTaskComboBox.SelectedIndex==-1)
				return;
			if(this.newDefaultTaskComboBox.SelectedIndex==0)
				this.menuLunchTime_Click(sender, e);
			if(this.newDefaultTaskComboBox.SelectedIndex==1)
				this.menuOtherPersonal_Click(sender, e);
			if(this.newDefaultTaskComboBox.SelectedIndex==2)
				this.menuJobPhoneCall_Click(sender, e);
			if(this.newDefaultTaskComboBox.SelectedIndex==3)
				this.menuCheckingJobMail_Click(sender, e);
			if(this.newDefaultTaskComboBox.SelectedIndex==4)
				this.menuJobMeeting_Click(sender, e);
		}

		private void mnuLunchTime_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem item in this.taskList.SelectedItems)
			{
				Log log = (Log) item.Tag;
				Logs.UpdateLogDefaultTask(log.Id, DefaultTaskEnum.LunchTime);
			}
		}

		private void mnuOtherPersonal_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem item in this.taskList.SelectedItems)
			{
				Log log = (Log) item.Tag;
				Logs.UpdateLogDefaultTask(log.Id, DefaultTaskEnum.OtherPersonal);
			}
		}

		private void mnuJobPhoneCall_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem item in this.taskList.SelectedItems)
			{
				Log log = (Log) item.Tag;
				Logs.UpdateLogDefaultTask(log.Id, DefaultTaskEnum.JobPhoneCall);
			}
		}

		private void mnuCheckJobMail_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem item in this.taskList.SelectedItems)
			{
				Log log = (Log) item.Tag;
				Logs.UpdateLogDefaultTask(log.Id, DefaultTaskEnum.CheckingJobMail);
			}
		}

		private void mnuJobMeeting_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem item in this.taskList.SelectedItems)
			{
				Log log = (Log) item.Tag;
				Logs.UpdateLogDefaultTask(log.Id, DefaultTaskEnum.JobMeeting);
			}
		}




	}
}