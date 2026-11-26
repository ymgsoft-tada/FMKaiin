using System.Windows.Forms;
using ComponentSUI;

namespace App
{
    /// <summary>
    /// 入力ルール（S 系のコントロールを使う画面用）。項目名から、入力欄の桁数・入力できる文字・IME などを引く。
    /// </summary>
    /// <remarks>
    /// <para>
    /// Gc 系のコントロールを使う画面用の <see cref="AppDbRule"/> と、同じ内容を S 系のルールで書いたもの。
    /// 画面を S 系へ移している間は、両方を持つ。ルールを変えるときは、両方を同じように直すこと
    /// （Test/RuleHarness.cs が、2 つが同じ内容になっているかを項目ごとに比べる。実行は Test/run.sh）。
    /// すべての画面を S 系へ移したら、AppDbRule をやめる。
    /// </para>
    /// <para>
    /// このクラスは Gc 系（GrapeCity のコントロールや、comset の GControlDBRule）を参照しない。
    /// </para>
    /// </remarks>
    public static class AppSControlRule
    {
        /// <summary>合計用ルール名</summary>
        public const string RuleCurrencyTotal = "Currency_Total";

        /// <summary>金額用ルール名</summary>
        public const string RuleCurrency = "Currency";

        /// <summary>金額用ルール名（9 桁）</summary>
        public const string RuleCurrency9 = "Currency9";

        /// <summary>金額用ルール名（9 桁・小数第 1 位）</summary>
        public const string RuleCurrency9_1 = "Currency9_1";

        /// <summary>小数点第 1 位用</summary>
        public const string RuleRatio1 = "Ratio1";
        /// <summary>小数点第 2 位用</summary>
        public const string RuleRatio2 = "Ratio2";
        /// <summary>小数点第 3 位用</summary>
        public const string RuleRatio3 = "Ratio3";

        /// <summary>検索用などの日付入力フィールド用</summary>
        public const string RuleDate = "Date";

        public const int Code6 = 6;

        private static SControlDBRule _rule;

        /// <summary>
        /// ルールの表。<see cref="SControlDB.EndAdd(SControlDBRule)"/> に渡す。最初に使うときに作る。
        /// </summary>
        public static SControlDBRule Rule
        {
            get
            {
                if (_rule == null)
                {
                    _rule = Create();
                }
                return _rule;
            }
        }

        /// <summary>
        /// 項目のルールを、コントロールに設定する。DB にバインドしない入力欄（検索欄など）に使う。
        /// </summary>
        /// <param name="ctl">設定するコントロール</param>
        /// <param name="rowfield">項目名（またはルール名）</param>
        public static void SetControlByRule(Control ctl, string rowfield)
        {
            Rule.SetControlByRule(ctl, rowfield);
        }

        /// <summary>
        /// 項目のルールを、コントロールに設定する（入力欄が複数ある項目）。
        /// </summary>
        /// <param name="ctls">設定するコントロール</param>
        /// <param name="rowfield">項目名（またはルール名）</param>
        public static void SetControlByRule(Control[] ctls, string rowfield)
        {
            Rule.SetControlByRule(ctls, rowfield);
        }

