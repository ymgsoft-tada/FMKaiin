
//
// ※このプログラムはDBAutoProperties2Access2000により自動的に生成されました。(fj)
//
// MDB File :
//		D:\client\DotNet4.6\Ishikai\FMKaiin\FMKaiin\bin\Debug\system\Data.mdb
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ComponentIO;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// テーブル編集の際に使うクラスです。
	/// </summary>
	public partial class t_iryokikan : FieldProp
	{
		/// <summary>
		/// フィールド[Access 高速検索用]。
		/// </summary>
		public const string FID_Auto = "ID_Auto";
		/// <summary>
		/// Access 高速検索用
		/// </summary>
		public int ID_Auto
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Auto]);	}
			set	{	_set(FID_Auto, value);	}
		}
		
		/// <summary>
		/// Access 高速検索用。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Auto_Null
		{
			get	{	if (row == null || row[FID_Auto] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Auto]); }	}
			set	{	_set(FID_Auto, value);	}
		}
		
		/// <summary>
		/// フィールド[医療機関ID]。
		/// </summary>
		public const string FID_Iryokikan = "ID_Iryokikan";
		/// <summary>
		/// 医療機関ID
		/// </summary>
		public int ID_Iryokikan
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Iryokikan]);	}
			set	{	_set(FID_Iryokikan, value);	}
		}
		
		/// <summary>
		/// 医療機関ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Iryokikan_Null
		{
			get	{	if (row == null || row[FID_Iryokikan] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Iryokikan]); }	}
			set	{	_set(FID_Iryokikan, value);	}
		}
		
		/// <summary>
		/// フィールド[医療機関コード]。
		/// </summary>
		public const string FIRK_Code = "IRK_Code";
		/// <summary>
		/// 医療機関コード
		/// </summary>
		public int IRK_Code
		{
			get	{	return Cast.Int(row == null ? null : row[FIRK_Code]);	}
			set	{	_set(FIRK_Code, value);	}
		}
		
		/// <summary>
		/// 医療機関コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IRK_Code_Null
		{
			get	{	if (row == null || row[FIRK_Code] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIRK_Code]); }	}
			set	{	_set(FIRK_Code, value);	}
		}
		
		/// <summary>
		/// フィールド[医療機関名]。
		/// </summary>
		public const string FIRK_Name = "IRK_Name";
		/// <summary>
		/// 医療機関名
		/// </summary>
		public string IRK_Name
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Name]);	}
			set	{	_set(FIRK_Name, value);	}
		}
		
		/// <summary>
		/// 医療機関名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Name_Null
		{
			get	{	if (row == null || row[FIRK_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Name]); }	}
			set	{	_set(FIRK_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[施設カナ]。
		/// </summary>
		public const string FIRK_Kana = "IRK_Kana";
		/// <summary>
		/// 施設カナ
		/// </summary>
		public string IRK_Kana
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Kana]);	}
			set	{	_set(FIRK_Kana, value);	}
		}
		
		/// <summary>
		/// 施設カナ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Kana_Null
		{
			get	{	if (row == null || row[FIRK_Kana] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Kana]); }	}
			set	{	_set(FIRK_Kana, value);	}
		}
		
		/// <summary>
		/// フィールド[施設通称カナ]。
		/// </summary>
		public const string FIRK_Tsusho = "IRK_Tsusho";
		/// <summary>
		/// 施設通称カナ
		/// </summary>
		public string IRK_Tsusho
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Tsusho]);	}
			set	{	_set(FIRK_Tsusho, value);	}
		}
		
		/// <summary>
		/// 施設通称カナ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Tsusho_Null
		{
			get	{	if (row == null || row[FIRK_Tsusho] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Tsusho]); }	}
			set	{	_set(FIRK_Tsusho, value);	}
		}
		
		/// <summary>
		/// フィールド[郵便番号]。
		/// </summary>
		public const string FIRK_Post = "IRK_Post";
		/// <summary>
		/// 郵便番号
		/// </summary>
		public string IRK_Post
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Post]);	}
			set	{	_set(FIRK_Post, value);	}
		}
		
		/// <summary>
		/// 郵便番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Post_Null
		{
			get	{	if (row == null || row[FIRK_Post] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Post]); }	}
			set	{	_set(FIRK_Post, value);	}
		}
		
		/// <summary>
		/// フィールド[住所１]。
		/// </summary>
		public const string FIRK_Add1 = "IRK_Add1";
		/// <summary>
		/// 住所１
		/// </summary>
		public string IRK_Add1
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Add1]);	}
			set	{	_set(FIRK_Add1, value);	}
		}
		
		/// <summary>
		/// 住所１。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Add1_Null
		{
			get	{	if (row == null || row[FIRK_Add1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Add1]); }	}
			set	{	_set(FIRK_Add1, value);	}
		}
		
		/// <summary>
		/// フィールド[住所２]。
		/// </summary>
		public const string FIRK_Add2 = "IRK_Add2";
		/// <summary>
		/// 住所２
		/// </summary>
		public string IRK_Add2
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Add2]);	}
			set	{	_set(FIRK_Add2, value);	}
		}
		
		/// <summary>
		/// 住所２。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Add2_Null
		{
			get	{	if (row == null || row[FIRK_Add2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Add2]); }	}
			set	{	_set(FIRK_Add2, value);	}
		}
		
		/// <summary>
		/// フィールド[電話番号]。
		/// </summary>
		public const string FIRK_Tel1 = "IRK_Tel1";
		/// <summary>
		/// 電話番号
		/// </summary>
		public string IRK_Tel1
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Tel1]);	}
			set	{	_set(FIRK_Tel1, value);	}
		}
		
		/// <summary>
		/// 電話番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Tel1_Null
		{
			get	{	if (row == null || row[FIRK_Tel1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Tel1]); }	}
			set	{	_set(FIRK_Tel1, value);	}
		}
		
		/// <summary>
		/// フィールド[FAX]。
		/// </summary>
		public const string FIRK_Tes2 = "IRK_Tes2";
		/// <summary>
		/// FAX
		/// </summary>
		public string IRK_Tes2
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Tes2]);	}
			set	{	_set(FIRK_Tes2, value);	}
		}
		
		/// <summary>
		/// FAX。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Tes2_Null
		{
			get	{	if (row == null || row[FIRK_Tes2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Tes2]); }	}
			set	{	_set(FIRK_Tes2, value);	}
		}
		
		/// <summary>
		/// フィールド[開設主体]。
		/// </summary>
		public const string FIRK_KaisetsuShutai = "IRK_KaisetsuShutai";
		/// <summary>
		/// 開設主体
		/// </summary>
		public int IRK_KaisetsuShutai
		{
			get	{	return Cast.Int(row == null ? null : row[FIRK_KaisetsuShutai]);	}
			set	{	_set(FIRK_KaisetsuShutai, value);	}
		}
		
		/// <summary>
		/// 開設主体。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IRK_KaisetsuShutai_Null
		{
			get	{	if (row == null || row[FIRK_KaisetsuShutai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIRK_KaisetsuShutai]); }	}
			set	{	_set(FIRK_KaisetsuShutai, value);	}
		}
		
		/// <summary>
		/// フィールド[病床有無]。
		/// </summary>
		public const string FIRK_ByoshoUmu = "IRK_ByoshoUmu";
		/// <summary>
		/// 病床有無
		/// </summary>
		public bool IRK_ByoshoUmu
		{
			get	{	return Cast.Bool(row == null ? null : row[FIRK_ByoshoUmu]);	}
			set	{	_set(FIRK_ByoshoUmu, value);	}
		}
		
		/// <summary>
		/// フィールド[許可病床]。
		/// </summary>
		public const string FIRK_Kyoka = "IRK_Kyoka";
		/// <summary>
		/// 許可病床
		/// </summary>
		public int IRK_Kyoka
		{
			get	{	return Cast.Int(row == null ? null : row[FIRK_Kyoka]);	}
			set	{	_set(FIRK_Kyoka, value);	}
		}
		
		/// <summary>
		/// 許可病床。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IRK_Kyoka_Null
		{
			get	{	if (row == null || row[FIRK_Kyoka] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIRK_Kyoka]); }	}
			set	{	_set(FIRK_Kyoka, value);	}
		}
		
		/// <summary>
		/// フィールド[介護施設]。
		/// </summary>
		public const string FIRK_Kaigo = "IRK_Kaigo";
		/// <summary>
		/// 介護施設
		/// </summary>
		public bool IRK_Kaigo
		{
			get	{	return Cast.Bool(row == null ? null : row[FIRK_Kaigo]);	}
			set	{	_set(FIRK_Kaigo, value);	}
		}
		
		/// <summary>
		/// フィールド[その他]。
		/// </summary>
		public const string FIRK_Etc = "IRK_Etc";
		/// <summary>
		/// その他
		/// </summary>
		public bool IRK_Etc
		{
			get	{	return Cast.Bool(row == null ? null : row[FIRK_Etc]);	}
			set	{	_set(FIRK_Etc, value);	}
		}
		
		/// <summary>
		/// フィールド[その他メモ]。
		/// </summary>
		public const string FIRK_Memo = "IRK_Memo";
		/// <summary>
		/// その他メモ
		/// </summary>
		public string IRK_Memo
		{
			get	{	return Cast.String(row == null ? null : row[FIRK_Memo]);	}
			set	{	_set(FIRK_Memo, value);	}
		}
		
		/// <summary>
		/// その他メモ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string IRK_Memo_Null
		{
			get	{	if (row == null || row[FIRK_Memo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FIRK_Memo]); }	}
			set	{	_set(FIRK_Memo, value);	}
		}
		
		/// <summary>
		/// フィールド[組コード]。
		/// </summary>
		public const string FIRK_KumiCode = "IRK_KumiCode";
		/// <summary>
		/// 組コード
		/// </summary>
		public int IRK_KumiCode
		{
			get	{	return Cast.Int(row == null ? null : row[FIRK_KumiCode]);	}
			set	{	_set(FIRK_KumiCode, value);	}
		}
		
		/// <summary>
		/// 組コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? IRK_KumiCode_Null
		{
			get	{	if (row == null || row[FIRK_KumiCode] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FIRK_KumiCode]); }	}
			set	{	_set(FIRK_KumiCode, value);	}
		}
		
		/// <summary>
		/// フィールド[退会区分]。
		/// </summary>
		public const string FIRK_TaikaiKbn = "IRK_TaikaiKbn";
		/// <summary>
		/// 退会区分
		/// </summary>
		public bool IRK_TaikaiKbn
		{
			get	{	return Cast.Bool(row == null ? null : row[FIRK_TaikaiKbn]);	}
			set	{	_set(FIRK_TaikaiKbn, value);	}
		}
		
		/// <summary>
		/// フィールド[[要時間]最終更新日時]。
		/// </summary>
		public const string FLastUpdate = "LastUpdate";
		/// <summary>
		/// [要時間]最終更新日時
		/// </summary>
		public DateTime LastUpdate
		{
			get	{	return Cast.DateTime(row == null ? null : row[FLastUpdate]);	}
			set	{	_set_datetime(FLastUpdate, value);	}
		}
		
		/// <summary>
		/// [要時間]最終更新日時。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? LastUpdate_Null
		{
			get	{	if (row == null || row[FLastUpdate] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FLastUpdate]); }	}
			set	{	_set_datetime(FLastUpdate, value);	}
		}
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="o">編集する行のDataRow、DataRowView、DBViewのどれか。DBViewの場合、現在指している行のデータになります。</param>
		public t_iryokikan(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_iryokikan 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_iryokikan 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_iryokikan");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Iryokikan, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Kana, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Tsusho, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Post, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Add1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Add2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Tel1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Tes2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_KaisetsuShutai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_ByoshoUmu, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Kyoka, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Kaigo, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Etc, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Memo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_KumiCode, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_TaikaiKbn, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
