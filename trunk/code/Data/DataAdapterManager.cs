using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace PTM.Data
{
	/// <summary>
	/// Summary description for DataAdapterManager2.
	/// </summary>
	public class DataAdapterManager : Control
	{
		private OleDbConnection desingOleDbConnection;
		private OleDbConnection productionOleDbConnection;
		internal OleDbDataAdapter tasksDataAdapter;
		internal OleDbDataAdapter defaultTaskDataAdapter;
		private OleDbCommand oleDbSelectCommand3;
		private OleDbCommand oleDbInsertCommand3;
		private OleDbCommand oleDbUpdateCommand3;
		private OleDbCommand oleDbDeleteCommand3;
		internal OleDbDataAdapter applicationsLogDataAdapter;
		internal OleDbDataAdapter applicationsSummaryDataAdapter;
		private OleDbCommand oleDbSelectCommand6;
		internal OleDbDataAdapter configurationDataAdapter;
		private OleDbCommand oleDbSelectCommand8;
		private OleDbCommand oleDbInsertCommand6;
		private OleDbCommand oleDbUpdateCommand6;
		private OleDbCommand oleDbDeleteCommand6;
		private OleDbCommand oleDbSelectCommand2;
		private OleDbCommand oleDbInsertCommand2;
		private OleDbCommand oleDbUpdateCommand2;
		private OleDbCommand oleDbDeleteCommand2;
		private OleDbCommand oleDbSelectCommand1;
		private OleDbCommand oleDbInsertCommand1;
		private OleDbCommand oleDbUpdateCommand1;
		private OleDbCommand oleDbDeleteCommand1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private static string userNameData;
		//private string connectionString;
		//private OleDbConnection[] connections;

		public DataAdapterManager(string userName)
		{
			// This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

			userNameData = userName;
			string dataSource = GetDataSource();
			connectionString = this.desingOleDbConnection.ConnectionString.Replace(this.desingOleDbConnection.DataSource, dataSource);
			this.productionOleDbConnection = this.desingOleDbConnection;
			//this.productionOleDbConnection = new OleDbConnection(connectionString);
			this.productionOleDbConnection.ConnectionString = connectionString;
			
		}

		private static string GetDataSource()
		{
			string appdir = Directory.GetCurrentDirectory();
			string dbdir = appdir + @"\" + userNameData;
			if (!Directory.Exists(dbdir))
				Directory.CreateDirectory(dbdir);

			//string dataSource = dbdir + @"\" + ConfigurationSettings.AppSettings["DefaultFileName"] + ".mdb";
			string dataSource = dbdir + @"\data.mdb";
			if (!File.Exists(dataSource))
				File.Copy(appdir + @"\ptm.mdb", dataSource, false);

			return dataSource;
		}


		public bool DeleteDataSource()
		{
			string appdir = Directory.GetCurrentDirectory();
			string dbdir = appdir + @"\" + userNameData;
			if (!Directory.Exists(dbdir))
				return false;

			//string dataSource = dbdir + @"\" + ConfigurationSettings.AppSettings["DefaultFileName"] + ".mdb";
			string dataSource = dbdir + @"\data.mdb";
			if (!File.Exists(dataSource))
				return false;
			else
			{
				File.Delete(dataSource);
				return true;
			}
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.desingOleDbConnection = new System.Data.OleDb.OleDbConnection();
			this.productionOleDbConnection = new System.Data.OleDb.OleDbConnection();
			this.tasksDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
			this.defaultTaskDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand3 = new System.Data.OleDb.OleDbCommand();
			this.applicationsLogDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.applicationsSummaryDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand6 = new System.Data.OleDb.OleDbCommand();
			this.configurationDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand6 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand6 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand8 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand6 = new System.Data.OleDb.OleDbCommand();
			// 
			// desingOleDbConnection
			// 
			this.desingOleDbConnection.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source=""C:\PTM-Sourceforge\trunk\database\ptm.mdb"";Jet OLEDB:Engine Type=5;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1";
			// 
			// tasksDataAdapter
			// 
			this.tasksDataAdapter.DeleteCommand = this.oleDbDeleteCommand2;
			this.tasksDataAdapter.InsertCommand = this.oleDbInsertCommand2;
			this.tasksDataAdapter.SelectCommand = this.oleDbSelectCommand2;
			this.tasksDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "Tasks", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("DefaultTaskId", "DefaultTaskId"),
																																																				new System.Data.Common.DataColumnMapping("Description", "Description"),
																																																				new System.Data.Common.DataColumnMapping("Id", "Id"),
																																																				new System.Data.Common.DataColumnMapping("IsDefaultTask", "IsDefaultTask"),
																																																				new System.Data.Common.DataColumnMapping("IsFinished", "IsFinished"),
																																																				new System.Data.Common.DataColumnMapping("ParentId", "ParentId"),
																																																				new System.Data.Common.DataColumnMapping("StartDate", "StartDate"),
																																																				new System.Data.Common.DataColumnMapping("StopDate", "StopDate"),
																																																				new System.Data.Common.DataColumnMapping("TotalTime", "TotalTime")})});
			this.tasksDataAdapter.UpdateCommand = this.oleDbUpdateCommand2;
			this.tasksDataAdapter.RowUpdated += new System.Data.OleDb.OleDbRowUpdatedEventHandler(this.tasksDataAdapter_RowUpdated);
			// 
			// oleDbDeleteCommand2
			// 
			this.oleDbDeleteCommand2.CommandText = @"DELETE FROM Tasks WHERE (Id = ?) AND (DefaultTaskId = ? OR ? IS NULL AND DefaultTaskId IS NULL) AND (Description = ?) AND (IsDefaultTask = ?) AND (IsFinished = ?) AND (ParentId = ? OR ? IS NULL AND ParentId IS NULL) AND (StartDate = ? OR ? IS NULL AND StartDate IS NULL) AND (StopDate = ? OR ? IS NULL AND StopDate IS NULL) AND (TotalTime = ? OR ? IS NULL AND TotalTime IS NULL)";
			this.oleDbDeleteCommand2.Connection = this.desingOleDbConnection;
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Id", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DefaultTaskId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DefaultTaskId", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DefaultTaskId1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DefaultTaskId", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description", System.Data.OleDb.OleDbType.VarWChar, 80, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_IsDefaultTask", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "IsDefaultTask", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_IsFinished", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "IsFinished", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ParentId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ParentId", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ParentId1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ParentId", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_StartDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "StartDate", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_StartDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "StartDate", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_StopDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "StopDate", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_StopDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "StopDate", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TotalTime", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TotalTime", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TotalTime1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TotalTime", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand2
			// 
			this.oleDbInsertCommand2.CommandText = "INSERT INTO Tasks(DefaultTaskId, Description, IsDefaultTask, IsFinished, ParentId" +
				", StartDate, StopDate, TotalTime) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand2.Connection = this.desingOleDbConnection;
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("DefaultTaskId", System.Data.OleDb.OleDbType.Integer, 0, "DefaultTaskId"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 80, "Description"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("IsDefaultTask", System.Data.OleDb.OleDbType.Boolean, 2, "IsDefaultTask"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("IsFinished", System.Data.OleDb.OleDbType.Boolean, 2, "IsFinished"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ParentId", System.Data.OleDb.OleDbType.Integer, 0, "ParentId"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("StartDate", System.Data.OleDb.OleDbType.DBDate, 0, "StartDate"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("StopDate", System.Data.OleDb.OleDbType.DBDate, 0, "StopDate"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TotalTime", System.Data.OleDb.OleDbType.Integer, 0, "TotalTime"));
			// 
			// oleDbSelectCommand2
			// 
			this.oleDbSelectCommand2.CommandText = "SELECT DefaultTaskId, Description, Id, IsDefaultTask, IsFinished, ParentId, Start" +
				"Date, StopDate, TotalTime FROM Tasks";
			this.oleDbSelectCommand2.Connection = this.desingOleDbConnection;
			// 
			// oleDbUpdateCommand2
			// 
			this.oleDbUpdateCommand2.CommandText = "UPDATE Tasks SET DefaultTaskId = ?, Description = ?, IsDefaultTask = ?, IsFinishe" +
				"d = ?, ParentId = ?, StartDate = ?, StopDate = ?, TotalTime = ? WHERE (Id = ?)";
			this.oleDbUpdateCommand2.Connection = this.desingOleDbConnection;
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("DefaultTaskId", System.Data.OleDb.OleDbType.Integer, 0, "DefaultTaskId"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 80, "Description"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("IsDefaultTask", System.Data.OleDb.OleDbType.Boolean, 2, "IsDefaultTask"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("IsFinished", System.Data.OleDb.OleDbType.Boolean, 2, "IsFinished"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ParentId", System.Data.OleDb.OleDbType.Integer, 0, "ParentId"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("StartDate", System.Data.OleDb.OleDbType.DBDate, 0, "StartDate"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("StopDate", System.Data.OleDb.OleDbType.DBDate, 0, "StopDate"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TotalTime", System.Data.OleDb.OleDbType.Integer, 0, "TotalTime"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Id", System.Data.OleDb.OleDbType.Integer, 0, "Id"));
			// 
			// defaultTaskDataAdapter
			// 
			this.defaultTaskDataAdapter.DeleteCommand = this.oleDbDeleteCommand3;
			this.defaultTaskDataAdapter.InsertCommand = this.oleDbInsertCommand3;
			this.defaultTaskDataAdapter.SelectCommand = this.oleDbSelectCommand3;
			this.defaultTaskDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											 new System.Data.Common.DataTableMapping("Table", "DefaultTasks", new System.Data.Common.DataColumnMapping[] {
																																																							 new System.Data.Common.DataColumnMapping("Description", "Description"),
																																																							 new System.Data.Common.DataColumnMapping("Id", "Id")})});
			this.defaultTaskDataAdapter.UpdateCommand = this.oleDbUpdateCommand3;
			// 
			// oleDbDeleteCommand3
			// 
			this.oleDbDeleteCommand3.CommandText = "DELETE FROM DefaultTasks WHERE (Id = ?) AND (Description = ? OR ? IS NULL AND Des" +
				"cription IS NULL)";
			this.oleDbDeleteCommand3.Connection = this.desingOleDbConnection;
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Id", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand3
			// 
			this.oleDbInsertCommand3.CommandText = "INSERT INTO DefaultTasks(Description, Id) VALUES (?, ?)";
			this.oleDbInsertCommand3.Connection = this.desingOleDbConnection;
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 50, "Description"));
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Id", System.Data.OleDb.OleDbType.Integer, 0, "Id"));
			// 
			// oleDbSelectCommand3
			// 
			this.oleDbSelectCommand3.CommandText = "SELECT Description, Id FROM DefaultTasks";
			this.oleDbSelectCommand3.Connection = this.desingOleDbConnection;
			// 
			// oleDbUpdateCommand3
			// 
			this.oleDbUpdateCommand3.CommandText = "UPDATE DefaultTasks SET Description = ?, Id = ? WHERE (Id = ?) AND (Description =" +
				" ? OR ? IS NULL AND Description IS NULL)";
			this.oleDbUpdateCommand3.Connection = this.desingOleDbConnection;
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 50, "Description"));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Id", System.Data.OleDb.OleDbType.Integer, 0, "Id"));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Id", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			// 
			// applicationsLogDataAdapter
			// 
			this.applicationsLogDataAdapter.DeleteCommand = this.oleDbDeleteCommand1;
			this.applicationsLogDataAdapter.InsertCommand = this.oleDbInsertCommand1;
			this.applicationsLogDataAdapter.SelectCommand = this.oleDbSelectCommand1;
			this.applicationsLogDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																												 new System.Data.Common.DataTableMapping("Table", "ApplicationsLog", new System.Data.Common.DataColumnMapping[] {
																																																									new System.Data.Common.DataColumnMapping("ActiveTime", "ActiveTime"),
																																																									new System.Data.Common.DataColumnMapping("ApplicationFullPath", "ApplicationFullPath"),
																																																									new System.Data.Common.DataColumnMapping("Caption", "Caption"),
																																																									new System.Data.Common.DataColumnMapping("Id", "Id"),
																																																									new System.Data.Common.DataColumnMapping("LastUpdateTime", "LastUpdateTime"),
																																																									new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																									new System.Data.Common.DataColumnMapping("ProcessId", "ProcessId"),
																																																									new System.Data.Common.DataColumnMapping("TaskLogId", "TaskLogId"),
																																																									new System.Data.Common.DataColumnMapping("UserProcessorTime", "UserProcessorTime")})});
			this.applicationsLogDataAdapter.UpdateCommand = this.oleDbUpdateCommand1;
			this.applicationsLogDataAdapter.RowUpdated += new System.Data.OleDb.OleDbRowUpdatedEventHandler(this.applicationsLogDataAdapter_RowUpdated);
			// 
			// oleDbDeleteCommand1
			// 
			this.oleDbDeleteCommand1.CommandText = @"DELETE FROM ApplicationsLog WHERE (Id = ?) AND (ActiveTime = ? OR ? IS NULL AND ActiveTime IS NULL) AND (ApplicationFullPath = ? OR ? IS NULL AND ApplicationFullPath IS NULL) AND (Caption = ? OR ? IS NULL AND Caption IS NULL) AND (LastUpdateTime = ? OR ? IS NULL AND LastUpdateTime IS NULL) AND (Name = ? OR ? IS NULL AND Name IS NULL) AND (ProcessId = ? OR ? IS NULL AND ProcessId IS NULL) AND (TaskLogId = ?) AND (UserProcessorTime = ? OR ? IS NULL AND UserProcessorTime IS NULL)";
			this.oleDbDeleteCommand1.Connection = this.desingOleDbConnection;
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Id", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ActiveTime", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ActiveTime", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ActiveTime1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ActiveTime", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ApplicationFullPath", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ApplicationFullPath", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ApplicationFullPath1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ApplicationFullPath", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Caption", System.Data.OleDb.OleDbType.VarWChar, 120, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Caption", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Caption1", System.Data.OleDb.OleDbType.VarWChar, 120, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Caption", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_LastUpdateTime", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "LastUpdateTime", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_LastUpdateTime1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "LastUpdateTime", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Name", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Name", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Name1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Name", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ProcessId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessId", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ProcessId1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessId", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TaskLogId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TaskLogId", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_UserProcessorTime", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UserProcessorTime", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_UserProcessorTime1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UserProcessorTime", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand1
			// 
			this.oleDbInsertCommand1.CommandText = "INSERT INTO ApplicationsLog(ActiveTime, ApplicationFullPath, Caption, LastUpdateT" +
				"ime, Name, ProcessId, TaskLogId, UserProcessorTime) VALUES (?, ?, ?, ?, ?, ?, ?," +
				" ?)";
			this.oleDbInsertCommand1.Connection = this.desingOleDbConnection;
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("ActiveTime", System.Data.OleDb.OleDbType.Integer, 0, "ActiveTime"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("ApplicationFullPath", System.Data.OleDb.OleDbType.VarWChar, 255, "ApplicationFullPath"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Caption", System.Data.OleDb.OleDbType.VarWChar, 120, "Caption"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("LastUpdateTime", System.Data.OleDb.OleDbType.DBDate, 0, "LastUpdateTime"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Name", System.Data.OleDb.OleDbType.VarWChar, 50, "Name"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("ProcessId", System.Data.OleDb.OleDbType.Integer, 0, "ProcessId"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TaskLogId", System.Data.OleDb.OleDbType.Integer, 0, "TaskLogId"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("UserProcessorTime", System.Data.OleDb.OleDbType.Integer, 0, "UserProcessorTime"));
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT ActiveTime, ApplicationFullPath, Caption, Id, LastUpdateTime, Name, Proces" +
				"sId, TaskLogId, UserProcessorTime FROM ApplicationsLog";
			this.oleDbSelectCommand1.Connection = this.desingOleDbConnection;
			// 
			// oleDbUpdateCommand1
			// 
			this.oleDbUpdateCommand1.CommandText = "UPDATE ApplicationsLog SET ActiveTime = ?, ApplicationFullPath = ?, Caption = ?, " +
				"LastUpdateTime = ?, Name = ?, ProcessId = ?, TaskLogId = ?, UserProcessorTime = " +
				"? WHERE (Id = ?)";
			this.oleDbUpdateCommand1.Connection = this.desingOleDbConnection;
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("ActiveTime", System.Data.OleDb.OleDbType.Integer, 0, "ActiveTime"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("ApplicationFullPath", System.Data.OleDb.OleDbType.VarWChar, 255, "ApplicationFullPath"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Caption", System.Data.OleDb.OleDbType.VarWChar, 120, "Caption"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("LastUpdateTime", System.Data.OleDb.OleDbType.DBDate, 0, "LastUpdateTime"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Name", System.Data.OleDb.OleDbType.VarWChar, 50, "Name"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("ProcessId", System.Data.OleDb.OleDbType.Integer, 0, "ProcessId"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TaskLogId", System.Data.OleDb.OleDbType.Integer, 0, "TaskLogId"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("UserProcessorTime", System.Data.OleDb.OleDbType.Integer, 0, "UserProcessorTime"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Id", System.Data.OleDb.OleDbType.Integer, 0, "Id"));
			// 
			// applicationsSummaryDataAdapter
			// 
			this.applicationsSummaryDataAdapter.SelectCommand = this.oleDbSelectCommand6;
			this.applicationsSummaryDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																													 new System.Data.Common.DataTableMapping("Table", "ApplicationsSummary", new System.Data.Common.DataColumnMapping[] {
																																																											new System.Data.Common.DataColumnMapping("TaskId", "TaskId"),
																																																											new System.Data.Common.DataColumnMapping("TotalActiveTime", "TotalActiveTime"),
																																																											new System.Data.Common.DataColumnMapping("TotalApplicationsLog", "TotalApplicationsLog"),
																																																											new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																											new System.Data.Common.DataColumnMapping("ApplicationFullPath", "ApplicationFullPath")})});
			// 
			// oleDbSelectCommand6
			// 
			this.oleDbSelectCommand6.CommandText = @"SELECT TasksLog.TaskId, SUM(ApplicationsLog.ActiveTime) AS TotalActiveTime, COUNT(ApplicationsLog.Id) AS TotalApplicationsLog, ApplicationsLog.Name, ApplicationsLog.ApplicationFullPath FROM (TasksLog INNER JOIN ApplicationsLog ON TasksLog.Id = ApplicationsLog.TaskLogId) GROUP BY TasksLog.TaskId, TasksLog.InsertTime, ApplicationsLog.Name, ApplicationsLog.ApplicationFullPath HAVING (TasksLog.InsertTime >= ?) AND (TasksLog.InsertTime < ?) AND (TasksLog.TaskId = ?) ORDER BY SUM(ApplicationsLog.ActiveTime) DESC";
			this.oleDbSelectCommand6.Connection = this.desingOleDbConnection;
			this.oleDbSelectCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("InsertTime", System.Data.OleDb.OleDbType.DBDate, 0, "InsertTime"));
			this.oleDbSelectCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("InsertTime1", System.Data.OleDb.OleDbType.DBDate, 0, "InsertTime"));
			this.oleDbSelectCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("TaskId", System.Data.OleDb.OleDbType.Integer, 0, "TaskId"));
			// 
			// configurationDataAdapter
			// 
			this.configurationDataAdapter.DeleteCommand = this.oleDbDeleteCommand6;
			this.configurationDataAdapter.InsertCommand = this.oleDbInsertCommand6;
			this.configurationDataAdapter.SelectCommand = this.oleDbSelectCommand8;
			this.configurationDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											   new System.Data.Common.DataTableMapping("Table", "Configuration", new System.Data.Common.DataColumnMapping[] {
																																																								new System.Data.Common.DataColumnMapping("ConfigValue", "ConfigValue"),
																																																								new System.Data.Common.DataColumnMapping("Description", "Description"),
																																																								new System.Data.Common.DataColumnMapping("Id", "Id"),
																																																								new System.Data.Common.DataColumnMapping("KeyValue", "KeyValue"),
																																																								new System.Data.Common.DataColumnMapping("ListValue", "ListValue")})});
			this.configurationDataAdapter.UpdateCommand = this.oleDbUpdateCommand6;
			this.configurationDataAdapter.RowUpdated += new System.Data.OleDb.OleDbRowUpdatedEventHandler(this.configurationDataAdapter_RowUpdated);
			// 
			// oleDbDeleteCommand6
			// 
			this.oleDbDeleteCommand6.CommandText = @"DELETE FROM Configuration WHERE (Id = ?) AND (ConfigValue = ? OR ? IS NULL AND ConfigValue IS NULL) AND (Description = ? OR ? IS NULL AND Description IS NULL) AND (KeyValue = ? OR ? IS NULL AND KeyValue IS NULL) AND (ListValue = ? OR ? IS NULL AND ListValue IS NULL)";
			this.oleDbDeleteCommand6.Connection = this.desingOleDbConnection;
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Id", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ConfigValue", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ConfigValue", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ConfigValue1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ConfigValue", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_KeyValue", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "KeyValue", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_KeyValue1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "KeyValue", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ListValue", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ListValue", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ListValue1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ListValue", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand6
			// 
			this.oleDbInsertCommand6.CommandText = "INSERT INTO Configuration(ConfigValue, Description, KeyValue, ListValue) VALUES (" +
				"?, ?, ?, ?)";
			this.oleDbInsertCommand6.Connection = this.desingOleDbConnection;
			this.oleDbInsertCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("ConfigValue", System.Data.OleDb.OleDbType.VarWChar, 50, "ConfigValue"));
			this.oleDbInsertCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 50, "Description"));
			this.oleDbInsertCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("KeyValue", System.Data.OleDb.OleDbType.Integer, 0, "KeyValue"));
			this.oleDbInsertCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("ListValue", System.Data.OleDb.OleDbType.VarWChar, 50, "ListValue"));
			// 
			// oleDbSelectCommand8
			// 
			this.oleDbSelectCommand8.CommandText = "SELECT ConfigValue, Description, Id, KeyValue, ListValue FROM Configuration";
			this.oleDbSelectCommand8.Connection = this.desingOleDbConnection;
			// 
			// oleDbUpdateCommand6
			// 
			this.oleDbUpdateCommand6.CommandText = @"UPDATE Configuration SET ConfigValue = ?, Description = ?, KeyValue = ?, ListValue = ? WHERE (Id = ?) AND (ConfigValue = ? OR ? IS NULL AND ConfigValue IS NULL) AND (Description = ? OR ? IS NULL AND Description IS NULL) AND (KeyValue = ? OR ? IS NULL AND KeyValue IS NULL) AND (ListValue = ? OR ? IS NULL AND ListValue IS NULL)";
			this.oleDbUpdateCommand6.Connection = this.desingOleDbConnection;
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("ConfigValue", System.Data.OleDb.OleDbType.VarWChar, 50, "ConfigValue"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 50, "Description"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("KeyValue", System.Data.OleDb.OleDbType.Integer, 0, "KeyValue"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("ListValue", System.Data.OleDb.OleDbType.VarWChar, 50, "ListValue"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Id", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ConfigValue", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ConfigValue", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ConfigValue1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ConfigValue", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Description1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Description", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_KeyValue", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "KeyValue", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_KeyValue1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "KeyValue", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ListValue", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ListValue", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ListValue1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ListValue", System.Data.DataRowVersion.Original, null));
			// 
			// DataAdapterManager
			// 
			this.Enabled = false;
			this.TabStop = false;
			this.Visible = false;

		}

		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}

		private void applicationsLogDataAdapter_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
		{
			int lastId;
			OleDbCommand idCMD = new OleDbCommand("SELECT @@IDENTITY", e.Command.Connection);

			if (e.StatementType == StatementType.Insert)
			{
				lastId = (int)idCMD.ExecuteScalar();
				e.Row["Id"] = lastId;
			}

		}

		private void tasksDataAdapter_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
		{
			lock(e.Command.Connection)
			{
				int lastId;
				OleDbCommand idCMD = new OleDbCommand("SELECT @@IDENTITY", e.Command.Connection);

				if (e.StatementType == StatementType.Insert)
				{
					lastId = (int)idCMD.ExecuteScalar();
					e.Row["Id"] = lastId;
				}
			}
		}

		private void configurationDataAdapter_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
		{
			int lastId;
			OleDbCommand idCMD = new OleDbCommand("SELECT @@IDENTITY", e.Command.Connection);

			if (e.StatementType == StatementType.Insert)
			{
				lastId = (int)idCMD.ExecuteScalar();
				e.Row["Id"] = -lastId;
			}
		}

		private static string connectionString;
		private static  OleDbCommand GetNewCommand(string cmdText)
		{
			if(connectionString==null)
			{
				connectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source=""@DATA_SOURCE"";Jet OLEDB:Engine Type=5;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1";
				string dataSource = GetDataSource();
				connectionString.Replace("@DATA_SOURCE", dataSource);
			}
			
			OleDbConnection connection = new OleDbConnection(connectionString);
			OleDbCommand command = new OleDbCommand(cmdText, connection);
			return command;
		}
		public static int ExecuteNonQuery(string cmdText, string[] paramNames,  object[] paramValues)
		{
			OleDbCommand cmd;
			cmd = GetNewCommand(cmdText);
			for(int i = 0; i<paramValues.Length;i++)
			{
				OleDbParameter param = new OleDbParameter(paramNames[i], GetOleDbType(paramValues[i]));
				param.Value = paramValues[i];
				param.SourceColumn = paramNames[i];
				cmd.Parameters.Add(param);
			}
			try
			{
				cmd.Connection.Open();
				return cmd.ExecuteNonQuery();	
			}
			finally
			{
				cmd.Connection.Close();				
			}
		}
		
		public static int ExecuteNonQuery(string cmdText)
		{
			OleDbCommand cmd;
			cmd = GetNewCommand(cmdText);
			try
			{
				cmd.Connection.Open();
				return cmd.ExecuteNonQuery();	
			}
			finally
			{
				cmd.Connection.Close();				
			}
		}

		private static OleDbType GetOleDbType(object paramValue)
		{
			if(paramValue== null || paramValue == DBNull.Value)
				return OleDbType.Variant;
			if(paramValue.GetType() == typeof(int))
				return OleDbType.Integer;
			if(paramValue.GetType() == typeof(DateTime))
				return OleDbType.Date;
			if(paramValue.GetType() == typeof(string))
				return OleDbType.VarWChar;
			
			throw new DataException("Type Db type not found:" + paramValue.ToString());
		}

		public static Hashtable ExecuteGetFirstRow(string cmdText)
		{
			OleDbCommand cmd;
			cmd = GetNewCommand(cmdText);
			try
			{
				cmd.Connection.Open();
				OleDbDataReader reader = cmd.ExecuteReader();
				if(!reader.HasRows)
					return null;
				Hashtable hash = new Hashtable();
				reader.Read();
				for(int i=0;i<reader.FieldCount;i++)
					hash.Add(reader.GetName(i), reader[i]);
				reader.Close();				
				return hash;				
			}
			finally
			{
				cmd.Connection.Close();
			}
		}
		public static ArrayList ExecuteGetRows(string cmdText, string[] paramNames,  object[] paramValues)
		{
			OleDbCommand cmd;
			cmd = GetNewCommand(cmdText);
			for(int i = 0; i<paramValues.Length;i++)
			{
				OleDbParameter param = new OleDbParameter(paramNames[i], GetOleDbType(paramValues[i]));
				param.Value = paramValues[i];
				param.SourceColumn = paramNames[i];
				cmd.Parameters.Add(param);
			}
			try
			{			
				cmd.Connection.Open();
				OleDbDataReader reader = cmd.ExecuteReader();
				if(!reader.HasRows)
					return new ArrayList();
				ArrayList list = new ArrayList();
				while(reader.Read())
				{
					Hashtable hash = new Hashtable();
					for(int i=0;i<reader.FieldCount;i++)
						hash.Add(reader.GetName(i), reader[i]);
					list.Add(hash);
				}
				reader.Close();
				return list;
			}
			finally
			{
				cmd.Connection.Close();
			}
		}
		public static int ExecuteInsert(string cmdText, string[] paramNames, object[] paramValues)
		{
			OleDbCommand cmd;
			cmd = GetNewCommand(cmdText);
			for(int i = 0; i<paramValues.Length;i++)
			{
				OleDbParameter param = new OleDbParameter(paramNames[i], GetOleDbType(paramValues[i]));
				param.Value = paramValues[i];
				param.SourceColumn = paramNames[i];
				cmd.Parameters.Add(param);
			}
			try
			{
				cmd.Connection.Open();
				int r = cmd.ExecuteNonQuery();
				if(r==0)
					throw new DataException("Database is not responding.");
				cmd = new OleDbCommand("SELECT @@IDENTITY", cmd.Connection);
				return (int) cmd.ExecuteScalar();
			}
			finally
			{
				cmd.Connection.Close();				
			}
			
		}
	}
}