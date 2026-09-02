
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
	public partial class t_kaisetsushutai : FieldProp
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
		/// フィールド[開設主体ID]。
		/// </summary>
		public const string FID_Kaisetsushutai = "ID_Kaisetsushutai";
		/// <summary>
		/// 開設主体ID
		/// </summary>
		public int ID_Kaisetsushutai
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Kaisetsushutai]);	}
			set	{	_set(FID_Kaisetsushutai, value);	}
		}
		
		/// <summary>
		/// 開設主体ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Kaisetsushutai_Null
		{
			get	{	if (row == null || row[FID_Kaisetsushutai] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Kaisetsushutai]); }	}
			set	{	_set(FID_Kaisetsushutai, value);	}
		}
		
		/// <summary>
		/// フィールド[開設主体コード]。
		/// </summary>
		public const string FKST_Code = "KST_Code";
		/// <summary>
		/// 開設主体コード
		/// </summary>
		public int KST_Code
		{
			get	{	return Cast.Int(row == null ? null : row[FKST_Code]);	}
			set	{	_set(FKST_Code, value);	}
		}
		
		/// <summary>
		/// 開設主体コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? KST_Code_Null
		{
			get	{	if (row == null || row[FKST_Code] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKST_Code]); }	}
			set	{	_set(FKST_Code, value);	}
		}
		
		/// <summary>
		/// フィールド[開設主体の名称(分類)]。
		/// </summary>
		public const string FKST_NameCategory = "KST_NameCategory";
		/// <summary>
		/// 開設主体の名称(分類)
		/// </summary>
		public string KST_NameCategory
		{
			get	{	return Cast.String(row == null ? null : row[FKST_NameCategory]);	}
			set	{	_set(FKST_NameCategory, value);	}
		}
		
		/// <summary>
		/// 開設主体の名称(分類)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string KST_NameCategory_Null
		{
			get	{	if (row == null || row[FKST_NameCategory] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKST_NameCategory]); }	}
			set	{	_set(FKST_NameCategory, value);	}
		}
		
		/// <summary>
		/// フィールド[開設主体の名称]。
		/// </summary>
		public const string FKST_Name = "KST_Name";
		/// <summary>
		/// 開設主体の名称
		/// </summary>
		public string KST_Name
		{
			get	{	return Cast.String(row == null ? null : row[FKST_Name]);	}
			set	{	_set(FKST_Name, value);	}
		}
		
		/// <summary>
		/// 開設主体の名称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string KST_Name_Null
		{
			get	{	if (row == null || row[FKST_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKST_Name]); }	}
			set	{	_set(FKST_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[開設主体の説明]。
		/// </summary>
		public const string FKST_Description = "KST_Description";
		/// <summary>
		/// 開設主体の説明
		/// </summary>
		public string KST_Description
		{
			get	{	return Cast.String(row == null ? null : row[FKST_Description]);	}
			set	{	_set(FKST_Description, value);	}
		}
		
		/// <summary>
		/// 開設主体の説明。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string KST_Description_Null
		{
			get	{	if (row == null || row[FKST_Description] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKST_Description]); }	}
			set	{	_set(FKST_Description, value);	}
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
		public t_kaisetsushutai(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_kaisetsushutai 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_kaisetsushutai 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_kaisetsushutai");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaisetsushutai, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKST_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKST_NameCategory, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKST_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKST_Description, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
