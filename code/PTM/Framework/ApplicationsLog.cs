using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using PTM.Data;
using PTM.Framework.Infos;
using PTM.Util;
using PTM.View;

namespace PTM.Framework
{
	/// <summary>
	/// Summary description for ApplicationsLog.
	/// </summary>
	public sealed class ApplicationsLog
	{
		private ApplicationsLog()
		{
		}

		
		/// <summary>
		/// Inner list of currently managed applications log.
		/// </summary>
		private static ArrayList currentApplicationsLog;
		/// <summary>
		/// Reference to the last evaluated process .
		/// </summary>
		private static IntPtr lastProcess;
		/// <summary>
		/// TimeStamp of the last logged time .
		/// </summary>
		private static DateTime lastCallTime;
		/// <summary>
		/// Internal Thread that controls the log.
		/// </summary>
		private static Thread loggingThread;

		#region Public Methods
		/// <summary>
		/// Initializes static class attributes.
		/// </summary>
		public static void Initialize()
		{
			loggingThread = null;
			lastProcess = IntPtr.Zero;

#if NO_APPSLOG
            currentApplicationsLog = null;
#else
            currentApplicationsLog = new ArrayList();

            Logs.CurrentLogDurationChanged += new ElapsedEventHandler(Logs_CurrentLogDurationChanged);
			Logs.LogChanged += new Logs.LogChangeEventHandler(TasksLog_LogChanged);
			Logs.AfterStopLogging += new EventHandler(TasksLog_AfterStopLogging);
#endif
		}

		/// <summary>
		/// Updates the database with the information refered to the managed 
		/// applications  logs.
		/// </summary>
		public static void UpdateCurrentApplicationsLog()
		{
			if (currentApplicationsLog == null)
				return;
			string cmd = "UPDATE ApplicationsLog SET ActiveTime = ?, Caption = ? WHERE (Id = ?)";
            lock (currentApplicationsLog)
            {
                for (int i = 0; i< currentApplicationsLog.Count;i++)
                {
                    ApplicationLog applicationLog = (ApplicationLog) currentApplicationsLog[i];
                    DbHelper.ExecuteNonQuery(cmd,
                                             new string[] {"ActiveTime", "Caption", "Id"},
                                             new object[]
                                                 {
                                                     applicationLog.ActiveTime,
                                                     GetLast255Chars(applicationLog.Caption),
                                                     applicationLog.Id
                                                 });
                } //foreach
            }
		} //UpdateCurrentApplicationsLog

		/// <summary>
		/// GetApplicationsLog retrieves each Application log related 
		/// to a Task selected by its TaskLogId
		/// </summary>
		public static ArrayList GetApplicationsLog(int taskLogId)
		{
			ArrayList resultsHT =
				DbHelper.ExecuteGetRows(
				"SELECT Id, Name, Caption, ApplicationFullPath, ActiveTime FROM ApplicationsLog WHERE TaskLogId = " +
				taskLogId);
			ArrayList results = new ArrayList();
			foreach (IDictionary dictionary in resultsHT)
			{
				ApplicationLog applicationLog = new ApplicationLog();
				applicationLog.Id = (int) dictionary["Id"];
				applicationLog.Name = (string) dictionary["Name"];
                if (dictionary["Caption"]==DBNull.Value)
                    applicationLog.Caption = String.Empty;
                else
                    applicationLog.Caption = (string)dictionary["Caption"];
				applicationLog.ApplicationFullPath = (string) dictionary["ApplicationFullPath"];
				applicationLog.ActiveTime = (int) dictionary["ActiveTime"];
				applicationLog.TaskLogId = taskLogId;
				results.Add(applicationLog);
			} //foreach
			return results;
		} //GetApplicationsLog

        public static void DeleteApplicationLog(int applicationLogId)
        {
            IDictionary result;
            result = DbHelper.ExecuteGetFirstRow("SELECT TaskLogId FROM ApplicationsLog WHERE Id = " + applicationLogId);
            if (result == null)
                throw new ApplicationException("The application log entry doesn't exists.");
            int logId = Convert.ToInt32(result["TaskLogId"]);
            //if (logId == Logs.CurrentLog.Id)
            //    throw new ApplicationException("Applications log entries from current task can't be deleted.");

            for (int i = 0; i < currentApplicationsLog.Count;i++ )
            {
                ApplicationLog applicationLog = (ApplicationLog) currentApplicationsLog[i];
                if(applicationLog.Id == applicationLogId)
                {
                    currentApplicationsLog.RemoveAt(i);
                    break;
                }
            }

            DbHelper.ExecuteNonQuery("DELETE FROM ApplicationsLog WHERE Id = " + applicationLogId);

            if (ApplicationsLogChanged != null)
            {
                ApplicationLog alog = new ApplicationLog();
                alog.Id = applicationLogId;
                alog.TaskLogId = logId;
                ApplicationsLogChanged(new ApplicationLogChangeEventArgs(alog, DataRowAction.Delete));
            }
        }

