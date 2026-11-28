using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using App.Controls;

namespace App
{
    /// <summary>
    /// アプリのトップメニュー（白を基調にした、透明感のある明るいデザイン）。
    /// 左のメニューで業務の区分（会員登録・引き落とし業務・マスター設定）を選ぶと、右側のボタン一覧がその区分のものに入れ替わる。
    /// 右側には区分ごとのパネル（ページ）を重ねて置いてあり、選んだ区分のパネルだけを表示する。
    /// 右側のボタンを押すと、対応する画面をモーダルで開く。ボタンと開く画面の対応は <see cref="RegisterMenuActions"/> にまとめてある。
    /// 画面の構成は FormMain2.Designer.cs にあり、ボタンやセクションの見た目は Controls 配下のコントロールが描画する。
    /// 背景画像を画面いっぱいに描く処理は、継承元の <see cref="GlassBackdropForm"/> にある。
    /// 仕様は _doc/spec/コントロール・フォーム仕様.md にある。
    /// </summary>
    public partial class FormMain2 : GlassBackdropForm
    {
        /// <summary>画面で指定しているフォント（BIZ UDPゴシック）が PC に入っていない場合に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>タイトルバーの背景色。濃い紺色。</summary>
        private static readonly Color TitleBarBackColor = Color.FromArgb(32, 64, 99);

        /// <summary>タイトルバーの文字色。</summary>
        private static readonly Color TitleBarForeColor = Color.White;

        /// <summary>左メニューの項目と、その項目を選んだときに右側へ表示するパネルの組。</summary>
        private sealed class MenuPage
        {
            public MenuPage(SideMenuButton navButton, Control panel)
            {
                NavButton = navButton;
                Panel = panel;
            }

            /// <summary>左メニューのボタン。ボタンの文字列は、右側の見出しにもそのまま使う。</summary>
            public SideMenuButton NavButton { get; private set; }

            /// <summary>右側に表示するパネル。</summary>
            public Control Panel { get; private set; }
        }

        private readonly MenuPage[] _pages;
        private MenuPage _currentPage;

        public FormMain2()
        {
            InitializeComponent();
            ApplyFontFallback();

            lblLoginUser.Text = "ログイン：" + (AppGlobal.LoginUser != null ? AppGlobal.LoginUser.Name : string.Empty);
            lblVersion.Text = "Version" + AppConst.AppVersion;

            _pages = new[]
            {
                new MenuPage(navKaiin, pnlPageKaiin),
                new MenuPage(navHikiotoshi, pnlPageHikiotoshi),
                new MenuPage(navMaster, pnlPageMaster)
            };
            foreach (MenuPage page in _pages)
            {
                MenuPage target = page;
                page.NavButton.Click += delegate { SelectPage(target); };
            }
            SelectPage(_pages[0]);

            RegisterToolbarActions();
            RegisterMenuActions();

            // 起動直後に Enter キーで先頭のメニューを実行できるよう、最初のページの先頭のボタンにフォーカスを置く
            ActiveControl = btnKaiin;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // タイトルバーの色を指定できるのは Windows 11 以降。それより前では標準の色のままになる
            TitleBarColor.Apply(this, TitleBarBackColor, TitleBarForeColor);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // デザイン時の大きさより小さくするとボタンの文字が収まらなくなるため、縮小できないようにする
            MinimumSize = Size;
        }

        /// <summary>
        /// 画面で指定しているフォントが入っていない PC では、
        /// そのフォントを使っている箇所を <see cref="FallbackFontFamily"/> に差し替える。
        /// フォントが入っている PC では何も変わらない。
        /// </summary>
        private void ApplyFontFallback()
        {
            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
        }

        #region ページの切り替え

        /// <summary>
        /// 右側の表示を <paramref name="page"/> に切り替え、左メニューの選択状態と見出しをそれに合わせる。
        /// </summary>
        private void SelectPage(MenuPage page)
        {
            if (page == _currentPage)
            {
                return;
            }

            // 新しいページを表示してから、ほかのページを隠す。
            // 逆の順にすると、どのページも表示されていない状態が一瞬見えてしまう
            pnlContent.SuspendLayout();
            page.Panel.Visible = true;
            foreach (MenuPage other in _pages)
            {
                other.NavButton.Selected = other == page;
                if (other != page)
                {
                    other.Panel.Visible = false;
                }
            }
            pnlContent.ResumeLayout();

            lblHeading.Text = page.NavButton.Text;
            _currentPage = page;
        }

