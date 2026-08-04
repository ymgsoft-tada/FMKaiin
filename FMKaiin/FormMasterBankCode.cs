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
	/// [作成者 tanaka]
	/// 銀行コードマスタ
	/// </summary>
	public partial class FormMasterBankCode : FormFrame
    {
		DBView dvBankCode;
		GGridDBCommon gcom;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMasterBankCode()
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

		protected override void FormFrame_Load(object sender, EventArgs e)
		{
			FormBg_Progress prog = new FormBg_Progress();
			prog.DoWorkEvent += prog_DoWorkEvent;
			prog.TitleText = "しばらくお待ちください。";
			prog.LabelText = "データを取得しています。";
			prog.ShowDialog();
			prog.Dispose();
			prog = null;

			base.FormFrame_Load(sender,e);
		}

		private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;
			prog.AdvanceProgress(100);

			dvBankCode = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_bank_code, $"ORDER BY {t_bank_code.FBCD_Code}, {t_bank_code.FBCD_CodeShiten}"), this.BindingContext);

			AppGlobal.InitBankCodeMg();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			
			gcom.Add(new GGridDBText(t_bank_code.FBCD_Code, "銀行ｺｰﾄﾞ", 0.15f, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(t_bank_code.FBCD_FullName, "銀行名（正式名称）", 0.35f));
			gcom.Add(new GGridDBText(t_bank_code.FBCD_CodeShiten, "支店ｺｰﾄﾞ", 0.15f, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(t_bank_code.FBCD_NameShiten, "支店名", 0.0f));

			gcom.EndAdd(dvBankCode);

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;

			base.FormFrame_Shown(sender, e);
		}

		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ShownでRefillしているので終了時に共通クラスを初期化し直す。
			AppGlobal.InitBankCodeMg();

			base.FormFrame_FormClosing(sender, e);
		}

		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvBankCode.Count > 0)
			{
				rowEdit();
			}
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMasterBankCode.Functions);

			FuncMasterBankCode.RowAdd.Execute = rowAdd;
			FuncMasterBankCode.RowEdit.Execute = rowEdit;
			FuncMasterBankCode.RowDelete.Execute = rowDelete;
			FuncMasterBankCode.Close.Execute = formClose;
		}

		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_bank_code nrow = new t_bank_code(dvBankCode.NewRow());


			FormMasterBankCode_DlgEntry frm = new FormMasterBankCode_DlgEntry();
			frm.Mode = FormMasterBankCode_DlgEntry.eMode.Add;
			frm.Row = nrow.Row;
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				dvBankCode.Add(frm.Row);
				AppGlobal.DB.UpdateTable(TableProp.t_bank_code);

				dvBankCode.SearchRow(t_bank_code.FID_Auto, nrow.ID_Auto);

				AppGlobal.InitBankCodeMg();
			}

			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 編集
		/// </summary>
		void rowEdit()
		{
			if (dvBankCode.Count > 0)
			{
				DataRow row = dvBankCode.NewRow();

				AppDb.CopyDataRow(dvBankCode.CurrentRow.Row, row);

				//ユニークIDがないので、ID_Autoをキーにする
				row[t_bank_code.FID_Auto] = dvBankCode.CurrentRow[t_bank_code.FID_Auto];

				FormMasterBankCode_DlgEntry frm = new FormMasterBankCode_DlgEntry();
				frm.Mode = FormMasterBankCode_DlgEntry.eMode.Edit;
				frm.Row = row;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					AppDb.CopyDataRow(frm.Row, dvBankCode.CurrentRow.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_bank_code);

					AppGlobal.InitBankCodeMg();
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
			if (dvBankCode.Count > 0)
			{
				t_bank_code xrow = new t_bank_code(dvBankCode.CurrentRow);

				//if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_bank_code, t_bank_code., xrow.ID_Bank, TableProp.t_bank) == true)
				//{
				//	AppMsgBox.Show(this, AppMsgBoxIndex.UsedOtherTable);
				//	return;
				//}

				string str = string.Format("銀行コード：{0}　支店コード:{1}", xrow.BCD_Code, xrow.BCD_CodeShiten);

				if (AppMsgBox.Show(this, AppMsgBoxIndex.Delete, str) == System.Windows.Forms.DialogResult.Yes &&
					AppMsgBox.Show(this,AppMsgBoxIndex.Delete2) == DialogResult.Yes)
				{
					dvBankCode.Delete();
					AppGlobal.DB.UpdateTable(TableProp.t_bank_code);

					AppGlobal.InitBankCodeMg();
				}
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
