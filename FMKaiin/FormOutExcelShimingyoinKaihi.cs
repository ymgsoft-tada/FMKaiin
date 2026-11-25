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
	/// 市民病院会費請求
	/// </summary>
	public partial class FormOutExcelShimingyoinKaihi : FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutExcelShimingyoinKaihi()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelShimingyoinKaihi.Functions);

			iYear.UcValue = AppDate.ParseDateFirstDay(DateTime.Now);

			iPiriod.ExBeginUpdate();
			iPiriod.ExAddItem("Ⅰ期",1);
			iPiriod.ExAddItem("Ⅱ期",2);
			iPiriod.ExAddItem("Ⅲ期",3);
			iPiriod.ExEndUpdate();
			iPiriod.ExSetSelectedIndexByValue(1);

			rdoKen.Checked = true;

			funcKeys.On(FuncOutExcelShimingyoinKaihi.OutExcel, doExec);
			funcKeys.On(FuncOutExcelShimingyoinKaihi.Close, formClose);
		}

		void doExec()
		{
			if (iYear.UcValue == null)
			{
				appToolTip.Show(iYear, AppToolTipIndex.CannotUseBlank);
				iYear.Focus();
				return;
			}

			int year = iYear.UcValue.Value.Year;
			int period = Cast.Int(iPiriod.ExGetValue());
			eTypeIkai ikai = (rdoKen.Checked == true) ? eTypeIkai.Keni : eTypeIkai.Shii;

			AppExcelShiminByoinKaihi xls = new AppExcelShiminByoinKaihi(year, period, ikai);

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
