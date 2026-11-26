using System.Drawing;
using System.Drawing.Drawing2D;

namespace App.Controls
{
    /// <summary>
    /// 団体のロゴ（<see cref="OrganizationLogoShape"/>）を表示するコントロール。メイン画面の見出しに使っている。
    /// ロゴの色は ForeColor、背景は BackColor。背景の角丸（CornerRadius）と余白（Padding）は <see cref="IconBox"/> と同じ。
    /// </summary>
    /// <remarks>
    /// ロゴはこのアプリに固有の図柄なので、共通のアイコン（<see cref="MenuIconKind"/>・<see cref="IconBox"/>）には入れず、
    /// このコントロールに分けてある。共通の部品（Controls/Common）には置かないこと。
    /// </remarks>
    public class IconLogo : IconBoxBase
    {
        protected override void DrawIcon(Graphics g, RectangleF bounds)
        {
            // 線画ではなく塗りつぶしで描く。形は元の画像の輪郭を写し取ったもの
            using (GraphicsPath logo = OrganizationLogoShape.CreatePath())
            {
                MenuIconPainter.FillShape(g, logo, bounds, ForeColor);
            }
        }
    }
}
