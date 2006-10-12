using System.ComponentModel;
using System.Windows.Forms;
using PTM.Business.Helpers;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for SplashForm.
	/// </summary>
	internal class SplashForm : Form
	{
		private PictureBox pictureBox1;
		private Label label1;
		private Label lblVersion;
		private Panel panel1;
		private ProgressBar progressBar;
		private IContainer components;

		internal SplashForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			lblVersion.Text += ConfigurationHelper.GetVersionString();


			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (SplashForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 11);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(62, 58);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.Font =
				new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold,
				                        System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.label1.ForeColor = System.Drawing.Color.Brown;
			this.label1.Location = new System.Drawing.Point(80, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "People Task Manager";
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.Color.White;
			this.lblVersion.ForeColor = System.Drawing.Color.Black;
			this.lblVersion.Location = new System.Drawing.Point(88, 48);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.progressBar);
			this.panel1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(272, 112);
			this.panel1.TabIndex = 4;
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(8, 80);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(256, 23);
			this.progressBar.TabIndex = 0;
			// 
			// SplashForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(272, 112);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SplashForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TransparencyKey = System.Drawing.Color.Lime;
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		internal void SetLoadProgress(int percent)
		{
			if (percent > 100)
				return;
			this.progressBar.Value = percent;
			this.Refresh();
			Application.DoEvents();
		}

		internal void AddProgress(int percent)
		{
			SetLoadProgress(this.progressBar.Value + 10);
		}
	}
}