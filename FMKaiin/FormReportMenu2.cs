using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// Report 出力用メニュー2
	/// </summary>
	public partial class FormReportMenu2 : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormReportMenu2()
		{
			InitializeComponent();
		}

		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			btnHikiMeisai.Click += BtnHikiMeisai_Click;
			btnHikiIkaiMeisai.Click += BtnHikiIkaiMeisai_Click;
			btnHikiIkaiShukei.Click += BtnHikiIkaiShukei_Click;
			btnHikiKakunin.Click += BtnHikiKakunin_Click;
			btnMinyukin.Click += BtnMinyukin_Click;
			btnChoshuMeisai.Click += BtnChoshuMeisai_Click;
			btnHikiShukei.Click += BtnHikiShukei_Click;


			base.FormFrame_Shown(sender, e);
		}

		// ⑪会員別引落明細表
		private void BtnHikiMeisai_Click(object sender, EventArgs e)
		{
		}

		// ⑫所属医会別引落明細表
		private void BtnHikiIkaiMeisai_Click(object sender, EventArgs e)
		{
		}

		// ⑬所属医会別引落集計表
		private void BtnHikiIkaiShukei_Click(object sender, EventArgs e)
		{
		}

		// ⑭引落金額確認表
		private void BtnHikiKakunin_Click(object sender, EventArgs e)
		{
			// 画面呼び出し
			FormOutHikiotoshiKakunin frm = new FormOutHikiotoshiKakunin();
			frm.ShowDialog();

			frm.Dispose();
			frm = null;
		}

		// ⑮未入金者用請求書
		private void BtnMinyukin_Click(object sender, EventArgs e)
		{
			// 画面呼び出し
			FormOutSeikyuMinyukin frm = new FormOutSeikyuMinyukin();
			frm.ShowDialog();

			frm.Dispose();
			frm = null;
		}

		// ⑯年間会費徴収明細書
		private void BtnChoshuMeisai_Click(object sender, EventArgs e)
		{
			// 画面呼び出し
			FormOutChosyuMeisaiYear frm = new FormOutChosyuMeisaiYear();
			frm.ShowDialog();

			frm.Dispose();
			frm = null;
		}

		// ⑰引落口座別集計表
		private void BtnHikiShukei_Click(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncExcelMenu.Functions);

			funcKeys.On(FuncExcelMenu.Close, formClose);
		}

		void formClose()
		{
			this.Close();
		}
	}
}
