using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using PTM.Data;
using PTM.View;

namespace PTM.Business
{
	internal sealed class Tasks
	{
		private static DbDataAdapter dataAdapter;
		private static PTMDataset.TasksDataTable tasksDataTable;
		private static PTMDataset.TasksDataTable tasksDataTableInstance;
		private static PTMDataset.TasksRow currentTaskRow;
		private static PTMDataset.TasksRow rootTaskRow;
		private const string DEFAULT_ROOT_TASK_NAME = "My Job";

		private Tasks()
		{
		}


		#region Properties
		internal static PTMDataset.TasksRow CurrentTaskRow
		{
			get { return CloneRow(currentTaskRow); }
		}
		internal static PTMDataset.TasksRow RootTasksRow
		{
			get
			{
				return CloneRow(rootTaskRow);
			}
		}
		internal static int Count
		{
			get
			{
				return tasksDataTable.Count;
			}
		}
		#endregion

		#region Public Methods
		internal static void Initialize(PTMDataset.TasksDataTable table, DbDataAdapter adapter)
		{
			rootTaskRow = null;
			dataAdapter = adapter;
			tasksDataTable = table;
			tasksDataTableInstance = new PTMDataset.TasksDataTable();
			LoadAllTasks();
			currentTaskRow = null;
			TasksLog.TasksLogRowChanged+=new PTMDataset.TasksLogRowChangeEventHandler(taskLogsTable_TasksLogRowChanged);
			tasksDataTable.TasksRowChanged+=new PTMDataset.TasksRowChangeEventHandler(tasksDataTable_TasksRowChanged);
			tasksDataTable.TasksRowDeleting+=new PTMDataset.TasksRowChangeEventHandler(tasksDataTable_TasksRowDeleting);
		}
		public static PTMDataset.TasksRow NewTasksRow()
		{
			return tasksDataTableInstance.NewTasksRow();
		}
		public static PTMDataset.TasksRow FindById(int taskId)
		{
			PTMDataset.TasksRow findedRow;
			findedRow = tasksDataTable.FindById(taskId);
			return CloneRow(findedRow);
		}
		public static PTMDataset.TasksRow FindByParentIdAndDescription(int parentId, string description)
		{
			PTMDataset.TasksRow[] rows;
			rows = (PTMDataset.TasksRow[]) tasksDataTable.Select(
				tasksDataTable.DescriptionColumn.ColumnName +
				"='" + description + "' AND " +
				tasksDataTable.ParentIdColumn.ColumnName +
				"=" + parentId);

			if(rows.Length>0)
				return CloneRow(rows[0]);
			else
				return null;
		}
		public static int AddTasksRow(PTMDataset.TasksRow tasksRow)
		{
			ValidateTaskRow(tasksRow);
			tasksRow.TotalTime = 0;
			tasksRow.IsFinished = false;
			tasksRow.SetStartDateNull();
			tasksRow.SetStopDateNull();
			SetDefaultTask(tasksRow);
			PTMDataset.TasksRow row;
			row = tasksDataTable.NewTasksRow();
			row.ItemArray = tasksRow.ItemArray;
			tasksDataTable.AddTasksRow(row);
			//SaveTaskRow(row);
			SaveTasks();
			if(TasksRowChanged!=null)
			{
				tasksRow.Id = row.Id;
				TasksRowChanged(null, new PTMDataset.TasksRowChangeEvent(tasksRow, DataRowAction.Add));
			}
				
			return row.Id;
		}

		public static void UpdateTaskRow(PTMDataset.TasksRow tasksRow)
		{
			ValidateTaskRow(tasksRow);
			SetDefaultTask(tasksRow);
			PTMDataset.TasksRow row;
			row = tasksDataTable.FindById(tasksRow.Id);
			row.ItemArray = tasksRow.ItemArray;
			//SaveTaskRow(row);
			SaveTasks();
			if(TasksRowChanged!=null)
				TasksRowChanged(null, new PTMDataset.TasksRowChangeEvent(tasksRow, DataRowAction.Change));
		}
		
