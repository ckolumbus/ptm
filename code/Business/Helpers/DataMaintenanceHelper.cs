using System;
using PTM.Data;

namespace PTM.Business.Helpers
{
	/// <summary>
	/// Summary description for DataMaintenanceHelper.
	/// </summary>
	public sealed class DataMaintenanceHelper
	{
		private DataMaintenanceHelper()
		{
		}
		
		public void DeleteIdleEntries()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			DateTime limitDate = DateTime.Today.AddDays(-Convert.ToInt32(config.Value));
			
			//Delete Idle logs
			DataAdapterManager.ExecuteNonQuery("DELETE FROM TasksLog " +
				" WHERE TasksLog.TaskId IN " +
				" (select Id from Tasks where Tasks.IsDefaultTask = 1 and Tasks.DefaultTaskId = "+ (int)DefaultTaskEnum.Idle +")" +
				" AND TasksLog.InsertTime <=?", new string[] {"InsertTime"},
				new object[] {limitDate});
			
			//Delete Idle tasks without any log
			DataAdapterManager.ExecuteNonQuery("DELETE FROM Tasks " +
				"WHERE Tasks.IsDefaultTask = 1 and Tasks.DefaultTaskId = " + (int)DefaultTaskEnum.Idle +
				" AND Tasks.Id Not In (select distinct TasksLog.TaskId from TasksLog inner join Tasks on TasksLog.TaskId = Tasks.Id where Tasks.IsDefaultTask = 1 and Tasks.DefaultTaskId = "+ (int)DefaultTaskEnum.Idle +")");
		}
	}
}
