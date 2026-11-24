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
	/// 振込依頼書
	/// </summary>
	public partial class FormOutExcelFurikomiIrai : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutExcelFurikomiIrai()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelFurikomiIrai.Functions);

			iDateYM.UcValue = AppDate.ParseDateFirstDay(DateTime.Now);
			setShoribi();

			iDateYM.UcValueChanged += IDateYM_UcValueChanged;

			funcKeys.On(FuncOutExcelFurikomiIrai.OutExcel, doExec);
			funcKeys.On(FuncOutExcelFurikomiIrai.Close, formClose);
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

			//¶ 2026/11/16 CC 過去の月の送金指定日と同じ計算を使う（翌月15日。土日なら次の月曜）
			iDate.UcValue = AppExcelFurikomiIrai.GetDefaultShoribi(iDateYM.UcValue.Value);
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

			AppExcelFurikomiIrai xls = new AppExcelFurikomiIrai(ym, iDate.UcValue.Value);

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
