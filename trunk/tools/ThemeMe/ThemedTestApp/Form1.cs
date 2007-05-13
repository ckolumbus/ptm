using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ThemedTestApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.TreeView treeView1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private System.Windows.Forms.MonthCalendar monthCalendar1;
    private System.Windows.Forms.HScrollBar hScrollBar1;
    private System.Windows.Forms.VScrollBar vScrollBar1;
    private System.Windows.Forms.TrackBar trackBar1;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.ToolBar toolBar1;
    private System.Windows.Forms.ToolBarButton toolBarButton1;
    private System.Windows.Forms.ToolBarButton toolBarButton2;
    private System.Windows.Forms.StatusBar statusBar1;
    private System.Windows.Forms.StatusBarPanel statusBarPanel1;
    private System.Windows.Forms.StatusBarPanel statusBarPanel2;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.CheckedListBox checkedListBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.listView1 = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
      this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
      this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
      this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.toolBar1 = new System.Windows.Forms.ToolBar();
      this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
      this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
      this.statusBar1 = new System.Windows.Forms.StatusBar();
      this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
      this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
      this.groupBox1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.button1.Location = new System.Drawing.Point(8, 48);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(80, 24);
      this.button1.TabIndex = 0;
      this.button1.Text = "button1";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(104, 48);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(168, 20);
      this.textBox1.TabIndex = 1;
      this.textBox1.Text = "textBox1";
      // 
      // checkBox1
      // 
      this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.checkBox1.Location = new System.Drawing.Point(280, 48);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.TabIndex = 2;
      this.checkBox1.Text = "checkBox1";
      // 
      // radioButton1
      // 
      this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.radioButton1.Location = new System.Drawing.Point(400, 48);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.TabIndex = 3;
      this.radioButton1.Text = "radioButton1";
      // 
      // radioButton2
      // 
      this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.radioButton2.Location = new System.Drawing.Point(400, 72);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.TabIndex = 4;
      this.radioButton2.Text = "radioButton2";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.listBox1});
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBox1.Location = new System.Drawing.Point(192, 104);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(240, 152);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "groupBox1";
      // 
      // listView1
      // 
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                this.columnHeader1,
                                                                                this.columnHeader2});
      this.listView1.Location = new System.Drawing.Point(16, 104);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(160, 144);
      this.listView1.TabIndex = 0;
      this.listView1.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Column 1";
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Column 2";
      // 
      // treeView1
      // 
      this.treeView1.ImageIndex = -1;
      this.treeView1.Location = new System.Drawing.Point(448, 104);
      this.treeView1.Name = "treeView1";
      this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
                                                                          new System.Windows.Forms.TreeNode("Root", new System.Windows.Forms.TreeNode[] {
                                                                                                                                                          new System.Windows.Forms.TreeNode("Child 1", new System.Windows.Forms.TreeNode[] {
                                                                                                                                                                                                                                             new System.Windows.Forms.TreeNode("Child 1:1"),
                                                                                                                                                                                                                                             new System.Windows.Forms.TreeNode("Child 1:2"),
                                                                                                                                                                                                                                             new System.Windows.Forms.TreeNode("Child 1:3")}),
                                                                                                                                                          new System.Windows.Forms.TreeNode("Child 2", new System.Windows.Forms.TreeNode[] {
                                                                                                                                                                                                                                             new System.Windows.Forms.TreeNode("Child 2:1"),
                                                                                                                                                                                                                                             new System.Windows.Forms.TreeNode("Child 2:2")})})});
      this.treeView1.SelectedImageIndex = -1;
      this.treeView1.Size = new System.Drawing.Size(216, 88);
      this.treeView1.TabIndex = 6;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                              this.tabPage1,
                                                                              this.tabPage2});
      this.tabControl1.Location = new System.Drawing.Point(16, 280);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(168, 176);
      this.tabControl1.TabIndex = 7;
      // 
      // tabPage1
      // 
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Size = new System.Drawing.Size(160, 150);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Tab 1";
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Size = new System.Drawing.Size(160, 46);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Tab 2";
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.Location = new System.Drawing.Point(200, 304);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(224, 20);
      this.dateTimePicker1.TabIndex = 8;
      // 
      // monthCalendar1
      // 
      this.monthCalendar1.Location = new System.Drawing.Point(456, 280);
      this.monthCalendar1.Name = "monthCalendar1";
      this.monthCalendar1.TabIndex = 9;
      // 
      // hScrollBar1
      // 
      this.hScrollBar1.Location = new System.Drawing.Point(200, 344);
      this.hScrollBar1.Name = "hScrollBar1";
      this.hScrollBar1.Size = new System.Drawing.Size(224, 17);
      this.hScrollBar1.TabIndex = 10;
      // 
      // vScrollBar1
      // 
      this.vScrollBar1.Location = new System.Drawing.Point(432, 280);
      this.vScrollBar1.Name = "vScrollBar1";
      this.vScrollBar1.Size = new System.Drawing.Size(17, 168);
      this.vScrollBar1.TabIndex = 11;
      // 
      // trackBar1
      // 
      this.trackBar1.Location = new System.Drawing.Point(200, 368);
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new System.Drawing.Size(216, 45);
      this.trackBar1.TabIndex = 12;
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(200, 424);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(216, 23);
      this.progressBar1.TabIndex = 13;
      this.progressBar1.Value = 50;
      // 
      // toolBar1
      // 
      this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                this.toolBarButton1,
                                                                                this.toolBarButton2});
      this.toolBar1.DropDownArrows = true;
      this.toolBar1.Name = "toolBar1";
      this.toolBar1.ShowToolTips = true;
      this.toolBar1.Size = new System.Drawing.Size(680, 39);
      this.toolBar1.TabIndex = 16;
      // 
      // toolBarButton1
      // 
      this.toolBarButton1.Text = "Button 1";
      // 
      // toolBarButton2
      // 
      this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
      this.toolBarButton2.Text = "Button 2";
      // 
      // statusBar1
      // 
      this.statusBar1.Location = new System.Drawing.Point(0, 464);
      this.statusBar1.Name = "statusBar1";
      this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                  this.statusBarPanel1,
                                                                                  this.statusBarPanel2});
      this.statusBar1.ShowPanels = true;
      this.statusBar1.Size = new System.Drawing.Size(680, 22);
      this.statusBar1.TabIndex = 17;
      this.statusBar1.Text = "statusBar1";
      // 
      // statusBarPanel1
      // 
      this.statusBarPanel1.Text = "Panel 1";
      // 
      // statusBarPanel2
      // 
      this.statusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
      this.statusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
      this.statusBarPanel2.Text = "Panel 2";
      // 
      // comboBox1
      // 
      this.comboBox1.Items.AddRange(new object[] {
                                                   "Row 1",
                                                   "Row 2",
                                                   "Row 3"});
      this.comboBox1.Location = new System.Drawing.Point(512, 48);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(152, 21);
      this.comboBox1.TabIndex = 18;
      this.comboBox1.Text = "comboBox1";
      // 
      // listBox1
      // 
      this.listBox1.Items.AddRange(new object[] {
                                                  "Row 1",
                                                  "Row 2",
                                                  "Row 3"});
      this.listBox1.Location = new System.Drawing.Point(16, 24);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(208, 108);
      this.listBox1.TabIndex = 0;
      // 
      // checkedListBox1
      // 
      this.checkedListBox1.Items.AddRange(new object[] {
                                                         "Row 1",
                                                         "Row 2",
                                                         "Row 3"});
      this.checkedListBox1.Location = new System.Drawing.Point(448, 208);
      this.checkedListBox1.Name = "checkedListBox1";
      this.checkedListBox1.Size = new System.Drawing.Size(216, 64);
      this.checkedListBox1.TabIndex = 19;
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(680, 486);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.checkedListBox1,
                                                                  this.comboBox1,
                                                                  this.statusBar1,
                                                                  this.toolBar1,
                                                                  this.progressBar1,
                                                                  this.trackBar1,
                                                                  this.vScrollBar1,
                                                                  this.hScrollBar1,
                                                                  this.monthCalendar1,
                                                                  this.dateTimePicker1,
                                                                  this.tabControl1,
                                                                  this.treeView1,
                                                                  this.groupBox1,
                                                                  this.radioButton2,
                                                                  this.radioButton1,
                                                                  this.checkBox1,
                                                                  this.textBox1,
                                                                  this.button1,
                                                                  this.listView1});
      this.Name = "Form1";
      this.Text = "Themed controls";
      this.groupBox1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

	}
}
