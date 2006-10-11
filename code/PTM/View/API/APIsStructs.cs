using System;
using System.Runtime.InteropServices;

namespace System.Runtime.InteropServices.APIs
{
	internal class APIsStructs
	{
		#region DLLVERSIONINFO
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct DLLVERSIONINFO
		{
			internal int cbSize;
			internal int dwMajorVersion;
			internal int dwMinorVersion;
			internal int dwBuildNumber;
			internal int dwPlatformID;
		}
		#endregion
		#region DLLVERSIONINFO2
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct DLLVERSIONINFO2
		{
			internal DLLVERSIONINFO info1;
			internal int dwFlags;
			ulong ullVersion;
		}
		#endregion
		#region WIN32_FIND_DATA
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			internal struct WIN32_FIND_DATA
		{
			internal uint fileAttributes;
			internal FILETIME creationTime;
			internal FILETIME lastAccessTime;
			internal FILETIME lastWriteTime;
			internal uint fileSizeHigh;
			internal uint fileSizeLow;
			internal uint reserved0;
			internal uint reserved1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
			internal string fileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=14)]
			internal string alternateFileName;
		}
		#endregion

		#region SHITEMIDLIST
		[StructLayout(LayoutKind.Sequential)]
			internal struct SHITEMIDLIST
		{
			internal SHITEMID[] mkid;
		}
		#endregion
		#region SHITEMID
		[StructLayout(LayoutKind.Sequential)]
			internal struct SHITEMID
		{
			internal ushort cb;
			internal byte abID;
		}
		#endregion
		#region SHFILEOPSTRUCT
		/// <summary>
		/// Contains information that the SHFileOperation function uses to perform file operations.
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
		internal struct SHFILEOPSTRUCT
		{
			/// <summary>
			/// Window handle to the dialog box to display information about the status of the file operation.
			/// </summary>
			internal IntPtr hwnd;
			/// <summary>
			/// Value that indicates which operation to perform.
			/// </summary>
			internal UInt32 wFunc;
			/// <summary>
			/// Address of a buffer to specify one or more source file names.
			/// </summary>
			internal IntPtr pFrom;
			/// <summary>
			/// Address of a buffer to contain the name of the destination file or directory.
			/// </summary>
			internal IntPtr pTo;
			/// <summary>
			/// Flags that control the file operation (should use APISEnums.FOF).
			/// </summary>
			internal UInt16 fFlags;
			/// <summary>
			/// Value that receives TRUE if the user aborted any file operations before they were completed, or FALSE otherwise.
			/// </summary>
			internal Int32 fAnyOperationsAborted;
			/// <summary>
			/// A handle to a name mapping object containing the old and new names of the renamed files.
			/// </summary>
			internal IntPtr hNameMappings;
			/// <summary>
			/// Address of a string to use as the title of a progress dialog box.
			/// </summary>
			[MarshalAs(UnmanagedType.LPWStr)] internal string lpszProgressTitle;
		}
		#endregion
		#region SHELLEXECUTEINFO
		[StructLayoutAttribute(LayoutKind.Sequential)]
			internal struct SHELLEXECUTEINFO
		{
			internal int cbSize;
			internal APIsEnums.ShellExecuteFlags fMask;
			internal IntPtr hWnd;
			internal string lpVerb;
			internal string lpFile;
			internal string lpParameters;
			internal string lpDirectory;
			internal APIsEnums.ShowWindowStyles nShow;
			internal IntPtr hInstApp;
			internal IntPtr lpIDList;
			internal int lpClass;
			internal int hkeyClass;
			internal int dwHotKey;
			internal IntPtr hIcon;
			internal IntPtr hProcess;
		}
		#endregion
		#region SHFILEINFO
		[StructLayout(LayoutKind.Sequential)]
		internal struct SHFILEINFO
		{
			internal SHFILEINFO(bool b)
			{
				hIcon=IntPtr.Zero;iIcon=0;dwAttributes=0;szDisplayName="";szTypeName="";
			}
			internal IntPtr hIcon;
			internal int iIcon;
			internal uint dwAttributes;
			[MarshalAs(UnmanagedType.LPStr, SizeConst=260)]
			internal string szDisplayName;
			[MarshalAs(UnmanagedType.LPStr, SizeConst=80)]
			internal string szTypeName;
		};
		#endregion

		#region STRRET
		[StructLayout(LayoutKind.Sequential)]
			internal struct STRRET
		{
			internal int uType;
			//		IntPtr pOleStr;
			//		uint uOffset;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=260)] internal byte[] cStr;
		}
		#endregion
		#region MENUITMEINFO
		[StructLayoutAttribute(LayoutKind.Sequential)]
			internal struct MENUITMEINFO
		{
			internal int cbSize; 
			internal APIsEnums.MenuItemMasks fMask; 
			internal APIsEnums.MenuItemTypes fType; 
			internal APIsEnums.MenuItemStates fState; 
			internal int wID; 
			internal IntPtr hSubMenu; 
			internal IntPtr hbmpChecked; 
			internal IntPtr hbmpUnchecked; 
			internal int dwItemData; 
			internal IntPtr dwTypeData; 
			internal int cch; 
			internal IntPtr hbmpItem;
		}
		#endregion
		#region StartupInfo
		[StructLayout(LayoutKind.Sequential)]
			internal class StartupInfo
		{
			internal int cb;
			internal String lpReserved;
			internal String lpDesktop;
			internal String lpTitle;
			internal int dwX;
			internal int dwY;
			internal int dwXSize;
			internal int dwYSize;
			internal int dwXCountChars;
			internal int dwYCountChars;
			internal int dwFillAttribute;
			internal int dwFlags;
			internal UInt16 wShowWindow;
			internal UInt16 cbReserved2;
			internal Byte  lpReserved2;
			internal int hStdInput;
			internal int hStdOutput;
			internal int hStdError;
		}
		#endregion
		#region ProcessInformation
		[StructLayout(LayoutKind.Sequential)]
			internal class ProcessInformation
		{
			internal int hProcess;
			internal int hThread;
			internal int dwProcessId;
			internal int dwThreadId;
		}
		#endregion
		#region MENUITEMINFO
		[StructLayout(LayoutKind.Sequential)]
			internal struct MENUITEMINFO
		{
			internal uint cbSize;
			internal uint fMask;
			internal uint fType;
			internal uint fState;
			internal int	wID;
			internal int	/*HMENU*/	  hSubMenu;
			internal int	/*HBITMAP*/   hbmpChecked;
			internal int	/*HBITMAP*/	  hbmpUnchecked;
			internal int	/*ULONG_PTR*/ dwItemData;
			internal String dwTypeData;
			internal uint cch;
			internal int /*HBITMAP*/ hbmpItem;
		}
		#endregion
		#region FORMATETC
		[StructLayout(LayoutKind.Sequential)]
		internal struct FORMATETC
		{
			internal APIsEnums.ClipboardFormats	cfFormat;
			internal uint ptd;
			internal APIsEnums.TargetDevices		dwAspect;
			internal int			lindex;
			internal APIsEnums.StorageMediumTypes		tymed;
		}
		#endregion
		#region STGMEDIUM
		[StructLayout(LayoutKind.Sequential)]
			internal struct STGMEDIUM
		{
			internal uint tymed;
			internal uint hGlobal;
			internal uint pUnkForRelease;
		}
		#endregion
		#region CMINVOKECOMMANDINFO
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			internal struct CMINVOKECOMMANDINFO
		{
			internal int cbSize;				// sizeof(CMINVOKECOMMANDINFO)
			internal int fMask;				// any combination of CMIC_MASK_*
			internal IntPtr hwnd;				// might be NULL (indicating no owner window)
			internal IntPtr lpVerb;			// either a string or MAKEINTRESOURCE(idOffset)
			internal IntPtr lpParameters;		// might be NULL (indicating no parameter)
			internal IntPtr lpDirectory;		// might be NULL (indicating no specific directory)
			internal int nShow;				// one of SW_ values for ShowWindow() API
			internal int dwHotKey;
			internal IntPtr hIcon;
		}
		#endregion

		#region LV_ITEM
		[StructLayoutAttribute(LayoutKind.Sequential)]
		internal struct LV_ITEM
		{
			internal APIsEnums.ListViewItemFlags mask;
			internal Int32 iItem;
			internal Int32 iSubItem;
			internal APIsEnums.ListViewItemStates state;
			internal APIsEnums.ListViewItemStates stateMask;
			internal String pszText;
			internal Int32 cchTextMax;
			internal Int32 iImage;
			internal IntPtr lParam;
			internal Int32 iIndent;
		}
		#endregion
		#region LVCOLUMN
		[StructLayoutAttribute(LayoutKind.Sequential)]
			internal struct LVCOLUMN
		{
			internal Int32 mask;
			internal Int32 fmt;
			internal Int32 cx;
			internal string pszText;
			internal Int32 cchTextMax;
			internal Int32 iSubItem;
			internal Int32 iImage;
			internal Int32 iOrder;
		}
		#endregion
		#region LVHITTESTINFO
		[StructLayoutAttribute(LayoutKind.Sequential)]
		internal struct LVHITTESTINFO
		{
			internal POINTAPI pt;
			internal int flags;
			internal Int32 iItem;
			internal Int32 iSubItem;
		}
		#endregion
		#region NMLVDISPINFO
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct NMLVDISPINFO
		{
			internal NMHDR hdr;
			internal LV_ITEM lvitem;
		}	
		#endregion
		#region NMLISTVIEW
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct NMLISTVIEW
		{
			internal NMHDR nmhdr;
			internal int iItem;
			internal int iSubItem;
			internal uint uNewState;
			internal uint uOldState;
			internal uint uChanged;
			internal POINTAPI ptAction;
			internal IntPtr lParam;
			internal bool NewSelected
			{
				get
				{
					return ((APIsEnums.ListViewItemStates)uNewState & APIsEnums.ListViewItemStates.SELECTED) == APIsEnums.ListViewItemStates.SELECTED;
				}
			}
			internal bool OldSelected
			{
				get
				{
					return ((APIsEnums.ListViewItemStates)uOldState & APIsEnums.ListViewItemStates.SELECTED) == APIsEnums.ListViewItemStates.SELECTED;
				}
			}
			internal bool NewCheck
			{
				get
				{
					try
					{
						return uNewState >= 0x1000 ? ((uNewState & (uint)APIsEnums.ListViewItemStates.STATEIMAGEMASK) >> 12) - 1 > 0 : false;
					}
					catch
					{
						return false;
					}
				}
			}
			internal bool OldCheck
			{
				get
				{
					try
					{
						return uOldState >= 0x1000 ? ((uOldState & (uint)APIsEnums.ListViewItemStates.STATEIMAGEMASK) >> 12) - 1 > 0 : false;
					}
					catch
					{
						return false;
					}
				}
			}
			internal bool NewFocused
			{
				get
				{
					return ((APIsEnums.ListViewItemStates)uNewState & APIsEnums.ListViewItemStates.FOCUSED) == APIsEnums.ListViewItemStates.FOCUSED;
				}
			}
			internal bool OldFocused
			{
				get
				{
					return ((APIsEnums.ListViewItemStates)uOldState & APIsEnums.ListViewItemStates.FOCUSED) == APIsEnums.ListViewItemStates.FOCUSED;
				}
			}
			internal bool Select
			{
				get
				{
					return !OldSelected && NewSelected;
				}
			}
			internal bool UnSelect
			{
				get
				{
					return OldSelected && !NewSelected;
				}
			}
			internal bool Focus
			{
				get
				{
					return !OldFocused && NewFocused;
				}
			}
			internal bool UnFocus
			{
				get
				{
					return OldFocused && !NewFocused;
				}
			}
			internal bool Check
			{
				get
				{
					return !OldCheck && NewCheck;
				}
			}
			internal bool UnCheck
			{
				get
				{
					return OldCheck && !NewCheck;
				}
			}
		}
		#endregion
		#region HDITEM
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct HDITEM
		{
			internal	APIsEnums.HeaderItemFlags mask;
			internal	int     cxy;
			internal	IntPtr  pszText;
			internal	IntPtr  hbm;
			internal	int     cchTextMax;
			internal	int     fmt;
			internal	int     lParam;
			internal	int     iImage;      
			internal	int     iOrder;
		}	
		#endregion
		#region HD_HITTESTINFO
		[StructLayout(LayoutKind.Sequential)]
			internal struct HD_HITTESTINFO 
		{  
			internal POINTAPI pt;  
			internal APIsEnums.HeaderControlHitTestFlags flags; 
			internal int iItem; 
		}
		#endregion

		#region POINTAPI
		[StructLayoutAttribute(LayoutKind.Sequential)]
		internal struct POINTAPI
		{
			internal POINTAPI(System.Drawing.Point p) {x = p.X; y = p.Y;}
			internal POINTAPI(Int32 X, Int32 Y) {x = X; y = Y;}
			internal Int32 x;
			internal Int32 y;
		}
		#endregion
		#region RECT
		[StructLayout(LayoutKind.Sequential)]
		internal struct RECT
		{
			internal RECT(Drawing.Rectangle rectangle)
			{	left = rectangle.Left; top = rectangle.Top;
				right = rectangle.Right; bottom = rectangle.Bottom;}
			internal int left;
			internal int top;
			internal int right;
			internal int bottom;
		}
		#endregion
		#region SIZE
		[StructLayout(LayoutKind.Sequential)]
		internal struct SIZE
		{
			internal int cx;
			internal int cy;
		}
		#endregion

		#region NMHDR
