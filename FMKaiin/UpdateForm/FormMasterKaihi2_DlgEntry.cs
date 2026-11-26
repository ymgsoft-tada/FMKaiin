using System;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 医会会費マスタ 情報画面。<see cref="FormMasterKaihi_DlgEntry"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。項目と入力欄の受け渡しは <see cref="SControlDB"/>、
    /// 入力ルールは <see cref="AppSControlRule"/>、区分の一覧は <see cref="AppSCombo"/>、iFAX グループの一覧は <see cref="AppSTableCombo"/>、
    /// 銀行コードの選択は S 系の画面（<see cref="FormSelectorBankCode2"/>）を使う。
    /// 銀行名・支店名は、銀行コード・支店コードから引いて表示するだけの欄（入力できない）。月額費用の合計も、表示するだけの欄。
    /// </remarks>
    public partial class FormMasterKaihi2_DlgEntry : FormFrame
    {
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
            Copy
        }

        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// 処理モード
        /// </summary>
        public eMode Mode { get; set; }

        /// <summary>
        /// iFAXグループ情報 DBView
        /// </summary>
        DBView dvIfax;

        /// <summary>
        /// 画面項目とDBとの関連情報
        /// </summary>
        SControlDB sctl;

        /// <summary>
        /// 画面起動時のレコード状態
        /// </summary>
        DataRow srcrow = null;
        /// <summary>
        /// 編集するレコード
        /// </summary>
        DataRow editrow = null;
        /// <summary>
        /// 画面を表示したときの、編集するレコードの内容（閉じるときに、入力があったかを調べる）
        /// </summary>
        RowSnapshot shownrow = null;

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
                srcrow = value; // 元画面から受取った情報セット
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
        /// 月額費用コントロール配列
        /// </summary>
        SNumberBox[] iCosts;

        /// <summary>
        /// 月額費用field
        /// </summary>
        string[] costsField;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterKaihi2_DlgEntry()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);

            Mode = eMode.Add;

            // 月額費用1～12のコントロールとt_kaihiカラム名を配列化
            iCosts = new SNumberBox[]
            {
                iCost1,  iCost2,  iCost3,
                iCost4,  iCost5,  iCost6,
                iCost7,  iCost8,  iCost9,
                iCost10, iCost11, iCost12,
            };

            costsField = new string[]
            {
                t_kaihi.FKaihi_GetsugakuCost1,  t_kaihi.FKaihi_GetsugakuCost2,
                t_kaihi.FKaihi_GetsugakuCost3,  t_kaihi.FKaihi_GetsugakuCost4,
                t_kaihi.FKaihi_GetsugakuCost5,  t_kaihi.FKaihi_GetsugakuCost6,
                t_kaihi.FKaihi_GetsugakuCost7,  t_kaihi.FKaihi_GetsugakuCost8,
                t_kaihi.FKaihi_GetsugakuCost9,  t_kaihi.FKaihi_GetsugakuCost10,
                t_kaihi.FKaihi_GetsugakuCost11, t_kaihi.FKaihi_GetsugakuCost12,
            };
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
            else if (Mode == eMode.Copy)
            {
                title = "：コピー追加";
            }

            this.Text += title;
            base.FormFrame_Load(sender, e);
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            // コンボボックスの値セット
            // enumをセット
            AppSCombo.SetComboBox(iKozaType, enumKbn.DTypeKoza, (int)eTypeKoza.None); // 口座区分
            AppSCombo.SetComboBox(iKbnKaihi, enumKbn.DTypeKaihi, (int)eTypeKaihi.None); // 会費区分
            AppSCombo.SetComboBox(iKaihiZokusei, enumKbn.DKaihiZokusei, (int)eKaihiZokusei.None); // 会費属性

            // iSearch
            dvIfax = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_ifax));
            AppSTableCombo.SetComboBox_IfaxGroup(iBankIfax, dvIfax); // iFAXグループ

            // 手動セット
            iSearchUsed.BeginUpdate(); // 検索時表示区分
            iSearchUsed.Items.Clear();
            iSearchUsed.AddItem("表示する", true);
            iSearchUsed.AddItem("表示しない", false);
            iSearchUsed.EndUpdate();

            // 画面～DBの紐づけ
            sctl = new SControlDB(this, getDataRow);

            sctl.Add(new SControlDBCombo(t_kaihi.FKaihi_KaihiType, iKbnKaihi)); // 会費区分
            sctl.Add(new SControlDBText(t_kaihi.FCD_Kaihi, iCode)); // 会費コード
            sctl.Add(new SControlDBRuby( // 2つ目にカナ入力される
                            new string[] {
                                t_kaihi.FKaihi_Name, // 会費印刷用名称
                                t_kaihi.FKaihi_ShortName}, // 会費略称 ※RuleはRubyにしていない(略称は完全にカナではないようであるため)
                            new STextBox[] {
                                iName,
                                iShortName
                            }
            ));
            sctl.Add(new SControlDBText(t_kaihi.FKaihi_Biko, iBiko)); // 備考
            sctl.Add(new SControlDBNumber(t_kaihi.FKaihi_GunCode, iKaihiGunCode)); // 会費群
            sctl.Add(new SControlDBCombo(t_kaihi.FKaihi_SearchUsed, iSearchUsed)); // 検索時表示区分

            // 月額費用
            for (int i = 0; i < iCosts.Length; i++)
            {
                sctl.Add(new SControlDBNumber(costsField[i], iCosts[i]));
            }

            // 銀行情報
            sctl.Add(new SControlDBText(t_kaihi.FKaihi_BankCode, iBankCD));
            sctl.Add(new SControlDBText(t_kaihi.FKaihi_BankCodeShiten, iShitenCD));
            sctl.Add(new SControlDBCombo(t_kaihi.FKaihi_BankKozaType, iKozaType));
            sctl.Add(new SControlDBText(t_kaihi.FKaihi_BankKozaNo, iKozaNo));
            sctl.Add(new SControlDBRuby( // 2つ目にカナ入力される
                            new string[] {
                                t_kaihi.FKaihi_BankKozaName, // 口座名義
                                t_kaihi.FKaihi_BankKozaNameKana}, // カナ
                            new STextBox[] {
                                iKozaName,
                                iKozaNameKana
                            }
            ));
            sctl.Add(new SControlDBCombo(t_kaihi.FKaihi_BankIfax, iBankIfax));
            sctl.Add(new SControlDBCombo(t_kaihi.FKaihi_Zokusei, iKaihiZokusei));

            sctl.EndAdd(AppSControlRule.Rule);

            rowFetch();

            fetchBank();
            fetchShiten();

            refreshTotal();

            // 入力させないコントロールの非活性
            oCostAll.Enabled = false; // 月額費用-合計
            iBankName.Enabled = false; // 銀行名
            iShitenName.Enabled = false; // 支店名

            //■ イベントの登録
            iBankCD.TextChanged += iBankCD_TextChanged;
            iShitenCD.TextChanged += iShitenCD_TextChanged;
            iBankCD.MouseDoubleClick += cd_MouseDoubleClick;
            iShitenCD.MouseDoubleClick += cd_MouseDoubleClick;

            // 月額費用1～12
            foreach (SNumberBox ctl in iCosts)
            {
                // テキスト変更時に合計更新
                ctl.ValueChanged += iCost_ValueChanged;
            }

            // ここまでに入れた最初の値を控える。閉じるときに、これと比べて、入力があったかを調べる
            shownrow = new RowSnapshot(editrow);

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// 銀行コード・支店コードの欄のダブルクリック: 銀行コードの選択画面を開く
        /// </summary>
        private void cd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showBankCode();
        }

        /// <summary>
        /// 銀行-銀行コード 値変更
        /// </summary>
        private void iBankCD_TextChanged(object sender, EventArgs e)
        {
            fetchBank();
            fetchShiten();
        }

        /// <summary>
        /// 銀行-支店コード 値変更
        /// </summary>
        private void iShitenCD_TextChanged(object sender, EventArgs e)
        {
            fetchShiten();
        }

        /// <summary>
        /// 銀行-銀行名のセット
        /// </summary>
        void fetchBank()
        {
            BankCode bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCD.Text));

            string name = "";
            if (bc != null)
            {
                name = bc.Name;
            }

            iBankName.Text = name;
        }

        /// <summary>
        /// 銀行-支店名のセット
        /// </summary>
        void fetchShiten()
        {
            t_bank_code si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD.Text), Cast.Int(iShitenCD.Text));

            string name = "";
            if (si != null)
            {
                name = si.BCD_NameShiten + "支店";
            }

            iShitenName.Text = name;
        }

        /// <summary>
        /// 月額費用 値変更
        /// </summary>
        private void iCost_ValueChanged(object sender, EventArgs e)
        {
            sctl.UpdateByControl((Control)sender); // 該当コントロール値→row更新
            refreshTotal();
        }

        /// <summary>
        /// 会費合計の更新
        /// </summary>
        void refreshTotal()
        {
            decimal costTotal = 0m;

            foreach (string field in costsField)
            {
                costTotal += Cast.Decimal(Row[field]);
            }

            oCostAll.Value = costTotal;
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
        /// 閉じる処理
        /// </summary>
        protected override void FormFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formCloseReason != App.FormCloseReason.Save)
            {
                // フォーカスがある入力欄の内容を、行へ書き戻してから、変更の有無を調べる
                sctl.Update();

                // 画面を表示してから、何か入力したときだけ、破棄の確認を出す（追加・コピー追加でも、何も入力していなければ出さない）
                if (shownrow != null && shownrow.IsModified(editrow) == true)
                {
                    if (AppMsgBox.Show(AppMsgBoxIndex.CancelClose) == DialogResult.No) // データ破棄メッセージ
                    {
                        e.Cancel = true;
                    }
                }
            }

            base.FormFrame_FormClosing(sender, e);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterKaihi_DlgEntry.Functions);

            funcKeys.On(FuncMasterKaihi_DlgEntry.Save, saveClose);   // 登録
            funcKeys.On(FuncMasterKaihi_DlgEntry.Cancel, closeCancel); // キャンセル
        }

        /// <summary>
        /// 銀行コード選択呼出し
        /// </summary>
        void showBankCode()
        {
            t_kaihi xrow = new t_kaihi(editrow);

            FormSelectorBankCode2 frm = new FormSelectorBankCode2();
            frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCD.Text), Cast.Int(iShitenCD.Text));
            frm.ShowDialog();
            if (frm.FormCloseReason == FormCloseReason.Exec)
            {
                xrow.Kaihi_BankCode = frm.SelectedBankCode.BCD_Code;
                xrow.Kaihi_BankCodeShiten = frm.SelectedBankCode.BCD_CodeShiten;

                rowFetch();
            }
            frm.Dispose();
        }

        /// <summary>
        /// 検証処理
        /// </summary>
        /// <returns></returns>
        bool validateRow()
        {
            // フォーカスがある入力欄の内容を、行へ書き戻す（ファンクション キーではフォーカスが動かないため）
            sctl.Update();

            // 空白チェック
            Control[] ctls =
            {
                iKbnKaihi, iCode, iName, iShortName, iKaihiZokusei
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
            Kaihi obj = AppGlobal.Kaihis.GetCode(iCode.Text);

            if (obj != null && obj.ID != Cast.Int(editrow[t_kaihi.FID_Kaihi]))
            {
                iCode.Select();
                appToolTip.Show(iCode, AppToolTipIndex.BookingCode);
                return false;
            }

            return true;
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
        /// データ取得
        /// </summary>
        DataRow getDataRow()
        {
            return editrow;
        }
    }
}
