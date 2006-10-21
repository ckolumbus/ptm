using System;
using System.Collections;
using System.Diagnostics;
using PTM.Business.Helpers;
using PTM.Infos;

namespace PTM.Business
{
	/// <summary>
	/// Summary description for MergedLogs.
	/// </summary>
	public sealed class MergedLogs : CollectionBase
	{
		public MergedLogs()
		{
		}
		
		public MergedLog this[int index]
		{
			get{ return (MergedLog) this.List[index];}
			set{ this.List[index] = value;}
		}

		public void Add(MergedLog mergedLog)
		{
			this.List.Add(mergedLog);
		}
		
		public static MergedLogs GetMergedLogsByDay(DateTime day)
		{
			DateTime date = day.Date;
			ArrayList logs = Logs.GetLogsByDay(date);
			MergedLogs mergedList = new MergedLogs();
			
			//ArrayList mergedList = new ArrayList();
			//ArrayList needsDelete = new ArrayList();
			//ArrayList needsUpdate = new ArrayList();
			Configuration config = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			int timeTolerance = (int)config.Value*60;
			int m = -1;
			for(int i = 0; i<logs.Count;i++)
			{
				Log log = (Log) logs[i];
				if(m>=0)
				{
					MergedLog merged = mergedList[m];
					DateTime mergedEndTime = merged.MergeLog.InsertTime.AddSeconds(merged.MergeLog.Duration);
					Debug.Assert(merged.MergeLog.InsertTime<=log.InsertTime, "The list must be always ordered.");
					if(log.TaskId==merged.MergeLog.TaskId && mergedEndTime.Subtract(log.InsertTime).TotalSeconds<timeTolerance)
					{
						merged.MergeLog.Duration += log.Duration;
						merged.DeletedLogs.Add(log);
						//needsDelete.Add(log);
						//needsUpdate.Add(merged);
						continue;
					}
				}
				mergedList.Add(new MergedLog(log));;
				m++;
			}
			return mergedList;
		}
	}
}
