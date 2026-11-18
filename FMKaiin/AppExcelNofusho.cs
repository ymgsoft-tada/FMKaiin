using C1.C1Excel;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 会費納付書（日医・県医・市医）のExcel出力
	/// 引落データを会員区分（会費）ごとに集計して、指定した月のシートを出力する。
	/// </summary>
	public class AppExcelNofusho : AppExcel
	{
		//¶ 2026/11/16 CC テンプレートのシート名
		/// <summary>日医のシート名</summary>
		const string SheetNichii = "Nichii";
		/// <summary>県医のシート名</summary>
		const string SheetKeni = "Keni";
		/// <summary>市医のシート名</summary>
		const string SheetShii = "Shii";

		//¶ 2026/11/16 CC テンプレート上の位置（0始まり）。3シートとも同じ
		/// <summary>月分の行</summary>
		const int RowYM = 2;
		/// <summary>月分の列</summary>
		const int ColYM = 0;
		/// <summary>金額（合計の参照）・処理日の行</summary>
		const int RowHead = 3;
		/// <summary>金額（合計の参照）の列</summary>
		const int ColHeadTotal = 2;
		/// <summary>処理日の列</summary>
		const int ColShoribi = 4;
		/// <summary>明細の先頭行</summary>
		const int RowDetail = 5;
		/// <summary>テンプレートに用意してある明細の行数（この下に空行が1行あり、その次が合計行）</summary>
		const int DetailCount = 40;
		/// <summary>会員区分の列</summary>
		const int ColName = 0;
		/// <summary>会費の列</summary>
		const int ColCost = 1;
		/// <summary>会員数の列</summary>
		const int ColCount = 2;
		/// <summary>金額の列</summary>
		const int ColAmount = 3;
		/// <summary>前月比（人数）の列（印刷範囲外）</summary>
		const int ColZengetsu = 6;
		/// <summary>テンプレートに用意してある脚注の行数</summary>
		const int FootCount = 12;

		/// <summary>未入会の会費コードの下4桁</summary>
		const int CodeMinyukai = 9999;

		/// <summary>
		/// 会員区分1行分のデータ
		/// </summary>
		class Kubun
		{
			/// <summary>会費</summary>
			public Kaihi Kaihi;
			/// <summary>会員数</summary>
			public int Count;
			/// <summary>前月の会員数</summary>
			public int PrevCount;
			/// <summary>金額（調整額込み）</summary>
			public decimal Amount;
		}

		/// <summary>
		/// 調整金1行分のデータ（同じ名称・同じ金額でまとめたもの）
		/// </summary>
		class Chosei
		{
			/// <summary>名称（医会名を除いた会費名）</summary>
			public string Name = "";
			/// <summary>1件あたりの金額</summary>
			public decimal Cost;
			/// <summary>まとめた会費のうち最小の会費コード</summary>
			public int MinCD;
			/// <summary>対象の会員ID（引落データの件数分）</summary>
			public List<int> KaiinIDs = new List<int>();
			/// <summary>行の見出し（調整金①など）</summary>
			public string Label = "";
		}

		/// <summary>
		/// 1か月分の集計結果
		/// </summary>
		class Monthly
		{
			/// <summary>その月の引落データがあるか（医会を問わない）</summary>
			public bool HasData;
			/// <summary>対象の医会の引落データがあるか</summary>
			public bool HasIkaiData;
			/// <summary>会員区分ごとの集計（キーは会費ID）</summary>
			public Dictionary<int, Kubun> Kubuns = new Dictionary<int, Kubun>();
			/// <summary>調整金の集計</summary>
			public List<Chosei> Choseis = new List<Chosei>();
		}

		DateTime ym;
		DateTime shoribi;
		eTypeIkai ikai;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="ym">月分（月の初日）</param>
		/// <param name="shoribi">処理日</param>
		/// <param name="ikai">医会区分（日医・県医・市医）</param>
		public AppExcelNofusho(DateTime ym, DateTime shoribi, eTypeIkai ikai)
		{
			this.ym = AppDate.ParseDateFirstDay(ym);
			this.shoribi = shoribi.Date;
			this.ikai = ikai;
		}

		/// <summary>
		/// テンプレートファイル名
		/// </summary>
		protected override string TemplateFile
		{
			get { return "Nofusho.xls"; }
		}

		/// <summary>
		/// 保存ダイアログに初期表示するファイル名
		/// </summary>
		protected override string DefaultFileName
		{
			get { return $"{getYMText()}分 {getIkaiName()}会費納付書.xls"; }
		}

		/// <summary>
		/// 前回保存したファイルパス
		/// </summary>
		protected override string SavePath
		{
			get { return Properties.Settings.Default.ExcelNofushoSavePath; }
			set { Properties.Settings.Default.ExcelNofushoSavePath = value; }
		}

		/// <summary>
		/// 月分の表記（2026年9月）を取得します。
		/// </summary>
		string getYMText()
		{
			return $"{ym.Year}年{ym.Month}月";
		}

		/// <summary>
		/// 医会の名称（会費名の先頭に付いている名称）を取得します。
		/// </summary>
		string getIkaiName()
		{
			switch (ikai)
			{
				case eTypeIkai.Nichii: return "日医";
				case eTypeIkai.Keni: return "県医";
				default: return "市医";
			}
		}

		/// <summary>
		/// 医会区分に対応するシート名を取得します。
		/// </summary>
		string getSheetName(eTypeIkai type)
		{
			switch (type)
			{
				case eTypeIkai.Nichii: return SheetNichii;
				case eTypeIkai.Keni: return SheetKeni;
				default: return SheetShii;
			}
		}

		/// <summary>
		/// 医会区分に対応する会費属性を取得します。
		/// </summary>
		eKaihiZokusei getZokusei()
		{
			switch (ikai)
			{
				case eTypeIkai.Nichii: return eKaihiZokusei.Nichii;
				case eTypeIkai.Keni: return eKaihiZokusei.Keni;
				default: return eKaihiZokusei.Shii;
			}
		}

		/// <summary>
		/// 合計行から脚注の先頭行までの行数を取得します。
		/// </summary>
		int getFootOffset()
		{
			//¶ 2026/11/16 CC 市医は合計の3行下に「藤沢市医師会のみ在籍」の欄があるので、その下から使う
			return (ikai == eTypeIkai.Shii) ? 5 : 3;
		}

		/// <summary>
		/// 脚注のうち、使わなくても表示したままにする行数を取得します。
		/// </summary>
		int getFootVisible()
		{
			//¶ 2026/11/16 CC 現行のExcelの印刷範囲に入っていた行数。これを超える行は、使わなければ非表示にする
			switch (ikai)
			{
				case eTypeIkai.Nichii: return 7;
				case eTypeIkai.Keni: return 5;
				default: return 6;
			}
		}

		/// <summary>
		/// データを抽出し、ブックへ書き込みます。
		/// </summary>
		protected override eResult writeBook()
		{
			Monthly cur = aggregate(ym);
			if (cur.HasIkaiData == false)
			{
				return eResult.NotFoundData;
			}

			//¶ 2026/11/16 CC 前月比は前月の引落データを同じ方法で数えて求める
			Monthly prev = aggregate(ym.AddMonths(-1));

			List<Kubun> kubuns = createKubunList(cur, prev);
			setChoseiLabel(cur.Choseis);

			writeSheet(book.Sheets[getSheetName(ikai)], kubuns, cur.Choseis, prev.HasData);

			//¶ 2026/11/16 CC 医会ごとに別ファイルにするため、出力しない医会のシートを消す
			foreach (eTypeIkai type in new eTypeIkai[] { eTypeIkai.Nichii, eTypeIkai.Keni, eTypeIkai.Shii })
			{
				if (type != ikai)
				{
					book.Sheets.Remove(getSheetName(type));
				}
			}

			book.Sheets[0].Name = getYMText();

			return eResult.Success;
		}

		/// <summary>
		/// 指定した月の引落データを、対象の医会について集計します。
		/// </summary>
		/// <param name="month">月分（月の初日）</param>
		Monthly aggregate(DateTime month)
		{
			Monthly ret = new Monthly();
			eKaihiZokusei zokusei = getZokusei();
			string prefix = getIkaiName();

			DataTable dt = AppGlobal.DB.FillQuery(
				$"SELECT * FROM {TableProp.t_hikiotoshi} " +
				$"WHERE {t_hikiotoshi.FHiki_DateYM} = #{month}# " +
				$"ORDER BY {t_hikiotoshi.FID_Hikiotoshi}");

			ret.HasData = (dt.Rows.Count > 0);

			// 会員区分の会費の引落金額（キーは 会員ID_会費ID）
			Dictionary<string, List<decimal>> costs = new Dictionary<string, List<decimal>>();
			Dictionary<string, Chosei> dicChosei = new Dictionary<string, Chosei>();

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				t_hikiotoshi hrow = new t_hikiotoshi(dt.Rows[i]);

				//¶ 2026/11/16 CC 支払方法は問わない（現金も含める）
				Kaihi kaihi = AppGlobal.Kaihis.Get(hrow.ID_Kaihi);
				if (kaihi == null || kaihi.XRow.Kaihi_Zokusei != zokusei)
				{
					continue;
				}

				ret.HasIkaiData = true;

				if (kaihi.XRow.Kaihi_KaihiType == eTypeKaihi.Ishikai)
				{
					// 会員区分の会費
					Kubun k;
					if (ret.Kubuns.ContainsKey(kaihi.ID) == true)
					{
						k = ret.Kubuns[kaihi.ID];
					}
					else
					{
						k = new Kubun();
						k.Kaihi = kaihi;
						ret.Kubuns.Add(kaihi.ID, k);
					}

					k.Amount += hrow.Hiki_Cost;

					string key = $"{hrow.ID_Kaiin}_{kaihi.ID}";
					if (costs.ContainsKey(key) == false)
					{
						costs.Add(key, new List<decimal>());
					}
					costs[key].Add(hrow.Hiki_Cost);
				}
				else
				{
					//¶ 2026/11/16 CC 医師会会費以外（日医調整金、県医会費返金額など）は、名称と金額ごとにまとめて別の行にする
					if (hrow.Hiki_Cost == 0)
					{
						continue;
					}

					string name = trimIkaiName(Cast.String(kaihi.XRow.Kaihi_Name), prefix);
					string key = $"{name}_{hrow.Hiki_Cost}";

					Chosei c;
					if (dicChosei.ContainsKey(key) == true)
					{
						c = dicChosei[key];
					}
					else
					{
						c = new Chosei();
						c.Name = name;
						c.Cost = hrow.Hiki_Cost;
						c.MinCD = int.MaxValue;
						dicChosei.Add(key, c);
					}

					c.MinCD = Math.Min(c.MinCD, kaihi.CD);
					c.KaiinIDs.Add(hrow.ID_Kaiin);
				}
			}

			//¶ 2026/11/16 CC 引落データは調整金額と通常の会費を区別しない。調整金額の登録と同じ金額の行を1件ずつ除き、
			//¶ 2026/11/16 CC 残った行がある会員だけを会員数に数える（区分を移った会員の旧区分に調整額だけがある場合に数えないため）
			DataTable dtc = AppGlobal.DB.FillQuery(
				$"SELECT * FROM {TableProp.t_kaiin_chosei} " +
				$"WHERE {t_kaiin_chosei.FKCS_DateYM} = #{month}#");

			for (int i = 0; i < dtc.Rows.Count; i++)
			{
				t_kaiin_chosei crow = new t_kaiin_chosei(dtc.Rows[i]);

				string key = $"{crow.ID_Kaiin}_{crow.ID_Kaihi}";
				if (costs.ContainsKey(key) == true)
				{
					costs[key].Remove(crow.KCS_Cost);
				}
			}

			foreach (KeyValuePair<string, List<decimal>> pair in costs)
			{
				if (pair.Value.Count == 0)
				{
					continue;
				}

				int kaihiID = Cast.Int(pair.Key.Substring(pair.Key.IndexOf('_') + 1));
				ret.Kubuns[kaihiID].Count++;
			}

			ret.Choseis.AddRange(dicChosei.Values);
			ret.Choseis.Sort((a, b) =>
			{
				int c = a.MinCD.CompareTo(b.MinCD);
				return (c != 0) ? c : a.Cost.CompareTo(b.Cost);
			});

			return ret;
		}

		/// <summary>
		/// 会費名の先頭から医会の名称を除きます。
		/// </summary>
		string trimIkaiName(string name, string prefix)
		{
			name = name.Trim();

			if (name.StartsWith(prefix) == true && name.Length > prefix.Length)
			{
				return name.Substring(prefix.Length);
			}

			return name;
		}

		/// <summary>
		/// 出力する会員区分の一覧を作成します（会費コード順）。
		/// </summary>
		/// <param name="cur">指定月の集計</param>
		/// <param name="prev">前月の集計</param>
		List<Kubun> createKubunList(Monthly cur, Monthly prev)
		{
			List<Kubun> list = new List<Kubun>();

			foreach (Kubun k in cur.Kubuns.Values)
			{
				if (prev.Kubuns.ContainsKey(k.Kaihi.ID) == true)
				{
					k.PrevCount = prev.Kubuns[k.Kaihi.ID].Count;
				}
				list.Add(k);
			}

			//¶ 2026/11/16 CC 前月だけに会員がいた区分も、前月比を出すために載せる
			foreach (Kubun p in prev.Kubuns.Values)
			{
				if (cur.Kubuns.ContainsKey(p.Kaihi.ID) == false)
				{
					Kubun k = new Kubun();
					k.Kaihi = p.Kaihi;
					k.PrevCount = p.Count;
					list.Add(k);
				}
			}

			list.RemoveAll(k =>
			{
				// 当月も前月も会員がなく、金額もない区分は出さない
				if (k.Count == 0 && k.PrevCount == 0 && k.Amount == 0)
				{
					return true;
				}
				//¶ 2026/11/16 CC 未入会は出さない。金額がある場合だけは、合計を引落データと合わせるために出す
				return (k.Kaihi.CD % 10000 == CodeMinyukai && k.Amount == 0);
			});

			list.Sort((a, b) => a.Kaihi.CD.CompareTo(b.Kaihi.CD));

			return list;
		}

		/// <summary>
		/// 調整金の行の見出し（調整金①など）をセットします。番号は名称ごとに振ります。
		/// </summary>
		void setChoseiLabel(List<Chosei> list)
		{
			Dictionary<string, int> nos = new Dictionary<string, int>();

			foreach (Chosei c in list)
			{
				int no = (nos.ContainsKey(c.Name) == true) ? nos[c.Name] + 1 : 1;
				nos[c.Name] = no;

				//¶ 2026/11/16 CC 丸数字は⑳まで。それより後は(21)の形にする
				string mark = (no <= 20) ? ((char)('①' + no - 1)).ToString() : $"({no})";
				c.Label = $"{c.Name}{mark}";
			}
		}

		/// <summary>
		/// シートへ書き込みます。
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="kubuns">会員区分の明細</param>
		/// <param name="choseis">調整金の明細</param>
		/// <param name="hasPrev">前月の引落データがあるか</param>
		void writeSheet(XLSheet sheet, List<Kubun> kubuns, List<Chosei> choseis, bool hasPrev)
		{
			string prefix = getIkaiName();
			int count = kubuns.Count + choseis.Count;
			// 合計行（明細の下に空行を1行はさむ）
			int rowTotal = RowDetail + DetailCount + 1;

			if (count > DetailCount)
			{
				//¶ 2026/11/16 CC テンプレートの行数を超えた分は、最終明細行を複製して足す。合計行と脚注は下へずれる
				int add = count - DetailCount;
				insertCopyRows(sheet, RowDetail + DetailCount - 1, add);
				rowTotal += add;
			}

			setCellValue(sheet, RowYM, ColYM, $"　{getYMText()}分");
			setCellValue(sheet, RowHead, ColShoribi, shoribi);

			// 会員区分
			for (int i = 0; i < kubuns.Count; i++)
			{
				int row = RowDetail + i;
				Kubun k = kubuns[i];

				setCellValue(sheet, row, ColName, $"　{trimIkaiName(Cast.String(k.Kaihi.XRow.Kaihi_Name), prefix)}");
				setCellValue(sheet, row, ColCost, Cast.Decimal(k.Kaihi.GetCost(ym)));
				setCellValue(sheet, row, ColCount, k.Count);
				//¶ 2026/11/16 CC 金額は会費×会員数ではなく、引落金額の合計（調整額を含む）
				setCellValue(sheet, row, ColAmount, k.Amount);

				//¶ 2026/11/16 CC 前月の引落データがないときは、前月比を出さない
				if (hasPrev == true)
				{
					setCellValue(sheet, row, ColZengetsu, k.Count - k.PrevCount);
				}
			}

			// 調整金
			for (int i = 0; i < choseis.Count; i++)
			{
				int row = RowDetail + kubuns.Count + i;
				Chosei c = choseis[i];

				setCellValue(sheet, row, ColName, c.Label);
				setCellValue(sheet, row, ColCost, c.Cost);
				setCellValue(sheet, row, ColCount, c.KaiinIDs.Count);
				setCellValue(sheet, row, ColAmount, c.Cost * c.KaiinIDs.Count);
			}

			//¶ 2026/11/16 CC 使わない明細行は非表示にする（現行のExcelも会員のいない区分を非表示にしている）
			for (int i = count; i < DetailCount; i++)
			{
				sheet.Rows[RowDetail + i].Visible = false;
			}

			// 合計
			//¶ 2026/11/16 CC 会員数の合計は会員区分の行だけ（調整金の人数は含めない）
			if (kubuns.Count > 0)
			{
				setCellFormula(sheet, rowTotal, ColCount, $"SUM({getCellName(RowDetail, ColCount)}:{getCellName(RowDetail + kubuns.Count - 1, ColCount)})");
			}
			else
			{
				setCellValue(sheet, rowTotal, ColCount, 0);
			}
			setCellFormula(sheet, rowTotal, ColAmount, $"SUM({getCellName(RowDetail, ColAmount)}:{getCellName(rowTotal - 1, ColAmount)})");
			setCellFormula(sheet, RowHead, ColHeadTotal, getCellName(rowTotal, ColAmount));

			// 脚注（調整金の内訳）
			int rowFoot = rowTotal + getFootOffset();
			for (int i = 0; i < choseis.Count; i++)
			{
				setCellValue(sheet, rowFoot + i, ColName, getFootText(choseis[i]));
			}

			//¶ 2026/11/16 CC 脚注の行は多めに用意してある。現行の行数を超える分は、使わなければ非表示にする
			//¶ 2026/11/16 CC 最後の行は外枠の下の罫線を持つので、常に表示したままにする
			for (int i = Math.Max(choseis.Count, getFootVisible() - 1); i < FootCount - 1; i++)
			{
				sheet.Rows[rowFoot + i].Visible = false;
			}
		}

		/// <summary>
		/// 調整金の脚注（調整金①：2,500円×1名分（○○先生））を取得します。
		/// </summary>
		string getFootText(Chosei c)
		{
			List<Kaiin> kaiins = new List<Kaiin>();
			int unknown = 0;

			foreach (int id in c.KaiinIDs)
			{
				Kaiin ka = AppGlobal.Kaiins.Get(id);
				if (ka == null)
				{
					unknown++;
				}
				else
				{
					kaiins.Add(ka);
				}
			}

			kaiins.Sort((a, b) => a.CD.CompareTo(b.CD));

			List<string> names = new List<string>();
			foreach (Kaiin ka in kaiins)
			{
				names.Add($"{Cast.String(ka.XRow.Kaiin_Name).Trim()}先生");
			}
			//¶ 2026/11/16 CC 会員情報にない会員は、人数だけ出す
			if (unknown > 0)
			{
				names.Add($"会員情報なし{unknown}名");
			}

			return $"　{c.Label}：{c.Cost:#,0}円×{c.KaiinIDs.Count}名分（{string.Join("、", names)}）";
		}
	}
}
