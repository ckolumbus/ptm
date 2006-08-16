using System;
using System.Data;
using NUnit.Framework;
using PTM.Business;
using PTM.Data;

namespace PTM.Test.Business
{
	/// <summary>
	/// Descripción breve de Tasks.
	/// </summary>
	[TestFixture]
	public class TasksTest
	{
		public TasksTest()
		{
		}

		private int tasksRowChangedEvent_RowAddedCount;
		private int tasksRowChangedEvent_RowUpdatedCount;
		private int tasksRowDeletingEventCount;
		
		[SetUp]
		public void SetUp()
		{
			this.TearDown();
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
			
			tasksRowChangedEvent_RowAddedCount = 0;
			tasksRowChangedEvent_RowUpdatedCount = 0;
			tasksRowDeletingEventCount = 0;
			Tasks.TasksRowChanged+=new PTM.Data.PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TasksRowDeleting+=new PTM.Data.PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowDeleting);
		}

		[Test]
		public void InitializeTest()
		{
			Assert.AreEqual(1, UnitOfWork.PtmDataset.Tasks.Count);
			Assert.IsNotNull(Tasks.RootTasksRow);
		}


		[Test]
		public void AddTaskTest()
		{			
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "AddTaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			int count = Tasks.Count;
			int eventCount = tasksRowChangedEvent_RowAddedCount;
			int id = Tasks.AddTasksRow(row);
			Assert.AreEqual(count+1, Tasks.Count);
			Assert.AreEqual(eventCount+1, tasksRowChangedEvent_RowAddedCount);
			PTMDataset.TasksRow addedTaskRow;
			addedTaskRow = Tasks.FindById(id);
			Assert.AreEqual(row.ParentId, addedTaskRow.ParentId);
			Assert.AreEqual("AddTaskTest", addedTaskRow.Description);
			Assert.AreEqual(0, addedTaskRow.TotalTime);
			Assert.AreEqual(false, addedTaskRow.IsFinished);
			Assert.AreEqual(false, addedTaskRow.IsDefaultTask);
			Assert.AreEqual(true, addedTaskRow.IsDefaultTaskIdNull());
			Assert.AreEqual(true, addedTaskRow.IsStartDateNull());
			Assert.AreEqual(true, addedTaskRow.IsStopDateNull());
			
		}

