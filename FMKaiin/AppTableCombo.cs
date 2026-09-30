using ComponentDB;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// iサーチの設定情報の共通化クラス
	/// </summary>
	public class AppTableCombo
	{
		/// <summary>
		/// 職種区分の名称用フィールド
		/// </summary>
		public const string Fld_TypeJobName = "Fld_TypeJobName";
		/// <summary>
		/// 休診判定用フィールド
		/// </summary>
		public const string Fld_IsKyushin = "Fld_IsKyushin";
		/// <summary>
		/// 事業区分の名称フィールド
		/// </summary>
		public const string Fld_JigyoName = "Fld_JigyoName";
		/// <summary>銀行名＋支店名のフィールド</summary>
		public const string Fld_BankName = "Fld_BankName";

		/// <summary>
		/// 文字列扱い（0埋め）となるコードフィールド
		/// </summary>
		public const string Fld_CoedString = "Fld_CoedString";
		/// <summary>
		/// 提出先名
		/// </summary>
		public const string Fld_Teishutsusaki = "Fld_Teishutsusaki";

		/// <summary>
		/// 医療機関iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_IryoKikan(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
//			cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_iryokikan.FIRK_Code);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_iryokikan.FIRK_Code, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_iryokikan.FIRK_Name, "名称", 220);
			cmb.CompareValue = t_iryokikan.FID_Iryokikan; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 0; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 診療科目iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_Shinryoka(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			//cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_shinryoka.FSRK_Code);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_shinryoka.FSRK_Code, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_shinryoka.FSRK_Name, "名称", 220);
			cmb.CompareValue = t_shinryoka.FID_Shinryoka; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 学会iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_Gakkai(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			//cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_gakkai.FGKAI_Code);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_gakkai.FGKAI_Code, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_gakkai.FGKAI_Name, "名称", 220);
			cmb.CompareValue = t_gakkai.FID_Gakkai; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 会員区分iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_KaiinKbn(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			//cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_kaiinkbn.FCD_KaiinKbn);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_kaiinkbn.FCD_KaiinKbn, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_kaiinkbn.FKIK_Name, "名称", 220);
			cmb.CompareValue = t_kaiinkbn.FID_KaiinKbn; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 医会会員iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_Kaihi(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			cmb.RowFilter = dv.RowFilter; // dv側絞込み情報を適用
			cmb.Sort = dv.Sort; // dv側Sort情報を適用
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_kaihi.FCD_Kaihi, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_kaihi.FKaihi_Name, "名称", 220);
			cmb.CompareValue = t_kaihi.FID_Kaihi; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 医師会iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_Ishikai(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			//cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_ishikai.FCD_Ishikai);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_ishikai.FCD_Ishikai, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_ishikai.FISK_Name, "名称", 220);
			cmb.CompareValue = t_ishikai.FID_Ishikai; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 開設主体iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_Kaisetsushutai(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			//cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_kaisetsushutai.FKST_Code);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_kaisetsushutai.FKST_Code, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_kaisetsushutai.FKST_Name, "名称", 220);
			cmb.CompareValue = t_kaisetsushutai.FID_Kaisetsushutai; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 組コードiサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_KumiCode(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			//cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_kumicd.FCD_KumiCode);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_kumicd.FCD_KumiCode, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_kumicd.FKMC_Name, "名称", 220);
			cmb.CompareValue = t_kumicd.FID_KumiCode; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 施設業務iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_Shisetsugyomu(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			//cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_shisetsugyomu.FSGY_Code);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_shisetsugyomu.FSGY_Code, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_shisetsugyomu.FSGY_Name, "名称", 220);
			cmb.CompareValue = t_shisetsugyomu.FID_ShisetsuGyomu; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// iFAXグループiサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_IfaxGroup(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			//cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_ifax.FCD_iFax);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_ifax.FCD_iFax, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_ifax.FIFX_Name, "名称", 220);
			cmb.CompareValue = t_ifax.FID_iFax; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

	}
}
