using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 角丸の枠線と山形の矢印を持つコンボボックス。
    /// 動作（ドロップダウン・キーボード操作）は標準の <see cref="ComboBox"/> のままで、
    /// 項目の文字列を自前で描画し、標準の描画が終わった後に枠と矢印の部分を上描きしている。
    /// 高さは <see cref="ComboBox.ItemHeight"/> で指定する（96 DPI 基準のピクセル）。
    /// 上描きは <see cref="FlatStyle.Flat"/> の見た目を前提にしているので、FlatStyle は変更しないこと。
    /// <para>
    /// <see cref="FillColor"/> を指定すると、閉じた状態の見た目（背景・枠・文字列・矢印）をすべてこのコントロールが描く。
    /// 半透明色を指定すれば、親の描画内容（背景画像など）が透けて見える。標準のコンボボックスは背景を透過できないので、
    /// 透過させたい場合はこの方法を使う。ドロップダウンで開く一覧は別のウィンドウなので、こちらは不透明（BackColor）のまま。
    /// </para>
    /// </summary>
    public class BorderedComboBox : ComboBox
    {
        private const int WM_PAINT = 0x000F;
        private const int WM_PRINTCLIENT = 0x0318;

        /// <summary>閉じた状態の表示部分の、外枠からの余白。標準のコンボボックス（FlatStyle.Flat）と同じ値。</summary>
        private const int ItemInset = 3;
        /// <summary>項目の文字列の左側に確保する余白。</summary>
        private const float TextLeftPadding = 8f;
        /// <summary>矢印の幅の半分。</summary>
        private const float ArrowHalfWidth = 4.5f;
        /// <summary>矢印の高さ。</summary>
        private const float ArrowHeight = 4.5f;
        /// <summary>矢印の線幅。</summary>
        private const float ArrowStrokeWidth = 1.6f;

        private static readonly Color DefaultBorderColor = Color.FromArgb(186, 197, 211);

        private Color _borderColor = DefaultBorderColor;
        private int _cornerRadius = 4;
        private Color _fillColor = Color.Empty;
        private bool _backdropBlur;
        private Color _selectionColor = Color.Empty;
        private Color _selectionForeColor = Color.Empty;

        public BorderedComboBox()
        {
            FlatStyle = FlatStyle.Flat;

            // 標準描画ではコントロールの高さがフォントで決まってしまうため、項目を自前で描画して
            // ItemHeight で高さを指定できるようにする（コントロールの高さは ItemHeight + 6 になる）
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        /// <summary>枠線の色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "186, 197, 211")]
        [Description("枠線の色。")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        /// <summary>
        /// 枠の角の半径。96 DPI 基準のピクセルで指定する。
        /// 中の文字列や矢印は四角い領域に描かれるので、高さの 3 分の 1 程度までにしておくこと。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(4)]
        [Description("枠の角の半径。中の文字列や矢印は四角い領域に描かれるので、高さの 3 分の 1 程度までにしておくこと。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>
        /// 閉じた状態の塗り色。空（<see cref="Color.Empty"/>）の場合は標準の描画を使い、背景は BackColor になる。
        /// 指定すると、閉じた状態の見た目をすべてこのコントロールが描くようになり、半透明色も使える。
        /// <see cref="ComboBox.DropDownStyle"/> が DropDownList のときだけ有効。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("閉じた状態の塗り色。空の場合は標準の描画（背景は BackColor）。指定すると半透明色（例: 110, 255, 255, 255）も使え、親の描画内容が透けて見える。DropDownList のときだけ有効。")]
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                UpdatePaintMode();
                Invalidate();
            }
        }

        /// <summary>
        /// 後ろにある背景を、ぼかして映すか（すりガラスの表現）。<see cref="FillColor"/> を指定していて、
        /// 親フォームが <see cref="IBackdropBlurSource"/> を実装している場合にだけ効果がある。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("後ろにある背景を、ぼかして映すか（すりガラスの表現）。FillColor を指定していて、親フォームが対応している場合にだけ効果がある。")]
        public bool BackdropBlur
        {
            get { return _backdropBlur; }
            set { _backdropBlur = value; Invalidate(); }
        }

        /// <summary>
        /// 開いた一覧で、選択中の行の背景色。空（<see cref="Color.Empty"/>）の場合は Windows の設定色（反転表示の色）。
        /// 一覧は不透明な面に描くので、半透明色ではなく不透明な色を指定すること。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("開いた一覧で、選択中の行の背景色。空の場合は Windows の設定色。不透明な色を指定すること。")]
        public Color SelectionColor
        {
            get { return _selectionColor; }
            set { _selectionColor = value; Invalidate(); }
        }

        /// <summary>
        /// 開いた一覧で、選択中の行の文字色。空（<see cref="Color.Empty"/>）の場合は、
        /// <see cref="SelectionColor"/> を指定していれば <see cref="Control.ForeColor"/>、指定していなければ Windows の設定色。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        [Description("開いた一覧で、選択中の行の文字色。空の場合は、SelectionColor を指定していれば ForeColor、指定していなければ Windows の設定色。")]
        public Color SelectionForeColor
        {
            get { return _selectionForeColor; }
            set { _selectionForeColor = value; Invalidate(); }
        }

        /// <summary>閉じた状態の見た目を、このコントロール自身がすべて描くか。</summary>
        private bool PaintsOwnSurface
        {
            get { return !_fillColor.IsEmpty && DropDownStyle == ComboBoxStyle.DropDownList; }
        }

        /// <summary>
        /// 描画の方式を切り替える。自分で描く場合は、標準の描画を止めて（UserPaint）、裏で描いてから一度に表示する。
        /// </summary>
        private void UpdatePaintMode()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, PaintsOwnSurface);
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);

            // 自前描画の ItemHeight は自動スケーリングの対象外なので、高さの拡大率に合わせて追従させる
            if ((specified & BoundsSpecified.Height) != 0 && factor.Height != 1f && DrawMode != DrawMode.Normal)
            {
                ItemHeight = Math.Max(1, (int)Math.Round(ItemHeight * factor.Height));
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            // 閉じた状態の表示部分は、フォーカスがあっても反転表示にしない
            bool isEditPortion = (e.State & DrawItemState.ComboBoxEdit) != 0;
            bool highlighted = !isEditPortion && (e.State & DrawItemState.Selected) != 0;

            Color background;
            Color foreground;
            if (highlighted)
            {
                // 選択中の行。色を指定していなければ Windows の設定色で反転表示する。
                // 背景色だけを指定した場合は、文字は通常の色のままにする（Windows の反転用の文字色は、指定した背景色に合うとは限らない）
                background = _selectionColor.IsEmpty ? SystemColors.Highlight : _selectionColor;
                if (!_selectionForeColor.IsEmpty)
                {
                    foreground = _selectionForeColor;
                }
                else
                {
                    foreground = _selectionColor.IsEmpty ? SystemColors.HighlightText : ForeColor;
                }
            }
            else
            {
                background = Enabled ? BackColor : SystemColors.Control;
                foreground = Enabled ? ForeColor : SystemColors.GrayText;
            }

            using (var backgroundBrush = new SolidBrush(background))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            if (e.Index >= 0)
            {
                int padding = (int)Math.Round(TextLeftPadding * LayoutScale.Of(this));
                var textBounds = new Rectangle(e.Bounds.X + padding, e.Bounds.Y, Math.Max(0, e.Bounds.Width - padding), e.Bounds.Height);
                TextRenderer.DrawText(
                    e.Graphics,
                    GetItemText(Items[e.Index]),
                    Font,
                    textBounds,
                    foreground,
                    TextFormatFlags.Left |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.SingleLine |
                    TextFormatFlags.NoPadding |
                    TextFormatFlags.NoPrefix |
                    TextFormatFlags.EndEllipsis);
            }

            if (isEditPortion &&
                (e.State & DrawItemState.Focus) != 0 &&
                (e.State & DrawItemState.NoFocusRect) == 0)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // 自分で描く場合は OnPaint が全面を描くので、ここでは何もしない
            if (!PaintsOwnSurface)
            {
                base.OnPaintBackground(pevent);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // ここが呼ばれるのは、自分で描く方式（UserPaint）のときだけ
            if (PaintsOwnSurface)
            {
                PaintOwnSurface(e.Graphics);
            }
            base.OnPaint(e);
        }

        protected override void OnDropDownStyleChanged(EventArgs e)
        {
            base.OnDropDownStyleChanged(e);
            UpdatePaintMode();
            Invalidate();
        }

        // 自分で描く方式では、表示内容が変わるきっかけごとに描き直しを求める必要がある
        // （標準の描画では Windows が自動で描き直す）
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            InvalidateOwnSurface();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            InvalidateOwnSurface();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            InvalidateOwnSurface();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            InvalidateOwnSurface();
        }

        private void InvalidateOwnSurface()
        {
            if (PaintsOwnSurface)
            {
                Invalidate();
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Simple スタイルは一覧が常に表示されており、枠と矢印の上描きが当てはまらない。
            // 自分で描く方式では OnPaint が枠と矢印まで描いているので、上描きは不要
            if (DropDownStyle == ComboBoxStyle.Simple || PaintsOwnSurface)
            {
                return;
            }

            if (m.Msg == WM_PAINT)
            {
                using (Graphics g = CreateGraphics())
                {
                    PaintFrame(g);
                }
            }
            else if (m.Msg == WM_PRINTCLIENT)
            {
                // DrawToBitmap や印刷など、画面以外への描画要求
                using (Graphics g = Graphics.FromHdc(m.WParam))
                {
                    PaintFrame(g);
                }
            }
        }

        /// <summary>
        /// <paramref name="region"/>（このコントロール内の座標）の範囲に、親コントロールの描画内容を描く。
        /// 親の背景が透明（背景画像の上に置かれているなど）で、角丸の外側を単色で塗れない場合に使う。
        /// </summary>
        private void PaintParentContent(Graphics g, Region region)
        {
            Control parent = Parent;
            if (parent == null)
            {
                return;
            }

            GraphicsState state = g.Save();
            try
            {
                g.SetClip(region, CombineMode.Intersect);

                // 親の座標系に合わせてから、親に自分自身の背景と前景を描かせる
                g.TranslateTransform(-Left, -Top);
                using (var args = new PaintEventArgs(g, new Rectangle(Left, Top, Width, Height)))
                {
                    InvokePaintBackground(parent, args);
                    InvokePaint(parent, args);
                }
            }
            finally
            {
                g.Restore(state);
            }
        }

        /// <summary>
        /// 閉じた状態の見た目をすべて描く（<see cref="FillColor"/> を指定しているとき）。
        /// 下から順に、親の描画内容、ぼかした背景（指定時）、塗り、枠線、選択中の項目の文字列、矢印。
        /// </summary>
        private void PaintOwnSurface(Graphics g)
        {
            float scale = LayoutScale.Of(this);
            Color textColor = Enabled ? ForeColor : SystemColors.GrayText;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 親の描画内容を全面に描いて下地にする。角丸の外側と、半透明の塗りの向こう側に見える
            using (var whole = new Region(new Rectangle(0, 0, Width, Height)))
            {
                PaintParentContent(g, whole);
            }

            var frameBounds = new RectangleF(0f, 0f, Width - 1, Height - 1);
            using (GraphicsPath frame = PaintHelper.CreateRoundedRectangle(frameBounds, _cornerRadius * scale))
            using (var fillBrush = new SolidBrush(_fillColor))
            using (var borderPen = new Pen(_borderColor, Math.Max(1f, (float)Math.Round(scale))))
            {
                if (_backdropBlur && IsHandleCreated)
                {
                    var source = FindForm() as IBackdropBlurSource;
                    if (source != null)
                    {
                        // このコントロールの左上が、フォームの表示領域のどこに当たるかを求めて渡す
                        Point origin = ((Control)source).PointToClient(PointToScreen(Point.Empty));
                        source.FillBlurredBackdrop(g, frame, origin);
                    }
                }
                g.FillPath(fillBrush, frame);
                g.DrawPath(borderPen, frame);
            }

            // 選択中の項目。位置は標準の描画（OnDrawItem）と同じになるようにしている
            Rectangle buttonBounds = GetButtonBounds(scale);
            int itemLeft = RightToLeft == RightToLeft.Yes ? buttonBounds.Right : ItemInset;
            int itemRight = RightToLeft == RightToLeft.Yes ? Width - ItemInset : buttonBounds.Left;
            var itemBounds = new Rectangle(itemLeft, ItemInset, Math.Max(0, itemRight - itemLeft), Math.Max(0, Height - ItemInset * 2));
            if (SelectedIndex >= 0)
            {
                int padding = (int)Math.Round(TextLeftPadding * scale);
                var textBounds = new Rectangle(itemBounds.X + padding, itemBounds.Y, Math.Max(0, itemBounds.Width - padding), itemBounds.Height);
                TextRenderer.DrawText(
                    g,
                    GetItemText(Items[SelectedIndex]),
                    Font,
                    textBounds,
                    textColor,
                    TextFormatFlags.Left |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.SingleLine |
                    TextFormatFlags.NoPadding |
                    TextFormatFlags.NoPrefix |
                    TextFormatFlags.EndEllipsis);
            }

            DrawArrow(g, buttonBounds, textColor, scale);

            // キーボード操作時のフォーカス表示
            if (Focused && ShowFocusCues && !DroppedDown)
            {
                ControlPaint.DrawFocusRectangle(g, itemBounds);
            }
        }

        /// <summary>右端（右から左へ書く設定では左端）の、矢印を描く領域。</summary>
        private Rectangle GetButtonBounds(float scale)
        {
            int buttonWidth = SystemInformation.VerticalScrollBarWidth + (int)Math.Round(4f * scale);
            int buttonLeft = RightToLeft == RightToLeft.Yes ? 1 : Width - buttonWidth - 1;
            return new Rectangle(buttonLeft, 1, buttonWidth, Height - 2);
        }

        /// <summary>山形の矢印を描く。</summary>
        private static void DrawArrow(Graphics g, Rectangle buttonBounds, Color color, float scale)
        {
            float centerX = buttonBounds.Left + buttonBounds.Width / 2f - 2f * scale;
            float centerY = buttonBounds.Top + buttonBounds.Height / 2f;
            float halfWidth = ArrowHalfWidth * scale;
            float halfHeight = ArrowHeight * scale / 2f;
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

        private void PaintFrame(Graphics g)
        {
            float scale = LayoutScale.Of(this);
            Color surround = Parent != null ? Parent.BackColor : SystemColors.Control;
            Color background = Enabled ? BackColor : SystemColors.Control;
            Color arrowColor = Enabled ? ForeColor : SystemColors.GrayText;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 矢印ボタンの領域を背景色で塗り直し、山形の矢印を描く
            Rectangle buttonBounds = GetButtonBounds(scale);
            using (var backgroundBrush = new SolidBrush(background))
            {
                g.FillRectangle(backgroundBrush, buttonBounds);
            }
            DrawArrow(g, buttonBounds, arrowColor, scale);

            // 角丸の外側を親の背景色で塗ってから、枠線を描く
            var frameBounds = new RectangleF(0f, 0f, Width - 1, Height - 1);
            using (GraphicsPath frame = PaintHelper.CreateRoundedRectangle(frameBounds, _cornerRadius * scale))
            {
                using (var outside = new Region(new Rectangle(0, 0, Width, Height)))
                {
                    outside.Exclude(frame);
                    if (surround.A == 255)
                    {
                        using (var surroundBrush = new SolidBrush(surround))
                        {
                            g.FillRegion(surroundBrush, outside);
                        }
                    }
                    else
                    {
                        PaintParentContent(g, outside);
                    }
                }

                using (var borderPen = new Pen(_borderColor, Math.Max(1f, (float)Math.Round(scale))))
                {
                    g.DrawPath(borderPen, frame);
                }
            }
        }
    }
}
