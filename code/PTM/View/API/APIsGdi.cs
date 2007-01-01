using System;

namespace System.Runtime.InteropServices.APIs
{
	internal class APIsGdi
	{
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		static internal extern bool TransparentBlt(
			IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int hHeightDest,
			IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int hHeightSrc,
			int crTransparent);
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		static internal extern IntPtr GetDC(IntPtr hWnd);
		[DllImport("gdi32.dll")]
		static internal extern bool StretchBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
			IntPtr hDCSrc,  int XOriginScr, int YOriginSrc, int WidthScr, int HeightScr, APIsEnums.PatBltTypes Rop);
		[DllImport("gdi32.dll")]
		static internal extern IntPtr CreateCompatibleDC(IntPtr hDC);
		[DllImport("gdi32.dll")]
		static internal extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int Width, int Heigth);
		[DllImport("gdi32.dll")]
		static internal extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
		[DllImport("gdi32.dll")]
		static internal extern bool BitBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
			IntPtr hDCSrc,  int XOriginScr, int YOriginSrc, APIsEnums.PatBltTypes flags);
		[DllImport("gdi32.dll")]
		static internal extern IntPtr DeleteDC(IntPtr hDC);
		[DllImport("gdi32.dll")]
		static internal extern bool PatBlt(IntPtr hDC, int XLeft, int YLeft, int Width, int Height, uint Rop);
		[DllImport("gdi32.dll")]
		static internal extern bool DeleteObject(IntPtr hObject);
		[DllImport("gdi32.dll")]
		static internal extern uint GetPixel(IntPtr hDC, int XPos, int YPos);
		[DllImport("gdi32.dll")]
		static internal extern int SetMapMode(IntPtr hDC, int fnMapMode);
		[DllImport("gdi32.dll")]
		static internal extern int GetObjectType(IntPtr handle);
		[DllImport("gdi32")]
		internal static extern IntPtr CreateDIBSection(IntPtr hdc, ref APIsStructs.BITMAPINFO_FLAT bmi, 
			int iUsage, ref int ppvBits, IntPtr hSection, int dwOffset);
		[DllImport("gdi32")]
		internal static extern int GetDIBits(IntPtr hDC, IntPtr hbm, int StartScan, int ScanLines, int lpBits, APIsStructs.BITMAPINFOHEADER bmi, int usage);
		[DllImport("gdi32")]
		internal static extern int GetDIBits(IntPtr hdc, IntPtr hbm, int StartScan, int ScanLines, int lpBits, ref APIsStructs.BITMAPINFO_FLAT bmi, int usage);
		[DllImport("gdi32")]
		internal static extern IntPtr GetPaletteEntries(IntPtr hpal, int iStartIndex, int nEntries, byte[] lppe);
		[DllImport("gdi32")]
		internal static extern IntPtr GetSystemPaletteEntries(IntPtr hdc, int iStartIndex, int nEntries, byte[] lppe);
		[DllImport("gdi32")]
		internal static extern uint SetDCBrushColor(IntPtr hdc,  uint crColor);
		[DllImport("gdi32")]
		internal static extern IntPtr CreateSolidBrush(uint crColor);
		[DllImport("gdi32")]
		internal static extern int SetBkMode(IntPtr hDC, APIsEnums.BackgroundMode mode);
		[DllImport("gdi32")]
		internal static extern int SetViewportOrgEx(IntPtr hdc,  int x, int y,  int param);
		[DllImport("gdi32")]
		internal static extern uint SetTextColor(IntPtr hDC, uint colorRef);
		[DllImport("gdi32")]
		internal static extern int SetStretchBltMode(IntPtr hDC, APIsEnums.StrechModeFlags StrechMode);
		[DllImport("gdi32")]
		internal static extern uint SetPixel(IntPtr hDC, int x, int y, uint color);
		
		[StructLayout(LayoutKind.Explicit)]
			public struct WIN32Rect 
		{
			[FieldOffset(0)] public int left;
			[FieldOffset(4)] public int top;
			[FieldOffset(8)] public int right;
			[FieldOffset(12)] public int bottom;
		}   


		[DllImport("gdi32")]
		internal static extern int GetClipBox(	System.IntPtr hDC,
			ref WIN32Rect r );
	}
}
