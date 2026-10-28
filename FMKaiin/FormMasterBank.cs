using C1.Win.C1TrueDBGrid;
using ComponentDB;
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
	/// 銀行情報マスタ (給与システム流用)
    /// </summary>
    public partial class FormMasterBank : FormFrame
    {
		DBView dvBank;
		GGridDBCommon gcom;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMasterBank()
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

			switch (e.KeyCode)
			{
				case Keys.Enter:
					if (this.ActiveControl == grid)
					{
						rowEdit();
					}
					break;
			}
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			dvBank = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_bank, $"ORDER BY {t_bank.FBNK_Code}"), this.BindingContext);

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			
			gcom.Add(new GGridDBText(t_bank.FBNK_Code, "銀行名", 0.6f, GGridDBCellDisp.Left));
			gcom.SetUnboundColumnFetch(ubName);
			gcom.Add(new GGridDBText(t_bank.FBNK_KozaType, "区分", 0.15f, GGridDBCellDisp.Center));
			gcom.SetUnboundColumnFetch(ubType);
			gcom.Add(new GGridDBText(t_bank.FBNK_KozaNo, "口座番号", 0.0f, GGridDBCellDisp.Right));

			gcom.EndAdd(dvBank);

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;

			base.FormFrame_Shown(sender, e);
		}

		string ubName(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_bank xrow = new t_bank(dvBank[e.Row]);

			t_bank_code bc = AppGlobal.BankCodeMg.GetBankCodeRow(xrow.BNK_Code, xrow.BNK_CodeShiten);
			string str = "";
			if (bc != null)
			{
				str += $"{bc.BCD_FullName}　{bc.BCD_NameShiten}支店";
			}

			return str;
		}

		string ubType(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_bank xrow = new t_bank(dvBank[e.Row]);

			return enumKbn.DTypeKoza[(int)xrow.BNK_KozaType];
		}

		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ShownでRefillしているので終了時に共通クラスを初期化し直す。
			AppGlobal.InitBasic();

			base.FormFrame_FormClosing(sender, e);
		}

		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvBank.Count > 0)
			{
				rowEdit();
			}
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMasterBank.Functions);

			FuncMasterBank.RowAdd.Execute = rowAdd;
			FuncMasterBank.RowEdit.Execute = rowEdit;
			FuncMasterBank.RowDelete.Execute = rowDelete;
			FuncMasterBank.Close.Execute = formClose;
		}

		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_bank nrow = new t_bank(dvBank.NewRow());

			nrow.ID_Bank		= AppDbID.GetNewID(dvBank, t_bank.FID_Bank);
			nrow.BNK_KozaType	= eTypeKoza.Futsu;

			FormMasterBank_DlgEntry frm = new FormMasterBank_DlgEntry();
			frm.Mode = FormMasterBank_DlgEntry.eMode.Add;
			frm.Row = nrow.Row;
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				dvBank.Add(frm.Row);
				AppGlobal.DB.UpdateTable(TableProp.t_bank);

				dvBank.SearchRow(t_bank.FID_Bank, nrow.ID_Bank);
			}

			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 編集
		/// </summary>
		void rowEdit()
		{
			if (dvBank.Count > 0)
			{
				DataRow row = dvBank.NewRow();

				AppDb.CopyDataRow(dvBank.CurrentRow.Row, row);

				FormMasterBank_DlgEntry frm = new FormMasterBank_DlgEntry();
				frm.Mode = FormMasterBank_DlgEntry.eMode.Edit;
				frm.Row = row;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					AppDb.CopyDataRow(frm.Row, dvBank.CurrentRow.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_bank);
				}

				frm.Dispose();
				frm = null;
			}
		}

		/// <summary>
		/// 削除
		/// </summary>
		void rowDelete()
		{
			if (dvBank.Count > 0)
			{
				t_bank xrow = new t_bank(dvBank.CurrentRow);

				if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_bank, t_bank.FID_Bank, xrow.ID_Bank, TableProp.t_bank) == true)
				{
					AppMsgBox.Show(this, AppMsgBoxIndex.UsedOtherTable);
					return;
				}

				string str = string.Format("{0} : {1}", xrow.BNK_Code, xrow.BNK_Name);

				if (AppMsgBox.Show(this, AppMsgBoxIndex.Delete, str) == System.Windows.Forms.DialogResult.Yes)
				{
					dvBank.Delete();
					AppGlobal.DB.UpdateTable(TableProp.t_bank);
				}
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
