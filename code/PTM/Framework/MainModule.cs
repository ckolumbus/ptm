using System.Windows.Forms;
using PTM.Data;
using PTM.Framework.Helpers;

namespace PTM.Framework
{
	public class MainModule
	{
		protected MainModule()
		{
		} //MainModule

		/// <summary>
		/// Initializes the engine components, 
		/// </summary>
		public static void Initialize(string userName)
		{
			DbHelper.Initialize(userName);
			Application.DoEvents();

			DBUpdaterHelper.UpdateDataBase();
			Application.DoEvents();

			DbHelper.CompactDB();
			Application.DoEvents();

			Tasks.Initialize();
			Application.DoEvents();

			DataMaintenanceHelper.DeleteIdleEntries();
			Application.DoEvents();
			DataMaintenanceHelper.DeleteZeroOrNullActiveTimeEntries();
			Application.DoEvents();
			DataMaintenanceHelper.GroupLogs(false);
			Application.DoEvents();
            
			Logs.Initialize();
			Application.DoEvents();
			Logs.FillMissingTimeUntilNow();
			Application.DoEvents();
			ApplicationsLog.Initialize();
			Application.DoEvents();
		}
	} //MainModule
} //namespace