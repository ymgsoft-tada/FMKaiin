using ComponentDB;
using ComponentFile;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// 銀行用引落データファイル作成クラス（全銀協規定形式 預金口座振替・120byte固定長）
	/// </summary>
	public class AppBankFile
	{
		/// <summary>
		/// 処理結果
		/// </summary>
		public enum eResut
		{
			/// <summary></summary>
			None,
			/// <summary>成功</summary>
			Success,
			/// <summary>ファイルに保存出来ない</summary>
			CannotFileSave,
			/// <summary>該当データがない</summary>
			DataNotFound,
			/// <summary>
			/// データの整合性が取れていない
			/// </summary>
			UnmatchData,
		}

		/// <summary>
		/// 出力ファイル名
		/// </summary>
		public const string BankFileName = "ZENGIN.TXT";

		/// <summary>委託者名（ヘッダーレコード）</summary>
		const string ItakushaName = "ｼﾔﾀﾞﾝﾎｳｼﾞﾝﾌｼﾞｻﾜｼｲｼｶｲ";

		/// <summary>種別コード 91:預金口座振替</summary>
		const int ShubetsuHikiotoshi = 91;
		/// <summary>
		/// コード区分。全銀協規定では 0:JIS、1:EBCDIC。
		/// 送付先（神奈川県医師信用組合）で受け付けられている実績データに合わせて 1 とする。
		/// </summary>
		const int KbnCode		= 1;
		/// <summary>新規コード 0:その他</summary>
		const int NewCodeOther	= 0;
		const int RecordHeader	= 1;
		const int RecordData	= 2;
		const int RecordTrailer	= 8;
		const int RecordEnd		= 9;
		/// <summary>1レコードのバイト数</summary>
		const int RecordLength	= 120;

		const string BlankSpace = " ";

		/// <summary>出力文字コード（半角カナは1byte）</summary>
		static readonly Encoding SJis = Encoding.GetEncoding("Shift_JIS");

		/// <summary>テキスト内容</summary>
		StringBuilder sb;
		/// <summary>レコードの開始位置（レコード長チェック用）</summary>
		int recordStart;
		/// <summary>レコード長が不正なレコードの件数</summary>
		int cnt_lengthError;
		/// <summary>保存先</summary>
		string file;
		eResut result;
		DateTime ym;
		DateTime hikiotoshibi;
		Bank bank;

		/// <summary>
		/// ログ情報を取得します。
		/// </summary>
		public LogTable Logs { get; private set; }

		/// <summary>
		/// 引落口座（会員情報の口座①〜③のいずれか）
		/// </summary>
		class Koza
		{
			public int?		BankCode;
			public int?		ShitenCode;
			public eTypeKoza	KozaType;
			public string	KozaNo;
			public string	KozaName;
		}

		/// <summary>
		/// 引落レコード（会員×支払区分 単位で明細を合算したもの）
		/// </summary>
		class HikiRecord
		{
			public int			ID_Kaiin;
			public t_kaiin		Kaiin;
			public eShiharai	Shiharai;
			public Koza			Koza;
			public decimal		Cost;
		}

		/// <summary>
		/// 引落データを作成します。
		/// </summary>
		/// <param name="_ym">月度</param>
		/// <param name="_bank">引落元銀行（委託者口座）</param>
		/// <param name="_hikiotoshibi">引落日</param>
		public eResut CreateHikiotoshi(DateTime _ym, Bank _bank, DateTime _hikiotoshibi)
		{
			result			= eResut.None;
			ym				= _ym;
			bank			= _bank;
			hikiotoshibi	= _hikiotoshibi;

			if (file != null)
			{
				FormBg_Progress prog = new FormBg_Progress();

				prog.TitleText = "しばらくお待ちください。";
				prog.LabelText = "FBデータを生成しています。";
				prog.DoWorkEventEx += prog_DoWorkEventEx;
				prog.ShowDialog();
				prog.Dispose();
				prog = null;
			}

			return result;
		}

		/// <summary>
		/// 引落データ生成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void prog_DoWorkEventEx(object sender, DoWorkEventArgsEx e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;

			sb					= new StringBuilder();
			cnt_lengthError		= 0;
			Logs				= new LogTable();

			Logs.Apend(LogTable.eStatus.None, "FBデータの作成処理を開始します。");

			prog.SetProgress(0);

			// ヘッダーレコード
			beginRecord();
			appendKbnData(RecordHeader);							// 1 データ区分
			appendShubetsu(ShubetsuHikiotoshi);						// 2 種別コード
			appendKbnCode(KbnCode);									// 3 コード区分
			appendCodeKaisha(bank.XRow.BNK_CodeCompany);			// 4 委託者コード
			appendItakushaName(ItakushaName);						// 5 委託者名
			appendHikiotoshibi(hikiotoshibi);						// 6 引落日
			appendBankCode(bank.XRow.BNK_Code);						// 7 取引銀行番号
			appendBankName(getBankName(bank.XRow.BNK_Code));								// 8 取引銀行名 ※t_bankには名称が保存されないため銀行コードマスタから取得
			appendShitenCode(bank.XRow.BNK_CodeShiten);										// 9 取引支店番号
			appendShitenName(getShitenName(bank.XRow.BNK_Code, bank.XRow.BNK_CodeShiten));	// 10 取引支店名
			appendKamoku(bank.XRow.BNK_KozaType);					// 11 預金種目
			appendKozaNo(bank.XRow.BNK_KozaNo);						// 12 口座番号
			appendDummy(17);										// 13 ダミー
			endRecord();

			Logs.Apend(LogTable.eStatus.None, "ヘッダーレコードを作成しました。");

			// 該当月の引落明細と会員情報を取得
			// ※ 両テーブルに同名の列（ID_Kaiin 等）があるため「*」は使わず、必要な列だけを明示する
			string[] kaiin_fields = new string[]
			{
				t_kaiin.FCD_Kaiin, t_kaiin.FKaiin_Name,
				t_kaiin.FKaiin_BankCode1, t_kaiin.FKaiin_BankShitenCode1, t_kaiin.FKaiin_BankKozaType1, t_kaiin.FKaiin_BankKozaNo1, t_kaiin.FKaiin_BankKozaName1,
				t_kaiin.FKaiin_BankCode2, t_kaiin.FKaiin_BankShitenCode2, t_kaiin.FKaiin_BankKozaType2, t_kaiin.FKaiin_BankKozaNo2, t_kaiin.FKaiin_BankKozaName2,
				t_kaiin.FKaiin_BankCode3, t_kaiin.FKaiin_BankShitenCode3, t_kaiin.FKaiin_BankKozaType3, t_kaiin.FKaiin_BankKozaNo3, t_kaiin.FKaiin_BankKozaName3,
			};
			string kaiin_select = string.Join(", ", kaiin_fields.Select(f => $"{TableProp.t_kaiin}.{f}"));

			string sql = $"SELECT " +
						 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin}, " +
						 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Shiharai}, " +
						 $"{TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Cost}, " +
						 $"{kaiin_select} " +
						 $"FROM {TableProp.t_hikiotoshi} " +
						 $"LEFT JOIN {TableProp.t_kaiin} ON {TableProp.t_hikiotoshi}.{t_hikiotoshi.FID_Kaiin} = {TableProp.t_kaiin}.{t_kaiin.FID_Kaiin} " +
						 $"WHERE ({TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_DateYM} = #{ym}#) " +
						 $"ORDER BY {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, {TableProp.t_hikiotoshi}.{t_hikiotoshi.FHiki_Shiharai}";

			DataTable dt = AppGlobal.DB.FillQuery(sql);

			int cnt_detail		= dt.Rows.Count;	// 明細件数
			int cnt_taishogai	= 0;				// 口座引落以外の明細件数
			int cnt_error		= 0;				// 不整合のあった引落レコード数
			int cnt_zero		= 0;				// 金額が0円以下で除外した引落レコード数
			int cnt_record		= 0;				// 出力した引落レコード数
			decimal total		= 0;				// 合計金額

			// 会員×支払区分 単位で明細を合算
			Dictionary<string, HikiRecord>	dics	= new Dictionary<string, HikiRecord>();
			List<HikiRecord>				records	= new List<HikiRecord>();

			for (int i = 0; i < cnt_detail; i++)
			{
				t_hikiotoshi hrow = new t_hikiotoshi(dt.Rows[i]);
				t_kaiin		 krow = new t_kaiin(dt.Rows[i]);

				eShiharai shiharai = hrow.Hiki_Shiharai;

				// 銀行以外の支払区分は引落対象外
				if (shiharai != eShiharai.Koza1 && shiharai != eShiharai.Koza2 && shiharai != eShiharai.Koza3)
				{
					cnt_taishogai++;
					continue;
				}

				string key = $"{hrow.ID_Kaiin}_{(int)shiharai}";

				if (dics.ContainsKey(key) == false)
				{
					HikiRecord nrec = new HikiRecord();
					nrec.ID_Kaiin	= hrow.ID_Kaiin;
					nrec.Kaiin		= krow;
					nrec.Shiharai	= shiharai;
					nrec.Koza		= getKoza(krow, shiharai);
					nrec.Cost		= 0;

					dics.Add(key, nrec);
					records.Add(nrec);
				}

				dics[key].Cost += hrow.Hiki_Cost;
			}

			if (cnt_taishogai > 0)
			{
				Logs.Apend(LogTable.eStatus.None, $"口座引落以外の明細{cnt_taishogai}件は対象外としました。");
			}

			// データレコード
			for (int i = 0; i < records.Count; i++)
			{
				HikiRecord rec = records[i];

				prog.SetProgress((i + 1) * 100 / records.Count);

				// 会員情報が存在しない
				if (rec.Kaiin.CD_Kaiin_Null == null)
				{
					Logs.Apend(LogTable.eStatus.Warning, $"会員ID:{rec.ID_Kaiin}の会員情報が存在しません。");
					cnt_error++;
					continue;
				}

				string who = $"{rec.Kaiin.CD_Kaiin}:{rec.Kaiin.Kaiin_Name}（{enumKbn.DShiharai[(int)rec.Shiharai]}）";

				// 金額が0円以下は除外
				if (rec.Cost <= 0)
				{
					Logs.Apend(LogTable.eStatus.Warning, $"{who}の引落金額が0円以下のため対象外としました。");
					cnt_zero++;
					continue;
				}

				// 不整合チェック
				bool check = true;
				if (rec.Koza.BankCode == null)
				{
					Logs.Apend(LogTable.eStatus.Warning, $"{who}の銀行コードが登録されていません。");
					check = false;
				}
				if (rec.Koza.ShitenCode == null)
				{
					Logs.Apend(LogTable.eStatus.Warning, $"{who}の支店コードが登録されていません。");
					check = false;
				}
				if (rec.Koza.KozaType == eTypeKoza.None)
				{
					Logs.Apend(LogTable.eStatus.Warning, $"{who}の口座区分が登録されていません。");
					check = false;
				}
				if (rec.Koza.KozaNo == "")
				{
					Logs.Apend(LogTable.eStatus.Warning, $"{who}の口座番号が登録されていません。");
					check = false;
				}
				if (rec.Koza.KozaName == "")
				{
					Logs.Apend(LogTable.eStatus.Warning, $"{who}の口座名が登録されていません。");
					check = false;
				}

				if (check == false)
				{
					cnt_error++;
					continue;
				}

				beginRecord();
				appendKbnData(RecordData);														// 1 データ区分
				appendBankCode(rec.Koza.BankCode.Value);										// 2 引落銀行番号
				appendBankName(getBankName(rec.Koza.BankCode.Value));							// 3 引落銀行名
				appendShitenCode(rec.Koza.ShitenCode.Value);									// 4 引落支店番号
				appendShitenName(getShitenName(rec.Koza.BankCode.Value, rec.Koza.ShitenCode.Value));	// 5 引落支店名
				appendDummy(4);																	// 6 ダミー
				appendKamoku(rec.Koza.KozaType);												// 7 預金種目
				appendKozaNo(rec.Koza.KozaNo);													// 8 口座番号
				appendYokinshaName(rec.Koza.KozaName);											// 9 預金者名
				appendCost(rec.Cost);															// 10 引落金額
				appendNewCode(NewCodeOther);													// 11 新規コード
				appendKokyakuNo(rec.Kaiin.CD_Kaiin);											// 12 顧客番号
				appendDummy(9);																	// 13 振替結果コード（請求時は空白）＋ 14 ダミー
				endRecord();

				cnt_record++;
				total += rec.Cost;
			}

			if (cnt_detail == 0)
			{
				Logs.Apend(LogTable.eStatus.Warning, $"{AppDate.GetYearMonth(ym)}度の引落明細データが作成されていません。");
				result = eResut.DataNotFound;
			}
			else
			if (cnt_error > 0)
			{
				Logs.Apend(LogTable.eStatus.Warning, $"{cnt_error}件の引落データに不整合があります。内容を確認してください。");
				result = eResut.UnmatchData;
			}
			else
			if (cnt_record == 0)
			{
				Logs.Apend(LogTable.eStatus.Warning, $"{AppDate.GetYearMonth(ym)}度に口座引落の対象となる明細がありません。");
				result = eResut.DataNotFound;
			}
			else
			{
				if (cnt_zero > 0)
				{
					Logs.Apend(LogTable.eStatus.None, $"引落金額が0円以下の{cnt_zero}件は対象外としました。");
				}
				Logs.Apend(LogTable.eStatus.None, $"{cnt_record}件の引落データレコードを作成しました。合計金額：{total:#,0}円");

				// トレーラーレコード
				beginRecord();
				appendKbnData(RecordTrailer);	// 1 データ区分
				appendCount(cnt_record);		// 2 合計件数
				appendTotalCost(total);			// 3 合計金額
				appendZero(6);					// 4 振替済件数
				appendZero(12);					// 5 振替済金額
				appendZero(6);					// 6 振替不能件数
				appendZero(12);					// 7 振替不能金額
				appendDummy(65);				// 8 ダミー
				endRecord();

				// エンドレコード
				beginRecord();
				appendKbnData(RecordEnd);
				appendDummy(119);
				endRecord();

				Logs.Apend(LogTable.eStatus.None, "トレーラー・エンドレコードを作成しました。");

				if (cnt_lengthError > 0)
				{
					Logs.Apend(LogTable.eStatus.Error, $"{cnt_lengthError}件のレコード長が{RecordLength}byteになっていません。口座名などに半角カナ以外の文字が含まれていないか確認してください。");
					result = eResut.UnmatchData;
				}
				else
				{
					Logs.Apend(LogTable.eStatus.None, $"FBデータファイルを作成します。");
					Logs.Apend(LogTable.eStatus.None, $"ファイルパス：{file}");

					if (FileText.Write(file, sb.ToString()) == false)
					{
						Logs.Apend(LogTable.eStatus.Error, $"ファイルの書き込みに失敗しました。{Path.GetFileName(file)}は、他で使用されている可能性があります。");

						// 書込みエラー
						result = eResut.CannotFileSave;
					}
					else
					{
						result = eResut.Success;

						Properties.Settings.Default.FBSavePath = file;
						Properties.Settings.Default.Save();
					}
				}
			}

			if (result != eResut.Success)
			{
				Logs.Apend(LogTable.eStatus.Error, $"FBデータの作成に失敗したため、処理を中断しました。");
			}
			else
			{
				Logs.Apend(LogTable.eStatus.Success, $"FBデータの作成が完了しました。");
			}

			prog.FinishProgress();
		}

		/// <summary>
		/// ファイルパスを取得します。
		/// </summary>
		public void ShowSaveFilePath()
		{
			string filepath = Cast.String(Properties.Settings.Default.FBSavePath).Trim();

			SaveFileDialog	fd		= new SaveFileDialog();
			// はじめに表示されるフォルダ
			string inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string inifile = BankFileName;

			if (filepath != "")
			{
				inipath = Path.GetDirectoryName(filepath);
				inifile = Path.GetFileName(filepath);
			}

			fd.InitialDirectory = inipath;
			fd.FileName			= inifile;
			// [ファイルの種類]に表示される選択肢を指定する
			fd.Filter = string.Format("TXTファイル|*.txt");
			fd.FilterIndex = 0;
			fd.Title = "保存先を指定してください。";
			fd.RestoreDirectory = true;

			// ダイアログを表示
			if (fd.ShowDialog() == DialogResult.OK)
			{
				filepath = fd.FileName;
			}
			else
			{
				filepath = null;
			}

			file = filepath;
		}

		/// <summary>
		/// 支払区分に対応する会員情報の口座を取得します。
		/// </summary>
		Koza getKoza(t_kaiin krow, eShiharai shiharai)
		{
			Koza koza = new Koza();

			switch (shiharai)
			{
				case eShiharai.Koza1 :
					koza.BankCode	= krow.Kaiin_BankCode1_Null;
					koza.ShitenCode	= krow.Kaiin_BankShitenCode1_Null;
					koza.KozaType	= krow.Kaiin_BankKozaType1;
					koza.KozaNo		= Cast.String(krow.Kaiin_BankKozaNo1_Null).Trim();
					koza.KozaName	= Cast.String(krow.Kaiin_BankKozaName1_Null).Trim();
					break;
				case eShiharai.Koza2 :
					koza.BankCode	= krow.Kaiin_BankCode2_Null;
					koza.ShitenCode	= krow.Kaiin_BankShitenCode2_Null;
					koza.KozaType	= krow.Kaiin_BankKozaType2;
					koza.KozaNo		= Cast.String(krow.Kaiin_BankKozaNo2_Null).Trim();
					koza.KozaName	= Cast.String(krow.Kaiin_BankKozaName2_Null).Trim();
					break;
				case eShiharai.Koza3 :
					koza.BankCode	= krow.Kaiin_BankCode3_Null;
					koza.ShitenCode	= krow.Kaiin_BankShitenCode3_Null;
					koza.KozaType	= krow.Kaiin_BankKozaType3;
					koza.KozaNo		= Cast.String(krow.Kaiin_BankKozaNo3_Null).Trim();
					koza.KozaName	= Cast.String(krow.Kaiin_BankKozaName3_Null).Trim();
					break;
			}

			return koza;
		}

		/// <summary>
		/// 銀行コードマスタから銀行名カナを取得します。未登録なら空白。
		/// </summary>
		string getBankName(int cd)
		{
			BankCode bc = AppGlobal.BankCodeMg.GetBankCode(cd);

			if (bc == null)
			{
				return BlankSpace;
			}

			return Cast.String(bc.Furigana);
		}

		/// <summary>
		/// 銀行コードマスタから支店名カナを取得します。未登録なら空白。
		/// </summary>
		string getShitenName(int cd, int cd_shiten)
		{
			t_bank_code xrow = AppGlobal.BankCodeMg.GetBankCodeRow(cd, cd_shiten);

			if (xrow == null)
			{
				return BlankSpace;
			}

			return Cast.String(xrow.BCD_NameShitenFurigana);
		}

		/// <summary>
		/// 文字列を半角に変換し、指定バイト数に切り詰め／右空白埋めします。
		/// </summary>
		/// <param name="str">変換元</param>
		/// <param name="width">バイト数</param>
		string fitWidth(string str, int width)
		{
			string narrow = StrConv.ToNarrow(Cast.String(str));

			StringBuilder buf = new StringBuilder();
			int len = 0;

			// バイト数を超えない範囲で文字単位に切り詰め
			foreach (char c in narrow)
			{
				int n = SJis.GetByteCount(c.ToString());
				if (len + n > width)
				{
					break;
				}
				buf.Append(c);
				len += n;
			}

			buf.Append(' ', width - len);

			return buf.ToString();
		}

		/// <summary>
		/// レコードの開始
		/// </summary>
		void beginRecord()
		{
			recordStart = sb.Length;
		}

		/// <summary>
		/// レコードの終了（改行を付加し、レコード長を検証）
		/// </summary>
		void endRecord()
		{
			string rec = sb.ToString(recordStart, sb.Length - recordStart);

			if (SJis.GetByteCount(rec) != RecordLength)
			{
				cnt_lengthError++;
			}

			sb.AppendLine();
		}

		/// <summary>
		/// 引落日（MMDD）
		/// </summary>
		/// <param name="dt"></param>
		void appendHikiotoshibi(DateTime dt)
		{
			string str = dt.ToString("MMdd");

			sb.Append(str);
		}
		/// <summary>
		/// 委託者名
		/// </summary>
		/// <param name="name"></param>
		void appendItakushaName(string name)
		{
			sb.Append(fitWidth(name, 40));
		}
		/// <summary>
		/// 委託者コード
		/// </summary>
		/// <param name="cd"></param>
		void appendCodeKaisha(string cd)
		{
			string val = Cast.String(cd).Trim().PadLeft(10, '0');

			sb.Append(val);
		}
		/// <summary>
		/// 種別コード
		/// </summary>
		/// <param name="cd"></param>
		void appendShubetsu(int cd)
		{
			string val = cd.ToString().PadLeft(2, '0');

			sb.Append(val);
		}
		/// <summary>
		/// コード区分
		/// </summary>
		/// <param name="cd">0.JIS、1.EBCDIC</param>
		void appendKbnCode(int cd)
		{
			sb.Append(cd);
		}
		/// <summary>
		/// 合計件数
		/// </summary>
		/// <param name="dec"></param>
		void appendCount(decimal dec)
		{
			string val = Math.Floor(dec).ToString().PadLeft(6, '0');

			sb.Append(val);
		}
		/// <summary>
		/// 合計金額
		/// </summary>
		/// <param name="dec"></param>
		void appendTotalCost(decimal dec)
		{
			string val = Math.Floor(dec).ToString().PadLeft(12, '0');

			sb.Append(val);
		}

		/// <summary>
		/// データ区分
		/// </summary>
		/// <param name="val"></param>
		void appendKbnData(int val)
		{
			sb.Append(val);
		}

		/// <summary>
		/// 銀行コード
		/// </summary>
		/// <param name="cd"></param>
		void appendBankCode(int cd)
		{
			string val = cd.ToString().PadLeft(4, '0');

			sb.Append(val);
		}
		/// <summary>
		/// 銀行名
		/// </summary>
		/// <param name="name"></param>
		void appendBankName(string name)
		{
			sb.Append(fitWidth(name, 15));
		}
		/// <summary>
		/// 支店コード
		/// </summary>
		/// <param name="cd"></param>
		void appendShitenCode(int cd)
		{
			string val = cd.ToString().PadLeft(3, '0');

			sb.Append(val);
		}
		/// <summary>
		/// 支店名
		/// </summary>
		/// <param name="name"></param>
		void appendShitenName(string name)
		{
			sb.Append(fitWidth(name, 15));
		}
		/// <summary>
		/// 預金種目
		/// </summary>
		void appendKamoku(eTypeKoza type)
		{
			string val = "1";

			switch(type)
			{
				case eTypeKoza.Touza : val = "2"; break;
			}

			sb.Append(val);
		}
		/// <summary>
		/// 口座番号
		/// </summary>
		/// <param name="cd"></param>
		void appendKozaNo(string cd)
		{
			string val = Cast.String(cd).Trim().PadLeft(7, '0');

			sb.Append(val);
		}
		/// <summary>
		/// 預金者名
		/// </summary>
		/// <param name="name"></param>
		void appendYokinshaName(string name)
		{
			sb.Append(fitWidth(name, 30));
		}
		/// <summary>
		/// 金額
		/// </summary>
		/// <param name="dec"></param>
		void appendCost(decimal dec)
		{
			string val = Math.Floor(dec).ToString().PadLeft(10, '0');

			sb.Append(val);
		}
		/// <summary>
		/// 新規コード
		/// </summary>
		/// <param name="cd">0.その他、1.第１回分、2.変更分</param>
		void appendNewCode(int cd)
		{
			sb.Append(cd);
		}
		/// <summary>
		/// 顧客番号（会員コードを右詰め0埋め）
		/// </summary>
		/// <param name="cd"></param>
		void appendKokyakuNo(int cd)
		{
			string val = cd.ToString().PadLeft(20, '0');

			sb.Append(val);
		}
		/// <summary>
		/// 0埋め
		/// </summary>
		/// <param name="cnt">桁数</param>
		void appendZero(int cnt)
		{
			sb.Append('0', cnt);
		}
		/// <summary>
		/// ダミー（空白埋め）
		/// </summary>
		/// <param name="cnt">桁数</param>
		void appendDummy(int cnt)
		{
			sb.Append(' ', cnt);
		}
	}
}
