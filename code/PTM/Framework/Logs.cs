using System;
using System.Collections;
using System.Data;
using System.Timers;
using PTM.Data;
using PTM.Framework.Helpers;
using PTM.Framework.Infos;

namespace PTM.Framework
{
	public sealed class Logs
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
			taskLogTimer.Elapsed += new ElapsedEventHandler(TaskLogTimer_Elapsed);
		}

		public static Log AddLog(int taskId)
		{
			UpdateCurrentLogDuration();

			Log log = new Log();
			log.Duration = 0;
			log.InsertTime = DateTime.Now;
			log.TaskId = taskId;
			log.Id = DbHelper.ExecuteInsert("INSERT INTO TasksLog(Duration, InsertTime, TaskId) VALUES (?, ?, ?)",
			                                new string[] {"Duration", "InsertTime", "TaskId"},
			                                new object[] {log.Duration, log.InsertTime, log.TaskId});

			currentLog = log;
			if (LogChanged != null)
			{
				LogChanged(new LogChangeEventArgs(log, DataRowAction.Add));
			}
			return log;
		}

		public static void UpdateLogTaskId(int logId, int taskId)
		{
			Log log;
			log = FindById(logId);
			log.TaskId = taskId;
			DbHelper.ExecuteNonQuery("UPDATE TasksLog SET TaskId = " + taskId + " WHERE Id = " + logId);

			if (currentLog != null && currentLog.Id == logId)
				currentLog.TaskId = taskId;

			if (LogChanged != null)
			{
				LogChanged(new LogChangeEventArgs(log, DataRowAction.Change));
			}
		}

		public static void DeleteLog(int id)
		{
			int idleTaskId = Tasks.IdleTask.Id;
			UpdateLogTaskId(id, idleTaskId);
		}

		public static Log FindById(int id)
		{
			if (currentLog != null && currentLog.Id == id)
				return currentLog;
			IDictionary dictionary;
			dictionary = DbHelper.ExecuteGetFirstRow("Select TaskId, Duration, InsertTime  from TasksLog where Id = " + id);
			if (dictionary == null)
				return null;
			Log log = new Log();
			log.Id = id;
			log.TaskId = (int) dictionary["TaskId"];
			log.Duration = (int) dictionary["Duration"];
			log.InsertTime = (DateTime) dictionary["InsertTime"];
			return log;
		}

		public static void StartLogging()
		{
			taskLogTimer.Start();
			if (AfterStartLogging != null)
				AfterStartLogging(null, null);
		}

		public static void StopLogging()
		{
			taskLogTimer.Stop();
			UpdateCurrentLogDuration();
			if (AfterStopLogging != null)
				AfterStopLogging(null, null);
		}

		/// <summary>
		/// Fill with Idle logs the time that the application was off.
		/// </summary>
		public static void FillMissingTimeUntilNow()
		{
			//Check db is not empty
			int logCount = (int) DbHelper.ExecuteScalar("Select Count(1) From TasksLog");
			if (logCount == 0)
				return;

			DateTime lastLogInsert = (DateTime) DbHelper.ExecuteScalar("Select max(InsertTime) from TasksLog");

			int lastLogDuration = (int) DbHelper.ExecuteScalar("Select Duration from TasksLog Where InsertTime >= ?",
			                                                   new string[] {"Duration"}, new object[] {lastLogInsert});

			FillMissingTimeUntilNowRecursively(lastLogInsert, lastLogDuration);
		}

		private static void FillMissingTimeUntilNowRecursively(DateTime lastLogInsert, int lastLogDuration)
		{
			if (DateTime.Now.Subtract(lastLogInsert).TotalSeconds < 60) // less than 1 minute is ignored
			{
				return;
			}

			DateTime lastLogFinish = lastLogInsert.AddSeconds(lastLogDuration);
			if ((DateTime.Now - lastLogFinish).TotalSeconds < 60) // less than 1 minute is ignored
			{
				return;
			}

			int defaultTaskId = Tasks.IdleTask.Id;
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			int duration = (int) ((DateTime.Now - lastLogFinish).TotalSeconds > ((int) config.Value)*60
			                      	? (int) config.Value*60
			                      	: (DateTime.Now - lastLogFinish).TotalSeconds);

			DbHelper.ExecuteInsert("INSERT INTO TasksLog(Duration, InsertTime, TaskId) VALUES (?, ?, ?)",
			                       new string[] {"Duration", "InsertTime", "TaskId"},
			                       new object[] {duration, lastLogFinish, defaultTaskId});
			FillMissingTimeUntilNowRecursively(lastLogFinish, duration);
		}

		public static ArrayList GetLogsByDay(DateTime day)
		{
			DateTime date = day.Date;
			ArrayList arrayList = DbHelper.ExecuteGetRows(
				"Select Id, TaskId, Duration, InsertTime  from TasksLog where InsertTime >= ? and InsertTime <= ? order by InsertTime",
				new string[] {"InsertTimeFrom", "InsertTimeTo"}, new object[] {date, date.AddDays(1).AddSeconds(-1)});

			if (arrayList == null)
				return null;

			ArrayList list = new ArrayList();
			foreach (IDictionary dictionary in arrayList)
			{
				Log log = new Log();
				log.Id = (int) dictionary["Id"];
				;
				log.TaskId = (int) dictionary["TaskId"];
				log.Duration = (int) dictionary["Duration"];
				log.InsertTime = (DateTime) dictionary["InsertTime"];
				list.Add(log);
			}
			return list;
		}

		public static ArrayList GetLogsByTask(int taskId)
		{
			ArrayList arrayList = DbHelper.ExecuteGetRows(
				"Select Id, TaskId, Duration, InsertTime  from TasksLog where TaskId = ? order by InsertTime",
				new string[] {"TaskId"}, new object[] {taskId});

			if (arrayList == null)
				return null;

			ArrayList list = new ArrayList();
			foreach (IDictionary dictionary in arrayList)
			{
				Log log = new Log();
				log.Id = (int) dictionary["Id"];
				;
				log.TaskId = (int) dictionary["TaskId"];
				log.Duration = (int) dictionary["Duration"];
				log.InsertTime = (DateTime) dictionary["InsertTime"];
				list.Add(log);
			}
			return list;
		}

		public static void UpdateCurrentLogDuration()
		{
			if (currentLog == null)
				return;

			DbHelper.ExecuteNonQuery("UPDATE TasksLog SET Duration = ? WHERE Id = " + currentLog.Id,
			                         new string[] {"Duration"}, new object[] {currentLog.Duration});
            //if (LogChanged != null)
            //{
            //    LogChanged(new LogChangeEventArgs(currentLog, DataRowAction.Change));
            //}
		}

		public static void AddIdleTaskLog()
		{
			AddLog(Tasks.IdleTask.Id);
		}

		public static void ChangeLogsTaskId(int oldTaskId, int newTaskId)
		{
			ArrayList logs;
			logs = GetLogsByTask(oldTaskId);
			for (int i = 0; i < logs.Count; i++)
			{
				Log log = (Log) logs[i];
				UpdateLogTaskId(log.Id, newTaskId);
			}
			//DbHelper.ExecuteNonQuery("Update TasksLog Set TaskId = " + newTaskId + " Where TaskId = oldTaskId");
		}

		#endregion

		#region Private Methods

		private static void TaskLogTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (currentLog == null)
				return;

			TimeSpan t = new TimeSpan(0, 0, currentLog.Duration);
			t = t.Add(new TimeSpan(0, 0, 1));
			currentLog.Duration = Convert.ToInt32(t.TotalSeconds);
            //if (LogChanged != null)
            //{
            //    LogChanged(new LogChangeEventArgs(currentLog, DataRowAction.Change));
            //}
			if (CurrentLogDurationChanged != null)
				CurrentLogDurationChanged(sender, e);
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
		public static event ElapsedEventHandler CurrentLogDurationChanged;
		public static event EventHandler AfterStartLogging;
		public static event EventHandler AfterStopLogging;

		#endregion
	}
}