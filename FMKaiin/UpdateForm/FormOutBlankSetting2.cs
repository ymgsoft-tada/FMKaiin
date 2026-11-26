using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ComponentSUI;

namespace App
{
    /// <summary>
    /// 帳票の余白設定の画面。<see cref="FormOutBlankSetting"/> と同じ内容を、S 系の部品（comset の UI）で作ったもの。
    /// 印刷プレビュー（<see cref="FormReportPreview"/>）の「余白設定」から開く。
    /// </summary>
    /// <remarks>
    /// 印字する位置を、横・縦にどれだけずらすかを、スクロール バーで決める。値は 1/100 インチ単位で、50 が「ずらさない」。
    /// スクロール バーと、用紙の絵を描く部分は、標準のコントロールをそのまま使っている（S 系に、当たる部品が無いため）。
    /// ファンクション キーのバーは無い（操作は、3 つのボタン）。
    /// 元の画面と違う点: 渡された値がスクロール バーの範囲の外でも、例外にせず、範囲の中に収める。
    /// 用紙の絵は、表示倍率に合わせた大きさで描く。
    /// </remarks>
    public partial class FormOutBlankSetting2 : FormFrame
    {
        /// <summary>BIZ UDPゴシックが PC に無いときに、見出しや項目名に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>スクロール バーの値のうち、「ずらさない」に当たる値。</summary>
        private const int CenterValue = 50;

        /// <summary>スクロール バーの値 1 つ分（1/100 インチ）の長さ。mm。</summary>
        private const double MillimetersPerUnit = 0.254;

        /// <summary>スクロール バーを端まで動かしたときに、絵の中の文字が動く量（96 DPI 基準のピクセル）。値 100 につき、この量だけ動く。</summary>
        private const float PreviewShiftPerHundred = 8f;

        /// <summary>
        /// 帳票のグループ名（グループ名ごとに同じ余白設定を使う）
        /// </summary>
        string blankGroupName;
        /// <summary>
        /// 横マージン
        /// </summary>
        int marginH;
        /// <summary>
        /// 縦マージン
        /// </summary>
        int marginV;
        /// <summary>
        /// キャンセルを押されたかを通知します
        /// </summary>
        bool cancel = true;

        /// <summary>用紙（余白の線つき）の絵。</summary>
        private readonly Image paperImage;
        /// <summary>印字される文字の絵。余白の設定に合わせて動かす。</summary>
        private readonly Image textImage;
        /// <summary>用紙の枠の絵。文字の上に重ねる。</summary>
        private readonly Image frameImage;

        #region *** Property ***
        /// <summary>
        /// 横マージン
        /// </summary>
        public int MarginH
        {
            get
            {
                return marginH;
            }
        }
        /// <summary>
        /// 縦マージン
        /// </summary>
        public int MarginV
        {
            get
            {
                return marginV;
            }
        }
        /// <summary>
        /// キャンセルが押されたかを通知します。
        /// </summary>
        public bool Cancel
        {
            get
            {
                return cancel;
            }
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_blankGroupName">帳票のグループ名（グループ名ごとに同じ余白設定を使う）</param>
        /// <param name="_marginH">横マージン</param>
        /// <param name="_marginV">縦マージン</param>
        public FormOutBlankSetting2(string _blankGroupName, int _marginH, int _marginV)
        {
            InitializeComponent();

            // 背景の画像と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;

            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);

            blankGroupName = _blankGroupName;
            marginH = _marginH;
            marginV = _marginV;

            // 絵は、ここで 1 回だけ取り出す（ImageList は、取り出すたびに新しい画像を作るため）。画面と一緒に破棄する
            paperImage = imgList1.Images["MarginSet0"];
            textImage = imgList2.Images["MarginSet1"];
            frameImage = imgList1.Images["MarginSet2"];
            Disposed += delegate
            {
                paperImage.Dispose();
                textImage.Dispose();
                frameImage.Dispose();
            };

            hScroll.ValueChanged += hScroll_ValueChanged;
            vScroll.ValueChanged += vScroll_ValueChanged;

            picPaper.Paint += picPaper_Paint;

            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancel_Click;
            btnClear.Click += btnClear_Click;

            hScroll.Value = clampToScroll(hScroll, marginH);
            vScroll.Value = clampToScroll(vScroll, marginV);
            // 値が変わらなかったとき（最初から 0 のとき）は ValueChanged が起きないので、表示は、ここでも合わせておく
            showValues();
        }

