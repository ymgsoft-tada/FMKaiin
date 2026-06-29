using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
	/// <summary>
	/// 
	/// </summary>
	public static class WindowsConst
	{
		/// <summary></summary>
		public const int WM_NCHITTEST		 = 0x0084;

		/*
		 * Window Styles
		 */
		/// <summary></summary>
		public const int	WS_OVERLAPPED	= 0x00000000;
		/// <summary></summary>
		public const long	WS_POPUP		= 0x80000000L; //unchecked((int) WS_POPUP) 
		/// <summary></summary>
		public const int	WS_CHILD		= 0x40000000;
		/// <summary></summary>
		public const int	WS_MINIMIZE		= 0x20000000;
		/// <summary></summary>
		public const int	WS_VISIBLE		= 0x10000000;
		/// <summary></summary>
		public const int	WS_DISABLED		= 0x08000000;
		/// <summary></summary>
		public const int	WS_CLIPSIBLINGS = 0x04000000;
		/// <summary></summary>
		public const int	WS_CLIPCHILDREN = 0x02000000;
		/// <summary></summary>
		public const int	WS_MAXIMIZE		= 0x01000000;
		/// <summary></summary>
		public const int	WS_CAPTION		= 0x00C00000;/* WS_BORDER | WS_DLGFRAME	*/
		/// <summary></summary>
		/// <summary></summary>
		public const int	WS_BORDER		= 0x00800000;
		/// <summary></summary>
		public const int	WS_DLGFRAME		= 0x00400000;
		/// <summary></summary>
		public const int	WS_VSCROLL		= 0x00200000;
		/// <summary></summary>
		public const int	WS_HSCROLL		= 0x00100000;
		/// <summary></summary>
		public const int	WS_SYSMENU		= 0x00080000;
		/// <summary></summary>
		public const int	WS_THICKFRAME	= 0x00040000;
		/// <summary></summary>
		public const int	WS_GROUP		= 0x00020000;
		/// <summary></summary>
		public const int	WS_TABSTOP		= 0x00010000;

		/// <summary></summary>
		public const int	WS_MINIMIZEBOX	= 0x00020000;
		/// <summary></summary>
		public const int	WS_MAXIMIZEBOX	= 0x00010000;

		/// <summary></summary>
		public const int	WS_TILED		= WS_OVERLAPPED;
		/// <summary></summary>
		public const int	WS_ICONIC		= WS_MINIMIZE;
		/// <summary></summary>
		public const int	WS_SIZEBOX		= WS_THICKFRAME;
		/// <summary></summary>
		public const int	WS_TILEDWINDOW	= WS_OVERLAPPEDWINDOW;

		/*
		 * Common Window Styles
		*/
		/// <summary></summary>
		public const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED |
													WS_CAPTION |
													WS_SYSMENU |
													WS_THICKFRAME |
													WS_MINIMIZEBOX |
													WS_MAXIMIZEBOX);

		/// <summary></summary>
		public const long WS_POPUPWINDOW = (WS_POPUP |
													WS_BORDER |
													WS_SYSMENU);

		/// <summary></summary>
		public const int WS_CHILDWINDOW = (WS_CHILD);


		/*
		 * Extended Window Styles
		 */
		/// <summary></summary>
		public const int WS_EX_DLGMODALFRAME = 0x00000001;
		/// <summary></summary>
		public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
		/// <summary></summary>
		public const int WS_EX_TOPMOST = 0x00000008;
		/// <summary></summary>
		public const int WS_EX_ACCEPTFILES = 0x00000010;
		/// <summary></summary>
		public const int WS_EX_TRANSPARENT = 0x00000020;
		/// <summary></summary>
		/// <summary></summary>
		public const int WS_EX_MDICHILD = 0x00000040;
		/// <summary></summary>
		public const int WS_EX_TOOLWINDOW = 0x00000080;
		/// <summary></summary>
		public const int WS_EX_WINDOWEDGE = 0x00000100;
		/// <summary></summary>
		public const int WS_EX_CLIENTEDGE = 0x00000200;
		/// <summary></summary>
		public const int WS_EX_CONTEXTHELP = 0x00000400;

		/// <summary></summary>
		public const int WS_EX_RIGHT = 0x00001000;
		/// <summary></summary>
		public const int WS_EX_LEFT = 0x00000000;
		/// <summary></summary>
		public const int WS_EX_RTLREADING = 0x00002000;
		/// <summary></summary>
		public const int WS_EX_LTRREADING = 0x00000000;
		/// <summary></summary>
		public const int WS_EX_LEFTSCROLLBAR = 0x00004000;
		/// <summary></summary>
		public const int WS_EX_RIGHTSCROLLBAR = 0x00000000;

		/// <summary></summary>
		public const int WS_EX_CONTROLPARENT = 0x00010000;
		/// <summary></summary>
		public const int WS_EX_STATICEDGE = 0x00020000;
		/// <summary></summary>
		public const int WS_EX_APPWINDOW = 0x00040000;


		/// <summary></summary>
		public const int WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
		/// <summary></summary>
		public const int WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
		/// <summary></summary>
		public const int WS_EX_LAYERED = 0x00080000;

		/// <summary></summary>
		public const int WS_EX_NOINHERITLAYOUT = 0x00100000; 
			// Disable inheritence of mirroring by children
		/// <summary></summary>
		public const int WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
		/// <summary></summary>
		public const int WS_EX_COMPOSITED = 0x02000000;
		/// <summary></summary>
		public const int WS_EX_NOACTIVATE = 0x08000000;

		/*
		 * WM_NCHITTEST and MOUSEHOOKSTRUCT Mouse Position Codes
		 */
		/// <summary></summary>
		public const int HTERROR = (-2);
		/// <summary></summary>
		public const int HTTRANSPARENT = (-1);
		/// <summary></summary>
		public const int HTNOWHERE = 0;
		/// <summary></summary>
		public const int HTCLIENT = 1;
		/// <summary></summary>
		public const int HTCAPTION = 2;
		/// <summary></summary>
		public const int HTSYSMENU = 3;
		/// <summary></summary>
		public const int HTGROWBOX = 4;
		/// <summary></summary>
		public const int HTSIZE = HTGROWBOX;
		/// <summary></summary>
		public const int HTMENU = 5;
		/// <summary></summary>
		public const int HTHSCROLL = 6;
		/// <summary></summary>
		public const int HTVSCROLL = 7;
		/// <summary></summary>
		public const int HTMINBUTTON = 8;
		/// <summary></summary>
		public const int HTMAXBUTTON = 9;
		/// <summary></summary>
		public const int HTLEFT = 10;
		/// <summary></summary>
		/// <summary></summary>
		public const int HTRIGHT = 11;
		/// <summary></summary>
		public const int HTTOP = 12;
		/// <summary></summary>
		public const int HTTOPLEFT = 13;
		/// <summary></summary>
		public const int HTTOPRIGHT = 14;
		/// <summary></summary>
		public const int HTBOTTOM = 15;
		/// <summary></summary>
		public const int HTBOTTOMLEFT = 16;
		/// <summary></summary>
		public const int HTBOTTOMRIGHT = 17;
		/// <summary></summary>
		public const int HTBORDER = 18;
		/// <summary></summary>
		public const int HTREDUCE = HTMINBUTTON;
		/// <summary></summary>
		public const int HTZOOM = HTMAXBUTTON;
		/// <summary></summary>
		public const int HTSIZEFIRST = HTLEFT;
		/// <summary></summary>
		public const int HTSIZELAST = HTBOTTOMRIGHT;

		/// <summary></summary>
		public const int HTOBJECT = 19;
		/// <summary></summary>
		public const int HTCLOSE = 20;
		/// <summary></summary>
		public const int HTHELP = 21;
	}
}
