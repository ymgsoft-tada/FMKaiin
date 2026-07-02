using System;
using System.Collections.Generic;
using System.Text;

using ComponentDB;
using System.Diagnostics;
using ComponentDebug;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// DB アップデートの実行メソッド履歴リスト。
	/// </summary>
	public partial class AppDbUpdate
	{
		//
		// 参考サイト。
		//		テーブルの構造を変更するクエリ（ALTER TABLE）
		//			http://www.nurs.or.jp/~ppoy/access/access/acQ020.html
		//		データ型
		//			http://www.nurs.or.jp/~ppoy/access/access/acEt019.html
		//
		#region *** Public Value ***
		/// <summary>
		/// DB 更新履歴。
		/// </summary>
		public static AppDbUpdateHistory[]		History;
		#endregion		



		#region *** Private Method ***
		/// <summary>
		/// テーブルに指定したフィールドが存在するかDAOでチェクします。
		/// </summary>
		/// <param name="db"></param>
		/// <param name="tbl">テーブル</param>
		/// <param name="fld">フィールド</param>
		static bool checkExistFieldForDAO(DAO.Database db, string tbl, string fld)
		{
			if (db != null)
			{
				try
				{
					if (checkExistTableForDAO(db, tbl) == true)
					{
						for (int i = 0; i < db.TableDefs[tbl].Fields.Count; i++)
						{
							if (db.TableDefs[tbl].Fields[i].Name == fld)
							{
								return true;
							}
						}
					}
				}
				catch
				{
					ErrLog.WriteLine("{0}:{1}について、DAOからフィールド確認に失敗しました。", tbl, fld);
				}
			}

			return false;
		}

		/// <summary>
		/// テーブルに指定したフィールドが存在するかチェクします。
		/// </summary>
		/// <param name="db">db</param>
		/// <param name="tbl">テーブル名</param>
		static bool checkExistTableForDAO(DAO.Database db, string tbl)
		{
			try
			{
				if (db != null)
				{
					for (int i = 0; i < db.TableDefs.Count; i++)
					{
						if (db.TableDefs[i].Name == tbl)
						{
							return true;
						}
					}
				}
			}
			catch
			{
				ErrLog.WriteLine("{0}について、DAOからテーブル名の確認に失敗しました。",tbl);
			}

			return false;
		}

		/// <summary>
		/// DBからテーブルを削除します。（DAOだと消せないテーブルに有効です）
		/// </summary>
		/// <param name="db">DBオブジェクト</param>
		/// <param name="tbl">テーブル名</param>
		static bool dropTable(DB db, string tbl)
		{
			List<string> tables = db.GetTableNameList();

			// 一旦テーブルを削除する。
			if(db.GetTableNameList().Contains(tbl) == true)
			{
				try
				{
					if (db.DBCon.State == System.Data.ConnectionState.Open)
					{
						db.Close();
					}
					db.ExecuteQuery("DROP TABLE {0}", tbl);
				}
				catch(Exception ex)
				{
					ErrLog.WriteLine("Cannot Drop Table 【{0}】",tbl);
					ErrLog.WriteException(ex);
				}

				return true;
			}

			return false;
		}
		#endregion
	}

	#region AppDbUpdateHistory
	/// <summary>
	/// [作成者 fj]
	/// アップデート履歴。バージョンと、それに対応する更新メソッドを示します。
	/// </summary>
	public class AppDbUpdateHistory
	{
		/// <summary>
		/// アップデート処理を実行するためのイベントハンドラです。
		/// </summary>
		/// <param name="moddb">アップデートを行う DB。</param>
		/// <param name="seedb">参照用の DB。</param>
		public delegate bool UpdateHandler(DB moddb, DB seedb);
		
		#region *** Private Value ***
		/// <summary>
		/// バージョン情報。
		/// </summary>
		string			version;
		/// <summary>
		/// アップデートメソッド。
		/// </summary>
		UpdateHandler	updateMethod;
		#endregion
		
		#region *** Property ***
		/// <summary>
		/// バージョン情報。（文字列）
		/// </summary>
		public string	VersionStr
		{
			get
			{
				return version;
			}
		}
		/// <summary>
		/// バージョン情報。（数字の配列） 例：0.10.01→[0][10][1]、1.00.00→[1][0][0]
		/// </summary>
		public int[]	Version
		{
			get
			{
				return VersionToArray(version);
			}
		}
		
		/// <summary>
		/// アップデートメソッド
		/// </summary>
		public UpdateHandler UpdateMethod
		{
			get
			{
				return updateMethod;
			}
		}

		/// <summary>
		/// 指定されたバージョンが自分のバージョンより低い場合に、バージョンアップの必要アリを返します。
		/// </summary>
		/// <param name="verstr">チェックしたいバージョン</param>
		/// <returns>必要あればtrue</returns>
		public bool CheckNeedVersionUp(string verstr)
		{
			bool ret = false;

			if (CheckVersion(version, verstr) == eVersion.Lower)
			{
				ret = true;
			}

			return ret;
		}

		/// <summary>
		/// バージョン
		/// </summary>
		public enum eVersion
		{
			/// <summary></summary>
			None,
			/// <summary>上位</summary>
			Upper,
			/// <summary>下位</summary>
			Lower,
			/// <summary>同一</summary>
			Same,
		}

		/// <summary>
		/// 指定元と指定先を比較してバージョンの差異をチェックして返します
		/// </summary>
		/// <param name="src">指定元</param>
		/// <param name="chk">指定先</param>
		/// <returns>バージョンチェック</returns>
		public static eVersion CheckVersion(string src, string chk)
		{
			eVersion ret = eVersion.None;
			
			int [] srcVer = VersionToArray(src);
			int [] chkVer = VersionToArray(chk);

			if (srcVer != null && chkVer != null)
			{
				if (srcVer[0] > chkVer[0])
				{
					// メジャーバージョンアップ
					ret = eVersion.Lower;
				}
				else
				if (srcVer[0] < chkVer[0])
				{
					ret = eVersion.Upper;
				}
				else
				if (srcVer[1] >  chkVer[1])
				{
					// マイナーバージョンアップ
					ret = eVersion.Lower;
				}
				else
				if (srcVer[1] <  chkVer[1])
				{
					ret = eVersion.Upper;
				}
				else
				if (srcVer[2] >  chkVer[2])
				{
					// リビジョンバージョンアップ
					ret = eVersion.Lower;
				}
				else
				if (srcVer[2] <  chkVer[2])
				{
					ret = eVersion.Upper;
				}
				else
				{
					ret = eVersion.Same;
				}
			}

			return ret;
		}

		/// <summary>
		/// バージョン情報を数字の配列へ変換します。 例：0.10.01→[0][10][1]、1.00.00→[1][0][0]
		/// </summary>
		public static int[] VersionToArray(string verstr)
		{
			string[]	ver_str = System.Text.RegularExpressions.Regex.Split(verstr, @"\.");
			
			if (ver_str.Length != 0 && ver_str.Length != 3)
			{
				Debug.WriteLine("{0}は、バージョンの区切りが不正です。Version1.0.0としてアップデートを行います。", verstr);

				// 不正な場合はVersion1.0.0として認識させる。
				ver_str = new string[]{ "1", "0", "0"};
			}

			int[]		ver		= new int[ver_str.Length];

			for (int i = 0; i < ver_str.Length; i++)
			{
				ver[i] = ComponentIO.Cast.Int(ver_str[i]);
			}

			return ver;
		}
		#endregion
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="_version">バージョンを表す数字。例：0.10.01</param>
		/// <param name="_updateMethod">アップデートメソッド。</param>
		public AppDbUpdateHistory(string _version, UpdateHandler _updateMethod)
		{
			version = _version;
			updateMethod = _updateMethod;
		}
		#endregion
	}
	#endregion
}
