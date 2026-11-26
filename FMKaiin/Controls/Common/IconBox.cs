using System;
using System.ComponentModel;
using System.Drawing;

namespace App.Controls
{
    /// <summary>
    /// <see cref="MenuIconPainter"/> のアイコンを 1 つ表示するだけのコントロール。
    /// アイコンの色は ForeColor、背景は BackColor（既定では親と同じ色）を使う。
    /// CornerRadius を指定すると背景が角丸になり、色付きのタイルにアイコンを載せたロゴのように使える。
    /// アイコンは Padding の内側に描く（背景と余白は、土台の <see cref="IconBoxBase"/> が受け持つ）。
    /// </summary>
    public class IconBox : IconBoxBase
    {
        private MenuIconKind _iconKind = MenuIconKind.None;
        private float _lineWeight = 1f;

        /// <summary>表示するアイコン。</summary>
        [Category("Appearance")]
        [DefaultValue(MenuIconKind.None)]
        [Description("表示するアイコン。")]
        public MenuIconKind IconKind
        {
            get { return _iconKind; }
            set
            {
                if (_iconKind == value)
                {
                    return;
                }
                _iconKind = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 線の太さの倍率。1 が標準。
        /// アイコンを大きく表示するときに線だけ細く保ちたい場合は 1 より小さくする。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(1f)]
        [Description("線の太さの倍率。1 が標準。大きく表示するときに線を細く保ちたい場合は 1 より小さくする。")]
        public float LineWeight
        {
            get { return _lineWeight; }
            set
            {
                if (value <= 0f || float.IsNaN(value) || float.IsInfinity(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "線の太さの倍率は 0 より大きい値で指定してください。");
                }
                if (_lineWeight == value)
                {
                    return;
                }
                _lineWeight = value;
                Invalidate();
            }
        }

        protected override void DrawIcon(Graphics g, RectangleF bounds)
        {
            MenuIconPainter.Draw(g, _iconKind, bounds, ForeColor, _lineWeight);
        }
    }
}
