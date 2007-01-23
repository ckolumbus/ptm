using System;
using System.Collections;
using PTM.Data;
using PTM.Framework.Infos;

namespace PTM.Framework
{
	/// <summary>
	/// Summary description for Summary class.
	/// </summary>
	public sealed class TasksSummaries
	{
		private TasksSummaries()
		{
		} //Summary

		#region Private Methods

		private const string NOT_DETAILED = "Not Detailed";

		private static ArrayList ExecuteTaskSummary(DateTime initialDate, DateTime finalDate)
		{
			ArrayList summaryList = new ArrayList();
			ArrayList list = DbHelper.ExecuteGetRows(
				"SELECT TasksLog.TaskId, Sum( TasksLog.Duration ) AS TotalTime FROM TasksLog " +
				"WHERE ( ( (TasksLog.InsertTime)>=? And (TasksLog.InsertTime)<=? ) )" +
				"GROUP BY TasksLog.TaskId;",
				new string[] {"InsertTimeFrom", "InsertTimeTo"},
				new object[] {initialDate, finalDate});

			foreach (Hashtable hashtable in list)
			{
				TaskSummary taskSum = new TaskSummary();
				taskSum.TaskId = (int) hashtable["TaskId"];
				taskSum.TotalActiveTime = (double) hashtable["TotalTime"];
				summaryList.Add(taskSum);
			} //foreach
			return summaryList;
		} //ExecuteTaskSummary

		#endregion

		#region Public Methods

		public static ArrayList GetTaskSummary(PTMDataset.TasksRow parentRow, DateTime initialDate, DateTime finalDate)
		{
			Logs.UpdateCurrentLogDuration();
			ArrayList summaryList;
			ArrayList returnList = new ArrayList();

			summaryList = ExecuteTaskSummary(initialDate, finalDate);

			while (summaryList.Count > 0)
			{
				TaskSummary sumRow = (TaskSummary) summaryList[0];
				PTMDataset.TasksRow row = Tasks.FindById(sumRow.TaskId);
				sumRow.Description = row.Description;
				sumRow.IsActive = row.IsActive;
				sumRow.IconId = row.IconId;
				if (!sumRow.IsActive)
				{
					sumRow.TotalInactiveTime = sumRow.TotalActiveTime;
					sumRow.TotalActiveTime = 0;
				} //if

				if (sumRow.TaskId != Tasks.IdleTasksRow.Id) //ignore idle time
				{
					if (row.Id != parentRow.Id)
					{
						if (row.IsParentIdNull())
						{
							summaryList.Remove(sumRow);
							continue;
						} //if

						if (row.ParentId == parentRow.Id)
						{
							TaskSummary retrow = FindTaskSummaryByTaskId(returnList, sumRow.TaskId);
							if (retrow == null)
							{
								returnList.Add(sumRow);
							}
							else
							{
								retrow.TotalInactiveTime += sumRow.TotalInactiveTime;
								retrow.TotalActiveTime += sumRow.TotalActiveTime;
							} //if-else
						}
						else
						{
							TaskSummary psumRow = FindTaskSummaryByTaskId(summaryList, row.ParentId);
							if (psumRow == null)
							{
								PTMDataset.TasksRow prow = Tasks.FindById(row.ParentId);
								psumRow = sumRow;
								psumRow.TaskId = prow.Id;
								continue;
							} //if
							psumRow.TotalInactiveTime += sumRow.TotalInactiveTime;
							psumRow.TotalActiveTime += sumRow.TotalActiveTime;
						} //if-else
					}
					else
					{
						sumRow.Description = NOT_DETAILED;
						returnList.Add(sumRow);
					} //if-else
				} //if
				summaryList.Remove(sumRow);
			} //while
			return returnList;
		} //GetTaskSummary

		public static TaskSummary FindTaskSummaryByTaskId(ArrayList taskSummaryList, int taskId)
		{
			foreach (TaskSummary taskSummary in taskSummaryList)
			{
				if (taskSummary.TaskId == taskId)
					return taskSummary;
			} //foreach
			return null;
		} //FindTaskSummaryByTaskId

		#endregion
	} //Summary
} //namespace