//		[StructLayout(LayoutKind.Sequential)]
			internal struct NMHDR
		{
			internal IntPtr hwndFrom;
			internal int idFrom;
			internal int code;
		}
		#endregion
		#region NMCUSTOMDRAW
		[StructLayout(LayoutKind.Sequential)]
		internal struct NMCUSTOMDRAW
		{
			internal NMHDR hdr;
			internal int dwDrawStage;
			internal IntPtr hdc;
			internal RECT rc;
			internal uint dwItemSpec;
			internal uint uItemState;
			internal IntPtr lItemlParam;
		}
		#endregion
		#region NMLVCUSTOMDRAW
		[StructLayout(LayoutKind.Sequential)]
		internal struct NMLVCUSTOMDRAW 
		{
			internal NMCUSTOMDRAW nmcd;
			internal int clrText;
			internal int clrTextBk;
			internal int iSubItem;
			internal int dwItemType;
			internal int clrFace;
			internal int iIconEffect;
			internal int iIconPhase;
			internal int iPartId;
			internal int iStateId;
			internal RECT rcText;
			internal uint uAlign;
		} 
		#endregion
		#region NMTVCUSTOMDRAW
		[StructLayout(LayoutKind.Sequential)]
		internal struct NMTVCUSTOMDRAW 
		{
			internal NMCUSTOMDRAW nmcd;
			internal uint clrText;
			internal uint clrTextBk;
			internal int iLevel;
		}
		#endregion

		#region BITMAPINFO_FLAT
		[StructLayout(LayoutKind.Sequential)]
			internal struct BITMAPINFO_FLAT 
		{
			internal int      bmiHeader_biSize;
			internal int      bmiHeader_biWidth;
			internal int      bmiHeader_biHeight;
			internal short    bmiHeader_biPlanes;
			internal short    bmiHeader_biBitCount;
			internal int      bmiHeader_biCompression;
			internal int      bmiHeader_biSizeImage;
			internal int      bmiHeader_biXPelsPerMeter;
			internal int      bmiHeader_biYPelsPerMeter;
			internal int      bmiHeader_biClrUsed;
			internal int      bmiHeader_biClrImportant;
			[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1024)]
			internal byte[] bmiColors; 
		}
		#endregion
		#region BITMAPINFOHEADER
		[StructLayout(LayoutKind.Sequential)]
			internal class BITMAPINFOHEADER 
		{
			internal int      biSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER));
			internal int      biWidth;
			internal int      biHeight;
			internal short    biPlanes;
			internal short    biBitCount;
			internal int      biCompression;
			internal int      biSizeImage;
			internal int      biXPelsPerMeter;
			internal int      biYPelsPerMeter;
			internal int      biClrUsed;
			internal int      biClrImportant;
		}
		#endregion

		#region MSG
		[StructLayout(LayoutKind.Sequential)]
		internal struct MSG 
		{
			internal IntPtr hwnd;
			internal int message;
			internal IntPtr wParam;
			internal IntPtr lParam;
			internal int time;
			internal int pt_x;
			internal int pt_y;
		}
		#endregion
		#region PAINTSTRUCT
		[StructLayout(LayoutKind.Sequential)]
		internal struct PAINTSTRUCT
		{
			internal IntPtr hdc;
			internal int fErase;
			internal System.Drawing.Rectangle rcPaint;
			internal int fRestore;
			internal int fIncUpdate;
			internal int Reserved1;
			internal int Reserved2;
			internal int Reserved3;
			internal int Reserved4;
			internal int Reserved5;
			internal int Reserved6;
			internal int Reserved7;
			internal int Reserved8;
		}
		#endregion
		#region TRACKMOUSEEVENTS
		[StructLayout(LayoutKind.Sequential)]
			internal struct TRACKMOUSEEVENTS
		{
			internal uint cbSize;
			internal APIsEnums.TrackerEventFlags dwFlags;
			internal IntPtr hWnd;
			internal uint dwHoverTime;
		}
		#endregion
		#region WINDOWPLACEMENT
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct WINDOWPLACEMENT
		{	
			internal uint length; 
			internal uint flags; 
			internal uint showCmd; 
			internal APIsStructs.POINTAPI ptMinPosition; 
			internal APIsStructs.POINTAPI ptMaxPosition; 
			internal APIsStructs.RECT  rcNormalPosition; 
		}
		#endregion

		#region SCROLLINFO
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct SCROLLINFO
		{
			internal 	uint   cbSize;
			internal 	uint   fMask;
			internal 	int    nMin;
			internal 	int    nMax;
			internal 	uint   nPage;
			internal 	int    nPos;
			internal 	int    nTrackPos;
		}
		#endregion
		#region SCROLLBARINFO
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct SCROLLBARINFO
		{
			internal uint  cbSize;
			internal APIsStructs.RECT  rcScrollBar;
			internal int   dxyLineButton;
			internal int   xyThumbTop;
			internal int   xyThumbBottom;
			internal int   reserved;
			[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
			internal uint[] rgstate;
		}
		#endregion

		#region PCOMBOBOXINFO
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct PCOMBOBOXINFO
		{
			internal uint  cbSize;
			internal APIsStructs.RECT  rcItem;
			internal APIsStructs.RECT  rcButton;
			internal int   stateButton;
			internal IntPtr hwndCombo;
			internal IntPtr hwndItem;
			internal IntPtr hwndList;
		}
		#endregion

		#region BLENDFUNCTION
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal struct BLENDFUNCTION
		{
			internal byte BlendOp;
			internal byte BlendFlags;
			internal byte SourceConstantAlpha;
			internal byte AlphaFormat;
		}
		#endregion

		#region NMHEADER
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal struct NMHEADER
		{
			internal NMHDR nmhdr;
			internal int iItem;
			internal int iButton;
			internal HDITEM pItem;
		}
		#endregion

		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal struct INPUT
		{
			internal uint type;
			internal int dx;
			internal int dy;
			internal uint mouseData;
			internal uint dwFlags;
			internal uint time;
			internal uint dwExtra;
		}
	}
}
