using System;
using System.Collections;
using System.Data;
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
			rows = DbHelper.ExecuteGetRows("Select * from DefaultTasks");
			foreach (Hashtable ht in rows)
			{
				DefaultTask dt = new DefaultTask();
				dt.DefaultTaskId = (int) ht["Id"];
				dt.Description = (string) ht["Description"];
				dt.IsActive = (bool) ht["IsActive"];
				dt.IconId = (int) ht["Icon"];
				dt.Hidden = (bool) ht["Hidden"];
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
				if(defaultTaskId == DefaultTasks.IdleTaskId)
					throw new ApplicationException("Idle common task can't be deleted.");
				DbHelper.ExecuteNonQuery("Update DefaultTasks Set Hidden = ? Where Id  = ?", new string[] {"Hidden", "Id"},
				                         new object[] {true, defaultTaskId});
				DefaultTask df = (DefaultTask) table[defaultTaskId];
				df.Hidden = true;
				if(DefaultTaskChanged!=null)
				{
					DefaultTaskChanged(new DefaultTaskChangeEventArgs(df, DataRowAction.Delete));
				}
			}
			else
			{
				throw new ApplicationException("Delete failed. The common task with Id = " + defaultTaskId + " doesn't exists.");		
			}			
		}

		public static int Add(string description, bool isActive, int iconId)
		{
			if (description.Trim().Length == 0)
				throw new ApplicationException("Description can't be empty");
			
			description = description.Trim();
			
			int id = Convert.ToInt32(DbHelper.ExecuteScalar("Select Max(Id) from DefaultTasks")) + 1;
			DbHelper.ExecuteNonQuery("Insert into DefaultTasks (Id, Description, IsActive, Icon, Hidden) values (?, ?, ?, ?, ?)",
			                         new string[] {"Id", "Description", "IsActive", "Icon", "Hidden"}, 
			                         new object[] {id, description, isActive, iconId, false});
			
			DefaultTask df = new DefaultTask();
			df.DefaultTaskId = id;
			df.Description = description;
			df.IsActive = isActive;
			df.IconId = iconId;
			table.Add(id, df);
			if(DefaultTaskChanged!=null)
			{
				DefaultTaskChanged(new DefaultTaskChangeEventArgs(df, DataRowAction.Add));
			}
			return id;
		}

		public static void Update(int defaultTaskId, string description, bool isActive, int iconId)
		{
			if(table.ContainsKey(defaultTaskId))
			{
				if (description.Trim().Length == 0)
					throw new ApplicationException("Description can't be empty");
				
				description = description.Trim();
				
				DbHelper.ExecuteNonQuery("Update DefaultTasks Set Description = ?, IsActive = ?, Icon = ? Where Id  = " + defaultTaskId.ToString(), 
					new string[]{"Description", "IsActive", "Icon"}, new object[]{description, isActive, iconId} );
				DefaultTask df = new DefaultTask();
				df.DefaultTaskId = defaultTaskId;
				df.Description = description;
				df.IsActive = isActive;
				df.IconId = iconId;
				table[defaultTaskId] = df;
				if(DefaultTaskChanged!=null)
				{
					DefaultTaskChanged(new DefaultTaskChangeEventArgs(df, DataRowAction.Change));
				}
			}
			else
			{
				throw new ApplicationException("Update failed. The common task with Id = " + defaultTaskId + " doesn't exists.");
			}	
		}
		
		public delegate void DefaultTaskChangeEventHandler(DefaultTaskChangeEventArgs e);
		public class DefaultTaskChangeEventArgs : EventArgs
		{
			private DefaultTask defaultTask;
			private DataRowAction action;
			public DefaultTaskChangeEventArgs(DefaultTask defaultTask, DataRowAction action)
			{
				this.defaultTask = defaultTask;
				this.action = action;
			}
			public DefaultTask DefaultTask
			{
				get { return defaultTask; }
			}
			
			public DataRowAction Action
			{
				get { return action; }
			}
		}
		public static event DefaultTaskChangeEventHandler DefaultTaskChanged;
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
		private bool hidden;

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

		public bool Hidden
		{
			get{return hidden;}
			set { hidden = value;}
		}
	}

}
