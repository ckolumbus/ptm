using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace PTM.Addin
{
	/// <summary>
	/// Summary description for TabPageAddin.
	/// </summary>
	public class TabPageAddin : UserControl
	{
		/// <summary> TabPageAddin
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TabPageAddin()
		{
			// This call is required by the Windows.Forms Form Designer.
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TabPageAddin
			// 
			this.Name = "TabPageAddin";
			this.Size = new System.Drawing.Size(296, 280);
		}
		#endregion
		
	}
}
