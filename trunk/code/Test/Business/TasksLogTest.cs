using System;
using System.Collections;
using System.Data;
using System.Threading;
using NUnit.Framework;
using PTM.Business;
using PTM.Data;
using PTM.Infos;

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
			
			Logs.LogChanged+=new PTM.Business.Logs.LogChangeEventHandler(TasksLog_LogChanged);
			Logs.AfterStartLogging+=new EventHandler(TasksLog_AfterStartLogging);
			Logs.AfterStopLogging+=new EventHandler(TasksLog_AfterStopLogging);
			Logs.TasksLogDurationCountElapsed+=new System.Timers.ElapsedEventHandler(TasksLog_TasksLogDurationCountElapsed);
		}

		[Test]
		public void InitializeTest()
		{
			Assert.IsNull(Logs.CurrentLog);
		}

		[Test]
		public void AddTasksLogRowTest()
		{
			PTMDataset.TasksRow taskrow;
			taskrow = Tasks.NewTasksRow();
			taskrow.Description = "AddTaskTest";
			taskrow.ParentId = Tasks.RootTasksRow.Id;
			int id = Tasks.AddTasksRow(taskrow);

			Log log;
			log = Logs.AddLog(id);
			Assert.AreEqual(id, Tasks.CurrentTaskRow.Id);
			Assert.AreEqual(1, this.tasksLogRowChangedEvent_RowAddedCount);

			Log addedLog;
			addedLog = Logs.FindById(log.Id);
			Assert.AreEqual(0, addedLog.Duration);
			Assert.AreEqual(DateTime.Today, addedLog.InsertTime.Date);
			Assert.AreEqual(id, addedLog.TaskId);
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

			Log logRow;
			logRow = Logs.AddLog(taskrow1.Id);
			
			logRow.TaskId = taskrow2.Id;
			Logs.UpdateLogTaskId(logRow.Id, logRow.TaskId);
			
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
			
			Log log1;
			log1 = Logs.AddLog(taskrow1.Id);
			
			Logs.DeleteLog(log1.Id);
			Log deletedLog;
			deletedLog = Logs.FindById(log1.Id);
			PTMDataset.TasksRow idleTaskRow;
			idleTaskRow = Tasks.FindById(deletedLog.TaskId);
			Assert.AreEqual(true, idleTaskRow.IsDefaultTask);
			Assert.AreEqual((int)DefaultTask.Idle, idleTaskRow.DefaultTaskId);
			
			Logs.UpdateLogTaskId(log1.Id, taskrow1.Id);
			Log updatedRow;
			updatedRow = Logs.FindById(log1.Id);
			Assert.AreEqual(taskrow1.Id, updatedRow.TaskId);
			
			Logs.DeleteLog(log1.Id);
			deletedLog = Logs.FindById(log1.Id);
			idleTaskRow = Tasks.FindById(deletedLog.TaskId);
			Assert.AreEqual(true, idleTaskRow.IsDefaultTask);
			Assert.AreEqual((int)DefaultTask.Idle, idleTaskRow.DefaultTaskId);
			
		}
		[Test]
		public void AddDefaultTaskLogTest()
		{
			Log log = Logs.AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.CheckingMail);
			PTMDataset.TasksRow task;
			task = Tasks.FindById(log.TaskId);
			Assert.AreEqual(true, task.IsDefaultTask);
			Assert.AreEqual((int)DefaultTask.CheckingMail, task.DefaultTaskId);
			log = Logs.AddDefaultTaskLog(Tasks.RootTasksRow.Id, DefaultTask.CheckingMail);
			task = Tasks.FindById(log.TaskId);
			Assert.AreEqual(true, task.IsDefaultTask);
			Assert.AreEqual((int)DefaultTask.CheckingMail, task.DefaultTaskId);
		}
		
		[Test]
		public void LoogingTest()
		{
			Logs.StartLogging();
			
			Log row1;
			row1 = Logs.AddLog(Tasks.RootTasksRow.Id);
			
			Thread.Sleep(3000);
			
			Log row2;
			row2 = Logs.AddLog(Tasks.RootTasksRow.Id);
			
			Thread.Sleep(2000);
			
			Logs.StopLogging();
			
			row1 = Logs.FindById(row1.Id);
			row2 = Logs.FindById(row2.Id);
			
			Assert.IsTrue(row1.Duration>=3, row1.Duration.ToString());
			
			Assert.IsTrue(row2.Duration>=2, row2.Duration.ToString());
			
			Assert.AreEqual(1, afterStartLoggingCount);
			Assert.AreEqual(1, afterStopLogging);
			Assert.IsTrue(tasksLogDurationCountElapsed>=5);
		}
		
		[Test]
		public void GetLogsByDayTest()
		{
			PTMDataset.TasksRow taskrow1;
			taskrow1 = Tasks.NewTasksRow();
			taskrow1.Description = "TaskTest1";
			taskrow1.ParentId = Tasks.RootTasksRow.Id;
			taskrow1.Id = Tasks.AddTasksRow(taskrow1);
			
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			
			ArrayList list = Logs.GetLogsByDay(DateTime.Today);
			Assert.AreEqual(5, list.Count);
			
		}
		
		[TearDown]
		public void TearDown()
		{
			Logs.LogChanged-=new PTM.Business.Logs.LogChangeEventHandler(TasksLog_LogChanged);
			Logs.AfterStartLogging-=new EventHandler(TasksLog_AfterStartLogging);
			Logs.AfterStopLogging-=new EventHandler(TasksLog_AfterStopLogging);
			Logs.TasksLogDurationCountElapsed-=new System.Timers.ElapsedEventHandler(TasksLog_TasksLogDurationCountElapsed);
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
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

		private void TasksLog_LogChanged(PTM.Business.Logs.LogChangeEventArgs e)
		{
			if(e.Action == DataRowAction.Add)
				tasksLogRowChangedEvent_RowAddedCount++;
			else if(e.Action == DataRowAction.Change)
				tasksLogRowChangedEvent_RowUpdatedCount++;
		}
	}
}
