
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
	public partial class t_kaihi : FieldProp
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
		/// フィールド[会費コード(ID)]。
		/// </summary>
		public const string FID_Kaihi = "ID_Kaihi";
		/// <summary>
		/// 会費コード(ID)
		/// </summary>
		public int ID_Kaihi
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Kaihi]);	}
			set	{	_set(FID_Kaihi, value);	}
		}
		
		/// <summary>
		/// 会費コード(ID)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Kaihi_Null
		{
			get	{	if (row == null || row[FID_Kaihi] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Kaihi]); }	}
			set	{	_set(FID_Kaihi, value);	}
		}
		
		/// <summary>
		/// フィールド[会費コード]。
		/// </summary>
		public const string FCD_Kaihi = "CD_Kaihi";
		/// <summary>
		/// 会費コード
		/// </summary>
		public int CD_Kaihi
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_Kaihi]);	}
			set	{	_set(FCD_Kaihi, value);	}
		}
		
		/// <summary>
		/// 会費コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_Kaihi_Null
		{
			get	{	if (row == null || row[FCD_Kaihi] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_Kaihi]); }	}
			set	{	_set(FCD_Kaihi, value);	}
		}
		
		/// <summary>
		/// フィールド[会費区分(ID)]。
		/// </summary>
		public const string FID_KbnKaihi = "ID_KbnKaihi";
		/// <summary>
		/// 会費区分(ID)
		/// </summary>
		public int ID_KbnKaihi
		{
			get	{	return Cast.Int(row == null ? null : row[FID_KbnKaihi]);	}
			set	{	_set(FID_KbnKaihi, value);	}
		}
		
		/// <summary>
		/// 会費区分(ID)。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_KbnKaihi_Null
		{
			get	{	if (row == null || row[FID_KbnKaihi] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_KbnKaihi]); }	}
			set	{	_set(FID_KbnKaihi, value);	}
		}
		
		/// <summary>
		/// フィールド[会費印刷用名称]。
		/// </summary>
		public const string FKaihi_Name = "Kaihi_Name";
		/// <summary>
		/// 会費印刷用名称
		/// </summary>
		public string Kaihi_Name
		{
			get	{	return Cast.String(row == null ? null : row[FKaihi_Name]);	}
			set	{	_set(FKaihi_Name, value);	}
		}
		
		/// <summary>
		/// 会費印刷用名称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaihi_Name_Null
		{
			get	{	if (row == null || row[FKaihi_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaihi_Name]); }	}
			set	{	_set(FKaihi_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[会費略称]。
		/// </summary>
		public const string FKaihi_ShortName = "Kaihi_ShortName";
		/// <summary>
		/// 会費略称
		/// </summary>
		public string Kaihi_ShortName
		{
			get	{	return Cast.String(row == null ? null : row[FKaihi_ShortName]);	}
			set	{	_set(FKaihi_ShortName, value);	}
		}
		
		/// <summary>
		/// 会費略称。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaihi_ShortName_Null
		{
			get	{	if (row == null || row[FKaihi_ShortName] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaihi_ShortName]); }	}
			set	{	_set(FKaihi_ShortName, value);	}
		}
		
		/// <summary>
		/// フィールド[備考]。
		/// </summary>
		public const string FKaihi_Bikou = "Kaihi_Bikou";
		/// <summary>
		/// 備考
		/// </summary>
		public string Kaihi_Bikou
		{
			get	{	return Cast.String(row == null ? null : row[FKaihi_Bikou]);	}
			set	{	_set(FKaihi_Bikou, value);	}
		}
		
		/// <summary>
		/// 備考。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaihi_Bikou_Null
		{
			get	{	if (row == null || row[FKaihi_Bikou] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaihi_Bikou]); }	}
			set	{	_set(FKaihi_Bikou, value);	}
		}
		
		/// <summary>
		/// フィールド[会費群]。
		/// </summary>
		public const string FKaihi_GunCode = "Kaihi_GunCode";
		/// <summary>
		/// 会費群
		/// </summary>
		public int Kaihi_GunCode
		{
			get	{	return Cast.Int(row == null ? null : row[FKaihi_GunCode]);	}
			set	{	_set(FKaihi_GunCode, value);	}
		}
		
		/// <summary>
		/// 会費群。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaihi_GunCode_Null
		{
			get	{	if (row == null || row[FKaihi_GunCode] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaihi_GunCode]); }	}
			set	{	_set(FKaihi_GunCode, value);	}
		}
		
		/// <summary>
		/// フィールド[マスタ検索表示区分]。
		/// </summary>
		public const string FKaihi_SearchUsed = "Kaihi_SearchUsed";
		/// <summary>
		/// マスタ検索表示区分
		/// </summary>
		public bool Kaihi_SearchUsed
		{
			get	{	return Cast.Bool(row == null ? null : row[FKaihi_SearchUsed]);	}
			set	{	_set(FKaihi_SearchUsed, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_1月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost1 = "Kaihi_GetsugakuCost1";
		/// <summary>
		/// 月額費用_1月
		/// </summary>
		public decimal Kaihi_GetsugakuCost1
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost1]);	}
			set	{	_set(FKaihi_GetsugakuCost1, value);	}
		}
		
		/// <summary>
		/// 月額費用_1月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost1_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost1] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost1]); }	}
			set	{	_set(FKaihi_GetsugakuCost1, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_2月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost2 = "Kaihi_GetsugakuCost2";
		/// <summary>
		/// 月額費用_2月
		/// </summary>
		public decimal Kaihi_GetsugakuCost2
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost2]);	}
			set	{	_set(FKaihi_GetsugakuCost2, value);	}
		}
		
		/// <summary>
		/// 月額費用_2月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost2_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost2] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost2]); }	}
			set	{	_set(FKaihi_GetsugakuCost2, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_3月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost3 = "Kaihi_GetsugakuCost3";
		/// <summary>
		/// 月額費用_3月
		/// </summary>
		public decimal Kaihi_GetsugakuCost3
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost3]);	}
			set	{	_set(FKaihi_GetsugakuCost3, value);	}
		}
		
		/// <summary>
		/// 月額費用_3月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost3_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost3] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost3]); }	}
			set	{	_set(FKaihi_GetsugakuCost3, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_4月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost4 = "Kaihi_GetsugakuCost4";
		/// <summary>
		/// 月額費用_4月
		/// </summary>
		public decimal Kaihi_GetsugakuCost4
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost4]);	}
			set	{	_set(FKaihi_GetsugakuCost4, value);	}
		}
		
		/// <summary>
		/// 月額費用_4月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost4_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost4] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost4]); }	}
			set	{	_set(FKaihi_GetsugakuCost4, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_5月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost5 = "Kaihi_GetsugakuCost5";
		/// <summary>
		/// 月額費用_5月
		/// </summary>
		public decimal Kaihi_GetsugakuCost5
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost5]);	}
			set	{	_set(FKaihi_GetsugakuCost5, value);	}
		}
		
		/// <summary>
		/// 月額費用_5月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost5_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost5] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost5]); }	}
			set	{	_set(FKaihi_GetsugakuCost5, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_6月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost6 = "Kaihi_GetsugakuCost6";
		/// <summary>
		/// 月額費用_6月
		/// </summary>
		public decimal Kaihi_GetsugakuCost6
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost6]);	}
			set	{	_set(FKaihi_GetsugakuCost6, value);	}
		}
		
		/// <summary>
		/// 月額費用_6月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost6_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost6] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost6]); }	}
			set	{	_set(FKaihi_GetsugakuCost6, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_7月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost7 = "Kaihi_GetsugakuCost7";
		/// <summary>
		/// 月額費用_7月
		/// </summary>
		public decimal Kaihi_GetsugakuCost7
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost7]);	}
			set	{	_set(FKaihi_GetsugakuCost7, value);	}
		}
		
		/// <summary>
		/// 月額費用_7月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost7_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost7] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost7]); }	}
			set	{	_set(FKaihi_GetsugakuCost7, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_8月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost8 = "Kaihi_GetsugakuCost8";
		/// <summary>
		/// 月額費用_8月
		/// </summary>
		public decimal Kaihi_GetsugakuCost8
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost8]);	}
			set	{	_set(FKaihi_GetsugakuCost8, value);	}
		}
		
		/// <summary>
		/// 月額費用_8月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost8_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost8] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost8]); }	}
			set	{	_set(FKaihi_GetsugakuCost8, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_9月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost9 = "Kaihi_GetsugakuCost9";
		/// <summary>
		/// 月額費用_9月
		/// </summary>
		public decimal Kaihi_GetsugakuCost9
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost9]);	}
			set	{	_set(FKaihi_GetsugakuCost9, value);	}
		}
		
		/// <summary>
		/// 月額費用_9月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost9_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost9] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost9]); }	}
			set	{	_set(FKaihi_GetsugakuCost9, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_10月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost10 = "Kaihi_GetsugakuCost10";
		/// <summary>
		/// 月額費用_10月
		/// </summary>
		public decimal Kaihi_GetsugakuCost10
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost10]);	}
			set	{	_set(FKaihi_GetsugakuCost10, value);	}
		}
		
		/// <summary>
		/// 月額費用_10月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost10_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost10] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost10]); }	}
			set	{	_set(FKaihi_GetsugakuCost10, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_11月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost11 = "Kaihi_GetsugakuCost11";
		/// <summary>
		/// 月額費用_11月
		/// </summary>
		public decimal Kaihi_GetsugakuCost11
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost11]);	}
			set	{	_set(FKaihi_GetsugakuCost11, value);	}
		}
		
		/// <summary>
		/// 月額費用_11月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost11_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost11] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost11]); }	}
			set	{	_set(FKaihi_GetsugakuCost11, value);	}
		}
		
		/// <summary>
		/// フィールド[月額費用_12月]。
		/// </summary>
		public const string FKaihi_GetsugakuCost12 = "Kaihi_GetsugakuCost12";
		/// <summary>
		/// 月額費用_12月
		/// </summary>
		public decimal Kaihi_GetsugakuCost12
		{
			get	{	return Cast.Decimal(row == null ? null : row[FKaihi_GetsugakuCost12]);	}
			set	{	_set(FKaihi_GetsugakuCost12, value);	}
		}
		
		/// <summary>
		/// 月額費用_12月。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public decimal? Kaihi_GetsugakuCost12_Null
		{
			get	{	if (row == null || row[FKaihi_GetsugakuCost12] == System.DBNull.Value) { return null; } else { return Cast.Decimal(row[FKaihi_GetsugakuCost12]); }	}
			set	{	_set(FKaihi_GetsugakuCost12, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行コード]。
		/// </summary>
		public const string FKaihi_BankCode = "Kaihi_BankCode";
		/// <summary>
		/// 銀行コード
		/// </summary>
		public int Kaihi_BankCode
		{
			get	{	return Cast.Int(row == null ? null : row[FKaihi_BankCode]);	}
			set	{	_set(FKaihi_BankCode, value);	}
		}
		
		/// <summary>
		/// 銀行コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaihi_BankCode_Null
		{
			get	{	if (row == null || row[FKaihi_BankCode] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaihi_BankCode]); }	}
			set	{	_set(FKaihi_BankCode, value);	}
		}
		
		/// <summary>
		/// フィールド[支店コード]。
		/// </summary>
		public const string FKaihi_BankCodeShiten = "Kaihi_BankCodeShiten";
		/// <summary>
		/// 支店コード
		/// </summary>
		public int Kaihi_BankCodeShiten
		{
			get	{	return Cast.Int(row == null ? null : row[FKaihi_BankCodeShiten]);	}
			set	{	_set(FKaihi_BankCodeShiten, value);	}
		}
		
		/// <summary>
		/// 支店コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaihi_BankCodeShiten_Null
		{
			get	{	if (row == null || row[FKaihi_BankCodeShiten] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaihi_BankCodeShiten]); }	}
			set	{	_set(FKaihi_BankCodeShiten, value);	}
		}
		
		/// <summary>
		/// フィールド[口座区分 0/None/ 1/Futsu/普通 2/Touza/当座]。
		/// </summary>
		public const string FKaihi_BankKozaType = "Kaihi_BankKozaType";
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座
		/// </summary>
		public eTypeKoza Kaihi_BankKozaType
		{
			get	{	return (eTypeKoza)Cast.Int(row == null ? null : row[FKaihi_BankKozaType]);	}
			set	{	_set(FKaihi_BankKozaType, (int)value);	}
		}
		
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaihi_BankKozaType_Null
		{
			get	{	if (row == null || row[FKaihi_BankKozaType] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaihi_BankKozaType]); }	}
			set	{	_set(FKaihi_BankKozaType, value);	}
		}
		
		/// <summary>
		/// フィールド[口座番号]。
		/// </summary>
		public const string FKaihi_BankKozaNo = "Kaihi_BankKozaNo";
		/// <summary>
		/// 口座番号
		/// </summary>
		public string Kaihi_BankKozaNo
		{
			get	{	return Cast.String(row == null ? null : row[FKaihi_BankKozaNo]);	}
			set	{	_set(FKaihi_BankKozaNo, value);	}
		}
		
		/// <summary>
		/// 口座番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaihi_BankKozaNo_Null
		{
			get	{	if (row == null || row[FKaihi_BankKozaNo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaihi_BankKozaNo]); }	}
			set	{	_set(FKaihi_BankKozaNo, value);	}
		}
		
		/// <summary>
		/// フィールド[口座名義]。
		/// </summary>
		public const string FKaihi_BankKozaName = "Kaihi_BankKozaName";
		/// <summary>
		/// 口座名義
		/// </summary>
		public string Kaihi_BankKozaName
		{
			get	{	return Cast.String(row == null ? null : row[FKaihi_BankKozaName]);	}
			set	{	_set(FKaihi_BankKozaName, value);	}
		}
		
		/// <summary>
		/// 口座名義。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string Kaihi_BankKozaName_Null
		{
			get	{	if (row == null || row[FKaihi_BankKozaName] == System.DBNull.Value) { return null; } else { return Cast.String(row[FKaihi_BankKozaName]); }	}
			set	{	_set(FKaihi_BankKozaName, value);	}
		}
		
		/// <summary>
		/// フィールド[iFax区分 0/None/ 99/etc/その他（iFAX登録なし）]。
		/// </summary>
		public const string FKaihi_BankIfaxType = "Kaihi_BankIfaxType";
		/// <summary>
		/// iFax区分 0/None/ 99/etc/その他（iFAX登録なし）
		/// </summary>
		public eTypeIfax Kaihi_BankIfaxType
		{
			get	{	return (eTypeIfax)Cast.Int(row == null ? null : row[FKaihi_BankIfaxType]);	}
			set	{	_set(FKaihi_BankIfaxType, (int)value);	}
		}
		
		/// <summary>
		/// iFax区分 0/None/ 99/etc/その他（iFAX登録なし）。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? Kaihi_BankIfaxType_Null
		{
			get	{	if (row == null || row[FKaihi_BankIfaxType] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FKaihi_BankIfaxType]); }	}
			set	{	_set(FKaihi_BankIfaxType, value);	}
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
		public t_kaihi(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_kaihi 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_kaihi 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_kaihi");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Kaihi, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_Kaihi, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_KbnKaihi, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_ShortName, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_Bikou, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GunCode, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_SearchUsed, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost1, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost2, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost3, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost4, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost5, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost6, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost7, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost8, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost9, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost10, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost11, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_GetsugakuCost12, typeof(decimal));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_BankCode, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_BankCodeShiten, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_BankKozaType, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_BankKozaNo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_BankKozaName, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FKaihi_BankIfaxType, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
