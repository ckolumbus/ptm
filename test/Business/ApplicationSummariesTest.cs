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
	/// Summary description for ApplicationSummariesTest.
	/// </summary>
	[TestFixture]
	public class ApplicationSummariesTest
	{
		public ApplicationSummariesTest()
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
		public void GetApplicationSummaryTest()
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

			Logs.StartLogging();
			Logs.AddLog(row1.Id);
			Thread.Sleep(9000);
			Logs.AddLog(row1.Id);
			Thread.Sleep(5000);

			Logs.AddLog(row2.Id);
			Thread.Sleep(8000);
			Logs.AddLog(row2.Id);
			Thread.Sleep(9000);

			Logs.AddLog(row3.Id);
			Thread.Sleep(9000);
			Logs.AddLog(row3.Id);
			Thread.Sleep(7000);

			Logs.StopLogging();

			ArrayList result;
			result =
				ApplicationSummaries.GetApplicationsSummary(Tasks.RootTasksRow, DateTime.Today,
				                                            DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.IsTrue(result.Count > 0);
			double total = 0;
			foreach (ApplicationSummary applicationSummary in result)
			{
				total += applicationSummary.TotalActiveTime;
			}
			Assert.IsTrue(total > 0);

			result = ApplicationSummaries.GetApplicationsSummary(row1, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			double totalTask1 = 0;
			foreach (ApplicationSummary applicationSummary in result)
			{
				totalTask1 += applicationSummary.TotalActiveTime;
			}
			Assert.IsTrue(totalTask1 > 0);
			Assert.IsTrue(totalTask1 < total);

			result = ApplicationSummaries.GetApplicationsSummary(row2, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			double totalTask2 = 0;
			foreach (ApplicationSummary applicationSummary in result)
			{
				totalTask2 += applicationSummary.TotalActiveTime;
			}
			Assert.IsTrue(totalTask2 > 0);
			Assert.IsTrue(totalTask2 < total);

			result = ApplicationSummaries.GetApplicationsSummary(row3, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			double totalTask3 = 0;
			foreach (ApplicationSummary applicationSummary in result)
			{
				totalTask3 += applicationSummary.TotalActiveTime;
			}
			Assert.IsTrue(totalTask3 > 0);
			Assert.IsTrue(totalTask3 < total);

			Assert.IsTrue(totalTask1 + totalTask2 <= total);
			Assert.IsTrue(totalTask3 < totalTask1);
		}

		[TearDown]
		public void TearDown()
		{
			Logs.StopLogging();
			DbHelper.DeleteDataSource();
		}
	}
}