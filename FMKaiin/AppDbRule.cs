using ComponentGControlDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// 入力ルール
	/// </summary>
	public class AppDbRule
	{
		/// <summary>
		/// ルールクラス
		/// </summary>
		public static GControlDBRule Rule { get; private set; }

		/// <summary>合計用ルール名</summary>
		public const string RuleCurrencyTotal = "Currency_Total";

		/// <summary>金額用ルール名</summary>
		public const string RuleCurrency = "Currency";

		/// <summary>金額用ルール名</summary>
		public const string RuleCurrency9 = "Currency9";

		/// <summary>金額用ルール名</summary>
		public const string RuleCurrency9_1 = "Currency9_1";

		/// <summary>少数点第1用</summary>
		public const string RuleRatio1 = "Ratio1";
		/// <summary>少数点第2用</summary>
		public const string RuleRatio2 = "Ratio2";
		/// <summary>少数点第3用</summary>
		public const string RuleRatio3 = "Ratio3";

		public const int Code6 = 6;

		/// <summary>
		/// 初期化処理
		/// </summary>
		public static void Initialize()
		{
			Rule = new GControlDBRule();

			GControlDBRuleNumber code = new GControlDBRuleNumber("Code", Code6, 0);
			code.Align[0] = System.Drawing.ContentAlignment.BottomLeft;
			Rule.Add(code);

			GControlDBRuleNumber code4 = new GControlDBRuleNumber("Code4", 4, 0);
			code4.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code4);

			GControlDBRuleNumber code3 = new GControlDBRuleNumber("Code3", 3, 0);
			code3.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code3);

			GControlDBRuleNumber code7 = new GControlDBRuleNumber("Code7", 7, 0);
			code7.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code7);

			GControlDBRuleNumber code10 = new GControlDBRuleNumber("Code10", 10, 0);
			code10.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code10);

			GControlDBRuleNumber code12 = new GControlDBRuleNumber("Code12", 12, 0);
			code12.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code12);

			// 合計値フィールド
			GControlDBRuleCurrency total = new GControlDBRuleCurrency(RuleCurrencyTotal, 11, 0);
			total.Align[0] = ContentAlignment.TopRight;
			Rule.Add(total);

			GControlDBRuleCurrency curr9 = new GControlDBRuleCurrency(RuleCurrency9, 9, 0);
			curr9.Align[0] = ContentAlignment.BottomRight;
			curr9.AlternateBlank[0] = true;
			Rule.Add(curr9);

			GControlDBRuleCurrency curr9_1 = new GControlDBRuleCurrency(RuleCurrency9_1, 9, 1);
			curr9_1.Align[0] = ContentAlignment.BottomRight;
			curr9_1.AlternateBlank[0] = true;
			Rule.Add(curr9_1);

			GControlDBRuleCurrency curr = new GControlDBRuleCurrency(RuleCurrency, 7, 0);
			curr.Align[0] = ContentAlignment.BottomRight;
			curr.AlternateBlank[0] = true;
			Rule.Add(curr);

			GControlDBRuleNumber r1 = new GControlDBRuleNumber(RuleRatio1, 1,1);
			Rule.Add(r1);

			GControlDBRuleNumber r2 = new GControlDBRuleNumber(RuleRatio2, 3,2);
			Rule.Add(r2);

			GControlDBRuleNumber r3 = new GControlDBRuleNumber(RuleRatio3, 3,3);
			Rule.Add(r3);

			// 郵便番号(住所上段を30桁）
			GControlDBRulePostAddr post = new GControlDBRulePostAddr();
			post.MaxLength[2] = 30;

			// 半角カナのフィールド
			GControlDBRuleText kana = new GControlDBRuleText("Furigana", 40);
			kana.Format[0] = "H";
			kana.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
			kana.ImeMode[0]	= ImeMode.KatakanaHalf;
			Rule.Add(kana);

			GControlDBRuleText kana20 = new GControlDBRuleText("Furigana20", 20);
			kana20.Format[0] = "H";
			kana20.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
			kana20.ImeMode[0]	= ImeMode.KatakanaHalf;
			Rule.Add(kana20);

			GControlDBRuleText kana60 = new GControlDBRuleText("Furigana60", 60);
			kana60.Format[0] = "H";
			kana60.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
			kana60.ImeMode[0]	= ImeMode.KatakanaHalf;
			Rule.Add(kana60);

			GControlDBRuleText kana30 = new GControlDBRuleText("Furigana30", 30);
			kana30.Format[0] = "H";
			kana30.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
			kana30.ImeMode[0]	= ImeMode.KatakanaHalf;
			Rule.Add(kana30);


			GControlDBRuleText eisu = new GControlDBRuleText("Eisu", 40);
			eisu.ImeMode[0] = ImeMode.Off;
			Rule.Add(eisu);

			GControlDBRuleText eisu20 = new GControlDBRuleText("Eisu20", 20);
			eisu20.ImeMode[0] = ImeMode.Off;
			Rule.Add(eisu20);

			GControlDBRuleText hankaku = new GControlDBRuleText(80);
			hankaku.ImeMode[0]	= ImeMode.Disable;

			GControlDBRuleText numStr = new GControlDBRuleText(15);
			numStr.ImeMode[0]	= ImeMode.Disable;

			GControlDBRuleText numStr13 = new GControlDBRuleText(13);
			numStr13.ImeMode[0]	= ImeMode.Disable;

			GControlDBRuleText numStr12 = new GControlDBRuleText(12);
			numStr12.ImeMode[0]	= ImeMode.Disable;

			GControlDBRuleText memo = new GControlDBRuleText("Memo", 100);
			memo.Align[0] = ContentAlignment.TopLeft;

			GControlDBRuleText memo20 = new GControlDBRuleText("Memo", 20);
			memo20.Align[0] = ContentAlignment.TopLeft;

			GControlDBRuleText memo10 = new GControlDBRuleText("Memo", 10);
			memo10.Align[0] = ContentAlignment.TopLeft;
			GControlDBRuleText tekiyo = new GControlDBRuleText("Tekiyo", 20);
			tekiyo.Align[0] = ContentAlignment.TopLeft;
			tekiyo.HighlightText[0] = false;


			// 検索用などの日付入力フィールド
			Rule.Add(new GControlDBRuleDate("Date"));

			#region +++ t_basic +++
			Rule.Add(new GControlDBRuleRuby(t_basic.FBAS_Name, new int[] { 40, 80 }));
			Rule.Add(new GControlDBRuleText(t_basic.FBAS_NameDaihyo, 30));
			//Rule.Add(new GControlDBRuleText(t_basic.FBAS_Name2, 30));
			//Rule.Add(new GControlDBRuleText(t_basic.FBAS_NameDaihyo2, 30));
			Rule.Add(post, t_basic.FBAS_Post);
			Rule.Add(new GControlDBRuleText(t_basic.FBAS_Addr2, 40));
			Rule.Add(new GControlDBRuleHyphenSplit(t_basic.FBAS_Tel1, new int[] {5,4,5}));
			Rule.Add(new GControlDBRuleHyphenSplit(t_basic.FBAS_Tel2, new int[] { 5, 4, 5 }));
			Rule.Add(new GControlDBRuleHyphenSplit(t_basic.FBAS_Fax, new int[] {5,4,5}));

			Rule.Add(new GControlDBRuleDate(t_basic.FBAS_DateYM));
			Rule.Add(new GControlDBRuleDate(t_basic.FBAS_DateYMLatest));

			Rule.Add(numStr13, t_basic.FBAS_HojinNo);
			Rule.Add(eisu20, t_basic.FBAS_MyNoPWD);
			#endregion

			#region +++ t_tantosha +++
			Rule.Add(code, t_tantosha.FCD_Tanto);
			Rule.Add(new GControlDBRuleText(t_tantosha.FTNT_Name, 20));
			Rule.Add(eisu20, t_tantosha.FTNT_Password);
			#endregion

			#region +++ t_kaiin +++
			Rule.Add(code, t_kaiin.FCD_Kaiin);
			Rule.Add(new GControlDBRuleRuby(t_kaiin.FKaiin_Name, new int[] { 15, 30 }));
