using C1.C1Excel;
using ComponentIO;
using System;
using System.Collections.Generic;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 会費申請書類（各種医会等 入会・変更・退会 申請書）のExcel出力
	/// 書類タイプごとのテンプレートに、会員の氏名・基本会費の金額・入会中の会費の○を書き込む。
	/// </summary>
	public class AppExcelKaihiShinsei : AppExcel
	{
		/// <summary>
		/// 書類タイプ
		/// </summary>
		public enum eType
		{
			/// <summary>新入会</summary>
			Nyukai = 1,
			/// <summary>新入会（研修医）</summary>
			NyukaiKenshui = 2,
			/// <summary>変更</summary>
			Henko = 11,
			/// <summary>変更（高齢用）</summary>
			HenkoKorei = 12,
			/// <summary>変更（4条C）</summary>
			Henko4joC = 13,
		}

		/// <summary>1年の月数</summary>
		const int MonthCount = 12;
		/// <summary>入会中の行に付ける印</summary>
		const string Maru = "○";

		//¶ 2026/11/16 CC テンプレート上の位置（0始まり）。全書類タイプで共通のもの
		/// <summary>宛名の列</summary>
		const int ColAtena = 0;
		/// <summary>組グループ会費の名称の列</summary>
		const int ColGroupName = 1;
		/// <summary>金額の列（基本会費は年額、医会等は月額）</summary>
		const int ColCost = 2;
		/// <summary>○の列（新入会は「入会希望」、変更は「現在」）</summary>
		const int ColMaru = 3;

		//¶ 2026/11/16 CC 基本会費の行に対応する会費コード。属性は日医・県医・市医の3つしかなく、行は会費コードで決める
		/// <summary>標準（日医4・県医6・市医3）</summary>
		static readonly int[] CodesStandard =
		{
			12000, 13000, 13001, 15000,
			21000, 22000, 23000, 23001, 25000, 25001,
			31001, 32001, 35001,
		};

		/// <summary>研修医（日医6・県医8・市医4）</summary>
		static readonly int[] CodesKenshui =
		{
			12000, 13000, 13001, 15000, 14001, 16001,
			21000, 22000, 23000, 23001, 25000, 25001, 24001, 26001,
			31001, 32001, 35001, 35121,
		};

		/// <summary>高齢用（日医3・県医4・市医4）</summary>
		static readonly int[] CodesKorei =
		{
			12010, 13010, 15010,
			21010, 22010, 23010, 25010,
			31001, 32011, 35011, 35111,
		};

		/// <summary>4条C（標準＋市医4条C）</summary>
		static readonly int[] Codes4joC =
		{
			12000, 13000, 13001, 15000,
			21000, 22000, 23000, 23001, 25000, 25001,
			31001, 32001, 35001, 35101,
		};

		/// <summary>高齢用で、年額ではなく月額（1月の月額）を出す会費コード（市医の4行）</summary>
		static readonly int[] CodesKoreiMonthly = { 31001, 32011, 35011, 35111 };

		//¶ 2026/11/16 CC 各医会の行（テンプレートの並び順）に対応する会費属性。金額はテンプレートの固定値を使う
		static readonly eKaihiZokusei[][] IkaiZokuseis =
		{
			new eKaihiZokusei[] { eKaihiZokusei.GakkoShii, eKaihiZokusei.GakkoKeni },	// 10 学校・保育園医部会費
			new eKaihiZokusei[] { eKaihiZokusei.Sangyo },		// 11 産業医部会費
			new eKaihiZokusei[] { eKaihiZokusei.Naika },		// 20 内科医会費
			new eKaihiZokusei[] { eKaihiZokusei.Shonika },		// 21 小児科医会費
			new eKaihiZokusei[] { eKaihiZokusei.GekaRinsho },	// 22 臨床外科医会費
			new eKaihiZokusei[] { eKaihiZokusei.GekaNoshin },	// 23 脳神経外科医会費
			new eKaihiZokusei[] { eKaihiZokusei.GekaSeikei },	// 24 整形外科医会費
			new eKaihiZokusei[] { eKaihiZokusei.Sanfujinka },	// 25 産婦人科医会費
			new eKaihiZokusei[] { eKaihiZokusei.Hifuka },		// 26 皮膚科医会費
			new eKaihiZokusei[] { eKaihiZokusei.Ganka },		// 27 眼科医会費
			new eKaihiZokusei[] { eKaihiZokusei.Jibika },		// 28 耳鼻科医会費
			new eKaihiZokusei[] { eKaihiZokusei.Hinyokika },	// 29 泌尿器科医会費
			new eKaihiZokusei[] { eKaihiZokusei.Sports },		// 40 健康スポーツ医部会費
			new eKaihiZokusei[] { eKaihiZokusei.Shokakika },	// 41 消化器病医会費
			new eKaihiZokusei[] { eKaihiZokusei.Josei },		// 42 女性医部会
			new eKaihiZokusei[] { eKaihiZokusei.HoikuKeni },	// 50 県医保育園医部会費
			new eKaihiZokusei[] { eKaihiZokusei.Jidosha },		// 51 神奈川県医療用自動車協会費
			new eKaihiZokusei[] { eKaihiZokusei.Kokyuki },		// 60 呼吸器・循環器研究会費
			new eKaihiZokusei[] { eKaihiZokusei.NaikaTaiken },	// 61 内科体験学習費
			new eKaihiZokusei[] { eKaihiZokusei.NaikaRinsho },	// 62 日本臨床内科医会費
		};

		/// <summary>俳句会会費の行に対応する会費属性</summary>
		static readonly eKaihiZokusei[] HaikuZokuseis = { eKaihiZokusei.Aoiku };
		/// <summary>将棋部会費の行に対応する会費属性</summary>
		static readonly eKaihiZokusei[] ShogiZokuseis = { eKaihiZokusei.Shogi };
		/// <summary>組グループ会費の行に対応する会費属性</summary>
		static readonly eKaihiZokusei[] GroupZokuseis =
		{
			eKaihiZokusei.Group1, eKaihiZokusei.Group2, eKaihiZokusei.Group3, eKaihiZokusei.Group4,
			eKaihiZokusei.Group5, eKaihiZokusei.Group5Gojo, eKaihiZokusei.Group6, eKaihiZokusei.Group7,
			eKaihiZokusei.Group8, eKaihiZokusei.Group9, eKaihiZokusei.Group10, eKaihiZokusei.KumiGroup,
		};

		/// <summary>
		/// 書類タイプごとのテンプレート上の位置（0始まり）。ない項目は-1
		/// </summary>
		class Layout
		{
			/// <summary>テンプレートファイル名</summary>
			public string TemplateFile;
			/// <summary>書類名（ファイル名に使う）</summary>
			public string Title;
			/// <summary>宛名の行</summary>
			public int RowAtena = -1;
			/// <summary>基本会費の先頭行</summary>
			public int RowKihon;
			/// <summary>基本会費の行に対応する会費コード</summary>
			public int[] KihonCodes;
			/// <summary>年額ではなく月額を出す会費コード</summary>
			public int[] MonthlyCodes = new int[0];
			/// <summary>各医会の先頭行</summary>
			public int RowIkai;
			/// <summary>俳句会会費の行</summary>
			public int RowHaiku = -1;
			/// <summary>将棋部会費の行</summary>
			public int RowShogi = -1;
			/// <summary>組グループ会費の行</summary>
			public int RowGroup = -1;
			/// <summary>組グループ会費の行に、入会中のグループ会費の名称と月額を書くかどうか</summary>
			public bool WriteGroup = false;
		}

		static readonly Dictionary<eType, Layout> Layouts = new Dictionary<eType, Layout>()
		{
			{
				eType.Nyukai, new Layout()
				{
					TemplateFile = "KaihiShinseiNyukai.xls",
					Title = "各種医会等申請書（新入会）",
					RowAtena = 0,
					RowKihon = 9,
					KihonCodes = CodesStandard,
					RowIkai = 24,
					RowGroup = 44,
				}
			},
			{
				//¶ 2026/11/16 CC 研修医用は、元のひな形に宛名と組グループ会費の行がない
				eType.NyukaiKenshui, new Layout()
				{
					TemplateFile = "KaihiShinseiNyukaiKenshui.xls",
					Title = "各種医会等申請書（新入会・研修医）",
					RowKihon = 8,
					KihonCodes = CodesKenshui,
					RowIkai = 28,
				}
			},
			{
				eType.Henko, new Layout()
				{
					TemplateFile = "KaihiShinseiHenko.xls",
					Title = "各種医会等申請書（変更）",
					RowAtena = 0,
					RowKihon = 8,
					KihonCodes = CodesStandard,
					RowIkai = 23,
					RowHaiku = 43,
					RowShogi = 44,
					RowGroup = 45,
					WriteGroup = true,
				}
			},
			{
				eType.HenkoKorei, new Layout()
				{
					TemplateFile = "KaihiShinseiHenkoKorei.xls",
					Title = "各種医会等申請書（変更・高齢用）",
					RowAtena = 0,
					RowKihon = 8,
					KihonCodes = CodesKorei,
					MonthlyCodes = CodesKoreiMonthly,
					RowIkai = 21,
					RowHaiku = 41,
					RowShogi = 42,
					RowGroup = 43,
					WriteGroup = true,
				}
			},
			{
				eType.Henko4joC, new Layout()
				{
					TemplateFile = "KaihiShinseiHenko4joC.xls",
					Title = "各種医会等申請書（変更・4条C）",
					RowAtena = 0,
					RowKihon = 8,
					KihonCodes = Codes4joC,
					RowIkai = 24,
					RowHaiku = 44,
					RowShogi = 45,
					RowGroup = 46,
					WriteGroup = true,
				}
			},
		};

		Kaiin kaiin;
		Layout layout;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="type">書類タイプ</param>
		/// <param name="kaiin">出力する会員</param>
		public AppExcelKaihiShinsei(eType type, Kaiin kaiin)
		{
			this.kaiin = kaiin;
			this.layout = Layouts[type];
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
				if (kaiin == null)
				{
					return $"{layout.Title}.xls";
				}

				string name = Cast.String(kaiin.XRow.Kaiin_Name);

				//¶ 2026/11/16 CC ファイル名に使えない文字は置き換える
				foreach (char c in System.IO.Path.GetInvalidFileNameChars())
				{
					name = name.Replace(c, '_');
				}

				return $"{layout.Title}_{kaiin.CodeString}{name}.xls";
			}
		}

		/// <summary>
		/// 前回保存したファイルパス
		/// </summary>
		protected override string SavePath
		{
			get { return Properties.Settings.Default.ExcelKaihiShinseiSavePath; }
			set { Properties.Settings.Default.ExcelKaihiShinseiSavePath = value; }
		}

		/// <summary>
		/// ブックへ書き込みます。
		/// </summary>
		protected override eResult writeBook()
		{
			if (kaiin == null)
			{
				return eResult.NotFoundData;
			}

			XLSheet sheet = book.Sheets[0];

			if (kaiin.CodeString != "")
			{
				sheet.Name = kaiin.CodeString;
			}

			// 宛名
			if (layout.RowAtena >= 0)
			{
				setCellValue(sheet, layout.RowAtena, ColAtena, $"{kaiin.XRow.Kaiin_Name}　先生");
			}

			// 基本会費の金額
			for (int i = 0; i < layout.KihonCodes.Length; i++)
			{
				Kaihi k = AppGlobal.Kaihis.GetCode(layout.KihonCodes[i]);

				//¶ 2026/11/16 CC 会費マスターにないコードの行は、テンプレートの金額のままにする
				if (k == null)
				{
					continue;
				}

				setCellValue(sheet, layout.RowKihon + i, ColCost, getKihonCost(k));
			}

			//¶ 2026/11/16 CC 入会中の会費の行に○を付ける。変更希望・退会希望は手入力なので書かない
			foreach (Kaihi k in kaiin.Kaihis)
			{
				if (k.XRow.Kaihi_KaihiType == eTypeKaihi.Ishikai)
				{
					//¶ 2026/11/16 CC 基本会費がこの書式の行にない場合は、○を付けずに出力する
					int idx = Array.IndexOf(layout.KihonCodes, k.CD);
					if (idx >= 0)
					{
						setCellValue(sheet, layout.RowKihon + idx, ColMaru, Maru);
					}
				}
				else if (k.XRow.Kaihi_KaihiType == eTypeKaihi.Ikai)
				{
					writeIkai(sheet, k);
				}
			}

			return eResult.Success;
		}

		/// <summary>
		/// 基本会費の行に出す金額を取得します。
		/// </summary>
		/// <param name="k">会費</param>
		/// <returns>年額（1月～12月の計）。月額で出す会費は1月の月額</returns>
		decimal getKihonCost(Kaihi k)
		{
			if (Array.IndexOf(layout.MonthlyCodes, k.CD) >= 0)
			{
				return k.GetCost(1) ?? 0;
			}

			decimal total = 0;
			for (int m = 1; m <= MonthCount; m++)
			{
				total += k.GetCost(m) ?? 0;
			}

			return total;
		}

		/// <summary>
		/// 医会等会費の○を、会費属性が一致する行へ書き込みます。
		/// </summary>
		/// <param name="sheet">シートオブジェクト</param>
		/// <param name="k">会員が入会中の医会等会費</param>
		void writeIkai(XLSheet sheet, Kaihi k)
		{
			eKaihiZokusei zokusei = k.XRow.Kaihi_Zokusei;

			for (int i = 0; i < IkaiZokuseis.Length; i++)
			{
				if (Array.IndexOf(IkaiZokuseis[i], zokusei) >= 0)
				{
					setCellValue(sheet, layout.RowIkai + i, ColMaru, Maru);
					return;
				}
			}

			if (layout.RowHaiku >= 0 && Array.IndexOf(HaikuZokuseis, zokusei) >= 0)
			{
				setCellValue(sheet, layout.RowHaiku, ColMaru, Maru);
				return;
			}

			if (layout.RowShogi >= 0 && Array.IndexOf(ShogiZokuseis, zokusei) >= 0)
			{
				setCellValue(sheet, layout.RowShogi, ColMaru, Maru);
				return;
			}

			if (layout.RowGroup >= 0 && Array.IndexOf(GroupZokuseis, zokusei) >= 0)
			{
				setCellValue(sheet, layout.RowGroup, ColMaru, Maru);

				//¶ 2026/11/16 CC 変更の書式は、入会中のグループ会費（会員ごとに1つ）の名称と1月の月額を出す
				if (layout.WriteGroup == true)
				{
					setCellValue(sheet, layout.RowGroup, ColGroupName, Cast.String(k.XRow.Kaihi_Name_Null));
					setCellValue(sheet, layout.RowGroup, ColCost, k.GetCost(1) ?? 0);
				}
			}
		}
	}
}
