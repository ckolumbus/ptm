using System;
using NUnit.Framework;
using PTM.Business;
using PTM.Data;

namespace PTM.Test.Business
{
	/// <summary>
	/// Summary description for ConfigurationTest.
	/// </summary>
	[TestFixture]
	public class ConfigurationTest
	{
		public ConfigurationTest()
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
		public void GetConfigurationTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration);
			Assert.AreEqual(ConfigurationKey.DefaultTasksLogDuration, config.Key);
			Assert.AreEqual(10, Convert.ToInt32(config.Value));
		}
		
		[TearDown]
		public void TearDown()
		{
			Logs.StopLogging();
			DataAdapterManager m = new DataAdapterManager("test");
			m.DeleteDataSource();
		}
	}
}
