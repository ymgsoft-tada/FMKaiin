using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 入力コントロール（<see cref="STextBox"/> など、名前が S で始まるもの）に共通の、既定の見た目と枠の描き方。
    /// 色や大きさを変えるときは、ここの値と、各コントロールのプロパティに付けている DefaultValue 属性の両方を直すこと
    /// （属性には定数しか書けないため、同じ値を 2 か所に書いている）。
    /// </summary>
    internal static class SControlStyle
    {
        /// <summary>既定のフォントの候補。PC に入っている最初のものを使う。</summary>
        private static readonly string[] FontFamilyCandidates = { "BIZ UDPゴシック", "游ゴシック", "Yu Gothic" };

        /// <summary>既定のフォントの大きさ（ポイント）。</summary>
        private const float FontSize = 10.5f;

        /// <summary>日本語（Shift JIS）の文字セット。デザイナーが書き出すフォントの指定と同じ値にしている。</summary>
        private const byte ShiftJisCharSet = 128;

        /// <summary>枠線から文字までの左右の余白。96 DPI 基準のピクセル。</summary>
        private const float HorizontalPadding = 6f;

        /// <summary>枠線なしのときの、端から文字までの左右の余白。96 DPI 基準のピクセル。</summary>
        private const float BorderlessHorizontalPadding = 3f;

        /// <summary>フォーカスがあるときの枠線の太さ。96 DPI 基準のピクセル。</summary>
        private const float FocusBorderWidth = 1.5f;

        /// <summary>一覧を開く矢印の、幅の半分。96 DPI 基準のピクセル。</summary>
        private const float ArrowHalfWidth = 4.5f;

        /// <summary>一覧を開く矢印の高さ。96 DPI 基準のピクセル。</summary>
        private const float ArrowHeight = 4.5f;

        /// <summary>一覧を開く矢印の線幅。96 DPI 基準のピクセル。</summary>
        private const float ArrowStrokeWidth = 1.6f;

        /// <summary>操作できないとき（無効・読み取り専用）の矢印などの薄さ。0 で元の色、1 で背景と同じ色。</summary>
        private const float InactiveFade = 0.6f;

        /// <summary>背景の明るさ（0～255）がこれより小さいと、その上の文字を白にする（<see cref="GetTextColorOn"/>）。</summary>
        private const float DarkBackgroundBrightness = 150f;

        /// <summary>枠の角の半径。96 DPI 基準のピクセル。</summary>
        public const int CornerRadius = 4;

        /// <summary>文字の色。</summary>
        public static readonly Color TextColor = Color.FromArgb(31, 38, 62);

        /// <summary>枠線の色。</summary>
        public static readonly Color BorderColor = Color.FromArgb(186, 197, 211);

        /// <summary>フォーカスがあるときの枠線の色。</summary>
        public static readonly Color FocusBorderColor = Color.FromArgb(64, 144, 232);

        /// <summary>
        /// フォーカスがあるときの背景色（Lavender）。どの入力欄を操作しているかが、枠線の色だけの場合より分かりやすくなる。
        /// 選択の色（<see cref="SelectionBackColor"/>）は、この色の上でも見分けられる濃さにしてある。
        /// 各コントロールの FocusBackColor プロパティの DefaultValue 属性にも、同じ色を書いてある。変えるときは両方を直すこと。
        /// </summary>
        public static readonly Color FocusBackColor = Color.Lavender;

        /// <summary>
        /// 無効（Enabled = false）のときの背景色。フォーカスがあるときの背景色（<see cref="FocusBackColor"/>。青系）と見分けられるよう、灰色にしてある。
        /// 各コントロールの DisabledBackColor プロパティの DefaultValue 属性にも、同じ色を書いてある。変えるときは両方を直すこと。
        /// </summary>
        public static readonly Color DisabledBackColor = Color.FromArgb(236, 238, 241);

        /// <summary>
        /// 選択の色（RoyalBlue）。選択した文字、一覧で指している行、カレンダーで指している日の背景に使う。
        /// 濃い色なので、その上の文字は白で描く（<see cref="GetTextColorOn"/>）。
        /// 各コントロールの SelectionColor / TextSelectionColor プロパティの DefaultValue 属性にも、同じ色を書いてある。変えるときは両方を直すこと。
        /// </summary>
        public static readonly Color SelectionBackColor = Color.RoyalBlue;

        /// <summary>
        /// 入力欄が空のときに表示する案内の文字（プレースホルダー）の色。入力した文字と見分けられるよう、薄い色にしてある。
        /// 入力欄の PlaceholderColor プロパティの DefaultValue 属性にも、同じ色を書いてある。変えるときは両方を直すこと。
        /// </summary>
        public static readonly Color PlaceholderColor = Color.FromArgb(200, 210, 226);

        /// <summary>情報・確認のアイコンの色（ツールチップ、メッセージ ボックス）。</summary>
        public static readonly Color InfoIconColor = Color.FromArgb(30, 110, 190);

        /// <summary>警告のアイコンの色（ツールチップ、メッセージ ボックス）。</summary>
        public static readonly Color WarningIconColor = Color.FromArgb(232, 142, 12);

        /// <summary>エラーのアイコンの色（ツールチップ、メッセージ ボックス）。</summary>
        public static readonly Color ErrorIconColor = Color.FromArgb(212, 60, 60);

        /// <summary>入力必須の項目名に付ける印（*）の色（<see cref="SLabel.Required"/>）。</summary>
        public static readonly Color RequiredMarkColor = Color.FromArgb(214, 48, 49);

        /// <summary>見出しの文字の色（ツールチップのタイトルなど）。</summary>
        public static readonly Color HeadingTextColor = Color.FromArgb(17, 42, 74);

        private static Font _font;

        /// <summary>
        /// 既定のフォント。アプリの終了まで使い回すので、破棄しないこと。
        /// </summary>
        public static Font Font
        {
            get
            {
                if (_font == null)
                {
                    _font = new Font(FindFontFamily(), FontSize, FontStyle.Regular, GraphicsUnit.Point, ShiftJisCharSet);
                }
                return _font;
            }
        }

        /// <summary>
        /// 枠線の太さ（ピクセル）。フォーカスがあるときは太くする。
        /// </summary>
        /// <param name="scale">表示倍率（<see cref="LayoutScale.Of"/> の値）。</param>
        public static int GetBorderWidth(float scale, bool focused)
        {
            float width = focused ? FocusBorderWidth * scale : scale;
            return Math.Max(focused ? 2 : 1, (int)Math.Round(width));
        }

        /// <summary>
        /// コントロールの左端（または右端）から文字までの距離（ピクセル）。
        /// 枠線の分は、太いほう（フォーカスがあるとき）に合わせて空ける。フォーカスの有無で文字が動かないようにするため。
        /// </summary>
        /// <param name="scale">表示倍率（<see cref="LayoutScale.Of"/> の値）。</param>
        /// <param name="bordered">枠線を描くか。</param>
        public static int GetTextInset(float scale, bool bordered)
        {
            return bordered
                ? GetBorderWidth(scale, true) + (int)Math.Round(HorizontalPadding * scale)
                : (int)Math.Round(BorderlessHorizontalPadding * scale);
        }

        /// <summary>
        /// 角丸の枠（塗りと枠線）を、(0, 0) から幅 <paramref name="width"/>・高さ <paramref name="height"/> の範囲に描く。
        /// 角丸の外側は塗らないので、必要なら先に親コントロールと同じ見た目で塗っておくこと
        /// （<see cref="PaintHelper.PaintParentAppearance"/>）。
        /// </summary>
        /// <param name="scale">表示倍率（<see cref="LayoutScale.Of"/> の値）。</param>
        /// <param name="borderWidth">枠線の太さ（ピクセル）。<see cref="GetBorderWidth"/> の値。</param>
        /// <param name="cornerRadius">角の半径。96 DPI 基準のピクセル。</param>
        public static void DrawFrame(
            Graphics g, int width, int height, float scale, Color fill, Color borderColor, int borderWidth, int cornerRadius)
        {
            // 線の外側の端が、ちょうどコントロールの端に来るようにする
            float offset = (borderWidth - 1) / 2f;
            var bounds = new RectangleF(offset, offset, width - 1 - offset * 2f, height - 1 - offset * 2f);

            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (GraphicsPath frame = PaintHelper.CreateRoundedRectangle(bounds, cornerRadius * scale))
                using (var fillBrush = new SolidBrush(fill))
                using (var borderPen = new Pen(borderColor, borderWidth))
                {
                    g.FillPath(fillBrush, frame);
                    g.DrawPath(borderPen, frame);
                }
            }
            finally
            {
                g.SmoothingMode = previous;
            }
        }

        /// <summary>
        /// 一覧を開く矢印を置く領域（コントロールの右端）。枠線の内側に収める。
        /// </summary>
        /// <param name="width">コントロールの幅。</param>
        /// <param name="height">コントロールの高さ。</param>
        /// <param name="scale">表示倍率（<see cref="LayoutScale.Of"/> の値）。</param>
        /// <param name="bordered">枠線を描くか。</param>
        public static Rectangle GetDropDownButtonBounds(int width, int height, float scale, bool bordered)
        {
            int border = bordered ? GetBorderWidth(scale, true) : 0;
            int buttonWidth = GetDropDownButtonWidth(scale);
            return new Rectangle(width - border - buttonWidth, border, buttonWidth, Math.Max(0, height - border * 2));
        }

        /// <summary>一覧を開く矢印を置く領域の幅（ピクセル）。</summary>
        public static int GetDropDownButtonWidth(float scale)
        {
            return SystemInformation.VerticalScrollBarWidth + (int)Math.Round(4f * scale);
        }

        /// <summary>
        /// 一覧を開く矢印（下向きの山形）を、<paramref name="bounds"/> の中に描く。
        /// </summary>
        public static void DrawDropDownArrow(Graphics g, Rectangle bounds, Color color, float scale)
        {
            float centerX = bounds.Left + bounds.Width / 2f - 2f * scale;
            float centerY = bounds.Top + bounds.Height / 2f;
            float halfWidth = ArrowHalfWidth * scale;
            float halfHeight = ArrowHeight * scale / 2f;

            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                using (var arrowPen = new Pen(color, ArrowStrokeWidth * scale))
                {
                    arrowPen.StartCap = LineCap.Round;
                    arrowPen.EndCap = LineCap.Round;
                    arrowPen.LineJoin = LineJoin.Round;
                    g.DrawLines(arrowPen, new[]
                    {
                        new PointF(centerX - halfWidth, centerY - halfHeight),
                        new PointF(centerX, centerY + halfHeight),
                        new PointF(centerX + halfWidth, centerY - halfHeight)
                    });
                }
            }
            finally
            {
                g.SmoothingMode = previous;
            }
        }

        /// <summary>
        /// 操作できないとき（無効・読み取り専用）に矢印などへ使う、薄くした色を返す。
        /// </summary>
        /// <param name="color">元の色（文字の色）。</param>
        /// <param name="background">背景の色。</param>
        public static Color GetInactiveColor(Color color, Color background)
        {
            return PaintHelper.Blend(color, background, InactiveFade);
        }

        /// <summary>
        /// <paramref name="background"/> の上に描く文字の色。背景が濃い色なら白、明るい色なら <paramref name="textColor"/> をそのまま返す。
        /// 選択の色（選択した文字、一覧で指している行、カレンダーで指している日）を濃い色に変えても、文字が読めるようにするためのもの。
        /// </summary>
        /// <param name="background">背景の色（選択の色）。</param>
        /// <param name="textColor">通常の文字の色。</param>
        public static Color GetTextColorOn(Color background, Color textColor)
        {
            // 人の目が感じる明るさ（0～255）。緑がいちばん明るく、青がいちばん暗く見えることを反映した重み付け
            float brightness = background.R * 0.299f + background.G * 0.587f + background.B * 0.114f;
            return brightness < DarkBackgroundBrightness ? Color.White : textColor;
        }

        private static string FindFontFamily()
        {
            foreach (string family in FontFamilyCandidates)
            {
                if (FontFallback.IsInstalled(family))
                {
                    return family;
                }
            }

            // 候補がどれも無い PC では、Windows フォームの既定のフォントにする
            return Control.DefaultFont.FontFamily.Name;
        }
    }
}
