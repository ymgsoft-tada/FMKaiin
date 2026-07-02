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

		///// <summary>
		///// 銀行検索用iサーチの作成
		///// </summary>
		///// <param name="cmb">設定するiサーチ</param>
		///// <param name="dv">ビュー</param>
		//public static void SetComboBox_Bank(UcTableComboBox cmb, DBView dv)
		//{
		//	string F_Name = "F_Name";

		//	DBView dv_tmp = new DBView(dv.DataTable.Copy());
		//	dv_tmp.DataTable.Columns.Add(F_Name, typeof(string));

		//	for(int i = 0; i < dv_tmp.Count; i++)
		//	{
		//		DataRow row = dv_tmp[i].Row;

		//		string name1 = Cast.String(row[t_bank.FBnk_Name]);
		//		string name2 = Cast.String(row[t_bank.FBnk_NameShiten]);

		//		if (name2 != "")
		//		{
		//			name1 += "・" + name2;
		//		}
		//		row[F_Name] = name1;
		//	}

		//	cmb.BeginUpdate();
		//	cmb.DBView = dv_tmp;
		//	cmb.ComboBox.ImeMode = ImeMode.Hiragana;
		//	cmb.RowFilter = "";
		//	cmb.Sort = DBQuery.GetSql(t_bank.FCD_Bank);
		//	cmb.DropDownSize = new Size(480, 250);
		//	cmb.SetColumn(t_bank.FCD_Bank,			"コード",	  80,	ContentAlignment.MiddleRight);
		//	cmb.SetColumn(F_Name,			"銀行・支店名", 400);
		//	cmb.CompareValue = t_bank.FID_Bank;
		//	cmb.ContentAlignment = ContentAlignment.BottomLeft;
		//	cmb.SelectedIndexNullLeave = -1;
		//	cmb.TextSubItemIndex = 1;
		//	cmb.Find = "";
		//	cmb.EndUpdate();
		//}

		///// <summary>
		///// スタッフコード検索用iサーチ作成
		///// </summary>
		///// <param name="cmb"></param>
		///// <param name="all">Trueで退職を含む全スタッフ Falseで在籍のみ</param>
		//public static void SetComboBox_Staff(UcTableComboBox cmb, bool all = false)
		//{
		//	cmb.BeginUpdate();
		//	cmb.DBView = new DBView(AppGlobal.Staffs.DbView);
		//	cmb.ComboBox.ImeMode = ImeMode.Hiragana;

		//	if (all == false)
		//	{
		//		cmb.RowFilter = DBQuery.GetSql($"{t_staff.FSTF_Used} = TRUE");
		//	}
		//	else
		//	{
		//		cmb.RowFilter = "";
		//	}

		//	cmb.Sort = DBQuery.GetSql(t_staff.FCD_Staff);
		//	cmb.DropDownSize = new Size(420, 300);
		//	cmb.SetColumn(Fld_CoedString, "コード", 80, ContentAlignment.MiddleRight);
		//	//cmb.SetColumn(t_staff.FCD_Staff, "コード", 80, ContentAlignment.MiddleRight);
		//	cmb.SetColumn(t_staff.FSTF_Name, "名　称", 150);
		//	cmb.SetColumn(t_staff.FSTF_NameFurigane, "フリガナ", 100);
		//	cmb.SetColumn(Fld_TypeJobName, "職種", 80);
		//	cmb.CompareValue = t_staff.FID_Staff;
		//	cmb.ContentAlignment = ContentAlignment.BottomLeft;
		//	cmb.SelectedIndexNullLeave = -1;
		//	cmb.TextSubItemIndex = 1;
		//	cmb.Find = "";
		//	cmb.EndUpdate();
		//}

		///// <summary>
		///// 商品検索用iサーチ作成
		///// </summary>
		///// <param name="cmb"></param>
		///// <param name="dv"></param>
		//public static void SetComboBox_Shohin(UcTableComboBox cmb, DBView dv)
		//{
		//	cmb.BeginUpdate();
		//	cmb.DBView = dv;
		//	cmb.ComboBox.ImeMode = ImeMode.Hiragana;
		//	//cmb.RowFilter = ;
		//	cmb.Sort = DBQuery.GetSql(t_shohin.FCD_Shohin);
		//	cmb.DropDownSize = new Size(440, 300);
		//	cmb.SetColumn(t_shohin.FCD_Shohin, "商品CD", 80, ContentAlignment.MiddleRight);
		//	cmb.SetColumn(t_shohin.FSho_Name, "商品名", 220);
		//	cmb.SetColumn(t_shohin.FSho_Hinban, "品番", 120);
		//	cmb.CompareValue = t_shohin.FID_Shohin;
		//	cmb.ContentAlignment = ContentAlignment.BottomLeft;
		//	cmb.SelectedIndexNullLeave = -1;
		//	cmb.TextSubItemIndex = 1;
		//	cmb.Find = "";
		//	cmb.EndUpdate();
		//}
	}
}
