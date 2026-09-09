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
	/// 施設・業務 管理用クラス
	/// </summary>
	public class AppShisetsugyomu
	{
		List<Shisetsugyomu> all_list;
		Dictionary<int, Shisetsugyomu> dics_id;
		Dictionary<int, Shisetsugyomu> dics_cd;

		/// <summary>
		/// 参照用のビュー
		/// </summary>
		public DBView DbView{get; private set;}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppShisetsugyomu()
		{
			all_list = new List<Shisetsugyomu>();
			dics_id = new Dictionary<int, Shisetsugyomu>();
			dics_cd = new Dictionary<int, Shisetsugyomu>();
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			all_list.Clear();
			dics_id.Clear();
			dics_cd.Clear();

			DbView = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_shisetsugyomu).Copy());

			// テーブルカラム以外で必要な情報を追加？数値カラムの文字型(検索用)など
			// 職種タイプ名称用のフィールド
//			DbView.DataTable.Columns.Add(AppTableCombo.Fld_TypeJobName, typeof(string));
			// 文字列コード
//			DbView.DataTable.Columns.Add(AppTableCombo.Fld_CoedString, typeof(string));
//			DbView.DataTable.Columns[AppTableCombo.Fld_CoedString].SetOrdinal(0);

			for (int i = 0 ; i < DbView.Count ; i++)
			{
				t_shisetsugyomu xrow = new t_shisetsugyomu(DbView[i]);

				// 職種タイプ名称のセット
//			    xrow.Row[AppTableCombo.Fld_TypeJobName] = enumKbn.DTypeJob[(int)xrow.STF_TypeJob];
				// 0埋めしたコード
//				xrow.Row[AppTableCombo.Fld_CoedString] = xrow.CD_Shisetsugyomu.ToString().PadLeft(6, '0');

				Shisetsugyomu obj = new Shisetsugyomu(xrow);

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
		public Shisetsugyomu Get(int id)
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
		public Shisetsugyomu GetCode(object obj)
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
	/// 施設・業務クラス
	/// </summary>
	public class Shisetsugyomu
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

		public t_shisetsugyomu XRow
		{
			get; private set;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Shisetsugyomu(t_shisetsugyomu row)
		{
			this.XRow = new t_shisetsugyomu(row.Row);

			this.ID = XRow.ID_ShisetsuGyomu;
			this.CD = XRow.SGY_Code;
//			this.CodeString = Cast.String(XRow.Row[AppTableCombo.Fld_CoedString]);
		}
	}
}
