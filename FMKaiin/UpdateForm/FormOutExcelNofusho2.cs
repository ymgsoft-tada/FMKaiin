using System;
using App.Controls;

namespace App
{
    /// <summary>
    /// 会費納付書の Excel 出力の画面。<see cref="FormOutExcelNofusho"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。日付は <see cref="SDateBox"/>、医師会の選択は <see cref="SRadioButton"/> を使う。
    /// 出力の処理は、元の画面と同じもの（<see cref="AppExcelNofusho"/>）を使う。
    /// </remarks>
    public partial class FormOutExcelNofusho2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>処理日の初期値にする日（月分の翌月）</summary>
        const int ShoribiDay = 15;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutExcelNofusho2()
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
            funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelNofusho.Functions);

            iDateYM.Value = AppDate.ParseDateFirstDay(DateTime.Now);
            setShoribi();

            rdoNichii.Checked = true;

            // 月分を確定したとき（Enter・フォーカスの移動・カレンダーでの選択）に、処理日を決め直す
            iDateYM.ValueChanged += iDateYM_ValueChanged;

            funcKeys.On(FuncOutExcelNofusho.OutExcel, doExec);
            funcKeys.On(FuncOutExcelNofusho.Close, formClose);
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

            // 処理日は月分の翌月15日。土日なら次の月曜にする。祝日は判定できないので手で直す（総合振込と同じ）
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

            eTypeIkai ikai = eTypeIkai.Shii;
            if (rdoNichii.Checked == true)
            {
                ikai = eTypeIkai.Nichii;
            }
            else if (rdoKeni.Checked == true)
            {
                ikai = eTypeIkai.Keni;
            }

            AppExcelNofusho xls = new AppExcelNofusho(ym, iDate.Value.Value, ikai);

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
        /// 閉じる
        /// </summary>
        void formClose()
        {
            this.Close();
        }
    }
}
