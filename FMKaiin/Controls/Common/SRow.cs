using System;
using System.Data;

namespace App
{
    /// <summary>
    /// 1 件分のデータ（行）。項目名で値を読み書きするだけの、最小限の入口。
    /// <see cref="SControlDB"/> は、行に対してこの操作しか行わない。データの持ち方（いまは DataTable）を将来変えることになっても、
    /// このインターフェースを実装したクラスを差し替えるだけで済むようにしている。
    /// </summary>
    public interface ISRow
    {
        /// <summary>
        /// 項目の値。値が無いことは <see cref="DBNull.Value"/> で表す。
        /// </summary>
        /// <param name="field">項目名。</param>
        object this[string field] { get; set; }

        /// <summary>
        /// <paramref name="field"/> という名前の項目があるか。
        /// </summary>
        bool Contains(string field);
    }

    /// <summary>
    /// <see cref="DataRow"/> を <see cref="ISRow"/> として扱うための包み。
    /// </summary>
    public sealed class SDataRow : ISRow
    {
        private readonly DataRow _row;

        public SDataRow(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }
            _row = row;
        }

        /// <summary>
        /// 包んでいる <see cref="DataRow"/>。
        /// 型付きの行クラス（t_kaiin など）で読み書きしたい場合に使う。使った箇所は DataTable に依存することになるので、
        /// 項目名での読み書きで足りる場合は、このプロパティを使わないこと。
        /// </summary>
        public DataRow Row
        {
            get { return _row; }
        }

        public object this[string field]
        {
            get { return _row[field]; }
            set { _row[field] = value ?? DBNull.Value; }
        }

        public bool Contains(string field)
        {
            return _row.Table.Columns.Contains(field);
        }

        /// <summary>
        /// <paramref name="row"/> を包んで返す。null なら null を返す。
        /// </summary>
        public static SDataRow Wrap(DataRow row)
        {
            return row == null ? null : new SDataRow(row);
        }
    }
}
