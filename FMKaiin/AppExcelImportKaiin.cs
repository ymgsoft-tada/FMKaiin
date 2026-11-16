using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 会員情報のExcel取込
	/// 会員コードをキーに、あれば上書き・なければ追加する。Excelにない会員は変更しない。
	/// 会員本体のほか、医師会会費（日医・県医・市医の3レコード）・担当診療科・所属学会・異動情報を登録する。
	/// 参加医会（医会等会費）はExcelにないため取り込まない（別のExcelから取り込む予定）。
	/// </summary>
	public class AppExcelImportKaiin : AppExcelImport
	{
		#region *** Excelの見出し名 ***
		// 半角カナ・全角数字は元ファイルの表記のまま。
		// 同じ見出しの2つ目以降は、基底クラスが「見出し#2」のように連番を付ける。
		const string COL_Code = "会員ｺｰﾄﾞ";
		const string COL_Zaiseki = "在籍区分";
		const string COL_DateNyukai = "入会年月日";
		const string COL_DateIdo = "異動年月日";
		const string COL_DateTaikai = "退会年月日";
		const string COL_KaihiNichii = "日医会員区分";
		const string COL_KaihiKeni = "県医会員区分";
		const string COL_KaihiShii = "市医会員区分";
		const string COL_ZenKenIName = "前県医名";
		const string COL_IsekiNo = "医籍登録番号";
		const string COL_KanaSei = "姓ｶﾅ";
		const string COL_KanaMei = "名ｶﾅ";
		const string COL_NameSei = "姓漢字";
		const string COL_NameMei = "名漢字";
		const string COL_OldKanaSei = "旧姓ｶﾅ";
		const string COL_OldKanaMei = "旧名ｶﾅ";
		const string COL_OldNameSei = "旧姓漢字";
		const string COL_OldNameMei = "旧名漢字";
		const string COL_DateBirth = "生年月日";
		const string COL_Sex = "性別";
		// 異動（チェック／選択肢／記述）
		const string COL_IdoIsekiHenko = "医籍登録番号変更";
		const string COL_IdoNameHenko = "氏名変更";
		const string COL_IdoKaigyo = "施設開業";
		const string COL_IdoShisetsu = "施設異動";
		const string COL_IdoShisetsuSel = "施設異動#2";
		const string COL_IdoShisetsuMemo = "施設異動#3";
		const string COL_IdoKaiin = "会員区分変更";
		const string COL_IdoKaiinSel = "会員区分変更#2";
		const string COL_IdoKaiinMemo = "会員区分変更#3";
		const string COL_IdoShozai = "施設所在地変更";
		const string COL_IdoShozaiSel = "施設所在地変更#2";
		const string COL_IdoShozaiMemo = "施設所在地変更#3";
		const string COL_IdoAddress = "現住所変更";
		const string COL_IdoAddressSel = "現住所変更#2";
		const string COL_IdoAddressMemo = "現住所変更#3";
		const string COL_IdoEtc = "その他";
		const string COL_IdoEtcSel = "その他#2";
		const string COL_IdoEtcMemo = "その他#3";
		// 退会
		const string COL_TaikaiShibo = "死亡";
		const string COL_TaikaiHaiki = "廃棄・退職";
		const string COL_TaikaiIdo = "医師会の異動";
		const string COL_TaikaiIdosaki = "移動先";
		const string COL_TaikaiEtc = "その他#4";
		const string COL_TaikaiEtcMemo = "その他理由";
		// 所属施設
		const string COL_ShisetsuName = "施設正式名";
		const string COL_ShisetsuTel = "電話番号";
		const string COL_Kaisetsu = "開設主体";
		const string COL_ShisetsuGyomu = "施設・業務";
		const string COL_ByoshoUmu = "病床有無";
		const string COL_ByoshoSu = "病床数";
		const string COL_HeisetsuKaigo = "併設介護";
		const string COL_HeisetsuEtc = "併設その他";
		const string COL_HeisetsuName = "併設施設名";
		// 本人
		const string COL_Tel = "本人電話番号";
		const string COL_Fax = "本人FAX";
		const string COL_Post = "本人郵便";
		const string COL_Addr1 = "本人住所１";
		const string COL_Addr2 = "本人住所２";
		const string COL_Mail = "本人ﾒｱﾄﾞ";
		const string COL_BunshoSofusaki = "文書送付先";
		// 学歴・資格
		const string COL_ShusshinKo = "出身校";
		const string COL_DateSotsugyo = "卒業年月";
		const string COL_DateIsekiToroku = "医籍登録年月日";
		const string COL_DateGakui = "学位取得年月";
		const string COL_ShiteiiHokeni = "指定医";
		const string COL_ShiteiiBotai = "指定医２";
		const string COL_ShiteiiSeishin = "指定医３";
		const string COL_ShiteiiEtc = "指定医４";
		const string COL_TypeKaiin = "会員区分";
		const string COL_FaxSofusaki = "FAX送信先区分";
		const string COL_JushoKeisai = "自宅住所掲載可否";
		const string COL_KaihiUchiwake = "月次会費内訳";
		// 口座
		const string COL_BankCode1 = "銀行ｺｰﾄﾞ１";
		const string COL_BankShiten1 = "支店ｺｰﾄﾞ１";
		const string COL_BankType1 = "口座区分";
		const string COL_BankNo1 = "口座番号";
		const string COL_BankName1 = "口座名義";
		const string COL_BankCode2 = "銀行ｺｰﾄﾞ２";
		const string COL_BankShiten2 = "支店ｺｰﾄﾞ２";
		const string COL_BankType2 = "口座区分#2";
		const string COL_BankNo2 = "口座番号#2";
		const string COL_BankName2 = "口座名義#2";

		/// <summary>診療科目のコード列（１が主たる診療科）</summary>
		static readonly string[] COL_Shinryoka =
		{
			"診療科目１", "診療科目２", "診療科目３", "診療科目４", "診療科目５",
			"診療科目６", "診療科目７", "診療科目８", "診療科目９", "診療科目１０",
		};
		/// <summary>所属学会のコード列</summary>
		static readonly string[] COL_Gakkai =
		{
			"所属学会１", "所属学会２", "所属学会３", "所属学会４", "所属学会５",
		};
		#endregion

		/// <summary>チェック項目がオンのときの末尾（例：「13 チェックオン」）</summary>
		const string CHECK_On = "チェックオン";
		/// <summary>現金払いを表す銀行コード</summary>
		const int BANK_Genkin = 9999;
		/// <summary>口座番号の桁数（数値セルで落ちた先頭の0を補う）</summary>
		const int KOZA_NoLength = 7;
		/// <summary>記述を連結するときの区切り</summary>
		const string MEMO_Separator = "／";

		#region *** 内部クラス ***
		/// <summary>口座1件分</summary>
		class BankInfo
		{
			/// <summary>銀行コード。未設定・現金はnull</summary>
			public int? Code;
			public int? Shiten;
			public int? KozaType;
			public string KozaNo = "";
			public string KozaName = "";
		}

		/// <summary>異動情報1件分</summary>
		class IdoInfo
		{
			public int JiyuKbn;
			public int DetailIdo;
			public string IdoMemo = "";
			public int DetailKaiin;
			public string KaiinMemo = "";
			public int DetailShozai;
			public string ShozaiMemo = "";
			public int DetailAddress;
			public string AddressMemo = "";
			public int DetailEtc;
			public string EtcMemo = "";

			/// <summary>登録済みの内容と比べるための文字列</summary>
			public string GetKey(DateTime date)
			{
				return $"{date:yyyyMMdd}|{JiyuKbn}|{DetailIdo}|{IdoMemo}|{DetailKaiin}|{KaiinMemo}|{DetailShozai}|{ShozaiMemo}|{DetailAddress}|{AddressMemo}|{DetailEtc}|{EtcMemo}";
			}
		}

		/// <summary>医療機関マスターへ補完する内容（医療機関ごと）</summary>
		class ShisetsuInfo
		{
			public int? KaisetsuID;
			public bool? ByoshoUmu;
			public int? ByoshoSu;
			public bool Kaigo;
			public bool Etc;
			public string EtcName = "";
			/// <summary>併設（介護・その他）を読み取り済みか</summary>
			public bool HeisetsuLoaded;
		}

		/// <summary>会員1行分から読み取った、医療機関マスターへ補完する内容</summary>
		class ShisetsuSrc
		{
			public int IryokikanID;
			public string Who;
			/// <summary>在籍・異動の会員の行か</summary>
			public bool Active;
			public int? KaisetsuID;
			public bool? ByoshoUmu;
			public int? ByoshoSu;
			public bool Kaigo;
			public bool Etc;
			public string EtcName = "";
		}

		/// <summary>検証済みの1行分</summary>
		class ImportRow
		{
			public int Code;
			public string Who;
			public string Name;
			public string NameKana;
			public string NameOld;
			public string NameKanaOld;
			public int Sex;
			public DateTime? DateBirth;
			public string Post;
			public string Addr1;
			public string Addr2;
			/// <summary>固定電話。携帯番号の場合は空</summary>
			public string Tel1 = "";
			/// <summary>携帯電話。固定電話の場合は空</summary>
			public string Tel2 = "";
			public string Fax;
			public string Mail;
			public int? IsekiNo;
			public DateTime? DateIsekiToroku;
			public string ShusshinKo;
			public DateTime? DateSotsugyo;
			public DateTime? DateGakui;
			public int Zaiseki;
			public DateTime? DateNyukai;
			public DateTime? DateIdo;
			public DateTime? DateTaikai;
			public int TypeKaiin;
			public int? IshikaiIdomaeID;
			public int? IryokikanID;
			public int? ShisetsuGyomuID;
			public int Shiteii;
			public BankInfo Bank1 = new BankInfo();
			public BankInfo Bank2 = new BankInfo();
			public int TaikaiJiyu;
			public string TaikaiEtcMemo;
			public int? IshikaiIdosakiID;
			public bool BunshoSofusaki;
			public bool FaxSofusaki;
			public bool KaihiUchiwake;
			public bool JushoKeisai;
			/// <summary>医師会会費のID（日医・県医・市医の順）</summary>
			public int[] KaihiIDs = new int[3];
			/// <summary>医師会会費の支払方法</summary>
			public int Shiharai;
			/// <summary>診療科のID（Excelの並び順。先頭が主たる診療科）</summary>
			public List<int> ShinryokaIDs = new List<int>();
			public List<int> GakkaiIDs = new List<int>();
			public List<IdoInfo> Idos = new List<IdoInfo>();
		}
		#endregion

		List<ImportRow> rows;

		/// <summary>医療機関マスターへ補完する内容（医療機関ID → 内容）</summary>
		Dictionary<int, ShisetsuInfo> shisetsus;

		/// <summary>会員の行ごとに読み取った、補完する内容</summary>
		List<ShisetsuSrc> shisetsuSrcs;

		/// <summary>
		/// 取込前に、会員と会員に紐づくデータ（会費・診療科・学会・異動・会費調整・引落データ）を全て削除するか。
		/// 削除は検証が通った後に行うため、Excelにエラーがあれば削除しません。
		/// </summary>
		public bool ClearAll { get; set; }

		/// <summary>
		/// 会員の行にある開設主体・病床・併設を、医療機関マスターへ書き込むか。
		/// </summary>
		public bool FillIryoKikan { get; set; }

		/// <summary>
		/// 必須の見出し名
		/// </summary>
		protected override string[] RequiredColumns
		{
			get
			{
				List<string> cols = new List<string>();

				cols.AddRange(new string[]
				{
					COL_Code, COL_Zaiseki, COL_DateNyukai, COL_DateIdo, COL_DateTaikai,
					COL_KaihiNichii, COL_KaihiKeni, COL_KaihiShii, COL_ZenKenIName, COL_IsekiNo,
					COL_KanaSei, COL_KanaMei, COL_NameSei, COL_NameMei, COL_DateBirth, COL_Sex,
					COL_IdoKaigyo, COL_IdoShisetsu, COL_IdoKaiin, COL_IdoShozai, COL_IdoAddress, COL_IdoEtc,
					COL_TaikaiShibo, COL_TaikaiHaiki, COL_TaikaiIdo, COL_TaikaiIdosaki, COL_TaikaiEtcMemo,
					COL_ShisetsuName, COL_ShisetsuTel, COL_Kaisetsu, COL_ShisetsuGyomu, COL_ByoshoUmu, COL_ByoshoSu,
					COL_HeisetsuKaigo, COL_HeisetsuEtc, COL_HeisetsuName,
					COL_Tel, COL_Fax, COL_Post, COL_Addr1, COL_Addr2, COL_Mail, COL_BunshoSofusaki,
					COL_ShusshinKo, COL_DateSotsugyo, COL_DateIsekiToroku, COL_DateGakui,
					COL_ShiteiiHokeni, COL_ShiteiiBotai, COL_ShiteiiSeishin, COL_ShiteiiEtc,
					COL_TypeKaiin, COL_FaxSofusaki, COL_JushoKeisai, COL_KaihiUchiwake,
					COL_BankCode1, COL_BankShiten1, COL_BankType1, COL_BankNo1, COL_BankName1,
					COL_BankCode2, COL_BankShiten2,
				});
				cols.AddRange(COL_Shinryoka);
				cols.AddRange(COL_Gakkai);

				return cols.ToArray();
			}
		}

		#region *** 検証 ***
		// 検証中のエラーの有無（1行の中で複数のエラーを出すため、行ごとにリセットする）
		bool rowOk;

		/// <summary>
		/// 読み込んだデータを検証します。
		/// </summary>
		protected override bool validateData()
		{
			rows = new List<ImportRow>();
			shisetsus = new Dictionary<int, ShisetsuInfo>();
			shisetsuSrcs = new List<ShisetsuSrc>();

			//¶ 2026/11/14 CC 同名見出しの2つ目以降（#2など）は RequiredColumns に入れられないため、ここで確かめる
			string[] subcols =
			{
				COL_IdoShisetsuSel, COL_IdoShisetsuMemo, COL_IdoKaiinSel, COL_IdoKaiinMemo, COL_IdoShozaiSel, COL_IdoShozaiMemo,
				COL_IdoAddressSel, COL_IdoAddressMemo, COL_IdoEtcSel, COL_IdoEtcMemo, COL_TaikaiEtc,
				COL_BankType2, COL_BankNo2, COL_BankName2,
			};
			foreach (string col in subcols)
			{
				if (dtSrc.Columns.Contains(col) == false)
				{
					Log.Apend(LogTable.eStatus.Error, $"指定されたファイルは、取込対象のファイルではありません。（見出し「{col}」に当たる列がありません）");
					return false;
				}
			}

			// 照合用の辞書
			Dictionary<string, int> dicShisetsuName = new Dictionary<string, int>(); // 施設正式名 → 医療機関ID
			Dictionary<string, int> dicShisetsuTel = new Dictionary<string, int>();  // 電話番号（数字のみ・先頭0なし） → 医療機関ID

			foreach (DataRow row in AppGlobal.DB.GetFillTable(TableProp.t_iryokikan).Rows)
			{
				int id = Cast.Int(row[t_iryokikan.FID_Iryokikan]);

				string name = Cast.String(row[t_iryokikan.FIRK_Name]).Trim();
				if (name != "" && dicShisetsuName.ContainsKey(name) == false)
				{
					dicShisetsuName.Add(name, id);
				}

				string tel = getTelKey(Cast.String(row[t_iryokikan.FIRK_Tel1]));
				if (tel != "" && dicShisetsuTel.ContainsKey(tel) == false)
				{
					dicShisetsuTel.Add(tel, id);
				}
			}

			Dictionary<string, int> dicIshikai = new Dictionary<string, int>(); // 医師会名称 → 医師会ID

			foreach (DataRow row in AppGlobal.DB.GetFillTable(TableProp.t_ishikai).Rows)
			{
				string name = Cast.String(row[t_ishikai.FISK_Name]).Trim();
				if (name != "" && dicIshikai.ContainsKey(name) == false)
				{
					dicIshikai.Add(name, Cast.Int(row[t_ishikai.FID_Ishikai]));
				}
			}

			int cnt_error = 0;
			int cnt_idoNoFlag = 0;
			Dictionary<int, int> codes = new Dictionary<int, int>(); // 会員コード → Excelの行番号

			foreach (DataRow src in dtSrc.Rows)
			{
				int rowno = getRowNo(src);
				ImportRow rec = new ImportRow();

				rowOk = true;

				// 会員コード
				string code = getValue(src, COL_Code);
				if (int.TryParse(code, out rec.Code) == false || rec.Code <= 0 || rec.Code > AppDbID.Max_Code)
				{
					Log.Apend(LogTable.eStatus.Error, $"{rowno}行目：会員コード「{code}」が正しくありません。");
					cnt_error++;
					continue;
				}

				string who = $"{rowno}行目（{rec.Code}）";
				rec.Who = who;

				if (codes.ContainsKey(rec.Code) == true)
				{
					error($"{who}：会員コードが{codes[rec.Code]}行目と重複しています。");
				}
				else
				{
					codes.Add(rec.Code, rowno);
				}

				// 氏名（漢字は全角空白、カナは半角空白でつなぐ）
				rec.Name = joinName(getValue(src, COL_NameSei), getValue(src, COL_NameMei), "　");
				rec.NameKana = joinName(getValue(src, COL_KanaSei), getValue(src, COL_KanaMei), " ");
				rec.NameOld = joinName(getValue(src, COL_OldNameSei), getValue(src, COL_OldNameMei), "　");
				rec.NameKanaOld = joinName(getValue(src, COL_OldKanaSei), getValue(src, COL_OldKanaMei), " ");

				if (rec.Name == "")
				{
					error($"{who}：氏名が入力されていません。");
				}
				if (rec.NameKana == "")
				{
					error($"{who}：氏名のカナが入力されていません。");
				}

				// 性別
				switch (getValue(src, COL_Sex))
				{
					case "男": rec.Sex = (int)eSex.Men; break;
					case "女": rec.Sex = (int)eSex.Women; break;
					case "": rec.Sex = (int)eSex.None; break;
					default:
						error($"{who}：性別「{getValue(src, COL_Sex)}」が正しくありません。");
						break;
				}

				// 在籍区分
				switch (getValue(src, COL_Zaiseki))
				{
					case "在籍": rec.Zaiseki = (int)eTypeZaiseki.Zaiseki; break;
					case "異動": rec.Zaiseki = (int)eTypeZaiseki.Ido; break;
					case "退会": rec.Zaiseki = (int)eTypeZaiseki.Taikai; break;
					default:
						error($"{who}：在籍区分「{getValue(src, COL_Zaiseki)}」が正しくありません。");
						break;
				}

				// 日付
				rec.DateBirth = getDate(src, COL_DateBirth, who);
				rec.DateNyukai = getDate(src, COL_DateNyukai, who);
				rec.DateIdo = getDate(src, COL_DateIdo, who);
				rec.DateTaikai = getDate(src, COL_DateTaikai, who);
				rec.DateIsekiToroku = getDate(src, COL_DateIsekiToroku, who);
				rec.DateSotsugyo = getDate(src, COL_DateSotsugyo, who);
				rec.DateGakui = getDate(src, COL_DateGakui, who);

				if (rec.DateNyukai == null && rowOk == true)
				{
					error($"{who}：入会年月日が入力されていません。");
				}

				// 本人の連絡先
				rec.Post = formatPost(getValue(src, COL_Post));
				rec.Addr1 = getValue(src, COL_Addr1);
				rec.Addr2 = getValue(src, COL_Addr2);
				rec.Mail = getValue(src, COL_Mail);

				bool warn;
				string tel = formatTel(getValue(src, COL_Tel), out warn);
				if (warn == true)
				{
					Log.Apend(LogTable.eStatus.Warning, $"{who}：本人電話番号「{tel}」は区切り位置を判定できないため、ハイフンなしで登録します。");
				}
				//¶ 2026/11/14 CC 本人電話番号は1列だけなので、携帯番号（070/080/090）なら携帯電話番号欄へ入れる（ユーザー了承）
				if (isMobileTel(tel) == true)
				{
					rec.Tel2 = tel;
				}
				else
				{
					rec.Tel1 = tel;
				}

				rec.Fax = formatTel(getValue(src, COL_Fax), out warn);
				if (warn == true)
				{
					Log.Apend(LogTable.eStatus.Warning, $"{who}：本人FAX「{rec.Fax}」は区切り位置を判定できないため、ハイフンなしで登録します。");
				}

				// 医籍・学歴
				rec.IsekiNo = getInt(src, COL_IsekiNo, who, true);
				rec.ShusshinKo = getValue(src, COL_ShusshinKo);

				// 会員区分（Excelの値は区分のコードと同じ）
				int? typeKaiin = getInt(src, COL_TypeKaiin, who, false);
				if (typeKaiin != null)
				{
					if (enumKbn.DTypeKaiin.ContainsKey(typeKaiin.Value) == false)
					{
						error($"{who}：会員区分「{typeKaiin}」が正しくありません。");
					}
					else
					{
						rec.TypeKaiin = typeKaiin.Value;
					}
				}

				// 送付先など（画面のコンボの値に合わせる）
				//¶ 2026/11/14 CC 文書送付先は必須。空欄（退会者のみ）は画面の初期値と同じ「施設」にする
				rec.BunshoSofusaki = (getValue(src, COL_BunshoSofusaki) != "自宅");
				rec.FaxSofusaki = (getValue(src, COL_FaxSofusaki) != "自宅");
				rec.KaihiUchiwake = (getValue(src, COL_KaihiUchiwake) != "必要"); // true：不要／false：必要
				rec.JushoKeisai = (getValue(src, COL_JushoKeisai) == "可");

				// 指定医
				if (isOn(src, COL_ShiteiiHokeni) == true) rec.Shiteii |= (int)AppConst.eShiteii.Hokeni;
				if (isOn(src, COL_ShiteiiBotai) == true) rec.Shiteii |= (int)AppConst.eShiteii.Botai;
				if (isOn(src, COL_ShiteiiSeishin) == true) rec.Shiteii |= (int)AppConst.eShiteii.Seishin;
				if (isOn(src, COL_ShiteiiEtc) == true) rec.Shiteii |= (int)AppConst.eShiteii.Etc;

				// 口座
				bool genkin1;
				bool genkin2;
				rec.Bank1 = getBank(src, COL_BankCode1, COL_BankShiten1, COL_BankType1, COL_BankNo1, COL_BankName1, who, out genkin1);
				rec.Bank2 = getBank(src, COL_BankCode2, COL_BankShiten2, COL_BankType2, COL_BankNo2, COL_BankName2, who, out genkin2);

				// 医師会会費（日医・県医・市医）
				//¶ 2026/11/14 CC 支払方法はExcelにない。銀行コード1が現金（9999）なら現金、それ以外は口座①とする（ユーザー了承）
				rec.Shiharai = (int)((genkin1 == true) ? eShiharai.Genkin : eShiharai.Koza1);

				string[] kaihicols = { COL_KaihiNichii, COL_KaihiKeni, COL_KaihiShii };
				for (int i = 0; i < kaihicols.Length; i++)
				{
					string cd = getValue(src, kaihicols[i]);
					Kaihi kaihi = (cd == "") ? null : AppGlobal.Kaihis.GetCode(cd);

					if (kaihi == null)
					{
						error($"{who}：{kaihicols[i]}「{cd}」が会費マスターに登録されていません。");
					}
					else
					{
						rec.KaihiIDs[i] = kaihi.ID;
					}
				}

				// 診療科目（コード → ID）
				foreach (string col in COL_Shinryoka)
				{
					string cd = getValue(src, col);
					if (cd == "")
					{
						continue;
					}

					Shinryoka sk = AppGlobal.Shinryokas.GetCode(cd);
					if (sk == null)
					{
						error($"{who}：{col}「{cd}」が診療科マスターに登録されていません。");
					}
					else
					if (rec.ShinryokaIDs.Contains(sk.ID) == false)
					{
						rec.ShinryokaIDs.Add(sk.ID);
					}
				}

				// 所属学会（コード → ID）
				foreach (string col in COL_Gakkai)
				{
					string cd = getValue(src, col);
					if (cd == "")
					{
						continue;
					}

					Gakkai gk = AppGlobal.Gakkais.GetCode(cd);
					if (gk == null)
					{
						error($"{who}：{col}「{cd}」が学会マスターに登録されていません。");
					}
					else
					{
						rec.GakkaiIDs.Add(gk.ID);
					}
				}

				// 施設業務（コード → ID）
				string gyomu = getValue(src, COL_ShisetsuGyomu);
				if (gyomu != "")
				{
					Shisetsugyomu sg = AppGlobal.Shisetsugyomus.GetCode(gyomu);
					if (sg == null)
					{
						error($"{who}：施設・業務「{gyomu}」が施設業務マスターに登録されていません。");
					}
					else
					{
						rec.ShisetsuGyomuID = sg.ID;
					}
				}

				// 所属医療機関
				//¶ 2026/11/14 CC Excelに医療機関コードがないため、施設正式名→電話番号の順で照合する。どちらもなければ所属なし（ユーザー了承）
				string shisetsu = getValue(src, COL_ShisetsuName);
				if (shisetsu != "")
				{
					string telkey = getTelKey(getValue(src, COL_ShisetsuTel));

					if (dicShisetsuName.ContainsKey(shisetsu) == true)
					{
						rec.IryokikanID = dicShisetsuName[shisetsu];

						//¶ 2026/11/14 CC 医療機関マスターへの補完には、名称が一致した行だけを使う（電話番号だけの一致は、同じ番号を引き継いだ別の施設の可能性がある）
						readShisetsuInfo(src, rec.IryokikanID.Value, who, rec.Zaiseki != (int)eTypeZaiseki.Taikai);
					}
					else
					if (telkey != "" && dicShisetsuTel.ContainsKey(telkey) == true)
					{
						rec.IryokikanID = dicShisetsuTel[telkey];
						Log.Apend(LogTable.eStatus.Warning, $"{who}：施設「{shisetsu}」は名称が一致しないため、電話番号が一致する医療機関（{AppGlobal.IryoKikans.Get(rec.IryokikanID.Value).XRow.IRK_Name}）を設定します。");
					}
					else
					{
						Log.Apend(LogTable.eStatus.Warning, $"{who}：施設「{shisetsu}」が医療機関マスターにないため、所属医療機関は未設定で登録します。");
					}
				}

				// 入会前の医師会
				rec.IshikaiIdomaeID = findIshikai(dicIshikai, getValue(src, COL_ZenKenIName));
				if (getValue(src, COL_ZenKenIName) != "" && rec.IshikaiIdomaeID == null)
				{
					Log.Apend(LogTable.eStatus.Warning, $"{who}：前県医名「{getValue(src, COL_ZenKenIName)}」が医師会マスターにないため、異動前医師会は未設定で登録します。");
				}

				// 退会
				//¶ 2026/11/14 CC 退会事由は1つしか持てないので、複数チェックがある場合は 死亡→廃棄・退職→医師会の異動→その他 の順で採用する
				if (isOn(src, COL_TaikaiShibo) == true) rec.TaikaiJiyu = (int)eTypeTaikaiJiyu.Shibo;
				else if (isOn(src, COL_TaikaiHaiki) == true) rec.TaikaiJiyu = (int)eTypeTaikaiJiyu.Haiki;
				else if (isOn(src, COL_TaikaiIdo) == true) rec.TaikaiJiyu = (int)eTypeTaikaiJiyu.Ido;
				else if (isOn(src, COL_TaikaiEtc) == true) rec.TaikaiJiyu = (int)eTypeTaikaiJiyu.Etc;

				rec.TaikaiEtcMemo = getValue(src, COL_TaikaiEtcMemo);

				rec.IshikaiIdosakiID = findIshikai(dicIshikai, getValue(src, COL_TaikaiIdosaki));
				if (getValue(src, COL_TaikaiIdosaki) != "" && rec.IshikaiIdosakiID == null)
				{
					Log.Apend(LogTable.eStatus.Warning, $"{who}：移動先「{getValue(src, COL_TaikaiIdosaki)}」が医師会マスターにないため、異動先医師会は未設定で登録します。");
				}

				// 異動情報
				readIdo(src, rec);

				if (rec.Idos.Count > 0 && rec.DateIdo == null)
				{
					Log.Apend(LogTable.eStatus.Warning, $"{who}：異動のチェックがありますが異動年月日がないため、異動情報は登録しません。");
					rec.Idos.Clear();
				}
				else
				if (rec.Idos.Count == 0 && rec.DateIdo != null)
				{
					cnt_idoNoFlag++;
				}

				if (rowOk == true)
				{
					rows.Add(rec);
				}
				else
				{
					cnt_error++;
				}
			}

			if (cnt_idoNoFlag > 0)
			{
				//¶ 2026/11/14 CC 異動年月日だけで異動のチェックがない会員は、異動情報を作らない（ユーザー了承）
				Log.Apend(LogTable.eStatus.None, $"異動年月日はあるが異動のチェックがない会員が{cnt_idoNoFlag}人います。異動情報は登録しません。");
			}

			if (cnt_error > 0)
			{
				Log.Apend(LogTable.eStatus.Error, $"{cnt_error}件の行にエラーがあります。");
				return false;
			}

			if (FillIryoKikan == true)
			{
				mergeShisetsuInfo();
			}

			return true;
		}

		/// <summary>
		/// エラーをログへ追加し、検証中の行をエラーにします。
		/// </summary>
		void error(string msg)
		{
			Log.Apend(LogTable.eStatus.Error, msg);
			rowOk = false;
		}

		/// <summary>
		/// チェック項目がオンかを調べます。
		/// </summary>
		bool isOn(DataRow src, string col)
		{
			return getValue(src, col).EndsWith(CHECK_On);
		}

		/// <summary>
		/// 姓と名をつなぎます。
		/// </summary>
		string joinName(string sei, string mei, string separator)
		{
			if (sei == "" || mei == "")
			{
				return sei + mei;
			}

			return sei + separator + mei;
		}

		/// <summary>
		/// 日付の列を読み取ります。読み取れない場合はエラーにします。
		/// </summary>
		DateTime? getDate(DataRow src, string col, string who)
		{
			DateTime? date;

			if (tryParseDate(getValue(src, col), out date) == false)
			{
				error($"{who}：{col}「{getValue(src, col)}」を日付として読み取れません。");
			}

			return date;
		}

		/// <summary>
		/// 整数の列を読み取ります。読み取れない場合はエラーにします。
		/// </summary>
		/// <param name="zeroIsNull">0を未入力として扱うか</param>
		int? getInt(DataRow src, string col, string who, bool zeroIsNull)
		{
			string val = getValue(src, col);
			if (val == "")
			{
				return null;
			}

			int num;
			if (int.TryParse(val, out num) == false)
			{
				error($"{who}：{col}「{val}」が数値ではありません。");
				return null;
			}

			if (zeroIsNull == true && num == 0)
			{
				return null;
			}

			return num;
		}

		/// <summary>
		/// 口座の列を読み取ります。
		/// </summary>
		/// <param name="genkin">銀行コードが現金（9999）の場合にtrue</param>
		BankInfo getBank(DataRow src, string colCode, string colShiten, string colType, string colNo, string colName, string who, out bool genkin)
		{
			BankInfo bank = new BankInfo();

			genkin = false;

			int? code = getInt(src, colCode, who, true);
			if (code == BANK_Genkin)
			{
				//¶ 2026/11/14 CC 9999は現金払いを表す値で、銀行コードとしては登録しない
				genkin = true;
				code = null;
			}

			bank.KozaName = getValue(src, colName);

			if (code == null)
			{
				return bank;
			}

			bank.Code = code;
			bank.Shiten = getInt(src, colShiten, who, true);
			bank.KozaType = getInt(src, colType, who, true);

			if (bank.KozaType != null && bank.KozaType != (int)eTypeKoza.Futsu && bank.KozaType != (int)eTypeKoza.Touza)
			{
				error($"{who}：{colType}「{bank.KozaType}」が正しくありません。");
			}

			//¶ 2026/11/14 CC 口座番号は数値セルで先頭の0が落ちているので、7桁になるよう0を補う
			string no = getValue(src, colNo);
			if (no != "")
			{
				bank.KozaNo = no.PadLeft(KOZA_NoLength, '0');
			}

			return bank;
		}

		/// <summary>
		/// 電話番号を照合用の形（数字のみ・先頭の0なし）にします。番号なしを表す値（全て9）は空にします。
		/// </summary>
		string getTelKey(string tel)
		{
			string key = "";

			foreach (char c in tel)
			{
				if (c >= '0' && c <= '9')
				{
					key += c;
				}
			}

			key = key.TrimStart('0');

			if (key.Trim('9') == "")
			{
				return "";
			}

			return key;
		}

		/// <summary>
		/// 医師会の名称から医師会IDを探します。「藤沢市」のように「医師会」が付かない表記にも対応します。
		/// </summary>
		/// <returns>見つからない場合はnull</returns>
		int? findIshikai(Dictionary<string, int> dic, string name)
		{
			if (name == "")
			{
				return null;
			}

			if (dic.ContainsKey(name) == true)
			{
				return dic[name];
			}
			if (dic.ContainsKey(name + "医師会") == true)
			{
				return dic[name + "医師会"];
			}

			return null;
		}

		/// <summary>
		/// 会員の行から、医療機関マスターへ補完する内容を読み取ります。
		/// </summary>
		/// <param name="active">在籍・異動の会員の行か（退会者の行は古い内容の可能性がある）</param>
		void readShisetsuInfo(DataRow src, int iryokikanId, string who, bool active)
		{
			ShisetsuSrc info = new ShisetsuSrc();

			info.IryokikanID = iryokikanId;
			info.Who = who;
			info.Active = active;

			// 開設主体（コード → ID）
			string kaisetsu = getValue(src, COL_Kaisetsu);
			if (kaisetsu != "")
			{
				Kaisetsushutai ks = AppGlobal.Kaisetsushutais.GetCode(kaisetsu);
				if (ks == null)
				{
					error($"{who}：開設主体「{kaisetsu}」が開設主体マスターに登録されていません。");
				}
				else
				{
					info.KaisetsuID = ks.ID;
				}
			}

			// 病床有無
			string byosho = getValue(src, COL_ByoshoUmu);
			if (byosho == "有" || byosho == "無")
			{
				info.ByoshoUmu = (byosho == "有");
			}
			else
			if (byosho != "")
			{
				error($"{who}：病床有無「{byosho}」が正しくありません。");
			}

			// 病床数
			info.ByoshoSu = getInt(src, COL_ByoshoSu, who, true);

			// 併設の施設
			info.Kaigo = isOn(src, COL_HeisetsuKaigo);
			info.Etc = isOn(src, COL_HeisetsuEtc);
			info.EtcName = getValue(src, COL_HeisetsuName);

			shisetsuSrcs.Add(info);
		}

		/// <summary>
		/// 会員の行ごとに読み取った内容を、医療機関ごとにまとめます。
		/// 同じ医療機関の会員が複数いる場合は、在籍・異動の会員の行を先に見て、最初の値を採用します。
		/// </summary>
		void mergeShisetsuInfo()
		{
			//¶ 2026/11/14 CC 退会者の行は当時の内容のままで現在と違うことがあるため、在籍・異動の会員の行を優先する
			List<ShisetsuSrc> sorted = new List<ShisetsuSrc>();
			sorted.AddRange(shisetsuSrcs.FindAll(s => s.Active == true));
			sorted.AddRange(shisetsuSrcs.FindAll(s => s.Active == false));

			foreach (ShisetsuSrc src in sorted)
			{
				if (shisetsus.ContainsKey(src.IryokikanID) == false)
				{
					shisetsus.Add(src.IryokikanID, new ShisetsuInfo());
				}

				ShisetsuInfo info = shisetsus[src.IryokikanID];
				List<string> diffs = new List<string>();

				if (src.KaisetsuID != null)
				{
					if (info.KaisetsuID == null) info.KaisetsuID = src.KaisetsuID;
					else if (info.KaisetsuID != src.KaisetsuID) diffs.Add("開設主体");
				}

				if (src.ByoshoUmu != null)
				{
					if (info.ByoshoUmu == null) info.ByoshoUmu = src.ByoshoUmu;
					else if (info.ByoshoUmu != src.ByoshoUmu) diffs.Add("病床有無");
				}

				if (src.ByoshoSu != null)
				{
					if (info.ByoshoSu == null) info.ByoshoSu = src.ByoshoSu;
					else if (info.ByoshoSu != src.ByoshoSu) diffs.Add("病床数");
				}

				if (info.HeisetsuLoaded == false)
				{
					info.Kaigo = src.Kaigo;
					info.Etc = src.Etc;
					info.EtcName = src.EtcName;
					info.HeisetsuLoaded = true;
				}
				else
				if (info.Kaigo != src.Kaigo || info.Etc != src.Etc)
				{
					diffs.Add("併設の施設");
				}

				//¶ 2026/11/14 CC 食い違いの警告は、在籍・異動の会員どうしの場合だけ出す（退会者の行との違いは想定内）
				if (diffs.Count > 0 && src.Active == true)
				{
					string name = AppGlobal.IryoKikans.Get(src.IryokikanID).XRow.IRK_Name;
					Log.Apend(LogTable.eStatus.Warning, $"{src.Who}：医療機関「{name}」の{string.Join("・", diffs)}が、他の会員の行と一致しません。先に読んだ値を採用します。");
				}
			}
		}

		/// <summary>
		/// 異動のチェックから異動情報を作ります。チェック1つにつき1件。
		/// 選択肢にない文言は「その他」とし、記述欄へ入れます。
		/// </summary>
		void readIdo(DataRow src, ImportRow rec)
		{
			IdoInfo ido;

			// 施設開業 → 医師会の異動・施設開業・施設異動
			if (isOn(src, COL_IdoKaigyo) == true)
			{
				ido = new IdoInfo();
				ido.JiyuKbn = (int)eTypeIdoJiyu.Ido;
				ido.DetailIdo = (int)eIdoShisetsu.Etc;
				ido.IdoMemo = "施設開業";
				rec.Idos.Add(ido);
			}

			// 施設異動 → 医師会の異動・施設開業・施設異動
			if (isOn(src, COL_IdoShisetsu) == true)
			{
				string sel = getValue(src, COL_IdoShisetsuSel);
				string memo = getValue(src, COL_IdoShisetsuMemo);

				ido = new IdoInfo();
				ido.JiyuKbn = (int)eTypeIdoJiyu.Ido;

				switch (sel)
				{
					case "勤務先": ido.DetailIdo = (int)eIdoShisetsu.KinmuSaki; break;
					case "休養": ido.DetailIdo = (int)eIdoShisetsu.Kyuyo; break;
					case "廃業": ido.DetailIdo = (int)eIdoShisetsu.Haigyo; break;
					case "退職": ido.DetailIdo = (int)eIdoShisetsu.Taisｈoku; break;
					default:
						ido.DetailIdo = (int)eIdoShisetsu.Etc;
						memo = joinMemo(sel, memo);
						break;
				}

				ido.IdoMemo = memo;
				rec.Idos.Add(ido);
			}

			// 会員区分変更 → 会員区分の変更
			if (isOn(src, COL_IdoKaiin) == true)
			{
				string sel = getValue(src, COL_IdoKaiinSel);
				string memo = getValue(src, COL_IdoKaiinMemo);

				ido = new IdoInfo();
				ido.JiyuKbn = (int)eTypeIdoJiyu.Kaiin;

				if (sel == "開業")
				{
					ido.DetailKaiin = (int)eIdoKaiinHenko.Kaigyo;
				}
				else
				if (sel == "管理変更")
				{
					ido.DetailKaiin = (int)eIdoKaiinHenko.KanriHenko;
				}
				else
				if (sel == "" && memo == "開設者変更")
				{
					ido.DetailKaiin = (int)eIdoKaiinHenko.KaisetsuHenko;
				}
				else
				{
					ido.DetailKaiin = (int)eIdoKaiinHenko.Haigyo; // 廃業・その他
					memo = joinMemo(sel, memo);
				}

				ido.KaiinMemo = memo;
				rec.Idos.Add(ido);
			}

			// 施設所在地変更
			if (isOn(src, COL_IdoShozai) == true)
			{
				string sel = getValue(src, COL_IdoShozaiSel);
				string memo = getValue(src, COL_IdoShozaiMemo);

				ido = new IdoInfo();
				ido.JiyuKbn = (int)eTypeIdoJiyu.Shozai;

				if (sel == "移転")
				{
					ido.DetailShozai = (int)eIdoShozai.Iten;
				}
				else
				{
					ido.DetailShozai = (int)eIdoShozai.Etc;
					memo = joinMemo(sel, memo);
				}

				ido.ShozaiMemo = memo;
				rec.Idos.Add(ido);
			}

			// 現住所変更
			if (isOn(src, COL_IdoAddress) == true)
			{
				string sel = getValue(src, COL_IdoAddressSel);
				string memo = getValue(src, COL_IdoAddressMemo);

				ido = new IdoInfo();
				ido.JiyuKbn = (int)eTypeIdoJiyu.Address;

				if (sel == "転居")
				{
					ido.DetailAddress = (int)eIdoAddress.Tenkyo;
				}
				else
				{
					ido.DetailAddress = (int)eIdoAddress.Etc;
					memo = joinMemo(sel, memo);
				}

				ido.AddressMemo = memo;
				rec.Idos.Add(ido);
			}

			// その他（医籍登録番号変更・氏名変更も、受け皿がないためその他として登録する）
			bool iseki = isOn(src, COL_IdoIsekiHenko);
			bool namehenko = isOn(src, COL_IdoNameHenko);

			if (isOn(src, COL_IdoEtc) == true || iseki == true || namehenko == true)
			{
				string sel = getValue(src, COL_IdoEtcSel);
				string memo = getValue(src, COL_IdoEtcMemo);

				ido = new IdoInfo();
				ido.JiyuKbn = (int)eTypeIdoJiyu.Etc;

				if (sel == "名称変更")
				{
					ido.DetailEtc = (int)eIdoEtc.MeishoHenko;
				}
				else
				if (sel == "法人化")
				{
					ido.DetailEtc = (int)eIdoEtc.Hojinka;
				}
				else
				{
					ido.DetailEtc = (int)eIdoEtc.Etc;
					memo = joinMemo(sel, memo);
				}

				if (iseki == true) memo = joinMemo("医籍登録番号変更", memo);
				if (namehenko == true) memo = joinMemo("氏名変更", memo);

				ido.EtcMemo = memo;
				rec.Idos.Add(ido);
			}
		}

		/// <summary>
		/// 記述をつなぎます。
		/// </summary>
		string joinMemo(string a, string b)
		{
			if (a == "" || b == "")
			{
				return a + b;
			}

			return a + MEMO_Separator + b;
		}
		#endregion

		#region *** 反映 ***
		/// <summary>
		/// 検証済みのデータをDBへ反映します。
		/// </summary>
		protected override bool updateData()
		{
			DataTable tblKaiin = AppGlobal.DB.GetFillTable(TableProp.t_kaiin);
			DataTable tblKaihi = AppGlobal.DB.GetFillTable(TableProp.t_kaiin_kaihi);
			DataTable tblShinryo = AppGlobal.DB.GetFillTable(TableProp.t_kaiin_shinryoka);
			DataTable tblGakkai = AppGlobal.DB.GetFillTable(TableProp.t_kaiin_gakkai);
			DataTable tblIdo = AppGlobal.DB.GetFillTable(TableProp.t_kaiin_ido);
			DataTable tblChosei = AppGlobal.DB.GetFillTable(TableProp.t_kaiin_chosei);
			DataTable tblHikiotoshi = AppGlobal.DB.GetFillTable(TableProp.t_hikiotoshi);
			DataTable tblKikan = AppGlobal.DB.GetFillTable(TableProp.t_iryokikan);

			DataTable[] tables = { tblKaiin, tblKaihi, tblShinryo, tblGakkai, tblIdo, tblChosei, tblHikiotoshi, tblKikan };

			try
			{
				if (ClearAll == true)
				{
					//¶ 2026/11/14 CC 全クリア。会員IDを参照する会費調整・引落データも一緒に削除する（ユーザー了承）
					int cnt_del = tblKaiin.Rows.Count;
					int cnt_chosei = tblChosei.Rows.Count;
					int cnt_hikiotoshi = tblHikiotoshi.Rows.Count;

					foreach (DataTable tbl in new DataTable[] { tblKaihi, tblShinryo, tblGakkai, tblIdo, tblChosei, tblHikiotoshi, tblKaiin })
					{
						foreach (DataRow row in tbl.Select())
						{
							row.Delete();
						}
					}

					Log.Apend(LogTable.eStatus.None, $"登録済みの会員{cnt_del}件と、会員に紐づく会費・診療科・学会・異動情報を削除しました。");
					Log.Apend(LogTable.eStatus.Warning, $"会費調整{cnt_chosei}件、引落データ{cnt_hikiotoshi}件も削除しました。参加医会（医会等会費）は取込に含まれないため、再登録が必要です。");
				}

				// 既存の会員（コード → 行）、ID・グループIDの最大値、医籍登録番号 → グループID
				Dictionary<int, DataRow> dicKaiin = new Dictionary<int, DataRow>();
				Dictionary<int, int> dicGroup = new Dictionary<int, int>();
				int maxid = 0;
				int maxgroup = 0;

				foreach (DataRow row in tblKaiin.Rows)
				{
					if (row.RowState == DataRowState.Deleted)
					{
						continue;
					}

					int cd = Cast.Int(row[t_kaiin.FCD_Kaiin]);
					if (dicKaiin.ContainsKey(cd) == false)
					{
						dicKaiin.Add(cd, row);
					}

					maxid = Math.Max(maxid, Cast.Int(row[t_kaiin.FID_Kaiin]));

					int group = Cast.Int(row[t_kaiin.FID_KaiinGroup]);
					maxgroup = Math.Max(maxgroup, group);

					int iseki = Cast.Int(row[t_kaiin.FKaiin_IsekiTorokuNo]);
					if (iseki > 0 && group > 0 && dicGroup.ContainsKey(iseki) == false)
					{
						dicGroup.Add(iseki, group);
					}
				}

				// 既存の子レコード（会員ID → 行。登録順）
				Dictionary<int, List<DataRow>> dicKaihi = getChildRows(tblKaihi, t_kaiin_kaihi.FID_Kaiin);
				Dictionary<int, List<DataRow>> dicShinryo = getChildRows(tblShinryo, t_kaiin_shinryoka.FID_Kaiin);
				Dictionary<int, List<DataRow>> dicGakkai = getChildRows(tblGakkai, t_kaiin_gakkai.FID_Kaiin);
				Dictionary<int, List<DataRow>> dicIdo = getChildRows(tblIdo, t_kaiin_ido.FID_Kaiin);

				int cnt_add = 0;
				int cnt_update = 0;
				int cnt_same = 0;

				foreach (ImportRow rec in rows)
				{
					DataRow row;
					bool isnew = false;
					bool changed = false;

					if (dicKaiin.ContainsKey(rec.Code) == true)
					{
						row = dicKaiin[rec.Code];
					}
					else
					{
						isnew = true;
						maxid++;

						row = tblKaiin.NewRow();
						row[t_kaiin.FID_Kaiin] = maxid;
						row[t_kaiin.FCD_Kaiin] = rec.Code;

						//¶ 2026/11/14 CC 再入会の紐付け：医籍登録番号が同じ会員は同じ会員グループにする（ユーザー了承）
						if (rec.IsekiNo != null && dicGroup.ContainsKey(rec.IsekiNo.Value) == true)
						{
							row[t_kaiin.FID_KaiinGroup] = dicGroup[rec.IsekiNo.Value];
						}
						else
						{
							maxgroup++;
							row[t_kaiin.FID_KaiinGroup] = maxgroup;

							if (rec.IsekiNo != null)
							{
								dicGroup.Add(rec.IsekiNo.Value, maxgroup);
							}
						}
					}

					int id = Cast.Int(row[t_kaiin.FID_Kaiin]);

					changed |= setValue(row, t_kaiin.FKaiin_Name, rec.Name);
					changed |= setValue(row, t_kaiin.FKaiin_NameKana, rec.NameKana);
					changed |= setValue(row, t_kaiin.FKaiin_NameOld, rec.NameOld);
					changed |= setValue(row, t_kaiin.FKaiin_NameKanaOld, rec.NameKanaOld);
					changed |= setValue(row, t_kaiin.FKaiin_Sex, rec.Sex);
					changed |= setValue(row, t_kaiin.FKaiin_DateBirth, rec.DateBirth);
					changed |= setValue(row, t_kaiin.FKaiin_Post, rec.Post);
					changed |= setValue(row, t_kaiin.FKaiin_Addr1, rec.Addr1);
					changed |= setValue(row, t_kaiin.FKaiin_Addr2, rec.Addr2);

					//¶ 2026/11/14 CC 本人電話番号は1列なので、番号が入った側の欄だけを更新する（もう一方の欄は既存値を残す）
					if (rec.Tel1 != "" || rec.Tel2 == "")
					{
						changed |= setValue(row, t_kaiin.FKaiin_Tel1, rec.Tel1);
					}
					if (rec.Tel2 != "")
					{
						changed |= setValue(row, t_kaiin.FKaiin_Tel2, rec.Tel2);
					}

					changed |= setValue(row, t_kaiin.FKaiin_Fax, rec.Fax);
					changed |= setValue(row, t_kaiin.FKaiin_Mail1, rec.Mail);
					changed |= setValue(row, t_kaiin.FKaiin_IsekiTorokuNo, rec.IsekiNo);
					changed |= setValue(row, t_kaiin.FKaiin_DateIsekiToroku, rec.DateIsekiToroku);
					changed |= setValue(row, t_kaiin.FKaiin_ShusshinKo, rec.ShusshinKo);
					changed |= setValue(row, t_kaiin.FKaiin_DateSotsugyo, rec.DateSotsugyo);
					changed |= setValue(row, t_kaiin.FKaiin_DateShutokuGakui, rec.DateGakui);
					changed |= setValue(row, t_kaiin.FKaiin_TypeZaiseki, rec.Zaiseki);
					changed |= setValue(row, t_kaiin.FKaiin_DateNyukai, rec.DateNyukai);

					//¶ 2026/11/14 CC 登録日は必須だがExcelにないため、未入力なら入会年月日を入れる（ユーザー了承）
					if (isEmpty(row[t_kaiin.FKaiin_DateToroku]) == true)
					{
						changed |= setValue(row, t_kaiin.FKaiin_DateToroku, rec.DateNyukai);
					}

					changed |= setValue(row, t_kaiin.FKaiin_TypeKaiin, rec.TypeKaiin);
					changed |= setValue(row, t_kaiin.FID_Ishikai_Idomae, rec.IshikaiIdomaeID);
					changed |= setValue(row, t_kaiin.FID_Shinryoka_Main, (rec.ShinryokaIDs.Count > 0) ? (int?)rec.ShinryokaIDs[0] : null);
					changed |= setValue(row, t_kaiin.FID_Iryokikan, rec.IryokikanID);
					changed |= setValue(row, t_kaiin.FID_ShisetsuGyomu, rec.ShisetsuGyomuID);
					changed |= setValue(row, t_kaiin.FKaiin_Shiteii, rec.Shiteii);

					changed |= setValue(row, t_kaiin.FKaiin_BankCode1, rec.Bank1.Code);
					changed |= setValue(row, t_kaiin.FKaiin_BankShitenCode1, rec.Bank1.Shiten);
					changed |= setValue(row, t_kaiin.FKaiin_BankKozaType1, rec.Bank1.KozaType);
					changed |= setValue(row, t_kaiin.FKaiin_BankKozaNo1, rec.Bank1.KozaNo);
					changed |= setValue(row, t_kaiin.FKaiin_BankKozaName1, rec.Bank1.KozaName);
					changed |= setValue(row, t_kaiin.FKaiin_BankCode2, rec.Bank2.Code);
					changed |= setValue(row, t_kaiin.FKaiin_BankShitenCode2, rec.Bank2.Shiten);
					changed |= setValue(row, t_kaiin.FKaiin_BankKozaType2, rec.Bank2.KozaType);
					changed |= setValue(row, t_kaiin.FKaiin_BankKozaNo2, rec.Bank2.KozaNo);
					changed |= setValue(row, t_kaiin.FKaiin_BankKozaName2, rec.Bank2.KozaName);

					changed |= setValue(row, t_kaiin.FKaiin_DateTaikai, rec.DateTaikai);
					changed |= setValue(row, t_kaiin.FKaiin_TypeTaikaiJiyu, rec.TaikaiJiyu);
					changed |= setValue(row, t_kaiin.FKaiin_TaikaiEtcmemo, rec.TaikaiEtcMemo);
					changed |= setValue(row, t_kaiin.FID_Ishikai_Idosaki, rec.IshikaiIdosakiID);
					changed |= setValue(row, t_kaiin.FKaiin_BunshoSofusaki, rec.BunshoSofusaki);
					changed |= setValue(row, t_kaiin.FKaiin_FaxSofusaki, rec.FaxSofusaki);
					changed |= setValue(row, t_kaiin.FKaiin_KaihiUchiwake, rec.KaihiUchiwake);
					changed |= setValue(row, t_kaiin.FKaiin_JushoKeisai, rec.JushoKeisai);

					changed |= updateKaihi(tblKaihi, dicKaihi, id, rec);
					changed |= updateIdList(tblShinryo, dicShinryo, id, rec.ShinryokaIDs, t_kaiin_shinryoka.FID_Kaiin, t_kaiin_shinryoka.FID_Shinryoka);
					changed |= updateIdList(tblGakkai, dicGakkai, id, rec.GakkaiIDs, t_kaiin_gakkai.FID_Kaiin, t_kaiin_gakkai.FID_Gakkai);
					changed |= updateIdo(tblIdo, dicIdo, id, rec);

					if (isnew == true)
					{
						row[t_kaiin.FLastUpdateUser] = AppGlobal.LoginUser.ID;
						tblKaiin.Rows.Add(row);
						cnt_add++;
					}
					else
					if (changed == true)
					{
						row[t_kaiin.FLastUpdateUser] = AppGlobal.LoginUser.ID;
						cnt_update++;
					}
					else
					{
						cnt_same++;
					}
				}

				// 医療機関マスターの補完
				int cnt_kikan = 0;
				if (FillIryoKikan == true)
				{
					cnt_kikan = fillIryoKikan(tblKikan);
				}

				//¶ 2026/11/14 CC 会員本体を先に、子テーブルを後に更新する
				string[] names =
				{
					TableProp.t_kaiin, TableProp.t_kaiin_kaihi, TableProp.t_kaiin_shinryoka, TableProp.t_kaiin_gakkai,
					TableProp.t_kaiin_ido, TableProp.t_kaiin_chosei, TableProp.t_hikiotoshi, TableProp.t_iryokikan,
				};
				foreach (string name in names)
				{
					if (AppGlobal.DB.UpdateTable(name) == false)
					{
						rejectChanges(tables);
						return false;
					}
				}

				Log.Apend(LogTable.eStatus.Success, $"会員情報を取り込みました。追加：{cnt_add}件／更新：{cnt_update}件／変更なし：{cnt_same}件");
				if (FillIryoKikan == true)
				{
					Log.Apend(LogTable.eStatus.Success, $"医療機関マスターの開設主体・病床・併設を補完しました。更新：{cnt_kikan}件");
				}

				return true;
			}
			catch
			{
				//¶ 2026/11/14 CC 途中で失敗した場合は、バッファ上の変更を取り消してから呼び出し元へ伝える
				rejectChanges(tables);
				throw;
			}
		}

		/// <summary>
		/// バッファ上の変更を取り消します。
		/// </summary>
		void rejectChanges(DataTable[] tables)
		{
			foreach (DataTable tbl in tables)
			{
				tbl.RejectChanges();
			}
		}

		/// <summary>
		/// 子テーブルの行を、会員IDごとにまとめます。
		/// </summary>
		Dictionary<int, List<DataRow>> getChildRows(DataTable tbl, string fldKaiin)
		{
			Dictionary<int, List<DataRow>> dic = new Dictionary<int, List<DataRow>>();

			foreach (DataRow row in tbl.Rows)
			{
				if (row.RowState == DataRowState.Deleted)
				{
					continue;
				}

				int id = Cast.Int(row[fldKaiin]);

				if (dic.ContainsKey(id) == false)
				{
					dic.Add(id, new List<DataRow>());
				}
				dic[id].Add(row);
			}

			return dic;
		}

		/// <summary>
		/// 医師会会費（日医・県医・市医）の3レコードを更新します。
		/// 参加医会（医会区分がその他）のレコードには触れません。
		/// </summary>
		/// <returns>変更あり：true</returns>
		bool updateKaihi(DataTable tbl, Dictionary<int, List<DataRow>> dic, int kaiinId, ImportRow rec)
		{
			int[] types = { (int)eTypeIkai.Nichii, (int)eTypeIkai.Keni, (int)eTypeIkai.Shii };

			// 登録済みの医師会会費
			List<DataRow> olds = new List<DataRow>();
			if (dic.ContainsKey(kaiinId) == true)
			{
				foreach (DataRow old in dic[kaiinId])
				{
					int type = Cast.Int(old[t_kaiin_kaihi.FKaihiTypeIkai]);
					if (Array.IndexOf(types, type) >= 0)
					{
						olds.Add(old);
					}
				}
			}

			// 内容が同じなら何もしない
			if (olds.Count == types.Length)
			{
				bool same = true;

				for (int i = 0; i < types.Length; i++)
				{
					bool found = false;

					foreach (DataRow old in olds)
					{
						if (Cast.Int(old[t_kaiin_kaihi.FKaihiTypeIkai]) == types[i] &&
							Cast.Int(old[t_kaiin_kaihi.FID_Kaihi]) == rec.KaihiIDs[i] &&
							Cast.Int(old[t_kaiin_kaihi.FKaihiShiharai]) == rec.Shiharai)
						{
							found = true;
							break;
						}
					}

					if (found == false)
					{
						same = false;
						break;
					}
				}

				if (same == true)
				{
					return false;
				}
			}

			foreach (DataRow old in olds)
			{
				old.Delete();
			}

			for (int i = 0; i < types.Length; i++)
			{
				DataRow nrow = tbl.NewRow();
				nrow[t_kaiin_kaihi.FID_Kaiin] = kaiinId;
				nrow[t_kaiin_kaihi.FID_Kaihi] = rec.KaihiIDs[i];
				nrow[t_kaiin_kaihi.FKaihiShiharai] = rec.Shiharai;
				nrow[t_kaiin_kaihi.FKaihiTypeIkai] = types[i];
				tbl.Rows.Add(nrow);
			}

			return true;
		}

		/// <summary>
		/// 会員ごとのID一覧（担当診療科・所属学会）を更新します。内容が変わる場合だけ入れ直します。
		/// </summary>
		/// <returns>変更あり：true</returns>
		bool updateIdList(DataTable tbl, Dictionary<int, List<DataRow>> dic, int kaiinId, List<int> ids, string fldKaiin, string fldId)
		{
			List<DataRow> olds = dic.ContainsKey(kaiinId) ? dic[kaiinId] : new List<DataRow>();

			if (olds.Count == ids.Count)
			{
				bool same = true;

				for (int i = 0; i < ids.Count; i++)
				{
					if (Cast.Int(olds[i][fldId]) != ids[i])
					{
						same = false;
						break;
					}
				}

				if (same == true)
				{
					return false;
				}
			}

			foreach (DataRow old in olds)
			{
				old.Delete();
			}

			foreach (int id in ids)
			{
				DataRow nrow = tbl.NewRow();
				nrow[fldKaiin] = kaiinId;
				nrow[fldId] = id;
				tbl.Rows.Add(nrow);
			}

			return true;
		}

		/// <summary>
		/// 異動情報を更新します。内容が変わる場合だけ入れ直します。
		/// </summary>
		/// <returns>変更あり：true</returns>
		bool updateIdo(DataTable tbl, Dictionary<int, List<DataRow>> dic, int kaiinId, ImportRow rec)
		{
			List<DataRow> olds = dic.ContainsKey(kaiinId) ? dic[kaiinId] : new List<DataRow>();

			if (olds.Count == rec.Idos.Count)
			{
				bool same = true;

				for (int i = 0; i < olds.Count; i++)
				{
					DataRow old = olds[i];

					IdoInfo cur = new IdoInfo();
					cur.JiyuKbn = Cast.Int(old[t_kaiin_ido.FIdoJiyuKbn]);
					cur.DetailIdo = Cast.Int(old[t_kaiin_ido.FIdoJiyuDetail_Ido]);
					cur.IdoMemo = Cast.String(old[t_kaiin_ido.FIdoJiyuDetail_IdoMemo]);
					cur.DetailKaiin = Cast.Int(old[t_kaiin_ido.FIdoJiyuDetail_Kaiin]);
					cur.KaiinMemo = Cast.String(old[t_kaiin_ido.FIdoJiyuDetail_KaiinMemo]);
					cur.DetailShozai = Cast.Int(old[t_kaiin_ido.FIdoJiyuDetail_Shozai]);
					cur.ShozaiMemo = Cast.String(old[t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo]);
					cur.DetailAddress = Cast.Int(old[t_kaiin_ido.FIdoJiyuDetail_Address]);
					cur.AddressMemo = Cast.String(old[t_kaiin_ido.FIdoJiyuDetail_AddressMemo]);
					cur.DetailEtc = Cast.Int(old[t_kaiin_ido.FIdoJiyuDetail_Etc]);
					cur.EtcMemo = Cast.String(old[t_kaiin_ido.FIdoJiyuDetail_EtcMemo]);

					DateTime olddate = isEmpty(old[t_kaiin_ido.FIdoDate]) ? DateTime.MinValue : (DateTime)old[t_kaiin_ido.FIdoDate];

					if (cur.GetKey(olddate) != rec.Idos[i].GetKey(rec.DateIdo.Value))
					{
						same = false;
						break;
					}
				}

				if (same == true)
				{
					return false;
				}
			}

			foreach (DataRow old in olds)
			{
				old.Delete();
			}

			for (int i = 0; i < rec.Idos.Count; i++)
			{
				IdoInfo ido = rec.Idos[i];

				DataRow nrow = tbl.NewRow();
				nrow[t_kaiin_ido.FID_Kaiin] = kaiinId;
				nrow[t_kaiin_ido.FID_Ido] = i + 1;
				nrow[t_kaiin_ido.FIdoDate] = rec.DateIdo.Value;
				nrow[t_kaiin_ido.FIdoJiyuKbn] = ido.JiyuKbn;
				nrow[t_kaiin_ido.FIdoJiyuDetail_Ido] = ido.DetailIdo;
				nrow[t_kaiin_ido.FIdoJiyuDetail_Kaiin] = ido.DetailKaiin;
				nrow[t_kaiin_ido.FIdoJiyuDetail_Shozai] = ido.DetailShozai;
				nrow[t_kaiin_ido.FIdoJiyuDetail_Address] = ido.DetailAddress;
				nrow[t_kaiin_ido.FIdoJiyuDetail_Etc] = ido.DetailEtc;
				setValue(nrow, t_kaiin_ido.FIdoJiyuDetail_IdoMemo, ido.IdoMemo);
				setValue(nrow, t_kaiin_ido.FIdoJiyuDetail_KaiinMemo, ido.KaiinMemo);
				setValue(nrow, t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo, ido.ShozaiMemo);
				setValue(nrow, t_kaiin_ido.FIdoJiyuDetail_AddressMemo, ido.AddressMemo);
				setValue(nrow, t_kaiin_ido.FIdoJiyuDetail_EtcMemo, ido.EtcMemo);
				tbl.Rows.Add(nrow);
			}

			return true;
		}

		/// <summary>
		/// 会員の行から読み取った開設主体・病床・併設を、医療機関マスターへ書き込みます。
		/// Excelに値がない項目は変更しません。
		/// </summary>
		/// <returns>更新した医療機関の件数</returns>
		int fillIryoKikan(DataTable tblKikan)
		{
			int cnt = 0;

			foreach (DataRow row in tblKikan.Rows)
			{
				int id = Cast.Int(row[t_iryokikan.FID_Iryokikan]);

				if (shisetsus.ContainsKey(id) == false)
				{
					continue;
				}

				ShisetsuInfo info = shisetsus[id];
				bool changed = false;

				if (info.KaisetsuID != null)
				{
					changed |= setValue(row, t_iryokikan.FIRK_KaisetsuShutai, info.KaisetsuID);
				}

				if (info.ByoshoUmu != null)
				{
					changed |= setValue(row, t_iryokikan.FIRK_ByoshoUmu, info.ByoshoUmu.Value);

					//¶ 2026/11/14 CC 病床なしの場合、許可病床は画面と同じく空にする
					if (info.ByoshoUmu == false)
					{
						changed |= setValue(row, t_iryokikan.FIRK_Kyoka, null);
					}
					else
					if (info.ByoshoSu != null)
					{
						changed |= setValue(row, t_iryokikan.FIRK_Kyoka, info.ByoshoSu);
					}
				}

				changed |= setValue(row, t_iryokikan.FIRK_Kaigo, info.Kaigo);
				changed |= setValue(row, t_iryokikan.FIRK_Etc, info.Etc);
				changed |= setValue(row, t_iryokikan.FIRK_Memo, (info.Etc == true) ? info.EtcName : "");

				if (changed == true)
				{
					row[t_iryokikan.FLastUpdateUser] = AppGlobal.LoginUser.ID;
					cnt++;
				}
			}

			return cnt;
		}
		#endregion

		/// <summary>
		/// 反映後の処理
		/// </summary>
		protected override void afterUpdate()
		{
			if (FillIryoKikan == true)
			{
				// 医療機関情報(共通)の再取得
				AppGlobal.InitIryoKikan();
			}

			// 会員情報(共通)の再取得
			AppGlobal.InitKaiin();
		}
	}
}
