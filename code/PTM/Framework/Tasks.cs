using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Text;
using PTM.Data;
using PTM.View;

namespace PTM.Framework
{
	public sealed class Tasks
	{
		private static DbDataAdapter dataAdapter;
		private static PTMDataset.TasksDataTable tasksDataTable;
		private static PTMDataset.TasksDataTable tasksDataTableInstance;
		private static PTMDataset.TasksRow currentTaskRow;
		private static PTMDataset.TasksRow rootTaskRow;
		private static PTMDataset.TasksRow idleTaskRow;
		private const string DEFAULT_ROOT_TASK_NAME = "My Job";
		private const string DEFAULT_IDLE_TASK_NAME = "Idle";

		private Tasks()
		{
		}

		
		#region Properties

		public static PTMDataset.TasksRow CurrentTaskRow
		{
			get { return CloneRow(currentTaskRow); }
		}

		public static PTMDataset.TasksRow RootTasksRow
		{
			get { return CloneRow(rootTaskRow); }
		}
		
		public static PTMDataset.TasksRow IdleTasksRow
		{
			get { return CloneRow(idleTaskRow); }
		}

		public static int Count
		{
			get { return tasksDataTable.Count; }
		}

		
		#endregion

		#region Public Methods

		public static void Initialize(PTMDataset.TasksDataTable table, DbDataAdapter adapter)
		{
			rootTaskRow = null;
			dataAdapter = adapter;
			tasksDataTable = table;
			tasksDataTableInstance = new PTMDataset.TasksDataTable();
			LoadAllTasks();
			currentTaskRow = null;
			Logs.LogChanged += new Logs.LogChangeEventHandler(TasksLog_LogChanged);
			tasksDataTable.TasksRowChanged += new PTMDataset.TasksRowChangeEventHandler(tasksDataTable_TasksRowChanged);
			tasksDataTable.TasksRowDeleting += new PTMDataset.TasksRowChangeEventHandler(tasksDataTable_TasksRowDeleting);
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

			if (rows.Length > 0)
				return CloneRow(rows[0]);
			else
				return null;
		}

		public static int AddTasksRow(PTMDataset.TasksRow tasksRow)
		{
			ValidateTaskRow(tasksRow, true);
			tasksRow.TotalTime = 0;
			tasksRow.IsFinished = false;
			tasksRow.SetStartDateNull();
			tasksRow.SetStopDateNull();
			if(tasksRow.IsIconIdNull())
				tasksRow.IconId = IconsManager.DefaultTaskIconId;
			if(tasksRow.IsIsActiveNull())
				tasksRow.IsActive = true;
			PTMDataset.TasksRow row;
			row = tasksDataTable.NewTasksRow();
			row.ItemArray = tasksRow.ItemArray;
			row.Id = -1;
			tasksDataTable.AddTasksRow(row);
			//SaveTaskRow(row);
			SaveTasks();
			if (TasksRowChanged != null)
			{
				tasksRow.Id = row.Id;
				TasksRowChanged(null, new PTMDataset.TasksRowChangeEvent(tasksRow, DataRowAction.Add));
			}

			return row.Id;
		}

		public static void UpdateTaskRow(PTMDataset.TasksRow tasksRow)
		{
			tasksRow.Description = tasksRow.Description.Trim();
			if (tasksRow.Id == rootTaskRow.Id || tasksRow.Id == idleTaskRow.Id)
				throw new ApplicationException("This task can't be updated.");
			ValidateTaskRow(tasksRow, false);
			PTMDataset.TasksRow row;
			row = tasksDataTable.FindById(tasksRow.Id);
			row.ItemArray = tasksRow.ItemArray;
			SaveTasks();
			if (TasksRowChanged != null)
				TasksRowChanged(null, new PTMDataset.TasksRowChangeEvent(tasksRow, DataRowAction.Change));
		}

		public static void DeleteTaskRow(PTMDataset.TasksRow tasksRow)
		{
			if (CurrentTaskRow != null)
				if (CurrentTaskRow.Id == tasksRow.Id || IsParent(tasksRow.Id, CurrentTaskRow.Id) > 0)
				{
					throw new ApplicationException(
						"This task can't be deleted now. You are currently working on it or in a part of it.");
				}

			if (tasksRow.Id == rootTaskRow.Id || tasksRow.Id == idleTaskRow.Id)
				throw new ApplicationException("This task can't be deleted.");

			PTMDataset.TasksRow row;
			row = tasksDataTable.FindById(tasksRow.Id);
			DeleteOnCascade(row);
		}

