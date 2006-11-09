using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using PTM.Framework;
using PTM.Data;

namespace PTM.Test.Business
{
	/// <summary>
	/// Descripción breve de DefaultTasksTest.
	/// </summary>
	[TestFixture]
	public class DefaultTasksTest
	{
		public DefaultTasksTest()
		{
		}

		private int deletedCount = 0;
		private int addedCount = 0;
		private int changedCount = 0;
		
		[SetUp]
		public void SetUp()
		{
			DbHelper.Initialize("test");
			DbHelper.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
			deletedCount = 0;
			addedCount = 0;
			changedCount = 0;
			DefaultTasks.DefaultTaskChanged+=new PTM.Framework.DefaultTasks.DefaultTaskChangeEventHandler(DefaultTasks_DefaultTaskChanged);
		}

		[Test]
		public void InitializeTest()
		{
			Assert.IsTrue(DefaultTasks.Table.Count > 0);
		}
		
		[Test]
		public void AddTest()
		{
			int id = DefaultTasks.Add("Test", true, 1);
			Assert.AreEqual(1, this.addedCount);
			Assert.IsTrue(DefaultTasks.Table.Contains(id));
			DefaultTask dt = (DefaultTask) DefaultTasks.Table[id];
			Assert.AreEqual("Test", dt.Description);
			Assert.AreEqual(true, dt.IsActive);
			Assert.AreEqual(1, dt.IconId);

			Hashtable ht;
			ht = DbHelper.ExecuteGetFirstRow("Select Description, IsActive, Icon, Hidden From DefaultTasks Where Id = " + id);
			Assert.AreEqual("Test", ht["Description"]);
			Assert.AreEqual(true, ht["IsActive"]);
			Assert.AreEqual(1, ht["Icon"]);
			Assert.AreEqual(false, ht["Hidden"]);
		}
		
		[Test]
		public void UpdateTest()
		{
			DefaultTasks.Update(1, "UpdateTest", true, 1);
			Assert.AreEqual(1, this.changedCount);
			DefaultTask dt = (DefaultTask) DefaultTasks.Table[1];
			Assert.AreEqual("UpdateTest", dt.Description);
			Assert.AreEqual(true, dt.IsActive);
			Assert.AreEqual(1, dt.IconId);
			
			Hashtable ht;
			ht = DbHelper.ExecuteGetFirstRow("Select Description, IsActive, Icon, Hidden From DefaultTasks Where Id = 1");
			Assert.AreEqual("UpdateTest", ht["Description"]);
			Assert.AreEqual(true, ht["IsActive"]);
			Assert.AreEqual(1, ht["Icon"]);
			Assert.AreEqual(false, ht["Hidden"]);
		}
		
		[Test]
		public void UpdateAndTaskUpdatedTooTest()
		{
			int taskId = Tasks.AddDeafultTask(Tasks.RootTasksRow.Id, 2);
			
			DefaultTasks.Update(2, "UpdateAndTaskUpdatedTooTest", true, 1);
			Assert.AreEqual(1, this.changedCount);
			PTMDataset.TasksRow task;
			task = Tasks.FindById(taskId);
			Assert.AreEqual("UpdateAndTaskUpdatedTooTest", task.Description);
		}
		
		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void UpdateDontExitsTest()
		{
			DefaultTasks.Update(99, "UpdateTest", true, 1);
		}
		
		[Test]
		public void DeleteTest()
		{
			DefaultTasks.Delete(1);
			Assert.AreEqual(1, this.deletedCount);
			Assert.IsFalse(DefaultTasks.Table.Contains(1));
			
			Hashtable ht;
			ht = DbHelper.ExecuteGetFirstRow("Select Hidden From DefaultTasks Where Id = 1");
			Assert.AreEqual(true, ht["Hidden"]);
		}

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void DeleteDontExitsTest()
		{
			DefaultTasks.Delete(99);
		}
		
		[TearDown]
		public void TearDown()
		{
			DefaultTasks.DefaultTaskChanged-=new PTM.Framework.DefaultTasks.DefaultTaskChangeEventHandler(DefaultTasks_DefaultTaskChanged);
			Logs.StopLogging();
			DbHelper.DeleteDataSource();
		}

		private void DefaultTasks_DefaultTaskChanged(PTM.Framework.DefaultTasks.DefaultTaskChangeEventArgs e)
		{
			if(e.Action == DataRowAction.Add)
				this.addedCount++;

			if(e.Action == DataRowAction.Change)
				this.changedCount++;

			if(e.Action == DataRowAction.Delete)
				this.deletedCount++;
		}
	}
}