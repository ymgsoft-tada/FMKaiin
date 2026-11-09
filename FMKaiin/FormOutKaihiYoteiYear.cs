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
	/// 年間会費徴収予定通知書 印刷条件画面
	/// </summary>
	public partial class FormOutKaihiYoteiYear:FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutKaihiYoteiYear()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{

			// 年月初期値:当日
			iTaishoYM.UcFormat = UcDate.UcDateFormat.YYYYMM;
			iTaishoYM.UcDateCtl.DropDownCalendar.CalendarType = GrapeCity.Win.Editors.CalendarType.YearMonth;
			iTaishoYM.UcValue = DateTime.Now;
			
			// コンボの作成
			// 会員コード
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
			AppTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);
			AppTableCombo.SetComboBox_Kaiin(iKaiinTo, dv);

			// 在籍区分
			AppCombo.SetComboBox(iZaisekiFrom, enumKbn.DTypeZaiseki);
			AppCombo.SetComboBox(iZaisekiTo, enumKbn.DTypeZaiseki);

			// 医療機関コード
			DBView dv_iryo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_iryokikan));
			AppTableCombo.SetComboBox_IryoKikan(iIryoFrom, dv_iryo);
			AppTableCombo.SetComboBox_IryoKikan(iIryoTo, dv_iryo);

			//■ イベントの登録

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncOutKaihiYoteiYear.Functions);

			FuncOutKaihiYoteiYear.Preview.Execute = preview;
			FuncOutKaihiYoteiYear.Close.Execute = formClose;
		}

		/// <summary>
		/// 検証処理
		/// </summary>
		/// <returns></returns>
		bool validateRow()
		{
			// 空白チェック
			Control[] ctls =
			{
				iTaishoYM,
			};

			foreach (Control ctl in ctls)
			{
				int len = ctl.Text.Length;

				if (ctl is UcDate)
				{
					UcDate tmp = (UcDate)ctl; // プロパティ指定のためいったん変換して格納
					if (tmp.UcValue == null) // 日付変換できない場合UcValueはnull
					{
						ctl.Select();
						appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
						return false;
					}
				}
				else
				{
					if (ctl is UcTableComboBox)
					{
						len = ((UcTableComboBox)ctl).Text.Length;
					}

					if (len == 0)
					{
						ctl.Select();
						appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// プレビュー
		/// </summary>
		void preview()
		{
			if (validateRow() == true)
			{
				ReportFlexKaihiYoteiYear rep = new ReportFlexKaihiYoteiYear();
				rep.SelectedDateYM = Cast.DateTime(iTaishoYM.UcValue);
				rep.CreateView(iKaiinFrom.Value, iKaiinTo.Value, iZaisekiFrom.ExGetValue(iZaisekiFrom.SelectedIndex), iZaisekiTo.ExGetValue(iZaisekiTo.SelectedIndex), iIryoFrom.Value, iIryoTo.Value);
				rep.ReportExec();

				rep = null;
			}
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
