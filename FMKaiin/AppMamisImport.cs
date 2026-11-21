using ComponentDebug;
using ComponentIO;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// MAMIS（日本医師会 会員情報システム）の申請情報CSVを会員情報へ取り込むクラス。
	/// 読込・照合・差分作成（Load）と、反映・履歴保存（Update）に分かれます。
	/// </summary>
	public class AppMamisImport
	{
		/// <summary>処理結果</summary>
		public enum eResult
		{
			/// <summary></summary>
			None,
			/// <summary>成功</summary>
			Success,
			/// <summary>ファイルオープン失敗</summary>
			OpenFileFailed,
			/// <summary>MAMISの申請情報CSVではない</summary>
			NotMamisFile,
			/// <summary>データなし</summary>
			NoData,
			/// <summary>DB更新失敗</summary>
			UpdateFailed,
			/// <summary>予期せぬエラー</summary>
			Error,
		}

		/// <summary>会員ごとの照合結果</summary>
		public enum eStatus
		{
			/// <summary></summary>
			None,
			/// <summary>取込可（医籍登録番号で照合）</summary>
			Ok,
			/// <summary>取込可（氏名・性別・生年月日で照合）</summary>
			OkByName,
			/// <summary>変更なし</summary>
			Same,
			/// <summary>会員なし</summary>
			NotFound,
			/// <summary>不一致</summary>
			Mismatch,
		}

		/// <summary>
		/// 取込項目（1項目分の変更内容）
		/// </summary>
		public class Item
		{
			/// <summary>項目名（画面の項目名）</summary>
			public string Name;
			/// <summary>変更区分</summary>
			public eMamisLogType Type;
			/// <summary>変更前（表示用）</summary>
			public string Before;
			/// <summary>変更後（表示用）</summary>
			public string After;
			/// <summary>t_kaiin の項目名。子テーブルの場合はnull</summary>
			public string Field;
			/// <summary>t_kaiin へセットする値</summary>
			public object Value;
			/// <summary>子テーブル名（診療科・学会）。t_kaiin の項目の場合はnull</summary>
			public string ListTable;
			/// <summary>子テーブルへ登録するID</summary>
			public List<int> ListIDs;
		}

		/// <summary>
		/// 会員ごとの取込内容
		/// </summary>
		public class Person
		{
			/// <summary>No</summary>
			public int No;
			/// <summary>照合結果</summary>
			public eStatus Status;
			/// <summary>注意あり（一部の項目を取り込まない）</summary>
			public bool Warning;
			/// <summary>会員ID</summary>
			public int KaiinID;
			/// <summary>会員コード</summary>
			public int KaiinCD;
			/// <summary>氏名（照合できた場合は登録済みの氏名、できない場合はMAMISの氏名）</summary>
			public string Name;
			/// <summary>医籍登録番号（MAMIS）</summary>
			public string IsekiNo;
			/// <summary>申請区分（MAMIS）</summary>
			public string Shinsei;
			/// <summary>更新日時（MAMIS）</summary>
			public string UpdateDate;
			/// <summary>取込項目</summary>
			public List<Item> Items = new List<Item>();
			/// <summary>注意・取り込めない理由</summary>
			public List<string> Notes = new List<string>();

			/// <summary>取込対象：true</summary>
			public bool IsTarget
			{
				get { return Status == eStatus.Ok || Status == eStatus.OkByName; }
			}

			/// <summary>状況の表示名</summary>
			public string StatusText
			{
				get
				{
					switch (Status)
					{
						case eStatus.Ok:		return (Warning == true) ? "取込可（注意）" : "取込可";
						case eStatus.OkByName:	return "取込可（氏名等で照合）";
						case eStatus.Same:		return "変更なし";
						case eStatus.NotFound:	return "会員なし";
						case eStatus.Mismatch:	return "不一致";
					}
					return "";
				}
			}

			/// <summary>内容の表示（注意・理由があればそれを、なければ項目名の一覧）</summary>
			public string ContentText
			{
				get
				{
					if (Notes.Count > 0)
					{
						return string.Join("／", Notes);
					}

					if (Items.Count <= 3)
					{
						return string.Join("、", Items.Select(x => x.Name));
					}

					return string.Join("、", Items.Take(3).Select(x => x.Name)) + " ほか";
				}
			}
		}

		#region *** CSVの列名 ***
		const string COL_Shinsei = "申請区分";
		const string COL_IsekiNo = "医籍登録番号";
		const string COL_Sei = "氏名_姓";
		const string COL_Mei = "氏名_名";
		const string COL_SeiKana = "氏名_姓_フリガナ";
		const string COL_MeiKana = "氏名_名_フリガナ";
		const string COL_OldSei = "旧姓_姓";
		const string COL_OldMei = "旧姓_名";
		const string COL_OldSeiKana = "旧姓_姓_フリガナ";
		const string COL_OldMeiKana = "旧姓_名_フリガナ";
		const string COL_Birth = "生年月日";
		const string COL_Sex = "性別";
		const string COL_ShinryoMain = "主たる診療科コード";
		const string COL_ShinryoFmt = "担当する診療科{0:00}コード";
		const string COL_ShisetsuName = "所属施設の名称";
		const string COL_Tel = "自宅現住所_TEL";
		const string COL_Fax = "自宅現住所_FAX";
		const string COL_Post = "自宅現住所_郵便番号";
		const string COL_Pref = "自宅現住所_都道府県";
		const string COL_Addr = "自宅現住所_住所";
		const string COL_AddrBuild = "自宅現住所_住所建物名";
		const string COL_Mobile = "携帯電話番号";
		const string COL_Mail = "電子メールアドレス";
		const string COL_ShisetsuGyomu = "施設業務";
		const string COL_Daigaku = "出身大学";
		const string COL_Sotsugyo = "卒業年月";
		const string COL_IsekiDate = "医籍登録日";
		const string COL_Daigakuin = "卒業大学院";
		const string COL_Shuryo = "大学院卒業年月";
		const string COL_Gakui = "学位取得";
		const string COL_GakkaiFmt = "所属学会{0:00}コード";
		const string COL_ShiteiiFmt = "指定医区分{0:00}";
		const string COL_Nyukai = "入会年月日";
		const string COL_NyukaiIshikai3 = "入会_第3階層_医師会コード";
		const string COL_NyukaiKikanTel = "入会_医療機関所在地_電話番号";
		const string COL_TaikaiStatus3 = "退会_第3階層_申請ステータス";
		const string COL_TaikaiDate3 = "退会_第3階層_退会日";
		const string COL_TaikaiHaigyo = "退会事由：廃業・退職";
		const string COL_TaikaiShibo = "退会事由：死亡";
		const string COL_TaikaiEtc = "退会事由：その他";
		const string COL_TaikaiEtcText = "退会事由：その他テキスト";
		const string COL_UpdateDate = "更新日時";
		const string COL_ShinseiDate = "申請日時";

		/// <summary>申請情報CSVと判定するための列</summary>
		static readonly string[] RequiredColumns =
		{
			COL_Shinsei, COL_IsekiNo, COL_Sei, COL_Mei, COL_Birth, COL_Sex, COL_ShinryoMain, COL_ShisetsuName,
			COL_Nyukai, COL_TaikaiDate3, COL_UpdateDate, COL_ShinseiDate,
		};
		#endregion

		#region *** MAMISの値（意味は列名とサンプルからの推定。先方へ確認中） ***
		//¶ 2026/11/21 CC 申請区分・申請ステータスの意味はMAMISの仕様書で未確認。確認後に見直す
		/// <summary>申請区分：入会</summary>
		const string SHINSEI_Nyukai = "01";
		/// <summary>申請区分：退会</summary>
		const string SHINSEI_Taikai = "02";
		/// <summary>申請区分：異動</summary>
		const string SHINSEI_Ido = "03";
		/// <summary>申請区分：他医師会への異動</summary>
		const string SHINSEI_Tenshutsu = "04";
		/// <summary>申請ステータス：承認済</summary>
		const string STATUS_Shonin = "5";
		/// <summary>学会コード：その他（学会名の自由記載）。取込先がないため取り込まない</summary>
		const int GAKKAI_Etc = 999;
		/// <summary>所属施設の名称：所属なし</summary>
		const string SHISETSU_None = "施設なし";
		#endregion

		/// <summary>会員ごとの取込内容（Load後に参照できます）</summary>
		public List<Person> Persons { get; private set; }

		/// <summary>取込ファイル名</summary>
		public string FileName { get; private set; }

		/// <summary>保存した取込履歴ID（Update後に参照できます）</summary>
		public int LogID { get; private set; }

		/// <summary>取込対象の会員数</summary>
		public int TargetCount
		{
			get { return (Persons == null) ? 0 : Persons.Count(x => x.IsTarget == true); }
		}

		DataTable dtSrc;
		eResult execResult;

		// 照合・変換用
		Dictionary<int, List<DataRow>> dicIseki;
		Dictionary<string, List<DataRow>> dicName;
		Dictionary<int, List<int>> dicShinryo;
		Dictionary<int, List<int>> dicGakkai;
		Dictionary<string, int> dicKikanName;
		Dictionary<string, int> dicKikanTel;
		Dictionary<string, DataRow> dicIshikaiCode;
		Dictionary<int, DataRow> dicIshikaiID;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppMamisImport()
		{
			Persons = new List<Person>();
		}

		#region *** 読込・照合・差分作成 ***
		/// <summary>
		/// CSVを読み込み、会員と照合して取込内容を作成します。データベースは更新しません。
		/// </summary>
		/// <param name="path">CSVファイルのパス</param>
		public eResult Load(string path)
		{
			execResult = eResult.Error;
			Persons = new List<Person>();
			FileName = Path.GetFileName(path);

			FormBg_Progress prog = new FormBg_Progress();
			prog.TitleText = "しばらくお待ちください。";
			prog.LabelText = "MAMISデータを読み込んでいます。";
			prog.Arg = path;
			prog.DoWorkEvent += doLoadEvent;
			prog.ShowDialog();
			prog.Dispose();
			prog = null;

			return execResult;
		}

		void doLoadEvent(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;

			try
			{
				execResult = loadFileToTable((string)prog.Arg);
				if (execResult != eResult.Success)
				{
					return;
				}
				prog.AdvanceProgress(30);

				if (dtSrc.Rows.Count == 0)
				{
					execResult = eResult.NoData;
					return;
				}

				initMatching();
				prog.AdvanceProgress(20);

				createPersons();
				prog.AdvanceProgress(50);

				execResult = eResult.Success;
			}
			catch (Exception ex)
			{
				ErrLog.WriteException(ex);
				execResult = eResult.Error;
			}
		}

		/// <summary>
		/// CSVファイルの読み込み処理。MAMISのCSVはUTF-8（BOM付き）。
		/// </summary>
		eResult loadFileToTable(string path)
		{
			dtSrc = new DataTable();

			try
			{
				using (TextFieldParser parser = new TextFieldParser(path, Encoding.UTF8))
				{
					parser.TextFieldType = FieldType.Delimited;
					parser.SetDelimiters(",");

					bool head = true;

					while (parser.EndOfData == false)
					{
						string[] flds = parser.ReadFields();

						if (head == true)
						{
							foreach (string fld in flds)
							{
								string name = fld.Trim();

								//¶ 2026/11/21 CC 同名の列があると追加できないため、申請情報CSVではないとみなす
								if (name == "" || dtSrc.Columns.Contains(name) == true)
								{
									return eResult.NotMamisFile;
								}
								dtSrc.Columns.Add(name, typeof(string));
							}

							foreach (string req in RequiredColumns)
							{
								if (dtSrc.Columns.Contains(req) == false)
								{
									return eResult.NotMamisFile;
								}
							}

							head = false;
							continue;
						}

						DataRow nrow = dtSrc.NewRow();
						for (int i = 0; i < flds.Length && i < dtSrc.Columns.Count; i++)
						{
							nrow[i] = flds[i];
						}
						dtSrc.Rows.Add(nrow);
					}

					if (head == true)
					{
						return eResult.NotMamisFile;
					}
				}

				return eResult.Success;
			}
			catch (Exception ex)
			{
				ErrLog.WriteException(ex);
				dtSrc = null;
				return eResult.OpenFileFailed;
			}
		}

		/// <summary>
		/// 照合用の辞書を作成します。
		/// </summary>
		void initMatching()
		{
			dicIseki = new Dictionary<int, List<DataRow>>();
			dicName = new Dictionary<string, List<DataRow>>();

			foreach (DataRow row in AppGlobal.DB.GetFillTable(TableProp.t_kaiin).Rows)
			{
				int iseki = Cast.Int(row[t_kaiin.FKaiin_IsekiTorokuNo]);
				if (iseki > 0)
				{
					if (dicIseki.ContainsKey(iseki) == false)
					{
						dicIseki.Add(iseki, new List<DataRow>());
					}
					dicIseki[iseki].Add(row);
				}

				string key = getNameKey(Cast.String(row[t_kaiin.FKaiin_Name]), Cast.Int(row[t_kaiin.FKaiin_Sex]), getDate(row[t_kaiin.FKaiin_DateBirth]));
				if (key != null)
				{
					if (dicName.ContainsKey(key) == false)
					{
						dicName.Add(key, new List<DataRow>());
					}
					dicName[key].Add(row);
				}
			}

			dicShinryo = getChildIDs(TableProp.t_kaiin_shinryoka, t_kaiin_shinryoka.FID_Kaiin, t_kaiin_shinryoka.FID_Shinryoka);
			dicGakkai = getChildIDs(TableProp.t_kaiin_gakkai, t_kaiin_gakkai.FID_Kaiin, t_kaiin_gakkai.FID_Gakkai);

			//¶ 2026/11/21 CC 医療機関はコード体系がMAMISと異なるため、名称→電話番号の順で照合する。同じ名称・番号が複数ある場合は使わない
			dicKikanName = new Dictionary<string, int>();
			dicKikanTel = new Dictionary<string, int>();
			foreach (DataRow row in AppGlobal.DB.GetFillTable(TableProp.t_iryokikan).Rows)
			{
				int id = Cast.Int(row[t_iryokikan.FID_Iryokikan]);
				addUniqueKey(dicKikanName, removeSpace(Cast.String(row[t_iryokikan.FIRK_Name])), id);
				addUniqueKey(dicKikanTel, getDigits(Cast.String(row[t_iryokikan.FIRK_Tel1])), id);
			}

			dicIshikaiCode = new Dictionary<string, DataRow>();
			dicIshikaiID = new Dictionary<int, DataRow>();
			foreach (DataRow row in AppGlobal.DB.GetFillTable(TableProp.t_ishikai).Rows)
			{
				string cd = Cast.String(row[t_ishikai.FCD_Ishikai]).Trim();
				if (cd != "" && dicIshikaiCode.ContainsKey(cd) == false)
				{
					dicIshikaiCode.Add(cd, row);
				}

				int id = Cast.Int(row[t_ishikai.FID_Ishikai]);
				if (dicIshikaiID.ContainsKey(id) == false)
				{
					dicIshikaiID.Add(id, row);
				}
			}
		}

		/// <summary>
		/// 重複のないキーだけを辞書へ登録します。重複したキーは 0 にして使えなくします。
		/// </summary>
		void addUniqueKey(Dictionary<string, int> dic, string key, int id)
		{
			if (key == "")
			{
				return;
			}

			if (dic.ContainsKey(key) == true)
			{
				dic[key] = 0;
			}
			else
			{
				dic.Add(key, id);
			}
		}

		/// <summary>
		/// 子テーブルのIDを会員IDごとにまとめます。
		/// </summary>
		Dictionary<int, List<int>> getChildIDs(string table, string fldKaiin, string fldId)
		{
			Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();

			foreach (DataRow row in AppGlobal.DB.GetFillTable(table).Rows)
			{
				int id = Cast.Int(row[fldKaiin]);

				if (dic.ContainsKey(id) == false)
				{
					dic.Add(id, new List<int>());
				}
				dic[id].Add(Cast.Int(row[fldId]));
			}

			return dic;
		}

		/// <summary>
		/// 会員ごとの取込内容を作成します。
		/// </summary>
		void createPersons()
		{
			//¶ 2026/11/21 CC 1人に複数の申請行がある場合は、更新日時が最新の行を使う（ユーザー了承）
			Dictionary<string, DataRow> latest = new Dictionary<string, DataRow>();
			List<string> order = new List<string>();

			foreach (DataRow src in dtSrc.Rows)
			{
				string iseki = get(src, COL_IsekiNo);
				string key = (iseki != "") ? iseki : $"#{dtSrc.Rows.IndexOf(src)}";

				if (latest.ContainsKey(key) == false)
				{
					latest.Add(key, src);
					order.Add(key);
				}
				else
				if ((parseDate(get(src, COL_UpdateDate)) ?? DateTime.MinValue) >= (parseDate(get(latest[key], COL_UpdateDate)) ?? DateTime.MinValue))
				{
					latest[key] = src;
				}
			}

			int no = 0;
			foreach (string key in order)
			{
				DataRow src = latest[key];

				Person p = new Person();
				p.No = ++no;
				p.Name = $"{get(src, COL_Sei)}　{get(src, COL_Mei)}";
				p.IsekiNo = get(src, COL_IsekiNo);
				p.Shinsei = getShinseiName(get(src, COL_Shinsei));
				p.UpdateDate = get(src, COL_UpdateDate);

				DataRow row = matchKaiin(src, p);
				if (row != null)
				{
					p.KaiinID = Cast.Int(row[t_kaiin.FID_Kaiin]);
					p.KaiinCD = Cast.Int(row[t_kaiin.FCD_Kaiin]);
					p.Name = Cast.String(row[t_kaiin.FKaiin_Name]);

					createItems(src, row, p);

					if (p.Items.Count == 0)
					{
						p.Status = eStatus.Same;
					}
				}

				Persons.Add(p);
			}
		}

		/// <summary>
		/// 申請区分の表示名を取得します。
		/// </summary>
		string getShinseiName(string kbn)
		{
			switch (kbn)
			{
				case SHINSEI_Nyukai:	return "入会";
				case SHINSEI_Taikai:	return "退会";
				case SHINSEI_Ido:		return "異動";
				case SHINSEI_Tenshutsu:	return "異動（他医師会）";
			}
			return kbn;
		}

		/// <summary>
		/// MAMISの行に対応する会員を探します。
		/// 医籍登録番号で照合し、一致がなければ氏名・性別・生年月日で照合します（ユーザー了承）。
		/// </summary>
		/// <returns>会員の行。決まらない場合はnull（p.Status と p.Notes に理由を入れます）</returns>
		DataRow matchKaiin(DataRow src, Person p)
		{
			int iseki = Cast.Int(get(src, COL_IsekiNo));
			string name = removeSpace(get(src, COL_Sei) + get(src, COL_Mei));
			int sex = getSex(get(src, COL_Sex));
			DateTime? birth = parseDate(get(src, COL_Birth));

			// 1. 医籍登録番号
			if (iseki > 0 && dicIseki.ContainsKey(iseki) == true)
			{
				DataRow row = chooseRow(dicIseki[iseki]);
				if (row == null)
				{
					p.Status = eStatus.Mismatch;
					p.Notes.Add("同じ医籍登録番号の会員が複数登録されているため、対象を決められません");
					return null;
				}

				//¶ 2026/11/21 CC 番号が一致しても、氏名か生年月日が食い違う場合は取り込まない
				string cname = removeSpace(Cast.String(row[t_kaiin.FKaiin_Name]));
				if (cname != name)
				{
					p.Status = eStatus.Mismatch;
					p.Notes.Add($"氏名が一致しません（登録 {Cast.String(row[t_kaiin.FKaiin_Name])} ／ MAMIS {get(src, COL_Sei)}　{get(src, COL_Mei)}）");
					return null;
				}

				DateTime? cbirth = getDate(row[t_kaiin.FKaiin_DateBirth]);
				if (cbirth != null && birth != null && cbirth.Value != birth.Value)
				{
					p.Status = eStatus.Mismatch;
					p.Notes.Add($"生年月日が一致しません（登録 {formatDate(cbirth, false)} ／ MAMIS {formatDate(birth, false)}）");
					return null;
				}

				p.Status = eStatus.Ok;
				return row;
			}

			// 2. 氏名・性別・生年月日（3項目すべてが一致し、1人に絞れた場合だけ）
			string key = getNameKey(name, sex, birth);
			if (key != null && dicName.ContainsKey(key) == true)
			{
				//¶ 2026/11/21 CC 別の医籍登録番号が登録されている会員は、別人の可能性があるため対象外（ユーザー了承）
				List<DataRow> cands = dicName[key].Where(x => Cast.Int(x[t_kaiin.FKaiin_IsekiTorokuNo]) == 0).ToList();

				if (cands.Count == 0)
				{
					p.Status = eStatus.Mismatch;
					p.Notes.Add("氏名・性別・生年月日が一致する会員はいますが、登録されている医籍登録番号が異なります");
					return null;
				}

				DataRow row = chooseRow(cands);
				if (row == null)
				{
					p.Status = eStatus.Mismatch;
					p.Notes.Add("氏名・性別・生年月日が一致する会員が複数登録されているため、対象を決められません");
					return null;
				}

				p.Status = eStatus.OkByName;
				return row;
			}

			p.Status = eStatus.NotFound;
			p.Notes.Add("医籍登録番号、または氏名・性別・生年月日が一致する会員が登録されていません");
			return null;
		}

		/// <summary>
		/// 候補から対象の会員を1人選びます。退会していない会員を優先し、絞れなければnullを返します（ユーザー了承）。
		/// </summary>
		DataRow chooseRow(List<DataRow> rows)
		{
			List<DataRow> zaiseki = rows.Where(x => Cast.Int(x[t_kaiin.FKaiin_TypeZaiseki]) != (int)eTypeZaiseki.Taikai).ToList();
			List<DataRow> cands = (zaiseki.Count > 0) ? zaiseki : rows;

			return (cands.Count == 1) ? cands[0] : null;
		}

		/// <summary>
		/// 取込項目を作成します。MAMIS側が空欄の項目は、登録済みの値を残します。
		/// </summary>
		void createItems(DataRow src, DataRow row, Person p)
		{
			int id = p.KaiinID;

			// --- 基本情報 ---
			addName(p, row, t_kaiin.FKaiin_Name, "氏名", get(src, COL_Sei), get(src, COL_Mei), "　", false);
			addName(p, row, t_kaiin.FKaiin_NameKana, "フリガナ", get(src, COL_SeiKana), get(src, COL_MeiKana), " ", true);
			addName(p, row, t_kaiin.FKaiin_NameOld, "旧氏名", get(src, COL_OldSei), get(src, COL_OldMei), "　", false);
			addName(p, row, t_kaiin.FKaiin_NameKanaOld, "旧氏名フリガナ", get(src, COL_OldSeiKana), get(src, COL_OldMeiKana), " ", true);

			int sex = getSex(get(src, COL_Sex));
			if (sex > 0)
			{
				addID(p, row, t_kaiin.FKaiin_Sex, "性別", sex, getEnumName(enumKbn.DSex, Cast.Int(row[t_kaiin.FKaiin_Sex])), getEnumName(enumKbn.DSex, sex));
			}

			addDate(p, row, t_kaiin.FKaiin_DateBirth, "生年月日", parseDate(get(src, COL_Birth)), false);
			addText(p, row, t_kaiin.FKaiin_Post, "郵便番号", get(src, COL_Post));
			addText(p, row, t_kaiin.FKaiin_Addr1, "住所", get(src, COL_Pref) + get(src, COL_Addr));
			addText(p, row, t_kaiin.FKaiin_Addr2, "住所（建物名）", get(src, COL_AddrBuild));
			addText(p, row, t_kaiin.FKaiin_Tel1, "電話番号", getTel(get(src, COL_Tel)));
			addText(p, row, t_kaiin.FKaiin_Tel2, "携帯電話番号", getTel(get(src, COL_Mobile)));
			addText(p, row, t_kaiin.FKaiin_Fax, "FAX", getTel(get(src, COL_Fax)));
			addText(p, row, t_kaiin.FKaiin_Mail1, "メールアドレス", get(src, COL_Mail));

			int iseki = Cast.Int(get(src, COL_IsekiNo));
			if (iseki > 0)
			{
				int cur = Cast.Int(row[t_kaiin.FKaiin_IsekiTorokuNo]);
				addID(p, row, t_kaiin.FKaiin_IsekiTorokuNo, "医籍登録番号", iseki, (cur > 0) ? cur.ToString() : "", iseki.ToString());
			}

			addDate(p, row, t_kaiin.FKaiin_DateIsekiToroku, "医籍登録年月日", parseDate(get(src, COL_IsekiDate)), false);
			addText(p, row, t_kaiin.FKaiin_ShusshinKo, "出身校", get(src, COL_Daigaku));
			addDate(p, row, t_kaiin.FKaiin_DateSotsugyo, "卒業年月", parseDate(get(src, COL_Sotsugyo)), true);
			addText(p, row, t_kaiin.FKaiin_ShusshinIn, "出身校（大学院）", get(src, COL_Daigakuin));
			addDate(p, row, t_kaiin.FKaiin_DateShuryo, "修了年月", parseDate(get(src, COL_Shuryo)), true);
			addDate(p, row, t_kaiin.FKaiin_DateShutokuGakui, "学位取得年月", parseDate(get(src, COL_Gakui)), true);
			addDate(p, row, t_kaiin.FKaiin_DateNyukai, "入会年月日", parseDate(get(src, COL_Nyukai)), false);
			addDate(p, row, t_kaiin.FKaiin_DateShinsei, "申請年月日", parseDate(get(src, COL_ShinseiDate)), false);

			// --- 情報1 ---
			string cd_main = get(src, COL_ShinryoMain);
			if (cd_main != "")
			{
				Shinryoka sk = AppGlobal.Shinryokas.GetCode(cd_main);
				if (sk != null)
				{
					addID(p, row, t_kaiin.FID_Shinryoka_Main, "診療科目（主）", sk.ID, getShinryokaName(Cast.Int(row[t_kaiin.FID_Shinryoka_Main])), sk.XRow.SRK_Name);
				}
				else
				{
					addWarning(p, $"診療科コード「{cd_main}」が診療科マスターにないため、診療科目（主）は取り込みません");
				}
			}

			List<int> shinryos = new List<int>();
			for (int i = 1; i <= 9; i++)
			{
				string cd = get(src, string.Format(COL_ShinryoFmt, i));
				if (cd == "")
				{
					continue;
				}

				Shinryoka sk = AppGlobal.Shinryokas.GetCode(cd);
				if (sk == null)
				{
					addWarning(p, $"診療科コード「{cd}」が診療科マスターにないため、取り込みません");
				}
				else
				if (shinryos.Contains(sk.ID) == false)
				{
					shinryos.Add(sk.ID);
				}
			}
			addList(p, "診療科目（担当）", TableProp.t_kaiin_shinryoka, dicShinryo.ContainsKey(id) ? dicShinryo[id] : new List<int>(), shinryos, getShinryokaName);

			addIryoKikan(src, row, p);

			string cd_gyomu = get(src, COL_ShisetsuGyomu);
			if (cd_gyomu != "")
			{
				Shisetsugyomu sg = AppGlobal.Shisetsugyomus.GetCode(cd_gyomu);
				if (sg != null)
				{
					addID(p, row, t_kaiin.FID_ShisetsuGyomu, "施設・業務", sg.ID, getShisetsugyomuName(Cast.Int(row[t_kaiin.FID_ShisetsuGyomu])), getShisetsugyomuName(sg.ID));
				}
				else
				{
					addWarning(p, $"施設業務コード「{cd_gyomu}」が施設業務マスターにないため、施設・業務は取り込みません");
				}
			}

			int shiteii = 0;
			for (int i = 1; i <= 10; i++)
			{
				shiteii |= getShiteii(get(src, string.Format(COL_ShiteiiFmt, i)));
			}
			if (shiteii != 0)
			{
				int cur = Cast.Int(row[t_kaiin.FKaiin_Shiteii]);
				addID(p, row, t_kaiin.FKaiin_Shiteii, "指定医", shiteii, getShiteiiName(cur), getShiteiiName(shiteii));
			}

			// --- 情報3 ---
			List<int> gakkais = new List<int>();
			for (int i = 1; i <= 30; i++)
			{
				string cd = get(src, string.Format(COL_GakkaiFmt, i));
				if (cd == "" || Cast.Int(cd) == GAKKAI_Etc)
				{
					continue;
				}

				Gakkai gk = AppGlobal.Gakkais.GetCode(cd);
				if (gk == null)
				{
					addWarning(p, $"学会コード「{cd}」が学会マスターにないため、取り込みません");
				}
				else
				if (gakkais.Contains(gk.ID) == false)
				{
					gakkais.Add(gk.ID);
				}
			}
			addList(p, "所属学会", TableProp.t_kaiin_gakkai, dicGakkai.ContainsKey(id) ? dicGakkai[id] : new List<int>(), gakkais, getGakkaiName);

			addTaikai(src, row, p);
		}

		/// <summary>
		/// 所属の医療機関。医療機関マスターに登録があれば結び付けます（マスターへの追加はしません。ユーザー指示）。
		/// </summary>
		void addIryoKikan(DataRow src, DataRow row, Person p)
		{
			string name = get(src, COL_ShisetsuName);
			if (name == "" || name == SHISETSU_None)
			{
				return;
			}

			int kid = 0;
			string key = removeSpace(name);
			string tel = getDigits(get(src, COL_NyukaiKikanTel));

			if (dicKikanName.ContainsKey(key) == true)
			{
				kid = dicKikanName[key];
			}
			if (kid == 0 && tel != "" && dicKikanTel.ContainsKey(tel) == true)
			{
				kid = dicKikanTel[tel];
			}

			if (kid == 0)
			{
				addWarning(p, $"所属の医療機関「{name}」が医療機関マスターにないため、医療機関は取り込みません");
				return;
			}

			addID(p, row, t_kaiin.FID_Iryokikan, "医療機関", kid, getIryoKikanName(Cast.Int(row[t_kaiin.FID_Iryokikan])), getIryoKikanName(kid));
		}

		/// <summary>
		/// 退会年月日・退会事由・異動先医師会。承認済の退会申請（他医師会への異動を含む）の場合だけ取り込みます。
		/// </summary>
		void addTaikai(DataRow src, DataRow row, Person p)
		{
			string kbn = get(src, COL_Shinsei);

			if ((kbn != SHINSEI_Taikai && kbn != SHINSEI_Tenshutsu) || get(src, COL_TaikaiStatus3) != STATUS_Shonin)
			{
				return;
			}

			addDate(p, row, t_kaiin.FKaiin_DateTaikai, "退会年月日", parseDate(get(src, COL_TaikaiDate3)), false);

			eTypeTaikaiJiyu jiyu = eTypeTaikaiJiyu.None;
			if (kbn == SHINSEI_Tenshutsu)
			{
				jiyu = eTypeTaikaiJiyu.Ido;
			}
			else
			if (get(src, COL_TaikaiShibo) == "1")
			{
				jiyu = eTypeTaikaiJiyu.Shibo;
			}
			else
			if (get(src, COL_TaikaiHaigyo) == "1")
			{
				jiyu = eTypeTaikaiJiyu.Haiki;
			}
			else
			if (get(src, COL_TaikaiEtc) == "1")
			{
				jiyu = eTypeTaikaiJiyu.Etc;
			}

			if (jiyu != eTypeTaikaiJiyu.None)
			{
				addID(p, row, t_kaiin.FKaiin_TypeTaikaiJiyu, "退会事由", (int)jiyu, getEnumName(enumKbn.DTypeTaikaiJiyu, Cast.Int(row[t_kaiin.FKaiin_TypeTaikaiJiyu])), getEnumName(enumKbn.DTypeTaikaiJiyu, (int)jiyu));
			}

			if (jiyu == eTypeTaikaiJiyu.Etc)
			{
				addText(p, row, t_kaiin.FKaiin_TaikaiEtcmemo, "退会事由：その他の内容", get(src, COL_TaikaiEtcText));
			}

			if (jiyu == eTypeTaikaiJiyu.Ido)
			{
				string cd = get(src, COL_NyukaiIshikai3);
				if (cd != "")
				{
					if (dicIshikaiCode.ContainsKey(cd) == true)
					{
						int iid = Cast.Int(dicIshikaiCode[cd][t_ishikai.FID_Ishikai]);
						addID(p, row, t_kaiin.FID_Ishikai_Idosaki, "異動先医師会", iid, getIshikaiName(Cast.Int(row[t_kaiin.FID_Ishikai_Idosaki])), getIshikaiName(iid));
					}
					else
					{
						addWarning(p, $"医師会コード「{cd}」が医師会マスターにないため、異動先医師会は取り込みません");
					}
				}
			}
		}
		#endregion

		#region *** 取込項目の追加 ***
		void addItem(Person p, Item item)
		{
			item.Type = (item.Before == "") ? eMamisLogType.Add : eMamisLogType.Replace;
			p.Items.Add(item);
		}

		void addWarning(Person p, string msg)
		{
			p.Warning = true;
			if (p.Notes.Contains(msg) == false)
			{
				p.Notes.Add(msg);
			}
		}

		/// <summary>
		/// 文字の項目
		/// </summary>
		void addText(Person p, DataRow row, string fld, string title, string val)
		{
			string cur = Cast.String(row[fld]).Trim();

			if (val == "" || cur == val)
			{
				return;
			}

			addItem(p, new Item() { Name = title, Field = fld, Value = val, Before = cur, After = val });
		}

		/// <summary>
		/// 氏名・フリガナ。空白と全角半角の違いだけの場合は変更なしとみなします。
		/// </summary>
		/// <param name="narrow">半角へ変換して登録する：true</param>
		void addName(Person p, DataRow row, string fld, string title, string sei, string mei, string separator, bool narrow)
		{
			string cur = Cast.String(row[fld]).Trim();
			string val = (sei + separator + mei).Trim(' ', '　');

			//¶ 2026/11/21 CC フリガナは登録済みデータに合わせて半角カナで登録する（MAMISは全角カナ）
			if (narrow == true)
			{
				val = StrConv.ToNarrow(val);
			}

			if (val == "" || normalize(cur) == normalize(val))
			{
				return;
			}

			addItem(p, new Item() { Name = title, Field = fld, Value = val, Before = cur, After = val });
		}

		/// <summary>
		/// 日付の項目
		/// </summary>
		/// <param name="ym">年月までの項目：true（1日付で登録し、年月が同じなら変更なし）</param>
		void addDate(Person p, DataRow row, string fld, string title, DateTime? val, bool ym)
		{
			if (val == null)
			{
				return;
			}

			DateTime? cur = getDate(row[fld]);

			if (cur != null)
			{
				if (cur.Value == val.Value || (ym == true && cur.Value.Year == val.Value.Year && cur.Value.Month == val.Value.Month))
				{
					return;
				}
			}

			addItem(p, new Item() { Name = title, Field = fld, Value = val.Value, Before = formatDate(cur, ym), After = formatDate(val, ym) });
		}

		/// <summary>
		/// 数値・区分・IDの項目
		/// </summary>
		void addID(Person p, DataRow row, string fld, string title, int val, string before, string after)
		{
			if (Cast.Int(row[fld]) == val)
			{
				return;
			}

			addItem(p, new Item() { Name = title, Field = fld, Value = val, Before = before, After = after });
		}

		/// <summary>
		/// 子テーブルの項目（診療科・学会）。並び順の違いだけの場合は変更なしとみなします。
		/// </summary>
		void addList(Person p, string title, string table, List<int> olds, List<int> news, Func<int, string> getName)
		{
			if (news.Count == 0)
			{
				return;
			}

			if (olds.Count == news.Count && olds.All(x => news.Contains(x)) == true)
			{
				return;
			}

			addItem(p, new Item()
			{
				Name = title,
				ListTable = table,
				ListIDs = news,
				Before = string.Join("、", olds.Select(x => getName(x)).Where(x => x != "")),
				After = string.Join("、", news.Select(x => getName(x))),
			});
		}
		#endregion

		#region *** 値の変換 ***
		string get(DataRow src, string col)
		{
			if (src.Table.Columns.Contains(col) == false)
			{
				return "";
			}
			return Cast.String(src[col]).Trim(' ', '　', '\t');
		}

		string removeSpace(string str)
		{
			return str.Replace(" ", "").Replace("　", "");
		}

		/// <summary>
		/// 比較用に、空白を除いて半角へそろえます。
		/// </summary>
		string normalize(string str)
		{
			return StrConv.ToNarrow(removeSpace(str));
		}

		string getDigits(string str)
		{
			return Regex.Replace(StrConv.ToNarrow(str), "[^0-9]", "");
		}

		/// <summary>
		/// 電話番号。数字が9桁に満たない値は入力誤りとみなして空にします。
		/// </summary>
		string getTel(string str)
		{
			return (getDigits(str).Length >= 9) ? str : "";
		}

		int getSex(string str)
		{
			switch (str)
			{
				case "男": return (int)eSex.Men;
				case "女": return (int)eSex.Women;
			}
			return 0;
		}

		/// <summary>
		/// 氏名・性別・生年月日の照合キー。どれかが欠けている場合はnull。
		/// </summary>
		string getNameKey(string name, int sex, DateTime? birth)
		{
			name = removeSpace(name);

			if (name == "" || sex == 0 || birth == null)
			{
				return null;
			}

			return $"{name}|{sex}|{birth.Value:yyyyMMdd}";
		}

		DateTime? getDate(object value)
		{
			if (value == null || value == DBNull.Value)
			{
				return null;
			}
			return Cast.DateTime(value).Date;
		}

		/// <summary>
		/// MAMISの日付（yyyy/MM/dd、yyyy/MM、yyyy/MM/dd HH:mm:ss）を読み取ります。年月だけの場合は1日とします。
		/// 更新日時の比較に使うため、時刻は残します。
		/// </summary>
		DateTime? parseDateTime(string str)
		{
			string[] fmts = { "yyyy/MM/dd HH:mm:ss", "yyyy/M/d H:mm:ss", "yyyy/MM/dd", "yyyy/M/d", "yyyy/MM", "yyyy/M" };
			DateTime dt;

			if (str != "" && DateTime.TryParseExact(str, fmts, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) == true)
			{
				return dt;
			}
			return null;
		}

		DateTime? parseDate(string str)
		{
			DateTime? dt = parseDateTime(str);

			//¶ 2026/11/21 CC 更新日時どうしの比較は時刻まで必要なため、更新日時の列はparseDateTimeの値をそのまま使う
			if (dt != null && str.Contains(":") == false)
			{
				return dt.Value.Date;
			}
			return dt;
		}

		string formatDate(DateTime? dt, bool ym)
		{
			if (dt == null)
			{
				return "";
			}
			return dt.Value.ToString((ym == true) ? "yyyy/MM" : "yyyy/MM/dd");
		}

		string getEnumName(Dictionary<int, string> dic, int val)
		{
			return (dic.ContainsKey(val) == true) ? dic[val] : "";
		}

		string getShinryokaName(int id)
		{
			Shinryoka obj = AppGlobal.Shinryokas.Get(id);
			return (obj == null) ? "" : obj.XRow.SRK_Name;
		}

		string getGakkaiName(int id)
		{
			Gakkai obj = AppGlobal.Gakkais.Get(id);
			return (obj == null) ? "" : obj.XRow.GKAI_Name;
		}

		string getShisetsugyomuName(int id)
		{
			Shisetsugyomu obj = AppGlobal.Shisetsugyomus.Get(id);
			if (obj == null)
			{
				return "";
			}

			string disp = Cast.String(obj.XRow.Row[t_shisetsugyomu.FSGY_DispName]).Trim();
			return (disp != "") ? disp : Cast.String(obj.XRow.Row[t_shisetsugyomu.FSGY_Name]).Trim();
		}

		string getIryoKikanName(int id)
		{
			IryoKikan obj = AppGlobal.IryoKikans.Get(id);
			return (obj == null) ? "" : obj.XRow.IRK_Name;
		}

		string getIshikaiName(int id)
		{
			return (dicIshikaiID.ContainsKey(id) == true) ? Cast.String(dicIshikaiID[id][t_ishikai.FISK_Name]) : "";
		}

		/// <summary>
		/// 指定医区分（MAMISのコード）を、指定医のビットへ変換します。
		/// </summary>
		int getShiteii(string cd)
		{
			if (cd == "")
			{
				return 0;
			}

			switch (Cast.Int(cd))
			{
				case 1: return (int)AppConst.eShiteii.Hokeni;
				case 2: return (int)AppConst.eShiteii.Botai;
				case 3: return (int)AppConst.eShiteii.Seishin;
				case 4: return (int)AppConst.eShiteii.Seikatsu;
				case 5: return (int)AppConst.eShiteii.Rosai;
				case 6: return (int)AppConst.eShiteii.Kekkaku;
				case 7: return (int)AppConst.eShiteii.Shogai;
				case 8: return (int)AppConst.eShiteii.Genbaku;
				case 9: return (int)AppConst.eShiteii.Nanbyo;
				//¶ 2026/11/21 CC その他はコードマスターでは10、サンプルCSVでは99
				case 10:
				case 99: return (int)AppConst.eShiteii.Etc;
			}
			return 0;
		}

		string getShiteiiName(int val)
		{
			List<string> names = new List<string>();

			if ((val & (int)AppConst.eShiteii.Hokeni) != 0) names.Add("保険医");
			if ((val & (int)AppConst.eShiteii.Botai) != 0) names.Add("母体保護法");
			if ((val & (int)AppConst.eShiteii.Seishin) != 0) names.Add("精神保健");
			if ((val & (int)AppConst.eShiteii.Seikatsu) != 0) names.Add("生活保護");
			if ((val & (int)AppConst.eShiteii.Rosai) != 0) names.Add("労災");
			if ((val & (int)AppConst.eShiteii.Kekkaku) != 0) names.Add("結核");
			if ((val & (int)AppConst.eShiteii.Shogai) != 0) names.Add("身体障碍者");
			if ((val & (int)AppConst.eShiteii.Genbaku) != 0) names.Add("原爆一般");
			if ((val & (int)AppConst.eShiteii.Nanbyo) != 0) names.Add("難病");
			if ((val & (int)AppConst.eShiteii.Etc) != 0) names.Add("その他");

			return string.Join("、", names);
		}
		#endregion

		#region *** 反映・履歴保存 ***
		/// <summary>
		/// 取込可の会員をデータベースへ反映し、取込履歴を保存します。
		/// </summary>
		public eResult Update()
		{
			execResult = eResult.Error;
			LogID = 0;

			FormBg_Progress prog = new FormBg_Progress();
			prog.TitleText = "しばらくお待ちください。";
			prog.LabelText = "MAMISデータを取り込んでいます。";
			prog.DoWorkEvent += doUpdateEvent;
			prog.ShowDialog();
			prog.Dispose();
			prog = null;

			if (execResult == eResult.Success)
			{
				// 会員情報(共通)の再取得
				AppGlobal.InitKaiin();
			}

			return execResult;
		}

		void doUpdateEvent(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;

			try
			{
				//¶ 2026/11/21 CC 会員一覧（FormKaiin）から呼ばれた場合、会員テーブルは一覧のグリッドに結び付いている。
				//¶ 2026/11/21 CC 別スレッドから行を書き換えるとグリッドへの通知も別スレッドで起きるため、更新は画面側のスレッドで行う
				bool ret = (bool)prog.Invoke(new Func<bool>(() => updateData(prog)));

				execResult = (ret == true) ? eResult.Success : eResult.UpdateFailed;
			}
			catch (Exception ex)
			{
				ErrLog.WriteException(ex);
				execResult = eResult.Error;
			}
		}

		bool updateData(FormBg_Progress prog)
		{
			List<Person> targets = Persons.Where(x => x.IsTarget == true && x.Items.Count > 0).ToList();
			if (targets.Count == 0)
			{
				return false;
			}

			string[] names =
			{
				TableProp.t_kaiin, TableProp.t_kaiin_shinryoka, TableProp.t_kaiin_gakkai,
				TableProp.t_mamis_log, TableProp.t_mamis_log_detail,
			};

			Dictionary<string, DataTable> tbls = new Dictionary<string, DataTable>();
			foreach (string name in names)
			{
				tbls.Add(name, AppGlobal.DB.GetFillTable(name));
			}

			try
			{
				Dictionary<int, DataRow> dicKaiin = new Dictionary<int, DataRow>();
				foreach (DataRow row in tbls[TableProp.t_kaiin].Rows)
				{
					int id = Cast.Int(row[t_kaiin.FID_Kaiin]);
					if (dicKaiin.ContainsKey(id) == false)
					{
						dicKaiin.Add(id, row);
					}
				}

				// 取込履歴
				DataTable tblLog = tbls[TableProp.t_mamis_log];
				DataTable tblDetail = tbls[TableProp.t_mamis_log_detail];

				int logid = 0;
				foreach (DataRow row in tblLog.Rows)
				{
					logid = Math.Max(logid, Cast.Int(row[t_mamis_log.FID_MamisLog]));
				}
				logid++;

				int no = 0;
				int cnt = 0;

				foreach (Person p in targets)
				{
					if (dicKaiin.ContainsKey(p.KaiinID) == false)
					{
						//¶ 2026/11/21 CC 確認画面の表示中に会員が削除された場合
						ErrLog.WriteLine("MAMIS取込：会員ID{0}が存在しません。", p.KaiinID);
						rejectChanges(tbls);
						return false;
					}

					DataRow row = dicKaiin[p.KaiinID];

					foreach (Item item in p.Items)
					{
						if (item.Field != null)
						{
							row[item.Field] = item.Value;
						}
						else
						{
							updateList(tbls[item.ListTable], item.ListTable, p.KaiinID, item.ListIDs);
						}

						DataRow drow = tblDetail.NewRow();
						drow[t_mamis_log_detail.FID_MamisLog] = logid;
						drow[t_mamis_log_detail.FMLD_No] = ++no;
						drow[t_mamis_log_detail.FID_Kaiin] = p.KaiinID;
						drow[t_mamis_log_detail.FCD_Kaiin] = p.KaiinCD;
						drow[t_mamis_log_detail.FMLD_KaiinName] = p.Name;
						drow[t_mamis_log_detail.FMLD_ItemName] = item.Name;
						drow[t_mamis_log_detail.FMLD_Type] = (int)item.Type;
						drow[t_mamis_log_detail.FMLD_Before] = item.Before;
						drow[t_mamis_log_detail.FMLD_After] = item.After;
						tblDetail.Rows.Add(drow);
					}

					row[t_kaiin.FLastUpdateUser] = AppGlobal.LoginUser.ID;

					cnt++;
					prog.SetProgress(cnt * 70 / targets.Count);
				}

				DataRow lrow = tblLog.NewRow();
				lrow[t_mamis_log.FID_MamisLog] = logid;
				lrow[t_mamis_log.FMLG_Date] = DateTime.Now;
				lrow[t_mamis_log.FID_Tanto] = AppGlobal.LoginUser.ID;
				lrow[t_mamis_log.FMLG_TantoName] = AppGlobal.LoginUser.Name;
				lrow[t_mamis_log.FMLG_FileName] = FileName;
				lrow[t_mamis_log.FMLG_CntKaiin] = targets.Count;
				lrow[t_mamis_log.FMLG_CntItem] = no;
				tblLog.Rows.Add(lrow);

				//¶ 2026/11/21 CC 会員本体を先に、子テーブルと履歴を後に更新する
				foreach (string name in names)
				{
					if (AppGlobal.DB.UpdateTable(name) == false)
					{
						rejectChanges(tbls);
						return false;
					}
				}
				prog.SetProgress(100);

				LogID = logid;

				return true;
			}
			catch
			{
				//¶ 2026/11/21 CC 途中で失敗した場合は、バッファ上の変更を取り消してから呼び出し元へ伝える
				rejectChanges(tbls);
				throw;
			}
		}

		/// <summary>
		/// 子テーブル（診療科・学会）を入れ直します。
		/// </summary>
		void updateList(DataTable tbl, string table, int kaiinId, List<int> ids)
		{
			string fldKaiin = t_kaiin_shinryoka.FID_Kaiin;
			string fldId = t_kaiin_shinryoka.FID_Shinryoka;

			if (table == TableProp.t_kaiin_gakkai)
			{
				fldKaiin = t_kaiin_gakkai.FID_Kaiin;
				fldId = t_kaiin_gakkai.FID_Gakkai;
			}

			foreach (DataRow old in tbl.Select())
			{
				if (Cast.Int(old[fldKaiin]) == kaiinId)
				{
					old.Delete();
				}
			}

			foreach (int id in ids)
			{
				DataRow nrow = tbl.NewRow();
				nrow[fldKaiin] = kaiinId;
				nrow[fldId] = id;
				tbl.Rows.Add(nrow);
			}
		}

		void rejectChanges(Dictionary<string, DataTable> tbls)
		{
			foreach (DataTable tbl in tbls.Values)
			{
				tbl.RejectChanges();
			}
		}
		#endregion
	}
}
