using System;
using System.Collections;
using PTM.Data;

namespace PTM.Framework
{
	/// <summary>
	/// Descripción breve de DefaultTasks.
	/// </summary>
	public sealed class DefaultTasks
	{
		private DefaultTasks()
		{
			
		}
		
		public const int IdleTaskId = 1;
		private static Hashtable table;
		
		public static void Initialize()
		{
			table = new Hashtable();

			ArrayList rows;
			rows = DbHelper.ExecuteGetRows("Select * from DefaultTasks Where Hidden = 0");
			foreach (Hashtable ht in rows)
			{
				DefaultTask dt = new DefaultTask();
				dt.DefaultTaskId = (int) ht["Id"];
				dt.Description = (string) ht["Description"];
				dt.IsActive = (bool) ht["IsActive"];
				dt.IconId = (int) ht["Icon"];
				table.Add(dt.DefaultTaskId, dt);
			}
		}

		public static DefaultTask GetDefaultTask(int defaultTaskId)
		{
			if(table.Contains(defaultTaskId))
				return (DefaultTask) table[defaultTaskId];
			else
				return null;		
		}
		
		public static Hashtable Table
		{
			get{ return (Hashtable) table.Clone();}
		}
		
		public static void Delete(int defaultTaskId)
		{
			if(table.ContainsKey(defaultTaskId))
			{
				DbHelper.ExecuteNonQuery("Update Set Hidden = 1 from DefaultTasks Where Id  = " + defaultTaskId.ToString());
				table.Remove(defaultTaskId);
			}
			else
			{
				throw new ApplicationException("Delete failed. The common task with Id = " + defaultTaskId + " doesn't exists.");		
			}			
		}

		public static int Add(string description, bool isActive, int iconId)
		{
			int id = Convert.ToInt32(DbHelper.ExecuteScalar("Select Max(Id) from DefaultTasks")) + 1;
			DbHelper.ExecuteNonQuery("Insert into DefaultTasks (Id, Description, IsActive, Icon) values (?, ?, ?, ?)",
			                         new string[] {"Id", "Description", "IsActive", "Icon"}, 
			                         new object[] {id, description, isActive, iconId});
			
			DefaultTask df = new DefaultTask();
			df.DefaultTaskId = id;
			df.Description = description;
			df.IsActive = isActive;
			df.IconId = iconId;
			table.Add(id, df);
			return id;
		}

		public static void Update(int defaultTaskId, string description, bool isActive, int iconId)
		{
			if(table.ContainsKey(defaultTaskId))
			{
				DbHelper.ExecuteNonQuery("Update Set Description = ?, IsActive = ?, Icon = ? from DefaultTasks Where Id  = " + defaultTaskId.ToString(), 
					new string[]{"Description", "IsActive", "Icon"}, new object[]{description, isActive, iconId} );
				DefaultTask df = new DefaultTask();
				df.DefaultTaskId = defaultTaskId;
				df.Description = description;
				df.IsActive = isActive;
				df.IconId = iconId;
				table[defaultTaskId] = df;
			}
			else
			{
				throw new ApplicationException("Update failed. The common task with Id = " + defaultTaskId + " doesn't exists.");
			}	
		}
	}
	
	public class DefaultTask
	{
		public DefaultTask()
		{
		}
		
		private int defaultTaskId;
		private string description;
		private bool isActive;
		private int iconId;

		public int DefaultTaskId
		{
			get { return defaultTaskId; }
			set { defaultTaskId = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}

		public int IconId
		{
			get { return iconId; }
			set { iconId = value; }
		}
	}
	/*
	public enum DefaultTaskEnum : int
	{
		Idle = 1,
		LunchTime = 2,
		OtherPersonal = 3,
		JobPhoneCall = 4,
		CheckingJobMail = 5,
		JobMeeting = 7
	}*/
}
