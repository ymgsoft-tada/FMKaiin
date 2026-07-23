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
	/// 担当者管理用クラス
	/// tachi
	/// </summary>
	public class AppTanto
	{
		List<Tanto> all_list;
		Dictionary<int, Tanto> dics_id;
		Dictionary<int, Tanto> dics_cd;

		/// <summary>
		/// 参照用のビュー
		/// </summary>
		public DBView DbView{ get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppTanto()
		{
			all_list = new List<Tanto>();
			dics_id = new Dictionary<int, Tanto>();
			dics_cd = new Dictionary<int, Tanto>();
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			all_list.Clear();
			dics_id.Clear();
			dics_cd.Clear();

			DbView = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_tantosha).Copy());

			for (int i = 0 ; i < DbView.Count ; i++)
			{
				t_tantosha xrow = new t_tantosha(DbView[i]);

				Tanto obj = new Tanto(xrow);

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
		/// 職務クラスを取得します。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Tanto Get(int id)
		{
			if (dics_id.ContainsKey(id) == true)
			{
				return dics_id[id];
			}

			return null;
		}

		/// <summary>
		/// 職務クラスを取得します。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public Tanto GetCode(object obj)
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
	/// 職務クラス
	/// </summary>
	public class Tanto
	{
		/// <summary>ID</summary>
		public int ID { get; private set; }
		/// <summary>コード</summary>
		public int CD { get; private set; }
		/// <summary>名前</summary>
		public string Name { get; private set; }
		/// <summary>マイナンバー管理の有無</summary>
		public bool AvailableMyno { get; private set; }

		public t_tantosha XRow { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Tanto(t_tantosha row)
		{
			this.XRow = new t_tantosha(row.Row);

			this.ID = XRow.ID_Tanto;
			this.CD = XRow.CD_Tanto;
			this.Name = XRow.TNT_Name;
			this.AvailableMyno = XRow.TNT_AvailableMyNo;

			// SUは固定
			if (XRow.TNT_Auth == eAuth.SU)
			{
				this.AvailableMyno = true;
			}
		}
	}
}
