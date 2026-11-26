using System.Collections.Generic;
using System.Data;
using ComponentIO;

namespace App
{
    /// <summary>
    /// 行の内容の控え。入力の画面が、「画面を表示したときから、内容が変わったか」を調べるのに使う。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 使い方: 画面を表示して、最初の値を入れ終わったところ（FormFrame_Shown の最後）で、編集する行から作る。
    /// 閉じるときに <see cref="IsModified"/> で比べて、変わっているときだけ、破棄の確認を出す。
    /// 追加・訂正・コピー追加のどれでも、同じ使い方ができる（追加のときは、元の行と編集する行が同じなので、
    /// 元の行と比べる方法では、入力があったかが分からない）。
    /// </para>
    /// <para>
    /// 比べ方は <see cref="AppDb.CheckModified"/> と同じ。値を文字列にして比べ、ID_Auto は見ない。
    /// </para>
    /// </remarks>
    internal sealed class RowSnapshot
    {
        /// <summary>比べない項目（行を見分けるための連番）。</summary>
        private const string IgnoredColumn = "ID_Auto";

        private readonly Dictionary<string, string> _values = new Dictionary<string, string>();

        /// <summary><paramref name="row"/> の、いまの内容を控える。</summary>
        public RowSnapshot(DataRow row)
        {
            foreach (DataColumn column in row.Table.Columns)
            {
                if (column.ColumnName == IgnoredColumn)
                {
                    continue;
                }
                _values[column.ColumnName] = Cast.String(row[column]);
            }
        }

        /// <summary><paramref name="row"/> の内容が、控えたときから変わっていれば true。</summary>
        public bool IsModified(DataRow row)
        {
            foreach (KeyValuePair<string, string> pair in _values)
            {
                if (row.Table.Columns.Contains(pair.Key) == false)
                {
                    continue;
                }
                if (Cast.String(row[pair.Key]) != pair.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
