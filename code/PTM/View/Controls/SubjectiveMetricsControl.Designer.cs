namespace PTM.View.Controls
{
    partial class SubjectiveMetricsControl
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
            PTM.View.Controls.TreeListViewComponents.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new PTM.View.Controls.TreeListViewComponents.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            this.label1 = new System.Windows.Forms.Label();
            this.logDate = new System.Windows.Forms.DateTimePicker();
            this.taskList = new PTM.View.Controls.TreeListViewComponents.TreeListView();
            this.TaskDescriptionHeader = new System.Windows.Forms.ColumnHeader();
            this.DurationTaskHeader = new System.Windows.Forms.ColumnHeader();
            this.StartTimeHeader = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // logDate
            // 
            this.logDate.Location = new System.Drawing.Point(48, 7);
            this.logDate.Name = "logDate";
            this.logDate.Size = new System.Drawing.Size(248, 20);
            this.logDate.TabIndex = 7;
            // 
            // taskList
            // 
            this.taskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.taskList.AutoArrange = false;
            this.taskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskDescriptionHeader,
            this.DurationTaskHeader,
            this.StartTimeHeader});
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.None;
            this.taskList.Comparer = treeListViewItemCollectionComparer1;
            this.taskList.HideSelection = false;
            this.taskList.Location = new System.Drawing.Point(8, 169);
            this.taskList.Name = "taskList";
            this.taskList.Size = new System.Drawing.Size(376, 102);
            this.taskList.Sorting = System.Windows.Forms.SortOrder.None;
            this.taskList.TabIndex = 9;
            this.taskList.UseCompatibleStateImageBehavior = false;
            // 
            // TaskDescriptionHeader
            // 
            this.TaskDescriptionHeader.Text = "Task Description";
            this.TaskDescriptionHeader.Width = 226;
            // 
            // DurationTaskHeader
            // 
            this.DurationTaskHeader.Text = "Duration";
            this.DurationTaskHeader.Width = 65;
            // 
            // StartTimeHeader
            // 
            this.StartTimeHeader.Text = "Start Time";
            this.StartTimeHeader.Width = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.trackBar3);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Location = new System.Drawing.Point(8, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 94);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(63, 10);
            this.trackBar1.Maximum = 4;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(115, 42);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Energy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Motivation";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(82, 278);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(302, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // trackBar2
            // 
            this.trackBar2.LargeChange = 1;
            this.trackBar2.Location = new System.Drawing.Point(63, 46);
            this.trackBar2.Maximum = 4;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(115, 42);
            this.trackBar2.TabIndex = 4;
            this.trackBar2.Value = 2;
            // 
            // trackBar3
            // 
            this.trackBar3.LargeChange = 1;
            this.trackBar3.Location = new System.Drawing.Point(255, 10);
            this.trackBar3.Maximum = 4;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(115, 42);
            this.trackBar3.TabIndex = 5;
            this.trackBar3.Value = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Social adaptation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Day Overall";
            // 
            // SubjectiveMetricsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.taskList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logDate);
            this.Name = "SubjectiveMetricsControl";
            this.Size = new System.Drawing.Size(392, 312);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker logDate;
        private PTM.View.Controls.TreeListViewComponents.TreeListView taskList;
        private System.Windows.Forms.ColumnHeader TaskDescriptionHeader;
        private System.Windows.Forms.ColumnHeader DurationTaskHeader;
        private System.Windows.Forms.ColumnHeader StartTimeHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label label5;
    }
}
