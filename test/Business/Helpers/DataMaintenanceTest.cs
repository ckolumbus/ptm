using System;
using System.Collections;
using NUnit.Framework;
using PTM.Framework;
using PTM.Framework.Helpers;
using PTM.Data;

namespace PTM.Test.Business.Helpers
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
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
		}

		[Test]
		public void DeleteIdleEntriesTest()
		{
			//Make task tree
			PTMDataset.TasksRow task1;
			task1 = Tasks.NewTasksRow();
			task1.Description = "TaskTest1";
			task1.ParentId = Tasks.RootTasksRow.Id;
			task1.Id = Tasks.AddTasksRow(task1);

			PTMDataset.TasksRow task2;
			task2 = Tasks.NewTasksRow();
			task2.Description = "TaskTest2";
			task2.ParentId = Tasks.RootTasksRow.Id;
			task2.Id = Tasks.AddTasksRow(task2);

			PTMDataset.TasksRow task3;
			task3 = Tasks.NewTasksRow();
			task3.Description = "TaskTest3";
			task3.ParentId = task1.Id;
			task3.Id = Tasks.AddTasksRow(task3);

			int rootchilddefaultId = Tasks.AddDeafultTask(Tasks.RootTasksRow.Id, DefaultTaskEnum.OtherPersonal);
			int task1childdefaultId = Tasks.AddDeafultTask(task1.Id, DefaultTaskEnum.LunchTime);
			int task2childdefaultId = Tasks.AddDeafultTask(task2.Id, DefaultTaskEnum.CheckingJobMail);
			int task3childdefaultId = Tasks.AddDeafultTask(task3.Id, DefaultTaskEnum.JobMeeting);

			int rootchildidleId = Tasks.AddDeafultTask(Tasks.RootTasksRow.Id, DefaultTaskEnum.Idle);
			int task1childidleId = Tasks.AddDeafultTask(task1.Id, DefaultTaskEnum.Idle);
			int task2childidleId = Tasks.AddDeafultTask(task2.Id, DefaultTaskEnum.Idle);
			int task3childidleId = Tasks.AddDeafultTask(task3.Id, DefaultTaskEnum.Idle);

			int duration = (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value*60;
			
			//Add logs
			for (int i = 0; i <= (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value + 7; i++)
			{
				InsertLog(task1.Id, DateTime.Today.AddDays(-i), duration);
				InsertLog(task2.Id, DateTime.Today.AddDays(-i).AddSeconds(duration), duration);
				InsertLog(task3.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*2), duration);
				InsertLog(rootchilddefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*3), duration);
				InsertLog(task1childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*4), duration);
				InsertLog(task2childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*5), duration);
				InsertLog(task3childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*6), duration);

				InsertLog(rootchildidleId, DateTime.Today.AddDays(-i).AddSeconds(duration*7), duration);
				InsertLog(task1childidleId, DateTime.Today.AddDays(-i).AddSeconds(duration*8), duration);
				InsertLog(task2childidleId, DateTime.Today.AddDays(-i).AddSeconds(duration*9), duration);

				if (i > (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value)
					InsertLog(task3childidleId, DateTime.Today.AddDays(-i).AddSeconds(duration*10), duration);
			}

			DataMaintenanceHelper.DeleteIdleEntries();

			//validate
			for (int i = 0; i <= (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value + 7; i++)
			{
				ArrayList logs = Logs.GetLogsByDay(DateTime.Today.AddDays(-i));
				if (i > (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value)
				{
					Assert.AreEqual(7, logs.Count, "i=" + i.ToString());
				}
				else
				{
					Assert.AreEqual(10, logs.Count, "i=" + i.ToString());
				}
			}
			int idleTasksCount =
				(int)
				DbHelper.ExecuteScalar("select count(Id) from Tasks where Tasks.IsDefaultTask = 1 and Tasks.DefaultTaskId = " +
				                       (int) DefaultTaskEnum.Idle);
			Assert.AreEqual(3, idleTasksCount);
		}

		private void InsertLog(int taskId, DateTime insertTime, int duration)
		{
			DbHelper.ExecuteNonQuery("INSERT INTO TasksLog (TaskId, Duration, InsertTime) values (?, ?, ?)",
			                         new string[] {"TaskId", "Duration", "InsertTime"},
			                         new object[] {taskId, duration, insertTime});
		}


		[Test]
		public void GroupLogsTest()
		{
			//Make task tree
			PTMDataset.TasksRow task1;
			task1 = Tasks.NewTasksRow();
			task1.Description = "TaskTest1";
			task1.ParentId = Tasks.RootTasksRow.Id;
			task1.Id = Tasks.AddTasksRow(task1);

			PTMDataset.TasksRow task2;
			task2 = Tasks.NewTasksRow();
			task2.Description = "TaskTest2";
			task2.ParentId = Tasks.RootTasksRow.Id;
			task2.Id = Tasks.AddTasksRow(task2);

			PTMDataset.TasksRow task3;
			task3 = Tasks.NewTasksRow();
			task3.Description = "TaskTest3";
			task3.ParentId = task1.Id;
			task3.Id = Tasks.AddTasksRow(task3);

			int rootchilddefaultId = Tasks.AddDeafultTask(Tasks.RootTasksRow.Id, DefaultTaskEnum.OtherPersonal);
			int task1childdefaultId = Tasks.AddDeafultTask(task1.Id, DefaultTaskEnum.LunchTime);
			int task2childdefaultId = Tasks.AddDeafultTask(task2.Id, DefaultTaskEnum.CheckingJobMail);
			int task3childdefaultId = Tasks.AddDeafultTask(task3.Id, DefaultTaskEnum.JobMeeting);

			int duration = (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration).Value*60;
			
			//Add logs
			for (int i = 0; i <= (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value + 7; i++)
			{
				InsertLog(task1.Id, DateTime.Today.AddDays(-i), duration);
				InsertLog(task1.Id, DateTime.Today.AddDays(-i).AddSeconds(duration), duration);
				InsertLog(task1.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*2), duration);
				InsertLog(task1.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*3), duration);

				InsertLog(task2.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*4), duration);

				InsertLog(task3.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*5), duration);
				InsertLog(task3.Id, DateTime.Today.AddDays(-i).AddSeconds(duration*6), duration);

				InsertLog(rootchilddefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*7), duration);
				InsertLog(rootchilddefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*8), duration);
				InsertLog(rootchilddefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*9), duration);

				InsertLog(task1childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*10), duration);

				InsertLog(task2childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*11), duration);
				InsertLog(task2childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*12), duration);

				InsertLog(task3childdefaultId, DateTime.Today.AddDays(-i).AddSeconds(duration*13), duration);
			}

			DataMaintenanceHelper.GroupLogs();

			//validate
			for (int i = 0; i <= (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value + 7; i++)
			{
				ArrayList logs = Logs.GetLogsByDay(DateTime.Today.AddDays(-i));
				if (i > (int) ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays).Value)
				{
					Assert.AreEqual(7, logs.Count, "i=" + i.ToString());
				}
				else
				{
					Assert.AreEqual(14, logs.Count, "i=" + i.ToString());
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