namespace PTM.Data
{
	public sealed class UnitOfWork
	{
		private UnitOfWork()
		{
		} //UnitOfWork

		private static PTMDataset ptmDataset;
		private static DataAdapterManager dataAdapterManager;

		public static PTMDataset PtmDataset
		{
			get { return ptmDataset; }
		} //PtmDataset

		internal static void Initialize(PTMDataset ds, DataAdapterManager adapterManager)
		{
			ptmDataset = ds;
			dataAdapterManager = adapterManager;
		} //Initialize

		public static void Update()
		{
			ptmDataset.Tasks.BeginLoadData();
			dataAdapterManager.tasksDataAdapter.Update(ptmDataset.Tasks);
			ptmDataset.AcceptChanges();
			ptmDataset.Tasks.EndLoadData();
		} //Update
	} //UnitOfWork
} //End of namespace 