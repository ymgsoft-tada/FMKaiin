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
	/// 開設主体 管理用クラス
	/// </summary>
	public class AppKaisetsushutai
	{
		List<Kaisetsushutai> all_list;
		Dictionary<int, Kaisetsushutai> dics_id;
		Dictionary<int, Kaisetsushutai> dics_cd;

		/// <summary>
		/// 参照用のビュー
		/// </summary>
		public DBView DbView{get; private set;}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppKaisetsushutai()
		{
			all_list = new List<Kaisetsushutai>();
			dics_id = new Dictionary<int, Kaisetsushutai>();
			dics_cd = new Dictionary<int, Kaisetsushutai>();
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			all_list.Clear();
			dics_id.Clear();
			dics_cd.Clear();

			DbView = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_kaisetsushutai).Copy());

			// テーブルカラム以外で必要な情報を追加？数値カラムの文字型(検索用)など
			// 職種タイプ名称用のフィールド
//			DbView.DataTable.Columns.Add(AppTableCombo.Fld_TypeJobName, typeof(string));
			// 文字列コード
//			DbView.DataTable.Columns.Add(AppTableCombo.Fld_CoedString, typeof(string));
//			DbView.DataTable.Columns[AppTableCombo.Fld_CoedString].SetOrdinal(0);

			for (int i = 0 ; i < DbView.Count ; i++)
			{
				t_kaisetsushutai xrow = new t_kaisetsushutai(DbView[i]);

				// 職種タイプ名称のセット
				//			    xrow.Row[AppTableCombo.Fld_TypeJobName] = enumKbn.DTypeJob[(int)xrow.STF_TypeJob];
				// 0埋めしたコード
				//				xrow.Row[AppTableCombo.Fld_CoedString] = xrow.CD_Kaisetsusyutai.ToString().PadLeft(6, '0');

				Kaisetsushutai obj = new Kaisetsushutai(xrow);

				all_list.Add(obj);

				if (dics_id.ContainsKey(obj.ID) == false)
				{
					dics_id.Add(obj.ID, obj);
				}
				if (dics_cd.ContainsKey(obj.CD) == false)
				{
					dics_cd.Add(obj.CD, obj);
				}
			}
		}

		/// <summary>
		/// IDをキーに情報を取得します。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Kaisetsushutai Get(int id)
		{
			if (dics_id.ContainsKey(id) == true)
			{
				return dics_id[id];
			}

			return null;
		}

		/// <summary>
		/// コードをキーに情報を取得します。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public Kaisetsushutai GetCode(object obj)
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
	/// 開設主体クラス
	/// </summary>
	public class Kaisetsushutai
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
		/// <summary>コード（文字列）</summary>
//		public string CodeString
//		{
//			get; private set;
//		}

		public t_kaisetsushutai XRow
		{
			get; private set;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Kaisetsushutai(t_kaisetsushutai row)
		{
			this.XRow = new t_kaisetsushutai(row.Row);

			this.ID = XRow.ID_Kaisetsushutai;
			this.CD = XRow.KST_Code;
//			this.CodeString = Cast.String(XRow.Row[AppTableCombo.Fld_CoedString]);
		}
	}
}
