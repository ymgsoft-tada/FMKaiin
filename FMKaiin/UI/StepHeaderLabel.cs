using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;
using ComponentSUI;

namespace App.Controls
{
    /// <summary>
    /// 作業の工程を示す見出し。番号を入れた丸（<see cref="StepText"/>）と工程名（<see cref="Control.Text"/>）を横並びで表示し、
    /// その右に、次の工程へつなぐ矢印付きの線（<see cref="ConnectorColor"/>）を引ける。
    /// 工程名は、1 文字目を <see cref="Control.Font"/> の大きさで、2 文字目以降を少し小さく描く
    /// （<see cref="TrailingTextScale"/>）。文字の下のラインはそろえる。
    /// ボタンと見分けが付くよう、枠・塗り・影は持たない。表示するだけで、クリックやフォーカスは受け付けない。
    /// 背景は親コントロールの描画内容がそのまま見える（親の BackColor が Transparent の場合）。
    /// 寸法のプロパティはすべて 96 DPI 基準のピクセルで指定する。
    /// </summary>
    public class StepHeaderLabel : Control
    {
        /// <summary>丸の中の番号の文字の大きさ。工程名の文字の大きさに対する倍率。</summary>
        private const float StepFontRatio = 0.8f;
        /// <summary>工程名と、次の工程へつなぐ線の間隔。</summary>
        private const float ConnectorGap = 12f;
        /// <summary>次の工程へつなぐ線の太さ。</summary>
        private const float ConnectorWidth = 1.6f;
        /// <summary>線の右端に付ける矢印の大きさ（矢印の先から羽の端までの、縦横それぞれの長さ）。</summary>
        private const float ArrowHeadSize = 5f;

        private static readonly Color DefaultBadgeColor = Color.FromArgb(30, 96, 160);
        private static readonly Color DefaultBadgeForeColor = Color.White;

        // 工程名は、上下の位置を自分で決めて描く（1 文字目と 2 文字目以降で、文字の下のラインをそろえるため）
        private const TextFormatFlags LabelFlags =
            TextFormatFlags.Left |
            TextFormatFlags.Top |
            TextFormatFlags.SingleLine |
            TextFormatFlags.NoPadding |
            TextFormatFlags.NoPrefix;

        private static readonly Size MeasureArea = new Size(int.MaxValue, int.MaxValue);

        /// <summary>
        /// 字の下のラインの目安を測るときに描く、基準の文字。
        /// 1 つ目は日本語用（下端に横線があり、枠いっぱいに書かれる漢字）、2 つ目は英数字用（下端が平らな大文字）。
        /// 漢字やかなの下端は、英数字の下端より少し下にあるので、分けて持っている。
        /// </summary>
        private static readonly string[] BottomReferenceTexts = { "国", "H" };

        /// <summary>字の下端を測るときに、画素を「字の一部」とみなす暗さ。赤・緑・青の合計がこれ未満なら字とみなす（白は 765、黒は 0）。</summary>
        private const int InkThreshold = 384;

        /// <summary>測った結果（文字の上端から字の下端までの高さ）の覚え書き。キーはフォント名・大きさ・スタイル・文字。</summary>
        private static readonly Dictionary<string, int> InkBottomCache = new Dictionary<string, int>();

        private float _trailingTextScale = 0.9f;
        private string _stepText = string.Empty;
        private Color _badgeColor = DefaultBadgeColor;
        private Color _badgeForeColor = DefaultBadgeForeColor;
        private Color _connectorColor = Color.Empty;
        private int _badgeSize = 26;
        private int _badgeLeft;
        private int _textGap = 10;

