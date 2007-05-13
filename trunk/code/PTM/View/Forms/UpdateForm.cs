using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using PTM.Framework.Helpers;

namespace PTM.View.Forms
{
	/// <summary>
	/// Descripción breve de UpdateForm.
	/// </summary>
	internal class UpdateForm : Form
	{
		private Label label1;
		private LinkLabel linkLabel;
		private Button btnOk;

		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private Container components = null;

		internal UpdateForm(UpdaterHelper.UpdateInfo info)
		{
			//
			// Necesario para admitir el Diseñador de Windows Forms
			//
			InitializeComponent();

			this.label1.Text = this.label1.Text.Replace("{0}", info.ThisVersion).Replace("{1}", info.CurrentVersion);
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
			ResourceManager resources = new ResourceManager(typeof (UpdateForm));
			this.btnOk = new Button();
			this.label1 = new Label();
			this.linkLabel = new LinkLabel();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = FlatStyle.System;
			this.btnOk.Location = new Point(272, 56);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			// 
			// label1
			// 
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new Size(344, 32);
			this.label1.TabIndex = 2;
			this.label1.Text = "A new version is available, this version is {0} and the new version is {1}.";
			// 
			// linkLabel
			// 
			this.linkLabel.Cursor = Cursors.Hand;
			this.linkLabel.FlatStyle = FlatStyle.System;
			this.linkLabel.Location = new Point(8, 40);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new Size(176, 23);
			this.linkLabel.TabIndex = 9;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "http://sourceforge.net/projects/ptm";
			this.linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// UpdateForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new Size(5, 13);
			this.ClientSize = new Size(360, 88);
			this.Controls.Add(this.linkLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Icon = ((Icon) (resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateForm";
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Update available";
			this.TopMost = true;
			this.ResumeLayout(false);
		}

		#endregion

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(linkLabel.Text);
		}
	}
}