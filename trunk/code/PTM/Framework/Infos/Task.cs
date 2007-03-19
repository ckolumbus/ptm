

namespace PTM.Framework.Infos
{
	/// <summary>
	/// Descripción breve de Task.
	/// </summary>
	public class Task
	{
		private int id;
		private int parentId;
		private string description;
		private int iconId;
		private bool isActive;


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

		public Task Clone()
		{
			Task task = new Task();
			task.id = this.id;
			task.parentId = this.parentId;
			task.description = this.description;
			task.iconId = this.iconId;
			task.isActive = this.isActive;
			return task;
		}


	}
}
