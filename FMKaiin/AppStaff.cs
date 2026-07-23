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
	/// スタッフ管理用クラス
	/// tachi
	/// </summary>
	public class AppStaff
	{
		List<Staff> all_list;
		Dictionary<int, Staff> dics_id;
		Dictionary<int, Staff> dics_cd;

		/// <summary>
		/// 参照用のビュー
		/// </summary>
		public DBView DbView{get; private set;}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppStaff()
		{
			all_list = new List<Staff>();
			dics_id = new Dictionary<int, Staff>();
			dics_cd = new Dictionary<int, Staff>();
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			all_list.Clear();
			dics_id.Clear();
			dics_cd.Clear();

			DbView = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_staff).Copy());
			// 職種タイプ名称用のフィールド
			DbView.DataTable.Columns.Add(AppTableCombo.Fld_TypeJobName, typeof(string));
			// 文字列コード
			DbView.DataTable.Columns.Add(AppTableCombo.Fld_CoedString, typeof(string));
			DbView.DataTable.Columns[AppTableCombo.Fld_CoedString].SetOrdinal(0);

			for (int i = 0 ; i < DbView.Count ; i++)
			{
				t_staff xrow = new t_staff(DbView[i]);

				// 職種タイプ名称のセット
				xrow.Row[AppTableCombo.Fld_TypeJobName] = enumKbn.DTypeJob[(int)xrow.STF_TypeJob];
				// 0埋めしたコード
				xrow.Row[AppTableCombo.Fld_CoedString] = xrow.CD_Staff.ToString().PadLeft(6, '0');

				Staff obj = new Staff(xrow);

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
		/// スタッフクラスを取得します。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Staff Get(int id)
		{
			if (dics_id.ContainsKey(id) == true)
			{
				return dics_id[id];
			}

			return null;
		}

		/// <summary>
		/// スタッフクラスを取得します。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public Staff GetCode(object obj)
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
	/// スタッフクラス
	/// </summary>
	public class Staff
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
		public string CodeString
		{
			get; private set;
		}

		public t_staff XRow
		{
			get; private set;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Staff(t_staff row)
		{
			this.XRow = new t_staff(row.Row);

			this.ID = XRow.ID_Staff;
			this.CD = XRow.CD_Staff;
			this.CodeString = Cast.String(XRow.Row[AppTableCombo.Fld_CoedString]);

		}
	}
}
