using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentForm;
using ComponentGGridDB;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ComponentSUI;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 診療科マスタ
	/// </summary>
	public partial class FormMasterShinryoka : FormFrame
	{
		DBView dvShinryoka;

		GGridDBCommon gcom;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMasterShinryoka()
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

			switch(e.KeyCode)
			{
				case Keys.Enter :
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
			//¶ 2026/11/20 CC 名称検索欄の入力ルール
			AppDbRule.SetControlByRule(iNameShinryoka, t_shinryoka.FSRK_Name);

			// 追加時の動作仕様のため、再表示時(Shown()時)のみソートする(GetReFillTable()の利用)
			dvShinryoka =new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_shinryoka, $"ORDER BY {t_shinryoka.FSRK_Code}"), this.BindingContext);

			// グリッド作成
			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_shinryoka.FSRK_Code, "コード", 75, GGridDBCellDisp.Right)); //¶ 2026/11/20 CC 他のマスターに合わせて右寄せ
			gcom.Add(new GGridDBText(t_shinryoka.FSRK_Name, "名称", 0));
//			gcom.SetUnboundColumnFetch(ubUsed);

			gcom.EndAdd(dvShinryoka);

			changeFilter();

			grid.Select();

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;
//			iKbnKaihi.SelectedIndexChanged += iKbnKaihi_SelectedIndexChanged;
			iCodeShinryoka.TextChanged += iCodeShinryoka_TextChanged;
			iNameShinryoka.TextChanged += iNameShinryoka_TextChanged;
			btnClear.Click += btnClear_Click;

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// クリアボタンクリック
		/// </summary>
		private void btnClear_Click(object sender, EventArgs e)
		{
//			iKbnKaihi.SelectedIndexChanged -= iKbnKaihi_SelectedIndexChanged;
			iCodeShinryoka.TextChanged -= iCodeShinryoka_TextChanged;
			iNameShinryoka.TextChanged -= iNameShinryoka_TextChanged;

			// 条件初期化
//			iKbnKaihi.ExSetSelectedIndexByValue(AppCombo.SelectAllValue);
			iCodeShinryoka.ResetText();
			iNameShinryoka.ResetText();

			// 初期条件でフィルタ
			changeFilter();

//			iKbnKaihi.SelectedIndexChanged += iKbnKaihi_SelectedIndexChanged;
			iCodeShinryoka.TextChanged += iCodeShinryoka_TextChanged;
			iNameShinryoka.TextChanged += iNameShinryoka_TextChanged;
		}

		/// <summary>
		/// 名称 値変更
		/// </summary>
		private void iNameShinryoka_TextChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		/// <summary>
		/// グリッド上マウスダブルクリック
		/// </summary>
		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvShinryoka.Count > 0)
			{
				rowEdit();
			}
		}

		/// <summary>
		/// コード値変更
		/// </summary>
		private void iCodeShinryoka_TextChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		/// <summary>
		/// grid 検索時表示有無の文字列編集
		/// </summary>
/*
		string ubUsed(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_shinryoka xrow = new t_shinryoka(dvShinryoka[e.Row]);

			if (xrow.Kaihi_SearchUsed == true)
			{
				return "表示";
			}
			else
			{
				return "非表示";
			}
		}
*/

		/// <summary>
		/// フォームクローズ
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ShownでRefillしているので終了時に共通クラスを初期化し直す。
			AppGlobal.InitShinryoka();

			base.FormFrame_FormClosing(sender, e);
		}

		/// <summary>
		/// フィルタ変更
		/// </summary>
		void changeFilter()
		{
			string filter = "";

			// コードの検索
			// 部分一致検索にしたいため、文字列
			//¶ 2026/11/20 CC 前方一致に変更。数値列を文字列に変換してLIKEで比較する
			string cd = StrConv.ToNarrow(iCodeShinryoka.Text).Trim();
			if (Regex.IsMatch(cd, @"^[0-9]+$") == true)
			{
				if (filter != "") filter += " AND ";
				// 数値のみならコード検索
				filter += $"(CONVERT({t_shinryoka.FSRK_Code}, 'System.String') LIKE '{cd}%')";
			}

			//¶ 2026/11/20 CC 名称の検索（部分一致）
			if (iNameShinryoka.Text != "")
			{
				if (filter != "") filter += " AND ";
				filter += DBQuery.CreateLikeKanaString(t_shinryoka.FSRK_Name, iNameShinryoka.Text, true);
			}

			// フィルタ適用
			dvShinryoka.RowFilterQuery(filter);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncMasterShinryokamoku.Functions);

			funcKeys.On(FuncMasterShinryokamoku.RowAdd, rowAdd); // 追加
			funcKeys.On(FuncMasterShinryokamoku.RowEdit, rowEdit); // 訂正
			funcKeys.On(FuncMasterShinryokamoku.RowDelete, rowDelete); // 削除
			funcKeys.On(FuncMasterShinryokamoku.Close, formClose); // 閉じる
		}

		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_shinryoka nrow = new t_shinryoka(dvShinryoka.NewRow());

			// 画面項目未関連カラムの値セット
			nrow.ID_Shinryoka = AppDbID.GetNewID(dvShinryoka, t_shinryoka.FID_Shinryoka);


			FormMasterShinryoka_DlgEntry frm = new FormMasterShinryoka_DlgEntry();
			frm.Mode = FormMasterShinryoka_DlgEntry.eMode.Add;
			frm.Row = nrow.Row;
			//			frm.DbView							= dvShinryoka;
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				dvShinryoka.Add(frm.Row);
				AppGlobal.DB.UpdateTable(TableProp.t_shinryoka);

				// 診療科情報(共通)の再取得 ※情報が追加されたため
				AppGlobal.InitShinryoka();

				// 追加行にフォーカス
				dvShinryoka.SearchRow(t_shinryoka.FID_Shinryoka, nrow.ID_Shinryoka);
			}

			frm.Dispose();
			frm = null; // なかったが念のため追加

		}

		/// <summary>
		/// 編集
		/// </summary>
		void rowEdit()
		{

			if (dvShinryoka.Count > 0)
			{
				DataRow row = dvShinryoka.NewRow();

				AppDb.CopyDataRow(dvShinryoka.CurrentRow.Row, row);

				FormMasterShinryoka_DlgEntry frm = new FormMasterShinryoka_DlgEntry();
				frm.Mode = FormMasterShinryoka_DlgEntry.eMode.Edit;
				frm.Row = row;
				//				frm.DbView		= dvShinryoka;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					AppDb.CopyDataRow(frm.Row, dvShinryoka.CurrentRow.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_shinryoka);

					// 診療科情報(共通)の再取得 ※情報が訂正されたため
					AppGlobal.InitShinryoka();
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
			if (dvShinryoka.Count > 0)
			{
				t_shinryoka xrow = new t_shinryoka(dvShinryoka.CurrentRow);

				if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_shinryoka, t_shinryoka.FID_Shinryoka, xrow.ID_Shinryoka, TableProp.t_shinryoka) == true)
				{
					AppMsgBox.Show(AppMsgBoxIndex.UsedOtherTable);
					return;
				}

				if (AppMsgBox.Show(AppMsgBoxIndex.DeleteSelectedRow) == System.Windows.Forms.DialogResult.Yes)
				{
					dvShinryoka.Delete();
					AppGlobal.DB.UpdateTable(TableProp.t_shinryoka);

					// 診療科情報(共通)の再取得 ※情報が削除されたため
					AppGlobal.InitShinryoka();
				}
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		void formClose()
		{
			// DBViewの破棄
			dvShinryoka.Dispose();

			this.Close();
		}

	}
}
