

using System;

namespace PTM.Framework.Infos
{
	/// <summary>
	/// Descripción breve de Task.
	/// </summary>
	public class Task : IComparable
	{
		private int id;
		private int parentId;
		private string description;
		private int iconId;
		private bool isActive;
	    private int estimation;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public int ParentId
		{
			get { return parentId; }
			set { parentId = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public int IconId
		{
			get { return iconId; }
			set { iconId = value; }
		}

		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}

	    public int Estimation
	    {
	        get{ return estimation;}
            set { estimation = value; }
	    }

		public Task Clone()
		{
			Task task = new Task();
			task.id = this.id;
			task.parentId = this.parentId;
			task.description = this.description;
			task.iconId = this.iconId;
			task.isActive = this.isActive;
		    task.estimation = this.estimation;
			return task;
		}


		public int CompareTo(object obj)
		{
			return this.description.CompareTo(((Task) obj).description);
		}
	}
}
