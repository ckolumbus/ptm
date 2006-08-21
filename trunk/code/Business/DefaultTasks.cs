using System;
using PTM.Data;

namespace PTM.Business
{
	/// <summary>
	/// Descripción breve de DefaultTasks.
	/// </summary>
	public sealed class DefaultTasks
	{
		private DefaultTasks()
		{
		}
		
		public static void Initialize()
		{
			defaultTasksDataTable = new PTMDataset.TasksDataTable();
			LoadDefaultTasks();
		}

		private static void LoadDefaultTasks()
		{
			defaultTasksDataTable.BeginLoadData();
			foreach (ManagementDataset.DefaultTasksRow row in ConfigurationHelper.DefaultTasks.Rows)
			{
				PTMDataset.TasksRow trow = defaultTasksDataTable.NewTasksRow();
				trow.DefaultTaskId = row.Id;
				trow.Description = row.Description;
				trow.IsDefaultTask = true;
				defaultTasksDataTable.Rows.Add(trow);
			}
			defaultTasksDataTable.EndLoadData();
		}

		private static PTMDataset.TasksDataTable defaultTasksDataTable;
		internal static PTMDataset.TasksDataTable DefaultTasksDataTable
		{
			get { return (PTMDataset.TasksDataTable) defaultTasksDataTable.Copy(); }
		}
	}
}
