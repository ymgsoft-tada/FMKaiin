using C1.C1Excel;
using ComponentDebug;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// Excelファイル取込の基底クラス
	/// ファイルの読込・見出し行の特定・進捗表示・ログを受け持つ。
	/// 取込先ごとの検証と反映は派生クラスで実装する。
	/// </summary>
	public abstract class AppExcelImport
	{
		/// <summary>
		/// 取込結果
		/// </summary>
		public enum eResult
		{
			/// <summary>成功</summary>
			Success,
			/// <summary>ファイルを開けない</summary>
			OpenFileFailed,
			/// <summary>必須の見出しがない（対象外のファイル）</summary>
			NothingRequired,
			/// <summary>取込対象のデータがない</summary>
			NoData,
			/// <summary>検証エラーあり（DBは更新していない）</summary>
			ValidateError,
			/// <summary>DBの更新に失敗</summary>
			UpdateFailed,
			/// <summary>その他のエラー</summary>
			Error,
		}

		/// <summary>Excelの行番号（1始まり）を保持する列名</summary>
		protected const string F_RowNo = "_RowNo";

		/// <summary>見出し行を探す範囲（先頭からの行数）</summary>
		const int HEADER_SEARCH_ROWS = 20;

		/// <summary>
		/// Excelから読み込んだデータ。列名は見出しの文字列、値はすべて文字列。
		/// </summary>
		protected DataTable dtSrc;

		/// <summary>
		/// 見出し行より上にある行のセル値（行・列とも0始まり）。
		/// 表の上に書かれた区分などを派生クラスで読むために保持する。
		/// </summary>
		List<string[]> topRows = new List<string[]>();

		/// <summary>取込結果</summary>
		protected eResult execResult;

		/// <summary>ログ内容</summary>
		public LogTable Log { get; private set; }

		/// <summary>
		/// 必須の見出し名。すべて揃っている行を見出し行とみなします。
		/// </summary>
		protected abstract string[] RequiredColumns { get; }

		/// <summary>
		/// 読み込んだデータを検証します。エラーはログへ追加します。
		/// </summary>
		/// <returns>エラーなし：true</returns>
		protected abstract bool validateData();

		/// <summary>
		/// 検証済みのデータをDBへ反映します。
		/// </summary>
		/// <returns>成功：true</returns>
		protected abstract bool updateData();

		/// <summary>
		/// 反映後の処理（共通情報の再取得など）。画面側のスレッドで呼ばれます。
		/// </summary>
		protected virtual void afterUpdate()
		{
		}

		/// <summary>
		/// 取込を実行します。
		/// </summary>
		/// <param name="path">Excelファイルのパス</param>
		public eResult Execute(string path)
		{
			this.Log = new LogTable();
			execResult = eResult.Error;

			FormBg_Progress prog = new FormBg_Progress();
			prog.TitleText = "しばらくお待ちください。";
			prog.LabelText = "取込を実行しています。";
			prog.Arg = path;
			prog.DoWorkEvent += doImportEvent;
			prog.ShowDialog();
			prog.Dispose();
			prog = null;

			if (execResult == eResult.Success)
			{
				afterUpdate();
			}

			return execResult;
		}

		/// <summary>
		/// 実際の取込処理
		/// </summary>
		void doImportEvent(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;
			string path = (string)prog.Arg;

			try
			{
				execResult = loadFileToTable(path);
				if (execResult != eResult.Success)
				{
					return;
				}
				Log.Apend(LogTable.eStatus.None, $"{path}を読み込みました。データ行：{dtSrc.Rows.Count}件");
				prog.AdvanceProgress(30);

				if (dtSrc.Rows.Count == 0)
				{
					Log.Apend(LogTable.eStatus.Error, "取込対象のデータがありません。");
					execResult = eResult.NoData;
					return;
				}

				//¶ 2026/11/14 CC 全行を先に検証し、エラーが1件でもあればDBを更新しない
				if (validateData() == false)
				{
					Log.Apend(LogTable.eStatus.Error, "エラーがあるため、取込を中止しました。データベースは更新していません。");
					execResult = eResult.ValidateError;
					return;
				}
				prog.AdvanceProgress(30);

				if (updateData() == false)
				{
					Log.Apend(LogTable.eStatus.Error, "データベースの更新に失敗しました。");
					execResult = eResult.UpdateFailed;
					return;
				}
				prog.AdvanceProgress(40);

				execResult = eResult.Success;
			}
			catch (Exception ex)
			{
				ErrLog.WriteException(ex);
				Log.Apend(LogTable.eStatus.Error, $"予期せぬエラーが発生しました。{ex.Message}");
				execResult = eResult.Error;
			}
		}

		/// <summary>
		/// Excelファイルの読み込み処理。先頭シートを対象にします。
		/// </summary>
		/// <param name="path">ファイルパス</param>
		eResult loadFileToTable(string path)
		{
			dtSrc = new DataTable();

			C1XLBook book = new C1XLBook();

			try
			{
				try
				{
					FileFormat fmt = FileFormat.OpenXml;
					if (Path.GetExtension(path).ToLower() == ".xls")
					{
						fmt = FileFormat.Biff8;
					}

					book.Load(path, fmt);
				}
				catch (Exception ex)
				{
					//¶ 2026/11/14 CC Excelで開いたままの場合や、Excel以外のファイルの場合
					ErrLog.WriteException(ex);
					Log.Apend(LogTable.eStatus.Error, "対象外のファイルか、他で使用中の為ファイルをオープンできません。");
					return eResult.OpenFileFailed;
				}

				XLSheet sheet = book.Sheets[0];

				int headrow = findHeaderRow(sheet);
				if (headrow < 0)
				{
					Log.Apend(LogTable.eStatus.Error, "指定されたファイルは、取込対象のファイルではありません。（見出し行が見つかりません）");
					return eResult.NothingRequired;
				}

				// 見出し行より上の行を保持
				topRows.Clear();
				for (int r = 0; r < headrow; r++)
				{
					string[] vals = new string[sheet.Columns.Count];

					for (int c = 0; c < vals.Length; c++)
					{
						vals[c] = getCellString(sheet, r, c);
					}

					topRows.Add(vals);
				}

				// 見出し行から列を作成（Excelの列位置と対応させる）
				List<int> cols = new List<int>();

				dtSrc.Columns.Add(F_RowNo, typeof(int));

				for (int c = 0; c < sheet.Columns.Count; c++)
				{
					string name = getCellString(sheet, headrow, c);

					if (name == "")
					{
						continue;
					}

					//¶ 2026/11/14 CC 同じ見出しが複数ある場合、2つ目以降は「見出し#2」「見出し#3」の列名にして区別する
					if (dtSrc.Columns.Contains(name) == true)
					{
						int no = 2;
						while (dtSrc.Columns.Contains($"{name}#{no}") == true)
						{
							no++;
						}
						name = $"{name}#{no}";
					}

					dtSrc.Columns.Add(name, typeof(string));
					cols.Add(c);
				}

				// 見出し行の次からがデータ行
				for (int r = headrow + 1; r < sheet.Rows.Count; r++)
				{
					DataRow nrow = dtSrc.NewRow();
					bool empty = true;

					nrow[F_RowNo] = r + 1;

					for (int i = 0; i < cols.Count; i++)
					{
						string val = getCellString(sheet, r, cols[i]);

						nrow[i + 1] = val;

						if (val != "")
						{
							empty = false;
						}
					}

					//¶ 2026/11/14 CC 何も入っていない行は読み飛ばす
					if (empty == false)
					{
						dtSrc.Rows.Add(nrow);
					}
				}

				return eResult.Success;
			}
			finally
			{
				book.Dispose();
				book = null;
			}
		}

		/// <summary>
		/// 必須の見出しがすべて揃っている行を探します。
		/// </summary>
		/// <returns>行（0始まり）。見つからない場合は-1</returns>
		int findHeaderRow(XLSheet sheet)
		{
			for (int r = 0; r < sheet.Rows.Count && r < HEADER_SEARCH_ROWS; r++)
			{
				List<string> names = new List<string>();

				for (int c = 0; c < sheet.Columns.Count; c++)
				{
					names.Add(getCellString(sheet, r, c));
				}

				bool found = true;

				foreach (string req in RequiredColumns)
				{
					if (names.Contains(req) == false)
					{
						found = false;
						break;
					}
				}

				if (found == true)
				{
					return r;
				}
			}

			return -1;
		}

		/// <summary>
		/// セルの値を文字列で取得します。
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		string getCellString(XLSheet sheet, int row, int col)
		{
			XLCell cell = sheet.GetCell(row, col);

			if (cell == null || cell.Value == null)
			{
				return "";
			}

			object val = cell.Value;

			//¶ 2026/11/14 CC 日付セルは DateTime で返る場合があるので、書式を固定した文字列にする
			if (val is DateTime)
			{
				return ((DateTime)val).ToString("yyyy/MM/dd");
			}

			//¶ 2026/11/14 CC 数値セルは double で返るので、整数なら小数点なしの文字列にする
			if (val is double)
			{
				double d = (double)val;

				if (d == Math.Floor(d) && Math.Abs(d) < 1e15)
				{
					return ((long)d).ToString();
				}

				return d.ToString();
			}

			return val.ToString().Trim();
		}

		/// <summary>
		/// 行の値を取得します。見出しがない場合は空文字を返します。
		/// </summary>
		/// <param name="row">dtSrcの行</param>
		/// <param name="name">見出し名</param>
		protected string getValue(DataRow row, string name)
		{
			if (dtSrc.Columns.Contains(name) == false)
			{
				return "";
			}

			return row[name].ToString();
		}

		/// <summary>
		/// 見出し行より上にあるセルの値を取得します。範囲外は空文字を返します。
		/// </summary>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		protected string getTopValue(int row, int col)
		{
			if (row < 0 || row >= topRows.Count || col < 0 || col >= topRows[row].Length)
			{
				return "";
			}

			return topRows[row][col];
		}

		/// <summary>
		/// 見出し行より上にある行数を取得します。
		/// </summary>
		protected int getTopRowCount()
		{
			return topRows.Count;
		}

		/// <summary>
		/// 値が未入力（DBNull・空文字・空白のみ）かを調べます。
		/// </summary>
		protected bool isEmpty(object value)
		{
			return value == null || value == DBNull.Value || value.ToString().Trim() == "";
		}

		/// <summary>
		/// 値が変わる場合だけ行へセットします。空文字とnullはDBNullとして扱います。
		/// </summary>
		/// <returns>変更あり：true</returns>
		protected bool setValue(DataRow row, string fld, object value)
		{
			if (value == null || (value is string && (string)value == ""))
			{
				value = DBNull.Value;
			}

			object cur = row[fld];

			//¶ 2026/11/14 CC 既存データの空文字とDBNullは同じとみなす（変更なしの行を更新扱いにしない）
			if (cur is string && (string)cur == "")
			{
				cur = DBNull.Value;
			}

			if (cur.Equals(value) == true)
			{
				return false;
			}

			row[fld] = value;

			return true;
		}

		/// <summary>
		/// 日付の文字列を読み取ります。年月だけの場合は1日とします。
		/// 対応する形式：和暦（平成16年 6月 1日／平成 2年 3月）、西暦（1963年 7月 9日）、yyyy/MM/dd、Excelの日付値（シリアル値）。
		/// </summary>
		/// <param name="src">Excelの値</param>
		/// <param name="date">読み取った日付。空欄と0は未入力としてnull</param>
		/// <returns>読み取れない場合：false</returns>
		protected bool tryParseDate(string src, out DateTime? date)
		{
			date = null;

			src = src.Trim();
			if (src == "" || src == "0")
			{
				return true;
			}

			try
			{
				int year = 0;
				int month = 0;
				int day = 1;

				Match m = Regex.Match(src, @"^(明治|大正|昭和|平成|令和)\s*(\d+|元)年\s*(\d+)月(?:\s*(\d+)日)?$");
				if (m.Success == true)
				{
					switch (m.Groups[1].Value)
					{
						case "明治": year = 1867; break;
						case "大正": year = 1911; break;
						case "昭和": year = 1925; break;
						case "平成": year = 1988; break;
						case "令和": year = 2018; break;
					}

					year += (m.Groups[2].Value == "元") ? 1 : int.Parse(m.Groups[2].Value);
					month = int.Parse(m.Groups[3].Value);
					if (m.Groups[4].Success == true)
					{
						day = int.Parse(m.Groups[4].Value);
					}

					date = new DateTime(year, month, day);
					return true;
				}

				m = Regex.Match(src, @"^(\d{4})年\s*(\d+)月(?:\s*(\d+)日)?$");
				if (m.Success == false)
				{
					m = Regex.Match(src, @"^(\d{4})/(\d{1,2})(?:/(\d{1,2}))?$");
				}
				if (m.Success == true)
				{
					year = int.Parse(m.Groups[1].Value);
					month = int.Parse(m.Groups[2].Value);
					if (m.Groups[3].Success == true)
					{
						day = int.Parse(m.Groups[3].Value);
					}

					date = new DateTime(year, month, day);
					return true;
				}

				//¶ 2026/11/14 CC 表示形式が日付になっていないセルは、日付値（1900年からの日数）のまま入っている
				double serial;
				if (double.TryParse(src, out serial) == true && serial > 1000 && serial < 100000)
				{
					date = DateTime.FromOADate(serial).Date;
					return true;
				}
			}
			catch (ArgumentException)
			{
				//¶ 2026/11/14 CC 13月や32日など、存在しない日付
			}
			catch (FormatException)
			{
			}

			return false;
		}

		/// <summary>4桁の市外局番（0466-XX-XXXX の形で区切る）</summary>
		static readonly string[] TEL_Area4 =
		{
			"0422", "0428", "0436", "0438", "0439", "0460", "0463", "0465", "0466", "0467",
			"0470", "0475", "0476", "0478", "0479", "0480", "0493", "0494", "0495",
		};
		/// <summary>2桁の市外局番（03-XXXX-XXXX の形で区切る）</summary>
		static readonly string[] TEL_Area2 = { "03", "06" };
		/// <summary>3桁の市外局番（045-XXX-XXXX の形で区切る）</summary>
		static readonly string[] TEL_Area3 =
		{
			"011", "022", "042", "043", "044", "045", "046", "047", "048", "049", "052", "075", "078", "092",
		};
		/// <summary>携帯・IP電話（090-XXXX-XXXX の形で区切る）</summary>
		static readonly string[] TEL_Mobile = { "050", "070", "080", "090" };

		/// <summary>
		/// 電話番号をハイフン付きに整形します。
		/// Excelでは数値セルのため先頭の0が落ち、ハイフンもない（例：466267225）。
		/// </summary>
		/// <param name="src">Excelの値</param>
		/// <param name="warn">区切り位置を判定できなかった場合にtrue</param>
		protected string formatTel(string src, out bool warn)
		{
			warn = false;

			if (src == "")
			{
				return "";
			}

			// 数字以外を含む場合（既にハイフン付きなど）は、そのまま登録する
			foreach (char c in src)
			{
				if (c < '0' || c > '9')
				{
					return src;
				}
			}

			//¶ 2026/11/14 CC 9999999999 は番号なしを表すダミー値なので空にする
			if (src.Trim('9') == "")
			{
				return "";
			}

			string tel = src;
			if (tel.StartsWith("0") == false)
			{
				tel = "0" + tel;
			}

			if (tel.Length == 11)
			{
				foreach (string code in TEL_Mobile)
				{
					if (tel.StartsWith(code) == true)
					{
						return $"{tel.Substring(0, 3)}-{tel.Substring(3, 4)}-{tel.Substring(7)}";
					}
				}
			}

			if (tel.Length == 10)
			{
				//¶ 2026/11/14 CC 桁数の多い市外局番から先に判定する（0466 を 046 より先に）
				foreach (string code in TEL_Area4)
				{
					if (tel.StartsWith(code) == true)
					{
						return $"{tel.Substring(0, 4)}-{tel.Substring(4, 2)}-{tel.Substring(6)}";
					}
				}
				foreach (string code in TEL_Area2)
				{
					if (tel.StartsWith(code) == true)
					{
						return $"{tel.Substring(0, 2)}-{tel.Substring(2, 4)}-{tel.Substring(6)}";
					}
				}
				foreach (string code in TEL_Area3)
				{
					if (tel.StartsWith(code) == true)
					{
						return $"{tel.Substring(0, 3)}-{tel.Substring(3, 3)}-{tel.Substring(6)}";
					}
				}
			}

			warn = true;

			return tel;
		}

		/// <summary>
		/// 携帯電話の番号（070／080／090）かを調べます。
		/// </summary>
		/// <param name="tel">整形済みの電話番号</param>
		protected bool isMobileTel(string tel)
		{
			return tel.StartsWith("070") == true || tel.StartsWith("080") == true || tel.StartsWith("090") == true;
		}

		/// <summary>
		/// 郵便番号を 999-9999 の形に整形します。数値セルで先頭の0が落ちている場合は補います。
		/// </summary>
		protected string formatPost(string src)
		{
			if (src == "" || src.Length > 7)
			{
				return src;
			}

			foreach (char c in src)
			{
				if (c < '0' || c > '9')
				{
					return src;
				}
			}

			string post = src.PadLeft(7, '0');

			return $"{post.Substring(0, 3)}-{post.Substring(3)}";
		}

		/// <summary>
		/// Excelの行番号を取得します。
		/// </summary>
		/// <param name="row">dtSrcの行</param>
		protected int getRowNo(DataRow row)
		{
			return (int)row[F_RowNo];
		}
	}
}
