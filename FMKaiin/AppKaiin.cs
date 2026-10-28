using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentDB;
using ComponentIO;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 会員管理用クラス
	/// </summary>
	public class AppKaiin
	{
		List<Kaiin> all_list;
		Dictionary<int, Kaiin> dics_id;
		Dictionary<int, Kaiin> dics_cd;
		// グループIDをキーとした履歴情報
		Dictionary<int, List<Kaiin>> grp_history;

		/// <summary>
		/// 参照用のビュー
		/// </summary>
		public DBView DbView{get; private set;}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppKaiin()
		{
			all_list = new List<Kaiin>();
			dics_id = new Dictionary<int, Kaiin>();
			dics_cd = new Dictionary<int, Kaiin>();
			grp_history = new Dictionary<int, List<Kaiin>>();
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			all_list.Clear();
			dics_id.Clear();
			dics_cd.Clear();

			DbView = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin).Copy());
			// 職種タイプ名称用のフィールド
//			DbView.DataTable.Columns.Add(AppTableCombo.Fld_TypeJobName, typeof(string));
			// 文字列コード
			DbView.DataTable.Columns.Add(AppTableCombo.Fld_CoedString, typeof(string));
			DbView.DataTable.Columns[AppTableCombo.Fld_CoedString].SetOrdinal(0);

			for (int i = 0 ; i < DbView.Count ; i++)
			{
				t_kaiin xrow = new t_kaiin(DbView[i]);

				// 職種タイプ名称のセット
//				xrow.Row[AppTableCombo.Fld_TypeJobName] = enumKbn.DTypeJob[(int)xrow.STF_TypeJob];
				// 0埋めしたコード 仕様の確認および必要か検討
				xrow.Row[AppTableCombo.Fld_CoedString] = xrow.CD_Kaiin.ToString().PadLeft(6, '0');

				Kaiin obj = new Kaiin(xrow);

				all_list.Add(obj);

				if (dics_id.ContainsKey(obj.ID) == false)
				{
					dics_id.Add(obj.ID, obj);
				}
				if (dics_cd.ContainsKey(obj.CD) == false)
				{
					dics_cd.Add(obj.CD, obj);
				}

				if (xrow.ID_KaiinGroup > 0)
				{
					if (grp_history.ContainsKey(obj.GroupID) == false)
					{
						grp_history.Add(obj.GroupID, new List<Kaiin>());
					}
					else
					{
						//　再入会の人は過去分を登録
						obj.AddHistory(grp_history[obj.GroupID]);
					}
					grp_history[obj.GroupID].Add(obj);
				}
			}

			//¶kj 各会員の会費情報をセット
			DBView dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_kaihi));
			for (int i = 0; i < dv.Count; i++)
			{
				t_kaiin_kaihi xrow = new t_kaiin_kaihi(dv[i]);

				if (dics_id.ContainsKey(xrow.ID_Kaiin))
				{
					dics_id[xrow.ID_Kaiin].AddKaihi(xrow);
				}
			}

			// 学会情報の登録
			dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_gakkai));
			for (int i = 0; i < dv.Count; i++)
			{
				t_kaiin_gakkai xrow = new t_kaiin_gakkai(dv[i]);

				if (dics_id.ContainsKey(xrow.ID_Kaiin))
				{
					dics_id[xrow.ID_Kaiin].AddGakkai(xrow);
				}
			}

			// 診療科の登録
			dv = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaiin_shinryoka));
			for (int i = 0; i < dv.Count; i++)
			{
				t_kaiin_shinryoka xrow = new t_kaiin_shinryoka(dv[i]);

				if (dics_id.ContainsKey(xrow.ID_Kaiin))
				{
					dics_id[xrow.ID_Kaiin].AddShinryoka(xrow);
				}
			}
		}

		/// <summary>
		/// 会員クラスを取得します。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Kaiin Get(int id)
		{
			if (dics_id.ContainsKey(id) == true)
			{
				return dics_id[id];
			}

			return null;
		}

		/// <summary>
		/// 会員クラスを取得します。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public Kaiin GetCode(object obj)
		{
			int cd = Cast.Int(obj);
			if (dics_cd.ContainsKey(cd) == true)
			{
				return dics_cd[cd];
			}

			return null;
		}
	}

	/// <summary>
	/// 会員クラス
	/// </summary>
	public class Kaiin
	{
		/// <summary>ID</summary>
		public int ID
		{
			get; private set;
		}
		/// <summary>コード</summary>
		public int CD
		{
			get; private set;
		}
		/// <summary>グループID</summary>
		public int GroupID
		{
			get; private set;
		}
		/// <summary>コード（文字列）</summary>
		public string CodeString
		{
			get; private set;
		}

		public t_kaiin XRow
		{
			get; private set;
		}

		/// <summary>
		/// 所属している会費情報
		/// </summary>
		public List<Kaihi> Kaihis
		{
			get; private set;
		}

		/// <summary>
		/// 所属している学会
		/// </summary>
		public List<Gakkai> Gakkais
		{
			get; private set;
		}

		/// <summary>
		/// 担当している診療科
		/// </summary>
		public List<Shinryoka> Shinryokas
		{
			get; private set;
		}

		// 過去の入会情報
		List<Kaiin> history;

		// 会費単位の支払方法
		Dictionary<int, eShiharai> dic_shiharai;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Kaiin(t_kaiin row)
		{
			this.XRow = new t_kaiin(row.Row);

			this.ID = XRow.ID_Kaiin;
			this.CD = XRow.CD_Kaiin;
			this.GroupID = XRow.ID_KaiinGroup;
			this.CodeString = Cast.String(XRow.Row[AppTableCombo.Fld_CoedString]);

			Kaihis = new List<Kaihi>();
			Gakkais = new List<Gakkai>();
			Shinryokas  = new List<Shinryoka>();
			dic_shiharai = new Dictionary<int, eShiharai>();
			history = new List<Kaiin>();

		}

		/// <summary>
		/// 指定された会費の支払方法を取得します。
		/// </summary>
		/// <param name="kai"></param>
		/// <returns></returns>
		public eShiharai GetShiharai(Kaihi kai)
		{
			return GetShiharai(kai.ID);
		}

		/// <summary>
		/// 指定された会費の支払方法を取得します。
		/// </summary>
		public eShiharai GetShiharai(int id)
		{
			if (dic_shiharai.ContainsKey(id))
			{
				return dic_shiharai[id];
			}

			return eShiharai.None;
		}

		/// <summary>
		/// 会費の登録
		/// </summary>
		/// <param name="xrow">会費レコード</param>
		public void AddKaihi(t_kaiin_kaihi xrow)
		{
			if (xrow != null && xrow.ID_Kaiin == ID)
			{
				Kaihis.Add(AppGlobal.Kaihis.Get(xrow.ID_Kaihi));

				if (dic_shiharai.ContainsKey(xrow.ID_Kaihi) == false)
				{
					dic_shiharai.Add(xrow.ID_Kaihi, xrow.KaihiShiharai);
				}
			}
		}

		/// <summary>
		/// 学会の登録
		/// </summary>
		/// <param name="xrow">学会レコード</param>
		public void AddGakkai(t_kaiin_gakkai xrow)
		{
			if (xrow != null && xrow.ID_Kaiin == ID)
			{
				Gakkais.Add(AppGlobal.Gakkais.Get(xrow.ID_Gakkai));
			}
		}

		/// <summary>
		/// 診療科の登録
		/// </summary>
		/// <param name="xrow">学会レコード</param>
		public void AddShinryoka(t_kaiin_shinryoka xrow)
		{
			if (xrow != null && xrow.ID_Kaiin == ID)
			{
				Shinryokas.Add(AppGlobal.Shinryokas.Get(xrow.ID_Shinryoka));
			}
		}

		/// <summary>
		/// 履歴の登録
		/// </summary>
		/// <param name="vals"></param>
		public void AddHistory(List<Kaiin> vals)
		{
			history.AddRange(vals);
		}

		/// <summary>
		/// 過去の入会情報を取得します。
		/// </summary>
		/// <returns></returns>
		public List<Kaiin> GetHistoryList()
		{
			return history.ToList();
		}

		/// <summary>
		/// 在籍期間の合計月数を取得します。
		/// 現在の会員情報と過去の会員情報（再入会前）の在籍月数を合算します。
		/// 在籍中の場合はシステム日付までを期間とします。
		/// </summary>
		/// <returns>在籍月数の合計</returns>
		public int GetTotalMembershipMonths()
		{
			// 現在の会員情報
			int total = GetMembershipMonths();

			// 過去の会員情報（退会日なしは算出対象外）
			foreach (Kaiin ka in history)
			{
				if (ka.XRow.Kaiin_DateTaikai_Null == null)
					continue;

				total += GetMembershipMonths(ka.XRow.Kaiin_DateNyukai_Null, ka.XRow.Kaiin_DateTaikai_Null.Value);
			}

			return total;
		}

		/// <summary>
		/// 在籍期間の合計を「n年nヶ月」の文字列で取得します。
		/// </summary>
		/// <returns>在籍期間の文字列</returns>
		public string GetTotalMembershipYearMonth()
		{
			int months = GetTotalMembershipMonths();

			return $"{months / 12}年{months % 12}ヶ月";
		}

		/// <summary>
		/// 現在の会員情報の在籍月数を取得します。
		/// 在籍中の場合はシステム日付までを期間とします。
		/// </summary>
		/// <returns>在籍月数</returns>
		public int GetMembershipMonths()
		{
			return GetMembershipMonths(XRow.Kaiin_DateNyukai_Null, XRow.Kaiin_DateTaikai_Null ?? DateTime.Today);
		}

		/// <summary>
		/// 現在の会員情報の在籍期間を「n年nヶ月」の文字列で取得します。
		/// </summary>
		/// <returns>在籍期間の文字列</returns>
		public string GetMembershipYearMonth()
		{
			int months = GetMembershipMonths();

			return $"{months / 12}年{months % 12}ヶ月";
		}

		/// <summary>
		/// 1期間の在籍月数を取得します。入会日がない、または期間が逆転している場合は0を返します。
		/// </summary>
		/// <param name="from">入会日</param>
		/// <param name="to">退会日（在籍中はシステム日付）</param>
		/// <returns>在籍月数</returns>
		private static int GetMembershipMonths(DateTime? from, DateTime to)
		{
			if (from == null || to < from.Value)
				return 0;

			DateTime f = from.Value.Date;
			DateTime t = to.Date.AddDays(1);	// 退会日（当日）を在籍に含める

			int months = (t.Year - f.Year) * 12 + t.Month - f.Month;

			// 経過していない月は差し引く
			if (t.Day < f.Day)
				months--;

			return months;
		}
	}
}
