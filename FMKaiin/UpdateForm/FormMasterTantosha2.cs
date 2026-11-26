using System;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 担当者マスタ（一覧）。<see cref="FormMasterTantosha"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。グリッド（C1TrueDBGrid）と列・データの結び付けは <see cref="SGridDB"/> を使う。
    /// 追加・訂正は、S 系の入力画面（<see cref="FormMasterTantosha2_DlgEntry"/>）で行う。
    /// </remarks>
    public partial class FormMasterTantosha2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        DBView dvTanto;
        SGridDB sgrid;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterTantosha2()
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
            // 追加時の動作仕様のため、再表示時(Shown()時)のみソートする(GetReFillTable()の利用)
            dvTanto = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_tantosha, $"ORDER BY {t_tantosha.FCD_Tanto}"), this.BindingContext);
            dvTanto.RowFilterQuery($"{t_tantosha.FTNT_Auth} <> {(int)eAuth.SU}");

            sgrid = new SGridDB(grid, this);
            sgrid.Add(new SGridDBText(t_tantosha.FCD_Tanto, "コード", 0.15f, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText(t_tantosha.FTNT_Name, "氏名", 0.4f));
            // 権限は、区分の値を名称にして表示する。幅は、残りいっぱい
            sgrid.Add(new SGridDBText(t_tantosha.FTNT_Auth, "権限", 0.0f) { UnboundFetch = ubAuth });
            sgrid.EndAdd(dvTanto);

            //■ イベント
            grid.MouseDoubleClick += grid_MouseDoubleClick;

            base.FormFrame_Shown(sender, e);
        }

        string ubAuth(SGridDBColumn col, int row)
        {
            t_tantosha xrow = new t_tantosha(dvTanto[row]);

            return enumKbn.DAuth[(int)xrow.TNT_Auth];
        }

        protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ShownでRefillしているので終了時に共通クラスを初期化し直す。
            AppGlobal.InitTanto();

            base.FormFrame_FormClosing(sender, e);
        }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dvTanto.Count > 0)
            {
                rowEdit();
            }
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterTanto.Functions);

            funcKeys.On(FuncMasterTanto.RowAdd, rowAdd);
            funcKeys.On(FuncMasterTanto.RowEdit, rowEdit);
            funcKeys.On(FuncMasterTanto.RowDelete, rowDelete);
            funcKeys.On(FuncMasterTanto.Close, formClose);
        }

        /// <summary>
        /// 追加
        /// </summary>
        void rowAdd()
        {
            t_tantosha nrow = new t_tantosha(dvTanto.NewRow());

            nrow.ID_Tanto = AppDbID.GetNewID(TableProp.t_tantosha, t_tantosha.FID_Tanto);
            nrow.CD_Tanto_Null = AppDbID.GetNewCode(TableProp.t_tantosha, t_tantosha.FCD_Tanto);
            nrow.TNT_Auth = eAuth.Admin;
            nrow.TNT_Used = true;

            FormMasterTantosha2_DlgEntry frm = new FormMasterTantosha2_DlgEntry();
            frm.Mode = FormMasterTantosha2_DlgEntry.eMode.Add;
            frm.Row = nrow.Row;
            frm.ShowDialog();

            if (frm.FormCloseReason == FormCloseReason.Save)
            {
                dvTanto.Add(frm.Row);
                AppGlobal.DB.UpdateTable(TableProp.t_tantosha);

                // 共通クラスの初期化処理
                AppGlobal.InitTanto();

                dvTanto.SearchRow(t_tantosha.FID_Tanto, nrow.ID_Tanto);
            }

            frm.Dispose();
            frm = null;
        }

        /// <summary>
        /// 編集
        /// </summary>
        void rowEdit()
        {
            if (dvTanto.Count > 0)
            {
                //■ 担当者レコード
                DataRow row = dvTanto.NewRow();

                AppDb.CopyDataRow(dvTanto.CurrentRow.Row, row);

                FormMasterTantosha2_DlgEntry frm = new FormMasterTantosha2_DlgEntry();
                frm.Mode = FormMasterTantosha2_DlgEntry.eMode.Edit;
                frm.Row = row;
                frm.ShowDialog();

                if (frm.FormCloseReason == FormCloseReason.Save)
                {
                    AppDb.CopyDataRow(frm.Row, dvTanto.CurrentRow.Row);
                    AppGlobal.DB.UpdateTable(TableProp.t_tantosha);

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
            if (dvTanto.Count > 0)
            {
                t_tantosha xrow = new t_tantosha(dvTanto.CurrentRow);

                if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_tantosha, t_tantosha.FID_Tanto, xrow.ID_Tanto, TableProp.t_tantosha) == true)
                {
                    AppMsgBox.Show(this, AppMsgBoxIndex.UsedOtherTable);
                    return;
                }

                string str = string.Format("{0} : {1}", xrow.CD_Tanto, xrow.TNT_Name);

                if (AppGlobal.LoginUser.ID == xrow.ID_Tanto)
                {
                    AppMsgBox.Show(this, AppMsgBoxIndex.CannotDeleteLoginUser, str);
                    return;
                }

                if (AppMsgBox.Show(this, AppMsgBoxIndex.Delete, str) == System.Windows.Forms.DialogResult.Yes)
                {
                    dvTanto.Delete();
                    AppGlobal.DB.UpdateTable(TableProp.t_tantosha);
                    // 共通クラスの初期化処理
                    AppGlobal.InitTanto();
                }
            }
        }

        void formClose()
        {
            // DBViewの破棄
            dvTanto.Dispose();

            this.Close();
        }
    }
}
