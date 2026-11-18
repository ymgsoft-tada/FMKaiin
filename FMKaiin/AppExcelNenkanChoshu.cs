using C1.C1Excel;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 年間会費徴収書類（予定通知書・徴収明細書）のExcel出力
	/// 帳票と同等の内容を、手入力で直せる形で出力する。
	/// </summary>
	public class AppExcelNenkanChoshu : AppExcel
	{
		/// <summary>
		/// 書類タイプ
		/// </summary>
		public enum eType
		{
			/// <summary>年間会費徴収予定通知書（会費設定と調整額から作成）</summary>
			Yotei,
			/// <summary>年間会費徴収明細書（引落データから作成）</summary>
			Meisai,
		}

		/// <summary>1年の月数</summary>
		const int MonthCount = 12;
		/// <summary>テンプレートの明細行数</summary>
		const int DetailCount = 23;

		//¶ 2026/11/16 CC テンプレート上の位置（0始まり）。予定通知書と徴収明細書で共通のもの
		/// <summary>郵便番号の行（以降、住所1・住所2・医療機関名・氏名と続く）</summary>
		const int RowPost = 1;
		/// <summary>宛先の列</summary>
		const int ColAtesaki = 0;
		/// <summary>会員コードの行</summary>
		const int RowCode = 4;
		/// <summary>会員コードの列</summary>
		const int ColCode = 13;
		/// <summary>発行日の行</summary>
		const int RowDateOut = 5;
		/// <summary>発行日の列</summary>
		const int ColDateOut = 12;
		/// <summary>会費名の列</summary>
		const int ColName = 0;
		/// <summary>1月の列</summary>
		const int ColMonth = 1;
		/// <summary>合計の列</summary>
		const int ColTotal = 13;

		/// <summary>
		/// 書類タイプごとのテンプレート上の位置（0始まり）
		/// </summary>
		class Layout
		{
			/// <summary>テンプレートファイル名</summary>
			public string TemplateFile;
			/// <summary>書類名</summary>
			public string Title;
			/// <summary>宛先の先頭に付ける空白</summary>
			public string Indent;
			/// <summary>対象年の行</summary>
			public int RowYear;
			/// <summary>明細の先頭行</summary>
			public int RowDetail;
			/// <summary>年間金額の行</summary>
			public int RowNenkan;
			/// <summary>年間金額の列</summary>
			public int ColNenkan;
		}

		static readonly Layout LayoutYotei = new Layout()
		{
			TemplateFile = "NenkanChoshuYotei.xls",
			Title = "年間会費徴収予定通知書",
			Indent = "　",
			RowYear = 9,
			RowDetail = 11,
			RowNenkan = 38,
			ColNenkan = 12,
		};

		static readonly Layout LayoutMeisai = new Layout()
		{
			TemplateFile = "NenkanChoshuMeisai.xls",
			Title = "年間会費徴収明細書",
			Indent = "　　　",
			RowYear = 12,
			RowDetail = 15,
			RowNenkan = 42,
			ColNenkan = 11,
		};

		//¶ 2026/11/16 CC 予定通知書だけにある項目
		/// <summary>扱銀行（支払方法の番号）の列</summary>
		const int ColBankIdx = 14;
		/// <summary>扱銀行の情報を出す先頭行</summary>
		const int RowBankInfo = 36;

		//¶ 2026/11/16 CC 徴収明細書だけにある項目
		/// <summary>引落銀行名の行</summary>
		const int RowBankName = 7;
		/// <summary>支店名の行</summary>
		const int RowBankShiten = 8;
		/// <summary>引落銀行名・支店名の列</summary>
		const int ColBank = 9;

		/// <summary>
		/// 明細1行分のデータ
		/// </summary>
		class Detail
		{
			/// <summary>会費ID</summary>
			public int ID_Kaihi;
			/// <summary>会費名</summary>
			public string Name;
			/// <summary>支払方法</summary>
			public eShiharai Shiharai;
			/// <summary>月ごとの金額（[0]が1月）。空欄にする月はnull</summary>
			public decimal?[] Costs = new decimal?[MonthCount];
		}

		/// <summary>
		/// 1シート分のデータ
		/// </summary>
		class SheetData
		{
			/// <summary>シート名</summary>
			public string SheetName;
			/// <summary>会員</summary>
			public Kaiin Kaiin;
			/// <summary>引落口座（徴収明細書のみ）</summary>
			public eShiharai Shiharai;
			/// <summary>明細</summary>
			public List<Detail> Details = new List<Detail>();
		}

		/// <summary>
		/// 口座情報
		/// </summary>
		class Koza
		{
			/// <summary>銀行名</summary>
			public string BankName = "";
			/// <summary>支店名</summary>
			public string ShitenName = "";
			/// <summary>預金種別</summary>
			public string KozaType = "";
			/// <summary>口座番号</summary>
			public string KozaNo = "";
		}

		int year;
		eType type;
		List<Kaiin> kaiins;
		Layout layout;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="year">対象年（西暦。1月～12月）</param>
		/// <param name="type">書類タイプ</param>
		/// <param name="kaiins">出力する会員</param>
		public AppExcelNenkanChoshu(int year, eType type, List<Kaiin> kaiins)
		{
			this.year = year;
			this.type = type;
			this.kaiins = kaiins;
			this.layout = (type == eType.Yotei) ? LayoutYotei : LayoutMeisai;
		}

		/// <summary>
		/// テンプレートファイル名
		/// </summary>
		protected override string TemplateFile
		{
			get { return layout.TemplateFile; }
		}

		/// <summary>
		/// 保存ダイアログに初期表示するファイル名
		/// </summary>
		protected override string DefaultFileName
		{
			get
			{
				//¶ 2026/11/16 CC 1人だけ出力するときは、会員コードと氏名を続けて付ける
				if (kaiins.Count == 1)
				{
					string name = Cast.String(kaiins[0].XRow.Kaiin_Name);

					//¶ 2026/11/16 CC ファイル名に使えない文字は置き換える
					foreach (char c in System.IO.Path.GetInvalidFileNameChars())
					{
						name = name.Replace(c, '_');
					}

					return $"{year}年分_{layout.Title}_{kaiins[0].CodeString}{name}.xls";
				}

				return $"{year}年分_{layout.Title}.xls";
			}
		}

		/// <summary>
		/// 前回保存したファイルパス
		/// </summary>
		protected override string SavePath
		{
			get { return Properties.Settings.Default.ExcelNenkanChoshuSavePath; }
			set { Properties.Settings.Default.ExcelNenkanChoshuSavePath = value; }
		}

		/// <summary>
		/// データを抽出し、ブックへ書き込みます。
		/// </summary>
		protected override eResult writeBook()
		{
			List<SheetData> list = new List<SheetData>();

			foreach (Kaiin ka in kaiins)
			{
				if (ka == null)
				{
					continue;
				}

				if (type == eType.Yotei)
				{
					SheetData sd = createYotei(ka);
					if (sd != null)
					{
						list.Add(sd);
					}
				}
				else
				{
					list.AddRange(createMeisai(ka));
				}
			}

			if (list.Count == 0)
			{
				return eResult.NotFoundData;
			}

			XLSheet template = book.Sheets[0];

			//¶ 2026/11/16 CC 複製したシートには印刷範囲が引き継がれないので、テンプレートの印刷範囲を控えておく
			int printRows = 0;
			int printCols = 0;
			foreach (XLNamedRange nr in book.NamedRanges)
			{
				if (nr.IsBuiltInName == true && nr.CellRange != null)
				{
					printRows = nr.CellRange.RowTo + 1;
					printCols = nr.CellRange.ColumnTo + 1;
					break;
				}
			}

			//¶ 2026/11/16 CC 2シート目以降は、書き込む前のテンプレートを複製して追加する
			List<XLSheet> sheets = new List<XLSheet>();
			sheets.Add(template);
			for (int i = 1; i < list.Count; i++)
			{
				XLSheet clone = template.Clone();
				clone.Name = list[i].SheetName;
				book.Sheets.Add(clone);
				sheets.Add(clone);
			}
			template.Name = list[0].SheetName;

			for (int i = 0; i < list.Count; i++)
			{
				int extra = writeSheet(sheets[i], list[i]);

				if (i > 0 && printRows > 0)
				{
					//¶ 2026/11/16 CC 印刷範囲の名前は「Print_Area＋シートの位置」。行を足した分だけ広げる
					book.NamedRanges.Add($"Print_Area{i}", sheets[i], 0, 0, printRows + extra, printCols);
				}
			}

			return eResult.Success;
		}

		/// <summary>
		/// 対象年の各月が入会期間中かどうかを取得します。
		/// </summary>
		/// <param name="xrow">会員レコード</param>
		/// <returns>[0]が1月。期間中ならtrue</returns>
		bool[] getPeriod(t_kaiin xrow)
		{
			bool[] period = new bool[MonthCount];

			//¶ 2026/11/16 CC 入会日が空なら年初から、退会日が空なら年末までを期間中とする
			DateTime? nyukai = xrow.Kaiin_DateNyukai_Null;
			DateTime? taikai = xrow.Kaiin_DateTaikai_Null;

			for (int m = 0; m < MonthCount; m++)
			{
				DateTime first = AppDate.ParseDateFirstDay(year, m + 1);
				DateTime last = AppDate.ParseDateLastDay(first);

				//¶ 2026/11/16 CC 入会月と退会月は期間中として扱う
				if ((nyukai != null && nyukai.Value.Date > last) ||
					(taikai != null && taikai.Value.Date < first))
				{
					continue;
				}

				period[m] = true;
			}

			return period;
		}

		/// <summary>
		/// 明細行を新しく作ります。入会期間中の月は0、期間外の月は空欄で初期化します。
		/// </summary>
		Detail newDetail(int id_kaihi, string name, eShiharai shiharai, bool[] period)
		{
			Detail d = new Detail();
			d.ID_Kaihi = id_kaihi;
			d.Name = name;
			d.Shiharai = shiharai;

			for (int m = 0; m < MonthCount; m++)
			{
				if (period[m] == true)
				{
					d.Costs[m] = 0;
				}
			}

			return d;
		}

		/// <summary>
		/// 会費マスターから会費名を取得します。
		/// </summary>
		string getKaihiName(int id_kaihi)
		{
			Kaihi k = AppGlobal.Kaihis.Get(id_kaihi);

			return (k != null) ? Cast.String(k.XRow.Kaihi_Name_Null) : "";
		}

		/// <summary>
		/// 口座引落の支払方法かどうか
		/// </summary>
		bool isKoza(eShiharai shiharai)
		{
			return (shiharai == eShiharai.Koza1 || shiharai == eShiharai.Koza2 || shiharai == eShiharai.Koza3);
		}

		/// <summary>
		/// 予定通知書のデータを作成します（会費設定の月額＋調整額）。
		/// </summary>
		/// <param name="ka">会員</param>
		/// <returns>対象データがない場合はnull</returns>
		SheetData createYotei(Kaiin ka)
		{
			bool[] period = getPeriod(ka.XRow);

			if (Array.IndexOf(period, true) < 0)
			{
				// 対象年に入会期間がない
				return null;
			}

			SheetData sd = new SheetData();
			sd.Kaiin = ka;
			sd.SheetName = ka.CodeString;

			// 会費設定
			foreach (Kaihi k in ka.Kaihis)
			{
				Detail d = newDetail(k.ID, Cast.String(k.XRow.Kaihi_Name_Null), ka.GetShiharai(k), period);

				for (int m = 0; m < MonthCount; m++)
				{
					if (period[m] == true)
					{
						d.Costs[m] = k.GetCost(m + 1) ?? 0;
					}
				}

				sd.Details.Add(d);
			}

			//¶ 2026/11/16 CC 調整額は、支払方法に関係なく同じ会費の行へ合算する
			foreach (KeyValuePair<DateTime, List<t_kaiin_chosei>> kvp in ka.Choseis)
			{
				int m = kvp.Key.Month - 1;

				if (kvp.Key.Year != year || period[m] == false)
				{
					continue;
				}

				foreach (t_kaiin_chosei crow in kvp.Value)
				{
					Detail d = sd.Details.Find(x => x.ID_Kaihi == crow.ID_Kaihi);

					if (d == null)
					{
						//¶ 2026/11/16 CC 会費設定にない会費の調整額は行を追加する。扱銀行は調整額側の支払方法
						d = newDetail(crow.ID_Kaihi, getKaihiName(crow.ID_Kaihi), crow.KCS_Shiharai, period);
						sd.Details.Add(d);
					}

					d.Costs[m] = (d.Costs[m] ?? 0) + (crow.KCS_Cost_Null ?? 0);
				}
			}

			if (sd.Details.Count == 0)
			{
				return null;
			}

			return sd;
		}

		/// <summary>
		/// 徴収明細書のデータを作成します（引落データを引落口座ごとに集計）。
		/// </summary>
		/// <param name="ka">会員</param>
		/// <returns>引落口座ごとのシートデータ。対象データがない場合は0件</returns>
		List<SheetData> createMeisai(Kaiin ka)
		{
			bool[] period = getPeriod(ka.XRow);

			SortedDictionary<int, SheetData> dics = new SortedDictionary<int, SheetData>();

			DataTable dt = AppGlobal.DB.FillQuery(
				$"SELECT * FROM {TableProp.t_hikiotoshi} " +
				$"WHERE {t_hikiotoshi.FID_Kaiin} = {ka.ID} " +
				$"ORDER BY {t_hikiotoshi.FID_Hikiotoshi}");

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				t_hikiotoshi hrow = new t_hikiotoshi(dt.Rows[i]);

				DateTime? ym = hrow.Hiki_DateYM_Null;
				if (ym == null || ym.Value.Year != year)
				{
					continue;
				}

				eShiharai shiharai = hrow.Hiki_Shiharai;

				//¶ 2026/11/16 CC 口座引落（口座①～③）だけを対象にする。現金も含める場合は、この判定を変える
				if (isKoza(shiharai) == false)
				{
					continue;
				}

				int key = (int)shiharai;
				if (dics.ContainsKey(key) == false)
				{
					SheetData nsd = new SheetData();
					nsd.Kaiin = ka;
					nsd.Shiharai = shiharai;
					nsd.SheetName = $"{ka.CodeString}_{key}";
					dics.Add(key, nsd);
				}

				SheetData sd = dics[key];
				Detail d = sd.Details.Find(x => x.ID_Kaihi == hrow.ID_Kaihi);

				if (d == null)
				{
					d = newDetail(hrow.ID_Kaihi, getKaihiName(hrow.ID_Kaihi), shiharai, period);
					sd.Details.Add(d);
				}

				//¶ 2026/11/16 CC 入会期間外の月でも、引落データがあれば金額を出す
				int m = ym.Value.Month - 1;
				d.Costs[m] = (d.Costs[m] ?? 0) + (hrow.Hiki_Cost_Null ?? 0);
			}

			return new List<SheetData>(dics.Values);
		}

		/// <summary>
		/// 会員の口座情報を取得します。
		/// </summary>
		/// <param name="xrow">会員レコード</param>
		/// <param name="shiharai">支払方法（口座①～③）</param>
		Koza getKoza(t_kaiin xrow, eShiharai shiharai)
		{
			Koza koza = new Koza();

			int? cd = null;
			int? cd_shiten = null;
			int kozatype = 0;

			switch (shiharai)
			{
				case eShiharai.Koza1:
					cd = xrow.Kaiin_BankCode1_Null;
					cd_shiten = xrow.Kaiin_BankShitenCode1_Null;
					kozatype = xrow.Kaiin_BankKozaType1_Null ?? 0;
					koza.KozaNo = Cast.String(xrow.Kaiin_BankKozaNo1_Null);
					break;
				case eShiharai.Koza2:
					cd = xrow.Kaiin_BankCode2_Null;
					cd_shiten = xrow.Kaiin_BankShitenCode2_Null;
					kozatype = xrow.Kaiin_BankKozaType2_Null ?? 0;
					koza.KozaNo = Cast.String(xrow.Kaiin_BankKozaNo2_Null);
					break;
				case eShiharai.Koza3:
					cd = xrow.Kaiin_BankCode3_Null;
					cd_shiten = xrow.Kaiin_BankShitenCode3_Null;
					kozatype = xrow.Kaiin_BankKozaType3_Null ?? 0;
					koza.KozaNo = Cast.String(xrow.Kaiin_BankKozaNo3_Null);
					break;
				default:
					return koza;
			}

			t_bank_code bank = AppGlobal.BankCodeMg.GetBankCodeRow(cd, cd_shiten);
			if (bank != null)
			{
				koza.BankName = Cast.String(bank.BCD_FullName);
				koza.ShitenName = Cast.String(bank.BCD_NameShiten);
			}

			if (enumKbn.DTypeKoza.ContainsKey(kozatype) == true)
			{
				koza.KozaType = enumKbn.DTypeKoza[kozatype];
			}

			return koza;
		}

		/// <summary>
		/// シートへ書き込みます。
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="sd">シートデータ</param>
		/// <returns>明細行を足した行数</returns>
		int writeSheet(XLSheet sheet, SheetData sd)
		{
			Kaiin ka = sd.Kaiin;

			//¶ 2026/11/16 CC 入会期間中で金額がない月は0を表示する
			sheet.ShowZeros = true;

			//¶ 2026/11/16 CC 明細がテンプレートの行数を超える場合は、最終行を複製して足す（以降の行は下へずれる）
			int extra = Math.Max(0, sd.Details.Count - DetailCount);
			if (extra > 0)
			{
				insertCopyRows(sheet, layout.RowDetail + DetailCount - 1, extra);
			}

			// 宛先
			string post = Cast.String(ka.XRow.Kaiin_Post_Null);
			string addr1 = Cast.String(ka.XRow.Kaiin_Addr1_Null);
			string addr2 = Cast.String(ka.XRow.Kaiin_Addr2_Null);
			string kikanname = "";

			IryoKikan ik = AppGlobal.IryoKikans.Get(ka.XRow.ID_Iryokikan);
			if (ik != null)
			{
				kikanname = Cast.String(ik.XRow.IRK_Name_Null);

				//¶ 2026/11/16 CC 文書送付先が自宅以外なら、医療機関の郵便番号と住所を出す
				if (ka.XRow.Kaiin_BunshoSofusaki == true)
				{
					post = Cast.String(ik.XRow.IRK_Post_Null);
					addr1 = Cast.String(ik.XRow.IRK_Addr1_Null);
					addr2 = Cast.String(ik.XRow.IRK_Addr2_Null);
				}
			}

			setCellValue(sheet, RowPost, ColAtesaki, $"{layout.Indent}{post}");
			setCellValue(sheet, RowPost + 1, ColAtesaki, $"{layout.Indent}{addr1}");
			setCellValue(sheet, RowPost + 2, ColAtesaki, $"{layout.Indent}{addr2}");
			setCellValue(sheet, RowPost + 3, ColAtesaki, $"{layout.Indent}{kikanname}");
			setCellValue(sheet, RowPost + 4, ColAtesaki, $"{layout.Indent}{ka.XRow.Kaiin_Name}　様");

			// 会員コード・発行日・対象年
			setCellValue(sheet, RowCode, ColCode, $"　{ka.CodeString}");

			if (type == eType.Yotei)
			{
				setCellValue(sheet, RowDateOut, ColDateOut, $"{DateTime.Now:yyyy年MM月dd日}発行");
				setCellValue(sheet, layout.RowYear, 0, $"{year}年分");
			}
			else
			{
				setCellValue(sheet, RowDateOut, ColDateOut, $"{DateTime.Now:yyyy年MM月dd日}");
				setCellValue(sheet, layout.RowYear, 0, $"明細書作成対象期間　　　　{year}年分");
			}

			// 明細
			for (int i = 0; i < sd.Details.Count; i++)
			{
				int row = layout.RowDetail + i;
				Detail d = sd.Details[i];

				setCellValue(sheet, row, ColName, d.Name);

				for (int m = 0; m < MonthCount; m++)
				{
					//¶ 2026/11/16 CC nullの月（入会期間外）は空欄のまま
					if (d.Costs[m] != null)
					{
						setCellValue(sheet, row, ColMonth + m, d.Costs[m].Value);
					}
				}

				setCellFormula(sheet, row, ColTotal, $"SUM({getCellName(row, ColMonth)}:{getCellName(row, ColMonth + MonthCount - 1)})");

				if (type == eType.Yotei && isKoza(d.Shiharai) == true)
				{
					//¶ 2026/11/16 CC 扱銀行は口座の番号。現金などは空欄
					setCellValue(sheet, row, ColBankIdx, (int)d.Shiharai);
				}
			}

			// 合計
			int rowFirst = layout.RowDetail;
			int rowLast = layout.RowDetail + DetailCount + extra - 1;
			int rowTotal = rowLast + 1;

			for (int m = 0; m < MonthCount; m++)
			{
				int col = ColMonth + m;
				string range = $"{getCellName(rowFirst, col)}:{getCellName(rowLast, col)}";

				//¶ 2026/11/16 CC 金額が1つもない月（入会期間外）は、合計も空欄にする
				setCellFormula(sheet, rowTotal, col, $"IF(COUNT({range})=0,\"\",SUM({range}))");
			}
			setCellFormula(sheet, rowTotal, ColTotal, $"SUM({getCellName(rowFirst, ColTotal)}:{getCellName(rowLast, ColTotal)})");

			// 年間金額
			setCellFormula(sheet, layout.RowNenkan + extra, layout.ColNenkan, getCellName(rowTotal, ColTotal));

			// 銀行
			if (type == eType.Yotei)
			{
				//¶ 2026/11/16 CC 明細で使っている口座を、番号順に1行ずつ出す
				int line = 0;
				foreach (eShiharai shiharai in new eShiharai[] { eShiharai.Koza1, eShiharai.Koza2, eShiharai.Koza3 })
				{
					if (sd.Details.Exists(x => x.Shiharai == shiharai) == false)
					{
						continue;
					}

					Koza koza = getKoza(ka.XRow, shiharai);
					setCellValue(sheet, RowBankInfo + extra + line, 0,
						$"扱銀行　{(int)shiharai}. {koza.BankName} 　　 　{koza.ShitenName}　　{koza.KozaType}　{koza.KozaNo}");
					line++;
				}
			}
			else
			{
				Koza koza = getKoza(ka.XRow, sd.Shiharai);
				setCellValue(sheet, RowBankName, ColBank, $"　{koza.BankName}");
				setCellValue(sheet, RowBankShiten, ColBank, $"　{koza.ShitenName}");
			}

			return extra;
		}
	}
}
