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
	/// [作成者 CC]
	/// iFAXデータ（BizFAX 宛先リスト）ファイル作成クラス
	/// 1行目に「#GLIST-D*:」、2行目以降に宛先を1件1行で出力します。
	/// 　グループコード,FAX番号,施設名(20byte)/氏名(34byte)様/会員番号(6桁)
	/// </summary>
	public class AppIfaxFile
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
		}

		/// <summary>出力ファイル名（先頭に yyyyMM を付加）</summary>
		const string FileNameSuffix	= "iFAXデータBizFAX.txt";
		/// <summary>1行目の固定文字列</summary>
		const string HeaderLine		= "#GLIST-D*:";
		/// <summary>FAX番号が未登録の場合に出力する番号</summary>
		const string FaxNone		= "9999999999";
		/// <summary>敬称</summary>
		const string Keisho			= "様";
		/// <summary>グループコードの桁数</summary>
		const int WidthGroupCd		= 2;
		/// <summary>施設名のバイト数</summary>
		const int WidthShisetsu		= 20;
		/// <summary>氏名のバイト数（敬称を除く）</summary>
		const int WidthName			= 34;
		/// <summary>会員番号の桁数</summary>
		const int WidthKaiinCd		= 6;

		/// <summary>出力文字コード</summary>
		static readonly Encoding SJis = Encoding.GetEncoding("Shift_JIS");

		/// <summary>保存先</summary>
		string file;
		eResut result;
		/// <summary>出力対象のiFAXグループID。null の場合は全グループ</summary>
		int? id_ifax;

		/// <summary>
		/// 同一グループ内の同じ会員を1行にまとめるかどうかを取得、設定します。
		/// true  : まとめる（既定）
		/// false : 会員の会費（t_kaiin_kaihi）1件につき1行出力する
		/// </summary>
		public bool MergeDuplicate { get; set; } = true;

		/// <summary>
		/// ログ情報を取得します。
		/// </summary>
		public LogTable Logs { get; private set; }

		/// <summary>
		/// iFAXデータを作成します。
		/// </summary>
		/// <param name="_id_ifax">出力対象のiFAXグループID。null の場合は全グループ</param>
		public eResut Create(int? _id_ifax)
		{
			result	= eResut.None;
			id_ifax	= _id_ifax;

			if (file != null)
			{
				FormBg_Progress prog = new FormBg_Progress();

				prog.TitleText = "しばらくお待ちください。";
				prog.LabelText = "iFAXデータを生成しています。";
				prog.DoWorkEventEx += prog_DoWorkEventEx;
				prog.ShowDialog();
				prog.Dispose();
				prog = null;
			}

			return result;
		}

		/// <summary>
		/// iFAXデータ生成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void prog_DoWorkEventEx(object sender, DoWorkEventArgsEx e)
		{
			FormBg_Progress prog = (FormBg_Progress)sender;

			StringBuilder sb = new StringBuilder();

			Logs = new LogTable();

			Logs.Apend(LogTable.eStatus.None, "iFAXデータの作成処理を開始します。");

			prog.SetProgress(0);

			//¶ 2026/11/11 CC 会員の会費(t_kaiin_kaihi) → 会費(t_kaihi) → iFAXグループ(t_ifax) を辿り、在籍会員のみを取得
			//¶ 2026/11/11 CC ※ 複数テーブルに同名の列（ID_Kaiin 等）があるため「*」は使わず、必要な列だけを明示する
			string sql = $"SELECT " +
						 $"{TableProp.t_ifax}.{t_ifax.FCD_iFax}, " +
						 $"{TableProp.t_kaiin}.{t_kaiin.FID_Kaiin}, " +
						 $"{TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}, " +
						 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Name}, " +
						 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_Fax}, " +
						 $"{TableProp.t_kaiin}.{t_kaiin.FKaiin_FaxSofusaki}, " +
						 $"{TableProp.t_iryokikan}.{t_iryokikan.FIRK_Name}, " +
						 $"{TableProp.t_iryokikan}.{t_iryokikan.FIRK_Fax1} " +
						 $"FROM ((({TableProp.t_kaiin_kaihi} " +
						 $"INNER JOIN {TableProp.t_kaihi} ON {TableProp.t_kaiin_kaihi}.{t_kaiin_kaihi.FID_Kaihi} = {TableProp.t_kaihi}.{t_kaihi.FID_Kaihi}) " +
						 $"INNER JOIN {TableProp.t_ifax} ON {TableProp.t_kaihi}.{t_kaihi.FKaihi_BankIfax} = {TableProp.t_ifax}.{t_ifax.FID_iFax}) " +
						 $"INNER JOIN {TableProp.t_kaiin} ON {TableProp.t_kaiin_kaihi}.{t_kaiin_kaihi.FID_Kaiin} = {TableProp.t_kaiin}.{t_kaiin.FID_Kaiin}) " +
						 $"LEFT JOIN {TableProp.t_iryokikan} ON {TableProp.t_kaiin}.{t_kaiin.FID_Iryokikan} = {TableProp.t_iryokikan}.{t_iryokikan.FID_Iryokikan} " +
						 $"WHERE ({TableProp.t_kaiin}.{t_kaiin.FKaiin_TypeZaiseki} = {(int)eTypeZaiseki.Zaiseki}) ";

			if (id_ifax != null)
			{
				sql += $"AND ({TableProp.t_ifax}.{t_ifax.FID_iFax} = {id_ifax.Value}) ";
			}

			sql += $"ORDER BY {TableProp.t_ifax}.{t_ifax.FCD_iFax}, {TableProp.t_kaiin}.{t_kaiin.FCD_Kaiin}";

			DataTable dt = AppGlobal.DB.FillQuery(sql);

			int cnt_detail		= dt.Rows.Count;	//¶ 2026/11/11 CC 取得件数
			int cnt_record		= 0;				//¶ 2026/11/11 CC 出力した宛先行数
			int cnt_duplicate	= 0;				//¶ 2026/11/11 CC 同一グループ内の同じ会員としてまとめた件数
			int cnt_faxnone		= 0;				//¶ 2026/11/11 CC FAX番号が未登録の宛先行数

			HashSet<string> keys	= new HashSet<string>();
			HashSet<int> groups		= new HashSet<int>();

			//¶ 2026/11/11 CC 1行目
			sb.AppendLine(HeaderLine);

			//¶ 2026/11/11 CC 宛先行
			for (int i = 0; i < cnt_detail; i++)
			{
				DataRow row = dt.Rows[i];

				prog.SetProgress((i + 1) * 100 / cnt_detail);

				int cd_ifax		= Cast.Int(row[t_ifax.FCD_iFax]);
				int id_kaiin	= Cast.Int(row[t_kaiin.FID_Kaiin]);
				int cd_kaiin	= Cast.Int(row[t_kaiin.FCD_Kaiin]);

				//¶ 2026/11/11 CC 同一グループ内の同じ会員は1行にまとめる
				if (MergeDuplicate == true)
				{
					if (keys.Add($"{cd_ifax}_{id_kaiin}") == false)
					{
						cnt_duplicate++;
						continue;
					}
				}

				//¶ 2026/11/11 CC FAX番号：FAX送付先が施設なら所属医療機関、自宅なら会員のFAX番号
				bool sofu_shisetsu = Cast.Bool(row[t_kaiin.FKaiin_FaxSofusaki]);

				string fax = getFaxNo(sofu_shisetsu == true ? row[t_iryokikan.FIRK_Fax1] : row[t_kaiin.FKaiin_Fax]);

				if (fax == "")
				{
					fax = FaxNone;
					cnt_faxnone++;

					Logs.Apend(LogTable.eStatus.Warning, $"グループ:{getGroupCd(cd_ifax)} 会員番号:{getKaiinCd(cd_kaiin)} {Cast.String(row[t_kaiin.FKaiin_Name]).Trim()} のFAX番号（{(sofu_shisetsu == true ? "施設" : "自宅")}）が未登録のため、{FaxNone}で出力しました。");
				}

				sb.Append(getGroupCd(cd_ifax));
				sb.Append(",");
				sb.Append(fax);
				sb.Append(",");
				sb.Append(fitWidth(Cast.String(row[t_iryokikan.FIRK_Name]).Trim(), WidthShisetsu));
				sb.Append("/");
				sb.Append(fitWidth(Cast.String(row[t_kaiin.FKaiin_Name]).Trim(), WidthName));
				sb.Append(Keisho);
				sb.Append("/");
				sb.Append(getKaiinCd(cd_kaiin));
				sb.AppendLine();

				groups.Add(cd_ifax);
				cnt_record++;
			}

			if (cnt_record == 0)
			{
				Logs.Apend(LogTable.eStatus.Error, "出力対象のデータがありません。iFAXグループが設定された会費を持つ在籍会員が存在しません。");

				//¶ 2026/11/11 CC 該当なし
				result = eResut.DataNotFound;
			}
			else
			{
				if (cnt_duplicate > 0)
				{
					Logs.Apend(LogTable.eStatus.None, $"同一グループ内で重複する会員{cnt_duplicate}件は1行にまとめました。");
				}
				if (cnt_faxnone > 0)
				{
					Logs.Apend(LogTable.eStatus.None, $"FAX番号が未登録の宛先は{cnt_faxnone}件です。");
				}
				Logs.Apend(LogTable.eStatus.None, $"{groups.Count}グループ、{cnt_record}件の宛先を作成しました。");

				Logs.Apend(LogTable.eStatus.None, $"iFAXデータファイルを作成します。");
				Logs.Apend(LogTable.eStatus.None, $"ファイルパス：{file}");

				//¶ 2026/11/11 CC ※ FileText の既定の文字コードは実行中に変わり得るため、明示的に指定する
				if (FileText.Write(file, sb.ToString(), SJis) == false)
				{
					Logs.Apend(LogTable.eStatus.Error, $"ファイルの書き込みに失敗しました。{Path.GetFileName(file)}は、他で使用されている可能性があります。");

					//¶ 2026/11/11 CC 書込みエラー
					result = eResut.CannotFileSave;
				}
				else
				{
					result = eResut.Success;

					Properties.Settings.Default.IfaxSavePath = file;
					Properties.Settings.Default.Save();
				}
			}

			if (result != eResut.Success)
			{
				Logs.Apend(LogTable.eStatus.Error, $"iFAXデータの作成に失敗したため、処理を中断しました。");
			}
			else
			{
				Logs.Apend(LogTable.eStatus.Success, $"iFAXデータの作成が完了しました。");
			}

			prog.FinishProgress();
		}

		/// <summary>
		/// 保存先を指定するダイアログを表示します。
		/// ファイル名の初期値は、システム日付の年月から作成した「yyyyMMiFAXデータBizFAX.txt」です。
		/// </summary>
		public void ShowSaveFilePath()
		{
			string filepath = Cast.String(Properties.Settings.Default.IfaxSavePath).Trim();

			SaveFileDialog	fd		= new SaveFileDialog();
			//¶ 2026/11/11 CC はじめに表示されるフォルダ
			string inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			if (filepath != "")
			{
				string dir = Path.GetDirectoryName(filepath);

				if (Directory.Exists(dir) == true)
				{
					inipath = dir;
				}
			}

			fd.InitialDirectory = inipath;
			//¶ 2026/11/11 CC ファイル名は前回の保存名ではなく、システム日付から毎回作成する
			fd.FileName			= GetFileName(DateTime.Now);
			//¶ 2026/11/11 CC [ファイルの種類]に表示される選択肢を指定する
			fd.Filter = string.Format("TXTファイル|*.txt");
			fd.FilterIndex = 0;
			fd.Title = "保存先を指定してください。";
			fd.RestoreDirectory = true;

			//¶ 2026/11/11 CC ダイアログを表示
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
		/// 出力ファイル名（yyyyMMiFAXデータBizFAX.txt）を取得します。
		/// </summary>
		/// <param name="_ym">年月</param>
		public static string GetFileName(DateTime _ym)
		{
			return _ym.ToString("yyyyMM") + FileNameSuffix;
		}

		/// <summary>
		/// グループコード（2桁0埋め）
		/// </summary>
		string getGroupCd(int cd)
		{
			return cd.ToString().PadLeft(WidthGroupCd, '0');
		}

		/// <summary>
		/// 会員番号（6桁0埋め）
		/// </summary>
		string getKaiinCd(int cd)
		{
			return cd.ToString().PadLeft(WidthKaiinCd, '0');
		}

		/// <summary>
		/// FAX番号から数字だけを取り出します（ハイフン等を除去）。未登録なら空文字。
		/// </summary>
		string getFaxNo(object obj)
		{
			string narrow = StrConv.ToNarrow(Cast.String(obj));

			StringBuilder buf = new StringBuilder();

			foreach (char c in narrow)
			{
				if (c >= '0' && c <= '9')
				{
					buf.Append(c);
				}
			}

			return buf.ToString();
		}

		/// <summary>
		/// 文字列を指定バイト数（Shift_JIS）に切り詰め／右空白埋めします。全角・半角は変換しません。
		/// </summary>
		/// <param name="str">変換元</param>
		/// <param name="width">バイト数</param>
		string fitWidth(string str, int width)
		{
			StringBuilder buf = new StringBuilder();
			int len = 0;

			//¶ 2026/11/11 CC バイト数を超えない範囲で文字単位に切り詰め
			foreach (char c in Cast.String(str))
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
	}
}