        #endregion

        #region メニューのボタンと開く画面の対応

        /// <summary>
        /// 右側の各ボタンに、押したときに開く画面を割り当てる。
        /// needsBankCode を true にした画面は、開く前に銀行コードを読み込む（<see cref="EnsureBankCodeLoaded"/>）。
        /// ここで割り当てていないボタンは、開く画面がまだ無いもの。表示はするが、押しても何も起きない。
        /// </summary>
        private void RegisterMenuActions()
        {
            // 会員登録 ＞ 入力・データ作成
            OpenOnClick(btnKaiin, () => new FormKaiin(), needsBankCode: true);
            OpenOnClick(btnMamisImport, () => new FormMamisImport());

            // 会員登録 ＞ 帳票
            // 開く画面がまだ無いもの: btnRepKaiinJoho（会員情報）、btnRepKaiinList（会員一覧表）、
            //                         btnRepIryoKikanJoho（医療機関情報）、btnRepIryoKikanList（医療機関一覧表）
            OpenOnClick(btnRepKaihiUchiwakeMonth, () => new FormOutKaihiUchiwakeMonth(), needsBankCode: true);
            OpenOnClick(btnRepKaihiUchiwakeYear, () => new FormOutKaihiUchiwakeYear(), needsBankCode: true);
            OpenOnClick(btnRepKaihiList, () => new FormOutKaihiList(), needsBankCode: true);
            OpenOnClick(btnRepKaihiYoteiYear, () => new FormOutKaihiYoteiYear(), needsBankCode: true);

            // 会員登録 ＞ Excel出力
            OpenOnClick(btnXlsKaihiShinsei, () => new FormOutExcelKaihiShinsei());
            OpenOnClick(btnXlsShiminByoin, () => new FormOutExcelShimingyoinKaihi());
            OpenOnClick(btnXlsNenkanChoshu, () => new FormOutExcelNenkanChoshu());

            // 引き落とし業務 ＞ 入力・データ作成
            OpenOnClick(btnHikiotoshi, () => new FormHikiotoshi());
            OpenOnClick(btnCreateBankFile, () => new FormCreateBankFile(), needsBankCode: true);
            OpenOnClick(btnCreateIfaxFile, () => new FormCreateIfaxFile());

			// 引き落とし業務 ＞ 帳票
			// 開く画面がまだ無いもの: btnRepHikiMeisai（会員別引落明細表）、btnRepHikiIkaiMeisai（所属医会別引落明細表）、
			//                         btnRepHikiIkaiShukei（所属医会別引落集計表）、btnRepHikiKozaShukei（引落口座別集計表）
			OpenOnClick(btnRepHikiIkaiMeisai, () => new FormOutHikiotoshiIkaiMeisai(), needsBankCode: false);
			OpenOnClick(btnRepAtenaLabel, () => new FormOutAtenaLabel()); //¶ 2026/11/28 CC 宛名ラベル
			OpenOnClick(btnRepHikiIkaiShukei, () => new FormOutHikiotoshiIkaiShukei(), needsBankCode: true);
			OpenOnClick(btnRepHikiotoshiKakunin, () => new FormOutHikiotoshiKakunin(), needsBankCode: true);
            OpenOnClick(btnRepSeikyuMinyukin, () => new FormOutSeikyuMinyukin(), needsBankCode: true);
            OpenOnClick(btnRepChosyuMeisaiYear, () => new FormOutChosyuMeisaiYear(), needsBankCode: true);

            // 引き落とし業務 ＞ Excel出力
            OpenOnClick(btnXlsFurikomi, () => new FormOutExcelFurikomi());
            OpenOnClick(btnXlsFurikomiIrai, () => new FormOutExcelFurikomiIrai());
            OpenOnClick(btnXlsNofusho, () => new FormOutExcelNofusho());

            // マスター設定
            // 開く画面がまだ無いもの: btnMasterKaihi（会費マスター）
            OpenOnClick(btnMasterBasic, () => new FormMasterBasic(), needsBankCode: true);
            OpenOnClick(btnMasterTantosha, () => new FormMasterTantosha());
            OpenOnClick(btnMasterBankCode, () => new FormMasterBankCode());
            OpenOnClick(btnMasterShinryoka, () => new FormMasterShinryoka());
            OpenOnClick(btnMasterIryoKikan, () => new FormMasterIryoKikan());
            OpenOnClick(btnMasterIkaiKaihi, () => new FormMasterKaihi(), needsBankCode: true);
        }

