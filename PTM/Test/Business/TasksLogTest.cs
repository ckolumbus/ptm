using System;
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

		[SetUp]
		public void SetUp()
		{
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
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
			row = TasksLog.NewTasksLogRow();
			row.Duration = 60;
			row.TaskId = id;
			TasksLog.AddTasksLogRow(row);
			Assert.AreEqual(id, Tasks.CurrentTaskRow.Id);
		}

		[TearDown]
		public void TearDown()
		{
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
		}
	}
}
