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
    /// 診療科マスタ（一覧）。<see cref="FormMasterShinryoka"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。グリッド（C1TrueDBGrid）と列・データの結び付けは <see cref="SGridDB"/>、
    /// 検索欄の入力ルールは <see cref="AppSControlRule"/> を使う。
    /// 追加・訂正は、S 系の入力画面（<see cref="FormMasterShinryoka2_DlgEntry"/>）で行う。
    /// </remarks>
    public partial class FormMasterShinryoka2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        DBView dvShinryoka;
        SGridDB sgrid;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterShinryoka2()
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
            // 名称検索欄の入力ルール（コードの検索欄には、元の画面と同じく、ルールを設定しない）
            AppSControlRule.SetControlByRule(iNameShinryoka, t_shinryoka.FSRK_Name);

            // 追加時の動作仕様のため、再表示時(Shown()時)のみソートする(GetReFillTable()の利用)
            dvShinryoka = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_shinryoka, $"ORDER BY {t_shinryoka.FSRK_Code}"), this.BindingContext);

            // グリッド作成
            sgrid = new SGridDB(grid, this);
            sgrid.Add(new SGridDBText(t_shinryoka.FSRK_Code, "コード", 75, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText(t_shinryoka.FSRK_Name, "名称", 0.0f));
            sgrid.EndAdd(dvShinryoka);

            changeFilter();

            grid.Select();

            //■ イベント
            grid.MouseDoubleClick += grid_MouseDoubleClick;
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
            iCodeShinryoka.TextChanged -= iCodeShinryoka_TextChanged;
            iNameShinryoka.TextChanged -= iNameShinryoka_TextChanged;

            // 条件初期化
            iCodeShinryoka.Clear();
            iNameShinryoka.Clear();

            // 初期条件でフィルタ
            changeFilter();

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

            // コードの検索（前方一致）。コードは数値の項目なので、文字列に変換して LIKE で比べる。
            // 数字以外が入っているときは、コードでは絞り込まない
            string cd = StrConv.ToNarrow(iCodeShinryoka.Text).Trim();
            if (Regex.IsMatch(cd, @"^[0-9]+$") == true)
            {
                if (filter != "") filter += " AND ";
                filter += $"(CONVERT({t_shinryoka.FSRK_Code}, 'System.String') LIKE '{cd}%')";
            }

            // 名称の検索（部分一致）
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

            FormMasterShinryoka2_DlgEntry frm = new FormMasterShinryoka2_DlgEntry();
            frm.Mode = FormMasterShinryoka2_DlgEntry.eMode.Add;
            frm.Row = nrow.Row;
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
            frm = null;
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

                FormMasterShinryoka2_DlgEntry frm = new FormMasterShinryoka2_DlgEntry();
                frm.Mode = FormMasterShinryoka2_DlgEntry.eMode.Edit;
                frm.Row = row;
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
