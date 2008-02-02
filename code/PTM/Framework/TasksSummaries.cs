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
				TaskSummary currentSum = (TaskSummary) summaryList[0];
				Task currentTask = Tasks.FindById(currentSum.TaskId);
				currentSum.Description = currentTask.Description;
				currentSum.IsActive = currentTask.IsActive;
				currentSum.IconId = currentTask.IconId;
			    currentSum.TotalEstimation = currentTask.Estimation;
				if (!currentSum.IsActive)
				{
					currentSum.TotalInactiveTime = currentSum.TotalActiveTime;
					currentSum.TotalActiveTime = 0;
				} //if

                if (currentTask.Id != Tasks.IdleTask.Id) //ignore idle time
				{
					if (currentTask.Id != parentTask.Id)
					{
						if (currentTask.ParentId ==-1)
						{
							summaryList.Remove(currentSum);
							continue;
						} //if

						if (currentTask.ParentId == parentTask.Id)
						{
							TaskSummary retrow = FindTaskSummaryByTaskId(returnList, currentSum.TaskId);
							if (retrow == null)
							{
								returnList.Add(currentSum);
							}
							else
							{
								retrow.TotalInactiveTime += currentSum.TotalInactiveTime;
								retrow.TotalActiveTime += currentSum.TotalActiveTime;
                                retrow.TotalEstimation += currentSum.TotalEstimation;
							}
						}
						else
						{
							TaskSummary currentSumParent = FindTaskSummaryByTaskId(summaryList, currentTask.ParentId);
							if (currentSumParent == null) //If parent not in the summary list
							{
								currentSumParent = currentSum;
                                currentSumParent.TaskId = currentTask.ParentId; //just swith to parent task
								continue; //continue without remove the current sum from list
							}
                            else //else acum totals
							{
                                currentSumParent.TotalInactiveTime += currentSum.TotalInactiveTime;
                                currentSumParent.TotalActiveTime += currentSum.TotalActiveTime;
                                currentSumParent.TotalEstimation += currentSum.TotalEstimation;
							}
						}
					}
					else
					{
						currentSum.Description = NOT_DETAILED;
						returnList.Add(currentSum);
					}
				} //if
				summaryList.Remove(currentSum);
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