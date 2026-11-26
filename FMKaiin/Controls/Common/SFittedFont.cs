using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 文字列が表示する幅に収まらないときに、収まる大きさまで縮めたフォントを用意する。
    /// コンボ ボックス（<see cref="SComboBox"/>・<see cref="STableComboBox"/>）の「収まらないときは縮める」表示に使う。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 縮めるのは <see cref="MinimumSize"/>（8 ポイント）まで。それでも収まらない場合は、その大きさのフォントを返すので、
    /// 呼び出す側で、末尾を「…」にして描くこと（<see cref="TextFormatFlags.EndEllipsis"/>）。
    /// </para>
    /// <para>
    /// 描くたびに同じ計算をしないように、直前の結果（文字列・幅・元のフォントの組）を覚えている。
    /// 作ったフォントは、このオブジェクトが持つ。使い終わったら <see cref="Dispose"/> を呼ぶこと。
    /// </para>
    /// </remarks>
    internal sealed class SFittedFont : IDisposable
    {
        /// <summary>縮める下限の大きさ（ポイント）。これより小さい文字は、読みにくい。</summary>
        public const float MinimumSize = 8f;

        /// <summary>大きさを探すときの刻み（ポイント）。</summary>
        private const float Step = 0.5f;

        private string _text;
        private int _width;
        private Font _baseFont;
        private TextFormatFlags _flags;
        private Font _fitted;
        private bool _clipped;
        private bool _hasResult;

        /// <summary>直前の <see cref="Get"/> で、フォントを縮めたか。</summary>
        public bool IsShrunk
        {
            get { return _hasResult && _fitted != null; }
        }

        /// <summary>直前の <see cref="Get"/> で、下限まで縮めても収まらなかったか（末尾を「…」にして描く場合）。</summary>
        public bool IsClipped
        {
            get { return _hasResult && _clipped; }
        }

        /// <summary>
        /// <paramref name="text"/> が <paramref name="width"/> に収まるフォントを返す。
        /// 元のフォントで収まる場合は <paramref name="baseFont"/> そのものを返す。
        /// </summary>
        /// <param name="text">表示する文字列。</param>
        /// <param name="baseFont">元のフォント。</param>
        /// <param name="width">表示できる幅（ピクセル）。</param>
        /// <param name="flags">描くときの指定（幅の測り方に関わるもの）。「…」にする指定は、測るときには外す。</param>
        public Font Get(string text, Font baseFont, int width, TextFormatFlags flags)
        {
            flags &= ~(TextFormatFlags.EndEllipsis | TextFormatFlags.PathEllipsis | TextFormatFlags.WordEllipsis);
            if (_hasResult && _text == text && _width == width && _flags == flags && ReferenceEquals(_baseFont, baseFont))
            {
                return _fitted ?? baseFont;
            }

            Release();
            _text = text;
            _width = width;
            _baseFont = baseFont;
            _flags = flags;
            _hasResult = true;

            if (string.IsNullOrEmpty(text) || width <= 0 || Measure(text, baseFont, flags) <= width)
            {
                return baseFont;
            }

            float size = baseFont.SizeInPoints;
            if (size <= MinimumSize)
            {
                // これ以上は縮めない
                _clipped = true;
                return baseFont;
            }

            // 幅の比から見当を付けて、刻みごとに下げながら、収まる大きさを探す
            float candidate = (float)Math.Floor(size * width / Measure(text, baseFont, flags) / Step) * Step + Step;
            candidate = Math.Min(candidate, size - Step);
            for (;;)
            {
                candidate = Math.Max(candidate, MinimumSize);
                var font = new Font(baseFont.FontFamily, candidate, baseFont.Style, GraphicsUnit.Point, baseFont.GdiCharSet);
                bool fits = Measure(text, font, flags) <= width;
                if (fits || candidate <= MinimumSize)
                {
                    _fitted = font;
                    _clipped = !fits;
                    return font;
                }
                font.Dispose();
                candidate -= Step;
            }
        }

        private static int Measure(string text, Font font, TextFormatFlags flags)
        {
            return TextRenderer.MeasureText(text, font, new Size(int.MaxValue, int.MaxValue), flags).Width;
        }

        private void Release()
        {
            if (_fitted != null)
            {
                _fitted.Dispose();
                _fitted = null;
            }
            _clipped = false;
            _hasResult = false;
        }

        public void Dispose()
        {
            Release();
            _baseFont = null;
            _text = null;
        }
    }
}
