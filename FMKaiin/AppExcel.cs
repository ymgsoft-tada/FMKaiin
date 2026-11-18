using C1.C1Excel;
using ComponentDebug;
using ComponentIO;
using System;
using System.IO;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// Excel出力の基底クラス
	/// テンプレートの読込・保存先の選択・保存を受け持つ。
	/// 帳票ごとの抽出と書き込みは派生クラスで実装する。
	/// </summary>
	public abstract class AppExcel
	{
		/// <summary>
		/// 出力結果
		/// </summary>
		public enum eResult
		{
			/// <summary>成功</summary>
			Success,
			/// <summary>保存先の選択でキャンセル</summary>
			Cancel,
			/// <summary>テンプレートファイルなし</summary>
			NotExistTemplate,
			/// <summary>対象データなし</summary>
			NotFoundData,
			/// <summary>ファイルの保存に失敗</summary>
			CannotFileSave,
			/// <summary>その他のエラー</summary>
			Error,
		}

		/// <summary>保存用拡張子</summary>
		const string XLS_EXT = "Excelファイル|*.xls";

		/// <summary>
		/// Excelブック
		/// </summary>
		protected C1XLBook book;

		/// <summary>
		/// 保存したファイルパス
		/// </summary>
		public string FilePath { get; private set; }

		/// <summary>
		/// テンプレートファイル名（Excelフォルダ内のファイル名）
		/// </summary>
		protected abstract string TemplateFile { get; }

		/// <summary>
		/// 保存ダイアログに初期表示するファイル名
		/// </summary>
		protected abstract string DefaultFileName { get; }

		/// <summary>
		/// 前回保存したファイルパス。
		/// 帳票ごとのユーザー設定に結び付ける場合に、派生クラスで上書きします。
		/// 上書きしない場合は保持しません。
		/// </summary>
		protected virtual string SavePath
		{
			get { return ""; }
			set { }
		}

		/// <summary>
		/// データを抽出し、読み込み済みのブックへ書き込みます。
		/// </summary>
		/// <returns>Success以外を返すと保存せずに終了します。</returns>
		protected abstract eResult writeBook();

		/// <summary>
		/// Excelファイルを出力します。
		/// </summary>
		public eResult Execute()
		{
			string template = $"{AppConst.ExcelFolder}{TemplateFile}";

			if (File.Exists(template) == false)
			{
				return eResult.NotExistTemplate;
			}

			try
			{
				book = new C1XLBook();
				book.Load(template);

				eResult ret = writeBook();
				if (ret != eResult.Success)
				{
					return ret;
				}

				//¶ 2026/11/14 CC 対象データがあると分かってから保存先を尋ねる
				string path = showSaveFilePath();
				if (path == null)
				{
					return eResult.Cancel;
				}

				try
				{
					book.Save(path, FileFormat.Biff8);
				}
				catch (IOException ex)
				{
					//¶ 2026/11/14 CC 保存先のファイルがExcelで開かれている場合など
					ErrLog.WriteException(ex);
					return eResult.CannotFileSave;
				}
				catch (UnauthorizedAccessException ex)
				{
					ErrLog.WriteException(ex);
					return eResult.CannotFileSave;
				}

				FilePath = path;

				//¶ 2026/11/14 CC 次回の初期フォルダ用に保存先を保持する
				SavePath = path;
				Properties.Settings.Default.Save();

				return eResult.Success;
			}
			catch (Exception ex)
			{
				ErrLog.WriteException(ex);
				return eResult.Error;
			}
			finally
			{
				if (book != null)
				{
					book.Dispose();
					book = null;
				}
			}
		}

		/// <summary>
		/// 保存先のファイルパスを取得します。
		/// </summary>
		/// <returns>キャンセル時はnull</returns>
		string showSaveFilePath()
		{
			// はじめに表示されるフォルダ
			string inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			//¶ 2026/11/14 CC ファイル名は条件で毎回変わるため、前回の保存先からはフォルダだけを使う
			string savepath = Cast.String(SavePath).Trim();
			if (savepath != "")
			{
				try
				{
					string dir = Path.GetDirectoryName(savepath);
					if (Cast.String(dir) != "" && Directory.Exists(dir) == true)
					{
						inipath = dir;
					}
				}
				catch (ArgumentException)
				{
					//¶ 2026/11/14 CC 保持していたパスが不正な場合はデスクトップを使う
				}
			}

			using (SaveFileDialog fd = new SaveFileDialog())
			{
				fd.InitialDirectory = inipath;
				fd.FileName = DefaultFileName;
				fd.Filter = XLS_EXT;
				fd.FilterIndex = 0;
				fd.Title = "保存先を指定してください。";
				fd.RestoreDirectory = true;

				if (fd.ShowDialog() == DialogResult.OK)
				{
					return fd.FileName;
				}
			}

			return null;
		}

		/// <summary>
		/// セルへの値セット
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		/// <param name="value">値</param>
		protected void setCellValue(XLSheet sheet, int row, int col, object value)
		{
			XLCell cell = sheet[row, col];
			cell.Value = value;
		}

		/// <summary>
		/// セルへの数式セット
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		/// <param name="formula">数式（先頭の=は不要）</param>
		protected void setCellFormula(XLSheet sheet, int row, int col, string formula)
		{
			XLCell cell = sheet[row, col];
			cell.Formula = formula;
		}

		/// <summary>
		/// 指定行を書式ごと複製して、直下に挿入します。以降の行は下へずれます。
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="row">複製元の行（0始まり）</param>
		/// <param name="count">挿入する行数</param>
		protected void insertCopyRows(XLSheet sheet, int row, int count)
		{
			for (int i = 1; i <= count; i++)
			{
				sheet.Rows.Insert(row + i, sheet.Rows[row].Clone());
			}
		}

		/// <summary>
		/// セル番地（A1形式）を取得します。列はA～Zまで対応。
		/// </summary>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		protected string getCellName(int row, int col)
		{
			return $"{(char)('A' + col)}{row + 1}";
		}
	}
}
