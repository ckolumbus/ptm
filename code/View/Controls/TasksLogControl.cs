using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;
using PTM.Infos;
using PTM.View.Controls.TreeListViewComponents;
using PTM.View.Forms;

namespace PTM.View.Controls
{
	/// <summary>
	/// Summary description for TasksLog.
	/// </summary>
	public class TasksLogControl : UserControl
	{
		private Button editButton;
		private Button addTaskButton;
		private ColumnHeader TaskDescriptionHeader;
		private ColumnHeader DurationTaskHeader;
		private System.Timers.Timer notifyAnswerTimer;
		private ContextMenu notifyContextMenu;
		private MenuItem exitContextMenuItem;
		private System.Timers.Timer notifyTimer;
		private NotifyIcon notifyIcon;
		private IContainer components;
		private MenuItem menuItem1;
		private MenuItem menuItem5;
		private MenuItem menuItem6;
		private MenuItem menuItem7;
		private MenuItem menuItem8;
		private MenuItem menuItem9;
		private MenuItem menuItem2;
		private MenuItem menuItem3;
		private MenuItem menuItem4;
		private MenuItem menuItem10;
		private TreeListView taskList;
		private ColumnHeader Description;
		private ColumnHeader Time;
		private ColumnHeader Duration;
		private ColumnHeader Id;
		private System.Windows.Forms.Button switchToButton;
		private System.Windows.Forms.Button deleteButton;
		private ColumnHeader Id2;
		private System.Windows.Forms.ColumnHeader StartTimeHeader;
		//private ArrayList logList;

		public TasksLogControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
	
			exitContextMenuItem.Click+=new EventHandler(exitContextMenuItem_Click);
			notifyIcon.MouseDown+=new MouseEventHandler(notifyIcon_MouseDown);
			notifyTimer.Elapsed+=new ElapsedEventHandler(notifyTimer_Elapsed);
			notifyAnswerTimer.Elapsed+=new ElapsedEventHandler(notifyAnswerTimer_Elapsed);
			notifyIcon.Click+=new EventHandler(notifyIcon_Click);
			addTaskButton.Click+=new EventHandler(addTaskButton_Click);
			this.taskList.DoubleClick+=new EventHandler(taskList_DoubleClick);

			Tasks.TasksRowChanged+=new PTMDataset.TasksRowChangeEventHandler(TasksDataTable_TasksRowChanged);
			Tasks.TasksRowDeleting+=new PTMDataset.TasksRowChangeEventHandler(TasksDataTable_TasksRowDeleting);
			Logs.LogChanged+=new PTM.Business.Logs.LogChangeEventHandler(TasksLog_LogChanged);
			ApplicationsLog.ApplicationsLogRowChanged+=new PTMDataset.ApplicationsLogRowChangeEventHandler(ApplicationsLogTable_ApplicationsLogRowChanged);

			this.taskList.SmallImageList = IconsManager.IconsList;
			//this.Load+=new EventHandler(TasksLogControl_Load);
			
			
		}

		private void AddLogToList(Log log)
		{
			PTMDataset.TasksRow taskRow = Tasks.FindById(log.TaskId);
			TreeListViewItem itemA = new TreeListViewItem("", new string[] {"", ""});
			SetListItemValues(itemA,log, taskRow);
			taskList.Items.Insert(0,itemA);
		}
		
