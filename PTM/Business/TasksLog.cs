using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
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
		internal static void Initialize(PTMDataset.TasksLogDataTable dataTable, OleDbDataAdapter adapter)
		{
			tasksLogAdapter = adapter;
			taskLogsTable = dataTable;
			taskLogsTableInstace = new PTMDataset.TasksLogDataTable();
			currentTaskLog = null;
			taskLogTimer = new Timer(1000);
			taskLogTimer.Elapsed+=new ElapsedEventHandler(TaskLogTimer_Elapsed);
		}
		public static PTMDataset.TasksLogRow NewTasksLogRow()
		{
			return taskLogsTableInstace.NewTasksLogRow();
		}

		public static int AddTasksLogRow(PTMDataset.TasksLogRow tasksLogRow)
		{
			tasksLogRow.Duration = 0;
			tasksLogRow.InsertTime = DateTime.Now;

			PTMDataset.TasksLogRow row;
			
			row = taskLogsTable.NewTasksLogRow();
			row.ItemArray = tasksLogRow.ItemArray;
			taskLogsTable.AddTasksLogRow(row);
			SaveTasksLogs();
			currentTaskLog = row;
			if(TasksLogRowChanged!=null)
			{
				tasksLogRow.Id = row.Id;
				TasksLogRowChanged(null, new PTMDataset.TasksLogRowChangeEvent(tasksLogRow, DataRowAction.Add));
			}
			return row.Id;
		}

		public static void UpdateTaskLog(PTMDataset.TasksLogRow tasksLogRow)
		{
			PTMDataset.TasksLogRow row;
			row = taskLogsTable.FindById(tasksLogRow.Id);
//			if(row == null)
//			{
//				
//				tasksLogAdapter.SelectCommand = new OleDbCommand("SELECT * FROM TasksLog WHERE " + taskLogsTable.IdColumn.ColumnName + " = " +
//				                                tasksLogRow.Id, tasksLogAdapter.SelectCommand.Connection);
//				
//				tasksLogAdapter.Fill(taskLogsTable);
//				row = taskLogsTable.FindById(tasksLogRow.Id);
//				SaveTasksLogs();
//			}
			row.ItemArray = tasksLogRow.ItemArray;

			if(TasksLogRowChanged!=null)
			{
				TasksLogRowChanged(null, new PTMDataset.TasksLogRowChangeEvent(tasksLogRow, DataRowAction.Change));
			}
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
			row = NewTasksLogRow();
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
		public static event ElapsedEventHandler Elapsed;
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
			if(Elapsed!=null)
				Elapsed(sender, e);
		}

		#endregion


	}
}