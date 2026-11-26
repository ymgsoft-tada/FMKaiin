using System;
using System.Windows.Forms;
using App.Controls;
using ComponentDB;

namespace App
{
    /// <summary>
    /// ログイン画面。<see cref="FormLogin"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。担当者コードの入力ルールは <see cref="AppSControlRule"/> を使う。
    /// 上の団体のロゴと名前は、メイン画面（<see cref="FormMain2"/>）の見出しと同じ部品・同じ色。
    /// ファンクション キーのバーは無い（操作は、ログイン ボタンだけ）。
    /// </remarks>
    public partial class FormLogin2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        DBView dvTnt;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormLogin2()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);

            this.Text = AppConst.AppTitle + " ＜ログイン＞";
        }

        /// <summary>
        /// 初回描画
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            // 前回ログインした担当者のコードを入れておく（入っていれば、パスワードから入力を始める）
            iCode.Text = Properties.Settings.Default.LastTantosha;

            if (iCode.TextLength == 0)
            {
                iCode.Select();
            }
            else
            {
                iPwd.Select();
            }

            AppSControlRule.SetControlByRule(iCode, t_tantosha.FCD_Tanto);

            btnLogin.Click += btnLogin_Click;

            // 自分自身にフォーカスさせる
            this.Activate();

#if DEBUG
            // 開発中の動作確認用: 担当者コードとパスワードを入れておく（Debug ビルドだけ。Release ビルドには入らない）
            iCode.Text = "0";
            iPwd.Text = "adm";
#endif

            base.FormFrame_Shown(sender, e);
        }

        void btnLogin_Click(object sender, EventArgs e)
        {
            if (iCode.TextLength == 0)
            {
                appToolTip.Show(iCode, AppToolTipIndex.CannotUseBlank);
                iCode.Select();
            }
            else
            if (iPwd.TextLength == 0)
            {
                appToolTip.Show(iPwd, AppToolTipIndex.CannotUseBlank);
                iPwd.Select();
            }
            else
            {
                // ログイン時に情報を担当者マスタ情報は取得する
                this.Cursor = Cursors.WaitCursor;

                dvTnt = new DBView(AppGlobal.DB.GetReFillTable(TableProp.t_tantosha));
                dvTnt.SortQuery(t_tantosha.FCD_Tanto);

                this.Cursor = Cursors.Default;

                if (dvTnt.FindRow(iCode.Text) == true)
                {
                    t_tantosha xrow = new t_tantosha(dvTnt.CurrentRow);

                    if (xrow.TNT_Password == iPwd.Text)
                    {
                        Properties.Settings.Default.LastTantosha = iCode.Text;
                        Properties.Settings.Default.Save();

                        AppGlobal.SetLoginUser(xrow);

                        formCloseReason = FormCloseReason.Exec;
                        this.Close();
                    }
                    else
                    {
                        appToolTip.Show(iPwd, AppToolTipIndex.UnMatchPassword);
                        iPwd.Select();
                        iPwd.SelectAll();
                    }
                }
                else
                {
                    appToolTip.Show(iCode, AppToolTipIndex.NotFoundTantosha);
                    iCode.Select();
                    iCode.SelectAll();
                }
            }
        }
    }
}
