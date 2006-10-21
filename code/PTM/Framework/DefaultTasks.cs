using System;
using System.Collections;
using PTM.Data;

namespace PTM.Business
{
	/// <summary>
	/// Descripción breve de DefaultTasks.
	/// </summary>
	public sealed class DefaultTasks : ICollection
	{
		private DefaultTasks()
		{
		}
		
		private static ArrayList list;
		private static DefaultTasks instance;
		public static void Initialize()
		{
			instance = new DefaultTasks();
			list = new ArrayList();
			list.Add(new DefaultTask(DefaultTaskEnum.Idle));
			list.Add(new DefaultTask(DefaultTaskEnum.LunchTime));
			list.Add(new DefaultTask(DefaultTaskEnum.OtherPersonal));
			list.Add(new DefaultTask(DefaultTaskEnum.JobPhoneCall));
			list.Add(new DefaultTask(DefaultTaskEnum.CheckingJobMail));
			list.Add(new DefaultTask(DefaultTaskEnum.JobMeeting));
		}

		public static DefaultTasks List
		{
			get { return instance; }
		}
		public static string GetDefaultTaskDescription(DefaultTaskEnum defaultTaskEnum)
		{
			switch(defaultTaskEnum)
			{
				case DefaultTaskEnum.Idle:
					return "Idle";
				case DefaultTaskEnum.LunchTime:
					return "Lunch Time";
				case DefaultTaskEnum.OtherPersonal:
					return "Other/Personal";
				case DefaultTaskEnum.JobPhoneCall:
					return "Job Phone Call";
				case DefaultTaskEnum.CheckingJobMail:
					return "Checking Job Email";
				case DefaultTaskEnum.JobMeeting:
					return "Job Meeting";
			}
			return defaultTaskEnum.ToString();
		}
		
		public static bool IsActive(DefaultTaskEnum defaultTaskEnum)
		{
			switch(defaultTaskEnum)
			{
				case DefaultTaskEnum.Idle:
					return false;
				case DefaultTaskEnum.LunchTime:
					return false;
				case DefaultTaskEnum.OtherPersonal:
					return false;
				case DefaultTaskEnum.JobPhoneCall:
					return true;
				case DefaultTaskEnum.CheckingJobMail:
					return true;
				case DefaultTaskEnum.JobMeeting:
					return true;
			}
			return false;
		}
		
		public void CopyTo(Array array, int index)
		{
			list.CopyTo(array, index);
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
			get { return list.IsSynchronized; }
		}

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}
		
		public DefaultTask this[int index]
		{
			get
			{
				return (DefaultTask)list[index];
			}
		}
	}
	
	public class DefaultTask
	{
		public DefaultTask(DefaultTaskEnum type)
		{
			this.type = type;
		}
		public DefaultTaskEnum type;
		public string Description
	{
		get { return DefaultTasks.GetDefaultTaskDescription(type); }
	}

		public int DefaultTaskId
		{
			get { return (int)type; }
		}
		
		public bool IsActive
		{
			get
			{
				return DefaultTasks.IsActive(type);
			}
		}
	}
	
	public enum DefaultTaskEnum : int
	{
		Idle = 1,
		LunchTime = 2,
		OtherPersonal = 3,
		JobPhoneCall = 4,
		CheckingJobMail = 5,
		JobMeeting = 7
	}
}
