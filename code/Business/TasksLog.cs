using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Globalization;
using System.Timers;
using PTM.Data;

namespace PTM.Business
{
	/// <summary>
	/// Summary description for TaskNotificationsHelper.
	/// </summary>
	internal sealed class TasksLog
	{
		private TasksLog()
		{
		}

		private static OleDbDataAdapter tasksLogAdapter;
		private static PTMDataset.TasksLogDataTable taskLogsTable;
		private static PTMDataset.TasksLogDataTable taskLogsTableInstace;
		private static PTMDataset.TasksLogRow currentTaskLog;
		private static Timer taskLogTimer;

		#region Properties
		internal static PTMDataset.TasksLogRow CurrentTaskLog
		{
			get { return CloneRow(currentTaskLog); }
		}
		#endregion

		#region Public Methods
		public static void Initialize(PTMDataset.TasksLogDataTable dataTable, OleDbDataAdapter adapter)
		{
			tasksLogAdapter = adapter;
			taskLogsTable = dataTable;
			taskLogsTableInstace = new PTMDataset.TasksLogDataTable();
			currentTaskLog = null;
			taskLogTimer = new Timer(1000);
			taskLogTimer.Elapsed+=new ElapsedEventHandler(TaskLogTimer_Elapsed);
		}
//		public static PTMDataset.TasksLogRow NewTasksLogRow()
//		{
//			return taskLogsTableInstace.NewTasksLogRow();
//		}

		public static PTMDataset.TasksLogRow AddTasksLog(int taskId)
		{
			PTMDataset.TasksLogRow row;
			row = taskLogsTable.NewTasksLogRow();
			row.Duration = 0;
			row.InsertTime = DateTime.Now;
			row.TaskId = taskId;
			taskLogsTable.AddTasksLogRow(row);
			SaveTasksLogs();
			currentTaskLog = row;
			if(TasksLogRowChanged!=null)
			{
				TasksLogRowChanged(null, new PTMDataset.TasksLogRowChangeEvent(CloneRow(row), DataRowAction.Add));
			}
			return CloneRow(row);
		}

		public static void UpdateTaskLog(int id, int taskId)
		{
			PTMDataset.TasksLogRow row;
			row = taskLogsTable.FindById(id);
			row.TaskId = taskId;
			
			if(TasksLogRowChanged!=null)
			{
				TasksLogRowChanged(null, new PTMDataset.TasksLogRowChangeEvent(CloneRow(row), DataRowAction.Change));
			}
		}
		public static void DeleteTaskLog(int id)
		{
			PTMDataset.TasksLogRow logRow;
			logRow = taskLogsTable.FindById(id);
			
			PTMDataset.TasksRow taskRow;
			taskRow = Tasks.FindById(logRow.TaskId);
			
			string description = DefaultTask.Idle.ToString(CultureInfo.InvariantCulture);
			PTMDataset.TasksRow[] childRows;
			childRows = Tasks.GetChildTasks(Tasks.FindById(taskRow.ParentId));
			
			int defaultTaskId = -1;
			foreach (PTMDataset.TasksRow childRow in childRows)
			{
				if (string.Compare(childRow.Description.Replace(" ", null), description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
				{
					defaultTaskId = childRow.Id;
					break;
				}
			}
			
			if(defaultTaskId ==-1)
			foreach (PTMDataset.TasksRow defaultRow in DefaultTasks.DefaultTasksDataTable)
			{
				if (string.Compare(defaultRow.Description.Replace(" ", null), description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
				{
					PTMDataset.TasksRow row = Tasks.NewTasksRow();
					row.ItemArray = defaultRow.ItemArray;
					row.ParentId = taskRow.ParentId;
					row.Id = Tasks.AddTasksRow(row);
					defaultTaskId = row.Id;
					break;
				}
			}
			if(defaultTaskId !=-1)
			{
				UpdateTaskLog(id, defaultTaskId);
			}
			else
			{
				throw new ApplicationException("An unexpected error has been ocurred in the application during deleting a log.");
			}
		}
		public static PTMDataset.TasksLogRow AddDefaultTaskLog(int taskParentId, DefaultTask defaultTask)
		{

			string description = defaultTask.ToString(CultureInfo.InvariantCulture);
			PTMDataset.TasksRow[] childRows;
			childRows = Tasks.GetChildTasks(Tasks.FindById(taskParentId));
			foreach (PTMDataset.TasksRow childRow in childRows)
			{
				if (string.Compare(childRow.Description.Replace(" ", null), description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
				{
					return AddTasksLog(childRow.Id);
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
					return AddTasksLog(row.Id);
				}
			}
			throw new InvalidOperationException();
		}
		public static PTMDataset.TasksLogRow FindById(int taskLogId)
		{
			PTMDataset.TasksLogRow findedRow;
			findedRow = taskLogsTable.FindById(taskLogId);
			return CloneRow(findedRow);
		}
		public static void StartLogging()
		{
			taskLogTimer.Start();
			if(AfterStartLogging!=null)
				AfterStartLogging(null, null);
		}

		public static void StopLogging()
		{
			taskLogTimer.Stop();
			SaveTasksLogs();
			if(AfterStopLogging!=null)
				AfterStopLogging(null, null);
		}
		#endregion

		#region Private Methods
		private static PTMDataset.TasksLogRow CloneRow(PTMDataset.TasksLogRow tasksLogRow)
		{
			if(tasksLogRow==null)
				return null;
			PTMDataset.TasksLogRow row;
			row =  taskLogsTableInstace.NewTasksLogRow();
			row.ItemArray = tasksLogRow.ItemArray;
			return row;
		}
		private static void SaveTasksLogs()
		{
			tasksLogAdapter.Update(taskLogsTable);
			taskLogsTable.AcceptChanges();
		}

		#endregion

		#region Events
		public static event PTMDataset.TasksLogRowChangeEventHandler TasksLogRowChanged;
		public static event ElapsedEventHandler TasksLogDurationCountElapsed;
		public static event System.EventHandler AfterStartLogging;
		public static event System.EventHandler AfterStopLogging;
		private static void TaskLogTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if(currentTaskLog == null)
				return;

			TimeSpan t = new TimeSpan(0, 0, currentTaskLog.Duration);
			t = t.Add(new TimeSpan(0, 0, 1));
			currentTaskLog.Duration = Convert.ToInt32(t.TotalSeconds);
			if(TasksLogRowChanged!=null)
			{
				TasksLogRowChanged(null, new PTMDataset.TasksLogRowChangeEvent(CloneRow(currentTaskLog), DataRowAction.Change));
			}
			if(TasksLogDurationCountElapsed!=null)
				TasksLogDurationCountElapsed(sender, e);
		}

		#endregion


	}
}