		private void LoadTodayLog()
		{
			ArrayList list = Logs.GetLogsByDay(DateTime.Today);
			foreach (Log log in list)
			{
				AddLogToList(log);
			}
		}

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
			this.TaskDescriptionHeader = new System.Windows.Forms.ColumnHeader();
			this.StartTimeHeader = new System.Windows.Forms.ColumnHeader();
			this.DurationTaskHeader = new System.Windows.Forms.ColumnHeader();
			this.notifyAnswerTimer = new System.Timers.Timer();
			this.notifyContextMenu = new System.Windows.Forms.ContextMenu();
			this.exitContextMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.notifyTimer = new System.Timers.Timer();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.taskList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
			this.Description = new System.Windows.Forms.ColumnHeader();
			this.Time = new System.Windows.Forms.ColumnHeader();
			this.Duration = new System.Windows.Forms.ColumnHeader();
			this.Id = new System.Windows.Forms.ColumnHeader();
			this.Id2 = new System.Windows.Forms.ColumnHeader();
			this.switchToButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.notifyAnswerTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.notifyTimer)).BeginInit();
			this.SuspendLayout();
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.editButton.Location = new System.Drawing.Point(168, 280);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(72, 23);
			this.editButton.TabIndex = 8;
			this.editButton.Text = "&Edit...";
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// addTaskButton
			// 
			this.addTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.addTaskButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.addTaskButton.Location = new System.Drawing.Point(328, 280);
			this.addTaskButton.Name = "addTaskButton";
			this.addTaskButton.Size = new System.Drawing.Size(72, 23);
			this.addTaskButton.TabIndex = 7;
			this.addTaskButton.Text = "&New Log...";
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
			this.DurationTaskHeader.Width = 80;
			// 
			// notifyAnswerTimer
			// 
			this.notifyAnswerTimer.SynchronizingObject = this;
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
																														this.menuItem8,
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
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "Lunch Time";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "Go to the Bath";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 5;
			this.menuItem5.Text = "Phone Call";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 6;
			this.menuItem6.Text = "Checking Mail";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 7;
			this.menuItem7.Text = "Out of my place";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 8;
			this.menuItem8.Text = "On a Meeting";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 9;
			this.menuItem9.Text = "-";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 10;
			this.menuItem10.Text = "About...";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
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
			this.taskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																											  this.TaskDescriptionHeader,
																											  this.DurationTaskHeader,
																											  this.StartTimeHeader});
			this.taskList.HideSelection = false;
			this.taskList.Location = new System.Drawing.Point(8, 8);
			this.taskList.Name = "taskList";
			this.taskList.Size = new System.Drawing.Size(392, 264);
			this.taskList.TabIndex = 10;
			this.taskList.SelectedIndexChanged += new System.EventHandler(this.taskList_SelectedIndexChanged);
			// 
			// Description
			// 
			this.Description.Text = "Description";
			this.Description.Width = 230;
			// 
			// Time
			// 
			this.Time.Text = "Time";
			this.Time.Width = 80;
			// 
			// Duration
			// 
			this.Duration.Text = "Duration";
			this.Duration.Width = 78;
			// 
			// Id
			// 
			this.Id.Width = 0;
			// 
			// Id2
			// 
			this.Id2.Width = 0;
			// 
			// switchToButton
			// 
			this.switchToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.switchToButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.switchToButton.Location = new System.Drawing.Point(248, 280);
			this.switchToButton.Name = "switchToButton";
			this.switchToButton.Size = new System.Drawing.Size(72, 23);
			this.switchToButton.TabIndex = 11;
			this.switchToButton.Text = "&Switch To";
			this.switchToButton.Click += new System.EventHandler(this.switchToButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.deleteButton.Location = new System.Drawing.Point(88, 280);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(72, 23);
			this.deleteButton.TabIndex = 12;
			this.deleteButton.Text = "&Delete";
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// TasksLogControl
			// 
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.switchToButton);
			this.Controls.Add(this.taskList);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.addTaskButton);
			this.Name = "TasksLogControl";
			this.Size = new System.Drawing.Size(408, 312);
			((System.ComponentModel.ISupportInitialize)(this.notifyAnswerTimer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.notifyTimer)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region TaskLog

		public void NewTaskLog(bool mustAddATask)
		{
			notifyTimer.Stop();
			TaskLogForm tasklog = new TaskLogForm();
			if (tasklog.ShowDialog(this) == DialogResult.OK)
			{
				AddTaskLog(tasklog.SelectedTaskRow.Id, (int)tasklog.Duration.TotalMinutes);
			}
			else if (mustAddATask)
			{
				if(Tasks.CurrentTaskRow==null)
					AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.Idle);
				else
					AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.Idle);
			}
		}

		private void AddDefaultTaskLog(int taskParentId, DefaultTask defaultTask)
		{
			Logs.AddDefaultTaskLog(taskParentId, defaultTask);
			ResetNotifyTimer(Convert.ToInt32(ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration).ConfigValue));
		}

		private void AddTaskLog(int taskId , int defaultMins)
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
			if(!isValidEditableLog())
				return;
			int taskId = ((Log) taskList.SelectedItems[0].Tag).TaskId;
			//int taskId =  Convert.ToInt32(taskList.SelectedItems[0].SubItems[TaskIdHeader.Index].Text, CultureInfo.InvariantCulture);
			string duration= taskList.SelectedItems[0].SubItems[DurationTaskHeader.Index].Text;
			
			TaskLogForm taskLogForm = new TaskLogForm(taskId, duration);
			if(taskLogForm.ShowDialog(this.Parent)==DialogResult.OK)
			{
				for(int i = 0; i < taskList.SelectedItems.Count; i++)
				{
					int taskLogId = ((Log) taskList.SelectedItems[0].Tag).Id;
					//int taskLogId = Convert.ToInt32(taskList.SelectedItems[i].SubItems[TaskLogIdHeader.Index].Text, CultureInfo.InvariantCulture);
					Logs.UpdateLogTaskId(taskLogId, taskLogForm.SelectedTaskRow.Id);
				}
			}
		}
		
		private void DeleteSelectedTaskLog()
		{
			if(!isValidEditableLog())
				return;
			
				for(int i = 0; i < taskList.SelectedItems.Count; i++)
				{
					int taskLogId = ((Log) taskList.SelectedItems[i].Tag).Id;
					//int taskLogId = Convert.ToInt32(taskList.SelectedItems[i].SubItems[TaskLogIdHeader.Index].Text, CultureInfo.InvariantCulture);
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
		
		private void deleteButton_Click(object sender, System.EventArgs e)
		{
			DeleteSelectedTaskLog();
		}
		
		private bool isValidEditableLog()
		{
			if(this.taskList.SelectedItems.Count==0)
				return false;
			if(taskList.SelectedItems[0].Parent!=null)
				return false;
			
			return true;
		}
		
		private void switchToButton_Click(object sender, System.EventArgs e)
		{
			if(!isValidEditableLog())
				return;
			int taskId =  ((Log)taskList.SelectedItems[0].Tag).TaskId;
			//int taskId =  Convert.ToInt32(taskList.SelectedItems[0].SubItems[TaskIdHeader.Index].Text, CultureInfo.InvariantCulture);
			AddTaskLog(taskId, 
				Convert.ToInt32(ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration).ConfigValue, CultureInfo.InvariantCulture));

		}

		private void taskList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.taskList.SelectedItems.Count<=0)
				this.editButton.Enabled = false;
			else if(this.taskList.SelectedItems.Count == 1)
			{
				if(taskList.SelectedItems[0].Parent==null)
					this.editButton.Enabled = true;
				else
					this.editButton.Enabled = false;
			}
			else
			{
				if(taskList.SelectedItems[0].Parent!=null)
				{
					this.editButton.Enabled = false;
					return;
				}
				int taskId =  ((Log)taskList.SelectedItems[0].Tag).TaskId;
				for(int i = 1 ; i<this.taskList.SelectedItems.Count;i++)
				{
					if(taskList.SelectedItems[i].Parent!=null)
					{
						this.editButton.Enabled = false;
						return;
					}
					if(((Log)taskList.SelectedItems[i].Tag).TaskId!=taskId)
					{
						this.editButton.Enabled = false;
						return;
					}
				}
				this.editButton.Enabled = true;
			}
		}

		#endregion

		#region Notifications

		private NotifyForm notifyForm;
		private void notifyIcon_MouseDown(object sender, MouseEventArgs e)		{
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
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.Idle);
			}
			else if (notifyForm.Result == NotifyForm.NotifyResult.No)
			{
				NewTaskLog(true);
			}
			else if (notifyForm.Result == NotifyForm.NotifyResult.Yes)
			{
				AddTaskLog(Tasks.CurrentTaskRow.Id, 
					Convert.ToInt32(ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration).ConfigValue, CultureInfo.InvariantCulture));
			}
			else
			{
				throw new NotImplementedException();
			}
		}


		#endregion

		#region NotifyContextMenu

		public event EventHandler Exit;
		private void exitContextMenuItem_Click(object sender, EventArgs e)
		{
			//this.tasksLogTimer.Stop();
			//LogTimer.timer.Stop();
			this.notifyTimer.Stop();
			this.notifyAnswerTimer.Stop();
			this.notifyIcon.Visible = false;
			this.notifyIcon.Icon.Dispose();
			this.notifyIcon.Dispose();
			this.Exit(this, e);
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			if(Tasks.CurrentTaskRow==null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.Idle);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.Idle);
		}

		private void menuItem3_Click(object sender, EventArgs e)
		{
			if(Tasks.CurrentTaskRow==null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.LunchTime);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.LunchTime);
		}

		private void menuItem4_Click(object sender, EventArgs e)
		{
			if(Tasks.CurrentTaskRow==null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.GoToTheBath);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.GoToTheBath);
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
			if(Tasks.CurrentTaskRow==null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.PhoneCall);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.PhoneCall);
		}

		private void menuItem6_Click(object sender, EventArgs e)
		{
			if(Tasks.CurrentTaskRow==null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.CheckingMail);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.CheckingMail);
		}

		private void menuItem7_Click(object sender, EventArgs e)
		{
			if(Tasks.CurrentTaskRow==null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.OutOfMyplace);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.OutOfMyplace);
		}

		private void menuItem8_Click(object sender, EventArgs e)
		{
			if(Tasks.CurrentTaskRow==null)
				AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.OnAMeeting);
			else
				AddDefaultTaskLog(Tasks.CurrentTaskRow.ParentId, DefaultTask.OnAMeeting);
		}

		private void menuItem10_Click(object sender, EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this.Parent);
		}


		#endregion

		#region PTM events

		private void TasksDataTable_TasksRowChanged(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			if(e.Action == DataRowAction.Change)
			{
				foreach (ListViewItem item in this.taskList.Items)
				{
					if (((Log)item.Tag).TaskId == e.Row.Id)
					{
						item.SubItems[TaskDescriptionHeader.Index].Text = e.Row.Description;
					}
				}
			}
		}

		private void TasksDataTable_TasksRowDeleting(object sender, PTMDataset.TasksRowChangeEvent e)
		{
				if(e.Action == DataRowAction.Delete)
				{			
					foreach (ListViewItem item in this.taskList.Items)
					{
						if (((Log)item.Tag).TaskId == e.Row.Id)
						{
							item.Remove();
						}
					}
				}
		}

		private void TasksLog_LogChanged(PTM.Business.Logs.LogChangeEventArgs e)
		{
			if(e.Action == DataRowAction.Change)
			{
				foreach (TreeListViewItem item in this.taskList.Items)
				{
					if (((Log)item.Tag).Id == e.Log.Id)
					{
						PTMDataset.TasksRow taskRow = Tasks.FindById(e.Log.TaskId);
						SetListItemValues(item,e.Log, taskRow);
						break;
					}
				}
			}

			else if(e.Action == DataRowAction.Add)
			{
				PTMDataset.TasksRow taskRow = Tasks.FindById(e.Log.TaskId);
				TreeListViewItem itemA = new TreeListViewItem("", new string[] {"", ""});
				SetListItemValues(itemA,e.Log, taskRow);
				taskList.Items.Insert(0,itemA);
				this.notifyIcon.Text = taskRow.Description;			
			}
		}

		private void SetListItemValues(ListViewItem item, Log log ,PTMDataset.TasksRow taskRow)
		{
			if(item.SubItems[TaskDescriptionHeader.Index].Text != taskRow.Description)
			{
				item.Text = taskRow.Description;
			}
			item.SubItems[DurationTaskHeader.Index].Text = ViewHelper.Int32ToTimeString(log.Duration);
			item.SubItems[StartTimeHeader.Index].Text = log.InsertTime.ToShortTimeString();
			
			if (taskRow.IsDefaultTask)
			{
				item.ImageIndex = IconsManager.GetIndex(taskRow.DefaultTaskId.ToString(CultureInfo.InvariantCulture));
				if(Logs.CurrentLog!=null && log.Id == Logs.CurrentLog.Id)
					notifyIcon.Icon = IconsManager.GetIcon(taskRow.DefaultTaskId.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				item.ImageIndex = IconsManager.GetIndex("0");
				if(Logs.CurrentLog!=null && log.Id == Logs.CurrentLog.Id)
					notifyIcon.Icon =  IconsManager.GetIcon("0");
			}
			item.Tag = log;
		}

		private void ApplicationsLogTable_ApplicationsLogRowChanged(object sender, PTMDataset.ApplicationsLogRowChangeEvent e)
		{
			if(e.Action == DataRowAction.Change)
			{
				this.UpdateApplicationsList(e.Row);
			}
		}

		private void UpdateApplicationsList(PTMDataset.ApplicationsLogRow appRow)
		{
			if (appRow == null) {return;}

			TimeSpan active =  new TimeSpan(0,0,appRow.ActiveTime);
			string activeTime = ViewHelper.TimeSpanToTimeString(active);
			string caption = appRow.Caption.Length != 0 ? appRow.Caption : appRow.Name;
			TreeListViewItem lvi = null;
			foreach (TreeListViewItem item in this.taskList.Items[0].Items)
			{
				if (((Log)item.Tag).Id == appRow.Id)
				{
					lvi = item;
					lvi.SubItems[TaskDescriptionHeader.Index].Text = caption;
					lvi.SubItems[DurationTaskHeader.Index].Text = activeTime;
					break;
				}
			}
			if (lvi == null)
			{
				lvi = new TreeListViewItem(caption, new string[] {activeTime, "", appRow.Id.ToString(CultureInfo.InvariantCulture)});
				lvi.ImageIndex = IconsManager.AddIconFromFile(appRow.ApplicationFullPath);
				this.taskList.Items[0].Items.Add(lvi);
			}
		}

		#endregion

		private void TasksLogControl_Load(object sender, EventArgs e)
		{
			LoadTodayLog();
		}
	}
}
