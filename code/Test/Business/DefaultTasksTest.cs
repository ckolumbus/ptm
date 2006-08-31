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
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
		}

		[Test]
		public void InitializeTest()
		{
			Assert.IsTrue(DefaultTasks.List.Count>0);
		}

		[TearDown]
		public void TearDown()
		{
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
		}
	}
}
