using System.Windows.Forms;

namespace App.Controls
{
    /// <summary>
    /// 入力の途中の内容を、値として確定できるコントロール（<see cref="SDateBox"/>・<see cref="STableComboBox"/>）。
    /// これらのコントロールは、フォーカスがほかへ移るときに値を確定する。ファンクション キーでの保存のように、
    /// フォーカスを動かさずに値を読む処理は、読む前に <see cref="CommitEdit"/> を呼ぶこと
    /// （<see cref="App.SControlDB"/> の書き戻しは、自動で呼ぶ）。
    /// </summary>
    public interface ISEditCommit
    {
        /// <summary>
        /// 入力の途中の内容を確定する。フォーカスがほかへ移ったときと同じ結果になる（フォーカスは動かさない）。
        /// 入力の途中でなければ、何も変えない。
        /// </summary>
        void CommitEdit();
    }

    /// <summary>
    /// フォームがコントロールより先にキーを処理する仕組み（Enter で次の項目へ移る、など）に対して、
    /// 「いまは、このキーを自分で使う」と答えるコントロール。
    /// 一覧やカレンダーを開いている間の Enter（指している行・日の確定）や矢印キーが、これに当たる。
    /// </summary>
    /// <remarks>
    /// フォームは、キーを処理する前に <see cref="UsesKey"/> を呼び、true が返ったら、そのキーをコントロールに任せる。
    /// コントロールの種類ごとの判定をフォームの側に書かなくて済むようにするためのもの。
    /// </remarks>
    public interface ISKeyUser
    {
        /// <summary>
        /// いまの状態で、<paramref name="keyCode"/> のキーをこのコントロールが使うか。
        /// </summary>
        bool UsesKey(Keys keyCode);
    }
}
