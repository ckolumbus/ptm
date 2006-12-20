using System;
using System.Data.OleDb;
using PTM.Data;
using PTM.View;

namespace PTM.Framework.Helpers
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
				if(UpdateFromV09ToV091(oldVersion))
					continue;
				if(UpdateFromV091ToV092(oldVersion))
					continue;
				if(UpdateFromV092ToV093(oldVersion)) 
					continue;
				if(UpdateFromV093ToV094(oldVersion)) 
					continue;
				//if(UpdateFromV093ToVXX(oldVersion)) //Next check
				//	continue;
				findNextUpdate = false;
			}
		}

		private static bool UpdateFromV093ToV094(Configuration oldVersion)
		{
			if(string.Compare(oldVersion.Value.ToString().Trim(), "0.9.3")==0)
			{
				try
				{
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.ShowTasksFullPath, "1"));
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.4"));
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

		private static bool UpdateFromV092ToV093(Configuration oldVersion)
		{
			if(string.Compare(oldVersion.Value.ToString().Trim(), "0.9.2")==0)
			{
				try
				{
					DbHelper.AddColumn("DefaultTasks", "IsActive", "Bit");
					DbHelper.AddColumn("DefaultTasks", "Icon", "Integer");
					DbHelper.AddColumn("DefaultTasks", "Hidden", "Bit");
					string updateQuery = "Update DefaultTasks Set IsActive = ?, Icon = ?, Hidden = ? Where Id = ?";
					string[] parameterNames = new string[] {"IsActive", "Icon", "Hidden", "Id"};
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames, 
						new object[]{false, 0, false, 1});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames, 
						new object[]{false, 2, false, 2});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames, 
						new object[]{false, 3, false, 3});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames, 
						new object[]{true, 4, false, 4});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames, 
						new object[]{true, 5, false, 5});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames, 
						new object[]{false, 6, false, 6});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames, 
						new object[]{true, 7, false, 7});

					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.3"));					
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

		private static bool UpdateFromV091ToV092(Configuration oldVersion)
		{
			if(string.Compare(oldVersion.Value.ToString().Trim(), "0.9.1")==0)
			{
				try
				{
					DbHelper.CreateTable("Addins");
					DbHelper.AddColumn("Addins", "Path", "VarChar(255)");
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.2"));					
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

		private static bool UpdateFromV09ToV091(Configuration oldVersion)
		{
			if(string.Compare(oldVersion.Value.ToString().Trim(), "0.9")==0)
			{
				try
				{
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.CheckForUpdates, "1"));
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.1"));
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
