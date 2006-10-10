using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PTM.Addin.WeekView
{
	public class WeekReport : PTM.Addin.TabPageAddin
	{
		private Calendar.DayView dayView;
		private System.ComponentModel.IContainer components = null;

		public WeekReport()
		{
			// This call is required by the Windows Form Designer.
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
			this.dayView = new Calendar.DayView();
			this.SuspendLayout();
			// 
			// dayView
			// 
			this.dayView.AllowInplaceEditing = false;
			this.dayView.AllowNew = false;
			this.dayView.DaysToShow = 7;
			this.dayView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.dayView.Location = new System.Drawing.Point(8, 72);
			this.dayView.Name = "dayView";
			this.dayView.SelectionEnd = new System.DateTime(((long)(0)));
			this.dayView.SelectionStart = new System.DateTime(((long)(0)));
			this.dayView.Size = new System.Drawing.Size(392, 272);
			this.dayView.StartDate = new System.DateTime(((long)(0)));
			this.dayView.TabIndex = 0;
			// 
			// WeekReport
			// 
			this.Controls.Add(this.dayView);
			this.Name = "WeekReport";
			this.Size = new System.Drawing.Size(408, 352);
			this.ResumeLayout(false);

		}
		#endregion
	}
}

