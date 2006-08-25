using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Timers;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;
using PTM.Infos;
using PTM.View.Controls.TreeListViewComponents;
using PTM.View.Forms;

namespace PTM.View.Controls
{
	/// <summary>
	/// Summary description for Summary.
	/// </summary>
	public class SummaryControl : UserControl
	{
		private DateTimePicker dateTimePicker;
		private TreeListView taskList;
		private ColumnHeader DurationTaskHeader;
		private Label label1;
		private ImageList tasksIconsList;
		private GroupBox groupBox1;
		private GroupBox groupBox3;
		private ColumnHeader PercentHeader;
		private IndicatorControl indicator1;
		private ColumnHeader TaskHeader;
		private Label label2;
		private ComboBox parentTaskComboBox;
		private IContainer components;
		private Button browseButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private PTM.View.Controls.IndicatorControl indicator2;
		private System.Windows.Forms.GroupBox groupBox4;
		private PTM.View.Controls.IndicatorControl indicator3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ImageList toolBarImages;

		private PTMDataset.TasksDataTable parentTasksTable = new PTMDataset.TasksDataTable();
		private PTMDataset.TasksRow parentRow;
		
		public SummaryControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			ResourceManager resourceManager = new ResourceManager ("PTM.View.Controls.Icons", GetType().Assembly);

			this.tasksIconsList.Images.Clear();
			Icon resIcon;
			int i = 1;
			do
			{
				resIcon = (Icon) resourceManager.GetObject("Icon" + i.ToString(CultureInfo.InvariantCulture));
				i++;
				if(resIcon!=null)
					this.tasksIconsList.Images.Add(resIcon);
			}while(resIcon!=null);

			ManagementDataset.ConfigurationRow[] defaultgroups = ConfigurationHelper.GetExistingGroups();
			foreach (ManagementDataset.ConfigurationRow defaultgroup in defaultgroups)
			{
				PTMDataset.TasksRow parentTaskRow;
				parentTaskRow = parentTasksTable.NewTasksRow();
				PTMDataset.TasksRow taskRow;
				taskRow = Tasks.FindById(Convert.ToInt32(defaultgroup.ConfigValue, CultureInfo.InvariantCulture));
				if(taskRow == null)
					continue;
				parentTaskRow.ItemArray = taskRow.ItemArray;
				parentTaskRow.Description = ViewHelper.FixTaskPath(Tasks.GetFullPath(parentTaskRow),this.parentTaskComboBox.MaxLength);
				parentTasksTable.AddTasksRow(parentTaskRow);
			}
			this.parentTaskComboBox.DataSource = parentTasksTable;
			this.parentTaskComboBox.DisplayMember = parentTasksTable.DescriptionColumn.ColumnName;
			this.parentTaskComboBox.ValueMember = parentTasksTable.IdColumn.ColumnName;
			
			this.dateTimePicker.Value = DateTime.Today;
			
			if(parentTasksTable.Count>0)
			{
				parentRow = (PTMDataset.TasksRow) parentTasksTable.Rows[0];
				this.parentTaskComboBox.SelectedValue = parentRow.Id;
			}
			this.dateTimePicker.ValueChanged += new EventHandler(this.dateTimePicker_ValueChanged);
			this.parentTaskComboBox.SelectedIndexChanged+=new EventHandler(parentTaskComboBox_SelectedIndexChanged);
			
