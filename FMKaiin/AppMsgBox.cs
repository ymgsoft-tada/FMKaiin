
//
// ※このプログラムはSrcMakerForApplicationMessageにより自動的に生成されました。(fj)
//
// Inport File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKyuyo\_doc\AppMsgBox.xlsx
// Template File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKyuyo\_doc\AppMsgBox.cs.template
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
		/// {0}を削除しますか？
		/// </summary>
		Delete,
		/// <summary>
		/// 入力内容をキャンセルします。編集した内容は破棄されますがよろしいですか？
		/// </summary>
		CancelClose,
		/// <summary>
		/// @c(Red)削除すると元には戻せません。本当によろしいですか？
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
		/// 年度更新が完了しました。
		/// </summary>
		DoneYearUpdate,
		/// <summary>
		/// 予期せぬエラーが発生したため、データベースをバージョンアップする事が出来ません。
		/// </summary>
		FailedDataVersionUp,
		/// <summary>
		/// DBVersion：{0} は、システムでは管理できないデータです。
		/// </summary>
		CannotOpenUpperVersion,
		/// <summary>
		/// 対象となるデータがありません。
		/// </summary>
		NotFoundDataExport,
		/// <summary>
		/// コード最大値が登録されているため、これ以上新しい{0}を追加することができません。\r\n{0}マスターのコード番号を確認してください。
		/// </summary>
		MaxCodeForMaster,
		/// <summary>
		/// 「{0}度」への更新処理を実行します。よろしいですか？
		/// </summary>
		CreateNextMonth,
		/// <summary>
		/// {0}月度への更新処理が完了しました。
		/// </summary>
		SuccessNextMonth,
		/// <summary>
		/// 処理月度「{0}」が選択されました。
		/// </summary>
		SelectedMonth,
		/// <summary>
		/// {0}分のカレンダー情報を登録します。よろしいですか？
		/// </summary>
		ConfirmSaveCalendar,
		/// <summary>
		/// {0}分のカレンダー情報が登録されていません。\r\nカレンダー管理画面で設定情報を登録してください。
		/// </summary>
		NotFoundCalendarNeedCreate,
		/// <summary>
		/// パスワードを変更します。よろしいですか？
		/// </summary>
		ConfirmChangePassword,
		/// <summary>
		/// {0}度の給与計算データは既に作成済みです。再作成してもよろしいですか？\r\n（該当月度の作成済みデータについては全て破棄されます。）
		/// </summary>
		ConfirmRemakeKyuyoData,
		/// <summary>
		/// {0}度の給与計算データを生成しました。
		/// </summary>
		CreateKyuyoData,
		/// <summary>
		/// 確定状態を解除するため、作成済みの給与計算データが全てクリアされます。よろしいですか？
		/// </summary>
		ConfirmClearKyuyoData,
		/// <summary>
		/// {0}度の給与計算データをクリアしました。
		/// </summary>
		ClearKyuyoData,
		/// <summary>
		/// @c(Red)解除すると元には戻せません。本当によろしいですか？
		/// </summary>
		ConfirmClearKyuyoData2,
		/// <summary>
		/// {0}度の給与計算データを作成します。よろしいですか？
		/// </summary>
		ConfirmCreateKyuyoData,
		/// <summary>
		/// ログファイルの出力が完了しました。
		/// </summary>
		SuccessExportLog,
		/// <summary>
		/// ログファイルの出力に失敗しました。ファイルがなんらかのアプリケーションによって開かれている可能性があります。
		/// </summary>
		FailedExportLogReasonSave,
		/// <summary>
		/// アプリケーションを終了します。よろしいですか？
		/// </summary>
		ShutDown,
		/// <summary>
		/// サポートセンターのオペレータによる遠隔支援サービスを開始します。よろしいですか？
		/// </summary>
		StartRemoteSupport,
		/// <summary>
		/// 給与データが確定済のため、情報を編集する事ができません。
		/// </summary>
		FixedKyuyo,
		/// <summary>
		/// {0}は現在ログイン中のため、削除することができません。
		/// </summary>
		CannotDeleteLoginUser,
		/// <summary>
		/// {0}の休日診療データについて、現在の日区分と計算区分を元に再計算します。\r\n既に登録されている金額については全て破棄されます。よろしいですか？
		/// </summary>
		ConfirmRecalcKyushin,
		/// <summary>
		/// @c(Red)再計算すると元には戻せません。本当によろしいですか？
		/// </summary>
		ConfirmRecalcKyushin2,
		/// <summary>
		/// {0}件のデータについて、再計算を実行しました。
		/// </summary>
		SuccessRecalcKyushin,
		/// <summary>
		/// 過去分（最新月度でない）で確定済みの給与計算データを解除する事はできません。
		/// </summary>
		CannotCancelFixedKyuyo,
		/// <summary>
		/// CSVファイルから{0}件のデータインポートが完了しました。
		/// </summary>
		SuccessImportCSV,
		/// <summary>
		/// CSVインポートをキャンセルします。よろしいですか？
		/// </summary>
		CancelImportCSV,
		/// <summary>
		/// CSVファイルからデータのインポートを実行します。よろしいですか？
		/// </summary>
		ExecImportCSV,
		/// <summary>
		/// 入力された個人番号は有効ではありません。\r\nこのまま登録してもよろしいですか？
		/// </summary>
		ConfirmIlligalMyNumber,
		/// <summary>
		/// ログイン中の担当者にマイナンバーを管理する権限がありません。
		/// </summary>
		DisabledMyNumber,
		/// <summary>
		/// マイナンバー管理者のパスワードが登録されていません。\r\nつづけて管理用パスワードを登録してください。
		/// </summary>
		MyNumberManagerPWD,
		/// <summary>
		/// 指定した機能を実行するには、管理者権限が必要です。
		/// </summary>
		HavenotAuthAdmin,
		/// <summary>
		/// 同一月度・診療所にインポート済のデータがあります。インポート処理を実行しますか？
		/// </summary>
		ConfirmImportDuplicate,
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
				"{0}を削除しますか？"),
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
				"@c(Red)削除すると元には戻せません。本当によろしいですか？"),
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
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"年度更新が完了しました。"),
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
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"対象となるデータがありません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"コード最大値が登録されているため、これ以上新しい{0}を追加することができません。\r\n{0}マスターのコード番号を確認してください。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"「{0}度」への更新処理を実行します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"成功",
				"{0}月度への更新処理が完了しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"処理月度「{0}」が選択されました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"{0}分のカレンダー情報を登録します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"{0}分のカレンダー情報が登録されていません。\r\nカレンダー管理画面で設定情報を登録してください。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"パスワードを変更します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"{0}度の給与計算データは既に作成済みです。再作成してもよろしいですか？\r\n（該当月度の作成済みデータについては全て破棄されます。）"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"成功",
				"{0}度の給与計算データを生成しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"確定状態を解除するため、作成済みの給与計算データが全てクリアされます。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"成功",
				"{0}度の給与計算データをクリアしました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"@c(Red)解除すると元には戻せません。本当によろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"{0}度の給与計算データを作成します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"ログファイルの出力が完了しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				"エラー",
				"ログファイルの出力に失敗しました。ファイルがなんらかのアプリケーションによって開かれている可能性があります。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1,
				"確認",
				"アプリケーションを終了します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"サポートセンターのオペレータによる遠隔支援サービスを開始します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"給与データが確定済のため、情報を編集する事ができません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"{0}は現在ログイン中のため、削除することができません。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"{0}の休日診療データについて、現在の日区分と計算区分を元に再計算します。\r\n既に登録されている金額については全て破棄されます。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"@c(Red)再計算すると元には戻せません。本当によろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"{0}件のデータについて、再計算を実行しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"過去分（最新月度でない）で確定済みの給与計算データを解除する事はできません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"CSVファイルから{0}件のデータインポートが完了しました。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"CSVインポートをキャンセルします。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2,
				"確認",
				"CSVファイルからデータのインポートを実行します。よろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button2,
				"警告",
				"入力された個人番号は有効ではありません。\r\nこのまま登録してもよろしいですか？"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"ログイン中の担当者にマイナンバーを管理する権限がありません。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				"確認",
				"マイナンバー管理者のパスワードが登録されていません。\r\nつづけて管理用パスワードを登録してください。"),
			new AppMsgBoxData(
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				"警告",
				"指定した機能を実行するには、管理者権限が必要です。"),
			new AppMsgBoxData(
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button2,
				"警告",
				"同一月度・診療所にインポート済のデータがあります。インポート処理を実行しますか？"),
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

