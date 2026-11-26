# Gc コントロール仕様一覧

Gc 系コントロール(グレープシティ製 InputMan / PlusPak)を標準コントロールへ置き換える検討のために、現在の使用箇所と使われ方をまとめる。置き換えの方式は、この文書では扱わない。

- 1 章: 全体像(型の一覧、依存関係、フォーム別の配置数)
- 2 章: 全フォーム共通の使われ方(DB バインド、入力ルール、グリッド連携)
- 3 章: 型ごとの仕様
- 4 章: Gc を内包するユーザーコントロール(UcDate / UcTableComboBox / UcFileSelect)
- 5 章: 標準コントロールに同等の機能が無いもの
- 6 章: 調査で気づいた点
- 7 章: 付録(フォーム別の全インスタンス一覧)

| 項目 | 内容 |
| --- | --- |
| 調査日 | 2026-11-25 |
| 対象 | `dev` ブランチ(b9601c3)の `FMKaiin/` 配下の全 `.cs`、および共通ライブラリ comset のうち Gc を参照するファイル |
| 調べ方 | Designer の宣言・プロパティ設定と、コードビハインドでの参照を機械的に集計し、型ごとの拡張クラスと共通ライブラリは目で読んだ |
| 件数の数え方 | Designer のフィールド宣言 1 つを 1 インスタンスとする |

---

## 1. 全体像

### 1.1 使っている型

| 型 | 定義元 | 継承元 | インスタンス数 | 状態 |
| --- | --- | --- | ---: | --- |
| `GcTextBoxEx` | comset `GcTextBoxEx.cs` | `GrapeCity.Win.Editors.GcTextBox` | 153 | フォームに 152、`UcFileSelect` の中に 1 |
| `GcComboBoxEx` | comset `GcComboBoxEx.cs` | `GrapeCity.Win.Editors.GcComboBox` | 53 | フォームに 52、`UcTableComboBox` の中に 1 |
| `GcNumberEx` | comset `GcNumberEx.cs` | `GrapeCity.Win.Editors.GcNumber` | 18 | フォームに 18 |
| `GcDateEx` | comset `GcDateEx.cs` | `GrapeCity.Win.Editors.GcDate` | 1 | `UcDate` の中だけ。フォームには直接置いていない |
| `GcShortcut` | GrapeCity.Win.Editors.v60 | — | 5 | 置いてあるだけで、設定も参照もない(3.5) |
| `GcFunctionKey` | GrapeCity.Win.PlusPak.v60 | — | 0 | フォーム上には無い。型の参照だけが残っている(3.6) |

フォームに直接置いている Gc 系は、すべて comset の拡張クラス(`～Ex`)である。素の `GcTextBox` などを直接置いている箇所は無い。

Gc を内包するユーザーコントロールの配置数は次のとおり。実行時の Gc インスタンス数は、上の表の数にこれが加わる。

| ユーザーコントロール | 内包する Gc | 配置数 |
| --- | --- | ---: |
| `UcDate` | `GcDateEx` 1 つ | 30 |
| `UcTableComboBox` | `GcComboBoxEx` 1 つ | 41 |
| `UcFileSelect` | `GcTextBoxEx` 1 つ | 4 |

Gc 系の型と一緒に使っている GrapeCity の型:

| 型 | 使っている場所 |
| --- | --- |
| `DropDownButton` | `GcComboBoxEx` / `GcDateEx` のサイドボタン。`UcDate.Designer.cs`・`UcTableComboBox.Designer.cs` にも宣言がある |
| `FlatStyleEx` | `GcComboBoxEx` の Designer 設定(全 53 か所で `Flat`) |
| `CalendarType` / `YearMonthFormat` | `UcDate`、および帳票条件画面など 7 か所での年月カレンダー指定 |
| `FieldsEditMode` / `ButtonVisibility` | `UcDate` |
| `ScrollBarMode` / `TextChangingEventArgs` / `DropDownOpeningEventArgs` / `DropDownClosingEventArgs` / `ListItem` | `UcTableComboBox`、`GcComboBoxEx` |
| `AllowSpace` / `LengthUnit` / `CrLfMode` / `HighlightText` | 入力ルール(comset `GControlDBRule.cs`、`AppDbRule.cs`) |
| `FunctionKeyButton` / `CombinationKeyDisplayMode` | `AppFunctionKey.cs` |

### 1.2 参照アセンブリとライセンス

`FMKaiin.csproj` が参照している GrapeCity のアセンブリ:

| アセンブリ | バージョン |
| --- | --- |
| `GrapeCity.Win.Editors.v60` | 6.0.2011.630 |
| `GrapeCity.Win.PlusPak.v60` | 6.0.2010.518 |
| `GrapeCity.Framework.InputMan.v21` | 2.1.2010.303 |
| `GrapeCity.Framework.PlusPak.v21` | 2.1.2010.303 |

`FMKaiin/Properties/licenses.licx` には、`GcTextBox` / `GcComboBox` / `GcNumber` / `GcDate` / `GcShortcut` / `GcFunctionKey` と、comset の `GcTextBoxEx` / `GcComboBoxEx` / `GcNumberEx` / `GcDateEx` の行がある。

### 1.3 依存の構造

Gc への依存は、フォームのコードよりも共通ライブラリ comset(`_Library/_All/comset/comset/`。このリポジトリの外)に多い。

```text
フォーム (Form～.cs / Form～.Designer.cs)
 ├─ 直接配置 ─────────────────────────→ GcTextBoxEx / GcComboBoxEx / GcNumberEx
 ├─ ユーザーコントロール経由
 │    UcDate ─────────────────────────→ GcDateEx
 │    UcTableComboBox ────────────────→ GcComboBoxEx
 │    UcFileSelect ───────────────────→ GcTextBoxEx
 ├─ DB との値の受け渡し
 │    GControlDB (comset) ────────────→ 型を判定して Gc のメンバーを読み書き
 ├─ 入力ルール(桁数・文字種・IME)
 │    AppDbRule → GControlDBRule (comset) → 型を判定して Gc のプロパティを設定
 └─ グリッドのセル編集
      GGridDB (comset) ───────────────→ 型を判定して Gc へキー入力を渡す

GcTextBoxEx / GcComboBoxEx / GcNumberEx / GcDateEx (comset)
 └─ 継承 ─────────────────────────────→ GrapeCity.Win.Editors.Gc～
```

comset の中で Gc を参照しているファイルは、拡張クラス 4 つのほかに `GControlDB.cs`・`GControlDBRule.cs`・`GGridDB.cs` の 3 つである。

### 1.4 フォーム別の配置数

`UcDate` / `UcFileSelect` / `UcTableComboBox` の行は、そのユーザーコントロール自身が内包している Gc を表す。

| フォーム | GcTextBoxEx | GcComboBoxEx | GcNumberEx | GcDateEx | GcShortcut | UcDate | UcTableComboBox | UcFileSelect |
| --- | ---: | ---: | ---: | ---: | ---: | ---: | ---: | ---: |
| `FormCreateBankFile` |  |  |  |  |  | 2 | 1 |  |
| `FormCreateIfaxFile` |  |  |  |  |  |  | 1 |  |
| `FormHikiotoshi` |  |  | 2 |  |  | 1 |  |  |
| `FormHikiotoshi_DlgEntry` | 3 |  | 2 |  |  |  | 1 |  |
| `FormKaiin` | 2 |  |  |  |  | 1 |  |  |
| `FormKaiin_DlgChosei` | 2 | 1 | 1 |  |  | 1 | 1 |  |
| `FormKaiin_DlgEntry` | 59 | 13 |  |  |  | 10 | 9 |  |
| `FormKaiin_DlgEntry_Idoinfo` | 4 | 6 |  |  | 1 | 1 |  |  |
| `FormKaiin_DlgHistory` | 3 |  |  |  |  |  |  |  |
| `FormLogin` | 2 |  |  |  |  |  |  |  |
| `FormMain` | 1 |  |  |  | 1 |  |  |  |
| `FormMamisImport` |  |  |  |  |  |  |  | 1 |
| `FormMasterBankCode` | 2 |  |  |  |  |  |  |  |
| `FormMasterBankCode_DlgEntry` | 7 |  |  |  | 1 |  |  |  |
| `FormMasterBank_DlgEntry` | 8 | 1 |  |  | 1 |  |  |  |
| `FormMasterBasic` | 14 | 1 |  |  |  |  | 1 |  |
| `FormMasterImport` |  |  |  |  |  |  |  | 3 |
| `FormMasterIryoKikan` | 2 | 1 |  |  |  |  | 1 |  |
| `FormMasterIryoKikan_DlgEntry` | 17 | 2 |  |  | 1 |  | 3 |  |
| `FormMasterKaihi` | 2 | 2 |  |  |  |  |  |  |
| `FormMasterKaihi_DlgEntry` | 12 | 4 | 13 |  |  |  | 1 |  |
| `FormMasterShinryoka` | 2 |  |  |  |  |  |  |  |
| `FormMasterShinryoka_DlgEntry` | 2 |  |  |  |  |  |  |  |
| `FormMasterTantosha_DlgEntry` | 3 | 2 |  |  |  |  |  |  |
| `FormMasterTantosha_DlgPwd` | 3 |  |  |  |  |  |  |  |
| `FormOutChosyuMeisaiYear` |  | 2 |  |  |  | 1 | 4 |  |
| `FormOutExcelFurikomi` |  |  |  |  |  | 2 |  |  |
| `FormOutExcelFurikomiIrai` |  |  |  |  |  | 2 |  |  |
| `FormOutExcelKaihiShinsei` |  |  |  |  |  |  | 1 |  |
| `FormOutExcelNenkanChoshu` |  |  |  |  |  | 1 | 1 |  |
| `FormOutExcelNofusho` |  |  |  |  |  | 2 |  |  |
| `FormOutExcelShimingyoinKaihi` |  | 1 |  |  |  | 1 |  |  |
| `FormOutHikiotoshiKakunin` |  | 2 |  |  |  | 1 | 2 |  |
| `FormOutKaihiList` |  | 2 |  |  |  |  | 2 |  |
| `FormOutKaihiListBank` |  | 2 |  |  |  |  | 2 |  |
| `FormOutKaihiUchiwakeMonth` |  | 3 |  |  |  | 1 | 2 |  |
| `FormOutKaihiUchiwakeYear` |  | 3 |  |  |  | 1 | 2 |  |
| `FormOutKaihiYoteiYear` |  | 2 |  |  |  | 1 | 4 |  |
| `FormOutSeikyuMinyukin` |  | 2 |  |  |  | 1 | 2 |  |
| `FormSelectorBankCode` | 2 |  |  |  |  |  |  |  |
| `UcDate` |  |  |  | 1 |  |  |  |  |
| `UcFileSelect` | 1 |  |  |  |  |  |  |  |
| `UcTableComboBox` |  | 1 |  |  |  |  |  |  |
| **合計** | **153** | **53** | **18** | **1** | **5** | **30** | **41** | **4** |

Gc 系またはそれを内包するユーザーコントロールを置いているフォームは 40、ユーザーコントロールは 3 である。

---

## 2. 全フォーム共通の使われ方

### 2.1 Designer の設定

Designer では、見た目に関するプロパティだけを設定している。イベントの登録は Designer には 1 件も無く、すべてコード側か comset が行う。

| 型 | 全インスタンスに共通の設定 | 一部だけの設定 |
| --- | --- | --- |
| `GcTextBoxEx` | `BorderStyle = FixedSingle`、`SingleBorderColor = DarkGray`、`ExFocusHighlight = true`、`ImeStr = ""` | `DisabledBackColor = (235,240,255)` と `DisabledForeColor = Black`(54)、`ContentAlignment`(21)、`ImeMode = Off`(16)、`BackColor`(8)、`Font`(7)、`Enabled = false`(5)、`PasswordChar = '*'`(4)、`BorderStyle = None`(2) |
| `GcComboBoxEx` | `DropDownStyle = DropDownList`、`FlatStyle = Flat`、`ListHeaderPane.Visible = false`、`SingleBorderColor = DarkGray`、`ExFocusHighlight = true`、`ExDataSource` / `ExCompareContent` / `ExCompareValue = null` | `BorderStyle = None`(3) |
| `GcNumberEx` | `ExMaxValue = 9,999,999,999,999`、`ExMinValue = -9,999,999,999,999`、`ExCommaDelimiter = ","`、`ExAlternateBlank = true`、`AlternateText.Zero.Text = "0"`、`AlternateText.DisplayZero.Text = "0"`、`Spin.SpinOnKeys = false`、`SingleBorderColor = DarkGray`、`ExPrefix` / `ExSuffix = null` | `DisabledBackColor` / `DisabledForeColor`(6)、`BackColor`(3)、`Enabled = false`(3)、`BorderStyle = None`(2) |

`BorderStyle = None` のものは、グリッドのセルに重ねて表示する入力欄である(2.5)。

### 2.2 拡張クラス共通の機能

