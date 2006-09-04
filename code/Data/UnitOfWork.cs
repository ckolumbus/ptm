namespace PTM.Data
{
	public sealed class UnitOfWork
	{
		private UnitOfWork()
		{
		}//UnitOfWork

		private static PTMDataset ptmDataset;
		private static DataAdapterManager dataAdapterManager;

		public static PTMDataset PtmDataset
		{
			get { return ptmDataset; }
		}//PtmDataset

		internal static void Initialize(PTMDataset ds,  DataAdapterManager adapterManager)
		{
			ptmDataset = ds;
			dataAdapterManager = adapterManager;
		}//Initialize

		internal static void Update()
		{
				ptmDataset.Tasks.BeginLoadData();
				//ptmDataset.TasksLog.BeginLoadData();
				//ptmDataset.ApplicationsLog.BeginLoadData();
				dataAdapterManager.tasksDataAdapter.Update(ptmDataset.Tasks);
				//dataAdapterManager.tasksLogDataAdapter.Update(ptmDataset.TasksLog);
				//dataAdapterManager.applicationsLogDataAdapter.Update(ptmDataset.ApplicationsLog);
				ptmDataset.AcceptChanges();
				ptmDataset.Tasks.EndLoadData();
				//ptmDataset.TasksLog.EndLoadData();
				//ptmDataset.ApplicationsLog.EndLoadData();
		}//Update
	}//UnitOfWork
}//End of namespace 
