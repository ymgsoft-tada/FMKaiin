using System;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 銀行コードマスタ登録画面。<see cref="FormMasterBankCode_DlgEntry"/> と同じ内容を、S 系のコントロール（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。項目と入力欄の受け渡しは <see cref="SControlDB"/>、
    /// 入力ルールは <see cref="AppSControlRule"/> を使う。
    /// 銀行名・支店名を入力すると、その読みを、それぞれのカナの欄へ追記する（<see cref="SControlDBRuby"/>）。
    /// </remarks>
    public partial class FormMasterBankCode2_DlgEntry : FormFrame
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

        SControlDB sctl;

        /// <summary>
        /// 起動時のレコード状態
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
                srcrow = value;
                if (this.Mode == eMode.Add)
                {
                    editrow = srcrow;
                }
                else
                {
                    // ID_Auto（行を見分けるキー）も含めて、すべての項目を写す
                    editrow = srcrow.Table.NewRow();
                    editrow.ItemArray = srcrow.ItemArray;
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterBankCode2_DlgEntry()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);

            Mode = eMode.Add;
        }

        protected override void FormFrame_Load(object sender, EventArgs e)
        {
            // タイトルは「メイン名：サブ名」
            string title = "：追加";

            if (Mode == eMode.Edit)
            {
                title = "：訂正";
            }
            else
            if (Mode == eMode.Copy)
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
            sctl = new SControlDB(this, getDataRow);

            sctl.Add(new SControlDBText(t_bank_code.FBCD_Code, iBankCode));
            sctl.Add(new SControlDBText(t_bank_code.FBCD_CodeShiten, iShitenCode));

            sctl.Add(new SControlDBRuby(
                            new string[]{
                                t_bank_code.FBCD_Name,
                                t_bank_code.FBCD_NameFurigana,
                            },
                            new STextBox[]{
                                iNameBank,
                                iNameKanaBank,
                            }));

            sctl.Add(new SControlDBRuby(
                            new string[]{
                                t_bank_code.FBCD_NameShiten,
                                t_bank_code.FBCD_NameShitenFurigana,
                            },
                            new STextBox[]{
                                iNameShiten,
                                iNameKanaShiten,
                            }));
            sctl.Add(new SControlDBText(t_bank_code.FBCD_FullName, iFullNameBank));

            sctl.EndAdd(AppSControlRule.Rule);

            rowFetch();

            funckey.Select();
            iBankCode.Select();

            // ここまでに入れた最初の値を控える。閉じるときに、これと比べて、入力があったかを調べる
            shownrow = new RowSnapshot(editrow);

            base.FormFrame_Shown(sender, e);
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
                    if (AppMsgBox.Show(AppMsgBoxIndex.CancelClose) == System.Windows.Forms.DialogResult.No)
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
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterBankCode_DlgEntry.Functions);

            funcKeys.On(FuncMasterBankCode_DlgEntry.Save, saveClose);
            funcKeys.On(FuncMasterBankCode_DlgEntry.Cancel, closeCancel);
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
            STextBox[] ctls =
            {
                iBankCode,  iShitenCode, iNameBank, iNameShiten, iNameKanaBank, iNameKanaShiten, iFullNameBank
            };

            foreach (STextBox ctl in ctls)
            {
                if (ctl.TextLength == 0)
                {
                    ctl.Select();
                    appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank);
                    return false;
                }
            }

            // 重複チェック
            BankCode obj = AppGlobal.BankCodeMg.GetBankCode(iBankCode.Text);

            if (obj != null)
            {
                t_bank_code xrow = obj.GetShiten(iShitenCode.Text);

                if (xrow != null && xrow.ID_Auto != Cast.Int(editrow[t_bank_code.FID_Auto]))
                {
                    iShitenCode.Select();
                    appToolTip.Show(iShitenCode, AppToolTipIndex.BookingCode);
                    return false;
                }
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
