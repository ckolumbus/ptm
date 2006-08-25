using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using PTM.Data;
using PTM.Infos;
using PTM.View;
using Timer = System.Timers.Timer;

namespace PTM.Business
{
	internal sealed class ApplicationsLog
	{
		private ApplicationsLog()
		{
		}

	
		//private static DbDataAdapter dataAdapter;
		//private static Process[] processes;
		//private static PTMDataset.ApplicationsLogDataTable applicationsLogTable;
		private static ArrayList currentApplicationsLog;
		private static Process lastProcess;
		private static System.Timers.Timer applicationsTimer;
		private static DateTime lastCallTime;
		private static Thread loggingThread;

		#region Properties
		#endregion

		#region Public Methods
		public static void Initialize()
		{
			//processes = Process.GetProcesses();
			loggingThread = null;
			lastProcess = null;
			applicationsTimer = new Timer(1000);
			//dataAdapter = adapter;
			//applicationsLogTable = dataTable;
			
			applicationsTimer.Elapsed+=new ElapsedEventHandler(ApplicationsTimer_Elapsed);			
			Logs.LogChanged+=new Logs.LogChangeEventHandler(TasksLog_LogChanged);
			//Logs.AfterStartLogging+=new EventHandler(TasksLog_AfterStartLogging);
			Logs.AfterStopLogging+=new EventHandler(TasksLog_AfterStopLogging);
		}

//		public static void SaveApplicationsLog()
//		{
//			dataAdapter.Update(applicationsLogTable);
//			applicationsLogTable.AcceptChanges();
//		}
		public static void UpdateCurrentApplicationsLog()
		{
			string cmd = "UPDATE ApplicationsLog SET ActiveTime = ?, Caption = ?, LastUpdateTime = ?, UserProcessorTime = ? WHERE (Id = ?)";
			foreach (ApplicationLog applicationLog in currentApplicationsLog)
			{
				DataAdapterManager.ExecuteNonQuery(cmd,
					new string[]
																{"ActiveTime", "Caption", "LastUpdateTime", "UserProcessorTime", "Id"},
					new object[]
																{
																	applicationLog.ActiveTime, applicationLog.Caption.Substring(0,Math.Min(applicationLog.Caption.Length, 120)),
																	applicationLog.LastUpdateTime, applicationLog.UserProcessorTime,
																	applicationLog.Id
																});
			}
		}
		#endregion

		#region Private Methods
		private static void UpdateActiveProcess()
		{
			Process currentProcess=null;
			try
			{
				DateTime initCallTime = DateTime.Now;
				applicationsTimer.Stop();
				
				currentProcess = GetCurrentProcess();

				if (currentProcess == null)
				{
					return;
				}
				else
				{
					ApplicationLog applicationLog = FindCurrentApplication(currentProcess.Id);
//					string select = applicationsLogTable.ProcessIdColumn.ColumnName + "=" + currentProcess.Id;
//					select += "AND " + applicationsLogTable.TaskLogIdColumn.ColumnName + "=" + Logs.CurrentLog.Id;
//					PTMDataset.ApplicationsLogRow[] rows = (PTMDataset.ApplicationsLogRow[]) applicationsLogTable.Select(select);
					if (applicationLog == null)
					{
						ApplicationLog row = new ApplicationLog();
						row.TaskLogId = Logs.CurrentLog.Id;
						row.ProcessId = currentProcess.Id;
						try
						{
							row.Name = currentProcess.MainModule.ModuleName;
							row.ApplicationFullPath = currentProcess.MainModule.FileName;
						}
						catch(Win32Exception)
						{
							return;
						}
						row.UserProcessorTime = Convert.ToInt32(currentProcess.UserProcessorTime.TotalSeconds);
						row.Caption = currentProcess.MainWindowTitle;
						row.ActiveTime = Convert.ToInt32((DateTime.Now-initCallTime).TotalSeconds);
						row.LastUpdateTime = DateTime.Now;
						InsertApplicationLog(row);
						currentApplicationsLog.Add(row);
						if(ApplicationsLogChanged!=null)
							ApplicationsLogChanged(new ApplicationLogChangeEventArgs(applicationLog, DataRowAction.Add));
					}
					else
					{
						applicationLog.Caption = currentProcess.MainWindowTitle;
						applicationLog.UserProcessorTime = Convert.ToInt32(currentProcess.UserProcessorTime.TotalSeconds);
						if(currentProcess==lastProcess)
							applicationLog.ActiveTime = Convert.ToInt32(new TimeSpan(0, 0, applicationLog.ActiveTime).Add(DateTime.Now - applicationLog.LastUpdateTime).TotalSeconds);
						else
							applicationLog.ActiveTime = Convert.ToInt32(new TimeSpan(0, 0, applicationLog.ActiveTime).Add(DateTime.Now - lastCallTime).TotalSeconds);
						applicationLog.LastUpdateTime = DateTime.Now;
						if(ApplicationsLogChanged!=null)
							ApplicationsLogChanged(new ApplicationLogChangeEventArgs(applicationLog, DataRowAction.Change));
					}
					
					return;
				}
			}
			finally
			{
				lastProcess = currentProcess;
				lastCallTime = DateTime.Now;
				applicationsTimer.Start();
			}
		}