`GcTextBoxEx` / `GcComboBoxEx` / `GcNumberEx` / `GcDateEx` は、次の動作を同じ作りで持っている。

| 機能 | 内容 |
| --- | --- |
| フォーカス時の色変更 | フォーカスを受けると背景色と枠線色(`SingleBorderColor`)を変え、外れると元に戻す。`ReadOnly` のときは変えない。色は型ごとの static プロパティ `ExFocusColor` で持ち、テキスト・数値・日付は (173,233,196)、コンボは (210,241,210)。枠線色は (127,102,71)。アプリ側で `ExFocusColor` を設定している箇所は無い |
| F4 の抑止 | F4 キーを処理済みにして、Gc 標準のドロップダウン表示を止める |
| デザイナー対策 | フォームに置いたとき、サイドボタンや入力フィールドが Designer に重複して書き出されないよう、`SideButtons` / `Fields` / `DisplayFields` を `new` で隠している(`GcTextBoxEx` を除く) |

### 2.3 DB との値の受け渡し(GControlDB)

入力画面は、`GControlDB` にコントロールと DB 項目の組を登録し、`EndAdd(AppDbRule.Rule)` を呼ぶ。以後は comset が値の読み込み(`FetchAll`)と書き戻し(フォーカスが外れるときの `Validating`)を行う。登録の単位と件数は次のとおり。

| 登録クラス | 対象 | 件数 | 動作 | Gc への依存 |
| --- | --- | ---: | --- | --- |
| `GControlDBText` | `GcTextBoxEx` 1 つ | 57 | `Text` をそのまま読み書きする | 書き戻しで `(GcTextBoxEx)` に無条件でキャストし、`Multiline` を見る。他の型を渡すと例外になる |
| `GControlDBHyphenSplit` | `GcTextBoxEx` 2〜3 つ | 延べ 32 コントロール(11 項目) | 電話番号・郵便番号を `-` で分けて各欄に入れ、書き戻しでつなぐ | `Text` だけを使う |
| `GControlDBRuby` | `GcTextBoxEx` 2 つ(名称・ふりがな) | 18 コントロール(9 項目) | 名称を書き戻すとき、入力時の読みをふりがな欄へ追記する | `GcTextBoxEx.ExImeStr`(3.1)。他の型ではエラーログを出して何もしない |
| `GControlDBPostAddr` | `GcTextBoxEx` 3 つ(郵便番号 2 欄・住所) | 9 コントロール(3 項目) | 郵便番号の入力で住所を自動入力する | `Text` だけを使う |
| `GControlDBNumber` | `GcNumberEx` / `GcTextBoxEx` | 14 / 3 | 数値を読み書きする | `GcNumber` のときは `Value`(null 可)、それ以外は `Text` |
| `GControlDBCombo` | `GcComboBoxEx` | 29(直接)+ 16(`UcTableComboBox.ComboBox`) | 内部値で選択し、内部値を書き戻す | `ExSetSelectedIndexByValue` / `ExGetValue`。`GcComboBoxEx` 以外は「未対応」としてエラーログを出す |
| `GControlDBDate` | `GcDateEx`(`UcDate.UcDateCtl`) | 12 | 日付を読み書きする | `GcDate.Value`(null 可)。`GcDate` 以外は「未対応」 |
| `GControlDBCheckBox` | `YcCheckBoxEx` | — | Gc ではない | — |

`GControlDB` 本体が Gc の型を見ている処理:

| 処理 | 内容 |
| --- | --- |
| `EndAdd` | 登録した全コントロールを `Enabled = true`・`ReadOnly = false` にする(Designer の設定は上書きされる)。`GcTextBox` は `DropDown.AllowDrop = false` にする |
| `ReadOnly` / `ReadOnlyAll` | `EditBase`(テキスト・数値・日付の基底)と `GcComboBox` の `ReadOnly` を切り替え、背景色を `SystemColors.Control` にする。それ以外の型は「ReadOnly プロパティが実装されていません」とエラーログを出す |
| `ClearAll` | `GcTextBoxEx` / `GcNumberEx` / `GcDateEx` は `Text = ""`、`GcComboBoxEx` は先頭項目を選ぶ。それ以外の型は何もしない |
| フォーカス取得時 | `HighlightText` が無効のコントロールは、カーソルを末尾へ移す(`EditBase.Select`) |
| イベント登録 | 登録した全コントロールの `Enter` と `Validating` |

フォーム側が呼んでいる `GControlDB` のメソッドは `Add`(160)、`EndAdd`(17)、`FetchAll`(17)、`UpdateByControl`(16)、`ClearAll`(14)、`Update`(7)、`FetchByField`(1)、`FetchByControl`(1)。

### 2.4 入力ルール(AppDbRule / GControlDBRule)

桁数・入力できる文字・IME の状態・文字位置は、Designer ではなく `AppDbRule.cs` に DB 項目ごとのルールとして定義してある。適用の経路は 2 つある。

| 経路 | 対象 | 件数 |
| --- | --- | --- |
| `GControlDB.EndAdd(AppDbRule.Rule)` | DB にバインドしたコントロール。バインド先の項目名でルールを探す | `GcTextBoxEx` 108、`GcNumberEx` 14、`UcDate` 12 |
| `AppDbRule.SetControlByRule(コントロール, 項目名)` | 検索欄など、DB にバインドしていないコントロール | `GcTextBoxEx` 13、`UcDate` 2 |

ルールの適用(`GControlDBRuleBase.setControlByRule`)は、コントロールの型ごとに次のプロパティを設定する。Gc 以外の型は「未対応のコントロールです」とエラーログを出して何も設定しない。

| 型 | 設定するプロパティ |
| --- | --- |
| 共通(`EditBase`) | `ImeMode`、`ContentAlignment` |
| `GcTextBox` | `MaxLength`、`MaxLengthUnit`(文字単位)、`Format`(文字種)、`AllowSpace`、`HighlightText`(フォーカス時の全選択)、`AcceptsCrLf`(貼り付け時に改行を除く) |
| `GcNumber` | `Clear()`、`ExMaxValue`(桁数から 9…9 を作る)、`ExMinValue`(その負数)、`ExCommaDelimiter`、`ExAlternateBlank`、`HighlightText`、`Spin.Increment = 0` |
| `GcDate` | `PromptChar = ' '`、`HighlightText`(全体 / なし) |
| `GcComboBox` | `MaxLength`、`MaxLengthUnit`、`Format`、`AllowSpace`、`HighlightText`、`ImeMode = Disable`。ただし、このアプリではコンボの項目にルールを登録していないので、実際には適用されていない |

ルールの種類と既定値(comset `GControlDBRule.cs`):

| ルール | 桁数 | `Format` | 空白 | IME | 文字位置 | 備考 |
| --- | --- | --- | --- | --- | --- | --- |
| `Text(n)` | n 文字 | 指定なし | 全角・半角とも可 | ひらがな | 左下 | |
| `Number(n,m)` | 整数 n 桁・小数 m 桁 | `9` | 不可 | 無効 | 右下 | 桁区切りなし。`GcTextBoxEx` に適用した場合は「数字だけ n 文字」になる |
| `Currency(n,m)` | 整数 n 桁・小数 m 桁 | `9` | 不可 | 無効 | 右下 | 桁区切り `,` |
| `HyphenSplit(a,b,c)` | 欄ごとに a・b・c 文字 | `9` | 不可 | 無効 | 右下 | |
| `Ruby(a,b)` | 名称 a 文字、ふりがな b 文字 | 名称 `^@`、ふりがな `H` | 名称は両方可、ふりがなは半角だけ | 名称ひらがな、ふりがな半角カタカナ | 左下 | |
| `PostAddr` | 3・4・30(住所)・50(住所ふりがな) | `9`・`9`・`^@`・`H^@` | 郵便番号は不可 | 郵便番号は無効、住所はひらがな | 郵便番号は右下 | |
| `Date` | — | — | — | 無効 | 中央下 | |

すべてのルールで、フォーカス時は全選択(`HighlightText = true`)、改行は除去(`AcceptsCrLf = Filter`)。

`Format` は InputMan の書式キーワードである。ソースのコメントから読み取れる意図は次のとおり。キーワードの正確な定義は、InputMan のヘルプで確認していない。

| 値 | コメントに書かれている意図 |
| --- | --- |
| `9` | 数字だけ |
| `H` | 半角カナの欄(IME を半角カタカナにして使う) |
| `^@` | 半角記号を入力させない(検索で特殊文字として扱われるため) |

`AppDbRule.cs` が上の既定値を変えているルール:

| 名前 | 内容 | 使っている項目 |
| --- | --- | --- |
| `code` / `code3` / `code4` / `code7` / `code10` | `Number(6,0)` など。`code` だけ文字位置が左下 | 会員コード、担当者コード、銀行コード(4)、支店コード(3)、口座番号(7)、委託者コード(10) |
| `kana20` / `kana30` | `Text(n)` に `Format = H`、空白は半角だけ、IME は半角カタカナ | 口座名義、銀行名・支店名のふりがな |
| `eisu20` | `Text(20)` で IME オフ | パスワード |
| `hankaku` / `numStr13` | `Text(80)` / `Text(13)` で IME 無効 | メールアドレス、法人番号 |
| `curr` | `Currency(7,0)` で空欄可 | 引落金額、調整金額 |
| `memo20` | `Text(20)` で文字位置が左上 | 引落・調整のメモ |
| `post` | `PostAddr` で住所を 30 文字 | 郵便番号・住所 |

項目ごとに実際に適用されるルールは、7 章の表の「入力ルール」列に載せた。

### 2.5 グリッドのセル編集(GGridDB)

グリッド(`C1TrueDBGrid`)のセルに入力欄を重ねて表示する画面がある。`GGridDB.SetFocusControlInGrid(コントロール)` で列と入力欄を結び付ける。対象は 15 コントロール(`GcTextBoxEx` 2、`GcNumberEx` 2、`GcComboBoxEx` 3、`UcTableComboBox` 7、`UcDate` 1)で、どれも枠線なし(`BorderStyle = None`)にしてある。

グリッド上でキーを押すと、comset がその列の入力欄へフォーカスを移し、押したキーを入力欄へ渡す。このとき型を見て処理を分けている。

| 入力欄の型 | 動作 |
| --- | --- |
| `GcNumber` | キー入力を受け取れないため、`Value` / `Text` と `SelectionStart` を直接設定する(`-` は `"-0"`、`.` は 0、数字はその値) |
| `GcTextBox` | `Format == "9"` なら `Text` に 1 文字を入れてカーソルを末尾へ。それ以外は `SendKeys` で送る(IME がオンのときは F13 を前置) |
| `GcComboBox` | Delete / BackSpace でセルの値を消さない |

### 2.6 FormFrame のキー処理

`FormFrame.CheckEnabledFormKeyPreview` が、フォーカス中のコントロールの型を見て、フォームのキー処理(Enter で次の項目へ、など)を止めている。

| キー | 条件 | 動作 |
| --- | --- | --- |
| Enter | `GcComboBoxEx` / `GcDateEx` でドロップダウンが開いている(`DroppedDown`) | フォームでは処理しない(ドロップダウンの確定に使う) |
| ↑ / ↓ | `GcComboBoxEx` / `GcDateEx` にフォーカスがある | フォームでは処理しない(項目・日付の変更に使う) |

`FormFrame.updateCurrentControlValue` は、フォーカス中のコントロールが `UcTableComboBox` / `UcDate` のとき、内側の Gc コントロールを取り出して `GControlDB.UpdateByControl` に渡す。

---

## 3. 型ごとの仕様

### 3.1 GcTextBoxEx(153)

**拡張クラスが追加している機能**

| メンバー | 内容 | 使用状況 |
| --- | --- | --- |
| `ImeStr` / `ExImeStr` | IME で確定した文字列の「読み」をためておく。`WM_IME_COMPOSITION` を拾い、`ImmGetCompositionString` で読みを取得する。`ExImeStr` は取得と同時にクリアする。テキストが空になるとクリアする | `GControlDBRuby` がふりがなの自動入力に使う(9 項目) |
| `ExCompositionEvent` / `IMECompositionStart` / `IMECompositionEnd` | 変換確定・開始・終了のイベント | 使っていない |
| `SetEnterHighlight` / `SetLeaveHighlight` | フォーカス時の色変更(2.2) | 全インスタンス |
| `ExFocusHighlight` | 色変更の有効・無効 | プロパティはあるが、色変更の処理が参照していない(6 章) |

**用途別の内訳**(フォームに置いた 152)

| 用途 | 数 |
| --- | ---: |
| DB にバインドした入力欄(2.3) | 116 |
| バインドせず、ルールだけ適用した入力欄(一覧画面の検索欄、ログイン、パスワード変更) | 13 |
| どちらも無い欄(会員名などの表示専用、銀行名の表示など) | 23 |

**コード側で使っているメンバー**

