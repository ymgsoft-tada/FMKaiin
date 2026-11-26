using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentSUI;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// Report 出力用メニュー
	/// </summary>
	public partial class FormReportMenu1 : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormReportMenu1()
		{
			InitializeComponent();
		}

		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			btnKaiin.Click += BtnKaiin_Click;
			btnKaiinBank.Click += BtnKaiinBank_Click;
			btnKaiinIkai.Click += BtnKaiinIkai_Click;
			btnKaihiMonth.Click += BtnKaihiMonth_Click;
			btnKaihiYear.Click += BtnKaihiYear_Click;
			btnKaihiShubetsu.Click += BtnKaihiShubetsu_Click;
			btnKaihiShubetsuBank.Click += BtnKaihiShubetsuBank_Click;
			btnIryokikan.Click += BtnIryokikan_Click;
			btnIryokikanIchiran.Click += BtnIryokikanIchiran_Click;
			btnChoshuYotei.Click += BtnChoshuYotei_Click;

			base.FormFrame_Shown(sender, e);
		}

		// ①会員情報
		private void BtnKaiin_Click(object sender, EventArgs e)
		{
			//FormOutExcelFurikomiIrai frm = new FormOutExcelFurikomiIrai();
			//frm.ShowDialog();
			//frm.Dispose();
		}

		// ②会員一覧表（銀行名）
		private void BtnKaiinBank_Click(object sender, EventArgs e)
		{
			//FormOutExcelNofusho frm = new FormOutExcelNofusho();
			//frm.ShowDialog();
			//frm.Dispose();
		}

		// ③会員一覧表（参加医会）
		private void BtnKaiinIkai_Click(object sender, EventArgs e)
		{
			//FormOutExcelFurikomi frm = new FormOutExcelFurikomi();
			//frm.ShowDialog();
			//frm.Dispose();
		}

		// ④会費月別内訳
		private void BtnKaihiMonth_Click(object sender, EventArgs e)
		{
			// 画面呼び出し
			FormOutKaihiUchiwakeMonth frm = new FormOutKaihiUchiwakeMonth();
			frm.ShowDialog();

			frm.Dispose();
			frm = null;
		}

		// ⑤年会費内訳書
		private void BtnKaihiYear_Click(object sender, EventArgs e)
		{
			// 画面呼び出し
			FormOutKaihiUchiwakeYear frm = new FormOutKaihiUchiwakeYear();
			frm.ShowDialog();

			frm.Dispose();
			frm = null;
		}

		// ⑥会費種別一覧表
		private void BtnKaihiShubetsu_Click(object sender, EventArgs e)
		{
			// 画面呼び出し
			FormOutKaihiList frm = new FormOutKaihiList();
			frm.ShowDialog();

			frm.Dispose();
			frm = null;
		}

		// ⑦会費種別一覧表（銀行有）
		private void BtnKaihiShubetsuBank_Click(object sender, EventArgs e)
		{
			// 画面呼び出し
			FormOutKaihiListBank frm = new FormOutKaihiListBank();
			frm.ShowDialog();

			frm.Dispose();
			frm = null;
		}

		// ⑧医療機関情報
		private void BtnIryokikan_Click(object sender, EventArgs e)
		{
			//FormOutExcelShimingyoinKaihi frm = new FormOutExcelShimingyoinKaihi();
			//frm.ShowDialog();
			//frm.Dispose();
		}

		// ⑨医療機関一覧表
		private void BtnIryokikanIchiran_Click(object sender, EventArgs e)
		{
			//FormOutExcelShimingyoinKaihi frm = new FormOutExcelShimingyoinKaihi();
			//frm.ShowDialog();
			//frm.Dispose();
		}

		// ⑩年間会費徴収予定通知書
		private void BtnChoshuYotei_Click(object sender, EventArgs e)
		{
			// 画面呼び出し
			FormOutKaihiYoteiYear frm = new FormOutKaihiYoteiYear();
			frm.ShowDialog();

			frm.Dispose();
			frm = null;
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
