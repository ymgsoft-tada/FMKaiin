using ComponentDB;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// [作成者 tanaka]
	/// 銀行コードを管理するクラス
	/// </summary>
	public class BankCodeManager
	{
		/// <summary>
		/// 銀行だけのビュー
		/// </summary>
		public DBView BankView { get; private set; }

		/// <summary>
		/// 支店を含む全てのビュー
		/// </summary>
		public DBView DbView { get; private set; }

		Dictionary<int, BankCode> dics;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BankCodeManager()
		{
//			Init(); // 他と合わせてnew時はInitなし、別途実施
		}

		/// <summary>
		/// 初期化処理
		/// </summary>
		public void Init()
		{
			DbView = new DBView(AppGlobal.DB.GetFillTable(TableProp.t_bank_code));
			BankView = new DBView(t_bank_code.GetTable());

			dics = new Dictionary<int, BankCode>();

			for (int i = 0; i < DbView.Count; i++)
			{
				t_bank_code xrow = new t_bank_code(DbView[i]);

				if (xrow.BCD_Code > 0)
				{
					if (dics.ContainsKey(xrow.BCD_Code) == false)
					{
						dics.Add(xrow.BCD_Code, new BankCode(xrow));

						DataRow nrow = BankView.NewRow();
						AppDb.CopyDataRow(xrow.Row, nrow);
						BankView.Add(nrow);
					}
				
					dics[xrow.BCD_Code].AddShiten(xrow);
				}
			}
		}

		/// <summary>
		/// 指定した銀行コードに該当するBankCodeクラスを取得します。
		/// </summary>
		/// <param name="cd"></param>
		/// <returns></returns>
		public BankCode GetBankCode(object cd)
		{
			int code = Cast.Int(cd);
			if (dics.ContainsKey(code))
			{
				return dics[code];
			}

			return null;
		}

		/// <summary>
		/// 指定した銀行コード・支店コードに該当するt_bank_codeクラスを取得します。
		/// </summary>
		/// <param name="cd"></param>
		/// <param name="cd_shiten"></param>
		/// <returns></returns>
		public t_bank_code GetBankCodeRow(object cd, object cd_shiten)
		{
			BankCode bc = GetBankCode(cd);

			if (bc != null)
			{
				return bc.GetShiten(cd_shiten);
			}

			return null;
		}
	}

	/// <summary>
	/// 銀行コードクラス
	/// </summary>
	public class BankCode
	{
		/// <summary>銀行コード</summary>
		public int Code { get{ return XRow.BCD_Code; } }
		/// <summary>銀行名</summary>
		public string Name { get{ return XRow.BCD_FullName;} }
		/// <summary>銀行フリガナ</summary>
		public string Furigana { get{ return XRow.BCD_NameFurigana;} }
		/// <summary>銀行コードレコード</summary>
		public t_bank_code XRow { get; private set; }

		Dictionary<int, t_bank_code> shiten;

		/// <summary>コンストラクタ</summary>
		public BankCode(t_bank_code xrow)
		{
			this.XRow = xrow;
			shiten = new Dictionary<int, t_bank_code>();
		}

		/// <summary>
		/// 支店の追加
		/// </summary>
		/// <param name="xrow"></param>
		public void AddShiten(t_bank_code xrow)
		{
			if (shiten.ContainsKey(xrow.BCD_CodeShiten) == false)
			{
				shiten.Add(xrow.BCD_CodeShiten, xrow);
			}
		}

		/// <summary>
		/// 指定した支店コードを取得します。
		/// </summary>
		/// <param name="cd_shiten"></param>
		/// <returns></returns>
		public t_bank_code GetShiten(object cd_shiten)
		{
			int code = Cast.Int(cd_shiten);
			if (shiten.ContainsKey(code) == true)
			{
				return shiten[code];
			}

			return null;
		}


	}
}
