using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PTM.View.Controls;

namespace HansBlomme.Windows.Forms
{
	[
		ToolboxBitmap(typeof (NotifyIcon), "HansBlomme.Windows.Forms.NotifyIcon.bmp"),
			ToolboxItemFilter("HansBlomme.Windows.Forms"), DefaultProperty("Text"), DefaultEvent("MouseDown")]
	public sealed class NotifyIcon : Component
	{
		[Category("Action"), Description("Occurs when the balloon is dismissed because of a mouse click.")]
		public event BalloonClickEventHandler BalloonClick;

		[
			Category("Behavior"),
				Description(
					"Occurs when the balloon disappears\u2014for example, when the icon is deleted. This message is not sent if the balloon is dismissed because of a timeout or a mouse click."
					)]
		public event BalloonHideEventHandler BalloonHide;

		[Category("Behavior"), Description("Occurs when the balloon is shown (balloons are queued).")]
		public event BalloonShowEventHandler BalloonShow;

		[Description("Occurs when the balloon is dismissed because of a timeout."), Category("Behavior")]
		public event BalloonTimeoutEventHandler BalloonTimeout;

		[Description("Occurs when the user clicks the icon in the status area."), Category("Action")]
		public event ClickEventHandler Click;

		[
			Description("Occurs when the user double-clicks the icon in the status notification area of the taskbar."),
				Category("Action")]
		public event DoubleClickEventHandler DoubleClick;

		[
			Description(
				"Occurs when the user presses the mouse button while the pointer is over the icon in the status notification area of the taskbar."
				), Category("Mouse")]
		public event MouseDownEventHandler MouseDown;

		[
			Description(
				"Occurs when the user moves the mouse while the pointer is over the icon in the status notification area of the taskbar."
				), Category("Mouse")]
		public event MouseMoveEventHandler MouseMove;

		[
			Description(
				"Occurs when the user releases the mouse button while the pointer is over the icon in the status notification area of the taskbar."
				), Category("Mouse")]
		public event MouseUpEventHandler MouseUp;

		//public event MenuItemRightClickEventHandler MenuItemRightClick;

		public NotifyIcon()
		{
			this.Messages = new MessageHandler();
			this.InitializeComponent();
			this.Initialize();
		}

		public NotifyIcon(IContainer Container) : this()
		{
			Container.Add(this);
			this.Initialize();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Visible = false;
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private void Initialize()
		{
			this.NID.hwnd = this.Messages.Handle;
			this.NID.cbSize = Marshal.SizeOf(typeof (NOTIFYICONDATA));
			this.NID.uFlags = 7;
			this.NID.uCallbackMessage = 0x401;
			this.NID.uVersion = 5;
			this.NID.szTip = "";
			this.NID.uID = 0;
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		private void Messages_BalloonClick(object sender)
		{
			if (!this.m_VisibleBeforeBalloon)
			{
				this.Visible = false;
			}
			if (this.BalloonClick != null)
			{
				this.BalloonClick(this);
			}
		}

		private void Messages_BalloonHide(object sender)
		{
			if (this.BalloonHide != null)
			{
				this.BalloonHide(this);
			}
		}

		private void Messages_BalloonShow(object sender)
		{
			if (this.BalloonShow != null)
			{
				this.BalloonShow(this);
			}
		}

		private void Messages_BalloonTimeout(object sender)
		{
			if (!this.m_VisibleBeforeBalloon)
			{
				this.Visible = false;
			}
			if (this.BalloonTimeout != null)
			{
				this.BalloonTimeout(this);
			}
		}

		private void Messages_Click(object sender, EventArgs e)
		{
			if (this.Click != null)
			{
				this.Click(this, e);
			}
		}

		private void Messages_DoubleClick(object sender, EventArgs e)
		{
			if (this.DoubleClick != null)
			{
				this.DoubleClick(this, e);
			}
		}

		private void Messages_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.MouseDown != null)
			{
				this.MouseDown(this, e);
			}
		}

