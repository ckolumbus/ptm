using System;
using System.Collections;
using System.Diagnostics;
using PTM.Data;
using PTM.Infos;

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
		
		public static void DeleteIdleEntries()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			DateTime limitDate = DateTime.Today.AddDays(-(int)config.Value);
			
			//Delete Idle logs
			DbHelper.ExecuteNonQuery("DELETE FROM TasksLog " +
				" WHERE TasksLog.TaskId IN " +
				" (select Id from Tasks where Tasks.IsDefaultTask = 1 and Tasks.DefaultTaskId = "+ (int)DefaultTaskEnum.Idle +")" +
				" AND TasksLog.InsertTime < ?", new string[] {"InsertTime"},
				new object[] {limitDate});
			
			//Delete Idle tasks without any log
			DbHelper.ExecuteNonQuery("DELETE FROM Tasks " +
				"WHERE Tasks.IsDefaultTask = 1 AND Tasks.DefaultTaskId = " + (int)DefaultTaskEnum.Idle +
				" AND Tasks.Id Not In (select distinct TasksLog.TaskId from TasksLog inner join Tasks on TasksLog.TaskId = Tasks.Id )");
			
		}
		
		public static void MergeContiguousLogs(int daysToMerge)
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			int i = 1;
			DateTime date = DateTime.Today.AddDays(-(int)config.Value);
			while(true)
			{
				if(i>= daysToMerge) 
					break;
				date = date.AddDays(-1);
				ArrayList list = Logs.GetLogsByDay(date);
				if(list.Count==0)
					break;
				MergeLogs(list);
				i++;
			}
		}

		private static void MergeLogs(ArrayList logs)
		{
			ArrayList mergedList = new ArrayList();
			ArrayList needsDelete = new ArrayList();
			ArrayList needsUpdate = new ArrayList();
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			int timeTolerance = (int)config.Value*60;
			int m = -1;
			for(int i = 0; i<logs.Count;i++)
			{
				Log log = (Log) logs[i];
				if(m>=0)
				{
					Log merged = (Log) mergedList[m];
					DateTime mergedEndTime = merged.InsertTime.AddSeconds(merged.Duration);
					Debug.Assert(merged.InsertTime<log.InsertTime, "The list must be always ordered.");
					if(log.TaskId==merged.TaskId && (mergedEndTime - log.InsertTime).Seconds<timeTolerance)
					{
							merged.Duration += log.Duration;
							needsDelete.Add(log);
							needsUpdate.Add(merged);
							break;
					}
				}
				mergedList.Add(log);
				m++;
			}
		}
	}
}
