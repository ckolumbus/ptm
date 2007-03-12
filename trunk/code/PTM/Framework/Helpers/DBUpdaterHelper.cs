using System;
using System.Data.OleDb;
using System.Windows.Forms;
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
			while (findNextUpdate)
			{
				Configuration oldVersion = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataBaseVersion);
				if (UpdateFromV00ToV09(oldVersion))
					continue;
				if (UpdateFromV09ToV091(oldVersion))
					continue;
				if (UpdateFromV091ToV092(oldVersion))
					continue;
				if (UpdateFromV092ToV093(oldVersion))
					continue;
				if (UpdateFromV093ToV094(oldVersion))
					continue;
				if (UpdateFromV094ToV95(oldVersion))
					continue;
				if(UpdateFromV095ToV96(oldVersion))
					continue;
				if(UpdateFromV096ToV97(oldVersion))
					continue;
				//if(UpdateFromV097ToVXX(oldVersion)) //Next check
				//	continue;
				findNextUpdate = false;
			}
		}

		private static bool UpdateFromV096ToV97(Configuration oldVersion)
		{
			if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.6") == 0)
			{
				try
				{
					DbHelper.DeleteColumn("Tasks", "StartDate");
					DbHelper.DeleteColumn("Tasks", "StopDate");
					DbHelper.DeleteColumn("Tasks", "IsFinished");
					DbHelper.DeleteColumn("Tasks", "TotalTime");
					DbHelper.DeleteColumn("ApplicationsLog", "ProcessId");
					DbHelper.DeleteColumn("ApplicationsLog", "Caption");
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.7"));
					return true;
				}
				catch (OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}

		private static bool UpdateFromV095ToV96(Configuration oldVersion)
		{
			if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.5") == 0)
			{
				try
				{
					DataMaintenanceHelper.GroupLogs(true);
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.6"));
					return true;
				}
				catch (OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}

		private static bool UpdateFromV094ToV95(Configuration oldVersion)
		{
			if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.4") == 0)
			{
				try
				{
					DbHelper.AddColumn("Tasks", "IsActive", "Bit");
					DbHelper.AddColumn("Tasks", "IconId", "Integer");
					DbHelper.ExecuteNonQuery("Update Tasks Set IsActive = 1, IconId = " + IconsManager.DefaultTaskIconId);
					Application.DoEvents();
					DbHelper.ExecuteNonQuery(
						"UPDATE Tasks INNER JOIN DefaultTasks ON Tasks.DefaultTaskId = DefaultTasks.Id SET Tasks.IsActive = DefaultTasks.IsActive, Tasks.IconId = DefaultTasks.Icon, Tasks.Description = DefaultTasks.Description");
					Application.DoEvents();
					DbHelper.DeleteTable("DefaultTasks");
					Application.DoEvents();
					DbHelper.DeleteConstraint("Tasks", "IsDefaultTask");
					DbHelper.DeleteConstraint("Tasks", "DefaultTaskId");
					DbHelper.DeleteColumn("Tasks", "IsDefaultTask");
					DbHelper.DeleteColumn("Tasks", "DefaultTaskId");
					Application.DoEvents();
					DbHelper.ExecuteNonQuery("Delete from Tasks where Description = 'Idle'");
					Application.DoEvents();
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.5"));
					return true;
				}
				catch (OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}

		private static bool UpdateFromV093ToV094(Configuration oldVersion)
		{
			if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.3") == 0)
			{
				try
				{
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.ShowTasksFullPath, "1"));
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.4"));
					return true;
				}
				catch (OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}

		private static bool UpdateFromV092ToV093(Configuration oldVersion)
		{
			if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.2") == 0)
			{
				try
				{
					DbHelper.AddColumn("DefaultTasks", "IsActive", "Bit");
					DbHelper.AddColumn("DefaultTasks", "Icon", "Integer");
					DbHelper.AddColumn("DefaultTasks", "Hidden", "Bit");
					string updateQuery = "Update DefaultTasks Set IsActive = ?, Icon = ?, Hidden = ? Where Id = ?";
					string[] parameterNames = new string[] {"IsActive", "Icon", "Hidden", "Id"};
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames,
					                         new object[] {false, 0, false, 1});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames,
					                         new object[] {false, 2, false, 2});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames,
					                         new object[] {false, 3, false, 3});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames,
					                         new object[] {true, 4, false, 4});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames,
					                         new object[] {true, 5, false, 5});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames,
					                         new object[] {false, 6, false, 6});
					DbHelper.ExecuteNonQuery(updateQuery, parameterNames,
					                         new object[] {true, 7, false, 7});

					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.3"));
					return true;
				}
				catch (OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}

		private static bool UpdateFromV091ToV092(Configuration oldVersion)
		{
			if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.1") == 0)
			{
				try
				{
					DbHelper.CreateTable("Addins");
					DbHelper.AddColumn("Addins", "Path", "VarChar(255)");
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.2"));
					return true;
				}
				catch (OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}

		private static bool UpdateFromV09ToV091(Configuration oldVersion)
		{
			if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9") == 0)
			{
				try
				{
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.CheckForUpdates, "1"));
					ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.1"));
					return true;
				}
				catch (OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}

		private static bool UpdateFromV00ToV09(Configuration oldVersion)
		{
			if (oldVersion == null)
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
				catch (OleDbException ex)
				{
					Logger.Write(ex.Message);
					return false;
				}
			}
			return false;
		}
	}
}