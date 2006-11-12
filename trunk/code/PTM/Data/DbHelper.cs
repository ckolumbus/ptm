using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

namespace PTM.Data
{
	/// <summary>
	/// Summary description for DbHelper.
	/// </summary>
	public sealed class DbHelper
	{
		private DbHelper()
		{
		}
		
		private static string userNameData;
		private static string connectionString;
		public static void Initialize(string userName)
		{
			userNameData = userName;
			string dataSource = GetDataSource();
			connectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source=""@DATA_SOURCE"";Jet OLEDB:Engine Type=5;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1";
			connectionString = connectionString.Replace("@DATA_SOURCE", dataSource);
		}
		
		
		public static string GetDataSource()
		{
			//string appdir = Directory.GetCurrentDirectory();
			//string appdir = Application.StartupPath;
			//string appdir = Path.GetDirectoryName(Application.ExecutablePath);
			string appdir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			
			string dbdir = appdir + @"\" + userNameData;
			if (!Directory.Exists(dbdir))
				Directory.CreateDirectory(dbdir);

			string dataSource = dbdir + @"\data.mdb";
			if (!File.Exists(dataSource))
				File.Copy(appdir + @"\ptm.mdb", dataSource, false);

			return dataSource;
		}
		
		public static bool DeleteDataSource()
		{
			string appdir = Directory.GetCurrentDirectory();
			string dbdir = appdir + @"\" + userNameData;
			if (!Directory.Exists(dbdir))
				return false;

			string dataSource = dbdir + @"\data.mdb";
			if (!File.Exists(dataSource))
				return false;
			else
			{
				File.Delete(dataSource);
				return true;
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
		public static ArrayList ExecuteGetRows(string cmdText)
		{
			OleDbCommand cmd;
			cmd = GetNewCommand(cmdText);
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
			catch(Exception ex)
			{
				ex = ex;
				throw;
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
		
		
		public static object ExecuteScalar(string cmdText)
		{
			OleDbCommand cmd;
			cmd = GetNewCommand(cmdText);
			try
			{
				cmd.Connection.Open();
				return cmd.ExecuteScalar();	
			}
			finally
			{
				cmd.Connection.Close();				
			}
		}
		public static object ExecuteScalar(string cmdText, string[] paramNames,  object[] paramValues)
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
				return cmd.ExecuteScalar();	
			}
			finally
			{
				cmd.Connection.Close();				
			}
		}
		
		
		public static  OleDbCommand GetNewCommand(string cmdText)
		{
			OleDbConnection connection = GetConnection();
			OleDbCommand command = new OleDbCommand(cmdText, connection);
			return command;
		}

		public static OleDbConnection GetConnection()
		{
			return new OleDbConnection(connectionString);
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
			if(paramValue.GetType() == typeof(bool))
				return OleDbType.Boolean;
			
			throw new DataException("Type Db type not found:" + paramValue.ToString());
		}

		public static void CompactDB()
		{
			object[] oParams;

			Type typJRO=Type.GetTypeFromProgID("JRO.JetEngine");
			if (typJRO==null)
			{
				//phps. msjro is not registered
				string strMsjrodll=Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles"),@"Common Files\System\ado\msjro.dll");
				if (File.Exists(strMsjrodll))
				{
					//start a process to register the dll
					Process procRegisterMsjro=Process.Start("regsvr32.exe",string.Concat("/s \"",strMsjrodll,"\""));
					procRegisterMsjro.WaitForExit();
					typJRO=Type.GetTypeFromProgID("JRO.JetEngine");
				}
			}

			if (typJRO==null)
			{
				throw new InvalidOperationException("JRO.JetEngine can not be created... please check if it is installed");
			}

			//create an inctance of a Jet Replication Object
			object objJRO = 
				Activator.CreateInstance(typJRO); 
			
			string dataSource = GetDataSource();
			string tempFile = Path.GetDirectoryName(dataSource) + "\\temp.mdb";
			oParams = new object[] {
											  connectionString,
											  "Provider=Microsoft.Jet.OLEDB.4.0;Data" + 
											  " Source="+ tempFile + ";Jet OLEDB:Engine Type=5"};

			objJRO.GetType().InvokeMember("CompactDatabase",
				System.Reflection.BindingFlags.InvokeMethod,
				null,
				objJRO,
				oParams);

			System.IO.File.Delete(dataSource);
			System.IO.File.Move(tempFile, dataSource);

			System.Runtime.InteropServices.Marshal.ReleaseComObject(objJRO);
			objJRO=null;
		}
		
		
		public static void DeleteColumn(string tableName, string columnName)
		{
			ExecuteNonQuery("ALTER TABLE " + tableName + " DROP COLUMN " + columnName);
		}
		
		public static void AddColumn(string tableName, string columnName, string dbType)
		{
			ExecuteNonQuery("ALTER TABLE " + tableName + " ADD " + columnName + " " + dbType);
		}
		
		public static void ModifyColumnType(string tableName, string columnName, string dbType)
		{
			ExecuteNonQuery("ALTER TABLE " + tableName + " ALTER COLUMN " + columnName + " " + dbType);
		}
		
		public static void DeleteConstraint(string tableName, string constraintName)
		{
			ExecuteNonQuery("ALTER TABLE " + tableName + " DROP CONSTRAINT " + constraintName);
		}
		
		public static void AddPrimaryKey(string tableName, string columnName)
		{
			ExecuteNonQuery("ALTER TABLE " + tableName + " ADD PRIMARY KEY (" + columnName + ")");
		}

		public static void CreateTable(string tableName)
		{
			ExecuteNonQuery("CREATE TABLE " + tableName);
		}
	}
}
