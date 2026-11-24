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

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 医会会費マスタ
	/// </summary>
	public partial class FormMasterKaihi : FormFrame
	{
		DBView dvKaihi;

		GGridDBCommon gcom;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMasterKaihi()
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
			// コントロールの制御ルール指定
			AppDbRule.SetControlByRule(iCodeKaihi, t_kaihi.FCD_Kaihi);

			// コンボボックス値セット
			// 会費区分コード Enum
			AppCombo.SetComboBox(iKbnKaihi, enumKbn.DTypeKaihi);

			//¶ 2026/11/20 CC 検索表示コンボ（手動セット。FormMasterIryoKikanの退会区分と同じ作り）
			iKbnUsed.ExBeginUpdate();
			iKbnUsed.ExAddItem("", "all"); //¶ 2026/11/20 CC "すべて"代替
			iKbnUsed.ExAddItem("表示", true);
			iKbnUsed.ExAddItem("非表示", false);
			iKbnUsed.ExEndUpdate();

			//¶ 2026/11/20 CC 名称検索欄の入力ルール
			AppDbRule.SetControlByRule(iNameKaihi, t_kaihi.FKaihi_Name);

			//AppGlobal.InitKaihi(); // メモ:FormFrame_FormClosing()内のInitはここでもよい

			// 初期選択値
			//			iKbnKaihi.ExSetSelectedIndexByValue((int)AppGlobal.LoginUser.XRow.TNT_TypeShinryojo);
			//			iDay.ExSetSelectedIndexByValue(AppCombo.SelectAllValue);

			// 追加時の動作仕様のため、再表示時(Shown()時)のみソートする(GetReFillTable()の利用)
			dvKaihi = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaihi, $"ORDER BY {t_kaihi.FCD_Kaihi}"), this.BindingContext);

			// グリッド作成
			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);

			//¶ 2026/11/20 CC 列幅を見直し。備考は全件が空のため右端（残り幅）へ移動
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_KaihiType, "区分", 80));
			gcom.SetUnboundColumnFetch(ubKaihiKbn);
			gcom.Add(new GGridDBText(t_kaihi.FCD_Kaihi, "コード", 70, GGridDBCellDisp.Right));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_Name, "名称", 200));
//			gcom.Add(new GGridDBText(t_kaihi.FKaihi_ShortName, "略称", 90));
//			gcom.Add(new GGridDBText(t_kaihi.FKaihi_GunCode, "会費群", 60));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_SearchUsed, "検索表示", 70));
			gcom.SetUnboundColumnFetch(ubUsed);
			gcom.SetCellDisp(GGridDBCellDisp.Center);
//			gcom.Add(new GGridDBText(t_kaihi.FKaihi_BankCode, "銀行コード", 60));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_BankCode, "銀行名", 130));
			gcom.SetUnboundColumnFetch(ubBankName);