		public static void DeleteTaskRow(PTMDataset.TasksRow tasksRow)
		{
			if(Tasks.CurrentTaskRow!=null)
				if(Tasks.CurrentTaskRow.Id == tasksRow.Id || Tasks.IsParent(tasksRow, Tasks.CurrentTaskRow))
				{
					throw new ApplicationException("This task can't be deleted now. You are currently working on it or in a part of it.");
				}
			
			if(tasksRow.Id == Tasks.rootTaskRow.Id)
				throw new ApplicationException("The root task can't be deleted.");

			PTMDataset.TasksRow row;
			row = tasksDataTable.FindById(tasksRow.Id);
			row.Delete();	
			
			//SaveTaskRow(row);
			SaveTasks();
			if(TasksRowDeleting!=null)
				TasksRowDeleting(null, new PTMDataset.TasksRowChangeEvent(tasksRow, DataRowAction.Delete));
		}
		public static PTMDataset.TasksRow[] GetChildTasks(PTMDataset.TasksRow tasksRow)
		{
			PTMDataset.TasksRow row;
			row = tasksDataTable.FindById(tasksRow.Id);
			foreach (DataRelation relation in tasksDataTable.ChildRelations)
			{
				if (relation.ChildTable.TableName == tasksDataTable.TableName)
				{				
					return CloneRows((PTMDataset.TasksRow[]) row.GetChildRows(relation));
				}
			}
			return new PTMDataset.TasksRow[] {};
		}

