using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using PTM.Business;
using PTM.Data;
using PTM.View;
using PTM.View.Forms;

[assembly:CLSCompliant(false)]
namespace PTM
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public sealed class MainClass
	{

		internal static MemoryMappedFile sharedMemory;
		internal static bool systemShutdown = false;

		private MainClass()
		{
		}

		[STAThread]
		private static void Main()
		{
			//Launch();
			RunSingleInstance();
		}

		private static void RunSingleInstance()
		{
// Used to check if we can create a new mutex
			bool newMutexCreated = false;
			// The name of the mutex is to be prefixed with Local\ to make sure that its is created in the per-session namespace, not in the global namespace.
			string mutexName = "Local\\" + Assembly.GetExecutingAssembly().GetName().Name;

			Mutex mutex = null;
			try
			{
				// Create a new mutex object with a unique name
				mutex = new Mutex(false, mutexName, out newMutexCreated);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Exiting...", "Exception thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}

			// When the mutex is created for the first time we run the program since it is the first instance.
			if (newMutexCreated)
			{
				//Create the Shared Memory to store the window handle. This memory is shared between processes
				lock (typeof (MainForm))
				{
					MainClass.sharedMemory = MemoryMappedFile.CreateMMF("Local\\sharedMemoryITimeTracker", MemoryMappedFile.FileAccess.ReadWrite, 8);
				}
				Launch();
				
			}

			else // If the mutex already exists, no need to launch a new instance of the program because a previous instance is running .
			{
				try
				{
					// Get the Program's main window handle, which was previously stored in shared memory.
					IntPtr mainWindowHandle = IntPtr.Zero;
					lock (typeof (MainForm))
					{
						mainWindowHandle = MemoryMappedFile.ReadHandle("Local\\sharedMemoryITimeTracker");
					}
					if (mainWindowHandle != IntPtr.Zero)
					{
						if (ViewHelper.IsIconic(mainWindowHandle))
							ViewHelper.ShowWindow(mainWindowHandle, ViewHelper.SW_SHOWNORMAL); // Restore the Window 
						else
							ViewHelper.ShowWindow(mainWindowHandle, ViewHelper.SW_RESTORE); // Restore the Window 

						ViewHelper.UpdateWindow(mainWindowHandle);
					}
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Exiting...", "Exception thrown");
				}

				// Tell the garbage collector to keep the Mutex alive until the code execution reaches this point, ie. normally when the program is exiting.
				GC.KeepAlive(mutex);
				// Release the Mutex 
				try
				{
					mutex.ReleaseMutex();
				}
				catch (ApplicationException ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Exception thrown");
					GC.Collect();
				}
			}
		}

		static SplashForm splash = new SplashForm();

		internal static void Launch()
		{
			Application.CurrentCulture = CultureInfo.InvariantCulture;
			
			splash.Show();
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Interval = 1000;
			timer.Elapsed+=new System.Timers.ElapsedEventHandler(timer_Elapsed);
			timer.Start();
			//splash.Refresh();
			//Application.EnableVisualStyles();
			//Application.DoEvents();
			Application.DoEvents();
			MainModule.Initialize(new PTMDataset(), "data");
			splash.SetLoadProgress(50);
			splash.Refresh();
			Application.DoEvents();
			MainForm main = new MainForm();
			splash.SetLoadProgress(100);
			splash.Refresh();
			Application.DoEvents();
			splash.Close();
			splash = null;
			Application.DoEvents();
			Application.Run(main);
		}

		private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			splash.AddProgress(10);
			splash.Refresh();
			Application.DoEvents();
		}
		
		static public string GetVersionString()
		{
			return " alpha v. 1.4.1";
		}
		
		
	}
}
