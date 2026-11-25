using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    /// <summary>
    /// 画面で使うフォントが PC に入っていない場合に、別のフォントへ差し替えるための補助。
    /// フォントが無くても Windows が自動で代わりのフォントを選ぶが、何が選ばれるかは環境任せになるため、
    /// 代わりに使うフォントをアプリ側で決めておきたいときに使う。
    /// </summary>
    internal static class FontFallback
    {
        /// <summary>
        /// 指定した名前のフォントがこの PC で使えるかを調べる。
        /// </summary>
        public static bool IsInstalled(string familyName)
        {
            if (string.IsNullOrEmpty(familyName))
            {
                return false;
            }

            try
            {
                using (new FontFamily(familyName))
                {
                    return true;
                }
            }
            catch (ArgumentException)
            {
                // FontFamily は、見つからない名前を渡すと ArgumentException を投げる
                return false;
            }
        }

        /// <summary>
        /// <paramref name="root"/> とその配下のコントロール・メニュー項目のうち、
        /// この PC に入っていないフォントを指定しているものを <paramref name="fallbackFamily"/> に差し替える。
        /// 大きさと太字などのスタイルは元のフォントのまま引き継ぐ。
        /// </summary>
        /// <remarks>
        /// 差し替え対象は「指定されたフォントが使えるかどうか」だけで決める。フォント名との照合はしないので、
        /// デザイナーがフォント名を日本語名（例: BIZ UDPゴシック）と英語名のどちらで書き出していても同じように動く。
        /// 差し替え用に作ったフォントは <paramref name="root"/> の破棄時にまとめて破棄する。
        /// </remarks>
        public static void ReplaceMissingFamilies(Control root, string fallbackFamily)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            // 代わりのフォントも無い PC では差し替えても意味がないので、Windows の自動選択に任せる
            if (!IsInstalled(fallbackFamily))
            {
                return;
            }

            var session = new ReplaceSession(fallbackFamily);
            session.ReplaceInControl(root);

            if (session.HasReplacements)
            {
                root.Disposed += delegate { session.DisposeFonts(); };
            }
        }

        /// <summary>
        /// 1 回の差し替え処理のあいだ使う作業用の状態。
        /// </summary>
        private sealed class ReplaceSession
        {
            private readonly string _fallbackFamily;

            // フォント名ごとの「使えるかどうか」。同じ名前を何度も調べないために覚えておく
            private readonly Dictionary<string, bool> _installed = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

            // 元のフォントごとの差し替え先。同じ指定のフォントを何度も作らないために覚えておく
            private readonly Dictionary<Font, Font> _replacements = new Dictionary<Font, Font>();

            public ReplaceSession(string fallbackFamily)
            {
                _fallbackFamily = fallbackFamily;
            }

            public bool HasReplacements
            {
                get { return _replacements.Count > 0; }
            }

            public void ReplaceInControl(Control control)
            {
                // 親から先に差し替える。フォントを個別に指定していない子は親のフォントをそのまま使っているので、
                // 親を差し替えた時点で子も差し替え後のフォントになり、下の判定には掛からない
                if (IsMissing(control.Font))
                {
                    control.Font = GetReplacement(control.Font);
                }

                var toolStrip = control as ToolStrip;
                if (toolStrip != null)
                {
                    ReplaceInItems(toolStrip.Items);
                }

                foreach (Control child in control.Controls)
                {
                    ReplaceInControl(child);
                }
            }

            public void DisposeFonts()
            {
                foreach (Font font in _replacements.Values)
                {
                    font.Dispose();
                }
                _replacements.Clear();
            }

            private void ReplaceInItems(ToolStripItemCollection items)
            {
                foreach (ToolStripItem item in items)
                {
                    if (IsMissing(item.Font))
                    {
                        item.Font = GetReplacement(item.Font);
                    }

                    var dropDownItem = item as ToolStripDropDownItem;
                    if (dropDownItem != null)
                    {
                        ReplaceInItems(dropDownItem.DropDownItems);
                    }
                }
            }

            private bool IsMissing(Font font)
            {
                // フォントが PC に無い場合、Name は Windows が選んだ代わりのフォントの名前になる。
                // 「何を指定したか」は OriginalFontName に残っているので、そちらで調べる。
                // OriginalFontName が空なのは名前を指定せずに作られたフォント（システム既定など）で、差し替え対象にしない
                string requestedName = font.OriginalFontName;
                if (string.IsNullOrEmpty(requestedName))
                {
                    return false;
                }

                bool installed;
                if (!_installed.TryGetValue(requestedName, out installed))
                {
                    installed = IsInstalled(requestedName);
                    _installed.Add(requestedName, installed);
                }
                return !installed;
            }

            private Font GetReplacement(Font original)
            {
                Font replacement;
                if (!_replacements.TryGetValue(original, out replacement))
                {
                    replacement = new Font(_fallbackFamily, original.Size, original.Style, original.Unit, original.GdiCharSet);
                    _replacements.Add(original, replacement);
                }
                return replacement;
            }
        }
    }
}
