
//
// ※このプログラムはSrcMakerForApplicationMessageにより自動的に生成されました。(fj)
//
// Inport File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshi\FMKyuyo\_doc\AppToolTip.xlsx
// Template File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshi\FMKyuyo\_doc\AppToolTip.cs.template
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace App
{
	/// <summary>
	/// AppToolTipIndex に準じたデータを示すクラスです。
	/// </summary>
	public class AppToolTipData
	{
		/// <summary>
		/// 表示タイプ。
		/// </summary>
		public ToolTipIcon			Icon;
		/// <summary>
		/// 表示タイトル。
		/// </summary>
		public string				Caption;
		/// <summary>
		/// 本文。
		/// </summary>
		public string				Text;
		/// <summary>
		/// ワークテキスト。GetData() を実行した際、引数変換後の値が入っています。
		/// </summary>
		public string				WorkText;
		
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public AppToolTipData()
		{
			Icon	= ToolTipIcon.None;
			Caption	= "";
			Text	= "";
		}
		
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public AppToolTipData(ToolTipIcon icon, string caption, string text)
		{
			Icon	= icon;
			Caption	= caption;
			Text	= text;
		}
	}
	
	/// <summary>
	/// アプリケーションで使用するツールチップインデックス名です。
	/// </summary>
	public enum AppToolTipIndex
	{
		/// <summary>
		/// 
		/// </summary>
		None,
		/// <summary>
		/// {0}
		/// </summary>
		AnythingTips,
		/// <summary>
		/// 空欄にする事はできません。
		/// </summary>
		CannotUseBlank,
		/// <summary>
		/// パスワードが異なります。
		/// </summary>
		UnMatchPassword,
		/// <summary>
		/// 最新の月度データが生成されていません。月度更新を実行する必要があります。
		/// </summary>
		CreateLatestYearMonth,
		/// <summary>
		/// 最新月度以降の日付は指定できません。
		/// </summary>
		CreateLatestYearMonth2,
		/// <summary>
		/// 現在のパスワードが異なります。
		/// </summary>
		UnMatchOldPassword,
		/// <summary>
		/// 入力されたコードは既に使用されています。他のコードを指定してください。
		/// </summary>
		BookingCode,
		/// <summary>
		/// 新しいパスワードと確認用パスワードが異なっています。
		/// </summary>
		UnMatchPassword2,
		/// <summary>
		/// 該当する担当者が見つかりません。
		/// </summary>
		NotFoundTantosha,
		/// <summary>
		/// 日付の指定が不正です。
		/// </summary>
		illegalDate,
		/// <summary>
		/// 時間には 0～23 までの数値を入力してください。
		/// </summary>
		illegalTimeH,
		/// <summary>
		/// 分には 0～59 までの数値を入力してください。
		/// </summary>
		illegalTimeM,
		/// <summary>
		/// 数値以外の文字が入力されています。
		/// </summary>
		OnlyNumber,
		/// <summary>
		/// 個人番号は{0}桁で入力してください。
		/// </summary>
		NotEnouthDigit,
		/// <summary>
		/// 月度内の日付を指定してください。
		/// </summary>
		OutOfDateRange,
		/// <summary>
		/// 月別手当として使用されているため、チェックをオフにする事はできません。
		/// </summary>
		CannotMonthCheckOff,
		/// <summary>
		/// 入力された個人番号は有効ではありません。
		/// </summary>
		IlligalMyNumber,
	}
	
	/// <summary>
	/// ツールチップ情報。
	/// </summary>
	public partial class AppToolTip
	{
		static AppToolTipData[]	toolTipData =
		{
			new AppToolTipData(
				ToolTipIcon.None,
				"",
				""),
			new AppToolTipData(
				ToolTipIcon.None,
				"",
				"{0}"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"入力必須項目",
				"空欄にする事はできません。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"認証エラー",
				"パスワードが異なります。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"指定された処理年月データが存在しません。",
				"最新の月度データが生成されていません。月度更新を実行する必要があります。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"指定された処理年月データが存在しません。",
				"最新月度以降の日付は指定できません。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"認証エラー",
				"現在のパスワードが異なります。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"重複しています。",
				"入力されたコードは既に使用されています。他のコードを指定してください。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"認証エラー",
				"新しいパスワードと確認用パスワードが異なっています。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"担当者コードが存在しません",
				"該当する担当者が見つかりません。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"日付エラー",
				"日付の指定が不正です。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"入力値不正",
				"時間には 0～23 までの数値を入力してください。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"入力値不正",
				"分には 0～59 までの数値を入力してください。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"入力値不正",
				"数値以外の文字が入力されています。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"桁数が不足しています。",
				"個人番号は{0}桁で入力してください。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"範囲外の日付です。",
				"月度内の日付を指定してください。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"既に勤務データ入力で使用されています。",
				"月別手当として使用されているため、チェックをオフにする事はできません。"),
			new AppToolTipData(
				ToolTipIcon.Warning,
				"入力値不正",
				"入力された個人番号は有効ではありません。"),
		};
	}
	
	/// <summary>
	/// アプリケーションで使用するツールチップです。
	/// </summary>
	public partial class AppToolTip
	{
		#region *** Private Value ***
		/// <summary>
		/// ツールチップ
		/// </summary>
		ComponentForm.ToolTip	toolTip;
		#endregion
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="form">ツールチップを表示するフォーム</param>
		public AppToolTip(Form form)
		{
			toolTip = new ComponentForm.ToolTip(form);
		}
		#endregion
		
		#region *** Property ***
		/// <summary>
		/// AppToolTip で使用している ComponentForm.ToolTip クラスを取得します。
		/// 特に理由がない場合、このプロパティは使用しないでください。
		/// </summary>
		public ComponentForm.ToolTip	ToolTip
		{
			get
			{
				return toolTip;
			}
		}
		/// <summary>
		/// ツールチップが現在アクティブかどうかを示す値を取得します。
		/// </summary>
		public bool Active
		{
			get
			{
				return toolTip.Active;
			}
		}
		#endregion
		
		#region *** Public Method ***
		/// <summary>
		/// ツールチップの表示
		/// </summary>
		/// <param name="icon">メッセージタイプ</param>
		/// <param name="caption">メッセージタイトル</param>
		/// <param name="text">メッセージ本文</param>
		public void Show(ToolTipIcon icon, string caption, string text)
		{
			int			x0 = Cursor.Position.X + 32;
			int			y0 = Cursor.Position.Y + 16;
			
			// 現在表示があれば消す
			toolTip.Hide();

			// 表示タイプに応じたツールチップ呼び出し
			switch (icon)
			{
				case ToolTipIcon.None :
					toolTip.Tips(x0, y0, text);
					break;
				case ToolTipIcon.Info :
					toolTip.Info(x0, y0, caption, text);
					break;
				case ToolTipIcon.Warning :
					toolTip.Warning(x0, y0, caption, text);
					break;
				case ToolTipIcon.Error :
					toolTip.Error(x0, y0, caption, text);
					break;
			}
		}
		
		/// <summary>
		/// ツールチップの表示
		/// </summary>
		/// <param name="index">メッセージインデックス</param>
		/// <param name="arglist">メッセージテキストが「例えば{0}番目」といったものの場合、{0}を具体的内容に変換するための引数リスト</param>
		public void Show(AppToolTipIndex index, params object[] arglist)
		{
			string			caption;
			string			text;
			ToolTipIcon		icon;
			
			// メッセージを取得
			GetMessage(index, out caption, out text, out icon, arglist);
			
			Show(icon, caption, text);
		}
		
		/// <summary>
		/// ツールチップの表示
		/// </summary>
		/// <param name="ctl">指し示すコントロール</param>
		/// <param name="x0">コントロールからの相対座標X</param>
		/// <param name="y0">コントロールからの相対座標Y</param>
		/// <param name="icon">メッセージタイプ</param>
		/// <param name="caption">メッセージタイトル</param>
		/// <param name="text">メッセージ本文</param>
		public void Show(Control ctl, int x0, int y0, ToolTipIcon icon, string caption, string text)
		{
			// 現在表示があれば消す
			toolTip.Hide();

			// 表示タイプに応じたツールチップ呼び出し
			switch (icon)
			{
				case ToolTipIcon.None :
					toolTip.Tips(ctl, x0, y0, text);
					break;
				case ToolTipIcon.Info :
					toolTip.Info(ctl, x0, y0, caption, text);
					break;
				case ToolTipIcon.Warning :
					toolTip.Warning(ctl, x0, y0, caption, text);
					break;
				case ToolTipIcon.Error :
					toolTip.Error(ctl, x0, y0, caption, text);
					break;
			}
		}
		
		/// <summary>
		/// ツールチップの表示
		/// </summary>
		/// <param name="ctl">指し示すコントロール</param>
		/// <param name="x0">コントロールからの相対座標X</param>
		/// <param name="y0">コントロールからの相対座標Y</param>
		/// <param name="index">メッセージインデックス</param>
		/// <param name="arglist">メッセージテキストが「例えば{0}番目」といったものの場合、{0}を具体的内容に変換するための引数リスト</param>
		public void Show(Control ctl, int x0, int y0, AppToolTipIndex index, params object[] arglist)
		{
			string			caption;
			string			text;
			ToolTipIcon		icon;
			
			// メッセージを取得
			GetMessage(index, out caption, out text, out icon, arglist);
			
			Show(ctl, x0, y0, icon, caption, text);
		}
		
		/// <summary>
		/// ツールチップの表示
		/// </summary>
		/// <param name="ctl">指し示すコントロール</param>
		/// <param name="index">メッセージインデックス</param>
		/// <param name="arglist">メッセージテキストが「例えば{0}番目」といったものの場合、{0}を具体的内容に変換するための引数リスト</param>
		public void Show(Control ctl, AppToolTipIndex index, params object[] arglist)
		{
			string			caption;
			string			text;
			ToolTipIcon		icon;
			
			// メッセージを取得
			GetMessage(index, out caption, out text, out icon, arglist);
			
			Show(ctl, icon, caption, text);
		}

		/// <summary>
		/// ツールチップの表示
		/// </summary>
		/// <param name="ctl">指し示すコントロール</param>
		/// <param name="icon">メッセージタイプ</param>
		/// <param name="caption">メッセージタイトル</param>
		/// <param name="text">メッセージ本文</param>
		public void Show(Control ctl, ToolTipIcon icon, string caption, string text)
		{	
			// 現在表示があれば消す
			toolTip.Hide();

			// 表示タイプに応じたツールチップ呼び出し
			switch (icon)
			{
				case ToolTipIcon.None :
					toolTip.Tips(ctl, text);
					break;
				case ToolTipIcon.Info :
					toolTip.Info(ctl, caption, text);
					break;
				case ToolTipIcon.Warning :
					toolTip.Warning(ctl, caption, text);
					break;
				case ToolTipIcon.Error :
					toolTip.Error(ctl, caption, text);
					break;
			}
		}
		
		/// <summary>
		/// ツールチップを隠します。
		/// </summary>
		public void Hide()
		{
			toolTip.Hide();
		}
		#endregion

		#region *** Private Method ***
		/// <summary>
		/// 表示テキストを取得します。
		/// </summary>
		/// <param name="index">メッセージインデックス。</param>
		/// <param name="arglist">表示テキストの{0}などを変換するための情報。</param>
		/// <returns>ツールチップ情報。</returns>
		public static AppToolTipData GetData(AppToolTipIndex index, params object[] arglist)
		{
			int		idx = (int)index;
			
			if (idx < 0 || idx >= toolTipData.Length)
			{
				return null;
			}
			
			AppToolTipData		data = toolTipData[idx];
			
			data.WorkText = data.Text;
			
			// 固有パラメータの変換
			for (int i = 0; i < arglist.Length; i++)
			{
				string	param = "{" + i.ToString() + "}";
				
				data.WorkText = data.WorkText.Replace(param, arglist[i].ToString());
			}
			
			return data;
		}
		/// <summary>
		/// 表示テキストを取得します。
		/// </summary>
		/// <param name="index">メッセージインデックス。</param>
		/// <param name="arglist">表示テキストの{0}などを変換するための情報。</param>
		/// <returns>表示テキスト。</returns>
		public static string GetText(AppToolTipIndex index, params object[] arglist)
		{
			AppToolTipData		data = GetData(index, arglist);
			
			if (data == null)
			{
				return "";
			}
			
			return data.WorkText;
		}
		/// <summary>
		/// メッセージを取得します。
		/// </summary>
		/// <param name="index">メッセージインデックス。</param>
		/// <param name="caption">タイトルキャプション。</param>
		/// <param name="text">表示テキスト。</param>
		/// <param name="icon">表示タイプ。</param>
		/// <param name="arglist">表示テキストの{0}などを変換するための情報。</param>
		public static void GetMessage(AppToolTipIndex index, out string caption, out string text, out ToolTipIcon icon, params object[] arglist)
		{
			AppToolTipData		data = GetData(index, arglist);
			
			if (data == null)
			{
				caption = "";
				text    = "";
				icon    = ToolTipIcon.None;
			}
			else
			{
				icon	= data.Icon;
				text	= data.WorkText;
				caption = data.Caption;
			}
		}
		#endregion
	}
}


