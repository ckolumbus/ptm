using System;
using System.Collections;
using System.Data;
using System.Threading;
using System.Timers;
using NUnit.Framework;
using PTM.Framework;
using PTM.Data;
using PTM.Framework.Infos;
using PTM.Test.Framework.Helpers;

namespace PTM.Test.Framework
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
			//TearDown();
			DbHelper.Initialize("test");
			DbHelper.DeleteDataSource();
			MainModule.Initialize("test");

			tasksLogRowChangedEvent_RowAddedCount = 0;
			tasksLogRowChangedEvent_RowUpdatedCount = 0;
			afterStartLoggingCount = 0;
			afterStopLogging = 0;
			tasksLogDurationCountElapsed = 0;

			Logs.LogChanged += new Logs.LogChangeEventHandler(TasksLog_LogChanged);
			Logs.AfterStartLogging += new EventHandler(TasksLog_AfterStartLogging);
			Logs.AfterStopLogging += new EventHandler(TasksLog_AfterStopLogging);
			Logs.CurrentLogDurationChanged += new ElapsedEventHandler(TasksLog_TasksLogDurationCountElapsed);
		}

		[Test]
		public void InitializeTest()
		{
			Assert.IsNull(Logs.CurrentLog);
		}

		[Test]
		public void AddTasksLogRowTest()
		{
			int id = Tasks.AddTask("AddTaskTest", Tasks.RootTask.Id).Id;

			Log log;
			log = Logs.AddLog(id);
			Assert.AreEqual(id, Tasks.CurrentTask.Id);
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

			int taskId1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id).Id;

			int taskId2 = Tasks.AddTask("TaskTest2", Tasks.RootTask.Id).Id;

			Log logRow;
			logRow = Logs.AddLog(taskId1);

			logRow.TaskId = taskId2;
			Logs.UpdateLogTaskId(logRow.Id, logRow.TaskId);

			Assert.AreEqual(taskId2, Tasks.CurrentTask.Id);
			Assert.AreEqual(1, this.tasksLogRowChangedEvent_RowUpdatedCount);
		}
		
	
		[Test]
		public void DeleteTaskLogTest()
		{
			Task taskrow1;
			taskrow1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Log log1;
			log1 = Logs.AddLog(taskrow1.Id);

			Logs.DeleteLog(log1.Id);
			Log deletedLog;
			deletedLog = Logs.FindById(log1.Id);
			Task idleTaskRow;
			idleTaskRow = Tasks.FindById(deletedLog.TaskId);
			Assert.AreEqual(Tasks.IdleTask.Id, idleTaskRow.Id);

			Logs.UpdateLogTaskId(log1.Id, taskrow1.Id);
			Log updatedRow;
			updatedRow = Logs.FindById(log1.Id);
			Assert.AreEqual(taskrow1.Id, updatedRow.TaskId);

			Logs.DeleteLog(log1.Id);
			deletedLog = Logs.FindById(log1.Id);
			idleTaskRow = Tasks.FindById(deletedLog.TaskId);
			Assert.AreEqual(Tasks.IdleTask.Id, idleTaskRow.Id);
		}

		[Test]
		public void LoogingTest()
		{
			Logs.StartLogging();

			Log row1;
			row1 = Logs.AddLog(Tasks.RootTask.Id);

			Thread.Sleep(3000);

			Log row2;
			row2 = Logs.AddLog(Tasks.RootTask.Id);

			Thread.Sleep(2000);

			Logs.StopLogging();

			row1 = Logs.FindById(row1.Id);
			row2 = Logs.FindById(row2.Id);

			Assert.IsTrue(row1.Duration >= 3, row1.Duration.ToString());

			Assert.IsTrue(row2.Duration >= 2, row2.Duration.ToString());

			Assert.AreEqual(1, afterStartLoggingCount);
			Assert.AreEqual(1, afterStopLogging);
			Assert.IsTrue(tasksLogDurationCountElapsed >= 5);
		}

		[Test]
		public void GetLogsByDayTest()
		{
			Task taskrow1;
			taskrow1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);

			ArrayList list = Logs.GetLogsByDay(DateTime.Today);
			Assert.AreEqual(5, list.Count);
		}

		[Test]
		public void GetLogsByTaskTest()
		{
			Task taskrow1;
			taskrow1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);

			ArrayList list = Logs.GetLogsByTask(taskrow1.Id);
			Assert.AreEqual(5, list.Count);
		}
		
		[Test]
		public void ChangeLogsTaskIdTest()
		{
			Task taskrow1;
			taskrow1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id);

			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			Logs.AddLog(taskrow1.Id);
			
			Task taskrow2;
			taskrow2 = Tasks.AddTask("TaskTest2", Tasks.RootTask.Id);
			
			Logs.ChangeLogsTaskId(taskrow1.Id, taskrow2.Id);
			
			ArrayList list = Logs.GetLogsByTask(taskrow2.Id);
			Assert.AreEqual(5, list.Count);
			
			list = Logs.GetLogsByTask(taskrow1.Id);
			Assert.AreEqual(0, list.Count);
		}

        [Test]
        public void GetTaskLogDateRangeTest()
        {
            Task task = Tasks.AddTask("GetTaskLogDateRangeTest1", Tasks.RootTask.Id);

            DateRange range;
            range = Logs.GetTaskLogDateRange(task.Id);
            Assert.AreEqual(DateTime.MaxValue, range.StartDate);
            Assert.AreEqual(DateTime.MinValue, range.EndDate);

            //just one entry
            DateTime curTime = DateTime.Today.AddDays(-1);
            DataMaintenanceTest.InsertLog(task.Id, curTime, 10);
            range = Logs.GetTaskLogDateRange(task.Id);
            Assert.AreEqual(curTime, range.StartDate);
            Assert.AreEqual(curTime, range.EndDate);
            
            //range
            DateTime startDate = DateTime.Today.AddDays(-5);
            DateTime endDate = DateTime.Today;
            DataMaintenanceTest.InsertLog(task.Id,startDate, 10);
            DataMaintenanceTest.InsertLog(task.Id, endDate, 10);
            range = Logs.GetTaskLogDateRange(task.Id);
            Assert.AreEqual(startDate, range.StartDate);
            Assert.AreEqual(endDate, range.EndDate);
            
            //child range
            Task child1 = Tasks.AddTask("GetTaskLogDateRangeTest2", task.Id);
            Task child2 = Tasks.AddTask("GetTaskLogDateRangeTest3", task.Id);
            Task child3 = Tasks.AddTask("GetTaskLogDateRangeTest4", child1.Id);
            startDate = DateTime.Today.AddDays(-9);
            endDate = DateTime.Today.AddDays(2);
            DataMaintenanceTest.InsertLog(child3.Id, startDate, 10);
            DataMaintenanceTest.InsertLog(child2.Id, endDate, 10);
            DataMaintenanceTest.InsertLog(child1.Id, DateTime.Today.AddDays(-2), 10);
            range = Logs.GetTaskLogDateRange(task.Id);
            Assert.AreEqual(startDate, range.StartDate);
            Assert.AreEqual(endDate, range.EndDate);
        }
		
		[TearDown]
		public void TearDown()
		{
			Logs.StopLogging();
			Logs.LogChanged -= new Logs.LogChangeEventHandler(TasksLog_LogChanged);
			Logs.AfterStartLogging -= new EventHandler(TasksLog_AfterStartLogging);
			Logs.AfterStopLogging -= new EventHandler(TasksLog_AfterStopLogging);
			Logs.CurrentLogDurationChanged -= new ElapsedEventHandler(TasksLog_TasksLogDurationCountElapsed);
			//DbHelper.DeleteDataSource();
		}

		private void TasksLog_AfterStartLogging(object sender, EventArgs e)
		{
			afterStartLoggingCount++;
		}

		private void TasksLog_AfterStopLogging(object sender, EventArgs e)
		{
			afterStopLogging++;
		}

		private void TasksLog_TasksLogDurationCountElapsed(object sender, ElapsedEventArgs e)
		{
			tasksLogDurationCountElapsed++;
		}

		private void TasksLog_LogChanged(Logs.LogChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Add)
				tasksLogRowChangedEvent_RowAddedCount++;
			else if (e.Action == DataRowAction.Change)
				tasksLogRowChangedEvent_RowUpdatedCount++;
		}
	}
}