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
		/// <summary>銀行名＋支店名のフィールド</summary>
		public const string Fld_BankName = "Fld_BankName";

		/// <summary>
		/// 文字列扱い（0埋め）となるコードフィールド
		/// </summary>
		public const string Fld_CoedString = "Fld_CoedString";

		/// <summary>
		/// 銀行検索用iサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="type"></param>
		public static void SetComboBox_Bank(UcTableComboBox cmb)
		{
			cmb.BeginUpdate();
			cmb.DBView = new DBView(AppGlobal.Banks.DbView);
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;

			cmb.Sort = DBQuery.GetSql(t_bank.FBNK_Code);
			cmb.DropDownSize = new Size(470, 300);
			cmb.SetColumn(t_bank.FBNK_Code, "銀行ｺｰﾄﾞ", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_bank.FBNK_CodeShiten, "支店ｺｰﾄﾞ", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(Fld_BankName, "銀行名", 290);

			cmb.CompareValue = t_bank.FID_Bank;
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 2;
			cmb.Find = "";
			cmb.EndUpdate();
		}

		/// <summary>
		/// 会員リストiサーチ作成
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_Kaiin(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
//			cmb.RowFilter = dv.RowFilter;
			cmb.Sort = DBQuery.GetSql(t_kaiin.FCD_Kaiin);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_kaiin.FCD_Kaiin, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_kaiin.FKaiin_Name, "氏名", 150);
			//cmb.SetColumn(t_kaiin.FKaiin_NameKana, "フリガナ", 100);
			cmb.SetColumn(t_kaiin.FKaiin_NameKana, "フリガナ", 0);
			cmb.CompareValue = t_kaiin.FID_Kaiin; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 0; // 選択確定時に表示する列
			cmb.Find = "";
			cmb.EndUpdate();
		}

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
			cmb.RowFilter = DBQuery.GetSql($"{t_gakkai.FGKAI_DispName} Is Not Null"); // 使用行
			cmb.Sort = DBQuery.GetSql(t_gakkai.FGKAI_Code);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_gakkai.FGKAI_Code, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_gakkai.FGKAI_DispName, "名称", 220);
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
		//public static void SetComboBox_KaiinKbn(UcTableComboBox cmb, DBView dv)
		//{
		//	cmb.BeginUpdate();
		//	cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
		//	cmb.ComboBox.ImeMode = ImeMode.Hiragana;
		//	//cmb.RowFilter = dv.RowFilter;
		//	cmb.Sort = DBQuery.GetSql(t_kaiinkbn.FCD_KaiinKbn);
		//	cmb.DropDownSize = new Size(440, 300);
		//	cmb.SetColumn(t_kaiinkbn.FCD_KaiinKbn, "コード", 80, ContentAlignment.MiddleRight);
		//	cmb.SetColumn(t_kaiinkbn.FKIK_Name, "名称", 220);
		//	cmb.CompareValue = t_kaiinkbn.FID_KaiinKbn; // Comboから取得する列
		//	cmb.ContentAlignment = ContentAlignment.BottomLeft;
		//	cmb.SelectedIndexNullLeave = -1;
		//	cmb.TextSubItemIndex = 1; // 選択確定時に表示する列
		//	cmb.Find = "";
		//	cmb.EndUpdate();
		//}

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
		/// 医会会員iサーチ作成(会費区分表示あり)
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="dv"></param>
		public static void SetComboBox_Kaihi2(UcTableComboBox cmb, DBView dv)
		{
			cmb.BeginUpdate();
			cmb.DBView = dv; // set時にDBViewがnewされる → 引数のdvとcmb.DBViewは別物となる(DataTableは同じ)
			cmb.ComboBox.ImeMode = ImeMode.Hiragana;
			cmb.RowFilter = dv.RowFilter; // dv側絞込み情報を適用
			cmb.Sort = dv.Sort; // dv側Sort情報を適用
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_kaihi.FKaihi_KaihiType, "会費区分", 80);
			cmb.SetColumn(t_kaihi.FCD_Kaihi, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_kaihi.FKaihi_Name, "名称", 220);
			cmb.CompareValue = t_kaihi.FID_Kaihi; // Comboから取得する列
			cmb.ContentAlignment = ContentAlignment.BottomLeft;
			cmb.SelectedIndexNullLeave = -1;
			cmb.TextSubItemIndex = 2; // 選択確定時に表示する列
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
			cmb.RowFilter = DBQuery.GetSql($"{t_shisetsugyomu.FSGY_DispName} Is Not Null"); // 使用行
			cmb.Sort = DBQuery.GetSql(t_shisetsugyomu.FSGY_Code);
			cmb.DropDownSize = new Size(440, 300);
			cmb.SetColumn(t_shisetsugyomu.FSGY_Code, "コード", 80, ContentAlignment.MiddleRight);
			cmb.SetColumn(t_shisetsugyomu.FSGY_DispName, "名称", 220);
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
