using System;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 銀行コード選択リスト。<see cref="FormSelectorBankCode"/> と同じ内容を、S 系の部品（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。グリッド（C1TrueDBGrid）と列・データの結び付けは <see cref="SGridDB"/>、
    /// 検索欄の入力ルールは <see cref="AppSControlRule"/> を使う。
    /// </remarks>
    public partial class FormSelectorBankCode2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        DBView dvBC;
        SGridDB sgrid;

        /// <summary>
        /// 選択された銀行コードのレコード
        /// </summary>
        public t_bank_code SelectedBankCode { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormSelectorBankCode2()
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
                        execClose();
                    }
                    break;
            }

            base.FormFrame_KeyDown(sender, e);
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            dvBC = new DBView(AppGlobal.BankCodeMg.DbView, this.BindingContext);
            dvBC.SortQuery($"{t_bank_code.FBCD_Code}, {t_bank_code.FBCD_CodeShiten}");

            sgrid = new SGridDB(grid, this);
            sgrid.Add(new SGridDBText(t_bank_code.FBCD_Code, "銀行ｺｰﾄﾞ", 0.15f, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText(t_bank_code.FBCD_FullName, "銀行名", 0.35f));
            sgrid.Add(new SGridDBText(t_bank_code.FBCD_CodeShiten, "支店ｺｰﾄﾞ", 0.15f, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText(t_bank_code.FBCD_NameShiten, "支店名", 0.0f));
            sgrid.EndAdd(dvBC);

            if (SelectedBankCode != null)
            {
                // 選択されている銀行コードの行を指して、見える位置に表示する
                dvBC.FindRow(new object[] { SelectedBankCode.BCD_Code, SelectedBankCode.BCD_CodeShiten });
                sgrid.SetRow(grid.Row);
            }

            // t_bankは存在しない可能性ありのためt_bank_codeを指定
            AppSControlRule.SetControlByRule(iBank, t_bank_code.FBCD_Name);
            AppSControlRule.SetControlByRule(iShiten, t_bank_code.FBCD_NameShiten);

            // イベント登録
            grid.MouseDoubleClick += grid_MouseDoubleClick;
            iBank.TextChanged += iBank_TextChanged;
            iShiten.TextChanged += iShiten_TextChanged;

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// 検索欄の内容で、一覧を絞り込む
        /// </summary>
        void viewFilter()
        {
            dvBC.RowFilterQuery(AppBankCodeFilter.Create(iBank.Text, iShiten.Text));
        }

        private void iShiten_TextChanged(object sender, EventArgs e)
        {
            viewFilter();
            grid.Row = 0;
        }

        private void iBank_TextChanged(object sender, EventArgs e)
        {
            viewFilter();
            grid.Row = 0;
        }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            execClose();
        }

        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncSelectorBankCode.Functions);

            funcKeys.On(FuncSelectorBankCode.Clear, filterClear);
            funcKeys.On(FuncSelectorBankCode.Exec, execClose);
            funcKeys.On(FuncSelectorBankCode.Cancel, cancelClose);
        }

        /// <summary>
        /// 検索欄を空にして、すべての行を表示する
        /// </summary>
        void filterClear()
        {
            iBank.TextChanged -= iBank_TextChanged;
            iShiten.TextChanged -= iShiten_TextChanged;

            iBank.Clear();
            iShiten.Clear();

            viewFilter();

            iBank.Select();

            iBank.TextChanged += iBank_TextChanged;
            iShiten.TextChanged += iShiten_TextChanged;
        }

        void execClose()
        {
            if (dvBC.Count > 0)
            {
                formCloseReason = FormCloseReason.Exec;
                SelectedBankCode = new t_bank_code(dvBC.CurrentRow);
                this.Close();
            }
        }

        void cancelClose()
        {
            this.Close();
        }
    }
}
