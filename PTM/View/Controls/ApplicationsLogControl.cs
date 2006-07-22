using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;

[assembly: ComVisible(false)]
namespace PTM.View.Controls
{
	/// <summary>
	/// Summary description for ApplicationsLog.
	/// </summary>
	public class ApplicationsLogControl : UserControl
	{
		private ListView applicationsList;
		private ColumnHeader colName;
		private ColumnHeader colActiveTime;
		private ColumnHeader colProcessId;
		private IContainer components;

		public ApplicationsLogControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//applicationsTimer.Elapsed+=new ElapsedEventHandler(applicationsTimer_Elapsed);
			ApplicationsLog.ApplicationsLogRowChanged+=new PTMDataset.ApplicationsLogRowChangeEventHandler(ApplicationsLogTable_ApplicationsLogRowChanged);
			applicationsList.SmallImageList = IconsManager.IconsList;
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
			this.applicationsList = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colActiveTime = new System.Windows.Forms.ColumnHeader();
			this.colProcessId = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// applicationsList
			// 
			this.applicationsList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.applicationsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.applicationsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																														 this.colName,
																														 this.colActiveTime,
																														 this.colProcessId});
			this.applicationsList.FullRowSelect = true;
			this.applicationsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.applicationsList.Location = new System.Drawing.Point(8, 8);
			this.applicationsList.MultiSelect = false;
			this.applicationsList.Name = "applicationsList";
			this.applicationsList.Size = new System.Drawing.Size(368, 272);
			this.applicationsList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.applicationsList.TabIndex = 1;
			this.applicationsList.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 289;
			// 
			// colActiveTime
			// 
			this.colActiveTime.Text = "Active Time";
			this.colActiveTime.Width = 78;
			// 
			// colProcessId
			// 
			this.colProcessId.Text = "Process Id";
			this.colProcessId.Width = 0;
			// 
			// ApplicationsLog
			// 
			this.Controls.Add(this.applicationsList);
			this.Name = "ApplicationsLog";
			this.Size = new System.Drawing.Size(384, 288);
			this.ResumeLayout(false);

		}
		#endregion

		
//		public void Start()
//		{
//			IconListManager.SetImageList(this.applicationsIconsList);
//			this.applicationsTimer.Start();
//		}
//		public void Stop()
//		{
//			this.applicationsTimer.Stop();
//		}

		private int currentTaskLogId=-1;
		private void UpdateApplicationsList(PTMDataset.ApplicationsLogRow appRow)
		{
			//this.applicationsTimer.Stop();
			//PTMDataset.ApplicationsLogRow appRow = ApplicationsLogHelper.UpdateActiveProcess();
			if (appRow == null) {return;}

			if(currentTaskLogId != appRow.TaskLogId)
				this.applicationsList.Items.Clear();

			currentTaskLogId=appRow.TaskLogId;
			TimeSpan active =  new TimeSpan(0,0,appRow.ActiveTime);
			string activeTime = ViewHelper.TimeSpanToTimeString(active);
			string caption = appRow.Caption.Length != 0 ? appRow.Caption : appRow.Name;
			ListViewItem lvi = null;
			foreach (ListViewItem item in this.applicationsList.Items)
			{
				if (item.SubItems[colProcessId.Index].Text == appRow.Id.ToString(CultureInfo.InvariantCulture))
				{
					lvi = item;
					lvi.SubItems[colName.Index].Text = caption;
					lvi.SubItems[colActiveTime.Index].Text = activeTime;
					break;
				}
			}
			if (lvi == null)
			{
				lvi = new ListViewItem(new string[] {caption, activeTime, appRow.Id.ToString(CultureInfo.InvariantCulture)});
				lvi.ImageIndex = IconsManager.AddIconFromFile(appRow.ApplicationFullPath);
				this.applicationsList.Items.Add(lvi);
			}

			//this.applicationsTimer.Start();
		}

//		private void applicationsTimer_Elapsed(object sender, ElapsedEventArgs e)
//		{
//			Thread t = new Thread(new ThreadStart(GetApplicationsInfo));
//			t.Priority = ThreadPriority.BelowNormal;
//			t.Start();
		//		}

		private void ApplicationsLogTable_ApplicationsLogRowChanged(object sender, PTMDataset.ApplicationsLogRowChangeEvent e)
		{
			if(e.Action == DataRowAction.Change)
			{
				this.UpdateApplicationsList(e.Row);
			}
		}
	}
}
