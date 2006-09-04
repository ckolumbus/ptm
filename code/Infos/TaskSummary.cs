using System;
using System.Windows.Forms;

namespace PTM.Infos
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
		}//TaskSummary

		/// <summary>
		/// Task Id of this summary
		/// </summary>
		int taskId;
		/// <summary>
		/// Total elapsed time with this task
		/// </summary>
		double totalTime;
		/// <summary>
		/// Task description
		/// </summary>
		string description;
		/// <summary>
		/// Stores if this task is one of the Default tasks.
		/// </summary>
		bool isDefaultTask;
		/// <summary>
		/// In case of being a Defautl Task stores its defaultTask Id.
		/// </summary>
		int defaultTaskId;

		/// <summary>
		/// TaskId Accessors
		/// </summary>
		public int TaskId
		{
			get { return taskId; }
			set
			{ taskId = value; }
		}//TaskId

		/// <summary>
		/// TotalTime Accessors
		/// </summary>
		public double TotalTime
		{
			get{ return totalTime;}
			set { totalTime = value; }
		}//TotalTime

		/// <summary>
		/// Description Accessors
		/// </summary>
		public string Description
		{
			get { return description; }
			set
			{ description = value; }
		}//Description

		/// <summary>
		/// IsDefaultTask Accessors
		/// </summary>
		public bool IsDefaultTask
		{
			get { return isDefaultTask; }
			set { isDefaultTask = value; }
		}//IsDefaultTask

		/// <summary>
		/// DefaultTaskId Accessors
		/// </summary>
		public int DefaultTaskId
		{
			get { return defaultTaskId; }
			set { defaultTaskId = value; }
		}//DefaultTaskId

	}//end of class
}//end of namespace
