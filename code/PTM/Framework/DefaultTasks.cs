using System;
using System.Collections;
using PTM.Data;

namespace PTM.Framework
{
	/// <summary>
	/// Descripción breve de DefaultTasks.
	/// </summary>
	public sealed class DefaultTasks : ICollection
	{
		private DefaultTasks()
		{
			
		}
		
		public const int IdleTaskId = 1;
		private static ArrayList list;
		
		public static void Initialize()
		{
			list = new ArrayList();

			ArrayList rows;
			rows = DbHelper.ExecuteGetRows("Select * from DefaultTasks");
			foreach (Hashtable ht in rows)
			{
				DefaultTask dt = new DefaultTask();
				dt.DefaultTaskId = (int) ht["Id"];
				dt.Description = (string) ht["Description"];
				dt.IsActive = (bool) ht["IsActive"];
				dt.IconId = (int) ht["Icon"];
				list.Add(dt);
			}
		}

		public static DefaultTask GetDefaultTask(int defaultTaskId)
		{
			foreach (DefaultTask defaultTask in list)
			{
				if(defaultTask.DefaultTaskId == defaultTaskId)
					return defaultTask;
			}
			return null;			
		}
		
		public static ArrayList List
		{
			get{ return (ArrayList) list.Clone();}
		}


		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { return list.Count; }
		}

		public object SyncRoot
		{
			get { return list.SyncRoot; }
		}

		public bool IsSynchronized
		{
			get {return list.IsSynchronized; }
		}

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
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