        public StepHeaderLabel()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                // 親の BackColor が Transparent のときに、親の描画内容の上へ見出しだけを描けるようにする
                ControlStyles.SupportsTransparentBackColor,
                true);
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
        }

        /// <summary>丸の中に表示する番号（例: 01）。空の場合は丸を描かず、工程名だけを表示する。</summary>
        [Category("Appearance")]
        [DefaultValue("")]
        [Description("丸の中に表示する番号（例: 01）。空の場合は丸を描かず、工程名だけを表示する。")]
        public string StepText
        {
            get { return _stepText; }
            set { _stepText = value ?? string.Empty; Invalidate(); }
        }

        /// <summary>
        /// 工程名の 2 文字目以降の文字の大きさ。1 文字目（<see cref="Control.Font"/> の大きさ）に対する倍率。
        /// 1 にすると、全部の文字が同じ大きさになる。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(0.9f)]
        [Description("工程名の 2 文字目以降の文字の大きさ。1 文字目に対する倍率（1 で同じ大きさ）。文字の下のラインは 1 文字目にそろう。")]
        public float TrailingTextScale
        {
            get { return _trailingTextScale; }
            set
            {
                if (value <= 0f || float.IsNaN(value) || float.IsInfinity(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "文字の大きさの倍率は 0 より大きい値で指定してください。");
                }
                _trailingTextScale = value;
                Invalidate();
            }
        }

        /// <summary>番号を入れる丸の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "30, 96, 160")]
        [Description("番号を入れる丸の色。")]
        public Color BadgeColor
        {
            get { return _badgeColor; }
            set { _badgeColor = value; Invalidate(); }
        }

        /// <summary>丸の中の番号の文字色。工程名の文字色は ForeColor で指定する。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "White")]
        [Description("丸の中の番号の文字色。工程名の文字色は ForeColor で指定する。")]
        public Color BadgeForeColor
        {
            get { return _badgeForeColor; }
            set { _badgeForeColor = value; Invalidate(); }
        }

        /// <summary>
        /// 次の工程へつなぐ線の色。空（<see cref="Color.Empty"/>）の場合は線を引かない（最後の工程で使う）。
        /// 指定すると、工程名の右からコントロールの右端まで線を引き、右端を矢印にする。半透明色も指定できる。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("次の工程へつなぐ線の色。空の場合は線を引かない。指定すると、工程名の右から右端まで線を引き、右端を矢印にする。")]
        public Color ConnectorColor
        {
            get { return _connectorColor; }
            set { _connectorColor = value; Invalidate(); }
        }

        /// <summary>番号を入れる丸の直径。コントロールの高さに収まらない場合は自動で縮む。</summary>
        [Category("Appearance")]
        [DefaultValue(26)]
        [Description("番号を入れる丸の直径。コントロールの高さに収まらない場合は自動で縮む。")]
        public int BadgeSize
        {
            get { return _badgeSize; }
            set { _badgeSize = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>左端から丸までの間隔。丸を描かない場合は工程名までの間隔になる。</summary>
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("左端から丸までの間隔。丸を描かない場合は工程名までの間隔になる。")]
        public int BadgeLeft
        {
            get { return _badgeLeft; }
            set { _badgeLeft = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>丸と工程名の間隔。</summary>
        [Category("Appearance")]
        [DefaultValue(10)]
        [Description("丸と工程名の間隔。")]
        public int TextGap
        {
            get { return _textGap; }
            set { _textGap = Math.Max(0, value); Invalidate(); }
        }

        protected override Size DefaultSize
        {
            get { return new Size(200, 32); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float scale = LayoutScale.Of(this);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 番号を入れた丸
            float textLeft = _badgeLeft * scale;
            if (_stepText.Length > 0)
            {
                float badgeSide = Math.Max(0f, Math.Min(_badgeSize * scale, Height - 1));
                var badgeBounds = new RectangleF(_badgeLeft * scale, (Height - 1 - badgeSide) / 2f, badgeSide, badgeSide);
                using (var badgeBrush = new SolidBrush(_badgeColor))
                {
                    g.FillEllipse(badgeBrush, badgeBounds);
                }

                using (var stepFont = new Font(Font.FontFamily, Font.Size * StepFontRatio, Font.Style, Font.Unit))
                {
                    TextRenderer.DrawText(
                        g,
                        _stepText,
                        stepFont,
                        Rectangle.Round(badgeBounds),
                        _badgeForeColor,
                        TextFormatFlags.HorizontalCenter |
                        TextFormatFlags.VerticalCenter |
                        TextFormatFlags.SingleLine |
                        TextFormatFlags.NoPadding |
                        TextFormatFlags.NoPrefix);
                }
                textLeft = badgeBounds.Right + _textGap * scale;
            }

            // 工程名。次の工程へつなぐ線と重ならないよう、文字列の幅だけを使う
            int left = (int)Math.Round(textLeft);
            int textWidth = DrawLabelText(g, left);

            if (!_connectorColor.IsEmpty)
            {
                float connectorLeft = left + textWidth + (textWidth > 0 ? ConnectorGap * scale : 0f);
                DrawConnector(g, connectorLeft, scale);
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// 工程名を <paramref name="left"/> の位置から描き、描いた幅を返す。
        /// 1 文字目は <see cref="Control.Font"/> の大きさで上下中央に置き、2 文字目以降は
        /// <see cref="TrailingTextScale"/> 倍の大きさで、字の下のラインを 1 文字目にそろえて描く。
        /// 幅に収まらない場合は、2 文字目以降の末尾を「…」で省略する。
        /// </summary>
        private int DrawLabelText(Graphics g, int left)
        {
            string text = Text;
            int available = Width - left;
            if (string.IsNullOrEmpty(text) || available <= 0)
            {
                return 0;
            }

            Color color = Enabled ? ForeColor : SystemColors.GrayText;

            // 1 文字目（結合文字やサロゲート ペアは、まとめて 1 文字として扱う）
            string head = StringInfo.GetNextTextElement(text, 0);
            string tail = text.Substring(head.Length);
            Size headSize = TextRenderer.MeasureText(g, head, Font, MeasureArea, LabelFlags);
            int headTop = (Height - headSize.Height) / 2;
            int headWidth = Math.Min(headSize.Width, available);
            TextRenderer.DrawText(g, head, Font, new Rectangle(left, headTop, headWidth, headSize.Height), color, LabelFlags);
            if (tail.Length == 0 || headWidth >= available)
            {
                return headWidth;
            }

            // 2 文字目以降
            using (var tailFont = new Font(Font.FontFamily, Font.Size * _trailingTextScale, Font.Style, Font.Unit))
            {
                Size tailSize = TextRenderer.MeasureText(g, tail, tailFont, MeasureArea, LabelFlags);

                // 文字の上端から字の下のラインまでの高さは、文字の大きさによって違う。
                // その差の分だけ下げて描くと、1 文字目と字の下のラインがそろう
                string reference = ChooseBottomReference(Font, head);
                int tailTop = headTop + GetBottomLine(Font, head, reference) - GetBottomLine(tailFont, tail, reference);
                int tailWidth = Math.Min(tailSize.Width, available - headWidth);
                TextRenderer.DrawText(
                    g,
                    tail,
                    tailFont,
                    new Rectangle(left + headWidth, tailTop, tailWidth, tailSize.Height),
                    color,
                    LabelFlags | TextFormatFlags.EndEllipsis);
                return headWidth + tailWidth;
            }
        }

        /// <summary>
        /// 1 文字目（<paramref name="head"/>）の下端にいちばん近い基準の文字を、<see cref="BottomReferenceTexts"/> から選ぶ。
        /// 1 文字目が漢字やかななら日本語用、英数字なら英数字用の基準になる。
        /// </summary>
        private static string ChooseBottomReference(Font font, string head)
        {
            int headBottom = GetInkBottom(font, head);
            string closest = BottomReferenceTexts[0];
            int closestDistance = int.MaxValue;
            foreach (string reference in BottomReferenceTexts)
            {
                int distance = Math.Abs(headBottom - GetInkBottom(font, reference));
                if (distance < closestDistance)
                {
                    closest = reference;
                    closestDistance = distance;
                }
            }
            return closest;
        }

        /// <summary>
        /// <paramref name="text"/> を <paramref name="font"/> で描いたときの、文字の上端から「字の下のライン」までの高さ（ピクセル）を返す。
        /// </summary>
        /// <remarks>
        /// フォントの寸法（アセント）から計算する方法は使わず、文字を実際に描いて下端の画素を測る。
        /// Windows は字の位置を画素単位に丸めて描くので、寸法から計算した位置とは、文字の大きさによって 1 ピクセル食い違うため。
        /// まず基準の文字（<paramref name="referenceText"/>）の下端を測り、それを下のラインの目安にする。
        /// 丸め方は文字ごとにも違うので、次に <paramref name="text"/> の各文字の下端を測り、
        /// 基準との差が 1 ピクセル以内の文字（基準と同じ高さに下端がある、ふつうの文字）のうち、最も多い差の分だけ補正する。
        /// 「・」「ー」のように下端が高い文字や、下へはみ出す文字は、下のラインを決める材料にしない。
        /// </remarks>
        private static int GetBottomLine(Font font, string text, string referenceText)
        {
            int reference = GetInkBottom(font, referenceText);

            // 基準との差（-1・0・+1）ごとに、文字の数を数える
            var counts = new int[3];
            TextElementEnumerator elements = StringInfo.GetTextElementEnumerator(text);
            while (elements.MoveNext())
            {
                int difference = GetInkBottom(font, elements.GetTextElement()) - reference;
                if (difference >= -1 && difference <= 1)
                {
                    counts[difference + 1]++;
                }
            }

            // 最も多い差を採る。同数の場合は、基準どおり（差 0）を優先する
            int correction = 0;
            if (counts[0] > counts[1] && counts[0] > counts[2])
            {
                correction = -1;
            }
            else if (counts[2] > counts[1] && counts[2] > counts[0])
            {
                correction = 1;
            }
            return reference + correction;
        }

        /// <summary>
        /// <paramref name="text"/> を <paramref name="font"/> で描いたときの、文字の上端から、いちばん下にある字の画素までの高さを返す。
        /// 何も描かれない場合（空白など）は 0。測った結果は覚えておき、同じフォントと文字では描き直さない。
        /// </summary>
        private static int GetInkBottom(Font font, string text)
        {
            string key = string.Format(
                CultureInfo.InvariantCulture,
                "{0}|{1:R}|{2}|{3}|{4}",
                font.Name,
                font.SizeInPoints,
                (int)font.Style,
                font.GdiCharSet,
                text);

            lock (InkBottomCache)
            {
                int cached;
                if (InkBottomCache.TryGetValue(key, out cached))
                {
                    return cached;
                }

                int bottom = MeasureInkBottom(font, text);
                InkBottomCache.Add(key, bottom);
                return bottom;
            }
        }

        /// <summary>
        /// <paramref name="text"/> を白地に黒で描き、文字の上端から、いちばん下にある黒い画素までの高さを数える。
        /// </summary>
        private static int MeasureInkBottom(Font font, string text)
        {
            Size cell = TextRenderer.MeasureText(text, font, MeasureArea, LabelFlags);
            if (cell.Width <= 0 || cell.Height <= 0)
            {
                return 0;
            }

            using (var bitmap = new Bitmap(cell.Width, cell.Height, PixelFormat.Format32bppRgb))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);
                    TextRenderer.DrawText(g, text, font, new Rectangle(Point.Empty, cell), Color.Black, Color.White, LabelFlags);
                }

                // 下の行から順に見て、黒に近い画素（白と黒の中間より暗い）がある最初の行が、字の下端
                for (int y = bitmap.Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        if (pixel.R + pixel.G + pixel.B < InkThreshold)
                        {
                            return y + 1;
                        }
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// <paramref name="left"/> からコントロールの右端まで、次の工程へつなぐ線を上下中央に引き、右端を矢印にする。
        /// 矢印を描けるだけの長さがない場合は何も描かない。
        /// </summary>
        private void DrawConnector(Graphics g, float left, float scale)
        {
            float penWidth = ConnectorWidth * scale;
            float head = ArrowHeadSize * scale;
            float right = Width - 1 - penWidth / 2f;
            float centerY = (Height - 1) / 2f;
            if (right - left < head * 2f)
            {
                return;
            }

            using (var pen = new Pen(_connectorColor, penWidth))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                // 線と矢印を 1 本の折れ線にまとめて描く（半透明色でも、線と矢印の重なりが濃くならない）
                using (var path = new GraphicsPath())
                {
                    path.AddLine(left, centerY, right, centerY);
                    path.StartFigure();
                    path.AddLines(new[]
                    {
                        new PointF(right - head, centerY - head),
                        new PointF(right, centerY),
                        new PointF(right - head, centerY + head)
                    });
                    g.DrawPath(pen, path);
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            Invalidate();
        }

        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new StepHeaderLabelAccessibleObject(this);
        }

        /// <summary>
        /// 読み上げソフトなどに、ボタンや入力欄ではなく表示用の文字列であることを伝える。
        /// </summary>
        private sealed class StepHeaderLabelAccessibleObject : ControlAccessibleObject
        {
            public StepHeaderLabelAccessibleObject(StepHeaderLabel owner)
                : base(owner)
            {
            }

            public override AccessibleRole Role
            {
                get
                {
                    AccessibleRole role = Owner.AccessibleRole;
                    return role != AccessibleRole.Default ? role : AccessibleRole.StaticText;
                }
            }
        }
    }
}
