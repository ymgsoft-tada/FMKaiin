using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 医会会費マスタ（一覧）。<see cref="FormMasterKaihi"/> と同じ内容を、S 系の部品（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。グリッド（C1TrueDBGrid）と列・データの結び付けは <see cref="SGridDB"/>、
    /// 検索欄の入力ルールは <see cref="AppSControlRule"/>、会費区分の一覧は <see cref="AppSCombo"/> を使う。
    /// 追加・訂正・コピー追加は、S 系の入力画面（<see cref="FormMasterKaihi2_DlgEntry"/>）で行う。
    /// 銀行名・支店名の列は、銀行コードの表（AppGlobal.BankCodeMg）から引くので、この画面を開く前に、銀行コードを読み込んでおくこと
    /// （メニューは、元の画面と同じく、読み込んでから開く）。
    /// </remarks>
    public partial class FormMasterKaihi2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>検索表示のコンボの、先頭の空欄（すべて）の項目の値。</summary>
        private const string KbnUsedAll = "all";

        DBView dvKaihi;

        SGridDB sgrid;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterKaihi2()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
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
            // コントロールの制御ルール指定
            AppSControlRule.SetControlByRule(iCodeKaihi, t_kaihi.FCD_Kaihi);

            // コンボボックス値セット
            // 会費区分コード Enum
            AppSCombo.SetComboBox(iKbnKaihi, enumKbn.DTypeKaihi);

            // 検索表示コンボ（手動セット。FormMasterIryoKikan2 の退会区分と同じ作り）
            iKbnUsed.BeginUpdate();
            iKbnUsed.Items.Clear();
            iKbnUsed.AddItem("", KbnUsedAll); // "すべて"代替
            iKbnUsed.AddItem("表示", true);
            iKbnUsed.AddItem("非表示", false);
            iKbnUsed.EndUpdate();

            // 名称検索欄の入力ルール
            AppSControlRule.SetControlByRule(iNameKaihi, t_kaihi.FKaihi_Name);

            // 追加時の動作仕様のため、再表示時(Shown()時)のみソートする(GetReFillTable()の利用)
            dvKaihi = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_kaihi, $"ORDER BY {t_kaihi.FCD_Kaihi}"), this.BindingContext);

            // グリッド作成
            sgrid = new SGridDB(grid, this);

            // 備考は、右端（残りの幅）。
            // 区分の列の幅は、いちばん長い名称（グループ会費）が収まるよう、元の画面（80）より広げてある
            sgrid.Add(new SGridDBText(t_kaihi.FKaihi_KaihiType, "区分", 100) { UnboundFetch = ubKaihiKbn });
            sgrid.Add(new SGridDBText(t_kaihi.FCD_Kaihi, "コード", 70, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText(t_kaihi.FKaihi_Name, "名称", 200));
            sgrid.Add(new SGridDBText(t_kaihi.FKaihi_SearchUsed, "検索表示", 70, SGridDBCellDisp.Center) { UnboundFetch = ubUsed });
            sgrid.Add(new SGridDBText(t_kaihi.FKaihi_BankCode, "銀行名", 130) { UnboundFetch = ubBankName });
            sgrid.Add(new SGridDBText(t_kaihi.FKaihi_BankCodeShiten, "支店名", 130) { UnboundFetch = ubBankNameShiten });
            sgrid.Add(new SGridDBText(t_kaihi.FKaihi_Biko, "備考", 0.0f));

            sgrid.EndAdd(dvKaihi);

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
            iKbnKaihi.SelectedValue = (int)eTypeKaihi.None; // 先頭の空欄項目に戻す
            iCodeKaihi.Clear();
            iNameKaihi.Clear();
            iKbnUsed.SelectedValue = KbnUsedAll;

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
        string ubKaihiKbn(SGridDBColumn col, int row)
        {
            t_kaihi xrow = new t_kaihi(dvKaihi[row]);
            return enumKbn.DTypeKaihi[(int)xrow.Kaihi_KaihiType];
        }

        /// <summary>
        /// grid 検索時表示有無の文字列編集
        /// </summary>
        string ubUsed(SGridDBColumn col, int row)
        {
            t_kaihi xrow = new t_kaihi(dvKaihi[row]);

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
        string ubBankName(SGridDBColumn col, int row)
        {
            t_kaihi xrow = new t_kaihi(dvKaihi[row]);

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
        string ubBankNameShiten(SGridDBColumn col, int row)
        {
            t_kaihi xrow = new t_kaihi(dvKaihi[row]);

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

            // 会費区分の検索（一覧の並びの番号が、そのまま区分の値になっている）
            int kbnKaihiIdx = iKbnKaihi.SelectedIndex;
            if (kbnKaihiIdx > 0) // 空欄選択以外
            {
                filter += $"{t_kaihi.FKaihi_KaihiType} = {kbnKaihiIdx}";
            }

            // 会費コードの検索（前方一致）。コードは数値の項目なので、文字列に変換して LIKE で比べる。
            // 数字以外が入っているときは、コードでは絞り込まない
            string kaihiCd = StrConv.ToNarrow(iCodeKaihi.Text).Trim();
            if (Regex.IsMatch(kaihiCd, @"^[0-9]+$") == true)
            {
                if (filter != "") filter += " AND ";
                filter += $"(CONVERT({t_kaihi.FCD_Kaihi}, 'System.String') LIKE '{kaihiCd}%')";
            }

            // 名称の検索（部分一致）
            if (iNameKaihi.Text != "")
            {
                if (filter != "") filter += " AND ";
                filter += DBQuery.CreateLikeKanaString(t_kaihi.FKaihi_Name, iNameKaihi.Text, true);
            }

            // 検索表示の検索
            if (iKbnUsed.SelectedIndex > 0) // 空欄選択以外
            {
                if (filter != "") filter += " AND ";
                filter += $"{t_kaihi.FKaihi_SearchUsed} = {Cast.Bool(iKbnUsed.SelectedValue)}";
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

            FormMasterKaihi2_DlgEntry frm = new FormMasterKaihi2_DlgEntry();
            frm.Mode = FormMasterKaihi2_DlgEntry.eMode.Add;
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
                nrow.CD_Kaihi_Null = null; // 会費コード:クリア（検索時表示は、コピー元のまま）

                FormMasterKaihi2_DlgEntry frm = new FormMasterKaihi2_DlgEntry();
                frm.Mode = FormMasterKaihi2_DlgEntry.eMode.Copy;
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

                FormMasterKaihi2_DlgEntry frm = new FormMasterKaihi2_DlgEntry();
                frm.Mode = FormMasterKaihi2_DlgEntry.eMode.Edit;
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
