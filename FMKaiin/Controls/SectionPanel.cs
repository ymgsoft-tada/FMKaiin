using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 色付きの見出し帯を持つ角丸のパネル。
    /// 見出し帯の下が子コントロールの配置領域になる（<see cref="DisplayRectangle"/> から見出し帯を除外しているため、
    /// Dock や Padding は見出し帯を意識せずに指定できる）。
    /// 寸法のプロパティはすべて 96 DPI 基準のピクセルで指定する。
    /// 色のプロパティ（FillColor・HeaderColor・BorderColor）には半透明色も指定できる。
    /// BackColor を Transparent にすると親コントロールの描画内容が下地になるので、
    /// 半透明の FillColor と組み合わせて、後ろが透けるガラスのようなパネルにできる。
    /// <see cref="ShadowSize"/> を指定すると、パネルの下に柔らかい影を落とす。影はコントロールの領域内に描くので、
    /// パネル自体はコントロールより一回り小さくなる（上と左右は影の広がりの分、下は ShadowSize の分だけ内側）。
    /// </summary>
    public class SectionPanel : Panel
    {
        /// <summary>飾り枠付きのとき、枠の一辺に対する中のアイコンの一辺の比率。</summary>
        private const float RingedIconRatio = 0.54f;

        private static readonly Color DefaultHeaderColor = Color.FromArgb(24, 125, 194);
        private static readonly Color DefaultHeaderForeColor = Color.White;
        private static readonly Color DefaultBorderColor = Color.FromArgb(186, 197, 211);
        private static readonly Color DefaultShadowColor = Color.FromArgb(72, 56, 48);

        private string _title = string.Empty;
        private MenuIconKind _headerIcon = MenuIconKind.None;
        private Color _headerColor = DefaultHeaderColor;
        private Color _headerForeColor = DefaultHeaderForeColor;
        private Color _headerIconColor = Color.Empty;
        private Color _borderColor = DefaultBorderColor;
        private int _headerHeight = 44;
        private int _headerIconSize = 27;
        private int _headerTextGap = 14;
        private int _cornerRadius = 5;
        private bool _headerIconRing;
        private Color _fillColor = Color.Empty;
        private Color _borderGradientColor = Color.Empty;
        private bool _backdropBlur;
        private bool _headerOutlined;
        private Color _headerGradientColor = Color.Empty;
        private Color _shadowColor = DefaultShadowColor;
        private int _shadowSize;

        // 背景が透明のときに使う、描画結果の作り置き。_surfaceDirty が true の間は古いので、次に使うときに描き直す
        private Bitmap _surface;
        private bool _surfaceDirty = true;        private int _headerIconLeft = 16;
        private float _headerIconLineWeight = 1f;

        public SectionPanel()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
        }

        /// <summary>見出し帯に表示する文字列。フォントは <see cref="Control.Font"/> を使う。</summary>
        [Category("Appearance")]
        [DefaultValue("")]
        [Description("見出し帯に表示する文字列。フォントは Font を使う。")]
        public string Title
        {
            get { return _title; }
            set { _title = value ?? string.Empty; Invalidate(); }
        }

        /// <summary>見出し帯に表示するアイコン。</summary>
        [Category("Appearance")]
        [DefaultValue(MenuIconKind.None)]
        [Description("見出し帯に表示するアイコン。")]
        public MenuIconKind HeaderIcon
        {
            get { return _headerIcon; }
            set { _headerIcon = value; Invalidate(); }
        }

        /// <summary>見出し帯の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "24, 125, 194")]
        [Description("見出し帯の色。")]
        public Color HeaderColor
        {
            get { return _headerColor; }
            set { _headerColor = value; Invalidate(); }
        }

        /// <summary>見出し帯の文字列の色。<see cref="HeaderIconColor"/> が空の場合はアイコンもこの色で描く。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "White")]
        [Description("見出し帯の文字列の色。HeaderIconColor が空の場合はアイコンもこの色で描く。")]
        public Color HeaderForeColor
        {
            get { return _headerForeColor; }
            set { _headerForeColor = value; Invalidate(); }
        }

        /// <summary>
        /// 見出しアイコン（飾り枠を含む）の色。空（<see cref="Color.Empty"/>）の場合は <see cref="HeaderForeColor"/> と同じ色で描く。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("見出しアイコン（飾り枠を含む）の色。空の場合は HeaderForeColor と同じ色で描く。")]
        public Color HeaderIconColor
        {
            get { return _headerIconColor; }
            set { _headerIconColor = value; Invalidate(); }
        }

        /// <summary>見出しアイコンを円形の飾り枠で囲むか。囲む場合、アイコンは枠の中に小さく描く。</summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("見出しアイコンを円形の飾り枠で囲むか。囲む場合、アイコンは枠の中に小さく描く。")]
        public bool HeaderIconRing
        {
            get { return _headerIconRing; }
            set { _headerIconRing = value; Invalidate(); }
        }

        /// <summary>見出しアイコンの一辺。飾り枠付きの場合は枠の直径。見出し帯の高さに収まらない場合は自動で縮む。</summary>
        [Category("Appearance")]
        [DefaultValue(27)]
        [Description("見出しアイコンの一辺。飾り枠付きの場合は枠の直径。見出し帯の高さに収まらない場合は自動で縮む。")]
        public int HeaderIconSize
        {
            get { return _headerIconSize; }
            set { _headerIconSize = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>見出しアイコンと見出し文字列の間隔。</summary>
        [Category("Appearance")]
        [DefaultValue(14)]
        [Description("見出しアイコンと見出し文字列の間隔。")]
        public int HeaderTextGap
        {
            get { return _headerTextGap; }
            set { _headerTextGap = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>
        /// 本体の塗り色。空（<see cref="Color.Empty"/>）の場合は <see cref="Control.BackColor"/> で塗る。
        /// 後ろが透けるパネルにするには、BackColor を Transparent にしたうえで、ここに半透明色を指定する。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("本体の塗り色。空の場合は BackColor で塗る。後ろが透けるパネルにするには、BackColor を Transparent にして、ここに半透明色（例: 48, 255, 255, 255）を指定する。")]
        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value; Invalidate(); }
        }

        /// <summary>
        /// 枠線をグラデーションにする場合の、右下側の色。空（<see cref="Color.Empty"/>）の場合は <see cref="BorderColor"/> の単色。
        /// 指定すると、左上が BorderColor、右下がこの色になり、左上から光が当たっているような立体感が出る。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("枠線をグラデーションにする場合の、右下側の色。空の場合は BorderColor の単色。指定すると左上が BorderColor、右下がこの色になる。")]
        public Color BorderGradientColor
        {
            get { return _borderGradientColor; }
            set { _borderGradientColor = value; Invalidate(); }
        }

        /// <summary>
        /// パネルの後ろにある背景を、ぼかして映すか（すりガラスの表現）。
        /// 親フォームが <see cref="IBackdropBlurSource"/> を実装している場合にだけ効果がある。
        /// ぼかした背景の上に <see cref="FillColor"/> と見出し帯の色が重なるので、どちらも半透明色にしておくこと。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("パネルの後ろにある背景を、ぼかして映すか（すりガラスの表現）。親フォームが対応している場合にだけ効果がある。FillColor と HeaderColor は半透明色にしておくこと。")]
        public bool BackdropBlur
        {
            get { return _backdropBlur; }
            set { _backdropBlur = value; Invalidate(); }
        }

        /// <summary>
        /// 見出し帯のまわりにも枠線を残し、帯と本体の境界に線を引くか。
        /// 既定（false）では、不透明な帯は枠線を帯の色で覆い、帯が端まで塗られて見える（濃い色の帯向き）。
        /// 淡い色の帯にするときは true にすると、パネル全体が 1 本の枠線で囲まれて輪郭がはっきりする。
        /// 半透明の帯は、この設定に関係なく枠線を残す。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("見出し帯のまわりにも枠線を残し、帯と本体の境界に線を引くか。淡い色の帯にするときは true にすると輪郭がはっきりする。")]
        public bool HeaderOutlined
        {
            get { return _headerOutlined; }
            set { _headerOutlined = value; Invalidate(); }
        }

        /// <summary>
        /// 見出し帯を左右方向のグラデーションにする場合の、右端の色。空（<see cref="Color.Empty"/>）の場合は <see cref="HeaderColor"/> の単色。
        /// 指定すると、左端が HeaderColor、右端がこの色になる。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("見出し帯を左右方向のグラデーションにする場合の、右端の色。空の場合は HeaderColor の単色。指定すると左端が HeaderColor、右端がこの色になる。")]
        public Color HeaderGradientColor
        {
            get { return _headerGradientColor; }
            set { _headerGradientColor = value; Invalidate(); }
        }

        /// <summary>
        /// パネルの下に影が伸びる長さ。0（既定）にすると影なしになり、パネルはコントロールいっぱいに描かれる。
        /// 1 以上にすると、パネルはその分だけコントロールより小さく描かれ、子コントロールの配置領域も同じだけ狭くなる。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("パネルの下に影が伸びる長さ。0 にすると影なしになる。1 以上にすると、パネルはその分だけコントロールより小さく描かれる。")]
        public int ShadowSize
        {
            get { return _shadowSize; }
            set
            {
                int size = Math.Max(0, value);
                if (_shadowSize == size)
                {
                    return;
                }
                _shadowSize = size;
                PerformLayout();
                Invalidate();
            }
        }

        /// <summary>影の色。濃さは自動で薄められるので、ここでは色味だけを指定する。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "72, 56, 48")]
        [Description("影の色。濃さは自動で薄められるので、ここでは色味だけを指定する。")]
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set { _shadowColor = value; Invalidate(); }
        }

        /// <summary>左端から見出しアイコンまでの間隔。アイコンがない場合は見出し文字列までの間隔になる。</summary>
        [Category("Appearance")]
        [DefaultValue(16)]
        [Description("左端から見出しアイコンまでの間隔。アイコンがない場合は見出し文字列までの間隔になる。")]
        public int HeaderIconLeft
        {
            get { return _headerIconLeft; }
            set { _headerIconLeft = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>見出しアイコンの線の太さの倍率。1 が標準で、細い線にしたい場合は 1 より小さくする。</summary>
        [Category("Appearance")]
        [DefaultValue(1f)]
        [Description("見出しアイコンの線の太さの倍率。1 が標準で、細い線にしたい場合は 1 より小さくする。")]
        public float HeaderIconLineWeight
        {
            get { return _headerIconLineWeight; }
            set
            {
                if (value <= 0f || float.IsNaN(value) || float.IsInfinity(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "線の太さの倍率は 0 より大きい値で指定してください。");
                }
                _headerIconLineWeight = value;
                Invalidate();
            }
        }

        /// <summary>パネルの角の半径。0 にすると角丸なしになる。</summary>
        [Category("Appearance")]
        [DefaultValue(5)]
        [Description("パネルの角の半径。0 にすると角丸なしになる。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>パネルの枠線の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "186, 197, 211")]
        [Description("パネルの枠線の色。")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        /// <summary>見出し帯の高さ。</summary>
        [Category("Appearance")]
        [DefaultValue(44)]
        [Description("見出し帯の高さ。")]
        public int HeaderHeight
        {
            get { return _headerHeight; }
            set
            {
                int height = Math.Max(0, value);
                if (_headerHeight == height)
                {
                    return;
                }
                _headerHeight = height;
                PerformLayout();
                Invalidate();
            }
        }

        /// <summary>
        /// 子コントロールの配置領域。見出し帯の分だけ上端を下げ、影を付けている場合は影の分だけ内側にする。
        /// </summary>
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                DropShadow shadow = Shadow;
                int headerHeight = ScaledHeaderHeight;
                rect.X += shadow.SideInset;
                rect.Width = Math.Max(0, rect.Width - shadow.SideInset * 2);
                rect.Y += shadow.SideInset + headerHeight;
                rect.Height = Math.Max(0, rect.Height - shadow.SideInset - shadow.Size - headerHeight);
                return rect;
            }
        }

        /// <summary>96 DPI を 1 とした表示倍率。描画に使う固定寸法へ掛ける。</summary>
        private float DpiScale
        {
            get { return LayoutScale.Of(this); }
        }

        /// <summary>現在の表示倍率での、影の寸法。</summary>
        private DropShadow Shadow
        {
            get { return new DropShadow(_shadowSize, DpiScale); }
        }

        /// <summary>見出し帯の下に本体が透けて見えるか（見出し帯の色に半透明色を使っているか）。</summary>
        private bool HeaderIsTranslucent
        {
            get { return _headerColor.A < 255 || (!_headerGradientColor.IsEmpty && _headerGradientColor.A < 255); }
        }

        private int ScaledHeaderHeight
        {
            get { return (int)Math.Round(_headerHeight * DpiScale); }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // 背景が不透明な通常のパネルは、そのまま描く
            if (BackColor.A == 255)
            {
                PaintSurface(e);
                DrawTitle(e.Graphics);
                return;
            }

            // 背景が透明のパネルは、描画結果を作り置きしておき、必要な範囲だけを写す。
            // 透明のパネルは、上に載っている子コントロールが自分の下地として何度も描かせにくる
            // （1 回の再描画で子の数だけ）。そのたびに親の内容・ぼかした背景・帯・文字を描き直すと重いので、写すだけにする
            Bitmap surface = GetSurface();
            if (surface == null)
            {
                PaintSurface(e);
                DrawTitle(e.Graphics);
                return;
            }

            Rectangle area = Rectangle.Intersect(e.ClipRectangle, new Rectangle(Point.Empty, surface.Size));
            if (area.IsEmpty)
            {
                return;
            }

            CompositingMode previousMode = e.Graphics.CompositingMode;
            e.Graphics.CompositingMode = CompositingMode.SourceCopy;
            e.Graphics.DrawImage(surface, area, area, GraphicsUnit.Pixel);
            e.Graphics.CompositingMode = previousMode;

            // 見出し文字列は作り置きに含めず、写した後にここで描く。文字は下地の色となじませて描かれるので、
            // 作り置き用の画像の上に描くと、縁が黒ずんでしまう
            if (area.Top < Shadow.SideInset + ScaledHeaderHeight)
            {
                DrawTitle(e.Graphics);
            }
        }

        /// <summary>
        /// 作り置きしたパネルの描画結果を返す。古くなっている場合は、ここで描き直す。
        /// 作れない状態（大きさが 0、ウィンドウ未作成）の場合は null。
        /// </summary>
        private Bitmap GetSurface()
        {
            if (Width <= 0 || Height <= 0 || !IsHandleCreated)
            {
                return null;
            }
            if (_surface != null && !_surfaceDirty && _surface.Size == Size)
            {
                return _surface;
            }

            if (_surface == null || _surface.Size != Size)
            {
                if (_surface != null)
                {
                    _surface.Dispose();
                }

                // 透明度を持たない形式にする。文字は Windows の描画機能（GDI）で描いており、
                // 透明度を持つ形式だと、文字の部分が透明として扱われて抜けてしまう
                _surface = new Bitmap(Width, Height, PixelFormat.Format32bppRgb);
            }

            using (Graphics g = Graphics.FromImage(_surface))
            using (var args = new PaintEventArgs(g, new Rectangle(Point.Empty, _surface.Size)))
            {
                PaintSurface(args);
            }
            _surfaceDirty = false;
            return _surface;
        }

        /// <summary>
        /// パネルの見た目のうち、見出し文字列以外（親の描画内容、本体、見出し帯、枠線、見出しのアイコン）を描く。
        /// 見出し文字列は <see cref="DrawTitle"/> で描く。
        /// </summary>
        private void PaintSurface(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float scale = DpiScale;
            int headerHeight = ScaledHeaderHeight;

            if (BackColor.A < 255)
            {
                // 背景が透明の場合は、親コントロールの描画内容をそのまま下地にする（標準の透明背景の仕組み）。
                // この上に半透明の FillColor を重ねると、後ろが透けたガラスのような見た目になる
                base.OnPaintBackground(e);
            }
            else
            {
                // 角丸の外側が親と同じ見た目になるよう、先に全体を親と同じ見た目にしてから、上に本体を描く
                PaintParentAppearance(g);
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float penWidth = Math.Max(1f, (float)Math.Round(scale));
            // 半透明の帯、または枠線を残す指定の帯は、帯の上にも枠線を描き、本体との境界にも線を引く
            bool translucentHeader = headerHeight > 0 && (HeaderIsTranslucent || _headerOutlined);

            // 影を付けている場合、パネルの本体はコントロールより一回り小さい（影なしならコントロールいっぱい）
            DropShadow shadow = Shadow;
            RectangleF bounds = shadow.GetSurfaceBounds(Width, Height);
            if (bounds.Width <= 0f || bounds.Height <= 0f)
            {
                return;
            }

            float radius = _cornerRadius * scale;
            float headerBottom = bounds.Top + headerHeight;
            using (GraphicsPath outline = PaintHelper.CreateRoundedRectangle(bounds, radius))
            using (var bodyBrush = new SolidBrush(_fillColor.IsEmpty ? BackColor : _fillColor))
            using (Pen borderPen = PaintHelper.CreateBorderPen(bounds, _borderColor, _borderGradientColor, penWidth))
            {
                if (shadow.Size > 0)
                {
                    // 影はパネルの外側にだけ描く。パネルが半透明でも、影がパネル越しに透けて見えないようにする
                    GraphicsState beforeShadow = g.Save();
                    g.SetClip(outline, CombineMode.Exclude);
                    shadow.Draw(g, bounds, radius, _shadowColor, 1f);
                    g.Restore(beforeShadow);
                }

                if (_backdropBlur)
                {
                    PaintBlurredBackdrop(g, outline);
                }
                g.FillPath(bodyBrush, outline);
                if (!translucentHeader)
                {
                    g.DrawPath(borderPen, outline);
                }

                // 見出し帯は外形と同じパスを上側だけ切り出して塗り、角の丸みを一致させる
                if (headerHeight > 0)
                {
                    GraphicsState state = g.Save();

                    // 切り出す範囲は、枠線の太さの分だけ上と左右へ広げておく（枠線を帯の色で上書きするときに欠けないようにする）
                    g.SetClip(new RectangleF(
                        bounds.Left - penWidth,
                        bounds.Top - penWidth,
                        bounds.Width + 1f + penWidth * 2f,
                        headerHeight + penWidth));
                    using (Brush headerBrush = CreateHeaderBrush(bounds))
                    {
                        g.FillPath(headerBrush, outline);

                        if (!translucentHeader)
                        {
                            // 不透明な帯は、帯の範囲の枠線を帯と同じ色で上書きして、端まで帯の色に見せる
                            using (var headerPen = new Pen(headerBrush, penWidth))
                            {
                                g.DrawPath(headerPen, outline);
                            }
                        }
                    }
                    g.Restore(state);
                }

                if (translucentHeader)
                {
                    // 半透明の帯では枠線を帯の上にも残し、帯と本体の境界にも同じ色の線を引く
                    g.DrawPath(borderPen, outline);
                    g.DrawLine(borderPen, bounds.Left + penWidth, headerBottom, bounds.Right - penWidth, headerBottom);
                }
            }

            if (headerHeight <= 0)
            {
                return;
            }

            float iconSide = Math.Min(_headerIconSize * scale, headerHeight);
            if (_headerIcon != MenuIconKind.None)
            {
                Color iconColor = _headerIconColor.IsEmpty ? _headerForeColor : _headerIconColor;
                var iconBounds = new RectangleF(
                    bounds.Left + _headerIconLeft * scale,
                    bounds.Top + (headerHeight - iconSide) / 2f,
                    iconSide,
                    iconSide);
                if (_headerIconRing)
                {
                    MenuIconPainter.DrawRing(g, iconBounds, iconColor);

                    float inset = iconSide * (1f - RingedIconRatio) / 2f;
                    MenuIconPainter.Draw(g, _headerIcon, RectangleF.Inflate(iconBounds, -inset, -inset), iconColor, _headerIconLineWeight);
                }
                else
                {
                    MenuIconPainter.Draw(g, _headerIcon, iconBounds, iconColor, _headerIconLineWeight);
                }
            }
        }

        /// <summary>
        /// 見出し帯を塗るブラシを作る。呼び出し側で Dispose すること。
        /// <see cref="HeaderGradientColor"/> を指定している場合は、パネルの左端から右端へ色が変わるグラデーションになる。
        /// </summary>
        private Brush CreateHeaderBrush(RectangleF bounds)
        {
            if (_headerGradientColor.IsEmpty || _headerGradientColor.ToArgb() == _headerColor.ToArgb() || bounds.Width <= 0f)
            {
                return new SolidBrush(_headerColor);
            }

            var brush = new LinearGradientBrush(
                new PointF(bounds.Left, 0f),
                new PointF(bounds.Right, 0f),
                _headerColor,
                _headerGradientColor);

            // 左端・右端の 1 画素に反対側の色が回り込まないようにする
            brush.WrapMode = WrapMode.TileFlipXY;
            return brush;
        }

        /// <summary>
        /// パネル全体を、親コントロールと同じ見た目で塗る（背景が不透明のパネルの下地）。
        /// 親の背景が単色ならその色で塗り、親の背景が透明（さらに後ろの背景を透かしている）なら親の描画内容を写す。
        /// この上に角丸の本体を描くので、角丸で欠ける四隅にだけ、ここで塗った内容が残る。
        /// </summary>
        private void PaintParentAppearance(Graphics g)
        {
            // 処理は、アイコンやボタンと共通のものを使う
            PaintHelper.PaintParentAppearance(this, g, InvokePaintBackground, InvokePaint);
        }

        /// <summary>
        /// 見出し帯の文字列を描く。アイコンがある場合は、その右に間隔を空けて描く。
        /// </summary>
        private void DrawTitle(Graphics g)
        {
            int headerHeight = ScaledHeaderHeight;
            if (headerHeight <= 0 || _title.Length == 0)
            {
                return;
            }

            float scale = DpiScale;
            RectangleF bounds = Shadow.GetSurfaceBounds(Width, Height);
            float textLeft = bounds.Left + _headerIconLeft * scale;
            if (_headerIcon != MenuIconKind.None)
            {
                float iconSide = Math.Min(_headerIconSize * scale, headerHeight);
                textLeft += iconSide + _headerTextGap * scale;
            }

            int left = (int)Math.Round(textLeft);
            int top = (int)Math.Round(bounds.Top);
            int right = (int)Math.Round(bounds.Right) + 1;
            var textBounds = new Rectangle(left, top, Math.Max(0, right - left), headerHeight);
            TextRenderer.DrawText(
                g,
                _title,
                Font,
                textBounds,
                _headerForeColor,
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine |
                TextFormatFlags.NoPadding |
                TextFormatFlags.NoPrefix |
                TextFormatFlags.EndEllipsis);
        }

        /// <summary>
        /// パネルの形（<paramref name="outline"/>）の範囲に、後ろの背景をぼかしたものを描く。
        /// ぼかした背景を持っているのは親フォームなので、フォームに描いてもらう。
        /// </summary>
        private void PaintBlurredBackdrop(Graphics g, GraphicsPath outline)
        {
            var source = FindForm() as IBackdropBlurSource;
            if (source == null || !IsHandleCreated)
            {
                return;
            }

            // このパネルの左上が、フォームの表示領域のどこに当たるかを求めて渡す
            Point origin = ((Control)source).PointToClient(PointToScreen(Point.Empty));
            source.FillBlurredBackdrop(g, outline, origin);
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            // このパネル自身が描き直しを求められたら、作り置きも古くなったものとして扱う。
            // 大きさ・位置・プロパティの変更、親の描き直し（背景が透明のコントロールへは親から伝わる）はすべてここを通る。
            // 子コントロールだけの描き直し（マウスオーバーなど）はここを通らないので、作り置きをそのまま使える
            _surfaceDirty = true;
            base.OnInvalidated(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _surfaceDirty = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _surface != null)
            {
                _surface.Dispose();
                _surface = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Invalidate();
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }
    }
}
