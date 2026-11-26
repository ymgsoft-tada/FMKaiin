using System;
using System.Drawing;
using C1.Win.C1TrueDBGrid;

namespace App.Controls
{
    /// <summary>
    /// グリッド（C1TrueDBGrid）の、S 系の画面での既定の見た目と動き。<see cref="SGridDB"/> が、グリッドを受け取ったときに設定する。
    /// 色は、S 系のコントロール（<see cref="SControlStyle"/>）と、テーブル コンボの一覧（<see cref="STablePopup"/>）に合わせてある。
    /// </summary>
    /// <remarks>
    /// 動き（並べ替え・列の移動・行の追加などを禁止する設定）は、Gc 系の画面用の AppGridCommon.StyleSet と同じ。
    /// 見た目（色・フォント・行の高さ）だけを、新しいデザインに合わせて変えている。
    /// </remarks>
    internal static class SGridStyle
    {
        /// <summary>行の高さ。96 DPI 基準のピクセル。</summary>
        private const float RowHeight = 24f;

        /// <summary>列の見出しの高さ。96 DPI 基準のピクセル。</summary>
        private const float HeadingHeight = 28f;

        // 列の見出し・罫線の色は、テーブル コンボの一覧（STablePopup）と同じ
        private static readonly Color HeadingBackColor = Color.FromArgb(243, 246, 250);
        private static readonly Color HeadingForeColor = Color.FromArgb(96, 104, 120);
        private static readonly Color LineColor = Color.FromArgb(226, 232, 240);

        /// <summary>
        /// <paramref name="grid"/> に、既定の見た目と動きを設定する。
        /// </summary>
        public static void Apply(C1TrueDBGrid grid)
        {
            float scale = LayoutScale.Of(grid);
            Color selection = SControlStyle.SelectionBackColor;

            //■ 動き（AppGridCommon.StyleSet と同じ）
            // 列の移動・選択、行の高さの変更・選択、行の追加・更新、並べ替え、複数行の選択は、させない
            grid.AllowColMove = false;
            grid.AllowColSelect = false;
            grid.AllowRowSizing = RowSizingEnum.None;
            grid.AllowRowSelect = false;
            grid.AllowAddNew = false;
            grid.AllowUpdate = false;
            grid.AllowSort = false;
            grid.AllowArrows = true;
            grid.MultiSelect = MultiSelectEnum.None;
            // 指している行は、行の全体を色で示す
            grid.MarqueeStyle = MarqueeEnum.HighlightRow;
            // 左端の、行を選ぶための列は出さない
            grid.RecordSelectors = false;
            // 横スクロール バーは出さない。縦スクロール バーは、行数にかかわらず出しておく（列の幅が変わらないように）
            grid.HScrollBar.Style = ScrollBarStyleEnum.None;
            grid.VScrollBar.Style = ScrollBarStyleEnum.Always;
            // 列の幅の合計がグリッドの幅に足りない分は、いちばん右の列を広げる
            grid.ExtendRightColumn = true;

            //■ 見た目
            grid.FlatStyle = FlatModeEnum.Flat;
            // 外枠は、S 系の入力欄の枠線と同じ色
            grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            grid.BorderColor = SControlStyle.BorderColor;
            grid.Font = SControlStyle.Font;
            grid.RowHeight = (int)Math.Round(RowHeight * scale);
            // 行が無い部分の色
            grid.BackColor = Color.White;
            grid.RowDivider.Color = LineColor;
            grid.RowSubDividerColor = LineColor;
            grid.AlternatingRows = false;

            // 行
            grid.Style.Font = SControlStyle.Font;
            grid.Style.ForeColor = SControlStyle.TextColor;
            grid.Style.BackColor = Color.White;
            grid.Style.VerticalAlignment = AlignVertEnum.Center;

            // 列の見出し
            grid.HeadingStyle.Font = SControlStyle.Font;
            grid.HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
            grid.HeadingStyle.VerticalAlignment = AlignVertEnum.Center;
            grid.HeadingStyle.ForeColor = HeadingForeColor;
            grid.HeadingStyle.BackColor = HeadingBackColor;
            grid.HeadingStyle.BackColor2 = HeadingBackColor;
            grid.HeadingStyle.GradientMode = GradientModeEnum.None;
            grid.HeadingStyle.Borders.Color = LineColor;
            foreach (Split split in grid.Splits)
            {
                split.HeadingStyle.Font = SControlStyle.Font;
                split.HeadingStyle.VerticalAlignment = AlignVertEnum.Center;
                split.ColumnCaptionHeight = (int)Math.Round(HeadingHeight * scale);
            }

            // 指している行。S 系のコントロールの「選択の色」と同じにして、文字はその上で読める色にする
            grid.HighLightRowStyle.BackColor = selection;
            grid.HighLightRowStyle.BackColor2 = selection;
            grid.HighLightRowStyle.GradientMode = GradientModeEnum.None;
            grid.HighLightRowStyle.ForeColor = SControlStyle.GetTextColorOn(selection, SControlStyle.TextColor);

            // 選択した行・列（選択はさせない設定だが、色はそろえておく）
            grid.SelectedStyle.BackColor = selection;
            grid.SelectedStyle.GradientMode = GradientModeEnum.None;
            grid.SelectedStyle.ForeColor = SControlStyle.GetTextColorOn(selection, SControlStyle.TextColor);

            grid.FooterStyle.ForeColor = SControlStyle.TextColor;
        }

        /// <summary>
        /// 列 1 つ分の見た目を設定する。列は <see cref="SGridDB"/> が作るので、作るたびに呼ぶ。
        /// </summary>
        public static void ApplyColumn(C1DisplayColumn column)
        {
            // 列の間の線は、行の間の線と同じ色
            column.ColumnDivider.Color = LineColor;

            column.HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
            column.HeadingStyle.VerticalAlignment = AlignVertEnum.Center;
            column.HeadingStyle.Trimming = StringTrimming.EllipsisCharacter;
            column.Style.VerticalAlignment = AlignVertEnum.Center;
            column.Style.Trimming = StringTrimming.EllipsisCharacter;
        }
    }
}
