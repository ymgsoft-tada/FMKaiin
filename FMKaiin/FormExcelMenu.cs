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

			base.FormFrame_Shown(sender, e);
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
