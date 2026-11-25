using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// アイコン付きの自前描画ボタンの基底クラス。
    /// <see cref="Button"/> を継承しているので、Click・キーボード操作・ニーモニック・タブ移動は標準のまま使える。
    /// 派生クラスは <see cref="PaintButton"/> で見た目だけを実装する。
    /// </summary>
    public abstract class IconButtonBase : Button
    {
        /// <summary>複数行の文字列の行送りの下限。文字の大きさに対する倍率。</summary>
        private const float MinimumLineSpacing = 1.3f;

        private MenuIconKind _iconKind = MenuIconKind.None;
        private Color? _iconColor;
        private float _iconLineWeight = 1f;
        private bool _hovered;
        private bool _pressed;

        protected IconButtonBase()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
        }

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
        /// アイコンの色。空（<see cref="Color.Empty"/>）の場合は <see cref="Control.ForeColor"/> と同じ色で描く。
        /// 文字の色は <see cref="Control.ForeColor"/> で指定する。
        /// 何も設定していないときの値は <see cref="DefaultIconColor"/> で、派生クラスごとに決まる。
        /// </summary>
        [Category("Appearance")]
        [Description("アイコンの色。空の場合は ForeColor（文字の色）と同じ色で描く。")]
        public Color IconColor
        {
            get { return _iconColor ?? DefaultIconColor; }
            set
            {
                _iconColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// アイコンの線の太さの倍率。1 が標準で、細い線にしたい場合は 1 より小さくする。
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(1f)]
        [Description("アイコンの線の太さの倍率。1 が標準で、細い線にしたい場合は 1 より小さくする。")]
        public float IconLineWeight
        {
            get { return _iconLineWeight; }
            set
            {
                if (value <= 0f || float.IsNaN(value) || float.IsInfinity(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "線の太さの倍率は 0 より大きい値で指定してください。");
                }
                if (_iconLineWeight == value)
                {
                    return;
                }
                _iconLineWeight = value;
                Invalidate();
            }
        }

        /// <summary>
        /// <see cref="IconColor"/> を設定していないときの色。
        /// 既定は空（<see cref="Control.ForeColor"/> と同じ色で描く）。
        /// </summary>
        protected virtual Color DefaultIconColor
        {
            get { return Color.Empty; }
        }

        // 既定値と同じ色のときはデザイナーがコードに書き出さないようにする
        private bool ShouldSerializeIconColor()
        {
            Color current = IconColor;
            Color defaultColor = DefaultIconColor;
            return current.IsEmpty != defaultColor.IsEmpty || current.ToArgb() != defaultColor.ToArgb();
        }

        // プロパティ ウィンドウの「リセット」で既定値に戻す
        private void ResetIconColor()
        {
            _iconColor = null;
            Invalidate();
        }

        /// <summary>
        /// 現在の状態で文字を描く色。無効状態ではグレーになる。
        /// </summary>
        protected Color CurrentTextColor
        {
            get { return Enabled ? ForeColor : SystemColors.GrayText; }
        }

        /// <summary>
        /// 現在の状態でアイコンを描く色。無効状態ではグレーになる。
        /// </summary>
        protected Color CurrentIconColor
        {
            get
            {
                if (!Enabled)
                {
                    return SystemColors.GrayText;
                }
                Color iconColor = IconColor;
                return iconColor.IsEmpty ? ForeColor : iconColor;
            }
        }

        /// <summary>マウスポインターが乗っているか。</summary>
        protected bool IsHovered
        {
            get { return _hovered; }
        }

        /// <summary>マウスまたはスペースキーで押下中か。</summary>
        protected bool IsPressed
        {
            get { return _pressed; }
        }

        /// <summary>96 DPI を 1 とした表示倍率。描画に使う固定寸法へ掛ける。</summary>
        protected float DpiScale
        {
            get { return LayoutScale.Of(this); }
        }

        /// <summary>
        /// ボタンの見た目を描画する。呼び出し時点で、背景は親コントロールと同じ見た目になっている
        /// （通常は親の背景色の塗りつぶし。背景が透明の場合は親コントロールの描画内容そのもの）。
        /// </summary>
        protected abstract void PaintButton(Graphics g);

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BackColor.A == 255)
            {
                // 角丸の外側が親と同じ見た目になるよう、先に全体を親と同じ見た目にする
                // （親の背景が単色ならその色で塗り、透明なら親の描画内容を写す）
                if (Parent == null)
                {
                    e.Graphics.Clear(SystemColors.Control);
                }
                else
                {
                    PaintHelper.PaintParentAppearance(this, e.Graphics, InvokePaintBackground, InvokePaint);
                }
            }
            else
            {
                // 背景が透明（BackColor が Transparent。親が透明ならそれを引き継ぐ）の場合は、
                // 親コントロールの描画内容を下地として描いてから、その上へボタンを描く。
                // 半透明のパネルの上に半透明のボタンを重ねるときに使う。
                // Button は背景を自前で描く設定（ControlStyles.Opaque）になっていて、OnPaintBackground が
                // 自動では呼ばれないため、ここで明示的に呼ぶ
                OnPaintBackground(e);
            }
            PaintButton(e.Graphics);
        }

        /// <summary>
        /// <see cref="Control.Text"/> を <paramref name="bounds"/> 内に左寄せ・上下中央で描画する。
        /// 折り返しは Text 中の改行でのみ行う。
        /// </summary>
        protected void DrawButtonText(Graphics g, Rectangle bounds, Color color)
        {
            DrawButtonText(g, bounds, color, HorizontalAlignment.Left);
        }

        /// <summary>
        /// <see cref="Control.Text"/> を <paramref name="bounds"/> 内に、左右は <paramref name="alignment"/>、上下は中央で描画する。
        /// 折り返しは Text 中の改行でのみ行う。
        /// </summary>
        protected void DrawButtonText(Graphics g, Rectangle bounds, Color color, HorizontalAlignment alignment)
        {
            if (string.IsNullOrEmpty(Text) || bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            TextFormatFlags flags = CreateTextFormatFlags(alignment);

            // 行の高さが文字の大きさちょうどしかないフォントでは複数行が詰まって見えるため、
            // 1 行ずつ描画して行送りの下限を文字の大きさ基準で確保する
            string[] lines = SplitTextLines();
            int lineHeight;
            int lineAdvance;
            MeasureTextLine(g, bounds.Width, flags, out lineHeight, out lineAdvance);
            int blockHeight = lineAdvance * (lines.Length - 1) + lineHeight;
            int top = bounds.Y + (bounds.Height - blockHeight) / 2;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length == 0)
                {
                    continue;
                }
                var lineBounds = new Rectangle(bounds.X, top + lineAdvance * i, bounds.Width, lineHeight);
                TextRenderer.DrawText(g, lines[i], Font, lineBounds, color, flags);
            }
        }

        /// <summary>
        /// <see cref="DrawButtonText(Graphics, Rectangle, Color)"/> で <see cref="Control.Text"/> を描いたときの、
        /// 文字列全体（複数行ならすべての行）の高さを返す。Text が空の場合は 0。
        /// </summary>
        /// <param name="width">文字列を描く幅。</param>
        protected int MeasureButtonTextHeight(Graphics g, int width)
        {
            if (string.IsNullOrEmpty(Text) || width <= 0)
            {
                return 0;
            }

            int lineHeight;
            int lineAdvance;
            MeasureTextLine(g, width, CreateTextFormatFlags(HorizontalAlignment.Left), out lineHeight, out lineAdvance);
            return lineAdvance * (SplitTextLines().Length - 1) + lineHeight;
        }

        private TextFormatFlags CreateTextFormatFlags(HorizontalAlignment alignment)
        {
            TextFormatFlags flags = TextFormatFlags.Top | TextFormatFlags.NoPadding;
            if (alignment == HorizontalAlignment.Center)
            {
                flags |= TextFormatFlags.HorizontalCenter;
            }
            else if (alignment == HorizontalAlignment.Right)
            {
                flags |= TextFormatFlags.Right;
            }
            else
            {
                flags |= TextFormatFlags.Left;
            }

            if (!UseMnemonic)
            {
                flags |= TextFormatFlags.NoPrefix;
            }
            else if (!ShowKeyboardCues)
            {
                flags |= TextFormatFlags.HidePrefix;
            }
            return flags;
        }

        /// <summary><see cref="Control.Text"/> を改行で区切った各行。</summary>
        private string[] SplitTextLines()
        {
            return Text.Replace("\r\n", "\n").Split('\n');
        }

        /// <summary>
        /// 1 行の高さ（<paramref name="lineHeight"/>）と、次の行までの行送り（<paramref name="lineAdvance"/>）を求める。
        /// </summary>
        private void MeasureTextLine(Graphics g, int width, TextFormatFlags flags, out int lineHeight, out int lineAdvance)
        {
            lineHeight = TextRenderer.MeasureText(g, "X", Font, new Size(width, int.MaxValue), flags).Height;
            float emHeight = Font.SizeInPoints * g.DpiY / 72f;
            lineAdvance = Math.Max(lineHeight, (int)Math.Round(emHeight * MinimumLineSpacing));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            SetHovered(true);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SetHovered(false);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                SetPressed(true);
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            SetPressed(false);
        }

        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            base.OnKeyDown(kevent);
            if (kevent.KeyCode == Keys.Space)
            {
                SetPressed(true);
            }
        }

        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            base.OnKeyUp(kevent);
            SetPressed(false);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            SetPressed(false);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!Enabled)
            {
                _hovered = false;
                _pressed = false;
            }
            Invalidate();
        }

        private void SetHovered(bool value)
        {
            if (_hovered == value)
            {
                return;
            }
            _hovered = value;
            Invalidate();
        }

        private void SetPressed(bool value)
        {
            if (_pressed == value)
            {
                return;
            }
            _pressed = value;
            Invalidate();
        }
    }
}
