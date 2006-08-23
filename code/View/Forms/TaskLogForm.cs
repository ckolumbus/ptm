using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for TaskNotificationForm.
	/// </summary>
	public class TaskLogForm : Form
	{
		private Label label1;
		private Label label3;
		private Label label6;
		private ComboBox taskComboBox;
		private NumericUpDown minsNumericUpDown;
		private Button okButton;
		private Button cancelButton;
		private Label label2;
		private Button browseGroupButton;
		private ComboBox parentTaskComboBox;
		private Label durationValueLabel;
		private CheckBox hideDefaultTasksCheckBox;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public TaskLogForm()
		{
			InitializeComponent();			
			FillDefaultParentTasks();

			this.durationValueLabel.Visible = false;
			//parentTaskComboBox.SelectedIndex = -1;
			//this.parentTaskComboBox.SelectedIndexChanged+=new EventHandler(parentTaskComboBox_SelectedIndexChanged);

			int defaultMins = Convert.ToInt32(ConfigurationHelper.GetConfiguration(ConfigurationKey.DefaultTasksLogDuration).ConfigValue, CultureInfo.InvariantCulture);
			this.duration = new TimeSpan(0, defaultMins, 0);
			this.minsNumericUpDown.Value = duration.Minutes;

			if(Tasks.CurrentTaskRow != null)
			{
				parentTaskComboBox.SelectedValue = Tasks.CurrentTaskRow.ParentId;
			}
			else
			{
				parentTaskComboBox.SelectedValue = Tasks.RootTasksRow.Id;
			}
			parentTaskComboBox_SelectedValueChanged(null, null);
		}

		public TaskLogForm(int taskId, string duration)
		{
			InitializeComponent();
			FillDefaultParentTasks();
//			parentTaskComboBox.SelectedIndex = -1;
//			this.parentTaskComboBox.SelectedIndexChanged+=new EventHandler(parentTaskComboBox_SelectedIndexChanged);

			this.minsNumericUpDown.Enabled = false;
			this.minsNumericUpDown.Visible = false;
			this.durationValueLabel.Visible = true;
			this.durationValueLabel.Text = duration;
			//this.duration = new TimeSpan(0, 0, durationSeconds);
			//this.minsNumericUpDown.Value = duration.Minutes;
			
			PTMDataset.TasksRow row;
			row  = Tasks.FindById(taskId);
			PTMDataset.TasksRow parentRow = Tasks.FindById(row.ParentId);
			SetParentTask(parentRow);
			SetChildTask(row);
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TaskLogForm));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.taskComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.minsNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.browseGroupButton = new System.Windows.Forms.Button();
			this.parentTaskComboBox = new System.Windows.Forms.ComboBox();
			this.durationValueLabel = new System.Windows.Forms.Label();
			this.hideDefaultTasksCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.minsNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(288, 72);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 0;
			this.okButton.Text = "Ok";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(376, 72);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Description:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// taskComboBox
			// 
			this.taskComboBox.Enabled = false;
			this.taskComboBox.Location = new System.Drawing.Point(80, 32);
			this.taskComboBox.MaxLength = 80;
			this.taskComboBox.Name = "taskComboBox";
			this.taskComboBox.Size = new System.Drawing.Size(256, 21);
			this.taskComboBox.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 23);
			this.label3.TabIndex = 13;
			this.label3.Text = "Duration:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// minsNumericUpDown
			// 
			this.minsNumericUpDown.Increment = new System.Decimal(new int[] {
																									 5,
																									 0,
																									 0,
																									 0});
			this.minsNumericUpDown.Location = new System.Drawing.Point(80, 56);
			this.minsNumericUpDown.Maximum = new System.Decimal(new int[] {
																								  60,
																								  0,
																								  0,
																								  0});
			this.minsNumericUpDown.Minimum = new System.Decimal(new int[] {
																								  1,
																								  0,
																								  0,
																								  0});
			this.minsNumericUpDown.Name = "minsNumericUpDown";
			this.minsNumericUpDown.Size = new System.Drawing.Size(48, 20);
			this.minsNumericUpDown.TabIndex = 16;
			this.minsNumericUpDown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
			this.minsNumericUpDown.Value = new System.Decimal(new int[] {
																								1,
																								0,
																								0,
																								0});
			this.minsNumericUpDown.ValueChanged += new System.EventHandler(this.minsNumericUpDown_ValueChanged);
			// 
			// label6
			// 
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(128, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 23);
			this.label6.TabIndex = 17;
			this.label6.Text = "mins.";
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 18;
			this.label2.Text = "Detail Level:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// browseGroupButton
			// 
			this.browseGroupButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.browseGroupButton.Location = new System.Drawing.Point(368, 6);
			this.browseGroupButton.Name = "browseGroupButton";
			this.browseGroupButton.Size = new System.Drawing.Size(80, 23);
			this.browseGroupButton.TabIndex = 20;
			this.browseGroupButton.Text = "Browse...";
			this.browseGroupButton.Click += new System.EventHandler(this.browseGroupButton_Click);
			// 
			// parentTaskComboBox
			// 
			this.parentTaskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.parentTaskComboBox.Location = new System.Drawing.Point(80, 8);
			this.parentTaskComboBox.MaxLength = 50;
			this.parentTaskComboBox.Name = "parentTaskComboBox";
			this.parentTaskComboBox.Size = new System.Drawing.Size(280, 21);
			this.parentTaskComboBox.TabIndex = 21;
			this.parentTaskComboBox.SelectedValueChanged += new System.EventHandler(this.parentTaskComboBox_SelectedValueChanged);
			// 
			// durationValueLabel
			// 
			this.durationValueLabel.Location = new System.Drawing.Point(80, 56);
			this.durationValueLabel.Name = "durationValueLabel";
			this.durationValueLabel.Size = new System.Drawing.Size(112, 23);
			this.durationValueLabel.TabIndex = 22;
			// 
			// hideDefaultTasksCheckBox
			// 
			this.hideDefaultTasksCheckBox.Checked = true;
			this.hideDefaultTasksCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.hideDefaultTasksCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.hideDefaultTasksCheckBox.Location = new System.Drawing.Point(344, 32);
			this.hideDefaultTasksCheckBox.Name = "hideDefaultTasksCheckBox";
			this.hideDefaultTasksCheckBox.Size = new System.Drawing.Size(112, 32);
			this.hideDefaultTasksCheckBox.TabIndex = 23;
			this.hideDefaultTasksCheckBox.Text = "Hide default tasks";
			this.hideDefaultTasksCheckBox.CheckedChanged += new System.EventHandler(this.hideDefaultTasksCheckBox_CheckedChanged);
			// 
			// TaskLogForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(454, 102);
			this.Controls.Add(this.hideDefaultTasksCheckBox);
			this.Controls.Add(this.parentTaskComboBox);
			this.Controls.Add(this.browseGroupButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.minsNumericUpDown);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.taskComboBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.durationValueLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TaskLogForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Task Log";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.TaskLogForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.minsNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion


		private TimeSpan duration;
		private PTMDataset.TasksRow selectedTaskRow = null;
		private PTMDataset.TasksRow selectedParentTaskRow = null;

		public PTMDataset.TasksRow SelectedTaskRow
		{
			get { return selectedTaskRow; }
		}

		public TimeSpan Duration
		{
			get { return duration; }
			set { duration = value; }
		}


		private PTMDataset.TasksDataTable childTasksTable = null;
		private PTMDataset.TasksDataTable parentTasksTable = null;

		private void FillDefaultParentTasks()
		{
			parentTasksTable = new PTMDataset.TasksDataTable();
			parentTasksTable.Rows.Clear();
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
				parentTaskRow.Description = ViewHelper.FixTaskPath(Tasks.GetFullPath(parentTaskRow), this.parentTaskComboBox.MaxLength);
				parentTasksTable.AddTasksRow(parentTaskRow);
			}
			this.parentTaskComboBox.DisplayMember = parentTasksTable.DescriptionColumn.ColumnName;
			this.parentTaskComboBox.ValueMember = parentTasksTable.IdColumn.ColumnName;
			this.parentTaskComboBox.DataSource = parentTasksTable.DefaultView;
		}

		private void FillChildTasks(bool showDefaulTasks)
		{
			childTasksTable = new PTMDataset.TasksDataTable();
			PTMDataset.TasksRow[] childRows;
			childRows = Tasks.GetChildTasks(selectedParentTaskRow);

			if(showDefaulTasks)
			{
				foreach (PTMDataset.TasksRow childRow in childRows)
				{
					PTMDataset.TasksRow row;
					row = childTasksTable.NewTasksRow();
					row.ItemArray = childRow.ItemArray;
					childTasksTable.AddTasksRow(row);
				}
				foreach (PTMDataset.TasksRow defaultRow in DefaultTasks.DefaultTasksDataTable.Rows)
				{
					bool exist = false;
					foreach (PTMDataset.TasksRow childRow in childTasksTable.Rows)
					{
						if (string.Compare(childRow.Description.Replace(" ", null), defaultRow.Description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
						{
							exist = true;
							break;
						}
					}
					if(!exist)
					{
						PTMDataset.TasksRow row;
						row = childTasksTable.NewTasksRow();
						row.ItemArray = defaultRow.ItemArray;
						row.ParentId = this.selectedParentTaskRow.Id;
						row.Id = - defaultRow.Id;
						childTasksTable.AddTasksRow(row);
					}
				}
			}
			else
			{
				foreach (PTMDataset.TasksRow childRow in childRows)
				{
					bool exist = false;
					foreach (PTMDataset.TasksRow defaultRow in DefaultTasks.DefaultTasksDataTable.Rows)
					{
						if (string.Compare(childRow.Description.Replace(" ", null), defaultRow.Description.Replace(" ", null), true, CultureInfo.InvariantCulture) == 0)
						{
							exist = true;
							break;
						}
					}
					if(!exist)
					{
						PTMDataset.TasksRow row;
						row = childTasksTable.NewTasksRow();
						row.ItemArray = childRow.ItemArray;
						childTasksTable.AddTasksRow(row);
					}
				}
			}
			this.taskComboBox.DisplayMember = childTasksTable.DescriptionColumn.ColumnName;
			this.taskComboBox.ValueMember = childTasksTable.IdColumn.ColumnName;
			this.taskComboBox.DataSource = childTasksTable.DefaultView;
			this.taskComboBox.Enabled = true;
			this.taskComboBox.Focus();
		}
		private void SetChildTask(PTMDataset.TasksRow childTaskRow)
		{
			if(childTasksTable.FindById(childTaskRow.Id)==null)
			{
				PTMDataset.TasksRow row = this.childTasksTable.NewTasksRow();
				row.ItemArray = childTaskRow.ItemArray;
				this.childTasksTable.Rows.InsertAt(row, 0 );
			}
			this.taskComboBox.SelectedValue= childTaskRow.Id;
		}
		private void SetParentTask(PTMDataset.TasksRow parentTaskRow)
		{
			if(parentTaskRow==null)
				return;
			if(parentTasksTable.FindById(parentTaskRow.Id)==null)
			{
				PTMDataset.TasksRow parentRow = this.parentTasksTable.NewTasksRow();
				parentRow.ItemArray = parentTaskRow.ItemArray;
				parentRow.Description = ViewHelper.FixTaskPath(Tasks.GetFullPath(parentRow), this.parentTaskComboBox.MaxLength);
				this.parentTasksTable.Rows.InsertAt(parentRow, 0 );
			}
			this.parentTaskComboBox.SelectedValue= parentTaskRow.Id;
			parentTaskComboBox_SelectedValueChanged(null, null);
		}


		private void TaskLogForm_Load(object sender, EventArgs e)
		{
			this.parentTaskComboBox.Focus();		
		}

		private void hideDefaultTasksCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			FillChildTasks(!this.hideDefaultTasksCheckBox.Checked);
		}

		private void browseGroupButton_Click(object sender, EventArgs e)
		{
			TasksHierarchyForm tgForm = new TasksHierarchyForm();
			tgForm.ShowDialog(this);
			//FillDefaultParentTasks();
			if(tgForm.SelectedTaskRow == null)
			{
				//if(this.selectedParentTaskRow==null)
					return;
//				else
//					SetParentTask(selectedParentTaskRow);
			}
			else
			{
				SetParentTask(tgForm.SelectedTaskRow);
			}
		}

		private bool cancelClose = false;
		private void okButton_Click(object sender, EventArgs e)
		{
			try
			{
				string description = this.taskComboBox.Text.Trim();
				this.duration = new TimeSpan(0, Convert.ToInt32(this.minsNumericUpDown.Value), 0);

				PTMDataset.TasksRow row;
				row = Tasks.FindByParentIdAndDescription((int) this.parentTaskComboBox.SelectedValue, description);

				if (row==null)
				{
					this.selectedTaskRow = Tasks.NewTasksRow();
					this.selectedTaskRow.Description = description;
					this.selectedTaskRow.ParentId = (int) this.parentTaskComboBox.SelectedValue;
					this.selectedTaskRow.Id = Tasks.AddTasksRow(this.selectedTaskRow);
				}
				else
				{
					selectedTaskRow = row;
				}
				ConfigurationHelper.AddRecentTask(this.selectedParentTaskRow);
			}
			catch(ApplicationException aex)
			{
				MessageBox.Show(aex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cancelClose = true;
				return;
			}
		}

		private void minsNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			if(minsNumericUpDown.Value > minsNumericUpDown.Maximum)
			{
				minsNumericUpDown.Value = minsNumericUpDown.Maximum;
			}
		}
		
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing (e);
			if(cancelClose)
			{e.Cancel = true;
			cancelClose= false;}
		}

		private void parentTaskComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if(parentTaskComboBox.SelectedIndex == -1)
				return;
			this.selectedParentTaskRow = Tasks.FindById((int) parentTaskComboBox.SelectedValue);

			FillChildTasks(!this.hideDefaultTasksCheckBox.Checked);
		}
	}
}