using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using PTM.Framework.Helpers;

namespace PTM.View.Forms
{
	/// <summary>
	/// Descripción breve de ConfigurationForm.
	/// </summary>
	internal class ConfigurationForm : Form
	{
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Label label1;
		private Label label3;
		private Label label4;
		private Button btnOk;
		private Button btnCancel;
		private Button btnReset;
		private TrackBar trackBar;
		private NumericUpDown numericUpDown;
		private CheckBox checkBox;
		private Label durationLabel;
		private CheckBox checkBox1;

		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private Container components = null;

		internal ConfigurationForm()
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

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new GroupBox();
			this.label4 = new Label();
			this.durationLabel = new Label();
			this.trackBar = new TrackBar();
			this.label3 = new Label();
			this.checkBox = new CheckBox();
			this.groupBox2 = new GroupBox();
			this.label1 = new Label();
			this.numericUpDown = new NumericUpDown();
			this.btnReset = new Button();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			this.checkBox1 = new CheckBox();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize) (this.trackBar)).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize) (this.numericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.durationLabel);
			this.groupBox1.Controls.Add(this.trackBar);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.FlatStyle = FlatStyle.System;
			this.groupBox1.Location = new Point(8, 112);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(240, 88);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Log entry duration";
			// 
			// label4
			// 
			this.label4.Location = new Point(200, 24);
			this.label4.Name = "label4";
			this.label4.Size = new Size(32, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Max";
			// 
			// durationLabel
			// 
			this.durationLabel.Location = new Point(80, 56);
			this.durationLabel.Name = "durationLabel";
			this.durationLabel.Size = new Size(64, 23);
			this.durationLabel.TabIndex = 1;
			this.durationLabel.Text = "10 mins.";
			// 
			// trackBar
			// 
			this.trackBar.Location = new Point(40, 16);
			this.trackBar.Maximum = 30;
			this.trackBar.Minimum = 5;
			this.trackBar.Name = "trackBar";
			this.trackBar.Size = new Size(152, 45);
			this.trackBar.SmallChange = 5;
			this.trackBar.TabIndex = 0;
			this.trackBar.TickFrequency = 5;
			this.trackBar.Value = 10;
			this.trackBar.Scroll += new EventHandler(this.trackBar1_Scroll);
			// 
			// label3
			// 
			this.label3.Location = new Point(8, 24);
			this.label3.Name = "label3";
			this.label3.Size = new Size(24, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "Min";
			// 
			// checkBox
			// 
			this.checkBox.FlatStyle = FlatStyle.System;
			this.checkBox.Location = new Point(8, 48);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new Size(152, 24);
			this.checkBox.TabIndex = 1;
			this.checkBox.Text = "Run at Windows Startup";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.numericUpDown);
			this.groupBox2.FlatStyle = FlatStyle.System;
			this.groupBox2.Location = new Point(8, 208);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(240, 56);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Database maintenance";
			// 
			// label1
			// 
			this.label1.Location = new Point(48, 24);
			this.label1.Name = "label1";
			this.label1.Size = new Size(184, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "days. Compact data older than this.";
			// 
			// numericUpDown
			// 
			this.numericUpDown.Location = new Point(8, 24);
			this.numericUpDown.Maximum = new Decimal(new int[]
			                                         	{
			                                         		30,
			                                         		0,
			                                         		0,
			                                         		0
			                                         	});
			this.numericUpDown.Minimum = new Decimal(new int[]
			                                         	{
			                                         		1,
			                                         		0,
			                                         		0,
			                                         		0
			                                         	});
			this.numericUpDown.Name = "numericUpDown";
			this.numericUpDown.Size = new Size(40, 20);
			this.numericUpDown.TabIndex = 0;
			this.numericUpDown.Value = new Decimal(new int[]
			                                       	{
			                                       		15,
			                                       		0,
			                                       		0,
			                                       		0
			                                       	});
			// 
			// btnReset
			// 
			this.btnReset.FlatStyle = FlatStyle.System;
			this.btnReset.Location = new Point(8, 16);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new Size(96, 23);
			this.btnReset.TabIndex = 3;
			this.btnReset.Text = "Reset defaults";
			this.btnReset.Click += new EventHandler(this.btnReset_Click);
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = DialogResult.OK;
			this.btnOk.FlatStyle = FlatStyle.System;
			this.btnOk.Location = new Point(80, 280);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.FlatStyle = FlatStyle.System;
			this.btnCancel.Location = new Point(168, 280);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.FlatStyle = FlatStyle.System;
			this.checkBox1.Location = new Point(8, 80);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(216, 24);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "Check for newer versions automatically";
			// 
			// ConfigurationForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new Size(256, 312);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.checkBox);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigurationForm";
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Configuration";
			this.Load += new EventHandler(this.ConfigurationForm_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize) (this.trackBar)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize) (this.numericUpDown)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			this.durationLabel.Text = this.trackBar.Value.ToString() + " mins.";
		}

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			LoadStartUpStatup();
			Configuration c = ConfigurationHelper.GetConfiguration(ConfigurationKey.DataMaintenanceDays);
			this.numericUpDown.Value = Convert.ToInt32(c.Value);
			c = ConfigurationHelper.GetConfiguration(ConfigurationKey.TasksLogDuration);
			this.trackBar.Value = Convert.ToInt32(c.Value);
			this.durationLabel.Text = this.trackBar.Value.ToString() + " mins.";

			c = ConfigurationHelper.GetConfiguration(ConfigurationKey.CheckForUpdates);
			if (Convert.ToInt32(c.Value) == 1)
				this.checkBox1.Checked = true;
			else
				this.checkBox1.Checked = false;
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			this.checkBox.Checked = true;
			this.checkBox1.Checked = true;
			this.trackBar.Value = 10;
			this.numericUpDown.Value = 7;
			this.durationLabel.Text = this.trackBar.Value.ToString() + " mins.";
		}

		private void LoadStartUpStatup()
		{
			try
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
			}
			catch
			{
				this.checkBox.Checked = false;
			}
		} //LoadStartUpStatup

		private static void SetWindowsStartUp(bool enable)
		{
			try
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
			}
			catch
			{
				
			}
		} //SetWindowsStartUp

		private void btnOk_Click(object sender, EventArgs e)
		{
			SetWindowsStartUp(this.checkBox.Checked);
			Configuration c = new Configuration(ConfigurationKey.DataMaintenanceDays, Convert.ToInt32(this.numericUpDown.Value));
			ConfigurationHelper.SaveConfiguration(c);
			c = new Configuration(ConfigurationKey.TasksLogDuration, Convert.ToInt32(this.trackBar.Value));
			ConfigurationHelper.SaveConfiguration(c);
			if (this.checkBox1.Checked)
				c = new Configuration(ConfigurationKey.CheckForUpdates, 1);
			else
				c = new Configuration(ConfigurationKey.CheckForUpdates, 0);
			ConfigurationHelper.SaveConfiguration(c);
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}