        /// <summary>
        /// 初回描画
        /// </summary>
        protected override void FormFrame_Shown(object sender, EventArgs e)
        {
            hScroll.Select();

            base.FormFrame_Shown(sender, e);
        }

        /// <summary>
        /// 値を、スクロール バーで動かせる範囲に収める（つまみの幅の分だけ、Maximum より小さい値までしか動かせない）。
        /// </summary>
        static int clampToScroll(ScrollBar bar, int value)
        {
            int max = bar.Maximum - bar.LargeChange + 1;
            return Math.Max(bar.Minimum, Math.Min(max, value));
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            // 設定された値の保存
            marginH = hScroll.Value;
            marginV = vScroll.Value;

            cancel = false;

            this.Close();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            hScroll.Value = CenterValue;
            vScroll.Value = CenterValue;
        }

        void hScroll_ValueChanged(object sender, EventArgs e)
        {
            showValues();
        }

        void vScroll_ValueChanged(object sender, EventArgs e)
        {
            showValues();
        }

        /// <summary>
        /// いまの設定（横・縦にずらす量）を、mm で表示して、用紙の絵を描き直す。
        /// </summary>
        void showValues()
        {
            // 1/100inch → mm に
            lblH.Text = "横　" + string.Format("{0:0.0}", (hScroll.Value - CenterValue) * MillimetersPerUnit) + "mm";
            lblV.Text = "縦　" + string.Format("{0:0.0}", (vScroll.Value - CenterValue) * MillimetersPerUnit) + "mm";
            picPaper.Invalidate();
        }

        /// <summary>
        /// 用紙の絵を描く。用紙を中央に描き、その上に、余白の設定の分だけずらした文字と、用紙の枠を重ねる。
        /// </summary>
        void picPaper_Paint(object sender, PaintEventArgs e)
        {
            // 絵の大きさは 96 DPI 基準なので、表示倍率に合わせて拡大する（絵を置く枠も、同じ倍率で大きくなっているため）
            float scale = LayoutScale.Of(picPaper);
            Rectangle client = picPaper.ClientRectangle;

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            // 用紙
            e.Graphics.DrawImage(paperImage, centered(client, paperImage, scale, 0f, 0f));

            // マージン設定によって移動する文字
            float shiftX = (hScroll.Value - CenterValue) * PreviewShiftPerHundred / 100f;
            float shiftY = (vScroll.Value - CenterValue) * PreviewShiftPerHundred / 100f;
            e.Graphics.DrawImage(textImage, centered(client, textImage, scale, shiftX, shiftY));

            // 用紙の枠
            e.Graphics.DrawImage(frameImage, centered(client, frameImage, scale, 0f, 0f));
        }

        /// <summary>
        /// 絵を、<paramref name="client"/> の中央（そこから <paramref name="shiftX"/>・<paramref name="shiftY"/> だけずらした位置）に描くときの範囲を返す。
        /// ずらす量は 96 DPI 基準で、<paramref name="scale"/> を掛ける。
        /// </summary>
        static RectangleF centered(Rectangle client, Image image, float scale, float shiftX, float shiftY)
        {
            float width = image.Width * scale;
            float height = image.Height * scale;
            return new RectangleF(
                client.Left + (client.Width - width) / 2f + shiftX * scale,
                client.Top + (client.Height - height) / 2f + shiftY * scale,
                width,
                height);
        }
    }
}
