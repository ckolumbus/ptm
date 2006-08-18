using PTM.Data;

namespace PTM.Business
{
	internal enum DefaultTask : int
	{
		Idle = 1,
		LunchTime = 2,
		GoToTheBath = 3,
		PhoneCall = 4,
		CheckingMail = 5,
		OutOfMyplace = 6,
		OnAMeeting = 7
	}


	public class MainModule
	{
		

		protected MainModule()
		{
		}

		public static void Initialize(PTMDataset ds, string userName)
		{
			DataAdapterManager adapterManager = new DataAdapterManager(userName);
			UnitOfWork.Initialize(ds, adapterManager);
			ConfigurationHelper.Initialize(adapterManager.configurationDataAdapter, adapterManager.defaultTaskDataAdapter);
			DefaultTasks.Initialize();
			Tasks.Initialize(ds.Tasks, adapterManager.tasksDataAdapter);
			Logs.Initialize();
			ApplicationsLog.Initialize(ds.ApplicationsLog, adapterManager.applicationsLogDataAdapter);
			Summary.Initialize(adapterManager.taskSumaryDataAdapter, adapterManager.applicationsSummaryDataAdapter);
		}
	
		

//		protected static PTMDataset.TasksDataTable GetTasksTable()
//		{
//			return UnitOfWork.PtmDataset.Tasks;
//		}
//
//		protected static PTMDataset.TasksLogDataTable GetTasksLogDataTable()
//		{
//			return UnitOfWork.PtmDataset.TasksLog;
//		}
//
//		protected static PTMDataset.ApplicationsLogDataTable GetApplicationsLogDataTable()
//		{
//			return UnitOfWork.PtmDataset.ApplicationsLog;
//		}





	}
}