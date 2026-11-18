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
	/// 納付書
	/// </summary>
	public partial class FormOutExcelNofusho : FormFrame
	{
		/// <summary>処理日の初期値にする日（月分の翌月）</summary>
		const int ShoribiDay = 15;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutExcelNofusho()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncOutExcelNofusho.Functions);

			iDateYM.UcValue = AppDate.ParseDateFirstDay(DateTime.Now);
			setShoribi();

			rdoNichii.Checked = true;

			iDateYM.UcValueChanged += IDateYM_UcValueChanged;

			FuncOutExcelNofusho.OutExcel.Execute = doExec;
			FuncOutExcelNofusho.Close.Execute = formClose;
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

			//¶ 2026/11/16 CC 処理日は月分の翌月15日。土日なら次の月曜にする。祝日は判定できないので手で直す（総合振込と同じ）
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

			eTypeIkai ikai = eTypeIkai.Shii;
			if (rdoNichii.Checked == true)
			{
				ikai = eTypeIkai.Nichii;
			}
			else if (rdoKeni.Checked == true)
			{
				ikai = eTypeIkai.Keni;
			}

			AppExcelNofusho xls = new AppExcelNofusho(ym, iDate.UcValue.Value, ikai);

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

		void formClose()
		{
			this.Close();
		}
	}
}
