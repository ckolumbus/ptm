using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.APIs;
using System.Windows.Forms;
using PTM.Framework.Helpers;
using PopupControl;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for NotifyForm.
	/// </summary>
	internal class NotifyForm : UserControl
	{
		private Panel panel1;
		private Timer timer;
		private IContainer components;
		private Label currentTaskLabel;
		private Label label1;
		private Button yesButton;
		private Button noButton;

		internal enum NotifyResult
		{
			Waiting,
			Yes,
			No,
			Cancel
		}

		internal NotifyForm(string currentTask)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//this.Load += new EventHandler(NotifyForm_Load);
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
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.CausesValidation = false;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.noButton);
            this.panel1.Controls.Add(this.yesButton);
            this.panel1.Controls.Add(this.currentTaskLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 88);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "What are you doing now?";
            // 
            // noButton
            // 
            this.noButton.BackColor = System.Drawing.SystemColors.Control;
            this.noButton.CausesValidation = false;
            this.noButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noButton.Location = new System.Drawing.Point(11, 55);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(104, 23);
            this.noButton.TabIndex = 3;
            this.noButton.TabStop = false;
            this.noButton.Text = "Select from list";
            this.noButton.UseVisualStyleBackColor = false;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // yesButton
            // 
            this.yesButton.BackColor = System.Drawing.SystemColors.Control;
            this.yesButton.CausesValidation = false;
            this.yesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yesButton.Location = new System.Drawing.Point(11, 25);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(66, 23);
            this.yesButton.TabIndex = 2;
            this.yesButton.TabStop = false;
            this.yesButton.Text = "Continue";
            this.yesButton.UseVisualStyleBackColor = false;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // currentTaskLabel
            // 
            this.currentTaskLabel.AutoEllipsis = true;
            this.currentTaskLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentTaskLabel.Location = new System.Drawing.Point(83, 30);
            this.currentTaskLabel.Name = "currentTaskLabel";
            this.currentTaskLabel.Size = new System.Drawing.Size(102, 16);
            this.currentTaskLabel.TabIndex = 10;
            this.currentTaskLabel.Text = "last task";
            // 
            // timer
            // 
            this.timer.Interval = 15000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // NotifyForm
            // 
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(195, 88);
            this.Controls.Add(this.panel1);
            this.Name = "NotifyForm";
            this.Text = "NotifyForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private NotifyResult result = NotifyResult.Waiting;

		internal NotifyResult Result
		{
			get { return result; }
		}

		private void timer_Tick(object sender, EventArgs e)
		{
    		this.timer.Stop();
            (Parent as Popup).Close();
		}

		private void noButton_Click(object sender, EventArgs e)
		{
			result = NotifyResult.No;
			this.timer.Stop();
            (Parent as Popup).Close();
		}

		private void yesButton_Click(object sender, EventArgs e)
		{
			result = NotifyResult.Yes;
			this.timer.Stop();
            (Parent as Popup).Close();
		}

		protected void OnClosed(EventArgs e)
		{
			if (result == NotifyResult.Waiting)
			{
				result = NotifyResult.Cancel;
			}
			this.timer.Stop();
		}

		static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        public void ShowNoActivate()
		{
			int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
			int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
			this.Left = screenWidth - this.Width;
			this.Top = screenHeight - this.Height;

			Configuration c;
		    c = ConfigurationHelper.GetConfiguration(ConfigurationKey.PlaySoundOnReminder);
            if (Convert.ToInt32(c.Value) == 1)
                System.Media.SystemSounds.Asterisk.Play();

			this.timer.Start();

            //// Show the window without activating it.
            APIsUser32.ShowWindow(this.Handle, APIsEnums.ShowWindowStyles.SHOWNOACTIVATE);

            //// Equivalent to setting TopMost = true, except don't activate the window.
            APIsUser32.SetWindowPos(this.Handle, HWND_TOPMOST, Left, Top, Width, Height, 10);    
            //SetWindowPos((int)this.Handle, 0, Left, Top, Width, Height, System.Convert.ToUInt16(SWP.FRAMECHANGED | SWP.NOACTIVATE | SWP.NOCOPYBITS | SWP.NOMOVE | SWP.NOOWNERZORDER | SWP.NOSENDCHANGING | SWP.NOSIZE | SWP.NOZORDER));
		}

        public void reset()
        {
            this.timer.Stop();
        }

        public void Prepare()
        {
//            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
//            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
//            this.Left = screenWidth - this.Width;
//            this.Top = screenHeight - this.Height;

            Configuration c;
            c = ConfigurationHelper.GetConfiguration(ConfigurationKey.PlaySoundOnReminder);
            if (Convert.ToInt32(c.Value) == 1)
                System.Media.SystemSounds.Asterisk.Play();

            this.timer.Start();
        }      

        protected override void SetVisibleCore(bool value)
        {
            if (value)
            {
                APIsUser32.ShowWindow(this.Handle, APIsEnums.ShowWindowStyles.SHOWNOACTIVATE);
            }
            else base.SetVisibleCore(value);
        }


	}
}