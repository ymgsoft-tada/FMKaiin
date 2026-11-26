using System;
using System.IO;
using ComponentSUI;
using ComponentIO;

namespace App
{
    /// <summary>
    /// MAMISデータ取込（取込CSVの指定）の画面。<see cref="FormMamisImport"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。取込 CSV は、ファイルを選ぶ入力欄（<see cref="SFileSelect"/>）で指定する。
    /// 読込・照合・取込の処理は、元の画面と同じもの（<see cref="AppMamisImport"/>）を使う。
    /// 取込確認と、取込結果・取込履歴は、S 系の画面（<see cref="FormMamisImport2_DlgConfirm"/>・<see cref="FormMamisImport2_DlgLog"/>）を開く。
    /// </remarks>
    public partial class FormMamisImport2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMamisImport2()
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
            iFile.Select();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの登録
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncMamisImport.Functions);

            funcKeys.On(FuncMamisImport.History, showHistory);
            funcKeys.On(FuncMamisImport.Exec, doExec);
            funcKeys.On(FuncMamisImport.Close, formClose);
        }

        /// <summary>
        /// 取込開始
        /// </summary>
        void doExec()
        {
            string path = Cast.String(iFile.Text).Trim();

            if (path == "")
            {
                iFile.Select();
                appToolTip.Show(iFile, AppToolTipIndex.CannotUseBlank);
                return;
            }
            if (File.Exists(path) == false)
            {
                AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
                return;
            }

            // 読込と会員との照合。この時点ではデータベースを更新しない
            AppMamisImport imp = new AppMamisImport();

            switch (imp.Load(path))
            {
                case AppMamisImport.eResult.Success:
                    break;
                case AppMamisImport.eResult.NoData:
                    AppMsgBox.Show(this, AppMsgBoxIndex.NotFoundMamisImportData);
                    return;
                case AppMamisImport.eResult.NotMamisFile:
                    AppMsgBox.Show(this, AppMsgBoxIndex.NotMamisImportFile);
                    return;
                case AppMamisImport.eResult.OpenFileFailed:
                    AppMsgBox.Show(this, AppMsgBoxIndex.AnythingMessage, "対象外のファイルか、他で使用中の為ファイルをオープンできません。");
                    return;
                default:
                    AppMsgBox.Show(this, AppMsgBoxIndex.FailedMamisImport);
                    return;
            }

            // 取込確認
            FormMamisImport2_DlgConfirm frm = new FormMamisImport2_DlgConfirm();
            frm.Import = imp;
            frm.ShowDialog();
            bool exec = (frm.FormCloseReason == FormCloseReason.Exec);
            frm.Dispose();
            frm = null;

            if (exec == false)
            {
                return;
            }

            // 取込処理
            if (imp.Update() != AppMamisImport.eResult.Success)
            {
                AppMsgBox.Show(this, AppMsgBoxIndex.FailedMamisImport);
                return;
            }

            // 取込結果
            showLog(imp.LogID);
        }

        /// <summary>
        /// 履歴
        /// </summary>
        void showHistory()
        {
            showLog(null);
        }

        /// <summary>
        /// 取込結果・取込履歴を表示します。
        /// </summary>
        /// <param name="logid">取込履歴ID。nullの場合は全ての履歴</param>
        void showLog(int? logid)
        {
            FormMamisImport2_DlgLog frm = new FormMamisImport2_DlgLog();
            frm.LogID = logid;
            frm.ShowDialog();
            frm.Dispose();
            frm = null;
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        void formClose()
        {
            this.Close();
        }
    }
}
