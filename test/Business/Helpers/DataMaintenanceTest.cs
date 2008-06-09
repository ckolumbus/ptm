using System;
using System.Collections;
using NUnit.Framework;
using PTM.Framework;
using PTM.Framework.Helpers;
using PTM.Data;
using PTM.Framework.Infos;

namespace PTM.Test.Framework.Helpers
{
	/// <summary>
	/// Summary description for DataMaintenanceTest.
	/// </summary>
	[TestFixture]
	public class DataMaintenanceTest
	{
		public DataMaintenanceTest()
		{
		}

		[SetUp]
		public void SetUp()
		{
			DbHelper.Initialize("test");
			DbHelper.DeleteDataSource();
			MainModule.Initialize("test");
		}

		[Test]
		public void DeleteIdleEntriesTest()
		{
			//Make task tree
			int taskId1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id).Id;

			int taskId2 = Tasks.AddTask("TaskTest2", Tasks.RootTask.Id).Id;

			int taskId3 = Tasks.AddTask("TaskTest3", taskId1).Id;

//			int rootchilddefaultId = Tasks.AddDeafultTask(Tasks.RootTasksRow.Id, DefaultTasks.GetDefaultTask(2).DefaultTaskId);
//			int task1childdefaultId = Tasks.AddDeafultTask(task1.Id, DefaultTasks.GetDefaultTask(3).DefaultTaskId);
//			int task2childdefaultId = Tasks.AddDeafultTask(task2.Id, DefaultTasks.GetDefaultTask(4).DefaultTaskId);
//			int task3childdefaultId = Tasks.AddDeafultTask(task3.Id, DefaultTasks.GetDefaultTask(5).DefaultTaskId);

//			int rootchildidleId = Tasks.AddDeafultTask(Tasks.RootTasksRow.Id, DefaultTasks.IdleTaskId);
//			int task1childidleId = Tasks.AddDeafultTask(task1.Id, DefaultTasks.IdleTaskId);
//			int task2childidleId = Tasks.AddDeafultTask(task2.Id, DefaultTasks.IdleTaskId);
//			int task3childidleId = Tasks.AddDeafultTask(task3.Id, DefaultTasks.IdleTaskId);

			int duration = (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value*60;
			
			//Add logs (DataMaintenanceDays + one week logeed days)
			for (int i = 0; i <= (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value + 7; i++)
			{
				InsertLog(taskId1, DateTime.Today.AddDays(-i), duration);
				InsertLog(taskId2, DateTime.Today.AddDays(-i).AddSeconds(duration), duration);
				InsertLog(taskId3, DateTime.Today.AddDays(-i).AddSeconds(duration*2), duration);
//				InsertLog(rootchilddefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*3), duration);
//				InsertLog(task1childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*4), duration);
//				InsertLog(task2childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*5), duration);
//				InsertLog(task3childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*6), duration);

				InsertLog(Tasks.IdleTask.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*3), duration);
				InsertLog(Tasks.IdleTask.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*4), duration);
				InsertLog(Tasks.IdleTask.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*5), duration);

//				if (i > (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value)
//					InsertLog(Tasks.IdleTask.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*6), duration);
			}

			DataMaintenanceHelper.DeleteIdleEntries();

			//validate
			for (int i = 0; i <= (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value + 7; i++)
			{
				ArrayList logs = Logs.GetLogsByDay(DateTime.Today.AddDays(-i));
				if (i > (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value)
				{
					Assert.AreEqual(3, logs.Count, "i=" + i.ToString());
				}
				else
				{
					Assert.AreEqual(6, logs.Count, "i=" + i.ToString());
				}
			}
		}

		public static void InsertLog(int taskId, DateTime insertTime, int duration)
		{
			DbHelper.ExecuteNonQuery("INSERT INTO TasksLog (TaskId, Duration, InsertTime) values (?, ?, ?)",
			                         new string[] {"TaskId", "Duration", "InsertTime"},
			                         new object[] {taskId, duration, insertTime});
		}


		[Test]
		public void GroupLogsTest()
		{
			//Make task tree
			int taskId1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id).Id;

			int taskId2 = Tasks.AddTask("TaskTest2", Tasks.RootTask.Id).Id;

			int taskId3 = Tasks.AddTask("TaskTest3", taskId1).Id;

//			int rootchilddefaultId = Tasks.AddDeafultTask(Tasks.RootTasksRow.Id, DefaultTasks.GetDefaultTask(2).DefaultTaskId);
//			int task1childdefaultId = Tasks.AddDeafultTask(task1.Id, DefaultTasks.GetDefaultTask(3).DefaultTaskId);
//			int task2childdefaultId = Tasks.AddDeafultTask(task2.Id, DefaultTasks.GetDefaultTask(4).DefaultTaskId);
//			int task3childdefaultId = Tasks.AddDeafultTask(task3.Id, DefaultTasks.GetDefaultTask(5).DefaultTaskId);

			int duration = (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value*60;
			
			//Add logs
			for (int i = 0; i <= (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value + 7; i++)
			{
				InsertLog(taskId1, DateTime.Today.AddDays(-i), duration);
				InsertLog(taskId1, DateTime.Today.AddDays(-i).AddSeconds(duration), duration);
				InsertLog(taskId1, DateTime.Today.AddDays(-i).AddSeconds(duration*2), duration);
				InsertLog(taskId1, DateTime.Today.AddDays(-i).AddSeconds(duration*3), duration);

				InsertLog(taskId2, DateTime.Today.AddDays(-i).AddSeconds(duration*4), duration);

				InsertLog(taskId3, DateTime.Today.AddDays(-i).AddSeconds(duration*5), duration);
				InsertLog(taskId3, DateTime.Today.AddDays(-i).AddSeconds(duration*6), duration);

//				InsertLog(rootchilddefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*7), duration);
//				InsertLog(rootchilddefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*8), duration);
//				InsertLog(rootchilddefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*9), duration);
//
//				InsertLog(task1childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*10), duration);
//
//				InsertLog(task2childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*11), duration);
//				InsertLog(task2childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*12), duration);
//
//				InsertLog(task3childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*13), duration);
			}

			DataMaintenanceHelper.GroupLogs(false);

			//validate
			for (int i = 0; i <= (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value + 7; i++)
			{
				ArrayList logs = Logs.GetLogsByDay(DateTime.Today.AddDays(-i));
				if (i > (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value)
				{
					Assert.AreEqual(3, logs.Count, "i=" + i.ToString());
				}
				else
				{
					Assert.AreEqual(7, logs.Count, "i=" + i.ToString());
				}
			}
		}

		[TearDown]
		public void TearDown()
		{
			Logs.StopLogging();
			DbHelper.DeleteDataSource();
		}
	}
}