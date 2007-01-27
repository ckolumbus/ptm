using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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
		private OleDbCommand oleDbSelectCommand1;
		private OleDbCommand oleDbInsertCommand1;
		private OleDbCommand oleDbUpdateCommand1;
		private OleDbCommand oleDbDeleteCommand1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public DataAdapterManager()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			string dataSource = DbHelper.GetDataSource();
			string connectionString =
				this.desingOleDbConnection.ConnectionString.Replace(this.desingOleDbConnection.DataSource, dataSource);
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
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			// 
			// desingOleDbConnection
			// 
			this.desingOleDbConnection.ConnectionString =
				@"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source=""C:\PTM_trunk\database\ptm.mdb"";Jet OLEDB:Engine Type=5;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1";
			// 
			// tasksDataAdapter
			// 
			this.tasksDataAdapter.DeleteCommand = this.oleDbDeleteCommand1;
			this.tasksDataAdapter.InsertCommand = this.oleDbInsertCommand1;
			this.tasksDataAdapter.SelectCommand = this.oleDbSelectCommand1;
			this.tasksDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[]
			                                             	{
			                                             		new System.Data.Common.DataTableMapping("Table", "Tasks",
			                                             		                                        new
			                                             		                                        	System.Data.Common.
			                                             		                                        	DataColumnMapping[]
			                                             		                                        	{
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping(
			                                             		                                        			"Description",
			                                             		                                        			"Description"),
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping(
			                                             		                                        			"IconId", "IconId"),
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping("Id",
			                                             		                                        			                  "Id"),
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping(
			                                             		                                        			"IsActive", "IsActive"),
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping(
			                                             		                                        			"IsFinished", "IsFinished")
			                                             		                                        		,
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping(
			                                             		                                        			"ParentId", "ParentId"),
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping(
			                                             		                                        			"StartDate", "StartDate"),
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping(
			                                             		                                        			"StopDate", "StopDate"),
			                                             		                                        		new
			                                             		                                        			System.Data.Common.
			                                             		                                        			DataColumnMapping(
			                                             		                                        			"TotalTime", "TotalTime")
			                                             		                                        	})
			                                             	});
			this.tasksDataAdapter.UpdateCommand = this.oleDbUpdateCommand1;
			this.tasksDataAdapter.RowUpdated +=
				new System.Data.OleDb.OleDbRowUpdatedEventHandler(this.tasksDataAdapter_RowUpdated);
			// 
			// oleDbDeleteCommand1
			// 
			this.oleDbDeleteCommand1.CommandText = "DELETE FROM Tasks WHERE (Id = ?)";
			this.oleDbDeleteCommand1.Connection = this.desingOleDbConnection;
			this.oleDbDeleteCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("Id", System.Data.OleDb.OleDbType.Integer, 0,
				                                     System.Data.ParameterDirection.Input, false, ((System.Byte) (0)),
				                                     ((System.Byte) (0)), "Id", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand1
			// 
			this.oleDbInsertCommand1.CommandText =
				"INSERT INTO Tasks(Description, IconId, IsActive, IsFinished, ParentId, StartDate," +
				" StopDate, TotalTime) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand1.Connection = this.desingOleDbConnection;
			this.oleDbInsertCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 80, "Description"));
			this.oleDbInsertCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("IconId", System.Data.OleDb.OleDbType.Integer, 0, "IconId"));
			this.oleDbInsertCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("IsActive", System.Data.OleDb.OleDbType.Boolean, 2, "IsActive"));
			this.oleDbInsertCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("IsFinished", System.Data.OleDb.OleDbType.Boolean, 2, "IsFinished"));
			this.oleDbInsertCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("ParentId", System.Data.OleDb.OleDbType.Integer, 0, "ParentId"));
			this.oleDbInsertCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("StartDate", System.Data.OleDb.OleDbType.DBDate, 0, "StartDate"));
			this.oleDbInsertCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("StopDate", System.Data.OleDb.OleDbType.DBDate, 0, "StopDate"));
			this.oleDbInsertCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("TotalTime", System.Data.OleDb.OleDbType.Integer, 0, "TotalTime"));
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText =
				"SELECT Description, IconId, Id, IsActive, IsFinished, ParentId, StartDate, StopDa" +
				"te, TotalTime FROM Tasks";
			this.oleDbSelectCommand1.Connection = this.desingOleDbConnection;
			// 
			// oleDbUpdateCommand1
			// 
			this.oleDbUpdateCommand1.CommandText =
				"UPDATE Tasks SET Description = ?, IconId = ?, IsActive = ?, IsFinished = ?, Paren" +
				"tId = ?, StartDate = ?, StopDate = ?, TotalTime = ? WHERE (Id = ?)";
			this.oleDbUpdateCommand1.Connection = this.desingOleDbConnection;
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("Description", System.Data.OleDb.OleDbType.VarWChar, 80, "Description"));
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("IconId", System.Data.OleDb.OleDbType.Integer, 0, "IconId"));
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("IsActive", System.Data.OleDb.OleDbType.Boolean, 2, "IsActive"));
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("IsFinished", System.Data.OleDb.OleDbType.Boolean, 2, "IsFinished"));
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("ParentId", System.Data.OleDb.OleDbType.Integer, 0, "ParentId"));
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("StartDate", System.Data.OleDb.OleDbType.DBDate, 0, "StartDate"));
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("StopDate", System.Data.OleDb.OleDbType.DBDate, 0, "StopDate"));
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("TotalTime", System.Data.OleDb.OleDbType.Integer, 0, "TotalTime"));
			this.oleDbUpdateCommand1.Parameters.Add(
				new System.Data.OleDb.OleDbParameter("Original_Id", System.Data.OleDb.OleDbType.Integer, 0,
				                                     System.Data.ParameterDirection.Input, false, ((System.Byte) (0)),
				                                     ((System.Byte) (0)), "Id", System.Data.DataRowVersion.Original, null));
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
			lock (e.Command.Connection)
			{
				int lastId;
				OleDbCommand idCMD = new OleDbCommand("SELECT @@IDENTITY", e.Command.Connection);

				if (e.StatementType == StatementType.Insert)
				{
					lastId = (int) idCMD.ExecuteScalar();
					e.Row["Id"] = lastId;
				}
			}
		}
	}
}