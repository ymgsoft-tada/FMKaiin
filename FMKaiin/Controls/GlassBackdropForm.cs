using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 背景画像を使う画面の土台になるフォーム。
    /// 背景画像（<see cref="Control.BackgroundImage"/>）を画面いっぱいに描き、ガラス調のコントロール
    /// （BackdropBlur を true にした <see cref="SectionPanel"/> など）へ、その背景を強くぼかしたものを提供する。
    /// ガラス調のコントロールを置かず、背景画像を描くためだけに使ってもよい（ぼかした背景は、求められるまで作らない）。
    /// 使うには、このフォームを継承した画面で背景画像を設定し、配置（<see cref="Control.BackgroundImageLayout"/>）を
    /// <see cref="ImageLayout.Stretch"/> にする。
    /// 背景画像を設定していない場合と、配置が Stretch 以外の場合は、標準のフォームと同じ描画になる。
    /// その場合は、背景色（<see cref="Control.BackColor"/>）が半透明のパネル越しに見える。
    /// </summary>
    public class GlassBackdropForm : Form, IBackdropBlurSource
    {
        /// <summary>
        /// ガラス越しの背景をぼかす強さ。背景画像をいったんこの分の 1 の大きさへ縮めてから引き伸ばしてぼかす。
        /// 大きいほど強くぼける。目安は 3～8。強くしすぎる（30 など）と後ろの景色が一様な色まで溶けてしまい、
        /// 上に載せたカードが透けて見えず、ただの明るい板に見える。
        /// </summary>
        private const int BackdropBlurDownscale = 8;

        /// <summary>
        /// ぼかした背景の元画像を持っておく大きさ（縮めた状態からの拡大率）。
        /// ぼけた画像は色の変化がゆるやかなので、この程度の大きさで持っておけば、
        /// ウィンドウの大きさへ単純に引き伸ばしても粗さは見えない。
        /// 拡大した結果は <see cref="BlurredMasterMaxWidth"/> × <see cref="BlurredMasterMaxHeight"/> までに収める。
        /// </summary>
        private const int BackdropBlurMasterScale = 20;

        /// <summary>
        /// 背景の元画像を持っておく幅の上限。これより大きな画面では、ここから単純に引き伸ばす。
        /// これより大きな背景画像は、この幅まで縮めて持つ（大きな写真を設定しても、メモリを使いすぎないようにする）。
        /// </summary>
        private const int BackdropMasterMaxWidth = 1920;

        /// <summary>背景の元画像を持っておく高さの上限。</summary>
        private const int BackdropMasterMaxHeight = 1200;

        /// <summary>ぼかした背景の元画像を持っておく幅の上限（上限いっぱいで約 16MB）。</summary>
        private const int BlurredMasterMaxWidth = 2560;

        /// <summary>ぼかした背景の元画像を持っておく高さの上限。</summary>
        private const int BlurredMasterMaxHeight = 1600;

        // 半透明のコントロールは自分を描くたびに後ろの背景を描き直すので、背景は作り置きしておく。
        // 作り置きは 2 段階に分けている。ウィンドウの大きさを変えている間は何度も作り直すことになるので、
        // 時間のかかるなめらかな拡大縮小は「元画像」を作るときの 1 回だけにして、大きさが変わるたびの処理は単純な写し取りにする。
        //   元画像（背景画像が変わったときだけ作る）
        //     _backdropMaster     … 背景画像を、画面いっぱいに表示しても足りる大きさへなめらかに引き伸ばしたもの
        //     _blurredMaster      … 背景画像をぼかしたもの
        //   表示用（ウィンドウの大きさが変わるたびに、元画像から単純に写し取って作る）
        //     _scaledBackdrop     … 背景そのもの
        //     _blurredBackdrop    … ガラス越しに見える、ぼかした背景
        // ぼかした背景（_blurredMaster・_blurredBackdrop）は、ガラス調のコントロールが最初に求めたときに作る。
        // ガラス調のコントロールを置いていない画面では作らない
        private Image _backdropSource;
        private Bitmap _backdropMaster;
        private Bitmap _blurredMaster;
        private Bitmap _scaledBackdrop;
        private Bitmap _blurredBackdrop;
        private TextureBrush _blurredBackdropBrush;

        public GlassBackdropForm()
        {
            // 背景と半透明のコントロールを重ねて描くため、ちらつきを抑える
            DoubleBuffered = true;
        }

        /// <inheritdoc />
        public bool FillBlurredBackdrop(Graphics g, GraphicsPath path, Point origin)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (!EnsureBlurredBackdrop())
            {
                return false;
            }

            // ぼかした背景のうち、呼び出したコントロールが重なっている部分が出るように位置を合わせる
            _blurredBackdropBrush.ResetTransform();
            _blurredBackdropBrush.TranslateTransform(-origin.X, -origin.Y);
            g.FillPath(_blurredBackdropBrush, path);
            return true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!EnsureBackdropCache())
            {
                // 背景画像なし（または引き伸ばし以外の配置）の場合は標準の描画に任せる
                base.OnPaintBackground(e);
                return;
            }

            // 描き直す範囲だけを、画素をそのまま写す。半透明のコントロールが自分の下地として
            // ここを呼ぶときは範囲がそのコントロールの位置だけなので、全体を写すより大幅に軽い
            Rectangle area = Rectangle.Intersect(e.ClipRectangle, new Rectangle(Point.Empty, _scaledBackdrop.Size));
            if (area.IsEmpty)
            {
                return;
            }

            CompositingMode previousMode = e.Graphics.CompositingMode;
            e.Graphics.CompositingMode = CompositingMode.SourceCopy;
            e.Graphics.DrawImage(_scaledBackdrop, area, area, GraphicsUnit.Pixel);
            e.Graphics.CompositingMode = previousMode;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // 背景画像は画面の大きさに合わせて伸び縮みするので、その上に重なっている
            // 半透明のコントロールも描き直す（位置が変わらないコントロールは、そのままでは再描画されない）
            if (BackgroundImage != null)
            {
                Invalidate(true);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseBackdropCache();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 背景を描くための画像を用意する。すでに用意済みで、背景画像も画面の大きさも変わっていなければ何もしない。
        /// </summary>
        /// <returns>
        /// 用意できた場合は true。背景画像がない場合と、配置が <see cref="ImageLayout.Stretch"/> 以外の場合は false。
        /// </returns>
        private bool EnsureBackdropCache()
        {
            Image source = BackgroundImage;
            Size size = ClientSize;
            if (source == null || BackgroundImageLayout != ImageLayout.Stretch || size.Width <= 0 || size.Height <= 0)
            {
                ReleaseBackdropCache();
                return false;
            }

            if (_backdropSource != source)
            {
                // 背景画像が変わった（または初回）。時間のかかる元画像づくりは、ここで 1 回だけ行う
                ReleaseBackdropCache();
                _backdropSource = source;
                _backdropMaster = CreateBackdropMaster(source);
            }

            if (_scaledBackdrop == null || _scaledBackdrop.Size != size)
            {
                // ウィンドウの大きさが変わった。元画像から単純に写し取るだけなので軽い。
                // ぼかした背景の表示用もここで破棄され、次に求められたときに新しい大きさで作り直される
                ReleaseSizedBackdrop();
                _scaledBackdrop = Resample(_backdropMaster, size.Width, size.Height, InterpolationMode.NearestNeighbor);
            }
            return true;
        }

        /// <summary>
        /// ぼかした背景を描くための画像を用意する。すでに用意済みなら何もしない。
        /// </summary>
        /// <returns>用意できた場合は true。<see cref="EnsureBackdropCache"/> が false を返す場合は false。</returns>
        private bool EnsureBlurredBackdrop()
        {
            if (!EnsureBackdropCache())
            {
                return false;
            }

            if (_blurredMaster == null)
            {
                _blurredMaster = CreateBlurredMaster(_backdropSource);
            }
            if (_blurredBackdrop == null)
            {
                Size size = _scaledBackdrop.Size;
                _blurredBackdrop = Resample(_blurredMaster, size.Width, size.Height, InterpolationMode.NearestNeighbor);
                _blurredBackdropBrush = new TextureBrush(_blurredBackdrop, WrapMode.Clamp);
            }
            return true;
        }

        /// <summary>
        /// 背景の元画像を作る。画面いっぱいに表示しても足りる大きさ（上限あり）へ、なめらかに拡大または縮小する。
        /// </summary>
        private Bitmap CreateBackdropMaster(Image source)
        {
            // 幅は「背景画像の幅」と「画面の幅」の大きい方。ただし上限を超えないようにする
            int screenWidth = Screen.FromControl(this).Bounds.Width;
            int width = Math.Min(Math.Max(source.Width, screenWidth), BackdropMasterMaxWidth);

            // 高さは背景画像の縦横比に合わせる。縦長の画像でも上限を超えないようにする
            // （背景は画面の大きさへ引き伸ばして表示するので、ここで縦横比が変わっても見た目には影響しない）
            int height = (int)Math.Round((double)source.Height * width / source.Width);
            height = Math.Max(1, Math.Min(height, BackdropMasterMaxHeight));
            return Resample(source, width, height, InterpolationMode.HighQualityBicubic);
        }

        /// <summary>
        /// ぼかした背景の元画像を作る。小さく縮めてから引き伸ばすと、細部が失われて全体がぼける。
        /// 一度に引き伸ばさず途中の大きさを挟むことで、ぼけ方を滑らかにしている。
        /// </summary>
        private static Bitmap CreateBlurredMaster(Image source)
        {
            // 縮める大きさは、背景の元画像の上限に収めた大きさを基準にする。上限より大きな背景画像でも、
            // 表示したときのぼけの強さが弱くならず、縮めた画像も大きくなりすぎない
            int baseWidth = Math.Min(source.Width, BackdropMasterMaxWidth);
            int baseHeight = Math.Min(source.Height, BackdropMasterMaxHeight);
            int smallWidth = Math.Max(2, baseWidth / BackdropBlurDownscale);
            int smallHeight = Math.Max(2, baseHeight / BackdropBlurDownscale);

            // 引き伸ばした結果は上限に収める。途中の大きさも、最終の大きさを超えないようにする
            int finalWidth = Math.Min(smallWidth * BackdropBlurMasterScale, BlurredMasterMaxWidth);
            int finalHeight = Math.Min(smallHeight * BackdropBlurMasterScale, BlurredMasterMaxHeight);
            int stepScale = Math.Max(1, (int)Math.Round(Math.Sqrt(BackdropBlurMasterScale)));
            int mediumWidth = Math.Min(smallWidth * stepScale, finalWidth);
            int mediumHeight = Math.Min(smallHeight * stepScale, finalHeight);

            using (Bitmap small = Resample(source, smallWidth, smallHeight, InterpolationMode.HighQualityBicubic))
            using (Bitmap medium = Resample(small, mediumWidth, mediumHeight, InterpolationMode.HighQualityBicubic))
            {
                return Resample(medium, finalWidth, finalHeight, InterpolationMode.HighQualityBicubic);
            }
        }

        /// <summary>作り置きをすべて破棄する。</summary>
        private void ReleaseBackdropCache()
        {
            ReleaseSizedBackdrop();
            if (_backdropMaster != null)
            {
                _backdropMaster.Dispose();
                _backdropMaster = null;
            }
            if (_blurredMaster != null)
            {
                _blurredMaster.Dispose();
                _blurredMaster = null;
            }
            _backdropSource = null;
        }

        /// <summary>作り置きのうち、ウィンドウの大きさに合わせて作った表示用の画像だけを破棄する。</summary>
        private void ReleaseSizedBackdrop()
        {
            if (_blurredBackdropBrush != null)
            {
                _blurredBackdropBrush.Dispose();
                _blurredBackdropBrush = null;
            }
            if (_blurredBackdrop != null)
            {
                _blurredBackdrop.Dispose();
                _blurredBackdrop = null;
            }
            if (_scaledBackdrop != null)
            {
                _scaledBackdrop.Dispose();
                _scaledBackdrop = null;
            }
        }

        /// <summary>
        /// <paramref name="source"/> を指定の大きさへ拡大または縮小した画像を作る。呼び出し側で Dispose すること。
        /// </summary>
        private static Bitmap Resample(Image source, int width, int height, InterpolationMode interpolation)
        {
            var result = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(result))
            using (var attributes = new ImageAttributes())
            {
                // 端の画素を折り返して参照させ、拡大縮小したときに画像の縁が薄くならないようにする
                attributes.SetWrapMode(WrapMode.TileFlipXY);
                g.InterpolationMode = interpolation;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawImage(
                    source,
                    new Rectangle(0, 0, width, height),
                    0,
                    0,
                    source.Width,
                    source.Height,
                    GraphicsUnit.Pixel,
                    attributes);
            }
            return result;
        }
    }
}