//			gcom.Add(new GGridDBText(t_kaihi.FKaihi_BankCodeShiten, "支店コード", 60));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_BankCodeShiten, "支店名", 130));
			gcom.SetUnboundColumnFetch(ubBankNameShiten);
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_Biko, "備考", 0));

			gcom.EndAdd(dvKaihi);

			changeFilter();

			grid.Select();

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;
			iKbnKaihi.SelectedIndexChanged += iKbnKaihi_SelectedIndexChanged;
			iCodeKaihi.TextChanged += iCodeKaihi_TextChanged;
			iNameKaihi.TextChanged += iNameKaihi_TextChanged;
			iKbnUsed.SelectedIndexChanged += iKbnUsed_SelectedIndexChanged;
			btnClear.Click += btnClear_Click;

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// グリッド上マウスダブルクリック
		/// </summary>
		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dvKaihi.Count > 0)
			{
				rowEdit();
			}
		}

		/// <summary>
		/// クリアボタンクリック
		/// </summary>
		private void btnClear_Click(object sender, EventArgs e)
		{
			iKbnKaihi.SelectedIndexChanged -= iKbnKaihi_SelectedIndexChanged;
			iCodeKaihi.TextChanged -= iCodeKaihi_TextChanged;
			iNameKaihi.TextChanged -= iNameKaihi_TextChanged;
			iKbnUsed.SelectedIndexChanged -= iKbnUsed_SelectedIndexChanged;

			// 条件初期化
			iKbnKaihi.ExSetSelectedIndexByValue((int)eTypeKaihi.None); //¶ 2026/11/20 CC 先頭の空欄項目に戻す（SelectAllValueは項目に無く未選択になっていた）
			iCodeKaihi.ResetText();
			iNameKaihi.ResetText();
			iKbnUsed.ExSetSelectedIndexByValue("all");

			// 初期条件でフィルタ
			changeFilter();

			iKbnKaihi.SelectedIndexChanged += iKbnKaihi_SelectedIndexChanged;
			iCodeKaihi.TextChanged += iCodeKaihi_TextChanged;
			iNameKaihi.TextChanged += iNameKaihi_TextChanged;
			iKbnUsed.SelectedIndexChanged += iKbnUsed_SelectedIndexChanged;
		}

		/// <summary>
		/// 名称 値変更
		/// </summary>
		private void iNameKaihi_TextChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		/// <summary>
		/// 検索表示コンボボックス変更
		/// </summary>
		private void iKbnUsed_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		/// <summary>
		/// 会費区分コンボボックス変更
		/// </summary>
		private void iKbnKaihi_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		/// <summary>
		/// 会費コード 値変更
		/// </summary>
		private void iCodeKaihi_TextChanged(object sender, EventArgs e)
		{
			changeFilter();
		}

		/// <summary>
		/// grid 会費区分の文字列編集
		/// </summary>
		string ubKaihiKbn(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaihi xrow = new t_kaihi(dvKaihi[e.Row]);
			return enumKbn.DTypeKaihi[(int)xrow.Kaihi_KaihiType];
		}

		/// <summary>
		/// grid 検索時表示有無の文字列編集
		/// </summary>
		string ubUsed(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaihi xrow = new t_kaihi(dvKaihi[e.Row]);

			if (xrow.Kaihi_SearchUsed == true)
			{
				return "表示";
			}
			else
			{
				return "非表示";
			}
		}

		/// <summary>
		/// grid 銀行名の文字列編集
		/// </summary>
		string ubBankName(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaihi xrow = new t_kaihi(dvKaihi[e.Row]);

			t_bank_code si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(xrow.Kaihi_BankCode), Cast.Int(xrow.Kaihi_BankCodeShiten));
			if (si != null)
			{
				return si.BCD_Name;
			}

			return null;
		}

		/// <summary>
		/// grid 銀行支店名の文字列編集
		/// </summary>
		string ubBankNameShiten(GGridDBBase col, UnboundColumnFetchEventArgs e)
		{
			t_kaihi xrow = new t_kaihi(dvKaihi[e.Row]);

			t_bank_code si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(xrow.Kaihi_BankCode), Cast.Int(xrow.Kaihi_BankCodeShiten));
			if (si != null)
			{
				return si.BCD_NameShiten;
			}

			return null;
		}

		/// <summary>
		/// フォームクローズ
		/// </summary>
		protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ShownでRefillしDataTableが更新されるため医会会費情報(共通)を再取得
			AppGlobal.InitKaihi();

			base.FormFrame_FormClosing(sender, e);
		}

		/// <summary>
		/// フィルタ変更
		/// </summary>
		void changeFilter()
		{
			string filter = "";

			//			eTypeShinryojo shinryo = Cast.Enumelate<eTypeShinryojo>(iKbnKaihi.ExGetValue(), eTypeShinryojo.None);
			//			if (shinryo != eTypeShinryojo.None)
			//			{
			//				if (filter != "") filter += " AND ";
			//				filter += $"{t_shokumu_price.FSMP_TypeShinryojo} = {(int)shinryo}";
			//			}

			// 会費区分の検索
			int kbnKaihiIdx = iKbnKaihi.SelectedIndex;
			if (kbnKaihiIdx > 0) // 空欄選択以外
			{
//				if (filter != "") filter += " AND ";
				filter += $"{t_kaihi.FKaihi_KaihiType} = {kbnKaihiIdx}";
			}

			// 会費コードの検索
			// 部分一致(or1桁目前方一致)が実装可能なら会費区分と組合せ、不可ならいずれかフィルタでよい(コード指定なら1件か0かだから区分絞り合わせるのは意味ない)
			// 部分一致検索にしたいため、文字列
			// 1桁目は前方一致にしたい
			// 数値用のCreateLikeKanaString()はない。部分一致はできるが前方一致の設定はない。
			//¶ 2026/11/20 CC 前方一致に変更。数値列を文字列に変換してLIKEで比較する
			string kaihiCd = StrConv.ToNarrow(iCodeKaihi.Text).Trim();
			if (Regex.IsMatch(kaihiCd, @"^[0-9]+$") == true)
			{
				if (filter != "") filter += " AND ";
				filter += $"(CONVERT({t_kaihi.FCD_Kaihi}, 'System.String') LIKE '{kaihiCd}%')";
			}

			//¶ 2026/11/20 CC 名称の検索（部分一致）
			if (iNameKaihi.Text != "")
			{
				if (filter != "") filter += " AND ";
				filter += DBQuery.CreateLikeKanaString(t_kaihi.FKaihi_Name, iNameKaihi.Text, true);
			}

			//¶ 2026/11/20 CC 検索表示の検索
			if (iKbnUsed.SelectedIndex > 0) //¶ 2026/11/20 CC 空欄選択以外
			{
				if (filter != "") filter += " AND ";
				filter += $"{t_kaihi.FKaihi_SearchUsed} = {Cast.Bool(iKbnUsed.ExGetValue())}";
			}

			// フィルタ適用
			dvKaihi.RowFilterQuery(filter);
		}

		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			funcKeys = new FunctionKeyBinder(funckey, FuncMasterKaihi.Functions);

			funcKeys.On(FuncMasterKaihi.RowAdd, rowAdd); // 追加
			funcKeys.On(FuncMasterKaihi.RowEdit, rowEdit); // 訂正
			funcKeys.On(FuncMasterKaihi.RowDelete, rowDelete); // 削除
			funcKeys.On(FuncMasterKaihi.CopyAdd, copyAdd); // コピー追加
			funcKeys.On(FuncMasterKaihi.Close, formClose); // 閉じる
		}

		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_kaihi nrow = new t_kaihi(dvKaihi.NewRow());

			// 内部項目カラムや初期値のセット
			nrow.ID_Kaihi = AppDbID.GetNewID(dvKaihi, t_kaihi.FID_Kaihi);
			nrow.Kaihi_SearchUsed = true; // 検索時表示:表示する

			FormMasterKaihi_DlgEntry frm = new FormMasterKaihi_DlgEntry();
			frm.Mode = FormMasterKaihi_DlgEntry.eMode.Add;
			frm.Row = nrow.Row;
			frm.ShowDialog();

			if (frm.FormCloseReason == FormCloseReason.Save)
			{
				dvKaihi.Add(frm.Row);
				AppGlobal.DB.UpdateTable(TableProp.t_kaihi);

				// 医会会費情報(共通)の再取得 ※情報が追加されたため
				AppGlobal.InitKaihi();

				// 追加行にフォーカス
				dvKaihi.SearchRow(t_kaihi.FID_Kaihi, nrow.ID_Kaihi);
			}

			frm.Dispose();
			frm = null; // なかったが念のため追加
		}

		/// <summary>
		/// コピー追加
		/// </summary>
		void copyAdd()
		{
			if (dvKaihi.Count > 0)
			{
				t_kaihi nrow = new t_kaihi(dvKaihi.NewRow());
				t_kaihi srow = new t_kaihi(dvKaihi.CurrentRow);

				AppDb.CopyDataRow(srow.Row, nrow.Row);

				// 内部項目カラムや初期値のセット
				nrow.ID_Kaihi = AppDbID.GetNewID(dvKaihi, t_kaihi.FID_Kaihi);
				nrow.CD_Kaihi_Null = null; // 会費コード:クリア
				//nrow.Kaihi_SearchUsed = true; // 検索時表示 引き継ぐ

				FormMasterKaihi_DlgEntry frm = new FormMasterKaihi_DlgEntry();
				frm.Mode = FormMasterKaihi_DlgEntry.eMode.Copy;
				frm.Row = nrow.Row;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					dvKaihi.Add(frm.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_kaihi);

					// 医会会費情報(共通)の再取得 ※情報が追加されたため
					AppGlobal.InitKaihi();

					// 追加行にフォーカス
					dvKaihi.SearchRow(t_kaihi.FID_Kaihi, nrow.ID_Kaihi);
				}

				frm.Dispose();
				frm = null;
			}
		}

		/// <summary>
		/// 編集
		/// </summary>
		void rowEdit()
		{
			if (dvKaihi.Count > 0)
			{
				DataRow row = dvKaihi.NewRow();

				AppDb.CopyDataRow(dvKaihi.CurrentRow.Row, row);

				FormMasterKaihi_DlgEntry frm = new FormMasterKaihi_DlgEntry();
				frm.Mode = FormMasterKaihi_DlgEntry.eMode.Edit;
				frm.Row = row;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					AppDb.CopyDataRow(frm.Row, dvKaihi.CurrentRow.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_kaihi);

					// 医会会費情報(共通)の再取得 ※情報が訂正されたため
					AppGlobal.InitKaihi();
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
			if (dvKaihi.Count > 0)
			{
				t_kaihi xrow = new t_kaihi(dvKaihi.CurrentRow);

				if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_kaihi, t_kaihi.FID_Kaihi, xrow.ID_Kaihi, TableProp.t_kaihi) == true)
				{
					AppMsgBox.Show(AppMsgBoxIndex.UsedOtherTable);
					return;
				}

				if (AppMsgBox.Show(AppMsgBoxIndex.DeleteSelectedRow) == System.Windows.Forms.DialogResult.Yes)
				{
					dvKaihi.Delete();
					AppGlobal.DB.UpdateTable(TableProp.t_kaihi);

					// 医会会費情報(共通)の再取得 ※情報が削除されたため
					AppGlobal.InitKaihi();
				}
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		void formClose()
		{
			// DBViewの破棄
			dvKaihi.Dispose();

			this.Close();
		}
	}
}
