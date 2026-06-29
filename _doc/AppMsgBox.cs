
//
// ※このプログラムはSrcMakerForApplicationMessageにより自動的に生成されました。(fj)
//
// Inport File :
//		D:\client\DotNet4.6\Itoshokai\Chokuso\_doc\AppMsgBox.xlsx
// Template File :
//		D:\client\DotNet4.6\Itoshokai\Chokuso\_doc\AppMsgBox.cs.template
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using ComponentForm;

namespace App
{
	#region *** Data Class ***
	/// <summary>
	/// AppMsgBoxIndex に準じたデータを示すクラスです。
	/// </summary>
	public class AppMsgBoxData
	{
		/// <summary>
		/// メッセージボックスに表示するボタンです。
		/// </summary>
		public MessageBoxButtons	Button;
		/// <summary>
		/// メッセージボックスに表示するアイコンです。
		/// </summary>
		public MessageBoxIcon		Icon;
		/// <summary>
		/// 最初に選択されているボタン番号です。
		/// </summary>
		public MessageBoxDefaultButton	DefButton;
		/// <summary>
		/// タイトル。
		/// </summary>
		public string			Title;
		/// <summary>
		/// 本文。
		/// </summary>
		public string				Text;
		/// <summary>
		/// 本文の{0}などを変換した後の文字列。
		/// </summary>
		public string				WorkText;
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public AppMsgBoxData(MessageBoxButtons button, MessageBoxIcon icon, string title, string text)
		{
			Button	  = button;
			Icon	  = icon;
			DefButton = MessageBoxDefaultButton.Button1;
			Title	  = title;
			Text	  = text;
			WorkText  = text;
		}
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public AppMsgBoxData(MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defbutton, string title, string text)
		{
			Button	  = button;
			Icon	  = icon;
			DefButton = defbutton;
			Title	  = title;
			Text	  = text;
			WorkText  = text;
		}
		#endregion
	}
	#endregion
	
