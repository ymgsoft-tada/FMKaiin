using GControlGcComboBoxEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// コンボボックス作成用クラス
	/// </summary>
	public class AppCombo
	{
		/// <summary>全ての選択値</summary>
		public const int SelectAllValue = -1;

		/// <summary>
		/// 指定したenum辞書からコンボボックスを生成します。
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dics"></param>
		/// <param name="select_all">trueで「すべて」を追加します。</param>
		/// <param name="ignores"></param>
		public static void SetComboBox(GcComboBoxEx cmb, Dictionary<int, string> dics, params int[] ignores)
		{
			SetComboBox(cmb, dics, false, ignores);
		}
		/// <summary>
		/// 指定したenum辞書からコンボボックスを生成します。
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dics"></param>
		/// <param name="select_all">trueで「すべて」を追加します。</param>
		/// <param name="ignores"></param>
		public static void SetComboBox(GcComboBoxEx cmb, Dictionary<int, string> dics, bool select_all, params int[] ignores)
		{
			cmb.ExBeginUpdate();
			cmb.ExClearItem();

			if (select_all == true)
			{
				cmb.ExAddItem("すべて", SelectAllValue);
			}

			foreach(KeyValuePair<int, string> kvp in dics)
			{
				if (ignores == null || ignores.Contains(kvp.Key) == false)
				{
					cmb.ExAddItem(kvp.Value, kvp.Key);
				}
			}
			cmb.ExEndUpdate();
		}
	}
}
