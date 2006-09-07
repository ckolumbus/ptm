using System;
using NUnit.Framework;
using PTM.Business;
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

		[SetUp]
		public void SetUp()
		{
			DbHelper.Initialize("test");
			DbHelper.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
		}

		[Test]
		public void InitializeTest()
		{
			Assert.IsTrue(DefaultTasks.List.Count>0);
		}
		
		[Test]
		public void DefaulttaskIsActive()
		{
//			Assert.AreEqual(false, DefaultTasks.IsActive(DefaultTaskEnum.Idle));
//			Assert.AreEqual(false, DefaultTasks.IsActive(DefaultTaskEnum.LunchTime));
//			Assert.AreEqual(false, DefaultTasks.IsActive(DefaultTaskEnum.OtherPersonal));
//			Assert.AreEqual(true, DefaultTasks.IsActive(DefaultTaskEnum.CheckingJobMail));
//			Assert.AreEqual(true, DefaultTasks.IsActive(DefaultTaskEnum.JobMeeting));
//			Assert.AreEqual(true, DefaultTasks.IsActive(DefaultTaskEnum.JobPhoneCall));

			foreach (DefaultTask defaultTask in DefaultTasks.List)
			{
				switch(defaultTask.DefaultTaskId)
				{
					case (int)DefaultTaskEnum.Idle:
						Assert.AreEqual(false, defaultTask.IsActive);
						break;
					case (int)DefaultTaskEnum.LunchTime:
						Assert.AreEqual(false, defaultTask.IsActive);
						break;
					case (int)DefaultTaskEnum.OtherPersonal:
						Assert.AreEqual(false, defaultTask.IsActive);
						break;
					case (int)DefaultTaskEnum.CheckingJobMail:
						Assert.AreEqual(true, defaultTask.IsActive);
						break;	
					case (int)DefaultTaskEnum.JobMeeting:
						Assert.AreEqual(true, defaultTask.IsActive);
						break;
					case (int)DefaultTaskEnum.JobPhoneCall:
						Assert.AreEqual(true, defaultTask.IsActive);
						break;
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
