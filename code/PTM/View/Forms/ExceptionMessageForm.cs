using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using PTM.Framework.Helpers;

namespace PTM.View.Forms
{
    public partial class ExceptionMessageForm : Form
    {
        public ExceptionMessageForm(Exception ex)
        {
            InitializeComponent();
            string nl = "\r\n";
            StringBuilder b = new StringBuilder();
            b.Append("Version: " + ConfigurationHelper.GetVersionString() + nl);
            b.Append("Internal Version: " + ConfigurationHelper.GetInternalVersionString() + nl);
            b.Append("Message: " + ex.Message + nl);
            b.Append("Stack Trace: " + ex.StackTrace + nl);
            if(ex.InnerException!=null)
            {
                b.Append("Inner Message: " + ex.InnerException.Message + nl);
                b.Append("Inner Stack Trace: " + ex.InnerException.StackTrace);
            }
            this.textBox.Text = b.ToString();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}