	    #endregion

		#region Private Methods

		/// <summary>
		/// Updates Current process (Currently Active) Information
		/// If the process is the same as the last Update, increments time stamp.
		/// </summary>
		private static void UpdateActiveProcess()
		{
			IntPtr processId = IntPtr.Zero;
			try
			{
				DateTime initCallTime = DateTime.Now;
				
				//currentProcess = GetCurrentHWnd();
				IntPtr hwnd = GetCurrentHWnd();
				if (hwnd == IntPtr.Zero)
				{
					return;
				}
				
				GetWindowThreadProcessId(hwnd, out processId);
				int processIdInt32 = processId.ToInt32();
				// This is a PTM.Framework.Infos.ApplicationLog
				ApplicationLog applicationLog = FindCurrentApplication(processIdInt32);
				if (applicationLog == null)
				{
					// First time this application is detected
					applicationLog = new ApplicationLog();
					applicationLog.ProcessId = processIdInt32;
						
					using(Process proc = Process.GetProcessById(processIdInt32))
					{
						applicationLog.ApplicationFullPath = proc.MainModule.FileName;
						applicationLog.Name = proc.MainModule.ModuleName;
					}
					applicationLog.Caption = GetText(hwnd);
					applicationLog.ActiveTime = Convert.ToInt32((DateTime.Now - initCallTime).TotalSeconds);
					applicationLog.LastUpdateTime = DateTime.Now;
					applicationLog.TaskLogId = Logs.CurrentLog.Id;
					InsertApplicationLog(applicationLog);
					
					if (ApplicationsLogChanged != null)
					{
						ApplicationsLogChanged(
							new ApplicationLogChangeEventArgs(applicationLog,
							DataRowAction.Add));
					} //if
				}
				else
				{
					applicationLog.Caption = GetText(hwnd);
					if (processId == lastProcess)
					{
						applicationLog.ActiveTime = Convert.ToInt32(
							new TimeSpan(0, 0, applicationLog.ActiveTime).Add(DateTime.Now - applicationLog.LastUpdateTime).TotalSeconds);
					}
					else
					{
						applicationLog.ActiveTime = Convert.ToInt32(
							new TimeSpan(0, 0, applicationLog.ActiveTime).Add(DateTime.Now - lastCallTime).TotalSeconds);
					} //if-else
					applicationLog.LastUpdateTime = DateTime.Now;
					if (ApplicationsLogChanged != null)
					{
						ApplicationsLogChanged(
							new ApplicationLogChangeEventArgs(applicationLog,
							DataRowAction.Change));
					} //if
				} //if-else
				return;

			}
            catch (Win32Exception w32Ex) //Bug 1884407
		    {
		        Logger.WriteException(w32Ex);
		    }
            finally
			{
				lastProcess = processId;
				lastCallTime = DateTime.Now;
			} //try-catch-finally
		} //UpdateActiveProcess

		/// <summary>
		/// Inserts Database information with an application log
		/// </summary>
		private static void InsertApplicationLog(ApplicationLog applicationLog)
		{
			string cmd =
				"INSERT INTO ApplicationsLog(ActiveTime, ApplicationFullPath, Name, TaskLogId, Caption) VALUES (?, ?, ?, ?, ?)";
		    applicationLog.Id =
		        DbHelper.ExecuteInsert(cmd,
		                               new string[]
		                                   {
		                                       "ActiveTime", "ApplicationFullPath", "Name", "TaskLogId", "Caption"
		                                   },
		                               new object[]
		                                   {
	                                       applicationLog.ActiveTime,
                                           GetLast255Chars(applicationLog.ApplicationFullPath),
											applicationLog.Name,
											applicationLog.TaskLogId, 
                                            GetLast255Chars(applicationLog.Caption)
										});
			currentApplicationsLog.Add(applicationLog);
		} //InsertApplicationLog

        private static string GetLast255Chars(string text)
        {
            if (text == null) return null;

            return text.Substring(
                Math.Max(0, text.Length - 255),
                Math.Min(text.Length, 255));
        }

