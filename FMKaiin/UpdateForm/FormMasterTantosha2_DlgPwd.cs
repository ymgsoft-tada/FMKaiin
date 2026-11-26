using System;
using System.Windows.Forms;
using ComponentSUI;

namespace App
{
    /// <summary>
    /// 担当者マスタ（パスワード変更）。<see cref="FormMasterTantosha_DlgPwd"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系のコントロール・コードは使っていない。入力ルールは <see cref="AppSControlRule"/> を使う。
    /// </remarks>
    public partial class FormMasterTantosha2_DlgPwd : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// 画面間連携用レコード
        /// </summary>
        public t_tantosha TantoRow
        {
            get; set;
        }

        /// <summary>
        /// 新しいパスワード
        /// </summary>
        public string NewPassword
        {
            get
            {
                return iPwd_New.Text;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMasterTantosha2_DlgPwd()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
        }

        /// <summary>
        /// 初回描画処理
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            AppSControlRule.SetControlByRule(iPwd_Old, t_tantosha.FTNT_Password);
            AppSControlRule.SetControlByRule(iPwd_New, t_tantosha.FTNT_Password);
            AppSControlRule.SetControlByRule(iPwd_New2, t_tantosha.FTNT_Password);

            iPwd_Old.Select();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMasterTanto_DlgPwd.Functions);

            funcKeys.On(FuncMasterTanto_DlgPwd.Save, saveClose);
            funcKeys.On(FuncMasterTanto_DlgPwd.Cancel, closeCancel);
        }

        /// <summary>
        /// 検証処理
        /// </summary>
        /// <returns></returns>
        bool validateRow()
        {
            // 空白チェック
            STextBox[] ctls =
            {
                iPwd_Old, iPwd_New, iPwd_New2,
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

            if (iPwd_Old.Text != TantoRow.TNT_Password)
            {
                iPwd_Old.Select();
                appToolTip.Show(iPwd_Old, AppToolTipIndex.UnMatchOldPassword);
                return false;
            }

            if (iPwd_New.Text != iPwd_New2.Text)
            {
                iPwd_New2.Select();
                appToolTip.Show(iPwd_New2, AppToolTipIndex.UnMatchPassword2);
                return false;
            }

            if (AppMsgBox.Show(this, AppMsgBoxIndex.ConfirmChangePassword) == DialogResult.No)
            {
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
    }
}
