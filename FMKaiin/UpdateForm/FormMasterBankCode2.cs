using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 銀行コードマスタ（一覧）。<see cref="FormMasterBankCode"/> と同じ内容を、S 系の部品（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。グリッド（C1TrueDBGrid）と列・データの結び付けは <see cref="SGridDB"/>、
    /// 検索欄の入力ルールは <see cref="AppSControlRule"/> を使う。
    /// 追加・訂正は、S 系の入力画面（<see cref="FormMasterBankCode2_DlgEntry"/>）で行う。
    /// </remarks>
    public partial class FormMasterBankCode2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        DBView dvBankCode;
        SGridDB sgrid;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterBankCode2()
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

        protected override void FormFrame_Load(object sender, EventArgs e)
        {
            // 件数が多いので、読み込みの間は進行状況の画面を出す
            FormBg_Progress prog = new FormBg_Progress();
            prog.DoWorkEvent += prog_DoWorkEvent;
            prog.TitleText = "しばらくお待ちください。";
            prog.LabelText = "データを取得しています。";
            prog.ShowDialog();
            prog.Dispose();
            prog = null;

            base.FormFrame_Load(sender, e);
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
            sgrid = new SGridDB(grid, this);
            // 銀行コードは 4 桁、支店コードは 3 桁に、ゼロ埋めで表示する。
            // コードの列の幅は、見出し（銀行ｺｰﾄﾞ・支店ｺｰﾄﾞ）が 1 行に収まるよう、元の画面（0.10）より少し広げてある
            sgrid.Add(new SGridDBText(t_bank_code.FBCD_Code, "銀行ｺｰﾄﾞ", 0.12f, SGridDBCellDisp.Right) { UnboundFetch = ubCode });
            sgrid.Add(new SGridDBText(t_bank_code.FBCD_FullName, "銀行名（正式名称）", 0.42f));
            sgrid.Add(new SGridDBText(t_bank_code.FBCD_CodeShiten, "支店ｺｰﾄﾞ", 0.12f, SGridDBCellDisp.Right) { UnboundFetch = ubCodeShiten });
            sgrid.Add(new SGridDBText(t_bank_code.FBCD_NameShiten, "支店名", 0.0f));
            sgrid.EndAdd(dvBankCode);

            // 検索欄の入力ルール（FormSelectorBankCode2 と同じ）
            AppSControlRule.SetControlByRule(iBank, t_bank_code.FBCD_Name);
            AppSControlRule.SetControlByRule(iShiten, t_bank_code.FBCD_NameShiten);

            //■ イベント
            grid.MouseDoubleClick += grid_MouseDoubleClick;
            iBank.TextChanged += iBank_TextChanged;
            iShiten.TextChanged += iShiten_TextChanged;
            btnClear.Click += btnClear_Click;

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// grid 銀行コードの文字列編集（4桁ゼロ埋め）
        /// </summary>
        string ubCode(SGridDBColumn col, int row)
        {
            t_bank_code xrow = new t_bank_code(dvBankCode[row]);

            return Cast.String(xrow.BCD_Code).PadLeft(4, '0');
        }

        /// <summary>
        /// grid 支店コードの文字列編集（3桁ゼロ埋め）
        /// </summary>
        string ubCodeShiten(SGridDBColumn col, int row)
        {
            t_bank_code xrow = new t_bank_code(dvBankCode[row]);

            return Cast.String(xrow.BCD_CodeShiten).PadLeft(3, '0');
        }

        /// <summary>
        /// フィルタ変更（検索条件は FormSelectorBankCode2 と同じ）
        /// </summary>
        void viewFilter()
        {
            dvBankCode.RowFilterQuery(AppBankCodeFilter.Create(iBank.Text, iShiten.Text));
        }

        /// <summary>
        /// 銀行検索 値変更
        /// </summary>
        private void iBank_TextChanged(object sender, EventArgs e)
        {
            viewFilter();
            grid.Row = 0;
        }

        /// <summary>
        /// 支店検索 値変更
        /// </summary>
        private void iShiten_TextChanged(object sender, EventArgs e)
        {
            viewFilter();
            grid.Row = 0;
        }

        /// <summary>
        /// 検索クリアボタンクリック
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            iBank.TextChanged -= iBank_TextChanged;
            iShiten.TextChanged -= iShiten_TextChanged;

            iBank.Clear();
            iShiten.Clear();

            viewFilter();
            grid.Row = 0;

            iBank.Select();

            iBank.TextChanged += iBank_TextChanged;
            iShiten.TextChanged += iShiten_TextChanged;
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
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterBankCode.Functions);

            funcKeys.On(FuncMasterBankCode.RowAdd, rowAdd);
            funcKeys.On(FuncMasterBankCode.RowEdit, rowEdit);
            funcKeys.On(FuncMasterBankCode.RowDelete, rowDelete);
            funcKeys.On(FuncMasterBankCode.Close, formClose);
        }

        /// <summary>
        /// 追加
        /// </summary>
        void rowAdd()
        {
            t_bank_code nrow = new t_bank_code(dvBankCode.NewRow());

            FormMasterBankCode2_DlgEntry frm = new FormMasterBankCode2_DlgEntry();
            frm.Mode = FormMasterBankCode2_DlgEntry.eMode.Add;
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

                FormMasterBankCode2_DlgEntry frm = new FormMasterBankCode2_DlgEntry();
                frm.Mode = FormMasterBankCode2_DlgEntry.eMode.Edit;
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

                string str = string.Format("銀行コード：{0}　支店コード:{1}", xrow.BCD_Code, xrow.BCD_CodeShiten);

                if (AppMsgBox.Show(this, AppMsgBoxIndex.Delete, str) == System.Windows.Forms.DialogResult.Yes &&
                    AppMsgBox.Show(this, AppMsgBoxIndex.Delete2) == DialogResult.Yes)
                {
                    dvBankCode.Delete();
                    AppGlobal.DB.UpdateTable(TableProp.t_bank_code);

                    AppGlobal.InitBankCodeMg();
                }
            }
        }

        void formClose()
        {
            // DBViewの破棄
            dvBankCode.Dispose();

            this.Close();
        }
    }
}
