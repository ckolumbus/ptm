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
			Assert.IsNotNull(Tasks.RootTask);
			Assert.IsNotNull(Tasks.IdleTask);

			Assert.AreEqual(-1, Tasks.RootTask.ParentId);
		}

		[Test]
		public void InitializeExistingDBTest()
		{
			MainModule.Initialize("test");
			Assert.IsNotNull(Tasks.RootTask);
		}

		[Test]
		public void AddTaskTest()
		{
			int count = Tasks.Count;
			int eventCount = tasksRowChangedEvent_RowAddedCount;
			int id = Tasks.AddTask("AddTaskTest", Tasks.RootTask.Id).Id;
			Assert.AreEqual(count + 1, Tasks.Count);
			Assert.AreEqual(eventCount + 1, tasksRowChangedEvent_RowAddedCount);
			Task addedTask;
			addedTask = Tasks.FindById(id);
			Assert.AreEqual(Tasks.RootTask.Id, addedTask.ParentId);
			Assert.AreEqual("AddTaskTest", addedTask.Description);
			Assert.AreEqual(true, addedTask.IsActive);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Task already exist")]
		public void AddTaskAlreadyExists()
		{
			Tasks.AddTask("AddTaskAlreadyExists", Tasks.RootTask.Id);
			Tasks.AddTask("AddTaskAlreadyExists", Tasks.RootTask.Id);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Description can't be null")]
		public void AddTaskDescripcionNull()
		{
			Tasks.AddTask(null, Tasks.RootTask.Id);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "Description can't be empty")]
		public void AddTaskDescripcionEmpty()
		{
			Tasks.AddTask(string.Empty, Tasks.RootTask.Id);
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
			Task task;
			task = Tasks.AddTask("TaskTest", Tasks.RootTask.Id);
			int count = Tasks.Count;
			int eventCount = tasksRowChangedEvent_RowUpdatedCount;
			task.Description = "UpdatedTaskTest";
			Tasks.UpdateTask(task);
			Assert.AreEqual(count, Tasks.Count);
			Task updatedRow;
			updatedRow = Tasks.FindById(task.Id);
			Assert.AreEqual("UpdatedTaskTest", updatedRow.Description);
			Assert.AreEqual(eventCount + 1, this.tasksRowChangedEvent_RowUpdatedCount);
		}

		
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateRootTaskTest()
		{
			Tasks.UpdateTask(Tasks.RootTask);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateIdleTaskTest()
		{
			Tasks.UpdateTask(Tasks.IdleTask);
		}
		
		[Test]
		public void UpdateParentTask()
		{
			Task task1;
			task1 = Tasks.AddTask("Task1Test", Tasks.RootTask.Id);

			Task task2;
			task2 = Tasks.AddTask("Task2Test", Tasks.RootTask.Id);

			Tasks.UpdateParentTask(task2.Id, task1.Id);
			Task updatedTask;
			updatedTask = Tasks.FindById(task2.Id);

			Assert.AreEqual(task1.Id, updatedTask.ParentId);
		}
		
		[Test]
		public void UpdateParentTaskMergeExistingTask()
		{
			Task task1;
			task1 = Tasks.AddTask("Parent Task",  Tasks.RootTask.Id);
			
			Task task2;
			task2 = Tasks.AddTask("To Be Merged Task", Tasks.RootTask.Id);
			
			Task task3;
			task3 = Tasks.AddTask(task2.Description, task1.Id);

			Log log2 = Logs.AddLog(task2.Id);
			Log log3 = Logs.AddLog(task3.Id);
			
			Tasks.UpdateParentTask(task3.Id, Tasks.RootTask.Id);
			
			Assert.IsNull(Tasks.FindById(task3.Id));//row3 is deleted
			
			Assert.AreEqual(task2.Id, Logs.FindById(log2.Id).TaskId);
			Assert.AreEqual(task2.Id, Logs.FindById(log3.Id).TaskId);//log3 changes task
			
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateParentTaskRootTaskTest()
		{
			Task task;
			task = Tasks.AddTask("Task1Test", Tasks.RootTask.Id);
			Tasks.UpdateParentTask(Tasks.RootTask.Id, task.Id);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be updated.")]
		public void UpdateParentTaskIdleTaskTest()
		{
			Task task;
			task = Tasks.AddTask("Task1Test", Tasks.RootTask.Id);
			Tasks.UpdateParentTask(Tasks.IdleTask.Id, task.Id);
		}

		[Test]
		public void DeleteTaskTest()
		{
			Task task;
			task = Tasks.AddTask("TaskTest", Tasks.RootTask.Id);
			int count = Tasks.Count;
			int eventCount = tasksRowDeletingEventCount;
			Tasks.DeleteTask(task.Id);
			Assert.AreEqual(count - 1, Tasks.Count);
			Assert.AreEqual(eventCount + 1, this.tasksRowDeletingEventCount);
		}

		[Test]
		[
			ExpectedException(typeof (ApplicationException),
				"This task can't be deleted now. You are currently working on it or in a part of it.")]
		public void DeleteCurrentTaskTest()
		{
			Task task;
			task = Tasks.AddTask("TaskTest", Tasks.RootTask.Id);
			Logs.AddLog(task.Id);
			Tasks.DeleteTask(task.Id);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be deleted.")]
		public void DeleteRootTaskTest()
		{
			Tasks.DeleteTask(Tasks.RootTask.Id);
		}
		
		[Test]
		[ExpectedException(typeof (ApplicationException), "This task can't be deleted.")]
		public void DeleteIdleTaskTest()
		{
			Tasks.DeleteTask(Tasks.IdleTask.Id);
		}

		[Test]
		[
			ExpectedException(typeof (ApplicationException),
				"This task can't be deleted now. You are currently working on it or in a part of it.")]
		public void DeleteChildOfCurrentTaskTest()
		{
			Task task1;
			task1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Task task2;
			task2 = Tasks.AddTask("TaskTest2", task1.Id);

			Logs.AddLog(task2.Id);
			Tasks.DeleteTask(task1.Id);
		}

		[Test]
		public void DeleteOnCascadeTaskTest()
		{
			Task task1;
			task1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Tasks.AddTask("TaskTest2", task1.Id);

			int count = Tasks.Count;
			Tasks.DeleteTask(task1.Id);
			Assert.AreEqual(count - 2, Tasks.Count);
			Assert.AreEqual(2, tasksRowDeletingEventCount);
		}


		[Test]
		public void GetChildTasksTest()
		{
			Task[] childs;
			childs = Tasks.GetChildTasks(Tasks.RootTask.Id);
			Assert.AreEqual(1, childs.Length, "Just get the default idle row");

			Task task1;
			task1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Task task2;
			task2 = Tasks.AddTask("TaskTest2", Tasks.RootTask.Id);

			Task task3;
			task3 = Tasks.AddTask("TaskTest3", task1.Id);

			childs = Tasks.GetChildTasks(Tasks.RootTask.Id);
			Assert.AreEqual(2 + 1, childs.Length, "2 plus default idle row");

			childs = Tasks.GetChildTasks(task1.Id);
			Assert.AreEqual(1, childs.Length);

			childs = Tasks.GetChildTasks(task2.Id);
			Assert.AreEqual(0, childs.Length);

			childs = Tasks.GetChildTasks(task3.Id);
			Assert.AreEqual(0, childs.Length);
		}


		[Test]
		public void GetFullPathTest()
		{
			string path;
			path = Tasks.GetFullPath(Tasks.RootTask.Id);
			Assert.AreEqual(String.Empty, path);

			Task task1;
			task1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Task task2;
			task2 = Tasks.AddTask("TaskTest2", Tasks.RootTask.Id);

			Task task3;
			task3 = Tasks.AddTask("TaskTest3", task1.Id);

			path = Tasks.GetFullPath(task1.Id);
			Assert.AreEqual(@"TaskTest1", path);

			path = Tasks.GetFullPath(task2.Id);
			Assert.AreEqual(@"TaskTest2", path);

			path = Tasks.GetFullPath(task3.Id);
			Assert.AreEqual(@"TaskTest1\TaskTest3", path);
		}

		[Test]
		public void IsParentTest()
		{
			Task task1;
			task1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Task task2;
			task2 = Tasks.AddTask("TaskTest2", Tasks.RootTask.Id);

			Task task3;
			task3 = Tasks.AddTask("TaskTest3", task1.Id);

			int result;
			result = Tasks.IsParent(Tasks.RootTask.Id, -1);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(-1, Tasks.RootTask.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(Tasks.RootTask.Id, Tasks.RootTask.Id);
			Assert.AreEqual(0, result);

			result = Tasks.IsParent(Tasks.RootTask.Id, task1.Id);
			Assert.AreEqual(1, result);

			result = Tasks.IsParent(Tasks.RootTask.Id, task2.Id);
			Assert.AreEqual(1, result);

			result = Tasks.IsParent(Tasks.RootTask.Id, task3.Id);
			Assert.AreEqual(2, result);

			result = Tasks.IsParent(task1.Id, Tasks.RootTask.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(task1.Id, task2.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(task1.Id, task3.Id);
			Assert.AreEqual(1, result);

			result = Tasks.IsParent(task2.Id, task1.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(task2.Id, task3.Id);
			Assert.AreEqual(-1, result);

			result = Tasks.IsParent(task3.Id, task1.Id);
			Assert.AreEqual(-1, result);
		}

		[TearDown]
		public void TearDown()
		{
			Logs.StopLogging();
			Tasks.TaskChanged -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TaskDeleting -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleting);
			//DbHelper.DeleteDataSource();
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