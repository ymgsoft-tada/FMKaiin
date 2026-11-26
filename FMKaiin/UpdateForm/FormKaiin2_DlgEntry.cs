using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ComponentSUI;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 会員情報入力画面。<see cref="FormKaiin_DlgEntry"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// <para>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。項目と入力欄の受け渡しは <see cref="SControlDB"/>、
    /// 入力ルールは <see cref="AppSControlRule"/>、区分の一覧は <see cref="AppSCombo"/>、
    /// 表から選ぶ一覧（医療機関・医師会・診療科・学会・会費など）は <see cref="AppSTableCombo"/> を使う。
    /// 情報1 ～ 情報3 の切り替えは <see cref="STabControl"/>。
    /// </para>
    /// <para>
    /// 一覧（医師会の会費・診療科目・参加医会・所属学会）は <see cref="SGridDB"/> で、セルに入力欄を重ねて、その場で編集する。
    /// 異動情報の追加・訂正は <see cref="FormKaiin2_DlgEntry_Idoinfo"/>、銀行コードの選択は <see cref="FormSelectorBankCode2"/> で行う。
    /// </para>
    /// </remarks>
    public partial class FormKaiin2_DlgEntry : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        // 異動情報の一覧の、データに無い列（詳細・その他）の名前。表示する文字列は、異動事由に応じて、複数の項目から作る
        private const string F_IdoDetail = "F_IdoDetail";
        private const string F_IdoDetailMemo = "F_IdoDetailMemo";

        /// <summary>
        /// 処理モード
        /// </summary>
        public enum eMode
        {
            /// <summary>追加</summary>
            Add,
            /// <summary>訂正</summary>
            Edit,
            /// <summary>コピー追加</summary>
            Copy,
            /// <summary>再入会</summary>
            Rejoin,
        }

        /// <summary>
        /// 処理モード
        /// </summary>
        public eMode Mode
        {
            get; set;
        }

        /// <summary>
        /// コントロールとデータ連結クラス
        /// </summary>
        SControlDB sctl;

        /// <summary>
        /// 医療機関マスタ DBView iSearch用
        /// </summary>
        DBView dvIryoKikanCmb;

        /// <summary>
        /// 医師会マスタ DBView iSearch用
        /// </summary>
        DBView dvIshikai;

        /// <summary>
        /// 施設業務マスタ DBView iSearch用
        /// </summary>
        DBView dvShisetsugyomu;

        /// <summary>
        /// 医療機関マスタ DBView
        /// </summary>
        DBView dvIryoKikan;
        /// <summary>
        /// コントロールとデータ連結クラス 医療機関情報用
        /// </summary>
        SControlDB sctl_iryo;

        // --医師会会員Grid--
        /// <summary>
        /// 医会会費マスタ(医師会会費) DBView iSearch用
        /// </summary>
        DBView dvIkai;
        /// <summary>
        /// 会員-医会(医師会)の関連 DBView
        /// </summary>
        DBView dvKaiinKaihiIshikai;
        public DBView DvKaiinKaihiIshikai
        {
            //get { return dvKaiinKaihi; }
            set { dvKaiinKaihiIshikai = CreateChildView(value); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
        }
        /// <summary>
        /// 医師会会員Grid
        /// </summary>
        SGridDB sgrid_kaihiIshikai;
        /// <summary>
        /// コントロールとデータ連結クラス_医師会会員Grid用
        /// </summary>
        SControlDB sctl_kaihiIshikai;

        // --診療科目Grid--
        /// <summary>
        /// 診療科マスタ DBView
        /// </summary>
        DBView dvShinryo;
        /// <summary>
        /// 会員-診療科の関連 DBView
        /// </summary>
        DBView dvKaiinShinryo;
        public DBView DvKaiinShinryo
        {
            //get { return dvKaiinShinryo; }
            set { dvKaiinShinryo = CreateChildView(value); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
        }
        /// <summary>
        /// 診療科目Grid
        /// </summary>
        SGridDB sgrid_shinryo;
        /// <summary>
        /// コントロールとデータ連結クラス_診療科目Grid用
        /// </summary>
        SControlDB sctl_shinryo;

        // --所属学会Grid--
        /// <summary>
        /// 学会マスタ DBView
        /// </summary>
        DBView dvGakkai;
        /// <summary>
        /// 会員-学会の関連 DBView
        /// </summary>
        DBView dvKaiinGakkai;
        public DBView DvKaiinGakkai
        {
            //get { return dvKaiinGakkai; }
            set { dvKaiinGakkai = CreateChildView(value); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
        }
        /// <summary>
        /// 所属学会Grid
        /// </summary>
        SGridDB sgrid_gakkai;
        /// <summary>
        /// コントロールとデータ連結クラス_所属学会Grid用
        /// </summary>
        SControlDB sctl_gakkai;

        // --参加医会Grid--
        /// <summary>
        /// 医会会費マスタ DBView
        /// </summary>
        DBView dvKaihi;
        /// <summary>
        /// 会員-医会の関連 DBView
        /// </summary>
        DBView dvKaiinKaihi;
        public DBView DvKaiinKaihi
        {
            //get { return dvKaiinKaihi; }
            set { dvKaiinKaihi = CreateChildView(value); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
        }
        /// <summary>
        /// 参加医会Grid
        /// </summary>
        SGridDB sgrid_kaihi;
        /// <summary>
        /// コントロールとデータ連結クラス_参加医会Grid用
        /// </summary>
        SControlDB sctl_kaihi;

        // --異動情報Grid--
        /// <summary>
        /// 会員-異動情報の関連 DBView
        /// </summary>
        DBView dvKaiinIdo;
        public DBView DvKaiinIdo
        {
            //get { return dvKaiinIdo; }
            set { dvKaiinIdo = CreateChildView(value); } // DlgEntry画面でBindするため、受取ったDBViewを引数にnewして別Viewを準備(DataTableは同じ)
        }
        /// <summary>
        /// 異動情報Grid
        /// </summary>
        SGridDB sgrid_ido;

        /// <summary>
        /// 起動時のレコード状態
        /// </summary>
        DataRow srcrow = null;
        /// <summary>
        /// 編集するレコード
        /// </summary>
        DataRow editrow = null;
        /// <summary>
        /// 画面を表示したときの、編集するレコードの内容（閉じるときに、入力があったかを調べるのに使う）
        /// </summary>
        RowSnapshot shownrow = null;
        /// <summary>
        /// 画面を表示したときの、一覧の行の内容（同じく、一覧に入力があったかを調べるのに使う）
        /// </summary>
        string shownChildren = null;

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
        public FormKaiin2_DlgEntry()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);

            Mode = eMode.Add;
        }

        /// <summary>
        /// キーダウン処理
        /// </summary>
        protected override void FormFrame_KeyDown(object sender, KeyEventArgs e)
        {
            //■ ファンクションキーの標準ショートカットの抑止
            e.Handled = base.Patcher_CheckShortcutForKillingWindowsAction(e.KeyCode, e.Alt);

            // 処理が必要かどうかチェック（一覧・カレンダーを開いている間の Enter などは、コントロールが使う）
            if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
            {
                return;
            }
            if (this.ActiveControl == null)
            {
                return;
            }
            if (sgrid_ido == null)
            {
                // 初回描画処理が終わる前（グリッドの準備がまだ）
                base.FormFrame_KeyDown(sender, e);
                return;
            }

            switch (e.KeyCode)
            {
                // Enterによるフォーカス移動で、Gridへの移動に対応するための記述

                case Keys.Enter:
                    if (this.ActiveControl == iIshikaiShiharai && grid_KaihiIshikai.Row == 2 && grid_KaihiIshikai.Col == 2)
                    {
                        // 医師会会員Gridの最後のセル → 情報1 の先頭へ
                        tabInfo.SelectedIndex = 0;
                        iTantoMainKamoku.Select();

                        return;
                    }
                    else
                    if (this.ActiveControl == iGenmen)
                    {
                        grid_KaihiIshikai.Select();
                        grid_KaihiIshikai.Row = 0;
                        grid_KaihiIshikai.Col = 1;

                        sgrid_kaihiIshikai.SelectInput();

                        return;
                    }
                    else
                    if (this.ActiveControl == grid_Sinryo) // Enterでのフォーカス先が診療Grid
                    {
                        sgrid_shinryo.SelectInput(); // Grid内のセルへフォーカス移動

                        return;
                    }
                    else
                    if (sgrid_shinryo.IsInputControl(this.ActiveControl))
                    {
                        if (sgrid_shinryo.SelectNextInput(!e.Shift) == false) // 次コントロールが存在しない(最終行)
                        {
                            iIryoKikan.Select(); // 医療機関コード選択へフォーカス移動
                        }
                        return;
                    }
                    else
                    if (this.ActiveControl == grid_Gakkai) // Enterでのフォーカス先が学会Grid
                    {
                        sgrid_gakkai.SelectInput(); // Grid内のセルへフォーカス移動 ※Enter押下で行追加機能なし→Grid0行状態ではフォーカスが機能しない

                        return;
                    }
                    else
                    if (sgrid_gakkai.IsInputControl(this.ActiveControl))
                    {
                        if (sgrid_gakkai.SelectNextInput(!e.Shift) == false) // 次コントロールが存在しない(最終行)
                        {
                            grid_Ido.Select(); // 異動情報Gridへフォーカス移動
                        }
                        return;
                    }
                    else
                    if (this.ActiveControl == grid_Kaihi) // Enterでのフォーカス先が参加医会Grid
                    {
                        sgrid_kaihi.SelectInput(); // Grid内のセルへフォーカス移動

                        return;
                    }
                    else
                    if (sgrid_kaihi.IsInputControl(this.ActiveControl))
                    {
                        if (sgrid_kaihi.SelectNextInput(!e.Shift) == false) // 次コントロールが存在しない(最終行)
                        {
                            iBankCD1.Select(); // 銀行情報の先頭へフォーカス移動
                        }
                        return;
                    }
                    else
                    if (this.ActiveControl == grid_Ido) // Enterでのフォーカス先が異動情報Grid
                    {
                        if (dvKaiinIdo.Count > 0)
                        {
                            rowEditIdo();
                        }
                        return;
                    }
                    else
                    if (this.ActiveControl == grid_KaihiIshikai) // Enterでのフォーカス先が医師会会員Grid
                    {
                        sgrid_kaihiIshikai.SelectInput(); // Grid内のセルへフォーカス移動

                        return;
                    }
                    else
                    if (sgrid_kaihiIshikai.IsInputControl(this.ActiveControl))
                    {
                        sgrid_kaihiIshikai.SelectNextInput(!e.Shift);
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

            switch(Mode)
            {
                case eMode.Edit:
                    title = "：訂正";
                    break;

                case eMode.Copy:
                    title = "：コピー追加";
                    break;

                case eMode.Rejoin:
                    title = "：追加(再入会)";
                    break;
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
                t_kaiin tmprow = new t_kaiin(editrow);
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

            // 卒業年月・修了年月・学位取得年月は、年月で入力する（デザイナーで、DateFormat に YearMonth を指定してある）

            // コンボボックスの作成
            // enumから
            AppSCombo.SetComboBox(iSex, enumKbn.DSex); // 性別
            AppSCombo.SetComboBox(iKaiinKbn, enumKbn.DTypeKaiin); // 会員区分
            AppSCombo.SetComboBox(iZaiseki, enumKbn.DTypeZaiseki, (int)eTypeZaiseki.None); // 在籍区分
            AppSCombo.SetComboBox(iTaikaiJiyu, enumKbn.DTypeTaikaiJiyu); // 退会事由
            AppSCombo.SetComboBox(iKozaType1, enumKbn.DTypeKoza); // 銀行 口座区分 空欄選択あり
            AppSCombo.SetComboBox(iKozaType2, enumKbn.DTypeKoza);
            AppSCombo.SetComboBox(iKozaType3, enumKbn.DTypeKoza);
            AppSCombo.SetComboBox(iGenmen, enumKbn.DTypeGenmen); // 会費減免事由

            // iSearch
            dvIryoKikan = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_iryokikan)); // 下で会員IDフィルタし情報表示に使うdv
            dvIryoKikanCmb = new DBView(dvIryoKikan); // DBViewを複製(フィルタかけない版)
            AppSTableCombo.SetComboBox_IryoKikan(iIryoKikan, dvIryoKikanCmb); // 医療機関コード

            dvIshikai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_ishikai));
            AppSTableCombo.SetComboBox_Ishikai(iIshikaiIdomae, dvIshikai); // 異動前医師会
            AppSTableCombo.SetComboBox_Ishikai(iIshikaiIdosaki, dvIshikai); // 異動先医師会

            dvShisetsugyomu = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_shisetsugyomu));
            AppSTableCombo.SetComboBox_Shisetsugyomu(iIryoShisetsugyomu, dvShisetsugyomu); // 医療 - 施設・業務

            dvShinryo = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_shinryoka)); // ※下記の担当診療GridのiSearchでも利用
            AppSTableCombo.SetComboBox_Shinryoka(iTantoMainKamoku, dvShinryo); // 診療科目(主担当)

            // 手動
            setBoolCombo(iBunshoSofu, "施設", "自宅"); // 文書送付先
            setBoolCombo(iFaxSofu, "施設", "自宅"); // FAX送付先
            setBoolCombo(iKaihiUchiwake, "不要", "必要"); // 月別会費内訳

            sctl = new SControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

            sctl.Add(new SControlDBText(t_kaiin.FCD_Kaiin, iCode)); // 会員コード
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_Mail1, iMail1)); // メールアドレス
            sctl.Add(new SControlDBNumber(t_kaiin.FKaiin_IsekiTorokuNo, iIsekiTourokuNo)); // 医籍登録番号
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateIsekiToroku, iIsekiTorokuDate)); // 医籍登録年月日
            sctl.Add(new SControlDBRuby(
                new string[] {
                                t_kaiin.FKaiin_Name,    // 氏名
                                t_kaiin.FKaiin_NameKana // 氏名カナ
                },
                new STextBox[] {
                                iName,
                                iNameFurigana
                }
            ));
            sctl.Add(new SControlDBRuby(
                new string[] {
                                t_kaiin.FKaiin_NameOld,    // 旧氏名
                                t_kaiin.FKaiin_NameKanaOld // 氏名カナ
                },
                new STextBox[] {
                                iNameOld,
                                iNameFuriganaOld
                }
            ));
            sctl.Add(new SControlDBPostAddr(
                new string[]{
                                t_kaiin.FKaiin_Post, // 郵便番号
                                t_kaiin.FKaiin_Addr1 // 住所1
                },
                new STextBox[]{
                                iPost1,
                                iPost2,
                                iAddr1
                }
            ));
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_Addr2, iAddr2)); // 住所2
            sctl.Add(new SControlDBHyphenSplit(t_kaiin.FKaiin_Tel1, new STextBox[] { iTel1_1, iTel1_2, iTel1_3 })); // 電話番号
            sctl.Add(new SControlDBHyphenSplit(t_kaiin.FKaiin_Tel2, new STextBox[] { iTel2_1, iTel2_2, iTel2_3 })); // 携帯電話番号
            sctl.Add(new SControlDBHyphenSplit(t_kaiin.FKaiin_Fax, new STextBox[] { iFax_1, iFax_2, iFax_3 })); // FAX
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateBirth, iBirthday)); // 生年月日
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_Sex, iSex)); // 性別

            sctl.Add(new SControlDBText(t_kaiin.FKaiin_ShusshinKo, iShusshinKou)); // 出身校
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateSotsugyo, iSotsugyoNengetsu)); // 卒業年月
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_ShusshinIn, iShusshinDaigakuin)); // 出身校(大学院)
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateShuryo, iShuryoNengetsu)); // 修了年月
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateShutokuGakui, iGakuiNengetsu)); // 学位取得年月
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateToroku, iTorokuDate)); // 登録日
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_TypeKaiin, iKaiinKbn)); // 会員区分
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateNyukai, iNyukaiDate)); // 入会年月日
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateShonin, iShoninDate)); // 承認日
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateShinsei, iShinseiDate)); // 申請年月日

            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_TypeZaiseki, iZaiseki)); // 在籍区分
            sctl.Add(new SControlDBCombo(t_kaiin.FID_Ishikai_Idomae, iIshikaiIdomae)); // 異動前医師会

            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_TypeGenmen, iGenmen)); // 会費減免事由

            // 医師会会員Grid準備
            // ■コントロール設定
            // コンボボックス
            AppSCombo.SetComboBox(iIshikaiShiharai, enumKbn.DShiharai, (int)eShiharai.None); // 支払方法

            dvIkai = new DBView(AppGlobal.Kaihis.DbView); // 念のためdvKaihiと合わせてFillから変更
            dvIkai.RowFilterQuery($"{t_kaihi.FKaihi_SearchUsed} = TRUE AND {t_kaihi.FKaihi_KaihiType} = {(int)eTypeKaihi.Ishikai}"); // 会費区分1
            dvIkai.SortQuery($"{ t_kaihi.FCD_Kaihi}");
            AppSTableCombo.SetComboBox_Kaihi(iIshikai, dvIkai); // 医師会会員区分

            // ■Grid設定
            // 列ごとに、セルに重ねて入力するコントロール（InputControl）と、入力欄を移る順番（InputTabIndex）を指定する
            sgrid_kaihiIshikai = new SGridDB(grid_KaihiIshikai, this);

            sgrid_kaihiIshikai.Add(new SGridDBText(t_kaiin_kaihi.FKaihiTypeIkai, "会員区分", 102, SGridDBCellDisp.Center)
            {
                UnboundFetch = ubTypeIshikai, // 名称変換
            });
            sgrid_kaihiIshikai.Add(new SGridDBText(t_kaiin_kaihi.FID_Kaihi, "医師会名称", 300, SGridDBCellDisp.Left)
            {
                UnboundFetch = ubIkaiNameIshikai, // ID→名称変換
                InputControl = iIshikai,
                InputTabIndex = 0,
            });
            sgrid_kaihiIshikai.Add(new SGridDBText(t_kaiin_kaihi.FKaihiShiharai, "支払方法", 0.0f, SGridDBCellDisp.Center)
            {
                UnboundFetch = ubShiharaiHohoIshikai, // 名称変換
                InputControl = iIshikaiShiharai,
                InputTabIndex = 1,
            });

            sgrid_kaihiIshikai.EndAdd(dvKaiinKaihiIshikai);

            // コントロールのDBバインド
            sctl_kaihiIshikai = new SControlDB(this, getDataRowKaihiIshikai);
            sctl_kaihiIshikai.Add(new SControlDBCombo(t_kaiin_kaihi.FID_Kaihi, iIshikai)); // 医師会ID
            sctl_kaihiIshikai.Add(new SControlDBCombo(t_kaiin_kaihi.FKaihiShiharai, iIshikaiShiharai)); // 支払方法
            sctl_kaihiIshikai.EndAdd(AppSControlRule.Rule);

            changeFilterKaihiIshikai(); // 表示中会員の情報のみへフィルタ
            rowFetchKaihiIshikai(); // コントロール値セット(グリッド初期表示値)

            // --タブ1--
            sctl.Add(new SControlDBCombo(t_kaiin.FID_Shinryoka_Main, iTantoMainKamoku)); // 診療科目(主)

            // 医療機関
            sctl.Add(new SControlDBCombo(t_kaiin.FID_Iryokikan, iIryoKikan)); // 医療機関コード
            sctl.Add(new SControlDBCombo(t_kaiin.FID_ShisetsuGyomu, iIryoShisetsugyomu)); // 施設・業務
            // 指定医chkbox → バインドでなく個別に対応(fetchIryoShiteii())
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_ShiteiiEtc, iIryoShiteiiEtc)); // 指定医:その他入力欄

            // --タブ2--
            // 銀行
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankCode1, iBankCD1)); // 銀行コード
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankShitenCode1, iShitenCD1)); // 支店コード
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_BankKozaType1, iKozaType1)); // 口座区分
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankKozaNo1, iKozaNo1)); // 口座番号
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankKozaName1, iKozaName1)); // 口座名義
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankCode2, iBankCD2));
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankShitenCode2, iShitenCD2));
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_BankKozaType2, iKozaType2));
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankKozaNo2, iKozaNo2));
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankKozaName2, iKozaName2));
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankCode3, iBankCD3));
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankShitenCode3, iShitenCD3));
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_BankKozaType3, iKozaType3));
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankKozaNo3, iKozaNo3));
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_BankKozaName3, iKozaName3));

            // --タブ3--
            // 退会
            sctl.Add(new SControlDBDate(t_kaiin.FKaiin_DateTaikai, iTaikaiDate)); // 退会年月日
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_TypeTaikaiJiyu, iTaikaiJiyu)); // 退会事由
            sctl.Add(new SControlDBText(t_kaiin.FKaiin_TaikaiEtcmemo, iTaikaiJiyuEtc)); // 退会事由その他
            sctl.Add(new SControlDBCombo(t_kaiin.FID_Ishikai_Idosaki, iIshikaiIdosaki)); // 異動先医師会

            // 処理区分
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_BunshoSofusaki, iBunshoSofu)); // 文書送付先
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_FaxSofusaki, iFaxSofu)); // FAX送付先
            sctl.Add(new SControlDBCombo(t_kaiin.FKaiin_KaihiUchiwake, iKaihiUchiwake)); // 月別会費内訳

            sctl.EndAdd(AppSControlRule.Rule);

            // 医療機関情報-指定医
            // 指定医情報チェックボックスのTag値設定
            iIryoShiteii1.Tag  = AppConst.eShiteii.Hokeni;
            iIryoShiteii2.Tag  = AppConst.eShiteii.Botai;
            iIryoShiteii3.Tag  = AppConst.eShiteii.Seishin;
            iIryoShiteii4.Tag  = AppConst.eShiteii.Seikatsu;
            iIryoShiteii5.Tag  = AppConst.eShiteii.Rosai;
            iIryoShiteii6.Tag  = AppConst.eShiteii.Kekkaku;
            iIryoShiteii7.Tag  = AppConst.eShiteii.Shogai;
            iIryoShiteii8.Tag  = AppConst.eShiteii.Genbaku;
            iIryoShiteii9.Tag  = AppConst.eShiteii.Nanbyo;
            iIryoShiteii10.Tag = AppConst.eShiteii.Etc;

            // 入力させないコントロールの非活性
            iBankName1.Enabled = false; // 銀行名
            iShitenName1.Enabled = false; // 支店名
            iBankName2.Enabled = false; // 銀行名
            iShitenName2.Enabled = false; // 支店名
            iBankName3.Enabled = false; // 銀行名
            iShitenName3.Enabled = false; // 支店名

            // コントロールへDB値セット (中に指定医chkboxセット有)
            rowFetch();

            // 銀行名称1～3表示
            fetchBank();
            fetchShiten();

            // 退会事由コントロール 状態制御
            enabled_iTaikaiJiyuCtl();

            //■ イベントの登録
            iIryoKikan.SelectedValueChanged += iIryoKikan_SelectedValueChanged;
            iBankCD1.TextChanged += iBankCD_TextChanged;
            iBankCD1.MouseDoubleClick += cd_MouseDoubleClick;
            iBankCD2.TextChanged += iBankCD_TextChanged;
            iBankCD2.MouseDoubleClick += cd_MouseDoubleClick;
            iBankCD3.TextChanged += iBankCD_TextChanged;
            iBankCD3.MouseDoubleClick += cd_MouseDoubleClick;
            iShitenCD1.TextChanged += iShitenCD_TextChanged;
            iShitenCD1.MouseDoubleClick += cd_MouseDoubleClick;
            iShitenCD2.TextChanged += iShitenCD_TextChanged;
            iShitenCD2.MouseDoubleClick += cd_MouseDoubleClick;
            iShitenCD3.TextChanged += iShitenCD_TextChanged;
            iShitenCD3.MouseDoubleClick += cd_MouseDoubleClick;
            iTaikaiJiyu.SelectedIndexChanged += iTaikaiJiyu_SelectedIndexChanged; // 退会事由cmb
            iIryoShiteii10.CheckedChanged += iIryoShiteii10_CheckedChanged; // 医療-指定医その他chk

            // 医療機関情報準備
            // 医療機関情報バインド（表示するだけ。入力は、医療機関マスタで行う）
            sctl_iryo = new SControlDB(this, getDataRowIryo);

            sctl_iryo.Add(new SControlDBText(t_iryokikan.FIRK_Tsusho, iIryoShisetsuName)); // 通称
            sctl_iryo.Add(new SControlDBText(t_iryokikan.FIRK_TsushoKana, iIryoShisetsuNameKana)); // 通称カナ
            sctl_iryo.Add(new SControlDBHyphenSplit(t_iryokikan.FIRK_Post, new STextBox[] { iIryoPost1, iIryoPost2 })); // 郵便番号
            sctl_iryo.Add(new SControlDBText(t_iryokikan.FIRK_Addr1, iIryoAddr1)); // 住所1
            sctl_iryo.Add(new SControlDBText(t_iryokikan.FIRK_Addr2, iIryoAddr2)); // 住所2
            sctl_iryo.Add(new SControlDBHyphenSplit(t_iryokikan.FIRK_Tel1, new STextBox[] { iIryoTel1_1, iIryoTel1_2, iIryoTel1_3 })); // TEL
            sctl_iryo.Add(new SControlDBHyphenSplit(t_iryokikan.FIRK_Fax1, new STextBox[] { iIryoFax1_1, iIryoFax1_2, iIryoFax1_3 })); // FAX
            SControlDBText txtKaisetsuShutai = new SControlDBText(t_iryokikan.FIRK_KaisetsuShutai, iIryoKaisetsushutai); // 開設主体 (ID→名称へ)
            txtKaisetsuShutai.FetchRunEx = KaisetsuShutaiEx; // 文言変換用メソッドへ
            sctl_iryo.Add(txtKaisetsuShutai);
            SControlDBText txtByoshoUmu = new SControlDBText(t_iryokikan.FIRK_ByoshoUmu, iIryoByoshoUmu); // 病床有無
            txtByoshoUmu.FetchRunEx = ByoshoUmuEx; // 文言変換用メソッドへ
            sctl_iryo.Add(txtByoshoUmu);
            sctl_iryo.Add(new SControlDBNumber(t_iryokikan.FIRK_Kyoka, iIryoByoshoCnt)); // 許可病床数
            sctl_iryo.Add(new SControlDBCheckBox(t_iryokikan.FIRK_Kaigo, iIryoHeisetsu)); // 併設の施設-介護施設
            sctl_iryo.Add(new SControlDBCheckBox(t_iryokikan.FIRK_Etc, iIryoHeisetsuEtc)); // 併設の施設-その他
            sctl_iryo.Add(new SControlDBText(t_iryokikan.FIRK_Memo, iIryoHeisetsuName)); // 併設の施設-その他名称
            SControlDBText txtKumiCode = new SControlDBText(t_iryokikan.FIRK_KumiCode, iIryoKumi); // 組コード
            txtKumiCode.FetchRunEx = KumiCodeEx; // 文言変換用メソッドへ
            sctl_iryo.Add(txtKumiCode);

            sctl_iryo.EndAdd(AppSControlRule.Rule);

            changeFilterIryoKikanShown(); // 表示中会員の情報のみへフィルタ
            rowFetchIryoKikan(); // コントロール値セット

            // 医療機関情報の非活性
            iIryoShisetsuName.Enabled = false;
            iIryoShisetsuNameKana.Enabled = false;
            iIryoPost1.Enabled = false;
            iIryoPost2.Enabled = false;
            iIryoAddr1.Enabled = false;
            iIryoAddr2.Enabled = false;
            iIryoTel1_1.Enabled = false;
            iIryoTel1_2.Enabled = false;
            iIryoTel1_3.Enabled = false;
            iIryoFax1_1.Enabled = false;
            iIryoFax1_2.Enabled = false;
            iIryoFax1_3.Enabled = false;
            iIryoKaisetsushutai.Enabled = false;
            iIryoByoshoUmu.Enabled = false;
            iIryoByoshoCnt.Enabled = false;
            iIryoHeisetsu.Enabled = false;
            iIryoHeisetsuEtc.Enabled = false;
            iIryoHeisetsuName.Enabled = false;
            iIryoKumi.Enabled = false;

            //-----担当診療科目Grid準備-----
            // ■コントロール設定
            // コンボボックス
            AppSTableCombo.SetComboBox_Shinryoka(iTantoKamoku, dvShinryo); // iSearch

            // ■Grid設定
            sgrid_shinryo = new SGridDB(grid_Sinryo, this);

            sgrid_shinryo.Add(new SGridDBText(t_kaiin_shinryoka.FID_Shinryoka, "名称", 0.0f, SGridDBCellDisp.Left)
            {
                UnboundFetch = ubShinryokaName, // ID→名称変換
                InputControl = iTantoKamoku,
                InputTabIndex = 1,
            });

            sgrid_shinryo.EndAdd(dvKaiinShinryo);

            // コントロールのDBバインド
            sctl_shinryo = new SControlDB(this, getDataRowShinryo);
            sctl_shinryo.Add(new SControlDBCombo(t_kaiin_shinryoka.FID_Shinryoka, iTantoKamoku));
            sctl_shinryo.EndAdd(AppSControlRule.Rule);

            // イベント登録
            btnKamokuAdd.Click += btnKamokuAdd_Click;
            btnKamokuDel.Click += btnKamokuDel_Click;
            dvKaiinShinryo.RowFetchDemand += dvKaiinShinryo_RowFetchDemand;

            changeFilterShinryoka(); // 表示中会員の情報のみへフィルタ
            rowFetchShinryo(); // コントロール値セット(グリッド初期表示値)

            //-----所属学会Grid準備-----
            // ■コントロール設定
            // コンボボックス
            dvGakkai = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_gakkai));
            AppSTableCombo.SetComboBox_Gakkai(iSyozokuGakkai, dvGakkai); // iSearch

            // ■Grid設定
            sgrid_gakkai = new SGridDB(grid_Gakkai, this);

            sgrid_gakkai.Add(new SGridDBText(t_kaiin_gakkai.FID_Gakkai, "名称", 0.0f, SGridDBCellDisp.Left)
            {
                UnboundFetch = ubGakkaiName, // ID→名称変換
                InputControl = iSyozokuGakkai,
                InputTabIndex = 1,
            });

            sgrid_gakkai.EndAdd(dvKaiinGakkai);

            // コントロールのDBバインド
            sctl_gakkai = new SControlDB(this, getDataRowGakkai);
            sctl_gakkai.Add(new SControlDBCombo(t_kaiin_gakkai.FID_Gakkai, iSyozokuGakkai));
            sctl_gakkai.EndAdd(AppSControlRule.Rule);

            // イベント登録
            btnGakkaiAdd.Click += btnGakkaiAdd_Click;
            btnGakkaiDel.Click += btnGakkaiDel_Click;
            dvKaiinGakkai.RowFetchDemand += dvKaiinGakkai_RowFetchDemand;

            changeFilterGakkai(); // 表示中会員の情報のみへフィルタ
            rowFetchGakkai(); // コントロール値セット(グリッド初期表示値)

            //-----参加医会Grid準備-----
            // ■コントロール設定
            // コンボボックス
            AppSCombo.SetComboBox(iShiharai, enumKbn.DShiharai, (int)eShiharai.None); // 支払方法

            dvKaihi = new DBView(AppGlobal.Kaihis.DbView); // iサーチ内でテーブル外項目を利用するためFillから変更
            dvKaihi.RowFilterQuery($"{t_kaihi.FKaihi_SearchUsed} = TRUE AND {t_kaihi.FKaihi_KaihiType} IN ({(int)eTypeKaihi.Ikai}, {(int)eTypeKaihi.Group})"); // 会費区分2,3
            dvKaihi.SortQuery($"{t_kaihi.FCD_Kaihi}");
            AppSTableCombo.SetComboBox_Kaihi2(iSankaIkai, dvKaihi); // iSearch 参加医会

            // ■Grid設定
            sgrid_kaihi = new SGridDB(grid_Kaihi, this);

            sgrid_kaihi.Add(new SGridDBText(t_kaiin_kaihi.FID_Kaihi, "名称", 250, SGridDBCellDisp.Left)
            {
                UnboundFetch = ubIkaiName, // ID→名称変換
                InputControl = iSankaIkai,
                InputTabIndex = 1,
            });
            sgrid_kaihi.Add(new SGridDBText(t_kaiin_kaihi.FKaihiShiharai, "支払方法", 0.0f, SGridDBCellDisp.Center)
            {
                UnboundFetch = ubShiharaiHoho, // 名称変換
                InputControl = iShiharai,
                InputTabIndex = 2,
            });

            sgrid_kaihi.EndAdd(dvKaiinKaihi);

            // コントロールのDBバインド
            sctl_kaihi = new SControlDB(this, getDataRowKaihi);
            sctl_kaihi.Add(new SControlDBCombo(t_kaiin_kaihi.FID_Kaihi, iSankaIkai));
            sctl_kaihi.Add(new SControlDBCombo(t_kaiin_kaihi.FKaihiShiharai, iShiharai));
            sctl_kaihi.EndAdd(AppSControlRule.Rule);

            // イベント登録
            btnKaihiAdd.Click += btnKaihiAdd_Click;
            btnKaihiDel.Click += btnKaihiDel_Click;
            dvKaiinKaihi.RowFetchDemand += dvKaiinKaihi_RowFetchDemand;

            changeFilterKaihi(); // 表示中会員の情報のみへフィルタ
            rowFetchKaihi(); // コントロール値セット(グリッド初期表示値)

            //-----異動情報Grid準備-----
            // ■Grid設定
            sgrid_ido = new SGridDB(grid_Ido, this);

            sgrid_ido.Add(new SGridDBDate(t_kaiin_ido.FIdoDate, "年月日", 100));
            sgrid_ido.Add(new SGridDBText(t_kaiin_ido.FIdoJiyuKbn, "事由", 90, SGridDBCellDisp.Left)
            {
                UnboundFetch = ubIdoJiyuKbn, // 名称変換
            });
            sgrid_ido.Add(new SGridDBUnbound(F_IdoDetail, "詳細", ubIdoJiyuDetail, 110, SGridDBCellDisp.Left)); // 利用DB値が1つではないためSGridDBUnboundを使用
            sgrid_ido.Add(new SGridDBUnbound(F_IdoDetailMemo, "その他", ubIdoJiyuDetailMemo, 0.0f, SGridDBCellDisp.Left));

            sgrid_ido.EndAdd(dvKaiinIdo);

            // イベント登録
            btnIdoAdd.Click += btnIdoAdd_Click;
            btnIdoEdit.Click += btnIdoEdit_Click;
            btnIdoDel.Click += btnIdoDel_Click;
            grid_Ido.MouseDoubleClick += grid_Ido_MouseDoubleClick; // 編集へ
            dvKaiinIdo.RowFetchDemand += dvKaiinIdo_RowFetchDemand;

            changeFilterIdo(); // 表示中会員の情報のみへフィルタ

            // ここまでに入れた最初の値を控える。閉じるときに、これと比べて、入力があったかを調べる
            shownrow = new RowSnapshot(editrow);
            shownChildren = childRowsText();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// 値が true / false の 2 つの項目を持つコンボを作って、true の項目を選んでおく
        /// </summary>
        static void setBoolCombo(SComboBox cmb, string trueText, string falseText)
        {
            cmb.BeginUpdate();
            cmb.Items.Clear();
            cmb.AddItem(trueText, true);
            cmb.AddItem(falseText, false);
            cmb.EndUpdate();

            cmb.SelectByValue(true);
        }

        /// <summary>
        /// この会員の、一覧（医師会の会費・診療科目・所属学会・参加医会・異動情報）の行の内容を、1 つの文字列にする。
        /// 閉じるときに、画面を表示したときのものと比べて、一覧に入力があったかを調べるのに使う
        /// （一覧の行は、追加のときなど、この画面を開く前に呼び元が足していることがあるので、テーブルの変更の有無では調べられない）。
        /// </summary>
        string childRowsText()
        {
            StringBuilder sb = new StringBuilder();

            foreach (DBView dv in new DBView[] { dvKaiinKaihiIshikai, dvKaiinShinryo, dvKaiinGakkai, dvKaiinKaihi, dvKaiinIdo })
            {
                sb.Append('[');
                for (int i = 0; i < dv.Count; i++)
                {
                    foreach (object value in dv[i].Row.ItemArray)
                    {
                        sb.Append(Cast.String(value)).Append('\t');
                    }
                    sb.Append('\n');
                }
                sb.Append(']');
            }

            return sb.ToString();
        }

        /// <summary>
        /// 退会事由コンボボックス変更
        /// </summary>
        void iTaikaiJiyu_SelectedIndexChanged(object sender, EventArgs e)
        {
            enabled_iTaikaiJiyuCtl();
        }

        /// <summary>
        /// 退会事由関連コントロール制御
        /// </summary>
        void enabled_iTaikaiJiyuCtl()
        {
            if (iTaikaiJiyu.SelectedIndex == (int)eTypeTaikaiJiyu.Ido)
            {
                iIshikaiIdosaki.Enabled = true; // 退会事由:医師会の異動→活性
            }
            else
            {
                iIshikaiIdosaki.Enabled = false;
            }

            if (iTaikaiJiyu.SelectedIndex == (int)eTypeTaikaiJiyu.Etc)
            {
                iTaikaiJiyuEtc.Enabled = true; // 退会事由:その他→活性
            }
            else
            {
                iTaikaiJiyuEtc.Enabled = false;
            }
        }

        /// <summary>
        /// 会員区分コンボボックス変更
        /// </summary>
        //void iKaiinKbn_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}

        /// <summary>
        /// 銀行-銀行コード 値変更
        /// </summary>
        private void iBankCD_TextChanged(object sender, EventArgs e)
        {
            fetchBank((STextBox)sender);
            fetchShiten((STextBox)sender);
        }

        /// <summary>
        /// 銀行-支店コード 値変更
        /// </summary>
        private void iShitenCD_TextChanged(object sender, EventArgs e)
        {
            fetchShiten((STextBox)sender);
        }

        /// <summary>
        /// 銀行-銀行コード,支店コード ダブルクリック
        /// </summary>
        private void cd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showBankCode((STextBox)sender);
        }

        /// <summary>
        /// 銀行-銀行名,支店名のセット(口座1～3)
        /// </summary>
        void fetchBank()
        {
            BankCode bc;
            string name = "";

            // 銀行名のセット(口座1)
            bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCD1.Text));
            name = "";
            if (bc != null)
            {
                name = bc.Name;
            }
            iBankName1.Text = name;

            // 銀行名のセット(口座2)
            bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCD2.Text));
            name = "";
            if (bc != null)
            {
                name = bc.Name;
            }
            iBankName2.Text = name;

            // 銀行名のセット(口座3)
            bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCD3.Text));
            name = "";
            if (bc != null)
            {
                name = bc.Name;
            }
            iBankName3.Text = name;
        }

        /// <summary>
        /// 銀行-支店名のセット(口座1～3)
        /// </summary>
        void fetchShiten()
        {
            t_bank_code si;
            string name = "";

            // 銀行-支店名のセット(口座1)
            si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD1.Text), Cast.Int(iShitenCD1.Text));
            if (si != null)
            {
                name = si.BCD_NameShiten + "支店";
            }
            iShitenName1.Text = name;

            // 銀行-支店名のセット(口座2)
            si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD2.Text), Cast.Int(iShitenCD2.Text));
            name = "";
            if (si != null)
            {
                name = si.BCD_NameShiten + "支店";
            }
            iShitenName2.Text = name;

            // 銀行-支店名のセット(口座3)
            si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD3.Text), Cast.Int(iShitenCD3.Text));
            name = "";
            if (si != null)
            {
                name = si.BCD_NameShiten + "支店";
            }
            iShitenName3.Text = name;
        }

        /// <summary>
        /// 銀行-銀行名のセット(入力時イベント用)
        /// </summary>
        void fetchBank(STextBox txtbox)
        {
            // 銀行名の取得
            BankCode bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(txtbox.Text));
            string name = "";
            if (bc != null)
            {
                name = bc.Name;
            }

            // イベント発生元分岐 文字列セット先決定
            switch (txtbox.Name)
            {
                case "iBankCD1":
                    iBankName1.Text = name;
                    break;
                case "iBankCD2":
                    iBankName2.Text = name;
                    break;
                case "iBankCD3":
                    iBankName3.Text = name;
                    break;
            }
        }

        /// <summary>
        /// 銀行-支店名のセット(入力時イベント用)
        /// </summary>
        void fetchShiten(STextBox txtbox)
        {
            t_bank_code si;
            string name = "";

            // イベント発生元分岐 銀行コード欄の特定やテキストセット先の決定
            switch (txtbox.Name)
            {
                case "iBankCD1":
                case "iShitenCD1":
                    si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD1.Text), Cast.Int(iShitenCD1.Text));
                    if (si != null)
                    {
                        name = si.BCD_NameShiten + "支店";
                    }
                    iShitenName1.Text = name;
                    break;

                case "iBankCD2":
                case "iShitenCD2":
                    si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD2.Text), Cast.Int(iShitenCD2.Text));
                    if (si != null)
                    {
                        name = si.BCD_NameShiten + "支店";
                    }
                    iShitenName2.Text = name;
                    break;

                case "iBankCD3":
                case "iShitenCD3":
                    si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD3.Text), Cast.Int(iShitenCD3.Text));
                    if (si != null)
                    {
                        name = si.BCD_NameShiten + "支店";
                    }
                    iShitenName3.Text = name;
                    break;
            }
        }

        /// <summary>
        /// 銀行コード選択呼出し
        /// </summary>
        void showBankCode(STextBox txtbox)
        {
            t_kaiin xrow = new t_kaiin(editrow);
            FormSelectorBankCode2 frm = new FormSelectorBankCode2();

            // イベント発生元分岐 銀行コード欄の特定やテキストセット先の決定
            switch (txtbox.Name)
            {
                case "iBankCD1":
                case "iShitenCD1":
//					FormSelectorBankCode2 frm = new FormSelectorBankCode2();
                    frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD1.Text), Cast.Int(iShitenCD1.Text));
