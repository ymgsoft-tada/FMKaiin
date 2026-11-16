using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 会費マスターのExcel取込
	/// 会費コードをキーに、あれば上書き・なければ追加する。Excelにない会費は変更しない。
	/// 会費区分（医師会会費／医会等会費）は、表の上に書かれた会費区分コードから判定する。
	/// </summary>
	public class AppExcelImportKaihi : AppExcelImport
	{
		// Excelの見出し名（半角カナ・全角数字は元ファイルの表記のまま）
		const string COL_Code = "会費ｺｰﾄﾞ";
		const string COL_Name = "会費名称";
		const string COL_Total = "合計";
		const string COL_Ifax = "iFAXｸﾞﾙｰﾌﾟ";
		/// <summary>月額の見出し（1月～12月）</summary>
		static readonly string[] COL_Months =
		{
			"１月", "２月", "３月", "４月", "５月", "６月",
			"７月", "８月", "９月", "１０月", "１１月", "１２月",
		};
		/// <summary>会費区分コードの見出し（表の上にあり、次の行に値が入る）</summary>
		const string TOP_KaihiType = "会費区分ｺｰﾄﾞ";

		/// <summary>月額のフィールド名（1月～12月）</summary>
		static readonly string[] FLD_Months =
		{
			t_kaihi.FKaihi_GetsugakuCost1, t_kaihi.FKaihi_GetsugakuCost2, t_kaihi.FKaihi_GetsugakuCost3,
			t_kaihi.FKaihi_GetsugakuCost4, t_kaihi.FKaihi_GetsugakuCost5, t_kaihi.FKaihi_GetsugakuCost6,
			t_kaihi.FKaihi_GetsugakuCost7, t_kaihi.FKaihi_GetsugakuCost8, t_kaihi.FKaihi_GetsugakuCost9,
			t_kaihi.FKaihi_GetsugakuCost10, t_kaihi.FKaihi_GetsugakuCost11, t_kaihi.FKaihi_GetsugakuCost12,
		};

		/// <summary>
		/// 検証済みの1行分
		/// </summary>
		class ImportRow
		{
			public int Code;
			public string Name;
			/// <summary>月額（1月～12月）。未入力はnull</summary>
			public decimal?[] Months = new decimal?[12];
			/// <summary>iFAXグループのID。0は未設定</summary>
			public int IfaxID;
		}

		List<ImportRow> rows;

		/// <summary>ファイルの会費区分</summary>
		eTypeKaihi kaihiType = eTypeKaihi.None;

		/// <summary>
		/// 取込前に、ファイルと同じ会費区分の会費を全て削除するか。
		/// 削除は検証が通った後に行うため、Excelにエラーがあれば削除しません。
		/// </summary>
		public bool ClearAll { get; set; }

		/// <summary>
		/// 必須の見出し名
		/// </summary>
		protected override string[] RequiredColumns
		{
			get
			{
				List<string> cols = new List<string>();

				cols.Add(COL_Code);
				cols.Add(COL_Name);
				cols.AddRange(COL_Months);
				cols.Add(COL_Total);
				cols.Add(COL_Ifax);

				return cols.ToArray();
			}
		}

		/// <summary>
		/// 読み込んだデータを検証します。
		/// </summary>
		protected override bool validateData()
		{
			rows = new List<ImportRow>();

			// 会費区分（表の上の「会費区分ｺｰﾄﾞ」の次の行）
			string type = "";
			for (int r = 0; r < getTopRowCount(); r++)
			{
				if (getTopValue(r, 0) == TOP_KaihiType)
				{
					type = getTopValue(r + 1, 0);
					break;
				}
			}

			if (type == "1")
			{
				kaihiType = eTypeKaihi.Ishikai;
			}
			else
			if (type == "2")
			{
				kaihiType = eTypeKaihi.Ikai;
			}
			else
			{
				Log.Apend(LogTable.eStatus.Error, $"会費区分コード「{type}」が正しくありません。（1：医師会会費／2：医会等会費）");
				return false;
			}

			Log.Apend(LogTable.eStatus.None, $"会費区分：{enumKbn.DTypeKaihi[(int)kaihiType]}");

			int cnt_error = 0;
			Dictionary<int, int> codes = new Dictionary<int, int>(); // 会費コード → Excelの行番号

			foreach (DataRow src in dtSrc.Rows)
			{
				int rowno = getRowNo(src);
				bool ok = true;
				ImportRow rec = new ImportRow();

				// 会費コード
				string code = getValue(src, COL_Code);
				if (int.TryParse(code, out rec.Code) == false || rec.Code <= 0 || rec.Code > AppDbID.Max_Code)
				{
					Log.Apend(LogTable.eStatus.Error, $"{rowno}行目：会費コード「{code}」が正しくありません。");
					cnt_error++;
					continue;
				}

				string who = $"{rowno}行目（{rec.Code}）";

				if (codes.ContainsKey(rec.Code) == true)
				{
					Log.Apend(LogTable.eStatus.Error, $"{who}：会費コードが{codes[rec.Code]}行目と重複しています。");
					ok = false;
				}
				else
				{
					codes.Add(rec.Code, rowno);
				}

				rec.Name = getValue(src, COL_Name);

				// 月額
				decimal sum = 0;
				for (int i = 0; i < COL_Months.Length; i++)
				{
					string val = getValue(src, COL_Months[i]);
					if (val == "")
					{
						continue;
					}

					decimal cost;
					if (decimal.TryParse(val, out cost) == false)
					{
						Log.Apend(LogTable.eStatus.Error, $"{who}：{COL_Months[i]}の金額「{val}」が正しくありません。");
						ok = false;
						continue;
					}

					rec.Months[i] = cost;
					sum += cost;
				}

				// 合計は取り込まない。月額の和と合わない場合は警告のみ
				string total = getValue(src, COL_Total);
				if (total != "")
				{
					decimal totalval;
					if (decimal.TryParse(total, out totalval) == false || totalval != sum)
					{
						Log.Apend(LogTable.eStatus.Warning, $"{who}：合計「{total}」が月額の合計「{sum:0.##}」と一致しません。月額をそのまま登録します。");
					}
				}

				// iFAXグループ（コード → ID）。空欄と0は未設定
				string ifax = getValue(src, COL_Ifax);
				if (ifax != "" && ifax != "0")
				{
					int ifaxcd;
					IfaxGroup grp = null;

					if (int.TryParse(ifax, out ifaxcd) == true)
					{
						grp = AppGlobal.IfaxGroups.GetCode(ifaxcd);
					}

					if (grp == null)
					{
						Log.Apend(LogTable.eStatus.Error, $"{who}：iFAXグループ「{ifax}」がiFAXグループマスターに登録されていません。");
						ok = false;
					}
					else
					{
						rec.IfaxID = grp.ID;
					}
				}

				if (ok == true)
				{
					rows.Add(rec);
				}
				else
				{
					cnt_error++;
				}
			}

			if (cnt_error > 0)
			{
				Log.Apend(LogTable.eStatus.Error, $"{cnt_error}件の行にエラーがあります。");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 検証済みのデータをDBへ反映します。
		/// </summary>
		protected override bool updateData()
		{
			DataTable tblKaihi = AppGlobal.DB.GetFillTable(TableProp.t_kaihi);

			try
			{
				if (ClearAll == true)
				{
					//¶ 2026/11/14 CC クリアは、ファイルと同じ会費区分の行だけを対象にする（別の区分のデータを残すため。ユーザー了承）
					int cnt_del = 0;

					foreach (DataRow row in tblKaihi.Select())
					{
						if (Cast.Int(row[t_kaihi.FKaihi_KaihiType]) == (int)kaihiType)
						{
							row.Delete();
							cnt_del++;
						}
					}

					Log.Apend(LogTable.eStatus.None, $"登録済みの会費（{enumKbn.DTypeKaihi[(int)kaihiType]}）{cnt_del}件を削除しました。");
					//¶ 2026/11/14 CC 口座情報・会費属性はExcelにないため戻らない。IDも振り直しになる（付け替えはしない。ユーザー了承）
					Log.Apend(LogTable.eStatus.Warning, "削除した会費の口座情報・会費属性は元に戻りません。再設定が必要です。");
					Log.Apend(LogTable.eStatus.Warning, "会費のIDを振り直したため、登録済みの会員の会費・会費調整・引落データは再設定が必要です。");
				}

				// 既存の会費（コード → 行）と、IDの最大値
				Dictionary<int, DataRow> dicKaihi = new Dictionary<int, DataRow>();
				int maxid = 0;

				foreach (DataRow row in tblKaihi.Rows)
				{
					if (row.RowState == DataRowState.Deleted)
					{
						continue;
					}

					int cd = Cast.Int(row[t_kaihi.FCD_Kaihi]);
					if (dicKaihi.ContainsKey(cd) == false)
					{
						dicKaihi.Add(cd, row);
					}

					int id = Cast.Int(row[t_kaihi.FID_Kaihi]);
					if (id > maxid)
					{
						maxid = id;
					}
				}

				int cnt_add = 0;
				int cnt_update = 0;
				int cnt_same = 0;

				foreach (ImportRow rec in rows)
				{
					DataRow row;
					bool isnew = false;
					bool changed = false;

					if (dicKaihi.ContainsKey(rec.Code) == true)
					{
						row = dicKaihi[rec.Code];
					}
					else
					{
						//¶ 2026/11/14 CC Excelにない項目は、追加時のみ既定値を入れる。更新時は既存値（口座情報・会費属性など）を残す
						isnew = true;
						maxid++;

						row = tblKaihi.NewRow();
						row[t_kaihi.FID_Kaihi] = maxid;
						row[t_kaihi.FCD_Kaihi] = rec.Code;
						// 検索表示区分：名称のない行（コードだけの予備行）は検索に出さない
						row[t_kaihi.FKaihi_SearchUsed] = (rec.Name != "");
						row[t_kaihi.FKaihi_Zokusei] = (int)getDefaultZokusei(rec.Code);
					}

					changed |= setValue(row, t_kaihi.FKaihi_KaihiType, (int)kaihiType);
					changed |= setValue(row, t_kaihi.FKaihi_Name, rec.Name);

					//¶ 2026/11/14 CC Excelに略称の列がないため、略称が空なら名称を入れる（入力済みの略称は変えない）
					if (isEmpty(row[t_kaihi.FKaihi_ShortName]) == true)
					{
						changed |= setValue(row, t_kaihi.FKaihi_ShortName, rec.Name);
					}

					for (int i = 0; i < FLD_Months.Length; i++)
					{
						//¶ 2026/11/14 CC Excelが空欄で、登録済みの月額が未入力または0の場合は変更なしとみなす
						if (rec.Months[i] == null && (isEmpty(row[FLD_Months[i]]) == true || Cast.Decimal(row[FLD_Months[i]]) == 0))
						{
							continue;
						}

						changed |= setValue(row, FLD_Months[i], rec.Months[i]);
					}

					changed |= setValue(row, t_kaihi.FKaihi_BankIfax, rec.IfaxID);

					if (isnew == true)
					{
						tblKaihi.Rows.Add(row);
						cnt_add++;
					}
					else
					if (changed == true)
					{
						cnt_update++;
					}
					else
					{
						cnt_same++;
					}
				}

				if (AppGlobal.DB.UpdateTable(TableProp.t_kaihi) == false)
				{
					tblKaihi.RejectChanges();
					return false;
				}

				Log.Apend(LogTable.eStatus.Success, $"会費マスターを取り込みました。追加：{cnt_add}件／更新：{cnt_update}件／変更なし：{cnt_same}件");

				return true;
			}
			catch
			{
				//¶ 2026/11/14 CC 途中で失敗した場合は、バッファ上の変更を取り消してから呼び出し元へ伝える
				tblKaihi.RejectChanges();
				throw;
			}
		}

		/// <summary>
		/// 反映後の処理
		/// </summary>
		protected override void afterUpdate()
		{
			// 会費情報(共通)の再取得
			AppGlobal.InitKaihi();
		}

		/// <summary>
		/// 追加時の会費属性を取得します。
		/// 医師会会費はコードの先頭桁（1：日医／2：県医／3：市医）で決め、それ以外は「その他」とします。
		/// </summary>
		/// <param name="code">会費コード</param>
		eKaihiZokusei getDefaultZokusei(int code)
		{
			if (kaihiType == eTypeKaihi.Ishikai)
			{
				switch (code.ToString().Substring(0, 1))
				{
					case "1": return eKaihiZokusei.Nichii;
					case "2": return eKaihiZokusei.Keni;
					case "3": return eKaihiZokusei.Shii;
				}
			}

			return eKaihiZokusei.Etc;
		}
	}
}
