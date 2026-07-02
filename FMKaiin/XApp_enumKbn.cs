
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
	/// [列挙] 診療所区分
	/// </summary>
	public enum eTypeShinryojo
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 北
		/// </summary>
		North = 1,
		/// <summary>
		/// 南
		/// </summary>
		South = 2,
	}
	
	/// <summary>
	/// [列挙] 職種区分
	/// </summary>
	public enum eTypeJob
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 医師
		/// </summary>
		Dr = 1,
		/// <summary>
		/// 看護師
		/// </summary>
		Nurse = 2,
		/// <summary>
		/// 事務
		/// </summary>
		Jimu = 3,
		/// <summary>
		/// 薬剤師
		/// </summary>
		Yakuzaishi = 4,
	}
	
	/// <summary>
	/// [列挙] 性別
	/// </summary>
	public enum eSex
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 男性
		/// </summary>
		Men = 1,
		/// <summary>
		/// 女性
		/// </summary>
		Women = 2,
	}
	
	/// <summary>
	/// [列挙] 端数処理
	/// </summary>
	public enum eHasu
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 切捨
		/// </summary>
		Kirisute = 1,
		/// <summary>
		/// 切上
		/// </summary>
		Kiriage = 2,
		/// <summary>
		/// 四捨五入
		/// </summary>
		Shishagonyu = 3,
	}
	
	/// <summary>
	/// [列挙] 口座区分
	/// </summary>
	public enum eTypeKoza
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// 普通
		/// </summary>
		Futsu = 1,
		/// <summary>
		/// 当座
		/// </summary>
		Touza = 2,
	}
	
	/// <summary>
	/// [列挙] 権限区分
	/// </summary>
	public enum eAuth
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// SuperUser
		/// </summary>
		SU = 1,
		/// <summary>
		/// 管理者
		/// </summary>
		Admin = 2,
		/// <summary>
		/// 一般
		/// </summary>
		Ippan = 3,
	}
	
	/// <summary>
	/// [列挙] 作成区分
	/// </summary>
	public enum eCreateType
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// インポート
		/// </summary>
		Import = 1,
	}
	
	/// <summary>
	/// [作成者 fj]
	/// テーブル編集の際に使うクラスです。
	/// </summary>
	public static class enumKbn
	{
		/// <summary>
		/// eTypeShinryojo に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeShinryojo;
		/// <summary>
		/// eTypeJob に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeJob;
		/// <summary>
		/// eSex に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DSex;
		/// <summary>
		/// eHasu に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DHasu;
		/// <summary>
		/// eTypeKoza に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DTypeKoza;
		/// <summary>
		/// eAuth に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DAuth;
		/// <summary>
		/// eCreateType に対応した辞書です。
		/// </summary>
		public static Dictionary<int, string> DCreateType;
		
		/// <summary>
		/// 列挙辞書を初期化します。
		/// </summary>
		public static void InitEnumDictionary()
		{
			DTypeShinryojo = new Dictionary<int, string>();
			DTypeShinryojo.Add((int)eTypeShinryojo.None, "");
			DTypeShinryojo.Add((int)eTypeShinryojo.North, "北");
			DTypeShinryojo.Add((int)eTypeShinryojo.South, "南");
			
			DTypeJob = new Dictionary<int, string>();
			DTypeJob.Add((int)eTypeJob.None, "");
			DTypeJob.Add((int)eTypeJob.Dr, "医師");
			DTypeJob.Add((int)eTypeJob.Nurse, "看護師");
			DTypeJob.Add((int)eTypeJob.Jimu, "事務");
			DTypeJob.Add((int)eTypeJob.Yakuzaishi, "薬剤師");
			
			DSex = new Dictionary<int, string>();
			DSex.Add((int)eSex.None, "");
			DSex.Add((int)eSex.Men, "男性");
			DSex.Add((int)eSex.Women, "女性");
			
			DHasu = new Dictionary<int, string>();
			DHasu.Add((int)eHasu.None, "");
			DHasu.Add((int)eHasu.Kirisute, "切捨");
			DHasu.Add((int)eHasu.Kiriage, "切上");
			DHasu.Add((int)eHasu.Shishagonyu, "四捨五入");
			
			DTypeKoza = new Dictionary<int, string>();
			DTypeKoza.Add((int)eTypeKoza.None, "");
			DTypeKoza.Add((int)eTypeKoza.Futsu, "普通");
			DTypeKoza.Add((int)eTypeKoza.Touza, "当座");
			
			DAuth = new Dictionary<int, string>();
			DAuth.Add((int)eAuth.None, "");
			DAuth.Add((int)eAuth.SU, "SuperUser");
			DAuth.Add((int)eAuth.Admin, "管理者");
			DAuth.Add((int)eAuth.Ippan, "一般");
			
			DCreateType = new Dictionary<int, string>();
			DCreateType.Add((int)eCreateType.None, "");
			DCreateType.Add((int)eCreateType.Import, "インポート");
		}
	}
}