//					frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(xrow.Kaiin_BankCode1_Null), Cast.Int(xrow.Kaiin_BankShitenCode1_Null));
                    frm.ShowDialog();
                    if (frm.FormCloseReason == FormCloseReason.Exec)
                    {
                        xrow.Kaiin_BankCode1 = frm.SelectedBankCode.BCD_Code;
                        xrow.Kaiin_BankShitenCode1 = frm.SelectedBankCode.BCD_CodeShiten;

                        rowFetch();
                    }
                    break;

                case "iBankCD2":
                case "iShitenCD2":
                    frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD2.Text), Cast.Int(iShitenCD2.Text));
                    frm.ShowDialog();
                    if (frm.FormCloseReason == FormCloseReason.Exec)
                    {
                        xrow.Kaiin_BankCode2 = frm.SelectedBankCode.BCD_Code;
                        xrow.Kaiin_BankShitenCode2 = frm.SelectedBankCode.BCD_CodeShiten;

                        rowFetch();
                    }
                    break;

                case "iBankCD3":
                case "iShitenCD3":
                    frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD3.Text), Cast.Int(iShitenCD3.Text));
                    frm.ShowDialog();
                    if (frm.FormCloseReason == FormCloseReason.Exec)
                    {
                        xrow.Kaiin_BankCode3 = frm.SelectedBankCode.BCD_Code;
                        xrow.Kaiin_BankShitenCode3 = frm.SelectedBankCode.BCD_CodeShiten;

                        rowFetch();
                    }
                    break;
            }

            frm.Dispose();
        }

        // --医師会会員Grid--
        /// <summary>
        ///  Enum値より、Gridに医会区分名を表示します。
        /// </summary>
        string ubTypeIshikai(SGridDBColumn col, int row)
        {
            t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihiIshikai[row]);

            if (enumKbn.DTypeIkai.ContainsKey((int)xrow.KaihiTypeIkai) == true)
            {
                return enumKbn.DTypeIkai[(int)xrow.KaihiTypeIkai];
            }

            return null;
        }

        /// <summary>
        /// IDを基に、Gridに医会名を表示します。
        /// </summary>
        string ubIkaiNameIshikai(SGridDBColumn col, int row)
        {
            t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihiIshikai[row]);

            Kaihi kh = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

            string name = "";
            if (kh != null)
            {
                name = kh.XRow.Kaihi_Name;
            }

            return name;
        }

        /// <summary>
        /// Enum値より、Gridに支払方法名を表示します。
        /// </summary>
        string ubShiharaiHohoIshikai(SGridDBColumn col, int row)
        {
            t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihiIshikai[row]);
            //			eShiharai hoho = Cast.Enumelate<eShiharai>(xrow.KaihiShiharai_Null, eShiharai.None);
            //			Kaihi kh = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

            if (enumKbn.DShiharai.ContainsKey((int)xrow.KaihiShiharai) == true)
            {
                return enumKbn.DShiharai[(int)xrow.KaihiShiharai];
            }

            return null;
        }

        /// <summary>
        /// 医師会会員gridの現在行を返します。
        /// </summary>
        /// <returns>現在行</returns>
        DataRow getDataRowKaihiIshikai()
        {
            if (dvKaiinKaihiIshikai.CurrentRow != null && dvKaiinKaihiIshikai.Count > 0)
            {
                return dvKaiinKaihiIshikai.CurrentRow.Row;
            }

            return null;
        }

        /// <summary>
        /// フィルタ変更(t_kaiin_kaihi)_医師会会員
        /// </summary>
        void changeFilterKaihiIshikai()
        {
            string filter = "";

            // 該当会員の情報&医会区分が1,2,3へフィルタ
            t_kaiin tmprow = new t_kaiin(editrow);

            filter =
                $"{t_kaiin_kaihi.FID_Kaiin} = {tmprow.ID_Kaiin}" +
                $" AND {t_kaiin_kaihi.FKaihiTypeIkai} IN ({(int)eTypeIkai.Nichii}, {(int)eTypeIkai.Keni}, {(int)eTypeIkai.Shii})";

            // 適用
            dvKaiinKaihiIshikai.RowFilterQuery(filter);
        }

        /// <summary>
        /// Gridカレント行の内容を各コントロールにセットします(医師会会員grid)
        /// </summary>
        void rowFetchKaihiIshikai()
        {
            if (sctl_kaihiIshikai == null)
            {
                //				Debug.WriteLine("SControlDBが設定されていません。");
                return;
            }

            if (dvKaiinKaihiIshikai.CurrentRow == null)
            {
                // 現在行がないので、コントロールの内容をクリアする。
                sctl_kaihiIshikai.ClearAll();
            }
            else
            {
                // 現在行を取得
                sctl_kaihiIshikai.FetchAll();
            }
        }

        // --医療機関情報関連--
        /// <summary>
        /// 医療機関情報-開設主体の文字列変換
        /// <param name="data">バインドしたDB値</param>
        /// </summary>
        object KaisetsuShutaiEx(ISRow row, object data)
        {
            // 開設主体情報の取得
            Kaisetsushutai ks = AppGlobal.Kaisetsushutais.Get(Cast.Int(data));

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
        /// 医療機関情報-病床有無の文字列変換
        /// <param name="data">バインドしたDB値</param>
        /// </summary>
        object ByoshoUmuEx(ISRow row, object data)
        {
            // DB値によって名称をreturn
            if (Cast.Bool(data) == true)
            {
                return "有";
            }
            else
            {
                return "無";
            }
        }

        /// <summary>
        /// 医療機関情報-組コードの文字列変換
        /// <param name="data">バインドしたDB値</param>
        /// </summary>
        object KumiCodeEx(ISRow row, object data)
        {
            // 組コード情報の取得
            KumiCode kc = AppGlobal.KumiCodes.Get(Cast.Int(data));

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
        /// 指定医情報をチェックボックスにセットします。
        /// </summary>
        void fetchIryoShiteii()
        {
            int si = Cast.Int(this.Row[t_kaiin.FKaiin_Shiteii]);

            // チェックのコントロール配列

            List<SCheckBox> shiteiis = new List<SCheckBox>()
            {
                iIryoShiteii1, iIryoShiteii2, iIryoShiteii3, iIryoShiteii4, iIryoShiteii5,
                iIryoShiteii6, iIryoShiteii7, iIryoShiteii8, iIryoShiteii9, iIryoShiteii10,
            };

            // コントロールごとに処理
            foreach (SCheckBox chk in shiteiis)
            {
                if ((si & (int)chk.Tag) != 0)
                {
                    chk.Checked = true;
                }
            }

            // その他チェックありの場合、入力欄を活性
            enabled_ShiteiiCtl();
        }

        /// <summary>
        /// 指定医情報をDB対象フィールドにセットします。
        /// </summary>
        void updateIryoShiteii()
        {
            int shiteii = 0;

            // チェックのコントロール配列
            List<SCheckBox> shiteiis = new List<SCheckBox>()
            {
                iIryoShiteii1, iIryoShiteii2, iIryoShiteii3, iIryoShiteii4, iIryoShiteii5,
                iIryoShiteii6, iIryoShiteii7, iIryoShiteii8, iIryoShiteii9, iIryoShiteii10,
            };

            // コントロールごとに処理
            foreach (SCheckBox chk in shiteiis)
            {
                if (chk.Checked == true)
                {
                    shiteii |= (int)chk.Tag;
                }
            }

            this.Row[t_kaiin.FKaiin_Shiteii] = shiteii;

            // その他チェックありの場合、DB値をセット, それ以外はクリア
            /*if (iIryoShiteii10.Checked == true)
            {
                this.Row[t_kaiin.FKaiin_ShiteiiEtc] = iIryoShiteiiEtc.Text;
            }
            else
            {
                this.Row[t_kaiin.FKaiin_ShiteiiEtc] = null;
            }*/

        }

        /// <summary>
        /// 医療機関-指定医:その他 チェック変更
        /// </summary>
        void iIryoShiteii10_CheckedChanged(object sender, EventArgs e)
        {
            enabled_ShiteiiCtl();
        }

        /// <summary>
        /// 指定医情報のその他メモ欄制御
        /// </summary>
        void enabled_ShiteiiCtl()
        {
            // その他チェックありの場合、入力欄を活性
            if (iIryoShiteii10.Checked == true)
            {
                iIryoShiteiiEtc.Enabled = true;
            }
            else
            {
                iIryoShiteiiEtc.Enabled = false;
                iIryoShiteiiEtc.Text = null;
            }
        }

        /// <summary>
        /// 医療機関情報の現在行を返します。
        /// </summary>
        /// <returns>現在行</returns>
        DataRow getDataRowIryo()
        {
            if (dvIryoKikan.CurrentRow != null && dvIryoKikan.Count > 0)
            {
                return dvIryoKikan.CurrentRow.Row;
            }

            return null;
        }

        /// <summary>
        /// 画面表示時フィルタ(t_iryokikan)
        /// </summary>
        void changeFilterIryoKikanShown()
        {
            string filter = "";

            // 該当会員の医療機関情報へフィルタ
            t_kaiin tmprow = new t_kaiin(editrow);

            filter =
                $"{t_iryokikan.FID_Iryokikan} = {tmprow.ID_Iryokikan}";

            // 適用
            dvIryoKikan.RowFilterQuery(filter);
        }

        /// <summary>
        /// 入力時フィルタ変更(t_iryokikan)。選んだ医療機関の情報を表示する
        /// </summary>
        void changeFilterIryoKikan()
        {
            // 選んでいる医療機関（未選択なら、どの行にも当たらない 0 になる）
            int iryoKikanId = Cast.Int(iIryoKikan.SelectedValue);

            // フィルタ適用
            dvIryoKikan.RowFilterQuery($"{t_iryokikan.FID_Iryokikan} = {iryoKikanId}");
        }

        /// <summary>
        /// 医療機関マスタDBView値を各コントロールにセットします
        /// </summary>
        void rowFetchIryoKikan()
        {
            if (sctl_iryo == null)
            {
                //				Debug.WriteLine("SControlDBが設定されていません。");
                return;
            }

            if (dvIryoKikan.CurrentRow == null)
            {
                // 現在行がないので、コントロールの内容をクリアする。
                sctl_iryo.ClearAll();
            }
            else
            {
                // 現在行を取得
                sctl_iryo.FetchAll();
            }

            //setEnableFunctionButton();
        }

        /// <summary>
        /// 医療機関コード イベント
        /// </summary>
        private void iIryoKikan_SelectedValueChanged(object sender, EventArgs e)
        {
            changeFilterIryoKikan();
            rowFetchIryoKikan();
        }

        // --診療Grid--
        /// <summary>
        /// IDを基に、Gridに診療科名を表示します。
        /// </summary>
        string ubShinryokaName(SGridDBColumn col, int row)
        {
            t_kaiin_shinryoka xrow = new t_kaiin_shinryoka(dvKaiinShinryo[row]);

            Shinryoka sk = AppGlobal.Shinryokas.Get(xrow.ID_Shinryoka);

            string name = "";
            if (sk != null)
            {
                name = sk.XRow.SRK_Name;
            }

            return name;
        }

        /// <summary>
        /// 診療科目gridの現在行を返します。
        /// </summary>
        /// <returns>現在行</returns>
        DataRow getDataRowShinryo()
        {
            if (dvKaiinShinryo.CurrentRow != null && dvKaiinShinryo.Count > 0)
            {
                return dvKaiinShinryo.CurrentRow.Row;
            }

            return null;
        }

        /// <summary>
        /// フィルタ変更(t_kaiin_shinryoka)
        /// </summary>
        void changeFilterShinryoka()
        {
            string filter = "";

            // 該当会員の情報へフィルタ
            t_kaiin tmprow = new t_kaiin(editrow);

            filter =
                $"{t_kaiin_shinryoka.FID_Kaiin} = {tmprow.ID_Kaiin}";

            // 適用
            dvKaiinShinryo.RowFilterQuery(filter);
        }

        /// <summary>
        /// Gridカレント行の内容を各コントロールにセットします(担当診療Grid)
        /// </summary>
        void rowFetchShinryo()
        {
            if (sctl_shinryo == null)
            {
//				Debug.WriteLine("SControlDBが設定されていません。");
                return;
            }

            if (dvKaiinShinryo.CurrentRow == null)
            {
                // 現在行がないので、コントロールの内容をクリアする。
                sctl_shinryo.ClearAll();
            }
            else
            {
                // 現在行を取得
                sctl_shinryo.FetchAll();
            }

            //setEnableFunctionButton();
        }

        /// <summary>
        /// 追加(担当診療Grid)
        /// </summary>
        private void btnKamokuAdd_Click(object sender, EventArgs e)
        {
            t_kaiin tmprow = new t_kaiin(editrow);
            t_kaiin_shinryoka nrow = new t_kaiin_shinryoka(dvKaiinShinryo.NewRow());

            // NewRowへフィルタ済の会員IDセット
            nrow.ID_Kaiin = tmprow.ID_Kaiin;

            // 行追加
            dvKaiinShinryo.Add(nrow.Row);
            // 追加した行をカレント行にする
            sgrid_shinryo.SetRow(dvKaiinShinryo.Count - 1);

            // 編集状態にして表示
            sgrid_shinryo.Select();
            grid_Sinryo.Col = 0;
            sgrid_shinryo.SelectInput();
        }

        /// <summary>
        /// 削除(担当診療Grid)
        /// </summary>
        private void btnKamokuDel_Click(object sender, EventArgs e)
        {
            if (dvKaiinShinryo.Count > 0)
            {
                t_kaiin_shinryoka xrow = new t_kaiin_shinryoka(dvKaiinShinryo.CurrentRow);

                // 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
                //				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
                //				{
                //					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
                //					return;
                //				}

                DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の診療科目(担当)情報");
                if (dr == DialogResult.No)
                {
                    return;
                }

                dvKaiinShinryo.Delete();
                grid_Sinryo.Refresh();

                // 削除後にソート番号を振り直す。
                //				for (int i = 0; i < dvRoom.Count; i++)
                //				{
                //					T_BukkenRoom row = new T_BukkenRoom(dvRoom[i]);

                //					row.BukkenRoom_SortNo = i;
                //				}

                // 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
                sgrid_shinryo.Select();
            }
        }

        /// <summary>
        /// 変更要求処理(担当診療Grid)
        /// </summary>
        void dvKaiinShinryo_RowFetchDemand(object sender, EventArgs e)
        {
            // 再フィルタ
            changeFilterShinryoka();

            // コントロール値セット
            rowFetchShinryo();
        }

        // --学会Grid--
        /// <summary>
        /// IDを基に、Gridに学会名を表示します。
        /// </summary>
        string ubGakkaiName(SGridDBColumn col, int row)
        {
            t_kaiin_gakkai xrow = new t_kaiin_gakkai(dvKaiinGakkai[row]);

            Gakkai gk = AppGlobal.Gakkais.Get(xrow.ID_Gakkai);

            string name = "";
            if (gk != null)
            {
                name = gk.XRow.GKAI_Name;
            }

            return name;
        }

        /// <summary>
        /// 所属学会gridの現在行を返します。
        /// </summary>
        /// <returns>現在行</returns>
        DataRow getDataRowGakkai()
        {
            if (dvKaiinGakkai.CurrentRow != null && dvKaiinGakkai.Count > 0)
            {
                return dvKaiinGakkai.CurrentRow.Row;
            }

            return null;
        }

        /// <summary>
        /// フィルタ変更(t_kaiin_gakkai)
        /// </summary>
        void changeFilterGakkai()
        {
            string filter = "";

            // 該当会員の情報へフィルタ
            t_kaiin tmprow = new t_kaiin(editrow);

            filter =
                $"{t_kaiin_gakkai.FID_Kaiin} = {tmprow.ID_Kaiin}";

            // 適用
            dvKaiinGakkai.RowFilterQuery(filter);
        }

        /// <summary>
        /// Gridカレント行の内容を各コントロールにセットします(所属学会Grid)
        /// </summary>
        void rowFetchGakkai()
        {
            if (sctl_gakkai == null)
            {
//				Debug.WriteLine("SControlDBが設定されていません。");
                return;
            }

            if (dvKaiinGakkai.CurrentRow == null)
            {
                // 現在行がないので、コントロールの内容をクリアする。
                sctl_gakkai.ClearAll();
            }
            else
            {
                // 現在行を取得
                sctl_gakkai.FetchAll();
            }

            //setEnableFunctionButton();
        }

        /// <summary>
        /// 追加(所属学会Grid)
        /// </summary>
        private void btnGakkaiAdd_Click(object sender, EventArgs e)
        {
            t_kaiin tmprow = new t_kaiin(editrow);
            t_kaiin_gakkai nrow = new t_kaiin_gakkai(dvKaiinGakkai.NewRow());

            // NewRowへフィルタ済の会員IDセット
            nrow.ID_Kaiin = tmprow.ID_Kaiin;

            // 行追加
            dvKaiinGakkai.Add(nrow.Row);
            // 追加した行をカレント行にする
            sgrid_gakkai.SetRow(dvKaiinGakkai.Count - 1);

            // 編集状態にして表示
            sgrid_gakkai.Select();
            grid_Gakkai.Col = 0;
            sgrid_gakkai.SelectInput();
        }

        /// <summary>
        /// 削除(所属学会Grid)
        /// </summary>
        private void btnGakkaiDel_Click(object sender, EventArgs e)
        {
            if (dvKaiinGakkai.Count > 0)
            {
                t_kaiin_gakkai xrow = new t_kaiin_gakkai(dvKaiinGakkai.CurrentRow);

                // 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
//				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
//				{
//					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
//					return;
//				}

                DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の学会所属情報");
                if (dr == DialogResult.No)
                {
                    return;
                }

                dvKaiinGakkai.Delete();
                grid_Gakkai.Refresh();

                // 削除後にソート番号を振り直す。
//				for (int i = 0; i < dvRoom.Count; i++)
//				{
//					T_BukkenRoom row = new T_BukkenRoom(dvRoom[i]);

//					row.BukkenRoom_SortNo = i;
//				}

                // 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
                sgrid_gakkai.Select();
            }
        }

        /// <summary>
        /// 変更要求処理(所属学会Grid)
        /// </summary>
        void dvKaiinGakkai_RowFetchDemand(object sender, EventArgs e)
        {
            // 再フィルタ
            changeFilterGakkai();

            // コントロール値セット
            rowFetchGakkai();
        }

        // --参加医会Grid--
        /// <summary>
        /// IDを基に、Gridに医会名を表示します。
        /// </summary>
        string ubIkaiName(SGridDBColumn col, int row)
        {
            t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihi[row]);

            Kaihi kh = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

            string name = "";
            if (kh != null)
            {
                name = kh.XRow.Kaihi_Name;
            }

            return name;
        }

        /// <summary>
        /// Enum値より、Gridに支払方法名を表示します。
        /// </summary>
        string ubShiharaiHoho(SGridDBColumn col, int row)
        {
            t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihi[row]);
//			eShiharai hoho = Cast.Enumelate<eShiharai>(xrow.KaihiShiharai_Null, eShiharai.None);
            //			Kaihi kh = AppGlobal.Kaihis.Get(xrow.ID_Kaihi);

            if (enumKbn.DShiharai.ContainsKey((int)xrow.KaihiShiharai) == true)
            {
                return enumKbn.DShiharai[(int)xrow.KaihiShiharai];
            }

            return null;
        }

        /// <summary>
        /// 参加医会gridの現在行を返します。
        /// </summary>
        /// <returns>現在行</returns>
        DataRow getDataRowKaihi()
        {
            if (dvKaiinKaihi.CurrentRow != null && dvKaiinKaihi.Count > 0)
            {
                return dvKaiinKaihi.CurrentRow.Row;
            }

            return null;
        }

        /// <summary>
        /// フィルタ変更(t_kaiin_kaihi)
        /// </summary>
        void changeFilterKaihi()
        {
            string filter = "";

            // 該当会員の情報&医会区分が4へフィルタ
            t_kaiin tmprow = new t_kaiin(editrow);

            filter =
                $"{t_kaiin_kaihi.FID_Kaiin} = {tmprow.ID_Kaiin}" +
                $" AND {t_kaiin_kaihi.FKaihiTypeIkai} = {(int)eTypeIkai.Etc}";

            // 適用
            dvKaiinKaihi.RowFilterQuery(filter);
        }

        /// <summary>
        /// Gridカレント行の内容を各コントロールにセットします(参加医会Grid)
        /// </summary>
        void rowFetchKaihi()
        {
            if (sctl_kaihi == null)
            {
//				Debug.WriteLine("SControlDBが設定されていません。");
                return;
            }

            if (dvKaiinKaihi.CurrentRow == null)
            {
                // 現在行がないので、コントロールの内容をクリアする。
                sctl_kaihi.ClearAll();
            }
            else
            {
                // 現在行を取得
                sctl_kaihi.FetchAll();
            }

            //setEnableFunctionButton();
        }

        /// <summary>
        /// 追加(参加医会Grid)
        /// </summary>
        private void btnKaihiAdd_Click(object sender, EventArgs e)
        {
            t_kaiin tmprow = new t_kaiin(editrow);
            t_kaiin_kaihi nrow = new t_kaiin_kaihi(dvKaiinKaihi.NewRow());

            // NewRowへフィルタ済の会員IDセット
            nrow.ID_Kaiin = tmprow.ID_Kaiin;

            // デフォルト値セット
            nrow.KaihiShiharai = eShiharai.Koza1; // 支払方法
            nrow.KaihiTypeIkai = eTypeIkai.Etc; // その他(参加医会)

            // 行追加
            dvKaiinKaihi.Add(nrow.Row);
            // 追加した行をカレント行にする
            sgrid_kaihi.SetRow(dvKaiinKaihi.Count - 1);

            // 編集状態にして表示
            sgrid_kaihi.Select();
            grid_Kaihi.Col = 0;
            sgrid_kaihi.SelectInput();
        }

        /// <summary>
        /// 削除(参加医会Grid)
        /// </summary>
        private void btnKaihiDel_Click(object sender, EventArgs e)
        {
            if (dvKaiinKaihi.Count > 0)
            {
                t_kaiin_kaihi xrow = new t_kaiin_kaihi(dvKaiinKaihi.CurrentRow);

                // 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
                //				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
                //				{
                //					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
                //					return;
                //				}

                DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の参加医会情報");
                if (dr == DialogResult.No)
                {
                    return;
                }

                dvKaiinKaihi.Delete();
                grid_Kaihi.Refresh();

                // 削除後にソート番号を振り直す。
                //				for (int i = 0; i < dvRoom.Count; i++)
                //				{
                //					T_BukkenRoom row = new T_BukkenRoom(dvRoom[i]);

                //					row.BukkenRoom_SortNo = i;
                //				}

                // 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
                sgrid_kaihi.Select();
            }
        }

        /// <summary>
        /// 変更要求処理(参加医会Grid)
        /// </summary>
        void dvKaiinKaihi_RowFetchDemand(object sender, EventArgs e)
        {
            // 再フィルタ
            changeFilterKaihi();

            // コントロール値セット
            rowFetchKaihi();
        }

        // --異動情報Grid--
        /// <summary>
        /// Gridに異動事由名称を表示します(Enum値より)。
        /// </summary>
        string ubIdoJiyuKbn(SGridDBColumn col, int row)
        {
            t_kaiin_ido xrow = new t_kaiin_ido(dvKaiinIdo[row]);

            return enumKbn.DTypeIdoJiyu[(int)xrow.IdoJiyuKbn];
        }

        /// <summary>
        /// Gridに異動詳細名称を表示します(Enum値より)。
        /// </summary>
        string ubIdoJiyuDetail(SGridDBColumn col, int row)
        {
            t_kaiin_ido xrow = new t_kaiin_ido(dvKaiinIdo[row]);
            string name = "";

            // 異動事由区分により詳細の参照値を決定
            switch (xrow.IdoJiyuKbn)
            {
                // 医師会の異動・施設開業・施設異動
                case eTypeIdoJiyu.Ido:
                    name = enumKbn.DIdoShisetsu[(int)xrow.IdoJiyuDetail_Ido];
                    break;

                // 会員区分の変更
                case eTypeIdoJiyu.Kaiin:
                    name = enumKbn.DIdoKaiinHenko[(int)xrow.IdoJiyuDetail_Kaiin];
                    break;

                // 施設所在地変更
                case eTypeIdoJiyu.Shozai:
                    name = enumKbn.DIdoShozai[(int)xrow.IdoJiyuDetail_Shozai];
                    break;

                // 現住所変更
                case eTypeIdoJiyu.Address:
                    name = enumKbn.DIdoAddress[(int)xrow.IdoJiyuDetail_Address];
                    break;

                // その他
                case eTypeIdoJiyu.Etc:
                    name = enumKbn.DIdoEtc[(int)xrow.IdoJiyuDetail_Etc];
                    break;

                default:
                    break;
            }

            return name;
        }

        /// <summary>
        /// Gridに異動詳細その他のメモを表示します。
        /// </summary>
        string ubIdoJiyuDetailMemo(SGridDBColumn col, int row)
        {
            t_kaiin_ido xrow = new t_kaiin_ido(dvKaiinIdo[row]);
            string memo = "";

            // 異動事由区分により詳細の参照値を決定
            switch (xrow.IdoJiyuKbn)
            {
                // 医師会の異動・施設開業・施設異動
                case eTypeIdoJiyu.Ido:
                    memo = xrow.IdoJiyuDetail_IdoMemo_Null;
                    break;

                // 会員区分の変更
                case eTypeIdoJiyu.Kaiin:
                    //memo = xrow.IdoJiyuDetail_KaiinMemo_Null; // 値登録箇所なし
                    break;

                // 施設所在地変更
                case eTypeIdoJiyu.Shozai:
                    memo = xrow.IdoJiyuDetail_ShozaiMemo_Null;
                    break;

                // 現住所変更
                case eTypeIdoJiyu.Address:
                    memo = xrow.IdoJiyuDetail_AddressMemo_Null;
                    break;

                // その他
                case eTypeIdoJiyu.Etc:
                    memo = xrow.IdoJiyuDetail_EtcMemo_Null;
                    break;

                default:
                    break;
            }

            return memo;
        }

        //		/// <summary>
        //		/// 異動情報gridの現在行を返します。
        //		/// </summary>
        //		/// <param name="datatag">タグ</param>
        //		/// <returns>現在行</returns>
        //		DataRow getDataRowIdo()
        //		{
        //			if (dvKaiinIdo.CurrentRow != null && dvKaiinIdo.Count > 0)
        //			{
        //				return dvKaiinIdo.CurrentRow.Row;
        //			}

        //			return null;
        //		}

        /// <summary>
        /// フィルタ変更(t_kaiin_ido)
        /// </summary>
        void changeFilterIdo()
        {
            string filter = "";

            // 該当会員の情報へフィルタ
            t_kaiin tmprow = new t_kaiin(editrow);

            filter =
                $"{t_kaiin_ido.FID_Kaiin} = {tmprow.ID_Kaiin}";

            // 適用
            dvKaiinIdo.RowFilterQuery(filter);
        }

        /// <summary>
        /// 追加ボタン(異動情報Grid)
        /// </summary>
        private void btnIdoAdd_Click(object sender, EventArgs e)
        {
            t_kaiin tmprow = new t_kaiin(editrow);
            t_kaiin_ido nrow = new t_kaiin_ido(dvKaiinIdo.NewRow());

            // NewRowへ内部項目をセット
            nrow.ID_Kaiin = tmprow.ID_Kaiin; // フィルタ済の会員ID
            nrow.ID_Ido = AppDbID.GetNewID(dvKaiinIdo, t_kaiin_ido.FID_Ido); // 異動情報ID(新規)

            // 編集画面表示
            FormKaiin2_DlgEntry_Idoinfo frm = new FormKaiin2_DlgEntry_Idoinfo();
            frm.Mode = FormKaiin2_DlgEntry_Idoinfo.eMode.Add;
            frm.Row = nrow.Row;
            frm.ShowDialog();

            if (frm.FormCloseReason == FormCloseReason.Save)
            {
                // DBViewへ行追加
                dvKaiinIdo.Add(frm.Row);

                // 追加行にフォーカス
                dvKaiinIdo.SearchRow(t_kaiin_ido.FID_Ido, nrow.ID_Ido);
            }

            frm.Dispose();
            frm = null;
        }

        /// <summary>
        /// 訂正ボタン(異動情報Grid)
        /// </summary>
        private void btnIdoEdit_Click(object sender, EventArgs e)
        {
            if (dvKaiinIdo.Count > 0)
            {
                rowEditIdo();
            }
        }

        /// <summary>
        /// ダブルクリック(異動情報Grid)
        /// </summary>
        private void grid_Ido_MouseDoubleClick(object sender, EventArgs e)
        {
            if (dvKaiinIdo.Count > 0)
            {
                rowEditIdo();
            }
        }

        /// <summary>
        /// 編集(異動情報Grid)
        /// </summary>
        void rowEditIdo()
        {
            if (dvKaiinIdo.Count > 0)
            {
                DataRow row = dvKaiinIdo.NewRow();

                AppDb.CopyDataRow(dvKaiinIdo.CurrentRow.Row, row);

                FormKaiin2_DlgEntry_Idoinfo frm = new FormKaiin2_DlgEntry_Idoinfo();
                frm.Mode = FormKaiin2_DlgEntry_Idoinfo.eMode.Edit;
                frm.Row = row;
                frm.ShowDialog();

                if (frm.FormCloseReason == FormCloseReason.Save)
                {
                    AppDb.CopyDataRow(frm.Row, dvKaiinIdo.CurrentRow.Row);
                    // DB更新はDlgEntry呼び元画面で実施
                }

                frm.Dispose();
                frm = null;
            }
        }

        /// <summary>
        /// 削除(異動情報Grid)
        /// </summary>
        private void btnIdoDel_Click(object sender, EventArgs e)
        {
            if (dvKaiinIdo.Count > 0)
            {
                t_kaiin_ido xrow = new t_kaiin_ido(dvKaiinIdo.CurrentRow);

                // 他で使われているかチェック,必要か検討 → 医会会費のときは必要そう？
                //				if (AppDbKeiyaku.CheckUsedField(T_Keiyaku.FID_BukkenRoom, xrow.ID_BukkenRoom) == true)
                //				{
                //					AppMsgBox.Show(AppMsgBoxIndex.CanNotDeleteReaseonUsed, xrow.BukkenRoom_Name);
                //					return;
                //				}

                DialogResult dr = AppMsgBox.Show(AppMsgBoxIndex.Delete, "選択中の異動情報");
                if (dr == DialogResult.No)
                {
                    return;
                }

                dvKaiinIdo.Delete();
                grid_Ido.Refresh();

                // 入力状態になっているかもしれないので、グリッドへフォーカスを移す。
                sgrid_ido.Select();
            }
        }

        /// <summary>
        /// 変更要求処理(異動情報Grid)
        /// </summary>
        void dvKaiinIdo_RowFetchDemand(object sender, EventArgs e)
        {
            // 再フィルタ
            changeFilterIdo();
        }

        /// <summary>
        /// 行情報の取得(会員情報)
        /// </summary>
        void rowFetch()
        {
            if (Row != null)
            {
                sctl.FetchAll();

                // 医療機関-指定医のセット
                fetchIryoShiteii();
            }
            else
            {
                sctl.ClearAll();
            }
        }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formCloseReason != App.FormCloseReason.Save)
            {
                // フォーカスがある入力欄の内容を、行へ書き戻してから、変更の有無を調べる
                updateFocusedInput();

                // 画面を表示したときから内容が変わっているときだけ、破棄の確認を出す（一覧の行の変更も含む）
                if (shownrow != null && (shownrow.IsModified(editrow) == true || shownChildren != childRowsText()))
                {
                    if (AppMsgBox.Show(AppMsgBoxIndex.CancelClose) == System.Windows.Forms.DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }

            base.FormFrame_FormClosing(sender, e);
        }

        /// <summary>
        /// フォーカスがある入力欄の内容を、行へ書き戻す（ファンクション キーや、閉じるボタンでは、フォーカスが動かないため）。
        /// 一覧のセルで入力している最中なら、その内容は、一覧の行へ書き戻す
        /// </summary>
        void updateFocusedInput()
        {
            foreach (SControlDB ctl in new SControlDB[] { sctl, sctl_iryo, sctl_kaihiIshikai, sctl_shinryo, sctl_gakkai, sctl_kaihi })
            {
                if (ctl != null)
                {
                    ctl.Update();
                }
            }
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncKaiin_DlgEntry.Functions);

            funcKeys.On(FuncKaiin_DlgEntry.ShowTab1, showTab1);
            funcKeys.On(FuncKaiin_DlgEntry.ShowTab2, showTab2);
            funcKeys.On(FuncKaiin_DlgEntry.ShowTab3, showTab3);

            funcKeys.On(FuncKaiin_DlgEntry.Save, saveClose);
            funcKeys.On(FuncKaiin_DlgEntry.Cancel, closeCancel);

//			bool enabled = false;

            //if (AppGlobal.LoginUser.AvailableMyno == true)
            ////if (AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.Admin ||
            ////  　AppGlobal.LoginUser.XRow.TNT_Auth == eAuth.SU)
            //{
            //	enabled = true;
            //}

        }

        /// <summary>
        /// 検証処理
        /// </summary>
        /// <returns></returns>
        bool validateRow()
        {
            // 最終フォーカスコントロールをRowへ反映
            updateFocusedInput();

            // 医療機関-指定医のDB値セット(chkbox)
            updateIryoShiteii();

            // 更新担当者のセット
            this.Row[t_kaiin.FLastUpdateUser] = AppGlobal.LoginUser.ID;

            // 退会事由:医師会の異動 以外を選択→異動先医師会のDB値をクリア
            if (iTaikaiJiyu.SelectedIndex != (int)eTypeTaikaiJiyu.Ido)
            {
                this.Row[t_kaiin.FID_Ishikai_Idosaki] = DBNull.Value;
            }
            // 退会事由:その他 以外を選択→退会その他詳細のDB値をクリア
            if (iTaikaiJiyu.SelectedIndex != (int)eTypeTaikaiJiyu.Etc)
            {
                this.Row[t_kaiin.FKaiin_TaikaiEtcmemo] = null;
            }

            // 空白チェック
            Control[] ctls =
            {
                iCode, iName, iNameFurigana, iNyukaiDate, iZaiseki, iTorokuDate, iBunshoSofu,
            };

            foreach (Control ctl in ctls)
            {
                bool blank;

                SDateBox date = ctl as SDateBox;
                if (date != null)
                {
                    blank = date.Value == null; // 日付変換できない場合Valueはnull
                }
                else
                {
                    blank = ctl.Text.Length == 0;
                }

                if (blank == true)
                {
                    showInputError(ctl, AppToolTipIndex.CannotUseBlank);
                    return false;
                }
            }

            // 重複チェック
            Kaiin obj = AppGlobal.Kaiins.GetCode(iCode.Text);

            if (obj != null && obj.ID != Cast.Int(editrow[t_kaiin.FID_Kaiin]))
            {
                showInputError(iCode, AppToolTipIndex.BookingCode);
                return false;
            }

            // 日付範囲チェック

            return true;
        }

        /// <summary>
        /// 入力の誤りを、その入力欄に表示する。入力欄が、表示していないタブのページにあるときは、先にそのページへ切り替える
        /// （切り替えないと、フォーカスを移せず、メッセージも見えない位置に出る）
        /// </summary>
        void showInputError(Control ctl, AppToolTipIndex index)
        {
            for (Control parent = ctl.Parent; parent != null; parent = parent.Parent)
            {
                TabPage page = parent as TabPage;
                if (page != null)
                {
                    tabInfo.SelectedTab = page;
                    break;
                }
            }

            ctl.Select();
            appToolTip.Show(ctl, index);
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
        /// タブ表示切替
        /// </summary>
        void showTab1()
        {
            tabInfo.SelectedIndex = 0;
        }

        /// <summary>
        /// タブ表示切替
        /// </summary>
        void showTab2()
        {
            tabInfo.SelectedIndex = 1;
        }

        /// <summary>
        /// タブ表示切替
        /// </summary>
        void showTab3()
        {
            tabInfo.SelectedIndex = 2;
        }

        /// <summary>
        /// データ取得
        /// </summary>
        DataRow getDataRow()
        {
            if (editrow != null)
            {
                return editrow;
            }

            return null;
        }
    }
}
