
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

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// Tableを定義するクラスです。
	/// </summary>
	public partial class TableProp
	{
		#region *** Property ***
		/// <summary>
		/// enumKbn
		/// </summary>
		public const string enumKbn = "enumKbn";
		
		/// <summary>
		/// t_basic
		/// </summary>
		public const string t_basic = "t_basic";
		
		/// <summary>
		/// t_staff
		/// </summary>
		public const string t_staff = "t_staff";
		
		/// <summary>
		/// t_tantosha
		/// </summary>
		public const string t_tantosha = "t_tantosha";
		#endregion
		
		#region *** Public Method ***
		/// <summary>
		/// 全てのテーブルの列挙辞書を初期化します。
		/// </summary>
		public static void InitAllEnumDictionary()
		{
			App.enumKbn.InitEnumDictionary();

		}
		#endregion
	}
}
