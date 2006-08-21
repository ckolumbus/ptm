using System;
using System.Windows.Forms;

namespace PTM.Infos
{
	/// <summary>
	/// Descripción breve de TaskSummary.
	/// </summary>
	public class TaskSummary
	{
		public TaskSummary()
		{
		}

		int taskId;
		double totalTime;
		string descripcion;
		bool isDefaultTask;
		int defaultTaskId;

		public int TaskId
		{
			get { return taskId; }
			set
			{ taskId = value; }
		}

		public double TotalTime
		{
			get
			{
				return totalTime;
			}
			set { totalTime = value; }
		}

		public string Description
		{
			get { return descripcion; }
			set
			{ descripcion = value; }
		}

		public bool IsDefaultTask
		{
			get { return isDefaultTask; }
			set
			{ isDefaultTask = value; }
		}

		public int DefaultTaskId
		{
			get { return defaultTaskId; }
			set
			{ defaultTaskId = value; }
		}


	}
}
