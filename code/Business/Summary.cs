using System;
using System.Collections;
using System.Data.OleDb;
using PTM.Data;
using PTM.Infos;

namespace PTM.Business
{
	/// <summary>
	/// Summary description for Summary.
	/// </summary>
	internal sealed class Summary
	{
		private Summary()
		{
		}

		private const string NOT_DETAILED = "Not Detailed";
		private static SummaryDataset.ApplicationsSummaryDataTable applicationsSummary = null;
		private static OleDbDataAdapter applicationsSummaryAdapter;

		public static void Initialize(OleDbDataAdapter applicationsSummaryDataAdapter)
		{
			applicationsSummaryAdapter = applicationsSummaryDataAdapter;
		}


		public static ArrayList GetTaskSummary(PTMDataset.TasksRow parentRow, DateTime initialDate, DateTime finalDate)
		{

			Logs.UpdateCurrentLogDuration();
			ArrayList summaryList;
			ArrayList returnList = new ArrayList();

			summaryList = ExecuteTaskSummary(initialDate, finalDate);

			//			if (parentRow == null)
			//				return summaryDataset.TasksSummary;

			while (summaryList.Count > 0)
			{
				TaskSummary sumRow = (TaskSummary) summaryList[0];
				PTMDataset.TasksRow row = Tasks.FindById(sumRow.TaskId);
				sumRow.Description = row.Description;
				sumRow.IsDefaultTask = row.IsDefaultTask;
				if (sumRow.IsDefaultTask)
					sumRow.DefaultTaskId = row.DefaultTaskId;
				
				if(sumRow.DefaultTaskId!=(int)DefaultTaskEnum.Idle)//ignore idle time
				{
					if (row.Id != parentRow.Id)
					{
						if(row.IsParentIdNull())
						{
							summaryList.Remove(sumRow);
							continue;
						}

						if (row.ParentId == parentRow.Id)
						{
							TaskSummary retrow = FindTaskSummaryByTaskId(returnList, sumRow.TaskId);
							if (retrow == null)
								returnList.Add(sumRow);
							else
							{
								retrow.TotalTime += sumRow.TotalTime;
							}
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
							}
							psumRow.TotalTime += sumRow.TotalTime;
						}
					}
					else
					{
						sumRow.Description = NOT_DETAILED;
						returnList.Add(sumRow);
					}
					}
				summaryList.Remove(sumRow);
			}
			return returnList;
		}

		private static ArrayList ExecuteTaskSummary(DateTime initialDate, DateTime finalDate)
		{
			ArrayList summaryList = new ArrayList();
			ArrayList list = DataAdapterManager.ExecuteGetRows("SELECT TasksLog.TaskId, Sum(TasksLog.Duration) AS TotalTime FROM TasksLog " + 
				"WHERE (((TasksLog.InsertTime)>=? And (TasksLog.InsertTime)<=?))" + 
				"GROUP BY TasksLog.TaskId;", new string[]{"InsertTimeFrom", "InsertTimeTo"}, new object[]{initialDate, finalDate});
	
			foreach (Hashtable hashtable in list)
			{
				TaskSummary sum = new TaskSummary();
				sum.TaskId = (int) hashtable["TaskId"];
				sum.TotalTime = (double) hashtable["TotalTime"];
				summaryList.Add(sum);
			}
			return summaryList;
		}

		internal static SummaryDataset.ApplicationsSummaryDataTable GetApplicationsSummary(PTMDataset.TasksRow parentRow, DateTime ini, DateTime end)
		{
			ApplicationsLog.UpdateCurrentApplicationsLog();
			applicationsSummary = new SummaryDataset.ApplicationsSummaryDataTable();
			GetRecursiveSummary(parentRow, ini, end);
			return applicationsSummary;
		}

		private static void GetRecursiveSummary(PTMDataset.TasksRow parentRow, DateTime ini, DateTime end)
		{
			SummaryDataset.ApplicationsSummaryDataTable tempDataset = new SummaryDataset.ApplicationsSummaryDataTable();
			applicationsSummaryAdapter.SelectCommand.Parameters["TaskId"].Value = parentRow.Id;
			applicationsSummaryAdapter.SelectCommand.Parameters["InsertTime"].Value = ini;
			applicationsSummaryAdapter.SelectCommand.Parameters["InsertTime1"].Value = end;
			applicationsSummaryAdapter.Fill(tempDataset);
			foreach (SummaryDataset.ApplicationsSummaryRow row in tempDataset.Rows)
			{
				SummaryDataset.ApplicationsSummaryRow[] appSums = (SummaryDataset.ApplicationsSummaryRow[]) applicationsSummary.Select(
					applicationsSummary.ApplicationFullPathColumn.ColumnName +
					"='" + row.ApplicationFullPath + "'");
				if (appSums.Length == 0)
				{
					applicationsSummary.ImportRow(row);
				}
				else
				{
					appSums[0].TotalActiveTime += row.TotalActiveTime;
					appSums[0].TotalApplicationsLog += row.TotalApplicationsLog;
				}
			}
			PTMDataset.TasksRow[] childRows;
			childRows = Tasks.GetChildTasks(parentRow.Id);
			foreach (PTMDataset.TasksRow childRow in childRows)
			{
				GetRecursiveSummary(childRow, ini, end);
			}
		}

		public static TaskSummary FindTaskSummaryByTaskId(ArrayList taskSummaryList, int taskId)
		{
			foreach (TaskSummary taskSummary in taskSummaryList)
			{
				if(taskSummary.TaskId == taskId)
					return taskSummary;
			}
			return null;
		}
	}
}