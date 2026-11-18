using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App
{
	/// <summary>
	/// [作成者 CC]
	/// 医療機関マスターのExcel取込
	/// 医療機関コードをキーに、あれば上書き・なければ追加する。Excelにない医療機関は変更しない。
	/// </summary>
	public class AppExcelImportIryoKikan : AppExcelImport
	{
		// Excelの見出し名（半角カナ・全角数字は元ファイルの表記のまま）
		const string COL_Code = "医療機関ｺｰﾄﾞ";
		const string COL_Name = "施設正式名";
		const string COL_Kana = "施設ｶﾅ";
		const string COL_Post = "郵便番号";
		const string COL_Addr1 = "住所１";
		const string COL_Addr2 = "住所２";
		const string COL_Tel = "TEL";
		const string COL_Fax = "FAX";
		const string COL_Taikai = "退会区分";
		const string COL_Kumi = "組ｺｰﾄﾞ";
		/// <summary>診療科目の見出しの先頭（診療科目１～診療科目10）</summary>
		const string COL_ShinryokaPrefix = "診療科目";

		const string TAIKAI_Kaiin = "会員";
		const string TAIKAI_Taikai = "退会";

		/// <summary>
		/// 検証済みの1行分
		/// </summary>
		class ImportRow
		{
			public int Code;
			public string Name;
			public string Kana;
			public string Post;
			public string Addr1;
			public string Addr2;
			public string Tel;
			public string Fax;
			public bool TaikaiKbn;
			/// <summary>組コードのID。未入力はnull</summary>
			public int? KumiID;
			/// <summary>診療科のID（Excelの並び順）</summary>
			public List<int> ShinryokaIDs = new List<int>();
		}

		List<ImportRow> rows;

		/// <summary>
		/// 取込前に、医療機関と医療機関の診療科を全て削除するか。
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
				return new string[]
				{
					COL_Code, COL_Name, COL_Kana, COL_Post, COL_Addr1, COL_Addr2,
					COL_Tel, COL_Fax, COL_Taikai, COL_Kumi,
				};
			}
		}

		/// <summary>
		/// 読み込んだデータを検証します。
		/// </summary>
		protected override bool validateData()
		{
			rows = new List<ImportRow>();

			int cnt_error = 0;
			Dictionary<int, int> codes = new Dictionary<int, int>(); // 医療機関コード → Excelの行番号

			// 診療科目の列（Excelの列順）
			List<string> shinryoCols = new List<string>();
			foreach (DataColumn col in dtSrc.Columns)
			{
				if (col.ColumnName.StartsWith(COL_ShinryokaPrefix) == true)
				{
					shinryoCols.Add(col.ColumnName);
				}
			}

			foreach (DataRow src in dtSrc.Rows)
			{
				int rowno = getRowNo(src);
				bool ok = true;
				ImportRow rec = new ImportRow();

				// 医療機関コード
				string code = getValue(src, COL_Code);
				if (int.TryParse(code, out rec.Code) == false || rec.Code <= 0 || rec.Code > AppDbID.Max_Code)
				{
					Log.Apend(LogTable.eStatus.Error, $"{rowno}行目：医療機関コード「{code}」が正しくありません。");
					cnt_error++;
					continue;
				}

				string who = $"{rowno}行目（{rec.Code}）";

				if (codes.ContainsKey(rec.Code) == true)
				{
					Log.Apend(LogTable.eStatus.Error, $"{who}：医療機関コードが{codes[rec.Code]}行目と重複しています。");
					ok = false;
				}
				else
				{
					codes.Add(rec.Code, rowno);
				}

				rec.Name = getValue(src, COL_Name);
				rec.Kana = getValue(src, COL_Kana);
				rec.Post = getValue(src, COL_Post);
				rec.Addr1 = getValue(src, COL_Addr1);
				rec.Addr2 = getValue(src, COL_Addr2);

				// TEL・FAX
				bool warn;
				rec.Tel = formatTel(getValue(src, COL_Tel), out warn);
				if (warn == true)
				{
					Log.Apend(LogTable.eStatus.Warning, $"{who}：TEL「{rec.Tel}」は区切り位置を判定できないため、ハイフンなしで登録します。");
				}
				rec.Fax = formatTel(getValue(src, COL_Fax), out warn);
				if (warn == true)
				{
					Log.Apend(LogTable.eStatus.Warning, $"{who}：FAX「{rec.Fax}」は区切り位置を判定できないため、ハイフンなしで登録します。");
				}

				// 退会区分
				string taikai = getValue(src, COL_Taikai);
				if (taikai == TAIKAI_Kaiin)
				{
					rec.TaikaiKbn = true;
				}
				else
				if (taikai == TAIKAI_Taikai)
				{
					rec.TaikaiKbn = false;
				}
				else
				{
					Log.Apend(LogTable.eStatus.Error, $"{who}：退会区分「{taikai}」が正しくありません。（{TAIKAI_Kaiin}／{TAIKAI_Taikai}）");
					ok = false;
				}

				// 組コード（コード → ID）
				string kumi = getValue(src, COL_Kumi);
				if (kumi != "")
				{
					int kumicd;
					KumiCode kc = null;

					if (int.TryParse(kumi, out kumicd) == true)
					{
						kc = AppGlobal.KumiCodes.GetCode(kumicd);
					}

					if (kc == null)
					{
						Log.Apend(LogTable.eStatus.Error, $"{who}：組コード「{kumi}」が組コードマスターに登録されていません。");
						ok = false;
					}
					else
					{
						rec.KumiID = kc.ID;
					}
				}

				// 診療科目（コード → ID）
				foreach (string colname in shinryoCols)
				{
					string srk = getValue(src, colname);
					if (srk == "")
					{
						continue;
					}

					int srkcd;
					Shinryoka sk = null;

					if (int.TryParse(srk, out srkcd) == true)
					{
						sk = AppGlobal.Shinryokas.GetCode(srkcd);
					}

					if (sk == null)
					{
						Log.Apend(LogTable.eStatus.Error, $"{who}：{colname}「{srk}」が診療科マスターに登録されていません。");
						ok = false;
					}
					else
					if (rec.ShinryokaIDs.Contains(sk.ID) == true)
					{
						Log.Apend(LogTable.eStatus.Warning, $"{who}：{colname}「{srk}」は同じ行で重複しているため、1件だけ登録します。");
					}
					else
					{
						rec.ShinryokaIDs.Add(sk.ID);
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
			DataTable tblKikan = AppGlobal.DB.GetFillTable(TableProp.t_iryokikan);
			DataTable tblShinryo = AppGlobal.DB.GetFillTable(TableProp.t_iryokikan_shinryoka);

			try
			{
				if (ClearAll == true)
				{
					//¶ 2026/11/14 CC 全クリア。以降は既存データなしとして全件を追加する（IDは1から振り直しになる）
					int cnt_del = tblKikan.Rows.Count;

					foreach (DataRow row in tblShinryo.Select())
					{
						row.Delete();
					}
					foreach (DataRow row in tblKikan.Select())
					{
						row.Delete();
					}

					Log.Apend(LogTable.eStatus.None, $"登録済みの医療機関{cnt_del}件を削除しました。");
					//¶ 2026/11/14 CC 会員は医療機関をIDで参照しているが、付け替えはしない（会員も取込し直す前提。ユーザー了承）
					Log.Apend(LogTable.eStatus.Warning, "医療機関のIDを振り直したため、登録済みの会員の所属医療機関は再設定が必要です。");
				}

				// 既存の医療機関（コード → 行）と、IDの最大値
				Dictionary<int, DataRow> dicKikan = new Dictionary<int, DataRow>();
				int maxid = 0;

				foreach (DataRow row in tblKikan.Rows)
				{
					if (row.RowState == DataRowState.Deleted)
					{
						continue;
					}

					t_iryokikan xrow = new t_iryokikan(row);

					int cd = Cast.Int(xrow.IRK_Code_Null);
					if (dicKikan.ContainsKey(cd) == false)
					{
						dicKikan.Add(cd, row);
					}
					if (xrow.ID_Iryokikan > maxid)
					{
						maxid = xrow.ID_Iryokikan;
					}
				}

				// 既存の診療科（医療機関ID → 行。登録順）
				Dictionary<int, List<DataRow>> dicShinryo = new Dictionary<int, List<DataRow>>();

				foreach (DataRow row in tblShinryo.Rows)
				{
					if (row.RowState == DataRowState.Deleted)
					{
						continue;
					}

					int id =Cast.Int(row[t_iryokikan_shinryoka.FID_Iryokikan]);

					if (dicShinryo.ContainsKey(id) == false)
					{
						dicShinryo.Add(id, new List<DataRow>());
					}
					dicShinryo[id].Add(row);
				}

				int cnt_add = 0;
				int cnt_update = 0;
				int cnt_same = 0;

				foreach (ImportRow rec in rows)
				{
					DataRow row;
					bool isnew = false;
					bool changed = false;

					if (dicKikan.ContainsKey(rec.Code) == true)
					{
						row = dicKikan[rec.Code];
					}
					else
					{
						//¶ 2026/11/14 CC Excelにない項目は、追加時のみ既定値を入れる（病床有無＝無、併設＝なし）。更新時は既存値を残す
						isnew = true;
						maxid++;

						row = tblKikan.NewRow();
						row[t_iryokikan.FID_Iryokikan] = maxid;
						row[t_iryokikan.FIRK_Code] = rec.Code;
						row[t_iryokikan.FIRK_ByoshoUmu] = false;
						row[t_iryokikan.FIRK_Kaigo] = false;
						row[t_iryokikan.FIRK_Etc] = false;
					}

					changed |= setValue(row, t_iryokikan.FIRK_Name, rec.Name);
					changed |= setValue(row, t_iryokikan.FIRK_Kana, rec.Kana);
					//¶ 2026/11/14 CC Excelに通称の列がないため、通称が空なら正式名を、通称カナが空ならカナを入れる（入力済みの通称は変えない）
					if (isEmpty(row[t_iryokikan.FIRK_Tsusho]) == true)
					{
						changed |= setValue(row, t_iryokikan.FIRK_Tsusho, rec.Name);
					}
					if (isEmpty(row[t_iryokikan.FIRK_TsushoKana]) == true)
					{
						changed |= setValue(row, t_iryokikan.FIRK_TsushoKana, rec.Kana);
					}
					changed |= setValue(row, t_iryokikan.FIRK_Post, rec.Post);
					changed |= setValue(row, t_iryokikan.FIRK_Addr1, rec.Addr1);
					changed |= setValue(row, t_iryokikan.FIRK_Addr2, rec.Addr2);
					changed |= setValue(row, t_iryokikan.FIRK_Tel1, rec.Tel);
					changed |= setValue(row, t_iryokikan.FIRK_Fax1, rec.Fax);
					changed |= setValue(row, t_iryokikan.FIRK_TaikaiKbn, rec.TaikaiKbn);
					changed |= setValue(row, t_iryokikan.FIRK_KumiCode, rec.KumiID);

					int id = Cast.Int(row[t_iryokikan.FID_Iryokikan]);

					// 診療科目は、内容が変わる場合だけ入れ直す
					List<DataRow> olds = null;
					if (dicShinryo.ContainsKey(id) == true)
					{
						olds = dicShinryo[id];
					}

					if (isSameShinryoka(olds, rec.ShinryokaIDs) == false)
					{
						changed = true;

						if (olds != null)
						{
							foreach (DataRow old in olds)
							{
								old.Delete();
							}
						}

						foreach (int srkid in rec.ShinryokaIDs)
						{
							DataRow nrow = tblShinryo.NewRow();
							nrow[t_iryokikan_shinryoka.FID_Iryokikan] = id;
							nrow[t_iryokikan_shinryoka.FID_Shinryoka] = srkid;
							nrow[t_iryokikan_shinryoka.FChk_Shinryoka] = true;
							tblShinryo.Rows.Add(nrow);
						}
					}

					if (isnew == true)
					{
						row[t_iryokikan.FLastUpdateUser] = AppGlobal.LoginUser.ID;
						tblKikan.Rows.Add(row);
						cnt_add++;
					}
					else
					if (changed == true)
					{
						row[t_iryokikan.FLastUpdateUser] = AppGlobal.LoginUser.ID;
						cnt_update++;
					}
					else
					{
						cnt_same++;
					}
				}

				if (AppGlobal.DB.UpdateTable(TableProp.t_iryokikan) == false ||
					AppGlobal.DB.UpdateTable(TableProp.t_iryokikan_shinryoka) == false)
				{
					tblKikan.RejectChanges();
					tblShinryo.RejectChanges();
					return false;
				}

				Log.Apend(LogTable.eStatus.Success, $"医療機関マスターを取り込みました。追加：{cnt_add}件／更新：{cnt_update}件／変更なし：{cnt_same}件");

				return true;
			}
			catch
			{
				//¶ 2026/11/14 CC 途中で失敗した場合は、バッファ上の変更を取り消してから呼び出し元へ伝える
				tblKikan.RejectChanges();
				tblShinryo.RejectChanges();
				throw;
			}
		}

		/// <summary>
		/// 反映後の処理
		/// </summary>
		protected override void afterUpdate()
		{
			// 医療機関情報(共通)の再取得
			AppGlobal.InitIryoKikan();
		}

		/// <summary>
		/// 登録済みの診療科と、取り込む診療科が同じ内容（並び順を含む）かを調べます。
		/// </summary>
		bool isSameShinryoka(List<DataRow> olds, List<int> ids)
		{
			int cnt = (olds == null) ? 0 : olds.Count;

			if (cnt != ids.Count)
			{
				return false;
			}

			for (int i = 0; i < cnt; i++)
			{
				if (Cast.Int(olds[i][t_iryokikan_shinryoka.FID_Shinryoka]) != ids[i] ||
					Cast.Bool(olds[i][t_iryokikan_shinryoka.FChk_Shinryoka]) == false)
				{
					return false;
				}
			}

			return true;
		}
	}
}
