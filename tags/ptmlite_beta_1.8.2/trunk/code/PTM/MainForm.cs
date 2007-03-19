using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using PTM.Addin;
using PTM.Data;
using PTM.Framework;
using PTM.Framework.Helpers;
using PTM.View;
using PTM.View.Controls;
using PTM.View.Forms;

namespace PTM
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	internal class MainForm : Form
	{
		private MainMenu mainMenu;
		private TabControl tabControl;
		private MenuItem menuItem1;
		private TabPage tasksPage;
		private MenuItem exitMenuItem;
		private TabPage summaryPage;
		private StatusBar statusBar;
		//private TabPage statisticsPage;
		private TasksLogControl tasksLogControl;
		private SummaryControl summaryControl;
		//private StatisticsControl statisticsControl;
		private MenuItem menuItem2;
		private MenuItem aboutMenuItem;
		private MenuItem menuItem4;
		private IContainer components;
		private bool systemShutdown = false;
		private MenuItem menuItem5;
		private MenuItem menuItem6;
		private MenuItem menuItem3;
		private MenuItem menuItem7;
		private bool AnimationDisabled = false;

		internal MainForm()
		{
			InitializeComponent();
			Application.DoEvents();
			InitializeTabPages();
			base.Text += " " + ConfigurationHelper.GetVersionString();
			Application.DoEvents();
			UpdateStartUpPath();
			Application.DoEvents();

			LoadAddins();
			Application.DoEvents();
		}

		private void InitializeTabPages()
		{
			this.tabControl.SuspendLayout();
			this.tasksPage.SuspendLayout();
			this.summaryPage.SuspendLayout();
			//this.statisticsPage.SuspendLayout();
			this.SuspendLayout();
			this.tasksLogControl = new TasksLogControl();
			this.summaryControl = new SummaryControl();
			//this.statisticsControl = new StatisticsControl();

			this.tasksPage.Controls.Add(this.tasksLogControl);
			// 
			// tasksLogControl
			// 
			this.tasksLogControl.BackColor = SystemColors.Control;
			this.tasksLogControl.Dock = DockStyle.Fill;
			this.tasksLogControl.Location = new Point(0, 0);
			this.tasksLogControl.Name = "tasksLogControl";
			this.tasksLogControl.Size = new Size(408, 358);
			this.tasksLogControl.TabIndex = 0;
			this.tasksLogControl.StatusChanged += new AddinTabPage.StatusChangedDelegate(tabPage_StatusChanged);
			this.tasksLogControl.Exit += new EventHandler(Exit);

			this.summaryPage.Controls.Add(this.summaryControl);
			// 
			// summaryControl
			// 
			this.summaryControl.BackColor = SystemColors.Control;
			this.summaryControl.Dock = DockStyle.Fill;
			this.summaryControl.Location = new Point(0, 0);
			this.summaryControl.Name = "summaryControl";
			this.summaryControl.Size = new Size(408, 358);
			this.summaryControl.TabIndex = 0;
			this.summaryControl.StatusChanged += new AddinTabPage.StatusChangedDelegate(tabPage_StatusChanged);
/*
			this.statisticsPage.Controls.Add(this.statisticsControl);
			// 
			// statisticsControl
			// 
			this.statisticsControl.BackColor = SystemColors.Control;
			this.statisticsControl.Dock = DockStyle.Fill;
			this.statisticsControl.Location = new Point(0, 0);
			this.statisticsControl.Name = "statisticsControl";
			this.statisticsControl.Size = new Size(408, 358);
			this.statisticsControl.TabIndex = 0;
			this.statisticsControl.StatusChanged += new AddinTabPage.StatusChangedDelegate(tabPage_StatusChanged);
*/
			this.tabControl.ResumeLayout(false);
			this.tasksPage.ResumeLayout(false);
			this.summaryPage.ResumeLayout(false);
			//this.statisticsPage.ResumeLayout(false);
			this.ResumeLayout();
		}

//MainForm

		private void LoadAddins()
		{
			ArrayList list;
			list = AddinHelper.GetTabPageAddins();
			foreach (AddinTabPage addin in list)
			{
				TabPage tabPage = new TabPage(addin.Text);
				addin.Dock = DockStyle.Fill;
				tabPage.Controls.Add(addin);
				this.tabControl.Controls.Add(tabPage);
			}
		}


		private void UpdateStartUpPath()
		{
			RegistryKey reg = Registry.CurrentUser.
				OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			if (reg.GetValue("PTM") != null)
			{
				if (reg.GetValue("PTM").ToString() != Assembly.GetExecutingAssembly().Location) //update path
					reg.SetValue("PTM", Assembly.GetExecutingAssembly().Location);
			} //if-else
			reg.Close();
		} //UpdateStartUpPath


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
				} //foreach

