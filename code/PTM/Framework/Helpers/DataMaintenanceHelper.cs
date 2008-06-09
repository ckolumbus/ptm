using System;
using System.Collections;
using System.Data.OleDb;
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
			DateTime limitDate = DateTime.Today.AddDays(-(int) config.Value);

			//Delete Idle logs
			DbHelper.ExecuteNonQuery("DELETE FROM TasksLog " +
			                         " WHERE TasksLog.TaskId =  " + Tasks.IdleTask.Id +
			                         " AND TasksLog.InsertTime < ?", new string[] {"InsertTime"},
			                         new object[] {limitDate});
		}

        public static void DeleteZeroOrNullActiveTimeEntries()
		{
            DbHelper.ExecuteNonQuery("Delete from ApplicationsLog where ActiveTime is NULL or ActiveTime = 0");
            // See bug 1917606
            DbHelper.ExecuteNonQuery("Delete from TasksLog where Duration is NULL or Duration = 0");
		}
		public static void GroupLogs(bool fullCheck)
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			DateTime date = DateTime.Today.AddDays(-(int) config.Value);
			while (true)
			{
				object value = DbHelper.ExecuteScalar("SELECT Max(InsertTime) FROM TasksLog WHERE InsertTime<?",
				                                      new string[] {"InsertTime"}, new object[] {date});

				if (value == DBNull.Value)
					break;

				date = ((DateTime) value).Date;

				//ArrayList list = Logs.GetLogsByDay(date);
				bool mergeNeeded = GroupLogsList(date);

				if(fullCheck)
					continue;

				if (!mergeNeeded)
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
					ArrayList applicationsLog;
					applicationsLog = ApplicationsLog.GetApplicationsLog(mergedLog.MergeLog.Id);
					applicationsLog = MergeApplicationsLists(applicationsLog, con, trans);

					if (mergedLog.DeletedLogs.Count == 0)
						continue;
					OleDbCommand command;

					foreach (Log log in mergedLog.DeletedLogs)
					{
						ArrayList deletedApplicationsLog;
						deletedApplicationsLog = ApplicationsLog.GetApplicationsLog(log.Id);

						applicationsLog = MergeApplicationsLists(applicationsLog, deletedApplicationsLog, mergedLog.MergeLog.Id, con, trans);

//						command =
//							new OleDbCommand(
//								"Delete from ApplicationsLog Where TaskLogId = " + log.Id, con, trans);
//						command.ExecuteNonQuery();

						command = new OleDbCommand("Delete from TasksLog Where Id = " + log.Id, con, trans);
						command.ExecuteNonQuery();
					}

					command =
						new OleDbCommand(
							"Update TasksLog Set Duration = " + mergedLog.MergeLog.Duration + " Where Id = " + mergedLog.MergeLog.Id, con,
							trans);
					command.ExecuteNonQuery();
					mergeNeeded = true;
				}

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

		//Merge appSumaryList2 into appSumaryList1
		private static ArrayList MergeApplicationsLists(ArrayList appSumaryList1, ArrayList appSumaryList2, int mergedLogId, OleDbConnection con, OleDbTransaction trans )
		{
			OleDbCommand command;		
			foreach (ApplicationLog row in appSumaryList2)
			{
				ApplicationLog sum = null;
				for (int i = 0; i < appSumaryList1.Count; i++)
				{
					if (string.Compare(row.Name, ((ApplicationLog) appSumaryList1[i]).Name, true) ==
						0)
					{
						sum = (ApplicationLog) appSumaryList1[i];
						break;
					}
				}

				if (sum == null)
				{
					appSumaryList1.Add(row);
					command =
						new OleDbCommand(
						"Update ApplicationsLog Set TaskLogId = " + mergedLogId + " Where Id = " + row.Id, con, trans);
					command.ExecuteNonQuery();
				}
				else
				{
					sum.ActiveTime += row.ActiveTime;
					command =
						new OleDbCommand(
						"Update ApplicationsLog Set ActiveTime = " + sum.ActiveTime + " Where Id = " + sum.Id, con, trans);
					command.ExecuteNonQuery();

					command =
							new OleDbCommand(
					"Delete from ApplicationsLog Where Id = " + row.Id, con, trans);
					command.ExecuteNonQuery();
				} //if-else
			} //foreach
			return appSumaryList1;
		}

		//Merge appSumaryList1 same apps
		private static ArrayList MergeApplicationsLists(ArrayList appSumaryList1, OleDbConnection con, OleDbTransaction trans )
		{
			OleDbCommand command;
			
			for(int i = 0; i < appSumaryList1.Count ;i++)
			{
				for(int j = i+1; j < appSumaryList1.Count ;j++)
				{
					if (string.Compare(((ApplicationLog) appSumaryList1[i]).Name, ((ApplicationLog) appSumaryList1[j]).Name, true) ==
						0)
					{
						((ApplicationLog) appSumaryList1[i]).ActiveTime += ((ApplicationLog) appSumaryList1[j]).ActiveTime;
						command =
							new OleDbCommand(
							"Update ApplicationsLog Set ActiveTime = " + ((ApplicationLog) appSumaryList1[i]).ActiveTime + " Where Id = " + ((ApplicationLog) appSumaryList1[i]).Id, con, trans);
						command.ExecuteNonQuery();

						command =
							new OleDbCommand(
							"Delete from ApplicationsLog Where Id = " + ((ApplicationLog) appSumaryList1[j]).Id, con, trans);
						command.ExecuteNonQuery();

						appSumaryList1.RemoveAt(j);
						j--;
						
					}
				}
			}
			return appSumaryList1;
		}
	}
}