		private static void InsertApplicationLog(ApplicationLog applicationLog)
		{
			string cmd =
				"INSERT INTO ApplicationsLog(ActiveTime, ApplicationFullPath, Caption, LastUpdateTime, Name, ProcessId, TaskLogId, UserProcessorTime) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
			applicationLog.Id =
				DataAdapterManager.ExecuteInsert(cmd,
				                                 new string[]
				                                 	{
				                                 		"ActiveTime", "ApplicationFullPath", "Caption", "LastUpdateTime", "Name",
				                                 		"ProcessId", "TaskLogId", "UserProcessorTime"
				                                 	},
				                                 new object[]
				                                 	{
				                                 		applicationLog.ActiveTime, applicationLog.ApplicationFullPath.Substring(Math.Max(0,applicationLog.ApplicationFullPath.Length-255),Math.Min(applicationLog.ApplicationFullPath.Length, 255)),
				                                 		applicationLog.Caption.Substring(0,Math.Min(applicationLog.Caption.Length, 120)), applicationLog.LastUpdateTime, applicationLog.Name,
				                                 		applicationLog.ProcessId, applicationLog.TaskLogId,
				                                 		applicationLog.UserProcessorTime
				                                 	});
		}

		private static ApplicationLog FindCurrentApplication(int processId)
		{
			foreach (ApplicationLog application in currentApplicationsLog)
			{
				if(application.ProcessId == processId)
					return application;
			}
			return null;
		}

		private static Process GetCurrentProcess()
		{
			//Process currentProcess=null;
			////Solution with Process.GetCurrentProcess()
//			currentProcess = Process.GetCurrentProcess();
//			IntPtr phnd = ViewHelper.GetParent(currentProcess.Handle);
//			if (!ViewHelper.IsWindow(phnd))
//				return null;
//			return currentProcess;
			
			IntPtr hwnd = ViewHelper.GetForegroundWindow();
				
			if (hwnd.ToInt32() == 0)
				return null;
				
			IntPtr pwnd = ViewHelper.GetParent(hwnd);
			if (pwnd.ToInt32() == 0)
				pwnd = hwnd;
			
			if (!ViewHelper.IsWindow(pwnd))
				return null;

			//IntPtr processId = ViewHelper.GetWindowThreadProcessId(pwnd, IntPtr.Zero);
			IntPtr pid;
			ViewHelper.GetWindowThreadProcessId(pwnd, out pid);
			
			return Process.GetProcessById(pid.ToInt32());
			/*
			foreach (Process process in processes)
			{
				if(!process.HasExited)
				if (process.MainWindowHandle == pwnd || HasThreadId(process, processId))
				{
					currentProcess = process;
					currentProcess.Refresh();
					break;
				}
			}
			
			if(currentProcess == null)
			{
				processes = Process.GetProcesses();
				foreach (Process process in processes)
				{
					if(!process.HasExited)
					if (process.MainWindowHandle == pwnd || HasThreadId(process, processId))
					{
						currentProcess = process;
						currentProcess.Refresh();
						break;
					}
				}
			}
			return currentProcess;
			*/
		}

//		private static bool HasThreadId(Process process, IntPtr processId)
//		{
//			foreach (ProcessThread  pt in process.Threads)
//			{
//				if (pt.Id == processId.ToInt32())
//					return true;
//			}
//			return false;
//		}

		
		private static void TasksLog_LogChanged(Logs.LogChangeEventArgs e)
		{
			if(e.Action == DataRowAction.Add)
			{
				applicationsTimer.Stop();
				JoinLoggingThread();
				if(currentApplicationsLog==null)
					currentApplicationsLog = new ArrayList();
				else if(currentApplicationsLog.Count>0)
				{
					UpdateCurrentApplicationsLog();
					currentApplicationsLog = new ArrayList();
				}
				InvokeLoggingThread();
				//UpdateActiveProcess();
			}
		}

		private static void JoinLoggingThread()
		{
			if(loggingThread!=null && loggingThread.IsAlive)
				loggingThread.Join(5000);
		}


		private static void ApplicationsTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			//UpdateActiveProcess();
			InvokeLoggingThread();
		}
		
		
		private static void InvokeLoggingThread()
		{
			if(loggingThread!=null && loggingThread.IsAlive)
				return;
			loggingThread = new Thread(new ThreadStart(UpdateActiveProcess));
			loggingThread.Priority = ThreadPriority.Normal;
			loggingThread.Start();
		}

//		private static void TasksLog_AfterStartLogging(object sender, EventArgs e)
//		{
//			//InvokeLoggingThread();
//            UpdateActiveProcess();
//		}

		private static void TasksLog_AfterStopLogging(object sender, EventArgs e)
		{
			applicationsTimer.Stop();
			UpdateCurrentApplicationsLog();
		}

		#endregion

		#region Events
		public static  event ApplicationLogChangeEventHandler ApplicationsLogChanged;
		public delegate void ApplicationLogChangeEventHandler(ApplicationLogChangeEventArgs e);
		public class ApplicationLogChangeEventArgs : EventArgs
		{
			private ApplicationLog applicationLog;
			private DataRowAction action;
			public ApplicationLogChangeEventArgs(ApplicationLog applicationLog, DataRowAction action)
			{
				this.applicationLog = applicationLog;
				this.action = action;
			}
			public ApplicationLog ApplicationLog
			{
				get { return applicationLog; }
			}
			
			public DataRowAction Action
			{
				get { return action; }
			}
		}

//		private static void RaiseApplicationLogChangeEvent(ApplicationLog applicationLog)
//		{
//			if(ApplicationsLogChanged!=null)
//			{
//				ApplicationsLogChanged(new ApplicationLogChangeEventArgs(applicationLog, DataRowAction.Add));
//			}
//		}

		#endregion


	}
}