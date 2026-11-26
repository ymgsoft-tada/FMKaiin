using System;
using ComponentSUI;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 市民病院会費請求資料の Excel 出力の画面。<see cref="FormOutExcelShimingyoinKaihi"/> と同じ内容を、S 系のコントロール（comset の UI）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。年は <see cref="SDateBox"/>、期は <see cref="SComboBox"/>、
    /// 医会の選択は <see cref="SRadioButton"/> を使う。出力の処理は、元の画面と同じもの（<see cref="AppExcelShiminByoinKaihi"/>）を使う。
    /// </remarks>
    public partial class FormOutExcelShimingyoinKaihi2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormOutExcelShimingyoinKaihi2()
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
            funcKeys = new FunctionKeyBinder(funckey, FuncOutExcelShimingyoinKaihi.Functions);

            iYear.Value = AppDate.ParseDateFirstDay(DateTime.Now);

            iPeriod.BeginUpdate();
            iPeriod.AddItem("Ⅰ期", 1);
            iPeriod.AddItem("Ⅱ期", 2);
            iPeriod.AddItem("Ⅲ期", 3);
            iPeriod.EndUpdate();
            iPeriod.SelectedValue = 1;

            rdoKen.Checked = true;

            funcKeys.On(FuncOutExcelShimingyoinKaihi.OutExcel, doExec);
            funcKeys.On(FuncOutExcelShimingyoinKaihi.Close, formClose);
        }

        /// <summary>
        /// Excel 出力
        /// </summary>
        void doExec()
        {
            if (iYear.Value == null)
            {
                iYear.Select();
                appToolTip.Show(iYear, AppToolTipIndex.CannotUseBlank);
                return;
            }

            int year = iYear.Value.Value.Year;
            int period = Cast.Int(iPeriod.SelectedValue);
            eTypeIkai ikai = (rdoKen.Checked == true) ? eTypeIkai.Keni : eTypeIkai.Shii;

            AppExcelShiminByoinKaihi xls = new AppExcelShiminByoinKaihi(year, period, ikai);

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
