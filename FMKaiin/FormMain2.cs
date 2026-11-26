using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ComponentSUI;

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
            OpenSideBySideOnClick(btnKaiin, () => new FormKaiin(), () => new FormKaiin2(), needsBankCode: true);
            OpenSideBySideOnClick(btnMamisImport, () => new FormMamisImport(), () => new FormMamisImport2());

            // 会員登録 ＞ 帳票
            // 開く画面がまだ無いもの: btnRepKaiinJoho（会員情報）、btnRepKaiinList（会員一覧表）、
            //                         btnRepIryoKikanJoho（医療機関情報）、btnRepIryoKikanList（医療機関一覧表）
            OpenSideBySideOnClick(btnRepKaihiUchiwakeMonth, () => new FormOutKaihiUchiwakeMonth(), () => new FormOutKaihiUchiwakeMonth2(), needsBankCode: true);
            OpenSideBySideOnClick(btnRepKaihiUchiwakeYear, () => new FormOutKaihiUchiwakeYear(), () => new FormOutKaihiUchiwakeYear2(), needsBankCode: true);
            OpenSideBySideOnClick(btnRepKaihiList, () => new FormOutKaihiList(), () => new FormOutKaihiList2(), needsBankCode: true);
            OpenSideBySideOnClick(btnRepKaihiYoteiYear, () => new FormOutKaihiYoteiYear(), () => new FormOutKaihiYoteiYear2(), needsBankCode: true);

            // 会員登録 ＞ Excel出力
            OpenSideBySideOnClick(btnXlsKaihiShinsei, () => new FormOutExcelKaihiShinsei(), () => new FormOutExcelKaihiShinsei2());
            OpenSideBySideOnClick(btnXlsShiminByoin, () => new FormOutExcelShimingyoinKaihi(), () => new FormOutExcelShimingyoinKaihi2());
            OpenSideBySideOnClick(btnXlsNenkanChoshu, () => new FormOutExcelNenkanChoshu(), () => new FormOutExcelNenkanChoshu2());

            // 引き落とし業務 ＞ 入力・データ作成
            OpenSideBySideOnClick(btnHikiotoshi, () => new FormHikiotoshi(), () => new FormHikiotoshi2());
            OpenSideBySideOnClick(btnCreateBankFile, () => new FormCreateBankFile(), () => new FormCreateBankFile2(), needsBankCode: true);
            OpenSideBySideOnClick(btnCreateIfaxFile, () => new FormCreateIfaxFile(), () => new FormCreateIfaxFile2());

            // 引き落とし業務 ＞ 帳票
            // 開く画面がまだ無いもの: btnRepHikiMeisai（会員別引落明細表）、btnRepHikiIkaiMeisai（所属医会別引落明細表）、
            //                         btnRepHikiIkaiShukei（所属医会別引落集計表）、btnRepHikiKozaShukei（引落口座別集計表）、
            //                         btnRepAtenaLabel（宛名ラベル）
            OpenSideBySideOnClick(btnRepHikiotoshiKakunin, () => new FormOutHikiotoshiKakunin(), () => new FormOutHikiotoshiKakunin2(), needsBankCode: true);
            OpenSideBySideOnClick(btnRepSeikyuMinyukin, () => new FormOutSeikyuMinyukin(), () => new FormOutSeikyuMinyukin2(), needsBankCode: true);
            OpenSideBySideOnClick(btnRepChosyuMeisaiYear, () => new FormOutChosyuMeisaiYear(), () => new FormOutChosyuMeisaiYear2(), needsBankCode: true);

            // 引き落とし業務 ＞ Excel出力
            OpenSideBySideOnClick(btnXlsFurikomi, () => new FormOutExcelFurikomi(), () => new FormOutExcelFurikomi2());
            OpenSideBySideOnClick(btnXlsFurikomiIrai, () => new FormOutExcelFurikomiIrai(), () => new FormOutExcelFurikomiIrai2());
            OpenSideBySideOnClick(btnXlsNofusho, () => new FormOutExcelNofusho(), () => new FormOutExcelNofusho2());

            // マスター設定
            // 開く画面がまだ無いもの: btnMasterKaihi（会費マスター）
            // 【一時対応】新しい画面（クラス名の末尾が 2 のもの）がある場合は、見比べられるよう、従来の画面と並べて開く。
            // 新しい画面への移行が済んだら、OpenOnClick(ボタン, () => new 新しい画面(), ...) に戻す
            OpenSideBySideOnClick(btnMasterBasic, () => new FormMasterBasic(), () => new FormMasterBasic2(), needsBankCode: true);
            OpenSideBySideOnClick(btnMasterTantosha, () => new FormMasterTantosha(), () => new FormMasterTantosha2(), needsBankCode: true);
            OpenSideBySideOnClick(btnMasterBankCode, () => new FormMasterBankCode(), () => new FormMasterBankCode2());
            OpenSideBySideOnClick(btnMasterShinryoka, () => new FormMasterShinryoka(), () => new FormMasterShinryoka2());
            OpenSideBySideOnClick(btnMasterIryoKikan, () => new FormMasterIryoKikan(), () => new FormMasterIryoKikan2());
            OpenSideBySideOnClick(btnMasterIkaiKaihi, () => new FormMasterKaihi(), () => new FormMasterKaihi2(), needsBankCode: true);
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

        #region 【一時対応】従来の画面と新しい画面を並べて開く

        // 新しい画面（クラス名の末尾が 2 のもの）への移行が済むまでの一時的な処理。
        // 移行が済んだら、この region を丸ごと削除し、RegisterMenuActions の呼び出しを OpenOnClick に戻すこと。

        /// <summary>
        /// <paramref name="button"/> を押したときに、従来の画面と新しい画面の両方を、左右に並べて開くようにする。
        /// 2 つの画面を同時に操作できるよう、<see cref="OpenOnClick"/> と違ってモードレスで開く。
        /// そのため、開いている間もメニューを操作できる。同じボタンをもう一度押した場合は、もう 1 組開かずに、開いている画面を前面へ出す。
        /// 新しい画面は、従来の画面の初回描画が終わってから開く（2 つの画面のデータの読み込みが、同時に走らないように）。
        /// </summary>
        /// <param name="needsBankCode">開く画面が銀行コードを使う場合は true。</param>
        private void OpenSideBySideOnClick(Button button, Func<Form> createLegacyForm, Func<Form> createNewForm, bool needsBankCode = false)
        {
            // このボタンから開いた画面。閉じた画面は破棄済みになるので、IsDisposed で開いているかどうかを判断する
            var openedForms = new List<Form>();

            button.Click += delegate
            {
                openedForms.RemoveAll(form => form.IsDisposed);
                if (openedForms.Count > 0)
                {
                    foreach (Form form in openedForms)
                    {
                        form.Activate();
                    }
                    return;
                }

                if (needsBankCode)
                {
                    EnsureBankCodeLoaded();
                }

                // モードレスで開いた画面は、閉じたときに自動で破棄されるので、using で囲まない。
                // このメニューを所有者にしておくと、メニューの背面に隠れず、メニューを閉じたときに一緒に閉じる
                Form legacyForm = createLegacyForm();
                Form newForm = createNewForm();
                openedForms.Add(legacyForm);

                // 新しい画面は、従来の画面の初回描画（Shown の処理）が終わってから開く。
                // 2 つを続けて開くと、従来の画面が初回描画の中で進行状況の画面（FormBg_Progress）を出して待っている間に、
                // 新しい画面の初回描画が割り込み、2 つの画面のデータの読み込みが、別々のスレッドで同時に走る。
                // データベースの接続は 1 つを共有していて、同時には使えないので、片方の読み込みが失敗する（引落データ作成で、実際に落ちた）。
                // 画面自身の初回描画の処理は、画面のコンストラクターで Shown に登録されているので、ここで足す処理は、その後に呼ばれる
                legacyForm.Shown += delegate
                {
                    if (legacyForm.IsDisposed)
                    {
                        // 初回描画の中で閉じられた: 新しい画面は開かない
                        newForm.Dispose();
                        return;
                    }

                    openedForms.Add(newForm);
                    newForm.Show(this);
                    ArrangeSideBySide(legacyForm, newForm);
                };

                legacyForm.Show(this);
                if (legacyForm.IsDisposed)
                {
                    // 読み込み（Load）の中で閉じられた: Shown は発生しないので、新しい画面をここで破棄する
                    newForm.Dispose();
                    return;
                }

                // 従来の画面を、先に、並べたときの位置へ移しておく（新しい画面を開いたときに、実際の大きさでもう一度並べる）
                ArrangeSideBySide(legacyForm, newForm);
            };
        }

        /// <summary>
        /// 2 つの画面を、このメニューがある画面の作業領域の中央に、左右に並べる。
        /// 各画面が指定している「親の中央に表示」は、モードレスで開く場合には働かないので、ここで位置を決める。
        /// 並べると作業領域に収まらない場合は、右側の画面を作業領域の右端に合わせ、左側の画面に重ねる。
        /// </summary>
        private void ArrangeSideBySide(Form left, Form right)
        {
            Rectangle area = Screen.FromControl(this).WorkingArea;

            int leftX = area.Left + Math.Max(0, (area.Width - left.Width - right.Width) / 2);
            int rightX = Math.Max(area.Left, Math.Min(leftX + left.Width, area.Right - right.Width));

            left.Location = new Point(leftX, area.Top + Math.Max(0, (area.Height - left.Height) / 2));
            right.Location = new Point(rightX, area.Top + Math.Max(0, (area.Height - right.Height) / 2));
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
