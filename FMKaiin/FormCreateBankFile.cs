using ComponentDebug;
using ComponentIO;
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
	/// FB（銀行用引落）データ作成
	/// </summary>
	public partial class FormCreateBankFile : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormCreateBankFile()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			AppTableCombo.SetComboBox_Bank(iBank);

			DateTime date = AppDate.ParseDateFirstDay(DateTime.Now);

			iDateYM.UcValue = date;
			iBank.ComboBox.ExSetSelectedIndexByValue(AppGlobal.Basic.ID_Bank);
			
			// 出力日は処理月度の翌月25日
			iDate.UcValue = new DateTime(date.Year, date.Month, 25);

			AppDbRule.SetControlByRule(iDate.UcDateCtl, t_basic.FBAS_DateYM);
			iDate.Select();

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの登録
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncCreateBankFile.Functions);

			FuncCreateBankFile.Exec.Execute = doExec;
			FuncCreateBankFile.Close.Execute = formClose;
		}

		void doExec()
		{
			// 空白チェック
			if (iBank.Text == "")
			{
				iBank.Select();
				appToolTip.Show(iBank, AppToolTipIndex.CannotUseBlank);
				return;
			}
			if (iDateYM.UcValue == null)
			{
				iDateYM.Select();
				appToolTip.Show(iDateYM, AppToolTipIndex.CannotUseBlank);
				return;
			}
			if (iDate.UcValue == null)
			{
				iDate.Select();
				appToolTip.Show(iDate, AppToolTipIndex.CannotUseBlank);
				return;
			}

			DateTime ym = AppDate.ParseDateFirstDay(iDateYM.UcValue.Value);

			Bank bank = AppGlobal.Banks.Get(Cast.Int(iBank.Value));

			AppBankFile abf = new AppBankFile();
			abf.ShowSaveFilePath();

			// 実行処理
			AppBankFile.eResut ret = abf.CreateHikiotoshi(ym, bank, iDate.UcValue.Value);

			switch(ret)
			{
				case AppBankFile.eResut.DataNotFound :
				case AppBankFile.eResut.Success :
				case AppBankFile.eResut.CannotFileSave :
				case AppBankFile.eResut.UnmatchData :
					// 結果画面を表示
					FormLogView frm = new FormLogView();
					frm.SelectedYM = ym;
					frm.Table = abf.Logs.GetTable();
					frm.TitleText = "FBデータ作成結果";
					frm.ShowDialog();
					break;
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
