using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
		private System.ComponentModel.Container components = null;

		public AddinTabPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
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
				if(value != status)
				{
					status = value;
					OnStatusChanged();
				}
			}
		}

		public class StatusChangedEventAtgs :EventArgs
		{
			private string status;
			public StatusChangedEventAtgs(string status)
			{
				this.status = status;
			}
			public string Status
			{
				get { return status; }
				set { status = value; }
			}
		}

		
		public delegate void StatusChangedDelegate(StatusChangedEventAtgs e);
		public event StatusChangedDelegate StatusChanged;
		
		protected void OnStatusChanged()
		{
			if(this.StatusChanged!=null)
				StatusChanged(new StatusChangedEventAtgs(status));
			Application.DoEvents();
		}
		

		public virtual void OnTabPageSelected()
		{			
		}

	}
}
