using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using PTM.Data;

namespace PTM.Business.Helpers
{
	/// <summary>
	/// ConfigurationKey enumeration 
	/// used to match betwen table row Id, and elements.
	/// </summary>
	public enum ConfigurationKey : int
	{
		TasksLogDuration = 1,
		DataMaintenanceDays = 2
	}//ConfigurationKey enum

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
		}//ConfigurationHelper

		
		/// <summary>
		/// GetConfiguration  receives a configuration key
		/// @return  the configuration first item asociated with this item.
		/// TODO check if the configuration key doesn't exist 
		/// </summary>
		public static Configuration GetConfiguration( ConfigurationKey key )
		{
			Hashtable ht;
			ht = DbHelper.ExecuteGetFirstRow("SELECT ConfigValue from Configuration where KeyValue = " +
			                                      ((int) key).ToString());
			
			object configValue;
			switch(key)
			{
				case ConfigurationKey.TasksLogDuration:
				case ConfigurationKey.DataMaintenanceDays:
					configValue = Convert.ToInt32(ht["ConfigValue"]);
					break;				
				default:
					configValue = ht["ConfigValue"];
					break;
			}
			
			return new Configuration(key, configValue);
			
		}//GetConfiguration
		
		public static void SaveConfiguration(Configuration configuration)
		{
			switch(configuration.Key)
			{
				case ConfigurationKey.TasksLogDuration:
					if(Convert.ToInt32(configuration.Value)<1 || 
					   Convert.ToInt32(configuration.Value) > 60)
						throw new ApplicationException("The log duration can't be less than 1 min. and more than 60 min.");
					break;
				case ConfigurationKey.DataMaintenanceDays:
					if(Convert.ToInt32(configuration.Value)<0)
						throw new ApplicationException("Data maintenance days can't be less than 0.");
					break;
			}
			
			DbHelper.ExecuteNonQuery("UPDATE Configuration SET ConfigValue = ? WHERE KeyValue = " +
			                                   ((int) configuration.Key).ToString(), new string[]{"ConfigValue"}, new object[]{configuration.Value});
		}

	}//ConfigurationHelper
	
		
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
	
}//namespace
