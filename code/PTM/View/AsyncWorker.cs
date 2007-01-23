using System;
using System.Runtime.Remoting.Messaging;

namespace PTM.View
{
	public class AsyncWorker
	{
		public class OnWorkDoneEventArgs : EventArgs
		{
			private int workId;
			private object result;

			internal OnWorkDoneEventArgs(int workId, object result)
			{
				this.workId = workId;
				this.result = result;
			}

			public int WorkId
			{
				get { return workId; }
			}

			public object Result
			{
				get { return result; }
			}
		}

		public class OnBeforeDoWorkEventArgs : EventArgs
		{
			private int workId;

			internal OnBeforeDoWorkEventArgs(int workId)
			{
				this.workId = workId;
			}

			public int WorkId
			{
				get { return workId; }
			}
		}

		public delegate object AsyncWorkerDelegate(object parameter);

		public delegate void OnWorkDoneDelegate(OnWorkDoneEventArgs e);

		public delegate void OnBeforeDoWorkDelegate(OnBeforeDoWorkEventArgs e);

		public event OnWorkDoneDelegate OnWorkDone;
		public event OnBeforeDoWorkDelegate OnBeforeDoWork;

		public void DoWork(int workId, AsyncWorkerDelegate workDelegate, object[] parameters)
		{
			if (OnBeforeDoWork != null)
				OnBeforeDoWork(new OnBeforeDoWorkEventArgs(workId));
			workDelegate.BeginInvoke(parameters, new AsyncCallback(WorkCallBack), workId);
		}

		private void WorkCallBack(IAsyncResult asyncResult)
		{
			int workId = (int) asyncResult.AsyncState;
			AsyncResult aResult = (AsyncResult) asyncResult;
			AsyncWorkerDelegate temp = (AsyncWorkerDelegate) aResult.AsyncDelegate;
			object result = temp.EndInvoke(asyncResult);

			if (OnWorkDone != null)
				OnWorkDone(new OnWorkDoneEventArgs(workId, result));
		}
	}
}