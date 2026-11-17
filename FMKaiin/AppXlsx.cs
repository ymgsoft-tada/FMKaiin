using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Xml;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// xlsxファイルの中身（XML）を直接書き換えるクラス
	/// C1Excelで読み書きすると消えるもの（入力規則のプルダウンなど）を残したいテンプレートに使う。
	/// 既にあるセルへ値・数式を書き込むだけで、書式・図形・印刷設定には触れない。
	/// </summary>
	public class AppXlsx : IDisposable
	{
		/// <summary>ワークシートの名前空間</summary>
		const string NsMain = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
		/// <summary>リレーションIDの名前空間</summary>
		const string NsRel = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
		/// <summary>計算チェーンのリレーション種別</summary>
		const string RelTypeCalcChain = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/calcChain";
		/// <summary>ブックのパーツ</summary>
		const string WorkbookUri = "/xl/workbook.xml";

		/// <summary>
		/// シート1つ分のデータ
		/// </summary>
		class SheetData
		{
			/// <summary>パーツのURI</summary>
			public Uri Uri;
			/// <summary>シートのXML</summary>
			public XmlDocument Doc;
			/// <summary>行番号（1始まり）→ 行の要素</summary>
			public Dictionary<int, XmlElement> Rows;
		}

		Package package;
		XmlDocument workbook;
		Dictionary<string, SheetData> sheets = new Dictionary<string, SheetData>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="path">書き換えるxlsxファイルのパス（テンプレートのコピー）</param>
		public AppXlsx(string path)
		{
			package = Package.Open(path, FileMode.Open, FileAccess.ReadWrite);
			workbook = loadPart(new Uri(WorkbookUri, UriKind.Relative));
		}

		/// <summary>
		/// シートがあるかどうかを取得します。
		/// </summary>
		/// <param name="sheetName">シート名</param>
		public bool ExistsSheet(string sheetName)
		{
			return (getSheetUri(sheetName) != null);
		}

		/// <summary>
		/// セルへ数値をセットします。
		/// </summary>
		/// <param name="sheetName">シート名</param>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		/// <param name="value">値</param>
		public void SetNumber(string sheetName, int row, int col, decimal value)
		{
			XmlElement cell = clearCell(sheetName, row, col);
			appendElement(cell, "v", value.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// セルへ日付をセットします。表示形式はテンプレートのセルの書式に従います。
		/// </summary>
		/// <param name="sheetName">シート名</param>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		/// <param name="value">値</param>
		public void SetDate(string sheetName, int row, int col, DateTime value)
		{
			//¶ 2026/11/16 CC Excelの日付はシリアル値で持つ
			XmlElement cell = clearCell(sheetName, row, col);
			appendElement(cell, "v", value.ToOADate().ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// セルへ文字列をセットします。
		/// </summary>
		/// <param name="sheetName">シート名</param>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		/// <param name="value">値</param>
		public void SetString(string sheetName, int row, int col, string value)
		{
			//¶ 2026/11/16 CC 共有文字列の表には足さず、セルの中に直接持たせる（インライン文字列）
			XmlElement cell = clearCell(sheetName, row, col);
			cell.SetAttribute("t", "inlineStr");

			XmlElement si = cell.OwnerDocument.CreateElement("is", NsMain);
			cell.AppendChild(si);
			XmlElement t = appendElement(si, "t", value);
			//¶ 2026/11/16 CC 前後の空白を落とされないようにする
			//¶ 2026/11/16 CC 接頭辞を xml と明示して作る。SetAttribute(名前, 名前空間, 値) だと別の接頭辞が付き、Excelで開けないファイルになる
			XmlAttribute space = cell.OwnerDocument.CreateAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
			space.Value = "preserve";
			t.Attributes.Append(space);
		}

		/// <summary>
		/// セルへ数式をセットします。計算結果はExcelで開いたときに求められます。
		/// </summary>
		/// <param name="sheetName">シート名</param>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		/// <param name="formula">数式（先頭の=は不要）</param>
		public void SetFormula(string sheetName, int row, int col, string formula)
		{
			XmlElement cell = clearCell(sheetName, row, col);
			appendElement(cell, "f", formula);
		}

		/// <summary>
		/// 書き換えた内容をファイルへ保存して閉じます。
		/// </summary>
		public void Save()
		{
			foreach (SheetData sheet in sheets.Values)
			{
				savePart(sheet.Uri, sheet.Doc);
			}

			setFullCalcOnLoad();
			savePart(new Uri(WorkbookUri, UriKind.Relative), workbook);

			deleteCalcChain();

			package.Close();
			package = null;
		}

		/// <summary>
		/// ファイルを閉じます。Saveを呼んでいない場合、書き換えた内容は保存されません。
		/// </summary>
		public void Dispose()
		{
			if (package != null)
			{
				package.Close();
				package = null;
			}
		}

		/// <summary>
		/// セル番地（A1形式）を取得します。
		/// </summary>
		/// <param name="row">行（0始まり）</param>
		/// <param name="col">列（0始まり）</param>
		public static string GetCellName(int row, int col)
		{
			return $"{GetColumnName(col)}{row + 1}";
		}

		/// <summary>
		/// 列名（A、B、…、Z、AA、…）を取得します。
		/// </summary>
		/// <param name="col">列（0始まり）</param>
		public static string GetColumnName(int col)
		{
			string name = "";
			int n = col + 1;

			while (n > 0)
			{
				int mod = (n - 1) % 26;
				name = (char)('A' + mod) + name;
				n = (n - 1) / 26;
			}

			return name;
		}

		/// <summary>
		/// セル番地から列（0始まり）を取得します。
		/// </summary>
		static int getColumnIndex(string cellName)
		{
			int n = 0;

			foreach (char ch in cellName)
			{
				if (ch < 'A' || ch > 'Z')
				{
					break;
				}
				n = n * 26 + (ch - 'A' + 1);
			}

			return n - 1;
		}

		/// <summary>
		/// パーツのXMLを読み込みます。
		/// </summary>
		XmlDocument loadPart(Uri uri)
		{
			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = true;

			using (Stream st = package.GetPart(uri).GetStream(FileMode.Open, FileAccess.Read))
			{
				doc.Load(st);
			}

			return doc;
		}

		/// <summary>
		/// パーツのXMLを書き戻します。
		/// </summary>
		void savePart(Uri uri, XmlDocument doc)
		{
			//¶ 2026/11/16 CC Excelが出力するファイルに合わせて、BOMなしのUTF-8で書く
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Encoding = new UTF8Encoding(false);

			using (Stream st = package.GetPart(uri).GetStream(FileMode.Create, FileAccess.Write))
			using (XmlWriter writer = XmlWriter.Create(st, settings))
			{
				doc.Save(writer);
			}
		}

		/// <summary>
		/// シート名からパーツのURIを取得します。
		/// </summary>
		/// <returns>シートがない場合はnull</returns>
		Uri getSheetUri(string sheetName)
		{
			Uri wbUri = new Uri(WorkbookUri, UriKind.Relative);

			foreach (XmlElement el in workbook.GetElementsByTagName("sheet", NsMain))
			{
				if (el.GetAttribute("name") != sheetName)
				{
					continue;
				}

				string rid = el.GetAttribute("id", NsRel);
				PackageRelationship rel = package.GetPart(wbUri).GetRelationship(rid);

				return PackUriHelper.ResolvePartUri(wbUri, rel.TargetUri);
			}

			return null;
		}

		/// <summary>
		/// シートのデータを取得します（初回に読み込み）。
		/// </summary>
		SheetData getSheet(string sheetName)
		{
			if (sheets.ContainsKey(sheetName) == true)
			{
				return sheets[sheetName];
			}

			Uri uri = getSheetUri(sheetName);
			if (uri == null)
			{
				throw new ArgumentException($"シート「{sheetName}」がありません。");
			}

			SheetData sheet = new SheetData();
			sheet.Uri = uri;
			sheet.Doc = loadPart(uri);
			sheet.Rows = new Dictionary<int, XmlElement>();

			foreach (XmlElement el in sheet.Doc.GetElementsByTagName("row", NsMain))
			{
				int no;
				if (int.TryParse(el.GetAttribute("r"), out no) == true)
				{
					sheet.Rows[no] = el;
				}
			}

			sheets.Add(sheetName, sheet);

			return sheet;
		}

		/// <summary>
		/// 行の要素を取得します。なければ行番号の順になる位置に作ります。
		/// </summary>
		XmlElement getRow(SheetData sheet, int rowNo)
		{
			if (sheet.Rows.ContainsKey(rowNo) == true)
			{
				return sheet.Rows[rowNo];
			}

			XmlNode sheetData = sheet.Doc.GetElementsByTagName("sheetData", NsMain)[0];

			XmlElement row = sheet.Doc.CreateElement("row", NsMain);
			row.SetAttribute("r", rowNo.ToString());

			XmlNode after = null;
			foreach (XmlNode node in sheetData.ChildNodes)
			{
				XmlElement el = node as XmlElement;
				int no;
				if (el != null && int.TryParse(el.GetAttribute("r"), out no) == true && no > rowNo)
				{
					after = el;
					break;
				}
			}

			if (after != null)
			{
				sheetData.InsertBefore(row, after);
			}
			else
			{
				sheetData.AppendChild(row);
			}

			sheet.Rows[rowNo] = row;

			return row;
		}

		/// <summary>
		/// セルの要素を取得します。なければ列の順になる位置に作ります。
		/// </summary>
		XmlElement getCell(SheetData sheet, int row, int col)
		{
			XmlElement rowEl = getRow(sheet, row + 1);
			string name = GetCellName(row, col);

			XmlNode after = null;
			foreach (XmlNode node in rowEl.ChildNodes)
			{
				XmlElement el = node as XmlElement;
				if (el == null)
				{
					continue;
				}

				int c = getColumnIndex(el.GetAttribute("r"));
				if (c == col)
				{
					return el;
				}
				if (c > col)
				{
					after = el;
					break;
				}
			}

			//¶ 2026/11/16 CC 新しく作ったセルには書式が付かない。書式が要るセルは、テンプレート側に書式付きの空セルを用意しておく
			XmlElement cell = sheet.Doc.CreateElement("c", NsMain);
			cell.SetAttribute("r", name);

			if (after != null)
			{
				rowEl.InsertBefore(cell, after);
			}
			else
			{
				rowEl.AppendChild(cell);
			}

			return cell;
		}

		/// <summary>
		/// セルの中身（値・数式・データ型）を消し、書式だけを残した要素を取得します。
		/// </summary>
		XmlElement clearCell(string sheetName, int row, int col)
		{
			SheetData sheet = getSheet(sheetName);
			XmlElement cell = getCell(sheet, row, col);

			string name = cell.GetAttribute("r");
			string style = cell.GetAttribute("s");

			cell.RemoveAll();
			cell.SetAttribute("r", name);
			if (style != "")
			{
				cell.SetAttribute("s", style);
			}

			return cell;
		}

		/// <summary>
		/// 子要素を追加します。
		/// </summary>
		XmlElement appendElement(XmlElement parent, string name, string text)
		{
			XmlElement el = parent.OwnerDocument.CreateElement(name, NsMain);
			el.InnerText = text;
			parent.AppendChild(el);

			return el;
		}

		/// <summary>
		/// Excelで開いたときに、すべての数式を計算し直すように指定します。
		/// </summary>
		void setFullCalcOnLoad()
		{
			//¶ 2026/11/16 CC 数式のセルには計算結果が保存されているので、値を書き換えただけでは古い結果が表示される
			XmlNodeList list = workbook.GetElementsByTagName("calcPr", NsMain);
			XmlElement calc;

			if (list.Count > 0)
			{
				calc = (XmlElement)list[0];
			}
			else
			{
				//¶ 2026/11/16 CC 要素の順番が決まっているので、名前定義（なければシート一覧）の直後に入れる
				calc = workbook.CreateElement("calcPr", NsMain);

				XmlNodeList names = workbook.GetElementsByTagName("definedNames", NsMain);
				XmlNode prev = (names.Count > 0) ? names[0] : workbook.GetElementsByTagName("sheets", NsMain)[0];
				prev.ParentNode.InsertAfter(calc, prev);
			}

			calc.SetAttribute("fullCalcOnLoad", "1");
		}

		/// <summary>
		/// 計算チェーン（数式セルの計算順の一覧）を削除します。
		/// </summary>
		void deleteCalcChain()
		{
			//¶ 2026/11/16 CC 数式を値に書き換えたセルが一覧に残っていると、Excelが修復を求める。削除してもExcelが作り直す
			PackagePart wbPart = package.GetPart(new Uri(WorkbookUri, UriKind.Relative));
			List<PackageRelationship> rels = new List<PackageRelationship>(wbPart.GetRelationshipsByType(RelTypeCalcChain));

			foreach (PackageRelationship rel in rels)
			{
				Uri uri = PackUriHelper.ResolvePartUri(wbPart.Uri, rel.TargetUri);
				if (package.PartExists(uri) == true)
				{
					package.DeletePart(uri);
				}
				wbPart.DeleteRelationship(rel.Id);
			}
		}
	}
}