		private static void DeleteOnCascade(PTMDataset.TasksRow row)
		{
			PTMDataset.TasksRow[] rows;
			while (true)
			{
				rows = row.GetTasksRows();
				if (rows.Length == 0)
				{
					if (TasksRowDeleting != null)
						TasksRowDeleting(null, new PTMDataset.TasksRowChangeEvent(row, DataRowAction.Delete));
					row.Delete();
					SaveTasks();
					return;
				}
				PTMDataset.TasksRow child = rows[0];
				DeleteOnCascade(child);
			}
		}

		public static PTMDataset.TasksRow[] GetChildTasks(int taskId)
		{
			PTMDataset.TasksRow row;
			row = tasksDataTable.FindById(taskId);
			foreach (DataRelation relation in tasksDataTable.ChildRelations)
			{
				if (relation.ChildTable.TableName == tasksDataTable.TableName)
				{
					return CloneRows((PTMDataset.TasksRow[]) row.GetChildRows(relation));
				}
			}
			return null;
		}

		public static string GetFullPath(int taskId)
		{
			PTMDataset.TasksRow row;
			row = Tasks.FindById(taskId);
			ArrayList parents = new ArrayList();
			PTMDataset.TasksRow curRow = row;
			while (true)
			{
				if (curRow.IsParentIdNull())
					break;
				parents.Insert(0, curRow);
				curRow = tasksDataTable.FindById(curRow.ParentId);
			}
			StringBuilder path = new StringBuilder();
			foreach (PTMDataset.TasksRow tasksRow in parents)
			{
				path.Append(tasksRow.Description + @"\");
			}
			if(path.Length>0)
				return path.ToString(0, path.Length-1);
			else
				return String.Empty;
		}

		public static int IsParent(int parentTaskId, int childTaskId)
		{
			PTMDataset.TasksRow parentRow;
			parentRow = tasksDataTable.FindById(parentTaskId);

			PTMDataset.TasksRow childRow;
			childRow = tasksDataTable.FindById(childTaskId);

			if (parentRow == null || childRow == null)
				return -1;

			if (parentTaskId == childTaskId)
				return 0;

			if (childRow.IsParentIdNull())
				return -1;

			int parentId = childRow.ParentId;

			int generation = 1;

			while (true)
			{
				PTMDataset.TasksRow curRow;
				curRow = tasksDataTable.FindById(parentId);
				if (curRow.Id == parentRow.Id) return generation;
				if (curRow.IsParentIdNull()) return -1;
				parentId = curRow.ParentId;
				generation++;
			}
		}

		public static void UpdateParentTask(int taskId, int parentId)
		{
			PTMDataset.TasksRow row;
			row = FindById(taskId);
			if (row.Id == rootTaskRow.Id || row.Id == idleTaskRow.Id)
				throw new ApplicationException("This task can't be updated.");
			row.ParentId = parentId;
			UpdateTaskRow(row);
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
				AddIdleTask();
			}
			else
			{
				SetRootTask();
				SetIdleTask();
			}
			tasksDataTable.EndLoadData();
		}

		private static void SetRootTask()
		{
			foreach (PTMDataset.TasksRow tasksRow in tasksDataTable)
			{
				if (tasksRow.IsParentIdNull())
				{
					rootTaskRow = tasksRow;
					break;
				}
			}
		}
		
		private static void SetIdleTask()
		{
			foreach (DataRelation relation in tasksDataTable.ChildRelations)
			{
				if (relation.ChildTable.TableName == tasksDataTable.TableName)
				{
					PTMDataset.TasksRow[] rows = (PTMDataset.TasksRow[]) rootTaskRow.GetChildRows(relation);
					foreach (PTMDataset.TasksRow row in rows)
					{
						if(string.Compare(row.Description, Tasks.DEFAULT_IDLE_TASK_NAME)==0)
						{
							idleTaskRow = row;
							return;
						}
					}
				}
			}
			throw new ApplicationException("Unexpected error");
		}


		private static void AddRootTask()
		{
			PTMDataset.TasksRow row = tasksDataTable.NewTasksRow();
			row.BeginEdit();
			row.Description = DEFAULT_ROOT_TASK_NAME;
			row.IsActive = true;
			row.IsFinished = false;
			row.StartDate = DateTime.Now;
			row.IconId = IconsManager.DefaultTaskIconId;
			tasksDataTable.AddTasksRow(row);
			//SaveTaskRow(row);
			SaveTasks();
			row.EndEdit();
			rootTaskRow = row;
		}
		
