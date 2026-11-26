using System;
using System.Windows.Forms;
using C1.Win.FlexReport;
using C1.Win.FlexViewer;
using ComponentForm;
using ComponentIO;
using ComponentRegistry;
using ComponentSUI;

namespace App
{
    /// <summary>
    /// 印刷プレビュー。<see cref="FormReportPreview"/> と同じ内容で、上のツール バーだけを、S 系の画面の見た目に作り替えたもの。
    /// </summary>
    /// <remarks>
    /// 帳票を表示する部分（C1 のビューア）は、元の画面と同じものを使っている。
    /// ツール バーは、標準の ToolStrip をやめて、半透明のパネルの上に、アイコン付きのボタン（<see cref="ToolbarButton"/>）と、
    /// S 系の入力欄・コンボ ボックスを並べた。帳票のクラス（ReportFlex○○）が、<see cref="SetReport"/> で帳票を渡してから開く。
    /// 元の画面と違う点:
    /// 倍率の欄は、数字だけを表示する（「%」は、欄の右の文字）。倍率の選択肢は、ボタンから開くメニューではなく、コンボ ボックスにした。
    /// 倍率・ページの欄は、Enter のほか、フォーカスが外れたときにも反映する（倍率は、内容を変えたときだけ）。
    /// 余白設定は、S 系版の <see cref="FormOutBlankSetting2"/> を開く。
    /// </remarks>
    public partial class FormReportPreview2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、ボタンや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>倍率の一覧で、「実際のサイズ」を表す値。</summary>
        private const int ZoomChoiceActualSize = 10000;
        /// <summary>倍率の一覧で、「ページ全体」を表す値。</summary>
        private const int ZoomChoiceWholePage = 10001;
        /// <summary>倍率の一覧に並べる倍率（%）。</summary>
        private static readonly int[] ZoomChoices = { 400, 200, 150, 125, 100, 75, 50 };

        /// <summary>
        /// レポート
        /// </summary>
        ReportFlex.ReportInfo repInfo;
        /// <summary>
        /// カレントページ
        /// </summary>
        int currentPage;
        /// <summary>
        /// 余白設定に使用されるレジストリヘッダ
        /// </summary>
        string marginName;
        /// <summary>
        /// 横のマージン
        /// </summary>
        int marginLeft;
        /// <summary>
        /// 縦のマージン
        /// </summary>
        int marginTop;
        /// <summary>
        /// 倍率の欄に、最後に表示した文字列。欄の内容がこれと違うときだけ、倍率として反映する
        /// （「ページ全体」で表示している間に、欄を通り過ぎただけで、倍率の指定に変わってしまわないようにするため）。
        /// </summary>
        string shownZoomText = "";

