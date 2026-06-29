using ComponentIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// 藤沢医師会固有処理
	/// </summary>
	public partial class AppDate
	{
		/// <summary>
		/// HH-mmをHH:mmへ置き換えます。
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string ReplaceTimeFormat(object val)
		{
			string str = Cast.String(val);

			string[] strs = Regex.Split(str, "-");

			if (strs.Length == 2)
			{
				str = $"{strs[0].PadLeft(2, '0')}:{strs[1].PadLeft(2, '0')}";
			}

			return str;
		}
	}
}