| メンバー | 用途 |
| --- | --- |
| `Text`(取得 73、設定 37)、`TextLength`、`Text.Length` | 値の読み書き、空欄の判定 |
| `Enabled`(49) | 入力可否の切り替え |
| `Select()`(24)、`SelectAll()`(4) | エラー時などのフォーカス移動 |
| `Clear()`(4)、`ResetText()`(8) | 検索条件のクリア |
| `TextChanged`(22 コントロール) | 検索欄の絞り込み、銀行コードから銀行名の取得 |
| `MouseDoubleClick`(10 コントロール) | 銀行コード選択画面の表示 |
| `Name` | イベントの発生元の判定(`FormKaiin_DlgEntry.fetchBank` など) |
| `AllowDrop` / `DragEnter` / `DragDrop` / `Click` / `Leave` / `Width` | `UcFileSelect` の中 |

コード側が使っているのは、標準の `TextBox` にもあるメンバーだけである。Gc 固有の機能への依存は、Designer の設定(2.1)、入力ルール(2.4)、ふりがなの取得、フォーカス時の色変更にある。

引数や配列の型として `GcTextBoxEx` を書いている箇所: `FormKaiin_DlgEntry.cs`(`fetchBank` / `fetchShiten` / `showBankCode` の引数とキャスト)、`FormMasterBasic.cs`・`FormMasterShinryoka_DlgEntry.cs`・`FormMasterTantosha_DlgEntry.cs`・`FormMasterTantosha_DlgPwd.cs`(空欄チェック用の配列)、`UcFileSelect.cs`(`UcTxtCtl` プロパティ)。

### 3.2 GcComboBoxEx(53)

**拡張クラスが追加している機能**

| メンバー | 内容 | 使用状況 |
| --- | --- | --- |
| `ExBeginUpdate` / `ExClearItem` / `ExAddItem(表示文字列, 内部値)` / `ExEndUpdate` | 項目を「表示文字列と内部値の組」で登録する。`ExEndUpdate` で実際のリストへ反映する | フォームの 52 すべて。うち 40 は `AppCombo.SetComboBox`(区分の辞書から作る)経由、12 は直接 |
| `ExGetValue()` / `ExGetValue(index)` | 選択中の項目の内部値を返す | `GControlDBCombo`、帳票条件の取得など(フォームでは 21 コントロール) |
| `ExSetSelectedIndexByValue(値)` / `ExSetSelectedIndexByContent(文字列)` | 内部値・表示文字列から選択する。見つからなければ未選択にして `Text` を空にする | `GControlDBCombo`、初期値の設定 |
| `ExGetContent()` | 選択中の表示文字列を返す | 使っていない |
| `ExDataSource` / `ExCompareValue` / `ExCompareContent` | `DataTable` / `DataView` を一覧のデータ元にし、内部値・表示文字列に使う列名を指定する | `UcTableComboBox` だけ |
| `ExOpenBySpaceKey`(既定 true) | Space キーでドロップダウンを開閉する | 既定値のまま |
| `ExItems` | 登録した項目の一覧 | 使っていない |
| アイコン付き項目(`ExAddItem(icon, …)`) | 項目に画像を付ける | 使っていない |

コンストラクタで、ドロップダウンボタンを 1 つ追加し、`ListHeaderPane.Visible = false`、`DropDown.AllowResize = false`、`DropDownStyle = DropDownList` にしている。

**用途別の内訳**(フォームに置いた 52)

| 用途 | 数 |
| --- | ---: |
| DB にバインドした選択欄(区分、支払方法、口座種別など) | 29 |
| バインドしない選択欄(帳票条件の「〜から」「〜まで」、一覧の絞り込み) | 23 |

フォームに直接置いたものは、すべて「一覧から選ぶだけ」(`DropDownList`)で、1 列表示である。

**コード側で使っているメンバー**(フォーム)

| メンバー | 用途 |
| --- | --- |
| `Ex～` 各種 | 上の表のとおり |
| `SelectedIndex`(取得 38、設定 8) | 選択の判定(退会事由が「その他」のとき入力欄を有効にする、など) |
| `SelectedIndexChanged`(10 コントロール)、`SelectedValueChanged`(1) | 選択に応じた入力欄の有効・無効、一覧の絞り込み |
| `DroppedDown` | `FormFrame` のキー処理(2.6) |
| `Visible` | `FormMasterTantosha_DlgEntry.iShinryojo` を非表示にしている |

`UcTableComboBox` の中での使い方は 4.2 に分けて書く。

### 3.3 GcNumberEx(18)

**拡張クラスが追加している機能**

| メンバー | 内容 |
| --- | --- |
| `ExMaxValue` / `ExMinValue` | 入力できる最大値・最小値。この 2 つから入力書式(`###,##0.00` の形)を組み立て、範囲外になる入力は直前の値へ戻す |
| `ExCommaDelimiter` | 桁区切りの文字。空文字なら区切らない |
| `ExAlternateBlank` | 空欄を許すか。小数を持つ書式では自動で false になる |
| `ExPrefix` / `ExSuffix` | 表示時に前後へ付ける文字列。使っていない(全インスタンスで null) |
| `ExGetCurrencyField`(static) | 最大値・最小値から書式文字列を作る |
| キー処理 | カーソルキーでの増減を止める。小数を持たない書式で `.` を押すと値を 1000 倍する。空欄可のとき、Delete / BackSpace で 1 桁の値や全選択を消すと空欄(null)にする。`-` を押すと `-0` にする |

**用途別の内訳**

| 用途 | 数 | 場所 |
| --- | ---: | --- |
| DB にバインドした金額入力(`Currency(7,0)`) | 14 | `FormMasterKaihi_DlgEntry` の月額 12 か月分、`FormHikiotoshi_DlgEntry.iCost`、`FormKaiin_DlgChosei.iCost` |
| 合計などの表示専用(`Enabled = false`) | 4 | `FormHikiotoshi` の合計金額・件数、`FormHikiotoshi_DlgEntry.iTotalCost`、`FormMasterKaihi_DlgEntry.oCostAll` |

**コード側で使っているメンバー**: `Value`(`decimal?` の設定)、`Text`(書式化した文字列の設定)、`Enabled`、`ValueChanged`(金額の変更で合計を再計算。`FormMasterKaihi_DlgEntry` は 12 個を配列で回して登録している)。

### 3.4 GcDateEx(1。UcDate の中)

フォームからは必ず `UcDate` 経由で使う。`UcDate` の仕様は 4.1。

**拡張クラスが追加している機能**

| メンバー | 内容 |
| --- | --- |
| `ExMinDate` / `ExMaxDate` | 入力できる日付の範囲。カレンダーの範囲(`DropDownCalendar.MinDate` / `MaxDate`)にも反映する。フォーカスが外れるとき、範囲外の値を範囲の端へ丸める |
| キー処理 | Space / ↑ / ↓ でカレンダーを開く |
| `OnDropDownClosed` | カレンダーを閉じたら全選択にする |

コンストラクタで、入力書式と表示書式を `yyyy/MM/dd` にし、ドロップダウンボタンを 1 つ追加している(書式は `UcDate` が上書きする)。

### 3.5 GcShortcut(5)

`FormMain`、`FormKaiin_DlgEntry_Idoinfo`、`FormMasterBankCode_DlgEntry`、`FormMasterBank_DlgEntry`、`FormMasterIryoKikan_DlgEntry` の Designer に `gcShortcut1` があり、`new GcShortcut(this.components)` で生成している。ショートカットの設定(`SetShortcuts` など)は 1 件も無く、コードからの参照も無い。

### 3.6 GcFunctionKey(0)

画面下部のファンクションキーは、全画面が `FunctionKeyBar` + `FunctionKeyBinder` に置き換わっている(仕様は [ファンクションキー.md](../spec/ファンクションキー.md))。フォーム上に `GcFunctionKey` は無い。次の参照だけが残っている。

| 場所 | 内容 |
| --- | --- |
| `AppFunctionKey.cs` | `GcFunctionKey` を包むクラス。`FunctionKeyButtons`、`CombinationKeyDisplayMode` などを使う。インスタンスを作っている箇所は無い |
| `FormFrame.cs` | `protected AppFunctionKey appFuncKey = null;` と、null でないときだけ動く処理(3 か所)。代入している箇所が無いので、常に null |
| `licenses.licx` | `GrapeCity.Win.Bars.GcFunctionKey` の行 |

---

## 4. Gc を内包するユーザーコントロール

### 4.1 UcDate(30 か所)

`GcDateEx` を 1 つ内包し、和暦・西暦の切り替えと表示形式の指定をまとめたもの。

**公開しているメンバー**

| メンバー | 内容 |
| --- | --- |
| `UcValue`(`DateTime?`) | 値。設定時に範囲外なら端へ丸める。未入力は null |
| `UcFormat` | 表示形式。`YYYYMMDD`(22)、`YYYYMM`(6)、`YYYY`(2)、`YYYY_MM_DD`(0)。コードで実行時に切り替えている箇所もある |
| `UcValueChanged` | 値が変わったときのイベント(5 か所で使用) |
| `UcDateCtl` | 内側の `GcDateEx` を返す。`GControlDBDate` への登録(12)、ルールの適用(2)、カレンダーの種類の指定(7)に使っている |
| `UcSideButton` | カレンダーボタンを表示するか(非表示は 2 か所) |
| `UcReadOnly` / `UcEnabled` / `UcBackColor` / `UcBorderStyle` / `SingleBorderColor` / `DisabledForeColor` / `DisabledBackColor` / `Font` | 内側のコントロールへそのまま渡す |
| `UcCalenderMinDate` / `UcCalenderMaxDate` | カレンダーで選べる範囲。Designer では全 30 か所で 1868/01/01 〜 2100/12/31 |
| `SetKoyomiSeirekiOn`(static)/ `ChangedKoyomiSeirekiOn` | 和暦・西暦の切り替え。static の設定は全 `UcDate` に効く |

**表示形式ごとの書式**(`changeFormat`)

| `UcFormat` | 和暦: 入力 / 表示 | 西暦: 入力 / 表示 | カレンダー |
| --- | --- | --- | --- |
| `YYYYMMDD` | `g. ee/MM/dd` / `ggg ee年MM月dd日` | `yyyy/MM/dd` / `yyyy年MM月dd日` | 日付 |
| `YYYY_MM_DD` | `g. ee/MM/dd` / `ggg ee/MM/dd` | `yyyy/MM/dd` / `yyyy/MM/dd` | 日付 |
| `YYYYMM` | `g. ee/MM` / `ggg ee年MM月` | `yyyy/MM` / `yyyy年MM月` | 年月 |
| `YYYY` | `g．ee年` / `ggg ee年` | `yyyy` / `yyyy年` | 年月 |

フォーカスがあるときは入力用の書式、無いときは表示用の書式になる(`Fields` と `DisplayFields`)。

**内側の GcDateEx に対して使っている Gc 固有の機能**

| 機能 | 設定 |
| --- | --- |
| 入力書式・表示書式 | `Fields` / `DisplayFields` を書式文字列で組み立てる。和暦の年号(`g` / `ggg`)と和暦年(`ee`)を使う |
| 入力方式 | `FieldsEditMode = Unfixed`(`01/01` でも `1/1` でも入力できる) |
| 未入力部分の補完 | `RecommendedValue = 今年の 1 月 1 日`(年月だけ入力したときの日などを補う) |
| 入力範囲 | `ExMinDate = 1868/01/01`、`ExMaxDate = 2100/12/31` |
| カレンダー | `DropDownCalendar.CalendarType`(日付 / 年月)、`HeaderFormat`、`YearMonthFormat`、`UseHeaderFormat = true`、`MinDate` / `MaxDate` |
| サイドボタン | `SideButtons[0].Visible` で表示・非表示 |
| ホイール | `Spin.AllowSpin = false`(ホイールで日付を変えない) |
| 値 | `Value`(`DateTime?`)、`ValueChanged` |
| その他 | `ContentAlignment = BottomCenter`、`SingleBorderColor`、`DisabledForeColor` / `DisabledBackColor`、`ReadOnly`、フォーカス時に `SelectAll()` |

フォーム側が `UcDateCtl` 経由で直接触っているのは `DropDownCalendar.CalendarType = YearMonth` だけである(`FormKaiin_DlgEntry` の 3 か所、`FormOutHikiotoshiKakunin`、`FormOutKaihiUchiwakeMonth`、`FormOutKaihiYoteiYear`、`FormOutSeikyuMinyukin`)。

### 4.2 UcTableComboBox(41 か所)

`GcComboBoxEx` を 1 つ内包し、DB のテーブル(`DBView`)を複数列の一覧として表示して、入力した文字で絞り込めるようにしたもの。コンボの右に情報アイコン(`IconBox`)を持つ。Gc 固有の機能への依存がもっとも多い。

設定は主に `AppTableCombo.cs` にまとめてあり、マスターごとのメソッドで `DBView`・`CompareValue`・`DropDownSize`・`SetColumn`・`TextSubItemIndex`・`SelectedIndexNullLeave`・`RowFilter` を設定している。

**公開しているメンバー**

