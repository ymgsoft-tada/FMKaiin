using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 画像を高品質に拡大縮小して表示する <see cref="PictureBox"/>。
    /// 標準の PictureBox は大きな画像を小さく表示すると輪郭が荒れるため、
    /// ロゴのように元画像より小さく表示する用途ではこちらを使う。
    /// </summary>
    public class SmoothPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            base.OnPaint(pe);
        }
    }
}
