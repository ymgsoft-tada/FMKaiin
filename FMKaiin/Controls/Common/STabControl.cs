using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// タブでページを切り替えるコントロール。標準の <see cref="TabControl"/> を、S 系の見た目で描いたもの。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 標準の <see cref="TabControl"/> を継承していて、TabPages・SelectedIndex・SelectedIndexChanged などは、そのまま使える。
    /// ページの追加と、ページの中のコントロールの配置も、Visual Studio のデザイナーで、標準のものと同じようにできる。
    /// 変えているのは、描画（タブの見出しと、ページの枠）と、ページを置く範囲だけ。
    /// </para>
    /// <para>
    /// ページ（<see cref="TabPage"/>）の背景は、<see cref="PageColor"/> で塗る（ページを足したときに、自動で設定する）。
    /// 見出しの右の空いている部分は、親コントロールの見た目を写すので、背景の画像の上に置いても、四角く抜けない。
    /// </para>
    /// <para>
    /// タブの見出しの大きさは、フォントと <see cref="TabControl.Padding"/> から決まる（表示倍率が変わっても、文字に合う）。
    /// キーボードでは、標準と同じく、Ctrl+Tab / Ctrl+Shift+Tab と、見出しにフォーカスがあるときの ← → で切り替えられる。
    /// </para>
    /// </remarks>
    public class STabControl : TabControl
    {
        /// <summary>選んでいるタブの上に引く線の太さ（96 DPI のときのピクセル）。</summary>
        private const float AccentHeight = 3f;

        /// <summary>タブとページの枠の、角の丸み（96 DPI のときのピクセル）。</summary>
        private const float CornerRadius = 6f;

        private static readonly Color DefaultPageColor = Color.FromArgb(246, 249, 253);
        private static readonly Color DefaultFrameColor = Color.FromArgb(196, 206, 220);
        private static readonly Color DefaultAccentColor = Color.FromArgb(32, 100, 161);
        private static readonly Color InactiveTextColor = Color.FromArgb(92, 108, 134);
        private static readonly Color HoverFillColor = Color.FromArgb(150, 255, 255, 255);

        private Color _pageColor = DefaultPageColor;
        private Color _frameColor = DefaultFrameColor;
        private Color _accentColor = DefaultAccentColor;
        private int _hotIndex = -1;

        public STabControl()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            base.Font = SControlStyle.Font;
            // 見出しの文字の左右・上下に取る余白。見出しの大きさは、文字の大きさにこれを足したものになる
            base.Padding = new Point(16, 6);
        }

        #region プロパティ

        /// <summary>ページの背景色。ページ（TabPage）の BackColor にも、この色を設定する。</summary>
        [Category("Appearance")]
        [Description("ページの背景色。")]
        public Color PageColor
        {
            get { return _pageColor; }
            set
            {
                if (_pageColor == value)
                {
                    return;
                }
                _pageColor = value;
                foreach (TabPage page in TabPages)
                {
                    ApplyPageColor(page);
                }
                Invalidate();
            }
        }

        private bool ShouldSerializePageColor()
        {
            return _pageColor != DefaultPageColor;
        }

        private void ResetPageColor()
        {
            PageColor = DefaultPageColor;
        }

        /// <summary>ページとタブの枠線の色。</summary>
        [Category("Appearance")]
        [Description("ページとタブの枠線の色。")]
        public Color FrameColor
        {
            get { return _frameColor; }
            set
            {
                if (_frameColor == value)
                {
                    return;
                }
                _frameColor = value;
                Invalidate();
            }
        }

        private bool ShouldSerializeFrameColor()
        {
            return _frameColor != DefaultFrameColor;
        }

        private void ResetFrameColor()
        {
            FrameColor = DefaultFrameColor;
        }

        /// <summary>選んでいるタブの上に引く線の色。</summary>
        [Category("Appearance")]
        [Description("選んでいるタブの上に引く線の色。")]
        public Color AccentColor
        {
            get { return _accentColor; }
            set
            {
                if (_accentColor == value)
                {
                    return;
                }
                _accentColor = value;
                Invalidate();
            }
        }

        private bool ShouldSerializeAccentColor()
        {
            return _accentColor != DefaultAccentColor;
        }

        private void ResetAccentColor()
        {
            AccentColor = DefaultAccentColor;
        }

        /// <summary>
        /// ページを置く範囲。見出しの下から、枠線の内側まで。
        /// 標準の TabControl は、立体の枠の分だけ内側に取るが、ここでは枠線 1 本の分だけにする。
        /// </summary>
        public override Rectangle DisplayRectangle
        {
            get
            {
                int top = HeaderBottom;
                return new Rectangle(1, top + 1, Math.Max(0, Width - 2), Math.Max(0, Height - top - 2));
            }
        }

        #endregion

        #region 配置

        /// <summary>見出しの下端（ページの枠の上端）の y 座標。</summary>
        private int HeaderBottom
        {
            get
            {
                if (IsHandleCreated && TabCount > 0)
                {
                    return GetTabRect(0).Bottom;
                }
                // ハンドルを作る前（デザイナーの初期化中など）は、フォントから見積もる
                return Font.Height + Padding.Y * 2 + 2;
            }
        }

        private float ScaleFactor
        {
            get { return LayoutScale.Of(this); }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            var page = e.Control as TabPage;
            if (page != null)
            {
                ApplyPageColor(page);
            }
            base.OnControlAdded(e);
        }

        private void ApplyPageColor(TabPage page)
        {
            // 標準の「テーマの背景」を使うと、ページだけ白くなって、枠の色と合わなくなる
            page.UseVisualStyleBackColor = false;
            page.BackColor = _pageColor;
        }

        #endregion

        #region 描画

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // OnPaint で全面を描く
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 見出しの右の空いている部分と、角丸の外側は、親の見た目を写す
            PaintHelper.PaintParentAppearance(this, g, InvokePaintBackground, InvokePaint);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            float scale = ScaleFactor;
            float radius = CornerRadius * scale;
            int top = HeaderBottom;

            // ページの枠
            var frame = new RectangleF(0.5f, top + 0.5f, Width - 1f, Height - top - 1f);
            if (frame.Width > 1f && frame.Height > 1f)
            {
                using (GraphicsPath path = PaintHelper.CreateRoundedRectangle(frame, radius))
                using (var fill = new SolidBrush(_pageColor))
                using (var pen = new Pen(_frameColor, 1f))
                {
                    g.FillPath(fill, path);
                    g.DrawPath(pen, path);
                }
            }

            for (int i = 0; i < TabCount; i++)
            {
                if (i != SelectedIndex)
                {
                    DrawTab(g, i, false, scale, radius);
                }
            }
            if (SelectedIndex >= 0 && SelectedIndex < TabCount)
            {
                DrawTab(g, SelectedIndex, true, scale, radius);
            }
        }

        private void DrawTab(Graphics g, int index, bool selected, float scale, float radius)
        {
            Rectangle bounds = GetTabRect(index);
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            TabPage page = TabPages[index];
            bool enabled = Enabled && page.Enabled;

            if (selected)
            {
                // ページとつながって見えるように、下の辺は枠線を引かず、ページの色で 1 ピクセル下まで塗る
                var area = new RectangleF(bounds.Left + 0.5f, bounds.Top + 0.5f, bounds.Width - 1f, bounds.Height + 1f);
                using (GraphicsPath fillPath = CreateTabPath(area, radius, true))
                using (GraphicsPath linePath = CreateTabPath(area, radius, false))
                using (var fill = new SolidBrush(_pageColor))
                using (var pen = new Pen(_frameColor, 1f))
                {
                    g.FillPath(fill, fillPath);
                    g.DrawPath(pen, linePath);
                }

                // 上の線（選んでいるタブの印）
                float accent = Math.Max(2f, AccentHeight * scale);
                GraphicsState state = g.Save();
                try
                {
                    using (GraphicsPath clip = CreateTabPath(area, radius, true))
                    using (var brush = new SolidBrush(enabled ? _accentColor : SControlStyle.GetInactiveColor(_accentColor, _pageColor)))
                    {
                        g.SetClip(clip, CombineMode.Intersect);
                        g.FillRectangle(brush, area.Left, area.Top, area.Width, accent);
                    }
                }
                finally
                {
                    g.Restore(state);
                }
            }
            else if (index == _hotIndex && enabled)
            {
                var area = new RectangleF(bounds.Left + 1.5f, bounds.Top + 2.5f, bounds.Width - 3f, bounds.Height - 3f);
                using (GraphicsPath path = CreateTabPath(area, radius, true))
                using (var fill = new SolidBrush(HoverFillColor))
                {
                    g.FillPath(fill, path);
                }
            }

            Color textColor = selected ? SControlStyle.HeadingTextColor : InactiveTextColor;
            if (!enabled)
            {
                textColor = SControlStyle.GetInactiveColor(textColor, _pageColor);
            }

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
            if (!ShowKeyboardCues)
            {
                flags |= TextFormatFlags.HidePrefix;
            }
            TextRenderer.DrawText(g, page.Text, Font, bounds, textColor, flags);

            if (selected && Focused && ShowFocusCues)
            {
                Rectangle focus = Rectangle.Inflate(bounds, -(int)Math.Round(5 * scale), -(int)Math.Round(5 * scale));
                focus.Y += (int)Math.Round(1 * scale);
                ControlPaint.DrawFocusRectangle(g, focus);
            }
        }

        /// <summary>
        /// タブの形（上の 2 つの角だけを丸めた四角）を作る。
        /// <paramref name="closed"/> が false のときは、下の辺を除いた線（左の辺・上の辺・右の辺）にする。
        /// </summary>
        private static GraphicsPath CreateTabPath(RectangleF bounds, float radius, bool closed)
        {
            float diameter = Math.Min(radius * 2f, Math.Min(bounds.Width, bounds.Height));
            var path = new GraphicsPath();
            if (diameter <= 0.5f)
            {
                path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Top);
                path.AddLine(bounds.Left, bounds.Top, bounds.Right, bounds.Top);
                path.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Bottom);
            }
            else
            {
                path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Top + diameter / 2f);
                path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180f, 90f);
                path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270f, 90f);
                path.AddLine(bounds.Right, bounds.Top + diameter / 2f, bounds.Right, bounds.Bottom);
            }
            if (closed)
            {
                path.CloseFigure();
            }
            return path;
        }

        #endregion

        #region 描き直しのきっかけ

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            SetHotIndex(IndexAt(e.Location));
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SetHotIndex(-1);
        }

        private int IndexAt(Point point)
        {
            for (int i = 0; i < TabCount; i++)
            {
                if (GetTabRect(i).Contains(point))
                {
                    return i;
                }
            }
            return -1;
        }

        private void SetHotIndex(int index)
        {
            if (_hotIndex == index)
            {
                return;
            }
            _hotIndex = index;
            Invalidate(new Rectangle(0, 0, Width, HeaderBottom + 2));
        }

        #endregion
    }
}
