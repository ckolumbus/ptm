using System;
using System.Collections;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using PTM.Data;
using PTM.Framework.Infos;

namespace PTM.Framework.Helpers
{
	/// <summary>
	/// Summary description for DataMaintenanceHelper.
	/// </summary>
	public sealed class DataMaintenanceHelper
	{
		private DataMaintenanceHelper()
		{
		}
		
		public static void CompactDB()
		{
			DbHelper.CompactDB();
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
		
		public static void GroupLogs()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			DateTime date = DateTime.Today.AddDays(-(int)config.Value);
			while(true)
			{
				object value = DbHelper.ExecuteScalar("SELECT Max(InsertTime) FROM TasksLog WHERE InsertTime<?",
				                       new string[] {"InsertTime"}, new object[] {date});
				
				if(value == DBNull.Value)
					break;
			
				date = ((DateTime) value).Date;
				
				//ArrayList list = Logs.GetLogsByDay(date);
				bool mergeNeeded = GroupLogsList(date);
				if(!mergeNeeded)
					break;
			}
		}

		private static bool GroupLogsList(DateTime date)
		{
			MergedLogs mergeList;
			mergeList = MergedLogs.GetMergedLogsByDay(date);
			bool mergeNeeded = false;
			
			OleDbConnection con;
			con = DbHelper.GetConnection();
			con.Open();
			OleDbTransaction trans = con.BeginTransaction();
			try
			{
				foreach (MergedLog mergedLog in mergeList)
				{
					if(mergedLog.DeletedLogs.Count==0)
						continue;
					OleDbCommand command;
					foreach (Log log in mergedLog.DeletedLogs)
					{
						command = new OleDbCommand("Update ApplicationsLog Set TaskLogId = " +  mergedLog.MergeLog.Id + " Where TaskLogId = " + log.Id, con, trans);
						command.ExecuteNonQuery();
						
						command = new OleDbCommand("Delete from TasksLog Where Id = " + log.Id, con, trans);
						command.ExecuteNonQuery();
					}
					
					command = new OleDbCommand("Update TasksLog Set Duration = " + mergedLog.MergeLog.Duration+ " Where Id = " + mergedLog.MergeLog.Id, con, trans);
					command.ExecuteNonQuery();
					mergeNeeded = true;
				}
				
//				foreach (Log log in needsDelete)
//				{
//					OleDbCommand command = new OleDbCommand("Delete from TasksLog Where Id = " + log.Id, con, trans);
//					command.ExecuteNonQuery();
//				}
//				foreach (Log log in needsUpdate)
//				{
//					OleDbCommand command = new OleDbCommand("Update TasksLog Set Duration = " + log.Duration+ " Where Id = " + log.Id, con, trans);
//					command.ExecuteNonQuery();
//				}
				
				trans.Commit();
				return mergeNeeded;
			}
			catch
			{
				trans.Rollback();
				throw;
			}
			finally
			{
				con.Close();
			}
		}
	}
}
