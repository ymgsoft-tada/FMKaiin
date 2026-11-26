using System;
using System.Data;
using System.Windows.Forms;
using App.Controls;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 銀行情報の入力画面。<see cref="FormMasterBank_DlgEntry"/> と同じ内容を、S 系のコントロール（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。項目と入力欄の受け渡しは <see cref="SControlDB"/>、
    /// 入力ルールは <see cref="AppSControlRule"/>、口座区分の一覧は <see cref="AppSCombo"/>、
    /// 銀行コードの選択は S 系の画面（<see cref="FormSelectorBankCode2"/>）を使う。
    /// 銀行名・支店名とそのカナは、銀行コード・支店コードから引いて表示するだけの欄（入力できない）。
    /// </remarks>
    public partial class FormMasterBasic2_Bank_DlgEntry : FormFrame
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
                    editrow = srcrow.Table.NewRow();
                    AppDb.CopyDataRow(srcrow, editrow);
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterBasic2_Bank_DlgEntry()
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
            //　コンボボックスの作成
            AppSCombo.SetComboBox(iKozaType, enumKbn.DTypeKoza, (int)eTypeKoza.None);

            // 銀行名・支店名とそのカナは、表示するだけ
            iNameBank.Enabled = false;
            iNameKanaBank.Enabled = false;
            iNameShiten.Enabled = false;
            iNameKanaShiten.Enabled = false;

            sctl = new SControlDB(this, getDataRow);

            sctl.Add(new SControlDBText(t_bank.FBNK_Code, iBankCode));
            sctl.Add(new SControlDBText(t_bank.FBNK_CodeShiten, iShitenCode));
            sctl.Add(new SControlDBCombo(t_bank.FBNK_KozaType, iKozaType));
            sctl.Add(new SControlDBText(t_bank.FBNK_KozaNo, iKozaNo));
            sctl.Add(new SControlDBText(t_bank.FBNK_CodeCompany, iCodeCompany));

            sctl.EndAdd(AppSControlRule.Rule);

            rowFetch();

            iBankCode.Select();

            //■ イベントの登録
            iBankCode.TextChanged += iBankCode_TextChanged;
            iShitenCode.TextChanged += iShitenCode_TextChanged;
            iBankCode.MouseDoubleClick += cd_MouseDoubleClick;
            iShitenCode.MouseDoubleClick += cd_MouseDoubleClick;

            // ここまでに入れた最初の値を控える。閉じるときに、これと比べて、入力があったかを調べる
            shownrow = new RowSnapshot(editrow);

            base.FormFrame_Shown(sender, e);
        }

        private void cd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showBankCode();
        }

        private void iBankCode_TextChanged(object sender, EventArgs e)
        {
            fetchBank();
            fetchShiten();
        }

        private void iShitenCode_TextChanged(object sender, EventArgs e)
        {
            fetchShiten();
        }

        /// <summary>
        /// 銀行コードから、銀行名とそのカナを表示する
        /// </summary>
        void fetchBank()
        {
            BankCode bc = AppGlobal.BankCodeMg.GetBankCode(Cast.Int(iBankCode.Text));

            string name = "";
            string kana = "";
            if (bc != null)
            {
                name = bc.Name;
                kana = bc.Furigana;
            }

            iNameBank.Text = name;
            iNameKanaBank.Text = kana;
        }

        /// <summary>
        /// 銀行コード・支店コードから、支店名とそのカナを表示する
        /// </summary>
        void fetchShiten()
        {
            t_bank_code si = AppGlobal.BankCodeMg.GetBankCodeRow(Cast.Int(iBankCode.Text), Cast.Int(iShitenCode.Text));

            string name = "";
            string kana = "";
            if (si != null)
            {
                name = si.BCD_NameShiten;
                kana = si.BCD_NameShitenFurigana;
            }

            iNameShiten.Text = name;
            iNameKanaShiten.Text = kana;
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

            fetchBank();
            fetchShiten();
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
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterBank_DlgEntry.Functions);

            funcKeys.On(FuncMasterBank_DlgEntry.ShowBankCode, showBankCode);
            funcKeys.On(FuncMasterBank_DlgEntry.Save, saveClose);
            funcKeys.On(FuncMasterBank_DlgEntry.Cancel, closeCancel);
        }

        /// <summary>
        /// 銀行コードの選択の画面を開いて、選んだ銀行コード・支店コードを入れる
        /// </summary>
        void showBankCode()
        {
            t_bank xrow = new t_bank(editrow);

            FormSelectorBankCode2 frm = new FormSelectorBankCode2();
            frm.SelectedBankCode = AppGlobal.BankCodeMg.GetBankCodeRow(xrow.BNK_Code, xrow.BNK_CodeShiten);
            frm.ShowDialog();
            if (frm.FormCloseReason == FormCloseReason.Exec)
            {
                xrow.BNK_Code = frm.SelectedBankCode.BCD_Code;
                xrow.BNK_CodeShiten = frm.SelectedBankCode.BCD_CodeShiten;

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

            // 空白チェック（銀行名・支店名が空なのは、銀行コード・支店コードに当たる銀行が無いとき）
            Control[] ctls =
            {
                iBankCode,  iShitenCode, iNameBank, iNameShiten,
                iKozaNo,    iCodeCompany,
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
