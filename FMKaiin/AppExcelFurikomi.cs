using C1.C1Excel;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 総合振込（医師信用組合への振込依頼）のExcel出力
	/// 引落データを振込先ごとに集計して出力する。
	/// </summary>
	public class AppExcelFurikomi : AppExcel
	{
		/// <summary>神奈川県医師信用組合の銀行コード</summary>
		public const int BankCodeIshishin = 2304;
		/// <summary>医師信用組合以外の銀行へ振り込むときの手数料</summary>
		public const int FeeOtherBank = 200;

		//¶ 2026/11/16 CC テンプレート上の位置（0始まり）
		/// <summary>処理日の行</summary>
		const int RowShoribi = 0;
		/// <summary>処理日の列</summary>
		const int ColShoribi = 8;
		/// <summary>明細の先頭行</summary>
		const int RowDetail = 3;
		/// <summary>テンプレートに用意してある明細の行数</summary>
		const int DetailCount = 60;
		/// <summary>会費属性順の番号の列（印刷範囲外）</summary>
		const int ColZokusei = 0;
		/// <summary>フリガナ順の連番の列</summary>
		const int ColNo = 1;
		/// <summary>金融機関名の列</summary>
		const int ColBank = 2;
		/// <summary>支店名の列</summary>
		const int ColShiten = 3;
		/// <summary>口座種類の列</summary>
		const int ColKozaType = 4;
		/// <summary>口座番号の列</summary>
		const int ColKozaNo = 5;
		/// <summary>口座名義の列</summary>
		const int ColName = 6;
		/// <summary>名義フリガナの列</summary>
		const int ColKana = 7;
		/// <summary>振込額の列</summary>
		const int ColCost = 8;
		/// <summary>手数料の列</summary>
		const int ColFee = 9;
		/// <summary>会費属性の名称の列（印刷範囲外）</summary>
		const int ColZokuseiName = 10;

		//¶ 2026/11/16 CC 行は会費属性ごとに分けるが、ここに挙げた属性は右側の属性の行に合算する
		//¶ 2026/11/16 CC 合算する組が増えたら、ここに足す
		/// <summary>合算する会費属性（合算元 → 合算先）</summary>
		static readonly Dictionary<eKaihiZokusei, eKaihiZokusei> MergeZokusei = new Dictionary<eKaihiZokusei, eKaihiZokusei>()
		{
			{ eKaihiZokusei.Group5Gojo, eKaihiZokusei.Group5 },
			{ eKaihiZokusei.HoikuKeni, eKaihiZokusei.GakkoKeni },
		};

		//¶ 2026/11/16 CC 現行のExcelの並びに合わせる。この属性はフリガナ順に入れず、末尾にこの順で並べる
		/// <summary>フリガナ順の後ろに並べる会費属性</summary>
		static readonly eKaihiZokusei[] TailZokusei = new eKaihiZokusei[]
		{
			eKaihiZokusei.Keni,
			eKaihiZokusei.Shii,
			eKaihiZokusei.GakkoShii,
			eKaihiZokusei.GakkoKeni,
		};

		/// <summary>
		/// 明細1行分のデータ
		/// </summary>
		class Detail
		{
			/// <summary>会費属性（合算後）</summary>
			public eKaihiZokusei Zokusei;
			/// <summary>振込先口座が設定されているか</summary>
			public bool HasKoza;
			/// <summary>銀行コード</summary>
			public int BankCode;
			/// <summary>支店コード</summary>
			public int ShitenCode;
			/// <summary>口座種別</summary>
			public eTypeKoza KozaType;
			/// <summary>口座番号</summary>
			public string KozaNo = "";
			/// <summary>口座名義</summary>
			public string Name = "";
			/// <summary>名義フリガナ</summary>
			public string Kana = "";
			/// <summary>名義を採った会費の会費コード</summary>
			public int NameKaihiCD;
			/// <summary>振込額</summary>
			public decimal Cost;
		}

		DateTime ym;
		DateTime shoribi;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="ym">月分（月の初日）</param>
		/// <param name="shoribi">処理日</param>
		public AppExcelFurikomi(DateTime ym, DateTime shoribi)
		{
			this.ym = AppDate.ParseDateFirstDay(ym);
			this.shoribi = shoribi.Date;
		}

		/// <summary>
		/// テンプレートファイル名
		/// </summary>
		protected override string TemplateFile
		{
			get { return "Furikomi.xls"; }
		}

		/// <summary>
		/// 保存ダイアログに初期表示するファイル名
		/// </summary>
		protected override string DefaultFileName
		{
			get { return $"{ym.Year}({AppDate.GetYearWareki(ym, true)}) {ym.Month}月 総合振込.xls"; }
		}

		/// <summary>
		/// 前回保存したファイルパス
		/// </summary>
		protected override string SavePath
		{
			get { return Properties.Settings.Default.ExcelFurikomiSavePath; }
			set { Properties.Settings.Default.ExcelFurikomiSavePath = value; }
		}

		/// <summary>
		/// データを抽出し、ブックへ書き込みます。
		/// </summary>
		protected override eResult writeBook()
		{
			List<Detail> list = createDetails();
			if (list.Count == 0)
			{
				return eResult.NotFoundData;
			}

			writeSheet(book.Sheets[0], list);

			return eResult.Success;
		}

		/// <summary>
		/// 口座引落（口座①～③）かどうかを取得します。
		/// </summary>
		bool isKoza(eShiharai shiharai)
		{
			return (shiharai == eShiharai.Koza1 || shiharai == eShiharai.Koza2 || shiharai == eShiharai.Koza3);
		}

		/// <summary>
		/// 明細データを作成します（引落データを会費属性と振込先口座ごとに集計）。
		/// </summary>
		/// <returns>出力順に並べた明細。対象データがない場合は0件</returns>
		List<Detail> createDetails()
		{
			Dictionary<string, Detail> dics = new Dictionary<string, Detail>();

			DataTable dt = AppGlobal.DB.FillQuery(
				$"SELECT * FROM {TableProp.t_hikiotoshi} " +
				$"WHERE {t_hikiotoshi.FHiki_DateYM} = #{ym}# " +
				$"ORDER BY {t_hikiotoshi.FID_Hikiotoshi}");

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				t_hikiotoshi hrow = new t_hikiotoshi(dt.Rows[i]);

				//¶ 2026/11/16 CC 口座引落（口座①～③）だけを対象にする。現金は含めない
				if (isKoza(hrow.Hiki_Shiharai) == false)
				{
					continue;
				}

				Kaihi kaihi = AppGlobal.Kaihis.Get(hrow.ID_Kaihi);
				t_kaihi krow = (kaihi == null) ? null : kaihi.XRow;

				//¶ 2026/11/16 CC 会費マスターにない会費は、属性なし・口座未設定として集計する（金額を落とさない）
				eKaihiZokusei zokusei = (krow == null) ? eKaihiZokusei.None : krow.Kaihi_Zokusei;
				if (MergeZokusei.ContainsKey(zokusei) == true)
				{
					zokusei = MergeZokusei[zokusei];
				}

				int bankcd = (krow == null) ? 0 : krow.Kaihi_BankCode;
				int shitencd = (krow == null) ? 0 : krow.Kaihi_BankCodeShiten;
				eTypeKoza kozatype = (krow == null) ? eTypeKoza.None : krow.Kaihi_BankKozaType;
				//¶ 2026/11/16 CC 口座番号は先頭の0の有無で別の口座にならないように、0を除いて比べる
				string kozano = (krow == null) ? "" : Cast.String(krow.Kaihi_BankKozaNo).Trim().TrimStart('0');
				bool haskoza = (bankcd != 0 && kozano != "");

				//¶ 2026/11/16 CC 名義は表記ゆれがあるので、集計キーに入れない
				string key = (haskoza == true)
					? $"{(int)zokusei}_{bankcd}_{shitencd}_{(int)kozatype}_{kozano}"
					: $"{(int)zokusei}_";

				Detail d;
				if (dics.ContainsKey(key) == true)
				{
					d = dics[key];
				}
				else
				{
					d = new Detail();
					d.Zokusei = zokusei;
					d.HasKoza = haskoza;
					d.NameKaihiCD = int.MaxValue;
					if (haskoza == true)
					{
						d.BankCode = bankcd;
						d.ShitenCode = shitencd;
						d.KozaType = kozatype;
						d.KozaNo = kozano;
					}
					dics.Add(key, d);
				}

				//¶ 2026/11/16 CC 名義は、同じ行にまとめた会費のうち会費コードが最小のものを使う
				if (haskoza == true && krow.CD_Kaihi < d.NameKaihiCD)
				{
					d.NameKaihiCD = krow.CD_Kaihi;
					d.Name = Cast.String(krow.Kaihi_BankKozaName);
					d.Kana = Cast.String(krow.Kaihi_BankKozaNameKana);
				}

				d.Cost += hrow.Hiki_Cost;
			}

			// フリガナ順に並べる行、末尾に並べる行、口座未設定の行に分ける
			List<Detail> main = new List<Detail>();
			List<Detail> tail = new List<Detail>();
			List<Detail> nokoza = new List<Detail>();

			foreach (Detail d in dics.Values)
			{
				// 金額のない振込先は出力しない
				if (d.Cost == 0)
				{
					continue;
				}

				if (d.HasKoza == false)
				{
					nokoza.Add(d);
				}
				else if (Array.IndexOf(TailZokusei, d.Zokusei) >= 0)
				{
					tail.Add(d);
				}
				else
				{
					main.Add(d);
				}
			}

			//¶ 2026/11/16 CC 現行のExcelと同じく、半角カナの文字コード順（スペースが先、濁点は別の文字）
			main.Sort((a, b) =>
			{
				int c = string.CompareOrdinal(a.Kana, b.Kana);
				return (c != 0) ? c : compareZokusei(a, b);
			});
			tail.Sort((a, b) =>
			{
				int c = Array.IndexOf(TailZokusei, a.Zokusei).CompareTo(Array.IndexOf(TailZokusei, b.Zokusei));
				return (c != 0) ? c : string.CompareOrdinal(a.Kana, b.Kana);
			});
			nokoza.Sort(compareZokusei);

			List<Detail> list = new List<Detail>();
			list.AddRange(main);
			list.AddRange(tail);
			//¶ 2026/11/16 CC 振込先口座が未設定の会費は、口座の欄を空にして末尾に出す（合計を引落データと合わせ、漏れに気づけるようにする）
			list.AddRange(nokoza);

			return list;
		}

		/// <summary>
		/// 会費属性の順で比較します。
		/// </summary>
		int compareZokusei(Detail a, Detail b)
		{
			return ((int)a.Zokusei).CompareTo((int)b.Zokusei);
		}

		/// <summary>
		/// シートへ書き込みます。
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="list">明細データ</param>
		void writeSheet(XLSheet sheet, List<Detail> list)
		{
			setCellValue(sheet, RowShoribi, ColShoribi, shoribi);

			if (list.Count > DetailCount)
			{
				//¶ 2026/11/16 CC テンプレートの行数を超えた分は、最終明細行を複製して足す
				//¶ 2026/11/16 CC C1Excelは行を挿入しても数式と図の位置を直さない。合計は書き直すが、押印欄の図は元の位置に残る
				int add = list.Count - DetailCount;
				insertCopyRows(sheet, RowDetail + DetailCount - 1, add);

				// 合計（明細の下の空行までを範囲にする）
				int rowTotal = RowDetail + list.Count + 1;
				setCellFormula(sheet, rowTotal, ColCost, $"SUM({getCellName(RowDetail, ColCost)}:{getCellName(rowTotal - 1, ColCost)})");
				setCellFormula(sheet, rowTotal, ColFee, $"SUM({getCellName(RowDetail, ColFee)}:{getCellName(rowTotal - 1, ColFee)})");
			}

			// 明細
			for (int i = 0; i < list.Count; i++)
			{
				int row = RowDetail + i;
				Detail d = list[i];

				setCellValue(sheet, row, ColZokusei, (int)d.Zokusei);
				setCellValue(sheet, row, ColNo, i + 1);

				if (d.HasKoza == true)
				{
					t_bank_code bank = AppGlobal.BankCodeMg.GetBankCodeRow(d.BankCode, d.ShitenCode);
					if (bank != null)
					{
						setCellValue(sheet, row, ColBank, Cast.String(bank.BCD_FullName));
						setCellValue(sheet, row, ColShiten, Cast.String(bank.BCD_NameShiten));
					}

					if (enumKbn.DTypeKoza.ContainsKey((int)d.KozaType) == true)
					{
						setCellValue(sheet, row, ColKozaType, enumKbn.DTypeKoza[(int)d.KozaType]);
					}

					//¶ 2026/11/16 CC 口座番号はテンプレートの表示形式（7桁）を使うため、数値で入れる
					decimal no;
					if (decimal.TryParse(d.KozaNo, out no) == true)
					{
						setCellValue(sheet, row, ColKozaNo, no);
					}
					else
					{
						setCellValue(sheet, row, ColKozaNo, d.KozaNo);
					}

					setCellValue(sheet, row, ColName, d.Name);
					setCellValue(sheet, row, ColKana, d.Kana);
				}

				setCellValue(sheet, row, ColCost, d.Cost);
				setCellValue(sheet, row, ColFee, getFee(d));

				if (enumKbn.DKaihiZokusei.ContainsKey((int)d.Zokusei) == true)
				{
					setCellValue(sheet, row, ColZokuseiName, enumKbn.DKaihiZokusei[(int)d.Zokusei]);
				}
			}

			//¶ 2026/11/16 CC 使わない明細行は非表示にする（現行のExcelと同じ扱い。行を削除すると図の位置がずれる）
			for (int i = list.Count; i < DetailCount; i++)
			{
				sheet.Rows[RowDetail + i].Visible = false;
			}
		}

		/// <summary>
		/// 手数料を取得します。
		/// </summary>
		int getFee(Detail d)
		{
			// 医師信用組合への振込は手数料なし（支店は問わない）。口座未設定の行も0にする
			if (d.HasKoza == false || d.BankCode == BankCodeIshishin)
			{
				return 0;
			}

			return FeeOtherBank;
		}
	}
}
