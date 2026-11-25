using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using App.Controls;

namespace App
{
    /// <summary>
    /// 画面ごとのファンクションキー定義（<see cref="FuncKeyDefine"/> の配列。AppFormFuncKey.cs の Func○○.Functions）を
    /// <see cref="FunctionKeyBar"/> に表示し、キーが押されたら対応するメソッドを実行する。
    /// GcFunctionKey 用の <see cref="AppFunctionKey"/> と同じ役割で、FunctionKeyBar を使う画面ではこちらを使う。
    /// <para>
    /// 実行するメソッドは <see cref="On"/> で登録する。登録先はこのインスタンスなので、画面と一緒に破棄される。
    /// AppFunctionKey と同じ「Func○○.Save.Execute = メソッド」の書き方も使えるが、そちらは static な定義に画面の
    /// メソッドを入れるので、画面を閉じても参照が残り、同じ定義を使う画面を重ねて開くと上書きされる。新しく書く場合は On を使う。
    /// </para>
    /// 仕様は _doc/spec/ファンクションキー.md にある。
    /// </summary>
    public sealed class FunctionKeyBinder
    {
        /// <summary>
        /// 青く強調するボタンの、定義上の名前（_doc/AppFuncKey.xlsx の EnumName 列）。
        /// その画面の主な操作（登録・実行）を表す名前を並べる。
        /// </summary>
        private static readonly string[] EmphasizedNames = { "Exec", "Save", "Entry" };

        /// <summary>このキーが定義の最後にある場合に、右寄せのボタンを作る。</summary>
        private const Keys RightEndKey = Keys.F12;

        private const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;
        /// <summary>Func○○ クラスが持つ、定義の配列のフィールド名。</summary>
        private const string DefinesFieldName = "Functions";

        /// <summary>定義の配列（<see cref="DefinesFieldName"/>）を持つクラスの一覧。最初に必要になったときに 1 回だけ調べる。</summary>
        private static Type[] _defineOwnerTypes;

        private readonly FunctionKeyBar _bar;
        private readonly FuncKeyDefine[] _defines;
        private readonly Dictionary<FuncKeyDefine, Action> _actions = new Dictionary<FuncKeyDefine, Action>();

        /// <summary>
        /// <paramref name="bar"/> に、<paramref name="defines"/> の順でボタンを作る。
        /// <paramref name="bar"/> にすでにあるボタンは取り除く。
        /// </summary>
        /// <param name="bar">ボタンを表示する帯。</param>
        /// <param name="defines">ファンクションキーの定義。Func○○.Functions を渡す。null の場合はボタンなしになる。</param>
        public FunctionKeyBinder(FunctionKeyBar bar, FuncKeyDefine[] defines)
        {
            if (bar == null)
            {
                throw new ArgumentNullException(nameof(bar));
            }

            _bar = bar;
            _defines = defines ?? new FuncKeyDefine[0];

            Dictionary<FuncKeyDefine, string> names = FindDefineNames(_defines);
            int firstRightIndex = FindFirstRightAlignedIndex(_defines);

            _bar.SuspendLayout();
            _bar.ClearButtons();
            for (int i = 0; i < _defines.Length; i++)
            {
                FuncKeyDefine define = _defines[i];

                // 同じキーの定義が重なっている場合は、先にあるものを使う
                if (_bar.GetButton(define.Key) != null)
                {
                    continue;
                }

                FunctionKeyBarButton button = _bar.AddButton(define.Key, define.Name);
                button.Alignment = i >= firstRightIndex ? FunctionKeyAlignment.Right : FunctionKeyAlignment.Left;

                string name;
                button.Emphasized = names.TryGetValue(define, out name) && Array.IndexOf(EmphasizedNames, name) >= 0;
            }
            _bar.ResumeLayout();

            _bar.FunctionKeyPressed += delegate(object sender, FunctionKeyEventArgs e) { Exec(e.Key); };
        }

        /// <summary>ボタンを表示している帯。</summary>
        public FunctionKeyBar Bar
        {
            get { return _bar; }
        }

        /// <summary>
        /// <paramref name="define"/> のキーが押されたときに実行するメソッドを登録する。
        /// 同じ定義に 2 回登録した場合は、後から登録したものに置き換わる。
        /// </summary>
        /// <param name="define">Func○○.Save のような、キー 1 つ分の定義。</param>
        /// <param name="action">実行するメソッド。null を渡すと登録を取り消す。</param>
        public void On(FuncKeyDefine define, Action action)
        {
            if (define == null)
            {
                return;
            }

            if (action == null)
            {
                _actions.Remove(define);
            }
            else
            {
                _actions[define] = action;
            }
        }

