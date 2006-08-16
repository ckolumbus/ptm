using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Timers;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;
using PTM.View.Forms;

namespace PTM.View.Controls
{
	/// <summary>
	/// Summary description for Summary.
	/// </summary>
	public class SummaryControl : UserControl
	{
		private DateTimePicker dateTimePicker;
		private ListView taskList;
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
		private ColumnHeader TaskIdHeader;

		private PTMDataset.TasksDataTable parentTasksTable = new PTMDataset.TasksDataTable();
		
		public SummaryControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			ResourceManager resourceManager = new ResourceManager ("PTM.View.Controls.Icons", GetType().Assembly);

			this.tasksIconsList.Images.Clear();
			Icon resIcon = null;
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
			
			if(parentTaskComboBox.Items.Count>0)
				parentTaskComboBox.SelectedIndex = 0;
			this.dateTimePicker.ValueChanged += new EventHandler(this.dateTimePicker_ValueChanged);
			this.parentTaskComboBox.SelectedIndexChanged+=new EventHandler(parentTaskComboBox_SelectedIndexChanged);
			
			TasksLog.TasksLogDurationCountElapsed+=new ElapsedEventHandler(TaskLogTimer_Elapsed);
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
			this.taskList = new System.Windows.Forms.ListView();
			this.TaskHeader = new System.Windows.Forms.ColumnHeader();
			this.DurationTaskHeader = new System.Windows.Forms.ColumnHeader();
			this.PercentHeader = new System.Windows.Forms.ColumnHeader();
			this.TaskIdHeader = new System.Windows.Forms.ColumnHeader();
			this.tasksIconsList = new System.Windows.Forms.ImageList(this.components);
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.indicator1 = new PTM.View.Controls.IndicatorControl();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.parentTaskComboBox = new System.Windows.Forms.ComboBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
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
			this.taskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.taskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																											  this.TaskHeader,
																											  this.DurationTaskHeader,
																											  this.PercentHeader,
																											  this.TaskIdHeader});
			this.taskList.FullRowSelect = true;
			this.taskList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.taskList.Location = new System.Drawing.Point(8, 16);
			this.taskList.Name = "taskList";
			this.taskList.Size = new System.Drawing.Size(376, 160);
			this.taskList.SmallImageList = this.tasksIconsList;
			this.taskList.TabIndex = 2;
			this.taskList.View = System.Windows.Forms.View.Details;
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
			// TaskIdHeader
			// 
			this.TaskIdHeader.Text = "Id";
			this.TaskIdHeader.Width = 0;
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
			this.groupBox1.Size = new System.Drawing.Size(88, 96);
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
			this.indicator1.Size = new System.Drawing.Size(82, 77);
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
			this.groupBox3.Location = new System.Drawing.Point(8, 168);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(392, 184);
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
			// Summary
			// 
			this.Controls.Add(this.browseButton);
			this.Controls.Add(this.parentTaskComboBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimePicker);
			this.Controls.Add(this.groupBox1);
			this.Name = "Summary";
			this.Size = new System.Drawing.Size(408, 360);
			this.Load += new System.EventHandler(this.Summary_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void dateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			UpdateList();
		}
		

		private double totalTime = 0;
		private void Clear()
		{
			this.taskList.Items.Clear();
			indicator1.Maximum = 30600;//8.5 hrs.
			indicator1.Value = 0;
			indicator1.TextValue = "00.00 hrs.";
			indicator1.ForeColor = Color.Lime;
			totalTime = 0;
		}

		public void UpdateList()
		{
//			try
//			{
				Clear();
			UnitOfWork.Update();
				SummaryDataset.TasksSummaryDataTable summaryDataTable = Summary.GetTaskSummary( 
					Tasks.FindById((int)this.parentTaskComboBox.SelectedValue),
					dateTimePicker.Value.Date, dateTimePicker.Value.Date.AddDays(1));
				
				totalTime = 0;
				foreach (SummaryDataset.TasksSummaryRow row in summaryDataTable.Rows)
				{
					totalTime+= row.TotalTime;
				}
				foreach (SummaryDataset.TasksSummaryRow row in summaryDataTable.Rows)
				{
//					double percent =  0;
//					if(totalTime>0)
//						percent = row.TotalTime / totalTime;
					TimeSpan duration = new TimeSpan(0, 0, Convert.ToInt32(row.TotalTime));
					ListViewItem lvi = new ListViewItem(new string[] {row.Description, ViewHelper.TimeSpanToTimeString(duration), 0.ToString("0.0%", CultureInfo.InvariantCulture), row.TaskId.ToString(CultureInfo.InvariantCulture)});
					lvi.ImageIndex = 0;
					if (row.IsDefaultTask)
					{
						lvi.ImageIndex = row.DefaultTaskId;
					}
					else
					{
						lvi.ImageIndex = 0;
					}
					this.taskList.Items.Add(lvi);
				}
				CalculatePercent();
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
//			}
//			catch (Exception ex)
//			{
//				MessageBox.Show(ex.Message);
//			}
		}

		private void CalculatePercent()
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
			this.parentTaskComboBox.Focus();
			
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			TasksHierarchyForm tgForm = new TasksHierarchyForm();
			tgForm.ShowDialog(this);
			if(tgForm.SelectedTaskRow == null)
				return;

			if(parentTasksTable.FindById(tgForm.SelectedTaskRow.Id)==null)
			{
				PTMDataset.TasksRow parentRow = this.parentTasksTable.NewTasksRow();
				parentRow.ItemArray = tgForm.SelectedTaskRow.ItemArray;
				parentRow.Description = ViewHelper.FixTaskPath(Tasks.GetFullPath(parentRow), this.parentTaskComboBox.MaxLength);
				this.parentTasksTable.Rows.InsertAt(parentRow, 0);
			}
			this.parentTaskComboBox.SelectedValue= tgForm.SelectedTaskRow.Id;
		}

		private void parentTaskComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(parentTaskComboBox.SelectedIndex == -1)
				return;

			UpdateList();
		}


		private void TaskLogTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if(!this.Visible)
				return;

			if(this.dateTimePicker.Value != DateTime.Today)
				return;
			
			foreach (ListViewItem item in this.taskList.Items)
			{
				PTMDataset.TasksRow row = Tasks.FindById(Convert.ToInt32(item.SubItems[TaskIdHeader.Index].Text, CultureInfo.InvariantCulture));
				if(row.Id == Tasks.CurrentTaskRow.Id || Tasks.IsParent(row, Tasks.CurrentTaskRow))
				{
					TimeSpan t = TimeSpan.Parse(item.SubItems[DurationTaskHeader.Index].Text);
					t = t.Add(new TimeSpan(0, 0, 1));
					item.SubItems[DurationTaskHeader.Index].Text = ViewHelper.TimeSpanToTimeString(t);
					this.totalTime++;
					this.CalculatePercent();
					SetIndicatorsValues();
					return;
				}
			}
		
		}


	}
}