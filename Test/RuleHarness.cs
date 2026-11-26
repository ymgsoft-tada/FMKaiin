// 入力ルールの定義の検証用（FMKaiin のプロジェクトには含めない。実行は Test/run.sh）
// S 系の画面用の定義（AppSControlRule）が、Gc 系の画面用の定義（AppDbRule）と同じ内容になっているかを、項目ごとに比べる。
// ビルドした FMKaiin.exe の中の定義を読むので、先に FMKaiin をビルドしておくこと。
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using App;
using ComponentGControlDB;

internal static class RuleHarness
{
    private static int _failed;

    [STAThread]
    private static int Main()
    {
        Console.OutputEncoding = new UTF8Encoding(false);

        AppDbRule.Initialize();
        SControlDBRule sRules = AppSControlRule.Rule;

        // AppDbRule に登録されている項目名を取り出す（一覧を返すメンバーが無いので、中の表を直接読む）
        var gFields = new List<string>();
        var gRules = new Dictionary<string, GControlDBRuleBase>();
        FieldInfo indexField = typeof(GControlDBRule).GetField("cdbrule_index", BindingFlags.Instance | BindingFlags.NonPublic);
        if (indexField == null)
        {
            Console.WriteLine("NG   GControlDBRule の中の表（cdbrule_index）が見つからない");
            return 1;
        }
        foreach (DictionaryEntry entry in (IDictionary)indexField.GetValue(AppDbRule.Rule))
        {
            object search = entry.Value;
            string rowField = (string)GetMember(search, "RowField");
            string dataTag = (string)GetMember(search, "DataTag");
            if (!string.IsNullOrEmpty(dataTag))
            {
                Console.WriteLine("NG   データ タグ付きの登録がある（S 系では扱えない）: " + rowField + " / " + dataTag);
                _failed++;
                continue;
            }
            gFields.Add(rowField);
            gRules.Add(rowField, (GControlDBRuleBase)GetMember(search, "Rule"));
        }
        gFields.Sort(StringComparer.Ordinal);

        // 項目ごとに比べる
        foreach (string field in gFields)
        {
            SControlRule expected = SControlDBRuleConverter.Convert(gRules[field]);
            SControlRule actual = sRules.GetRule(field);
            if (actual == null)
            {
                Console.WriteLine("NG   " + field + " => AppSControlRule に無い。AppDbRule: " + Describe(expected, expected.Parts.Length));
                _failed++;
                continue;
            }

            int count = Math.Max(expected.Parts.Length, actual.Parts.Length);
            string e = Describe(expected, count);
            string a = Describe(actual, count);
            if (e == a)
            {
                Console.WriteLine("ok   " + field + " => " + a);
            }
            else
            {
                Console.WriteLine("NG   " + field + "\n       AppSControlRule: " + a + "\n       AppDbRule      : " + e);
                _failed++;
            }
        }

        // AppSControlRule にだけある項目
        foreach (string field in sRules.Fields)
        {
            if (!gRules.ContainsKey(field))
            {
                Console.WriteLine("NG   " + field + " => AppDbRule に無い");
                _failed++;
            }
        }
        Check("number of fields", sRules.Fields.Count, gFields.Count);

        // ルール名の定数
        Check("RuleCurrencyTotal", AppSControlRule.RuleCurrencyTotal, AppDbRule.RuleCurrencyTotal);
        Check("RuleCurrency", AppSControlRule.RuleCurrency, AppDbRule.RuleCurrency);
        Check("RuleCurrency9", AppSControlRule.RuleCurrency9, AppDbRule.RuleCurrency9);
        Check("RuleCurrency9_1", AppSControlRule.RuleCurrency9_1, AppDbRule.RuleCurrency9_1);
        Check("RuleRatio1", AppSControlRule.RuleRatio1, AppDbRule.RuleRatio1);
        Check("RuleRatio2", AppSControlRule.RuleRatio2, AppDbRule.RuleRatio2);
        Check("RuleRatio3", AppSControlRule.RuleRatio3, AppDbRule.RuleRatio3);
        Check("RuleDate", AppSControlRule.RuleDate, AppDbRule.RuleDate);
        Check("Code6", AppSControlRule.Code6, AppDbRule.Code6);

        // 銀行の一覧の列名
        Check("Fld_BankName", AppSTableCombo.Fld_BankName, AppTableCombo.Fld_BankName);

        Console.WriteLine(_failed == 0 ? "ALL PASSED" : ("FAILED: " + _failed));
        return _failed;
    }

    private static void Check(string name, object actual, object expected)
    {
        bool ok = Equals(actual, expected);
        if (!ok) _failed++;
        Console.WriteLine((ok ? "ok   " : "NG   ") + name + " => [" + actual + "]" + (ok ? "" : " expected [" + expected + "]"));
    }

    private static object GetMember(object target, string name)
    {
        const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        FieldInfo field = target.GetType().GetField(name, Flags);
        if (field != null)
        {
            return field.GetValue(target);
        }
        PropertyInfo property = target.GetType().GetProperty(name, Flags);
        if (property != null)
        {
            return property.GetValue(target, null);
        }
        throw new MissingMemberException(target.GetType().Name, name);
    }

    /// <summary>ルールの内容を、入力欄ごとに文字列にする（入力欄のルールが足りない分は、最後のものを使う）。</summary>
    private static string Describe(SControlRule rule, int count)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < count; i++)
        {
            SFieldRule p = rule.Parts[Math.Min(i, rule.Parts.Length - 1)];
            if (i > 0) sb.Append(" | ");
            sb.Append(p.MaxLength).Append('/').Append(p.MaxLengthUnit)
              .Append("/[").Append(p.Format).Append("]/").Append(p.AllowSpace)
              .Append('/').Append(p.ImeMode).Append('/').Append(p.TextAlign)
              .Append(p.HighlightText ? "/全選択" : "/全選択なし")
              .Append('/').Append(p.AcceptsCrLf);
            if (p.IsNumber)
            {
                sb.Append("/数値 小数").Append(p.DecimalPlaces)
                  .Append(p.ThousandsSeparator ? " 桁区切り" : "")
                  .Append(p.AllowNegative ? " マイナス可" : "")
                  .Append(p.AllowBlank ? " 空欄可" : "");
            }
        }
        return sb.ToString();
    }
}
