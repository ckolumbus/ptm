using System;
using System.Collections;
using System.Reflection;
using PTM.Data;

namespace PTM.Framework.Helpers
{
	/// <summary>
	/// ConfigurationKey enumeration 
	/// used to match betwen table row Id, and elements.
	/// </summary>
	public enum ConfigurationKey : int
	{
		DataBaseVersion = 0,
		TasksLogDuration = 1,
		DataMaintenanceDays = 2,
		CheckForUpdates = 3,
		ShowTasksFullPath = 4
	} //ConfigurationKey enum

	/// <summary>
	/// ConfigurationHelper class 
	/// Helper class 
	/// </summary>
	public sealed class ConfigurationHelper
	{
		/// <summary>
		/// ConfigurationHelper private Constructor. 
		/// Use Static Methods.
		/// </summary>
		private ConfigurationHelper()
		{
		} //ConfigurationHelper


		/// <summary>
		/// GetConfiguration  receives a configuration key
		/// @return  the configuration first item asociated with this item.
		/// </summary>
		public static Configuration GetConfiguration(ConfigurationKey key)
		{
			Hashtable ht;
			ht = DbHelper.ExecuteGetFirstRow("SELECT ConfigValue from Configuration where KeyValue = " +
			                                 ((int) key).ToString());
			if (ht == null)
				return null;
			object configValue;
			switch (key)
			{
				case ConfigurationKey.TasksLogDuration:
				case ConfigurationKey.DataMaintenanceDays:
					configValue = Convert.ToInt32(ht["ConfigValue"]);
					break;
				default:
					configValue = ht["ConfigValue"].ToString().Trim();
					break;
			}

			return new Configuration(key, configValue);
		} //GetConfiguration

		public static void SaveConfiguration(Configuration configuration)
		{
			switch (configuration.Key)
			{
				case ConfigurationKey.TasksLogDuration:
					if (Convert.ToInt32(configuration.Value) < 1 ||
					    Convert.ToInt32(configuration.Value) > 60)
						throw new ApplicationException("The log duration can't be less than 1 min. and more than 60 min.");
					break;
				case ConfigurationKey.DataMaintenanceDays:
					if (Convert.ToInt32(configuration.Value) < 0)
						throw new ApplicationException("Data maintenance days can't be less than 0.");
					break;
			}
			if (GetConfiguration(configuration.Key) != null)
			{
				DbHelper.ExecuteNonQuery("UPDATE Configuration SET ConfigValue = ? WHERE KeyValue = " +
				                         ((int) configuration.Key).ToString(), new string[] {"ConfigValue"},
				                         new object[] {configuration.Value});
			}
			else
			{
				DbHelper.ExecuteNonQuery("INSERT INTO Configuration (KeyValue, Description, ConfigValue) VALUES (?, ?, ?)",
				                         new string[] {"KeyValue", "Description", "ConfigValue"},
				                         new object[] {(int) configuration.Key, configuration.Key.ToString(), configuration.Value});
			}
		}


		public static string GetVersionString()
		{
			return "beta v. 1.6.8";
		} //GetVersionString

		public static string GetInternalVersionString()
		{
			return Assembly.GetExecutingAssembly().GetName().Version.ToString();
		} //GetInternalVersionString
	} //ConfigurationHelper


	public class Configuration
	{
		public Configuration(ConfigurationKey key, object value)
		{
			this.key = key;
			this.value = value;
		}

		private ConfigurationKey key;
		private object value;

		public ConfigurationKey Key
		{
			get { return key; }
			set { key = value; }
		}

		public object Value
		{
			get { return value; }
			set { this.value = value; }
		}
	}
} //namespace