//			Rule.Add(new GControlDBRuleText(t_kaiin.FKaiin_NameOld, 20));
			Rule.Add(post, t_kaiin.FKaiin_Post);
			Rule.Add(new GControlDBRuleText(t_kaiin.FKaiin_Addr2, 40));
			Rule.Add(new GControlDBRuleHyphenSplit(t_kaiin.FKaiin_Tel1, new int[] { 5, 4, 5 }));
			Rule.Add(new GControlDBRuleHyphenSplit(t_kaiin.FKaiin_Tel2, new int[] { 5, 4, 5 }));
			Rule.Add(new GControlDBRuleHyphenSplit(t_kaiin.FKaiin_Fax, new int[] { 5, 4, 5 }));
			Rule.Add(new GControlDBRuleNumber(t_kaiin.FKaiin_IsekiTorokuNo, 7, 0));
			Rule.Add(code4, t_kaiin.FKaiin_BankCode1);
			Rule.Add(code3, t_kaiin.FKaiin_BankShitenCode1);
			Rule.Add(code7, t_kaiin.FKaiin_BankKozaNo1);
			Rule.Add(kana30, t_kaiin.FKaiin_BankKozaName1);
			Rule.Add(code4, t_kaiin.FKaiin_BankCode2);
			Rule.Add(code3, t_kaiin.FKaiin_BankShitenCode2);
			Rule.Add(code7, t_kaiin.FKaiin_BankKozaNo2);
			Rule.Add(kana30, t_kaiin.FKaiin_BankKozaName2);
			Rule.Add(code4, t_kaiin.FKaiin_BankCode3);
			Rule.Add(code3, t_kaiin.FKaiin_BankShitenCode3);
			Rule.Add(code7, t_kaiin.FKaiin_BankKozaNo3);
			Rule.Add(kana30, t_kaiin.FKaiin_BankKozaName3);
			Rule.Add(hankaku, t_kaiin.FKaiin_Mail1);
