using System.Text.RegularExpressions;
using ComponentDB;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 銀行コードの一覧を、検索欄（銀行・支店）の内容で絞り込むための条件を作る。
    /// 銀行コードの選択の画面（<see cref="FormSelectorBankCode2"/>）と、銀行コード マスタの画面（<see cref="FormMasterBankCode2"/>）が使う。
    /// </summary>
    /// <remarks>
    /// Gc 系の画面（FormSelectorBankCode・FormMasterBankCode）は、同じ内容をそれぞれの画面の中に書いている。
    /// このクラスは Gc 系を参照しない。
    /// </remarks>
    public static class AppBankCodeFilter
    {
        /// <summary>
        /// 絞り込みの条件（DataView の RowFilter に渡す文字列）を作る。どちらの検索欄も空なら、空文字列（絞り込まない）。
        /// </summary>
        /// <param name="bank">銀行の検索欄の内容。</param>
        /// <param name="shiten">支店の検索欄の内容。</param>
        public static string Create(string bank, string shiten)
        {
            string filter = createPart(bank, t_bank_code.FBCD_Code, t_bank_code.FBCD_FullName, t_bank_code.FBCD_NameFurigana);
            string part = createPart(shiten, t_bank_code.FBCD_CodeShiten, t_bank_code.FBCD_NameShiten, t_bank_code.FBCD_NameShitenFurigana);

            if (filter != "" && part != "")
            {
                filter += " AND ";
            }
            return filter + part;
        }

        /// <summary>
        /// 検索欄 1 つ分の条件を作る。数字だけならコードが一致するもの、それ以外なら名称かフリガナで探す
        /// </summary>
        static string createPart(string str, string fieldCode, string fieldName, string fieldFurigana)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            string str_numchk = StrConv.ToNarrow(str);

            if (Regex.IsMatch(str_numchk, @"^[-.0-9]+$") == true)
            {
                // 数値のみならコード検索
                return string.Format("({0} = {1})", fieldCode, str_numchk);
            }

            // 一文字だけなら先頭一致、それ以外は部分一致
            bool partial = str.Length != 1;

            return string.Format("({0} OR {1})",
                                    DBQuery.CreateLikeKanaString(fieldName, str, partial),
                                    DBQuery.CreateLikeKanaString(fieldFurigana, str, partial));
        }
    }
}