| メンバー | 内容 |
| --- | --- |
| `DBView` | 一覧のデータ元。設定すると全列を非表示にする |
| `SetColumn(項目名, 見出し, 幅[, 文字位置])` | 一覧に表示する列を指定する(29 回呼ばれている) |
| `TextSubItemIndex` | 選択したとき入力欄に表示する列。その列が空の行は選べないようにフィルタを掛ける |
| `CompareValue` | 内部値に使う列名 |
| `Value` | 選択中の内部値 |
| `Text` | 表示文字列。設定すると該当行を探して選択する |
| `RowFilter` / `Sort` | 一覧の絞り込み・並び順 |
| `Find` / `Search(項目名, 値)` | 該当行を探して選択する |
| `DropDownSize` | 一覧の大きさ |
| `SelectedIndexNullLeave` | 未選択のままフォーカスが外れたときに選ぶ行。-1 なら入力を消す |
| `ComboBox` | 内側の `GcComboBoxEx` を返す。`GControlDBCombo` への登録(16)、`ImeMode` の設定(13)、`ExSetSelectedIndexByValue`(3)に使っている |
| `EnterImeMode` / `HighlightText` / `ContentAlignment` / `ComboBorderStyle` / `DiabledBackColor` / `DisabledForeColor` / `IconVisible` | 見た目と IME |
| `TextChanged` / `SelectIndexChanged` / `ValidateComboBox` | イベント |
| `BeginUpdate` / `EndUpdate` | 設定の開始・終了 |

**動作**

| 場面 | 動作 |
| --- | --- |
| 文字の入力 | 表示中の全列を対象に絞り込む。1 文字なら前方一致、2 文字以上なら部分一致(かなの種類を区別しない `DBQuery.CreateLikeKanaString`)。数値の列は前方一致。該当があれば一覧を開き、1 件だけならその行を選ぶ。該当が無ければ一覧を閉じる |
| ↑ / ↓ / PageUp / PageDown | 一覧を開いて行を移動する |
| 一覧を閉じたとき | 選択行の表示列の値を入力欄に入れ、絞り込みを解除して同じ行を選び直し、全選択にする。`ValidateComboBox` を発生させる |
| フォーカスが外れたとき | 未選択なら `SelectedIndexNullLeave` の行を選ぶか、入力を消す |
| ホイール | 一覧が閉じている間はコンボに渡さない(選択値を変えない)。マウスの下が別のコントロールならそちらへ送る |
| 半角記号 | 入力させない(`Format = "^@"`。`%` などが絞り込みの式に入らないようにするため) |

**内側の GcComboBoxEx に対して使っている Gc 固有の機能**

| 機能 | メンバー |
| --- | --- |
| 複数列の一覧と列見出し | `ListColumns[i].Visible` / `Width` / `DataPropertyName` / `Header.Text` / `Header.ContentAlignment` / `Header.ForeColor` / `DefaultSubItem.ContentAlignment`、`ListHeaderPane.Visible = true` |
| 入力欄に表示する列の指定 | `TextSubItemIndex`、`SelectedItem.SubItems[i].Value` |
| データ元 | `ExDataSource`(`DataView`)、`ExCompareValue` |
| 一覧の開閉の制御 | `DroppedDown`、`DropDownOpening`(キャンセル可)、`DropDownOpened`、`DropDownClosing`、`DropDownClosed` |
| 一覧の大きさ | `DropDown.Size`、`DropDown.AllowResize = true`(利用者が大きさを変えられる)、`DropDown.ShowShadow`、`ScrollBarMode = Fixed` |
| 入力中の文字の制御 | `TextChanging`(キャンセル可。絞り込みや一覧の開閉でテキストが書き換わるのを止めるために多用している)、`Format`、`SelectionStart` |
| 自動補完の停止 | `AutoSelect = false`、`AutoCompleteMode = None`、`AutoCompleteSource = None` |
| その他 | `DropDownStyle = DropDown`(文字入力可)、`ImeMode`、`HighlightText`、`ContentAlignment`、`DisabledBackColor` / `DisabledForeColor`、`BorderStyle`、`BeginUpdate` / `EndUpdate`、`SelectedIndex`、`Items.Count` |

ソースには、Gc のコンボの挙動に合わせた回避処理がいくつかある(未選択のまま ↑↓ を押すと落ちるため選択をいったん 0 にする、`EndUpdate` で `SelectedIndex` が 0 になるので戻す、など)。

### 4.3 UcFileSelect(4 か所)

`GcTextBoxEx` 1 つと参照ボタンを持つ、ファイル選択用の部品。`FormMamisImport` に 1、`FormMasterImport` に 3。

| 項目 | 内容 |
| --- | --- |
| 公開メンバー | `UcText`(パスの文字列)、`UcTxtCtl`(内側の `GcTextBoxEx`)、`FileDialogType`、`FileFilter`、`FileFilterIndex` |
| 動作 | 参照ボタンでダイアログを開き、選んだパスをテキスト欄に入れる。テキスト欄は Designer で `Enabled = false` になっており、コードで有効にしている箇所は無い(パスの表示欄として使っている) |
| 登録だけされている処理 | テキスト欄へのファイルのドラッグ&ドロップ、クリック時の全選択。テキスト欄が無効なので、実際に動くかは確認していない |
| Gc 固有の機能 | `DisabledBackColor` / `DisabledForeColor`(無効時の色)と `SingleBorderColor`。コードが使っているのは `Text`、`Width`、`AllowDrop`、`DragEnter`、`DragDrop`、`Click`、`Leave`、`Select()`、`SelectAll()` で、すべて標準の `TextBox` にもある |

フォームから `UcTxtCtl` を使っている箇所は無い。

---

## 5. 標準コントロールに同等の機能が無いもの

現在使っている機能のうち、標準の `TextBox` / `ComboBox` / `NumericUpDown` / `DateTimePicker` にそのまま対応するプロパティが無いものを挙げる。置き換え時に、自前の実装か仕様の変更が必要になる箇所である。

| # | 機能 | 現在の実現方法 | 使用範囲 |
| ---: | --- | --- | --- |
| 1 | 入力できる文字種の制限 | `GcTextBox.Format`(`9` / `H` / `^@`)、`AllowSpace` | ルールを適用した `GcTextBoxEx` 121、`UcTableComboBox` 41 |
| 2 | 貼り付け時の改行の除去 | `AcceptsCrLf = Filter` | 同上 |
| 3 | フォーカス時の全選択 / 末尾へのカーソル移動 | `HighlightText` | ルールを適用した全入力欄 |
| 4 | 文字の縦位置を含む配置 | `ContentAlignment`(左下・右下・左上など) | 全入力欄 |
| 5 | 枠線の色 | `SingleBorderColor` | 全インスタンス |
| 6 | 無効時の背景色・文字色 | `DisabledBackColor` / `DisabledForeColor` | `GcTextBoxEx` 54、`GcNumberEx` 6、`UcDate`、`UcTableComboBox` |
| 7 | フォーカス時の背景色・枠線色の変更 | 拡張クラス(2.2) | 全インスタンス |
| 8 | IME の読みの取得(ふりがなの自動入力) | `GcTextBoxEx.ExImeStr`。Win32 の IMM を直接呼んでおり、Gc の機能は使っていない | `GControlDBRuby` の 9 項目 |
| 9 | 表示文字列と内部値の組での項目管理 | `GcComboBoxEx.Ex～` | `GcComboBoxEx` 52 |
| 10 | コンボの `ReadOnly` | `GcComboBox.ReadOnly` | `GControlDB.ReadOnly` |
| 11 | 複数列・見出し付きのドロップダウン一覧、入力による絞り込み、開閉のキャンセル、テキスト変更のキャンセル | 4.2 | `UcTableComboBox` 41 |
| 12 | 桁区切り付きの数値入力、空欄(null)、桁数からの範囲設定、範囲外入力の取り消し | `GcNumberEx`(3.3) | `GcNumberEx` 18 |
| 13 | 和暦での日付の入力と表示、未入力(null)、年月だけ・年だけの入力、入力時と表示時で異なる書式、年月カレンダー | `UcDate`(4.1) | `UcDate` 30 |

これとは別に、comset が Gc の型を前提にしている箇所がある(2.3〜2.5)。コントロールを差し替えると、次の処理が動かなくなるか、例外になる。

| 場所 | 現在の動作 |
| --- | --- |
| `GControlDBText.Update` | `(GcTextBoxEx)` へのキャストで例外 |
| `GControlDBCombo` / `GControlDBDate` | 値の読み書きをしない(エラーログだけ) |
| `GControlDBRuby` | ふりがなを入れない(エラーログだけ) |
| `GControlDBRuleBase.setControlByRule` | ルールを適用しない(エラーログだけ) |
| `GControlDB.ReadOnly` / `ClearAll` | 読み取り専用にしない・クリアしない |
| `GControlDB.ctl_Enter` | カーソルの末尾移動をしない |
| `GGridDB` のキー入力の受け渡し | 数値欄・数字だけの欄への直接設定をしない(`SendKeys` 側の処理になる) |
| `FormFrame.CheckEnabledFormKeyPreview` | コンボ・日付の Enter / ↑↓ をフォームが処理してしまう |

comset は他のアプリと共用のライブラリで、このリポジトリの外にある。

---

## 6. 調査で気づいた点

置き換えの前に確認しておくとよい点。いずれも現状の事実で、この調査では変更していない。

| # | 内容 |
| ---: | --- |
| 1 | `ExFocusHighlight` は 4 つの拡張クラスすべてにあり、Designer でも全インスタンスに `true` を設定しているが、色変更の処理(`OnEnter` / `OnLeave`)はこの値を見ていない。false にしても色は変わる |
| 2 | `GcShortcut` は 5 フォームに置いてあるが、何も設定していない(3.5) |
| 3 | `GcFunctionKey` はフォームから無くなっているが、`AppFunctionKey.cs`、`FormFrame.appFuncKey`、`licenses.licx` に参照が残っている(3.6) |
| 4 | `FormMasterBasic.cs` で、`t_basic.FBAS_Tel2` と `t_basic.FBAS_Fax` の両方を同じ 3 つの欄(`iTel2_1`〜`iTel2_3`)にバインドしている。読み込み時は後から登録した FAX の値が表示され、書き戻し時は両方の項目に同じ値が入る。意図したものかは未確認 |
| 5 | DB にバインドしているのに入力ルールが登録されていない `GcTextBoxEx` が 8 つある。桁数の制限が掛からない。`FormKaiin_DlgEntry` の 6 つ(`iIryoKaisetsushutai`、`iIryoAddr1`、`iIryoShisetsuNameKana`、`iIryoHeisetsuName`、`iIryoByoshoUmu`、`iIryoKumi`)は入力不可にしている表示欄。`FormMasterBankCode_DlgEntry.iFullNameBank` と `FormMasterIryoKikan_DlgEntry.iHeisetsu` は入力できる欄 |
| 6 | コンボ(`GcComboBoxEx` 29、`UcTableComboBox` 16)のバインド先の項目には、入力ルールが 1 つも登録されていない。コンボ用のルール適用処理(2.4)は使われていない |
| 7 | `UcTableComboBox.cs` は Shift-JIS で保存されている(ほかに `UcInputControl_Super.cs` など 15 ファイル)。編集するときは文字コードを変えないこと |
| 8 | `GControlDB.EndAdd` は、登録したコントロールを一律に `Enabled = true`・`ReadOnly = false` にする。Designer で `Enabled = false` にしても、バインドしたコントロールには効かない |

---

## 7. 付録: フォーム別の全インスタンス一覧

Gc 系のコントロールと、Gc を内包するユーザーコントロール(`UcDate` / `UcTableComboBox` / `UcFileSelect`)を、フォームごとに Designer の宣言順で並べた。

**表の見方**

| 列 | 内容 |
| --- | --- |
| DB バインド | `GControlDB` への登録。`Text(項目名)` は `GControlDBText` の意味(2.3)。`UcDate` / `UcTableComboBox` は、内側のコントロール(`UcDateCtl` / `ComboBox`)を登録している |
| 入力ルール | 実際に適用されるルール(2.4)。`※直接適用` は `AppDbRule.SetControlByRule` で適用しているもの。`なし` は、バインドしているがルールが登録されていないもの。`—` はバインドもルールの適用も無いもの |
| Designer での個別設定 | 2.1 の共通設定と異なるものだけ。位置・大きさ・タブ順は省いた |
| コードでの扱い | フォームのコードがコントロール名で直接使っているメンバー。`=` は設定、`+=` はイベントの登録、`()` はメソッドの呼び出し。`AppCombo.SetComboBox[辞書]` は区分の辞書からの項目作成、`SetFocusControlInGrid` はグリッドのセル編集用(2.5) |

機械的に集計しているので、配列に入れて回している箇所(`FormMasterKaihi_DlgEntry` の `iCosts` への `ValueChanged` の登録、空欄チェック用の配列)と、`sender` をキャストして使っている箇所は「コードでの扱い」に出ない。

### FormCreateBankFile

