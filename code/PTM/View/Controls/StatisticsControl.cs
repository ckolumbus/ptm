using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using PTM.Addin;
using PTM.Framework;
using PTM.Data;
using PTM.Framework.Infos;
using PTM.View.Controls.TreeListViewComponents;
using PTM.View.Forms;

namespace PTM.View.Controls
{
	/// <summary>
	/// Summary description for Statistics.
	/// </summary>
	internal class StatisticsControl : TabPageAddin
	{
		private TreeListView applicationsList;
		private ColumnHeader colName;
		private ColumnHeader colActiveTime;
		private GroupBox groupBox3;
		private GroupBox groupBox4;
		private ColumnHeader colAppPercent;
		private Label label8;
		private Label AppsActiveTimeValue;
		private Button browseButton;
		private ComboBox parentTaskComboBox;
		private Label label2;
		private AsyncWorker worker;


		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private System.Windows.Forms.DateTimePicker toDateTimePicker;
		private System.Windows.Forms.RadioButton toRadioButton;
		private System.Windows.Forms.RadioButton fromRadioButton;
		private System.Windows.Forms.DateTimePicker fromDateTimePicker;
		private System.Windows.Forms.Button searchButton;

		private PTMDataset.TasksDataTable parentTasksTable = new PTMDataset.TasksDataTable();

		internal StatisticsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			worker = new AsyncWorker();
			worker.OnBeforeDoWork+=new PTM.View.AsyncWorker.OnBeforeDoWorkDelegate(worker_OnBeforeDoWork);
			worker.OnWorkDone+=new PTM.View.AsyncWorker.OnWorkDoneDelegate(worker_OnWorkDone);


			PTMDataset.TasksRow parentTaskRow;
			parentTaskRow = parentTasksTable.NewTasksRow();
			parentTaskRow.ItemArray = Tasks.RootTasksRow.ItemArray;
			parentTasksTable.AddTasksRow(parentTaskRow);
			this.parentTaskComboBox.DataSource = parentTasksTable;
			this.parentTaskComboBox.DisplayMember = parentTasksTable.DescriptionColumn.ColumnName;
			this.parentTaskComboBox.ValueMember = parentTasksTable.IdColumn.ColumnName;

			this.fromDateTimePicker.Value = DateTime.Today;
			this.toDateTimePicker.Value = DateTime.Today;

			if (parentTaskComboBox.Items.Count > 0)
				parentTaskComboBox.SelectedIndex = 0;
			this.fromDateTimePicker.ValueChanged += new EventHandler(this.dateTimePicker_ValueChanged);
			this.toDateTimePicker.ValueChanged += new EventHandler(this.dateTimePicker_ValueChanged);

