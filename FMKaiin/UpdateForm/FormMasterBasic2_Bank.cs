using System;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 銀行情報マスタ（一覧）。<see cref="FormMasterBank"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。グリッド（C1TrueDBGrid）と列・データの結び付けは <see cref="SGridDB"/> を使う。
    /// 追加・訂正は、S 系の入力画面（<see cref="FormMasterBasic2_Bank_DlgEntry"/>）で行う。
    /// </remarks>
    public partial class FormMasterBasic2_Bank : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        DBView dvBank;
        SGridDB sgrid;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterBasic2_Bank()
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
            dvBank = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_bank, $"ORDER BY {t_bank.FBNK_Code}"), this.BindingContext);

            sgrid = new SGridDB(grid, this);
            // 銀行名は、銀行コード・支店コードから引いた名称を表示する。区分は、区分の値を名称にして表示する
            sgrid.Add(new SGridDBText(t_bank.FBNK_Code, "銀行名", 0.6f, SGridDBCellDisp.Left) { UnboundFetch = ubName });
            sgrid.Add(new SGridDBText(t_bank.FBNK_KozaType, "区分", 0.15f, SGridDBCellDisp.Center) { UnboundFetch = ubType });
            sgrid.Add(new SGridDBText(t_bank.FBNK_KozaNo, "口座番号", 0.0f, SGridDBCellDisp.Right));
            sgrid.EndAdd(dvBank);

            //■ イベント
            grid.MouseDoubleClick += grid_MouseDoubleClick;

            base.FormFrame_Shown(sender, e);
        }

        string ubName(SGridDBColumn col, int row)
        {
            t_bank xrow = new t_bank(dvBank[row]);

            t_bank_code bc = AppGlobal.BankCodeMg.GetBankCodeRow(xrow.BNK_Code, xrow.BNK_CodeShiten);
            string str = "";
            if (bc != null)
            {
                str += $"{bc.BCD_FullName}　{bc.BCD_NameShiten}支店";
            }

            return str;
        }

        string ubType(SGridDBColumn col, int row)
        {
            t_bank xrow = new t_bank(dvBank[row]);

            return enumKbn.DTypeKoza[(int)xrow.BNK_KozaType];
        }

        protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ShownでRefillしているので終了時に共通クラスを初期化し直す。
            AppGlobal.InitBasic();

            base.FormFrame_FormClosing(sender, e);
        }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dvBank.Count > 0)
            {
                rowEdit();
            }
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterBank.Functions);

            funcKeys.On(FuncMasterBank.RowAdd, rowAdd);
            funcKeys.On(FuncMasterBank.RowEdit, rowEdit);
            funcKeys.On(FuncMasterBank.RowDelete, rowDelete);
            funcKeys.On(FuncMasterBank.Close, formClose);
        }

        /// <summary>
        /// 追加
        /// </summary>
        void rowAdd()
        {
            t_bank nrow = new t_bank(dvBank.NewRow());

            nrow.ID_Bank = AppDbID.GetNewID(dvBank, t_bank.FID_Bank);
            nrow.BNK_KozaType = eTypeKoza.Futsu;

            FormMasterBasic2_Bank_DlgEntry frm = new FormMasterBasic2_Bank_DlgEntry();
            frm.Mode = FormMasterBasic2_Bank_DlgEntry.eMode.Add;
            frm.Row = nrow.Row;
            frm.ShowDialog();

            if (frm.FormCloseReason == FormCloseReason.Save)
            {
                dvBank.Add(frm.Row);
                AppGlobal.DB.UpdateTable(TableProp.t_bank);

                dvBank.SearchRow(t_bank.FID_Bank, nrow.ID_Bank);
            }

            frm.Dispose();
            frm = null;
        }

        /// <summary>
        /// 編集
        /// </summary>
        void rowEdit()
        {
            if (dvBank.Count > 0)
            {
                DataRow row = dvBank.NewRow();

                AppDb.CopyDataRow(dvBank.CurrentRow.Row, row);

                FormMasterBasic2_Bank_DlgEntry frm = new FormMasterBasic2_Bank_DlgEntry();
                frm.Mode = FormMasterBasic2_Bank_DlgEntry.eMode.Edit;
                frm.Row = row;
                frm.ShowDialog();

                if (frm.FormCloseReason == FormCloseReason.Save)
                {
                    AppDb.CopyDataRow(frm.Row, dvBank.CurrentRow.Row);
                    AppGlobal.DB.UpdateTable(TableProp.t_bank);
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
            if (dvBank.Count > 0)
            {
                t_bank xrow = new t_bank(dvBank.CurrentRow);

                if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_bank, t_bank.FID_Bank, xrow.ID_Bank, TableProp.t_bank) == true)
                {
                    AppMsgBox.Show(this, AppMsgBoxIndex.UsedOtherTable);
                    return;
                }

                string str = string.Format("{0} : {1}", xrow.BNK_Code, xrow.BNK_Name);

                if (AppMsgBox.Show(this, AppMsgBoxIndex.Delete, str) == System.Windows.Forms.DialogResult.Yes)
                {
                    dvBank.Delete();
                    AppGlobal.DB.UpdateTable(TableProp.t_bank);
                }
            }
        }

        void formClose()
        {
            // DBViewの破棄
            dvBank.Dispose();

            this.Close();
        }
    }
}
