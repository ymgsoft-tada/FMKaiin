using ComponentDB;
using ComponentDebug;
using ComponentFile;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// データベース管理クラス
	/// </summary>
	public class AppDb
	{
		/// <summary>db</summary>
		DB db;

		/// <summary>アダプタのリスト</summary>
		Dictionary<string, DBAdapter> dicAdp;
		/// <summary>テーブルリスト</summary>
		Dictionary<string, DataTable> dicTbl;

		/// <summary>郵便番号DB</summary>
		public DBZipCode DBZipCode { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppDb()
		{
			dicTbl	= new Dictionary<string, DataTable>();
			dicAdp	= new Dictionary<string, DBAdapter>();

			DBZipCode = new DBZipCode(AppConst.ZipDBPath);
		}

		/// <summary>
		/// DBの最適化処理
		/// </summary>
		void compactDB()
		{
			if (DirectoryIO.Exists(AppConst.WorkDBFolder) == false)
			{
				DirectoryIO.Make(AppConst.WorkDBFolder);
			}

			// ファイルを作業エリアにコピー
			FileIO.Copy(AppConst.DBPath, AppConst.DBWorkPath);

			//■ ファイルの最適化。
			JRO.JetEngine jro = new JRO.JetEngine();
				
			string dbcon =	@"Provider=Microsoft.Jet.OLEDB.4.0;" +
							@"Data Source=" + AppConst.DBWorkPath + ";" + 
							@"Jet OLEDB:Database Password=""" + AppConst.AppDBPassword + @""";";
			string workcon =
							@"Provider=Microsoft.Jet.OLEDB.4.0;" +
							@"Data Source=" + AppConst.DBWorkPath2 + ";" + 
							@"Jet OLEDB:Database Password=""" + AppConst.AppDBPassword + @""";";
				
			// DB を最適化。
			try
			{
				jro.CompactDatabase(dbcon, workcon);

				// 最適化したファイルをData.dbとして扱う
				FileIO.Copy(AppConst.DBWorkPath2, AppConst.DBPath);
			}
			catch (Exception ex)
			{
				ErrLog.WriteException(ex);
			}
			
			// 作業エリアの削除
			DirectoryIO.Delete(AppConst.WorkDBFolder);
		}

		/// <summary>
		/// 初期化処理
		/// </summary>
		public bool Init()
		{
			bool ret = true;

			dicTbl.Clear();
			dicAdp.Clear();

			// DBの最適化
			compactDB();

			AppDbUpdateHistory.eVersion ver = AppDbUpdateManager.CheckUpperVersion(AppConst.DBPath);

			if (ver == AppDbUpdateHistory.eVersion.Lower)
			{
				FormBg_Progress form = new FormBg_Progress();
				form.StartPosition = FormStartPosition.CenterScreen;
				form.LabelText = "データベースを更新しています...";
				form.Args = new object[] { AppConst.DBFile, AppConst.SystemFolder + AppConst.DBSrcFile };
				form.DoWorkEventEx += new DoWorkEventHandlerEx(AppDbUpdate.UpdateDataDB);
				//form.NoBgWorker	= true;
				form.ShowDialog();

				if (AppDbUpdateManager.Result == false)
				{
					AppMsgBox.Show(AppMsgBoxIndex.FailedDataVersionUp);
					return false;
				}
			}
			else
			if (ver == AppDbUpdateHistory.eVersion.Upper)
			{
				string v = AppDbUpdateManager.GetVersion(AppConst.DBFile);

				// 上位バージョンは起動できない
				AppMsgBox.Show(AppMsgBoxIndex.CannotOpenUpperVersion, v);
				return false;
			}

			//DBへの接続
			db = new DB(AppConst.DBPath, AppConst.AppDBPassword);

			if (db.Error == DBError.None)
			{
				// オープン前の状態を自動バックアップ
				Backup();

				foreach(string name in db.GetTableNameList())
				{
					if (dicAdp.ContainsKey(name) == false)
					{
						DBAdapter adp = db.AddAdapter(name);
						dicAdp.Add(name, adp);
					}

					if (dicTbl.ContainsKey(name) == false)
					{
						DataTable tbl = new DataTable();
						dicTbl.Add(name, tbl);
					}

					dicAdp[name].Fill(dicTbl[name]);
				}
			}
			else
			{
				ErrLog.WriteLine("DBへの接続に失敗しました。");
				ret = false;
			}

			//DBView dv = new DBView(dicTbl[TableProp.sys_lastupdate]);

			//if (dv.Count > 0)
			//{
			//	sys_lastupdate xrow = new sys_lastupdate(dv[0]);
			//	lastupdate = xrow.SystemLastUpdate;
			//}

			return ret;
		}

		/// <summary>
		/// SQLの実行処理
		/// </summary>
		/// <param name="sql">SQL</param>
		/// <param name="param">パラメータ</param>
		/// <returns></returns>
		public int ExecuteQuery(string sql, params object[] param)
		{
			return db.ExecuteQuery(sql, param);
		}

		/// <summary>
		/// バッファ上のテーブルを取得します。
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public DataTable GetFillTable(string name)
		{
			DataTable dt = null;

			if (dicTbl.ContainsKey(name) == true)
			{
				dt = dicTbl[name];

				// 前回取得された時にUpdateが走ってない状態なら変更を取り消す。
				if (dt.GetChanges() != null)
				{
					dt.RejectChanges();
				}
			}

			return dt;
		}

		/// <summary>
		/// DBからテーブル情報を再取得します。
		/// </summary>
		/// <param name="name"></param>
		/// <param name="wheresql"></param>
		/// <returns></returns>
		public DataTable GetReFillTable(string name, string wheresql = "")
		{
			DataTable dt = null;

			if (dicTbl.ContainsKey(name) == true &&
				dicAdp.ContainsKey(name) == true)
			{
				dicAdp[name].AddSelectCommand(wheresql);
				dicAdp[name].Fill(dicTbl[name]);
				dt = dicTbl[name];
			}
			else
			{
				DBAdapter adp = db.AddAdapter(name);
				dicAdp.Add(name, adp);
				DataTable tbl = new DataTable();
				dicTbl.Add(name, tbl);
				dicAdp[name].AddSelectCommand(wheresql);
				dicAdp[name].Fill(dicTbl[name]);
				dt =  dicTbl[name];
			}

			return dt;
		}

		/// <summary>
		/// テーブルの更新処理
		/// </summary>
		/// <param name="name">テーブル名</param>
		/// <returns></returns>
		public bool UpdateTable(string name)
		{
			bool ret = false;

			if (dicTbl.ContainsKey(name) == true)
			{
				if (dicAdp[name].Update(dicTbl[name]) >= 0)
				{
					ret = true;
				}
			}

			return ret;
		}

		/// <summary>
		/// 最終処理
		/// </summary>
		public void Finish()
		{
			if (db != null)
			{
				foreach(string name in dicAdp.Keys)
				{
					db.DelAdapter(dicAdp[name]);
					dicTbl[name].Dispose();
					dicTbl[name] = null;
				}

				db.Close();
				db = null;
			}
		}

		/// <summary>
		/// オープンファイル用ダイアログを表示します。
		/// </summary>
		/// <param name="defdir"></param>
		/// <returns></returns>
		public static string ShowOpenFilePath(string defdir)
		{
			string filepath = null;

			OpenFileDialog	fd		= new OpenFileDialog();

			// はじめに表示されるフォルダ
			string	inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			
			if (defdir != null)
			{
				if (DirectoryIO.Exists(defdir) == true)
				{
					inipath = defdir;
				}
			}
			
			fd.InitialDirectory = inipath;
			fd.FileName			= "";
			// [ファイルの種類]に表示される選択肢を指定する
			fd.Filter = string.Format("{0}ファイル|*.{0}", AppConst.BackUpFileExt);
			fd.FilterIndex = 0;
			fd.Title = "復元するファイルを指定してください。";
			fd.RestoreDirectory = true;
			
			// ダイアログを表示
			if (fd.ShowDialog() == DialogResult.OK)
			{
				filepath = fd.FileName;
			}

			return filepath;
		}

		/// <summary>
		/// ファイルパスを取得します。
		/// </summary>
		/// <param name="title">ファイル名に表示する名前</param>
		public static string ShowSaveFilePath(string title)
		{
			string filepath = null;

			SaveFileDialog	fd		= new SaveFileDialog();

			// はじめに表示されるフォルダ
			string	inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			
			if (title != null)
			{
				string path = Path.GetDirectoryName(title);

				if (path.Length > 0)
				{
					inipath = path;
				}
			}
			
			fd.InitialDirectory = inipath;
			fd.FileName			= Path.GetFileName(title);
			// [ファイルの種類]に表示される選択肢を指定する
			fd.Filter = string.Format("{0}ファイル|*.{0}", AppConst.BackUpFileExt);
			fd.FilterIndex = 0;
			fd.Title = "保存先を指定してください。";
			fd.RestoreDirectory = true;
			
			// ダイアログを表示
			if (fd.ShowDialog() == DialogResult.OK)
			{
				filepath = fd.FileName;
			}

			return filepath;
		}

		/// <summary>
		/// 圧縮されたDBファイルを復元します。
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public bool RestoreFromZip(string path)
		{
			bool ret = false;

			try
			{
				DirectoryIO.Make(AppConst.TempRestoreFolder);

				if(DirectoryIO.Exists(AppConst.TempRestoreFolder) == true)
				{
					ret = SharpZip.Uncompress(path, AppConst.TempRestoreFolder);

					if (ret == true)
					{
						string tmpfile = string.Format(@"{0}\{1}", AppConst.TempRestoreFolder, AppConst.DBFile);

						if (FileIO.Exists(tmpfile) == true)
						{
							// 念の為、現在のデータを自動保存しておく。
							AppGlobal.DB.Backup();

							FileIO.Copy(tmpfile, AppConst.DBPath);
						}
						else
						{
							ErrLog.WriteLine("復元するData.dbが見つかりません。");
							// 解凍先にdbファイルがない。
							ret = false;
						}
					}

					DirectoryIO.Delete(AppConst.TempRestoreFolder);
				}
			
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
			}

			return ret;
		}

		/// <summary>
		/// DBを圧縮して保存します。
		/// </summary>
		/// <param name="path">保存用パス</param>
		public bool BackupToZip(string path)
		{
			bool ret = false;

			try
			{
				DirectoryIO.Make(AppConst.TempBackupFolder);

				if(DirectoryIO.Exists(AppConst.TempBackupFolder) == true)
				{
					string temppath = string.Format(@"{0}\{1}", AppConst.TempBackupFolder, AppConst.DBPath);
					FileIO.Copy(db.DBLocation, temppath);

					ret = SharpZip.Compress(path, AppConst.TempBackupFolder);

					DirectoryIO.Delete(AppConst.TempBackupFolder);
				}
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
			}

			return ret;
		}

		/// <summary>
		/// DBを世代管理バックアップします。
		/// </summary>
		/// <param name="path">保存場所</param>
		/// <param name="suffix">ファイル名の後ろに付ける文字</param>
		public void Backup(string path=AppConst.BackupFolder, string suffix="")
		{
			bool ret = false;
			try 
			{
				if (DirectoryIO.Exists(path) == false)
				{
					DirectoryIO.Make(path);
				}

				List<string> files = DirectoryIO.GetFiles(path).ToList<string>();

				files.Sort();

				if (files.Count >= AppConst.MaxBackupFile)
				{
					// 古いファイルを削除する。
					FileIO.Delete(files[0]);
				}

				DateTime dt = DateTime.Now;

				string name		= string.Format("{0}{1}.{2}", dt.ToString("yyyyMMddHHmmss"), suffix, AppConst.BackUpFileExt);
				string dstpath	= string.Format(@"{0}\{1}", path, name);

				// 圧縮して保存
				ret = BackupToZip(dstpath);
				//ret =  FileIO.Copy(db.DBLocation, dstpath);
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
			}
			finally
			{
				if (ret == false)
				{
					AppMsgBox.Show(AppMsgBoxIndex.ErrorDBBackup);
				}
			}
		}

		/// <summary>
		/// 指定されたテーブルのフィールド値が他のテーブルで使用されているかどうかをチェックします。
		/// </summary>
		/// <param name="tablename">テーブル名</param>
		/// <param name="fld">フィールド名</param>
		/// <param name="val">値</param>
		/// <param name="without">除外</param>
		public bool CheckUsedOtherTable(string tablename, string fld, object val, params string[] without)
		{
			DataTable dt = null;

			List<string> lwo = new List<string>(without);

			foreach(KeyValuePair<string, DataTable> kvp in dicTbl)
			{
				if (kvp.Key != tablename && lwo.Contains(kvp.Key) == false)
				{
					dt = kvp.Value;

					foreach(DataColumn col in dt.Columns)
					{
						if (col.ColumnName == fld)
						{
							DBView dv = new DBView(dt);
							dv.SortQuery(col.ColumnName);

							if (dv.FindRow(val) == true)
							{
								return true;
							}
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Selectクエリの内容をDataTableにfillします。
		/// </summary>
		/// <param name="dq"></param>
		/// <returns></returns>
		public DataTable FillQuery(string sql)
		{
			DataTable dt = new DataTable();

			if (db != null)
			{
				DBFill dbf = new DBFill(db, sql);

				if (dbf.Fill(dt) >= 0)
				{
					return dt;
				}
			}

			return null;
		}
		/// <summary>
		/// Selectクエリの内容をDataTableにfillします。
		/// </summary>
		/// <param name="dq"></param>
		/// <returns></returns>
		public DataTable FillQuery(DBQuery dq)
		{
			return FillQuery(dq.Sql);
		}

		/// <summary>
		/// レコードを比較して差異があればtrueを返します。
		/// </summary>
		/// <param name="srcrow">元レコード</param>
		/// <param name="dstrow">後レコード</param>
		/// <returns></returns>
		public static bool CheckModified(DataRow srcrow, DataRow dstrow)
		{
			foreach(DataColumn col in srcrow.Table.Columns)
			{
				if (col.ColumnName == "ID_Auto")
				{
					// ID_Autoは無視
					continue;
				}

				if (dstrow.Table.Columns.Contains(col.ColumnName) == true)
				{
					string src = Cast.String(srcrow[col.ColumnName]);
					string dst = Cast.String(dstrow[col.ColumnName]);

					if (src != dst)
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// DataRowのカラム情報に基づいてコピーし、マッチしなかったカラムの数を返します。（0なら正しくコピーされた事になる）
		/// </summary>
		/// <param name="srow">コピー元</param>
		/// <param name="drow">コピー先</param>
		/// <returns>マッチしなかったカラムの数（0なら正しくコピーされた事になる）</returns>
		public static int CopyDataRow(DataRow srow, DataRow drow)
		{
			return CopyDataRow(srow, drow, (List<string>)null);
		}
		
		/// <summary>
		/// DataRowのカラム情報に基づいてコピーし、マッチしなかったカラムの数を返します。（0なら正しくコピーされた事になる）
		/// </summary>
		/// <param name="srow">コピー元</param>
		/// <param name="drow">コピー先</param>
		/// <param name="exceptfields">コピー除外フィールドリスト</param>
		/// <returns>マッチしなかったカラムの数（0なら正しくコピーされた事になる）</returns>
		public static int CopyDataRow(DataRow srow, DataRow drow, string[] exceptfields)
		{
			List<string>	lists = new List<string>(exceptfields);
			
			return CopyDataRow(srow, drow, lists);
		}
		
		/// <summary>
		/// DataRowのカラム情報に基づいてコピーし、マッチしなかったカラムの数を返します。（0なら正しくコピーされた事になる）
		/// </summary>
		/// <param name="srow">コピー元</param>
		/// <param name="drow">コピー先</param>
		/// <param name="exceptfields">コピー除外フィールドリスト</param>
		/// <returns>マッチしなかったカラムの数（0なら正しくコピーされた事になる）</returns>
		public static int CopyDataRow(DataRow srow, DataRow drow, List<string> exceptfields)
		{
			DataTable tblDst = drow.Table;
			DataTable tblSrc = srow.Table;

			// 列数
			int cnt = tblDst.Columns.Count;

			foreach (DataColumn col in tblSrc.Columns)
			{
				string name = col.ColumnName;

				// コピー除外フィールドのため、スルー
				if (exceptfields != null && exceptfields.Contains(name) == true)
				{
					continue;
				}
				
				if (tblDst.Columns.Contains(name) == false)
				{
					// 同一カラム名がない
					ErrLog.WriteLine("■ CopyDataRow:同一カラム名が見つかりません。{0}", name);
					continue;	
				}

				if (col.DataType != tblDst.Columns[name].DataType)
				{
					// 型が異なる
					ErrLog.WriteLine("■ CopyDataRow:コピー先の型と異なります。");
					continue;
				}

				// 同一カラム発見
				cnt--;

				if (col.AutoIncrement == true)
				{
					// 自動インクリメント項目は対象外
					continue;
				}

				if (name == "ID_Auto")
				{
					// AutoIncrementを拾えないようなので、泣く泣く固定指定。。。
					continue;
				}

				if (drow[name].ToString() == srow[name].ToString())
				{
					// 同一値なので無視
					continue;
				}

				drow.BeginEdit();
				drow[name] = srow[name];
				drow.EndEdit();
			}

			return cnt;
		}
	}
}