		public static string GetFullPath(PTMDataset.TasksRow row)
		{
			ArrayList parents = new ArrayList();
			PTMDataset.TasksRow curRow = row;
			while (true)
			{
				parents.Insert(0, curRow);
				if(curRow.IsParentIdNull())
					break;
				curRow = tasksDataTable.FindById(curRow.ParentId);
			}
			StringBuilder path = new StringBuilder();
			foreach (PTMDataset.TasksRow tasksRow in parents)
			{
				path.Append(tasksRow.Description + @"\");
			}
			return path.ToString();
		}

		public static bool IsParent(PTMDataset.TasksRow parentRow, PTMDataset.TasksRow childRow)
		{
			if(parentRow == null || childRow == null)
				return false;
			
			if(childRow.IsParentIdNull())
				return false;
			
			int parentId = childRow.ParentId;

			while (true)
			{
				PTMDataset.TasksRow curRow;
				curRow = tasksDataTable.FindById(parentId);
				//if (curRow == null) return false;
				if(curRow.Id == parentRow.Id) return true;
				if(curRow.IsParentIdNull()) return false;
				parentId = curRow.ParentId;
			}
		}


		#endregion

		#region Private Methods
		private static void LoadAllTasks()
		{
			tasksDataTable.BeginLoadData();
			dataAdapter.Fill(tasksDataTable);
	
			if (tasksDataTable.Rows.Count == 0)
			{
				AddRootTask();
			}
			else
			{
				SetRootTask();
			}
			tasksDataTable.EndLoadData();
		}

		private static void SetRootTask()
		{
			foreach (PTMDataset.TasksRow tasksRow in tasksDataTable)
			{
				if(tasksRow.IsParentIdNull())
				{
					rootTaskRow = tasksRow;
					break;
				}
			}
		}

		private static void AddRootTask()
		{
			PTMDataset.TasksRow row = tasksDataTable.NewTasksRow();
			row.BeginEdit();
			row.Description = DEFAULT_ROOT_TASK_NAME;
			row.IsDefaultTask = false;
			row.IsFinished = false;
			row.StartDate = DateTime.Now;
			tasksDataTable.AddTasksRow(row);
			//SaveTaskRow(row);
			SaveTasks();
			row.EndEdit();
			rootTaskRow = row;
			ConfigurationHelper.AddRecentTask(row);
		}

		private static void SaveTasks()
		{
			foreach(PTMDataset.TasksRow row in tasksDataTable.Rows)
			{
				if(row.RowState == DataRowState.Deleted)
					Logger.Write("Tasks.SaveTaskRow: Deleted");
				else if(row.RowState == DataRowState.Added)
					Logger.Write("Tasks.SaveTaskRow: Added " + row.Description);
				else if(row.RowState == DataRowState.Modified)
					Logger.Write("Tasks.SaveTaskRow: Modified " + row.Description);
			}
			
			//PTMDataset.TasksRow[] rows = new PTMDataset.TasksRow[] {row};
			try
			{
				dataAdapter.Update(tasksDataTable);
			}
			catch (DBConcurrencyException ex)
			{
				Logger.Write("Cascade Delete: " + ex.Message);
			}
			
			tasksDataTable.AcceptChanges();
//			for(int i = 0;i<tasksDataTable.Rows.Count;i++)
//			{
//				PTMDataset.TasksRow trow = (PTMDataset.TasksRow) tasksDataTable.Rows[i];
//				if(trow.RowState != DataRowState.Detached)
//				{
//					trow.AcceptChanges();
//					i--;
//				}
//			}
//			foreach(PTMDataset.TasksRow trow in tasksDataTable.Rows)
//				if(trow.RowState != DataRowState.Detached)
//					trow.AcceptChanges();
		}
		private static void ValidateTaskRow(PTMDataset.TasksRow tasksRow)
		{
			if(tasksRow.IsParentIdNull())
				throw new ApplicationException("Parent can't be null");
			if(tasksRow.IsDescriptionNull())
				throw new ApplicationException("Description can't be null");
			if(tasksRow.Description.Length==0)
				throw new ApplicationException("Description can't be empty");
			if(Tasks.FindByParentIdAndDescription(tasksRow.ParentId, tasksRow.Description)!=null)
				throw new ApplicationException("Task already exist");
		}
		private static PTMDataset.TasksRow CloneRow(PTMDataset.TasksRow tasksRow)
		{
			if(tasksRow==null)
				return null;
			PTMDataset.TasksRow row;
			row = NewTasksRow();		
			row.ItemArray = tasksRow.ItemArray;
			return row;
		}
		private static PTMDataset.TasksRow[] CloneRows(PTMDataset.TasksRow[] tasksRows)
		{
			PTMDataset.TasksRow[] rowsCopy = new PTMDataset.TasksRow[tasksRows.Length];
			int i=0;
			foreach (PTMDataset.TasksRow row in tasksRows)
			{
				rowsCopy[i] = CloneRow(row);
				i++;
			}
			return rowsCopy;
		}
		private static void SetDefaultTask(PTMDataset.TasksRow tasksRow)
		{
			tasksRow.IsDefaultTask = false;
			tasksRow.SetDefaultTaskIdNull();
			foreach (PTMDataset.TasksRow defaultTask in DefaultTasks.DefaultTasksDataTable.Rows)
			{
				if (string.Compare(defaultTask.Description.Replace(" ", null), tasksRow.Description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
				{
					tasksRow.IsDefaultTask = true;
					tasksRow.DefaultTaskId = defaultTask.DefaultTaskId;
					break;
				}
			}
		}

		private static void ManageTaskLogRowChanged(PTMDataset.TasksLogRowChangeEvent e)
		{
			if(e.Row.Id == TasksLog.CurrentTaskLog.Id)
			{
				if(Tasks.currentTaskRow == null || e.Row.TaskId != Tasks.currentTaskRow.Id)
				{
					Tasks.currentTaskRow = Tasks.tasksDataTable.FindById(e.Row.TaskId);
					if(Tasks.currentTaskRow.IsStartDateNull())
					{
						Tasks.currentTaskRow.StartDate = DateTime.Now;
						//SaveTaskRow(Tasks.currentTaskRow);
						SaveTasks();
					}					
				}
			}
		}
		#endregion

		#region Events
			public static event PTMDataset.TasksRowChangeEventHandler TasksRowChanged;
			public static event PTMDataset.TasksRowChangeEventHandler TasksRowDeleting;
			private static void tasksDataTable_TasksRowChanged(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			if(e.Action == DataRowAction.Add)
			{
				Logger.Write("tasksDataTable_TasksRowChanged-Add: " + e.Row.Description);
//				e.Row.TotalTime = 0;
//				e.Row.IsFinished = false;
//				SaveTaskRow(e.Row);
			}
			else if(e.Action == DataRowAction.Change)
			{
				Logger.Write("tasksDataTable_TasksRowChanged-Change: " + e.Row.Description);
				//SaveTaskRow(e.Row);
			}
			else
			{
				return;
			}
//			if(TasksRowChanged!=null)
//				TasksRowChanged(sender, e);
		}
			private static void tasksDataTable_TasksRowDeleting(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			Logger.Write("tasksDataTable_TasksRowDeleting: " + e.Row.Description);
//			if(TasksRowDeleting!=null)
//				TasksRowDeleting(sender, e);
		}

		private static void taskLogsTable_TasksLogRowChanged(object sender, PTMDataset.TasksLogRowChangeEvent e)
			{
				ManageTaskLogRowChanged(e);
			}

		#endregion

	}
}