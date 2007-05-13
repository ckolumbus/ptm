using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using PTM.Framework.Helpers;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	internal class AboutForm : Form
	{
		private PictureBox pictureBox1;
		private Label label1;
		private Button button1;
		private Label label3;
		private PictureBox pictureBox2;
		private Label lblVersion;
		private Label label4;
		private LinkLabel linkLabel;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		internal AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.lblVersion.Text += ConfigurationHelper.GetVersionString();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "PTM";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(264, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(80, 32);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(100, 23);
            this.lblVersion.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(80, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Carlos D. Estrada";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(176, 56);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 16);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(80, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "cestradac@users.sourceforge.net";
            // 
            // linkLabel
            // 
            this.linkLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel.Location = new System.Drawing.Point(80, 80);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(176, 23);
            this.linkLabel.TabIndex = 8;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "http://sourceforge.net/projects/ptm";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(334, 129);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About PTM";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(linkLabel.Text);
		}
	}
}