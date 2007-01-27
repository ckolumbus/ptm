using System;
using System.Collections;
using PTM.Data;
using PTM.Framework.Infos;

namespace PTM.Framework
{
	/// <summary>
	/// Summary description for ApplicationSummaries.
	/// </summary>
	public class ApplicationSummaries
	{
		private ApplicationSummaries()
		{
		}

		#region Private Methods

		private static ArrayList GetApplicationsRecursiveSummary(PTMDataset.TasksRow parentRow, DateTime ini, DateTime end)
		{
			ArrayList arrayHT = DbHelper.ExecuteGetRows(
				"SELECT TasksLog.TaskId, Sum(ApplicationsLog.ActiveTime) AS TotalActiveTime, ApplicationsLog.Name, ApplicationsLog.ApplicationFullPath " +
				"FROM TasksLog INNER JOIN ApplicationsLog ON TasksLog.Id = ApplicationsLog.TaskLogId " +
				"WHERE TasksLog.Id IN (select TasksLog.Id from TasksLog where TasksLog.TaskId=? and TasksLog.InsertTime>=? and TasksLog.InsertTime<=?) " +
				"GROUP BY TasksLog.TaskId, ApplicationsLog.Name, ApplicationsLog.ApplicationFullPath",
				new string[] {"TaskId", "InsertTime1", "InsertTime2"}, new object[] {parentRow.Id, ini, end});

			ArrayList tempDataset = new ArrayList();
			foreach (Hashtable hashtable in arrayHT)
			{
				ApplicationSummary appSum = new ApplicationSummary();
				appSum.TaskId = (int) hashtable["TaskId"];
				appSum.TotalActiveTime = (double) hashtable["TotalActiveTime"];
				appSum.Name = (string) hashtable["Name"];
				appSum.ApplicationFullPath = (string) hashtable["ApplicationFullPath"];
				tempDataset.Add(appSum);
			} //foreach

			ArrayList appSumaryList = MergeApplicationSummaryLists(new ArrayList(), tempDataset);
			PTMDataset.TasksRow[] childRows;
			childRows = Tasks.GetChildTasks(parentRow.Id);
			foreach (PTMDataset.TasksRow childRow in childRows)
			{
				appSumaryList = MergeApplicationSummaryLists(appSumaryList, GetApplicationsRecursiveSummary(childRow, ini, end));
			} //foreach
			return appSumaryList;
		} //GetApplicationsRecursiveSummary

		private static ArrayList MergeApplicationSummaryLists(ArrayList appSumaryList1, ArrayList appSumaryList2)
		{
			foreach (ApplicationSummary row in appSumaryList2)
			{
				ApplicationSummary sum = null;
				for (int i = 0; i < appSumaryList1.Count; i++)
				{
					if (string.Compare(row.Name, ((ApplicationSummary) appSumaryList1[i]).Name, true) ==
					    0)
					{
						sum = (ApplicationSummary) appSumaryList1[i];
						break;
					}
				}

				if (sum == null)
				{
					appSumaryList1.Add(row);
				}
				else
				{
					sum.TotalActiveTime += row.TotalActiveTime;
				} //if-else
			} //foreach
			return appSumaryList1;
		}

		#endregion

		#region Public Methods

		public static ArrayList GetApplicationsSummary(PTMDataset.TasksRow parentRow, DateTime ini, DateTime end)
		{
			ApplicationsLog.UpdateCurrentApplicationsLog();
			return GetApplicationsRecursiveSummary(parentRow, ini, end);
		} //GetApplicationsSummary

		#endregion
	}
}