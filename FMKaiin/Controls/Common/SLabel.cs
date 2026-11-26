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
    /// <para>
    /// 入力必須の入力欄の項目名には、<see cref="Required"/> を true にすると、文字列の後ろに印（半角の *）を付けられる。
    /// 印は、このコントロールが描く（Text には入れない）ので、印の形や色を変えるときは、ここと <see cref="SControlStyle.RequiredMarkColor"/> を直せば、すべての画面に効く。
    /// </para>
    /// </summary>
    public class SLabel : Control
    {
        /// <summary>入力必須の印。</summary>
        private const string RequiredMark = "*";

        /// <summary>文字列と、入力必須の印の間のすき間の、文字の高さに対する割合。</summary>
        private const float RequiredMarkGapRatio = 0.12f;

        /// <summary>読み上げソフトなどに伝える名前の、入力必須のときに後ろへ足す文字列。</summary>
        private const string RequiredAccessibleSuffix = "（必須）";

        private const ContentAlignment AnyRight =
            ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
        private const ContentAlignment AnyCenter =
            ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;
        private const ContentAlignment AnyBottom =
            ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
        private const ContentAlignment AnyMiddle =
            ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;

        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        private bool _required;

        public SLabel()
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

        /// <summary>
        /// 入力必須の項目名か。true にすると、文字列の後ろに、入力必須の印（*）を付ける。既定は false。
        /// 印の分の幅が足りないときは、文字列のほうを「…」で省略して、印は必ず表示する。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("入力必須の項目名か。true にすると、文字列の後ろに、入力必須の印（*）を付ける。")]
        public bool Required
        {
            get { return _required; }
            set
            {
                if (_required == value)
                {
                    return;
                }
                _required = value;
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
                Color textColor = Enabled ? ForeColor : SystemColors.GrayText;
                if (_required)
                {
                    PaintTextWithRequiredMark(e.Graphics, bounds, textColor);
                }
                else
                {
                    TextRenderer.DrawText(e.Graphics, Text, Font, bounds, textColor, CreateTextFormatFlags());
                }
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// 文字列と、その後ろの入力必須の印を描く。文字列と印をひとまとまりとして、<see cref="TextAlign"/> の位置に置く。
        /// </summary>
        private void PaintTextWithRequiredMark(Graphics g, Rectangle bounds, Color textColor)
        {
            // 大きさを測るときは、寄せる向きと省略を外す（文字列そのものの幅を知るため）
            TextFormatFlags measureFlags = TextFormatFlags.SingleLine | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix;
            var unlimited = new Size(int.MaxValue, int.MaxValue);
            int markWidth = TextRenderer.MeasureText(g, RequiredMark, Font, unlimited, measureFlags).Width;
            int gap = Math.Max(1, (int)Math.Round(Font.Height * RequiredMarkGapRatio));

            // 文字列に使える幅は、印とすき間を除いた残り。収まらなければ、文字列を「…」で省略する
            int available = Math.Max(0, bounds.Width - markWidth - gap);
            int textWidth = Math.Min(TextRenderer.MeasureText(g, Text, Font, unlimited, measureFlags).Width, available);
            int groupWidth = textWidth + gap + markWidth;

            int left = bounds.Left;
            if ((_textAlign & AnyRight) != 0)
            {
                left = bounds.Right - groupWidth;
            }
            else if ((_textAlign & AnyCenter) != 0)
            {
                left = bounds.Left + (bounds.Width - groupWidth) / 2;
            }
            left = Math.Max(bounds.Left, left);

            // 上下の位置は、文字列と同じ決め方にする。左右は、ここで決めた位置に、左寄せで描く
            TextFormatFlags flags = CreateTextFormatFlags() & ~(TextFormatFlags.Right | TextFormatFlags.HorizontalCenter);

            if (textWidth > 0)
            {
                TextRenderer.DrawText(g, Text, Font, new Rectangle(left, bounds.Top, textWidth, bounds.Height), textColor, flags);
            }

            var markBounds = new Rectangle(left + textWidth + gap, bounds.Top, markWidth, bounds.Height);
            if (markBounds.Right <= bounds.Right)
            {
                Color markColor = Enabled ? SControlStyle.RequiredMarkColor : SystemColors.GrayText;
                TextRenderer.DrawText(g, RequiredMark, Font, markBounds, markColor, flags & ~TextFormatFlags.EndEllipsis);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new SLabelAccessibleObject(this);
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
        private sealed class SLabelAccessibleObject : ControlAccessibleObject
        {
            public SLabelAccessibleObject(SLabel owner)
                : base(owner)
            {
            }

            /// <summary>入力必須の項目名は、名前の後ろに「（必須）」を足して伝える（印は絵として描いていて、文字列には入っていないため）。</summary>
            public override string Name
            {
                get
                {
                    string name = base.Name;
                    var label = Owner as SLabel;
                    if (label != null && label.Required && !string.IsNullOrEmpty(name))
                    {
                        return name + RequiredAccessibleSuffix;
                    }
                    return name;
                }
                set { base.Name = value; }
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
