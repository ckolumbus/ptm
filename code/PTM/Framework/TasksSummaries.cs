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

		

		#region Public Methods

		public static ArrayList GetTaskSummary(Task parentTask, DateTime initialDate, DateTime finalDate)
		{
			Logs.UpdateCurrentLogDuration();
			ArrayList summaryList;
			ArrayList returnList = new ArrayList();

			summaryList = ExecuteTaskSummary(initialDate, finalDate);

			while (summaryList.Count > 0)
			{
				TaskSummary sumRow = (TaskSummary) summaryList[0];
				Task row = Tasks.FindById(sumRow.TaskId);
				sumRow.Description = row.Description;
				sumRow.IsActive = row.IsActive;
				sumRow.IconId = row.IconId;
				if (!sumRow.IsActive)
				{
					sumRow.TotalInactiveTime = sumRow.TotalActiveTime;
					sumRow.TotalActiveTime = 0;
				} //if

				if (sumRow.TaskId != Tasks.IdleTask.Id) //ignore idle time
				{
					if (row.Id != parentTask.Id)
					{
						if (row.ParentId ==-1)
						{
							summaryList.Remove(sumRow);
							continue;
						} //if

						if (row.ParentId == parentTask.Id)
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
								Task prow = Tasks.FindById(row.ParentId);
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

		public static int GetWorkedDays(DateTime initialDate, DateTime finalDate)
		{
			DateTime curDate = initialDate.Date;
			int workedDays = 0;
			while(curDate<=finalDate.Date)
			{
				int count = Convert.ToInt32(DbHelper.ExecuteScalar("Select count(Id) from TasksLog where TaskId <> ? and InsertTime>= ? and InsertTime<?",
				                         new string[] {"IdleTaskId", "InitialTime", "FinalTime"},
				                         new object[] {Tasks.IdleTask.Id, curDate, curDate.AddDays(1)}));
				if(count>0)
				{
					workedDays++;
				}
				curDate = curDate.AddDays(1);
			}
			return workedDays;
		}

        public static int GetWorkedTime(DateTime initialDate, DateTime finalDate)
        {
            initialDate = initialDate.Date;
            finalDate = finalDate.Date.AddDays(1);
            object workedTime = DbHelper.ExecuteScalar("Select Sum(Duration) from TasksLog where TaskId <> ? and InsertTime>= ? and InsertTime<?",
                                         new string[] { "IdleTaskId", "InitialTime", "FinalTime" },
                                         new object[] { Tasks.IdleTask.Id, initialDate, finalDate});
            if (workedTime == DBNull.Value)
                return 0;
            else 
                return Convert.ToInt32(workedTime);
        }

        public static int GetActiveTime(DateTime initialDate, DateTime finalDate)
        {
            initialDate = initialDate.Date;
            finalDate = finalDate.Date.AddDays(1);
            object workedTime = DbHelper.ExecuteScalar("Select Sum(Duration) from TasksLog Inner Join Tasks On TasksLog.TaskId = Tasks.Id Where Tasks.IsActive <> 0 and TaskId <> ? and InsertTime>= ? and InsertTime<?",
                                         new string[] { "IdleTaskId", "InitialTime", "FinalTime" },
                                         new object[] { Tasks.IdleTask.Id, initialDate, finalDate });
            if (workedTime == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(workedTime);
        }

		#endregion

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

			foreach (IDictionary dictionary in list)
			{
				TaskSummary taskSum = new TaskSummary();
				taskSum.TaskId = (int) dictionary["TaskId"];
				taskSum.TotalActiveTime = (double) dictionary["TotalTime"];
				summaryList.Add(taskSum);
			} //foreach
			return summaryList;
		} //ExecuteTaskSummary

		private static TaskSummary FindTaskSummaryByTaskId(ArrayList taskSummaryList, int taskId)
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