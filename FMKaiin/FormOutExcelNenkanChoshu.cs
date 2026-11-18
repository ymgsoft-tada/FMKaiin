using ComponentDB;
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
	/// 年間会費徴収書類
	/// </summary>
	public partial class FormOutExcelNenkanChoshu : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutExcelNenkanChoshu()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			//¶ 2026/11/16 CC 会員コンボの作成
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
			AppTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncOutExcelNenkanChoshu.Functions);

			iYear.UcValue = AppDate.ParseDateFirstDay(DateTime.Now);

			rdoYotei.Checked = true;

			FuncOutExcelNenkanChoshu.OutExcel.Execute = doExec;
			FuncOutExcelNenkanChoshu.Close.Execute = formClose;
		}

		void doExec()
		{
			if (iYear.UcValue == null)
			{
				appToolTip.Show(iYear, AppToolTipIndex.CannotUseBlank);
				iYear.Focus();
				return;
			}

			//¶ 2026/11/16 CC 会員の指定は必須
			Kaiin ka = AppGlobal.Kaiins.Get(Cast.Int(iKaiinFrom.Value));
			if (ka == null)
			{
				appToolTip.Show(iKaiinFrom, AppToolTipIndex.CannotUseBlank);
				iKaiinFrom.Focus();
				return;
			}

			int year = iYear.UcValue.Value.Year;
			AppExcelNenkanChoshu.eType type = (rdoYotei.Checked == true) ? AppExcelNenkanChoshu.eType.Yotei : AppExcelNenkanChoshu.eType.Meisai;

			//¶ 2026/11/16 CC 出力は1人ずつ。複数人に広げる場合は、このリストに会員を足す
			List<Kaiin> kaiins = new List<Kaiin>();
			kaiins.Add(ka);

			//¶ 2026/11/16 CC 銀行名の取得に銀行コードを使う。未ロードならここでロードする
			initBankCode();

			AppExcelNenkanChoshu xls = new AppExcelNenkanChoshu(year, type, kaiins);

			switch (xls.Execute())
			{
				case AppExcel.eResult.Success:
					AppMsgBox.Show(this, AppMsgBoxIndex.CreateExcelFile);
					break;
				case AppExcel.eResult.Cancel:
					break;
				case AppExcel.eResult.NotExistTemplate:
					AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
					break;
				case AppExcel.eResult.NotFoundData:
					AppMsgBox.Show(this, AppMsgBoxIndex.NotFoundDataExport);
					break;
				default:
					AppMsgBox.Show(this, AppMsgBoxIndex.FailedExcelFile);
					break;
			}
		}

		/// <summary>
		/// 銀行コードデータの取得処理
		/// </summary>
		void initBankCode()
		{
			//¶ 2026/11/16 CC 銀行コードは件数が多く、使う画面を開くときにロードされる（Form1と同じ手順）
			if (AppGlobal.BankCodeMg == null)
			{
				FormBg_Progress prog = new FormBg_Progress();
				prog.TitleText = "しばらくお待ちください。";
				prog.LabelText = "データをロードしています。";
				prog.DoWorkEvent += prog_DoWorkEvent;
				prog.ShowDialog();
				prog.Dispose();
				prog = null;
			}
		}

		private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;
			prog.AdvanceProgress(100);

			AppGlobal.DB.GetReFillTable(TableProp.t_bank_code);
			AppGlobal.InitBankCodeMg();
			AppGlobal.Banks.Init();
		}

		void formClose()
		{
			this.Close();
		}
	}
}
