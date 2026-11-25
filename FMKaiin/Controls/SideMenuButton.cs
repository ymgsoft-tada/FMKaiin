using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 画面の左側に縦に並べる、区分選択用のボタン。アイコンと文字列を横並びで表示する。
    /// <see cref="Selected"/> を true にすると、角丸の塗りと選択時の文字色で「いま選ばれている区分」を示す。
    /// 選択していないときは背景を持たず、マウスオーバー時だけ淡い背景を付ける。
    /// このボタンは選択中の見た目を持つだけで、複数のボタンのうち 1 つだけを選択状態にする制御はしない。
    /// 押されたときにどのボタンを選択状態にするかは、置いた画面の側で決める。
    /// 寸法のプロパティはすべて 96 DPI 基準のピクセルで指定する。
    /// </summary>
    public class SideMenuButton : IconButtonBase
    {
        /// <summary>文字列の右側に確保する余白。</summary>
        private const float TextRightPadding = 4f;
        /// <summary>選択中のボタンを押下したときに、塗り色を黒へ寄せる割合。</summary>
        private const float SelectedPressedBlend = 0.12f;
        /// <summary>選択していないボタンを押下したときに、背景色を文字の色へ寄せる割合。</summary>
        private const float PressedBlend = 0.1f;

        private static readonly Color DefaultSelectedFillColor = Color.FromArgb(38, 86, 134);
        private static readonly Color DefaultSelectedForeColor = Color.White;
        private static readonly Color DefaultHoverColor = Color.FromArgb(230, 240, 248);

        private bool _selected;
        private Color _selectedFillColor = DefaultSelectedFillColor;
        private Color _selectedForeColor = DefaultSelectedForeColor;
        private Color _hoverColor = DefaultHoverColor;
        private int _cornerRadius = 6;
        private int _iconSize = 22;
        private int _iconLeft = 14;
        private int _textGap = 12;

        /// <summary>選択中かどうか。true のとき、選択中の見た目で描く。</summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("選択中かどうか。true のとき、選択中の見た目（角丸の塗りと選択時の文字色）で描く。")]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected == value)
                {
                    return;
                }
                _selected = value;
                Invalidate();
            }
        }

        /// <summary>選択中の塗り色。半透明色も指定できる。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "38, 86, 134")]
        [Description("選択中の塗り色。")]
        public Color SelectedFillColor
        {
            get { return _selectedFillColor; }
            set { _selectedFillColor = value; Invalidate(); }
        }

        /// <summary>選択中の文字とアイコンの色。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "White")]
        [Description("選択中の文字とアイコンの色。")]
        public Color SelectedForeColor
        {
            get { return _selectedForeColor; }
            set { _selectedForeColor = value; Invalidate(); }
        }

        /// <summary>選択していないときの、マウスオーバー時の背景色。押下時の色もここから作る。半透明色も指定できる。</summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "230, 240, 248")]
        [Description("選択していないときの、マウスオーバー時の背景色。押下時の色もここから作る。")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; Invalidate(); }
        }

        /// <summary>背景の角の半径。0 にすると角丸なしになる。</summary>
        [Category("Appearance")]
        [DefaultValue(6)]
        [Description("背景の角の半径。0 にすると角丸なしになる。")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>アイコンの一辺。ボタンの高さに収まらない場合は自動で縮む。</summary>
        [Category("Appearance")]
        [DefaultValue(22)]
        [Description("アイコンの一辺。ボタンの高さに収まらない場合は自動で縮む。")]
        public int IconSize
        {
            get { return _iconSize; }
            set { _iconSize = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>左端からアイコンまでの間隔。</summary>
        [Category("Appearance")]
        [DefaultValue(14)]
        [Description("左端からアイコンまでの間隔。")]
        public int IconLeft
        {
            get { return _iconLeft; }
            set { _iconLeft = Math.Max(0, value); Invalidate(); }
        }

        /// <summary>アイコンと文字列の間隔。</summary>
        [Category("Appearance")]
        [DefaultValue(12)]
        [Description("アイコンと文字列の間隔。")]
        public int TextGap
        {
            get { return _textGap; }
            set { _textGap = Math.Max(0, value); Invalidate(); }
        }

        protected override void PaintButton(Graphics g)
        {
            float scale = DpiScale;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var bounds = new RectangleF(0f, 0f, Width - 1, Height - 1);
            float radius = _cornerRadius * scale;
            float lineWidth = Math.Max(1f, (float)Math.Round(scale));

            // 背景。選択中は常に塗り、選択していないときはマウスオーバー時と押下時だけ塗る
            Color background = Color.Empty;
            if (_selected)
            {
                background = Enabled && IsPressed
                    ? PaintHelper.Blend(_selectedFillColor, Color.Black, SelectedPressedBlend)
                    : _selectedFillColor;
            }
            else if (Enabled && IsPressed)
            {
                background = PaintHelper.Blend(_hoverColor, ForeColor, PressedBlend);
            }
            else if (Enabled && IsHovered)
            {
                background = _hoverColor;
            }

            if (!background.IsEmpty)
            {
                using (GraphicsPath path = PaintHelper.CreateRoundedRectangle(bounds, radius))
                using (var brush = new SolidBrush(background))
                {
                    g.FillPath(brush, path);
                }
            }

            // 文字とアイコンの色。選択中はどちらも選択時の色にそろえる
            Color textColor = CurrentTextColor;
            Color iconColor = CurrentIconColor;
            if (_selected && Enabled)
            {
                textColor = _selectedForeColor;
                iconColor = _selectedForeColor;
            }

            // キーボード操作時のフォーカス表示。選択中は濃い塗りの上でも見えるよう、内側に選択時の文字色で引く
            if (Focused && ShowFocusCues)
            {
                RectangleF focusBounds = _selected ? RectangleF.Inflate(bounds, -2f * scale, -2f * scale) : bounds;
                Color focusColor = _selected ? _selectedForeColor : SystemColors.Highlight;
                float focusRadius = _selected ? Math.Max(0f, radius - 2f * scale) : radius;
                using (GraphicsPath focus = PaintHelper.CreateRoundedRectangle(focusBounds, focusRadius))
                using (var focusPen = new Pen(focusColor, lineWidth))
                {
                    g.DrawPath(focusPen, focus);
                }
            }

            // アイコンなしの場合はアイコンの場所を空けず、左端の余白のすぐ後から文字列を描く
            float textStart = _iconLeft * scale;
            if (IconKind != MenuIconKind.None)
            {
                float iconSide = Math.Max(0f, Math.Min(_iconSize * scale, Height - 4f * scale));
                var iconBounds = new RectangleF(_iconLeft * scale, (Height - iconSide) / 2f, iconSide, iconSide);
                MenuIconPainter.Draw(g, IconKind, iconBounds, iconColor, IconLineWeight);
                textStart = iconBounds.Right + _textGap * scale;
            }

            int textLeft = (int)Math.Round(textStart);
            int textRight = Width - (int)Math.Round(TextRightPadding * scale);
            DrawButtonText(g, new Rectangle(textLeft, 0, textRight - textLeft, Height), textColor);
        }
    }
}
