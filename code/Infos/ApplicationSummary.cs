using System;

namespace PTM.Infos
{
	/// <summary>
	/// Summary description for ApplicationSummary.
	/// </summary>
	public class ApplicationSummary
	{
		public ApplicationSummary()
		{
		}
		
		int taskId;
		string name;
		string applicationFullPath;
		double totalActiveTime;

		public int TaskId
		{
			get { return taskId; }
			set { taskId = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string ApplicationFullPath
		{
			get { return applicationFullPath; }
			set { applicationFullPath = value; }
		}

		public double TotalActiveTime
		{
			get { return totalActiveTime; }
			set { totalActiveTime = value; }
		}
	}
}
