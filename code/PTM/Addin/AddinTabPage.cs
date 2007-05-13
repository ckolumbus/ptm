using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PTM.Addin
{
	/// <summary>
	/// Summary description for AddinTabPage.
	/// </summary>
	public class AddinTabPage : UserControl
	{
		/// <summary> AddinTabPage
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public AddinTabPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// AddinTabPage
			// 
			this.Name = "AddinTabPage";
			this.Size = new System.Drawing.Size(296, 280);
		}

		#endregion

		private string status = String.Empty;

		public string Status
		{
			get { return status; }
			set
			{
				if (value != status)
				{
					status = value;
					OnStatusChanged();
				}
			}
		}

		public class StatusChangedEventArgs : EventArgs
		{
			private string status;

			public StatusChangedEventArgs(string status)
			{
				this.status = status;
			}

			public string Status
			{
				get { return status; }
				set { status = value; }
			}
		}


		public delegate void StatusChangedDelegate(StatusChangedEventArgs e);

		public event StatusChangedDelegate StatusChanged;

		protected void OnStatusChanged()
		{
			if (this.StatusChanged != null)
			{
                StatusChangedDelegate del = new StatusChangedDelegate(StatusChanged);
                this.Invoke(del, new object[] { new StatusChangedEventArgs(status) });
			}
            //    StatusChanged(new StatusChangedEventArgs(status));
            //Application.DoEvents();
		}


		public virtual void OnTabPageSelected()
		{
		}
	}
}