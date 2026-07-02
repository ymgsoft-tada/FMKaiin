using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

using ComponentDB;
using ComponentIO;
using ComponentFile;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// DB アップデートを行います。
	/// </summary>
	public class AppDbUpdateManager
	{
		#region *** Private Value ***
		/// <summary>
		/// 変換対象 t_basic 情報。
		/// </summary>
		static t_basic		modsys = null;
		/// <summary>
		/// Update 結果。
		/// </summary>
		static bool			result;
		#endregion
		
		#region *** Property Value ***
		/// <summary>
		/// Update 結果を取得します。
		/// </summary>
		public static bool Result
		{
			get
			{
				return result;
			}
		}
		#endregion
		
		#region *** Public Method ***
		/// <summary>
		/// バージョンを更新します。
		/// </summary>
		/// <returns>true..成功、もしくは必要なし, false..失敗</returns>
		public static void UpdateVersion(object sender, DoWorkEventArgsEx e)
		{
			FormBg_Progress		bgform	= (FormBg_Progress)sender;
			
			const string		backupfile = "backup.tmp";
			string				modfile = Cast.String(e.Args[0]);
			string				seefile = Cast.String(e.Args[1]);
			
			// バージョンアップ前のファイルを保存。
			FileIO.Copy(modfile, backupfile);
			
			DB		moddb = new DB(modfile, AppConst.AppDBPassword);
			DB		seedb = new DB(seefile, AppConst.AppDBPassword);
			DBView	moddv_sys = new DBView(new DBAdapter(moddb, TableProp.t_basic));
			DBView	seedv_sys = new DBView(new DBAdapter(seedb, TableProp.t_basic));
			
			AppDbUpdateHistory.eVersion eVer = checkUpperVersion(moddv_sys);

			// 変換の必要なし。（Versionがサーバーより高いか、チェックが上手くできない）
			if (eVer != AppDbUpdateHistory.eVersion.Lower)
			{
				// 進捗バーの更新。
				if (bgform != null) bgform.FinishProgress();
				return;
			}
			
			//■ 変換対象の HistoryList を検索。
			string	modver_str = "0.0.0";
			int		modhis;

			if (moddv_sys.Contains(t_basic.FBAS_DBVersion) == true)
			{
				modver_str = modsys.BAS_DBVersion;
			}
			
			for (modhis = 0; modhis < AppDbUpdate.History.Length; modhis++)
			{
				if (AppDbUpdate.History[modhis].CheckNeedVersionUp(modver_str) == true)
				{
					break;
				}
			}
			// バージョンアップによる変換の必要なし。（Versionだけ上げる）
			if (modhis == AppDbUpdate.History.Length)
			{
				modsys.BAS_DBVersion = AppConst.AppDBVersion;
				moddv_sys.Update();
				
				// 進捗バーの更新。
				if (bgform != null) bgform.FinishProgress();
				
				result = true;
				return;
			}
			
			// mod 0.99.00 see 1.01.00 で、リストが
			//		1.00.02
			//		1.00.05
			// の２つなら、1.00.05 までデータベースを更新しなければいけない。
			
			//■ 変換参照の HistoryList を検索。
			int[]	seever = AppDbUpdateHistory.VersionToArray(AppConst.AppDBVersion);
			int		seehis;
			
			for (seehis = 0; seehis < AppDbUpdate.History.Length; seehis++)
			{
				if (AppDbUpdate.History[seehis].Version == seever)
				{
					// mod 0.99.00 see 0.99.07 で、リストが
					//		0.99.07
					// の１つなら、modhis は 0、seehis は 1 になっている必要があるので、１を足す。
					seehis++;
					break;
				}
			}
			
			//■ DBバージョンアップ処理を行う。
			for (int i = modhis; i < seehis; i++)
			{
				// verupでdbが変更されている可能性があるので、毎回取得する
				moddb = new DB(modfile, AppConst.AppDBPassword);

				bool succeed = AppDbUpdate.History[i].UpdateMethod(moddb, seedb);
				
				if (succeed == false)
				{
					// バージョンアップ前のファイルに戻す。
					FileIO.Copy(backupfile, modfile);
					FileIO.Delete(backupfile);
					
					// 変換失敗。
					result = false;
					return;
				}

				// 進捗バーの更新。
				if (bgform != null) bgform.SetProgress(100 * (i-modhis+1) / (seehis - modhis));
			}

			moddb.DelAdapter(moddv_sys.DBAdapter);
			moddv_sys = null;

			// 成功。
			moddv_sys = new DBView(new DBAdapter(moddb, TableProp.t_basic));
			if (moddv_sys.Count > 0)
			{
				modsys = new t_basic(moddv_sys[0].Row);
				modsys.BAS_DBVersion = AppConst.AppDBVersion;
				moddv_sys.Update();
			}
			
			// 進捗バーの更新。
			if (bgform != null) bgform.FinishProgress();
			
			// バージョンアップ前のバックアップファイルを消す。
			FileIO.Delete(backupfile);
			
			result = true;
			return;
		}
		
		/// <summary>
		/// バージョンが高いかチェックします。
		/// </summary>
		/// <param name="modfile">変換対象DBファイル名。</param>
		public static AppDbUpdateHistory.eVersion CheckUpperVersion(string modfile)
		{
			DB		moddb = new DB(modfile, AppConst.AppDBPassword);
			DBView	moddv_sys = new DBView(new DBAdapter(moddb, TableProp.t_basic));
			
			AppDbUpdateHistory.eVersion ret = checkUpperVersion(moddv_sys);

			//■ チェックしたDBを解放する。
			moddb.Close();
			moddb.Dispose();
			moddb = null;

			return ret;
		}

		/// <summary>
		/// 指定ファイルからバージョン情報を取得します。
		/// </summary>
		/// <param name="modfile">対象ＤＢファイル</param>
		public static string GetVersion(string modfile)
		{
			DB		moddb = new DB(modfile, AppConst.AppDBPassword);
			DBView	moddv_sys = new DBView(new DBAdapter(moddb, TableProp.t_basic));

			string ver = "";
			if (moddv_sys.CurrentRow != null)
			{
				ver = Cast.String(moddv_sys[t_basic.FBAS_DBVersion]);
			}

			//■ チェックしたDBを解放する。
			moddb.Close();
			moddb.Dispose();
			moddb = null;

			return ver;
		}
		
		/// <summary>
		/// バージョンが高いかチェックします。
		/// </summary>
		/// <param name="moddv_sys">変換対象DB。</param>
		static AppDbUpdateHistory.eVersion checkUpperVersion(DBView moddv_sys)
		{
			// T_System の内容を保存。
			modsys	= null;
			if (moddv_sys.Count > 0)
			{
				modsys	= new t_basic(moddv_sys[0]);
			}
			
			// Version のチェックができない。
			if (modsys == null)
			{
				return AppDbUpdateHistory.eVersion.None;
			}

			// DBVerup用フィールドが無い
			if (modsys.Row.Table.Columns.Contains(t_basic.FBAS_DBVersion) == false)
			{
				return AppDbUpdateHistory.eVersion.Lower;
			}

			// Version は比較参照より高い？
			return AppDbUpdateHistory.CheckVersion(AppConst.AppDBVersion, modsys.BAS_DBVersion);
		}
		#endregion
		
		#region *** Private Method ***
		/// <summary>
		/// エラー表示。DEBUG環境でのみ有効です
		/// </summary>
		[Conditional("DEBUG")]
		static void _write_err(Exception ex)
		{
			Debug.WriteLine("■■■ Exception");
			Debug.WriteLine("Source     = {0}", ex.Source);
			Debug.WriteLine("Type       = {0}", ex.GetType().ToString());
			Debug.WriteLine("Message    = {0}", ex.Message);
			Debug.WriteLine("StackTrace = {0}", ex.StackTrace);
			Debug.WriteLine("");
		}
		#endregion
	}
}
