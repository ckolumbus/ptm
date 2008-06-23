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
			int taskId1 = Tasks.AddTask("TaskTest1", Tasks.RootTask.Id).Id;

			int taskId2 = Tasks.AddTask("TaskTest2", Tasks.RootTask.Id).Id;

			int taskId3 = Tasks.AddTask("TaskTest3", taskId1).Id;

			Logs.StartLogging();
			Logs.AddLog(taskId1);
			Thread.Sleep(9000);
			Logs.AddLog(taskId1);
			Thread.Sleep(5000);

			Logs.AddLog(taskId2);
			Thread.Sleep(8000);
			Logs.AddLog(taskId2);
			Thread.Sleep(9000);

			Logs.AddLog(taskId3);
			Thread.Sleep(9000);
			Logs.AddLog(taskId3);
			Thread.Sleep(7000);

			Logs.StopLogging();

			ArrayList result;
			result =
				ApplicationSummaries.GetApplicationsSummary(Tasks.RootTask.Id, DateTime.Today,
				                                            DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.IsTrue(result.Count > 0);
			double total = 0;
			foreach (ApplicationSummary applicationSummary in result)
			{
				total += applicationSummary.TotalActiveTime;
			}
			Assert.IsTrue(total > 0);

			result = ApplicationSummaries.GetApplicationsSummary(taskId1, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			double totalTask1 = 0;
			foreach (ApplicationSummary applicationSummary in result)
			{
				totalTask1 += applicationSummary.TotalActiveTime;
			}
			Assert.IsTrue(totalTask1 > 0);
			Assert.IsTrue(totalTask1 < total);

			result = ApplicationSummaries.GetApplicationsSummary(taskId2, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			double totalTask2 = 0;
			foreach (ApplicationSummary applicationSummary in result)
			{
				totalTask2 += applicationSummary.TotalActiveTime;
			}
			Assert.IsTrue(totalTask2 > 0);
			Assert.IsTrue(totalTask2 < total);

			result = ApplicationSummaries.GetApplicationsSummary(taskId3, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
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