ファイル: `FormCreateBankFile.Designer.cs` / `FormCreateBankFile.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iBank` | UcTableComboBox | — | — | — | `ComboBox.ExSetSelectedIndexByValue()` `Text` `Select()` `Value` `appToolTip.Show` |
| `iDate` | UcDate | — | Date ※直接適用 | UcFormat=YYYYMMDD | `UcValue =` `UcDateCtl` `Select()` `UcValue` `UcValue.Value` `appToolTip.Show` |
| `iDateYM` | UcDate | — | — | UcFormat=YYYYMM | `UcValue =` `UcValue` `Select()` `UcValue.Value` `appToolTip.Show` |

### FormCreateIfaxFile

ファイル: `FormCreateIfaxFile.Designer.cs` / `FormCreateIfaxFile.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iIfax` | UcTableComboBox | — | — | — | `Select()` `Text` `Value` |

### FormHikiotoshi

ファイル: `FormHikiotoshi.Designer.cs` / `FormHikiotoshi.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iDateYM` | UcDate | — | — | UcFormat=YYYYMM | `UcValue =` `UcValueChanged +=` `UcValue` `UcValue.Value` `Select()` `appToolTip.Show` |
| `iTotalCost` | GcNumberEx | — | — | BackColor=(235,240,255)<br>Enabled=false | `Text =` |
| `iTotalCount` | GcNumberEx | — | — | BackColor=(235,240,255)<br>Enabled=false | `Text =` |

### FormHikiotoshi_DlgEntry

ファイル: `FormHikiotoshi_DlgEntry.Designer.cs` / `FormHikiotoshi_DlgEntry.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiin` | GcTextBoxEx | — | — | BackColor=(235,240,255)<br>ContentAlignment=MiddleLeft<br>Enabled=false<br>Font=メイリオ 9.75pt | `Text =` |
| `iDateYM` | GcTextBoxEx | — | — | BackColor=(235,240,255)<br>ContentAlignment=MiddleCenter<br>Enabled=false<br>Font=メイリオ 9.75pt | `Text =` |
| `iTotalCost` | GcNumberEx | — | — | BackColor=(235,240,255)<br>Enabled=false | `Value =` |
| `iMemo` | GcTextBoxEx | Text(t_hikiotoshi.FHiki_Memo) | Text(20) Align=TopLeft | BorderStyle=None | `SetFocusControlInGrid` |
| `iCost` | GcNumberEx | Number(t_hikiotoshi.FHiki_Cost) | Currency(7,0) Align=BottomRight AlternateBlank=true | BorderStyle=None | `ValueChanged +=` `SetFocusControlInGrid` |
| `iKaihi` | UcTableComboBox | Combo(t_hikiotoshi.FID_Kaihi) | なし | ComboBorderStyle=None | `ComboBox` `SetFocusControlInGrid` `appToolTip.Show` |

### FormKaiin

ファイル: `FormKaiin.Designer.cs` / `FormKaiin.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iSearch` | GcTextBoxEx | — | Ruby(15,30) ※直接適用 | — | `TextChanged +=` `ResetText()` `Text` |
| `iSearchIsekiTorokuNo` | GcTextBoxEx | — | — | — | `TextChanged +=` `ResetText()` `Text` |
| `iSearchBirthday` | UcDate | — | Date ※直接適用 | UcFormat=YYYYMMDD | `UcDateCtl` `UcValueChanged +=` `UcValue =` `UcValue` `UcValue.Value` |

### FormKaiin_DlgChosei

ファイル: `FormKaiin_DlgChosei.Designer.cs` / `FormKaiin_DlgChosei.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiin` | GcTextBoxEx | — | — | BackColor=(235,240,255)<br>ContentAlignment=MiddleLeft<br>Enabled=false<br>Font=メイリオ 9.75pt | `Text =` |
| `iKaihi` | UcTableComboBox | Combo(t_kaiin_chosei.FID_Kaihi) | なし | ComboBorderStyle=None | `ComboBox` `SetFocusControlInGrid` `appToolTip.Show` |
| `iCost` | GcNumberEx | Number(t_kaiin_chosei.FKCS_Cost) | Currency(7,0) Align=BottomRight AlternateBlank=true | BorderStyle=None | `SetFocusControlInGrid` `appToolTip.Show` |
| `iMemo` | GcTextBoxEx | Text(t_kaiin_chosei.FKCS_Memo) | Text(20) Align=TopLeft | BorderStyle=None | `SetFocusControlInGrid` |
| `iDateYM` | UcDate | Date(t_kaiin_chosei.FKCS_DateYM) | Date | UcBorderStyle=None<br>UcFormat=YYYYMM | `UcDateCtl` `SetFocusControlInGrid` `appToolTip.Show` |
| `iShiharai` | GcComboBoxEx | Combo(t_kaiin_chosei.FKCS_Shiharai) | なし | BorderStyle=None | `AppCombo.SetComboBox[enumKbn.DShiharai]` `SetFocusControlInGrid` |

### FormKaiin_DlgEntry

ファイル: `FormKaiin_DlgEntry.Designer.cs` / `FormKaiin_DlgEntry.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iMail1` | GcTextBoxEx | Text(t_kaiin.FKaiin_Mail1) | Text(80) ImeMode=Disable | — | — |
| `iTel2_3` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Tel2) | HyphenSplit(5,4,5) | — | — |
| `iTel2_2` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Tel2) | HyphenSplit(5,4,5) | — | — |
| `iTel2_1` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Tel2) | HyphenSplit(5,4,5) | — | — |
| `iBirthday` | UcDate | Date(t_kaiin.FKaiin_DateBirth) | Date | UcFormat=YYYYMMDD | `UcDateCtl` |
| `iZaiseki` | GcComboBoxEx | Combo(t_kaiin.FKaiin_TypeZaiseki) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iTel1_3` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Tel1) | HyphenSplit(5,4,5) | — | — |
| `iTel1_2` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Tel1) | HyphenSplit(5,4,5) | — | — |
| `iTel1_1` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Tel1) | HyphenSplit(5,4,5) | — | — |
| `iPost2` | GcTextBoxEx | PostAddr(t_kaiin.FKaiin_Post, t_kaiin.FKaiin_Addr1) | PostAddr MaxLength[2]=30 | — | — |
| `iPost1` | GcTextBoxEx | PostAddr(t_kaiin.FKaiin_Post, t_kaiin.FKaiin_Addr1) | PostAddr MaxLength[2]=30 | — | — |
| `iAddr2` | GcTextBoxEx | Text(t_kaiin.FKaiin_Addr2) | Text(40) | — | — |
| `iAddr1` | GcTextBoxEx | PostAddr(t_kaiin.FKaiin_Post, t_kaiin.FKaiin_Addr1) | PostAddr MaxLength[2]=30 | — | — |
| `iNameFurigana` | GcTextBoxEx | Ruby(t_kaiin.FKaiin_Name, t_kaiin.FKaiin_NameKana) | Ruby(15,30) | — | — |
| `iName` | GcTextBoxEx | Ruby(t_kaiin.FKaiin_Name, t_kaiin.FKaiin_NameKana) | Ruby(15,30) | — | — |
| `iCode` | GcTextBoxEx | Text(t_kaiin.FCD_Kaiin) | Number(6,0) Align=BottomLeft | BackColor=(173,233,196)<br>SingleBorderColor=(127,102,71) | `Text` `Select()` `appToolTip.Show` |
| `iSex` | GcComboBoxEx | Combo(t_kaiin.FKaiin_Sex) | なし | — | `AppCombo.SetComboBox[enumKbn.DSex]` |
| `iFax_3` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Fax) | HyphenSplit(5,4,5) | — | — |
| `iFax_2` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Fax) | HyphenSplit(5,4,5) | — | — |
| `iFax_1` | GcTextBoxEx | HyphenSplit(t_kaiin.FKaiin_Fax) | HyphenSplit(5,4,5) | — | — |
| `iIsekiTorokuDate` | UcDate | Date(t_kaiin.FKaiin_DateIsekiToroku) | Date | UcFormat=YYYYMMDD | `UcDateCtl` |
| `iIsekiTourokuNo` | GcTextBoxEx | Number(t_kaiin.FKaiin_IsekiTorokuNo) | Number(7,0) | — | — |
| `iSotsugyoNengetsu` | UcDate | Date(t_kaiin.FKaiin_DateSotsugyo) | Date | UcFormat=YYYYMMDD | `UcFormat =` `UcDateCtl.DropDownCalendar.CalendarType =` `UcDateCtl` |
| `iShusshinKou` | GcTextBoxEx | Text(t_kaiin.FKaiin_ShusshinKo) | Text(40) | — | — |
| `iShuryoNengetsu` | UcDate | Date(t_kaiin.FKaiin_DateShuryo) | Date | UcFormat=YYYYMMDD | `UcFormat =` `UcDateCtl.DropDownCalendar.CalendarType =` `UcDateCtl` |
| `iShusshinDaigakuin` | GcTextBoxEx | Text(t_kaiin.FKaiin_ShusshinIn) | Text(40) | — | — |
| `iGakuiNengetsu` | UcDate | Date(t_kaiin.FKaiin_DateShutokuGakui) | Date | UcFormat=YYYYMMDD | `UcFormat =` `UcDateCtl.DropDownCalendar.CalendarType =` `UcDateCtl` |
| `iTorokuDate` | UcDate | Date(t_kaiin.FKaiin_DateToroku) | Date | UcFormat=YYYYMMDD | `UcDateCtl` |
| `iNyukaiDate` | UcDate | Date(t_kaiin.FKaiin_DateNyukai) | Date | UcFormat=YYYYMMDD | `UcDateCtl` |
| `iIryoByoshoCnt` | GcTextBoxEx | Number(t_iryokikan.FIRK_Kyoka) | Number(4,0) | — | `Enabled =` |
| `iIryoKaisetsushutai` | GcTextBoxEx | Text(t_iryokikan.FIRK_KaisetsuShutai) | なし | — | `Enabled =` |
| `iIryoFax1_3` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Fax1) | HyphenSplit(5,4,5) | — | `Enabled =` |
| `iIryoFax1_2` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Fax1) | HyphenSplit(5,4,5) | — | `Enabled =` |
| `iIryoFax1_1` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Fax1) | HyphenSplit(5,4,5) | — | `Enabled =` |
| `iIryoTel1_3` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Tel1) | HyphenSplit(5,4,5) | — | `Enabled =` |
| `iIryoTel1_2` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Tel1) | HyphenSplit(5,4,5) | — | `Enabled =` |
| `iIryoTel1_1` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Tel1) | HyphenSplit(5,4,5) | — | `Enabled =` |
| `iIryoPost2` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Post) | PostAddr MaxLength[2]=30 | — | `Enabled =` |
| `iIryoPost1` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Post) | PostAddr MaxLength[2]=30 | — | `Enabled =` |
| `iIryoAddr2` | GcTextBoxEx | Text(t_iryokikan.FIRK_Addr2) | Text(40) | — | `Enabled =` |
| `iIryoAddr1` | GcTextBoxEx | Text(t_iryokikan.FIRK_Addr1) | なし | — | `Enabled =` |
| `iIryoShisetsuNameKana` | GcTextBoxEx | Text(t_iryokikan.FIRK_TsushoKana) | なし | — | `Enabled =` |
| `iIryoShisetsuName` | GcTextBoxEx | Text(t_iryokikan.FIRK_Tsusho) | Text(30) | — | `Enabled =` |
| `iIryoHeisetsuName` | GcTextBoxEx | Text(t_iryokikan.FIRK_Memo) | なし | — | `Enabled =` |
| `iIryoByoshoUmu` | GcTextBoxEx | Text(t_iryokikan.FIRK_ByoshoUmu) | なし | — | `Enabled =` |
| `iShoninDate` | UcDate | Date(t_kaiin.FKaiin_DateShonin) | Date | UcFormat=YYYYMMDD | `UcDateCtl` |
| `iIryoKumi` | GcTextBoxEx | Text(t_iryokikan.FIRK_KumiCode) | なし | — | `Enabled =` |
| `iTantoKamoku` | UcTableComboBox | Combo(t_kaiin_shinryoka.FID_Shinryoka) | なし | ComboBorderStyle=None | `ComboBox` `SetFocusControlInGrid` |
| `iSankaIkai` | UcTableComboBox | Combo(t_kaiin_kaihi.FID_Kaihi) | なし | ComboBorderStyle=None | `ComboBox` `SetFocusControlInGrid` |
| `iShitenName3` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iBankName3` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iKozaType3` | GcComboBoxEx | Combo(t_kaiin.FKaiin_BankKozaType3) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeKoza]` |
| `iKozaName3` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankKozaName3) | Text(30) Format=H AllowSpace=Narrow ImeMode=KatakanaHalf | — | — |
| `iKozaNo3` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankKozaNo3) | Number(7,0) Align=BottomRight | — | — |
| `iShitenCD3` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankShitenCode3) | Number(3,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iBankCD3` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankCode3) | Number(4,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iShitenName2` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iBankName2` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iKozaType2` | GcComboBoxEx | Combo(t_kaiin.FKaiin_BankKozaType2) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeKoza]` |
| `iKozaName2` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankKozaName2) | Text(30) Format=H AllowSpace=Narrow ImeMode=KatakanaHalf | — | — |
| `iKozaNo2` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankKozaNo2) | Number(7,0) Align=BottomRight | — | — |
| `iShitenCD2` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankShitenCode2) | Number(3,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iBankCD2` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankCode2) | Number(4,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iShitenName1` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iBankName1` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iKozaType1` | GcComboBoxEx | Combo(t_kaiin.FKaiin_BankKozaType1) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeKoza]` |
| `iKozaName1` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankKozaName1) | Text(30) Format=H AllowSpace=Narrow ImeMode=KatakanaHalf | — | — |
| `iKozaNo1` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankKozaNo1) | Number(7,0) Align=BottomRight | — | — |
| `iShitenCD1` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankShitenCode1) | Number(3,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iBankCD1` | GcTextBoxEx | Text(t_kaiin.FKaiin_BankCode1) | Number(4,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iTaikaiDate` | UcDate | Date(t_kaiin.FKaiin_DateTaikai) | Date | SingleBorderColor=(127,102,71)<br>UcBackColor=(173,233,196)<br>UcFormat=YYYYMMDD | `UcDateCtl` |
| `iTantoMainKamoku` | UcTableComboBox | Combo(t_kaiin.FID_Shinryoka_Main) | なし | — | `Select()` `ComboBox` |
| `iFaxSofu` | GcComboBoxEx | Combo(t_kaiin.FKaiin_FaxSofusaki) | なし | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `ExSetSelectedIndexByValue()` |
| `iBunshoSofu` | GcComboBoxEx | Combo(t_kaiin.FKaiin_BunshoSofusaki) | なし | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `ExSetSelectedIndexByValue()` |
| `iKaihiUchiwake` | GcComboBoxEx | Combo(t_kaiin.FKaiin_KaihiUchiwake) | なし | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `ExSetSelectedIndexByValue()` |
| `iIryoKikan` | UcTableComboBox | Combo(t_kaiin.FID_Iryokikan) | なし | — | `Select()` `ComboBox` `TextChanged +=` `Text` |
| `iShiharai` | GcComboBoxEx | Combo(t_kaiin_kaihi.FKaihiShiharai) | なし | BorderStyle=None | `AppCombo.SetComboBox[enumKbn.DShiharai]` `SetFocusControlInGrid` |
| `iIshikaiIdomae` | UcTableComboBox | Combo(t_kaiin.FID_Ishikai_Idomae) | なし | — | `ComboBox` |
| `iTaikaiJiyu` | GcComboBoxEx | Combo(t_kaiin.FKaiin_TypeTaikaiJiyu) | なし | — | `SelectedIndexChanged +=` `SelectedIndex` `AppCombo.SetComboBox[enumKbn.DTypeTaikaiJiyu]` |
| `iIryoShisetsugyomu` | UcTableComboBox | Combo(t_kaiin.FID_ShisetsuGyomu) | なし | — | `ComboBox` |
| `iTaikaiJiyuEtc` | GcTextBoxEx | Text(t_kaiin.FKaiin_TaikaiEtcmemo) | Text(40) | — | `Enabled =` |
| `iIshikaiShiharai` | GcComboBoxEx | Combo(t_kaiin_kaihi.FKaihiShiharai) | なし | BorderStyle=None | `AppCombo.SetComboBox[enumKbn.DShiharai]` `SetFocusControlInGrid` |
| `iIshikai` | UcTableComboBox | Combo(t_kaiin_kaihi.FID_Kaihi) | なし | ComboBorderStyle=None | `ComboBox` `SetFocusControlInGrid` |
| `iShinseiDate` | UcDate | Date(t_kaiin.FKaiin_DateShinsei) | Date | UcFormat=YYYYMMDD | `UcDateCtl` |
| `iIryoShiteiiEtc` | GcTextBoxEx | Text(t_kaiin.FKaiin_ShiteiiEtc) | Text(20) | — | `Text` `Enabled =` `Text =` |
| `iSyozokuGakkai` | UcTableComboBox | Combo(t_kaiin_gakkai.FID_Gakkai) | なし | ComboBorderStyle=None | `ComboBox` `SetFocusControlInGrid` |
| `iNameFuriganaOld` | GcTextBoxEx | Ruby(t_kaiin.FKaiin_NameOld, t_kaiin.FKaiin_NameKanaOld) | Ruby(15,30) | — | — |
| `iNameOld` | GcTextBoxEx | Ruby(t_kaiin.FKaiin_NameOld, t_kaiin.FKaiin_NameKanaOld) | Ruby(15,30) | — | — |
| `iIshikaiIdosaki` | UcTableComboBox | Combo(t_kaiin.FID_Ishikai_Idosaki) | なし | — | `ComboBox` `Enabled =` |
| `iGenmen` | GcComboBoxEx | Combo(t_kaiin.FKaiin_TypeGenmen) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeGenmen]` |
| `iKaiinKbn` | GcComboBoxEx | Combo(t_kaiin.FKaiin_TypeKaiin) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeKaiin]` |

