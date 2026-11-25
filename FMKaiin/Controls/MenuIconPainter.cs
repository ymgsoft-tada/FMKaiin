using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace App.Controls
{
    /// <summary>
    /// メニュー用の線画アイコンをベクター描画する。
    /// 各アイコンは 24×24 の設計グリッド上で定義し、描画先の大きさに合わせて拡大縮小するため、
    /// 画像リソースを持たずにどの大きさ・DPI でも同じ見た目になる。
    /// </summary>
    public static class MenuIconPainter
    {
        /// <summary>設計グリッドの一辺。</summary>
        private const float GridSize = 24f;
        /// <summary>設計グリッド上での標準の線幅。</summary>
        private const float StrokeWidth = 2f;
        /// <summary>設計グリッド上での細部用の線幅。</summary>
        private const float ThinStrokeWidth = 1.4f;

        /// <summary>設計グリッド上での飾り枠の外側の線幅。</summary>
        private const float RingStrokeWidth = 1.6f;
        /// <summary>設計グリッド上での飾り枠の内側の線幅。</summary>
        private const float RingInnerStrokeWidth = 0.5f;

        /// <summary>
        /// <paramref name="bounds"/> の中央に、収まる最大の正方形でアイコンを描画する。
        /// </summary>
        public static void Draw(Graphics g, MenuIconKind kind, RectangleF bounds, Color color)
        {
            Draw(g, kind, bounds, color, 1f);
        }

        /// <summary>
        /// <paramref name="bounds"/> の中央に、収まる最大の正方形でアイコンを描画する。
        /// <paramref name="lineWeight"/> は線の太さの倍率で、1 が標準。
        /// アイコンを大きく表示するときに線だけ細く保ちたい場合は 1 より小さくする。
        /// </summary>
        public static void Draw(Graphics g, MenuIconKind kind, RectangleF bounds, Color color, float lineWeight)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }
            if (kind == MenuIconKind.None || bounds.Width <= 0f || bounds.Height <= 0f || lineWeight <= 0f)
            {
                return;
            }

            GraphicsState state = EnterGrid(g, bounds);
            try
            {
                using (Pen pen = CreatePen(color, StrokeWidth * lineWeight))
                using (Pen thinPen = CreatePen(color, ThinStrokeWidth * lineWeight))
                using (var brush = new SolidBrush(color))
                {
                    DrawIcon(g, kind, pen, thinPen, brush);
                }
            }
            finally
            {
                g.Restore(state);
            }
        }

        /// <summary>
        /// <paramref name="bounds"/> の中央に、アイコンを囲むための円形の飾り枠を描画する。
        /// 外側は上下左右の 4 か所で切れた太めの円、内側は細い円の二重線になる。
        /// 中に入れるアイコンは、呼び出し側が枠より小さい領域に <see cref="Draw(Graphics, MenuIconKind, RectangleF, Color)"/> で描く。
        /// </summary>
        public static void DrawRing(Graphics g, RectangleF bounds, Color color)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }
            if (bounds.Width <= 0f || bounds.Height <= 0f)
            {
                return;
            }

            const float Center = GridSize / 2f;
            const float OuterRadius = 11f;
            const float InnerRadius = 8.8f;
            const float GapAngle = 24f;

            GraphicsState state = EnterGrid(g, bounds);
            try
            {
                using (Pen outerPen = CreatePen(color, RingStrokeWidth))
                using (Pen innerPen = CreatePen(color, RingInnerStrokeWidth))
                {
                    // 切れ目が真上・右・真下・左に来るよう、各切れ目の間を 1 本ずつ円弧で結ぶ
                    for (int i = 0; i < 4; i++)
                    {
                        float start = i * 90f + GapAngle / 2f;
                        g.DrawArc(
                            outerPen,
                            Center - OuterRadius,
                            Center - OuterRadius,
                            OuterRadius * 2f,
                            OuterRadius * 2f,
                            start,
                            90f - GapAngle);
                    }
                    DrawCircle(g, innerPen, Center, Center, InnerRadius);
                }
            }
            finally
            {
                g.Restore(state);
            }
        }

        /// <summary>
        /// 描画先を、<paramref name="bounds"/> の中央に収まる最大の正方形へ設計グリッドを写した座標系に切り替える。
        /// 戻り値を <see cref="Graphics.Restore"/> に渡すと元に戻る。
        /// </summary>
        private static GraphicsState EnterGrid(Graphics g, RectangleF bounds)
        {
            float size = Math.Min(bounds.Width, bounds.Height);
            GraphicsState state = g.Save();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TranslateTransform(
                bounds.X + (bounds.Width - size) / 2f,
                bounds.Y + (bounds.Height - size) / 2f);
            g.ScaleTransform(size / GridSize, size / GridSize);
            return state;
        }

        private static void DrawIcon(Graphics g, MenuIconKind kind, Pen pen, Pen thinPen, Brush brush)
        {
            switch (kind)
            {
                case MenuIconKind.Stethoscope: DrawStethoscope(g, pen); break;
                case MenuIconKind.Calendar: DrawCalendar(g, pen); break;
                case MenuIconKind.Users: DrawUsers(g, pen); break;
                case MenuIconKind.User: DrawUser(g, pen); break;
                case MenuIconKind.Laptop: DrawLaptop(g, pen); break;
                case MenuIconKind.FileDown: DrawFileDown(g, pen); break;
                case MenuIconKind.ListChecks: DrawListChecks(g, pen); break;
                case MenuIconKind.Calculator: DrawCalculator(g, pen, brush); break;
                case MenuIconKind.Printer: DrawPrinter(g, pen); break;
                case MenuIconKind.Bank: DrawBank(g, pen); break;
                case MenuIconKind.ExternalLink: DrawExternalLink(g, pen); break;
                case MenuIconKind.Document: DrawDocument(g, pen); break;
                case MenuIconKind.Gear: DrawGear(g, pen); break;
                case MenuIconKind.Tag: DrawTag(g, pen, brush); break;
                case MenuIconKind.Coins: DrawCoins(g, pen); break;
                case MenuIconKind.Tax: DrawTax(g, pen, thinPen); break;
                case MenuIconKind.Upload: DrawUpload(g, pen); break;
                case MenuIconKind.LogOut: DrawLogOut(g, pen); break;
                case MenuIconKind.Leaf: DrawLeaf(g, pen.Color); break;
                case MenuIconKind.Sliders: DrawSliders(g, pen); break;
                case MenuIconKind.LeafOutline: DrawLeafOutline(g, thinPen); break;
                case MenuIconKind.CalendarDays: DrawCalendarDays(g, pen, brush); break;
                case MenuIconKind.FileText: DrawFileText(g, pen); break;
                case MenuIconKind.ChevronRight: DrawPolyline(g, pen, 9f, 5f, 16f, 12f, 9f, 19f); break;
                case MenuIconKind.LayeredSquares: DrawLayeredSquares(g, pen); break;
                case MenuIconKind.DoubleSlash: DrawDoubleSlash(g, pen); break;
                case MenuIconKind.OrganizationLogo: DrawOrganizationLogo(g, brush); break;
                case MenuIconKind.Hospital: DrawHospital(g, pen); break;
                case MenuIconKind.BuildingList: DrawBuildingList(g, pen); break;
                case MenuIconKind.Database: DrawDatabase(g, pen); break;
                case MenuIconKind.BarChart: DrawBarChart(g, pen); break;
                case MenuIconKind.Table: DrawTable(g, pen); break;
                case MenuIconKind.Receipt: DrawReceipt(g, pen); break;
                case MenuIconKind.FileSpreadsheet: DrawFileSpreadsheet(g, pen); break;
                case MenuIconKind.FilePlus: DrawFilePlus(g, pen); break;
                case MenuIconKind.DocumentCoin: DrawDocumentCoin(g, pen); break;
                case MenuIconKind.Info: DrawInfo(g, pen, brush); break;
            }
        }

        #region 各アイコン

        private static void DrawStethoscope(Graphics g, Pen pen)
        {
            using (var path = new GraphicsPath())
            {
                // 耳管から胸の U 字まで
                path.AddLine(5f, 3f, 4f, 3f);
                AddArc(path, 4f, 5f, 2f, 270f, -90f);
                path.AddLine(2f, 5f, 2f, 9f);
                AddArc(path, 8f, 9f, 6f, 180f, -180f);
                path.AddLine(14f, 9f, 14f, 5f);
                AddArc(path, 12f, 5f, 2f, 0f, -90f);
                path.AddLine(12f, 3f, 11f, 3f);

                // チューブ
                path.StartFigure();
                AddArc(path, 14f, 15f, 6f, 180f, -180f);
                path.AddLine(20f, 15f, 20f, 12f);

                g.DrawPath(pen, path);
            }

            g.DrawLine(pen, 5f, 2f, 5f, 4f);
            g.DrawLine(pen, 11f, 2f, 11f, 4f);
            DrawCircle(g, pen, 20f, 10f, 2f);
        }

        private static void DrawCalendar(Graphics g, Pen pen)
        {
            DrawRoundedRectangle(g, pen, 3f, 4f, 18f, 18f, 2f);
            g.DrawLine(pen, 8f, 2f, 8f, 6f);
            g.DrawLine(pen, 16f, 2f, 16f, 6f);
            g.DrawLine(pen, 3f, 10f, 21f, 10f);
        }

        private static void DrawUsers(Graphics g, Pen pen)
        {
            using (var path = new GraphicsPath())
            {
                // 手前の人物の肩
                path.AddLine(2f, 21f, 2f, 19f);
                AddArc(path, 6f, 19f, 4f, 180f, 90f);
                path.AddLine(6f, 15f, 12f, 15f);
                AddArc(path, 12f, 19f, 4f, 270f, 90f);
                path.AddLine(16f, 19f, 16f, 21f);

                // 奥の人物の肩
                path.StartFigure();
                AddArc(path, 18f, 19f, 4f, 284.5f, 75.5f);
                path.AddLine(22f, 19f, 22f, 21f);

                // 奥の人物の頭
                path.StartFigure();
                AddArc(path, 15f, 7f, 4f, 284.5f, 151f);

                g.DrawPath(pen, path);
            }

            DrawCircle(g, pen, 9f, 7f, 4f);
        }

        private static void DrawUser(Graphics g, Pen pen)
        {
            using (var path = new GraphicsPath())
            {
                path.AddLine(5f, 21f, 5f, 19f);
                AddArc(path, 9f, 19f, 4f, 180f, 90f);
                path.AddLine(9f, 15f, 15f, 15f);
                AddArc(path, 15f, 19f, 4f, 270f, 90f);
                path.AddLine(19f, 19f, 19f, 21f);
                g.DrawPath(pen, path);
            }

            DrawCircle(g, pen, 12f, 7f, 4f);
        }

        private static void DrawLaptop(Graphics g, Pen pen)
        {
            DrawRoundedRectangle(g, pen, 4f, 5f, 16f, 11f, 1.5f);
            g.DrawLine(pen, 2f, 20f, 22f, 20f);
        }

        private static void DrawFileDown(Graphics g, Pen pen)
        {
            DrawFoldedSheet(g, pen);
            g.DrawLine(pen, 12f, 12f, 12f, 18f);
            DrawPolyline(g, pen, 9f, 15f, 12f, 18f, 15f, 15f);
        }

        private static void DrawFileText(Graphics g, Pen pen)
        {
            DrawFoldedSheet(g, pen);
            g.DrawLine(pen, 8f, 9f, 10f, 9f);
            g.DrawLine(pen, 8f, 13f, 16f, 13f);
            g.DrawLine(pen, 8f, 17f, 16f, 17f);
        }

        private static void DrawFileSpreadsheet(Graphics g, Pen pen)
        {
            DrawFoldedSheet(g, pen);
            g.DrawLine(pen, 9f, 12f, 15f, 18f);
            g.DrawLine(pen, 15f, 12f, 9f, 18f);
        }

        private static void DrawFilePlus(Graphics g, Pen pen)
        {
            DrawFoldedSheet(g, pen);
            g.DrawLine(pen, 12f, 11.5f, 12f, 18.5f);
            g.DrawLine(pen, 8.5f, 15f, 15.5f, 15f);
        }

        /// <summary>右上が折れた書類の外形。FileDown・FileText・FileSpreadsheet・FilePlus で共通に使う。</summary>
        private static void DrawFoldedSheet(Graphics g, Pen pen)
        {
            using (var path = new GraphicsPath())
            {
                // 書類の外形（右上は折り返しのため斜めに閉じる）
                path.AddLine(14.5f, 2f, 6f, 2f);
                AddArc(path, 6f, 4f, 2f, 270f, -90f);
                path.AddLine(4f, 4f, 4f, 20f);
                AddArc(path, 6f, 20f, 2f, 180f, -90f);
                path.AddLine(6f, 22f, 18f, 22f);
                AddArc(path, 18f, 20f, 2f, 90f, -90f);
                path.AddLine(20f, 20f, 20f, 7.5f);
                path.CloseFigure();

                // 折り返し
                path.StartFigure();
                path.AddLine(14f, 2f, 14f, 6f);
                AddArc(path, 16f, 6f, 2f, 180f, -90f);
                path.AddLine(16f, 8f, 20f, 8f);

                g.DrawPath(pen, path);
            }
        }

        private static void DrawCalendarDays(Graphics g, Pen pen, Brush brush)
        {
            DrawCalendar(g, pen);

            // 日付の点（3 列 × 2 行）
            float[] columns = { 8f, 12f, 16f };
            float[] rows = { 14f, 18f };
            foreach (float y in rows)
            {
                foreach (float x in columns)
                {
                    FillCircle(g, brush, x, y, 0.95f);
                }
            }
        }

        private static void DrawListChecks(Graphics g, Pen pen)
        {
            float[] rowCenters = { 6f, 12f, 18f };
            foreach (float y in rowCenters)
            {
                DrawPolyline(g, pen, 3f, y, 5f, y + 2f, 8.5f, y - 1.5f);
                g.DrawLine(pen, 12.5f, y, 21f, y);
            }
        }

        private static void DrawCalculator(Graphics g, Pen pen, Brush brush)
        {
            DrawRoundedRectangle(g, pen, 4f, 2f, 16f, 20f, 2f);
            g.DrawLine(pen, 8f, 6.5f, 16f, 6.5f);

            float[] columns = { 8f, 12f, 16f };
            float[] rows = { 11f, 14.5f, 18f };
            foreach (float y in rows)
            {
                foreach (float x in columns)
                {
                    FillCircle(g, brush, x, y, 1.1f);
                }
            }
        }

        private static void DrawPrinter(Graphics g, Pen pen)
        {
            using (var path = new GraphicsPath())
            {
                // 本体
                path.AddLine(6f, 18f, 4f, 18f);
                AddArc(path, 4f, 16f, 2f, 90f, 90f);
                path.AddLine(2f, 16f, 2f, 11f);
                AddArc(path, 4f, 11f, 2f, 180f, 90f);
                path.AddLine(4f, 9f, 20f, 9f);
                AddArc(path, 20f, 11f, 2f, 270f, 90f);
                path.AddLine(22f, 11f, 22f, 16f);
                AddArc(path, 20f, 16f, 2f, 0f, 90f);
                path.AddLine(20f, 18f, 18f, 18f);
                g.DrawPath(pen, path);
            }

            // 給紙側の用紙
            DrawPolyline(g, pen, 6f, 9f, 6f, 2f, 18f, 2f, 18f, 9f);
            // 排紙側の用紙
            DrawRoundedRectangle(g, pen, 6f, 14f, 12f, 8f, 1f);
        }

        private static void DrawBank(Graphics g, Pen pen)
        {
            g.DrawPolygon(pen, new[]
            {
                new PointF(12f, 2.5f),
                new PointF(21f, 7.5f),
                new PointF(3f, 7.5f)
            });

            float[] columns = { 6f, 10f, 14f, 18f };
            foreach (float x in columns)
            {
                g.DrawLine(pen, x, 11f, x, 17.5f);
            }

            g.DrawLine(pen, 3f, 21f, 21f, 21f);
        }

        private static void DrawExternalLink(Graphics g, Pen pen)
        {
            using (var path = new GraphicsPath())
            {
                path.AddLine(18f, 13f, 18f, 19f);
                AddArc(path, 16f, 19f, 2f, 0f, 90f);
                path.AddLine(16f, 21f, 5f, 21f);
                AddArc(path, 5f, 19f, 2f, 90f, 90f);
                path.AddLine(3f, 19f, 3f, 8f);
                AddArc(path, 5f, 8f, 2f, 180f, 90f);
                path.AddLine(5f, 6f, 11f, 6f);
                g.DrawPath(pen, path);
            }

            DrawPolyline(g, pen, 15f, 3f, 21f, 3f, 21f, 9f);
            g.DrawLine(pen, 10f, 14f, 21f, 3f);
        }

        private static void DrawDocument(Graphics g, Pen pen)
        {
            DrawRoundedRectangle(g, pen, 4f, 3f, 16f, 18f, 2f);
            g.DrawLine(pen, 8f, 8f, 16f, 8f);
            g.DrawLine(pen, 8f, 12f, 16f, 12f);
            g.DrawLine(pen, 8f, 16f, 16f, 16f);
        }

        private static void DrawGear(Graphics g, Pen pen)
        {
            const int ToothCount = 8;
            const float Center = 12f;
            const float OuterRadius = 10f;
            const float InnerRadius = 7.4f;
            const float ToothTopHalfAngle = 8.5f;
            const float ToothBaseHalfAngle = 15f;

            var points = new PointF[ToothCount * 4];
            for (int i = 0; i < ToothCount; i++)
            {
                float angle = i * (360f / ToothCount) - 90f;
                points[i * 4] = PolarPoint(Center, Center, InnerRadius, angle - ToothBaseHalfAngle);
                points[i * 4 + 1] = PolarPoint(Center, Center, OuterRadius, angle - ToothTopHalfAngle);
                points[i * 4 + 2] = PolarPoint(Center, Center, OuterRadius, angle + ToothTopHalfAngle);
                points[i * 4 + 3] = PolarPoint(Center, Center, InnerRadius, angle + ToothBaseHalfAngle);
            }

            g.DrawPolygon(pen, points);
            DrawCircle(g, pen, Center, Center, 3f);
        }

        private static void DrawTag(Graphics g, Pen pen, Brush brush)
        {
            g.DrawPolygon(pen, new[]
            {
                new PointF(2.5f, 2.5f),
                new PointF(12f, 2.5f),
                new PointF(21f, 11.5f),
                new PointF(11.5f, 21f),
                new PointF(2.5f, 12f)
            });
            FillCircle(g, brush, 7.5f, 7.5f, 1.2f);
        }

        private static void DrawCoins(Graphics g, Pen pen)
        {
            // 手前の硬貨
            DrawCircle(g, pen, 8f, 8f, 6f);
            DrawPolyline(g, pen, 7f, 6f, 8f, 6f, 8f, 10f);

            // 奥の硬貨（手前の硬貨に隠れる部分は描かない）
            g.DrawArc(pen, 10f, 10f, 12f, 12f, 290.4f, 230.1f);
            DrawPolyline(g, pen, 16.71f, 13.88f, 17.41f, 14.59f, 14.59f, 17.41f);
        }

        private static void DrawTax(Graphics g, Pen pen, Pen thinPen)
        {
            DrawRoundedRectangle(g, pen, 3f, 3f, 18f, 18f, 2f);

            // "TAX" の文字（フォントに依存しないよう線で描く）
            g.DrawLine(thinPen, 6.4f, 7f, 9.6f, 7f);
            g.DrawLine(thinPen, 8f, 7f, 8f, 11f);
            DrawPolyline(g, thinPen, 10.4f, 11f, 12f, 7f, 13.6f, 11f);
            g.DrawLine(thinPen, 11f, 9.7f, 13f, 9.7f);
            g.DrawLine(thinPen, 14.5f, 7f, 17.6f, 11f);
            g.DrawLine(thinPen, 17.6f, 7f, 14.5f, 11f);

            g.DrawLine(thinPen, 7f, 14.5f, 17f, 14.5f);
            g.DrawLine(thinPen, 7f, 17.5f, 17f, 17.5f);
        }

        private static void DrawUpload(Graphics g, Pen pen)
        {
            using (var path = new GraphicsPath())
            {
                path.AddLine(3f, 15f, 3f, 19f);
                AddArc(path, 5f, 19f, 2f, 180f, -90f);
                path.AddLine(5f, 21f, 19f, 21f);
                AddArc(path, 19f, 19f, 2f, 90f, -90f);
                path.AddLine(21f, 19f, 21f, 15f);
                g.DrawPath(pen, path);
            }

            DrawPolyline(g, pen, 17f, 8f, 12f, 3f, 7f, 8f);
            g.DrawLine(pen, 12f, 3f, 12f, 15f);
        }

        private static void DrawLogOut(Graphics g, Pen pen)
        {
            using (var path = new GraphicsPath())
            {
                path.AddLine(9f, 21f, 5f, 21f);
                AddArc(path, 5f, 19f, 2f, 90f, 90f);
                path.AddLine(3f, 19f, 3f, 5f);
                AddArc(path, 5f, 5f, 2f, 180f, 90f);
                path.AddLine(5f, 3f, 9f, 3f);
                g.DrawPath(pen, path);
            }

            DrawPolyline(g, pen, 16f, 17f, 21f, 12f, 16f, 7f);
            g.DrawLine(pen, 21f, 12f, 9f, 12f);
        }

        private static void DrawLeaf(Graphics g, Color color)
        {
            // 線画ではなく塗りつぶしで描く。左上を明るく、右下を指定色にして立体感を付ける
            Color highlight = PaintHelper.Blend(color, Color.White, 0.4f);

            using (var leaf = new GraphicsPath(FillMode.Alternate))
            using (var vein = new GraphicsPath())
            using (Pen veinPen = CreatePen(Color.Black, 1.2f))
            using (var brush = new LinearGradientBrush(new PointF(6f, 6f), new PointF(18f, 18f), highlight, color))
            {
                // グラデーションの範囲から外れた部分は折り返して、色の継ぎ目が出ないようにする
                brush.WrapMode = WrapMode.TileFlipXY;

                // 葉の外形。左下の付け根から右上の葉先へ向かう
                leaf.AddBezier(4.5f, 19.5f, 1.5f, 10f, 9.5f, 2.5f, 21.5f, 2.5f);
                leaf.AddBezier(21.5f, 2.5f, 21.5f, 14.5f, 14f, 22.5f, 4.5f, 19.5f);
                leaf.CloseFigure();

                // 葉脈。外形と重なる部分は塗りが抜け（背景が透ける）、付け根より外へ出た部分は柄として塗られる
                vein.AddBezier(2.5f, 21.5f, 6f, 17.5f, 11f, 12.5f, 16.5f, 8f);
                vein.Widen(veinPen);
                leaf.AddPath(vein, false);

                g.FillPath(brush, leaf);
            }
        }

        private static void DrawLayeredSquares(Graphics g, Pen pen)
        {
            // 同じ大きさの四角を 2 つ、右上（奥）と左下（手前）にずらして重ねる
            const float Side = 14f;
            const float BackLeft = 8f;
            const float BackTop = 2f;
            const float FrontLeft = 2f;
            const float FrontTop = 8f;

            // 色は 1 色から濃淡を作る。奥は淡い塗りと薄い輪郭、手前はごく淡い塗りと指定色そのままの輪郭
            Color color = pen.Color;
            Color backFill = Color.FromArgb(color.A * 0 / 100, color);
            Color backLine = Color.FromArgb(color.A * 50 / 100, color);
            Color frontFill = Color.FromArgb(color.A * 0 / 100, color);

            // 四角の角を丸めずに描くため、線の継ぎ目が尖るペンを使う（標準のペンは継ぎ目を丸める）
            using (var backBrush = new SolidBrush(backFill))
            using (var backPen = new Pen(backLine, pen.Width * 0.6f) { LineJoin = LineJoin.Miter })
            using (var frontBrush = new SolidBrush(frontFill))
            using (var frontPen = new Pen(color, pen.Width) { LineJoin = LineJoin.Miter })
            {
                g.FillRectangle(backBrush, BackLeft, BackTop, Side, Side);
                g.DrawRectangle(backPen, BackLeft, BackTop, Side, Side);
                g.FillRectangle(frontBrush, FrontLeft, FrontTop, Side, Side);
                g.DrawRectangle(frontPen, FrontLeft, FrontTop, Side, Side);
            }
        }

        private static void DrawDoubleSlash(Graphics g, Pen pen)
        {
            // 色は 1 色から濃淡を作る。1 本目は指定色そのまま、2 本目は淡くする
            Color color = pen.Color;
            Color secondColor = Color.FromArgb(color.A * 40 / 100, color);

            // 線の端を丸めずに切り落とすため、端の形が平らなペンを使う（標準のペンは端を丸める）
            using (var firstPen = new Pen(color, pen.Width))
            using (var secondPen = new Pen(secondColor, pen.Width))
            {
                g.DrawLine(firstPen, 4f, 21f, 10.5f, 3f);
                g.DrawLine(secondPen, 12f, 21f, 18.5f, 3f);
            }
        }

        private static void DrawOrganizationLogo(Graphics g, Brush brush)
        {
            // 線画ではなく塗りつぶしで描く。形は元の画像の輪郭を写し取ったもので、線の太さの指定は影響しない
            using (GraphicsPath logo = OrganizationLogoShape.CreatePath())
            {
                g.FillPath(brush, logo);
            }
        }

        private static void DrawLeafOutline(Graphics g, Pen thinPen)
        {
            // 細部用の線よりさらに細くして、文字の横に置いても目立ちすぎないようにする
            using (Pen outlinePen = CreatePen(thinPen.Color, thinPen.Width * 0.8f))
            using (var path = new GraphicsPath())
            {
                // 葉の外形。両端がとがった細長い形で、左下の付け根から右上の葉先へ向かう
                path.AddBezier(4.5f, 18.5f, 4.5f, 10f, 12f, 4.5f, 20.5f, 5f);
                path.AddBezier(20.5f, 5f, 20f, 13f, 13f, 19.5f, 4.5f, 18.5f);
                path.CloseFigure();

                // 柄。付け根から外へ短く伸ばし、葉の中へも少しだけ入れる
                // （葉の中を端まで横切らせると、円に斜線の記号のように見えてしまう）
                path.StartFigure();
                path.AddLine(2.5f, 20.5f, 7.5f, 15.5f);

                g.DrawPath(outlinePen, path);
            }
        }

        private static void DrawSliders(Graphics g, Pen pen)
        {
            const float Left = 3f;
            const float Right = 21f;
            const float KnobRadius = 2f;

            // 各行の高さと、つまみの位置
            float[] rows = { 6.5f, 12f, 17.5f };
            float[] knobs = { 9f, 15.5f, 10.5f };

            for (int i = 0; i < rows.Length; i++)
            {
                float y = rows[i];
                float knob = knobs[i];

                // 横線はつまみの円の手前と先で分けて描き、円の中を通さない
                g.DrawLine(pen, Left, y, knob - KnobRadius, y);
                g.DrawLine(pen, knob + KnobRadius, y, Right, y);
                DrawCircle(g, pen, knob, y, KnobRadius);
            }
        }

        private static void DrawHospital(Graphics g, Pen pen)
        {
            DrawRoundedRectangle(g, pen, 4f, 3f, 16f, 19f, 2f);

            // 十字
            g.DrawLine(pen, 12f, 6.5f, 12f, 12.5f);
            g.DrawLine(pen, 9f, 9.5f, 15f, 9.5f);

            // 入口
            DrawPolyline(g, pen, 10f, 22f, 10f, 17f, 14f, 17f, 14f, 22f);
        }

        private static void DrawBuildingList(Graphics g, Pen pen)
        {
            // 建物（窓 2 段と入口）
            DrawRoundedRectangle(g, pen, 2f, 3f, 11f, 19f, 1.5f);
            g.DrawLine(pen, 5.5f, 7.5f, 9.5f, 7.5f);
            g.DrawLine(pen, 5.5f, 11.5f, 9.5f, 11.5f);
            DrawPolyline(g, pen, 6f, 22f, 6f, 17f, 9f, 17f, 9f, 22f);

            // 一覧の行
            float[] rows = { 7f, 12.5f, 18f };
            foreach (float y in rows)
            {
                g.DrawLine(pen, 17f, y, 22f, y);
            }
        }

        private static void DrawDatabase(Graphics g, Pen pen)
        {
            const float Left = 4f;
            const float Width = 16f;
            const float EllipseHeight = 6f;

            // 上面
            g.DrawEllipse(pen, Left, 2.5f, Width, EllipseHeight);

            // 側面
            g.DrawLine(pen, Left, 5.5f, Left, 18.5f);
            g.DrawLine(pen, Left + Width, 5.5f, Left + Width, 18.5f);

            // 中段の区切りと底面（手前側の半分だけ）
            g.DrawArc(pen, Left, 9f, Width, EllipseHeight, 0f, 180f);
            g.DrawArc(pen, Left, 15.5f, Width, EllipseHeight, 0f, 180f);
        }

        private static void DrawBarChart(Graphics g, Pen pen)
        {
            // 縦軸と横軸
            DrawPolyline(g, pen, 3f, 3f, 3f, 21f, 21f, 21f);

            // 棒
            g.DrawLine(pen, 8f, 17f, 8f, 13f);
            g.DrawLine(pen, 13f, 17f, 13f, 7f);
            g.DrawLine(pen, 18f, 17f, 18f, 10f);
        }

        private static void DrawTable(Graphics g, Pen pen)
        {
            DrawRoundedRectangle(g, pen, 3f, 3f, 18f, 18f, 2f);

            // 行の罫線。1 行目は見出し行として列で区切らない
            g.DrawLine(pen, 3f, 9f, 21f, 9f);
            g.DrawLine(pen, 3f, 15f, 21f, 15f);
            g.DrawLine(pen, 12f, 9f, 12f, 21f);
        }

        private static void DrawReceipt(Graphics g, Pen pen)
        {
            // 紙の外形。下端は切り取り線のようにぎざぎざにする
            g.DrawPolygon(pen, new[]
            {
                new PointF(5f, 2.5f),
                new PointF(19f, 2.5f),
                new PointF(19f, 21.5f),
                new PointF(16.67f, 19.5f),
                new PointF(14.33f, 21.5f),
                new PointF(12f, 19.5f),
                new PointF(9.67f, 21.5f),
                new PointF(7.33f, 19.5f),
                new PointF(5f, 21.5f)
            });

            g.DrawLine(pen, 9f, 8f, 15f, 8f);
            g.DrawLine(pen, 9f, 12.5f, 15f, 12.5f);
        }

        private static void DrawDocumentCoin(Graphics g, Pen pen)
        {
            const float CoinX = 16.5f;
            const float CoinY = 16.5f;
            const float CoinRadius = 5f;

            // 書類。硬貨と重なる部分は描かず、硬貨の周りに線 1 本分の隙間を空ける
            GraphicsState beforeClip = g.Save();
            using (var coinArea = new GraphicsPath())
            {
                float clearRadius = CoinRadius + pen.Width;
                coinArea.AddEllipse(CoinX - clearRadius, CoinY - clearRadius, clearRadius * 2f, clearRadius * 2f);
                g.SetClip(coinArea, CombineMode.Exclude);
            }
            DrawRoundedRectangle(g, pen, 3f, 2f, 14f, 19f, 2f);
            g.DrawLine(pen, 7f, 7f, 13f, 7f);
            g.DrawLine(pen, 7f, 11f, 9.5f, 11f);
            g.Restore(beforeClip);

            // 硬貨
            DrawCircle(g, pen, CoinX, CoinY, CoinRadius);
            g.DrawLine(pen, CoinX, CoinY - 2f, CoinX, CoinY + 2f);
        }

        private static void DrawInfo(Graphics g, Pen pen, Brush brush)
        {
            DrawCircle(g, pen, 12f, 12f, 9.5f);

            // i の字（点と縦棒）
            FillCircle(g, brush, 12f, 7.6f, 1.3f);
            g.DrawLine(pen, 12f, 11.2f, 12f, 16.6f);
        }

        #endregion

        #region 描画補助

        private static Pen CreatePen(Color color, float width)
        {
            return new Pen(color, width)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };
        }

        /// <summary>中心と半径で指定した円弧をパスに追加する。角度は度、時計回りが正。</summary>
        private static void AddArc(GraphicsPath path, float centerX, float centerY, float radius, float startAngle, float sweepAngle)
        {
            path.AddArc(centerX - radius, centerY - radius, radius * 2f, radius * 2f, startAngle, sweepAngle);
        }

        private static void DrawCircle(Graphics g, Pen pen, float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius, radius * 2f, radius * 2f);
        }

        private static void FillCircle(Graphics g, Brush brush, float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius, radius * 2f, radius * 2f);
        }

        private static void DrawRoundedRectangle(Graphics g, Pen pen, float x, float y, float width, float height, float radius)
        {
            using (GraphicsPath path = PaintHelper.CreateRoundedRectangle(new RectangleF(x, y, width, height), radius))
            {
                g.DrawPath(pen, path);
            }
        }

        /// <summary>x, y の組を並べた座標列を折れ線として描画する。</summary>
        private static void DrawPolyline(Graphics g, Pen pen, params float[] coordinates)
        {
            var points = new PointF[coordinates.Length / 2];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new PointF(coordinates[i * 2], coordinates[i * 2 + 1]);
            }
            g.DrawLines(pen, points);
        }

        private static PointF PolarPoint(float centerX, float centerY, float radius, float angleInDegrees)
        {
            double radians = angleInDegrees * Math.PI / 180.0;
            return new PointF(
                centerX + (float)(radius * Math.Cos(radians)),
                centerY + (float)(radius * Math.Sin(radians)));
        }

        #endregion
    }
}