//				this.notifyIcon.Visible = false;
//				this.notifyIcon.Icon.Dispose();
//				this.notifyIcon.Dispose();

				if (components != null)
				{
					components.Dispose();
				} //if
			} //if
			base.Dispose(disposing);
		} //Dispose

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (MainForm));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.aboutMenuItem = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tasksPage = new System.Windows.Forms.TabPage();
			this.summaryPage = new System.Windows.Forms.TabPage();
			//this.statisticsPage = new System.Windows.Forms.TabPage();
			this.tabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
			                                 	{
			                                 		this.menuItem1,
			                                 		this.menuItem4,
			                                 		this.menuItem2
			                                 	});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
			                                  	{
			                                  		this.exitMenuItem
			                                  	});
			this.menuItem1.Text = "File";
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Index = 0;
			this.exitMenuItem.Text = "&Exit PTM";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
			                                  	{
			                                  		this.menuItem7,
			                                  		this.menuItem3,
			                                  		this.menuItem5,
			                                  		this.menuItem6
			                                  	});
			this.menuItem4.Text = "Tools";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 0;
			this.menuItem7.Shortcut = System.Windows.Forms.Shortcut.F3;
			this.menuItem7.Text = "Tasks Explorer...";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "Configuration...";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 3;
			this.menuItem6.Text = "Add-in Manager...";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
			                                  	{
			                                  		this.aboutMenuItem
			                                  	});
			this.menuItem2.Text = "Help";
			// 
			// aboutMenuItem
			// 
			this.aboutMenuItem.Index = 0;
			this.aboutMenuItem.Text = "About PTM...";
			this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 405);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(432, 22);
			this.statusBar.TabIndex = 1;
			this.statusBar.Text = "Ready";
			// 
			// tabControl
			// 
			this.tabControl.Anchor =
				((System.Windows.Forms.AnchorStyles)
				 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				   | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tasksPage);
			this.tabControl.Controls.Add(this.summaryPage);
			//this.tabControl.Controls.Add(this.statisticsPage);
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
			this.tasksPage.Location = new System.Drawing.Point(4, 22);
			this.tasksPage.Name = "tasksPage";
			this.tasksPage.Size = new System.Drawing.Size(408, 358);
			this.tasksPage.TabIndex = 1;
			this.tasksPage.Text = "Tasks Log";
			// 
			// summaryPage
			// 
			this.summaryPage.Location = new System.Drawing.Point(4, 22);
			this.summaryPage.Name = "summaryPage";
			this.summaryPage.Size = new System.Drawing.Size(408, 358);
			this.summaryPage.TabIndex = 2;
			this.summaryPage.Text = "Summary";
			/*
			// 
			// statisticsPage
			// 
			this.statisticsPage.Location = new System.Drawing.Point(4, 22);
			this.statisticsPage.Name = "statisticsPage";
			this.statisticsPage.Size = new System.Drawing.Size(408, 358);
			this.statisticsPage.TabIndex = 3;
			this.statisticsPage.Text = "Statistics";
			*/
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(432, 427);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.statusBar);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(440, 456);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PTM ";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tabControl.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		#region MainForm

		private void MainForm_Load(object sender, EventArgs e)
		{
			if(this.tabControl.SelectedTab!=null && this.tabControl.SelectedTab.Controls.Count>0)
				this.statusBar.Text = ((AddinTabPage) this.tabControl.SelectedTab.Controls[0]).Status;
			//this.tasksLogControl.NewTaskLog(true);
			Logs.StartLogging();
		} //MainForm_Load

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			AddinTabPage pageAddinTabPage = ((AddinTabPage) tabControl.SelectedTab.Controls[0]);
			this.statusBar.Text = pageAddinTabPage.Status;
			pageAddinTabPage.OnTabPageSelected();
		} //tabControl_SelectedIndexChanged


		protected override void WndProc(ref Message m)
		{
			// Once the program recieves WM_QUERYENDSESSION message, set the boolean systemShutdown.

			if (m.Msg == ViewHelper.WM_QUERYENDSESSION)
			{
				systemShutdown = true;
			}

			base.WndProc(ref m);
		} //WndProc

		protected override void OnClosing(CancelEventArgs e)
		{
			try
			{
				if (systemShutdown)
				{
					e.Cancel = false;
				}
				else
				{
					e.Cancel = true;
					//this.AnimateWindow();
					this.Visible = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Exiting...", "Exception thrown",
				                MessageBoxButtons.OK, MessageBoxIcon.Error);
			} //try-cacth

			base.OnClosing(e);
		} //OnClosing

//		private void AnimateWindow()
//		{
//			// if the user has not disabled animating windows...
//			if (!this.AnimationDisabled)
//			{
//				ViewHelper.RECT animateFrom = new ViewHelper.RECT();
//				ViewHelper.GetWindowRect(this.Handle, ref animateFrom);
//
//				ViewHelper.RECT animateTo = new ViewHelper.RECT();
//				IntPtr notifyAreaHandle = ViewHelper.GetNotificationAreaHandle();
//
//				if (notifyAreaHandle != IntPtr.Zero)
//				{
//					if (ViewHelper.GetWindowRect(notifyAreaHandle, ref animateTo))
//					{
//						ViewHelper.DrawAnimatedRects(this.Handle, ViewHelper.IDANI_CAPTION, ref animateFrom, ref animateTo);
//					} //if
//				} //if
//			} //if
//		} //AnimateWindow

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
		} //Exit

		private static void Save()
		{
            Logs.UpdateCurrentLogDuration();
			//ApplicationsLog.UpdateCurrentApplicationsLog();
		} //Save

		private void exitMenuItem_Click(object sender, EventArgs e)
		{
			Exit(sender, e);
		} //exitMenuItem_Click

		private void aboutMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this);
		} //aboutMenuItem_Click

		private void menuItem5_Click(object sender, EventArgs e)
		{
			ConfigurationForm config = new ConfigurationForm();
			config.ShowDialog(this);
		}

		private void menuItem6_Click(object sender, EventArgs e)
		{
			AddinForm addinForm = new AddinForm();
			addinForm.ShowDialog(this);
		}

		private void menuItem7_Click(object sender, EventArgs e)
		{
			TasksHierarchyForm taskHForm = new TasksHierarchyForm();
			taskHForm.ShowDialog(this);
		}

		#endregion

		private void tabPage_StatusChanged(AddinTabPage.StatusChangedEventAtgs e)
		{
			this.statusBar.Text = e.Status;
		}
	} //MainForm
} //end of namespace