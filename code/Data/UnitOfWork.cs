using System.Data;

namespace PTM.Data
{
	public sealed class UnitOfWork
	{
		private UnitOfWork()
		{
		}

		private static PTMDataset ptmDataset;
		private static DataAdapterManager dataAdapterManager;

		public static PTMDataset PtmDataset
		{
			get { return ptmDataset; }
		}

		internal static void Initialize(PTMDataset ds,  DataAdapterManager adapterManager)
		{
			ptmDataset = ds;
			dataAdapterManager = adapterManager;
		}

		internal static void Update()
		{
//			try
//			{
				ptmDataset.Tasks.BeginLoadData();
				ptmDataset.TasksLog.BeginLoadData();
				ptmDataset.ApplicationsLog.BeginLoadData();
				dataAdapterManager.tasksDataAdapter.Update(ptmDataset.Tasks);
				dataAdapterManager.tasksLogDataAdapter.Update(ptmDataset.TasksLog);
				dataAdapterManager.applicationsLogDataAdapter.Update(ptmDataset.ApplicationsLog);
				ptmDataset.AcceptChanges();
				ptmDataset.Tasks.EndLoadData();
				ptmDataset.TasksLog.EndLoadData();
				ptmDataset.ApplicationsLog.EndLoadData();
//			}
//			catch (DBConcurrencyException ex)
//			{
//				ex = ex;
//			}
		}
	}
}
