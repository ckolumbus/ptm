using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using PTM.Data;

namespace PTM.Business
{
	/// <summary>
	/// ConfigurationKey enumeration 
	/// used to match betwen table row Id, and elements.
	/// </summary>
	public enum ConfigurationKey : int
	{
		DefaultTasksLogDuration = 1,
		DepurationDays = 2
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
			ht = DataAdapterManager.ExecuteGetFirstRow("SELECT ConfigValue from Configuration where KeyValue = " +
			                                      ((int) key).ToString());
			
			return new Configuration(key, ht["ConfigValue"]);
			
		}//GetConfiguration

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