		/// <summary>
		/// Search for an Application Log by processId inside inner Array of
		/// Applications log.
		/// </summary>
		private static ApplicationLog FindCurrentApplication(int processId)
		{
			ApplicationLog result = null;
			foreach (ApplicationLog application in currentApplicationsLog)
			{
				if (application.ProcessId == processId)
				{
					result = application;
				} //if
			} //foreach
			return result;
		} //FindCurrentApplication

		[DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);
		[DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		static extern int GetWindowTextLength(IntPtr hWnd);
		[DllImport("user32.dll")] 
		private static extern IntPtr GetWindowThreadProcessId(IntPtr hwnd, out IntPtr lpdwProcessId);
        
		private static string GetText(IntPtr hWnd)
		{
			// Allocate correct string length first
			int length       = GetWindowTextLength(hWnd);
			StringBuilder sb = new StringBuilder(length + 1);
			GetWindowText(hWnd, sb, sb.Capacity);
			return sb.ToString();
		}


		/// <summary>
		/// Retrieves current Process Information
		/// </summary>
		private static IntPtr GetCurrentHWnd()
		{
			IntPtr hwnd = ViewHelper.GetForegroundWindow();

			if (hwnd != IntPtr.Zero)
			{
				IntPtr pwnd;
				do
				{
					pwnd = ViewHelper.GetParent(hwnd);
					if(pwnd != IntPtr.Zero)
						hwnd = pwnd;
				} while (pwnd != IntPtr.Zero);

				return hwnd;
			} //if
			return IntPtr.Zero;
		} //GetCurrentHWnd()

		/// <summary>
		/// Change log Event for Tasks
		/// </summary>
		private static void TasksLog_LogChanged(Logs.LogChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Add)
			{
				JoinLoggingThread();
				if (currentApplicationsLog.Count > 0)
				{
					UpdateCurrentApplicationsLog();
					currentApplicationsLog = new ArrayList();
				} //if-else
				InvokeLoggingThread();
				//UpdateActiveProcess();
			} //if
		} //TasksLog_LogChanged

        static void Logs_CurrentLogDurationChanged(object sender, ElapsedEventArgs e)
        {
            InvokeLoggingThread();
        }

		/// <summary>
		/// Join Loggin Thread (5000)
		/// </summary>
		private static void JoinLoggingThread()
		{
			if (loggingThread != null && loggingThread.IsAlive)
			{
                try
                {
                    loggingThread.Join(100);
                }
				catch(ThreadStateException){} //catch exception
			} //if-else
		} //JoinLoggingThread

		/// <summary>
		/// If The loggin thread is not initialized, initializes it.
		/// </summary>
		private static void InvokeLoggingThread()
		{
			if ((loggingThread != null) && loggingThread.IsAlive)
			{
				return;
			} //if
			loggingThread = new Thread(new ThreadStart(UpdateActiveProcess));
			loggingThread.Priority = ThreadPriority.Normal;
			loggingThread.Start();
		} //InvokeLoggingThread


		/// <summary>
		/// Task log after Stop logging Eveng
		/// </summary>
		private static void TasksLog_AfterStopLogging(object sender, EventArgs e)
		{
			JoinLoggingThread();
			UpdateCurrentApplicationsLog();
		} //TasksLog_AfterStopLogging

		#endregion

		#region Events
		/// <summary>
		/// Application Log Change Event Handler static attribute
		/// </summary>
		public static event ApplicationLogChangeEventHandler ApplicationsLogChanged;

		/// <summary>
		/// Application Log Change Event Handler delegate
		/// </summary>
		public delegate void ApplicationLogChangeEventHandler(ApplicationLogChangeEventArgs e);

		/// <summary>
		/// Application Log Change Event Argumments
		/// </summary>
		public class ApplicationLogChangeEventArgs : EventArgs
		{
			private ApplicationLog applicationLog;
			private DataRowAction action;

			public ApplicationLogChangeEventArgs(ApplicationLog applicationLog, DataRowAction action)
			{
				this.applicationLog = applicationLog;
				this.action = action;
			} //ApplicationLogChangeEventArgs

			public ApplicationLog ApplicationLog
			{
				get { return applicationLog; }
			} //ApplicationLog

			public DataRowAction Action
			{
				get { return action; }
			} //Action
		} //ApplicationLogChangeEventArgs

		#endregion

	} //ApplicationsLog
} //namespace