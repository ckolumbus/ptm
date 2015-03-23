

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
		private string description = String.Empty;
		private int iconId;
		private bool isActive;
	    private int estimation;
	    private bool hidden;
	    private int priority;
	    private string notes = String.Empty;
        private string accountID = String.Empty;
        private string matPath = String.Empty;

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

        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        public string MatPath
        {
            get { return matPath; }
            set { matPath = value; }
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
		    task.hidden = this.hidden;
		    task.priority = this.priority;
		    task.notes = this.notes;
            task.accountID = this.accountID;
			return task;
		}


		public int CompareTo(object obj)
		{
			return this.description.CompareTo(((Task) obj).description);
		}
	}
}
