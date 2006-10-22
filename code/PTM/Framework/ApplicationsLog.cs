using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using PTM.Data;
using PTM.Framework.Infos;
using PTM.View;
using Timer = System.Timers.Timer;

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

		#region Attributes
		/// <summary>
		/// Inner list of currently managed applications log.
		/// </summary>
		private static ArrayList currentApplicationsLog;
		
		/// <summary>
		/// Reference to the last evaluated process .
		/// </summary>
		private static Process lastProcess;

		/// <summary>
		/// Internal application timmer.
		/// </summary>
		private static System.Timers.Timer applicationsTimer;

		/// <summary>
		/// TimeStamp of the last logged time .
		/// </summary>
		private static DateTime lastCallTime;
		
		/// <summary>
		/// Internal Thread that controls the log.
		/// </summary>
		private static Thread loggingThread;
		#endregion

		#region Properties
		#endregion

		#region Public Methods
		/// <summary>
		/// Initializes static class attributes.
		/// </summary>
		public static void Initialize()
		{
			loggingThread = null;
			lastProcess = null;
			applicationsTimer = new Timer(1000);
			
			//Event Handling Initialization
			applicationsTimer.Elapsed += new ElapsedEventHandler(ApplicationsTimer_Elapsed);			
			Logs.LogChanged += new Logs.LogChangeEventHandler(TasksLog_LogChanged);
			Logs.AfterStopLogging += new EventHandler(TasksLog_AfterStopLogging);
		}//Initialize

		/// <summary>
		/// Updates the database with the information refered to the managed 
		/// applications  logs.
		/// </summary>
		public static void UpdateCurrentApplicationsLog()
		{
			if(currentApplicationsLog==null)
				return;
			string cmd = "UPDATE ApplicationsLog SET ActiveTime = ?, Caption = ? WHERE (Id = ?)";
			foreach (ApplicationLog applicationLog in currentApplicationsLog)
			{
				DbHelper.ExecuteNonQuery(cmd,
					new string[]
						{"ActiveTime", "Caption", "Id"},
					new object[]
						{
								applicationLog.ActiveTime, 
							applicationLog.Caption.Substring( 0, 
							Math.Min( applicationLog.Caption.Length, 120 ) ),
							applicationLog.Id
						});
			}//foreach
		}//UpdateCurrentApplicationsLog
		#endregion

		#region Private Methods
		
		/// <summary>
		/// Updates Current process (Currently Active) Information
		/// If the process is the same as the last Update, increments time stamp.
		/// </summary>
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
					// This is a PTM.Framework.Infos.ApplicationLog
					ApplicationLog applicationLog = FindCurrentApplication( currentProcess.Id );
					if ( applicationLog == null )
					{	// First time this application is detected
						ApplicationLog appLogRow = new ApplicationLog();
						appLogRow.TaskLogId = Logs.CurrentLog.Id;
						appLogRow.ProcessId = currentProcess.Id;
						try
						{
							appLogRow.Name = currentProcess.MainModule.ModuleName;
							appLogRow.ApplicationFullPath = currentProcess.MainModule.FileName;
						}
						catch(Win32Exception)
						{
							return;
						}//try-catch
						appLogRow.Caption = currentProcess.MainWindowTitle;
						appLogRow.ActiveTime = Convert.ToInt32((DateTime.Now-initCallTime).TotalSeconds);
						appLogRow.LastUpdateTime = DateTime.Now;
						InsertApplicationLog( appLogRow );
						currentApplicationsLog.Add( appLogRow );
						if( ApplicationsLogChanged != null )
						{
							ApplicationsLogChanged( 
								new ApplicationLogChangeEventArgs( applicationLog, 
								DataRowAction.Add ));
						}//if
					}
					else
					{
						applicationLog.Caption = currentProcess.MainWindowTitle;
						if( currentProcess == lastProcess )
						{
							applicationLog.ActiveTime = Convert.ToInt32( 
								new TimeSpan( 0, 0, applicationLog.ActiveTime ).Add( DateTime.Now - applicationLog.LastUpdateTime ).TotalSeconds );
						}
						else
						{
							applicationLog.ActiveTime = Convert.ToInt32( 
								new TimeSpan( 0, 0, applicationLog.ActiveTime ).Add( DateTime.Now - lastCallTime ).TotalSeconds);
						}//if-else
						applicationLog.LastUpdateTime = DateTime.Now;
						if( ApplicationsLogChanged != null )
						{
							ApplicationsLogChanged( 
								new ApplicationLogChangeEventArgs(applicationLog, 
								DataRowAction.Change ));
						}//if
					}//if-else
					return;
				}//if-else
			}
			finally
			{
				lastProcess = currentProcess;
				lastCallTime = DateTime.Now;
				applicationsTimer.Start();
			}//try-catch-finally
		}//UpdateActiveProcess

		/// <summary>
		/// Inserts Database information with an application log
		/// </summary>
		private static void InsertApplicationLog(ApplicationLog applicationLog)
		{
			string cmd =
				"INSERT INTO ApplicationsLog(ActiveTime, ApplicationFullPath, Caption, Name, ProcessId, TaskLogId) VALUES (?, ?, ?, ?, ?, ?)";
			applicationLog.Id =
				DbHelper.ExecuteInsert(cmd,
				new string[]
					{
						"ActiveTime", "ApplicationFullPath", "Caption", "Name",
						"ProcessId", "TaskLogId"
					},
				new object[]
					{
						applicationLog.ActiveTime, applicationLog.ApplicationFullPath.Substring(Math.Max(0,applicationLog.ApplicationFullPath.Length-255),Math.Min(applicationLog.ApplicationFullPath.Length, 255)),
						applicationLog.Caption.Substring(0,Math.Min(applicationLog.Caption.Length, 120)), applicationLog.Name,
						applicationLog.ProcessId, applicationLog.TaskLogId
					});
		}//InsertApplicationLog

		/// <summary>
		/// Search for an Application Log by processId inside inner Array of
		/// Applications log.
		/// </summary>
		private static ApplicationLog FindCurrentApplication(int processId)
		{
			ApplicationLog result = null;
			foreach (ApplicationLog application in currentApplicationsLog)
			{
				if(application.ProcessId == processId)
				{
					result = application;
				}//if
			}//foreach
			return result;
		}//FindCurrentApplication

		/// <summary>
		/// Retrieves current Process Information
		/// </summary>
		private static Process GetCurrentProcess()
		{
			//Process currentProcess=null;
			////Solution with Process.GetCurrentProcess()
			//			currentProcess = Process.GetCurrentProcess();
			//			IntPtr phnd = ViewHelper.GetParent(currentProcess.Handle);
			//			if (!ViewHelper.IsWindow(phnd))
			//				return null;
			//			return currentProcess;
			Process result = null;

			IntPtr hwnd = ViewHelper.GetForegroundWindow( );
				
			if (hwnd.ToInt32() != 0)
			{
				IntPtr pwnd = ViewHelper.GetParent( hwnd );
				if ( pwnd.ToInt32() == 0 )
				{
					pwnd = hwnd;
				}//if
				
				if ( ViewHelper.IsWindow( pwnd ) )
				{
					//IntPtr processId = ViewHelper.GetWindowThreadProcessId(pwnd, IntPtr.Zero);
					IntPtr pid;
					ViewHelper.GetWindowThreadProcessId( pwnd, out pid );
					
					result = Process.GetProcessById( pid.ToInt32() );
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
				}//if isWnd
			}//if
			return result;
		}//GetCurrentProcess()

		//		private static bool HasThreadId(Process process, IntPtr processId)
		//		{
		//			foreach (ProcessThread  pt in process.Threads)
		//			{
		//				if (pt.Id == processId.ToInt32())
		//					return true;
		//			}
		//			return false;
		//		}

		/// <summary>
		/// Change log Event for Tasks
		/// </summary>
		private static void TasksLog_LogChanged( Logs.LogChangeEventArgs e )
		{
			if( e.Action == DataRowAction.Add )
			{
				applicationsTimer.Stop();
				JoinLoggingThread();
				if( currentApplicationsLog == null )
				{
					currentApplicationsLog = new ArrayList();
				}
				else if( currentApplicationsLog.Count > 0 )
				{
					UpdateCurrentApplicationsLog();
					currentApplicationsLog = new ArrayList();
				}//if-else
				InvokeLoggingThread();
				//UpdateActiveProcess();
			}//if
		}//TasksLog_LogChanged

		/// <summary>
		/// Join Loggin Thread (5000)
		/// </summary>
		private static void JoinLoggingThread()
		{
			if( loggingThread != null && loggingThread.IsAlive )
			{
				loggingThread.Join(5000);
			}//if-else
		}//JoinLoggingThread


		/// <summary>
		/// Application Timer Elapsed Event
		/// </summary>
		private static void ApplicationsTimer_Elapsed( object sender, ElapsedEventArgs e )
		{
			//UpdateActiveProcess();
			InvokeLoggingThread();
		}//ApplicationsTimer_Elapsed
		
		
		/// <summary>
		/// If The loggin thread is not initialized, initializes it.
		/// </summary>
		private static void InvokeLoggingThread()
		{
			if( ( loggingThread != null ) && loggingThread.IsAlive )
			{
				return;
			}//if
			loggingThread = new Thread( new ThreadStart( UpdateActiveProcess ) );
			loggingThread.Priority = ThreadPriority.Normal;
			loggingThread.Start();
		}//InvokeLoggingThread

		/// <summary>
		/// Task log after Stop logging Eveng
		/// </summary>
		private static void TasksLog_AfterStopLogging(object sender, EventArgs e)
		{
			applicationsTimer.Stop();
			JoinLoggingThread();
			UpdateCurrentApplicationsLog();
		}//TasksLog_AfterStopLogging

		#endregion

		#region Events

		/// <summary>
		/// Application Log Change Event Handler static attribute
		/// </summary>
		public static  event ApplicationLogChangeEventHandler ApplicationsLogChanged;

		
		/// <summary>
		/// Application Log Change Event Handler delegate
		/// </summary>
		public delegate void ApplicationLogChangeEventHandler( ApplicationLogChangeEventArgs e );

		/// <summary>
		/// Application Log Change Event Argumments
		/// </summary>
		public class ApplicationLogChangeEventArgs : EventArgs
		{
			private ApplicationLog applicationLog;
			private DataRowAction action;
			public ApplicationLogChangeEventArgs( ApplicationLog applicationLog, DataRowAction action )
			{
				this.applicationLog = applicationLog;
				this.action = action;
			}//ApplicationLogChangeEventArgs

			public ApplicationLog ApplicationLog
			{
				get { return applicationLog; }
			}//ApplicationLog
			
			public DataRowAction Action
			{
				get { return action; }
			}//Action
		}//ApplicationLogChangeEventArgs

		#endregion

		/// <summary>
		/// GetApplicationsLog retrieves each Application log related 
		/// to a Task selected by its TaskLogId
		/// </summary>
		public static ArrayList GetApplicationsLog(int taskLogId)
		{
			ArrayList resultsHT = DbHelper.ExecuteGetRows("SELECT Id, ProcessId, Name, Caption, ApplicationFullPath, ActiveTime FROM ApplicationsLog WHERE TaskLogId = " + taskLogId.ToString());
			ArrayList results = new ArrayList();
			foreach (Hashtable hashtable in resultsHT)
			{
				ApplicationLog applicationLog = new ApplicationLog();
				applicationLog.Id = (int) hashtable["Id"];
				applicationLog.ProcessId = (int) hashtable["ProcessId"];
				applicationLog.Name = (string) hashtable["Name"];
				applicationLog.Caption = (string) hashtable["Caption"];
				applicationLog.ApplicationFullPath = (string) hashtable["ApplicationFullPath"];
				applicationLog.ActiveTime = (int) hashtable["ActiveTime"];
				applicationLog.TaskLogId = taskLogId;
				results.Add(applicationLog);
			}//foreach
			return results;
		}//GetApplicationsLog
	}//ApplicationsLog
}//namespace
