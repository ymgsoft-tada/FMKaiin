using System;
using App.Controls;
using ComponentIO;

namespace App
{
    /// <summary>
    /// FB（銀行用引落）データ作成の画面。<see cref="FormCreateBankFile"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。月度・引落日は <see cref="SDateBox"/>、引落元銀行は <see cref="STableComboBox"/> を使う。
    /// 作成の処理は、元の画面と同じもの（<see cref="AppBankFile"/>）を使う。結果は、S 系の結果の画面（<see cref="FormLogView2"/>）で表示する。
    /// </remarks>
    public partial class FormCreateBankFile2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormCreateBankFile2()
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
            AppSTableCombo.SetComboBox_Bank(iBank);

            DateTime date = AppDate.ParseDateFirstDay(DateTime.Now);
            iDateYM.Value = date;
            iBank.SelectedValue = AppGlobal.Basic.ID_Bank;
            // 引落日の初期値は、今月の25日
            iDate.Value = new DateTime(date.Year, date.Month, 25);

            AppSControlRule.SetControlByRule(iDate, t_basic.FBAS_DateYM);

            iDate.Select();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの登録
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncCreateBankFile.Functions);

            funcKeys.On(FuncCreateBankFile.Exec, doExec);
            funcKeys.On(FuncCreateBankFile.Close, formClose);
        }

        /// <summary>
        /// 作成実行
        /// </summary>
        void doExec()
        {
            // 空白チェック
            if (iBank.Text == "")
            {
                iBank.Select();
                appToolTip.Show(iBank, AppToolTipIndex.CannotUseBlank);
                return;
            }
            if (iDateYM.Value == null)
            {
                iDateYM.Select();
                appToolTip.Show(iDateYM, AppToolTipIndex.CannotUseBlank);
                return;
            }
            if (iDate.Value == null)
            {
                iDate.Select();
                appToolTip.Show(iDate, AppToolTipIndex.CannotUseBlank);
                return;
            }

            DateTime ym = AppDate.ParseDateFirstDay(iDateYM.Value.Value);
            Bank bank = AppGlobal.Banks.Get(Cast.Int(iBank.SelectedValue));

            AppBankFile abf = new AppBankFile();

            abf.ShowSaveFilePath();

            // 実行処理
            AppBankFile.eResut ret = abf.CreateHikiotoshi(ym, bank, iDate.Value.Value);

            switch (ret)
            {
                case AppBankFile.eResut.DataNotFound:
                case AppBankFile.eResut.Success:
                case AppBankFile.eResut.CannotFileSave:
                case AppBankFile.eResut.UnmatchData:
                    // 結果画面を表示
                    FormLogView2 frm = new FormLogView2();
                    frm.SelectedYM = ym;
                    frm.Table = abf.Logs.GetTable();
                    frm.TitleText = "FBデータ作成結果";
                    frm.ShowDialog();
                    break;
            }
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
