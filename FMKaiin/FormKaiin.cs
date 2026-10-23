using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using ComponentDB;
using ComponentGGridDB;
using ComponentIO;

namespace App
{
	/// <summary>
	/// 会員情報
	/// </summary>
	public partial class FormKaiin:FormFrame
	{

		DBView dvKaiin;
		GGridDBCommon gcom;

		/// <summary>
		/// 会員-診療科の関連 DBView (_DlgEntryにて操作)
		/// </summary>
		DBView dvKaiinShinryo;
		/// <summary>
		/// 会員-学会の関連 DBView (_DlgEntryにて操作)
		/// </summary>
		DBView dvKaiinGakkai;
		/// <summary>
		/// 会員-医会の関連 DBView (_DlgEntryにて操作)
		/// </summary>
		DBView dvKaiinKaihi;
		/// <summary>
		/// 会員-異動情報の関連 DBView (_DlgEntryにて操作)
		/// </summary>
		DBView dvKaiinIdo;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormKaiin()
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
			// グリッド作成
			// 追加時の動作仕様のため、再表示時(Shown()時)のみソートする(GetReFillTable()の利用)
			dvKaiin = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaiin, $"ORDER BY {t_kaiin.FCD_Kaiin}"), this.BindingContext);

			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_kaiin.FCD_Kaiin, "コード", 80, GGridDBCellDisp.Right));
			gcom.SetUnboundColumnFetch(ubCode);
			gcom.Add(new GGridDBText(t_kaiin.FKaiin_Name, "氏　名", 200));
			gcom.Add(new GGridDBText(t_kaiin.FKaiin_Sex, "性別", 60, GGridDBCellDisp.Center));
			gcom.SetUnboundColumnFetch(ubSex);
			gcom.Add(new GGridDBText(t_kaiin.FKaiin_TypeZaiseki, "在籍区分", 90, GGridDBCellDisp.Center));
			gcom.SetUnboundColumnFetch(ubZaiseki);
			gcom.Add(new GGridDBText(t_kaiin.FID_KaiinKbn, "会員区分", 0, GGridDBCellDisp.Center));
			gcom.SetUnboundColumnFetch(ubKaiinKbn);
			gcom.SetRowStyles(rowStyleUsed);

			gcom.EndAdd(dvKaiin);

			AppDbRule.SetControlByRule(iSearch, t_kaiin.FKaiin_Name);

			changeFilter();

			grid.Select();

			// _DlgEntry画面のGrid用データ取得
			dvKaiinShinryo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_shinryoka)); // 本画面のGridと連動しないためBindingContext指定なし
			dvKaiinGakkai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_gakkai));
			dvKaiinKaihi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_kaihi));
			dvKaiinIdo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_ido));

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;
			iSearch.TextChanged += iSearch_TextChanged;
			btnClear.Click += btnClear_Click;
			chkUnUsed.CheckedChanged += chkUnUsed_CheckedChanged;

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// 退会レコードの表現設定
		/// </summary>
		void rowStyleUsed(object sender, FetchRowStyleEventArgs e)
		{
			t_kaiin xrow = new t_kaiin(dvKaiin[e.Row]);

			if (xrow.Kaiin_TypeZaiseki == eTypeZaiseki.Taikai)
			{
				e.CellStyle.ForeColor = Color.Firebrick;
			}
			else
			{
				e.CellStyle.ResetForeColor();
			}
		}

		private void chkUnUsed_CheckedChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		private void iSearch_TextChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			iSearch.TextChanged -= iSearch_TextChanged;

			iSearch.ResetText();

			changeFilter();

			iSearch.TextChanged += iSearch_TextChanged;
		}

		string ubCode(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin xrow = new t_kaiin(dvKaiin[e.Row]);

			Kaiin kai = AppGlobal.Kaiins.Get(xrow.ID_Kaiin);

			if (kai != null)
			{
				return kai.CodeString;
			}

			return null;
		}

		string ubSex(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin xrow = new t_kaiin(dvKaiin[e.Row]);

			return Regex.Replace(enumKbn.DSex[(int)xrow.Kaiin_Sex], "性", "");
		}

		string ubZaiseki(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin xrow = new t_kaiin(dvKaiin[e.Row]);

			return enumKbn.DTypeZaiseki[(int)xrow.Kaiin_TypeZaiseki];
		}

		string ubKaiinKbn(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaiin xrow = new t_kaiin(dvKaiin[e.Row]);

			// 会員情報の取得
			KaiinKbn kk = AppGlobal.KaiinKbns.Get(Cast.Int(xrow.ID_KaiinKbn_Null));

			// 該当情報が存在したら名称をreturn
			if (kk != null)
			{
				return kk.XRow.KIK_Name_Null;
			}
			else
			{
				return null;
			}
		}

		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ShownでRefillしているので終了時に共通クラスを初期化し直す。
			AppGlobal.InitKaiin();

			base.FormFrame_FormClosing(sender, e);
		}

		private void chkAll_CheckedChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		/// <summary>
		/// フィルタ変更
		/// </summary>
		void changeFilter()
		{
			string filter = "";

			if (iSearch.Text != "")
			{
				string str = iSearch.Text;
				string str_numchk = StrConv.ToNarrow(str);

				if (Regex.IsMatch(str_numchk, @"^[-.0-9]+$") == true)
				{
					// 数値のみならコード検索
					filter += string.Format("({0} = {1})", t_kaiin.FCD_Kaiin, Cast.Int(str_numchk));
				}
				else
				{
					if (str.Length == 1)
					{
						// 一文字だけなら先頭一致
						filter += string.Format("({0} OR {1})",
												DBQuery.CreateLikeKanaString(t_kaiin.FKaiin_Name, str),
												DBQuery.CreateLikeKanaString(t_kaiin.FKaiin_NameKana, str));
					}
					else
					{
						// 部分一致
						filter += string.Format("({0} OR {1})",
												DBQuery.CreateLikeKanaString(t_kaiin.FKaiin_Name, str, true),
												DBQuery.CreateLikeKanaString(t_kaiin.FKaiin_NameKana, str, true));
					}

				}
			}

			if (chkUnUsed.Checked == false)
			{
				if (filter != "")
					filter += " AND ";
				filter += $"({t_kaiin.FKaiin_TypeZaiseki} <> {(int)eTypeZaiseki.Taikai})"; // 在籍区分:退会以外
			}

			dvKaiin.RowFilterQuery(filter);
		}

		private void iFilter_TextChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvKaiin.Count > 0)
			{
				rowEdit();
			}
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncKaiin.Functions);

			FuncKaiin.RowAdd.Execute = rowAdd;
			FuncKaiin.RowEdit.Execute = rowEdit;
			FuncKaiin.RowDelete.Execute = rowDelete;
			FuncKaiin.Preview.Execute = preview;
			FuncKaiin.Close.Execute = formClose;

			//bool enabled = false;

			//if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.Admin || 
			//	AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU)
			//{
			//	enabled = true;
			//}

			//appFuncKey.SetEnabled(FuncStaff.ShowMyno.Key, enabled);
			//appFuncKey.SetVisible(FuncStaff.ShowMyno.Key, enabled);

		}

		/// <summary>
		/// プレビュー
		/// </summary>
		void preview()
		{
			ReportFlexKaiinList rep = new ReportFlexKaiinList(dvKaiin);
			rep.ReportExec();
			rep = null;
		}

		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_kaiin nrow = new t_kaiin(dvKaiin.NewRow());

			// カラム値の初期セット(新規IDやデフォルト値など)
			nrow.ID_Kaiin = AppDbID.GetNewID(dvKaiin, t_kaiin.FID_Kaiin);
			nrow.CD_Kaiin_Null = AppDbID.GetNewCode(dvKaiin, t_kaiin.FCD_Kaiin);
			nrow.Kaiin_Sex = eSex.Men;
			nrow.Kaiin_TypeZaiseki = eTypeZaiseki.Zaiseki;
			nrow.Kaiin_DateToroku = DateTime.Today;
			nrow.Kaiin_BunshoSofusaki = true;
			nrow.Kaiin_FaxSofusaki = true;
			nrow.Kaiin_KaihiUchiwake = true;

			// --日医～市医Grid準備--
			addIshikaiKaihi(nrow.ID_Kaiin);

			// 入力画面呼び出し
			FormKaiin_DlgEntry frm = new FormKaiin_DlgEntry();
			frm.Mode = FormKaiin_DlgEntry.eMode.Add;
			frm.Row = nrow.Row;
			frm.DvKaiinKaihiIshikai = dvKaiinKaihi; // 医師会会員grid用DBView
			frm.DvKaiinShinryo = dvKaiinShinryo;    // 担当診療科目grid用DBView
			frm.DvKaiinGakkai = dvKaiinGakkai;      // 所属学会grid用DBView
			frm.DvKaiinKaihi = dvKaiinKaihi;        // 参加医会grid用DBView
			frm.DvKaiinIdo = dvKaiinIdo;            // 異動情報grid用DBView
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				dvKaiin.Add(frm.Row);

				// 会員情報 のDB更新
				AppGlobal.DB.UpdateTable(TableProp.t_kaiin);
				dvKaiin.AcceptChanges();

				// 会員-診療科目担当 のDB更新
				AppGlobal.DB.UpdateTable(TableProp.t_kaiin_shinryoka);
				dvKaiinShinryo.AcceptChanges(); // 修正の起点

				// 会員-所属学会 のDB更新
				AppGlobal.DB.UpdateTable(TableProp.t_kaiin_gakkai);
				dvKaiinGakkai.AcceptChanges(); // 修正の起点

				// 会員-参加医会 のDB更新
				AppGlobal.DB.UpdateTable(TableProp.t_kaiin_kaihi);
				dvKaiinKaihi.AcceptChanges(); // 修正の起点

				// 会員-診療科目担当 のDB更新
				AppGlobal.DB.UpdateTable(TableProp.t_kaiin_ido);
				dvKaiinIdo.AcceptChanges(); // 修正の起点

				// 共通クラスの初期化処理
				AppGlobal.InitKaiin();
				dvKaiin.SearchRow(t_kaiin.FID_Kaiin, nrow.ID_Kaiin);

			}
			else
			{
				dvKaiin.RejectChanges();
				dvKaiinShinryo.RejectChanges(); // DlgEntryでの科目grid変更内容破棄
				dvKaiinGakkai.RejectChanges();  // DlgEntryでの学会grid変更内容破棄
				dvKaiinKaihi.RejectChanges();   // DlgEntryでの医会grid変更内容破棄
				dvKaiinIdo.RejectChanges();     // DlgEntryでの異動grid変更内容破棄
			}

			frm.Dispose();
			frm = null;
		}

		/// <summary>
		/// 編集
		/// </summary>
		void rowEdit()
		{
			if (dvKaiin.Count > 0)
			{
				DataRow row = dvKaiin.NewRow();
				AppDb.CopyDataRow(dvKaiin.CurrentRow.Row, row); // 選択行データをコピー

				t_kaiin kai = new t_kaiin(row);
				DBView dvIshikai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_kaihi));
				dvIshikai.RowFilterQuery($"{t_kaiin_kaihi.FID_Kaiin} = {kai.ID_Kaiin} AND {t_kaiin_kaihi.FKaihiTypeIkai} IN ({(int)eTypeIkai.Nichii}, {(int)eTypeIkai.Keni}, {(int)eTypeIkai.Shii})"); // 日医or県医or市医に絞込み
				// 編集対象者の医師会レコードが存在しない
				if (dvIshikai.Count == 0)
				{
					// --日医～市医Grid準備--
					addIshikaiKaihi(kai.ID_Kaiin);
				}

				// 入力画面呼び出し
				FormKaiin_DlgEntry frm = new FormKaiin_DlgEntry();
				frm.Mode = FormKaiin_DlgEntry.eMode.Edit;
				frm.Row = row; // 選択中会員データ
				frm.DvKaiinKaihiIshikai = dvKaiinKaihi; // 医師会会員grid用DBView
				frm.DvKaiinShinryo = dvKaiinShinryo;    // 担当診療科目grid用DBView
				frm.DvKaiinGakkai = dvKaiinGakkai;      // 所属学会grid用DBView
				frm.DvKaiinKaihi = dvKaiinKaihi;        // 参加医会grid用DBView
				frm.DvKaiinIdo = dvKaiinIdo;            // 異動情報grid用DBView
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					// 会員情報 のDB更新
					AppDb.CopyDataRow(frm.Row, dvKaiin.CurrentRow.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin);

					// 会員-診療科目担当 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin_shinryoka);
					dvKaiinShinryo.AcceptChanges(); // 修正の起点

					// 会員-所属学会 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin_gakkai);
					dvKaiinGakkai.AcceptChanges(); // 修正の起点

					// 会員-参加医会 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin_kaihi);
					dvKaiinKaihi.AcceptChanges(); // 修正の起点

					// 会員-診療科目担当 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin_ido);
					dvKaiinIdo.AcceptChanges(); // 修正の起点

					// 会員情報の再取得
					AppGlobal.InitKaiin();
				}
				else
				{
					dvKaiinShinryo.RejectChanges(); // DlgEntryでの科目grid変更内容破棄
					dvKaiinGakkai.RejectChanges();  // DlgEntryでの学会grid変更内容破棄
					dvKaiinKaihi.RejectChanges();   // DlgEntryでの医会grid変更内容破棄
					dvKaiinIdo.RejectChanges();     // DlgEntryでの異動grid変更内容破棄
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
			if (dvKaiin.Count > 0)
			{
				t_kaiin xrow = new t_kaiin(dvKaiin.CurrentRow);

				// 削除対象テーブル以外で他に会員IDが使用されているかチェック
				if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_kaiin, t_kaiin.FID_Kaiin, xrow.ID_Kaiin, 
					TableProp.t_kaiin, TableProp.t_kaiin_shinryoka, TableProp.t_kaiin_gakkai, TableProp.t_kaiin_kaihi, TableProp.t_kaiin_ido) == true)
				{
					AppMsgBox.Show(AppMsgBoxIndex.UsedOtherTable);
					return;
				}

				if (AppMsgBox.Show(AppMsgBoxIndex.DeleteSelectedRow) == System.Windows.Forms.DialogResult.Yes)
				{
					// 紐づき情報削除
					DBView tmpDv;

					tmpDv = new DBView(dvKaiinShinryo); // 削除操作用のView作成
					tmpDv.RowFilterQuery($"{t_kaiin_shinryoka.FID_Kaiin} = {xrow.ID_Kaiin}"); // 削除対象の会員でフィルタ

					// 0件になるまでDBView.Delete()
					while (true)
					{
						if (tmpDv.Count == 0) break;
						tmpDv.Delete();
					}

					tmpDv = new DBView(dvKaiinGakkai); // 削除操作用のView作成
					tmpDv.RowFilterQuery($"{t_kaiin_gakkai.FID_Kaiin} = {xrow.ID_Kaiin}"); // 削除対象の会員でフィルタ

					// 0件になるまでDBView.Delete()
					while (true)
					{
						if (tmpDv.Count == 0) break;
						tmpDv.Delete();
					}

					tmpDv = new DBView(dvKaiinKaihi); // 削除操作用のView作成
					tmpDv.RowFilterQuery($"{t_kaiin_kaihi.FID_Kaiin} = {xrow.ID_Kaiin}"); // 削除対象の会員でフィルタ

					// 0件になるまでDBView.Delete()
					while (true)
					{
						if (tmpDv.Count == 0) break;
						tmpDv.Delete();
					}

					tmpDv = new DBView(dvKaiinIdo); // 削除操作用のView作成
					tmpDv.RowFilterQuery($"{t_kaiin_ido.FID_Kaiin} = {xrow.ID_Kaiin}"); // 削除対象の会員でフィルタ

					// 0件になるまでDBView.Delete()
					while (true)
					{
						if (tmpDv.Count == 0) break;
						tmpDv.Delete();
					}

					// 会員情報 指定行削除
					dvKaiin.Delete();

					// 会員情報 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin);
					// 会員-診療科目担当 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin_shinryoka);
					// 会員-所属学会 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin_gakkai);
					// 会員-参加医会 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin_kaihi);
					// 会員-診療科目担当 のDB更新
					AppGlobal.DB.UpdateTable(TableProp.t_kaiin_ido);

					// 共通クラスの初期化処理
					AppGlobal.InitKaiin();
				}
			}
		}

		void formClose()
		{
			this.Close();
		}

		/// <summary>
		/// 日医～市医のレコード追加処理
		/// <param name="kaiinId">追加行の会員IDセット値</param>
		/// </summary>
		void addIshikaiKaihi(int kaiinId)
		{
			// --日医～市医Grid準備--
			t_kaiin_kaihi kaihi_nrow;

			//if(kai.Kaihis.Contains( // KaihiTypeIkai=eTypeIkai.Nichii のデータがない場合処理

			kaihi_nrow = new t_kaiin_kaihi(dvKaiinKaihi.NewRow());
			// NewRowへ追加/編集予定の会員IDセット
			kaihi_nrow.ID_Kaiin = kaiinId;
			// デフォルト値セット
			kaihi_nrow.ID_Kaihi = AppGlobal.Kaihis.GetCode(19999).ID; // 19999:日医未入会
			kaihi_nrow.KaihiShiharai = eShiharai.Koza1; // 支払方法
			kaihi_nrow.KaihiTypeIkai = eTypeIkai.Nichii; // 日医
			// 行追加
			dvKaiinKaihi.Add(kaihi_nrow.Row);

			kaihi_nrow = new t_kaiin_kaihi(dvKaiinKaihi.NewRow());
			// NewRowへ追加/編集予定の会員IDセット
			kaihi_nrow.ID_Kaiin = kaiinId;
			// デフォルト値セット
			kaihi_nrow.ID_Kaihi = AppGlobal.Kaihis.GetCode(29999).ID; // 29999:県医未入会
			kaihi_nrow.KaihiShiharai = eShiharai.Koza1; // 支払方法
			kaihi_nrow.KaihiTypeIkai = eTypeIkai.Keni; // 県医
			// 行追加
			dvKaiinKaihi.Add(kaihi_nrow.Row);

			kaihi_nrow = new t_kaiin_kaihi(dvKaiinKaihi.NewRow());
			// NewRowへ追加/編集予定の会員IDセット
			kaihi_nrow.ID_Kaiin = kaiinId;
			// デフォルト値セット
			kaihi_nrow.ID_Kaihi = AppGlobal.Kaihis.GetCode(39999).ID; // 39999:市医未入会
			kaihi_nrow.KaihiShiharai = eShiharai.Koza1; // 支払方法
			kaihi_nrow.KaihiTypeIkai = eTypeIkai.Shii; // 市医
			// 行追加
			dvKaiinKaihi.Add(kaihi_nrow.Row);
		}
	}
}
