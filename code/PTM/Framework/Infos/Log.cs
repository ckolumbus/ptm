using System;
using System.Collections;

namespace PTM.Framework.Infos
{
	/// <summary>
	/// Base class for Logged Items.
	/// </summary>
	public class Log
	{
		public Log()
		{
		} //Log

		/// <summary>
		/// Log Id
		/// </summary>
		private int id;

		/// <summary>
		/// Task Id.
		/// </summary>
		private int taskId;

		/// <summary>
		/// Duration of this log.
		/// </summary>
		private int duration;

		/// <summary>
		/// Insertion TimeStamp
		/// </summary>
		private DateTime insertTime;


		private ArrayList applicationsLog = new ArrayList();

		/// <summary>
		/// Applications log list for this log
		/// </summary>
		public ArrayList ApplicationsLog
		{
			get { return applicationsLog; }
			set { applicationsLog = value; }
		}

		/// <summary>
		/// Id Accessors
		/// </summary>
		public int Id
		{
			get { return id; }
			set { id = value; }
		} //Id

		/// <summary>
		/// InsertTime Accessors
		/// </summary>
		public DateTime InsertTime
		{
			get { return insertTime; }
			set { insertTime = value; }
		} //InsertTime

		/// <summary>
		/// Duration Accessors
		/// </summary>
		public int Duration
		{
			get { return duration; }
			set { duration = value; }
		} //Duration

		/// <summary>
		/// TaskId Accessors
		/// </summary>
		public int TaskId
		{
			get { return taskId; }
			set { taskId = value; }
		} //TaskId
	} // End of class
} //End of namespace