		[Test]
		[ExpectedException(typeof(ApplicationException), "Task already exist")]
		public void AddTaskAlreadyExists()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "AddTaskAlreadyExists";
			row.ParentId = Tasks.RootTasksRow.Id;
			Tasks.AddTasksRow(row);
			Tasks.AddTasksRow(row);
		}

		[Test]
		[ExpectedException(typeof(ApplicationException), "Description can't be null")]
		public void AddTaskDescripcionNull()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.ParentId = Tasks.RootTasksRow.Id;
			Tasks.AddTasksRow(row);
		}

		[Test]
		[ExpectedException(typeof(ApplicationException), "Description can't be empty")]
		public void AddTaskDescripcionEmpty()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Description = String.Empty;
			Tasks.AddTasksRow(row);
		}

		[Test]
		[ExpectedException(typeof(ApplicationException), "Parent can't be null")]
		public void AddTaskParentNull()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "Test";
			Tasks.AddTasksRow(row);
		}

		[Test]
		public void AddDefaultTask()
		{
			PTMDataset.TasksRow defaultTaskRow;
			defaultTaskRow = (PTMDataset.TasksRow) DefaultTasks.DefaultTasksDataTable.Rows[0];
			
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = defaultTaskRow.Description;
			row.ParentId = Tasks.RootTasksRow.Id;
			int id = Tasks.AddTasksRow(row);
			PTMDataset.TasksRow addedTaskRow;
			addedTaskRow = Tasks.FindById(id);
			Assert.AreEqual(row.ParentId, addedTaskRow.ParentId);
			Assert.AreEqual(defaultTaskRow.Description, addedTaskRow.Description);
			Assert.AreEqual(0, addedTaskRow.TotalTime);
			Assert.AreEqual(false, addedTaskRow.IsFinished);
			Assert.AreEqual(true, addedTaskRow.IsDefaultTask);
			Assert.AreEqual(false, addedTaskRow.IsDefaultTaskIdNull());
		}


		[Test]
		public void UpdateTask()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "TaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTasksRow(row);
			int count = Tasks.Count;
			int eventCount = tasksRowChangedEvent_RowUpdatedCount;
			row.Description = "UpdatedTaskTest";
         Tasks.UpdateTaskRow(row);
			Assert.AreEqual(count, Tasks.Count);
			PTMDataset.TasksRow updatedRow;
			updatedRow = Tasks.FindById(row.Id);
			Assert.AreEqual("UpdatedTaskTest", updatedRow.Description);
			Assert.AreEqual(eventCount+1, this.tasksRowChangedEvent_RowUpdatedCount);
		}

		[Test]
		public void UpdateTaskToDefaultTask()
		{
			PTMDataset.TasksRow defaultTaskRow;
			defaultTaskRow = (PTMDataset.TasksRow) DefaultTasks.DefaultTasksDataTable.Rows[0];

			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "TaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTasksRow(row);
			int count = Tasks.Count;
			row.Description = defaultTaskRow.Description;
			Tasks.UpdateTaskRow(row);
			Assert.AreEqual(count, Tasks.Count);
			PTMDataset.TasksRow updatedRow;
			updatedRow = Tasks.FindById(row.Id);
			Assert.AreEqual(defaultTaskRow.Description, updatedRow.Description);
			Assert.AreEqual(true, updatedRow.IsDefaultTask);
			Assert.AreEqual(defaultTaskRow.DefaultTaskId, updatedRow.DefaultTaskId);
		}

		[Test]
		public void UpdateDefaultTaskToTask()
		{
			PTMDataset.TasksRow defaultTaskRow;
			defaultTaskRow = (PTMDataset.TasksRow) DefaultTasks.DefaultTasksDataTable.Rows[0];

			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = defaultTaskRow.Description;
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTasksRow(row);
			int count = Tasks.Count;
			row.Description = "TaskTest";
			Tasks.UpdateTaskRow(row);
			Assert.AreEqual(count, Tasks.Count);
			PTMDataset.TasksRow updatedRow;
			updatedRow = Tasks.FindById(row.Id);
			Assert.AreEqual("TaskTest", updatedRow.Description);
			Assert.AreEqual(false, updatedRow.IsDefaultTask);
			Assert.AreEqual(true, updatedRow.IsDefaultTaskIdNull());
		}


		[Test]
		public void DeleteTaskTest()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "TaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTasksRow(row);
			int count = Tasks.Count;
			int eventCount = tasksRowDeletingEventCount;
			Tasks.DeleteTaskRow(row);
			Assert.AreEqual(count-1, Tasks.Count);
			Assert.AreEqual(eventCount+1, this.tasksRowDeletingEventCount);
		}

		[Test]
		[ExpectedException(typeof(ApplicationException), "This task can't be deleted now. You are currently working on it or in a part of it.")]
		public void DeleteCurrentTaskTest()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "TaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTasksRow(row);
			PTMDataset.TasksLogRow log = TasksLog.NewTasksLogRow();
			log.TaskId = row.Id;
			TasksLog.AddTasksLogRow(log);
			Tasks.DeleteTaskRow(row);
		}
		
		[Test]
		[ExpectedException(typeof(ApplicationException), "The root task can't be deleted.")]
		public void DeleteRootTaskTest()
		{
			Tasks.DeleteTaskRow(Tasks.RootTasksRow);
		}

		[Test]
		[ExpectedException(typeof(ApplicationException), "This task can't be deleted now. You are currently working on it or in a part of it.")]
		public void DeleteChildOfCurrentTaskTest()
		{
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);

			PTMDataset.TasksRow row2;
			row2 = Tasks.NewTasksRow();
			row2.Description = "TaskTest2";
			row2.ParentId = row1.Id;
			row2.Id = Tasks.AddTasksRow(row2);

			PTMDataset.TasksLogRow log = TasksLog.NewTasksLogRow();
			log.TaskId = row2.Id;
			TasksLog.AddTasksLogRow(log);
			Tasks.DeleteTaskRow(row1);
		}

		[Test]
		public void DeleteOnCascadeTaskTest()
		{
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);

			PTMDataset.TasksRow row2;
			row2 = Tasks.NewTasksRow();
			row2.Description = "TaskTest2";
			row2.ParentId = row1.Id;
			row2.Id = Tasks.AddTasksRow(row2);

			int count = Tasks.Count;
			Tasks.DeleteTaskRow(row1);
			Assert.AreEqual(count-2, Tasks.Count);
		}


		[Test]
		public void GetChildTasksTest()
		{
			PTMDataset.TasksRow[] childs;
			childs = Tasks.GetChildTasks(Tasks.RootTasksRow);
			Assert.AreEqual(0, childs.Length);
			
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);

			PTMDataset.TasksRow row2;
			row2 = Tasks.NewTasksRow();
			row2.Description = "TaskTest2";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTasksRow(row2);
			
			PTMDataset.TasksRow row3;
			row3 = Tasks.NewTasksRow();
			row3.Description = "TaskTest3";
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTasksRow(row3);
			
			childs = Tasks.GetChildTasks(Tasks.RootTasksRow);
			Assert.AreEqual(2, childs.Length);
			
			childs = Tasks.GetChildTasks(row1);
			Assert.AreEqual(1, childs.Length);
			
			childs = Tasks.GetChildTasks(row2);
			Assert.AreEqual(0, childs.Length);

			childs = Tasks.GetChildTasks(row3);
			Assert.AreEqual(0, childs.Length);

		}
		
		
		[Test]
		public void GetFullPathTest()
		{
			string path;
			path = Tasks.GetFullPath(Tasks.RootTasksRow);
			Assert.AreEqual(@"My Job\", path);
			
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);

			PTMDataset.TasksRow row2;
			row2 = Tasks.NewTasksRow();
			row2.Description = "TaskTest2";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTasksRow(row2);
			
			PTMDataset.TasksRow row3;
			row3 = Tasks.NewTasksRow();
			row3.Description = "TaskTest3";
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTasksRow(row3);
			
			path = Tasks.GetFullPath(Tasks.RootTasksRow);
			Assert.AreEqual(@"My Job\", path);
			
			path = Tasks.GetFullPath(row1);
			Assert.AreEqual(@"My Job\TaskTest1\", path);
			
			path = Tasks.GetFullPath(row2);
			Assert.AreEqual(@"My Job\TaskTest2\", path);

			path = Tasks.GetFullPath(row3);
			Assert.AreEqual(@"My Job\TaskTest1\TaskTest3\", path);
		}
		
		[Test]
		public void IsParentTest()
		{			
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);

			PTMDataset.TasksRow row2;
			row2 = Tasks.NewTasksRow();
			row2.Description = "TaskTest2";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTasksRow(row2);
			
			PTMDataset.TasksRow row3;
			row3 = Tasks.NewTasksRow();
			row3.Description = "TaskTest3";
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTasksRow(row3);
			
			bool result;
			result = Tasks.IsParent(Tasks.RootTasksRow, null);
			Assert.AreEqual(false, result);
			
			result = Tasks.IsParent(null, Tasks.RootTasksRow);
			Assert.AreEqual(false, result);
			
			result = Tasks.IsParent(Tasks.RootTasksRow, row1);
			Assert.AreEqual(true, result);
			
			result = Tasks.IsParent(Tasks.RootTasksRow, row2);
			Assert.AreEqual(true, result);
			
			result = Tasks.IsParent(Tasks.RootTasksRow, row3);
			Assert.AreEqual(true, result);
			
			result = Tasks.IsParent(row1, Tasks.RootTasksRow);
			Assert.AreEqual(false, result);

			result = Tasks.IsParent(row1, row2);
			Assert.AreEqual(false, result);
			
			result = Tasks.IsParent(row1, row3);
			Assert.AreEqual(true, result);
			
			result = Tasks.IsParent(row2, row1);
			Assert.AreEqual(false, result);
			
			result = Tasks.IsParent(row2, row3);
			Assert.AreEqual(false, result);
			
			result = Tasks.IsParent(row3, row1);
			Assert.AreEqual(false, result);
		}
		
		[TearDown]
		public void TearDown()
		{
			Tasks.TasksRowChanged-=new PTM.Data.PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TasksRowDeleting-=new PTM.Data.PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowDeleting);
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
		}

		
		
		private void Tasks_TasksRowChanged(object sender, PTM.Data.PTMDataset.TasksRowChangeEvent e)
		{
			if(e.Action == DataRowAction.Add)
				tasksRowChangedEvent_RowAddedCount++;
			else if(e.Action == DataRowAction.Change)
				this.tasksRowChangedEvent_RowUpdatedCount++;
		}

		private void Tasks_TasksRowDeleting(object sender, PTM.Data.PTMDataset.TasksRowChangeEvent e)
		{
			tasksRowDeletingEventCount++;
		}
	}
}
