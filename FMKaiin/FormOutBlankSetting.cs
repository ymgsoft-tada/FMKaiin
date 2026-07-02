using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ComponentDB;
using ComponentRegistry;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// 帳票のマージン設定
	/// </summary>
	public partial class FormOutBlankSetting : Form
	{
		/// <summary>
		/// 帳票のグループ名（グループ名ごとに同じ余白設定を使う）
		/// </summary>
		string		blankGroupName;
		/// <summary>
		/// 横マージン
		/// </summary>
		int			marginH;
		/// <summary>
		/// 縦マージン
		/// </summary>
		int			marginV;
		/// <summary>
		/// キャンセルを押されたかを通知します
		/// </summary>
		bool		cancel = true;

		#region *** Property ***
		/// <summary>
		/// 横マージン
		/// </summary>
		public int	MarginH
		{
			get
			{
				return marginH;
			}
		}
		/// <summary>
		/// 縦マージン
		/// </summary>
		public int	MarginV
		{
			get
			{
				return marginV;
			}
		}
		/// <summary>
		/// キャンセルが押されたかを通知します。
		/// </summary>
		public bool Cancel
		{
			get
			{
				return cancel;
			}
		}
		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="_blankGroupName">帳票のグループ名（グループ名ごとに同じ余白設定を使う）</param>
		/// <param name="_marginH">横マージン</param>
		/// <param name="_marginV">縦マージン</param>
		public FormOutBlankSetting(string _blankGroupName, int _marginH, int _marginV)
		{
			InitializeComponent();
			
			blankGroupName	= _blankGroupName;
			marginH			= _marginH;
			marginV			= _marginV;
		}

		/// <summary>
		/// フォームロード
		/// </summary>
		private void Form_Load(object sender, EventArgs e)
		{
			hScroll.ValueChanged += new EventHandler(hScroll_ValueChanged);
			vScroll.ValueChanged += new EventHandler(vScroll_ValueChanged);

			picPaper.Paint += new PaintEventHandler(picPaper_Paint);
			
			butOK.Click += new EventHandler(butOK_Click);
			butCancel.Click += new EventHandler(butCancel_Click);
			butClear.Click += new EventHandler(butClear_Click);
			
			hScroll.Value = marginH;
			vScroll.Value = marginV;
		}
		
		void butOK_Click(object sender, EventArgs e)
		{
			// 設定された値の保存
			marginH = hScroll.Value;
			marginV = vScroll.Value;
			
			cancel = false;

			this.Close();
		}
		
		void butCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		void picPaper_Paint(object sender, PaintEventArgs e)
		{
			PictureBox	pbox = (PictureBox)sender;
			Bitmap		bmp0 = new Bitmap(imgList1.Images["MarginSet0"]);
			Bitmap		bmp1 = new Bitmap(imgList2.Images["MarginSet1"]);
			Bitmap		bmp2 = new Bitmap(imgList1.Images["MarginSet2"]);
			int			x, y;
			
			// pictureBoxの中心に
			x = (pbox.Width  - bmp0.Width)  / 2 - 2;
			y = (pbox.Height - bmp0.Height) / 2 - 2;
			
			e.Graphics.DrawImage(bmp0, x, y, bmp0.Width, bmp0.Height);
			
			// マージン設定によって移動する文字
			x  = (pbox.Width  - 96)  / 2 - 2;
			y  = (pbox.Height - 148) / 2 - 2;
			
			x += (hScroll.Value - 50) * 8 / 100;
			y += (vScroll.Value - 50) * 8 / 100;
			
			e.Graphics.DrawImage(bmp1, x, y, 96, 148);
			
			// pictureBoxの中心に
			x = (pbox.Width  - bmp2.Width)  / 2 - 2;
			y = (pbox.Height - bmp2.Height) / 2 - 2;
			
			e.Graphics.DrawImage(bmp2, x, y, bmp2.Width, bmp2.Height);
		}

		void vScroll_ValueChanged(object sender, EventArgs e)
		{
			// 1/100inch → mm に
			double	v = (double)(vScroll.Value - 50) * 0.254;
			lblV.Text = "縦　" + string.Format("{0:0.0}", v) + "mm";
			picPaper.Invalidate();
		}

		void hScroll_ValueChanged(object sender, EventArgs e)
		{
			// 1/100inch → mm に
			double	h = (double)(hScroll.Value - 50) * 0.254;
			lblH.Text = "横　" + string.Format("{0:0.0}", h) + "mm";
			picPaper.Invalidate();
		}

		void butClear_Click(object sender, EventArgs e)
		{
			hScroll.Value = 50;
			vScroll.Value = 50;
		}
	}
}