### FormKaiin_DlgEntry_Idoinfo

ファイル: `FormKaiin_DlgEntry_Idoinfo.Designer.cs` / `FormKaiin_DlgEntry_Idoinfo.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `gcShortcut1` | GcShortcut | — | — | — | — |
| `iIdoDate` | UcDate | Date(t_kaiin_ido.FIdoDate) | Date | SingleBorderColor=(127,102,71)<br>UcBackColor=(173,233,196)<br>UcFormat=YYYYMMDD | `UcDateCtl` `Select()` |
| `iIdoKbn` | GcComboBoxEx | Combo(t_kaiin_ido.FIdoJiyuKbn) | なし | — | `SelectedIndex` `SelectedIndexChanged +=` `AppCombo.SetComboBox[enumKbn.DTypeIdoJiyu]` |
| `iIdoEtcMemo` | GcTextBoxEx | Text(t_kaiin_ido.FIdoJiyuDetail_EtcMemo) | Text(20) | — | `Enabled =` `Text =` |
| `iIdoEtc` | GcComboBoxEx | Combo(t_kaiin_ido.FIdoJiyuDetail_Etc) | なし | — | `SelectedIndexChanged +=` `SelectedIndex` `AppCombo.SetComboBox[enumKbn.DIdoEtc]` |
| `iIdoKaiin` | GcComboBoxEx | Combo(t_kaiin_ido.FIdoJiyuDetail_Kaiin) | なし | — | `AppCombo.SetComboBox[enumKbn.DIdoKaiinHenko]` |
| `iIdoIdoMemo` | GcTextBoxEx | Text(t_kaiin_ido.FIdoJiyuDetail_IdoMemo) | Text(20) | — | `Enabled =` `Text =` |
| `iIdoIdo` | GcComboBoxEx | Combo(t_kaiin_ido.FIdoJiyuDetail_Ido) | なし | — | `SelectedIndexChanged +=` `SelectedIndex` `AppCombo.SetComboBox[enumKbn.DIdoShisetsu]` |
| `iIdoShozaiMemo` | GcTextBoxEx | Text(t_kaiin_ido.FIdoJiyuDetail_ShozaiMemo) | Text(20) | — | `Enabled =` `Text =` |
| `iIdoShozai` | GcComboBoxEx | Combo(t_kaiin_ido.FIdoJiyuDetail_Shozai) | なし | — | `SelectedIndexChanged +=` `SelectedIndex` `AppCombo.SetComboBox[enumKbn.DIdoShozai]` |
| `iIdoAddressMemo` | GcTextBoxEx | Text(t_kaiin_ido.FIdoJiyuDetail_AddressMemo) | Text(20) | — | `Enabled =` `Text =` |
| `iIdoAddress` | GcComboBoxEx | Combo(t_kaiin_ido.FIdoJiyuDetail_Address) | なし | — | `SelectedIndexChanged +=` `SelectedIndex` `AppCombo.SetComboBox[enumKbn.DIdoAddress]` |

### FormKaiin_DlgHistory

ファイル: `FormKaiin_DlgHistory.Designer.cs` / `FormKaiin_DlgHistory.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiin` | GcTextBoxEx | — | — | BackColor=(235,240,255) | `Text =` `Enabled =` |
| `iTotal` | GcTextBoxEx | — | — | BackColor=(235,240,255) | `Text =` `Enabled =` |
| `iIsekiNo` | GcTextBoxEx | — | — | BackColor=(235,240,255)<br>ContentAlignment=TopRight | `Text =` `Enabled =` |

### FormLogin

ファイル: `FormLogin.Designer.cs` / `FormLogin.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iCode` | GcTextBoxEx | — | Number(6,0) Align=BottomLeft ※直接適用 | ContentAlignment=BottomLeft<br>Font=メイリオ 9.75pt<br>ImeMode=Off | `Text =` `TextLength` `Select()` `Text` `SelectAll()` `appToolTip.Show` |
| `iPwd` | GcTextBoxEx | — | — | ContentAlignment=BottomLeft<br>Font=メイリオ 9.75pt<br>ImeMode=Off<br>PasswordChar='*' | `Select()` `Text =` `TextLength` `Text` `SelectAll()` `appToolTip.Show` |

### FormMain

ファイル: `FormMain.Designer.cs` / `FormMain.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iTantosha` | GcTextBoxEx | — | — | BackColor=(235,240,255)<br>ContentAlignment=BottomLeft<br>Enabled=false<br>Font=メイリオ 11.25pt | `Text =` |
| `gcShortcut1` | GcShortcut | — | — | — | — |

### FormMamisImport

ファイル: `FormMamisImport.Designer.cs` / `FormMamisImport.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iFile` | UcFileSelect | — | — | FileFilter="CSVファイル｜*.csv" | `Select()` `UcText` `appToolTip.Show` |

### FormMasterBankCode

ファイル: `FormMasterBankCode.Designer.cs` / `FormMasterBankCode.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iBank` | GcTextBoxEx | — | Ruby(20,40) ※直接適用 | — | `TextChanged +=` `Text` `Clear()` `Select()` |
| `iShiten` | GcTextBoxEx | — | Ruby(20,40) ※直接適用 | — | `TextChanged +=` `Text` `Clear()` |

### FormMasterBankCode_DlgEntry

ファイル: `FormMasterBankCode_DlgEntry.Designer.cs` / `FormMasterBankCode_DlgEntry.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `gcShortcut1` | GcShortcut | — | — | — | — |
| `iNameBank` | GcTextBoxEx | Ruby(t_bank_code.FBCD_Name, t_bank_code.FBCD_NameFurigana) | Ruby(20,40) | — | — |
| `iNameShiten` | GcTextBoxEx | Ruby(t_bank_code.FBCD_NameShiten, t_bank_code.FBCD_NameShitenFurigana) | Ruby(20,40) | — | — |
| `iNameKanaBank` | GcTextBoxEx | Ruby(t_bank_code.FBCD_Name, t_bank_code.FBCD_NameFurigana) | Ruby(20,40) | — | — |
| `iNameKanaShiten` | GcTextBoxEx | Ruby(t_bank_code.FBCD_NameShiten, t_bank_code.FBCD_NameShitenFurigana) | Ruby(20,40) | — | — |
| `iBankCode` | GcTextBoxEx | Text(t_bank_code.FBCD_Code) | Number(4,0) Align=BottomRight | — | `Select()` `Text` |
| `iShitenCode` | GcTextBoxEx | Text(t_bank_code.FBCD_CodeShiten) | Number(3,0) Align=BottomRight | — | `Text` `Select()` `appToolTip.Show` |
| `iFullNameBank` | GcTextBoxEx | Text(t_bank_code.FBCD_FullName) | なし | — | — |

### FormMasterBank_DlgEntry

