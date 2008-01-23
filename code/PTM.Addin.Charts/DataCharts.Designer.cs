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
            this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.generateButton = new System.Windows.Forms.Button();
            this.chartComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // zg
            // 
            this.zg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zg.Location = new System.Drawing.Point(3, 61);
            this.zg.Name = "zg";
            this.zg.ScrollGrace = 0;
            this.zg.ScrollMaxX = 0;
            this.zg.ScrollMaxY = 0;
            this.zg.ScrollMaxY2 = 0;
            this.zg.ScrollMinX = 0;
            this.zg.ScrollMinY = 0;
            this.zg.ScrollMinY2 = 0;
            this.zg.Size = new System.Drawing.Size(402, 342);
            this.zg.TabIndex = 0;
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.CustomFormat = "";
            this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toDateTimePicker.Location = new System.Drawing.Point(222, 32);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.Size = new System.Drawing.Size(88, 20);
            this.toDateTimePicker.TabIndex = 29;
            this.toDateTimePicker.Value = new System.DateTime(2006, 10, 2, 0, 0, 0, 0);
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.CustomFormat = "";
            this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fromDateTimePicker.Location = new System.Drawing.Point(78, 32);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(88, 20);
            this.fromDateTimePicker.TabIndex = 26;
            this.fromDateTimePicker.Value = new System.DateTime(2006, 10, 2, 0, 0, 0, 0);
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(318, 32);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 23);
            this.generateButton.TabIndex = 30;
            this.generateButton.Text = "Generate";
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // chartComboBox
            // 
            this.chartComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chartComboBox.Items.AddRange(new object[] {
            "- Select a chart -",
            "Hrs. worked vs. Day",
            "Hrs. worked vs. Week",
            "Hrs. worked vs. Month",
            "%Active-Inactive vs. Day",
            "%Active-Inactive vs. Week",
            "%Active-Inactive vs. Month"});
            this.chartComboBox.Location = new System.Drawing.Point(78, 4);
            this.chartComboBox.MaxLength = 50;
            this.chartComboBox.Name = "chartComboBox";
            this.chartComboBox.Size = new System.Drawing.Size(232, 21);
            this.chartComboBox.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 23);
            this.label2.TabIndex = 25;
            this.label2.Text = "Chart:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 33;
            this.label3.Text = "From:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(175, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 23);
            this.label4.TabIndex = 34;
            this.label4.Text = "To:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DataCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toDateTimePicker);
            this.Controls.Add(this.fromDateTimePicker);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.chartComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.zg);
            this.Name = "DataCharts";
            this.Size = new System.Drawing.Size(408, 406);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zg;
        private System.Windows.Forms.DateTimePicker toDateTimePicker;
        private System.Windows.Forms.DateTimePicker fromDateTimePicker;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.ComboBox chartComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
