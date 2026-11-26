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
    /// 医療機関マスタ（一覧）。<see cref="FormMasterIryoKikan"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// この画面は、Gc 系のコントロール・コードを使っていない。グリッド（C1TrueDBGrid）と列・データの結び付けは <see cref="SGridDB"/>、
    /// 検索欄の入力ルールは <see cref="AppSControlRule"/>、組コードの一覧は <see cref="AppSTableCombo"/> を使う。
    /// 追加・訂正は、S 系の入力画面（<see cref="FormMasterIryoKikan2_DlgEntry"/>）で行う。
    /// 印刷プレビュー（帳票の選択）は、S 系の画面の見た目で作った画面（<see cref="FormMasterIryoKikan2_Out"/>）で行う。
    /// </remarks>
    public partial class FormMasterIryoKikan2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>退会区分のコンボの、先頭の空欄（すべて）の項目の値。</summary>
        private const string KbnTaikaiAll = "all";

        DBView dvKikan;
        SGridDB sgrid;

        /// <summary>
        /// 組コード情報 DBView iSearch
        /// </summary>
        DBView dvKumi;

        /// <summary>
        /// 医療機関-診療科の関連 DBView (_DlgEntryにて操作)
        /// </summary>
        DBView dvIryokikanShinryoka;

        public FormMasterIryoKikan2()
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
            AppSControlRule.SetControlByRule(iNameIryo, t_iryokikan.FIRK_Name);

            // コンボボックス
            // 手動セット
            iKbnTaikai.BeginUpdate();
            iKbnTaikai.Items.Clear();
            iKbnTaikai.AddItem("", KbnTaikaiAll); // "すべて"代替
            iKbnTaikai.AddItem("会員", true);
            iKbnTaikai.AddItem("退会", false);
            iKbnTaikai.EndUpdate();

            // iSearchセット
            dvKumi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kumicd)); // ソートはAppSTableCombo側実施
            AppSTableCombo.SetComboBox_KumiCode(iKumiCode, dvKumi); // 組コード

            // 追加時の動作仕様のため、再表示時(Shown()時)のみソートする(GetReFillTable()の利用)
            dvKikan = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_iryokikan, $"ORDER BY {t_iryokikan.FIRK_Code}"), this.BindingContext);

            sgrid = new SGridDB(grid, this);
            sgrid.Add(new SGridDBText(t_iryokikan.FIRK_Code, "コード", 0.10f, SGridDBCellDisp.Right));
            sgrid.Add(new SGridDBText(t_iryokikan.FIRK_Name, "施設正式名", 0.5f));
            sgrid.Add(new SGridDBText(t_iryokikan.FIRK_KumiCode, "組コード", 0.15f, SGridDBCellDisp.Left) { UnboundFetch = ubKumiCode });
            sgrid.Add(new SGridDBText(t_iryokikan.FIRK_KaisetsuShutai, "開設主体", 0.15f) { UnboundFetch = ubKaisetsuShutai });
            sgrid.Add(new SGridDBText(t_iryokikan.FIRK_TaikaiKbn, "退会区分", 0.0f, SGridDBCellDisp.Center) { UnboundFetch = ubTaikaiKbn });
            sgrid.EndAdd(dvKikan);

            // _DlgEntry画面のGrid用データ取得
            dvIryokikanShinryoka = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_iryokikan_shinryoka)); // BindingContext:DBViewのカレント行とGridのカレント行追従させるため必要

            changeFilter();

            //■ イベント
            grid.MouseDoubleClick += grid_MouseDoubleClick;
            iKbnTaikai.SelectedIndexChanged += iKbnTaikai_SelectedIndexChanged;
            iKumiCode.SelectedIndexChanged += iKumiCode_SelectedIndexChanged;
            iCodeIryo.TextChanged += iCodeIryo_TextChanged;
            iNameIryo.TextChanged += iNameIryo_TextChanged;
            btnClear.Click += btnClear_Click;

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// grid 開設主体の文字列編集（ID を名称にする）
        /// </summary>
        string ubKaisetsuShutai(SGridDBColumn col, int row)
        {
            t_iryokikan xrow = new t_iryokikan(dvKikan[row]);

            // 開設主体情報の取得
            Kaisetsushutai ks = AppGlobal.Kaisetsushutais.Get(Cast.Int(xrow.IRK_KaisetsuShutai_Null));

            // 該当情報が存在したら名称をreturn
            if (ks != null)
            {
                return ks.XRow.KST_Name_Null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// grid 組コードの文字列編集（ID を名称にする）
        /// </summary>
        string ubKumiCode(SGridDBColumn col, int row)
        {
            t_iryokikan xrow = new t_iryokikan(dvKikan[row]);

            // 組コード情報の取得
            KumiCode kc = AppGlobal.KumiCodes.Get(Cast.Int(xrow.IRK_KumiCode_Null));

            // 該当情報が存在したら名称をreturn
            if (kc != null)
            {
                return kc.XRow.KMC_Name_Null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// grid 退会区分の文字列編集
        /// </summary>
        string ubTaikaiKbn(SGridDBColumn col, int row)
        {
            t_iryokikan xrow = new t_iryokikan(dvKikan[row]);

            if (xrow.IRK_TaikaiKbn == true)
            {
                return "会員";
            }
            else
            {
                return "退会";
            }
        }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dvKikan.Count > 0)
            {
                rowEdit();
            }
        }

        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            iCodeIryo.TextChanged -= iCodeIryo_TextChanged;
            iNameIryo.TextChanged -= iNameIryo_TextChanged;
            iKbnTaikai.SelectedIndexChanged -= iKbnTaikai_SelectedIndexChanged;
            iKumiCode.SelectedIndexChanged -= iKumiCode_SelectedIndexChanged;

            // 条件初期化
            iCodeIryo.Clear();
            iNameIryo.Clear();
            iKbnTaikai.SelectedValue = KbnTaikaiAll; // 先頭の空欄項目に戻す
            iKumiCode.SelectedValue = null; // 未選択にして、入力欄を空にする

            // 初期条件でフィルタ
            changeFilter();

            iCodeIryo.TextChanged += iCodeIryo_TextChanged;
            iNameIryo.TextChanged += iNameIryo_TextChanged;
            iKbnTaikai.SelectedIndexChanged += iKbnTaikai_SelectedIndexChanged;
            iKumiCode.SelectedIndexChanged += iKumiCode_SelectedIndexChanged;
        }

        /// <summary>
        /// 名称 値変更
        /// </summary>
        private void iNameIryo_TextChanged(object sender, EventArgs e)
        {
            changeFilter();
        }

        /// <summary>
        /// 医療機関コード 値変更
        /// </summary>
        private void iCodeIryo_TextChanged(object sender, EventArgs e)
        {
            changeFilter();
        }

        /// <summary>
        /// 退会区分コンボボックス変更
        /// </summary>
        private void iKbnTaikai_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeFilter();
        }

        /// <summary>
        /// 組コードiSearch変更
        /// </summary>
        private void iKumiCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeFilter();
        }

        /// <summary>
        /// フィルタ変更
        /// </summary>
        void changeFilter()
        {
            string filter = "";

            // 退会区分の検索
            int kbnTaikaiIdx = iKbnTaikai.SelectedIndex; // 空欄(all)の判断用

            if (kbnTaikaiIdx > 0) // 空欄選択以外
            {
                filter += $"{t_iryokikan.FIRK_TaikaiKbn} = {Cast.Bool(iKbnTaikai.SelectedValue)}";
            }

            // 組コードの検索
            int kumiId = Cast.Int(iKumiCode.SelectedValue);
            if (kumiId > 0) // ID=0がない前提
            {
                if (filter != "") filter += " AND ";
                filter += $"{t_iryokikan.FIRK_KumiCode} = {kumiId}";
            }

            // 医療機関コードの検索（前方一致）。コードは数値の項目なので、文字列に変換して LIKE で比べる。
            // 数字以外が入っているときは、コードでは絞り込まない
            string iryoKikanCd = StrConv.ToNarrow(iCodeIryo.Text).Trim();
            if (Regex.IsMatch(iryoKikanCd, @"^[0-9]+$") == true)
            {
                if (filter != "") filter += " AND ";
                filter += $"(CONVERT({t_iryokikan.FIRK_Code}, 'System.String') LIKE '{iryoKikanCd}%')";
            }

            // 名称の検索（施設正式名・施設カナ・通称のいずれかに部分一致）
            if (iNameIryo.Text != "")
            {
                if (filter != "") filter += " AND ";
                filter += string.Format("({0} OR {1} OR {2})",
                                            DBQuery.CreateLikeKanaString(t_iryokikan.FIRK_Name, iNameIryo.Text, true),
                                            DBQuery.CreateLikeKanaString(t_iryokikan.FIRK_Kana, iNameIryo.Text, true),
                                            DBQuery.CreateLikeKanaString(t_iryokikan.FIRK_Tsusho, iNameIryo.Text, true));
            }

            // フィルタ適用
            dvKikan.RowFilterQuery(filter);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterIryoKikan.Functions);

            funcKeys.On(FuncMasterIryoKikan.RowAdd, rowAdd);
            funcKeys.On(FuncMasterIryoKikan.RowEdit, rowEdit);
            funcKeys.On(FuncMasterIryoKikan.RowDelete, rowDelete);
            funcKeys.On(FuncMasterIryoKikan.Preview, preview);
            funcKeys.On(FuncMasterIryoKikan.Close, formClose);
        }

        /// <summary>
        /// 印刷プレビュー
        /// </summary>
        void preview()
        {
            FormMasterIryoKikan2_Out frm = new FormMasterIryoKikan2_Out(dvKikan);
            frm.ShowDialog();
            frm.Dispose();
            frm = null;
        }

        /// <summary>
        /// 追加
        /// </summary>
        void rowAdd()
        {
            t_iryokikan nrow = new t_iryokikan(dvKikan.NewRow());

            nrow.ID_Iryokikan = AppDbID.GetNewID(TableProp.t_iryokikan, t_iryokikan.FID_Iryokikan);
            nrow.IRK_Code_Null = AppDbID.GetNewCode(TableProp.t_iryokikan, t_iryokikan.FIRK_Code);
            nrow.IRK_ByoshoUmu = true;
            nrow.IRK_TaikaiKbn = true;
            nrow.IRK_Kaigo = true;

            FormMasterIryoKikan2_DlgEntry frm = new FormMasterIryoKikan2_DlgEntry();
            frm.Mode = FormMasterIryoKikan2_DlgEntry.eMode.Add;
            frm.Row = nrow.Row;
            frm.DvIryokikanShinryo = dvIryokikanShinryoka; // 標榜科目grid用DBView
            frm.ShowDialog();

            if (frm.FormCloseReason == FormCloseReason.Save)
            {
                dvKikan.Add(frm.Row);
                AppGlobal.DB.UpdateTable(TableProp.t_iryokikan);

                AppGlobal.DB.UpdateTable(TableProp.t_iryokikan_shinryoka); // 医療機関-標榜科目 のDB更新
                dvIryokikanShinryoka.AcceptChanges(); // ここでAcceptChangesして修正起点とする

                // 医療機関情報(共通)の再取得 ※情報が追加されたため
                AppGlobal.InitIryoKikan();

                dvKikan.SearchRow(t_iryokikan.FID_Iryokikan, nrow.ID_Iryokikan);
            }
            else
            {
                dvIryokikanShinryoka.RejectChanges(); // DlgEntryでの標榜科目grid変更内容の破棄
            }

            frm.Dispose();
            frm = null;
        }

        /// <summary>
        /// 編集
        /// </summary>
        void rowEdit()
        {
            if (dvKikan.Count > 0)
            {
                //■ 医療機関レコード
                DataRow row = dvKikan.NewRow();

                AppDb.CopyDataRow(dvKikan.CurrentRow.Row, row);

                FormMasterIryoKikan2_DlgEntry frm = new FormMasterIryoKikan2_DlgEntry();
                frm.Mode = FormMasterIryoKikan2_DlgEntry.eMode.Edit;
                frm.Row = row;
                frm.DvIryokikanShinryo = dvIryokikanShinryoka; // 標榜科目grid用DBView
                frm.ShowDialog();

                if (frm.FormCloseReason == FormCloseReason.Save)
                {
                    AppDb.CopyDataRow(frm.Row, dvKikan.CurrentRow.Row);
                    AppGlobal.DB.UpdateTable(TableProp.t_iryokikan);

                    AppGlobal.DB.UpdateTable(TableProp.t_iryokikan_shinryoka); // 医療機関-標榜科目 のDB更新
                    dvIryokikanShinryoka.AcceptChanges(); // ここでAcceptChangesして修正起点とする

                    // 医療機関情報(共通)の再取得
                    AppGlobal.InitIryoKikan();
                }
                else
                {
                    dvIryokikanShinryoka.RejectChanges(); // DlgEntryでの標榜科目grid変更内容の破棄
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
            if (dvKikan.Count > 0)
            {
                t_iryokikan xrow = new t_iryokikan(dvKikan.CurrentRow);

                // t_iryokikanとt_iryokikan_shinryoka以外でIDが使用されているかチェック
                if (AppGlobal.DB.CheckUsedOtherTable(TableProp.t_iryokikan, t_iryokikan.FID_Iryokikan, xrow.ID_Iryokikan, TableProp.t_iryokikan, TableProp.t_iryokikan_shinryoka) == true)
                {
                    AppMsgBox.Show(this, AppMsgBoxIndex.UsedOtherTable);
                    return;
                }

                string str = string.Format("{0} : {1}", xrow.IRK_Code_Null, xrow.IRK_Name);

                // 削除実行
                if (AppMsgBox.Show(this, AppMsgBoxIndex.Delete, str) == System.Windows.Forms.DialogResult.Yes)
                {
                    // 紐づき情報削除
                    DBView tmpDv;

                    tmpDv = new DBView(dvIryokikanShinryoka); // 削除操作用のView作成
                    tmpDv.RowFilterQuery($"{t_iryokikan_shinryoka.FID_Iryokikan} = {xrow.ID_Iryokikan}"); // 削除対象の医療機関でフィルタ

                    // 0件になるまでDBView.Delete()
                    while (true)
                    {
                        if (tmpDv.Count == 0) break;
                        tmpDv.Delete();
                    }

                    // 指定行削除
                    dvKikan.Delete();

                    // DB更新
                    AppGlobal.DB.UpdateTable(TableProp.t_iryokikan);
                    AppGlobal.DB.UpdateTable(TableProp.t_iryokikan_shinryoka);

                    // 医療機関情報(共通)の再取得
                    AppGlobal.InitIryoKikan();
                }
            }
        }

        void formClose()
        {
            // DBViewの破棄
            dvKikan.Dispose();

            this.Close();
        }
    }
}