			Logs.TasksLogDurationCountElapsed+=new ElapsedEventHandler(TaskLogTimer_Elapsed);
			this.taskList.DoubleClick+=new EventHandler(taskList_DoubleClick);
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
			this.label1 = new System.Windows.Forms.Label();
			this.taskList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
			this.TaskHeader = new System.Windows.Forms.ColumnHeader();
			this.DurationTaskHeader = new System.Windows.Forms.ColumnHeader();
			this.PercentHeader = new System.Windows.Forms.ColumnHeader();
			this.tasksIconsList = new System.Windows.Forms.ImageList(this.components);
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.indicator1 = new PTM.View.Controls.IndicatorControl();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.parentTaskComboBox = new System.Windows.Forms.ComboBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.indicator2 = new PTM.View.Controls.IndicatorControl();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.indicator3 = new PTM.View.Controls.IndicatorControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarImages = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Date:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
																											  this.DurationTaskHeader,
																											  this.PercentHeader});
			this.taskList.HideSelection = false;
			this.taskList.Location = new System.Drawing.Point(8, 16);
			this.taskList.MultiSelect = false;
			this.taskList.Name = "taskList";
			this.taskList.Size = new System.Drawing.Size(376, 184);
			this.taskList.SmallImageList = this.tasksIconsList;
			this.taskList.Sorting = System.Windows.Forms.SortOrder.None;
			this.taskList.TabIndex = 2;
			// 
			// TaskHeader
			// 
			this.TaskHeader.Text = "Description";
			this.TaskHeader.Width = 230;
			// 
			// DurationTaskHeader
			// 
			this.DurationTaskHeader.Text = "Duration";
			this.DurationTaskHeader.Width = 80;
			// 
			// PercentHeader
			// 
			this.PercentHeader.Text = "Percent";
			// 
			// tasksIconsList
			// 
			this.tasksIconsList.ImageSize = new System.Drawing.Size(16, 16);
			this.tasksIconsList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tasksIconsList.ImageStream")));
			this.tasksIconsList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Location = new System.Drawing.Point(80, 8);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new System.Drawing.Size(232, 20);
			this.dateTimePicker.TabIndex = 0;
			this.dateTimePicker.Value = new System.DateTime(2005, 8, 29, 0, 0, 0, 0);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.indicator1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.ForeColor = System.Drawing.Color.Blue;
			this.groupBox1.Location = new System.Drawing.Point(8, 64);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(72, 80);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Total Time";
			// 
			// indicator1
			// 
			this.indicator1.BackColor = System.Drawing.Color.Black;
			this.indicator1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.indicator1.ForeColor = System.Drawing.Color.Lime;
			this.indicator1.GraphWidth = 33;
			this.indicator1.Location = new System.Drawing.Point(3, 16);
			this.indicator1.Maximum = 2147483647;
			this.indicator1.Minimum = 0;
			this.indicator1.Name = "indicator1";
			this.indicator1.Size = new System.Drawing.Size(66, 61);
			this.indicator1.TabIndex = 0;
			this.indicator1.TextValue = "";
			this.indicator1.Value = 0;
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
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Tasks";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 9;
			this.label2.Text = "Detail Level:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// parentTaskComboBox
			// 
			this.parentTaskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.parentTaskComboBox.Location = new System.Drawing.Point(80, 32);
			this.parentTaskComboBox.MaxLength = 50;
			this.parentTaskComboBox.Name = "parentTaskComboBox";
			this.parentTaskComboBox.Size = new System.Drawing.Size(232, 21);
			this.parentTaskComboBox.TabIndex = 10;
			// 
			// browseButton
			// 
			this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.browseButton.Location = new System.Drawing.Point(320, 32);
			this.browseButton.Name = "browseButton";
			this.browseButton.TabIndex = 11;
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
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Active %";
			// 
			// indicator2
			// 
			this.indicator2.BackColor = System.Drawing.Color.Black;
			this.indicator2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.indicator2.ForeColor = System.Drawing.Color.Lime;
			this.indicator2.GraphWidth = 33;
			this.indicator2.Location = new System.Drawing.Point(3, 16);
			this.indicator2.Maximum = 2147483647;
			this.indicator2.Minimum = 0;
			this.indicator2.Name = "indicator2";
			this.indicator2.Size = new System.Drawing.Size(66, 61);
			this.indicator2.TabIndex = 0;
			this.indicator2.TextValue = "";
			this.indicator2.Value = 0;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.indicator3);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.ForeColor = System.Drawing.Color.Blue;
			this.groupBox4.Location = new System.Drawing.Point(168, 64);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(72, 80);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Inactive %";
			// 
			// indicator3
			// 
			this.indicator3.BackColor = System.Drawing.Color.Black;
			this.indicator3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.indicator3.ForeColor = System.Drawing.Color.Red;
			this.indicator3.GraphWidth = 33;
			this.indicator3.Location = new System.Drawing.Point(3, 16);
			this.indicator3.Maximum = 2147483647;
			this.indicator3.Minimum = 0;
			this.indicator3.Name = "indicator3";
			this.indicator3.Size = new System.Drawing.Size(66, 61);
			this.indicator3.TabIndex = 0;
			this.indicator3.TextValue = "";
			this.indicator3.Value = 0;
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
			// SummaryControl
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.browseButton);
			this.Controls.Add(this.parentTaskComboBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimePicker);
			this.Controls.Add(this.groupBox1);
			this.Name = "SummaryControl";
			this.Size = new System.Drawing.Size(408, 360);
			this.Load += new System.EventHandler(this.Summary_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void dateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			UpdateTasksSummary(dateTimePicker.Value);
		}
		

		private double totalTime = 0;
		private double totalActiveTime = 0;
		private double totalInactiveTime = 0;
		
		private void Clear()
		{
			this.taskList.Items.Clear();
			indicator1.Maximum = 30600;//8.5 hrs.
			indicator1.Value = 0;
			indicator1.TextValue = "00.00 hrs.";
			indicator1.ForeColor = Color.Lime;
			
			indicator2.Maximum = 100;
			indicator2.Value = 0;
			indicator2.TextValue = "0%";

			indicator3.Maximum = 100;
			indicator3.Value = 0;
			indicator3.TextValue = "0%";
			
			totalTime = 0;
			totalActiveTime = 0;
			totalInactiveTime = 0;
		}

		
		public void UpdateSummary()
		{
			if(this.dateTimePicker.Value != DateTime.Today)
				this.dateTimePicker.Value = DateTime.Today;
			else
			{
				UpdateTasksSummary(this.dateTimePicker.Value);
			}
		}
		
		private void UpdateTasksSummary(DateTime day)
		{
			try
			{
				Clear();
				this.Refresh();
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				ArrayList summaryList = Summary.GetTaskSummary( 
					Tasks.FindById((int)this.parentTaskComboBox.SelectedValue),
					day.Date, day.AddDays(1));
				
				totalTime = 0;

				foreach (TaskSummary summary in summaryList)
				{
					totalTime+= summary.TotalTime;
					if(!summary.IsDefaultTask)
						totalActiveTime += summary.TotalTime;
					else
						totalInactiveTime += summary.TotalTime;
					//					double percent =  0;
					//					if(totalTime>0)
					//						percent = row.TotalTime / totalTime;
					
					TimeSpan duration = new TimeSpan(0, 0, Convert.ToInt32(summary.TotalTime));
					TreeListViewItem lvi = new TreeListViewItem(summary.Description, new string[] {ViewHelper.TimeSpanToTimeString(duration), 0.ToString("0.0%", CultureInfo.InvariantCulture), summary.TaskId.ToString(CultureInfo.InvariantCulture)});
					lvi.ImageIndex = 0;
					if (summary.IsDefaultTask)
					{
						lvi.ImageIndex = summary.DefaultTaskId;
					}
					else
					{
						lvi.ImageIndex = 0;
					}
					lvi.Tag = summary;
					this.taskList.Items.Add(lvi);
				}
				CalculateTasksPercents();
				SetIndicatorsValues();
				//				if(totaltime> 30600)
				//				{
				//					indicator1.ForeColor = Color.Red;
				//					//overTimeValue.Text = ViewHelper.TimeSpanToString(new TimeSpan(0,0,(int)(row.TotalTime - 30600))) + " hrs.";
				//				}
				//				else
				//				{
				//					//overTimeValue.Text = "0.0 hrs.";
				//				}

			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}

		private void CalculateTasksPercents()
		{
			foreach (ListViewItem item in this.taskList.Items)
			{
				double percent =  0;
				TimeSpan t = TimeSpan.Parse(item.SubItems[this.DurationTaskHeader.Index].Text);
				if(totalTime>0)
					percent = t.TotalSeconds / totalTime;
				item.SubItems[PercentHeader.Index].Text = percent.ToString("0.0%", CultureInfo.InvariantCulture);
			}
		}

		private void SetIndicatorsValues()
		{
			indicator1.Value = Convert.ToInt32(Math.Min(30600 , totalTime));
			indicator1.TextValue = new TimeSpan(0,0, Convert.ToInt32(totalTime, CultureInfo.InvariantCulture)).TotalHours.ToString("0.00", CultureInfo.InvariantCulture) + " hrs.";
			
			if(totalTime>0)
			{
				int percentActiveTime = Convert.ToInt32(this.totalActiveTime*100/totalTime);
				indicator2.Value = percentActiveTime;
				indicator2.TextValue = percentActiveTime + "%";
			
				int percentInactiveTime = Convert.ToInt32(this.totalInactiveTime*100/totalTime);
				indicator3.Value = percentInactiveTime;
				indicator3.TextValue = percentInactiveTime + "%";	
			}		
		}

		//		private string TimeSpanToString(TimeSpan ts)
		//		{
		//			return ts.Hours.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + ":" + ts.Minutes.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + ":" + ts.Seconds.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
		//		}

		private void Summary_Load(object sender, EventArgs e)
		{
			//			if(TasksHelper.CurrentTaskRow != null)
			//			{
			//				parentTaskComboBox.SelectedValue = TasksHelper.CurrentTaskRow.ParentId;
			//			}
			//			else
			//			{
			//				parentTaskComboBox.SelectedValue = TasksHelper.RootTasksRow.Id;
			//			}
			//TasksLogHelper.TaskLogsTable.RowChanged+=new System.Data.DataRowChangeEventHandler(TaskLogsTable_RowChanged);
			//this.parentTaskComboBox.Focus();
			
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			TasksHierarchyForm tgForm = new TasksHierarchyForm();
			tgForm.ShowDialog(this);
			if(tgForm.SelectedTaskRow == null)
				return;
			
			SetParent(tgForm.SelectedTaskRow);
		}

		private void parentTaskComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(parentTaskComboBox.SelectedIndex == -1)
				return;

			UpdateTasksSummary(dateTimePicker.Value);
		}


		private void TaskLogTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if(!this.Visible)
				return;

			if(this.dateTimePicker.Value != DateTime.Today)
				return;
			
			foreach (ListViewItem item in this.taskList.Items)
			{
				TaskSummary sum = (TaskSummary) item.Tag;
				if(sum.TaskId == Logs.CurrentLog.TaskId || Tasks.IsParent(sum.TaskId, Logs.CurrentLog.TaskId))
				{
					sum.TotalTime++;
					TimeSpan duration = new TimeSpan(0, 0, Convert.ToInt32(sum.TotalTime));
					item.SubItems[DurationTaskHeader.Index].Text = ViewHelper.TimeSpanToTimeString(duration);
					this.totalTime++;
					if(!sum.IsDefaultTask)
						totalActiveTime ++;
					else
						totalInactiveTime ++;
					this.CalculateTasksPercents();
					SetIndicatorsValues();
					return;
				}
			}
		
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button.ImageIndex==0)
				GoToChildDetail();
			else if(e.Button.ImageIndex == 1)
				GoToParentDetail();
		}

		private void SetParent(PTMDataset.TasksRow parent)
		{
			if(parentTasksTable.FindById(parent.Id)==null)
			{
				parentRow = this.parentTasksTable.NewTasksRow();
				parentRow.ItemArray = parent.ItemArray;
				parentRow.Description = ViewHelper.FixTaskPath(Tasks.GetFullPath(parentRow), this.parentTaskComboBox.MaxLength);
				this.parentTasksTable.Rows.InsertAt(parentRow, 0);
			}
			this.parentTaskComboBox.SelectedValue = parent.Id;
		}
		
		private void GoToParentDetail()
		{
			if(Tasks.RootTasksRow.Id == this.parentRow.Id)
			{
				return;
			}
			SetParent(Tasks.FindById(parentRow.ParentId));
		}

		private void GoToChildDetail()
		{
			if(this.taskList.SelectedItems.Count==0)
			{
				return;
			}
			TaskSummary sum = (TaskSummary) this.taskList.SelectedItems[0].Tag;
			SetParent(Tasks.FindById(sum.TaskId));
		}

		private void taskList_DoubleClick(object sender, EventArgs e)
		{
			if(this.taskList.SelectedItems.Count==0)
				return;
			
			TaskSummary sum = (TaskSummary) this.taskList.SelectedItems[0].Tag;
			SetParent(Tasks.FindById(sum.TaskId));
		}
	}
}