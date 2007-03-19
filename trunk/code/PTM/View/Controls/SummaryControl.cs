using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Timers;
using System.Windows.Forms;
using PTM.Addin;
using PTM.Framework;
using PTM.Framework.Infos;
using PTM.View.Controls.TreeListViewComponents;
using PTM.View.Forms;

namespace PTM.View.Controls
{
	/// <summary>
	/// Summary description for Summary.
	/// </summary>
	internal class SummaryControl : AddinTabPage
	{
		private TreeListView taskList;
		private GroupBox groupBox1;
		private GroupBox groupBox3;
		private ColumnHeader PercentHeader;
		private IndicatorControl indicator1;
		private ColumnHeader TaskHeader;
		private Label label2;
		private ComboBox parentTaskComboBox;
		private IContainer components;
		private Button browseButton;
		private GroupBox groupBox2;
		private IndicatorControl indicator2;
		private Panel panel1;
		private ToolBar toolBar;
		private ToolBarButton toolBarButton1;
		private ToolBarButton toolBarButton2;
		private ImageList toolBarImages;

		private ArrayList parentTasksList = new ArrayList();
		private ColumnHeader InactiveTimeHeader;
		private ColumnHeader ActiveTimeHeader;
		private DateTimePicker fromDateTimePicker;
		private DateTimePicker toDateTimePicker;
		private RadioButton fromRadioButton;
		private RadioButton toRadioButton;
		private ToolTip toolTip;
		private Task parentTask;
		private GroupBox groupBox4;
		private IndicatorControl indicator3;
		private AsyncWorker worker;

		internal SummaryControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			worker = new AsyncWorker();
			worker.OnBeforeDoWork += new AsyncWorker.OnBeforeDoWorkDelegate(worker_OnBeforeDoWork);
			worker.OnWorkDone += new AsyncWorker.OnWorkDoneDelegate(worker_OnWorkDone);

			this.taskList.SmallImageList = IconsManager.IconsList;
			parentTasksList.Add(Tasks.RootTasksRow);
			this.parentTaskComboBox.DisplayMember = "Description";
			this.parentTaskComboBox.ValueMember = "Id";
			this.parentTaskComboBox.DataSource = parentTasksList;

			this.fromDateTimePicker.Value = DateTime.Today;
			this.toDateTimePicker.Value = DateTime.Today;

			if (parentTasksList.Count > 0)
			{
				parentTask = (Task) parentTasksList[0];
				this.parentTaskComboBox.SelectedValue = parentTask.Id;
			}
			this.fromDateTimePicker.ValueChanged += new EventHandler(this.dateTimePicker_ValueChanged);
			this.toDateTimePicker.ValueChanged += new EventHandler(this.dateTimePicker_ValueChanged);
			this.parentTaskComboBox.SelectedIndexChanged += new EventHandler(parentTaskComboBox_SelectedIndexChanged);

			Logs.TasksLogDurationCountElapsed += new ElapsedEventHandler(TaskLogTimer_Elapsed);
			this.taskList.DoubleClick += new EventHandler(taskList_DoubleClick);

