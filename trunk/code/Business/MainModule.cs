using System.Collections;
using System.Windows.Forms;
using PTM.Data;

namespace PTM.Business
{
	
	
	public class MainModule
	{	

		protected MainModule()
		{
		}//MainModule

		/// <summary>
		/// Initializes the engine components, 
		/// </summary>
		public static void Initialize(PTMDataset ds, string userName)
		{
			DataAdapterManager adapterManager = new DataAdapterManager( userName );
			Application.DoEvents();
			UnitOfWork.Initialize(ds, adapterManager);
			Application.DoEvents();
			ConfigurationHelper.Initialize(adapterManager.configurationDataAdapter);
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

	}//MainModule
}//namespace