		private void Messages_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.MouseMove != null)
			{
				this.MouseMove(this, e);
			}
		}

		private void Messages_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.MouseUp != null)
			{
				this.MouseUp(this, e);
			}
			if (e.Button == MouseButtons.Right)
			{
				this.Messages.Activate();
				Point point7 = new Point(0, 0);
				Point point6 = this.Messages.PointToScreen(point7);
				Point point4 = new Point(0, 0);
				Point point3 = this.Messages.PointToScreen(point4);
				Point point2 = new Point(Cursor.Position.X - point6.X, Cursor.Position.Y - point3.Y);
				Point point1 = point2;
				if (this.m_ContextMenu != null)
				{
					this.m_ContextMenu.Show(this.Messages, point1);
				}
			}
		}

//		private void _Messages_MenuItemRightClick(object sender, MenuItemRightClickEventArgs e)
//		{
////			if(this.MenuItemRightClick!=null)
////				MenuItemRightClick(sender, e);
//			if(this.ContextMenu!=null)
//			{
//				MenuItem item = this.ContextMenu.FindMenuItem(ContextMenu.FindHandle, e.MenuHandle);
//				TaskMenuItem taskItem = item.MenuItems[e.ItemIndex] as TaskMenuItem;
//				if(taskItem!=null)
//				{
//					taskItem.PerformPick();
//				}
//			}
//		}

		private void Messages_Reload()
		{
			if (this.Visible)
			{
				this.Visible = true;
			}
		}

		[DllImport("Shell32", CharSet=CharSet.Auto)]
		private static extern bool Shell_NotifyIcon(int dwMessage, ref NOTIFYICONDATA lpData);

		public void ShowBalloon(EBalloonIcon Icon, string Text, string Title, [Optional] int Timeout /* = 0x3a98 */)
		{
			this.m_VisibleBeforeBalloon = this.m_Visible;
			this.NID.uFlags |= 0x10;
			this.NID.uVersion = Timeout;
			this.NID.szInfo = Text;
			this.NID.szInfoTitle = Title;
			this.NID.dwInfoFlags = Convert.ToInt32((int) Icon);
			if (!this.Visible)
			{
				this.Visible = true;
			}
			else
			{
				Shell_NotifyIcon(1, ref this.NID);
			}
			this.NID.uFlags &= -17;
		}


		[DefaultValue(""), Description("The pop-up menu to show when the user right-clicks the icon."), Category("Behavior")]
		public ContextMenu ContextMenu
		{
			get { return this.m_ContextMenu; }
			set { this.m_ContextMenu = value; }
		}

		[Description("The icon to display in the system tray."), DefaultValue(""), Category("Appearance")]
		public Icon Icon
		{
			get { return this.m_Icon; }
			set
			{
				this.m_Icon = value;
				this.NID.uFlags |= 2;
				this.NID.hIcon = this.Icon.Handle;
				if (this.Visible)
				{
					Shell_NotifyIcon(1, ref this.NID);
				}
			}
		}

		private MessageHandler Messages
		{
			get { return this._Messages; }
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				if (this._Messages != null)
				{
					this._Messages.BalloonClick -= new MessageHandler.BalloonClickEventHandler(this.Messages_BalloonClick);
					this._Messages.BalloonTimeout -= new MessageHandler.BalloonTimeoutEventHandler(this.Messages_BalloonTimeout);
					this._Messages.BalloonHide -= new MessageHandler.BalloonHideEventHandler(this.Messages_BalloonHide);
					this._Messages.BalloonShow -= new MessageHandler.BalloonShowEventHandler(this.Messages_BalloonShow);
					this._Messages.Reload -= new MessageHandler.ReloadEventHandler(this.Messages_Reload);
					this._Messages.MouseUp -= new MessageHandler.MouseUpEventHandler(this.Messages_MouseUp);
					this._Messages.MouseMove -= new MessageHandler.MouseMoveEventHandler(this.Messages_MouseMove);
					this._Messages.MouseDown -= new MessageHandler.MouseDownEventHandler(this.Messages_MouseDown);
					this._Messages.DoubleClick -= new MessageHandler.DoubleClickEventHandler(this.Messages_DoubleClick);
					this._Messages.Click -= new MessageHandler.ClickEventHandler(this.Messages_Click);
					//this._Messages.MenuItemRightClick-=new HansBlomme.Windows.Forms.NotifyIcon.MessageHandler.MenuItemRightClickEventHandler(_Messages_MenuItemRightClick);
				}
				this._Messages = value;
				if (this._Messages != null)
				{
					this._Messages.BalloonClick += new MessageHandler.BalloonClickEventHandler(this.Messages_BalloonClick);
					this._Messages.BalloonTimeout += new MessageHandler.BalloonTimeoutEventHandler(this.Messages_BalloonTimeout);
					this._Messages.BalloonHide += new MessageHandler.BalloonHideEventHandler(this.Messages_BalloonHide);
					this._Messages.BalloonShow += new MessageHandler.BalloonShowEventHandler(this.Messages_BalloonShow);
					this._Messages.Reload += new MessageHandler.ReloadEventHandler(this.Messages_Reload);
					this._Messages.MouseUp += new MessageHandler.MouseUpEventHandler(this.Messages_MouseUp);
					this._Messages.MouseMove += new MessageHandler.MouseMoveEventHandler(this.Messages_MouseMove);
					this._Messages.MouseDown += new MessageHandler.MouseDownEventHandler(this.Messages_MouseDown);
					this._Messages.DoubleClick += new MessageHandler.DoubleClickEventHandler(this.Messages_DoubleClick);
					this._Messages.Click += new MessageHandler.ClickEventHandler(this.Messages_Click);
					//this._Messages.MenuItemRightClick+=new HansBlomme.Windows.Forms.NotifyIcon.MessageHandler.MenuItemRightClickEventHandler(_Messages_MenuItemRightClick);
				}
			}
		}

		[Description("The text that will be displayed when the mouse hovers over the icon."), Category("Appearance")]
		public string Text
		{
			get { return this.NID.szTip; }
			set
			{
				this.NID.szTip = value;
				if (this.Visible)
				{
					this.NID.uFlags |= 4;
					Shell_NotifyIcon(1, ref this.NID);
				}
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("Determines whether the control is visible or hidden.")]
		public bool Visible
		{
			get { return this.m_Visible; }
			set
			{
				this.m_Visible = value;
				if (!this.DesignMode)
				{
					if (this.m_Visible)
					{
						Shell_NotifyIcon(0, ref this.NID);
					}
					else
					{
						Shell_NotifyIcon(2, ref this.NID);
					}
				}
			}
		}


		[AccessedThroughProperty("Messages")] private MessageHandler _Messages;
		private IContainer components;
		private ContextMenu m_ContextMenu;
		private Icon m_Icon;
		private bool m_Visible;
		private bool m_VisibleBeforeBalloon;
		private NOTIFYICONDATA NID;
		private const int NIF_ICON = 2;
		private const int NIF_INFO = 0x10;
		private const int NIF_MESSAGE = 1;
		private const int NIF_STATE = 8;
		private const int NIF_TIP = 4;
		private const int NIM_ADD = 0;
		private const int NIM_DELETE = 2;
		private const int NIM_MODIFY = 1;
		private const int NIM_SETVERSION = 4;
		private const int NOTIFYICON_VERSION = 5;
		private const int WM_USER = 0x400;
		private const int WM_USER_TRAY = 0x401;


		public delegate void BalloonClickEventHandler(object sender);


		public delegate void BalloonHideEventHandler(object sender);


		public delegate void BalloonShowEventHandler(object sender);


		public delegate void BalloonTimeoutEventHandler(object sender);


		public delegate void ClickEventHandler(object sender, EventArgs e);


		public delegate void DoubleClickEventHandler(object sender, EventArgs e);


		public enum EBalloonIcon
		{
			None,
			Info,
			Warning,
			Error
		}

		private class MessageHandler : Form
		{
			public event BalloonClickEventHandler BalloonClick;
			public event BalloonHideEventHandler BalloonHide;
			public event BalloonShowEventHandler BalloonShow;
			public event BalloonTimeoutEventHandler BalloonTimeout;
			public new event ClickEventHandler Click;
			public new event DoubleClickEventHandler DoubleClick;
			public new event MouseDownEventHandler MouseDown;
			public new event MouseMoveEventHandler MouseMove;
			public new event MouseUpEventHandler MouseUp;
			public event ReloadEventHandler Reload;
			//public event MenuItemRightClickEventHandler MenuItemRightClick;

			public MessageHandler()
			{
				this.WM_TASKBARCREATED = RegisterWindowMessage("TaskbarCreated");
				this.ShowInTaskbar = false;
				this.StartPosition = FormStartPosition.Manual;
				this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
				Size size1 = new Size(100, 100);
				this.Size = size1;
				Point point1 = new Point(-500, -500);
				this.Location = point1;
				this.Show();
			}

			[DllImport("User32", CharSet=CharSet.Auto)]
			private static extern int RegisterWindowMessage(string lpString);

			protected override void WndProc(ref Message m)
			{
				int num2 = m.Msg;
				if (num2 == 0x401)
				{
					switch (m.LParam.ToInt32())
					{
						case 0x203:
						case 0x206:
						case 0x209:
							if (this.DoubleClick != null)
							{
								this.DoubleClick(this, new MouseEventArgs(MouseButtons, 0, MousePosition.X, MousePosition.Y, 0));
							}
							goto Label_0371;

						case 0x201:
						case 0x204:
						case 0x207:
							if (this.MouseDown != null)
							{
								this.MouseDown(this, new MouseEventArgs(MouseButtons, 0, MousePosition.X, MousePosition.Y, 0));
							}
							goto Label_0371;

						case 0x200:
							if (this.MouseMove != null)
							{
								this.MouseMove(this, new MouseEventArgs(MouseButtons, 0, MousePosition.X, MousePosition.Y, 0));
							}
							goto Label_0371;

						case 0x202:
							if (this.MouseUp != null)
							{
								this.MouseUp(this, new MouseEventArgs(MouseButtons.Left, 0, MousePosition.X, MousePosition.Y, 0));
							}
							if (this.Click != null)
							{
								this.Click(this, new MouseEventArgs(MouseButtons.Left, 0, MousePosition.X, MousePosition.Y, 0));
							}
							goto Label_0371;

						case 0x205:
							if (this.MouseUp != null)
							{
								this.MouseUp(this, new MouseEventArgs(MouseButtons.Right, 0, MousePosition.X, MousePosition.Y, 0));
							}
							if (this.Click != null)
							{
								this.Click(this, new MouseEventArgs(MouseButtons.Right, 0, MousePosition.X, MousePosition.Y, 0));
							}
							goto Label_0371;

						case 520:
							if (this.MouseUp != null)
							{
								this.MouseUp(this, new MouseEventArgs(MouseButtons.Middle, 0, MousePosition.X, MousePosition.Y, 0));
							}
							if (this.Click != null)
							{
								this.Click(this, new MouseEventArgs(MouseButtons.Middle, 0, MousePosition.X, MousePosition.Y, 0));
							}
							goto Label_0371;

						case 0x402:
							if (this.BalloonShow != null)
							{
								this.BalloonShow(this);
							}
							goto Label_0371;

						case 0x403:
							if (this.BalloonHide != null)
							{
								this.BalloonHide(this);
							}
							goto Label_0371;

						case 0x404:
							if (this.BalloonTimeout != null)
							{
								this.BalloonTimeout(this);
							}
							goto Label_0371;

						case 0x405:
							if (this.BalloonClick != null)
							{
								this.BalloonClick(this);
							}
							goto Label_0371;
					}

					Debug.WriteLine(m.LParam.ToInt32());
				}
				else if ((num2 == this.WM_TASKBARCREATED) && (this.Reload != null))
				{
					this.Reload();
				}
				if (m.Msg == WM_EXITMENULOOP)
				{
					TaskMenuItemHelper.selectedItem = null;
				}
//				else if ( m.Msg == WM_MENURBUTTONUP ) 
//				{ 
//					if(MenuItemRightClick!=null)
//						MenuItemRightClick(this, new MenuItemRightClickEventArgs(m.WParam.ToInt32(), m.LParam));
//					
//				} 


				Label_0371:
				base.WndProc(ref m);
			}

			//private const int WM_MENURBUTTONUP=0x0122; 
			private const int WM_EXITMENULOOP = 0x0212;


			private const int NIN_BALLOONHIDE = 0x403;
			private const int NIN_BALLOONSHOW = 0x402;
			private const int NIN_BALLOONTIMEOUT = 0x404;
			private const int NIN_BALLOONUSERCLICK = 0x405;
			private const int WM_LBUTTONDBLCLK = 0x203;
			private const int WM_LBUTTONDOWN = 0x201;
			private const int WM_LBUTTONUP = 0x202;
			private const int WM_MBUTTONDBLCLK = 0x209;
			private const int WM_MBUTTONDOWN = 0x207;
			private const int WM_MBUTTONUP = 520;
			private const int WM_MOUSEMOVE = 0x200;
			private const int WM_RBUTTONDBLCLK = 0x206;
			private const int WM_RBUTTONDOWN = 0x204;
			private const int WM_RBUTTONUP = 0x205;
			private int WM_TASKBARCREATED;
			private const int WM_USER = 0x400;
			private const int WM_USER_TRAY = 0x401;


			public delegate void BalloonClickEventHandler(object sender);


			public delegate void BalloonHideEventHandler(object sender);


			public delegate void BalloonShowEventHandler(object sender);


			public delegate void BalloonTimeoutEventHandler(object sender);


			public delegate void ClickEventHandler(object sender, EventArgs e);


			public delegate void DoubleClickEventHandler(object sender, EventArgs e);


			public delegate void MouseDownEventHandler(object sender, MouseEventArgs e);


			public delegate void MouseMoveEventHandler(object sender, MouseEventArgs e);


			public delegate void MouseUpEventHandler(object sender, MouseEventArgs e);

			//public delegate void MenuItemRightClickEventHandler(object sender, MenuItemRightClickEventArgs e);


			public delegate void ReloadEventHandler();
		}

		public delegate void MouseDownEventHandler(object sender, MouseEventArgs e);


		public delegate void MouseMoveEventHandler(object sender, MouseEventArgs e);


		public delegate void MouseUpEventHandler(object sender, MouseEventArgs e);

		public delegate void MenuItemRightClickEventHandler(object sender, MenuItemRightClickEventArgs e);

		public class MenuItemRightClickEventArgs : EventArgs
		{
			private int itemIndex;
			private IntPtr menuHandle;

			public MenuItemRightClickEventArgs(int menuIndex, IntPtr mnuHandle) : base()
			{
				this.itemIndex = menuIndex;
				this.menuHandle = mnuHandle;
			}

			public IntPtr MenuHandle
			{
				get { return menuHandle; }
				set { menuHandle = value; }
			}

			public int ItemIndex
			{
				get { return itemIndex; }
				set { itemIndex = value; }
			}
		}


		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		private struct NOTIFYICONDATA
		{
			public int cbSize;
			public IntPtr hwnd;
			public int uID;
			public int uFlags;
			public int uCallbackMessage;
			public IntPtr hIcon;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x80)] public string szTip;
			public int dwState;
			public int dwStateMask;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x100)] public string szInfo;
			public int uVersion;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x40)] public string szInfoTitle;
			public int dwInfoFlags;
		}
	}
}