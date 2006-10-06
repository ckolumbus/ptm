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
	/// Summary description for DataAdapterManager.
	/// </summary>
	public class DataAdapterManager : Control
	{
		private OleDbConnection desingOleDbConnection;
		private OleDbConnection productionOleDbConnection;
		internal OleDbDataAdapter tasksDataAdapter;
		private OleDbCommand oleDbSelectCommand2;
		private OleDbCommand oleDbInsertCommand2;
		private OleDbCommand oleDbUpdateCommand2;
		private OleDbCommand oleDbDeleteCommand2;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public DataAdapterManager()
		{
			// This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
			string dataSource = DbHelper.GetDataSource();
			string connectionString = this.desingOleDbConnection.ConnectionString.Replace(this.desingOleDbConnection.DataSource, dataSource);
			this.productionOleDbConnection = this.desingOleDbConnection;
			//this.productionOleDbConnection = new OleDbConnection(connectionString);
			this.productionOleDbConnection.ConnectionString = connectionString;
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

		
	}
}