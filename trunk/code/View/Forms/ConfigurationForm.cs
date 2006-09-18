using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using PTM.Business.Helpers;

namespace PTM.View.Forms
{
	/// <summary>
	/// Descripción breve de ConfigurationForm.
	/// </summary>
	public class ConfigurationForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.TrackBar trackBar;
		private System.Windows.Forms.NumericUpDown numericUpDown;
		private System.Windows.Forms.CheckBox checkBox;
		private System.Windows.Forms.Label durationLabel;
		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConfigurationForm()
		{
			//
			// Necesario para admitir el Diseñador de Windows Forms
			//
			InitializeComponent();

			//
			// TODO: agregar código de constructor después de llamar a InitializeComponent
			//
		}

		/// <summary>
		/// Limpiar los recursos que se estén utilizando.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Código generado por el Diseñador de Windows Forms
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.durationLabel = new System.Windows.Forms.Label();
			this.trackBar = new System.Windows.Forms.TrackBar();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBox = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.durationLabel);
			this.groupBox1.Controls.Add(this.trackBar);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(240, 88);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Log entry duration";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(200, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Max";
			// 
			// durationLabel
			// 
			this.durationLabel.Location = new System.Drawing.Point(80, 56);
			this.durationLabel.Name = "durationLabel";
			this.durationLabel.Size = new System.Drawing.Size(64, 23);
			this.durationLabel.TabIndex = 1;
			this.durationLabel.Text = "10 mins.";
			// 
			// trackBar
			// 
			this.trackBar.Location = new System.Drawing.Point(40, 16);
			this.trackBar.Maximum = 30;
			this.trackBar.Minimum = 5;
			this.trackBar.Name = "trackBar";
			this.trackBar.Size = new System.Drawing.Size(152, 45);
			this.trackBar.SmallChange = 5;
			this.trackBar.TabIndex = 0;
			this.trackBar.TickFrequency = 5;
			this.trackBar.Value = 10;
			this.trackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "Min";
			// 
			// checkBox
			// 
			this.checkBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox.Location = new System.Drawing.Point(8, 48);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new System.Drawing.Size(152, 24);
			this.checkBox.TabIndex = 1;
			this.checkBox.Text = "Run at Windows Startup";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.numericUpDown);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(8, 184);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(240, 56);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Database maintenance";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(48, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "days. Compact data older than this.";
			// 
			// numericUpDown
			// 
			this.numericUpDown.Location = new System.Drawing.Point(8, 24);
			this.numericUpDown.Maximum = new System.Decimal(new int[] {
																		  30,
																		  0,
																		  0,
																		  0});
			this.numericUpDown.Minimum = new System.Decimal(new int[] {
																		  1,
																		  0,
																		  0,
																		  0});
			this.numericUpDown.Name = "numericUpDown";
			this.numericUpDown.Size = new System.Drawing.Size(40, 20);
			this.numericUpDown.TabIndex = 0;
			this.numericUpDown.Value = new System.Decimal(new int[] {
																		15,
																		0,
																		0,
																		0});
			// 
			// btnReset
			// 
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnReset.Location = new System.Drawing.Point(8, 16);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(96, 23);
			this.btnReset.TabIndex = 3;
			this.btnReset.Text = "Reset defaults";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(80, 256);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(168, 256);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// ConfigurationForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(256, 291);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.checkBox);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ConfigurationForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Configuration";
			this.Load += new System.EventHandler(this.ConfigurationForm_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			this.durationLabel.Text = this.trackBar.Value.ToString() + " mins.";
		}

		private void ConfigurationForm_Load(object sender, System.EventArgs e)
		{
			LoadStartUpStatup();
			Configuration c = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);		
			this.numericUpDown.Value = Convert.ToInt32(c.Value);
			c = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			this.trackBar.Value = Convert.ToInt32(c.Value);
			this.durationLabel.Text = this.trackBar.Value.ToString() + " mins.";
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			this.checkBox.Checked = true;
			this.trackBar.Value = 10;
			this.numericUpDown.Value = 7;
			this.durationLabel.Text = this.trackBar.Value.ToString() + " mins.";
		}
		
		private void LoadStartUpStatup()
		{
			RegistryKey reg = Registry.CurrentUser.
				OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			if (reg.GetValue("PTM") == null)
			{
				this.checkBox.Checked = false;
			}
			else
			{
				this.checkBox.Checked = true;
			} //if-else
			reg.Close();
		} //LoadStartUpStatup
		
		private static void SetWindowsStartUp(bool enable)
		{
			RegistryKey reg = Registry.CurrentUser.OpenSubKey(
				"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			if (enable)
			{
				reg.SetValue("PTM", Assembly.GetExecutingAssembly().Location);
			}
			else
			{
				reg.DeleteValue("PTM", false);
			} //if-else

			reg.Close();
		}//SetWindowsStartUp

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			SetWindowsStartUp(this.checkBox.Checked);
			Configuration c = new Configuration(ConfigurationKey.DataMaintenanceDays, Convert.ToInt32(this.numericUpDown.Value));
			ConfigurationHelper.SaveConfiguration(c);
			c = new Configuration(ConfigurationKey.TasksLogDuration, Convert.ToInt32(this.trackBar.Value));
			ConfigurationHelper.SaveConfiguration(c);
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		} 
		
		
	}
}
