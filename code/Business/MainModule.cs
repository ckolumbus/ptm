using System.Windows.Forms;
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
			Application.DoEvents();
			UnitOfWork.Initialize(ds, adapterManager);
			Application.DoEvents();
			ConfigurationHelper.Initialize(adapterManager.configurationDataAdapter, adapterManager.defaultTaskDataAdapter);
			Application.DoEvents();
			DefaultTasks.Initialize();
			Application.DoEvents();
			Tasks.Initialize(ds.Tasks, adapterManager.tasksDataAdapter);
			Application.DoEvents();
			Logs.Initialize();
			Application.DoEvents();
			ApplicationsLog.Initialize();
			Application.DoEvents();
			Summary.Initialize(adapterManager.applicationsSummaryDataAdapter);
			Application.DoEvents();
		}

	}
}