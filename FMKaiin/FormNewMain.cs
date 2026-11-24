using System;
using System.Drawing;
using App.Controls;

namespace App
{
    /// <summary>
    /// アプリのトップメニュー（白を基調にした、透明感のある明るいデザイン）。
    /// 表示する項目と動作は Form1～Form4 と同じ。配置は次のとおり。
    /// ・日常処理のボタンを、月次の作業の流れ（準備 → 入力・確認 → 計算・出力）に沿って 3 列に並べている。
    /// ・マスター設定は、画面の下に横 1 列で並べている。
    /// 背景画像の上に、半透明のパネルと半透明のボタンを重ねている。
    /// 背景画像を画面いっぱいに描く処理は、継承元の <see cref="GlassBackdropForm"/> にある。
    /// 画面の構成は Form5.Designer.cs にあり、ボタンやセクションの見た目は Controls 配下のコントロールが描画する。
    /// </summary>
    public partial class FormNewMain : GlassBackdropForm
    {
        /// <summary>画面で指定しているフォント（游ゴシック）が PC に入っていない場合に使うフォント。</summary>
        private const string FallbackFontFamily = "Meiryo UI";

        /// <summary>タイトルバーの背景色。濃い紺色。</summary>
        private static readonly Color TitleBarBackColor = Color.FromArgb(32, 64, 99);

        /// <summary>タイトルバーの文字色。</summary>
        private static readonly Color TitleBarForeColor = Color.White;

        public FormNewMain()
        {
            InitializeComponent();
            ApplyFontFallback();
            ProcessingMonthChoices.Fill(cmbProcessingMonth);

            // 起動直後に Enter キーで先頭のメニューを実行できるよう、作業の流れの最初のボタンにフォーカスを置く
            ActiveControl = btnSelectMonth;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // タイトルバーの色を指定できるのは Windows 11 以降。それより前では標準の色のままになる
            TitleBarColor.Apply(this, TitleBarBackColor, TitleBarForeColor);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // デザイン時の大きさより小さくするとボタンの文字が収まらなくなるため、縮小できないようにする
            MinimumSize = Size;
        }

        /// <summary>
        /// 画面で指定しているフォントが入っていない PC では、
        /// そのフォントを使っている箇所を <see cref="FallbackFontFamily"/> に差し替える。
        /// フォントが入っている PC では何も変わらない。
        /// </summary>
        private void ApplyFontFallback()
        {
            FontFallback.ReplaceMissingFamilies(this, FallbackFontFamily);
        }
    }
}
