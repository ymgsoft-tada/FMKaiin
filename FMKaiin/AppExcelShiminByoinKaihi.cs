using C1.C1Excel;
using ComponentDB;
using ComponentIO;
using System;
using System.Collections.Generic;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 市民病院会費請求（別添資料）のExcel出力
	/// </summary>
	public class AppExcelShiminByoinKaihi : AppExcel
	{
		/// <summary>藤沢市民病院の組コード</summary>
		const int KumiCodeShiminByoin = 11;

		/// <summary>テンプレートのシート名（県医）</summary>
		const string SheetKeni = "県医";
		/// <summary>テンプレートのシート名（市医）</summary>
		const string SheetShii = "市医";

		//¶ 2026/11/14 CC テンプレート上の位置（0始まり）
		/// <summary>年度の行</summary>
		const int RowNendo = 2;
		/// <summary>見出しの行</summary>
		const int RowHead = 3;
		/// <summary>明細の先頭行</summary>
		const int RowDetail = 4;
		/// <summary>会員名の列</summary>
		const int ColName = 0;
		/// <summary>1か月目の列</summary>
		const int ColMonth = 1;
		/// <summary>期計の列</summary>
		const int ColTotal = 5;

		/// <summary>1期あたりの月数</summary>
		const int MonthCount = 4;

		/// <summary>期の名称</summary>
		static readonly Dictionary<int, string> PeriodNames = new Dictionary<int, string>()
		{
			{ 1, "Ⅰ期" },
			{ 2, "Ⅱ期" },
			{ 3, "Ⅲ期" },
		};

		/// <summary>期に含まれる月（年度は4月始まり）</summary>
		static readonly Dictionary<int, int[]> PeriodMonths = new Dictionary<int, int[]>()
		{
			{ 1, new int[] { 4, 5, 6, 7 } },
			{ 2, new int[] { 8, 9, 10, 11 } },
			{ 3, new int[] { 12, 1, 2, 3 } },
		};

		/// <summary>
		/// 明細1行分のデータ
		/// </summary>
		class Detail
		{
			/// <summary>会員名</summary>
			public string Name;
			/// <summary>月ごとの金額。在籍していない月はnull</summary>
			public decimal?[] Costs = new decimal?[MonthCount];
		}

		int year;
		int period;
		eTypeIkai ikai;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="year">年度（西暦）</param>
		/// <param name="period">期（1～3）</param>
		/// <param name="ikai">医会区分（県医または市医）</param>
		public AppExcelShiminByoinKaihi(int year, int period, eTypeIkai ikai)
		{
			this.year = year;
			this.period = period;
			this.ikai = ikai;
		}

		/// <summary>
		/// テンプレートファイル名
		/// </summary>
		protected override string TemplateFile
		{
			get { return "ShiminByoinKaihi.xls"; }
		}

		/// <summary>
		/// 保存ダイアログに初期表示するファイル名
		/// </summary>
		protected override string DefaultFileName
		{
			get { return $"{year}年度{PeriodNames[period]}_市民病院会費請求（{getSheetName(ikai)}）.xls"; }
		}

		/// <summary>
		/// 前回保存したファイルパス
		/// </summary>
		protected override string SavePath
		{
			get { return Properties.Settings.Default.ExcelShiminByoinSavePath; }
			set { Properties.Settings.Default.ExcelShiminByoinSavePath = value; }
		}

		/// <summary>
		/// データを抽出し、ブックへ書き込みます。
		/// </summary>
		protected override eResult writeBook()
		{
			if (PeriodMonths.ContainsKey(period) == false)
			{
				return eResult.Error;
			}

			List<Detail> list = createDetails();
			if (list.Count == 0)
			{
				return eResult.NotFoundData;
			}

			writeSheet(book.Sheets[getSheetName(ikai)], list);

			//¶ 2026/11/14 CC 県医と市医は別ファイルにするため、出力しない側のシートを消す
			//¶ 2026/11/14 CC 1ファイルに統一する場合は、writeSheetを医会ごとに呼び、この削除をやめる
			eTypeIkai other = (ikai == eTypeIkai.Keni) ? eTypeIkai.Shii : eTypeIkai.Keni;
			book.Sheets.Remove(getSheetName(other));

			return eResult.Success;
		}

		/// <summary>
		/// 医会区分に対応するシート名を取得します。
		/// </summary>
		string getSheetName(eTypeIkai type)
		{
			return (type == eTypeIkai.Keni) ? SheetKeni : SheetShii;
		}

		/// <summary>
		/// 期のn番目の月の初日を取得します。
		/// </summary>
		/// <param name="index">期の中での順番（0始まり）</param>
		DateTime getMonthFirstDay(int index)
		{
			int month = PeriodMonths[period][index];
			//¶ 2026/11/14 CC 1～3月は翌年
			int y = (month >= 4) ? year : year + 1;

			return AppDate.ParseDateFirstDay(y, month);
		}

		/// <summary>
		/// 明細データを作成します。
		/// </summary>
		List<Detail> createDetails()
		{
			List<Detail> list = new List<Detail>();

			//¶ 2026/11/14 CC 医療機関は組コードのID(ID_KumiCode)を持つため、コードからIDを引く
			KumiCode kumi = AppGlobal.KumiCodes.GetCode(KumiCodeShiminByoin);
			if (kumi == null)
			{
				return list;
			}

			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin));
			dv.SortQuery($"{t_kaiin.FCD_Kaiin}");

			for (int i = 0; i < dv.Count; i++)
			{
				t_kaiin xrow = new t_kaiin(dv[i]);

				// 所属医療機関が藤沢市民病院の会員だけを対象にする
				IryoKikan ik = AppGlobal.IryoKikans.Get(xrow.ID_Iryokikan);
				if (ik == null || ik.XRow.IRK_KumiCode != kumi.ID)
				{
					continue;
				}

				DateTime? nyukai = xrow.Kaiin_DateNyukai_Null;
				DateTime? taikai = xrow.Kaiin_DateTaikai_Null;

				//¶ 2026/11/14 CC 在籍以外で退会日がない会員は、在籍期間を判定できないので対象外
				if (xrow.Kaiin_TypeZaiseki != eTypeZaiseki.Zaiseki && taikai == null)
				{
					continue;
				}

				Kaiin ka = AppGlobal.Kaiins.Get(xrow.ID_Kaiin);

				Detail d = new Detail();
				d.Name = xrow.Kaiin_Name;
				bool zaiseki = false;

				for (int m = 0; m < MonthCount; m++)
				{
					DateTime first = getMonthFirstDay(m);
					DateTime last = AppDate.ParseDateLastDay(first);

					//¶ 2026/11/14 CC 入会月と退会月は在籍として扱う
					if ((nyukai != null && nyukai.Value.Date > last) ||
						(taikai != null && taikai.Value.Date < first))
					{
						continue;
					}

					zaiseki = true;
					d.Costs[m] = getCost(ka, first);
				}

				// 期間内に1か月でも在籍していれば出力する
				if (zaiseki == true)
				{
					list.Add(d);
				}
			}

			return list;
		}

		/// <summary>
		/// 指定月の請求額を取得します。
		/// 対象の医会区分で支払方法が現金の会費だけを集計し、それ以外は0とします。
		/// </summary>
		/// <param name="ka">会員</param>
		/// <param name="ym">対象月の初日</param>
		decimal getCost(Kaiin ka, DateTime ym)
		{
			decimal cost = 0;

			if (ka == null)
			{
				return cost;
			}

			// 対象の医会区分の会費ID
			List<int> ids = new List<int>();

			foreach (Kaihi k in ka.Kaihis)
			{
				if (k.LinkRow == null || k.LinkRow.KaihiTypeIkai != ikai)
				{
					continue;
				}

				ids.Add(k.ID);

				if (k.LinkRow.KaihiShiharai == eShiharai.Genkin)
				{
					cost += k.GetCost(ym) ?? 0;
				}
			}

			//¶ 2026/11/14 CC 調整金額は、調整側の支払方法が現金のものを加える（引落データ作成と同じ扱い）
			if (ka.Choseis.ContainsKey(ym))
			{
				foreach (t_kaiin_chosei crow in ka.Choseis[ym])
				{
					if (ids.Contains(crow.ID_Kaihi) == true && crow.KCS_Shiharai == eShiharai.Genkin)
					{
						cost += crow.KCS_Cost_Null ?? 0;
					}
				}
			}

			return cost;
		}

		/// <summary>
		/// シートへ書き込みます。
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="list">明細データ</param>
		void writeSheet(XLSheet sheet, List<Detail> list)
		{
			// 年度・見出し
			setCellValue(sheet, RowNendo, ColName, $"{year}年度");
			for (int m = 0; m < MonthCount; m++)
			{
				setCellValue(sheet, RowHead, ColMonth + m, $"{PeriodMonths[period][m]}月");
			}
			setCellValue(sheet, RowHead, ColTotal, $"{PeriodNames[period]}計");

			//¶ 2026/11/14 CC テンプレートの明細は1行。不足分を複製して挿入する（合計行は下へずれる）
			insertCopyRows(sheet, RowDetail, list.Count - 1);

			// 明細
			for (int i = 0; i < list.Count; i++)
			{
				int row = RowDetail + i;
				Detail d = list[i];

				setCellValue(sheet, row, ColName, d.Name);

				for (int m = 0; m < MonthCount; m++)
				{
					if (d.Costs[m] == null)
					{
						// 在籍していない月
						setCellValue(sheet, row, ColMonth + m, "-");
					}
					else
					{
						setCellValue(sheet, row, ColMonth + m, d.Costs[m].Value);
					}
				}

				setCellFormula(sheet, row, ColTotal, $"SUM({getCellName(row, ColMonth)}:{getCellName(row, ColMonth + MonthCount - 1)})");
			}

			// 合計
			int rowTotal = RowDetail + list.Count;
			for (int col = ColMonth; col <= ColTotal; col++)
			{
				setCellFormula(sheet, rowTotal, col, $"SUM({getCellName(RowDetail, col)}:{getCellName(rowTotal - 1, col)})");
			}
		}
	}
}
