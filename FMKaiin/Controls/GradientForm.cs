using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 背景を縦方向のグラデーションで塗れるフォーム。
    /// <see cref="BackgroundTopColor"/>（上端の色）と <see cref="BackgroundBottomColor"/>（下端の色）の両方を指定すると、
    /// 上から下へ色がなめらかに変わる背景になる。どちらかが空の場合は、標準どおり BackColor の単色で塗る。
    /// このフォームを継承した画面では、2 つの色がデザイナーのプロパティ一覧（表示）に出て、デザイン画面にも反映される。
    /// グラデーションを見せたいパネルやラベルは、BackColor を Transparent にしておく
    /// （不透明な背景色を持つコントロールは、その色で塗りつぶされる）。
    /// </summary>
    public class GradientForm : Form
    {
        private Color _backgroundTopColor = Color.Empty;
        private Color _backgroundBottomColor = Color.Empty;
        private int _paintedHeight = -1;

        public GradientForm()
        {
            // 背景と、その上に重ねる透明なコントロールをまとめて描いて、ちらつきを抑える
            DoubleBuffered = true;
        }

        /// <summary>
        /// 背景のグラデーションの上端の色。空（<see cref="Color.Empty"/>）の場合は BackColor の単色で塗る。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("背景のグラデーションの上端の色。BackgroundBottomColor と両方指定すると、上から下へ色が変わる背景になる。空の場合は BackColor の単色。")]
        public Color BackgroundTopColor
        {
            get { return _backgroundTopColor; }
            set { _backgroundTopColor = value; Invalidate(true); }
        }

        /// <summary>
        /// 背景のグラデーションの下端の色。空（<see cref="Color.Empty"/>）の場合は BackColor の単色で塗る。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("背景のグラデーションの下端の色。BackgroundTopColor と両方指定すると、上から下へ色が変わる背景になる。空の場合は BackColor の単色。")]
        public Color BackgroundBottomColor
        {
            get { return _backgroundBottomColor; }
            set { _backgroundBottomColor = value; Invalidate(true); }
        }

        /// <summary>背景をグラデーションで塗るか。背景画像を設定している場合は、画像を優先する。</summary>
        private bool PaintsGradient
        {
            get { return !_backgroundTopColor.IsEmpty && !_backgroundBottomColor.IsEmpty && BackgroundImage == null; }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Rectangle client = ClientRectangle;
            if (!PaintsGradient || client.Width <= 0 || client.Height <= 0)
            {
                base.OnPaintBackground(e);
                return;
            }

            // 色の変わり方は常に表示領域の上端から下端までを基準にして、描き直す範囲だけを塗る。
            // 背景が透明の子コントロールが自分の下地としてここを呼ぶときも、同じ基準なので継ぎ目ができない
            using (var brush = new LinearGradientBrush(client, _backgroundTopColor, _backgroundBottomColor, LinearGradientMode.Vertical))
            {
                // 上端・下端の 1 画素に反対側の色が回り込まないようにする
                brush.WrapMode = WrapMode.TileFlipXY;
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // 高さが変わると、グラデーション全体の色の位置が変わる。位置や大きさが変わらない子コントロール
            // （上端のヘッダーなど）はそのままでは再描画されないので、子も含めて描き直す
            int height = ClientSize.Height;
            if (PaintsGradient && height != _paintedHeight)
            {
                _paintedHeight = height;
                Invalidate(true);
            }
        }
    }
}
