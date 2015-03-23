using System;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using PTM.Addin;
using PTM.Util;
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
				if(UpdateFromV097ToV098(oldVersion))
					continue;
                if (UpdateFromV098ToV099(oldVersion))
                    continue;
                if (UpdateFromV099ToV0910(oldVersion))
                    continue;
                if (UpdateFromV_0_9_10_ToV_1_0_0(oldVersion))
                    continue;
                if (UpdateFromV_1_0_0ToV_1_0_1(oldVersion))
                    continue;
                if (UpdateFromV_1_0_1ToV_1_0_2(oldVersion))
                    continue;
                if (UpdateFromV_1_0_2ToV_1_0_3(oldVersion))
                    continue;
				findNextUpdate = false;
                RegisterAddins();
			}
		}

	    private static void RegisterAddins()
        {
            try
            {
                //if addins are prsents in current path then add them
                string curPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (File.Exists(curPath + @"\PTM.Addin.Charts.dll") && !AddinHelper.ExistAddin(curPath + @"\PTM.Addin.Charts.dll"))
                    AddinHelper.AddAddinAssembly(curPath + @"\PTM.Addin.Charts.dll");

                if (File.Exists(curPath + @"\PTM.Addin.WeekView.dll") && !AddinHelper.ExistAddin(curPath + @"\PTM.Addin.WeekView.dll"))
                    AddinHelper.AddAddinAssembly(curPath + @"\PTM.Addin.WeekView.dll");
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
            }
        }

        private static bool UpdateFromV_1_0_2ToV_1_0_3(Configuration oldVersion)
        {
            if (string.Compare(oldVersion.Value.ToString().Trim(), "1.0.2") == 0)
            {
                try
                {
                    DbHelper.AddColumn("Tasks", "AccountID", "VarChar(50)");
                    DbHelper.AddColumn("Tasks", "MatPath", "VarChar(255)"); 
                    ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "1.0.3"));
                    return true;
                }
                catch (OleDbException ex)
                {
                    Logger.WriteException(ex);
                    return false;
                }
            }
            return false;
        }

        private static bool UpdateFromV_1_0_1ToV_1_0_2(Configuration oldVersion)
        {
            if (string.Compare(oldVersion.Value.ToString().Trim(), "1.0.1") == 0)
            {
                try
                {
                    DbHelper.AddColumn("ApplicationsLog", "Caption", "VarChar(255)");
                    ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "1.0.2"));
                    return true;
                }
                catch (OleDbException ex)
                {
                    Logger.WriteException(ex);
                    return false;
                }
            }
            return false;
        }

        private static bool UpdateFromV_1_0_0ToV_1_0_1(Configuration oldVersion)
        {
            if (string.Compare(oldVersion.Value.ToString().Trim(), "1.0.0") == 0)
            {
                try
                {
                    DbHelper.AddIndex("TasksLog", "InsertTime");
                    ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "1.0.1"));
                    return true;
                }
                catch (OleDbException ex)
                {
                    Logger.WriteException(ex);
                    return false;
                }
            }
            return false;
        }

        private static bool UpdateFromV_0_9_10_ToV_1_0_0(Configuration oldVersion)
        {
            if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.10") == 0)
            {
                try
                {
                    DbHelper.AddColumn("Tasks", "Hidden", "Bit");
                    DbHelper.AddColumn("Tasks", "Priority", "Integer");
                    DbHelper.AddColumn("Tasks", "Notes", "VarChar(255)");
                    ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "1.0.0"));
                    return true;
                }
                catch (OleDbException ex)
                {
                    Logger.WriteException(ex);
                    return false;
                }
            }
            return false;
        }

        private static bool UpdateFromV099ToV0910(Configuration oldVersion)
        {
            if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.9") == 0)
            {
                try
                {
                    DbHelper.AddColumn("Tasks", "Estimation", "Integer");
                    ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.10"));
                    return true;
                }
                catch (OleDbException ex)
                {
                    Logger.WriteException(ex);
                    return false;
                }
            }
            return false;
        }

	    private static bool UpdateFromV098ToV099(Configuration oldVersion)
        {
            if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.8") == 0)
            {
                try
                {
                    ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.9"));
                    return true;
                }
                catch (OleDbException ex)
                {
                    Logger.WriteException(ex);
                    return false;
                }
            }
            return false;
        }

        private static bool UpdateFromV097ToV098(Configuration oldVersion)
        {
            if (string.Compare(oldVersion.Value.ToString().Trim(), "0.9.7") == 0)
            {
                try
                {
                    ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.PlaySoundOnReminder, "1"));
                    ConfigurationHelper.SaveConfiguration(new Configuration(ConfigurationKey.DataBaseVersion, "0.9.8"));
                    return true;
                }
                catch (OleDbException ex)
                {
                    Logger.WriteException(ex);
                    return false;
                }
            }
            return false;
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
                    Logger.WriteException(ex);
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
                    Logger.WriteException(ex);
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
                    Logger.WriteException(ex);
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
                    Logger.WriteException(ex);
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
                    Logger.WriteException(ex);
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
                    Logger.WriteException(ex);
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
                    Logger.WriteException(ex);
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
                    Logger.WriteException(ex);
					return false;
				}
			}
			return false;
		}
	}
}