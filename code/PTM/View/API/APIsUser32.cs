using System.Text;
using Microsoft.Win32;

namespace System.Runtime.InteropServices.APIs
{
	internal class APIsUser32
	{
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern uint SendInput(uint nInputs, APIsStructs.INPUT[] inputs, int cbSize);
//
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern bool ScreenToClient(IntPtr hWnd, ref APIsStructs.POINTAPI point);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int DrawFrameControl(IntPtr hDc, APIsStructs.RECT rect, APIsEnums.DrawFrameControlFlags nType,
//		                                            APIsEnums.DrawFrameControlStateFlags nState);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int GetSysColor(APIsEnums.SystemColors nIndex);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool DestroyIcon(IntPtr hIcon);
//
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern bool GetComboBoxInfo(IntPtr hWnd, ref APIsStructs.COMBOBOXINFO cbi);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool DestroyWindow(IntPtr hWnd);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr GetDC(IntPtr hWnd);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr GetDesktopWindow();
//
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern bool ShowWindow(IntPtr hWnd, APIsEnums.ShowWindowStyles State);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool UpdateWindow(IntPtr hWnd);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool SetForegroundWindow(IntPtr hWnd);
//
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
			int Y, int cx, int cy, uint uFlags);


//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool OpenClipboard(IntPtr hWndNewOwner);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool CloseClipboard();
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool EmptyClipboard();
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr SetClipboardData(uint Format, IntPtr hData);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref APIsStructs.RECT rc);
//
//		[DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
//		internal static extern IntPtr GetParent(IntPtr hWnd);
//
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern int SendMessage(HandleRef hWnd, APIsEnums.HeaderControlMessages msg, IntPtr wParam,
		                                       ref APIsStructs.HDITEM lParam);
//
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		internal static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, IntPtr lParam);

//		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
//		internal static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, StringBuilder lParam);

//		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
//		internal static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam,  String lParam);
//		//Also can add 'ref' or 'out' ahead 'String lParam'
//		// -- Do not use 'out String', use '[Out] StringBuilder' instead and initialize the string builder
//		// with proper length first. Dunno why but that is the only thing that worked for me.
//
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		internal static extern void SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, ref APIsStructs.RECT lParam);

//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, APIsEnums.HeaderControlMessages msg, IntPtr wParam,
		                                       ref APIsStructs.HD_HITTESTINFO hd_hittestinfo);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int SendMessage(IntPtr hWnd, APIsEnums.WindowMessages msg, int wParam, int lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern void SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref APIsStructs.NMHDR lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern void SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref APIsStructs.NMLVDISPINFO lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref APIsStructs.RECT lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref APIsStructs.POINTAPI lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr PostMessage(IntPtr hWnd, APIsEnums.WindowMessages msg, int wParam, int lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr SetFocus(IntPtr hWnd);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int DrawText(IntPtr hdc, string lpString, int nCount, ref APIsStructs.RECT lpRect,
//		                                    APIsEnums.DrawTextFormatFlags flags);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr SetParent(IntPtr hChild, IntPtr hParent);
//
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern IntPtr GetDlgItem(IntPtr hDlg, int nControlID);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int GetClientRect(IntPtr hWnd, ref APIsStructs.RECT rc);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int InvalidateRect(IntPtr hWnd, ref APIsStructs.RECT rc, int bErase);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int InvalidateRect(IntPtr hWnd, IntPtr rc, int bErase);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool WaitMessage();
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool PeekMessage(ref APIsStructs.MSG msg, int hWnd, uint wFilterMin, uint wFilterMax,
//		                                        APIsEnums.PeekMessageFlags flags);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool GetMessage(ref APIsStructs.MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool TranslateMessage(ref APIsStructs.MSG msg);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool DispatchMessage(ref APIsStructs.MSG msg);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr LoadCursor(IntPtr hInstance, APIsEnums.CursorTypes cursor);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr SetCursor(IntPtr hCursor);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr GetFocus();
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool ReleaseCapture();
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr SetCapture(IntPtr hWnd);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr BeginPaint(IntPtr hWnd, ref APIsStructs.PAINTSTRUCT ps);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool EndPaint(IntPtr hWnd, ref APIsStructs.PAINTSTRUCT ps);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref APIsStructs.POINTAPI pptDst,
//		                                                ref APIsStructs.SIZE psize,
//		                                                IntPtr hdcSrc, ref APIsStructs.POINTAPI pprSrc, Int32 crKey,
//		                                                ref APIsStructs.BLENDFUNCTION pblend,
//		                                                APIsEnums.UpdateLayeredWindowFlags dwFlags);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool GetWindowRect(IntPtr hWnd, ref APIsStructs.RECT rect);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool ClientToScreen(IntPtr hWnd, ref APIsStructs.POINTAPI pt);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool TrackMouseEvent(ref APIsStructs.TRACKMOUSEEVENTS tme);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern ushort GetKeyState(int virtKey);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern int GetClassName(IntPtr hWnd, StringBuilder ClassName, int nMaxCount);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRegion, uint flags);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr GetWindowDC(IntPtr hWnd);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern int FillRect(IntPtr hDC, ref APIsStructs.RECT rect, IntPtr hBrush);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern int GetWindowPlacement(IntPtr hWnd, ref APIsStructs.WINDOWPLACEMENT wp);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern int SetWindowText(IntPtr hWnd, string text);
//
//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
//		internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int GetSystemMetrics(APIsEnums.SystemMetricsCodes code);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int SetScrollInfo(IntPtr hwnd, int bar, ref APIsStructs.SCROLLINFO si, int fRedraw);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int EnableScrollBar(IntPtr hWnd, uint flags, uint arrows);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int BringWindowToTop(IntPtr hWnd);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int GetScrollInfo(IntPtr hwnd, int bar, ref APIsStructs.SCROLLINFO si);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy,
//		                                          ref APIsStructs.RECT rcScroll, ref APIsStructs.RECT rcClip,
//		                                          IntPtr UpdateRegion, ref APIsStructs.RECT rcInvalidated, uint flags);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool IsWindow(IntPtr hWnd);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int LockWindowUpdate(IntPtr hWnd);
//
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		internal static extern bool ValidateRect(IntPtr hWnd, ref APIsStructs.RECT rcInvalidated);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern bool ValidateRect(IntPtr hWnd, IntPtr rc);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int GetScrollBarInfo(IntPtr hWnd, APIsEnums.SystemObjects id, ref APIsStructs.SCROLLBARINFO sbi);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int SetProp(IntPtr hWnd, IntPtr atom, IntPtr hData);
//
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		internal static extern int CallWindowProc(IntPtr hOldProc, IntPtr hWnd, uint message, int wParam, int lParam);
//
		[DllImport("user32.dll")]
		internal static extern bool SendMessage(HandleRef hWnd, APIsEnums.ListViewMessages msg,
		                                        IntPtr wParam, ref APIsStructs.LV_ITEM lParam);
//
		[DllImport("user32.dll")]
		internal static extern IntPtr SendMessage(HandleRef hWnd, Int32 msg, IntPtr wParam, ref
		                                                                             	APIsStructs.LVHITTESTINFO lParam);
//
		[DllImport("user32.dll")]
		internal static extern bool SendMessage(HandleRef hWnd, int msg,
		                                        IntPtr wParam, ref IntPtr lParam);
	}
}