			this.Status = String.Empty;
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			Logs.TasksLogDurationCountElapsed -= new ElapsedEventHandler(TaskLogTimer_Elapsed);
			base.OnHandleDestroyed(e);
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SummaryControl));
			this.taskList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
			this.TaskHeader = new System.Windows.Forms.ColumnHeader();
			this.ActiveTimeHeader = new System.Windows.Forms.ColumnHeader();
			this.InactiveTimeHeader = new System.Windows.Forms.ColumnHeader();
			this.PercentHeader = new System.Windows.Forms.ColumnHeader();
			this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.indicator1 = new PTM.View.Controls.IndicatorControl();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.parentTaskComboBox = new System.Windows.Forms.ComboBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.indicator2 = new PTM.View.Controls.IndicatorControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarImages = new System.Windows.Forms.ImageList(this.components);
			this.fromRadioButton = new System.Windows.Forms.RadioButton();
			this.toRadioButton = new System.Windows.Forms.RadioButton();
			this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.indicator3 = new PTM.View.Controls.IndicatorControl();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// taskList
			// 
			this.taskList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.taskList.AllowColumnReorder = true;
			this.taskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.taskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.TaskHeader,
																					   this.ActiveTimeHeader,
																					   this.InactiveTimeHeader,
																					   this.PercentHeader});
			this.taskList.HideSelection = false;
			this.taskList.Location = new System.Drawing.Point(8, 16);
			this.taskList.MultiSelect = false;
			this.taskList.Name = "taskList";
			this.taskList.Size = new System.Drawing.Size(376, 184);
			this.taskList.Sorting = System.Windows.Forms.SortOrder.None;
			this.taskList.TabIndex = 0;
			// 
			// TaskHeader
			// 
			this.TaskHeader.Text = "Description";
			this.TaskHeader.Width = 200;
			// 
			// ActiveTimeHeader
			// 
			this.ActiveTimeHeader.Text = "Active Time";
			this.ActiveTimeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ActiveTimeHeader.Width = 70;
			// 
			// InactiveTimeHeader
			// 
			this.InactiveTimeHeader.Text = "Inactive Time";
			this.InactiveTimeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.InactiveTimeHeader.Width = 70;
			// 
			// PercentHeader
			// 
			this.PercentHeader.Text = "Percent";
			this.PercentHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PercentHeader.Width = 50;
			// 
			// fromDateTimePicker
			// 
			this.fromDateTimePicker.CustomFormat = "";
			this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.fromDateTimePicker.Location = new System.Drawing.Point(80, 32);
			this.fromDateTimePicker.Name = "fromDateTimePicker";
			this.fromDateTimePicker.Size = new System.Drawing.Size(88, 20);
			this.fromDateTimePicker.TabIndex = 0;
			this.fromDateTimePicker.Value = new System.DateTime(2006, 10, 2, 0, 0, 0, 0);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.indicator1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.ForeColor = System.Drawing.Color.Blue;
			this.groupBox1.Location = new System.Drawing.Point(8, 64);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(72, 80);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Total Time";
			// 
			// indicator1
			// 
			this.indicator1.BackColor = System.Drawing.Color.Black;
			this.indicator1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.indicator1.ForeColor = System.Drawing.Color.Lime;
			this.indicator1.Location = new System.Drawing.Point(3, 16);
			this.indicator1.Name = "indicator1";
			this.indicator1.Size = new System.Drawing.Size(66, 61);
			this.indicator1.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.taskList);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.ForeColor = System.Drawing.Color.Blue;
			this.groupBox3.Location = new System.Drawing.Point(8, 144);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(392, 208);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Tasks";
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
			// parentTaskComboBox
			// 
			this.parentTaskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.parentTaskComboBox.Location = new System.Drawing.Point(80, 5);
			this.parentTaskComboBox.MaxLength = 50;
			this.parentTaskComboBox.Name = "parentTaskComboBox";
			this.parentTaskComboBox.Size = new System.Drawing.Size(232, 21);
			this.parentTaskComboBox.TabIndex = 1;
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
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.indicator2);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.ForeColor = System.Drawing.Color.Blue;
			this.groupBox2.Location = new System.Drawing.Point(88, 64);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(72, 80);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Active %";
			// 
			// indicator2
			// 
			this.indicator2.BackColor = System.Drawing.Color.Black;
			this.indicator2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.indicator2.ForeColor = System.Drawing.Color.Lime;
			this.indicator2.Location = new System.Drawing.Point(3, 16);
			this.indicator2.Name = "indicator2";
			this.indicator2.Size = new System.Drawing.Size(66, 61);
			this.indicator2.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.toolBar);
			this.panel1.Location = new System.Drawing.Point(352, 120);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(48, 24);
			this.panel1.TabIndex = 14;
			// 
			// toolBar
			// 
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.toolBarButton1,
																					   this.toolBarButton2});
			this.toolBar.Divider = false;
			this.toolBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.toolBarImages;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(48, 26);
			this.toolBar.TabIndex = 0;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex = 0;
			this.toolBarButton1.ToolTipText = "Down level";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.ImageIndex = 1;
			this.toolBarButton2.ToolTipText = "Up level";
			// 
			// toolBarImages
			// 
			this.toolBarImages.ImageSize = new System.Drawing.Size(16, 16);
			this.toolBarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImages.ImageStream")));
			this.toolBarImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// fromRadioButton
			// 
			this.fromRadioButton.Checked = true;
			this.fromRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fromRadioButton.Location = new System.Drawing.Point(32, 32);
			this.fromRadioButton.Name = "fromRadioButton";
			this.fromRadioButton.Size = new System.Drawing.Size(48, 24);
			this.fromRadioButton.TabIndex = 15;
			this.fromRadioButton.TabStop = true;
			this.fromRadioButton.Text = "Date:";
			this.fromRadioButton.CheckedChanged += new System.EventHandler(this.fromRadioButton_CheckedChanged);
			// 
			// toRadioButton
			// 
			this.toRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.toRadioButton.Location = new System.Drawing.Point(184, 32);
			this.toRadioButton.Name = "toRadioButton";
			this.toRadioButton.Size = new System.Drawing.Size(40, 24);
			this.toRadioButton.TabIndex = 16;
			this.toRadioButton.Text = "To:";
			this.toRadioButton.CheckedChanged += new System.EventHandler(this.toRadioButton_CheckedChanged);
			// 
			// toDateTimePicker
			// 
			this.toDateTimePicker.CustomFormat = "";
			this.toDateTimePicker.Enabled = false;
			this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.toDateTimePicker.Location = new System.Drawing.Point(224, 32);
			this.toDateTimePicker.Name = "toDateTimePicker";
			this.toDateTimePicker.Size = new System.Drawing.Size(88, 20);
			this.toDateTimePicker.TabIndex = 17;
			this.toDateTimePicker.Value = new System.DateTime(2006, 10, 2, 0, 0, 0, 0);
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 0;
			this.toolTip.ShowAlways = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.indicator3);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.ForeColor = System.Drawing.Color.Blue;
			this.groupBox4.Location = new System.Drawing.Point(168, 64);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(72, 80);
			this.groupBox4.TabIndex = 18;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Avg. Time";
			this.groupBox4.Visible = false;
			// 
			// indicator3
			// 
			this.indicator3.BackColor = System.Drawing.Color.Black;
			this.indicator3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.indicator3.ForeColor = System.Drawing.Color.Lime;
			this.indicator3.Location = new System.Drawing.Point(3, 16);
			this.indicator3.Name = "indicator3";
			this.indicator3.Size = new System.Drawing.Size(66, 61);
			this.indicator3.TabIndex = 0;
			// 
			// SummaryControl
			// 
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.browseButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.fromDateTimePicker);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.toDateTimePicker);
			this.Controls.Add(this.toRadioButton);
			this.Controls.Add(this.fromRadioButton);
			this.Controls.Add(this.parentTaskComboBox);
			this.Name = "SummaryControl";
			this.Size = new System.Drawing.Size(408, 360);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private double totalTime = 0;
		private double totalActiveTime = 0;
		private int workedDays = 0;

		public override void OnTabPageSelected()
		{
			base.OnTabPageSelected();
			worker.DoWork((int) StatisticsControlWorks.GetTaskSummary, new AsyncWorker.AsyncWorkerDelegate(GetTasksSummary),
			              new object[] {null});
		}

		private void UpdateTasksSummary(TaskSummaryResult taskSummaryResult)
		{
			try
			{
				totalTime = 0;
				workedDays = taskSummaryResult.WorkedDays;
				this.taskList.BeginUpdate();
				this.taskList.Items.Clear();
				foreach (TaskSummary summary in taskSummaryResult.SummaryList)
				{
					totalTime += summary.TotalActiveTime;
					totalTime += summary.TotalInactiveTime;
					if (summary.IsActive)
						totalActiveTime += summary.TotalActiveTime;

					TimeSpan activeTimeSpan = new TimeSpan(0, 0, Convert.ToInt32(summary.TotalActiveTime));
					TimeSpan inactiveTimeSpan = new TimeSpan(0, 0, Convert.ToInt32(summary.TotalInactiveTime));
					TreeListViewItem lvi =
						new TreeListViewItem(summary.Description,
						                     new string[]
						                     	{
						                     		ViewHelper.TimeSpanToTimeString(activeTimeSpan),
						                     		ViewHelper.TimeSpanToTimeString(inactiveTimeSpan),
						                     		0.ToString("0.0%", CultureInfo.InvariantCulture),
						                     		summary.TaskId.ToString(CultureInfo.InvariantCulture)
						                     	});
					lvi.ImageIndex = 0;

					lvi.ImageIndex = summary.IconId;

					lvi.Tag = summary;
					this.taskList.Items.Add(lvi);
				}
				CalculateTasksPercents();
				SetIndicatorsValues();
			}
			catch
			{
				throw;
			}
			finally
			{
				this.taskList.EndUpdate();
				SetReadyState();
			}
		}

		private void CalculateTasksPercents()
		{
			foreach (ListViewItem item in this.taskList.Items)
			{
				double percent = 0;
				//TimeSpan t = TimeSpan.Parse(item.SubItems[this.DurationTaskHeader.Index].Text);
				TaskSummary sum = (TaskSummary) item.Tag;
				if (totalTime > 0)
					percent = (sum.TotalActiveTime + sum.TotalInactiveTime)/totalTime;
				item.SubItems[PercentHeader.Index].Text = percent.ToString("0.0%", CultureInfo.InvariantCulture);
			}
		}

		private void SetIndicatorsValues()
		{
			indicator1.Value = Convert.ToInt32(Math.Min(30600, totalTime));
			indicator1.TextValue =
				new TimeSpan(0, 0, Convert.ToInt32(totalTime, CultureInfo.InvariantCulture)).TotalHours.ToString("0.00",
				                                                                                                 CultureInfo.
				                                                                                                 	InvariantCulture) +
				" hrs.";

			if (totalTime > 0)
			{
				int percentActiveTime = Convert.ToInt32(this.totalActiveTime*100/totalTime);
				indicator2.Value = percentActiveTime;
				indicator2.TextValue = percentActiveTime + "%";
				string activeTime = new TimeSpan(0, 0, Convert.ToInt32(totalActiveTime, CultureInfo.InvariantCulture)).TotalHours.
				                    	ToString("0.00",
				                    	         CultureInfo.
				                    	         	InvariantCulture) +
				                    " hrs.";
				toolTip.SetToolTip(this.indicator2, activeTime);
				toolTip.SetToolTip(this.groupBox2, activeTime);
			}

			if(workedDays>0)
			{
				indicator3.Value = Convert.ToInt32(Math.Min(30600, totalTime/workedDays));
				indicator3.TextValue =
					new TimeSpan(0, 0, Convert.ToInt32(totalTime/workedDays, CultureInfo.InvariantCulture)).TotalHours.ToString("0.00",
					CultureInfo.
					InvariantCulture) +
					" hrs.";
			}				

			toolTip.SetToolTip(this.indicator3, workedDays + " worked days");
			toolTip.SetToolTip(this.groupBox4, workedDays+ " worked days");
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			TasksHierarchyForm tgForm = new TasksHierarchyForm();
			if(tgForm.ShowDialog(this) == DialogResult.OK)
				SetParent(tgForm.SelectedTaskId);
			
		}

		private void dateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			LaunchSummarySearch();
		}

		private void LaunchSummarySearch()
		{
			if (this.fromRadioButton.Checked)
			{
				this.toDateTimePicker.ValueChanged -= new EventHandler(dateTimePicker_ValueChanged);
				this.toDateTimePicker.Value = this.fromDateTimePicker.Value;
				this.toDateTimePicker.ValueChanged += new EventHandler(dateTimePicker_ValueChanged);
			}

			worker.DoWork((int) StatisticsControlWorks.GetTaskSummary, new AsyncWorker.AsyncWorkerDelegate(GetTasksSummary),
			              new object[] {null});
		}

		private void parentTaskComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (parentTaskComboBox.SelectedIndex == -1)
				return;

			parentTask = FindById(Convert.ToInt32(parentTaskComboBox.SelectedValue));
			LaunchSummarySearch();
		}

		private Task FindById(int taskId)
		{
			for(int i = 0;i<parentTasksList.Count;i++)
			{
				Task task = (Task)parentTasksList[i];
				if(task.Id == taskId)
					return task.Clone();
			}
			return null;
		}

		private void TaskLogTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (!this.Visible)
				return;

			if (this.fromDateTimePicker.Value > DateTime.Today || this.toDateTimePicker.Value < DateTime.Today)
				return;

			ListViewItem currentTaskSummary = null;

			int minGeneration = Int32.MaxValue;
			foreach (ListViewItem item in this.taskList.Items)
			{
				TaskSummary sum = (TaskSummary) item.Tag;
				int generations = Tasks.IsParent(sum.TaskId, Logs.CurrentLog.TaskId);
				if (generations >= 0 && generations < minGeneration)
				{
					minGeneration = generations;
					currentTaskSummary = item;
				}
			}

			if (currentTaskSummary != null)
			{
				TaskSummary sum = (TaskSummary) currentTaskSummary.Tag;
				if (!sum.IsActive)
				{
					sum.TotalInactiveTime++;
				}
				else
				{
					totalActiveTime ++;
					sum.TotalActiveTime++;
				}
				totalTime++;
				TimeSpan activeTimeSpan = new TimeSpan(0, 0, Convert.ToInt32(sum.TotalActiveTime));
				TimeSpan inactiveTimeSpan = new TimeSpan(0, 0, Convert.ToInt32(sum.TotalInactiveTime));
				currentTaskSummary.SubItems[ActiveTimeHeader.Index].Text = ViewHelper.TimeSpanToTimeString(activeTimeSpan);
				currentTaskSummary.SubItems[InactiveTimeHeader.Index].Text = ViewHelper.TimeSpanToTimeString(inactiveTimeSpan);

				this.CalculateTasksPercents();
				SetIndicatorsValues();
			}
		}

		private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button.ImageIndex == 0)
				GoToChildDetail();
			else if (e.Button.ImageIndex == 1)
				GoToParentDetail();
		}

		private void SetParent(int parentId)
		{
			if (FindById(parentId) == null)
			{
				parentTask = Tasks.FindById(parentId);
				parentTask.Description = ViewHelper.FixTaskPath(Tasks.GetFullPath(parentTask.Id), this.parentTaskComboBox.MaxLength);
				this.parentTasksList.Insert(0, parentTask);
				this.parentTaskComboBox.BeginUpdate();
				this.parentTaskComboBox.DataSource = null;
				this.parentTaskComboBox.DisplayMember = "Description";
				this.parentTaskComboBox.ValueMember = "Id";
				this.parentTaskComboBox.DataSource = parentTasksList;
				this.parentTaskComboBox.EndUpdate();
			}
			this.parentTaskComboBox.SelectedValue = parentId;
		}

		private void GoToParentDetail()
		{
			if (Tasks.RootTasksRow.Id == this.parentTask.Id)
			{
				return;
			}
			SetParent(parentTask.ParentId);
		}

		private void GoToChildDetail()
		{
			if (this.taskList.SelectedItems.Count == 0)
			{
				return;
			}
			TaskSummary sum = (TaskSummary) this.taskList.SelectedItems[0].Tag;
			SetParent(sum.TaskId);
		}

		private void taskList_DoubleClick(object sender, EventArgs e)
		{
			if (this.taskList.SelectedItems.Count == 0)
				return;

			TaskSummary sum = (TaskSummary) this.taskList.SelectedItems[0].Tag;
			SetParent(sum.TaskId);
		}

		private void toRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.toDateTimePicker.Enabled = true;
			this.fromRadioButton.Text = "From:";
			this.groupBox4.Visible = true;
		}

		private void fromRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.toDateTimePicker.Enabled = false;
			this.toDateTimePicker.Value = this.fromDateTimePicker.Value;
			this.fromRadioButton.Text = "Date:";
			this.groupBox4.Visible = false;
		}

		#region AsyncWork

		private enum StatisticsControlWorks : int
		{
			GetTaskSummary
		}

		private object GetTasksSummary(object p)
		{
			DateTime fromDate;
			DateTime toDate;
			fromDate = fromDateTimePicker.Value.Date;
			if (this.toRadioButton.Checked)
			{
				toDate = toDateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);
			}
			else
			{
				toDate = fromDateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);
			}
			TaskSummaryResult result = new TaskSummaryResult();
			result.SummaryList = TasksSummaries.GetTaskSummary(
				Tasks.FindById((int) this.parentTaskComboBox.SelectedValue),
				fromDate, toDate);
			result.WorkedDays = TasksSummaries.GetWorkedDays(fromDate.Date, toDate.Date);
			return result;
		}

		private class TaskSummaryResult
		{
			public ArrayList SummaryList;
			public int WorkedDays;
		}

		private void worker_OnBeforeDoWork(AsyncWorker.OnBeforeDoWorkEventArgs e)
		{
			switch (e.WorkId)
			{
				case (int) StatisticsControlWorks.GetTaskSummary:
					SetWaitState();
					break;
			}
		}

		private void worker_OnWorkDone(AsyncWorker.OnWorkDoneEventArgs e)
		{
			switch (e.WorkId)
			{
				case (int) StatisticsControlWorks.GetTaskSummary:

					UpdateTasksSummaryDelegate del = new UpdateTasksSummaryDelegate(UpdateTasksSummary);

					this.Invoke(del, new object[] {e.Result});
					break;
			}
		}

		private void SetWaitState()
		{
			this.Status = "Retrieving data...";
			this.parentTaskComboBox.Enabled = false;
			toDateTimePicker.Enabled = false;
			fromDateTimePicker.Enabled = false;
			this.browseButton.Enabled = false;
			this.taskList.Items.Clear();

			indicator1.Maximum = 30600; //8.5 hrs.
			indicator1.Value = 0;
			indicator1.TextValue = "0.00 hrs.";
			indicator1.ForeColor = Color.Lime;

			indicator2.Maximum = 100;
			indicator2.Value = 0;
			indicator2.TextValue = "0%";

			indicator3.Maximum = 30600; //8.5 hrs.
			indicator3.Value = 0;
			indicator3.TextValue = "0.00 hrs.";
			indicator3.ForeColor = Color.Lime;

			totalTime = 0;
			totalActiveTime = 0;

			this.Refresh();
			this.Cursor = Cursors.WaitCursor;
			foreach (Control control in this.Controls)
			{
				control.Cursor = Cursors.WaitCursor;
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
			if (this.toRadioButton.Checked)
				toDateTimePicker.Enabled = true;

			fromDateTimePicker.Enabled = true;
			this.browseButton.Enabled = true;
		}

		private delegate void UpdateTasksSummaryDelegate(TaskSummaryResult result);

		#endregion
	}
}