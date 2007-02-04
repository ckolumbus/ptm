using System;
using NUnit.Framework;
using PTM.Framework;
using PTM.Framework.Helpers;
using PTM.Data;

namespace PTM.Test.Framework.Helpers
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
			DbHelper.Initialize("test");
			DbHelper.DeleteDataSource();
			MainModule.Initialize("test");
		}

		[Test]
		public void GetConfigurationTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			Assert.AreEqual(ConfigurationKey.TasksLogDuration, config.Key);
			Assert.AreEqual(typeof (int), config.Value.GetType());
			Assert.AreEqual(10, config.Value);

			config = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			Assert.AreEqual(ConfigurationKey.DataMaintenanceDays, config.Key);
			Assert.AreEqual(typeof (int), config.Value.GetType());
			Assert.AreEqual(7, config.Value);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "The log duration can't be less than 1 min. and more than 60 min.")]
		public void SaveLogDurationLessThanPermitedTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			config.Value = 0;
			ConfigurationHelper.SaveConfiguration(config);
		}

		[Test]
		[ExpectedException(typeof (ApplicationException), "The log duration can't be less than 1 min. and more than 60 min.")]
		public void SaveLogDurationMoreThanPermitedTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			config.Value = 61;
			ConfigurationHelper.SaveConfiguration(config);
		}

		[Test]
		public void SaveLogDurationTest()
		{
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			config.Value = 30;
			ConfigurationHelper.SaveConfiguration(config);
			config = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			Assert.AreEqual(ConfigurationKey.TasksLogDuration, config.Key);
			Assert.AreEqual(30, config.Value);
		}


		[Test]
		[ExpectedException(typeof (ApplicationException), "Data maintenance days can't be less than 0.")]
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
			DbHelper.DeleteDataSource();
		}
	}
}