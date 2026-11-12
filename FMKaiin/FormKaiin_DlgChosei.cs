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
	/// 会費調整金額入力画面
	/// </summary>
	public partial class FormKaiin_DlgChosei : FormFrame
	{
		/// <summary>選択中の会員情報</summary>
		public Kaiin SelectKaiin { get; set; }
		
		DBView dvGrid;
		GGridDBCommon gcom;
		GControlDB gctl;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormKaiin_DlgChosei()
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


		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			if (SelectKaiin != null)
			{
				iKaiin.Text = $"{SelectKaiin.CD} {SelectKaiin.XRow.Kaiin_Name}";
			}

			DBView dvKaihi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaihi));
			AppTableCombo.SetComboBox_Kaihi(iKaihi, dvKaihi);

			dvGrid = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaiin_chosei, $"ORDER BY {t_kaiin_chosei.FKCS_DateYM}"), this.BindingContext);
			dvGrid.RowFilterQuery($"{t_kaiin_chosei.FID_Kaiin} = {SelectKaiin.ID}");

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_kaiin_chosei.FKCS_DateYM, "月分", 120));
			gcom.SetUnboundColumnFetch(ubYM);
			gcom.SetTabIndex(0);
			gcom.SetLocked(true);
			gcom.SetFocusControlInGrid(iDateYM);
			gcom.Add(new GGridDBText(t_kaiin_chosei.FID_Kaihi, "会費", 180));
			gcom.SetUnboundColumnFetch(ubKaihi);
			gcom.SetLocked(true);
			gcom.SetTabIndex(1);
			gcom.SetFocusControlInGrid(iKaihi);
			gcom.Add(new GGridDBCurrency(t_kaiin_chosei.FKCS_Cost, "金額", 150));
			gcom.SetLocked(true);
			gcom.SetTabIndex(2);
			gcom.SetFocusControlInGrid(iCost);
			gcom.Add(new GGridDBText(t_kaiin_chosei.FKCS_Memo, "備考", 0));
			gcom.SetLocked(true);
			gcom.SetTabIndex(3);
			gcom.SetFocusControlInGrid(iMemo);
			gcom.EndAdd(dvGrid);

			gctl = new GControlDB(this, getDataRow);
			gctl.Add(new GControlDBDate(t_kaiin_chosei.FKCS_DateYM, iDateYM.UcDateCtl));
			gctl.Add(new GControlDBCombo(t_kaiin_chosei.FID_Kaihi, iKaihi.ComboBox));
			gctl.Add(new GControlDBNumber(t_kaiin_chosei.FKCS_Cost, iCost));
			gctl.Add(new GControlDBText(t_kaiin_chosei.FKCS_Memo, iMemo));
			gctl.EndAdd(AppDbRule.Rule);

			rowFetch();

			base.FormFrame_Shown(sender, e);
		}

		string	ubYM(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid[e.Row]);

			if (xrow.KCS_DateYM_Null == null)
			{
				return null;
			}

			return AppDate.GetYearMonth(xrow.KCS_DateYM);
		}

		/// <summary>
		/// 会費
		/// </summary>
		string	ubKaihi(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid[e.Row]);

			Kaihi ka = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

			if (ka != null)
			{
				return ka.XRow.Kaihi_Name;
			}

			return null;
		}

		void rowFetch()
		{
			gctl.FetchAll();
		}

		protected override void FormFrame_Disposed(object sender, EventArgs e)
		{
			dvGrid.Dispose();

			base.FormFrame_Disposed(sender, e);
		}

		DataRow getDataRow(string tag)
		{
			if (dvGrid.Count > 0)
			{
				return dvGrid.CurrentRow.Row;
			}

			return null;
		}

		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncKaiin_DlgChosei.Functions);

			FuncKaiin_DlgChosei.RowAdd.Execute = rowAdd;
			FuncKaiin_DlgChosei.RowDelete.Execute = rowDelete;
			FuncKaiin_DlgChosei.Save.Execute = saveClose;
			FuncKaiin_DlgChosei.Cancel.Execute = formClose;
		}

		void rowAdd()
		{
			t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid.NewRow());
			xrow.ID_Kaiin = SelectKaiin.ID;
			dvGrid.Add(xrow.Row);

			grid.Row = grid.RowCount-1;
			grid.Col = 0;

			rowFetch();

			gcom.SelectInput();
		}

		void rowDelete()
		{
			if (AppMsgBox.Show(this, AppMsgBoxIndex.DeleteSelectedRow) == DialogResult.Yes)
			{
				dvGrid.Delete();
			}
		}

		bool validateRow()
		{
			for (int i = 0; i < dvGrid.Count; i++)
			{
				t_kaiin_chosei xrow = new t_kaiin_chosei(dvGrid[i]);

				if (xrow.KCS_DateYM_Null == null)
				{
					grid.Row = i;
					grid.Col = 0;
					gcom.SelectInput();

					appToolTip.Show(iDateYM, AppToolTipIndex.CannotUseBlank);

					return false;
				}
				else
				if (xrow.ID_Kaihi_Null == null)
				{
					grid.Row = i;
					grid.Col = 1;
					gcom.SelectInput();

					appToolTip.Show(iKaihi, AppToolTipIndex.CannotUseBlank);

					return false;
				}
				else
				if (xrow.KCS_Cost_Null == null)
				{
					grid.Row = i;
					grid.Col = 2;
					gcom.SelectInput();

					appToolTip.Show(iCost, AppToolTipIndex.CannotUseBlank);

					return false;
				}
			}

			return true;
		}

		void saveClose()
		{
			if (validateRow() == true)
			{
				// DB更新
				AppGlobal.DB.UpdateTable(TableProp.t_kaiin_chosei);

				formCloseReason = FormCloseReason.Save;
				this.Close();
			}
		}

		void formClose()
		{
			if (dvGrid.HasChanges() == true)
			{
				if (AppMsgBox.Show(this, AppMsgBoxIndex.CancelClose) == DialogResult.No) return;
			}

			this.Close();
		}
	}
}