        /// <summary>
        /// <paramref name="key"/> に対応するメソッドを実行する。
        /// <see cref="On"/> で登録したメソッドがあればそれを、無ければ定義の Execute を実行する。どちらも無ければ何もしない。
        /// 帯が無効（Enabled が false）のときは実行しない。
        /// </summary>
        public void Exec(Keys key)
        {
            if (!_bar.Enabled)
            {
                return;
            }

            foreach (FuncKeyDefine define in _defines)
            {
                if (define.Key != key)
                {
                    continue;
                }

                Action action;
                if (_actions.TryGetValue(define, out action))
                {
                    action();
                }
                else if (define.Execute != null)
                {
                    define.Execute();
                }
                return;
            }
        }

        /// <summary>
        /// <paramref name="key"/> のボタンを押せるかどうかを設定する。そのキーのボタンが無い場合は何もしない。
        /// </summary>
        public void SetEnabled(Keys key, bool enabled)
        {
            FunctionKeyBarButton button = _bar.GetButton(key);
            if (button != null)
            {
                button.Enabled = enabled;
            }
        }

        /// <summary>
        /// <paramref name="key"/> のボタンを表示するかどうかを設定する。そのキーのボタンが無い場合は何もしない。
        /// </summary>
        public void SetVisible(Keys key, bool visible)
        {
            FunctionKeyBarButton button = _bar.GetButton(key);
            if (button != null)
            {
                button.Visible = visible;
            }
        }

        /// <summary>
        /// 右寄せにする最初のボタンの位置を返す。この位置より前が左寄せ、この位置以降が右寄せになる。
        /// 右寄せにするボタンが無い場合は、定義の数（どのボタンよりも後ろ）を返す。
        /// </summary>
        /// <remarks>
        /// 最後のキーが F12 のときだけ、右寄せのボタンを作る。
        /// ボタンが 1 つなら、それを右寄せにする。2 つなら、1 つ目を左寄せ、2 つ目を右寄せにする（F11・F12 なら F11 が左、F12 が右）。
        /// 3 つ以上の場合は、隣り合うキーの番号がいちばん離れている所を左右の境目にする（F5・F11・F12 なら F5 と F11 の間）。
        /// キーボード上で近いキーどうしを、画面でも同じ側にまとめるため。
        /// 番号が続いている所（F11 と F12 など）は境目にしない。いちばん離れている所が複数ある場合は、右側のものを使う。
        /// すべての番号が続いている場合は、全部が右寄せになる。
        /// </remarks>
        private static int FindFirstRightAlignedIndex(FuncKeyDefine[] defines)
        {
            if (defines.Length == 0 || defines[defines.Length - 1].Key != RightEndKey)
            {
                return defines.Length;
            }
            if (defines.Length <= 2)
            {
                // 1 つなら全部（その 1 つ）が右、2 つなら最後の 1 つだけが右
                return defines.Length - 1;
            }

            int splitIndex = 0;
            int widestGap = 1;
            for (int i = 1; i < defines.Length; i++)
            {
                // F1～F24 のキー コードは連番なので、差がそのままキーの番号の差になる
                int gap = (int)defines[i].Key - (int)defines[i - 1].Key;
                if (gap > 1 && gap >= widestGap)
                {
                    widestGap = gap;
                    splitIndex = i;
                }
            }
            return splitIndex;
        }

        /// <summary>
        /// 各定義の、定義上の名前（Save・Exec など）を調べて返す。
        /// </summary>
        /// <remarks>
        /// 名前は <see cref="FuncKeyDefine"/> には入っておらず、自動生成される Func○○ クラスの static フィールド名
        /// （public readonly static FuncKeyDefine Save = Functions[1]; の「Save」）としてだけ存在する。
        /// そこで、渡された配列を Functions フィールドに持っているクラスを探し、そのクラスのフィールド名から求める。
        /// 該当するクラスが無い場合（Func○○.Functions 以外の配列を渡された場合）は、空の結果を返す。
        /// </remarks>
        private static Dictionary<FuncKeyDefine, string> FindDefineNames(FuncKeyDefine[] defines)
        {
            var names = new Dictionary<FuncKeyDefine, string>();

            foreach (Type type in GetDefineOwnerTypes())
            {
                if (!ReferenceEquals(type.GetField(DefinesFieldName, PublicStatic).GetValue(null), defines))
                {
                    continue;
                }

                foreach (FieldInfo field in type.GetFields(PublicStatic))
                {
                    if (field.FieldType != typeof(FuncKeyDefine))
                    {
                        continue;
                    }

                    var define = (FuncKeyDefine)field.GetValue(null);
                    if (define != null)
                    {
                        names[define] = field.Name;
                    }
                }
                break;
            }

            return names;
        }

        private static Type[] GetDefineOwnerTypes()
        {
            if (_defineOwnerTypes == null)
            {
                var types = new List<Type>();
                foreach (Type type in typeof(FuncKeyDefine).Assembly.GetTypes())
                {
                    FieldInfo field = type.GetField(DefinesFieldName, PublicStatic);
                    if (field != null && field.FieldType == typeof(FuncKeyDefine[]))
                    {
                        types.Add(type);
                    }
                }
                _defineOwnerTypes = types.ToArray();
            }
            return _defineOwnerTypes;
        }
    }
}
