
//
// ※このプログラムはDBAutoProperties2Access2000により自動的に生成されました。(fj)
//
// MDB File :
//		D:\client\DotNet4.6_YMGLib5\FujisawaIshikai\FMKaiin\FMKyuyo\bin\Debug\system\Data.mdb
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
	public partial class t_staff : FieldProp
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
		/// フィールド[スタッフCD]。
		/// </summary>
		public const string FCD_Staff = "CD_Staff";
		/// <summary>
		/// スタッフCD
		/// </summary>
		public int CD_Staff
		{
			get	{	return Cast.Int(row == null ? null : row[FCD_Staff]);	}
			set	{	_set(FCD_Staff, value);	}
		}
		
		/// <summary>
		/// スタッフCD。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? CD_Staff_Null
		{
			get	{	if (row == null || row[FCD_Staff] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FCD_Staff]); }	}
			set	{	_set(FCD_Staff, value);	}
		}
		
		/// <summary>
		/// フィールド[スタッフID]。
		/// </summary>
		public const string FID_Staff = "ID_Staff";
		/// <summary>
		/// スタッフID
		/// </summary>
		public int ID_Staff
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Staff]);	}
			set	{	_set(FID_Staff, value);	}
		}
		
		/// <summary>
		/// スタッフID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Staff_Null
		{
			get	{	if (row == null || row[FID_Staff] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Staff]); }	}
			set	{	_set(FID_Staff, value);	}
		}
		
		/// <summary>
		/// フィールド[職務ID]。
		/// </summary>
		public const string FID_Shokumu = "ID_Shokumu";
		/// <summary>
		/// 職務ID
		/// </summary>
		public int ID_Shokumu
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Shokumu]);	}
			set	{	_set(FID_Shokumu, value);	}
		}
		
		/// <summary>
		/// 職務ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Shokumu_Null
		{
			get	{	if (row == null || row[FID_Shokumu] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Shokumu]); }	}
			set	{	_set(FID_Shokumu, value);	}
		}
		
		/// <summary>
		/// フィールド[スタッフ名]。
		/// </summary>
		public const string FSTF_Name = "STF_Name";
		/// <summary>
		/// スタッフ名
		/// </summary>
		public string STF_Name
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_Name]);	}
			set	{	_set(FSTF_Name, value);	}
		}
		
		/// <summary>
		/// スタッフ名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_Name_Null
		{
			get	{	if (row == null || row[FSTF_Name] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_Name]); }	}
			set	{	_set(FSTF_Name, value);	}
		}
		
		/// <summary>
		/// フィールド[スタッフ名フリガナ]。
		/// </summary>
		public const string FSTF_NameFurigane = "STF_NameFurigane";
		/// <summary>
		/// スタッフ名フリガナ
		/// </summary>
		public string STF_NameFurigane
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_NameFurigane]);	}
			set	{	_set(FSTF_NameFurigane, value);	}
		}
		
		/// <summary>
		/// スタッフ名フリガナ。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_NameFurigane_Null
		{
			get	{	if (row == null || row[FSTF_NameFurigane] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_NameFurigane]); }	}
			set	{	_set(FSTF_NameFurigane, value);	}
		}
		
		/// <summary>
		/// フィールド[性別 0/None/ 1/Men/男性 2/Women/女性]。
		/// </summary>
		public const string FSTF_Sex = "STF_Sex";
		/// <summary>
		/// 性別 0/None/ 1/Men/男性 2/Women/女性
		/// </summary>
		public eSex STF_Sex
		{
			get	{	return (eSex)Cast.Int(row == null ? null : row[FSTF_Sex]);	}
			set	{	_set(FSTF_Sex, (int)value);	}
		}
		
		/// <summary>
		/// 性別 0/None/ 1/Men/男性 2/Women/女性。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? STF_Sex_Null
		{
			get	{	if (row == null || row[FSTF_Sex] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FSTF_Sex]); }	}
			set	{	_set(FSTF_Sex, value);	}
		}
		
		/// <summary>
		/// フィールド[職種区分 0/None/ 1/Dr/医師 2/Nurse/看護師 3/Jimu/事務 4/Yakuzaishi/薬剤師]。
		/// </summary>
		public const string FSTF_TypeJob = "STF_TypeJob";
		/// <summary>
		/// 職種区分 0/None/ 1/Dr/医師 2/Nurse/看護師 3/Jimu/事務 4/Yakuzaishi/薬剤師
		/// </summary>
		public eTypeJob STF_TypeJob
		{
			get	{	return (eTypeJob)Cast.Int(row == null ? null : row[FSTF_TypeJob]);	}
			set	{	_set(FSTF_TypeJob, (int)value);	}
		}
		
		/// <summary>
		/// 職種区分 0/None/ 1/Dr/医師 2/Nurse/看護師 3/Jimu/事務 4/Yakuzaishi/薬剤師。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? STF_TypeJob_Null
		{
			get	{	if (row == null || row[FSTF_TypeJob] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FSTF_TypeJob]); }	}
			set	{	_set(FSTF_TypeJob, value);	}
		}
		
		/// <summary>
		/// フィールド[診療所区分 0/None/ 1/North/北 2/South/南]。
		/// </summary>
		public const string FSTF_TypeShinryojo = "STF_TypeShinryojo";
		/// <summary>
		/// 診療所区分 0/None/ 1/North/北 2/South/南
		/// </summary>
		public eTypeShinryojo STF_TypeShinryojo
		{
			get	{	return (eTypeShinryojo)Cast.Int(row == null ? null : row[FSTF_TypeShinryojo]);	}
			set	{	_set(FSTF_TypeShinryojo, (int)value);	}
		}
		
		/// <summary>
		/// 診療所区分 0/None/ 1/North/北 2/South/南。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? STF_TypeShinryojo_Null
		{
			get	{	if (row == null || row[FSTF_TypeShinryojo] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FSTF_TypeShinryojo]); }	}
			set	{	_set(FSTF_TypeShinryojo, value);	}
		}
		
		/// <summary>
		/// フィールド[郵便番号]。
		/// </summary>
		public const string FSTF_Post = "STF_Post";
		/// <summary>
		/// 郵便番号
		/// </summary>
		public string STF_Post
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_Post]);	}
			set	{	_set(FSTF_Post, value);	}
		}
		
		/// <summary>
		/// 郵便番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_Post_Null
		{
			get	{	if (row == null || row[FSTF_Post] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_Post]); }	}
			set	{	_set(FSTF_Post, value);	}
		}
		
		/// <summary>
		/// フィールド[住所１]。
		/// </summary>
		public const string FSTF_Addr1 = "STF_Addr1";
		/// <summary>
		/// 住所１
		/// </summary>
		public string STF_Addr1
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_Addr1]);	}
			set	{	_set(FSTF_Addr1, value);	}
		}
		
		/// <summary>
		/// 住所１。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_Addr1_Null
		{
			get	{	if (row == null || row[FSTF_Addr1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_Addr1]); }	}
			set	{	_set(FSTF_Addr1, value);	}
		}
		
		/// <summary>
		/// フィールド[住所2]。
		/// </summary>
		public const string FSTF_Addr2 = "STF_Addr2";
		/// <summary>
		/// 住所2
		/// </summary>
		public string STF_Addr2
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_Addr2]);	}
			set	{	_set(FSTF_Addr2, value);	}
		}
		
		/// <summary>
		/// 住所2。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_Addr2_Null
		{
			get	{	if (row == null || row[FSTF_Addr2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_Addr2]); }	}
			set	{	_set(FSTF_Addr2, value);	}
		}
		
		/// <summary>
		/// フィールド[電話番号]。
		/// </summary>
		public const string FSTF_Tel1 = "STF_Tel1";
		/// <summary>
		/// 電話番号
		/// </summary>
		public string STF_Tel1
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_Tel1]);	}
			set	{	_set(FSTF_Tel1, value);	}
		}
		
		/// <summary>
		/// 電話番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_Tel1_Null
		{
			get	{	if (row == null || row[FSTF_Tel1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_Tel1]); }	}
			set	{	_set(FSTF_Tel1, value);	}
		}
		
		/// <summary>
		/// フィールド[電話番号２（携帯等）]。
		/// </summary>
		public const string FSTF_Tel2 = "STF_Tel2";
		/// <summary>
		/// 電話番号２（携帯等）
		/// </summary>
		public string STF_Tel2
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_Tel2]);	}
			set	{	_set(FSTF_Tel2, value);	}
		}
		
		/// <summary>
		/// 電話番号２（携帯等）。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_Tel2_Null
		{
			get	{	if (row == null || row[FSTF_Tel2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_Tel2]); }	}
			set	{	_set(FSTF_Tel2, value);	}
		}
		
		/// <summary>
		/// フィールド[生年月日]。
		/// </summary>
		public const string FSTF_DateBirthday = "STF_DateBirthday";
		/// <summary>
		/// 生年月日
		/// </summary>
		public DateTime STF_DateBirthday
		{
			get	{	return Cast.DateTime(row == null ? null : row[FSTF_DateBirthday]);	}
			set	{	_set(FSTF_DateBirthday, value);	}
		}
		
		/// <summary>
		/// 生年月日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? STF_DateBirthday_Null
		{
			get	{	if (row == null || row[FSTF_DateBirthday] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FSTF_DateBirthday]); }	}
			set	{	_set(FSTF_DateBirthday, value);	}
		}
		
		/// <summary>
		/// フィールド[入社日]。
		/// </summary>
		public const string FSTF_DateNyusha = "STF_DateNyusha";
		/// <summary>
		/// 入社日
		/// </summary>
		public DateTime STF_DateNyusha
		{
			get	{	return Cast.DateTime(row == null ? null : row[FSTF_DateNyusha]);	}
			set	{	_set(FSTF_DateNyusha, value);	}
		}
		
		/// <summary>
		/// 入社日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? STF_DateNyusha_Null
		{
			get	{	if (row == null || row[FSTF_DateNyusha] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FSTF_DateNyusha]); }	}
			set	{	_set(FSTF_DateNyusha, value);	}
		}
		
		/// <summary>
		/// フィールド[退職日]。
		/// </summary>
		public const string FSTF_DateTaishoku = "STF_DateTaishoku";
		/// <summary>
		/// 退職日
		/// </summary>
		public DateTime STF_DateTaishoku
		{
			get	{	return Cast.DateTime(row == null ? null : row[FSTF_DateTaishoku]);	}
			set	{	_set(FSTF_DateTaishoku, value);	}
		}
		
		/// <summary>
		/// 退職日。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public DateTime? STF_DateTaishoku_Null
		{
			get	{	if (row == null || row[FSTF_DateTaishoku] == System.DBNull.Value) { return null; } else { return Cast.DateTime(row[FSTF_DateTaishoku]); }	}
			set	{	_set(FSTF_DateTaishoku, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行コード]。
		/// </summary>
		public const string FSTF_BankCode = "STF_BankCode";
		/// <summary>
		/// 銀行コード
		/// </summary>
		public int STF_BankCode
		{
			get	{	return Cast.Int(row == null ? null : row[FSTF_BankCode]);	}
			set	{	_set(FSTF_BankCode, value);	}
		}
		
		/// <summary>
		/// 銀行コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? STF_BankCode_Null
		{
			get	{	if (row == null || row[FSTF_BankCode] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FSTF_BankCode]); }	}
			set	{	_set(FSTF_BankCode, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行支店コード]。
		/// </summary>
		public const string FSTF_BankShitenCode = "STF_BankShitenCode";
		/// <summary>
		/// 銀行支店コード
		/// </summary>
		public int STF_BankShitenCode
		{
			get	{	return Cast.Int(row == null ? null : row[FSTF_BankShitenCode]);	}
			set	{	_set(FSTF_BankShitenCode, value);	}
		}
		
		/// <summary>
		/// 銀行支店コード。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? STF_BankShitenCode_Null
		{
			get	{	if (row == null || row[FSTF_BankShitenCode] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FSTF_BankShitenCode]); }	}
			set	{	_set(FSTF_BankShitenCode, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行口座番号]。
		/// </summary>
		public const string FSTF_BankKozaNo = "STF_BankKozaNo";
		/// <summary>
		/// 銀行口座番号
		/// </summary>
		public string STF_BankKozaNo
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_BankKozaNo]);	}
			set	{	_set(FSTF_BankKozaNo, value);	}
		}
		
		/// <summary>
		/// 銀行口座番号。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_BankKozaNo_Null
		{
			get	{	if (row == null || row[FSTF_BankKozaNo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_BankKozaNo]); }	}
			set	{	_set(FSTF_BankKozaNo, value);	}
		}
		
		/// <summary>
		/// フィールド[銀行口座名]。
		/// </summary>
		public const string FSTF_BankKozaName = "STF_BankKozaName";
		/// <summary>
		/// 銀行口座名
		/// </summary>
		public string STF_BankKozaName
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_BankKozaName]);	}
			set	{	_set(FSTF_BankKozaName, value);	}
		}
		
		/// <summary>
		/// 銀行口座名。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_BankKozaName_Null
		{
			get	{	if (row == null || row[FSTF_BankKozaName] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_BankKozaName]); }	}
			set	{	_set(FSTF_BankKozaName, value);	}
		}
		
		/// <summary>
		/// フィールド[口座区分 0/None/ 1/Futsu/普通 2/Touza/当座]。
		/// </summary>
		public const string FSTF_KozaType = "STF_KozaType";
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座
		/// </summary>
		public eTypeKoza STF_KozaType
		{
			get	{	return (eTypeKoza)Cast.Int(row == null ? null : row[FSTF_KozaType]);	}
			set	{	_set(FSTF_KozaType, (int)value);	}
		}
		
		/// <summary>
		/// 口座区分 0/None/ 1/Futsu/普通 2/Touza/当座。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? STF_KozaType_Null
		{
			get	{	if (row == null || row[FSTF_KozaType] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FSTF_KozaType]); }	}
			set	{	_set(FSTF_KozaType, value);	}
		}
		
		/// <summary>
		/// フィールド[使用有無]。
		/// </summary>
		public const string FSTF_Used = "STF_Used";
		/// <summary>
		/// 使用有無
		/// </summary>
		public bool STF_Used
		{
			get	{	return Cast.Bool(row == null ? null : row[FSTF_Used]);	}
			set	{	_set(FSTF_Used, value);	}
		}
		
		/// <summary>
		/// フィールド[メール１]。
		/// </summary>
		public const string FSTF_Mail1 = "STF_Mail1";
		/// <summary>
		/// メール１
		/// </summary>
		public string STF_Mail1
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_Mail1]);	}
			set	{	_set(FSTF_Mail1, value);	}
		}
		
		/// <summary>
		/// メール１。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_Mail1_Null
		{
			get	{	if (row == null || row[FSTF_Mail1] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_Mail1]); }	}
			set	{	_set(FSTF_Mail1, value);	}
		}
		
		/// <summary>
		/// フィールド[メール２]。
		/// </summary>
		public const string FSTF_Mail2 = "STF_Mail2";
		/// <summary>
		/// メール２
		/// </summary>
		public string STF_Mail2
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_Mail2]);	}
			set	{	_set(FSTF_Mail2, value);	}
		}
		
		/// <summary>
		/// メール２。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_Mail2_Null
		{
			get	{	if (row == null || row[FSTF_Mail2] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_Mail2]); }	}
			set	{	_set(FSTF_Mail2, value);	}
		}
		
		/// <summary>
		/// フィールド[マイナンバー]。
		/// </summary>
		public const string FSTF_MyNo = "STF_MyNo";
		/// <summary>
		/// マイナンバー
		/// </summary>
		public string STF_MyNo
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_MyNo]);	}
			set	{	_set(FSTF_MyNo, value);	}
		}
		
		/// <summary>
		/// マイナンバー。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_MyNo_Null
		{
			get	{	if (row == null || row[FSTF_MyNo] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_MyNo]); }	}
			set	{	_set(FSTF_MyNo, value);	}
		}
		
		/// <summary>
		/// フィールド[旧姓]。
		/// </summary>
		public const string FSTF_NameOld = "STF_NameOld";
		/// <summary>
		/// 旧姓
		/// </summary>
		public string STF_NameOld
		{
			get	{	return Cast.String(row == null ? null : row[FSTF_NameOld]);	}
			set	{	_set(FSTF_NameOld, value);	}
		}
		
		/// <summary>
		/// 旧姓。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public string STF_NameOld_Null
		{
			get	{	if (row == null || row[FSTF_NameOld] == System.DBNull.Value) { return null; } else { return Cast.String(row[FSTF_NameOld]); }	}
			set	{	_set(FSTF_NameOld, value);	}
		}
		
		/// <summary>
		/// フィールド[提出先ID]。
		/// </summary>
		public const string FID_Teishutsusaki = "ID_Teishutsusaki";
		/// <summary>
		/// 提出先ID
		/// </summary>
		public int ID_Teishutsusaki
		{
			get	{	return Cast.Int(row == null ? null : row[FID_Teishutsusaki]);	}
			set	{	_set(FID_Teishutsusaki, value);	}
		}
		
		/// <summary>
		/// 提出先ID。System.DBNull.Value の場合 null を示します。
		/// </summary>
		public int? ID_Teishutsusaki_Null
		{
			get	{	if (row == null || row[FID_Teishutsusaki] == System.DBNull.Value) { return null; } else { return Cast.Int(row[FID_Teishutsusaki]); }	}
			set	{	_set(FID_Teishutsusaki, value);	}
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
		public t_staff(object o) : base(o) {}
		#endregion
		/// <summary>
		/// t_staff 型の空テーブルを作成し、返します。
		/// </summary>
		/// <returns>t_staff 型の空テーブル</returns>
		public static DataTable GetTable()
		{
			DataTable	dt = new DataTable("t_staff");
			
			DataColumn	col;
			
			col = new DataColumn(FID_Auto, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FCD_Staff, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Staff, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Shokumu, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Name, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_NameFurigane, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Sex, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_TypeJob, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_TypeShinryojo, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Post, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Addr1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Addr2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Tel1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Tel2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_DateBirthday, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_DateNyusha, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_DateTaishoku, typeof(DateTime));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_BankCode, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_BankShitenCode, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_BankKozaNo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_BankKozaName, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_KozaType, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Used, typeof(bool));
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Mail1, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_Mail2, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_MyNo, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FSTF_NameOld, typeof(string));
			col.AllowDBNull = true;
			col.MaxLength = 255;
			dt.Columns.Add(col);
			
			col = new DataColumn(FID_Teishutsusaki, typeof(int));
			dt.Columns.Add(col);
			
			col = new DataColumn(FLastUpdate, typeof(DateTime));
			dt.Columns.Add(col);
			
			return dt;
		}
	}
}
