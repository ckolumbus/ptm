namespace PopupControl
{
    partial class VToolTip
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VToolTip));
            this.panelTitle = new System.Windows.Forms.Panel();
            this.labelTitleImage = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.labelFooter = new System.Windows.Forms.Label();
            this.labelFooterImage = new System.Windows.Forms.Label();
            this.labelFooterText = new System.Windows.Forms.Label();
            this.panelFooterImage = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.labelText = new System.Windows.Forms.Label();
            this.panelImage = new System.Windows.Forms.Panel();
            this.labelImage = new System.Windows.Forms.Label();
            this.labelLine = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelFooterImage.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.labelTitle);
            this.panelTitle.Controls.Add(this.labelTitleImage);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(2, 2);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(434, 24);
            this.panelTitle.TabIndex = 0;
            // 
            // labelTitleImage
            // 
            this.labelTitleImage.BackColor = System.Drawing.Color.Transparent;
            this.labelTitleImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTitleImage.Image = ((System.Drawing.Image)(resources.GetObject("labelTitleImage.Image")));
            this.labelTitleImage.Location = new System.Drawing.Point(0, 0);
            this.labelTitleImage.Name = "labelTitleImage";
            this.labelTitleImage.Size = new System.Drawing.Size(24, 24);
            this.labelTitleImage.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.Location = new System.Drawing.Point(24, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(410, 24);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Title of VToolTip";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.labelFooterText);
            this.panelFooter.Controls.Add(this.labelFooter);
            this.panelFooter.Controls.Add(this.panelFooterImage);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(2, 156);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(434, 44);
            this.panelFooter.TabIndex = 1;
            // 
            // labelFooter
            // 
            this.labelFooter.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelFooter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFooter.Location = new System.Drawing.Point(24, 0);
            this.labelFooter.Name = "labelFooter";
            this.labelFooter.Size = new System.Drawing.Size(410, 24);
            this.labelFooter.TabIndex = 3;
            this.labelFooter.Text = "Title of VToolTip";
            this.labelFooter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelFooterImage
            // 
            this.labelFooterImage.BackColor = System.Drawing.Color.Transparent;
            this.labelFooterImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelFooterImage.Image = ((System.Drawing.Image)(resources.GetObject("labelFooterImage.Image")));
            this.labelFooterImage.Location = new System.Drawing.Point(0, 0);
            this.labelFooterImage.Name = "labelFooterImage";
            this.labelFooterImage.Size = new System.Drawing.Size(24, 24);
            this.labelFooterImage.TabIndex = 2;
            // 
            // labelFooterText
            // 
            this.labelFooterText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFooterText.Location = new System.Drawing.Point(24, 24);
            this.labelFooterText.Name = "labelFooterText";
            this.labelFooterText.Size = new System.Drawing.Size(410, 20);
            this.labelFooterText.TabIndex = 4;
            this.labelFooterText.Text = "labelFooterText";
            // 
            // panelFooterImage
            // 
            this.panelFooterImage.Controls.Add(this.labelFooterImage);
            this.panelFooterImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFooterImage.Location = new System.Drawing.Point(0, 0);
            this.panelFooterImage.Name = "panelFooterImage";
            this.panelFooterImage.Size = new System.Drawing.Size(24, 44);
            this.panelFooterImage.TabIndex = 5;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.labelText);
            this.panelContent.Controls.Add(this.panelImage);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(2, 26);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(434, 128);
            this.panelContent.TabIndex = 2;
            // 
            // labelText
            // 
            this.labelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelText.Location = new System.Drawing.Point(104, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(330, 128);
            this.labelText.TabIndex = 1;
            this.labelText.Text = resources.GetString("labelText.Text");
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.labelImage);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelImage.Location = new System.Drawing.Point(0, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(104, 128);
            this.panelImage.TabIndex = 6;
            // 
            // labelImage
            // 
            this.labelImage.BackColor = System.Drawing.Color.Transparent;
            this.labelImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelImage.Image = ((System.Drawing.Image)(resources.GetObject("labelImage.Image")));
            this.labelImage.Location = new System.Drawing.Point(0, 0);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(104, 123);
            this.labelImage.TabIndex = 2;
            // 
            // labelLine
            // 
            this.labelLine.BackColor = System.Drawing.Color.Black;
            this.labelLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelLine.Location = new System.Drawing.Point(2, 154);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(434, 2);
            this.labelLine.TabIndex = 3;
            this.labelLine.Text = "labelLine";
            // 
            // VToolTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.labelLine);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelTitle);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Name = "VToolTip";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(438, 202);
            this.panelTitle.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelFooterImage.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelTitleImage;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label labelFooter;
        private System.Windows.Forms.Label labelFooterText;
        private System.Windows.Forms.Label labelFooterImage;
        private System.Windows.Forms.Panel panelFooterImage;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.Label labelImage;
        private System.Windows.Forms.Label labelLine;
    }
}
