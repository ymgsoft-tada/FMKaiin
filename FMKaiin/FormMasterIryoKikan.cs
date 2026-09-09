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
using ComponentGGridDB;

namespace App
{
	public partial class FormMasterIryoKikan:FormFrame
	{

		DBView dvKikan;
		GGridDBCommon gcom;

		public FormMasterIryoKikan()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void FormFrame_Shown(object sender, EventArgs e)
		{
			dvKikan = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_iryokikan, $"ORDER BY {t_iryokikan.FIRK_Code}"), this.BindingContext);

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_iryokikan.FIRK_Code, "コード", 0.15f, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(t_iryokikan.FIRK_Name, "施設正式名", 0.4f));
			gcom.EndAdd(dvKikan);

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;

			base.FormFrame_Shown(sender, e);
		}

		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvKikan.Count > 0)
			{
				rowEdit();
			}
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMasterIryoKikan.Functions);

			FuncMasterIryoKikan.RowAdd.Execute = rowAdd;
			FuncMasterIryoKikan.RowEdit.Execute = rowEdit;
			FuncMasterIryoKikan.RowDelete.Execute = rowDelete;
			FuncMasterIryoKikan.Close.Execute = formClose;
		}

		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_iryokikan nrow = new t_iryokikan(dvKikan.NewRow());

			nrow.ID_Iryokikan = AppDbID.GetNewID(TableProp.t_iryokikan, t_iryokikan.FID_Iryokikan);
			nrow.IRK_Code_Null = AppDbID.GetNewCode(TableProp.t_iryokikan, t_iryokikan.FIRK_Code);

			FormMasterIryoKikan_DlgEntry frm = new FormMasterIryoKikan_DlgEntry();
			frm.Mode = FormMasterIryoKikan_DlgEntry.eMode.Add;
			frm.Row = nrow.Row;
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				dvKikan.Add(frm.Row);
				AppGlobal.DB.UpdateTable(TableProp.t_iryokikan);

				// 共通クラスの初期化処理
				AppGlobal.InitTanto();

				dvKikan.SearchRow(t_iryokikan.FID_Iryokikan, nrow.ID_Iryokikan);
			}

			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 編集
		/// </summary>
		void rowEdit()
		{
			if (dvKikan.Count > 0)
			{
				//■ 店舗レコード
				DataRow row = dvKikan.NewRow();

				AppDb.CopyDataRow(dvKikan.CurrentRow.Row, row);

				FormMasterIryoKikan_DlgEntry frm = new FormMasterIryoKikan_DlgEntry();
				frm.Mode = FormMasterIryoKikan_DlgEntry.eMode.Edit;
				frm.Row = row;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					AppDb.CopyDataRow(frm.Row, dvKikan.CurrentRow.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_iryokikan);

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
			if (dvKikan.Count > 0)
			{
				t_iryokikan xrow = new t_iryokikan(dvKikan.CurrentRow);

				if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_iryokikan, t_iryokikan.FID_Iryokikan, xrow.ID_Iryokikan, TableProp.t_iryokikan) == true)
				{
					AppMsgBox.Show(this, AppMsgBoxIndex.UsedOtherTable);
					return;
				}

				string str = string.Format("{0} : {1}", xrow.IRK_Code_Null, xrow.IRK_Name);

				if (AppMsgBox.Show(this, AppMsgBoxIndex.Delete, str) == System.Windows.Forms.DialogResult.Yes)
				{
					dvKikan.Delete();
					AppGlobal.DB.UpdateTable(TableProp.t_iryokikan);
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