using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace PTM.View
{
	/// <summary>
	/// Summary description for ViewHelper.
	/// </summary>
	public sealed class ViewHelper
	{
		private ViewHelper()
		{
		}

		[DllImport("User32")]
		internal static extern IntPtr GetForegroundWindow();

		[DllImport("User32")]
		internal static extern IntPtr GetParent(IntPtr hwnd);

		[DllImport("User32")]
		internal static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out IntPtr ProcessId);

		[DllImport("User32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWindow(IntPtr hWnd);



		internal const int WM_QUERYENDSESSION = 0x11;
		internal const int IDANI_CAPTION = 3;
		//		private const int SW_HIDE = 0;
		//		private const int SW_MAX = 10;
		//		private const int SW_MAXIMIZE = 3;
		//		private const int SW_MINIMIZE = 6;
		//		private const int SW_NORMAL = 1;
		internal const int SW_RESTORE = 9;
		//		private const int SW_SHOW = 5;
		//		private const int SW_SHOWDEFAULT = 10;
		//		private const int SW_SHOWMAXIMIZED = 3;
		//		private const int SW_SHOWMINIMIZED = 2;
		//		private const int SW_SHOWMINNOACTIVE = 7;
		//		private const int SW_SHOWNA = 8;
		//		private const int SW_SHOWNOACTIVATE = 4;
		internal const int SW_SHOWNORMAL = 1;

		[DllImport("User32.dll", EntryPoint="ShowWindow", CharSet=CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("User32.dll", EntryPoint="UpdateWindow", CharSet=CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool UpdateWindow(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint="IsIconic", CharSet=CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsIconic(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint="FindWindowEx", CharSet=CharSet.Auto)]
		internal static extern IntPtr FindWindowEx(
			IntPtr hwndParent,
			IntPtr hwndChildAfter,
			[MarshalAs(UnmanagedType.LPTStr)]
			string lpszClass,
			[MarshalAs(UnmanagedType.LPTStr)]
			string lpszWindow);

		[DllImport("user32.dll", EntryPoint="DrawAnimatedRects", CharSet=CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DrawAnimatedRects(IntPtr hwnd, int idAni, ref RECT lprcFrom, ref RECT lprcTo);

		[StructLayout(LayoutKind.Sequential)]
			public struct RECT
		{
			internal int left;
			internal int top;
			internal int right;
			internal int bottom;

			public override string ToString()
			{
				return ("Left :" + left.ToString(CultureInfo.InvariantCulture) + "," + "Top :" + top.ToString(CultureInfo.InvariantCulture) + "," + "Right :" + right.ToString(CultureInfo.InvariantCulture) + "," + "Bottom :" + bottom.ToString(CultureInfo.InvariantCulture));
			}
		}

		[DllImport("user32.dll", EntryPoint="GetWindowRect", CharSet=CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetWindowRect(IntPtr hwnd, ref RECT lpRect);

		internal static IntPtr GetNotificationAreaHandle()
		{
			IntPtr hwnd = ViewHelper.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
			Console.WriteLine("Shell_TrayWnd" + hwnd.ToString());
			hwnd = ViewHelper.FindWindowEx(hwnd, IntPtr.Zero, "TrayNotifyWnd", null);
			Console.WriteLine("TrayNotifyWnd" + hwnd.ToString());
			hwnd = ViewHelper.FindWindowEx(hwnd, IntPtr.Zero, "SysPager", null);
			Console.WriteLine("SysPager" + hwnd.ToString());

			if (hwnd != IntPtr.Zero)
				hwnd = ViewHelper.FindWindowEx(hwnd, IntPtr.Zero, null, "Notification Area");

			Console.WriteLine("Notification Area" + hwnd.ToString());
			return hwnd;
		}

		internal static string TimeSpanToTimeString(TimeSpan ts)
		{
			return ts.Hours.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + ":" + ts.Minutes.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + ":" + ts.Seconds.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
		}
		internal static string Int32ToTimeString(int seconds)
		{
			TimeSpan t = new TimeSpan(0, 0, seconds);
			return TimeSpanToTimeString(t);
		}



		internal static string FixTaskPath(string path, int maxLen)
		{
			if(path.Length>maxLen)
			{
				return "..." + path.Substring(path.Length-(maxLen-3), (maxLen-3));
			}
			else
			{
				return path;
			}
		}

	}
}