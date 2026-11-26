namespace App.Controls
{
    /// <summary>
    /// 入力できる空白の種類。書式（<see cref="SInputFormat"/>）とは別に、空白だけはこの設定で決まる。
    /// </summary>
    public enum SAllowSpace
    {
        /// <summary>空白を入力できない。</summary>
        None,
        /// <summary>全角の空白だけ入力できる。</summary>
        Wide,
        /// <summary>半角の空白だけ入力できる。</summary>
        Narrow,
        /// <summary>全角・半角のどちらの空白も入力できる。</summary>
        Both,
    }

    /// <summary>
    /// 貼り付ける文字列に改行が含まれているときの扱い。
    /// </summary>
    public enum SCrLfMode
    {
        /// <summary>改行をそのまま残す。1 行入力のコントロールでは、最初の改行より後ろを捨てる（標準のテキスト ボックスと同じ）。</summary>
        NoControl,
        /// <summary>改行だけを取り除いて、前後をつなげる。</summary>
        Filter,
        /// <summary>最初の改行より後ろを捨てる。</summary>
        Cut,
    }

    /// <summary>
    /// 最大文字数の数え方。
    /// </summary>
    public enum SLengthUnit
    {
        /// <summary>文字単位。サロゲート ペアの文字（「𠮷」など）は 2 と数える。DB の文字数（UTF-16 の長さ）と一致する。</summary>
        Char,
        /// <summary>バイト単位。半角の英数字・記号・カタカナを 1、それ以外を 2 と数える（Shift JIS のバイト数に相当）。</summary>
        Byte,
        /// <summary>見た目の文字単位。サロゲート ペアの文字や、異体字セレクター付きの文字を 1 と数える。</summary>
        TextElement,
    }
}