			this.Status = String.Empty;
			//this.parentTaskComboBox.SelectedIndexChanged += new EventHandler(parentTaskComboBox_SelectedIndexChanged);
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
			this.applicationsList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colActiveTime = new System.Windows.Forms.ColumnHeader();
			this.colAppPercent = new System.Windows.Forms.ColumnHeader();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.AppsActiveTimeValue = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.browseButton = new System.Windows.Forms.Button();
			this.parentTaskComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.toRadioButton = new System.Windows.Forms.RadioButton();
			this.fromRadioButton = new System.Windows.Forms.RadioButton();
			this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.searchButton = new System.Windows.Forms.Button();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// applicationsList
			// 
			this.applicationsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.applicationsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																														 this.colName,
																														 this.colActiveTime,
																														 this.colAppPercent});
			this.applicationsList.Location = new System.Drawing.Point(8, 16);
			this.applicationsList.MultiSelect = false;
			this.applicationsList.Name = "applicationsList";
			this.applicationsList.Size = new System.Drawing.Size(376, 136);
			this.applicationsList.TabIndex = 0;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 233;
			// 
			// colActiveTime
			// 
			this.colActiveTime.Text = "Active Time";
			this.colActiveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colActiveTime.Width = 78;
			// 
			// colAppPercent
			// 
			this.colAppPercent.Text = "Percent";
			this.colAppPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.applicationsList);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.ForeColor = System.Drawing.Color.Blue;
			this.groupBox3.Location = new System.Drawing.Point(8, 64);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(392, 160);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Applications";
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.AppsActiveTimeValue);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.ForeColor = System.Drawing.Color.Blue;
			this.groupBox4.Location = new System.Drawing.Point(8, 232);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(392, 48);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Totals";
			// 
			// AppsActiveTimeValue
			// 
			this.AppsActiveTimeValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.AppsActiveTimeValue.ForeColor = System.Drawing.Color.Black;
			this.AppsActiveTimeValue.Location = new System.Drawing.Point(112, 16);
			this.AppsActiveTimeValue.Name = "AppsActiveTimeValue";
			this.AppsActiveTimeValue.Size = new System.Drawing.Size(80, 23);
			this.AppsActiveTimeValue.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label8.ForeColor = System.Drawing.Color.Black;
			this.label8.Location = new System.Drawing.Point(8, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104, 23);
			this.label8.TabIndex = 0;
			this.label8.Text = "Apps. Active Time:";
			// 
			// browseButton
			// 
			this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.browseButton.Location = new System.Drawing.Point(320, 4);
			this.browseButton.Name = "browseButton";
			this.browseButton.TabIndex = 2;
			this.browseButton.Text = "Browse...";
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// parentTaskComboBox
			// 
			this.parentTaskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.parentTaskComboBox.Location = new System.Drawing.Point(80, 5);
			this.parentTaskComboBox.MaxLength = 50;
			this.parentTaskComboBox.Name = "parentTaskComboBox";
			this.parentTaskComboBox.Size = new System.Drawing.Size(232, 21);
			this.parentTaskComboBox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "Detail Level:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toDateTimePicker
			// 
			this.toDateTimePicker.CustomFormat = "";
			this.toDateTimePicker.Enabled = false;
			this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.toDateTimePicker.Location = new System.Drawing.Point(224, 32);
			this.toDateTimePicker.Name = "toDateTimePicker";
			this.toDateTimePicker.Size = new System.Drawing.Size(88, 20);
			this.toDateTimePicker.TabIndex = 21;
			this.toDateTimePicker.Value = new System.DateTime(2006, 10, 2, 0, 0, 0, 0);
			// 
			// toRadioButton
			// 
			this.toRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.toRadioButton.Location = new System.Drawing.Point(184, 32);
			this.toRadioButton.Name = "toRadioButton";
			this.toRadioButton.Size = new System.Drawing.Size(40, 24);
			this.toRadioButton.TabIndex = 20;
			this.toRadioButton.Text = "To:";
			this.toRadioButton.CheckedChanged += new System.EventHandler(this.toRadioButton_CheckedChanged);
			// 
			// fromRadioButton
			// 
			this.fromRadioButton.Checked = true;
			this.fromRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fromRadioButton.Location = new System.Drawing.Point(32, 32);
			this.fromRadioButton.Name = "fromRadioButton";
			this.fromRadioButton.Size = new System.Drawing.Size(48, 24);
			this.fromRadioButton.TabIndex = 19;
			this.fromRadioButton.TabStop = true;
			this.fromRadioButton.Text = "Date:";
			this.fromRadioButton.CheckedChanged += new System.EventHandler(this.fromRadioButton_CheckedChanged);
			// 
			// fromDateTimePicker
			// 
			this.fromDateTimePicker.CustomFormat = "";
			this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.fromDateTimePicker.Location = new System.Drawing.Point(80, 32);
			this.fromDateTimePicker.Name = "fromDateTimePicker";
			this.fromDateTimePicker.Size = new System.Drawing.Size(88, 20);
			this.fromDateTimePicker.TabIndex = 18;
			this.fromDateTimePicker.Value = new System.DateTime(2006, 10, 2, 0, 0, 0, 0);
			// 
			// searchButton
			// 
			this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchButton.Location = new System.Drawing.Point(320, 32);
			this.searchButton.Name = "searchButton";
			this.searchButton.TabIndex = 22;
			this.searchButton.Text = "Search";
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// StatisticsControl
			// 
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.browseButton);
			this.Controls.Add(this.parentTaskComboBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.toRadioButton);
			this.Controls.Add(this.fromRadioButton);
			this.Controls.Add(this.fromDateTimePicker);
			this.Controls.Add(this.toDateTimePicker);
			this.Name = "StatisticsControl";
			this.Size = new System.Drawing.Size(408, 288);
			this.Load += new System.EventHandler(this.Statistics_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		//private SummaryDataset.TasksSummaryDataTable tasksSummaryDataset;
		//private SummaryDataset.ApplicationsSummaryDataTable appsSummaryDataset;

		private void Statistics_Load(object sender, EventArgs e)
		{
			//this.dateTimePicker.Value = DateTime.Today;
			//this.dateTimePicker.ValueChanged+=new EventHandler(dateTimePicker_ValueChanged);
			//this.taskComboBox.SelectedIndexChanged+=new EventHandler(taskComboBox_SelectedIndexChanged);
			this.applicationsList.SmallImageList = IconsManager.IconsList;
			this.parentTaskComboBox.Focus();
		}

		private void dateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			if(this.fromRadioButton.Checked)
			{
				this.toDateTimePicker.Value = this.fromDateTimePicker.Value;
			}
		}

		private void GetTaskDetails(ArrayList appsSummaryList)
		{
			try
			{
				this.applicationsList.BeginUpdate();
				int appActiveTime = 0;
				foreach (ApplicationSummary applicationsSummaryRow in appsSummaryList)
				{
					appActiveTime += (int) applicationsSummaryRow.TotalActiveTime;
				}
				foreach (ApplicationSummary applicationsSummaryRow in appsSummaryList)
				{
					TimeSpan active = new TimeSpan(0, 0, (int) applicationsSummaryRow.TotalActiveTime);
					string activeTime = ViewHelper.TimeSpanToTimeString(active);
					double percent = 0;
					if (appActiveTime > 0)
						percent = applicationsSummaryRow.TotalActiveTime/appActiveTime;
					TreeListViewItem lvi =
						new TreeListViewItem(applicationsSummaryRow.Name,
						                     new string[]
						                     	{
						                     		activeTime, percent.ToString("0.0%", CultureInfo.InvariantCulture),
						                     		applicationsSummaryRow.TaskId.ToString(CultureInfo.InvariantCulture)
						                     	});
					lvi.ImageIndex = IconsManager.GetIconFromFile(applicationsSummaryRow.ApplicationFullPath);
					this.applicationsList.Items.Add(lvi);
				}
				AppsActiveTimeValue.Text = ViewHelper.TimeSpanToTimeString(new TimeSpan(0, 0, appActiveTime));
			}
			finally
			{
				this.applicationsList.EndUpdate();
				SetReadyState();
			}
		}

		private void SetReadyState()
		{
			this.Status = "";
			this.Cursor = Cursors.Default;
			foreach (Control control in this.Controls)
			{
				control.Cursor = Cursors.Default;						
			}
			this.parentTaskComboBox.Enabled = true;
			if(this.toRadioButton.Checked)
            toDateTimePicker.Enabled = true;

			fromDateTimePicker.Enabled = true;
			this.browseButton.Enabled = true;
			this.searchButton.Enabled = true;
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			TasksHierarchyForm tgForm = new TasksHierarchyForm();
			tgForm.ShowDialog(this);
			if (tgForm.SelectedTaskRow == null)
				return;

			if (parentTasksTable.FindById(tgForm.SelectedTaskRow.Id) == null)
			{
				PTMDataset.TasksRow parentRow = this.parentTasksTable.NewTasksRow();
				parentRow.ItemArray = tgForm.SelectedTaskRow.ItemArray;
				parentRow.Description = ViewHelper.FixTaskPath(Tasks.GetFullPath(parentRow.Id), this.parentTaskComboBox.MaxLength);
				this.parentTasksTable.Rows.InsertAt(parentRow, 0);
			}
			this.parentTaskComboBox.SelectedValue = tgForm.SelectedTaskRow.Id;
		}

		private void toRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			this.toDateTimePicker.Enabled = true;
			this.fromRadioButton.Text = "From:";
		}

		private void fromRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			this.toDateTimePicker.Enabled = false;
			this.toDateTimePicker.Value = this.fromDateTimePicker.Value;
			this.fromRadioButton.Text = "Date:";	
		}

		private void searchButton_Click(object sender, System.EventArgs e)
		{
			this.Status = "Retrieving data...";
			worker.DoWork((int)StatisticsControlWorks.GetTaskStatistics, new AsyncWorker.AsyncWorkerDelegate(GetTaskStatistics), new object[]{null});
		}


		#region AsyncWork
		private enum StatisticsControlWorks : int
		{
			GetTaskStatistics
		}

		public object GetTaskStatistics(object p)
		{
			DateTime fromDate;
			DateTime toDate;
			fromDate = fromDateTimePicker.Value.Date;
			if(this.toRadioButton.Checked)
			{
				toDate = toDateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);
			}
			else
			{
				toDate = fromDateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);						
			}
			
			ArrayList appsSummaryList = ApplicationSummaries.GetApplicationsSummary(
				Tasks.FindById((int) this.parentTaskComboBox.SelectedValue),
				fromDate, toDate);

			return appsSummaryList;
		}
		
		private void worker_OnBeforeDoWork(PTM.View.AsyncWorker.OnBeforeDoWorkEventArgs e)
		{
			switch(e.WorkId)
			{
				case (int)StatisticsControlWorks.GetTaskStatistics:
					SetWaitState();
					break;
			}
		}

		private void SetWaitState()
		{
			this.parentTaskComboBox.Enabled = false;
			toDateTimePicker.Enabled = false;
			fromDateTimePicker.Enabled = false;
			this.browseButton.Enabled = false;
			this.searchButton.Enabled = false;
			this.applicationsList.Items.Clear();
			this.AppsActiveTimeValue.Text = String.Empty;
         this.Refresh();
			this.Cursor = Cursors.WaitCursor;
			foreach (Control control in this.Controls)
			{
				control.Cursor = Cursors.WaitCursor;						
			}
		}

		private void worker_OnWorkDone(PTM.View.AsyncWorker.OnWorkDoneEventArgs e)
		{
			switch(e.WorkId)
			{
				case (int)StatisticsControlWorks.GetTaskStatistics:

					GetTaskDetailsDelegate del = new GetTaskDetailsDelegate( GetTaskDetails);

					this.Invoke(del, new object[]{e.Result});
					break;
			}
		}

		private delegate void GetTaskDetailsDelegate(ArrayList logs);

		#endregion
	}
}