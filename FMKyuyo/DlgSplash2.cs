using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace App
{
	/// <summary></summary>
	public partial class DlgSplash2 : Form
	{
		/// <summary></summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		/// <summary></summary>
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		/// <summary></summary>
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		/// <summary></summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		/// <summary></summary>
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DeleteObject(IntPtr hobject);

		/// <summary></summary>
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DeleteDC(IntPtr hdc);

		/// <summary></summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int UpdateLayeredWindow(
				IntPtr hwnd,
				IntPtr hdcDst,
				[System.Runtime.InteropServices.In()]
						ref Point pptDst,
				[System.Runtime.InteropServices.In()]
						ref Size psize,
				IntPtr hdcSrc,
				[System.Runtime.InteropServices.In()]
						ref Point pptSrc,
				int crKey,
				[System.Runtime.InteropServices.In()]
						ref BLENDFUNCTION pblend,
				int dwFlags);

		/// <summary></summary>
		public const byte AC_SRC_OVER = 0;
		/// <summary></summary>
		public const byte AC_SRC_ALPHA = 1;
		/// <summary></summary>
		public const int ULW_ALPHA = 2;

		/// <summary></summary>
		public const int WM_DISPLAYCHANGE		= 0x007E;

		/// <summary></summary>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BLENDFUNCTION
		{
			/// <summary></summary>
			public byte BlendOp;
			/// <summary></summary>
			public byte BlendFlags;
			/// <summary></summary>
			public byte SourceConstantAlpha;
			/// <summary></summary>
			public byte AlphaFormat;
		}

		/// <summary></summary>
		public DlgSplash2()
		{
			InitializeComponent();
			
			Bitmap bmp = null;
			
//			bmp = Properties.Resources.splash;
			
			this.BackgroundImage = bmp;

			this.Height = bmp.Height;
			this.Width	= bmp.Width;
			
			this.Load += DlgSplash2_Load;
		}

		private void DlgSplash2_Load(object sender, EventArgs e)
		{
			UpdateFormDisplay(this.BackgroundImage);
		}

		/// <summary></summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;

				cp.ExStyle = cp.ExStyle | WindowsConst.WS_EX_LAYERED;
				if ((cp.ExStyle & WindowsConst.WS_EX_TRANSPARENT) != 0){
					cp.ExStyle = cp.ExStyle & WindowsConst.WS_EX_TRANSPARENT;
				}
				return cp;
			}
		}

		/// <summary></summary>
		public void UpdateFormDisplay(Image backgroundImage)
		{
			IntPtr screenDc = GetDC(IntPtr.Zero);
			IntPtr memDc = CreateCompatibleDC(screenDc);
			IntPtr hBitmap = IntPtr.Zero;
			IntPtr oldBitmap = IntPtr.Zero;
			 
			try {
				//Display-image
				Bitmap bmp = new Bitmap(backgroundImage);
				
				Brush br = Brushes.WhiteSmoke;


				using (var g = Graphics.FromImage(bmp))
				{
					Font fnt = new Font("Arial", 11f);
					g.DrawString($"Version {AppConst.AppVersion}", fnt, br, 320, 177);
					fnt = new Font("Arial", 9f);
					g.DrawString("Copyright(C) YMGSoft All Rights Reserved.", fnt, br, 250, 275);
					fnt.Dispose();
				}

				hBitmap = bmp.GetHbitmap(Color.FromArgb(0));
				oldBitmap = SelectObject(memDc, hBitmap);

				//Display-rectangle
				Size size = bmp.Size;
				Point pointSource = new Point(0, 0);
				Point topPos = new Point(this.Left, this.Top);

				//Set up blending options
				BLENDFUNCTION blend = new BLENDFUNCTION();
				blend.BlendOp = AC_SRC_OVER;
				blend.BlendFlags = 0;
				blend.SourceConstantAlpha = 255;
				blend.AlphaFormat = AC_SRC_ALPHA;

				UpdateLayeredWindow(this.Handle, screenDc,
					ref topPos, ref size, memDc, ref pointSource, 0, ref blend, ULW_ALPHA);

				//Clean-up
				bmp.Dispose();

				ReleaseDC(IntPtr.Zero, screenDc);
				if (hBitmap != IntPtr.Zero) {
					SelectObject(memDc, oldBitmap);
					DeleteObject(hBitmap);
				}
				DeleteDC(memDc);
			}
			catch (Exception) {
			}
		}
	}
	
}
