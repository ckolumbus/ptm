using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;
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
		private ColumnHeader DateTaskHeader;
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
		private ColumnHeader TaskIdHeader;
		private ColumnHeader TaskLogIdHeader;
		private Button button1;
		private TreeListView taskList;
		private ColumnHeader Description;
		private ColumnHeader Time;
		private ColumnHeader Duration;
		private ColumnHeader Id;
		private ColumnHeader Id2;

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
			//tasksLogTimer.Elapsed+=new ElapsedEventHandler(tasksLogTimer_Elapsed);
			//TasksLogHelper.TaskLogTimer.Elapsed+=new ElapsedEventHandler(timer_Elapsed);

			Tasks.TasksRowChanged+=new PTMDataset.TasksRowChangeEventHandler(TasksDataTable_TasksRowChanged);
			Tasks.TasksRowDeleting+=new PTMDataset.TasksRowChangeEventHandler(TasksDataTable_TasksRowDeleting);
			TasksLog.TasksLogRowChanged+=new PTMDataset.TasksLogRowChangeEventHandler(TaskLogsTable_TasksLogRowChanged);
			ApplicationsLog.ApplicationsLogRowChanged+=new PTMDataset.ApplicationsLogRowChangeEventHandler(ApplicationsLogTable_ApplicationsLogRowChanged);

			this.taskList.SmallImageList = IconsManager.IconsList;
			
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
			this.DateTaskHeader = new System.Windows.Forms.ColumnHeader();
			this.DurationTaskHeader = new System.Windows.Forms.ColumnHeader();
			this.TaskLogIdHeader = new System.Windows.Forms.ColumnHeader();
			this.TaskIdHeader = new System.Windows.Forms.ColumnHeader();
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
			this.button1 = new System.Windows.Forms.Button();
			this.taskList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
			this.Description = new System.Windows.Forms.ColumnHeader();
			this.Time = new System.Windows.Forms.ColumnHeader();
			this.Duration = new System.Windows.Forms.ColumnHeader();
			this.Id = new System.Windows.Forms.ColumnHeader();
			this.Id2 = new System.Windows.Forms.ColumnHeader();
			((System.ComponentModel.ISupportInitialize)(this.notifyAnswerTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.notifyTimer)).BeginInit();
			this.SuspendLayout();
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.editButton.Location = new System.Drawing.Point(232, 280);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(72, 23);
			this.editButton.TabIndex = 8;
			this.editButton.Text = "Edit...";
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
			this.addTaskButton.Text = "Add...";
			// 
			// TaskDescriptionHeader
			// 
			this.TaskDescriptionHeader.Text = "Task Description";
			this.TaskDescriptionHeader.Width = 226;
			// 
			// DateTaskHeader
			// 
			this.DateTaskHeader.Text = "Start Time";
			this.DateTaskHeader.Width = 80;
			// 
			// DurationTaskHeader
			// 
			this.DurationTaskHeader.Text = "Duration";
			this.DurationTaskHeader.Width = 80;
			// 
			// TaskLogIdHeader
			// 
			this.TaskLogIdHeader.Text = "Log";
			this.TaskLogIdHeader.Width = 0;
			// 
			// TaskIdHeader
			// 
			this.TaskIdHeader.Text = "Id";
			this.TaskIdHeader.Width = 0;
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
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(88, 280);
			this.button1.Name = "button1";
			this.button1.TabIndex = 9;
			this.button1.Text = "Details...";
			this.button1.Visible = false;
			// 
			// taskList
			// 
			this.taskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.taskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																											  this.TaskDescriptionHeader,
																											  this.DurationTaskHeader,
																											  this.DateTaskHeader,
																											  this.TaskLogIdHeader,
																											  this.TaskIdHeader});
			this.taskList.Location = new System.Drawing.Point(8, 8);
			this.taskList.Name = "taskList";
			this.taskList.Size = new System.Drawing.Size(392, 264);
			this.taskList.TabIndex = 10;
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
			// TasksLogControl
			// 
			this.Controls.Add(this.taskList);
			this.Controls.Add(this.button1);
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

