using ComponentDebug;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 会費振込依頼書（医師信用組合）のExcel出力
	/// 引落データを会費属性ごと・月ごとに集計し、その年の1月から指定した月までを1つのファイルに出力する。
	/// 送金通知のプルダウン（入力規則）を残すため、C1Excelを使わずにxlsxを直接書き換える。
	/// </summary>
	public class AppExcelFurikomiIrai
	{
		/// <summary>テンプレートファイル名</summary>
		const string TemplateFile = "FurikomiIrai.xlsx";
		/// <summary>保存用拡張子</summary>
		const string XLSX_EXT = "Excelファイル|*.xlsx";

		/// <summary>振込依頼書のシート名</summary>
		const string SheetIrai = "振込依頼書";
		/// <summary>送金通知のシート名</summary>
		const string SheetTsuchi = "送金通知";

		/// <summary>処理日（送金指定日）の初期値にする日（月分の翌月）</summary>
		const int ShoribiDay = 15;

		//¶ 2026/11/16 CC テンプレート上の位置（0始まり）
		/// <summary>送金指定日の行</summary>
		const int RowShoribi = 2;
		/// <summary>見出しの行</summary>
		const int RowHeader = 3;
		/// <summary>明細の先頭行（会費属性1の行）。行は会費属性の区分値の順に並んでいる</summary>
		const int RowDetail = 4;
		/// <summary>明細の行数（会費属性1～40）</summary>
		const int DetailCount = 40;
		/// <summary>合計の行</summary>
		const int RowTotal = 44;
		/// <summary>前年12月分の列</summary>
		const int ColPrevDec = 7;
		/// <summary>年合計の列</summary>
		const int ColYearTotal = 20;
		/// <summary>手数料の列</summary>
		const int ColFee = 21;
		/// <summary>前月比の列</summary>
		const int ColZengetsu = 23;
		/// <summary>行のない会費の引落合計を出す列（印刷範囲外）</summary>
		const int ColEtc = 24;

		/// <summary>送金通知の日付の行</summary>
		const int RowTsuchiDate = 1;
		/// <summary>送金通知の日付の列</summary>
		const int ColTsuchiDate = 6;
		/// <summary>月の列数（前年12月分～12月分）</summary>
		const int MonthColCount = 13;

		//¶ 2026/11/16 CC 行は会費属性ごとだが、ここに挙げた属性は右側の属性の行に合算する（総合振込と同じ扱い）
		//¶ 2026/11/16 CC 県医保育園医部会は、このシートに専用の行があるので合算しない（総合振込とは違う）
		/// <summary>合算する会費属性（合算元 → 合算先）</summary>
		static readonly Dictionary<eKaihiZokusei, eKaihiZokusei> MergeZokusei = new Dictionary<eKaihiZokusei, eKaihiZokusei>()
		{
			{ eKaihiZokusei.Group5Gojo, eKaihiZokusei.Group5 },
		};

		/// <summary>
		/// 1か月分の集計
		/// </summary>
		class Month
		{
			/// <summary>月分（月の初日）</summary>
			public DateTime YM;
			/// <summary>出力する列</summary>
			public int Col;
			/// <summary>引落データが1件でもあるか（支払方法は問わない）</summary>
			public bool HasData;
			/// <summary>会費属性ごとの振込金額（添字は会費属性の区分値。0は使わない）</summary>
			public decimal[] Costs = new decimal[DetailCount + 1];
			/// <summary>シートに行のない会費属性の振込金額</summary>
			public decimal EtcCost;
		}

		DateTime ym;
		DateTime shoribi;

		/// <summary>
		/// 保存したファイルパス
		/// </summary>
		public string FilePath { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="ym">月分（月の初日）。この月の年の1月から、この月までを出力する</param>
		/// <param name="shoribi">処理日（月分の送金指定日）</param>
		public AppExcelFurikomiIrai(DateTime ym, DateTime shoribi)
		{
			this.ym = AppDate.ParseDateFirstDay(ym);
			this.shoribi = shoribi.Date;
		}

		/// <summary>
		/// 月分から、処理日（送金指定日）の初期値を取得します。
		/// </summary>
		/// <param name="ym">月分</param>
		public static DateTime GetDefaultShoribi(DateTime ym)
		{
			//¶ 2026/11/16 CC 月分の翌月15日。土日なら次の月曜にする。祝日は判定できないので手で直す（総合振込と同じ）
			DateTime next = AppDate.ParseDateFirstDay(ym).AddMonths(1);
			DateTime date = new DateTime(next.Year, next.Month, ShoribiDay);

			if (date.DayOfWeek == DayOfWeek.Saturday)
			{
				date = date.AddDays(2);
			}
			else if (date.DayOfWeek == DayOfWeek.Sunday)
			{
				date = date.AddDays(1);
			}

			return date;
		}

		/// <summary>
		/// Excelファイルを出力します。
		/// </summary>
		public AppExcel.eResult Execute()
		{
			string template = $"{AppConst.ExcelFolder}{TemplateFile}";

			if (File.Exists(template) == false)
			{
				return AppExcel.eResult.NotExistTemplate;
			}

			string temp = null;

			try
			{
				List<Month> months = createMonths();

				//¶ 2026/11/16 CC 指定した月の引落データがなければ出力しない
				if (months[months.Count - 1].HasData == false)
				{
					return AppExcel.eResult.NotFoundData;
				}

				string path = showSaveFilePath();
				if (path == null)
				{
					return AppExcel.eResult.Cancel;
				}

				//¶ 2026/11/16 CC 書き込みの途中で失敗しても保存先に壊れたファイルを残さないように、一時ファイルで作ってからコピーする
				temp = Path.GetTempFileName();
				File.Copy(template, temp, true);
				//¶ 2026/11/16 CC テンプレートが読み取り専用でも書き込めるようにする
				File.SetAttributes(temp, FileAttributes.Normal);

				using (AppXlsx xlsx = new AppXlsx(temp))
				{
					writeIrai(xlsx, months);
					writeTsuchi(xlsx);
					xlsx.Save();
				}

				try
				{
					File.Copy(temp, path, true);
				}
				catch (IOException ex)
				{
					//¶ 2026/11/16 CC 保存先のファイルがExcelで開かれている場合など
					ErrLog.WriteException(ex);
					return AppExcel.eResult.CannotFileSave;
				}
				catch (UnauthorizedAccessException ex)
				{
					ErrLog.WriteException(ex);
					return AppExcel.eResult.CannotFileSave;
				}

				FilePath = path;

				//¶ 2026/11/16 CC 次回の初期フォルダ用に保存先を保持する
				Properties.Settings.Default.ExcelFurikomiIraiSavePath = path;
				Properties.Settings.Default.Save();

				return AppExcel.eResult.Success;
			}
			catch (Exception ex)
			{
				ErrLog.WriteException(ex);
				return AppExcel.eResult.Error;
			}
			finally
			{
				if (temp != null && File.Exists(temp) == true)
				{
					try
					{
						File.Delete(temp);
					}
					catch (IOException)
					{
						//¶ 2026/11/16 CC 一時ファイルが消せなくても出力結果には影響しない
					}
				}
			}
		}

		/// <summary>
		/// 口座引落（口座①～③）かどうかを取得します。
		/// </summary>
		bool isKoza(eShiharai shiharai)
		{
			return (shiharai == eShiharai.Koza1 || shiharai == eShiharai.Koza2 || shiharai == eShiharai.Koza3);
		}

		/// <summary>
		/// 月ごとの集計を作成します（前年12月から指定した月まで）。
		/// </summary>
		/// <returns>月の順に並べた集計。最後が指定した月</returns>
		List<Month> createMonths()
		{
			List<Month> months = new List<Month>();
			Dictionary<DateTime, Month> dics = new Dictionary<DateTime, Month>();

			//¶ 2026/11/16 CC 1月分の前月比を出すために、前年12月分から集計する
			DateTime start = new DateTime(ym.Year, 1, 1).AddMonths(-1);
			for (DateTime d = start; d <= ym; d = d.AddMonths(1))
			{
				Month m = new Month();
				m.YM = d;
				m.Col = ColPrevDec + months.Count;
				months.Add(m);
				dics.Add(d, m);
			}

			DataTable dt = AppGlobal.DB.FillQuery(
				$"SELECT * FROM {TableProp.t_hikiotoshi} " +
				$"WHERE {t_hikiotoshi.FHiki_DateYM} >= #{start}# AND {t_hikiotoshi.FHiki_DateYM} <= #{ym}# " +
				$"ORDER BY {t_hikiotoshi.FID_Hikiotoshi}");

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				t_hikiotoshi hrow = new t_hikiotoshi(dt.Rows[i]);

				DateTime key = AppDate.ParseDateFirstDay(hrow.Hiki_DateYM);
				if (dics.ContainsKey(key) == false)
				{
					continue;
				}

				Month m = dics[key];
				m.HasData = true;

				//¶ 2026/11/16 CC 口座引落（口座①～③）だけを対象にする。現金は含めない（総合振込と同じ）
				if (isKoza(hrow.Hiki_Shiharai) == false)
				{
					continue;
				}

				Kaihi kaihi = AppGlobal.Kaihis.Get(hrow.ID_Kaihi);
				eKaihiZokusei zokusei = (kaihi == null) ? eKaihiZokusei.None : kaihi.XRow.Kaihi_Zokusei;
				if (MergeZokusei.ContainsKey(zokusei) == true)
				{
					zokusei = MergeZokusei[zokusei];
				}

				int no = (int)zokusei;
				if (no >= 1 && no <= DetailCount)
				{
					m.Costs[no] += hrow.Hiki_Cost;
				}
				else
				{
					//¶ 2026/11/16 CC 組グループ・その他・属性なし・会費マスターにない会費は、シートに行がない
					m.EtcCost += hrow.Hiki_Cost;
				}
			}

			return months;
		}

		/// <summary>
		/// 会費属性ごとの振込先の銀行コードを取得します。
		/// </summary>
		/// <returns>添字は会費属性の区分値。振込先が未設定の属性は0</returns>
		int[] getBankCodes()
		{
			int[] codes = new int[DetailCount + 1];

			//¶ 2026/11/16 CC 同じ属性に会費が複数あるときは、振込先が設定されている会費のうち会費コードが最小のものを使う
			DataTable dt = AppGlobal.DB.FillQuery(
				$"SELECT * FROM {TableProp.t_kaihi} ORDER BY {t_kaihi.FCD_Kaihi}");

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				t_kaihi krow = new t_kaihi(dt.Rows[i]);

				int no = (int)krow.Kaihi_Zokusei;
				if (no < 1 || no > DetailCount)
				{
					continue;
				}

				if (codes[no] == 0 && krow.Kaihi_BankCode != 0)
				{
					codes[no] = krow.Kaihi_BankCode;
				}
			}

			return codes;
		}

		/// <summary>
		/// 振込依頼書シートへ書き込みます。
		/// </summary>
		void writeIrai(AppXlsx xlsx, List<Month> months)
		{
			Month cur = months[months.Count - 1];
			Month prev = months[months.Count - 2];

			//¶ 2026/11/16 CC 年合計の見出し
			xlsx.SetString(SheetIrai, RowHeader, ColYearTotal, $"振込金額\n（{ym.Year}年合計）");

			//¶ 2026/11/17 CC 月の見出し（前年12月分～12月分）。データのない月も含めてすべての列に書く
			//¶ 2026/11/17 CC 送金通知の「yyyy年n月分」は、この見出しの括弧の中を数式で取り出している。形式を変えるときはテンプレートの数式も直す
			DateTime first = new DateTime(ym.Year, 1, 1).AddMonths(-1);
			for (int i = 0; i < MonthColCount; i++)
			{
				DateTime d = first.AddMonths(i);
				xlsx.SetString(SheetIrai, RowHeader, ColPrevDec + i, $"振込金額\n（{d.Year}年{d.Month}月分）");
			}

			foreach (Month m in months)
			{
				//¶ 2026/11/16 CC 送金指定日は、指定した月だけ画面の処理日を使い、それより前の月は自動計算する
				DateTime date = (m == cur) ? shoribi : GetDefaultShoribi(m.YM);
				xlsx.SetDate(SheetIrai, RowShoribi, m.Col, date);

				//¶ 2026/11/16 CC 引落データが1件もない月は金額を空欄のままにする（システムの運用開始前の月を手で入れられるように）
				if (m.HasData == false)
				{
					continue;
				}

				for (int no = 1; no <= DetailCount; no++)
				{
					xlsx.SetNumber(SheetIrai, RowDetail + no - 1, m.Col, m.Costs[no]);
				}
			}

			//¶ 2026/11/16 CC 手数料
			int[] banks = getBankCodes();
			for (int no = 1; no <= DetailCount; no++)
			{
				xlsx.SetNumber(SheetIrai, RowDetail + no - 1, ColFee, getFee(banks[no], cur.Costs[no]));
			}

			//¶ 2026/11/16 CC 前月比（明細と合計の行）
			string curCol = AppXlsx.GetColumnName(cur.Col);
			string prevCol = AppXlsx.GetColumnName(prev.Col);
			for (int row = RowDetail; row <= RowTotal; row++)
			{
				xlsx.SetFormula(SheetIrai, row, ColZengetsu, $"{curCol}{row + 1}-{prevCol}{row + 1}");
			}

			//¶ 2026/11/16 CC シートに行のない会費の引落は、印刷範囲の外に合計を出す（合計が総合振込と合わないときに気づけるようにする）
			if (cur.EtcCost != 0)
			{
				xlsx.SetString(SheetIrai, RowHeader, ColEtc, $"行のない会費の引落（{ym.Month}月分）");
				xlsx.SetNumber(SheetIrai, RowDetail, ColEtc, cur.EtcCost);
			}
		}

		/// <summary>
		/// 手数料を取得します。
		/// </summary>
		/// <param name="bankCode">振込先の銀行コード（未設定は0）</param>
		/// <param name="cost">指定した月の振込金額</param>
		int getFee(int bankCode, decimal cost)
		{
			//¶ 2026/11/16 CC 総合振込の手数料の合計と合うように、指定した月に振込のある行だけに入れる
			//¶ 2026/11/16 CC 医師信用組合への振込は手数料なし。金額は総合振込の定数を使う
			if (cost == 0 || bankCode == 0 || bankCode == AppExcelFurikomi.BankCodeIshishin)
			{
				return 0;
			}

			return AppExcelFurikomi.FeeOtherBank;
		}

		/// <summary>
		/// 送金通知シートへ書き込みます。
		/// </summary>
		void writeTsuchi(AppXlsx xlsx)
		{
			//¶ 2026/11/16 CC 振込金額は、この日付と振込依頼書の送金指定日を突き合わせる数式で引いている。同じ値を入れる
			//¶ 2026/11/17 CC 「yyyy年n月分」も同じ突き合わせで振込依頼書の見出しから引く数式なので、ここでは書かない
			xlsx.SetDate(SheetTsuchi, RowTsuchiDate, ColTsuchiDate, shoribi);
		}

		/// <summary>
		/// 保存先のファイルパスを取得します。
		/// </summary>
		/// <returns>キャンセル時はnull</returns>
		string showSaveFilePath()
		{
			//¶ 2026/11/16 CC はじめに表示されるフォルダ
			string inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			//¶ 2026/11/16 CC ファイル名は年で変わるため、前回の保存先からはフォルダだけを使う
			string savepath = Cast.String(Properties.Settings.Default.ExcelFurikomiIraiSavePath).Trim();
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
					//¶ 2026/11/16 CC 保持していたパスが不正な場合はデスクトップを使う
				}
			}

			using (SaveFileDialog fd = new SaveFileDialog())
			{
				fd.InitialDirectory = inipath;
				fd.FileName = $"{AppDate.GetYearWareki(ym, true)}会費振込依頼書（医師信組）.xlsx";
				fd.Filter = XLSX_EXT;
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
	}
}
