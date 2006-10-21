using System;
using System.Collections;

namespace PTM.Infos
{
	/// <summary>
	/// Summary description for MergedLog.
	/// </summary>
	public class MergedLog
	{
		public MergedLog()
		{
		}
		
		private Log mergeLog;
		private ArrayList deletedLogs = new ArrayList();

		public MergedLog(Log log)
		{
			this.mergeLog = log;
		}

		public Log MergeLog
		{
			get { return mergeLog; }
			set { mergeLog = value; }
		}

		public ArrayList DeletedLogs
		{
			get { return deletedLogs; }
			set { deletedLogs = value; }
		}
	}
}
