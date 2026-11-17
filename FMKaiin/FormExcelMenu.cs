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
	/// [作成者 kj]
	/// Excel 出力用メニュー
	/// </summary>
	public partial class FormExcelMenu : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormExcelMenu()
		{
			InitializeComponent();
		}

		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			btnShiminbyoinKaihi.Click += BtnShiminbyoinKaihi_Click;
			btnNenkanChoshu.Click += BtnNenkanChoshu_Click;
			btnKaihiShinsei.Click += BtnKaihiShinsei_Click;
			btnFurikomi.Click += BtnFurikomi_Click;
			btnNofusho.Click += BtnNofusho_Click;
			button2.Click += BtnFurikomiIrai_Click;

			base.FormFrame_Shown(sender, e);
		}

		private void BtnFurikomiIrai_Click(object sender, EventArgs e)
		{
			FormOutExcelFurikomiIrai frm = new FormOutExcelFurikomiIrai();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void BtnNofusho_Click(object sender, EventArgs e)
		{
			FormOutExcelNofusho frm = new FormOutExcelNofusho();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void BtnFurikomi_Click(object sender, EventArgs e)
		{
			FormOutExcelFurikomi frm = new FormOutExcelFurikomi();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void BtnKaihiShinsei_Click(object sender, EventArgs e)
		{
			FormOutExcelKaihiShinsei frm = new FormOutExcelKaihiShinsei();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void BtnNenkanChoshu_Click(object sender, EventArgs e)
		{
			FormOutExcelNenkanChoshu frm = new FormOutExcelNenkanChoshu();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void BtnShiminbyoinKaihi_Click(object sender, EventArgs e)
		{
			FormOutExcelShimingyoinKaihi frm = new FormOutExcelShimingyoinKaihi();
			frm.ShowDialog();
			frm.Dispose();
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncExcelMenu.Functions);

			FuncExcelMenu.Close.Execute = formClose;
		}

		void formClose()
		{
			this.Close();
		}
	}
}