        /// <summary>
        /// <paramref name="button"/> を押したときに、<paramref name="createForm"/> が作る画面をモーダルで開くようにする。
        /// </summary>
        /// <param name="needsBankCode">開く画面が銀行コードを使う場合は true。</param>
        private void OpenOnClick(Button button, Func<Form> createForm, bool needsBankCode = false)
        {
            button.Click += delegate
            {
                if (needsBankCode)
                {
                    EnsureBankCodeLoaded();
                }

                using (Form form = createForm())
                {
                    form.ShowDialog();
                }
            };
        }

        /// <summary>
        /// 銀行コードをまだ読み込んでいなければ、進捗ダイアログを表示して読み込む。
        /// 銀行コードは件数が多いので、起動時ではなく、銀行コードを使う画面を最初に開くときに読み込む。
        /// </summary>
        private void EnsureBankCodeLoaded()
        {
            if (AppGlobal.BankCodeMg != null)
            {
                return;
            }

            using (var progress = new FormBg_Progress())
            {
                progress.TitleText = "しばらくお待ちください。";
                progress.LabelText = "データをロードしています。";
                progress.DoWorkEvent += LoadBankCode;
                progress.ShowDialog();
            }
        }

        private void LoadBankCode(object sender, DoWorkEventArgs e)
        {
            var progress = (FormBg_Progress)sender;
            progress.AdvanceProgress(100);

            AppGlobal.DB.GetReFillTable(TableProp.t_bank_code);
            AppGlobal.InitBankCodeMg();
        }

        #endregion

        #region ツールバー

        private void RegisterToolbarActions()
        {
            btnDataSave.Click += delegate { BackupData(); };
            btnDataRestore.Click += delegate { RestoreData(); };
            btnRemoteSupport.Click += delegate { StartRemoteSupport(); };

            // ログアウトは、この画面を閉じてアプリを終了する
            btnLogout.Click += delegate { Close(); };
        }

        /// <summary>
        /// データ保存。保存先を選んでもらい、データベースをバックアップ ファイルへ書き出す。
        /// 成功したら、次回の保存・復旧で同じフォルダを最初に開けるよう、保存先のフォルダを設定に覚えておく。
        /// </summary>
        private void BackupData()
        {
            string fileName = string.Format("会員管理データ_{0}.{1}", DateTime.Now.ToString("yyMMdd_HHmm"), AppConst.BackUpFileExt);
            string backupDir = Properties.Settings.Default.BackUpDir;
            if (!string.IsNullOrEmpty(backupDir))
            {
                fileName = Path.Combine(backupDir, fileName);
            }

            string path = AppDb.ShowSaveFilePath(fileName);
            if (path == null)
            {
                return;
            }

            bool succeeded;
            Cursor = Cursors.WaitCursor;
            try
            {
                succeeded = AppGlobal.DB.BackupToZip(path);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (succeeded)
            {
                Properties.Settings.Default.BackUpDir = Path.GetDirectoryName(path);
                Properties.Settings.Default.Save();
            }
            AppMsgBox.Show(succeeded ? AppMsgBoxIndex.BackupSuccess : AppMsgBoxIndex.BackupFailed);
        }

        /// <summary>
        /// データ復旧。バックアップ ファイルを選んでもらい、データベースをその内容に戻す。
        /// 成功したら、戻したデータを読み直すためにアプリを再起動する。
        /// </summary>
        private void RestoreData()
        {
            string path = AppDb.ShowOpenFilePath(Properties.Settings.Default.BackUpDir);
            if (path == null)
            {
                return;
            }

            bool succeeded;
            Cursor = Cursors.WaitCursor;
            try
            {
                succeeded = AppGlobal.DB.RestoreFromZip(path);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            AppMsgBox.Show(succeeded ? AppMsgBoxIndex.RestoreSuccess : AppMsgBoxIndex.RestoreFailed);
            if (succeeded)
            {
                Application.Restart();
            }
        }

        /// <summary>
        /// 遠隔サポート用のプログラムを起動する。
        /// </summary>
        private void StartRemoteSupport()
        {
            try
            {
                Process.Start(AppConst.RemoteSupportFilePath);
            }
            catch (Win32Exception)
            {
                // 起動時の確認画面で利用者がキャンセルした場合と、プログラムが見つからない場合はここに来る。何もしない
            }
            catch (FileNotFoundException)
            {
                // プログラムが見つからない場合。何もしない
            }
        }

        #endregion
    }
}
