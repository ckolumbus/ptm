namespace PTM.Framework.Infos
{
	/// <summary>
	/// TaskSummary
	/// </summary>
	public class TaskSummary
	{
		/// <summary>
		/// TaskSummary Constructor
		/// </summary>
		public TaskSummary()
		{
		} //TaskSummary

		/// <summary>
		/// Task Id of this summary
		/// </summary>
		private int taskId;

		/// <summary>
		/// Total elapsed time with this task
		/// </summary>
		private double totalActiveTime;

		/// <summary>
		/// Total elapsed inactive time with this task
		/// </summary>
		private double totalInactiveTime;

		/// <summary>
		/// Task description
		/// </summary>
		private string description;

		/// <summary>
		/// Stores if this task is one of the Default tasks.
		/// </summary>
		private bool isDefaultTask;

		/// <summary>
		/// In case of being a Defautl Task stores its defaultTask Id.
		/// </summary>
		private int defaultTaskId;

		/// <summary>
		/// TaskId Accessors
		/// </summary>
		public int TaskId
		{
			get { return taskId; }
			set { taskId = value; }
		} //TaskId

		/// <summary>
		/// TotalActiveTime Accessors
		/// </summary>
		public double TotalActiveTime
		{
			get { return totalActiveTime; }
			set { totalActiveTime = value; }
		} //TotalTime

		/// <summary>
		/// TotalActiveTime Accessors
		/// </summary>
		public double TotalInactiveTime
		{
			get { return totalInactiveTime; }
			set { totalInactiveTime = value; }
		} //TotalTime

		/// <summary>
		/// Description Accessors
		/// </summary>
		public string Description
		{
			get { return description; }
			set { description = value; }
		} //Description

		/// <summary>
		/// IsDefaultTask Accessors
		/// </summary>
		public bool IsDefaultTask
		{
			get { return isDefaultTask; }
			set { isDefaultTask = value; }
		} //IsDefaultTask

		/// <summary>
		/// DefaultTaskId Accessors
		/// </summary>
		public int DefaultTaskId
		{
			get { return defaultTaskId; }
			set { defaultTaskId = value; }
		} //DefaultTaskId
	} //end of class
} //end of namespace