using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using PTM.Data;
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
		private static Process currentProcess;
		private static Process lastProcess;
		private static Timer applicationsTimer;
		private static Thread loggingThread;

		#region Properties
		#endregion

		#region Public Methods
		internal static void Initialize(PTMDataset.ApplicationsLogDataTable dataTable, DbDataAdapter adapter)
		{
			processes = Process.GetProcesses();
			loggingThread = null;
			currentProcess = null;
			lastProcess = null;
			applicationsTimer = new Timer(1000);
			dataAdapter = adapter;
			applicationsLogTable = dataTable;
			applicationsTimer.Elapsed+=new ElapsedEventHandler(ApplicationsTimer_Elapsed);
			applicationsLogTable.ApplicationsLogRowChanged+=new PTMDataset.ApplicationsLogRowChangeEventHandler(applicationsLogTable_ApplicationsLogRowChanged);
			Logs.LogChanged+=new PTM.Business.Logs.LogChangeEventHandler(TasksLog_LogChanged);
			Logs.AfterStartLogging+=new EventHandler(TasksLog_AfterStartLogging);
			Logs.AfterStopLogging+=new EventHandler(TasksLog_AfterStopLogging);			
		}

		#endregion

		#region Private Methods
		private static void UpdateActiveProcess()
		{
			try
			{
				applicationsTimer.Stop();
				currentProcess = null;

				if (processes == null)
					processes = Process.GetProcesses();

				IntPtr hwnd = ViewHelper.GetForegroundWindow();
				//TODO: Hacer que no tome en cuenta el taskbar
				if (hwnd.ToInt32() == 0)
					return;
				
				if (!ViewHelper.IsWindow(hwnd))
					return;

				IntPtr pwnd = ViewHelper.GetParent(hwnd);
				if (pwnd.ToInt32() == 0)
					pwnd = hwnd;

				IntPtr processId = ViewHelper.GetWindowThreadProcessId(pwnd, IntPtr.Zero);

				foreach (Process process in processes)
				{
					if (process.MainWindowHandle == pwnd || HasThreadId(process, processId))
					{
						currentProcess = process;
						currentProcess.Refresh();
						break;
					}
				}

				if (currentProcess == null)
				{
					processes = Process.GetProcesses();
					lastProcess = currentProcess;
					return;
				}
				else
				{
					PTMDataset.ApplicationsLogRow row;
					string select = applicationsLogTable.ProcessIdColumn.ColumnName + "=" + currentProcess.Id;
					select += "AND " + applicationsLogTable.TaskLogIdColumn.ColumnName + "=" + Logs.CurrentLog.Id;
					PTMDataset.ApplicationsLogRow[] rows = (PTMDataset.ApplicationsLogRow[]) applicationsLogTable.Select(select);
					if (rows.Length == 0)
						row = applicationsLogTable.NewApplicationsLogRow();
					else
						row = rows[0];

					row.TaskLogId = Logs.CurrentLog.Id;
					row.ProcessId = currentProcess.Id;
					row.Name = currentProcess.MainModule.ModuleName;
					row.UserProcessorTime = Convert.ToInt32(currentProcess.UserProcessorTime.TotalSeconds);
					row.ApplicationFullPath = currentProcess.MainModule.FileName;
					row.Caption = currentProcess.MainWindowTitle;

					if (row.RowState == DataRowState.Detached)
					{
						row.ActiveTime = 0;
					}
					else if (lastProcess != null && lastProcess.Id == currentProcess.Id)
					{
						row.ActiveTime = Convert.ToInt32(new TimeSpan(0, 0, row.ActiveTime).Add(DateTime.Now - row.LastUpdateTime).TotalSeconds);
					}

					row.LastUpdateTime = DateTime.Now;

					if (row.RowState == DataRowState.Detached)
						applicationsLogTable.AddApplicationsLogRow(row);

					lastProcess = currentProcess;
					return;
				}
			}
			catch
			{
				lastProcess = null;
				return;
			}
			finally
			{
				applicationsTimer.Start();
			}
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

		private static void SaveApplicationsLog()
		{
			dataAdapter.Update(applicationsLogTable);
			applicationsLogTable.AcceptChanges();
		}
		
		private static void TasksLog_LogChanged(PTM.Business.Logs.LogChangeEventArgs e)
		{
			if(e.Action == DataRowAction.Add)
			{
				SaveApplicationsLog();
				applicationsLogTable.Clear();
			}
		}

		#endregion

		#region Events
		public static  event PTMDataset.ApplicationsLogRowChangeEventHandler ApplicationsLogRowChanged;
		private static void ApplicationsTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			InvokeLoggingThread();
		}

		private static void InvokeLoggingThread()
		{
			if(loggingThread!=null && loggingThread.IsAlive)
				return;
			loggingThread = new Thread(new ThreadStart(UpdateActiveProcess));
			loggingThread.Priority = ThreadPriority.BelowNormal;
			loggingThread.Start();
		}

		private static void applicationsLogTable_ApplicationsLogRowChanged(object sender, PTMDataset.ApplicationsLogRowChangeEvent e)
		{
//			if(e.Action == DataRowAction.Add)
//			{
//				SaveApplicationsLogRow(e.Row);
//				e.Row.AcceptChanges();
//			}
			if(ApplicationsLogRowChanged!=null)
				ApplicationsLogRowChanged(sender, e);
		}

		private static void TasksLog_AfterStartLogging(object sender, EventArgs e)
		{
			InvokeLoggingThread();
			applicationsTimer.Start();
		}

		private static void TasksLog_AfterStopLogging(object sender, EventArgs e)
		{
			applicationsTimer.Stop();
			SaveApplicationsLog();
		}
		#endregion


	}
}