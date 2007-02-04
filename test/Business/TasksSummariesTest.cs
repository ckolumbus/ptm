using System;
using System.Collections;
using System.Threading;
using NUnit.Framework;
using PTM.Framework;
using PTM.Data;
using PTM.Framework.Infos;

namespace PTM.Test.Framework
{
	/// <summary>
	/// Descripción breve de SummaryTest.
	/// </summary>
	[TestFixture]
	public class TaskSummariesTest
	{
		public TaskSummariesTest()
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
		public void GetTaskSummaryTest()
		{
			Task row1;
			row1 = new Task();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.IsActive = true;
			row1.Id = Tasks.AddTask(row1);
			
			Task row2;
			row2 = new Task();
			row2.Description = "TaskTest2";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.IsActive = true;
			row2.Id = Tasks.AddTask(row2);
			
			Task row3;
			row3 = new Task();
			row3.Description = "TaskTest3";
			row3.ParentId = row1.Id;
			row3.IsActive = true;
			row3.Id = Tasks.AddTask(row3);
			
			Task row4;
			row4 = new Task();
			row4.Description = "TaskTest4";
			row4.ParentId = row1.Id;
			row4.IsActive = false;
			row4.Id = Tasks.AddTask(row4);

//			Task row4;
//			row4 = new Task();
//			row4.IsDefaultTask = true;
//			row4.Description = DefaultTasks.GetDefaultTask(3).Description;
//			row4.DefaultTaskId = DefaultTasks.GetDefaultTask(3).DefaultTaskId;
//			row4.ParentId = row1.Id;
//			row4.Id = Tasks.AddTasksRow(row4);

			Logs.StartLogging();
			Logs.AddLog(row1.Id);
			Thread.Sleep(3000);
			Logs.AddLog(row1.Id);
			Thread.Sleep(2000);

			Logs.AddLog(row2.Id);
			Thread.Sleep(1000);
			Logs.AddLog(row2.Id);
			Thread.Sleep(1000);

			Logs.AddLog(row3.Id);
			Thread.Sleep(1000);
			Logs.AddLog(row3.Id);
			Thread.Sleep(2000);

			Logs.AddLog(row4.Id);
			Thread.Sleep(1000);
			Logs.AddLog(row4.Id);
			Thread.Sleep(2000);

			Logs.StopLogging();

			//row1 ->5 + 3
			////row3->3
			////row4->3
			//row2 ->2

			ArrayList result;
			result = TasksSummaries.GetTaskSummary(Tasks.RootTasksRow, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(2, result.Count);
			TaskSummary sum1 = TasksSummaries.FindTaskSummaryByTaskId(result, row1.Id);
			Assert.IsTrue(sum1.TotalActiveTime >= 8);
			Assert.IsTrue(sum1.TotalInactiveTime >= 3);
			TaskSummary sum2 = TasksSummaries.FindTaskSummaryByTaskId(result, row2.Id);
			Assert.IsTrue(sum2.TotalActiveTime >= 2);
			Assert.IsTrue(sum2.TotalInactiveTime == 0);

			result = TasksSummaries.GetTaskSummary(row1, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(3, result.Count);
			sum1 = TasksSummaries.FindTaskSummaryByTaskId(result, row1.Id);
			Assert.IsTrue(sum1.TotalActiveTime >= 5);
			Assert.IsTrue(sum1.TotalInactiveTime == 0);
			sum2 = TasksSummaries.FindTaskSummaryByTaskId(result, row3.Id);
			Assert.IsTrue(sum2.TotalActiveTime >= 3);
			Assert.IsTrue(sum2.TotalInactiveTime == 0);
			sum1 = TasksSummaries.FindTaskSummaryByTaskId(result, row4.Id);
			Assert.IsTrue(sum1.TotalActiveTime == 0);
			Assert.IsTrue(sum1.TotalInactiveTime >= 3);

			result = TasksSummaries.GetTaskSummary(row3, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(1, result.Count);
			sum1 = TasksSummaries.FindTaskSummaryByTaskId(result, row3.Id);
			Assert.IsTrue(sum1.TotalActiveTime >= 3);
			Assert.IsTrue(sum1.TotalInactiveTime == 0);

			result = TasksSummaries.GetTaskSummary(row4, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(1, result.Count);
			sum1 = TasksSummaries.FindTaskSummaryByTaskId(result, row4.Id);
			Assert.IsTrue(sum1.TotalActiveTime == 0);
			Assert.IsTrue(sum1.TotalInactiveTime >= 3);

			result = TasksSummaries.GetTaskSummary(row2, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(1, result.Count);
			sum1 = TasksSummaries.FindTaskSummaryByTaskId(result, row2.Id);
			Assert.IsTrue(sum1.TotalActiveTime >= 2);
		}


		[TearDown]
		public void TearDown()
		{
			Logs.StopLogging();
			DbHelper.DeleteDataSource();
		}
	}
}