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
	/// 所属医会別引落明細表 印刷条件画面
	/// </summary>
	public partial class FormOutHikiotoshiIkaiMeisai:FormFrame
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormOutHikiotoshiIkaiMeisai()
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
			// 医会コード
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaihi));
			AppTableCombo.SetComboBox_Kaihi(iIkaiFrom, dv);
			AppTableCombo.SetComboBox_Kaihi(iIkaiTo, dv);

			// 金額欄の入力ルール
			AppDbRule.SetControlByRule(iCostFrom, t_hikiotoshi.FHiki_Cost);
			AppDbRule.SetControlByRule(iCostTo, t_hikiotoshi.FHiki_Cost);

			//■ イベントの登録

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncOutHikiotoshiIkaiMeisai.Functions);

			FuncOutHikiotoshiIkaiMeisai.Preview.Execute = preview;
			FuncOutHikiotoshiIkaiMeisai.Close.Execute = formClose;
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
				ReportFlexHikiotoshiIkaiMeisai rep = new ReportFlexHikiotoshiIkaiMeisai();
				rep.SelectedDateYM = Cast.DateTime(iTaishoYM.UcValue);
				rep.CreateView(iIkaiFrom.Value, iIkaiTo.Value, iCostFrom.Text, iCostTo.Text);
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