        /// <summary>
        /// .ctor
        /// </summary>
        public FormReportPreview2()
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);

            currentPage = 1;
            marginName = "blank";
            KeyPreview = true;

            // 画面の大きさを変えたとき: ツール バーは幅だけ、帳票の表示は幅と高さを、画面に合わせる
            layout = new FormLayout(this);
            layout.AddControl(secTool, FormLayoutRule.None, FormLayoutRule.None, FormLayoutRule.LinkBottom, FormLayoutRule.None);
            layout.AddControl(ppc, FormLayoutRule.None, FormLayoutRule.None, FormLayoutRule.LinkBottom, FormLayoutRule.LinkBottom);
            layout.EndAdd();

            // 最大化を標準とする。
            this.WindowState = FormWindowState.Maximized;

            this.SizeChanged += FormReportPreview_SizeChanged;
        }

        /// <summary>
        /// 初回描画
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            // マージンの初期化
            int hval = Cast.Int(RegCommon.Read(marginName + "_H"), 50);
            int vval = Cast.Int(RegCommon.Read(marginName + "_V"), 50);

            marginLeft = Math.Max(0, Math.Min(100, hval));
            marginTop = Math.Max(0, Math.Min(100, vval));

            // 倍率の一覧
            iZoomList.BeginUpdate();
            foreach (int zoom in ZoomChoices)
            {
                iZoomList.AddItem(zoom + "%", zoom);
            }
            iZoomList.AddItem("実際のサイズ", ZoomChoiceActualSize);
            iZoomList.AddItem("ページ全体", ZoomChoiceWholePage);
            iZoomList.EndUpdate();

            // レポート情報の初期化
            repInfo.LoadReport();

            // 最初のページを表示
            refreshPane();
            // 初期はページ全体で描画
            updateZoom(0);

            // event
            btnPageFirst.Click += btnPageFirst_Click;
            btnPagePrev.Click += btnPagePrev_Click;
            btnPageNext.Click += btnPageNext_Click;
            btnPageLast.Click += btnPageLast_Click;
            iPage.KeyDown += iPage_KeyDown;
            iPage.Leave += iPage_Leave;

            btnSetting.Click += btnSetting_Click;
            btnPrint.Click += btnPrint_Click;
            btnSave.Click += btnSave_Click;

            iZoomList.SelectionChangeCommitted += iZoomList_SelectionChangeCommitted;
            iZoom.KeyDown += iZoom_KeyDown;
            iZoom.Leave += iZoom_Leave;
            btnZoomIn.Click += btnZoomIn_Click;
            btnZoomOut.Click += btnZoomOut_Click;

            // last call
            base.FormFrame_Shown(sender, e);
        }

        void FormReportPreview_SizeChanged(object sender, EventArgs e)
        {
            // ページ全体の場合にフォームサイズに合わせて倍率表示を変更
            if (ppc.ZoomMode == FlexViewerZoomMode.WholePage)
            {
                updateZoom(0);
            }
        }

        /// <summary>
        /// レポートをセットする
        /// </summary>
        /// <param name="_repinfo">レポート情報クラス</param>
        public void SetReport(ReportFlex.ReportInfo _repinfo)
        {
            repInfo = _repinfo;
        }

        /// <summary>
        /// レポートをレポート名から取得
        /// </summary>
        /// <param name="reportName">レポート名</param>
        /// <returns>レポート情報</returns>
        public ReportFlex.ReportInfo GetReport(string reportName)
        {
            return repInfo;
        }

        #region Zoom
        /// <summary>拡大・縮小のボタンで、順に切り替える倍率（%）。大きい順。</summary>
        int[] zoomList = { 1000, 800, 400, 200, 150, 125, 100, 65, 50, 25, 5 };

        /// <summary>
        /// 倍率の欄の内容を、数値にして返す。読めないときは -1（<see cref="updateZoom"/> が 100 にする）。
        /// </summary>
        int readZoomText()
        {
            return Cast.Int(iZoom.Text, -1);
        }

        /// <summary>
        /// ＋
        /// </summary>
        void btnZoomIn_Click(object sender, EventArgs e)
        {
            int zoom = readZoomText();

            if (zoom < 0)
            {
                zoom = 100;
            }

            for (int i = zoomList.Length - 1; i > -1; i--)
            {
                if (zoom < zoomList[i])
                {
                    updateZoom(zoomList[i]);
                    break;
                }
            }
        }

        /// <summary>
        /// －
        /// </summary>
        void btnZoomOut_Click(object sender, EventArgs e)
        {
            int zoom = readZoomText();

            if (zoom < 0)
            {
                zoom = 100;
            }

            for (int i = 0; i < zoomList.Length; i++)
            {
                if (zoom > zoomList[i])
                {
                    updateZoom(zoomList[i]);
                    break;
                }
            }
        }

        /// <summary>
        /// 直接入力
        /// </summary>
        void iZoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                applyZoomText();
            }
        }

        void iZoom_Leave(object sender, EventArgs e)
        {
            applyZoomText();
        }

        /// <summary>
        /// 倍率の欄の内容が、表示したときから変わっていれば、倍率として反映する。
        /// </summary>
        void applyZoomText()
        {
            if (iZoom.Text != shownZoomText)
            {
                updateZoom(readZoomText());
            }
        }

        /// <summary>
        /// コンボ選択
        /// </summary>
        void iZoomList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int no = Cast.Int(iZoomList.SelectedValue, -1);

            switch (no)
            {
                case ZoomChoiceActualSize: no = 100; break;
                case ZoomChoiceWholePage: no = 0; break;
                default: break;
            }
            updateZoom(no);
        }
        #endregion

        #region Page
        /// <summary>
        /// 直接入力
        /// </summary>
        void iPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updatePage(Cast.Int(iPage.Text));
            }
        }

        /// <summary>
        /// ページ直接入力
        /// </summary>
        void iPage_Leave(object sender, EventArgs e)
        {
            updatePage(Cast.Int(iPage.Text));
        }

        /// <summary>
        /// 先頭に
        /// </summary>
        void btnPageFirst_Click(object sender, EventArgs e)
        {
            showPage(1);
        }

        /// <summary>
        /// １ページ前
        /// </summary>
        void btnPagePrev_Click(object sender, EventArgs e)
        {
            showPage(Math.Max(1, currentPage - 1));
        }

        /// <summary>
        /// １ページ後
        /// </summary>
        void btnPageNext_Click(object sender, EventArgs e)
        {
            showPage(Math.Min(repInfo.MaxPage, currentPage + 1));
        }

        /// <summary>
        /// 末尾に
        /// </summary>
        void btnPageLast_Click(object sender, EventArgs e)
        {
            showPage(repInfo.MaxPage);
        }

        /// <summary>
        /// 指定のページを表示する。いまのページと同じなら、何もしない。
        /// </summary>
        void showPage(int page)
        {
            if (page != currentPage)
            {
                currentPage = page;
                refreshPane();
            }
        }
        #endregion

        /// <summary>
        /// 余白設定
        /// </summary>
        void btnSetting_Click(object sender, EventArgs e)
        {
            //■ 余白設定画面の表示
            FormOutBlankSetting2 form = new FormOutBlankSetting2("blank", marginLeft, marginTop);
            form.ShowDialog();

            if (form.Cancel == false)
            {
                RegCommon.Write(marginName + "_H", form.MarginH);
                RegCommon.Write(marginName + "_V", form.MarginV);
                marginLeft = form.MarginH;
                marginTop = form.MarginV;

                repInfo.SetMargin(marginLeft, marginTop);
                refreshPane();
            }

            form.Dispose();
            form = null;
        }

        /// <summary>
        /// 印刷
        /// </summary>
        void btnPrint_Click(object sender, EventArgs e)
        {
            // 直接印刷
            repInfo.Print();
        }

        /// <summary>
        /// PDF作成
        /// </summary>
        void btnSave_Click(object sender, EventArgs e)
        {
            // PDF作成
            repInfo.SavePDF();
        }

        /// <summary>
        /// ページ更新
        /// </summary>
        void updatePage(int page)
        {
            if (page < 1)
            {
                page = 1;
            }
            if (page >= repInfo.MaxPage)
            {
                page = repInfo.MaxPage;
            }

            if (page != currentPage)
            {
                currentPage = page;
                refreshPane();
            }
            else
            {
                iPage.Text = currentPage.ToString();
                if (iPage.Focused)
                {
                    iPage.SelectAll();
                }
            }
        }

        /// <summary>
        /// 指定されたZoom値を適正までまるめ、設定する
        /// </summary>
        /// <param name="zoom">ズーム値(-1..Error, 範囲 0 - 1000, 0..ページ全体)</param>
        void updateZoom(int zoom)
        {
            if (zoom < 0)
            {
                // エラーは100に
                zoom = 100;
            }
            if (zoom > 1000)
            {
                zoom = 1000;
            }

            // 倍率の一覧で選んでおく項目（無ければ、何も選ばない）
            object choice = null;

            if (zoom == 100)
            {
                ppc.ZoomMode = FlexViewerZoomMode.ActualSize;
                // 「実際のサイズ」を選んだときは、そのままにする（100% と同じ意味）
                choice = Cast.Int(iZoomList.SelectedValue, -1) == ZoomChoiceActualSize ? ZoomChoiceActualSize : 100;
            }
            else if (zoom == 0)
            {
                ppc.ZoomMode = FlexViewerZoomMode.WholePage;
                choice = ZoomChoiceWholePage;
                zoom = (int)(ppc.ZoomFactor * 100);
            }
            else
            {
                ppc.ZoomMode = FlexViewerZoomMode.Custom;
                ppc.ZoomFactor = (float)zoom / 100;
                if (Array.IndexOf(ZoomChoices, zoom) >= 0)
                {
                    choice = zoom;
                }
            }

            if (choice == null || iZoomList.SelectByValue(choice) == false)
            {
                iZoomList.SelectedIndex = -1;
            }

            iZoom.Text = zoom.ToString();
            shownZoomText = iZoom.Text;
            if (iZoom.Focused)
            {
                iZoom.SelectAll();
            }
        }

        /// <summary>
        /// ページ表示の更新
        /// </summary>
        void refreshPane()
        {
            this.Cursor = Cursors.WaitCursor;

            secTool.Enabled = false;

            C1FlexReport rep = repInfo.Report;

            // 現在のページを表示
            repInfo.SetCurrentPage(currentPage);

            ppc.DocumentSource = rep;
            ppc.Select();

            iPage.Text = currentPage.ToString();
            lblMaxPage.Text = string.Format("/ {0}", repInfo.MaxPage);

            // 先頭、終端ページでの押せるボタン、押せないボタンの制御
            btnPageFirst.Enabled = btnPagePrev.Enabled = currentPage != 1;
            btnPageLast.Enabled = btnPageNext.Enabled = currentPage != repInfo.MaxPage;

            secTool.Enabled = true;

            this.Cursor = Cursors.Default;
        }
    }
}
