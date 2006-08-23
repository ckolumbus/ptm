using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;
using PTM.View;
using PTM.View.Controls;
using PTM.View.Forms;

namespace PTM
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : Form
	{
		private MainMenu mainMenu;
		private TabControl tabControl;
		private MenuItem menuItem1;
		private TabPage tasksPage;
		private MenuItem exitMenuItem;
		private TabPage summaryPage;
		private StatusBar statusBar;
		private StatusBarPanel statusBarPanel1;
		private StatusBarPanel statusBarPanel2;
		private StatusBarPanel statusBarPanel3;
		private TabPage statisticsPage;
		private TasksLogControl tasksLogControl;
		private SummaryControl summaryControl;
		private StatisticsControl statisticsControl;
		private MenuItem menuItem2;
		private MenuItem aboutMenuItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private IContainer components;

		public MainForm()
		{
			InitializeComponent();
			Application.DoEvents();
			this.Text += MainClass.GetVersionString();
			this.tasksLogControl.Exit+=new EventHandler(Exit);
			LoadIconsFromResources();
			Application.DoEvents();
		}

		private void LoadIconsFromResources()
		{
			ResourceManager resourceManager = new ResourceManager ("PTM.View.Controls.Icons", GetType().Assembly);
	
			Icon resIcon;
			int i = 1;
			do
			{
				resIcon = (Icon) resourceManager.GetObject("Icon" + i.ToString(CultureInfo.InvariantCulture));
				if(resIcon!=null)
				{
					IconsManager.AddIcon((i-1).ToString(CultureInfo.InvariantCulture), resIcon);
				}
				i++;
			}while(resIcon!=null);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (Image i in IconsManager.IconsList.Images)
				{
					i.Dispose();	
				}
				
//				this.notifyIcon.Visible = false;
//				this.notifyIcon.Icon.Dispose();
//				this.notifyIcon.Dispose();

				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.aboutMenuItem = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tasksPage = new System.Windows.Forms.TabPage();
			this.tasksLogControl = new PTM.View.Controls.TasksLogControl();
			this.summaryPage = new System.Windows.Forms.TabPage();
			this.summaryControl = new PTM.View.Controls.SummaryControl();
			this.statisticsPage = new System.Windows.Forms.TabPage();
			this.statisticsControl = new PTM.View.Controls.StatisticsControl();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tasksPage.SuspendLayout();
			this.summaryPage.SuspendLayout();
			this.statisticsPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											this.menuItem1,
																											this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											 this.menuItem3,
																											 this.exitMenuItem});
			this.menuItem1.Text = "File";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.F3;
			this.menuItem3.Text = "Explore &Tasks...";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Index = 1;
			this.exitMenuItem.Text = "&Exit People Task Manager";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											 this.aboutMenuItem});
			this.menuItem2.Text = "Help";
			// 
			// aboutMenuItem
			// 
			this.aboutMenuItem.Index = 0;
			this.aboutMenuItem.Text = "About People Task Manager...";
			this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 400);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																												 this.statusBarPanel1,
																												 this.statusBarPanel2,
																												 this.statusBarPanel3});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(432, 22);
			this.statusBar.TabIndex = 1;
			this.statusBar.Text = "Ready";
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.Text = "Ready";
			this.statusBarPanel1.Width = 140;
			// 
			// statusBarPanel2
			// 
			this.statusBarPanel2.Width = 140;
			// 
			// statusBarPanel3
			// 
			this.statusBarPanel3.Width = 136;
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tasksPage);
			this.tabControl.Controls.Add(this.summaryPage);
			this.tabControl.Controls.Add(this.statisticsPage);
			this.tabControl.HotTrack = true;
			this.tabControl.Location = new System.Drawing.Point(8, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(416, 384);
			this.tabControl.TabIndex = 2;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tasksPage
			// 
			this.tasksPage.Controls.Add(this.tasksLogControl);
			this.tasksPage.Location = new System.Drawing.Point(4, 22);
			this.tasksPage.Name = "tasksPage";
			this.tasksPage.Size = new System.Drawing.Size(408, 358);
			this.tasksPage.TabIndex = 1;
			this.tasksPage.Text = "Tasks Log";
			// 
			// tasksLogControl
			// 
			this.tasksLogControl.BackColor = System.Drawing.SystemColors.Control;
			this.tasksLogControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tasksLogControl.Location = new System.Drawing.Point(0, 0);
			this.tasksLogControl.Name = "tasksLogControl";
			this.tasksLogControl.Size = new System.Drawing.Size(408, 358);
			this.tasksLogControl.TabIndex = 0;
			// 
			// summaryPage
			// 
			this.summaryPage.Controls.Add(this.summaryControl);
			this.summaryPage.Location = new System.Drawing.Point(4, 22);
			this.summaryPage.Name = "summaryPage";
			this.summaryPage.Size = new System.Drawing.Size(408, 358);
			this.summaryPage.TabIndex = 2;
			this.summaryPage.Text = "Summary";
			// 
			// summaryControl
			// 
			this.summaryControl.BackColor = System.Drawing.SystemColors.Control;
			this.summaryControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.summaryControl.Location = new System.Drawing.Point(0, 0);
			this.summaryControl.Name = "summaryControl";
			this.summaryControl.Size = new System.Drawing.Size(408, 358);
			this.summaryControl.TabIndex = 0;
			// 
			// statisticsPage
			// 
			this.statisticsPage.Controls.Add(this.statisticsControl);
			this.statisticsPage.Location = new System.Drawing.Point(4, 22);
			this.statisticsPage.Name = "statisticsPage";
			this.statisticsPage.Size = new System.Drawing.Size(408, 358);
			this.statisticsPage.TabIndex = 3;
			this.statisticsPage.Text = "Statistics";
			// 
			// statisticsControl
			// 
			this.statisticsControl.BackColor = System.Drawing.SystemColors.Control;
			this.statisticsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statisticsControl.Location = new System.Drawing.Point(0, 0);
			this.statisticsControl.Name = "statisticsControl";
			this.statisticsControl.Size = new System.Drawing.Size(408, 358);
			this.statisticsControl.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(432, 422);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.statusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(440, 456);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "People Task Manager ";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tasksPage.ResumeLayout(false);
			this.summaryPage.ResumeLayout(false);
			this.statisticsPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			IntPtr mainWindowHandle = this.Handle;
			try
			{
				lock (this)
				{
					//Write the handle to the Shared Memory 
					MainClass.sharedMemory.WriteHandle(mainWindowHandle);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Exiting...", "Exception thrown",MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		protected override void WndProc(ref Message m)
		{
			// Once the program recieves WM_QUERYENDSESSION message, set the boolean systemShutdown.

			if (m.Msg == ViewHelper.WM_QUERYENDSESSION)
				MainClass.systemShutdown = true;
			base.WndProc(ref m);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			try
			{
				if (MainClass.systemShutdown == true)
					e.Cancel = false;
				else
				{
					e.Cancel = true;
					this.AnimateWindow();
					this.Visible = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Exiting...", "Exception thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			base.OnClosing(e);
		}

		private bool AnimationDisabled = false;
		private void AnimateWindow()
		{
			// if the user has not disabled animating windows...
			if (!this.AnimationDisabled)
			{
				ViewHelper.RECT animateFrom = new ViewHelper.RECT();
				ViewHelper.GetWindowRect(this.Handle, ref animateFrom);

				ViewHelper.RECT animateTo = new ViewHelper.RECT();
				IntPtr notifyAreaHandle = ViewHelper.GetNotificationAreaHandle();

				if (notifyAreaHandle != IntPtr.Zero)
				{
					if (ViewHelper.GetWindowRect(notifyAreaHandle, ref animateTo) == true)
					{
						ViewHelper.DrawAnimatedRects(this.Handle, ViewHelper.IDANI_CAPTION, ref animateFrom, ref animateTo);
					}
				}
			}
		}



		protected override void OnClosed(EventArgs e)
		{
//			this.notifyIcon.Visible = false;
//			this.notifyIcon.Icon.Dispose();
//			this.notifyIcon.Dispose();
			base.OnClosed(e);
		}
		#region MainForm

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.tasksLogControl.NewTaskLog(true);
			Logs.StartLogging();
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(tabControl.SelectedTab == this.summaryPage)
			{
				summaryControl.UpdateSummary();
			}
			if(tabControl.SelectedTab == this.statisticsPage)
			{
				statisticsControl.UpdateStatistics();
			}
		}



		#endregion

		#region Menu

		private void Exit(object sender, EventArgs e)
		{
			Logs.StopLogging();
			Application.DoEvents();
			this.Close();
			Save();
			Application.DoEvents();
			Application.Exit();
		}

		private static void Save()
		{
			UnitOfWork.Update();
		}
		
		
		#region MainMenu

		private void exitMenuItem_Click(object sender, EventArgs e)
		{
			Exit(sender, e);
		}

		#endregion

		private void aboutMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this);
		}
		
		
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			TasksHierarchyForm taskHForm = new TasksHierarchyForm();
			taskHForm.ShowDialog(this);
		}

		#endregion


	}
}