	/// <summary>
	/// [作成者 fj]
	/// アプリケーションで使用するメッセージボックスのインデックス名です。
	/// </summary>
	public enum AppMsgBoxIndex
	{
		/// <summary>
		/// 
		/// </summary>
		None,
		/// <summary>
		/// {0}
		/// </summary>
		AnythingMessage,
		/// <summary>
		/// ファイルが存在しません。
		/// </summary>
		NotExistFile,
		/// <summary>
		/// データベースに正しくアクセスできません。
		/// </summary>
		IllegalAccessDatabase,
		/// <summary>
		/// 選択中の行を削除しますか？
		/// </summary>
		DeleteSelectedRow,
		/// <summary>
		/// 「{0}」を削除しますか？
		/// </summary>
		Delete,
		/// <summary>
		/// 入力内容をキャンセルします。編集した内容は破棄されますがよろしいですか？
		/// </summary>
		CancelClose,
		/// <summary>
		/// @c(Red)削除すると元には戻さません。本当によろしいですか？
		/// </summary>
		Delete2,
		/// <summary>
		/// データベースへの接続が確立できません。
		/// </summary>
		CannotConnectDB,
		/// <summary>
		/// データベースに接続出来ないため、アプリケーションを終了します。
		/// </summary>
		DisconnedtDBShutdown,
		/// <summary>
		/// データベースの接続に成功しました。
		/// </summary>
		ConnectDoneDB,
		/// <summary>
		/// 予期せぬエラーが発生したため、アプリケーションを終了します。
		/// </summary>
		UnKnownError,
		/// <summary>
		/// 既に他のデータで使用されているため、削除する事ができません。
		/// </summary>
		UsedOtherTable,
		/// <summary>
		/// 選択するデータが存在しません。
		/// </summary>
		NotFoundSelectData,
		/// <summary>
		/// データベースの自動バックアップに失敗しました。
		/// </summary>
		ErrorDBBackup,
		/// <summary>
		/// {0}分の請求データを作成します。よろしいですか？
		/// </summary>
		ConfirmMakeSeikyuData,
		/// <summary>
		/// 既に{0}分の請求データが存在します。\r\n該当月のデータを削除してから請求データを再作成しますがよろしいですか？
		/// </summary>
		ConfirmReMakeSeikyuData,
		/// <summary>
		/// {0}分の請求データを作成しました。
		/// </summary>
		MakeSeikyuData,
		/// <summary>
		/// 「{0}」は、\r\n既に入金データが存在するため全額入金処理を実行できません。
		/// </summary>
		CannotNyukinAll,
		/// <summary>
		/// 「{0}」の入金データをクリアします。よろしいですか？
		/// </summary>
		ConfirmClearNyukin,
		/// <summary>
		/// ファイルの書き出しに失敗しました。
		/// </summary>
		FailedWriteNgFile,
		/// <summary>
		/// 出力するデータがありません。
		/// </summary>
		NoPrintData,
		/// <summary>
		/// CSVのエクスポートが完了しました。
		/// </summary>
		SuccessExportCSV,
		/// <summary>
		/// CSVのエクスポートに失敗しました。ファイルがなんらかのアプリケーションによって開かれている可能性があります。
		/// </summary>
		FailedExportCSVReasonSave,
		/// <summary>
		/// CSVのエクスポートに失敗しました。
		/// </summary>
		FailedExportCSVReasonConvert,
		/// <summary>
		/// データを指定パスに保存しました。
		/// </summary>
		BackupSuccess,
		/// <summary>
		/// 何らかの理由によりデータのバックアップに失敗しました。
		/// </summary>
		BackupFailed,
		/// <summary>
		/// データの復元に成功しました。アプリケーションを再起動します。
		/// </summary>
		RestoreSuccess,
		/// <summary>
		/// 何らかの理由によりデータのデータ復元に失敗しました。
		/// </summary>
		RestoreFailed,
		/// <summary>
		/// {0}までの未収金額を更新します。よろしいですか？
		/// </summary>
		ConfirmMishuUpdate,
		/// <summary>
		/// 未収更新が完了しました。
		/// </summary>
		MishuUpdateDone,
		/// <summary>
		/// 既に{請求項目データが存在します。\r\n該当月のデータを削除してから請求項目データを再作成しますがよろしいですか？
		/// </summary>
		ConfirmReMakeSeikyuKomoku,
		/// <summary>
		/// 入金額が請求額を超えています。登録してよろしいですか？
		/// </summary>
		OverSeikyuCost,
		/// <summary>
		/// 新年度「{0}～{1}」へデータを更新します。よろしいですか？
		/// </summary>
		ConfirmYearUpdate,
		/// <summary>
		/// 年度更新が完了しました。
		/// </summary>
		DoneYearUpdate,
		/// <summary>
		/// 既に該当月に入金データが存在します。\r\n再作成する場合は該当月の入金データを全て削除してください。
		/// </summary>
		ExistsNyukinData,
		/// <summary>
		/// 予期せぬエラーが発生したため、データベースをバージョンアップする事が出来ません。
		/// </summary>
		FailedDataVersionUp,
		/// <summary>
		/// DBVersion：{0} は、システムでは管理できないデータです。
		/// </summary>
		CannotOpenUpperVersion,
		/// <summary>
		/// {0}の{1}分請求データを削除します。よろしいですか？\r\n（※該当月分に入金データが存在する場合は同時に削除されます。）
		/// </summary>
		ConfirmDeleteSeikyusho,
		/// <summary>
		/// 請求データを削除しました。
		/// </summary>
		SeikyuDeleted,
		/// <summary>
		/// 楽一からの取込処理を中止します。よろしいですか？
		/// </summary>
		ConfirmCancelImport,
		/// <summary>
		/// 楽一から{0}件のインポート処理が完了しました。
		/// </summary>
		SuccessImportCSV,
		/// <summary>
		/// ｢{0}｣を新規に取引先マスターに登録しました。
		/// </summary>
		AddMasterTorihiki,
		/// <summary>
		/// ｢{0}｣を新規に商品マスターに登録しました。
		/// </summary>
		AddMasterShohin,
		/// <summary>
		/// {0}件中、チェックの付いた{1}件の伝票について\r\n送信データのCSVファイルを作成します。よろしいですか？
		/// </summary>
		ConfirmExport,
		/// <summary>
		/// 対象となるデータがありません。
		/// </summary>
		NotFoundDataExport,
		/// <summary>
		/// 供給受入商品が変更されました。商品マスタの情報も更新します。よろしいですか？
		/// </summary>
		AddShohinUpdate,
	};
	
