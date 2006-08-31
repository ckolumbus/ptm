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
		public static void Initialize()
		{
			list = new ArrayList();
			list.Add(new DefaultTask(DefaultTaskEnum.Idle));
			list.Add(new DefaultTask(DefaultTaskEnum.LunchTime));
			list.Add(new DefaultTask(DefaultTaskEnum.OtherPersonal));
			list.Add(new DefaultTask(DefaultTaskEnum.PhoneCall));
			list.Add(new DefaultTask(DefaultTaskEnum.CheckingMail));
			list.Add(new DefaultTask(DefaultTaskEnum.OnAMeeting));
			defaultTasksDataTable = new PTMDataset.TasksDataTable();
			LoadDefaultTasks();
		}

		private static void LoadDefaultTasks()
		{
			defaultTasksDataTable.BeginLoadData();
			foreach (ManagementDataset.DefaultTasksRow row in ConfigurationHelper.DefaultTasks.Rows)
			{
				PTMDataset.TasksRow trow = defaultTasksDataTable.NewTasksRow();
				trow.DefaultTaskId = row.Id;
				trow.Description = row.Description;
				trow.IsDefaultTask = true;
				defaultTasksDataTable.Rows.Add(trow);
			}
			defaultTasksDataTable.EndLoadData();
		}

		private static PTMDataset.TasksDataTable defaultTasksDataTable;

		internal static DefaultTasks DefaultTasksDataTable
		{
			get { return new DefaultTasks(); }
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
				case DefaultTaskEnum.PhoneCall:
					return "Job Phone Call";
				case DefaultTaskEnum.CheckingMail:
					return "Checking Job Email";
				case DefaultTaskEnum.OnAMeeting:
					return "Job Meeting";
			}
			return defaultTaskEnum.ToString();
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
	}
	
	public enum DefaultTaskEnum : int
	{
		Idle = 1,
		LunchTime = 2,
		OtherPersonal = 3,
		PhoneCall = 4,
		CheckingMail = 5,
		OnAMeeting = 7
	}
}
