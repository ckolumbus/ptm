using System;
using NUnit.Framework;
using PTM.Business;
using PTM.Business.Helpers;
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
			Assert.AreEqual(typeof(int), config.Value.GetType());
			Assert.AreEqual(10, config.Value);
			
			config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			Assert.AreEqual(ConfigurationKey.DataMaintenanceDays, config.Key);
			Assert.AreEqual(typeof(int), config.Value.GetType());
			Assert.AreEqual(15, config.Value);
		}
		
		[Test]
		[ExpectedException(typeof(ApplicationException), "The log duration can't be less than 1 min. and more than 60 min.")]
		public void SaveLogDurationLessThanPermitedTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration);
			config.Value = 0;
			ConfigurationHelper.SaveConfiguration(config);
		}
		
		[Test]
		[ExpectedException(typeof(ApplicationException), "The log duration can't be less than 1 min. and more than 60 min.")]
		public void SaveLogDurationMoreThanPermitedTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration);
			config.Value = 61;
			ConfigurationHelper.SaveConfiguration(config);
		}
		
		[Test]
		public void SaveLogDurationTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration);
			config.Value = 30;
			ConfigurationHelper.SaveConfiguration(config);
			config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration);
			Assert.AreEqual(ConfigurationKey.DefaultTasksLogDuration, config.Key);
			Assert.AreEqual(30, config.Value);
		}
		
		
		[Test]
		[ExpectedException(typeof(ApplicationException), "Data maintenance days can't be less than 0.")]
		public void SaveDataMaintenanceLessThanPermitedTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			config.Value = -1;
			ConfigurationHelper.SaveConfiguration(config);
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
