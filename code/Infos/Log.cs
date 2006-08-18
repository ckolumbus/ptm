using System;

namespace PTM.Infos
{
	/// <summary>
	/// Summary description for Log.
	/// </summary>
	public class Log
	{
		public Log()
		{
		}
		
		private int id;
		private int taskId;
		private int duration;
		private DateTime insertTime;

		public DateTime InsertTime
		{
			get { return insertTime; }
			set { insertTime = value; }
		}

		public int Duration
		{
			get { return duration; }
			set { duration = value; }
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public int TaskId
		{
			get { return taskId; }
			set { taskId = value; }
		}
		
		
		
	}
}
