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
			// 会費区分コード テーブル保存予定のためそこからボックス値をセット予定
//			AppCombo.SetComboBox(iKbnKaihi, enumKbn.DTypeShinryojo, true, (int)eTypeShinryojo.None);

			//			public static void SetComboBox(GcComboBoxEx cmb, Dictionary<int, string> dics, bool select_all, params int[] ignores)
			//			{
			iKbnKaihi.ExBeginUpdate();
			iKbnKaihi.ExClearItem();

//			AppGlobal.InitKaihi(); // FormFrame_FormClosing()内のInitはここでもよい

			bool select_all = true;
			if (select_all == true)
			{
				iKbnKaihi.ExAddItem("すべて", -1);
			}

			iKbnKaihi.ExEndUpdate();


			// 会費コード
			//			AppCombo.SetComboBox(iDay, enumKbn.DTypeDay, true, (int)eTypeDay.None);

			// 初期選択値
			//			iKbnKaihi.ExSetSelectedIndexByValue((int)AppGlobal.LoginUser.XRow.TNT_TypeShinryojo);
			//			iDay.ExSetSelectedIndexByValue(AppCombo.SelectAllValue);

			// マスタ追加時の動作仕様のため、再表示時にのみソートする(GetReFillTable()の利用)
			dvKaihi = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaihi, $"ORDER BY {t_kaihi.FCD_Kaihi}"), this.BindingContext);

			// グリッド作成
			AppGridCommon.StyleSet(grid);

			gcom = new GGridDBCommon(grid, this);
			gcom.Add(new GGridDBText(t_kaihi.FCD_Kaihi, "会費コード", 75));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_Name, "名称", 90));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_ShortName, "略称", 90));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_Bikou, "備考", 165));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_GunCode, "会費群", 60));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_SearchUsed, "検索", 60));
			gcom.SetUnboundColumnFetch(ubUsed);
//			gcom.Add(new GGridDBText(t_kaihi.FKaihi_BankCode, "銀行コード", 60));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_BankCode, "銀行名", 90));
			gcom.SetUnboundColumnFetch(ubBankName);
//			gcom.Add(new GGridDBText(t_kaihi.FKaihi_BankCodeShiten, "支店コード", 60));
			gcom.Add(new GGridDBText(t_kaihi.FKaihi_BankCodeShiten, "支店名", 90));
			gcom.SetUnboundColumnFetch(ubBankNameShiten);

			gcom.EndAdd(dvKaihi);

			changeFilter();

			grid.Select();

			//■ イベント
			grid.MouseDoubleClick += grid_MouseDoubleClick;
			iKbnKaihi.SelectedIndexChanged += iKbnKaihi_SelectedIndexChanged;
			iCodeKaihi.TextChanged += iCodeKaihi_TextChanged;
			btnClear.Click += btnClear_Click;

			base.FormFrame_Shown(sender, e);
		}

		/// <summary>
		/// クリアボタンクリック
		/// </summary>
		private void btnClear_Click(object sender, EventArgs e)
		{
			iKbnKaihi.SelectedIndexChanged -= iKbnKaihi_SelectedIndexChanged;
			iCodeKaihi.TextChanged -= iCodeKaihi_TextChanged;

			// 条件初期化
			iKbnKaihi.ExSetSelectedIndexByValue(AppCombo.SelectAllValue);
			iCodeKaihi.ResetText();

			// 初期条件でフィルタ
			changeFilter();

			iKbnKaihi.SelectedIndexChanged += iKbnKaihi_SelectedIndexChanged;
			iCodeKaihi.TextChanged += iCodeKaihi_TextChanged;
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
			// ShownでRefillしているので終了時に共通クラスを初期化し直す。
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
			// 区分はテーブルで持つ予定 データ量でコンボか入力か決めたい
			// ※会費コードの頭が会費区分ということなら、会費コードのテキスト検索だけにしたい
			int kbnKaihiId = Cast.Int(iKbnKaihi.Text); // ★コンボの中身決定後要検討
			if (kbnKaihiId != 0)
			{
				if (filter != "") filter += " AND ";
				filter += $"{t_kaihi.FID_KaihiKbn} = {kbnKaihiId}";
			}

			// 会費コードの検索
			// 部分一致検索にしたいため、文字列
			// 1桁目は前方一致にしたい
			// 数値用のCreateLikeKanaString()はない。部分一致はできるが前方一致の設定はない。
			int kaihiCd = Cast.Int(iCodeKaihi.Text);
			if (kaihiCd != 0)
			{
				if (filter != "") filter += " AND ";
				// 数値のみならコード検索
				filter += string.Format("({0} = {1})", t_kaihi.FCD_Kaihi, kaihiCd);
			}

			// フィルタ適用
			dvKaihi.RowFilterQuery(filter);
		}


		/// <summary>
		/// ファンクションの設定
		/// </summary>
		protected override void SetFunction()
		{
			appFuncKey = new AppFunctionKey(funckey, FuncMasterKaihi.Functions);

			FuncMasterKaihi.RowAdd.Execute		= rowAdd; // 追加
			FuncMasterKaihi.RowEdit.Execute	= rowEdit; // 訂正
			FuncMasterKaihi.RowDelete.Execute	= rowDelete; // 削除
//			FuncMasterKaihi.CopyAdd.Execute	= copyAdd;
			FuncMasterKaihi.Close.Execute		= formClose; // 閉じる
		}
/*
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

				nrow.ID_ShokumuPrice		= AppDbID.GetNewID(dvKaihi, t_kaihi.FID_ShokumuPrice);

				FormMasterKinmuTanka_DlgEntry frm	= new FormMasterKinmuTanka_DlgEntry();
				frm.Mode							= FormMasterKinmuTanka_DlgEntry.eMode.Copy;
				frm.Row								= nrow.Row;
				frm.DbView							= dvKaihi;
				frm.ShowDialog();

				if (frm.FormCloseReason == FormCloseReason.Save)
				{
					dvKaihi.Add(frm.Row);
					AppGlobal.DB.UpdateTable(TableProp.t_kaihi);

					// 共通クラスの初期化処理
					AppGlobal.InitShokumuPrice();
					dvKaihi.SearchRow(t_kaihi.FID_ShokumuPrice, nrow.ID_ShokumuPrice);
				}

				frm.Dispose();
			}
		}
*/
		/// <summary>
		/// 追加
		/// </summary>
		void rowAdd()
		{
			t_kaihi nrow = new t_kaihi(dvKaihi.NewRow());

			// 画面項目未関連カラムの値セット
			nrow.ID_Kaihi = AppDbID.GetNewID(dvKaihi, t_kaihi.FID_Kaihi);

			FormMasterKaihi_DlgEntry frm	= new FormMasterKaihi_DlgEntry();
			frm.Mode							= FormMasterKaihi_DlgEntry.eMode.Add;
			frm.Row								= nrow.Row;
//			frm.DbView							= dvKaihi;
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
		/// 編集
		/// </summary>
		void rowEdit()
		{
			if (dvKaihi.Count > 0)
			{
				DataRow row = dvKaihi.NewRow();

				AppDb.CopyDataRow(dvKaihi.CurrentRow.Row, row);

				FormMasterKaihi_DlgEntry frm = new FormMasterKaihi_DlgEntry();
				frm.Mode		= FormMasterKaihi_DlgEntry.eMode.Edit;
				frm.Row			= row;
//				frm.DbView		= dvKaihi;
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
			this.Close();
		}

	}
}
