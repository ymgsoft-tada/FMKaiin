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
	/// 会費種別一覧表（銀行有） 印刷条件画面
	/// </summary>
	public partial class FormOutKaihiListBank:FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutKaihiListBank()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			// コンボの作成
			// 会費区分コード
			AppCombo.SetComboBox(iTypeKaihiFrom, enumKbn.DTypeKaihi);
			AppCombo.SetComboBox(iTypeKaihiTo, enumKbn.DTypeKaihi);

			// 会費コード
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaihi));
			AppTableCombo.SetComboBox_Kaihi(iKaihiFrom, dv);
			AppTableCombo.SetComboBox_Kaihi(iKaihiTo, dv);

			//■ イベントの登録

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncOutKaihiListBank.Functions);

			FuncOutKaihiListBank.Preview.Execute = preview;
			FuncOutKaihiListBank.Close.Execute = formClose;
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

			ReportFlexKaihiListBank rep = new ReportFlexKaihiListBank();
			rep.CreateView(iTypeKaihiFrom.ExGetValue(iTypeKaihiFrom.SelectedIndex), iTypeKaihiTo.ExGetValue(iTypeKaihiTo.SelectedIndex), iKaihiFrom.Value, iKaihiTo.Value);
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
