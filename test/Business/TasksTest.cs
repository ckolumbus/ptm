using System;
using System.Data;
using NUnit.Framework;
using PTM.Framework;
using PTM.Data;
using PTM.Framework.Infos;

namespace PTM.Test.Business
{
	/// <summary>
	/// Descripci�n breve de Tasks.
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
			DbHelper.Initialize("test");
			DbHelper.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");

			tasksRowChangedEvent_RowAddedCount = 0;
			tasksRowChangedEvent_RowUpdatedCount = 0;
			tasksRowDeletingEventCount = 0;
			Tasks.TasksRowChanged += new PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TasksRowDeleting += new PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowDeleting);
		}

		[Test]
		public void InitializeTest()
		{
			Assert.AreEqual(2, UnitOfWork.PtmDataset.Tasks.Count);
			Assert.IsNotNull(Tasks.RootTasksRow);
		}

		[Test]
		public void InitializeExistingDBTest()
		{
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
			Assert.IsNotNull(Tasks.RootTasksRow);
		}

		[Test]
		public void AddTaskTest()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "AddTaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.IsActive = true;
			int count = Tasks.Count;
			int eventCount = tasksRowChangedEvent_RowAddedCount;
			int id = Tasks.AddTasksRow(row);
			Assert.AreEqual(count + 1, Tasks.Count);
			Assert.AreEqual(eventCount + 1, tasksRowChangedEvent_RowAddedCount);
			PTMDataset.TasksRow addedTaskRow;
			addedTaskRow = Tasks.FindById(id);
			Assert.AreEqual(row.ParentId, addedTaskRow.ParentId);
			Assert.AreEqual("AddTaskTest", addedTaskRow.Description);
			Assert.AreEqual(0, addedTaskRow.TotalTime);
			Assert.AreEqual(false, addedTaskRow.IsFinished);
			Assert.AreEqual(true, addedTaskRow.IsActive);
			Assert.AreEqual(true, addedTaskRow.IsStartDateNull());
			Assert.AreEqual(true, addedTaskRow.IsStopDateNull());
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Task already exist")]
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
		[ExpectedException(typeof (ApplicationException), "Description can't be null")]
		public void AddTaskDescripcionNull()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.ParentId = Tasks.RootTasksRow.Id;
			Tasks.AddTasksRow(row);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Description can't be empty")]
		public void AddTaskDescripcionEmpty()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Description = String.Empty;
			Tasks.AddTasksRow(row);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Parent can't be null")]
		public void AddTaskParentNull()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "Test";
			Tasks.AddTasksRow(row);
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
			Assert.AreEqual(eventCount + 1, this.tasksRowChangedEvent_RowUpdatedCount);
		}

		
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateRootTaskTest()
		{
			Tasks.UpdateTaskRow(Tasks.RootTasksRow);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateIdleTaskTest()
		{
			Tasks.UpdateTaskRow(Tasks.IdleTasksRow);
		}
		
		[Test]
		public void UpdateParentTask()
		{
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "Task1Test";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);

			PTMDataset.TasksRow row2;
			row2 = Tasks.NewTasksRow();
			row2.Description = "Task2Test";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTasksRow(row2);

			Tasks.UpdateParentTask(row2.Id, row1.Id);
			PTMDataset.TasksRow updatedTask;
			updatedTask = Tasks.FindById(row2.Id);

			Assert.AreEqual(row1.Id, updatedTask.ParentId);
		}
		
		[Test]
		public void UpdateParentTaskMergeExistingTask()
		{
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "Parent Task";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);
			
			PTMDataset.TasksRow row2;
			row2 = Tasks.NewTasksRow();
			row2.Description = "To Be Merged Task";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTasksRow(row2);
			
			PTMDataset.TasksRow row3;
			row3 = Tasks.NewTasksRow();
			row3.Description = row2.Description;
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTasksRow(row3);

			Log log2 = Logs.AddLog(row2.Id);
			Log log3 = Logs.AddLog(row3.Id);
			
			
			
			Tasks.UpdateParentTask(row3.Id, Tasks.RootTasksRow.Id);
			
			Assert.IsNull(Tasks.FindById(row3.Id));//row3 is deleted
			
			Assert.AreEqual(row2.Id, Logs.FindById(log2.Id).TaskId);
			Assert.AreEqual(row2.Id, Logs.FindById(log3.Id).TaskId);//log3 changes task
			
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateParentTaskRootTaskTest()
		{
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "Task1Test";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);
			Tasks.UpdateParentTask(Tasks.RootTasksRow.Id, row1.Id);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateParentTaskIdleTaskTest()
		{
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "Task1Test";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);
			Tasks.UpdateParentTask(Tasks.IdleTasksRow.Id, row1.Id);
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
			Assert.AreEqual(count - 1, Tasks.Count);
			Assert.AreEqual(eventCount + 1, this.tasksRowDeletingEventCount);
		}

		[Test]
		[
			ExpectedException(typeof (ApplicationException),
				"This task can't be deleted now. You are currently working on it or in a part of it.")]
		public void DeleteCurrentTaskTest()
		{
			PTMDataset.TasksRow row;
			row = Tasks.NewTasksRow();
			row.Description = "TaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTasksRow(row);
			Logs.AddLog(row.Id);
			Tasks.DeleteTaskRow(row);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be deleted.")]
		public void DeleteRootTaskTest()
		{
			Tasks.DeleteTaskRow(Tasks.RootTasksRow);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be deleted.")]
		public void DeleteIdleTaskTest()
		{
			Tasks.DeleteTaskRow(Tasks.IdleTasksRow);
		}

		[Test]
		[
			ExpectedException(typeof (ApplicationException),
				"This task can't be deleted now. You are currently working on it or in a part of it.")]
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

			Logs.AddLog(row2.Id);
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
			Assert.AreEqual(count - 2, Tasks.Count);
			Assert.AreEqual(2, tasksRowDeletingEventCount);
		}


		[Test]
		public void GetChildTasksTest()
		{
			PTMDataset.TasksRow[] childs;
			childs = Tasks.GetChildTasks(Tasks.RootTasksRow.Id);
			Assert.AreEqual(1, childs.Length, "Just get the default idle row");

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

			childs = Tasks.GetChildTasks(Tasks.RootTasksRow.Id);
			Assert.AreEqual(2 + 1, childs.Length, "2 plus default idle row");

			childs = Tasks.GetChildTasks(row1.Id);
			Assert.AreEqual(1, childs.Length);

			childs = Tasks.GetChildTasks(row2.Id);
			Assert.AreEqual(0, childs.Length);

			childs = Tasks.GetChildTasks(row3.Id);
			Assert.AreEqual(0, childs.Length);
		}


		[Test]
		public void GetFullPathTest()
		{
			string path;
			path = Tasks.GetFullPath(Tasks.RootTasksRow.Id);
			Assert.AreEqual(String.Empty, path);

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

			path = Tasks.GetFullPath(row1.Id);
			Assert.AreEqual(@"TaskTest1", path);

			path = Tasks.GetFullPath(row2.Id);
			Assert.AreEqual(@"TaskTest2", path);

			path = Tasks.GetFullPath(row3.Id);
			Assert.AreEqual(@"TaskTest1\TaskTest3", path);
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

			int result;
			result = Tasks.IsParent(Tasks.RootTasksRow.Id, -1);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(-1, Tasks.RootTasksRow.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(Tasks.RootTasksRow.Id, Tasks.RootTasksRow.Id);
			Assert.AreEqual(0, result);

			result = Tasks.IsParent(Tasks.RootTasksRow.Id, row1.Id);
			Assert.AreEqual(1, result);

			result = Tasks.IsParent(Tasks.RootTasksRow.Id, row2.Id);
			Assert.AreEqual(1, result);

			result = Tasks.IsParent(Tasks.RootTasksRow.Id, row3.Id);
			Assert.AreEqual(2, result);

			result = Tasks.IsParent(row1.Id, Tasks.RootTasksRow.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(row1.Id, row2.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(row1.Id, row3.Id);
			Assert.AreEqual(1, result);

			result = Tasks.IsParent(row2.Id, row1.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(row2.Id, row3.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(row3.Id, row1.Id);
			Assert.AreEqual(-1, result);
		}

		[TearDown]
		public void TearDown()
		{
			Logs.StopLogging();
			Tasks.TasksRowChanged -= new PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TasksRowDeleting -= new PTMDataset.TasksRowChangeEventHandler(Tasks_TasksRowDeleting);
			DbHelper.DeleteDataSource();
		}

		private void Tasks_TasksRowChanged(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
				tasksRowChangedEvent_RowAddedCount++;
			else if (e.Action == DataRowAction.Change)
				this.tasksRowChangedEvent_RowUpdatedCount++;
		}

		private void Tasks_TasksRowDeleting(object sender, PTMDataset.TasksRowChangeEvent e)
		{
			tasksRowDeletingEventCount++;
		}
	}
}