using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace App
{
	/// <summary>
	/// 定数群
	/// </summary>
	public class AppConst
	{
		/// <summary>
		/// 
		/// </summary>
		public const string AppName = "FMKaiin";
		/// <summary>
		/// メジャーバージョン
		/// </summary>
		public const string AppMajor = "0";
		/// <summary>
		/// マイナーバージョン
		/// </summary>
		public const string AppMinor = ".1";
		/// <summary>
		/// リビジョン
		/// </summary>
		public const string AppRevision = ".0";
		/// <summary>
		/// アプリケーションバージョン
		/// </summary>
		public const string AppVersion = AppMajor + AppMinor + AppRevision;
		/// <summary></summary>
		public const string AppDBPassword = "ymg884";
		/// <summary></summary>
		public const string AppDBVersion = "0.1.0";
		/// <summary></summary>
		public const string AppTitle = "藤沢市医師会 会員管理システム";

		/// <summary></summary>
		public const string DBSrcFile = "Data.mdb";

		/// <summary></summary>
		public const string DBFile = "Data.db";

		/// <summary>DBファイル</summary>
		public const string DBPath = DBFile;

		/// <summary>作業用DBファイルパス</summary>
		public const string DBWorkPath = WorkDBFolder + @"\" + DBFile;

		/// <summary>作業用DBファイルパス</summary>
		public const string DBWorkPath2 = WorkDBFolder + @"\" + DBFile + "2";

		/// <summary>システムフォルダ</summary>
		public const string SystemFolder = @"system\";

		/// <summary>レポートフォルダ</summary>
		public const string ReportFolder = @"report\";

		/// <summary>郵便番号ファイル</summary>
		public const string ZipDBPath = SystemFolder + "Post.db";

		/// <summary>遠隔サポートフィル</summary>
		public const string RemoteSupportFile = "TeamViewerQS_ja.exe";

		/// <summary>遠隔実行ファイルパス</summary>
		public const string RemoteSupportFilePath = SystemFolder + RemoteSupportFile;

		/// <summary>レジストリ</summary>
		public const string RegKey = @"YMG soft\FMKaiin";

		/// <summary>システムDBファイルのパス</summary>
		public const string SystemDBPath = SystemFolder + DBSrcFile;

		/// <summary>作業用エリア</summary>
		public const string WorkDBFolder = "work";

		/// <summary>バックアップフォルダ</summary>
		public const string BackupFolder = "backup";
		/// <summary>最大バックアップ数</summary>
		public const int MaxBackupFile = 30;
		/// <summary>バックアップファイルの拡張子</summary>
		public const string BackUpFileExt = "dbz";
		/// <summary>バックアップ用テンポラリフォルダ</summary>
		public const string TempBackupFolder = "temp_bk";
		/// <summary>リストア用テンポラリフォルダ</summary>
		public const string TempRestoreFolder = "temp_re";
		/// <summary>DB拡張子</summary>
		public const string DBExt = "db";

		/// <summary>フォームの背景色</summary>
		public static readonly Color FormBackColor = Color.WhiteSmoke;

		/// <summary>読み取り専用</summary>
		public static readonly Color ReadonlyFieldColor = Color.FromArgb(235, 240, 255);
		/// <summary>通常グリッドヘッダー</summary>
		public static readonly Color GridHeaderColor = Color.FromArgb(255, 69, 150, 216);

		/// <summary>YMGSOFTのHP</summary>
		public static string YmgSoftHomePage = "http://www.ymg-soft.com/";
		
	}

}
