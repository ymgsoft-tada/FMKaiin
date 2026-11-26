using System;
using System.Drawing;

namespace App.Controls
{
    /// <summary>
    /// プレースホルダー（入力欄が空のときに表示する案内の文字）を描くフォントを用意する。
    /// 入力した文字より控えめに見えるよう、入力欄のフォントより <see cref="SizeReduction"/> ポイント小さくする。
    /// </summary>
    /// <remarks>
    /// 描くたびにフォントを作らないように、直前の結果（元のフォントに対するもの）を覚えている。
    /// 作ったフォントは、このオブジェクトが持つ。使い終わったら <see cref="Dispose"/> を呼ぶこと。
    /// </remarks>
    internal sealed class SPlaceholderFont : IDisposable
    {
        /// <summary>入力欄のフォントより、どれだけ小さくするか（ポイント）。既定のフォント（10.5 ポイント）なら、9 ポイントになる。</summary>
        public const float SizeReduction = 1.5f;

        /// <summary>小さくする下限（ポイント）。入力欄のフォントがもともと小さいときに、読めない大きさにしないため。</summary>
        private const float MinimumSize = 7f;

        private Font _baseFont;
        private Font _font;

        /// <summary><paramref name="baseFont"/>（入力欄のフォント）に対する、プレースホルダーのフォントを返す。</summary>
        public Font Get(Font baseFont)
        {
            if (!ReferenceEquals(_baseFont, baseFont))
            {
                Release();
                _baseFont = baseFont;
                float size = Math.Max(MinimumSize, baseFont.SizeInPoints - SizeReduction);
                if (size < baseFont.SizeInPoints)
                {
                    _font = new Font(baseFont.FontFamily, size, baseFont.Style, GraphicsUnit.Point, baseFont.GdiCharSet);
                }
            }
            return _font ?? baseFont;
        }

        private void Release()
        {
            if (_font != null)
            {
                _font.Dispose();
                _font = null;
            }
            _baseFont = null;
        }

        public void Dispose()
        {
            Release();
        }
    }
}
