using System.Drawing;

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
			private ulong ullVersion;
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
					return
						((APIsEnums.ListViewItemStates) uNewState & APIsEnums.ListViewItemStates.SELECTED) ==
						APIsEnums.ListViewItemStates.SELECTED;
				}
			}

			internal bool OldSelected
			{
				get
				{
					return
						((APIsEnums.ListViewItemStates) uOldState & APIsEnums.ListViewItemStates.SELECTED) ==
						APIsEnums.ListViewItemStates.SELECTED;
				}
			}

			internal bool NewCheck
			{
				get
				{
					try
					{
						return
							uNewState >= 0x1000 ? ((uNewState & (uint) APIsEnums.ListViewItemStates.STATEIMAGEMASK) >> 12) - 1 > 0 : false;
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
						return
							uOldState >= 0x1000 ? ((uOldState & (uint) APIsEnums.ListViewItemStates.STATEIMAGEMASK) >> 12) - 1 > 0 : false;
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
					return
						((APIsEnums.ListViewItemStates) uNewState & APIsEnums.ListViewItemStates.FOCUSED) ==
						APIsEnums.ListViewItemStates.FOCUSED;
				}
			}

			internal bool OldFocused
			{
				get
				{
					return
						((APIsEnums.ListViewItemStates) uOldState & APIsEnums.ListViewItemStates.FOCUSED) ==
						APIsEnums.ListViewItemStates.FOCUSED;
				}
			}

			internal bool Select
			{
				get { return !OldSelected && NewSelected; }
			}

			internal bool UnSelect
			{
				get { return OldSelected && !NewSelected; }
			}

			internal bool Focus
			{
				get { return !OldFocused && NewFocused; }
			}

			internal bool UnFocus
			{
				get { return OldFocused && !NewFocused; }
			}

			internal bool Check
			{
				get { return !OldCheck && NewCheck; }
			}

			internal bool UnCheck
			{
				get { return OldCheck && !NewCheck; }
			}
		}

		#endregion

		#region HDITEM

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct HDITEM
		{
			internal APIsEnums.HeaderItemFlags mask;
			internal int cxy;
			internal IntPtr pszText;
			internal IntPtr hbm;
			internal int cchTextMax;
			internal int fmt;
			internal int lParam;
			internal int iImage;
			internal int iOrder;
		}

		#endregion

		#region POINTAPI

		[StructLayoutAttribute(LayoutKind.Sequential)]
		internal struct POINTAPI
		{
			internal POINTAPI(Point p)
			{
				x = p.X;
				y = p.Y;
			}

			internal POINTAPI(Int32 X, Int32 Y)
			{
				x = X;
				y = Y;
			}

			internal Int32 x;
			internal Int32 y;
		}

		#endregion

		#region RECT

		[StructLayout(LayoutKind.Sequential)]
		internal struct RECT
		{
			internal RECT(Rectangle rectangle)
			{
				left = rectangle.Left;
				top = rectangle.Top;
				right = rectangle.Right;
				bottom = rectangle.Bottom;
			}

			internal int left;
			internal int top;
			internal int right;
			internal int bottom;
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


		#region COMBOBOXINFO

		[StructLayout(LayoutKind.Sequential)]
			public struct COMBOBOXINFO 
		{
			public Int32 cbSize;
			public RECT rcItem;
			public RECT rcButton;
			public ComboBoxButtonState buttonState;
			public IntPtr hwndCombo;
			public IntPtr hwndItem;
			public IntPtr hwndList;
		}

		public enum ComboBoxButtonState 
		{
			STATE_SYSTEM_NONE = 0,
			STATE_SYSTEM_INVISIBLE = 0x00008000,
			STATE_SYSTEM_PRESSED = 0x00000008
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

	}
}