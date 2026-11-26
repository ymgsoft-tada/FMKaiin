using System;
using System.Data;
using System.Windows.Forms;
using ComponentSUI;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 担当者マスタ編集画面。<see cref="FormMasterTantosha_DlgEntry"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。項目と入力欄の受け渡しは <see cref="SControlDB"/>、
    /// 入力ルールは <see cref="AppSControlRule"/>、権限の一覧は <see cref="AppSCombo"/> を使う。
    /// FormMasterTantosha_DlgEntry にある「診療所」の欄は、表示も保存もしていなかった（非表示にしてあった）ので、この画面には置いていない。
    /// </remarks>
    public partial class FormMasterTantosha2_DlgEntry : FormFrame
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
        public FormMasterTantosha2_DlgEntry()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);

            Mode = eMode.Add;
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
            // コンボの作成
            AppSCombo.SetComboBox(iAuth, enumKbn.DAuth, (int)eAuth.None, (int)eAuth.SU);

            sctl = new SControlDB(this, getDataRow);
            sctl.Add(new SControlDBText(t_tantosha.FCD_Tanto, iCode));
            sctl.Add(new SControlDBText(t_tantosha.FTNT_Name, iName));
            sctl.Add(new SControlDBText(t_tantosha.FTNT_Password, iPwd));
            sctl.Add(new SControlDBCombo(t_tantosha.FTNT_Auth, iAuth));
            sctl.EndAdd(AppSControlRule.Rule);

            rowFetch();

            iCode.Select();

            //■ イベントの登録
            iAuth.SelectedValueChanged += iAuth_SelectedValueChanged;

            // ここまでに入れた最初の値を控える。閉じるときに、これと比べて、入力があったかを調べる
            shownrow = new RowSnapshot(editrow);

            base.FormFrame_Shown(sender, e);
        }

        private void iAuth_SelectedValueChanged(object sender, EventArgs e)
        {
            sctl.UpdateByControl(iAuth);
        }

        /// <summary>
        /// 行情報の取得
        /// </summary>
        void rowFetch()
        {
            if (Row != null)
            {
                sctl.FetchAll();

                if (Mode == eMode.Edit)
                {
                    iPwd.Enabled = false;
                }
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
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterTanto_DlgEntry.Functions);

            if (Mode == eMode.Add)
            {
                funcKeys.SetVisible(FuncMasterTanto_DlgEntry.ChangePWD.Key, false);
            }

            funcKeys.On(FuncMasterTanto_DlgEntry.ChangePWD, changePWD);
            funcKeys.On(FuncMasterTanto_DlgEntry.Save, saveClose);
            funcKeys.On(FuncMasterTanto_DlgEntry.Cancel, closeCancel);
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
                iCode, iName, iPwd,
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
            Tanto obj = AppGlobal.Tantos.GetCode(iCode.Text);

            if (obj != null && obj.ID != Cast.Int(editrow[t_tantosha.FID_Tanto]))
            {
                iCode.Select();
                appToolTip.Show(iCode, AppToolTipIndex.BookingCode);
                return false;
            }

            // 管理者以外はマイナンバー管理者にはなれない。
            t_tantosha xrow = new t_tantosha(editrow);

            if (xrow.TNT_Auth != eAuth.Admin && xrow.TNT_AvailableMyNo == true)
            {
                xrow.TNT_AvailableMyNo = false;
            }

            return true;
        }

        void changePWD()
        {
            t_tantosha xrow = new t_tantosha(editrow);

            FormMasterTantosha2_DlgPwd frm = new FormMasterTantosha2_DlgPwd();
            frm.TantoRow = xrow;
            frm.ShowDialog();
            if (frm.FormCloseReason == FormCloseReason.Save)
            {
                xrow.TNT_Password = frm.NewPassword;

                sctl.FetchByField(t_tantosha.FTNT_Password);
            }
            frm.Dispose();
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
