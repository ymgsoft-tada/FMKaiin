using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1.Win.FlexReport;
using ComponentDB;
using ComponentDebug;
using ComponentIO;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 宛名ラベル（A4縦 2列×6段）の帳票処理
	/// </summary>
	class ReportFlexAtenaLabel
	{
		/// <summary>
		/// 宛先の種類
		/// </summary>
		public enum eDest
		{
			/// <summary>会員（1人1枚）</summary>
			Kaiin,
			/// <summary>参加医会（医会ごとに1枚）</summary>
			Ikai,
			/// <summary>医療機関</summary>
			IryoKikan,
		}

		/// <summary>
		/// 宛名ラベルの出力条件
		/// </summary>
		public class Joken
		{
			/// <summary>宛先</summary>
			public eDest Dest = eDest.Kaiin;
			/// <summary>会員コード（開始）。null は指定なし</summary>
			public int? KaiinCdFrom = null;
			/// <summary>会員コード（終了）。null は指定なし</summary>
			public int? KaiinCdTo = null;
			/// <summary>会員区分。null はすべて</summary>
			public int? KaiinKbn = null;
			/// <summary>出力する在籍区分</summary>
			public List<int> Zaiseki = new List<int>();
			/// <summary>出力する参加医会（会費属性）</summary>
			public List<int> Zokusei = new List<int>();
			/// <summary>医療機関コード（開始）。null は指定なし</summary>
			public int? IryoCdFrom = null;
			/// <summary>医療機関コード（終了）。null は指定なし</summary>
			public int? IryoCdTo = null;
			/// <summary>退会した医療機関を除く</summary>
			public bool ExceptTaikai = true;
			/// <summary>印字開始位置の行（1～6）</summary>
			public int StartRow = 1;
			/// <summary>印字開始位置の列（1～2）</summary>
			public int StartCol = 1;
			/// <summary>繰返し枚数（1～）</summary>
			public int Copies = 1;
		}

		private string ReportFile = $"{AppConst.ReportFolder}{ReportProp.CRepAtenaLabelFilename}";
		private DBView outView;

		//¶ 2026/11/28 CC 1ページの段数と列数。Detail セクション1つがラベル1段（左右2枚）
		private const int MAX_LINE = 6;
		private const int MAX_COL = 2;

		//¶ 2026/11/28 CC 出力用テーブルの列名。末尾に列番号（0:左 1:右）を付けて使う
		private const string F_Post = "Post";
		private const string F_AddA = "AddA";
		private const string F_AddB = "AddB";
		private const string F_Shisetsu = "Shisetsu";
		private const string F_Name = "Name";
		private const string F_Code = "Code";
		private static readonly string[] F_All = { F_Post, F_AddA, F_AddB, F_Shisetsu, F_Name, F_Code };

		//¶ 2026/11/28 CC レポートの欄名（末尾の列番号を除いた部分）。XRep_AtenaLabel.cs の定数と対応する
		private static readonly Dictionary<string, string> fldNames = new Dictionary<string, string>()
		{
			{ F_Post,     CRepAtenaLabel.FFldPost0.TrimEnd('0') },
			{ F_AddA,     CRepAtenaLabel.FFldAddA0.TrimEnd('0') },
			{ F_AddB,     CRepAtenaLabel.FFldAddB0.TrimEnd('0') },
			{ F_Shisetsu, CRepAtenaLabel.FFldShisetsu0.TrimEnd('0') },
			{ F_Name,     CRepAtenaLabel.FFldName0.TrimEnd('0') },
			{ F_Code,     CRepAtenaLabel.FFldCode0.TrimEnd('0') },
		};

		/// <summary>
		/// ラベル1枚分の印字内容
		/// </summary>
		private class LabelData
		{
			public string Post = "";
			public string AddA = "";
			public string AddB = "";
			public string Shisetsu = "";
			public string Name = "";
			public string Code = "";
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReportFlexAtenaLabel()
		{
		}

		/// <summary>
		/// 出力条件から印刷用のビューを作成する
		/// </summary>
		/// <param name="joken">出力条件</param>
		/// <returns>宛先の件数（繰返し・空きラベルを含まない）</returns>
		public int CreateView(Joken joken)
		{
			List<LabelData> labels;
			switch (joken.Dest)
			{
				case eDest.IryoKikan:
					labels = getIryoKikan(joken);
					break;
				case eDest.Ikai:
					labels = getKaiin(joken, true);
					break;
				default:
					labels = getKaiin(joken, false);
					break;
			}

			// 出力用テーブルの作成
			DataTable dt = new DataTable();
			for (int col = 0; col < MAX_COL; col++)
			{
				foreach (string f in F_All)
				{
					dt.Columns.Add(f + col, typeof(string));
				}
			}
			outView = new DBView(dt);
			if (labels.Count == 0)
			{
				return 0;
			}

			//¶ 2026/11/28 CC 印字開始位置より前は空きラベルにし、繰返し枚数の分だけ同じ宛先を続ける
			List<LabelData> cells = new List<LabelData>();
			int skip = (joken.StartRow - 1) * MAX_COL + (joken.StartCol - 1);
			for (int i = 0; i < skip; i++)
			{
				cells.Add(new LabelData());
			}
			foreach (LabelData label in labels)
			{
				for (int i = 0; i < joken.Copies; i++)
				{
					cells.Add(label);
				}
			}
			//¶ 2026/11/28 CC ページの途中で終わるときは、ページ末尾まで空きラベルで埋める
			while (cells.Count % (MAX_LINE * MAX_COL) != 0)
			{
				cells.Add(new LabelData());
			}

			// 1行＝ラベル1段（左右2枚）
			for (int i = 0; i < cells.Count; i += MAX_COL)
			{
				DataRow row = dt.NewRow();
				for (int col = 0; col < MAX_COL; col++)
				{
					LabelData c = cells[i + col];
					row[F_Post + col] = c.Post;
					row[F_AddA + col] = c.AddA;
					row[F_AddB + col] = c.AddB;
					row[F_Shisetsu + col] = c.Shisetsu;
					row[F_Name + col] = c.Name;
					row[F_Code + col] = c.Code;
				}
				dt.Rows.Add(row);
			}
			return labels.Count;
		}

		/// <summary>
		/// 会員の宛先を取得する
		/// </summary>
		/// <param name="joken">出力条件</param>
		/// <param name="byIkai">true:参加医会ごとに1枚 false:1人1枚</param>
		List<LabelData> getKaiin(Joken joken, bool byIkai)
		{
			List<LabelData> labels = new List<LabelData>();
			//¶ 2026/11/28 CC 在籍区分・参加医会のチェックが1つも無いときは対象なし（IN () は SQL エラーになる）
			if (joken.Zaiseki.Count == 0)
			{
				return labels;
			}
			if (byIkai == true && joken.Zokusei.Count == 0)
			{
				return labels;
			}

			string tk = TableProp.t_kaiin;
			string ti = TableProp.t_iryokikan;
			string tkk = TableProp.t_kaiin_kaihi;
			string th = TableProp.t_kaihi;

			//¶ 2026/11/28 CC 複数テーブルに同名の列（ID_Kaiin 等）があるため「*」は使わず、必要な列だけを明示する
			string sql = $"SELECT " +
						 $"{tk}.{t_kaiin.FID_Kaiin}, " +
						 $"{tk}.{t_kaiin.FCD_Kaiin}, " +
						 $"{tk}.{t_kaiin.FKaiin_Name}, " +
						 $"{tk}.{t_kaiin.FKaiin_Post}, " +
						 $"{tk}.{t_kaiin.FKaiin_Addr1}, " +
						 $"{tk}.{t_kaiin.FKaiin_Addr2}, " +
						 $"{tk}.{t_kaiin.FKaiin_BunshoSofusaki}, " +
						 $"{ti}.{t_iryokikan.FIRK_Name}, " +
						 $"{ti}.{t_iryokikan.FIRK_Post}, " +
						 $"{ti}.{t_iryokikan.FIRK_Addr1}, " +
						 $"{ti}.{t_iryokikan.FIRK_Addr2} ";
			if (byIkai == true)
			{
				sql += $", {th}.{t_kaihi.FKaihi_Zokusei} " +
					   $"FROM (({tkk} " +
					   $"INNER JOIN {th} ON {tkk}.{t_kaiin_kaihi.FID_Kaihi} = {th}.{t_kaihi.FID_Kaihi}) " +
					   $"INNER JOIN {tk} ON {tkk}.{t_kaiin_kaihi.FID_Kaiin} = {tk}.{t_kaiin.FID_Kaiin}) " +
					   $"LEFT JOIN {ti} ON {tk}.{t_kaiin.FID_Iryokikan} = {ti}.{t_iryokikan.FID_Iryokikan} ";
			}
			else
			{
				sql += $"FROM {tk} " +
					   $"LEFT JOIN {ti} ON {tk}.{t_kaiin.FID_Iryokikan} = {ti}.{t_iryokikan.FID_Iryokikan} ";
			}

			sql += $"WHERE ({tk}.{t_kaiin.FKaiin_TypeZaiseki} IN ({string.Join(",", joken.Zaiseki)})) ";
			if (joken.KaiinCdFrom != null)
			{
				sql += $"AND ({tk}.{t_kaiin.FCD_Kaiin} >= {joken.KaiinCdFrom.Value}) ";
			}
			if (joken.KaiinCdTo != null)
			{
				sql += $"AND ({tk}.{t_kaiin.FCD_Kaiin} <= {joken.KaiinCdTo.Value}) ";
			}
			if (joken.KaiinKbn != null)
			{
				sql += $"AND ({tk}.{t_kaiin.FKaiin_TypeKaiin} = {joken.KaiinKbn.Value}) ";
			}
			if (byIkai == true)
			{
				//¶ 2026/11/28 CC 参加医会（医会区分:その他）の行だけを対象にし、チェックのある会費属性で絞る
				sql += $"AND ({tkk}.{t_kaiin_kaihi.FKaihiTypeIkai} = {(int)eTypeIkai.Etc}) " +
					   $"AND ({th}.{t_kaihi.FKaihi_Zokusei} IN ({string.Join(",", joken.Zokusei)})) " +
					   $"ORDER BY {th}.{t_kaihi.FKaihi_Zokusei}, {tk}.{t_kaiin.FCD_Kaiin}";
			}
			else
			{
				sql += $"ORDER BY {tk}.{t_kaiin.FCD_Kaiin}";
			}

			DataTable dt = AppGlobal.DB.FillQuery(sql);
			HashSet<string> keys = new HashSet<string>();
			foreach (DataRow row in dt.Rows)
			{
				if (byIkai == true)
				{
					//¶ 2026/11/28 CC 同じ会費属性の会費を複数持つ会員は、その医会で1枚にまとめる
					string key = $"{Cast.String(row[t_kaihi.FKaihi_Zokusei])}_{Cast.String(row[t_kaiin.FID_Kaiin])}";
					if (keys.Add(key) == false)
					{
						continue;
					}
				}

				LabelData label = new LabelData();
				string irkName = Cast.String(row[t_iryokikan.FIRK_Name]);
				//¶ 2026/11/28 CC 文書送付先が施設(true)なら所属医療機関の住所、自宅(false)なら会員の住所。
				//¶ 2026/11/28 CC 施設でも所属医療機関が無いときは会員の住所にする
				bool shisetsu = (row[t_kaiin.FKaiin_BunshoSofusaki] is bool) && ((bool)row[t_kaiin.FKaiin_BunshoSofusaki] == true);
				if (shisetsu == true && irkName.Length > 0)
				{
					label.Post = Cast.String(row[t_iryokikan.FIRK_Post]);
					label.AddA = Cast.String(row[t_iryokikan.FIRK_Addr1]);
					label.AddB = Cast.String(row[t_iryokikan.FIRK_Addr2]);
					label.Shisetsu = irkName;
				}
				else
				{
					label.Post = Cast.String(row[t_kaiin.FKaiin_Post]);
					label.AddA = Cast.String(row[t_kaiin.FKaiin_Addr1]);
					label.AddB = Cast.String(row[t_kaiin.FKaiin_Addr2]);
				}
				label.Name = $"{Cast.String(row[t_kaiin.FKaiin_Name])} 様";
				label.Code = Cast.String(row[t_kaiin.FCD_Kaiin]).PadLeft(6, '0');
				labels.Add(label);
			}
			return labels;
		}

		/// <summary>
		/// 医療機関の宛先を取得する
		/// </summary>
		/// <param name="joken">出力条件</param>
		List<LabelData> getIryoKikan(Joken joken)
		{
			List<LabelData> labels = new List<LabelData>();
			string ti = TableProp.t_iryokikan;

			string sql = $"SELECT " +
						 $"{ti}.{t_iryokikan.FIRK_Code}, " +
						 $"{ti}.{t_iryokikan.FIRK_Name}, " +
						 $"{ti}.{t_iryokikan.FIRK_Post}, " +
						 $"{ti}.{t_iryokikan.FIRK_Addr1}, " +
						 $"{ti}.{t_iryokikan.FIRK_Addr2}, " +
						 $"{ti}.{t_iryokikan.FIRK_TaikaiKbn} " +
						 $"FROM {ti} " +
						 $"WHERE (1 = 1) ";
			if (joken.IryoCdFrom != null)
			{
				sql += $"AND ({ti}.{t_iryokikan.FIRK_Code} >= {joken.IryoCdFrom.Value}) ";
			}
			if (joken.IryoCdTo != null)
			{
				sql += $"AND ({ti}.{t_iryokikan.FIRK_Code} <= {joken.IryoCdTo.Value}) ";
			}
			sql += $"ORDER BY {ti}.{t_iryokikan.FIRK_Code}";

			DataTable dt = AppGlobal.DB.FillQuery(sql);
			foreach (DataRow row in dt.Rows)
			{
				//¶ 2026/11/28 CC 退会区分は true:会員 false:退会（医療機関一覧表と同じ扱い）。未設定は退会とみなさない
				if (joken.ExceptTaikai == true)
				{
					if ((row[t_iryokikan.FIRK_TaikaiKbn] is bool) && ((bool)row[t_iryokikan.FIRK_TaikaiKbn] == false))
					{
						continue;
					}
				}

				LabelData label = new LabelData();
				label.Post = Cast.String(row[t_iryokikan.FIRK_Post]);
				label.AddA = Cast.String(row[t_iryokikan.FIRK_Addr1]);
				label.AddB = Cast.String(row[t_iryokikan.FIRK_Addr2]);
				label.Shisetsu = $"{Cast.String(row[t_iryokikan.FIRK_Name])} 御中";
				//¶ 2026/11/28 CC 現行の帳票に合わせ、医療機関コードは会員の氏名の行（名称の次の行）に印字する
				label.Name = Cast.String(row[t_iryokikan.FIRK_Code]).PadLeft(5, '0');
				labels.Add(label);
			}
			return labels;
		}

		/// <summary>
		/// レポートの実行
		/// </summary>
		/// <param name="isDirect">true:直接印刷 false:プレビュー</param>
		public bool ReportExec(bool isDirect = false)
		{
			bool success = true;
			if (ComponentFile.FileIO.Exists(ReportFile) == false)
			{
				ErrLog.WriteLine("レポートファイルが見つかりません。");
				return false;
			}

			if (outView == null || outView.Count == 0)
			{
				//出力するデータがない
				AppMsgBox.Show(AppMsgBoxIndex.NoPrintData);
				return false;
			}

			ReportFlex.ReportInfo repinfo = new ReportFlex.ReportInfo
			(
				ReportFile,
				ReportProp.CRepAtenaLabel,
				"",
				MAX_LINE,
				outView
			);

			repinfo.PrintSection += Repinfo_PrintSection;

			//直接印刷
			if (isDirect == true)
			{
				repinfo.LoadReport();
				repinfo.Print();
			}
			else
			{
				//プレビュー
				FormReportPreview frm = new FormReportPreview();
				frm.Width = 1220;
				frm.SetReport(repinfo);
				frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
				frm.ShowDialog();
				if (frm.FormCloseReason == FormCloseReason.None)
				{
					success = false;
				}
				frm.Dispose();
			}
			return success;
		}

		private void Repinfo_PrintSection(object sender, ReportFlex.ReportEventArgsEx e)
		{
			if (e.Source.Section != SectionTypeEnum.Detail)
			{
				return;
			}
			C1FlexReport rep = (C1FlexReport)sender;
			DataRow xrow = outView[(e.Page - 1) * MAX_LINE + e.Line].Row;

			for (int col = 0; col < MAX_COL; col++)
			{
				foreach (string f in F_All)
				{
					setFieldValue(rep, fldNames[f] + col, xrow[f + col]);
				}
			}
		}

		TextField getField(C1FlexReport rep, string name)
		{
			if (rep == null)
			{
				ErrLog.Assert("rep is null");
				return null;
			}
			if (rep.Fields.Contains(name) == false)
			{
				ErrLog.Assert($"illegal field name '{name}'");
				return null;
			}
			if (!(rep.Fields[name] is TextField))
			{
				ErrLog.Assert($"not FlexReport's TextField '{name}'");
				return null;
			}
			return (TextField)rep.Fields[name];
		}

		void setFieldValue(C1FlexReport rep, string fldname, object value)
		{
			TextField fld = getField(rep, fldname);
			if (fld != null)
			{
				fld.Text = Cast.String(value);
			}
			else
			{
				ErrLog.WriteLine($"レポートフィールド「{fldname}」は見つかりません。");
			}
		}
	}
}