//			Rule.Add(hankaku, t_kaiin.FKaiin_Mail2);
//			Rule.Add(code12, t_kaiin.FKaiin_MyNo);
			Rule.Add(new GControlDBRuleText(t_kaiin.FKaiin_ShusshinKou, 40));
			Rule.Add(new GControlDBRuleText(t_kaiin.FKaiin_ShusshinIn, 40));
			Rule.Add(new GControlDBRuleText(t_kaiin.FKaiin_TaikaiEtcmemo, 40));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateBirth));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateIsekiToroku));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateToroku));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateNyuukai));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateShonin));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateSotsugyo));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateShuryo));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateShutokuGakui));
			Rule.Add(new GControlDBRuleDate(t_kaiin.FKaiin_DateTaikai));
			#endregion

			// ※t_bank作成なしなら削除
			#region +++ t_bank +++
			Rule.Add(code4, t_bank.FBNK_Code);
			Rule.Add(code3, t_bank.FBNK_CodeShiten);
			Rule.Add(code7, t_bank.FBNK_KozaNo);
			Rule.Add(new GControlDBRuleText(t_bank.FBNK_Name, 20));
			Rule.Add(kana20, t_bank.FBNK_NameFurigana);
			Rule.Add(new GControlDBRuleText(t_bank.FBNK_NameShiten, 20));
			Rule.Add(kana20, t_bank.FBNK_NameFuriganaShiten);
			Rule.Add(code10, t_bank.FBNK_CodeCompany);
			#endregion

			#region +++ t_bank_code +++
			Rule.Add(code4, t_bank_code.FBCD_Code);
			Rule.Add(code3, t_bank_code.FBCD_CodeShiten);
			Rule.Add(new GControlDBRuleRuby(t_bank_code.FBCD_Name, new int[] { 20, 40 }));
			Rule.Add(new GControlDBRuleRuby(t_bank_code.FBCD_NameShiten, new int[] { 20, 40 }));
			//Rule.Add(new GControlDBRuleText(t_bank_code.FBCD_FullName, 40));
			#endregion

			#region +++ t_kaihi +++
			Rule.Add(new GControlDBRuleNumber(t_kaihi.FCD_Kaihi, 5, 0));
			Rule.Add(new GControlDBRuleText(t_kaihi.FKaihi_Name, 30));
			Rule.Add(new GControlDBRuleText(t_kaihi.FKaihi_ShortName, 30));
			Rule.Add(new GControlDBRuleText(t_kaihi.FKaihi_Bikou, 20));
			Rule.Add(new GControlDBRuleNumber(t_kaihi.FKaihi_GunCode, 4, 0));

			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost1, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost2, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost3, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost4, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost5, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost6, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost7, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost8, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost9, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost10, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost11, 7, 0));
			Rule.Add(new GControlDBRuleCurrency(t_kaihi.FKaihi_GetsugakuCost12, 7, 0));

			Rule.Add(code4, t_kaihi.FKaihi_BankCode);
			Rule.Add(code3, t_kaihi.FKaihi_BankCodeShiten);
			Rule.Add(code7, t_kaihi.FKaihi_BankKozaNo);
			Rule.Add(kana30, t_kaihi.FKaihi_BankKozaName);
			#endregion

			#region +++ t_iryokikan +++
			Rule.Add(new GControlDBRuleNumber(t_iryokikan.FIRK_Code, 5, 0));
			Rule.Add(new GControlDBRuleText(t_iryokikan.FIRK_Name, 30));
			Rule.Add(new GControlDBRuleText(t_iryokikan.FIRK_Kana, 30));
			Rule.Add(new GControlDBRuleText(t_iryokikan.FIRK_Tsusho, 30));

			Rule.Add(post, t_iryokikan.FIRK_Post);
			Rule.Add(new GControlDBRuleText(t_iryokikan.FIRK_Addr2, 40));
			Rule.Add(new GControlDBRuleHyphenSplit(t_iryokikan.FIRK_Tel1, new int[] { 5, 4, 5 }));
			Rule.Add(new GControlDBRuleHyphenSplit(t_iryokikan.FIRK_Fax1, new int[] { 5, 4, 5 }));
			Rule.Add(new GControlDBRuleNumber(t_iryokikan.FIRK_Kyoka, 4, 0));
			#endregion

			#region +++ t_shinryoka +++
			Rule.Add(new GControlDBRuleNumber(t_shinryoka.FSRK_Code, 2, 0));
			Rule.Add(new GControlDBRuleText(t_shinryoka.FSRK_Name, 30));
			#endregion

			#region +++ t_kaiin_ido +++
			Rule.Add(new GControlDBRuleDate(t_kaiin_ido.FIdoDate));
			Rule.Add(new GControlDBRuleText(t_kaiin_ido.FIdoJiyuEtcMemo, 12));
			#endregion

			#region +++ t_hikiotoshi +++
			Rule.Add(curr, t_hikiotoshi.FHiki_Cost);
			Rule.Add(memo20, t_hikiotoshi.FHiki_Memo);
			#endregion
		}

		/// <summary>
		/// コントロールに設定されたルールを適用します。
		/// </summary>
		/// <param name="ctl">適用させるコントロール</param>
		/// <param name="rowfield">フィールド名</param>
		public static void SetControlByRule(Control ctl, string rowfield)
		{
			SetControlByRule(ctl, rowfield, "");
		}
		
		/// <summary>
		/// コントロールに設定されたルールを適用します。
		/// </summary>
		/// <param name="ctl">適用させるコントロール</param>
		/// <param name="rowfield">フィールド名</param>
		/// <param name="datatag">データタグ名</param>
		public static void SetControlByRule(Control ctl, string rowfield, string datatag)
		{
			Control[] ctls = { ctl };
			
			SetControlByRule(ctls, rowfield, datatag);
		}
		
		/// <summary>
		/// コントロールに設定されたルールを適用します。
		/// </summary>
		/// <param name="ctls">適用させるコントロール</param>
		/// <param name="rowfield">フィールド名</param>
		public static void SetControlByRule(Control[] ctls, string rowfield)
		{
			SetControlByRule(ctls, rowfield, "");
		}
		
		/// <summary>
		/// コントロールに設定されたルールを適用します。
		/// </summary>
		/// <param name="ctls">適用させるコントロール</param>
		/// <param name="rowfield">フィールド名</param>
		/// <param name="datatag">データタグ名</param>
		public static void SetControlByRule(Control[] ctls, string rowfield, string datatag)
		{
			GControlDBRuleBase	rbase = Rule.GetRule(rowfield, datatag);
			
			if (rbase != null)
			{
				rbase.SetControlByRule(ctls, false);
			}
		}
	}
}