	/// <summary>
	/// メッセージボックス情報。
	/// </summary>
	public partial class AppMsgBox
	{
		static AppMsgBoxData[]	msgBoxData =
		{
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"",
				""),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"",
				"{0}"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"ファイルが存在しません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"データベースに正しくアクセスできません。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"選択中の行を削除しますか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"「{0}」を削除しますか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"入力内容をキャンセルします。編集した内容は破棄されますがよろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"@c(Red)削除すると元には戻さません。本当によろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"データベースへの接続が確立できません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"データベースに接続出来ないため、アプリケーションを終了します。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"データベースの接続に成功しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"予期せぬエラーが発生したため、アプリケーションを終了します。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"既に他のデータで使用されているため、削除する事ができません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"選択するデータが存在しません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"データベースの自動バックアップに失敗しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"{0}分の請求データを作成します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"既に{0}分の請求データが存在します。\r\n該当月のデータを削除してから請求データを再作成しますがよろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"{0}分の請求データを作成しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"既に入金のあるデータです。",
				"「{0}」は、\r\n既に入金データが存在するため全額入金処理を実行できません。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"「{0}」の入金データをクリアします。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"ファイルの書き出しに失敗しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"出力するデータがありません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"CSVのエクスポートが完了しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"CSVのエクスポートに失敗しました。ファイルがなんらかのアプリケーションによって開かれている可能性があります。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"CSVのエクスポートに失敗しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"成功",
				"データを指定パスに保存しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"何らかの理由によりデータのバックアップに失敗しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"成功",
				"データの復元に成功しました。アプリケーションを再起動します。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"何らかの理由によりデータのデータ復元に失敗しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"{0}までの未収金額を更新します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"未収更新が完了しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"既に{請求項目データが存在します。\r\n該当月のデータを削除してから請求項目データを再作成しますがよろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button2,
				"確認",
				"入金額が請求額を超えています。登録してよろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"新年度「{0}～{1}」へデータを更新します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"年度更新が完了しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"処理を中断しました。",
				"既に該当月に入金データが存在します。\r\n再作成する場合は該当月の入金データを全て削除してください。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"予期せぬエラーが発生したため、データベースをバージョンアップする事が出来ません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"DBVersion：{0} は、システムでは管理できないデータです。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"{0}の{1}分請求データを削除します。よろしいですか？\r\n（※該当月分に入金データが存在する場合は同時に削除されます。）"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"請求データを削除しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"楽一からの取込処理を中止します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"楽一から{0}件のインポート処理が完了しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"｢{0}｣を新規に取引先マスターに登録しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"｢{0}｣を新規に商品マスターに登録しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"{0}件中、チェックの付いた{1}件の伝票について\r\n送信データのCSVファイルを作成します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"対象となるデータがありません。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"供給受入商品が変更されました。商品マスタの情報も更新します。よろしいですか？"),
		};
	}
	
	/// <summary>
	/// アプリケーションで使用するメッセージボックスです。
	/// </summary>
	public partial class AppMsgBox
	{
		/// <summary>
		/// 親設定がなかった場合に参照される背景色です。
		/// </summary>
		public static Color BackColor
		{
			set
			{
				backcolor = value;
			}
		}
		static Color	backcolor = AppConst.FormBackColor;
		/// <summary>
		/// 親設定がなかった場合に参照される親フォームです。
		/// </summary>
		public static Control Parent
		{
			set
			{
				parent = value;
			}
		}
		static Control parent = null;

		/// <summary>
		/// 閉じられた理由
		/// </summary>
		public static MsgBox.eCloseReason CloseReason
		{
			get
			{
				return closeReason;
			}
		}
		static MsgBox.eCloseReason closeReason;
		
		/// <summary>
		/// メッセージボックス表示。Owner指定付。
		/// </summary>
		/// <param name="owner">ダイアログのオーナー</param>
		/// <param name="index">AppMsgBoxIndex列挙子</param>
		/// <param name="arglist">{?} 文字列と置き換えるメッセージ</param>
		/// <returns>ダイアログボタンの押された結果が返ります。</returns>
		public static DialogResult Show(Control owner, AppMsgBoxIndex index, params object[] arglist)
		{
			string					title;
			string					text;
			MessageBoxButtons		buttons;
			MessageBoxIcon			icon;
			MessageBoxDefaultButton	defbutton;
			DialogResult			result;
			
			// 表示前に初期化しておく。
			closeReason = MsgBox.eCloseReason.Default;

			// 表示するメッセージを取得
			getMessage(index, out title, out text, out buttons, out icon, out defbutton, arglist);
			
			// nullなら設定された親を参照しにいく
			if (owner == null)
			{
				owner = parent;
			}
			
			MsgBox form = new MsgBox(owner, text, title, buttons, icon, defbutton);
			if (owner != null && owner.IsDisposed == false)
			{
				form.StartPosition = FormStartPosition.CenterParent;
				form.BackColor	   = owner.BackColor;
			}
			else
			{
				// 親がなければ中央に表示
				form.StartPosition = FormStartPosition.CenterScreen;
				form.BackColor	   = backcolor;
			}
			form.ShowDialog();
			result = form.Result;
			closeReason = form.CloseReason;
			form.Dispose();
			
			return result;
		}
		
		/// <summary>
		/// メッセージボックス表示。
		/// </summary>
		/// <param name="index">AppMsgBoxIndex列挙子</param>
		/// <param name="arglist">{?} 文字列と置き換えるメッセージ</param>
		/// <returns>ダイアログボタンの押された結果が返ります。</returns>
		public static DialogResult Show(AppMsgBoxIndex index, params object[] arglist)
		{
			return Show(null, index, arglist);
		}
		
		/// <summary>
		/// メッセージボックス表示。
		/// </summary>
		/// <param name="index">AppMsgBoxIndex列挙子</param>
		/// <returns>ダイアログボタンの押された結果が返ります。</returns>
		public static DialogResult Show(AppMsgBoxIndex index)
		{
			return Show(null, index);
		}
		
		/// <summary>
		/// 該当するAppMsgBoxIndex列挙子のメッセージデータを返します。
		/// </summary>
		/// <param name="index">AppMsgBoxIndex列挙子</param>
		/// <param name="title">タイトル</param>
		/// <param name="text">表示メッセージ（取得用）</param>
		/// <param name="buttons">ボタン（取得用）</param>
		/// <param name="icon">表示アイコン（取得用）</param>
		/// <param name="defbutton">最初に選択されているボタン（取得用）</param>
		/// <param name="arglist">{?} 文字列と置き換えるメッセージ</param>
		private static void getMessage(
			AppMsgBoxIndex index,
			out string title,
			out string text,
			out MessageBoxButtons buttons,
			out MessageBoxIcon icon,
			out MessageBoxDefaultButton	defbutton,
			params object[] arglist)
		{
			AppMsgBoxData	data = GetData(index, arglist);
			
			if (data == null)
			{
				title   = "";
				text    = "";
				buttons = MessageBoxButtons.OK;
				icon    = MessageBoxIcon.None;
				defbutton = MessageBoxDefaultButton.Button1;
			}
			else
			{
				title	  = data.Title;
				text	  = data.WorkText;
				buttons	  = data.Button;
				icon	  = data.Icon;
				defbutton = data.DefButton;
			}
		}
		
		/// <summary>
		/// 表示テキストを取得します。
		/// </summary>
		/// <param name="index">メッセージインデックス。</param>
		/// <param name="arglist">表示テキストの{0}などを変換するための情報。</param>
		/// <returns>メッセージボックス情報。</returns>
		public static AppMsgBoxData GetData(AppMsgBoxIndex index, params object[] arglist)
		{
			int		idx = (int)index;
			
			if (idx < 0 || idx >= msgBoxData.Length)
			{
				return null;
			}
			
			AppMsgBoxData		data = msgBoxData[idx];
			
			data.WorkText = data.Text;
			
			// 固有パラメータの変換
			for (int i = 0; i < arglist.Length; i++)
			{
				string	param = "{" + i.ToString() + "}";
				
				data.WorkText = data.WorkText.Replace(param, arglist[i].ToString());
			}
			
			return data;
		}
	}
}

