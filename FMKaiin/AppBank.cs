using ComponentDB;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 銀行情報クラス
	/// </summary>
	public class AppBank
	{
		List<Bank> all_list;
		Dictionary<int, Bank> dics_id;
		Dictionary<int, Bank> dics_cd;

		/// <summary>
		/// 参照用のビュー
		/// </summary>
		public DBView DbView { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppBank()
		{
			all_list = new List<Bank>();
			dics_id = new Dictionary<int, Bank>();
			dics_cd = new Dictionary<int, Bank>();
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			all_list.Clear();
			dics_id.Clear();
			dics_cd.Clear();

			DbView = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_bank).Copy());
			// 銀行＋支店名のフィールド
			DbView.DataTable.Columns.Add(AppTableCombo.Fld_BankName, typeof(string));

			for (int i = 0; i < DbView.Count; i++)
			{
				t_bank xrow = new t_bank(DbView[i]);

				if (AppGlobal.BankCodeMg != null)
				{
					t_bank_code bc = AppGlobal.BankCodeMg.GetBankCodeRow(xrow.BNK_Code, xrow.BNK_CodeShiten);

					if (bc != null)
					{
						// 銀行名＋支店名をセット
						xrow.Row[AppTableCombo.Fld_BankName] = $"{bc.BCD_FullName} {bc.BCD_NameShiten}支店 {xrow.BNK_KozaNo}";
					}
				}

				Bank obj = new Bank(xrow);

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
		/// 銀行クラスを取得します。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Bank Get(int id)
		{
			if (dics_id.ContainsKey(id) == true)
			{
				return dics_id[id];
			}

			return null;
		}

		/// <summary>
		/// 銀行クラスを取得します。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public Bank GetCode(object obj)
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
	/// 銀行情報クラス
	/// </summary>
	public class Bank
	{
		/// <summary>ID</summary>
		public int ID { get; private set; }
		/// <summary>コード</summary>
		public int CD { get; private set; }

		public t_bank XRow { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Bank(t_bank row)
		{
			this.XRow = new t_bank(row.Row);

			this.ID = XRow.ID_Bank;
			this.CD = XRow.BNK_Code;
		}
	}
}