ファイル: `FormMasterBank_DlgEntry.Designer.cs` / `FormMasterBank_DlgEntry.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `gcShortcut1` | GcShortcut | — | — | — | — |
| `iNameBank` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iNameShiten` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iNameKanaBank` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iNameKanaShiten` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iCodeCompany` | GcTextBoxEx | Text(t_bank.FBNK_CodeCompany) | Number(10,0) Align=BottomRight | — | — |
| `iKozaType` | GcComboBoxEx | Combo(t_bank.FBNK_KozaType) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeKoza]` |
| `iKozaNo` | GcTextBoxEx | Text(t_bank.FBNK_KozaNo) | Number(7,0) Align=BottomRight | — | — |
| `iBankCode` | GcTextBoxEx | Text(t_bank.FBNK_Code) | Number(4,0) Align=BottomRight | — | `Select()` `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iShitenCode` | GcTextBoxEx | Text(t_bank.FBNK_CodeShiten) | Number(3,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |

### FormMasterBasic

ファイル: `FormMasterBasic.Designer.cs` / `FormMasterBasic.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iHojinNo` | GcTextBoxEx | Text(t_basic.FBAS_HojinNo) | Text(13) ImeMode=Disable | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iNameFurigana` | GcTextBoxEx | Ruby(t_basic.FBAS_Name, t_basic.FBAS_NameFurigana) | Ruby(40,80) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iNameDaihyo` | GcTextBoxEx | Text(t_basic.FBAS_NameDaihyo) | Text(30) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iTel2_3` | GcTextBoxEx | HyphenSplit(t_basic.FBAS_Tel2)<br>HyphenSplit(t_basic.FBAS_Fax) | HyphenSplit(5,4,5) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iTel2_2` | GcTextBoxEx | HyphenSplit(t_basic.FBAS_Tel2)<br>HyphenSplit(t_basic.FBAS_Fax) | HyphenSplit(5,4,5) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iTel2_1` | GcTextBoxEx | HyphenSplit(t_basic.FBAS_Tel2)<br>HyphenSplit(t_basic.FBAS_Fax) | HyphenSplit(5,4,5) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iTel1_3` | GcTextBoxEx | HyphenSplit(t_basic.FBAS_Tel1) | HyphenSplit(5,4,5) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iTel1_2` | GcTextBoxEx | HyphenSplit(t_basic.FBAS_Tel1) | HyphenSplit(5,4,5) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iTel1_1` | GcTextBoxEx | HyphenSplit(t_basic.FBAS_Tel1) | HyphenSplit(5,4,5) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iAddr2` | GcTextBoxEx | Text(t_basic.FBAS_Addr2) | Text(40) | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iAddr1` | GcTextBoxEx | PostAddr(t_basic.FBAS_Post, t_basic.FBAS_Addr1) | PostAddr MaxLength[2]=30 | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iPost2` | GcTextBoxEx | PostAddr(t_basic.FBAS_Post, t_basic.FBAS_Addr1) | PostAddr MaxLength[2]=30 | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iPost1` | GcTextBoxEx | PostAddr(t_basic.FBAS_Post, t_basic.FBAS_Addr1) | PostAddr MaxLength[2]=30 | ContentAlignment=BottomLeft<br>ImeMode=Off | — |
| `iName` | GcTextBoxEx | Ruby(t_basic.FBAS_Name, t_basic.FBAS_NameFurigana) | Ruby(40,80) | ContentAlignment=BottomLeft<br>ImeMode=Off | `Select()` `appToolTip.Show` |
| `iDispSeireki` | GcComboBoxEx | Combo(t_basic.FBAS_DispSeireki) | なし | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` |
| `iBank` | UcTableComboBox | Combo(t_basic.FID_Bank) | なし | — | `ComboBox` |

### FormMasterImport

ファイル: `FormMasterImport.Designer.cs` / `FormMasterImport.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iFileIryoKikan` | UcFileSelect | — | — | FileFilter="Excelファイル｜*.xlsx;*.xls" | `UcText` `appToolTip.Show` |
| `iFileKaihi` | UcFileSelect | — | — | FileFilter="Excelファイル｜*.xlsx;*.xls" | `UcText` `appToolTip.Show` |
| `iFileKaiin` | UcFileSelect | — | — | FileFilter="Excelファイル｜*.xlsx;*.xls" | `UcText` `appToolTip.Show` |

### FormMasterIryoKikan

ファイル: `FormMasterIryoKikan.Designer.cs` / `FormMasterIryoKikan.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iNameIryo` | GcTextBoxEx | — | Text(30) ※直接適用 | — | `TextChanged +=` `ResetText()` `Text` |
| `iCodeIryo` | GcTextBoxEx | — | — | — | `TextChanged +=` `ResetText()` `Text` |
| `iKbnTaikai` | GcComboBoxEx | — | — | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `SelectedIndexChanged +=` `ExSetSelectedIndexByValue()` `SelectedIndex` `ExGetValue()` |
| `iKumiCode` | UcTableComboBox | — | — | — | `SelectIndexChanged +=` `ComboBox.ExSetSelectedIndexByValue()` `Value` |

### FormMasterIryoKikan_DlgEntry

ファイル: `FormMasterIryoKikan_DlgEntry.Designer.cs` / `FormMasterIryoKikan_DlgEntry.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iTel2_2` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Fax1) | HyphenSplit(5,4,5) | — | — |
| `iTel2_1` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Fax1) | HyphenSplit(5,4,5) | — | — |
| `iTel1_2` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Tel1) | HyphenSplit(5,4,5) | — | — |
| `iTel1_1` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Tel1) | HyphenSplit(5,4,5) | — | — |
| `iPost2` | GcTextBoxEx | PostAddr(t_iryokikan.FIRK_Post, t_iryokikan.FIRK_Addr1) | PostAddr MaxLength[2]=30 | — | — |
| `iPost1` | GcTextBoxEx | PostAddr(t_iryokikan.FIRK_Post, t_iryokikan.FIRK_Addr1) | PostAddr MaxLength[2]=30 | — | — |
| `iAddr2` | GcTextBoxEx | Text(t_iryokikan.FIRK_Addr2) | Text(40) | — | — |
| `iAddr1` | GcTextBoxEx | PostAddr(t_iryokikan.FIRK_Post, t_iryokikan.FIRK_Addr1) | PostAddr MaxLength[2]=30 | — | — |
| `iCode` | GcTextBoxEx | Text(t_iryokikan.FIRK_Code) | Number(5,0) | — | `Text` `Select()` `appToolTip.Show` |
| `iShisetsuKana` | GcTextBoxEx | Ruby(t_iryokikan.FIRK_Name, t_iryokikan.FIRK_Kana) | Text(30) | — | — |
| `iShisetsuName` | GcTextBoxEx | Ruby(t_iryokikan.FIRK_Name, t_iryokikan.FIRK_Kana) | Text(30) | — | — |
| `iShisetsuTsushoKana` | GcTextBoxEx | Ruby(t_iryokikan.FIRK_Tsusho, t_iryokikan.FIRK_TsushoKana) | Text(30) | — | — |
| `iTel1_3` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Tel1) | HyphenSplit(5,4,5) | — | — |
| `iTel2_3` | GcTextBoxEx | HyphenSplit(t_iryokikan.FIRK_Fax1) | HyphenSplit(5,4,5) | — | — |
| `iKyokabyosho` | GcTextBoxEx | Text(t_iryokikan.FIRK_Kyoka) | Number(4,0) | — | `Text =` `Enabled =` |
| `iHeisetsu` | GcTextBoxEx | Text(t_iryokikan.FIRK_Memo) | なし | — | `Enabled =` |
| `iShisetsuTsusho` | GcTextBoxEx | Ruby(t_iryokikan.FIRK_Tsusho, t_iryokikan.FIRK_TsushoKana) | Text(30) | — | — |
| `iHyoboKamoku` | UcTableComboBox | Combo(t_iryokikan_shinryoka.FID_Shinryoka) | なし | ComboBorderStyle=None | `ComboBox` `SetFocusControlInGrid` |
| `gcShortcut1` | GcShortcut | — | — | — | — |
| `iByoshoUmu` | GcComboBoxEx | Combo(t_iryokikan.FIRK_ByoshoUmu) | なし | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `SelectedIndexChanged +=` `SelectedIndex` |
| `iTaikaiKbn` | GcComboBoxEx | Combo(t_iryokikan.FIRK_TaikaiKbn) | なし | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` |
| `iKaisetsushutai` | UcTableComboBox | Combo(t_iryokikan.FIRK_KaisetsuShutai) | なし | — | `ComboBox` |
| `iKumiCode` | UcTableComboBox | Combo(t_iryokikan.FIRK_KumiCode) | なし | — | `ComboBox` |

### FormMasterKaihi

ファイル: `FormMasterKaihi.designer.cs` / `FormMasterKaihi.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iNameKaihi` | GcTextBoxEx | — | Text(30) ※直接適用 | — | `TextChanged +=` `ResetText()` `Text` |
| `iKbnUsed` | GcComboBoxEx | — | — | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `SelectedIndexChanged +=` `ExSetSelectedIndexByValue()` `SelectedIndex` `ExGetValue()` |
| `iKbnKaihi` | GcComboBoxEx | — | — | — | `SelectedIndexChanged +=` `ExSetSelectedIndexByValue()` `SelectedIndex` `AppCombo.SetComboBox[enumKbn.DTypeKaihi]` |
| `iCodeKaihi` | GcTextBoxEx | — | Number(5,0) ※直接適用 | — | `TextChanged +=` `ResetText()` `Text` |

### FormMasterKaihi_DlgEntry

ファイル: `FormMasterKaihi_DlgEntry.designer.cs` / `FormMasterKaihi_DlgEntry.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iSearchUsed` | GcComboBoxEx | Combo(t_kaihi.FKaihi_SearchUsed) | なし | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` |
| `iCode` | GcTextBoxEx | Text(t_kaihi.FCD_Kaihi) | Number(5,0) | — | `Text` `Select()` `appToolTip.Show` |
| `iName` | GcTextBoxEx | Ruby(t_kaihi.FKaihi_Name, t_kaihi.FKaihi_ShortName) | Text(30) | — | — |
| `iShortName` | GcTextBoxEx | Ruby(t_kaihi.FKaihi_Name, t_kaihi.FKaihi_ShortName) | Text(30) | — | — |
| `iKozaName` | GcTextBoxEx | Ruby(t_kaihi.FKaihi_BankKozaName, t_kaihi.FKaihi_BankKozaNameKana) | Ruby(15,30) | — | — |
| `iKozaNo` | GcTextBoxEx | Text(t_kaihi.FKaihi_BankKozaNo) | Number(7,0) Align=BottomRight | — | — |
| `iShitenCD` | GcTextBoxEx | Text(t_kaihi.FKaihi_BankCodeShiten) | Number(3,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iBankCD` | GcTextBoxEx | Text(t_kaihi.FKaihi_BankCode) | Number(4,0) Align=BottomRight | — | `TextChanged +=` `MouseDoubleClick +=` `Text` |
| `iKozaType` | GcComboBoxEx | Combo(t_kaihi.FKaihi_BankKozaType) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeKoza]` |
| `iShitenName` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iBankName` | GcTextBoxEx | — | — | — | `Enabled =` `Text =` |
| `iBiko` | GcTextBoxEx | Text(t_kaihi.FKaihi_Biko) | Text(20) | — | — |
| `iCost1` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost1) | Currency(7,0) | — | — |
| `iCost2` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost2) | Currency(7,0) | — | — |
| `oCostAll` | GcNumberEx | — | — | — | `Enabled =` `Value =` |
| `iCost12` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost12) | Currency(7,0) | — | — |
| `iCost11` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost11) | Currency(7,0) | — | — |
| `iCost10` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost10) | Currency(7,0) | — | — |
| `iCost9` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost9) | Currency(7,0) | — | — |
| `iCost8` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost8) | Currency(7,0) | — | — |
| `iCost7` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost7) | Currency(7,0) | — | — |
| `iCost6` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost6) | Currency(7,0) | — | — |
| `iCost5` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost5) | Currency(7,0) | — | — |
| `iCost4` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost4) | Currency(7,0) | — | — |
| `iCost3` | GcNumberEx | Number(t_kaihi.FKaihi_GetsugakuCost3) | Currency(7,0) | — | — |
| `iKaihiGunCode` | GcTextBoxEx | Number(t_kaihi.FKaihi_GunCode) | Number(4,0) | — | — |
| `iBankIfax` | UcTableComboBox | Combo(t_kaihi.FKaihi_BankIfax) | なし | — | `ComboBox` |
| `iKbnKaihi` | GcComboBoxEx | Combo(t_kaihi.FKaihi_KaihiType) | なし | — | `AppCombo.SetComboBox[enumKbn.DTypeKaihi]` |
| `iKozaNameKana` | GcTextBoxEx | Ruby(t_kaihi.FKaihi_BankKozaName, t_kaihi.FKaihi_BankKozaNameKana) | Ruby(15,30) | — | — |
| `iKaihiZokusei` | GcComboBoxEx | Combo(t_kaihi.FKaihi_Zokusei) | なし | — | `AppCombo.SetComboBox[enumKbn.DKaihiZokusei]` |

### FormMasterShinryoka

ファイル: `FormMasterShinryoka.designer.cs` / `FormMasterShinryoka.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iNameShinryoka` | GcTextBoxEx | — | Text(30) ※直接適用 | — | `TextChanged +=` `ResetText()` `Text` |
| `iCodeShinryoka` | GcTextBoxEx | — | — | — | `TextChanged +=` `ResetText()` `Text` |

### FormMasterShinryoka_DlgEntry

ファイル: `FormMasterShinryoka_DlgEntry.Designer.cs` / `FormMasterShinryoka_DlgEntry.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iShisetsuName` | GcTextBoxEx | Text(t_shinryoka.FSRK_Name) | Text(30) | — | — |
| `iCode` | GcTextBoxEx | Text(t_shinryoka.FSRK_Code) | Number(2,0) | — | `Select()` `Text` `appToolTip.Show` |

### FormMasterTantosha_DlgEntry

