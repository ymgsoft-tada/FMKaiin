using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentGGridDB;

namespace App
{
	public partial class FormMasterTantosha:FormFrame
	{
		DBView dvTanto;
		GGridDBCommon gcom;

		int login_id = 0;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMasterTantosha()
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
			login_id = AppGlobal.LoginUser.ID;

			dvTanto = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_tantosha, $"ORDER BY {t_tantosha.FCD_Tanto}"), this.BindingContext);
			dvTanto.RowFilterQuery($"{t_tantosha.FTNT_Auth} <> {(int)eAuth.SU}");

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_tantosha.FCD_Tanto, "コード", 0.15f, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(t_tantosha.FTNT_Name, "氏　名", 0.4f));
			gcom.Add(new GGridDBText(t_tantosha.FTNT_Auth, "権限", 0.3f));
			gcom.SetUnboundColumnFetch(ubAuth);
			gcom.Add(new GGridDBText(t_tantosha.FTNT_TypeShinryojo, "診療所", 0.0f, GGridDBCellDisp.Center));
			gcom.SetUnboundColumnFetch(ubShinryojo);
			gcom.EndAdd(dvTanto);

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;

			base.FormFrame_Shown(sender, e);
		}

		string ubAuth(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_tantosha xrow = new t_tantosha(dvTanto[e.Row]);

			return enumKbn.DAuth[(int)xrow.TNT_Auth];
		}

		string ubShinryojo(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_tantosha xrow = new t_tantosha(dvTanto[e.Row]);

			return enumKbn.DTypeShinryojo[(int)xrow.TNT_TypeShinryojo];
		}

		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ShownでRefillしているので終了時に共通クラスを初期化し直す。
			AppGlobal.InitTanto();

			base.FormFrame_FormClosing(sender, e);
		}

		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvTanto.Count > 0)
			{
				rowEdit();
			}
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMasterTanto.Functions);

			FuncMasterTanto.RowAdd.Execute = rowAdd;
			FuncMasterTanto.RowEdit.Execute = rowEdit;
			FuncMasterTanto.RowDelete.Execute = rowDelete;
			FuncMasterTanto.Close.Execute = formClose;
		}

		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_tantosha nrow = new t_tantosha(dvTanto.NewRow());

			nrow.ID_Tanto = AppDbID.GetNewID(TableProp.t_tantosha, t_tantosha.FID_Tanto);
			nrow.CD_Tanto_Null = AppDbID.GetNewCode(TableProp.t_tantosha, t_tantosha.FCD_Tanto);
			nrow.TNT_Auth = eAuth.Admin;
			nrow.TNT_Used = true;

			FormMasterTantosha_DlgEntry frm = new FormMasterTantosha_DlgEntry();
			frm.Mode = FormMasterTantosha_DlgEntry.eMode.Add;
			frm.Row = nrow.Row;
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				dvTanto.Add(frm.Row);
				AppGlobal.DB.UpdateTable(TableProp.t_tantosha);

				// 共通クラスの初期化処理
				AppGlobal.InitTanto();

				dvTanto.SearchRow(t_tantosha.FID_Tanto, nrow.ID_Tanto);
			}

			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 編集
		/// </summary>
		void rowEdit()
		{
			if (dvTanto.Count > 0)
			{
				//■ 店舗レコード
				DataRow row = dvTanto.NewRow();

				AppDb.CopyDataRow(dvTanto.CurrentRow.Row, row);

				FormMasterTantosha_DlgEntry frm = new FormMasterTantosha_DlgEntry();
				frm.Mode = FormMasterTantosha_DlgEntry.eMode.Edit;
				frm.Row = row;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					AppDb.CopyDataRow(frm.Row, dvTanto.CurrentRow.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_tantosha);

					// 共通クラスの初期化処理
					AppGlobal.InitTanto();
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
			if (dvTanto.Count > 0)
			{
				t_tantosha xrow = new t_tantosha(dvTanto.CurrentRow);

				if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_tantosha, t_tantosha.FID_Tanto, xrow.ID_Tanto, TableProp.t_tantosha) == true)
				{
					AppMsgBox.Show(this, AppMsgBoxIndex.UsedOtherTable);
					return;
				}

				string str = string.Format("{0} : {1}", xrow.CD_Tanto, xrow.TNT_Name);

				if (AppGlobal.LoginUser.ID == xrow.ID_Tanto)
				{
					AppMsgBox.Show(this, AppMsgBoxIndex.CannotDeleteLoginUser, str);
					return;
				}

				if (AppMsgBox.Show(this, AppMsgBoxIndex.Delete, str) == System.Windows.Forms.DialogResult.Yes)
				{
					dvTanto.Delete();
					AppGlobal.DB.UpdateTable(TableProp.t_tantosha);
					// 共通クラスの初期化処理
					AppGlobal.InitTanto();
				}
			}
		}

		void formClose()
		{
			this.Close();
		}
	}
}
