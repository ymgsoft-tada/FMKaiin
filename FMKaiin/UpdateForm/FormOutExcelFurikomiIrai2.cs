using System;
using App.Controls;

namespace App
{
    /// <summary>
    /// 振込依頼書の Excel 出力の画面。<see cref="FormOutExcelFurikomiIrai"/> と同じ内容を、S 系のコントロール（Controls/Common）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。日付は <see cref="SDateBox"/> を使う。
    /// 出力の処理は、元の画面と同じもの（<see cref="AppExcelFurikomiIrai"/>）を使う。
    /// </remarks>
    public partial class FormOutExcelFurikomiIrai2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutExcelFurikomiIrai2()
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
            funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelFurikomiIrai.Functions);

            iDateYM.Value = AppDate.ParseDateFirstDay(DateTime.Now);
            setShoribi();

            // 月分を確定したとき（Enter・フォーカスの移動・カレンダーでの選択）に、処理日を決め直す
            iDateYM.ValueChanged += iDateYM_ValueChanged;

            funcKeys.On(FuncOutExcelFurikomiIrai.OutExcel, doExec);
            funcKeys.On(FuncOutExcelFurikomiIrai.Close, formClose);
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

            // 過去の月の送金指定日と同じ計算を使う（翌月15日。土日なら次の月曜）
            iDate.Value = AppExcelFurikomiIrai.GetDefaultShoribi(iDateYM.Value.Value);
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

            AppExcelFurikomiIrai xls = new AppExcelFurikomiIrai(ym, iDate.Value.Value);

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