ファイル: `FormMasterTantosha_DlgEntry.Designer.cs` / `FormMasterTantosha_DlgEntry.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iShinryojo` | GcComboBoxEx | — | — | — | `Visible =` `AppCombo.SetComboBox[enumKbn.DTypeShinryojo]` |
| `iPwd` | GcTextBoxEx | Text(t_tantosha.FTNT_Password) | Text(20) ImeMode=Off | PasswordChar='*' | `Enabled =` |
| `iAuth` | GcComboBoxEx | Combo(t_tantosha.FTNT_Auth) | なし | — | `SelectedValueChanged +=` `AppCombo.SetComboBox[enumKbn.DAuth]` |
| `iName` | GcTextBoxEx | Text(t_tantosha.FTNT_Name) | Text(20) | — | — |
| `iCode` | GcTextBoxEx | Text(t_tantosha.FCD_Tanto) | Number(6,0) Align=BottomLeft | — | `Select()` `Text` `appToolTip.Show` |

### FormMasterTantosha_DlgPwd

ファイル: `FormMasterTantosha_DlgPwd.Designer.cs` / `FormMasterTantosha_DlgPwd.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iPwd_Old` | GcTextBoxEx | — | Text(20) ImeMode=Off ※直接適用 | — | `Select()` `Text` `appToolTip.Show` |
| `iPwd_New2` | GcTextBoxEx | — | Text(20) ImeMode=Off ※直接適用 | PasswordChar='*' | `Text` `Select()` `appToolTip.Show` |
| `iPwd_New` | GcTextBoxEx | — | Text(20) ImeMode=Off ※直接適用 | PasswordChar='*' | `Text` |

### FormOutChosyuMeisaiYear

ファイル: `FormOutChosyuMeisaiYear.Designer.cs` / `FormOutChosyuMeisaiYear.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiinFrom` | UcTableComboBox | — | — | — | `Value` |
| `iKaiinTo` | UcTableComboBox | — | — | — | `Value` |
| `iZaisekiFrom` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iZaisekiTo` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iIryoTo` | UcTableComboBox | — | — | — | `Value` |
| `iIryoFrom` | UcTableComboBox | — | — | — | `Value` |
| `iTaishoYear` | UcDate | — | — | UcFormat=YYYYMMDD | `UcFormat =` `UcSideButton =` `UcValue =` `UcValue` |

### FormOutExcelFurikomi

ファイル: `FormOutExcelFurikomi.Designer.cs` / `FormOutExcelFurikomi.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iDateYM` | UcDate | — | — | UcFormat=YYYYMM | `UcValue =` `UcValueChanged +=` `UcValue` `UcValue.Value` `Focus()` `appToolTip.Show` |
| `iDate` | UcDate | — | — | UcFormat=YYYYMMDD | `UcValue =` `UcValue` `Focus()` `UcValue.Value` `appToolTip.Show` |

### FormOutExcelFurikomiIrai

ファイル: `FormOutExcelFurikomiIrai.Designer.cs` / `FormOutExcelFurikomiIrai.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iDateYM` | UcDate | — | — | UcFormat=YYYYMM | `UcValue =` `UcValueChanged +=` `UcValue` `UcValue.Value` `Focus()` `appToolTip.Show` |
| `iDate` | UcDate | — | — | UcFormat=YYYYMMDD | `UcValue =` `UcValue` `Focus()` `UcValue.Value` `appToolTip.Show` |

### FormOutExcelKaihiShinsei

ファイル: `FormOutExcelKaihiShinsei.Designer.cs` / `FormOutExcelKaihiShinsei.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiinFrom` | UcTableComboBox | — | — | — | `Value` `Focus()` `appToolTip.Show` |

### FormOutExcelNenkanChoshu

ファイル: `FormOutExcelNenkanChoshu.Designer.cs` / `FormOutExcelNenkanChoshu.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iYear` | UcDate | — | — | UcFormat=YYYY<br>UcSideButton=false | `UcValue =` `UcValue` `Focus()` `UcValue.Value.Year` `appToolTip.Show` |
| `iKaiinFrom` | UcTableComboBox | — | — | — | `Value` `Focus()` `appToolTip.Show` |

### FormOutExcelNofusho

ファイル: `FormOutExcelNofusho.Designer.cs` / `FormOutExcelNofusho.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iDateYM` | UcDate | — | — | UcFormat=YYYYMM | `UcValue =` `UcValueChanged +=` `UcValue` `UcValue.Value` `Focus()` `appToolTip.Show` |
| `iDate` | UcDate | — | — | UcFormat=YYYYMMDD | `UcValue =` `UcValue` `Focus()` `UcValue.Value` `appToolTip.Show` |

### FormOutExcelShimingyoinKaihi

ファイル: `FormOutExcelShimingyoinKaihi.Designer.cs` / `FormOutExcelShimingyoinKaihi.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iYear` | UcDate | — | — | UcFormat=YYYY<br>UcSideButton=false | `UcValue =` `UcValue` `Focus()` `UcValue.Value.Year` `appToolTip.Show` |
| `iPiriod` | GcComboBoxEx | — | — | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `ExSetSelectedIndexByValue()` `ExGetValue()` |

### FormOutHikiotoshiKakunin

ファイル: `FormOutHikiotoshiKakunin.Designer.cs` / `FormOutHikiotoshiKakunin.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiinFrom` | UcTableComboBox | — | — | — | `Value` |
| `iKaiinTo` | UcTableComboBox | — | — | — | `Value` |
| `iShiharaiFrom` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DShiharai]` |
| `iShiharaiTo` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DShiharai]` |
| `iTaishoYM` | UcDate | — | — | UcFormat=YYYYMMDD | `UcFormat =` `UcDateCtl.DropDownCalendar.CalendarType =` `UcValue =` `UcValue` |

### FormOutKaihiList

ファイル: `FormOutKaihiList.Designer.cs` / `FormOutKaihiList.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaihiFrom` | UcTableComboBox | — | — | — | `Value` |
| `iKaihiTo` | UcTableComboBox | — | — | — | `Value` |
| `iTypeKaihiFrom` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeKaihi]` |
| `iTypeKaihiTo` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeKaihi]` |

### FormOutKaihiListBank

ファイル: `FormOutKaihiListBank.Designer.cs` / `FormOutKaihiListBank.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaihiFrom` | UcTableComboBox | — | — | — | `Value` |
| `iKaihiTo` | UcTableComboBox | — | — | — | `Value` |
| `iTypeKaihiFrom` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeKaihi]` |
| `iTypeKaihiTo` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeKaihi]` |

### FormOutKaihiUchiwakeMonth

ファイル: `FormOutKaihiUchiwakeMonth.Designer.cs` / `FormOutKaihiUchiwakeMonth.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiinFrom` | UcTableComboBox | — | — | — | `Value` |
| `iKaiinTo` | UcTableComboBox | — | — | — | `Value` |
| `iZaisekiFrom` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iZaisekiTo` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iTaishoYM` | UcDate | — | — | UcFormat=YYYYMMDD | `UcFormat =` `UcDateCtl.DropDownCalendar.CalendarType =` `UcValue =` `UcValue` |
| `iUchiwakeFrom` | GcComboBoxEx | — | — | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `ExGetValue()` |

### FormOutKaihiUchiwakeYear

ファイル: `FormOutKaihiUchiwakeYear.Designer.cs` / `FormOutKaihiUchiwakeYear.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiinFrom` | UcTableComboBox | — | — | — | `Value` |
| `iKaiinTo` | UcTableComboBox | — | — | — | `Value` |
| `iZaisekiFrom` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iZaisekiTo` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iTaishoYear` | UcDate | — | — | UcFormat=YYYYMMDD | `UcFormat =` `UcSideButton =` `UcValue =` `UcValue` |
| `iUchiwakeFrom` | GcComboBoxEx | — | — | — | `ExBeginUpdate()` `ExAddItem()` `ExEndUpdate()` `ExGetValue()` |

### FormOutKaihiYoteiYear

ファイル: `FormOutKaihiYoteiYear.Designer.cs` / `FormOutKaihiYoteiYear.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiinFrom` | UcTableComboBox | — | — | — | `Value` |
| `iKaiinTo` | UcTableComboBox | — | — | — | `Value` |
| `iZaisekiFrom` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iZaisekiTo` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DTypeZaiseki]` |
| `iIryoTo` | UcTableComboBox | — | — | — | `Value` |
| `iIryoFrom` | UcTableComboBox | — | — | — | `Value` |
| `iTaishoYM` | UcDate | — | — | UcFormat=YYYYMMDD | `UcFormat =` `UcDateCtl.DropDownCalendar.CalendarType =` `UcValue =` `UcValue` |

### FormOutSeikyuMinyukin

ファイル: `FormOutSeikyuMinyukin.Designer.cs` / `FormOutSeikyuMinyukin.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iKaiinFrom` | UcTableComboBox | — | — | — | `Value` |
| `iKaiinTo` | UcTableComboBox | — | — | — | `Value` |
| `iTaishoYM` | UcDate | — | — | UcFormat=YYYYMMDD | `UcFormat =` `UcDateCtl.DropDownCalendar.CalendarType =` `UcValue =` `UcValue` |
| `iShiharaiTo` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DShiharai]` |
| `iShiharaiFrom` | GcComboBoxEx | — | — | — | `ExGetValue()` `AppCombo.SetComboBox[enumKbn.DShiharai]` |

### FormSelectorBankCode

ファイル: `FormSelectorBankCode.Designer.cs` / `FormSelectorBankCode.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `iBank` | GcTextBoxEx | — | Ruby(20,40) ※直接適用 | — | `TextChanged +=` `Text` `Clear()` `Select()` |
| `iShiten` | GcTextBoxEx | — | Ruby(20,40) ※直接適用 | — | `TextChanged +=` `Text` `Clear()` |

### UcDate

ファイル: `UcDate.Designer.cs` / `UcDate.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `date` | GcDateEx | — | — | ContentAlignment=BottomCenter<br>Font=メイリオ 9pt | `Font =` `Value` `ExMinDate` `ExMaxDate` `Value =` `SingleBorderColor` `SingleBorderColor =` `DisabledForeColor` `DisabledForeColor =` `DisabledBackColor` `DisabledBackColor =` `SideButtons` `BackColor` `BackColor =` `BorderStyle` `BorderStyle =` `ReadOnly` `ReadOnly =` `Enabled =` `DropDownCalendar.MinDate` `DropDownCalendar.MaxDate` `DropDownCalendar.MaxDate =` `DropDownCalendar.MinDate =` `Enter +=` `ValueChanged +=` `Size =` `Select()` `SelectAll()` `Refresh()` `DropDownCalendar.HeaderFormat =` `Fields.Clear()` `DisplayFields.Clear()` `Fields.AddRange()` `DisplayFields.AddRange()` `DropDownCalendar.CalendarType =` `DropDownCalendar.YearMonthFormat =` |

### UcFileSelect

ファイル: `UcFileSelect.Designer.cs` / `UcFileSelect.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `txtSelect` | GcTextBoxEx | — | — | Enabled=false<br>Font=メイリオ 9.75pt | `Text =` `Text` `Click +=` `Leave +=` `AllowDrop =` `DragDrop +=` `DragEnter +=` `Width =` `Select()` `SelectAll()` `Text.Length` |

### UcTableComboBox

ファイル: `UcTableComboBox.Designer.cs` / `UcTableComboBox.cs`

| コントロール名 | 型 | DB バインド | 入力ルール | Designer での個別設定 | コードでの扱い |
| --- | --- | --- | --- | --- | --- |
| `cmb` | GcComboBoxEx | — | — | ContentAlignment=MiddleLeft<br>DropDown.AllowResize=false<br>HighlightText=true<br>ListHeaderPane.Height=19 | `BorderStyle` `BorderStyle =` `DisabledBackColor` `DisabledBackColor =` `DisabledForeColor` `DisabledForeColor =` `ExCompareValue` `ExCompareValue =` `Text` `ExDataSource =` `ListColumns.Count` `ListColumns` `Text =` `TextChanged +=` `TextSubItemIndex` `SelectedIndex =` `SelectedIndex` `DropDown.Size =` `DropDown.Size` `Text.Length` `TextSubItemIndex =` `SelectedItem` `SelectedItem.SubItems` `HighlightText =` `HighlightText` `ContentAlignment =` `ContentAlignment` `DroppedDown` `ExGetValue()` `ImeMode =` `BeginUpdate()` `EndUpdate()` `ListHeaderPane.Visible =` `Format =` `AutoSelect =` `AutoCompleteMode =` `AutoCompleteSource =` `DropDownStyle =` `DropDown.AllowResize =` `DropDown.ShowShadow =` `ScrollBarMode =` `TextChanging +=` `KeyPress +=` `KeyDown +=` `KeyUp +=` `DropDownOpening +=` `DropDownOpened +=` `DropDownClosing +=` `DropDownClosed +=` `SelectedIndexChanged +=` `PreviewKeyDown +=` `Validated +=` `Select()` `SelectAll()` `Items.Count` `SetBounds()` |
