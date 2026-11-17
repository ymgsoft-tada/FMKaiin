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
	/// 振込データ一覧
	/// </summary>
	public partial class FormOutExcelFurikomi : FormFrame
	{
		/// <summary>処理日の初期値にする日（月分の翌月）</summary>
		const int ShoribiDay = 15;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutExcelFurikomi()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncOutExcelFurikomi.Functions);

			iDateYM.UcValue = AppDate.ParseDateFirstDay(DateTime.Now);
			setShoribi();

			iDateYM.UcValueChanged += IDateYM_UcValueChanged;

			FuncOutExcelFurikomi.OutExcel.Execute = doExec;
			FuncOutExcelFurikomi.Close.Execute = formClose;
		}

		private void IDateYM_UcValueChanged(object sender, EventArgs e)
		{
			setShoribi();
		}

		/// <summary>
		/// 月分から処理日の初期値をセットします。
		/// </summary>
		void setShoribi()
		{
			if (iDateYM.UcValue == null)
			{
				return;
			}

			//¶ 2026/11/16 CC 処理日は月分の翌月15日。土日なら次の月曜にする。祝日は判定できないので手で直す
			DateTime next = AppDate.ParseDateFirstDay(iDateYM.UcValue.Value).AddMonths(1);
			DateTime date = new DateTime(next.Year, next.Month, ShoribiDay);

			if (date.DayOfWeek == DayOfWeek.Saturday)
			{
				date = date.AddDays(2);
			}
			else if (date.DayOfWeek == DayOfWeek.Sunday)
			{
				date = date.AddDays(1);
			}

			iDate.UcValue = date;
		}

		void doExec()
		{
			if (iDateYM.UcValue == null)
			{
				appToolTip.Show(iDateYM, AppToolTipIndex.CannotUseBlank);
				iDateYM.Focus();
				return;
			}
			if (iDate.UcValue == null)
			{
				appToolTip.Show(iDate, AppToolTipIndex.CannotUseBlank);
				iDate.Focus();
				return;
			}

			DateTime ym = AppDate.ParseDateFirstDay(iDateYM.UcValue.Value);

			//¶ 2026/11/16 CC 銀行名の取得に銀行コードを使う。未ロードならここでロードする
			initBankCode();

			AppExcelFurikomi xls = new AppExcelFurikomi(ym, iDate.UcValue.Value);

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
