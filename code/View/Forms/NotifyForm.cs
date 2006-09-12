using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for NotifyForm.
	/// </summary>
	public class NotifyForm : Form
	{
		private Panel panel1;
		private Timer timer;
		private IContainer components;
		public const int WAIT_TIME = 10;
		private int waitCount = 0;
		private Label currentTaskLabel;
		private Label label1;
		private Button yesButton;
		private Button noButton;

//		private enum Status
//		{
//			Init,
//			Asking,
//			Closing,
//			Close
//		}

		public enum NotifyResult
		{
			Waiting,
			Yes,
			No,
			Cancel
		}

		//private Status status;

		public NotifyForm(string currentTask)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.Load += new EventHandler(NotifyForm_Load);
			this.currentTaskLabel.Text = currentTask;
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
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.noButton = new System.Windows.Forms.Button();
			this.yesButton = new System.Windows.Forms.Button();
			this.currentTaskLabel = new System.Windows.Forms.Label();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.noButton);
			this.panel1.Controls.Add(this.yesButton);
			this.panel1.Controls.Add(this.currentTaskLabel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(296, 78);
			this.panel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Are you still doing this?";
			// 
			// noButton
			// 
			this.noButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.noButton.Font =
				new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold,
				                        System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.noButton.Location = new System.Drawing.Point(184, 48);
			this.noButton.Name = "noButton";
			this.noButton.Size = new System.Drawing.Size(40, 23);
			this.noButton.TabIndex = 3;
			this.noButton.Text = "No";
			this.noButton.Click += new System.EventHandler(this.noButton_Click);
			// 
			// yesButton
			// 
			this.yesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.yesButton.Font =
				new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold,
				                        System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.yesButton.Location = new System.Drawing.Point(120, 48);
			this.yesButton.Name = "yesButton";
			this.yesButton.Size = new System.Drawing.Size(40, 23);
			this.yesButton.TabIndex = 2;
			this.yesButton.Text = "Yes";
			this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
			// 
			// currentTaskLabel
			// 
			this.currentTaskLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.currentTaskLabel.Font =
				new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold,
				                        System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.currentTaskLabel.Location = new System.Drawing.Point(8, 32);
			this.currentTaskLabel.Name = "currentTaskLabel";
			this.currentTaskLabel.Size = new System.Drawing.Size(280, 16);
			this.currentTaskLabel.TabIndex = 1;
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// NotifyForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (255)), ((System.Byte) (192)));
			this.ClientSize = new System.Drawing.Size(296, 78);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NotifyForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "NotifyForm";
			this.TopMost = true;
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private NotifyResult result = NotifyResult.Waiting;

		public NotifyResult Result
		{
			get { return result; }
		}

		private void timer_Tick(object sender, EventArgs e)
		{
//			if (status == Status.Init)
//			{
//				this.Opacity += 0.5;
//				if (this.Opacity >= 1)
//					status = Status.Asking;
//			}
//			else if (status == Status.Asking)
//			{
//				this.waitCount ++;
//				if (this.waitCount >= NotifyForm.WAIT_TIME)
//					status = Status.Closing;
//			}
//			else if (status == Status.Closing)
//			{
//				this.Opacity -= 0.5;
//				if (this.Opacity <= 0.00)
//				{
//					status = Status.Close;
//				}
//			}
//			else if (status == Status.Close)
//			{
//				this.timer.Stop();
//				this.Close();
//			}

			this.waitCount ++;
			if (this.waitCount >= WAIT_TIME)
			{
				this.timer.Stop();
				this.Close();
			}
		}

		private void NotifyForm_Load(object sender, EventArgs e)
		{
			int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
			int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
			this.Left = screenWidth - this.Width;
			this.Top = screenHeight - this.Height;
			this.timer.Start();
			//this.status = Status.Init;
		}

		private void noButton_Click(object sender, EventArgs e)
		{
			result = NotifyResult.No;
			this.timer.Stop();
			this.Close();
		}

		private void yesButton_Click(object sender, EventArgs e)
		{
			result = NotifyResult.Yes;
			this.timer.Stop();
			this.Close();
		}

		protected override void OnClosed(EventArgs e)
		{
			if (result == NotifyResult.Waiting)
			{
				result = NotifyResult.Cancel;
			}
			this.timer.Stop();
			base.OnClosed(e);
		}
	}
}