		private static void AddIdleTask()
		{
			PTMDataset.TasksRow row = tasksDataTable.NewTasksRow();
			row.BeginEdit();
			row.Description = DEFAULT_IDLE_TASK_NAME;
			row.IsActive = false;
			row.IsFinished = false;
			row.StartDate = DateTime.Now;
			row.ParentId = rootTaskRow.Id;
			row.IconId = IconsManager.IdleTaskIconId;
			tasksDataTable.AddTasksRow(row);
			SaveTasks();
			row.EndEdit();
			idleTaskRow = row;
		}

		private static void SaveTasks()
		{
			foreach (PTMDataset.TasksRow row in tasksDataTable.Rows)
			{
				if (row.RowState == DataRowState.Deleted)
					Logger.Write("Tasks.SaveTaskRow: Deleted");
				else if (row.RowState == DataRowState.Added)
					Logger.Write("Tasks.SaveTaskRow: Added " + row.Description);
				else if (row.RowState == DataRowState.Modified)
					Logger.Write("Tasks.SaveTaskRow: Modified " + row.Description);
			}

			//PTMDataset.TasksRow[] rows = new PTMDataset.TasksRow[] {row};
//			try
//			{
			dataAdapter.Update(tasksDataTable);
//			}
//			catch (DBConcurrencyException ex)
//			{
//				Logger.Write("Cascade Delete: " + ex.Message);
//			}

			tasksDataTable.AcceptChanges();
		}

		private static void ValidateTaskRow(PTMDataset.TasksRow tasksRow, bool insertRules)
		{

			if (tasksRow.IsParentIdNull())
				throw new ApplicationException("Parent can't be null");
			if (tasksRow.IsDescriptionNull())
				throw new ApplicationException("Description can't be null");
			if (tasksRow.Description.Trim().Length == 0)
				throw new ApplicationException("Description can't be empty");
			tasksRow.Description = tasksRow.Description.Trim();
			PTMDataset.TasksRow sameTaskByDescription;
			sameTaskByDescription = FindByParentIdAndDescription(tasksRow.ParentId, tasksRow.Description);
			
			if (insertRules)
			{
				if (sameTaskByDescription != null)
					throw new ApplicationException("Task already exist");
			}
			else
			{
				if (sameTaskByDescription != null && sameTaskByDescription.Id !=tasksRow.Id)
					throw new ApplicationException("Task already exist");			
			}

		}

		private static PTMDataset.TasksRow CloneRow(PTMDataset.TasksRow tasksRow)
		{
			if (tasksRow == null)
				return null;
			PTMDataset.TasksRow row;
			row = NewTasksRow();
			row.ItemArray = tasksRow.ItemArray;
			return row;
		}

		private static PTMDataset.TasksRow[] CloneRows(PTMDataset.TasksRow[] tasksRows)
		{
			PTMDataset.TasksRow[] rowsCopy = new PTMDataset.TasksRow[tasksRows.Length];
			int i = 0;
			foreach (PTMDataset.TasksRow row in tasksRows)
			{
				rowsCopy[i] = CloneRow(row);
				i++;
			}
			return rowsCopy;
		}

		private static void ManageTaskLogRowChanged(Logs.LogChangeEventArgs e)
		{
			if (e.Log.Id == Logs.CurrentLog.Id)
			{
				if (currentTaskRow == null || e.Log.TaskId != currentTaskRow.Id)
				{
					currentTaskRow = tasksDataTable.FindById(e.Log.TaskId);
					if (currentTaskRow.IsStartDateNull())
					{
						currentTaskRow.StartDate = DateTime.Now;
						SaveTasks();
					}
				}
			}
		}

		private static void TasksLog_LogChanged(Logs.LogChangeEventArgs e)
		{
			ManageTaskLogRowChanged(e);
		}

		
		#endregion

		#region Events

		public static event PTMDataset.TasksRowChangeEventHandler TasksRowChanged;
		public static event PTMDataset.TasksRowChangeEventHandler TasksRowDeleting;

		private static void tasksDataTable_TasksRowChanged(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				Logger.Write("tasksDataTable_TasksRowChanged-Add: " + e.Row.Description);
//				e.Row.TotalTime = 0;
//				e.Row.IsFinished = false;
//				SaveTaskRow(e.Row);
			}
			else if (e.Action == DataRowAction.Change)
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

		#endregion

		
	}
}