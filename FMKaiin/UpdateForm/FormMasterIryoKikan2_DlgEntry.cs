using System;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 医療機関マスタ編集画面。<see cref="FormMasterIryoKikan_DlgEntry"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。項目と入力欄の受け渡しは <see cref="SControlDB"/>、
    /// 入力ルールは <see cref="AppSControlRule"/>、開設主体・組コード・診療科の一覧は <see cref="AppSTableCombo"/> を使う。
    /// 標榜科目のグリッドは、セルに入力用のコントロール（診療科のコンボと、診療科目のチェック ボックス）を重ねて入力する
    /// （<see cref="SGridDB"/> の、セルの中で入力する機能）。
    /// </remarks>
    public partial class FormMasterIryoKikan2_DlgEntry : FormFrame
    {
        SControlDB sctl;

        /// <summary>
        /// 処理モード
        /// </summary>
        public enum eMode
        {
            /// <summary>追加</summary>
            Add,
            /// <summary>訂正</summary>
            Edit,
        }

        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// 処理モード
        /// </summary>
        public eMode Mode
        {
            get; set;
        }

        /// <summary>
        /// 開設主体情報 DBView iSearch
        /// </summary>
        DBView dvKaisetsu;

        /// <summary>
        /// 組コード情報 DBView iSearch
        /// </summary>
        DBView dvKumi;

        // --診療科目Grid--
        /// <summary>
        /// 診療科マスタ DBView iSearch
        /// </summary>
        DBView dvShinryo;
        /// <summary>
        /// 医療機関-診療科の関連 DBView
        /// </summary>
        DBView dvIryokikanShinryo;
        public DBView DvIryokikanShinryo
        {
            set { dvIryokikanShinryo = CreateChildView(value); } // DlgEntry画面でBindするため、DBViewを引数にnewして別Viewを準備(DataTableは同じ)
        }
        /// <summary>
        /// 診療科目Grid
        /// </summary>
        SGridDB sgrid_shinryo;
        /// <summary>
        /// コントロールとデータ連結クラス_診療科目Grid用
        /// </summary>
        SControlDB sctl_shinryo;

        /// <summary>
        /// 起動時のレコード状態
        /// </summary>
        DataRow srcrow = null;
        /// <summary>
        /// 編集するレコード
        /// </summary>
        DataRow editrow = null;

        /// <summary>
        /// 画面間連携用レコード
        /// </summary>
        public DataRow Row
        {
            get
            {
                return editrow;
            }
            set
            {
                srcrow = value;
                if (this.Mode == eMode.Add)
                {
                    editrow = srcrow;
                }
                else
                {
                    editrow = srcrow.Table.NewRow();
                    AppDb.CopyDataRow(srcrow, editrow);
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterIryoKikan2_DlgEntry()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
        }

        /// <summary>
        /// キーダウン処理
        /// </summary>
        protected override void FormFrame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (sgrid_shinryo == null)
                    {
                        break;
                    }
                    if (this.ActiveControl == grid_Sinryo) // Enterでのフォーカス先がGrid
                    {
                        sgrid_shinryo.SelectInput(); // Grid内のセルへフォーカス移動

                        return;
                    }
                    else
                    if (sgrid_shinryo.IsInputControl(this.ActiveControl))
                    {
                        // 診療科の一覧を開いている間の Enter は、コントロールが使う（指している行の確定）
                        if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
                        {
                            return;
                        }

                        // 次の入力欄へ。最後の行の最後の入力欄からは、グリッドへ戻る
                        sgrid_shinryo.SelectNextInput(!e.Shift);
                        return;
                    }
                    break;
            }

            base.FormFrame_KeyDown(sender, e); // 条件合致しない場合、親のメソッド実行
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        protected override void FormFrame_Load(object sender, EventArgs e)
        {
            string title = "：追加";

            if (Mode == eMode.Edit)
            {
                title = "：訂正";
            }

            this.Text += title;
            base.FormFrame_Load(sender, e);
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            // 訂正の場合、最終更新の情報表示
            if (Mode == eMode.Edit)
            {
                t_iryokikan tmprow = new t_iryokikan(editrow);
                oLastUpdate.Text = tmprow.LastUpdate_Null == null ? "" : $"{tmprow.LastUpdate:yyyy/MM/dd HH:mm}"; // 最終更新日時
                Tanto tt = AppGlobal.Tantos.Get(Cast.Int(tmprow.LastUpdateUser_Null));
                // 該当情報が存在したら名称をreturn
                if (tt != null)
                {
                    oLastUpdateUser.Text = tt.Name;
                }
                else
                {
                    oLastUpdateUser.Text = null;
                }
            }
            else
            {
                lblLastUpdate.Visible = false;
                oLastUpdate.Visible = false;
                oLastUpdateUser.Visible = false;
            }

            // コンボの作成
            // 手動セット
            iByoshoUmu.BeginUpdate();
            iByoshoUmu.Items.Clear();
            iByoshoUmu.AddItem("有", true);
            iByoshoUmu.AddItem("無", false);
            iByoshoUmu.EndUpdate();

            iTaikaiKbn.BeginUpdate();
            iTaikaiKbn.Items.Clear();
            iTaikaiKbn.AddItem("会員", true);
            iTaikaiKbn.AddItem("退会", false);
            iTaikaiKbn.EndUpdate();

            // iSearchセット
            dvKaisetsu = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaisetsushutai)); // ソートはAppSTableCombo側実施
            AppSTableCombo.SetComboBox_Kaisetsushutai(iKaisetsushutai, dvKaisetsu); // 開設主体
            dvKumi = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kumicd)); // ソートはAppSTableCombo側実施
            AppSTableCombo.SetComboBox_KumiCode(iKumiCode, dvKumi); // 組コード

            // 画面～DBの紐づけ
            sctl = new SControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

            sctl.Add(new SControlDBText(t_iryokikan.FIRK_Code, iCode));
            sctl.Add(new SControlDBRuby(
                            new string[] {
                                t_iryokikan.FIRK_Name,
                                t_iryokikan.FIRK_Kana},
                            new STextBox[] {
                                iShisetsuName,
                                iShisetsuKana
                            }
            ));
            sctl.Add(new SControlDBRuby(
                            new string[] {
                                t_iryokikan.FIRK_Tsusho,
                                t_iryokikan.FIRK_TsushoKana},
                            new STextBox[] {
                                iShisetsuTsusho,
                                iShisetsuTsushoKana
                            }
            ));
            sctl.Add(new SControlDBPostAddr(
                            new string[]{
                                t_iryokikan.FIRK_Post,
                                t_iryokikan.FIRK_Addr1
                            },
                            new STextBox[]{
                                iPost1,
                                iPost2,
                                iAddr1
                            }
            ));
            sctl.Add(new SControlDBText(t_iryokikan.FIRK_Addr2, iAddr2));
            sctl.Add(new SControlDBHyphenSplit(t_iryokikan.FIRK_Tel1, new STextBox[] { iTel1_1, iTel1_2, iTel1_3 }));
            sctl.Add(new SControlDBHyphenSplit(t_iryokikan.FIRK_Fax1, new STextBox[] { iTel2_1, iTel2_2, iTel2_3 }));
            sctl.Add(new SControlDBCombo(t_iryokikan.FIRK_KaisetsuShutai, iKaisetsushutai));
            sctl.Add(new SControlDBCombo(t_iryokikan.FIRK_ByoshoUmu, iByoshoUmu)); // truefalse
            sctl.Add(new SControlDBText(t_iryokikan.FIRK_Kyoka, iKyokabyosho));
            sctl.Add(new SControlDBCheckBox(t_iryokikan.FIRK_Kaigo, chkKaigo));
            sctl.Add(new SControlDBCheckBox(t_iryokikan.FIRK_Etc, chkEtc));
            sctl.Add(new SControlDBText(t_iryokikan.FIRK_Memo, iHeisetsu));
            sctl.Add(new SControlDBCombo(t_iryokikan.FIRK_KumiCode, iKumiCode));
            sctl.Add(new SControlDBCombo(t_iryokikan.FIRK_TaikaiKbn, iTaikaiKbn));

            sctl.EndAdd(AppSControlRule.Rule);

            rowFetch();

            enabledKyokabyosho(); // 許可病床のEnabled設定
            enabled_EtcMemo(); // 併設施設その他テキスト

            //-----標榜科目(診療科目)Grid準備-----
            // ■コントロール設定
            dvShinryo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_shinryoka));
            AppSTableCombo.SetComboBox_Shinryoka(iHyoboKamoku, dvShinryo); // iSearch

            // ■Grid設定
            // 列ごとに、セルに重ねて入力するコントロール（InputControl）と、入力欄を移る順番（InputTabIndex）を指定する
            sgrid_shinryo = new SGridDB(grid_Sinryo, this);

            sgrid_shinryo.Add(new SGridDBText(t_iryokikan_shinryoka.FID_Shinryoka, "名称", 220, SGridDBCellDisp.Left)
            {
                UnboundFetch = ubShinryokaName, // ID→名称変換
                InputControl = iHyoboKamoku,
                InputTabIndex = 1,
            });
            sgrid_shinryo.Add(new SGridDBText(t_iryokikan_shinryoka.FChk_Shinryoka, "診療科目", 0.0f, SGridDBCellDisp.Center)
            {
                UnboundFetch = ubShinryokaCheckbox, // 名称変換
                InputControl = iShinryoKamokuChk,
                InputTabIndex = 2,
            });

            sgrid_shinryo.EndAdd(dvIryokikanShinryo);

            // コントロールのDBバインド
            sctl_shinryo = new SControlDB(this, getDataRowShinryo);
            sctl_shinryo.Add(new SControlDBCombo(t_iryokikan_shinryoka.FID_Shinryoka, iHyoboKamoku));
            sctl_shinryo.Add(new SControlDBCheckBox(t_iryokikan_shinryoka.FChk_Shinryoka, iShinryoKamokuChk));
            sctl_shinryo.EndAdd(AppSControlRule.Rule);

            // イベント登録
            btnKamokuAdd.Click += btnKamokuAdd_Click;
            btnKamokuDel.Click += btnKamokuDel_Click;
            iByoshoUmu.SelectedIndexChanged += iByoshoUmu_SelectedIndexChanged;
            chkEtc.CheckedChanged += chkEtc_CheckedChanged;

            dvIryokikanShinryo.RowFetchDemand += dvIryokikanShinryo_RowFetchDemand;

            changeFilterShinryoka(); // 表示中会員の情報のみへフィルタ
            rowFetchShinryo(); // コントロール値セット(グリッド初期表示値)

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// IDを基に、Gridに診療科名を表示します。
        /// </summary>
        string ubShinryokaName(SGridDBColumn col, int row)
        {
            t_iryokikan_shinryoka xrow = new t_iryokikan_shinryoka(dvIryokikanShinryo[row]);

            Shinryoka sk = AppGlobal.Shinryokas.Get(xrow.ID_Shinryoka);

            string name = "";
            if (sk != null)
            {
                name = sk.XRow.SRK_Name;
            }

            return name;
        }

        /// <summary>
        /// bool値より、Gridに文言を表示します。
        /// </summary>
        string ubShinryokaCheckbox(SGridDBColumn col, int row)
        {
            t_iryokikan_shinryoka xrow = new t_iryokikan_shinryoka(dvIryokikanShinryo[row]);

            string name = "";
            if (xrow.Chk_Shinryoka == true)
            {
                name = "○";
            }

            return name; // Falseの場合、空欄
        }

        /// <summary>
        /// 診療科目gridの現在行を返します。
        /// </summary>
        /// <returns>現在行</returns>
        DataRow getDataRowShinryo()
        {
            if (dvIryokikanShinryo.CurrentRow != null && dvIryokikanShinryo.Count > 0)
            {
                return dvIryokikanShinryo.CurrentRow.Row;
            }

            return null;
        }

        /// <summary>
        /// フィルタ変更(t_iryokikan_shinryoka)
        /// </summary>
        void changeFilterShinryoka()
        {
            string filter = "";

            // 該当医療機関の診療科目情報へフィルタ
            t_iryokikan tmprow = new t_iryokikan(editrow);

            filter =
                $"{t_iryokikan_shinryoka.FID_Iryokikan} = {tmprow.ID_Iryokikan}";

            // 適用
            dvIryokikanShinryo.RowFilterQuery(filter);
        }

        /// <summary>
        /// Gridカレント行の内容を各コントロールにセットします(標榜診療科目Grid)
        /// </summary>
        void rowFetchShinryo()
        {
            if (sctl_shinryo == null)
            {
                return;
            }

            if (dvIryokikanShinryo.CurrentRow == null)
            {
                // 現在行がないので、コントロールの内容をクリアする。
                sctl_shinryo.ClearAll();
            }
            else
            {
                // 現在行を取得
                sctl_shinryo.FetchAll();
            }
        }

        /// <summary>
        /// 病床有無の変更イベント
        /// </summary>
        void iByoshoUmu_SelectedIndexChanged(object sender, EventArgs e)
        {
            enabledKyokabyosho();
        }

        /// <summary>
        /// 許可病床コントロールの制御変更
        /// </summary>
        void enabledKyokabyosho()
        {
            // 病床数の活性/非活性
            if (iByoshoUmu.SelectedIndex == 1) // 「無」
            {
                iKyokabyosho.Text = null;
                this.Row[t_iryokikan.FIRK_Kyoka] = DBNull.Value;
                iKyokabyosho.Enabled = false;
            }
            else // 「有」
            {
                iKyokabyosho.Enabled = true;
            }
        }

        /// <summary>
        /// 併設施設:その他chkbox変更
        /// </summary>
        void chkEtc_CheckedChanged(object sender, EventArgs e)
        {
            enabled_EtcMemo();
        }

        /// <summary>
        /// 併設施設:その他テキストの制御変更
        /// </summary>
        void enabled_EtcMemo()
        {
            if (chkEtc.Checked == true)
            {
                iHeisetsu.Enabled = true; // 併設施設:その他テキスト→活性
            }
            else
            {
                iHeisetsu.Enabled = false;
            }
        }

        /// <summary>
        /// 追加(標榜診療科目Grid)
        /// </summary>
        private void btnKamokuAdd_Click(object sender, EventArgs e)
        {
            t_iryokikan tmprow = new t_iryokikan(editrow);
            t_iryokikan_shinryoka nrow = new t_iryokikan_shinryoka(dvIryokikanShinryo.NewRow());

            // NewRowへフィルタ済の医療機関IDセット
            nrow.ID_Iryokikan = tmprow.ID_Iryokikan;
            // 診療科目は、オフから始める。チェック ボックスは、切り替えたときにだけ行へ書き戻すので、
            // ここで入れておかないと、切り替えなかった行の値が「値なし」のまま残る（画面ではオフに見える）
            nrow.Chk_Shinryoka = false;

            // 行追加
            dvIryokikanShinryo.Add(nrow.Row);
            // 追加した行をカレント行にする
            sgrid_shinryo.SetRow(dvIryokikanShinryo.Count - 1);

            // 編集状態にして表示
            sgrid_shinryo.Select();
            grid_Sinryo.Col = 0;
            sgrid_shinryo.SelectInput();
        }

        /// <summary>
        /// 削除(標榜診療科目Grid)
        /// </summary>
        private void btnKamokuDel_Click(object sender, EventArgs e)
        {
            if (dvIryokikanShinryo.Count > 0)
            {
                DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の標榜科目情報");
                if (dr == DialogResult.No)
                {
                    return;
                }

                dvIryokikanShinryo.Delete();
                grid_Sinryo.Refresh();

                // 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
                sgrid_shinryo.Select();
            }
        }

        /// <summary>
        /// 変更要求処理(標榜診療科目Grid)
        /// </summary>
        void dvIryokikanShinryo_RowFetchDemand(object sender, EventArgs e)
        {
            // 再フィルタ
            changeFilterShinryoka();

            // コントロール値セット
            rowFetchShinryo();
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterIryoKikan_DlgEntry.Functions);

            funcKeys.On(FuncMasterIryoKikan_DlgEntry.Save, saveClose);
            funcKeys.On(FuncMasterIryoKikan_DlgEntry.Cancel, closeCancel);
        }

        /// <summary>
        /// 保存終了
        /// </summary>
        void saveClose()
        {
            if (validateRow() == true)
            {
                formCloseReason = FormCloseReason.Save;
                this.Close();
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        void closeCancel()
        {
            this.Close();
        }

        /// <summary>
        /// 行情報の取得
        /// </summary>
        void rowFetch()
        {
            if (Row != null)
            {
                sctl.FetchAll();
            }
            else
            {
                sctl.ClearAll();
            }
        }

        /// <summary>
        /// データ取得
        /// </summary>
        DataRow getDataRow()
        {
            return editrow;
        }

        /// <summary>
        /// 検証処理
        /// </summary>
        /// <returns></returns>
        bool validateRow()
        {
            // フォーカスがある入力欄の内容を、行へ書き戻す（ファンクション キーではフォーカスが動かないため）。
            // 標榜科目のセルで入力している最中なら、その内容は、標榜科目の行へ書き戻す
            sctl.Update();
            sctl_shinryo.Update();

            // 更新担当者のセット
            this.Row[t_iryokikan.FLastUpdateUser] = AppGlobal.LoginUser.ID;

            // 併設施設:その他 チェックなし→その他テキストのDB値をクリア
            if (chkEtc.Checked != true)
            {
                this.Row[t_iryokikan.FIRK_Memo] = DBNull.Value;
            }

            // 空白チェック
            Control[] ctls =
            {
                iCode, iByoshoUmu, iTaikaiKbn,
            };

            foreach (Control ctl in ctls)
            {
                if (ctl.Text.Length == 0)
                {
                    ctl.Select();
                    appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
                    return false;
                }
            }

            // 重複チェック
            IryoKikan obj = AppGlobal.IryoKikans.GetCode(iCode.Text);

            if (obj != null && obj.ID != Cast.Int(editrow[t_iryokikan.FID_Iryokikan]))
            {
                iCode.Select();
                appToolTip.Show(iCode, AppToolTipIndex.BookingCode);
                return false;
            }

            return true;
        }
    }
}
