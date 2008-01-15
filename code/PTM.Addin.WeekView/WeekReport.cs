using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Calendar;
using PTM.Framework;
using PTM.Framework.Infos;

namespace PTM.Addin.WeekView
{
	public class WeekReport : AddinTabPage
	{
		private Calendar.DayView dayView;
		private Button backButton;
        private Button forwardButton;
		private IContainer components = null;
        private ToolTip toolTip;
        private MonthCalendar monthCalendar;
		private int currentWeek;

		public WeekReport()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			base.Text = "Week Report";
			// TODO: Add any initialization after the InitializeComponent call
			this.dayView.ResolveAppointments+=new ResolveAppointmentsEventHandler(dayView_ResolveAppointments);
            this.dayView.MouseMove += new MouseEventHandler(dayView_MouseMove);
			currentWeek = 0;
			this.Status = String.Empty;
		}        

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeekReport));
            Calendar.DrawTool drawTool1 = new Calendar.DrawTool();
            this.backButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.dayView = new Calendar.DayView();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
            this.backButton.Location = new System.Drawing.Point(11, 20);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(24, 23);
            this.backButton.TabIndex = 1;
            this.toolTip.SetToolTip(this.backButton, "Past week");
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.forwardButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardButton.Image")));
            this.forwardButton.Location = new System.Drawing.Point(150, 20);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(24, 23);
            this.forwardButton.TabIndex = 3;
            this.toolTip.SetToolTip(this.forwardButton, "Next week");
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 50;
            // 
            // monthCalendar
            // 
            this.monthCalendar.Enabled = false;
            this.monthCalendar.FirstDayOfWeek = System.Windows.Forms.Day.Sunday;
            this.monthCalendar.Location = new System.Drawing.Point(6, 17);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.ShowTodayCircle = false;
            this.monthCalendar.TabIndex = 4;
            // 
            // dayView
            // 
            drawTool1.DayView = this.dayView;
            this.dayView.ActiveTool = drawTool1;
            this.dayView.AllowInplaceEditing = false;
            this.dayView.AllowNew = false;
            this.dayView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dayView.DaysToShow = 7;
            this.dayView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dayView.HalfHourHeight = 34;
            this.dayView.Location = new System.Drawing.Point(186, 3);
            this.dayView.Name = "dayView";
            this.dayView.SelectionEnd = new System.DateTime(((long)(0)));
            this.dayView.SelectionStart = new System.DateTime(((long)(0)));
            this.dayView.Size = new System.Drawing.Size(190, 333);
            this.dayView.StartDate = new System.DateTime(((long)(0)));
            this.dayView.TabIndex = 0;
            this.dayView.WorkingHourEnd = 23;
            this.dayView.WorkingHourStart = 0;
            this.dayView.WorkingMinuteEnd = 59;
            this.dayView.WorkingMinuteStart = 0;
            // 
            // WeekReport
            // 
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.dayView);
            this.Controls.Add(this.monthCalendar);
            this.Name = "WeekReport";
            this.Size = new System.Drawing.Size(384, 344);
            this.ResumeLayout(false);

		}
		
		
		#endregion

		private void dayView_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				DateTime day = args.StartDate;
				do
				{
					MergedLogs logs;
					logs = MergedLogs.GetMergedLogsByDay(day);
					foreach (MergedLog log in logs)
					{				
						Task task;
						task = Tasks.FindById(log.MergeLog.TaskId);
						if(task.Id == Tasks.IdleTask.Id)
							continue;
					
						Appointment appointment = new Appointment();
						appointment.StartDate = log.MergeLog.InsertTime;
						appointment.EndDate = log.MergeLog.InsertTime.AddSeconds(log.MergeLog.Duration);
						appointment.Title = task.Description;
					    appointment.Tag = Tasks.GetFullPath(task.Id);
						appointment.Color = Color.Green;
						appointment.Locked = false;
						if(!task.IsActive)
						{
							appointment.Color = Color.Yellow;
						}
					
						args.Appointments.Add(appointment);
					}
					day = day.AddDays(1);
				} while (day <= args.EndDate);
			}
			catch
			{
				throw;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			SetWeek(currentWeek);
			base.OnLoad (e);
		}

		private void backButton_Click(object sender, EventArgs e)
		{
			this.currentWeek--;
			SetWeek(currentWeek);
		}
		
		private void forwardButton_Click(object sender, EventArgs e)
		{
			this.currentWeek++;
			SetWeek(currentWeek);
		}
		
		private void SetWeek(int week)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
                DateTime startDate = DateTime.Today.AddDays(-Convert.ToInt32(DateTime.Today.DayOfWeek) + week * 7);
			    this.monthCalendar.BoldedDates = new DateTime[] { startDate, startDate.AddDays(1), startDate.AddDays(2), startDate.AddDays(3), startDate.AddDays(4), startDate.AddDays(5), startDate.AddDays(6) };
                this.monthCalendar.SetDate(startDate);
                this.monthCalendar.UpdateBoldedDates();
                this.monthCalendar.Refresh();
			    this.dayView.StartDate = startDate;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F5)
				SetWeek(currentWeek);
			base.OnKeyDown (e);
		}

        void dayView_MouseMove(object sender, MouseEventArgs e)
        {
            if (dayView.GetAppointmentAt(e.X, e.Y) != null)
            {
                Appointment appointment = dayView.GetAppointmentAt(e.X, e.Y);
                //toolTip.ToolTipTitle = appointment.Title;
                toolTip.SetToolTip(dayView, appointment.Tag.ToString());
            }
            else
            {
                //toolTip.ToolTipTitle = "";
                toolTip.SetToolTip(dayView, "");
            }
        }
        
        
	}
}

