using System.Collections;
using System.Windows.Forms;
using PTM.Framework.Helpers;
using PTM.Data;

namespace PTM.Framework
{
	
	
	public class MainModule
	{	

		protected MainModule()
		{
		}//MainModule

		/// <summary>
		/// Initializes the engine components, 
		/// </summary>
		public static void Initialize(PTMDataset ds, string userName)
		{
			DbHelper.Initialize(userName);
			Application.DoEvents();
			DBUpdaterHelper.UpdateDataBase();
			Application.DoEvents();
			DataMaintenanceHelper.DeleteIdleEntries();
			Application.DoEvents();
			DataMaintenanceHelper.GroupLogs();
			Application.DoEvents();
			DbHelper.CompactDB();
			Application.DoEvents();
			DataAdapterManager adapterManager = new DataAdapterManager();
			Application.DoEvents();
			UnitOfWork.Initialize(ds, adapterManager);
			Application.DoEvents();
			DefaultTasks.Initialize();
			Application.DoEvents();
			Tasks.Initialize(ds.Tasks, adapterManager.tasksDataAdapter);
			Application.DoEvents();
			Logs.Initialize();
			Application.DoEvents();
			Logs.FillMissingTimeUntilNow();
			Application.DoEvents();
			ApplicationsLog.Initialize();
			Application.DoEvents();
		}

	}//MainModule
}//namespace
