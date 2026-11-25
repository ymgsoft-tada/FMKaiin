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
	/// 引落金額確認表 印刷条件画面
	/// </summary>
	public partial class FormOutHikiotoshiKakunin:FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutHikiotoshiKakunin()
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
			iTaishoYM.UcValue = AppDate.ParseDateFirstDay(DateTime.Now); // 引落データは○月1日登録のため加工する

			// コンボの作成
			// 会員コード
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
			AppTableCombo.SetComboBox_Kaiin(iKaiinFrom, dv);
			AppTableCombo.SetComboBox_Kaiin(iKaiinTo, dv);

			// 支払区分
			AppCombo.SetComboBox(iShiharaiFrom, enumKbn.DShiharai);
			AppCombo.SetComboBox(iShiharaiTo, enumKbn.DShiharai);

			//■ イベントの登録

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncOutHikiotoshiKakunin.Functions);

			funcKeys.On(FuncOutHikiotoshiKakunin.Preview, preview);
			funcKeys.On(FuncOutHikiotoshiKakunin.Close, formClose);
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
				ReportFlexHikiotoshiKakunin rep = new ReportFlexHikiotoshiKakunin();
				rep.SelectedDateYM = Cast.DateTime(iTaishoYM.UcValue);
				rep.CreateView(iKaiinFrom.Value, iKaiinTo.Value, iShiharaiFrom.ExGetValue(iShiharaiFrom.SelectedIndex), iShiharaiTo.ExGetValue(iShiharaiTo.SelectedIndex));
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
