namespace PTM.Addin.Charts
{
    partial class DataCharts
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
            this.components = new System.ComponentModel.Container();
            this.zg = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zg
            // 
            this.zg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zg.Location = new System.Drawing.Point(3, 79);
            this.zg.Name = "zg";
            this.zg.ScrollGrace = 0;
            this.zg.ScrollMaxX = 0;
            this.zg.ScrollMaxY = 0;
            this.zg.ScrollMaxY2 = 0;
            this.zg.ScrollMinX = 0;
            this.zg.ScrollMinY = 0;
            this.zg.ScrollMinY2 = 0;
            this.zg.Size = new System.Drawing.Size(343, 296);
            this.zg.TabIndex = 0;
            // 
            // DataCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zg);
            this.Name = "DataCharts";
            this.Size = new System.Drawing.Size(349, 378);
            this.Load += new System.EventHandler(this.DataCharts_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zg;
    }
}
