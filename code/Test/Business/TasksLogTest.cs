using System;
using System.Data;
using System.Threading;
using NUnit.Framework;
using PTM.Business;
using PTM.Data;

namespace PTM.Test.Business
{
	/// <summary>
	/// Summary description for TasksLogTest.
	/// </summary>
	[TestFixture]
	public class TasksLogTest
	{
		public TasksLogTest()
		{
		}

		
		private int tasksLogRowChangedEvent_RowAddedCount;
		private int tasksLogRowChangedEvent_RowUpdatedCount;
		private int afterStartLoggingCount;
		private int afterStopLogging;
		private int tasksLogDurationCountElapsed;
		
		[SetUp]
		public void SetUp()
		{
			TearDown();
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
			
			tasksLogRowChangedEvent_RowAddedCount = 0;
			tasksLogRowChangedEvent_RowUpdatedCount = 0;
			afterStartLoggingCount = 0;
			afterStopLogging = 0;
			tasksLogDurationCountElapsed = 0;
			
			TasksLog.TasksLogRowChanged+=new PTM.Data.PTMDataset.TasksLogRowChangeEventHandler(TasksLog_TasksLogRowChanged);
			TasksLog.AfterStartLogging+=new EventHandler(TasksLog_AfterStartLogging);
			TasksLog.AfterStopLogging+=new EventHandler(TasksLog_AfterStopLogging);
			TasksLog.TasksLogDurationCountElapsed+=new System.Timers.ElapsedEventHandler(TasksLog_TasksLogDurationCountElapsed);
		}

		[Test]
		public void InitializeTest()
		{
			Assert.AreEqual(0,UnitOfWork.PtmDataset.TasksLog.Count);
			Assert.IsNull(TasksLog.CurrentTaskLog);
		}

		[Test]
		public void AddTasksLogRowTest()
		{
			PTMDataset.TasksRow taskrow;
			taskrow = Tasks.NewTasksRow();
			taskrow.Description = "AddTaskTest";
			taskrow.ParentId = Tasks.RootTasksRow.Id;
			int id = Tasks.AddTasksRow(taskrow);

			PTMDataset.TasksLogRow row;
			row = TasksLog.AddTasksLog(id);
			Assert.AreEqual(id, Tasks.CurrentTaskRow.Id);
			Assert.AreEqual(1, this.tasksLogRowChangedEvent_RowAddedCount);

			PTMDataset.TasksLogRow addedRow;
			addedRow = TasksLog.FindById(row.Id);
			Assert.AreEqual(0, addedRow.Duration);
			Assert.AreEqual(DateTime.Today, addedRow.InsertTime.Date);
			Assert.AreEqual(id, addedRow.TaskId);
		}
		
		[Test]
		public void UpdateTaskLogTest()
		{
			PTMDataset.TasksRow taskrow1;
			taskrow1 = Tasks.NewTasksRow();
			taskrow1.Description = "TaskTest1";
			taskrow1.ParentId = Tasks.RootTasksRow.Id;
			taskrow1.Id = Tasks.AddTasksRow(taskrow1);
			
			PTMDataset.TasksRow taskrow2;
			taskrow2 = Tasks.NewTasksRow();
			taskrow2.Description = "TaskTest2";
			taskrow2.ParentId = Tasks.RootTasksRow.Id;
			taskrow2.Id = Tasks.AddTasksRow(taskrow2);

			PTMDataset.TasksLogRow row;
			row = TasksLog.AddTasksLog(taskrow1.Id);
			
			row.TaskId = taskrow2.Id;
			TasksLog.UpdateTaskLog(row.Id, row.TaskId);
			
			Assert.AreEqual(taskrow2.Id, Tasks.CurrentTaskRow.Id);
			Assert.AreEqual(1, this.tasksLogRowChangedEvent_RowUpdatedCount);
		}

