using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentDB;
using ComponentGControlDB;
using ComponentIO;
using GControlGcTextBoxEx;

namespace App
{
	/// <summary>
	/// 年会費内訳書 印刷条件画面
	/// </summary>
	public partial class FormOutKaihiUchiwakeYear:FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutKaihiUchiwakeYear()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// コンボの作成
			// 会員コード
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
			AppTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);
			AppTableCombo.SetComboBox_Kaiin(iKaiinTo, dv);

			// 在籍区分
			AppCombo.SetComboBox(iZaisekiFrom, enumKbn.DTypeZaiseki);
			AppCombo.SetComboBox(iZaisekiTo, enumKbn.DTypeZaiseki);

			//■ イベントの登録

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncOutKaihiUchiwakeYear.Functions);

			FuncOutKaihiUchiwakeYear.Preview.Execute = preview;
			FuncOutKaihiUchiwakeYear.Close.Execute = formClose;
		}

		/// <summary>
		/// 検証処理
		/// </summary>
		/// <returns></returns>
		//bool validateRow()
		//{
		//	return true;
		//}

		/// <summary>
		/// プレビュー
		/// </summary>
		void preview()
		{
			//if (validateRow() == true)
			//{

			ReportFlexKaihiUchiwakeYear rep = new ReportFlexKaihiUchiwakeYear();
			rep.CreateView(iKaiinFrom.Value, iKaiinTo.Value);
			rep.ReportExec();

			rep = null;

			//}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		void formClose()
		{
			this.Close();
		}
	}
}