        private static SControlDBRule Create()
        {
            var rule = new SControlDBRule();

            // ---- 複数の項目で使い回すルール ----

            // コード（左寄せ）
            SControlRule code = SControlRule.Number(Code6, 0);
            code.Parts[0].TextAlign = HorizontalAlignment.Left;
            rule.Add(code, "Code");

            SControlRule code4 = SControlRule.Number(4, 0);
            rule.Add(code4, "Code4");

            SControlRule code3 = SControlRule.Number(3, 0);
            rule.Add(code3, "Code3");

            SControlRule code7 = SControlRule.Number(7, 0);
            rule.Add(code7, "Code7");

            SControlRule code10 = SControlRule.Number(10, 0);
            rule.Add(code10, "Code10");

            rule.Add(SControlRule.Number(12, 0), "Code12");

            // 合計値フィールド
            rule.Add(SControlRule.Currency(11, 0), RuleCurrencyTotal);

            rule.Add(SControlRule.Currency(9, 0), RuleCurrency9);
            rule.Add(SControlRule.Currency(9, 1), RuleCurrency9_1);

            SControlRule curr = SControlRule.Currency(7, 0);
            rule.Add(curr, RuleCurrency);

            rule.Add(SControlRule.Number(1, 1), RuleRatio1);
            rule.Add(SControlRule.Number(3, 2), RuleRatio2);
            rule.Add(SControlRule.Number(3, 3), RuleRatio3);

            // 郵便番号（住所上段を 30 桁）
            SControlRule post = SControlRule.PostAddr(30, PostAddrRubyLength);

            // 半角カナのフィールド
            rule.Add(HalfKana(40), "Furigana");
            SControlRule kana20 = HalfKana(20);
            rule.Add(kana20, "Furigana20");
            rule.Add(HalfKana(60), "Furigana60");
            SControlRule kana30 = HalfKana(30);
            rule.Add(kana30, "Furigana30");

            // 英数字のフィールド（IME をオフにするだけで、文字の種類は制限しない）
            rule.Add(TextWithIme(40, ImeMode.Off), "Eisu");
            SControlRule eisu20 = TextWithIme(20, ImeMode.Off);
            rule.Add(eisu20, "Eisu20");

            // IME を使わせないフィールド
            SControlRule hankaku = TextWithIme(80, ImeMode.Disable);
            SControlRule numStr13 = TextWithIme(13, ImeMode.Disable);

            SControlRule memo20 = SControlRule.Text(20);

            // 検索用などの日付入力フィールド
            rule.Add(SControlRule.Date(), RuleDate);

            // ---- t_basic ----
            rule.Add(SControlRule.Ruby(40, 80), t_basic.FBAS_Name);
            rule.Add(SControlRule.Text(30), t_basic.FBAS_NameDaihyo);
            rule.Add(post, t_basic.FBAS_Post);
            rule.Add(SControlRule.Text(40), t_basic.FBAS_Addr2);
            rule.Add(Phone(), t_basic.FBAS_Tel1);
            rule.Add(Phone(), t_basic.FBAS_Tel2);
            rule.Add(Phone(), t_basic.FBAS_Fax);

            rule.Add(SControlRule.Date(), t_basic.FBAS_DateYM);
            rule.Add(SControlRule.Date(), t_basic.FBAS_DateYMLatest);

            rule.Add(numStr13, t_basic.FBAS_HojinNo);
            rule.Add(eisu20, t_basic.FBAS_MyNoPWD);

            // ---- t_tantosha ----
            rule.Add(code, t_tantosha.FCD_Tanto);
            rule.Add(SControlRule.Text(20), t_tantosha.FTNT_Name);
            rule.Add(eisu20, t_tantosha.FTNT_Password);

            // ---- t_kaiin ----
            rule.Add(code, t_kaiin.FCD_Kaiin);
            rule.Add(SControlRule.Ruby(15, 30), t_kaiin.FKaiin_Name);
            rule.Add(SControlRule.Ruby(15, 30), t_kaiin.FKaiin_NameOld);
            rule.Add(post, t_kaiin.FKaiin_Post);
            rule.Add(SControlRule.Text(40), t_kaiin.FKaiin_Addr2);
            rule.Add(Phone(), t_kaiin.FKaiin_Tel1);
            rule.Add(Phone(), t_kaiin.FKaiin_Tel2);
            rule.Add(Phone(), t_kaiin.FKaiin_Fax);
            rule.Add(SControlRule.Number(7, 0), t_kaiin.FKaiin_IsekiTorokuNo);
            rule.Add(code4, t_kaiin.FKaiin_BankCode1);
            rule.Add(code3, t_kaiin.FKaiin_BankShitenCode1);
            rule.Add(code7, t_kaiin.FKaiin_BankKozaNo1);
            rule.Add(kana30, t_kaiin.FKaiin_BankKozaName1);
            rule.Add(code4, t_kaiin.FKaiin_BankCode2);
            rule.Add(code3, t_kaiin.FKaiin_BankShitenCode2);
            rule.Add(code7, t_kaiin.FKaiin_BankKozaNo2);
            rule.Add(kana30, t_kaiin.FKaiin_BankKozaName2);
            rule.Add(code4, t_kaiin.FKaiin_BankCode3);
            rule.Add(code3, t_kaiin.FKaiin_BankShitenCode3);
            rule.Add(code7, t_kaiin.FKaiin_BankKozaNo3);
            rule.Add(kana30, t_kaiin.FKaiin_BankKozaName3);
            rule.Add(hankaku, t_kaiin.FKaiin_Mail1);
            rule.Add(SControlRule.Text(40), t_kaiin.FKaiin_ShusshinKo);
            rule.Add(SControlRule.Text(40), t_kaiin.FKaiin_ShusshinIn);
            rule.Add(SControlRule.Text(40), t_kaiin.FKaiin_TaikaiEtcmemo);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateBirth);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateIsekiToroku);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateToroku);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateNyukai);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateShonin);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateSotsugyo);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateShuryo);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateShutokuGakui);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateTaikai);
            rule.Add(SControlRule.Date(), t_kaiin.FKaiin_DateShinsei);
            rule.Add(SControlRule.Text(20), t_kaiin.FKaiin_ShiteiiEtc);

            // ---- t_bank ----
            rule.Add(code4, t_bank.FBNK_Code);
            rule.Add(code3, t_bank.FBNK_CodeShiten);
            rule.Add(code7, t_bank.FBNK_KozaNo);
            rule.Add(SControlRule.Text(20), t_bank.FBNK_Name);
            rule.Add(kana20, t_bank.FBNK_NameFurigana);
            rule.Add(SControlRule.Text(20), t_bank.FBNK_NameShiten);
            rule.Add(kana20, t_bank.FBNK_NameFuriganaShiten);
            rule.Add(code10, t_bank.FBNK_CodeCompany);

            // ---- t_bank_code ----
            rule.Add(code4, t_bank_code.FBCD_Code);
            rule.Add(code3, t_bank_code.FBCD_CodeShiten);
            rule.Add(SControlRule.Ruby(20, 40), t_bank_code.FBCD_Name);
            rule.Add(SControlRule.Ruby(20, 40), t_bank_code.FBCD_NameShiten);

            // ---- t_kaihi ----
            rule.Add(SControlRule.Number(5, 0), t_kaihi.FCD_Kaihi);
            rule.Add(SControlRule.Text(30), t_kaihi.FKaihi_Name);
            rule.Add(SControlRule.Text(30), t_kaihi.FKaihi_ShortName); // 完全にフリガナではなさそうなので個別にルール
            rule.Add(SControlRule.Text(20), t_kaihi.FKaihi_Biko);
            rule.Add(SControlRule.Number(4, 0), t_kaihi.FKaihi_GunCode);

            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost1);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost2);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost3);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost4);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost5);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost6);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost7);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost8);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost9);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost10);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost11);
            rule.Add(SControlRule.Currency(7, 0), t_kaihi.FKaihi_GetsugakuCost12);

            rule.Add(code4, t_kaihi.FKaihi_BankCode);
            rule.Add(code3, t_kaihi.FKaihi_BankCodeShiten);
            rule.Add(code7, t_kaihi.FKaihi_BankKozaNo);
            rule.Add(SControlRule.Ruby(15, 30), t_kaihi.FKaihi_BankKozaName);

            // ---- t_iryokikan ----
            rule.Add(SControlRule.Number(5, 0), t_iryokikan.FIRK_Code);
            rule.Add(SControlRule.Text(30), t_iryokikan.FIRK_Name);
            rule.Add(SControlRule.Text(30), t_iryokikan.FIRK_Kana);
            rule.Add(SControlRule.Text(30), t_iryokikan.FIRK_Tsusho);

            rule.Add(post, t_iryokikan.FIRK_Post);
            rule.Add(SControlRule.Text(40), t_iryokikan.FIRK_Addr2);
            rule.Add(Phone(), t_iryokikan.FIRK_Tel1);
            rule.Add(Phone(), t_iryokikan.FIRK_Fax1);
            rule.Add(SControlRule.Number(4, 0), t_iryokikan.FIRK_Kyoka);

            // ---- t_shinryoka ----
            rule.Add(SControlRule.Number(2, 0), t_shinryoka.FSRK_Code);
            rule.Add(SControlRule.Text(30), t_shinryoka.FSRK_Name);

            // ---- t_kaiin_ido ----
            rule.Add(SControlRule.Date(), t_kaiin_ido.FIdoDate);
            rule.Add(SControlRule.Text(20), t_kaiin_ido.FIdoJiyuDetail_IdoMemo);
            rule.Add(SControlRule.Text(20), t_kaiin_ido.FIdoJiyuDetail_KaiinMemo); // 未使用列
            rule.Add(SControlRule.Text(20), t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo);
            rule.Add(SControlRule.Text(20), t_kaiin_ido.FIdoJiyuDetail_AddressMemo);
            rule.Add(SControlRule.Text(20), t_kaiin_ido.FIdoJiyuDetail_EtcMemo);

            // ---- t_hikiotoshi ----
            rule.Add(curr, t_hikiotoshi.FHiki_Cost);
            rule.Add(memo20, t_hikiotoshi.FHiki_Memo);

            // ---- t_kaiin_chosei ----
            rule.Add(SControlRule.Date(), t_kaiin_chosei.FKCS_DateYM);
            rule.Add(curr, t_kaiin_chosei.FKCS_Cost);
            rule.Add(memo20, t_kaiin_chosei.FKCS_Memo);

            return rule;
        }

        /// <summary>住所のふりがなの最大文字数（住所のふりがなの欄を持つ画面は、いまは無い）。</summary>
        private const int PostAddrRubyLength = 50;

        /// <summary>電話番号・FAX 番号（市外局番 5 桁・市内局番 4 桁・番号 5 桁まで）。</summary>
        private static SControlRule Phone()
        {
            return SControlRule.HyphenSplit(5, 4, 5);
        }

        /// <summary>半角カナで入力する欄（半角文字だけ、空白は半角だけ、IME は半角カタカナ）。</summary>
        private static SControlRule HalfKana(int maxLength)
        {
            SControlRule rule = SControlRule.Text(maxLength);
            rule.Parts[0].Format = "H";
            rule.Parts[0].AllowSpace = SAllowSpace.Narrow;
            rule.Parts[0].ImeMode = ImeMode.KatakanaHalf;
            return rule;
        }

        /// <summary>文字の種類は制限せず、IME の状態だけを指定する欄。</summary>
        private static SControlRule TextWithIme(int maxLength, ImeMode imeMode)
        {
            SControlRule rule = SControlRule.Text(maxLength);
            rule.Parts[0].ImeMode = imeMode;
            return rule;
        }
    }
}