//		private void UpdateCurrentTaskLog()
//		{
//			if(this.taskList.Items.Count > 0)
//			{
//				TimeSpan t = new TimeSpan(0, 0, TasksLogHelper.CurrentTaskLog.Duration);
//
//				t = t.Add(new TimeSpan(0, 0, 1));
//				this.taskList.Items[0].SubItems[DurationTaskHeader.Index].Text = ViewHelper.TimeSpanToString(t);
//				TasksLogHelper.CurrentTaskLog.Duration = Convert.ToInt32(t.TotalSeconds);
//				this.notifyIcon.Text = this.taskList.Items[0].SubItems[TaskDescriptionHeader.Index].Text;
//			}
//		}

		public void NewTaskLog(bool mustAddATask)
		{
			notifyTimer.Stop();
			TaskLogForm tasklog = new TaskLogForm();
			if (tasklog.ShowDialog(this) == DialogResult.OK)
			{
				AddTaskLog(tasklog.SelectedTaskRow, (int)tasklog.Duration.TotalMinutes);
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

			string description = defaultTask.ToString(CultureInfo.InvariantCulture);
			PTMDataset.TasksRow[] childRows;
			childRows = Tasks.GetChildTasks(Tasks.FindById(taskParentId));
			foreach (PTMDataset.TasksRow childRow in childRows)
			{
				if (string.Compare(childRow.Description.Replace(" ", null), description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
				{
					AddTaskLog(childRow, Convert.ToInt32(ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration).ConfigValue, CultureInfo.InvariantCulture));
					return;
				}
			}
			
			foreach (PTMDataset.TasksRow defaultRow in DefaultTasks.DefaultTasksDataTable)
			{
				if (string.Compare(defaultRow.Description.Replace(" ", null), description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
				{
					PTMDataset.TasksRow row = Tasks.NewTasksRow();
					row.BeginEdit();
					row.ItemArray = defaultRow.ItemArray;
					row.ParentId = taskParentId;
					row.Id = Tasks.AddTasksRow(row);
					AddTaskLog(row, Convert.ToInt32(ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration).ConfigValue));
					return;
				}
			}
			throw new InvalidOperationException();
		}

		private void AddTaskLog(PTMDataset.TasksRow taskRow , int defaultMins)
		{
			PTMDataset.TasksLogRow row = TasksLog.NewTasksLogRow();
			row.TaskId= taskRow.Id;
			row.Id = TasksLog.AddTasksLogRow(row);
			notifyTimer.Stop();
			notifyTimer.Interval = 1000*60*defaultMins;
			notifyTimer.Start();
		}

		private void EditSelectedTaskLog()
		{
			if(this.taskList.SelectedItems.Count==0)
				return;
			if(taskList.SelectedItems[0].Parent!=null)
				return;
			if(taskList.SelectedItems.Count!=1)
			{
				throw new InvalidOperationException();
			}

			int taskId =  Convert.ToInt32(taskList.SelectedItems[0].SubItems[TaskIdHeader.Index].Text, CultureInfo.InvariantCulture);
			string duration= taskList.SelectedItems[0].SubItems[DurationTaskHeader.Index].Text;		
			TaskLogForm taskLogForm = new TaskLogForm(taskId, duration);
			if(taskLogForm.ShowDialog(this.Parent)==DialogResult.OK)
			{
				int taskLogId = Convert.ToInt32(taskList.SelectedItems[0].SubItems[TaskLogIdHeader.Index].Text, CultureInfo.InvariantCulture);
				PTMDataset.TasksLogRow logRow;
				logRow = TasksLog.FindById(taskLogId);
				logRow.TaskId = taskLogForm.SelectedTaskRow.Id;
				TasksLog.UpdateTaskLog(logRow);
			}
		}
		private void addTaskButton_Click(object sender, EventArgs e)
		{
			NewTaskLog(false);
		}

		private void editButton_Click(object sender, EventArgs e)
		{
			EditSelectedTaskLog();
			return;
		}


		private void taskList_DoubleClick(object sender, EventArgs e)
		{
			EditSelectedTaskLog();
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
			Thread th = new Thread(new ThreadStart(GetAnswer));
			th.Priority = ThreadPriority.BelowNormal;
			th.Start();
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
				AddTaskLog(Tasks.CurrentTaskRow, 
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
					if (item.SubItems[TaskIdHeader.Index].Text == e.Row.Id.ToString(CultureInfo.InvariantCulture))
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
						if (item.SubItems[TaskIdHeader.Index].Text == e.Row.Id.ToString(CultureInfo.InvariantCulture))
						{
							item.Remove();
						}
					}
				}
		}

		private void TaskLogsTable_TasksLogRowChanged(object sender, PTMDataset.TasksLogRowChangeEvent e)
		{
			if(e.Action == DataRowAction.Change)
			{
				foreach (TreeListViewItem item in this.taskList.Items)
				{
					if (item.SubItems[TaskLogIdHeader.Index].Text == e.Row.Id.ToString(CultureInfo.InvariantCulture))
					{
						PTMDataset.TasksRow taskRow = Tasks.FindById(e.Row.TaskId);
						SetListItemValues(item,e.Row, taskRow);
						break;
					}
				}
			}

			else if(e.Action == DataRowAction.Add)
			{
				PTMDataset.TasksRow taskRow = Tasks.FindById(e.Row.TaskId);
				TreeListViewItem itemA = new TreeListViewItem("", new string[] {"00:00:00", DateTime.Now.ToShortTimeString(), "",""});
				SetListItemValues(itemA,e.Row, taskRow);
				taskList.Items.Insert(0,itemA);
				this.notifyIcon.Text = taskRow.Description;			
			}
		}

		private void SetListItemValues(ListViewItem item, PTMDataset.TasksLogRow taskLogRow ,PTMDataset.TasksRow taskRow)
		{
			if(item.SubItems[TaskDescriptionHeader.Index].Text != taskRow.Description)
			{
				item.Text = taskRow.Description;
			}
			item.SubItems[TaskLogIdHeader.Index].Text = taskLogRow.Id.ToString(CultureInfo.InvariantCulture);
			item.SubItems[TaskIdHeader.Index].Text = taskRow.Id.ToString(CultureInfo.InvariantCulture);
			item.SubItems[DurationTaskHeader.Index].Text = ViewHelper.Int32ToTimeString(taskLogRow.Duration);
			
			if (taskRow.IsDefaultTask)
			{
				item.ImageIndex = IconsManager.GetIndex(taskRow.DefaultTaskId.ToString(CultureInfo.InvariantCulture));
				if(taskLogRow.Id == TasksLog.CurrentTaskLog.Id)
					notifyIcon.Icon = IconsManager.GetIcon(taskRow.DefaultTaskId.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				item.ImageIndex = IconsManager.GetIndex("0");
				if(taskLogRow.Id == TasksLog.CurrentTaskLog.Id)
					notifyIcon.Icon =  IconsManager.GetIcon("0");
			}
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
				if (Convert.ToInt32(item.SubItems[TaskLogIdHeader.Index].Text, 
					CultureInfo.InvariantCulture) == appRow.Id)
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


	}
}
