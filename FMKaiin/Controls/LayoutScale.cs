using System.Drawing;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// コントロールが表示されている画面の、96 DPI を 1 とした表示倍率を求める。
    /// 自前描画で使う固定寸法（角の半径やアイコンの大きさ。すべて 96 DPI 基準のピクセルで定義している）へ掛け、
    /// レイアウトの拡大率と描画の拡大率を一致させるために使う。
    /// </summary>
    /// <remarks>
    /// フォームのデザイナー ファイルに書かれる座標は、デザイナーを開いた PC の DPI を基準にした値になる
    /// （AutoScaleDimensions がその基準を表す）。そのため「デザイン時から何倍になったか」ではなく、
    /// フォームが実行時に使っている DPI そのものを基準にしている。
    /// </remarks>
    internal static class LayoutScale
    {
        private const float BaseDpi = 96f;

        private static float? _screenScale;

        /// <summary>
        /// <paramref name="control"/> の描画に使う表示倍率を返す。
        /// </summary>
        public static float Of(Control control)
        {
            // AutoScaleMode.Dpi のフォーム（またはユーザー コントロール）は、いま使っている DPI を
            // CurrentAutoScaleDimensions で公開している。レイアウトはこの値に合わせて拡大縮小されるので、同じ値を使う
            for (Control current = control; current != null; current = current.Parent)
            {
                var container = current as ContainerControl;
                if (container != null && container.AutoScaleMode == AutoScaleMode.Dpi)
                {
                    return container.CurrentAutoScaleDimensions.Height / BaseDpi;
                }
            }

            // 上記のコンテナーに載っていない場合（配置前、または AutoScaleMode.Dpi 以外のフォーム）は画面の DPI を使う
            return ScreenScale;
        }

        private static float ScreenScale
        {
            get
            {
                if (!_screenScale.HasValue)
                {
                    using (Graphics g = Graphics.FromHwnd(System.IntPtr.Zero))
                    {
                        _screenScale = g.DpiY / BaseDpi;
                    }
                }
                return _screenScale.Value;
            }
        }
    }
}
