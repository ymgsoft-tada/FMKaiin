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
		/// コンストラクタ
		/// </summary>
		public Kaiin(t_kaiin row)
		{
			this.XRow = new t_kaiin(row.Row);

			this.ID = XRow.ID_Kaiin;
			this.CD = XRow.CD_Kaiin;
			this.CodeString = Cast.String(XRow.Row[AppTableCombo.Fld_CoedString]);

		}
	}
}
