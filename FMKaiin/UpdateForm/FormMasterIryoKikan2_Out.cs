using System;
using ComponentDB;

namespace App
{
    /// <summary>
    /// 医療機関マスタの印刷プレビュー（出力する帳票を選ぶ画面）。<see cref="FormOutIryoKikan"/> と同じ内容を、S 系の画面の見た目（S画面仕様.md の 1.7）で作ったもの。
    /// </summary>
    /// <remarks>
    /// Gc 系・Yc 系のコントロール・コードは使っていない。帳票の選択は、S 系のラジオ ボタン（<see cref="App.Controls.SRadioButton"/>）で行う。
    /// 帳票は、元の画面と同じもの（<see cref="ReportFlexIryoKikan"/>・<see cref="ReportFlexIryoKikanJoho"/>）を使う。
    /// </remarks>
    public partial class FormMasterIryoKikan2_Out : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>
        /// 出力する医療機関（一覧の画面のビュー）
        /// </summary>
        private DBView outview;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_outview">出力する医療機関（一覧の画面のビュー）。医療機関情報は、このビューの現在の行を出力する</param>
        public FormMasterIryoKikan2_Out(DBView _outview)
        {
            outview = _outview;

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
            rdoList.Checked = true;
            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// ファンクションの登録
        /// </summary>
        protected override void SetFunction()
        {
            funcKeys = new FunctionKeyBinder(funckey, FuncOutIryoKikan.Functions);

            funcKeys.On(FuncOutIryoKikan.Preview, preview);
            funcKeys.On(FuncOutIryoKikan.Close, formClose);
        }

        /// <summary>
        /// 印刷プレビュー（選んだ帳票を出力する）
        /// </summary>
        void preview()
        {
            if (rdoList.Checked == true)
            {
                ReportFlexIryoKikan rep = new ReportFlexIryoKikan(outview);
                rep.ReportExec();
                rep = null;
            }
            else
            {
                // 医療機関情報は、一覧で選んでいる 1 件だけを出力する
                DBView view = new DBView(outview.DataTable.Clone());
                view.DataTable.ImportRow(outview.CurrentRow.Row);

                ReportFlexIryoKikanJoho rep = new ReportFlexIryoKikanJoho(view);
                rep.ReportExec();
                rep = null;
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
