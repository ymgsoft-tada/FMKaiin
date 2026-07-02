using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Diagnostics;

using C1.Win.C1TrueDBGrid;

using ComponentDB;
using ComponentIO;
using ComponentGGridDB;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// Grid で使用する基本セットです。
	/// </summary>
	public partial class AppGridCommon
	{
		/// <summary>
		/// グリッドのデフォルトスタイル設定
		/// </summary>
		public static void StyleSet(C1TrueDBGrid grid)
		{
			C1DataColumnCollection		dataCol;

			dataCol    = grid.Columns;
			
			//■ キャプションタイトル（一番上）
			grid.CaptionHeight                    = 23;
			grid.CaptionStyle.HorizontalAlignment = AlignHorzEnum.Center;
			grid.CaptionStyle.VerticalAlignment   = AlignVertEnum.Center;
			grid.CaptionStyle.ForeColor           = Color.White;
			grid.CaptionStyle.BackColor           = AppConst.GridHeaderColor;
			grid.CaptionStyle.BackColor2          = AppConst.GridHeaderColor;
			grid.CaptionStyle.GradientMode        = GradientModeEnum.None;
			// 右にちょっこりついてる人の絵
			grid.CaptionStyle.ForeGroundPicturePosition = ForeGroundPicturePositionEnum.Far;
			
			// 水平スクロールバーは出さない
			grid.HScrollBar.Style   = ScrollBarStyleEnum.None;
			// 行選択のマーキースタイル
			grid.MarqueeStyle       = MarqueeEnum.HighlightRow;
			// 行の幅（キャプションタイトル、列タイトルは別）
			grid.RowHeight          = 22;
			// 行で空欄になっている箇所のカラー
			grid.BackColor          = Color.GhostWhite;
			// 表示スタイル
			grid.FlatStyle          = FlatModeEnum.Flat;
			// １行をマルチラインにした場合の補助線
			grid.RowSubDividerColor = Color.FromArgb(255,237,237,237);
			// 行ごとの描画設定イベントを呼び出す
//			grid.FetchRowStyles     = true;
			
			//■ グリッドグローバル設定
			// 列の移動を禁止
			grid.AllowColMove           = false;
			// 列の選択を禁止
			grid.AllowColSelect         = false;
			// 行の移動を禁止
			grid.AllowRowSizing         = RowSizingEnum.None;
			// 行の選択を禁止
			grid.AllowRowSelect         = false;
			// 新規行追加を禁止
			grid.AllowAddNew            = false;
			// 行更新を禁止
			grid.AllowUpdate            = false;
			// 列ソートを禁止
			grid.AllowSort              = false;
			// 矢印キーを許可
			grid.AllowArrows            = true;
			// 複数選択を禁止
			grid.MultiSelect            = MultiSelectEnum.None;
			// 偶数・奇数スタイルの変更を許可
			grid.AlternatingRows        = true;
			// グリッドに足りない列幅を自動的に伸ばす
			grid.ExtendRightColumn      = true;
			
			SetHeadingStyle(grid);
			
//			grid.InactiveStyle.BackColor = Color.Blue;
//			grid.InactiveStyle.BackColor2 = Color.Red;
//			grid.InactiveStyle.GradientMode  = GradientModeEnum.None;
			
			// 列タイトルのスタイル
			grid.HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
			grid.HeadingStyle.VerticalAlignment   = AlignVertEnum.Center;
			grid.HeadingStyle.ForeColor     = Color.White;
//			grid.HeadingStyle.Font			= new Font(grid.HeadingStyle.Font, FontStyle.Bold);
			grid.HeadingStyle.BackColor     = AppConst.GridHeaderColor;
			grid.HeadingStyle.BackColor2    = AppConst.GridHeaderColor;


			grid.HeadingStyle.GradientMode  = GradientModeEnum.Vertical;
			// 行セレクタのスタイル
			grid.RecordSelectorStyle.BackColor      = Color.Gainsboro;
			grid.RecordSelectorStyle.BackColor2     = Color.Gainsboro;
			grid.RecordSelectorStyle.GradientMode   = GradientModeEnum.Vertical;
			grid.RecordSelectorStyle.Borders.BorderType = BorderTypeEnum.Flat;
			grid.RecordSelectorStyle.Borders.Left   = 0;
			grid.RecordSelectorStyle.Borders.Top    = 0;
			grid.RecordSelectorStyle.Borders.Right  = 1;
			grid.RecordSelectorStyle.Borders.Bottom = 1;

			// 偶数・奇数でスタイルを変更しない。
			grid.AlternatingRows = false;

			//// 偶数行のスタイル
			//grid.OddRowStyle.ForeColor      = Color.Black;
			//grid.OddRowStyle.BackColor      = Color.FromArgb(255,250,245,240);
			//grid.OddRowStyle.BackColor2     = Color.FromArgb(255,250,245,240);
			//grid.OddRowStyle.GradientMode   = GradientModeEnum.None;
			//// 奇数行のスタイル
			//grid.EvenRowStyle.ForeColor     = Color.Black;
			//grid.EvenRowStyle.BackColor     = Color.White;
			//grid.EvenRowStyle.BackColor2    = Color.White;
			//grid.EvenRowStyle.GradientMode  = GradientModeEnum.None;
			// 選択された列・行のスタイル
			grid.SelectedStyle.BackColor    = Color.Gray;
			grid.SelectedStyle.GradientMode = GradientModeEnum.None;
			grid.SelectedStyle.ForeColor    = Color.White;
			// ハイライト行のスタイル
//			grid.HighLightRowStyle.BackColor    = Color.WhiteSmoke;
			//grid.HighLightRowStyle.BackColor    = Color.FromArgb(255,255,226);
			//grid.HighLightRowStyle.BackColor2   = Color.FromArgb(255,255,226);

			grid.HighLightRowStyle.BackColor    = 
			grid.HighLightRowStyle.BackColor2   = Color.LemonChiffon;

//			grid.HighLightRowStyle.GradientMode = GradientModeEnum.Vertical;
			//grid.HighLightRowStyle.ForeColor    = Color.DarkSlateGray;
			grid.HighLightRowStyle.ForeColor    = Color.Black;
			
			grid.FooterStyle.ForeColor		= Color.Black;

			// 左行セレクタ
			grid.RecordSelectors = false;

			// スクロールバーを常に出す
			grid.VScrollBar.Style = ScrollBarStyleEnum.Always;
		}
		
		/// <summary>
		/// 行設定をします。指定が大きすぎる場合は丸めます。
		/// 選択された行がグリッドの中心にくるよう操作します。
		/// </summary>
		public static void SetRow(C1TrueDBGrid grid, int row)
		{
			if (grid.RowCount > 0)
			{
				if (grid.RowCount <= row)
				{
					row = grid.RowCount - 1;
				}
				
				// 選択された行が中心にくるようにする
				int		r = row - grid.VisibleRows / 2;
				if (r < 0)
				{
					r = 0;
				}
				grid.Row		= row;
				grid.FirstRow	= r;
			}
		}
		
		/// <summary>
		/// 複数選択された行から、行に該当しないものを消去し、行数でソートしたものを返します。
		/// </summary>
		public static ArrayList GetSortSelectedRows(C1TrueDBGrid grid, DBView dv)
		{
			return GetSortSelectedRows(grid.SelectedRows, dv);
		}
		
		/// <summary>
		/// 複数選択された行から、行に該当しないものを消去し、行数でソートしたものを返します。
		/// </summary>
		public static ArrayList GetSortSelectedRows(SelectedRowCollection srows, DBView dv)
		{
			// 追加する行の取得
			ArrayList temp = new ArrayList(srows);
			temp.Sort();
			
			// 行に関係のない数字を消す
			for (int i = temp.Count - 1; i > -1; i--)
			{
				if (Cast.Int(temp[i]) < 0 || Cast.Int(temp[i]) >= dv.Count)
				{
					temp.RemoveAt(i);
				}
			}
			
			return temp;
		}
		
		/// <summary>
		/// Enabled の設定を行います。見た目にも変化を加えます。
		/// </summary>
		/// <param name="grid">フラグを変更するグリッド</param>
		/// <param name="flg">Enabledフラグ</param>
		public static void SetEnabled(C1TrueDBGrid grid, bool flg)
		{
			Split	split = grid.Splits[0];
			
			grid.Enabled = flg;
			
			if (flg == false)
			{
				// 偶数行のスタイル
				grid.OddRowStyle.BackColor			= Color.Gainsboro;
				grid.OddRowStyle.GradientMode		= GradientModeEnum.None;
				// 奇数行のスタイル
				grid.EvenRowStyle.BackColor			= Color.Gainsboro;
				grid.EvenRowStyle.GradientMode		= GradientModeEnum.None;
				// ハイライト行のスタイル
				grid.HighLightRowStyle.BackColor	= Color.Gainsboro;
				grid.HighLightRowStyle.GradientMode = GradientModeEnum.None;
			}
			else
			{
				// 偶数行のスタイル
				grid.OddRowStyle.BackColor			= Color.White;
				grid.OddRowStyle.BackColor2			= Color.OldLace;
				grid.OddRowStyle.GradientMode		= GradientModeEnum.Vertical;
				// 奇数行のスタイル
				grid.EvenRowStyle.BackColor			= Color.White;
				grid.EvenRowStyle.BackColor2		= Color.White;
				grid.EvenRowStyle.GradientMode		= GradientModeEnum.None;
				// ハイライト行のスタイル
				grid.HighLightRowStyle.BackColor	= Color.WhiteSmoke;
				grid.HighLightRowStyle.BackColor2   = Color.Gainsboro;
				grid.HighLightRowStyle.GradientMode = GradientModeEnum.Vertical;
			}
			grid.Refresh();
		}
		
		/// <summary>
		/// グリッドの補足情報用 Tips を表示するための座標を取得します。
		/// </summary>
		/// <param name="tip">AppToolTip インスタンス。</param>
		/// <param name="grid">表示するグリッド。</param>
		/// <param name="adjx">該当セルからの補正位置情報 X。</param>
		/// <param name="adjy">該当セルからの補正位置情報 Y。</param>
		/// <param name="text">表示するテキスト。</param>
		public static void GridShow(AppToolTip tip, C1TrueDBGrid grid, int adjx, int adjy, string text)
		{
			int		x, y;
			
			GetGridTipsLocation(grid, out x, out y);
			tip.Show(grid, x + adjx, y + adjy, AppToolTipIndex.AnythingTips, text);
		}
		
		/// <summary>
		/// グリッドの補足情報用Tipsを表示するための座標を取得します。
		/// </summary>
		/// <param name="grid">表示するグリッド</param>
		/// <param name="x">該当セルからの補正位置情報X</param>
		/// <param name="y">該当セルからの補正位置情報Y</param>
		public static void GetGridTipsLocation(C1TrueDBGrid grid, int x, int y)
		{
			GetGridTipsLocation(grid, out x, out y);
		}
		
		/// <summary>
		/// グリッドの補足情報用Tipsを表示するための座標を取得します。
		/// </summary>
		/// <param name="grid">表示するグリッド</param>
		/// <param name="x">該当セルからの補正位置情報X</param>
		/// <param name="y">該当セルからの補正位置情報Y</param>
		public static void GetGridTipsLocation(C1TrueDBGrid grid, out int x, out int y)
		{
			GetGridTipsLocation(grid, grid.Col, out x, out y);
		}
		
		/// <summary>
		/// グリッドの補足情報用Tipsを表示するための座標を取得します。
		/// </summary>
		/// <param name="grid">表示するグリッド</param>
		/// <param name="col">該当セルからの補正カラム情報</param>
		/// <param name="x">該当セルからの補正位置情報X</param>
		/// <param name="y">該当セルからの補正位置情報Y</param>
		public static void GetGridTipsLocation(C1TrueDBGrid grid, int col, out int x, out int y)
		{
			x = 0;
			y = 0;
			
			if (grid.DataSource == null)
			{
				return;
			}
			
			int			sIndex = grid.SplitIndex;
			int			cIndex = col;
			int			rIndex = grid.Row;
			
			if (rIndex > grid.RowCount-1)
			{
				rIndex = grid.RowCount-1;
			}
			else if (rIndex < grid.FirstRow)
			{
				rIndex = grid.FirstRow;
			}
			
			if (cIndex > grid.Columns.Count-1)
			{
				cIndex = grid.Columns.Count-1;
			}
			else if (cIndex < 0)
			{
				cIndex = 0;
			}
			
			// セルのクライアント座標を得る
			Rectangle	rect;
			
			try
			{
				rect = grid.Splits[sIndex].GetCellBounds(rIndex, cIndex);
				x = rect.Location.X;
				y = rect.Location.Y;
			}
			catch
			{
				Debug.WriteLine("■ 行が表示できません。だそうです。謎エラー");
				return;
			}
		}

		/// <summary>
		/// 少数点以下第2位まで表示します。
		/// </summary>
		public static string UnboundDecimalPoint2(GGridDBBase col, C1.Win.C1TrueDBGrid.UnboundColumnFetchEventArgs e)
		{
			decimal dec = Cast.Decimal(col.UnboundValue);

			if (dec != 0)
			{
				return dec.ToString("#,#0.00");
			}

			return "0.00";
		}

		/// <summary>
		/// 列タイトルの共通スタイルを設定します。
		/// </summary>
		/// <param name="grid">グリッド</param>
		public static void SetHeadingStyle(C1TrueDBGrid grid)
		{
			SetHeadingStyle(grid, 25);
		}

		/// <summary>
		/// 列タイトルの共通スタイルを設定します。
		/// </summary>
		/// <param name="grid">グリッド</param>
		/// <param name="height">ヘッダの高さ</param>
		public static void SetHeadingStyle(C1TrueDBGrid grid, int height)
		{
			// 列タイトルの縦幅
			Split						split;
			
			for (int i = 0; i < grid.Splits.Count; i++)
			{
				split = grid.Splits[i];
				split.HeadingStyle.Font = new Font("メイリオ", 9.5f);
				split.HeadingStyle.VerticalAlignment = AlignVertEnum.Center;
				split.ColumnCaptionHeight = height;
			}
		}
		
		/// <summary>
		/// エラー表示。DEBUG環境でのみ有効です
		/// </summary>
		[Conditional("DEBUG")]
		static private void _write_err(Exception ex)
		{
			Debug.WriteLine("■■■ Exception");
			Debug.WriteLine("Source     = {0}", ex.Source);
			Debug.WriteLine("Type       = {0}", ex.GetType().ToString());
			Debug.WriteLine("Message    = {0}", ex.Message);
			Debug.WriteLine("StackTrace = {0}", ex.StackTrace);
			Debug.WriteLine("");
		}
	}
}
