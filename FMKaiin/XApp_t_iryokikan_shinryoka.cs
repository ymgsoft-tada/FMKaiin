
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
	public partial class t_iryokikan_shinryoka : FieldProp
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
		/// フィールド[標榜科目(診療科)ID]。
		/// </summary>
		public const string FID_Shinryoka = "ID_Shinryoka";
		/// <summary>
		/// 標榜科目(診療科)ID
		/// </summary>
		public int ID_Shinryoka
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Shinryoka]);	}
			set	{	_set(FID_Shinryoka, value);	}
		}
		
		/// <summary>
		/// 標榜科目(診療科)ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Shinryoka_Null
		{
			get	{	if (row == null || row[FID_Shinryoka] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Shinryoka]); }	}
			set	{	_set(FID_Shinryoka, value);	}
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
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="o">編集する行のDataRow、DataRowView、DBViewのどれか。DBViewの場合、現在指している行のデータになります。</param>
		public t_iryokikan_shinryoka(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_iryokikan_shinryoka 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_iryokikan_shinryoka 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_iryokikan_shinryoka");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Iryokikan, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Shinryoka, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Kana, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_Tsusho, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FIRK_KaisetsuShutai, typeof(int));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
