using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Timers;
using PTM.Data;
using PTM.Infos;

namespace PTM.Business
{
	internal sealed class Logs
	{
		private Logs()
		{
		}

		private static Log currentLog;
		private static Timer taskLogTimer;

		#region Properties
		public static Log CurrentLog
		{
			get { return currentLog; }
		}
		#endregion

		#region Public Methods
		public static void Initialize()
		{
			currentLog = null;
			taskLogTimer = new Timer(1000);
			taskLogTimer.Elapsed+=new ElapsedEventHandler(TaskLogTimer_Elapsed);
		}
		public static Log AddLog(int taskId)
		{
			UpdateCurrentLogDuration();
			
			Log log = new Log();
			log.Duration = 0;
			log.InsertTime = DateTime.Now;
			log.TaskId = taskId;
			log.Id  = DataAdapterManager.ExecuteInsert("INSERT INTO TasksLog(Duration, InsertTime, TaskId) VALUES (?, ?, ?)", 
				new string[]{"Duration", "InsertTime", "TaskId"}, new object[] {log.Duration, log.InsertTime, log.TaskId});
					
			currentLog = log;
			if(LogChanged!=null)
			{
				LogChanged(new LogChangeEventArgs(log, DataRowAction.Add));
			}
			return log;
		}

		public static void UpdateLogTaskId(int id, int taskId)
		{
			Log log;
			log = FindById(id);
			log.TaskId = taskId;
			DataAdapterManager.ExecuteNonQuery("UPDATE TasksLog SET TaskId = " + taskId + " WHERE Id = " + id);
			
			if(currentLog !=null && currentLog.Id == id)
				currentLog.TaskId = taskId;
			
			if(LogChanged!=null)
			{
				LogChanged(new LogChangeEventArgs(log, DataRowAction.Change));
			}
		}
		
		public static void DeleteLog(int id)
		{
			Log log;
			log = FindById(id);
			
			PTMDataset.TasksRow taskRow;
			taskRow = Tasks.FindById(log.TaskId);
			
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
				UpdateLogTaskId(id, defaultTaskId);
			}
			else
			{
				throw new ApplicationException("An unexpected error has been ocurred in the application during deleting a log.");
			}
		}
		public static Log AddDefaultTaskLog(int taskParentId, DefaultTask defaultTask)
		{

			string description = defaultTask.ToString(CultureInfo.InvariantCulture);
			PTMDataset.TasksRow[] childRows;
			childRows = Tasks.GetChildTasks(Tasks.FindById(taskParentId));
			foreach (PTMDataset.TasksRow childRow in childRows)
			{
				if (string.Compare(childRow.Description.Replace(" ", null), description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
				{
					return AddLog(childRow.Id);
				}
			}
			
			foreach (PTMDataset.TasksRow defaultRow in DefaultTasks.DefaultTasksDataTable)
			{
				if (string.Compare(defaultRow.Description.Replace(" ", null), description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
				{
					PTMDataset.TasksRow row = Tasks.NewTasksRow();
					row.ItemArray = defaultRow.ItemArray;
					row.ParentId = taskParentId;
					row.Id = Tasks.AddTasksRow(row);
					return AddLog(row.Id);
				}
			}
			throw new InvalidOperationException();
		}
		public static Log FindById(int id)
		{
//			if(currentLog!=null && currentLog.Id == id)
//				return currentLog;
			Hashtable hash;
			hash = DataAdapterManager.ExecuteGetFirstRow("Select TaskId, Duration, InsertTime  from TasksLog where Id = " + id);
			if(hash==null)
				return null;
			Log log = new Log();
			log.Id = id;
			log.TaskId = (int) hash["TaskId"];
			log.Duration = (int) hash["Duration"];
			log.InsertTime = (DateTime) hash["InsertTime"];
			return log;
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
			UpdateCurrentLogDuration();
			if(AfterStopLogging!=null)
				AfterStopLogging(null, null);
		}
		public static ArrayList GetLogsByDay(DateTime day)
		{
			DateTime date = day.Date;
			ArrayList hashList = DataAdapterManager.ExecuteGetRows(
				"Select Id, TaskId, Duration, InsertTime  from TasksLog where InsertTime >= ? and InsertTime <= ? order by InsertTime", 
				new string[]{"InsertTimeFrom", "InsertTimeTo"}, new object[]{date, date.AddDays(1).AddSeconds(-1)});
			
			if(hashList == null)
				return null;

			ArrayList list = new ArrayList();
			foreach (Hashtable hashtable in hashList)
			{
				Log log = new Log();
				log.Id = (int) hashtable["Id"];;
				log.TaskId = (int) hashtable["TaskId"];
				log.Duration = (int) hashtable["Duration"];
				log.InsertTime = (DateTime) hashtable["InsertTime"];
				list.Add(log);
			}
			return list;
		}
		
		public static void UpdateCurrentLogDuration()
		{
			if(currentLog==null)
				return;
			
			DataAdapterManager.ExecuteNonQuery("UPDATE TasksLog SET Duration = ? WHERE Id = " + Logs.currentLog.Id, 
				new string[]{"Duration"}, new object[]{Logs.currentLog.Duration});
			if(LogChanged!=null)
			{
				LogChanged(new LogChangeEventArgs(Logs.currentLog, DataRowAction.Change));
			}
		}

		#endregion

		#region Private Methods
		
		private static void TaskLogTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if(currentLog == null)
				return;

			TimeSpan t = new TimeSpan(0, 0, currentLog.Duration);
			t = t.Add(new TimeSpan(0, 0, 1));
			currentLog.Duration = Convert.ToInt32(t.TotalSeconds);
			if(LogChanged!=null)
			{
				LogChanged(new LogChangeEventArgs(currentLog, DataRowAction.Change));
			}
			if(TasksLogDurationCountElapsed!=null)
				TasksLogDurationCountElapsed(sender, e);
		}
		#endregion

		#region Events
		public delegate void LogChangeEventHandler(LogChangeEventArgs e);
		public class LogChangeEventArgs : EventArgs
		{
			private Log log;
			private DataRowAction action;
			public LogChangeEventArgs(Log log, DataRowAction action)
			{
				this.log = log;
				this.action = action;
			}
			public Log Log
			{
				get { return log; }
			}
			
			public DataRowAction Action
			{
				get { return action; }
			}
		}
		
		public static event LogChangeEventHandler LogChanged;
		public static event ElapsedEventHandler TasksLogDurationCountElapsed;
		public static event EventHandler AfterStartLogging;
		public static event EventHandler AfterStopLogging;
		#endregion
	}
}