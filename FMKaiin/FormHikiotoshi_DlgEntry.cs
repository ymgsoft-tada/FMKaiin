using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentGControlDB;
using ComponentGGridDB;
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
	/// 引落データ編集画面
	/// </summary>
	public partial class FormHikiotoshi_DlgEntry : FormFrame
	{
		/// <summary>選択中の月度</summary>
		public DateTime SelectYM { get; set; }
		/// <summary>選択中の会員情報</summary>
		public Kaiin SelectKaiin { get; set; }

		DBView dvHiki;

		GGridDBCommon gcom;
		GControlDB gctl;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormHikiotoshi_DlgEntry()
		{
			InitializeComponent();
		}

		/// <summary>
		/// キー操作
		/// </summary>
		protected override void FormFrame_KeyDown(object sender, KeyEventArgs e)
		{
			//■ ファンクションキーの標準ショートカットの抑止
			e.Handled = base.Patcher_CheckShortcutForKillingWindowsAction(e.KeyCode, e.Alt);

			// 処理が必要かどうかチェック。
			if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
			{
				return;
			}
			if (this.ActiveControl == null)
			{
				return;
			}

			if (e.KeyCode == Keys.Enter)
			{
				if (gcom.CheckFocusControl(this.ActiveControl) == false)
				{
					gcom.SelectInput();
				}
				else
				{
					gcom.SelectNextControl(!e.Shift);
				}
			}
			else
			if (e.KeyCode == Keys.Escape)
			{
				if (gcom.CheckFocusControl(this.ActiveControl) == true)
				{
					grid.Select();
				}
			}
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			dvHiki = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_hikiotoshi), this.BindingContext);
			dvHiki.RowFilterQuery($"{t_hikiotoshi.FID_Kaiin} = {SelectKaiin.ID} AND {t_hikiotoshi.FHiki_DateYM} = #{SelectYM}#");

			iDateYM.Text = AppDate.GetYearMonth(SelectYM);
			iKaiin.Text = $"{SelectKaiin.CD} {SelectKaiin.XRow.Kaiin_Name}";

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_hikiotoshi.FID_Kaihi, "参加医会",180));
			gcom.SetUnboundColumnFetch(ubIkai);
			gcom.SetLocked(true);
			gcom.Add(new GGridDBCurrency(t_hikiotoshi.FHiki_Cost, "金額", 150));
			gcom.SetFocusControlInGrid(iCost);
			gcom.SetTabIndex(0);
			gcom.Add(new GGridDBText(t_hikiotoshi.FHiki_Memo, "備考", 0));
			gcom.SetFocusControlInGrid(iMemo);
			gcom.SetTabIndex(1);
			gcom.EndAdd(dvHiki);

			gctl = new GControlDB(this, getDataRow);
			gctl.Add(new GControlDBNumber(t_hikiotoshi.FHiki_Cost, iCost));
			gctl.Add(new GControlDBText(t_hikiotoshi.FHiki_Memo, iMemo));
			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();
			calcTotal();

			// イベント登録
			grid.RowColChange += Grid_RowColChange;
			iCost.ValueChanged += ICost_ValueChanged;

			base.FormFrame_Shown(sender, e);
		}

		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (formCloseReason == FormCloseReason.Save)
			{
				AppGlobal.DB.UpdateTable(TableProp.t_hikiotoshi);
			}
			else
			if (dvHiki.HasChanges() == true && AppMsgBox.Show(this, AppMsgBoxIndex.CancelClose) == DialogResult.No)
			{
				e.Cancel = true;
			}
			else
			{
				dvHiki.RejectChanges();
			}

			base.FormFrame_FormClosing(sender, e);
		}

		private void ICost_ValueChanged(object sender, EventArgs e)
		{
			gctl.UpdateByControl(iCost);
			calcTotal();
		}

		private void Grid_RowColChange(object sender, RowColChangeEventArgs e)
		{
			rowFetch();
		}

		void calcTotal()
		{
			decimal cost = 0;
			for (int i = 0; i < dvHiki.Count; i++)
			{
				t_hikiotoshi xrow = new t_hikiotoshi(dvHiki[i]);

				cost += xrow.Hiki_Cost;
			}

			iTotalCost.Value = cost;
		}

		void rowFetch()
		{
			gctl.FetchAll();
		}

		DataRow getDataRow(string tag)
		{
			if (dvHiki.CurrentRow != null)
			{
				return dvHiki.CurrentRow.Row;
			}

			return null;
		}

		/// <summary>
		/// 参加医会名
		/// </summary>
		string	ubIkai(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_hikiotoshi xrow = new t_hikiotoshi(dvHiki[e.Row]);

			Kaihi ka = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

			if (ka != null)
			{
				return ka.XRow.Kaihi_Name;
			}

			return null;
		}

		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncHikiotoshi_DlgEntry.Functions);

			FuncHikiotoshi_DlgEntry.Save.Execute = saveClose;
			FuncHikiotoshi_DlgEntry.Cancel.Execute = formClose;
		}

		void saveClose()
		{			
			formCloseReason = FormCloseReason.Save;
			this.Close();
		}

		void formClose()
		{
			this.Close();
		}
	}
}
