using System;
using System.Data;
using NUnit.Framework;
using PTM.Framework;
using PTM.Data;
using PTM.Framework.Infos;

namespace PTM.Test.Framework
{

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
			MainModule.Initialize("test");

			tasksRowChangedEvent_RowAddedCount = 0;
			tasksRowChangedEvent_RowUpdatedCount = 0;
			tasksRowDeletingEventCount = 0;
			Tasks.TaskChanged += new Tasks.TaskChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TaskDeleting += new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleting);
		}

		[Test]
		public void InitializeTest()
		{
			Assert.IsNotNull(Tasks.RootTasksRow);
			Assert.IsNotNull(Tasks.IdleTasksRow);

			Assert.AreEqual(-1, Tasks.RootTasksRow.ParentId);
		}

		[Test]
		public void InitializeExistingDBTest()
		{
			MainModule.Initialize("test");
			Assert.IsNotNull(Tasks.RootTasksRow);
		}

		[Test]
		public void AddTaskTest()
		{
			Task row;
			row = new Task();
			row.Description = "AddTaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.IsActive = true;
			int count = Tasks.Count;
			int eventCount = tasksRowChangedEvent_RowAddedCount;
			int id = Tasks.AddTask(row);
			Assert.AreEqual(count + 1, Tasks.Count);
			Assert.AreEqual(eventCount + 1, tasksRowChangedEvent_RowAddedCount);
			Task addedTaskRow;
			addedTaskRow = Tasks.FindById(id);
			Assert.AreEqual(row.ParentId, addedTaskRow.ParentId);
			Assert.AreEqual("AddTaskTest", addedTaskRow.Description);
			Assert.AreEqual(true, addedTaskRow.IsActive);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Task already exist")]
		public void AddTaskAlreadyExists()
		{
			Task row;
			row = new Task();
			row.Description = "AddTaskAlreadyExists";
			row.ParentId = Tasks.RootTasksRow.Id;
			Tasks.AddTask(row);
			Tasks.AddTask(row);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Description can't be null")]
		public void AddTaskDescripcionNull()
		{
			Task row;
			row = new Task();
			row.ParentId = Tasks.RootTasksRow.Id;
			Tasks.AddTask(row);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Description can't be empty")]
		public void AddTaskDescripcionEmpty()
		{
			Task row;
			row = new Task();
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Description = String.Empty;
			Tasks.AddTask(row);
		}

//		[Test]
//		[ExpectedException(typeof (ApplicationException), "Parent can't be null")]
//		public void AddTaskParentNull()
//		{
//			Task row;
//			row = new Task();
//			row.Description = "Test";
//			Tasks.AddTask(row);
//		}


		[Test]
		public void UpdateTask()
		{
			Task row;
			row = new Task();
			row.Description = "TaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTask(row);
			int count = Tasks.Count;
			int eventCount = tasksRowChangedEvent_RowUpdatedCount;
			row.Description = "UpdatedTaskTest";
			Tasks.UpdateTask(row);
			Assert.AreEqual(count, Tasks.Count);
			Task updatedRow;
			updatedRow = Tasks.FindById(row.Id);
			Assert.AreEqual("UpdatedTaskTest", updatedRow.Description);
			Assert.AreEqual(eventCount + 1, this.tasksRowChangedEvent_RowUpdatedCount);
		}

		
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateRootTaskTest()
		{
			Tasks.UpdateTask(Tasks.RootTasksRow);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateIdleTaskTest()
		{
			Tasks.UpdateTask(Tasks.IdleTasksRow);
		}
		
		[Test]
		public void UpdateParentTask()
		{
			Task row1;
			row1 = new Task();
			row1.Description = "Task1Test";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);

			Task row2;
			row2 = new Task();
			row2.Description = "Task2Test";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTask(row2);

			Tasks.UpdateParentTask(row2.Id, row1.Id);
			Task updatedTask;
			updatedTask = Tasks.FindById(row2.Id);

			Assert.AreEqual(row1.Id, updatedTask.ParentId);
		}
		
		[Test]
		public void UpdateParentTaskMergeExistingTask()
		{
			Task row1;
			row1 = new Task();
			row1.Description = "Parent Task";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);
			
			Task row2;
			row2 = new Task();
			row2.Description = "To Be Merged Task";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTask(row2);
			
			Task row3;
			row3 = new Task();
			row3.Description = row2.Description;
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTask(row3);

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
			Task row1;
			row1 = new Task();
			row1.Description = "Task1Test";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);
			Tasks.UpdateParentTask(Tasks.RootTasksRow.Id, row1.Id);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateParentTaskIdleTaskTest()
		{
			Task row1;
			row1 = new Task();
			row1.Description = "Task1Test";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);
			Tasks.UpdateParentTask(Tasks.IdleTasksRow.Id, row1.Id);
		}

		[Test]
		public void DeleteTaskTest()
		{
			Task row;
			row = new Task();
			row.Description = "TaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTask(row);
			int count = Tasks.Count;
			int eventCount = tasksRowDeletingEventCount;
			Tasks.DeleteTask(row);
			Assert.AreEqual(count - 1, Tasks.Count);
			Assert.AreEqual(eventCount + 1, this.tasksRowDeletingEventCount);
		}

		[Test]
		[
			ExpectedException(typeof (ApplicationException),
				"This task can't be deleted now. You are currently working on it or in a part of it.")]
		public void DeleteCurrentTaskTest()
		{
			Task row;
			row = new Task();
			row.Description = "TaskTest";
			row.ParentId = Tasks.RootTasksRow.Id;
			row.Id = Tasks.AddTask(row);
			Logs.AddLog(row.Id);
			Tasks.DeleteTask(row);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be deleted.")]
		public void DeleteRootTaskTest()
		{
			Tasks.DeleteTask(Tasks.RootTasksRow);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be deleted.")]
		public void DeleteIdleTaskTest()
		{
			Tasks.DeleteTask(Tasks.IdleTasksRow);
		}

		[Test]
		[
			ExpectedException(typeof (ApplicationException),
				"This task can't be deleted now. You are currently working on it or in a part of it.")]
		public void DeleteChildOfCurrentTaskTest()
		{
			Task row1;
			row1 = new Task();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);

			Task row2;
			row2 = new Task();
			row2.Description = "TaskTest2";
			row2.ParentId = row1.Id;
			row2.Id = Tasks.AddTask(row2);

			Logs.AddLog(row2.Id);
			Tasks.DeleteTask(row1);
		}

		[Test]
		public void DeleteOnCascadeTaskTest()
		{
			Task row1;
			row1 = new Task();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);

			Task row2;
			row2 = new Task();
			row2.Description = "TaskTest2";
			row2.ParentId = row1.Id;
			row2.Id = Tasks.AddTask(row2);

			int count = Tasks.Count;
			Tasks.DeleteTask(row1);
			Assert.AreEqual(count - 2, Tasks.Count);
			Assert.AreEqual(2, tasksRowDeletingEventCount);
		}


		[Test]
		public void GetChildTasksTest()
		{
			Task[] childs;
			childs = Tasks.GetChildTasks(Tasks.RootTasksRow.Id);
			Assert.AreEqual(1, childs.Length, "Just get the default idle row");

			Task row1;
			row1 = new Task();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);

			Task row2;
			row2 = new Task();
			row2.Description = "TaskTest2";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTask(row2);

			Task row3;
			row3 = new Task();
			row3.Description = "TaskTest3";
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTask(row3);

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

			Task row1;
			row1 = new Task();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);

			Task row2;
			row2 = new Task();
			row2.Description = "TaskTest2";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTask(row2);

			Task row3;
			row3 = new Task();
			row3.Description = "TaskTest3";
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTask(row3);

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
			Task row1;
			row1 = new Task();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTask(row1);

			Task row2;
			row2 = new Task();
			row2.Description = "TaskTest2";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTask(row2);

			Task row3;
			row3 = new Task();
			row3.Description = "TaskTest3";
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTask(row3);

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
			Tasks.TaskChanged -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TaskDeleting -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleting);
			DbHelper.DeleteDataSource();
		}

		private void Tasks_TasksRowChanged(Tasks.TaskChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Add)
				tasksRowChangedEvent_RowAddedCount++;
			else if (e.Action == DataRowAction.Change)
				this.tasksRowChangedEvent_RowUpdatedCount++;
		}

		private void Tasks_TasksRowDeleting(Tasks.TaskChangeEventArgs e)
		{
			tasksRowDeletingEventCount++;
		}
	}
}