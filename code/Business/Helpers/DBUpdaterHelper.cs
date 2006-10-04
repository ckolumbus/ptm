using System;
using System.Data.OleDb;
using PTM.Data;
using PTM.View;

namespace PTM.Business.Helpers
{
	/// <summary>
	/// Summary description for DBUpdaterHelper.
	/// </summary>
	public sealed class DBUpdaterHelper
	{
		private DBUpdaterHelper()
		{
		}
		
		public static void UpdateDataBase()
		{
			bool findNextUpdate = true;
			while(findNextUpdate)
			{
				Configuration oldVersion = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataBaseVersion);
				if(UpdateFromV00ToV09(oldVersion))
					continue;
				if(UpdateFromV09ToV091(oldVersion)) //Next check
					continue;
				//if(UpdateFromV091ToVXX(oldVersion)) //Next check
				//	continue;
				findNextUpdate = false;
			}
		}

		private static bool UpdateFromV09ToV091(Configuration oldVersion)
		{
			if(string.Compare(oldVersion.Value.ToString().Trim(), "0.9")==0)
			{
				try
				{
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.1"));
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.CheckForUpdates, "1"));
					return true;					
				}
				catch(OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}

		private static bool UpdateFromV00ToV09(Configuration oldVersion)
		{
			if(oldVersion==null)
			{
				try
				{
					DbHelper.ExecuteNonQuery("Delete from Configuration");
					DbHelper.DeleteConstraint("Configuration", "PrimaryKey");
					DbHelper.DeleteConstraint("Configuration", "Id");
					DbHelper.DeleteColumn("Configuration", "Id");
					DbHelper.DeleteColumn("Configuration", "ListValue");
					DbHelper.AddPrimaryKey("Configuration", "KeyValue");
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9"));
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataMaintenanceDays, 7));
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.TasksLogDuration, 10));
					return true;					
				}
				catch(OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}
	}
}
