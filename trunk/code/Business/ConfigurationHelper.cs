using System;
using System.Data.Common;
using System.Globalization;
using PTM.Data;

namespace PTM.Business
{
	/// <summary>
	/// ConfigurationKey enumeration 
	/// used to match betwen table row Id, and elements.
	/// </summary>
	internal enum ConfigurationKey : int
	{
		DefaultTasksLogDuration = 1,
		DefaultFileName = 2,
		UserName = 3,
		MaxExistingGroupsDisplayed = 4,
		WorkHours = 5,
		RecentTasks = 100
	}//ConfigurationKey enum

	/// <summary>
	/// ConfigurationHelper class 
	/// Helper class 
	/// </summary>
	internal sealed class ConfigurationHelper
	{
		/// <summary>
		/// ConfigurationHelper private Constructor. 
		/// Use Static Methods.
		/// </summary>
		private ConfigurationHelper()
		{
		}//ConfigurationHelper

		private static ManagementDataset configurationDataset = new ManagementDataset();
		private static DbDataAdapter configurationDataAdapter;

		internal static void Initialize(DbDataAdapter configurationAdapter)
		{
			configurationDataAdapter = configurationAdapter;
			Load();
		}//Initialize

		/// <summary>
		/// Loads the Configuration info from the database
		/// </summary>
		private static void Load()
		{
			configurationDataAdapter.Fill(configurationDataset.Configuration);
		}

		/// <summary>
		/// GetConfiguration  receives a configuration key
		/// @return  the configuration first item asociated with this item.
		/// TODO check if the configuration key doesn't exist 
		/// </summary>
		internal static ManagementDataset.ConfigurationRow GetConfiguration( ConfigurationKey config )
		{
			return (ManagementDataset.ConfigurationRow) configurationDataset.Configuration.Select(
				configurationDataset.Configuration.KeyValueColumn.ColumnName +
				"=" + (int) config )[0];
		}//GetConfiguration

		/// <summary>
		/// AddRecentTask  insert as configuration item the recent Task passed as 
		/// parameter.
		/// TODO check if the configuration key doesn't exist 
		/// </summary>
		internal static void AddRecentTask( PTMDataset.TasksRow tasksRow )
		{
			ManagementDataset.ConfigurationRow[] groups = GetExistingGroups( );
			foreach ( ManagementDataset.ConfigurationRow configurationRow in groups )
			{
				if ( tasksRow.Id == Convert.ToInt32(
					configurationRow.ConfigValue, CultureInfo.InvariantCulture ) )
				{
					//If this task is already inside existing groups, return.
					return;
				}
			}//foreach

			
			ManagementDataset.ConfigurationRow row = configurationDataset.Configuration.
				NewConfigurationRow();
			row.KeyValue = (int) ConfigurationKey.RecentTasks;
			row.Description = ConfigurationKey.RecentTasks.ToString( CultureInfo.InvariantCulture );
			row.ConfigValue = tasksRow.Id.ToString( CultureInfo.InvariantCulture );
			row.ListValue = "0";
			configurationDataset.Configuration.Rows.Add( row );

			foreach (ManagementDataset.ConfigurationRow configurationRow in groups)
			{
				configurationRow.ListValue = ( Convert.ToInt32( configurationRow.ListValue, CultureInfo.InvariantCulture ) 
																			+ 1).ToString( CultureInfo.InvariantCulture );
			}//foreach

			if ( groups.Length >= Convert.ToInt32( GetConfiguration( ConfigurationKey.MaxExistingGroupsDisplayed ).ConfigValue ) )
			{
				//groups[groups.Length-1].Delete();
				//Delete de first one (The oldest ).
				groups[0].Delete();
			}//if
			Save();
		}//AddRecentTask

		/// <summary>
		/// GetExistingGroups  Retrieves the rows associates to the
		/// RecentTask enum value.
		/// </summary>
		internal static ManagementDataset.ConfigurationRow[] GetExistingGroups()
		{
			return ( ManagementDataset.ConfigurationRow[]) configurationDataset.Configuration.Select(
				configurationDataset.Configuration.KeyValueColumn.ColumnName +
				"=" + (int) ConfigurationKey.RecentTasks );
		}//GetExistingGroups

		/// <summary>
		/// Save  current configuration
		/// </summary>
		internal static void Save()
		{
			configurationDataAdapter.Update( configurationDataset.Configuration );
			int newValue = 0;

			//What does this?
			for (int i = 0; i < configurationDataset.Configuration.Rows.Count; i++)
			{
				newValue = (int) configurationDataset.Configuration.Rows[i][ configurationDataset.Configuration.IdColumn ];
				
				if( ( (int)configurationDataset.Configuration.Rows[i][ configurationDataset.Configuration.IdColumn ] ) < 0 )
				{
					newValue *= -1 ;
				}//if
				configurationDataset.Configuration.Rows[i][ configurationDataset.Configuration.IdColumn ] = (int) newValue;
			}//for
			configurationDataset.Configuration.AcceptChanges();
		}//Save
	}//ConfigurationHelper
}//namespace
