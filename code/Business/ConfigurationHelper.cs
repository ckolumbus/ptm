using System;
using System.Data.Common;
using System.Globalization;
using PTM.Data;

namespace PTM.Business
{
	internal enum ConfigurationKey : int
	{
		DefaultTasksLogDuration = 1,
		DefaultFileName = 2,
		UserName = 3,
		MaxExistingGroupsDisplayed = 4,
		WorkHours = 5,
		RecentTasks = 100
	}

	internal sealed class ConfigurationHelper
	{
		private ConfigurationHelper()
		{
		}

		private static ManagementDataset configurationDataset = new ManagementDataset();
		private static DbDataAdapter configurationDataAdapter;
		private static DbDataAdapter defaultTaskDataAdapter;

		internal static void Initialize(DbDataAdapter configurationAdapter, DbDataAdapter defaultTaskAdapter)
		{
			configurationDataAdapter = configurationAdapter;
			defaultTaskDataAdapter = defaultTaskAdapter;
			Load();
		}

		private static void Load()
		{
			configurationDataAdapter.Fill(configurationDataset.Configuration);
			defaultTaskDataAdapter.Fill(configurationDataset.DefaultTasks);
		}

		internal static ManagementDataset.DefaultTasksDataTable DefaultTasks
		{
			get { return configurationDataset.DefaultTasks; }
		}

		internal static ManagementDataset.ConfigurationRow GetConfiguration(ConfigurationKey config)
		{
			return (ManagementDataset.ConfigurationRow) configurationDataset.Configuration.Select(configurationDataset.Configuration.KeyValueColumn.ColumnName +
				"=" + (int) config)[0];
		}

		internal static void AddRecentTask(PTMDataset.TasksRow tasksRow)
		{
			ManagementDataset.ConfigurationRow[] groups = GetExistingGroups();
			foreach (ManagementDataset.ConfigurationRow configurationRow in groups)
			{
				if (tasksRow.Id == Convert.ToInt32(configurationRow.ConfigValue, CultureInfo.InvariantCulture))
					return;
			}

			
			ManagementDataset.ConfigurationRow row = configurationDataset.Configuration.NewConfigurationRow();
			row.KeyValue = (int) ConfigurationKey.RecentTasks;
			row.Description = ConfigurationKey.RecentTasks.ToString(CultureInfo.InvariantCulture);
			row.ConfigValue = tasksRow.Id.ToString(CultureInfo.InvariantCulture);
			row.ListValue = "0";
			configurationDataset.Configuration.Rows.Add(row);

			foreach (ManagementDataset.ConfigurationRow configurationRow in groups)
			{
				configurationRow.ListValue = (Convert.ToInt32(configurationRow.ListValue, CultureInfo.InvariantCulture) 
																			+ 1).ToString(CultureInfo.InvariantCulture);
			}

			if (groups.Length >= Convert.ToInt32(GetConfiguration(ConfigurationKey.MaxExistingGroupsDisplayed).ConfigValue))
			{
				//groups[groups.Length-1].Delete();
				groups[0].Delete();
			}
			Save();
		}

		internal static ManagementDataset.ConfigurationRow[] GetExistingGroups()
		{
			return (ManagementDataset.ConfigurationRow[]) configurationDataset.Configuration.Select(configurationDataset.Configuration.KeyValueColumn.ColumnName +
				"=" + (int) ConfigurationKey.RecentTasks);
		}

		internal static void Save()
		{
			configurationDataAdapter.Update(configurationDataset.Configuration);

			for (int i = 0; i < configurationDataset.Configuration.Rows.Count; i++)
			{
				configurationDataset.Configuration.Rows[i][configurationDataset.Configuration.IdColumn] = (int) configurationDataset.Configuration.Rows[i][configurationDataset.Configuration.IdColumn] < 0 ?
					-(int) configurationDataset.Configuration.Rows[i][configurationDataset.Configuration.IdColumn] :
					configurationDataset.Configuration.Rows[i][configurationDataset.Configuration.IdColumn];
			}
			configurationDataset.Configuration.AcceptChanges();
		}
	}
}
