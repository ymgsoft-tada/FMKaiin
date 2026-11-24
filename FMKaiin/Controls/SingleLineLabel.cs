using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 1 行の文字列を表示するラベル。
    /// 標準の <see cref="Label"/> は、行間が広く設定されたフォント（游ゴシック・游明朝など）で
    /// 上下中央揃えにすると文字が上へ寄るため、隣のコンボボックスやアイコンと高さが合わない。
    /// このラベルは文字の高さだけを基準に配置するので、フォントが変わっても（代替フォントに切り替わっても）
    /// 同じ位置に揃う。
    /// 文字列の左右に余白を付けないので、文字は <see cref="Control.Left"/> の位置から始まる。
    /// 折り返しはせず、幅に収まらない場合は末尾を「…」で省略する。
    /// </summary>
    public class SingleLineLabel : Control
    {
        private const ContentAlignment AnyRight =
            ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
        private const ContentAlignment AnyCenter =
            ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;
        private const ContentAlignment AnyBottom =
            ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
        private const ContentAlignment AnyMiddle =
            ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;

        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;

        public SingleLineLabel()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                // 親の BackColor が Transparent のときに、親の描画内容の上へ文字だけを描けるようにする
                ControlStyles.SupportsTransparentBackColor,
                true);
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
        }

        /// <summary>文字列の配置。既定は左寄せ・上下中央。</summary>
        [Category("Appearance")]
        [DefaultValue(ContentAlignment.MiddleLeft)]
        [Description("文字列の配置。")]
        public ContentAlignment TextAlign
        {
            get { return _textAlign; }
            set
            {
                if (!Enum.IsDefined(typeof(ContentAlignment), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ContentAlignment));
                }
                if (_textAlign == value)
                {
                    return;
                }
                _textAlign = value;
                Invalidate();
            }
        }

        protected override Size DefaultSize
        {
            get { return new Size(100, 23); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle bounds = ClientRectangle;
            bounds.X += Padding.Left;
            bounds.Y += Padding.Top;
            bounds.Width -= Padding.Horizontal;
            bounds.Height -= Padding.Vertical;

            if (!string.IsNullOrEmpty(Text) && bounds.Width > 0 && bounds.Height > 0)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    Text,
                    Font,
                    bounds,
                    Enabled ? ForeColor : SystemColors.GrayText,
                    CreateTextFormatFlags());
            }

            base.OnPaint(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new SingleLineLabelAccessibleObject(this);
        }

        private TextFormatFlags CreateTextFormatFlags()
        {
            // SingleLine を付けると、上下の位置が文字の高さだけで決まる（フォントの行間設定の影響を受けない）
            TextFormatFlags flags =
                TextFormatFlags.SingleLine |
                TextFormatFlags.NoPadding |
                TextFormatFlags.NoPrefix |
                TextFormatFlags.EndEllipsis;

            if ((_textAlign & AnyRight) != 0)
            {
                flags |= TextFormatFlags.Right;
            }
            else if ((_textAlign & AnyCenter) != 0)
            {
                flags |= TextFormatFlags.HorizontalCenter;
            }

            if ((_textAlign & AnyBottom) != 0)
            {
                flags |= TextFormatFlags.Bottom;
            }
            else if ((_textAlign & AnyMiddle) != 0)
            {
                flags |= TextFormatFlags.VerticalCenter;
            }

            return flags;
        }

        /// <summary>
        /// 読み上げソフトなどに、ボタンや入力欄ではなく表示用の文字列であることを伝える。
        /// </summary>
        private sealed class SingleLineLabelAccessibleObject : ControlAccessibleObject
        {
            public SingleLineLabelAccessibleObject(SingleLineLabel owner)
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
