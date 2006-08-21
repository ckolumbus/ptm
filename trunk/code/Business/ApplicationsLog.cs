using System;
using System.Data;
using System.Data.Common;
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

	
		private static DbDataAdapter dataAdapter;
		private static Process[] processes;
		private static PTMDataset.ApplicationsLogDataTable applicationsLogTable;
		private static Process lastProcess;
		private static Timer applicationsTimer;
		private static DateTime lastCallTime;
		private static Thread loggingThread;

		#region Properties
		#endregion

		#region Public Methods
		public static void Initialize(DbDataAdapter adapter)
		{
			processes = Process.GetProcesses();
			loggingThread = null;
			lastProcess = null;
			applicationsTimer = new Timer(1000);
			dataAdapter = adapter;
			//applicationsLogTable = dataTable;
			
			applicationsTimer.Elapsed+=new ElapsedEventHandler(ApplicationsTimer_Elapsed);			
			Logs.LogChanged+=new PTM.Business.Logs.LogChangeEventHandler(TasksLog_LogChanged);
			//Logs.AfterStartLogging+=new EventHandler(TasksLog_AfterStartLogging);
			Logs.AfterStopLogging+=new EventHandler(TasksLog_AfterStopLogging);
		}

		public static void SaveApplicationsLog()
		{
			dataAdapter.Update(applicationsLogTable);
			applicationsLogTable.AcceptChanges();
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
				
				IntPtr hwnd = ViewHelper.GetForegroundWindow();
				//TODO: Hacer que no tome en cuenta el taskbar
				if (hwnd.ToInt32() == 0)
					return;
				
				if (!ViewHelper.IsWindow(hwnd))
					return;

				IntPtr pwnd = ViewHelper.GetParent(hwnd);
				if (pwnd.ToInt32() == 0)
					pwnd = hwnd;

				
				currentProcess = GetCurrentProcess(pwnd);

				if (currentProcess == null)
				{
					return;
				}
				else
				{
					string select = applicationsLogTable.ProcessIdColumn.ColumnName + "=" + currentProcess.Id;
					select += "AND " + applicationsLogTable.TaskLogIdColumn.ColumnName + "=" + Logs.CurrentLog.Id;
					PTMDataset.ApplicationsLogRow[] rows = (PTMDataset.ApplicationsLogRow[]) applicationsLogTable.Select(select);
					if (rows.Length == 0)
					{
						PTMDataset.ApplicationsLogRow row;
						row = applicationsLogTable.NewApplicationsLogRow();
						row.TaskLogId = Logs.CurrentLog.Id;
						row.ProcessId = currentProcess.Id;
						row.Name = currentProcess.MainModule.ModuleName;
						row.UserProcessorTime = Convert.ToInt32(currentProcess.UserProcessorTime.TotalSeconds);
						row.ApplicationFullPath = currentProcess.MainModule.FileName;
						row.Caption = currentProcess.MainWindowTitle;
						row.Id = -1;
						row.ActiveTime = Convert.ToInt32((DateTime.Now-initCallTime).TotalSeconds);
						row.LastUpdateTime = DateTime.Now;
						applicationsLogTable.AddApplicationsLogRow(row);
						SaveApplicationsLog();
						RaiseApplicationLogChangeEvent(new PTMDataset.ApplicationsLogRowChangeEvent(row, DataRowAction.Add));
					}
					else
					{
						PTMDataset.ApplicationsLogRow row;
						row = rows[0];
						row.Caption = currentProcess.MainWindowTitle;
						row.UserProcessorTime = Convert.ToInt32(currentProcess.UserProcessorTime.TotalSeconds);
						if(currentProcess==lastProcess)
							row.ActiveTime = Convert.ToInt32(new TimeSpan(0, 0, row.ActiveTime).Add(DateTime.Now - row.LastUpdateTime).TotalSeconds);
						else
							row.ActiveTime = Convert.ToInt32(new TimeSpan(0, 0, row.ActiveTime).Add(DateTime.Now - lastCallTime).TotalSeconds);
						row.LastUpdateTime = DateTime.Now;
						RaiseApplicationLogChangeEvent(new PTMDataset.ApplicationsLogRowChangeEvent(row, DataRowAction.Change));
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

		private static Process GetCurrentProcess(IntPtr pwnd)
		{
			IntPtr processId = ViewHelper.GetWindowThreadProcessId(pwnd, IntPtr.Zero);
			Process currentProcess=null;
			foreach (Process process in processes)
			{
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
					if (process.MainWindowHandle == pwnd || HasThreadId(process, processId))
					{
						currentProcess = process;
						currentProcess.Refresh();
						break;
					}
				}
			}
			return currentProcess;
		}

		private static bool HasThreadId(Process process, IntPtr processId)
		{
			foreach (ProcessThread  pt in process.Threads)
			{
				if (pt.Id == processId.ToInt32())
					return true;
			}
			return false;
		}

		
		private static void TasksLog_LogChanged(PTM.Business.Logs.LogChangeEventArgs e)
		{
			if(e.Action == DataRowAction.Add)
			{
				if(applicationsLogTable==null)
					applicationsLogTable = new PTMDataset.ApplicationsLogDataTable();
				else if(applicationsLogTable.Rows.Count>0)
				{
					SaveApplicationsLog();
					applicationsLogTable = new PTMDataset.ApplicationsLogDataTable();
				}
				UpdateActiveProcess();
			}
		}

		private static void ApplicationsTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			UpdateActiveProcess();
			//InvokeLoggingThread();
		}
		private static void InvokeLoggingThread()
		{
			if(loggingThread!=null && loggingThread.IsAlive)
				return;
			loggingThread = new Thread(new ThreadStart(UpdateActiveProcess));
			loggingThread.Priority = ThreadPriority.BelowNormal;
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
			SaveApplicationsLog();
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

		private static void RaiseApplicationLogChangeEvent(PTMDataset.ApplicationsLogRowChangeEvent e)
		{
			if(ApplicationsLogChanged!=null)
			{
				ApplicationLog applicationLog = new ApplicationLog();
				applicationLog.Id = e.Row.Id;
				applicationLog.Name = e.Row.Name;
				applicationLog.ActiveTime = e.Row.ActiveTime;
				applicationLog.ApplicationFullPath = e.Row.ApplicationFullPath;
				applicationLog.Caption = e.Row.Caption;
				applicationLog.TaskLogId = e.Row.TaskLogId;
				ApplicationsLogChanged(new ApplicationLogChangeEventArgs(applicationLog, e.Action));
			}
		}

		#endregion


	}
}