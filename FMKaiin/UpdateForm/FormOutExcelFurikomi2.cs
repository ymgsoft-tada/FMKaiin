using System;
using System.ComponentModel;
using App.Controls;

namespace App
{
    /// <summary>
    /// 医師信用組合振込データ一覧の Excel 出力の画面。<see cref="FormOutExcelFurikomi"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。日付は <see cref="SDateBox"/> を使う。
    /// 出力の処理は、元の画面と同じもの（<see cref="AppExcelFurikomi"/>）を使う。
    /// </remarks>
    public partial class FormOutExcelFurikomi2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>処理日の初期値にする日（月分の翌月）</summary>
        const int ShoribiDay = 15;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutExcelFurikomi2()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
        }

        /// <summary>
        /// ファンクションの設定
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelFurikomi.Functions);

            iDateYM.Value = AppDate.ParseDateFirstDay(DateTime.Now);
            setShoribi();

            // 月分を確定したとき（Enter・フォーカスの移動・カレンダーでの選択）に、処理日を決め直す
            iDateYM.ValueChanged += iDateYM_ValueChanged;

            funcKeys.On(FuncOutExcelFurikomi.OutExcel, doExec);
            funcKeys.On(FuncOutExcelFurikomi.Close, formClose);
        }

        private void iDateYM_ValueChanged(object sender, EventArgs e)
        {
            setShoribi();
        }

        /// <summary>
        /// 月分から処理日の初期値をセットします。
        /// </summary>
        void setShoribi()
        {
            if (iDateYM.Value == null)
            {
                return;
            }

            // 処理日は月分の翌月15日。土日なら次の月曜にする。祝日は判定できないので手で直す
            DateTime next = AppDate.ParseDateFirstDay(iDateYM.Value.Value).AddMonths(1);
            DateTime date = new DateTime(next.Year, next.Month, ShoribiDay);

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }

            iDate.Value = date;
        }

        /// <summary>
        /// Excel 出力
        /// </summary>
        void doExec()
        {
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

            // 銀行名の取得に銀行コードを使う。未ロードならここでロードする
            initBankCode();

            AppExcelFurikomi xls = new AppExcelFurikomi(ym, iDate.Value.Value);

            switch (xls.Execute())
            {
                case AppExcel.eResult.Success:
                    AppMsgBox.Show(this, AppMsgBoxIndex.CreateExcelFile);
                    break;
                case AppExcel.eResult.Cancel:
                    break;
                case AppExcel.eResult.NotExistTemplate:
                    AppMsgBox.Show(this, AppMsgBoxIndex.NotExistFile);
                    break;
                case AppExcel.eResult.NotFoundData:
                    AppMsgBox.Show(this, AppMsgBoxIndex.NotFoundDataExport);
                    break;
                default:
                    AppMsgBox.Show(this, AppMsgBoxIndex.FailedExcelFile);
                    break;
            }
        }

        /// <summary>
        /// 銀行コードデータの取得処理
        /// </summary>
        void initBankCode()
        {
            // 銀行コードは件数が多く、使う画面を開くときにロードされる（Form1と同じ手順）
            if (AppGlobal.BankCodeMg == null)
            {
                FormBg_Progress prog = new FormBg_Progress();
                prog.TitleText = "しばらくお待ちください。";
                prog.LabelText = "データをロードしています。";
                prog.DoWorkEvent += prog_DoWorkEvent;
                prog.ShowDialog();
                prog.Dispose();
                prog = null;
            }
        }

        private void prog_DoWorkEvent(object sender, DoWorkEventArgs e)
        {
            FormBg_Progress prog = (FormBg_Progress)sender;
            prog.AdvanceProgress(100);

            AppGlobal.DB.GetReFillTable(TableProp.t_bank_code);
            AppGlobal.InitBankCodeMg();
            AppGlobal.Banks.Init();
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