		[Test]
		public void DeleteTaskLogTest()
		{
			PTMDataset.TasksRow taskrow1;
			taskrow1 = Tasks.NewTasksRow();
			taskrow1.Description = "TaskTest1";
			taskrow1.ParentId = Tasks.RootTasksRow.Id;
			taskrow1.Id = Tasks.AddTasksRow(taskrow1);
			
			PTMDataset.TasksLogRow row1;
			row1 = TasksLog.AddTasksLog(taskrow1.Id);
			
			TasksLog.DeleteTaskLog(row1.Id);
			PTMDataset.TasksLogRow deletedRow;
			deletedRow = TasksLog.FindById(row1.Id);
			PTMDataset.TasksRow idleTaskRow;
			idleTaskRow = Tasks.FindById(deletedRow.TaskId);
			Assert.AreEqual(true, idleTaskRow.IsDefaultTask);
			Assert.AreEqual((int)DefaultTask.Idle, idleTaskRow.DefaultTaskId);
			
			TasksLog.UpdateTaskLog(row1.Id, row1.TaskId);
			PTMDataset.TasksLogRow updatedRow;
			updatedRow = TasksLog.FindById(row1.Id);
			Assert.AreEqual(taskrow1.Id, updatedRow.TaskId);
			
			TasksLog.DeleteTaskLog(row1.Id);
			deletedRow = TasksLog.FindById(row1.Id);
			idleTaskRow = Tasks.FindById(deletedRow.TaskId);
			Assert.AreEqual(true, idleTaskRow.IsDefaultTask);
			Assert.AreEqual((int)DefaultTask.Idle, idleTaskRow.DefaultTaskId);
			
		}
		[Test]
		public void LoogingTest()
		{
			TasksLog.StartLogging();
			
			PTMDataset.TasksLogRow row1;
			row1 = TasksLog.AddTasksLog(Tasks.RootTasksRow.Id);
			
			Thread.Sleep(3000);
			
			PTMDataset.TasksLogRow row2;
			row2 = TasksLog.AddTasksLog(Tasks.RootTasksRow.Id);
			
			Thread.Sleep(2000);
			
			TasksLog.StopLogging();
			
			row1 = TasksLog.FindById(row1.Id);
			row2 = TasksLog.FindById(row2.Id);
			
			Assert.IsTrue(row1.Duration==3, row1.Duration.ToString());
			
			Assert.IsTrue(row2.Duration==2, row2.Duration.ToString());
			
			Assert.AreEqual(1, afterStartLoggingCount);
			Assert.AreEqual(1, afterStopLogging);
			Assert.AreEqual(5, tasksLogDurationCountElapsed);
		}
		
		
		[TearDown]
		public void TearDown()
		{
			TasksLog.TasksLogRowChanged-=new PTM.Data.PTMDataset.TasksLogRowChangeEventHandler(TasksLog_TasksLogRowChanged);
			TasksLog.AfterStartLogging-=new EventHandler(TasksLog_AfterStartLogging);
			TasksLog.AfterStopLogging-=new EventHandler(TasksLog_AfterStopLogging);
			TasksLog.TasksLogDurationCountElapsed-=new System.Timers.ElapsedEventHandler(TasksLog_TasksLogDurationCountElapsed);
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
		}

		private void TasksLog_TasksLogRowChanged(object sender, PTM.Data.PTMDataset.TasksLogRowChangeEvent e)
		{
			if(e.Action == DataRowAction.Add)
				tasksLogRowChangedEvent_RowAddedCount++;
			else if(e.Action == DataRowAction.Change)
				tasksLogRowChangedEvent_RowUpdatedCount++;
		}

		private void TasksLog_AfterStartLogging(object sender, EventArgs e)
		{
			afterStartLoggingCount++;
		}

		private void TasksLog_AfterStopLogging(object sender, EventArgs e)
		{
			afterStopLogging++;
		}

		private void TasksLog_TasksLogDurationCountElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			tasksLogDurationCountElapsed++;
		}
	}
}
