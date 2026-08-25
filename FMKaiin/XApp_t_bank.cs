
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
	public partial class t_bank : FieldProp
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
		/// フィールド[銀行ID]。
		/// </summary>
		public const string FID_Bank = "ID_Bank";
		/// <summary>
		/// 銀行ID
		/// </summary>
		public int ID_Bank
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Bank]);	}
			set	{	_set(FID_Bank, value);	}
		}
		
		/// <summary>
		/// 銀行ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Bank_Null
		{
			get	{	if (row == null || row[FID_Bank] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Bank]); }	}
			set	{	_set(FID_Bank, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行コード]。
		/// </summary>
		public const string FBNK_Code = "BNK_Code";
		/// <summary>
		/// 銀行コード
		/// </summary>
		public int BNK_Code
		{
			get	{	return Cast.Int(row == null ? null : row[FBNK_Code]);	}
			set	{	_set(FBNK_Code, value);	}
		}
		
		/// <summary>
		/// 銀行コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? BNK_Code_Null
		{
			get	{	if (row == null || row[FBNK_Code] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FBNK_Code]); }	}
			set	{	_set(FBNK_Code, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行名]。
		/// </summary>
		public const string FBNK_Name = "BNK_Name";
		/// <summary>
		/// 銀行名
		/// </summary>
		public string BNK_Name
		{
			get	{	return Cast.String(row == null ? null : row[FBNK_Name]);	}
			set	{	_set(FBNK_Name, value);	}
		}
		
		/// <summary>
		/// 銀行名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BNK_Name_Null
		{
			get	{	if (row == null || row[FBNK_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBNK_Name]); }	}
			set	{	_set(FBNK_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[支店コード]。
		/// </summary>
		public const string FBNK_CodeShiten = "BNK_CodeShiten";
		/// <summary>
		/// 支店コード
		/// </summary>
		public int BNK_CodeShiten
		{
			get	{	return Cast.Int(row == null ? null : row[FBNK_CodeShiten]);	}
			set	{	_set(FBNK_CodeShiten, value);	}
		}
		
		/// <summary>
		/// 支店コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? BNK_CodeShiten_Null
		{
			get	{	if (row == null || row[FBNK_CodeShiten] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FBNK_CodeShiten]); }	}
			set	{	_set(FBNK_CodeShiten, value);	}
		}
		
		/// <summary>
		/// フィールド[支店名]。
		/// </summary>
		public const string FBNK_NameShiten = "BNK_NameShiten";
		/// <summary>
		/// 支店名
		/// </summary>
		public string BNK_NameShiten
		{
			get	{	return Cast.String(row == null ? null : row[FBNK_NameShiten]);	}
			set	{	_set(FBNK_NameShiten, value);	}
		}
		
		/// <summary>
		/// 支店名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BNK_NameShiten_Null
		{
			get	{	if (row == null || row[FBNK_NameShiten] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBNK_NameShiten]); }	}
			set	{	_set(FBNK_NameShiten, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行名カナ]。
		/// </summary>
		public const string FBNK_NameFurigana = "BNK_NameFurigana";
		/// <summary>
		/// 銀行名カナ
		/// </summary>
		public string BNK_NameFurigana
		{
			get	{	return Cast.String(row == null ? null : row[FBNK_NameFurigana]);	}
			set	{	_set(FBNK_NameFurigana, value);	}
		}
		
		/// <summary>
		/// 銀行名カナ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BNK_NameFurigana_Null
		{
			get	{	if (row == null || row[FBNK_NameFurigana] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBNK_NameFurigana]); }	}
			set	{	_set(FBNK_NameFurigana, value);	}
		}
		
		/// <summary>
		/// フィールド[支店名カナ]。
		/// </summary>
		public const string FBNK_NameFuriganaShiten = "BNK_NameFuriganaShiten";
		/// <summary>
		/// 支店名カナ
		/// </summary>
		public string BNK_NameFuriganaShiten
		{
			get	{	return Cast.String(row == null ? null : row[FBNK_NameFuriganaShiten]);	}
			set	{	_set(FBNK_NameFuriganaShiten, value);	}
		}
		
		/// <summary>
		/// 支店名カナ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BNK_NameFuriganaShiten_Null
		{
			get	{	if (row == null || row[FBNK_NameFuriganaShiten] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBNK_NameFuriganaShiten]); }	}
			set	{	_set(FBNK_NameFuriganaShiten, value);	}
		}
		
		/// <summary>
		/// フィールド[会社コード]。
		/// </summary>
		public const string FBNK_CodeCompany = "BNK_CodeCompany";
		/// <summary>
		/// 会社コード
		/// </summary>
		public string BNK_CodeCompany
		{
			get	{	return Cast.String(row == null ? null : row[FBNK_CodeCompany]);	}
			set	{	_set(FBNK_CodeCompany, value);	}
		}
		
		/// <summary>
		/// 会社コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BNK_CodeCompany_Null
		{
			get	{	if (row == null || row[FBNK_CodeCompany] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBNK_CodeCompany]); }	}
			set	{	_set(FBNK_CodeCompany, value);	}
		}
		
		/// <summary>
		/// フィールド[口座区分 0/None/ 1/Futsu/普通 2/Touza/当座]。
		/// </summary>
		public const string FBNK_KozaType = "BNK_KozaType";
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座
		/// </summary>
		public eTypeKoza BNK_KozaType
		{
			get	{	return (eTypeKoza)Cast.Int(row == null ? null : row[FBNK_KozaType]);	}
			set	{	_set(FBNK_KozaType, (int)value);	}
		}
		
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? BNK_KozaType_Null
		{
			get	{	if (row == null || row[FBNK_KozaType] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FBNK_KozaType]); }	}
			set	{	_set(FBNK_KozaType, value);	}
		}
		
		/// <summary>
		/// フィールド[口座番号]。
		/// </summary>
		public const string FBNK_KozaNo = "BNK_KozaNo";
		/// <summary>
		/// 口座番号
		/// </summary>
		public string BNK_KozaNo
		{
			get	{	return Cast.String(row == null ? null : row[FBNK_KozaNo]);	}
			set	{	_set(FBNK_KozaNo, value);	}
		}
		
		/// <summary>
		/// 口座番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BNK_KozaNo_Null
		{
			get	{	if (row == null || row[FBNK_KozaNo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBNK_KozaNo]); }	}
			set	{	_set(FBNK_KozaNo, value);	}
		}
		
		/// <summary>
		/// フィールド[FDファイル名]。
		/// </summary>
		public const string FBNK_FDFile = "BNK_FDFile";
		/// <summary>
		/// FDファイル名
		/// </summary>
		public string BNK_FDFile
		{
			get	{	return Cast.String(row == null ? null : row[FBNK_FDFile]);	}
			set	{	_set(FBNK_FDFile, value);	}
		}
		
		/// <summary>
		/// FDファイル名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string BNK_FDFile_Null
		{
			get	{	if (row == null || row[FBNK_FDFile] == System.DBNull.Value) { return null; } else { return Cast.String(row[FBNK_FDFile]); }	}
			set	{	_set(FBNK_FDFile, value);	}
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
		public t_bank(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_bank 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_bank 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_bank");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Bank, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_Code, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_CodeShiten, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_NameShiten, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_NameFurigana, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_NameFuriganaShiten, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_CodeCompany, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_KozaType, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_KozaNo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FBNK_FDFile, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
