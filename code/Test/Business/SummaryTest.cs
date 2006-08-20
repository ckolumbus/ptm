using System;
using System.Collections;
using System.Threading;
using NUnit.Framework;
using PTM.Business;
using PTM.Data;
using PTM.Infos;

namespace PTM.Test.Business
{
	/// <summary>
	/// Descripción breve de SummaryTest.
	/// </summary>
	[TestFixture]
	public class SummaryTest
	{
		public SummaryTest()
		{
		}

		[SetUp]
		public void SetUp()
		{
			TearDown();
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
		}

		[Test]
		public void GetTaskSummaryTest()
		{
			PTMDataset.TasksRow row1;
			row1 = Tasks.NewTasksRow();
			row1.Description = "TaskTest1";
			row1.ParentId = Tasks.RootTasksRow.Id;
			row1.Id = Tasks.AddTasksRow(row1);

			PTMDataset.TasksRow row2;
			row2 = Tasks.NewTasksRow();
			row2.Description = "TaskTest2";
			row2.ParentId = Tasks.RootTasksRow.Id;
			row2.Id = Tasks.AddTasksRow(row2);
			
			PTMDataset.TasksRow row3;
			row3 = Tasks.NewTasksRow();
			row3.Description = "TaskTest3";
			row3.ParentId = row1.Id;
			row3.Id = Tasks.AddTasksRow(row3);

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

			Logs.StopLogging();

			//row1 ->5 + 3
			////row3->3
			//row2 ->2

			ArrayList result;
			result = Summary.GetTaskSummary(Tasks.RootTasksRow, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(2, result.Count);
			TaskSummary sum1 = Summary.FindTaskSummaryByTaskId(result, row1.Id);
			Assert.AreEqual(8, sum1.TotalTime);
			TaskSummary sum2 = Summary.FindTaskSummaryByTaskId(result, row2.Id);
			Assert.AreEqual(2, sum2.TotalTime);

			result = Summary.GetTaskSummary(row1, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(2, result.Count);
			sum1 = Summary.FindTaskSummaryByTaskId(result, row1.Id);
			Assert.AreEqual(5, sum1.TotalTime);
			sum2 = Summary.FindTaskSummaryByTaskId(result, row3.Id);
			Assert.AreEqual(3, sum2.TotalTime);

			result = Summary.GetTaskSummary(row3, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(1, result.Count);
			sum1 = Summary.FindTaskSummaryByTaskId(result, row3.Id);
			Assert.AreEqual(3, sum1.TotalTime);

			result = Summary.GetTaskSummary(row2, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
			Assert.AreEqual(1, result.Count);
			sum1 = Summary.FindTaskSummaryByTaskId(result, row2.Id);
			Assert.AreEqual(2, sum1.TotalTime);
		}


		[TearDown]
		public void TearDown()
		{
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
		}
	}
}
