using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using PTM.Data;

namespace PTM.Business
{
	/// <summary>
	/// Summary description for SummaryHelper.
	/// </summary>
	internal sealed class SummaryHelper
	{
		private SummaryHelper()
		{
		}

		//private static SummaryDataset summaryDataset = new SummaryDataset();

		private const string NOT_DETAILED = "Not Detailed";
		private static SummaryDataset.ApplicationsSummaryDataTable applicationsSummary = null;
		private static OleDbDataAdapter summaryAdapter;
		private static OleDbDataAdapter applicationsSummaryAdapter;

		public static void Initialize(OleDbDataAdapter summaryDataAdapter, OleDbDataAdapter applicationsSummaryDataAdapter)
		{
			summaryAdapter = summaryDataAdapter;
			applicationsSummaryAdapter = applicationsSummaryDataAdapter;
		}

		internal static SummaryDataset.TasksSummaryDataTable GetTaskSummary(PTMDataset.TasksRow parentRow, DateTime initialDate, DateTime finalDate)
		{
			SummaryDataset summaryDataset = new SummaryDataset();
			SummaryDataset returnDataset = new SummaryDataset();

			summaryAdapter.SelectCommand.Parameters["InsertTime"].Value = initialDate;
			summaryAdapter.SelectCommand.Parameters["InsertTime1"].Value = finalDate;
			summaryAdapter.Fill(summaryDataset.TasksSummary);
			if (parentRow == null)
				return summaryDataset.TasksSummary;

			while (summaryDataset.TasksSummary.Rows.Count > 0)
			{
				SummaryDataset.TasksSummaryRow sumRow = (SummaryDataset.TasksSummaryRow) summaryDataset.TasksSummary.Rows[0];
				PTMDataset.TasksRow row = Tasks.FindById(sumRow.TaskId);
				if (row.Id != parentRow.Id)
				{
					if(row.IsParentIdNull())
					{
						summaryDataset.TasksSummary.Rows.Remove(sumRow);
						summaryDataset.TasksSummary.AcceptChanges();
					}

					if (row.ParentId == parentRow.Id)
					{
						SummaryDataset.TasksSummaryRow retrow = returnDataset.TasksSummary.FindByTaskId(sumRow.TaskId);
						if (retrow == null)
							returnDataset.TasksSummary.ImportRow(sumRow);
						else
						{
							retrow.TotalTime += sumRow.TotalTime;
							retrow.TotalLogged += sumRow.TotalLogged;
						}
					}
					else
					{
						SummaryDataset.TasksSummaryRow psumRow = summaryDataset.TasksSummary.FindByTaskId(row.ParentId);
						if (psumRow == null)
						{
							PTMDataset.TasksRow prow = Tasks.FindById(row.ParentId);
							psumRow = sumRow;
							psumRow.TaskId = prow.Id;
							psumRow.Description = prow.Description;
							psumRow.IsDefaultTask = prow.IsDefaultTask;
							if (psumRow.IsDefaultTask)
								psumRow.DefaultTaskId = prow.DefaultTaskId;
							continue;
						}
						psumRow.TotalTime += sumRow.TotalTime;
						psumRow.TotalLogged += sumRow.TotalLogged;
					}
				}
				else
				{
					sumRow.Description = NOT_DETAILED;
					returnDataset.TasksSummary.ImportRow(sumRow);
				}
				summaryDataset.TasksSummary.Rows.Remove(sumRow);
				summaryDataset.TasksSummary.AcceptChanges();
			}
			return returnDataset.TasksSummary;
		}


		internal static SummaryDataset.ApplicationsSummaryDataTable GetApplicationsSummary(PTMDataset.TasksRow parentRow, DateTime ini, DateTime end)
		{
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
			//childRows = (PTMDataset.TasksRow[]) parentRow.GetChildRows(Tasks.GetRecursiveRelation((PTMDataset.TasksDataTable) parentRow.Table));
			childRows = Tasks.GetChildTasks(parentRow);
			foreach (PTMDataset.TasksRow childRow in childRows)
			{
				GetRecursiveSummary(childRow, ini, end);
			}
		}
	}
}