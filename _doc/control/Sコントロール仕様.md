# S コントロール仕様

Gc 系コントロール(InputMan)の置き換えとして作った、名前が S で始まるコントロールの仕様をまとめる。置き換え前の使われ方は [GcControl仕様一覧.md](GcControl仕様一覧.md)、これらの部品で作った画面の仕様は [S画面仕様.md](S画面仕様.md) にある。

- 1 章: 全体(進み具合、ファイル、共通の見た目、SControlDB の設計方針、収まらない文字を縮める表示)
- 2 章: STextBox(GcTextBoxEx の代わり)
- 3 章: SComboBox(GcComboBoxEx の代わり)
- 4 章: STableComboBox(UcTableComboBox の代わり)
- 5 章: SNumberBox(GcNumberEx の代わり)
- 6 章: SDateBox(UcDate / GcDateEx の代わり)
- 7 章: SControlDB(GControlDB / GControlDBRule の代わり)
- 8 章: 確認した内容と、未確認の点のまとめ(レビューでの指摘と対応、Gc 系への依存の検査、テストの実行の方法を含む)
- 9 章: SToolTip(ComponentForm.ToolTip の代わり)
- 10 章: SGridDB(GGridDB の代わり。一覧の表示と、セルの中で入力する機能)
- 11 章: SMsgBox(ComponentForm.MsgBox の代わり)
- 12 章: SButton(画面の中に置く、ファンクション キー以外のボタン)
- 13 章: SCheckBox(チェック ボックス)と SRadioButton(ラジオ ボタン)
- 14 章: SProgressBar(進み具合のバー)と SBusyIndicator(回る輪)
- 15 章: SLabel(1 行の文字列を表示するラベル。入力必須の印を付けられる)
- 16 章: STabControl(タブでページを切り替えるコントロール)
- 17 章: SFileSelect(UcFileSelect の代わり。ファイル・フォルダーのパスの入力欄)

章と節の番号は、2026-11-26 に、文書ごとに 1 から付け直した(それまでは、S画面仕様.md と分ける前の、通しの番号だった)。この文書の中を指す参照は、「7.2.4」「10 章」のように番号だけで書く。S画面仕様.md の章・節を指すときは、「S画面仕様の 1.7」のように、文書の名前を付けて書く。章を足すときは、最後の章の次の番号にする。

2〜7 章は、部品ごとに「対応」「動作」「変えた点」「入れていないもの」「未確認の点」の順で書く。

| 項目 | 内容 |
| --- | --- |
| 記載日 | 2026-11-25。2026-11-26 に、その日までの変更を反映して、番号を付け直した |
| 状態 | 作成済み。これらの部品を使う画面の一覧は、[S画面仕様.md](S画面仕様.md) にある |
| 置き場所 | `FMKaiin/Controls/Common/`。コントロールの名前空間は `App.Controls`、SControlDB は `App`。このフォルダーにあるものは、最終的に comset へ移す予定 |

---

## 1. 全体

### 1.1 進み具合

| 置き換え前 | 置き換え後 | 状態 |
| --- | --- | --- |
| `GcTextBoxEx` | `STextBox` | 作成済み |
| `GcComboBoxEx` | `SComboBox` | 作成済み |
| `UcTableComboBox`(中身は `GcComboBoxEx`) | `STableComboBox` | 作成済み |
| `GcNumberEx` | `SNumberBox` | 作成済み |
| `GcDateEx` / `UcDate` | `SDateBox` | 作成済み |
| `GControlDB` / `GControlDBRule`(comset) | `SControlDB` / `SControlDBRule` | 作成済み(設計方針は 1.4、仕様は 7 章) |
| `AppDbRule`(入力ルールの定義) | `AppSControlRule` | 作成済み(7.2.4)。全 133 項目が `AppDbRule` と同じ内容であることを、テストで確かめている。新しいコードの採番(`AppDbID.GetNewCode`)も、桁数の上限をこちらから引く(7.2.7) |
| `AppTableCombo`(一覧の内容の設定) | `AppSTableCombo` | 銀行(S画面仕様の 1.2)・組コード・診療科・開設主体(S画面仕様の 6.1)・iFAX グループ(S画面仕様の 7.1)・会員(氏名を表示するものと、会員コードを表示するもの)・医療機関(S画面仕様の 10.1)・会費(`SetComboBox_Kaihi`、会費区分の列つきの `SetComboBox_Kaihi2`)・医師会・学会・施設業務の一覧を作成済み。ほかは、画面を移すときに足す |
| `GGridDB`(comset) | `SGridDB` | 一覧の表示と、セルの中で入力する機能(10.7)を作成済み(10 章)。文字列・数値・金額・日付・プログラムで作る文字列の列と、行ごとの見た目(`RowStyle`)、表示するデータの差し替え(`SetView`)がある(10.2) |
| `AppCombo`(区分の一覧の設定) | `AppSCombo` | 作成済み(S画面仕様の 2.1)。区分を「1：在籍」のように値と表示名を並べて表示する `SetComboBoxWithCode` もある(`AppCombo` には無い) |
| `YcCheckBoxEx`(チェック ボックス) | `SCheckBox` | 作成済み(13 章) |
| 標準の `RadioButton`(ラジオ ボタン) | `SRadioButton` | 作成済み(13.6) |
| 標準の `ProgressBar` | `SProgressBar` | 作成済み(14.1) |
| 画像を切り替える回転アイコン(`FormBg_Progress` の中) | `SBusyIndicator` | 作成済み(14.2) |
| `SingleLineLabel`(1 行のラベル) | `SLabel` | 名前を変えて、`Controls/Common/` へ移した(2026-11-26。1.2)。その後、入力必須の印を付ける `Required` を足した(15 章) |
| 標準の `Button`(画面の中のボタン) | `SButton` | 作成済み(12.1) |
| 標準の `TabControl`(タブでページを切り替える) | `STabControl` | 作成済み(16 章)。会員の入力画面(`FormKaiin2_DlgEntry`)で使っている |
| `UcFileSelect`(ファイルのパスの入力欄と「...」のボタン) | `SFileSelect` | 作成済み(17 章)。MAMIS の取込の画面(`FormMamisImport2`)で使っている |
| `ComponentForm.ToolTip`(comset) | `SToolTip` | 作成済み(9 章)。`AppToolTip` の中身を差し替えたので、既存の画面のツールチップも新しい見た目になる |
| `ComponentForm.MsgBox`(comset) | `SMsgBox` | 作成済み(11 章)。`AppMsgBox` の中身を差し替えたので、既存の画面のメッセージ ボックスも新しい見た目になる |

S 系の部品で作った画面の進み具合と、メニューからの開き方は、[S画面仕様.md](S画面仕様.md) の「画面の一覧」にある。

### 1.2 ファイル

すべて `Controls/Common/` にある。SControlDB のファイルは 7 章に書く。

| ファイル | 内容 |
| --- | --- |
| `SEditBase.cs` | 入力欄に共通の土台(枠の描画、全選択、サイド ボタン、入力の選別)。標準の `TextBox` を継承 |
| `STextBox.cs` | テキスト ボックス。`SEditBase` を継承 |
| `SNumberBox.cs` | 数値の入力欄。`SEditBase` を継承 |
| `SDateBox.cs` | 日付の入力欄。`SEditBase` を継承 |
| `SCalendarPopup.cs` | `SDateBox` のカレンダー。フォームから直接は使わない |
| `SDateText.cs` | 日付と文字列の変換(表示用・編集用の形、入力の解釈、元号の表)。`SDateFormat` / `SDateCalendarKind` を含む |
| `SComboBox.cs` | コンボ ボックス。標準の `ComboBox` を継承。項目のクラス `SComboItem` を含む |
| `STableComboBox.cs` | テーブルの行を選ぶコンボ ボックス。`SEditBase` を継承。列のクラス `STableComboColumn` を含む |
| `STablePopup.cs` | `STableComboBox` の一覧(見出し付き・複数列)。フォームから直接は使わない |
| `SPopupForm.cs` | 一覧・カレンダー・ツールチップに共通の、フォーカスを受け取らない小さなウィンドウの土台 |
| `SToolTip.cs` / `SToolTipPopup.cs` | ツールチップ(9 章) |
| `SMsgBox.cs` / `SMsgBoxButton.cs` | メッセージ ボックスと、そのボタン(11 章) |
| `SButton.cs` | 角丸のボタン(12.1)。メッセージ ボックスのボタン(`SMsgBoxButton`)は、これを継承している |
| `SCheckBox.cs` | チェック ボックス(13 章)。標準の `CheckBox` を継承 |
| `SRadioButton.cs` | ラジオ ボタン(13.6)。標準の `RadioButton` を継承 |
| `SCheckStyle.cs` | チェック ボックスとラジオ ボタンに共通の、既定の見た目と、配置・描画の処理。フォームから直接は使わない |
| `SProgressBar.cs` | 進み具合のバー(14.1)。標準の `ProgressBar` の代わり |
| `SBusyIndicator.cs` | 処理中であることを示す、回る輪(14.2) |
| `STabControl.cs` | タブでページを切り替えるコントロール(16 章)。標準の `TabControl` を継承 |
| `SFileSelect.cs` | ファイル・フォルダーのパスの入力欄(17 章)。`SEditBase` を継承。ダイアログの種類 `SFileDialogType` を含む |
| `SFittedFont.cs` | 文字列が幅に収まらないときに、収まる大きさまで縮めたフォントを用意する(1.5)。フォームから直接は使わない |
| `SPlaceholderFont.cs` | プレースホルダーを描くフォント(入力欄のフォントより 1.5 ポイント小さいもの)を用意する(2.2.3)。フォームから直接は使わない |
| `SGridDB.cs` / `SGridDBInput.cs` / `SGridDBColumns.cs` / `SGridStyle.cs` | グリッド(C1TrueDBGrid)と、列・データの結び付け。セルの中で入力する機能。列の定義。グリッドの既定の見た目(10 章) |
| `STableRowSource.cs` | `STableComboBox` が一覧に出す行の取り出しと検索。フォームから直接は使わない |
| `SInputFormat.cs` | 入力できる文字の種類を表す書式 |
| `SInputFilter.cs` | 入力する文字列の選別・自動変換・文字数の制限 |
| `SInputEnums.cs` | `SAllowSpace` / `SCrLfMode` / `SLengthUnit` |
| `SControlStyle.cs` | 既定の見た目(フォント・色・寸法)と、枠・矢印の描画 |
| `SControlInterfaces.cs` | コントロールが外へ見せる約束ごと。`ISEditCommit`(入力の途中の内容の確定)と `ISKeyUser`(フォームより先にキーを使う。S画面仕様の 1.3) |

S 系のコントロール・S 系の画面が使っている、既存の共通の部品。comset へ一緒に移せるよう、`Controls/` と `FormHelpers/` から `Controls/Common/` へ移した(2026-11-25。`SLabel`・`SectionPanel`・`DropShadow`・`IBackdropBlurSource`・`SmoothPictureBox` は、2026-11-26 に移した。移したときには、中身と名前空間は変えていない。`SLabel` には、その後 `Required` を足した。15 章)。

| ファイル | 内容 | 使っているところ |
| --- | --- | --- |
| `PaintHelper.cs` | 親コントロールと同じ見た目で背景を描く、などの描画の補助 | 角丸の外側の描画 |
| `LayoutScale.cs` | 表示倍率の取得 | 枠・余白・矢印の寸法 |
| `FontFallback.cs` | PC にあるフォントの中から、使えるものを選ぶ | 既定のフォント |
| `TitleBarColor.cs` | ウィンドウのタイトル バーの色を設定する(Windows 11 以降)。画面で共通の色(濃い紺色に白い文字)も、ここに持つ。2026-11-26 に `FormHelpers/` から移した(名前空間は `App` のまま) | `SMsgBox`。アプリの側では、`FormFrame`・`FormBg_Progress`・`FormMain2` |
| `MenuIconPainter.cs` / `MenuIconKind.cs` | アイコンの描画と、その種類 | `SDateBox` のカレンダーのボタン、`SToolTip` のアイコン |
| `SLabel.cs` | 1 行の文字列を表示するラベル(文字の高さを基準に配置するので、フォントが変わっても、隣の入力欄と高さがそろう)。入力必須の項目名に印を付けられる(15 章)。以前の名前は `SingleLineLabel` | S 系の画面の、見出し・項目名・入力欄の間の「－」・表示するだけの文字 |
| `SectionPanel.cs` | 見出し帯付きの角丸のパネル(半透明の色・影に対応) | S 系の画面の、入力欄を置くパネル(`secBody`) |
| `DropShadow.cs` / `IBackdropBlurSource.cs` | 影の描画と、背景のぼかしを提供するフォームの約束ごと | `SectionPanel` |
| `SmoothPictureBox.cs` | 画像を高品質に縮小して表示する | (いまは、どこも使っていない。共通の部品として一緒に移した) |
| `IconBox.cs` / `IconBoxBase.cs` | アイコンを 1 つ表示するコントロールと、その土台(背景と余白) | (S 系のコントロールは使っていない。共通の部品として一緒に移した) |

団体のロゴ(`OrganizationLogoShape`)は、このアプリに固有の図柄なので、共通の部品には入れていない。以前は、アイコンの種類の 1 つ(`MenuIconKind.OrganizationLogo`)として `MenuIconPainter` に組み込まれていたが、ロゴ専用のコントロール `IconLogo`(`Controls/`)に分けた。`IconLogo` は、`IconBox` と同じ土台(`IconBoxBase`)の上に作ってあり、`MenuIconPainter.FillShape`(アイコンと同じ大きさの決め方で、渡された形を塗る)でロゴを描く。分ける前後で、描いた結果が同じであることを画素ごとに比べて確かめた。

`Controls/Common/` は、このフォルダーのファイルと、comset・グリッド(`C1.Win.C1TrueDBGrid.4.dll`。`SGridDB` が使う)だけでコンパイルできる(GrapeCity の DLL を参照に入れずにコンパイルして確かめた)。新しく足すものも、FMKaiin に固有のもの(テーブルの項目名、`AppGlobal` など)と Gc 系に依存させないこと。

継承の関係:

```text
TextBox ── SEditBase ─┬─ STextBox
                      ├─ SNumberBox
                      ├─ SDateBox ───────┬─(カレンダー)── SCalendarPopup
                      │                  └─(文字列の変換)── SDateText
                      ├─ STableComboBox ─┬─(一覧)── STablePopup
                      │                  └─(行と検索)── STableRowSource
                      └─ SFileSelect
ComboBox ── SComboBox
TabControl ── STabControl
Control ── SLabel
Button ── SButton ── SMsgBoxButton
CheckBox ── SCheckBox ────┬─(配置・文字・色)── SCheckStyle
RadioButton ── SRadioButton ─┘
Form ─┬─ SMsgBox ──(ボタン)── SMsgBoxButton
      └─ SPopupForm ─┬─ STablePopup
                     ├─ SCalendarPopup
                     └─ SToolTipPopup ──(表示・非表示の管理)── SToolTip
```

文字コードは BOM 付き UTF-8、改行は CRLF。`FMKaiin.csproj` に登録済み。

`SEditBase` は、枠・全選択・サイド ボタン・入力の選別の仕組みだけを持ち、どの文字を入力できるかは継承したクラスが決める。文字の種類の指定(`Format` など)と IME の読みは `STextBox` だけが持つ。数値・日付・テーブル コンボは `STextBox` ではないので、`STextBox` を受け取る処理(`SControlDBText` など)に渡すと、コンパイルの時点でエラーになる。

### 1.3 共通の見た目

既定値は `SControlStyle.cs` にまとめてある。値を変えるときは、`SControlStyle.cs` と、各コントロールのプロパティに付けている `DefaultValue` 属性の両方を直す。

| 項目 | 既定値 |
| --- | --- |
| フォント | BIZ UDPゴシック 10.5pt。PC に無ければ 游ゴシック、それも無ければ Windows フォームの既定のフォント |
| 文字の色 | (31, 38, 62) |
| 枠線 | 角丸(半径 4)。色は (186, 197, 211)、太さ 1 |
| フォーカスがあるときの枠線 | 色は (64, 144, 232)、太さ 2(表示倍率 × 1.5 を四捨五入。最小 2) |
| 無効のときの背景 | (236, 238, 241)。灰色(フォーカスがあるときの背景色と見分けられるように) |
| 一覧で指している行の背景 | RoyalBlue (65, 105, 225)。文字は白(選択の色。下の表の `SelectionColor`) |
| 高さ | 28(`SComboBox` は `ItemHeight` = 22 で同じ高さになる) |
| 文字の左端 | 枠線の太さ + 6。どのコントロールでもそろえてある |
| プレースホルダーの文字 | 色は (200, 210, 226)(`SControlStyle.PlaceholderColor`)。フォントは、入力欄のフォントより 1.5 ポイント小さい(10.5 ポイントの欄で 9 ポイント。下限 7 ポイント。`SPlaceholderFont`) |
| 入力必須の印(`SLabel` の `Required`) | 半角の `*`。色は (214, 48, 49)(`SControlStyle.RequiredMarkColor`) |

寸法は 96 DPI 基準のピクセルで、描画時に表示倍率(`LayoutScale`)を掛ける。色は添付の画面イメージから拾った。

どのコントロールにも共通のプロパティ:

| プロパティ | 既定値 | 内容 |
| --- | --- | --- |
| `BorderStyle` | FixedSingle | None にすると枠線と角丸をなくす(グリッドのセルに重ねる入力欄用)。Fixed3D は FixedSingle と同じ |
| `BorderColor` | (186, 197, 211) | 枠線の色 |
| `FocusBorderColor` | (64, 144, 232) | フォーカスがあるときの枠線の色 |
| `FocusBackColor` | Lavender (230, 230, 250) | フォーカスがあるときの背景色。空にすると背景色を変えない。読み取り専用のときは変えない |
| `TextSelectionColor`(入力欄)/ `SelectionColor`(コンボの一覧) | RoyalBlue (65, 105, 225) | 選択の色。選択した文字、一覧で指している行、カレンダーで指している日の背景。その上の文字は、読める色(濃い色の上では白)で描く |
| `DisabledBackColor` | (236, 238, 241) | 無効(`Enabled = false`)のときの背景色 |
| `DisabledForeColor` | 空 | 無効のときの文字色。空なら `ForeColor` と同じ(薄い灰色にならない) |
| `CornerRadius` | 4 | 角の半径 |
| `PlaceholderText` / `PlaceholderColor` | 空 / (200, 210, 226) | 空欄・未選択のときに薄い色で表示する案内の文字と、その色(2.2.3、3.2) |
| `ShrinkTextToFit` | 部品ごとに違う(1.5) | 文字列が幅に収まらないときに、フォントを縮めて表示するか |

`Font` と `ForeColor` は、既定値のままならデザイナーが Designer.cs に書き出さない。

### 1.4 SControlDB の設計方針

`SControlDB` は、次の方針で作った(2026-11-25 に決定。できあがったものの仕様は 7 章)。

| 項目 | 方針 |
| --- | --- |
| データの持ち方 | `DataTable` を使い続ける。.NET の標準に含まれていて追加の依存が無く、廃止の予定も無い |
| 行の読み書き | `DataRow` を直接受け取らない。項目名で値を読む・書くだけのインターフェース(仮称 `ISRow`)を介す。`DataRow` を包む実装(仮称 `SDataRow`)を 1 つ用意する |
| フォーム側の書き方 | `GControlDB` と同じにする(`new` → `Add` → `EndAdd`、行を返す `getDataRow`)。`DataRow` を返すデリゲートも受け取れるようにして、包む処理は `SControlDB` の中で行う |
| 読み込みと書き戻し | `GControlDB` と同じく、登録した項目を `SControlDB` が自動で処理する。項目ごとの読み書きをフォームのイベントに書かせる形にはしない |
| 例外用のフック(`GControlDB` の `FetchRunEx` / `UpdateRunEx` / `FieldUpdated` に当たるもの) | 引数は `DataRow` ではなく `ISRow` にする |

インターフェースを介す理由: `GControlDB` が `DataRow` に対して行っているのは `row[項目名]` の読み書きだけである。依存をここに絞っておけば、データの持ち方を将来変えることになっても、差し替えるのは `ISRow` の実装だけで済む。

着手時に確認したこと:

- フックを使っている 4 か所のうち、`FormKaiin_DlgEntry.cs` の `KaisetsuShutaiEx` / `ByoshoUmuEx` / `KumiCodeEx` は、行を使っていない(渡された値を名称に変えるだけ)。`FormHikiotoshi_DlgEntry.cs` の `cmbUpdateRunEx` は、行を型付きのクラス(`t_hikiotoshi`)で包んで、別の項目(支払方法)に書き込んでいる。`row[項目名]` の書き込みに書き直せる内容で、`DataRow` のほかの機能は使っていない。
- 置き場所は FMKaiin(`Controls/Common/`)にした。S 系のコントロールが FMKaiin にあり、comset からは参照できないため。comset は変更していない。最終的には、S 系のコントロールと一緒に comset へ移す予定。

### 1.5 収まらない文字を縮める表示(ShrinkTextToFit)

表示する文字列が欄の幅に収まらないときに、収まる大きさまでフォントを縮めて表示する(2026-11-26)。`ShrinkTextToFit` プロパティで、欄ごとに切り替える。

| 部品 | 既定 | 縮める場面 |
| --- | --- | --- |
| `STextBox` | true | フォーカスが無い間と、無効の間。複数行(`Multiline`)の欄では縮めない |
| `STableComboBox` | true | 同上 |
| `SDateBox` | true | 同上(縮めるのは表示用の形。フォーカスがある間の編集用の形は、元の大きさ) |
| `SFileSelect` | true | 同上(土台の `SEditBase` の既定がそのまま効く) |
| `SNumberBox` | false | true にした欄だけ、同上。既定を false にしたのは、金額などで、欄によって文字の大きさが違うと、桁を見比べにくいため |
| `SComboBox` | true | 閉じた状態の表示。フォーカスがあっても縮める。開いた一覧は、元のフォントのまま |

| 項目 | 動作 |
| --- | --- |
| 縮め方 | 収まる中でいちばん大きいフォントにする。0.5 ポイント刻みで、下限は 8 ポイント(`SFittedFont.MinimumSize`) |
| 下限まで縮めても収まらない | 8 ポイントで表示し、末尾を「…」にする |
| 収まる | 元のフォントのまま(見た目は変わらない) |
| 値への影響 | 無い(表示だけを変える) |
| false にしたとき | `STextBox` などは、縮めずに標準のテキスト ボックスの表示のまま(収まらない分は見えない)。`SComboBox` は、縮めずに末尾を「…」にする |
| デザイナーへの書き出し | 既定値と違うときだけ書き出す(既定値が部品ごとに違うので、`DefaultValue` 属性ではなく `ShouldSerializeShrinkTextToFit` で判定している) |

`STextBox`・`STableComboBox`・`SDateBox`・`SNumberBox`・`SFileSelect` では、設定は土台の `SEditBase` の 1 か所にあり、部品ごとの既定値は `DefaultShrinkTextToFit`(protected virtual。`SNumberBox` だけが false を返す)で決める。標準のテキスト ボックスは、描くフォントを文字列ごとに変えられないので、縮めて表示する間は、入力部分を自前で描いている(背景と文字を、標準と同じ色で描く。上下は中央)。フォーカスが入ると標準の描画に戻り、元のフォントで、収まらない分は横に送られる。IME で変換している最中も縮めない。パスワードの欄(`PasswordChar`)では、伏せ字の文字列の幅で判定する。

フォントは、描くたびに作らないよう、直前の結果(文字列・幅・元のフォントの組)を `SFittedFont` が覚えている。

---

## 2. STextBox

`GcTextBoxEx`(`GcTextBox`)の代わり。共通の土台 `SEditBase` を経由して標準の `TextBox` を継承しているので、`Text`・`Enabled`・`ReadOnly`・`SelectAll()`・`Clear()`・`TextChanged`・`PasswordChar` などは標準のものがそのまま使える。

### 2.1 対応

| GcTextBoxEx | STextBox |
| --- | --- |
| `Format` | `Format`(同じ書き方。2.2.1) |
| `AllowSpace` | `AllowSpace`(型は `SAllowSpace`) |
| `AutoConvert` | `AutoConvert` |
| `AcceptsCrLf` | `AcceptsCrLf`(型は `SCrLfMode`) |
| `MaxLength` / `MaxLengthUnit` | `MaxLength` / `MaxLengthUnit`(型は `SLengthUnit`) |
| `HighlightText` | `HighlightText` |
| `ContentAlignment` | `ContentAlignment` |
| `DisabledBackColor` / `DisabledForeColor` | 同じ名前 |
| `SingleBorderColor` | `BorderColor` |
| `BorderStyle = None` | 同じ |
| フォーカス時の色変更(`ExFocusColor`、static) | `FocusBorderColor`、`FocusBackColor`(コントロールごとのプロパティ) |
| `ExFocusHighlight` | なし(Gc 側でも機能していなかった) |
| `ImeStr`(取得・設定) | `ImeReading` |
| `ExImeStr`(取得して消す) | `TakeImeReading()` |
| `ExCompositionEvent` | `ImeReadingCommitted`(引数は `SImeReadingEventArgs`。`Reading` と `Text`) |
| `IMECompositionStart` / `IMECompositionEnd` | なし(使っている箇所が無い) |
| `InvalidInput` | `InvalidInput` |
| F4 の抑止 | なし(標準のテキスト ボックスは F4 で何も起きない) |

### 2.2 動作

#### 2.2.1 入力できる文字の制限(Format)

`Format` に、次のキーワードを並べた文字列を指定する。並べたキーワードのどれかに当てはまる文字を入力できる。空文字列は制限なし。

| 半角 | 全角 | 入力できる文字 |
| --- | --- | --- |
| `A` | `Ａ` | 英大文字 |
| `a` | `ａ` | 英小文字 |
| `K` | `Ｋ` | カタカナ(長音・濁点を含む) |
| `N` | `Ｎ` | カタカナ。小書き(ァ ッ ャ など)を除く |
| `9` | `９` | 数字 |
| `#` | `＃` | 数字と `+ - $ % \ , .` |
| `@` | `＠` | 記号 |
| `B` | `Ｂ` | 0 と 1 |
| `X` | `Ｘ` | 16 進数(0〜9、A〜F、a〜f) |
| `S` | `Ｓ` | 空白 |
| `H` | — | すべての半角文字 |
| — | `Ｊ` | ひらがな |
| — | `Ｇ` | ひらがな。小書きを除く |
| — | `Ｚ` | すべての全角文字 |
| — | `Ｔ` | サロゲート ペアの文字 |
| — | `Ｄ` | サロゲート ペア以外の全角文字 |
| — | `Ｉ` | JIS X 0208 の文字 |
| — | `Ｍ` | Shift JIS の全角文字 |
| — | `Ｖ` | 異体字セレクター |

- `^` より後ろに書いたものは「入力できない文字」になる。`H^@` は半角文字のうち記号以外。
- `^` より前に何も書かなければ「後ろに書いたもの以外すべて」になる。`^@` は半角記号以外すべて。
- キーワードと同じ字そのものを指定するときは、前に `\` を付ける。
- 現在のルールで使っているのは `9`・`H`・`^@`・`Aa9`・空文字列。

キーワードの一覧は InputMan 8.0J のヘルプ([GcTextBox.Format](https://docs.mescius.jp/help/inputman-winforms-8/GrapeCity.Win.Editors.v80~GrapeCity.Win.Editors.GcTextBox~Format.html))に合わせた。

#### 2.2.2 空白・自動変換・改行・文字数

| 機能 | 動作 |
| --- | --- |
| 空白(`AllowSpace`) | None / Wide(全角だけ)/ Narrow(半角だけ)/ Both。空白を入力できるかどうかは `Format` ではなく、この設定だけで決まる |
| 自動変換(`AutoConvert`。既定 true) | 入力できない文字を、入力できる文字に変換する。変換は、全角と半角、英字の大文字と小文字、ひらがなとカタカナ、小書きのかなと通常のかなの間で行い、3 回まで重ねて試す。変換しても入力できない文字は捨てる |
| 自動変換の例 | `9` で「１」→「1」。`H` で「ア」→「ｱ」、「あ」→「ｱ」、「ガ」→「ｶﾞ」(2 文字になる)。`^@` で「-」→「－」。`A` で「a」→「A」。空白が Narrow なら全角の空白 → 半角の空白 |
| 改行(`AcceptsCrLf`) | 貼り付ける文字列に改行があるとき。Filter は改行だけを取り除く。Cut は最初の改行より後ろを捨てる。NoControl は複数行ならそのまま、1 行入力なら最初の改行より後ろを捨てる |
| 制御文字 | 改行とタブ以外の制御文字は入力させない。タブは、複数行の入力欄にだけ入れる(1 行の入力欄では捨てる。表計算ソフトの複数のセルを貼り付けると、セルの区切りとしてタブが入ってくるため)。Ctrl+BackSpace で届く DEL 文字も入れない(標準のテキスト ボックスは、目に見えない文字として挿入してしまう) |
| 最大文字数(`MaxLength`。既定 32767、0 は制限なし) | 超える分は入力されない。貼り付けでは末尾を削る。サロゲート ペアの途中では切らない |
| 数え方(`MaxLengthUnit`) | Char(既定。サロゲート ペアを 2 と数える。DB の文字数と一致)/ Byte(半角を 1、それ以外を 2)/ TextElement(サロゲート ペアや異体字付きの文字を 1 と数える。Gc のルールが使っていた数え方) |
| 入力されなかったとき | 1 文字も入力されなかった場合に `InvalidInput` イベントが発生する |

制限が掛かるのは、キー入力・IME の確定・貼り付け(Ctrl+V、右クリック メニュー)の 3 つ。

#### 2.2.3 そのほかの動作

| 機能 | 動作 |
| --- | --- |
| 全選択(`HighlightText`。既定 false) | フォーカスが移ってきたときに全選択する。クリックで移ってきた場合も全選択する(ドラッグで範囲を選んだ場合はその選択を残す)。アプリを切り替えて戻っただけでは選択し直さない。フォーカスがある間にプログラムから文字列や値を設定した場合も、全選択し直す(標準の `TextBox` は、設定されると選択を解除してカーソルを先頭に戻す。画面を開いた直後は、先頭の項目にフォーカスが入った後で値を表示するので、この対応が無いと全選択にならない)。数値・日付・テーブル コンボも同じ。Tab などで離れるときに表示用の形へ直す分は、全選択しない |
| IME の読み(`ImeReading`) | IME で文字を確定するたびに、その読みを後ろへ追加する。`Text` が空になると空に戻る。`TakeImeReading()` は取得と同時に空にする。読みの文字の種類(半角カタカナなど)は IME による |
| 文字の位置(`ContentAlignment`。既定 MiddleLeft) | 左右は標準の `TextAlign` と連動する。上下は 1 行入力のときだけ効く |
| 高さ | 自由に指定できる(`AutoSize` の既定を false にしてある)。既定の大きさは 150 × 28 |
| 無効のとき | 背景を `DisabledBackColor`、文字を `DisabledForeColor`(空なら `ForeColor`)で描く。文字の位置は有効のときと同じ |
| 読み取り専用のとき | 標準の `TextBox` と同じ。`BackColor` を指定していなければ背景が `SystemColors.Control` になる |
| 枠と余白の上でのクリック | 入力部分のクリックとして扱う(フォーカスが移り、近い位置にカーソルが入る) |
| 複数行(`Multiline`) | 使える。枠の内側全体が入力部分になる |
| 画面以外への描画(`DrawToBitmap`) | 枠も描かれる |
| フォーカスがあるときの背景色(`FocusBackColor`。既定 Lavender) | フォーカスがある間、入力欄の背景をこの色にする。読み取り専用・無効のときは変えない。空にすると使わない |
| `BackColor` と `NormalBackColor` | フォーカスがあるときの背景色で表示している間は、`BackColor` がその色を返す。設定してある色は `NormalBackColor` で読める。背景色を覚えておいて後で戻す処理は、`NormalBackColor` を読むこと(`SControlDB` の読み取り専用の切り替えは、そうしている) |
| 選択した文字の色(`TextSelectionColor`。既定 RoyalBlue) | 選択した文字の背景をこの色にし、文字はその上で読める色(濃い色の上では白)にする。1 行の入力欄でだけ効く(複数行では Windows の標準の色)。空にすると、Windows の標準の色(濃い青に白い文字)になる |
| プレースホルダー(`PlaceholderText`。既定は空。色は `PlaceholderColor`、既定 (200, 210, 226)) | 入力欄が空のときに、薄い色で表示する案内の文字。「入力必須」や入力例に使う。文字を入力すると消え、空に戻すとまた表示する。フォーカスがある間も表示する(入力済みの内容を消したその場で、案内が見えるようにするため)。無効・読み取り専用のときと、IME で変換している最中は表示しない。`Text` の値には影響しない。寄せ方は `TextAlign` に従う。フォントは入力欄のフォントより 1.5 ポイント小さく(下限 7 ポイント。`SPlaceholderFont.SizeReduction` の 1 か所で変えられる)、1 行の入力欄では上下の中央に置く。数値・日付・テーブル コンボ・`SFileSelect` でも使える(土台の `SEditBase` が持っている)。`SComboBox` にも同じ名前のプロパティがある(3.2)。必須の欄に入れる文字(テキストは「入力必須」、コンボは「選択必須」)は、画面の側の決まり(S画面仕様の 1.7) |
| 収まらない文字を縮める(`ShrinkTextToFit`。既定 true) | フォーカスが無い間と無効の間、収まらない文字列を、収まる大きさまで縮めて表示する(1.5) |

#### 2.2.4 継承して使うための仕組み(SEditBase)

ここに書くものは、土台の `SEditBase` が持っている。`STextBox` のほか、`SNumberBox`・`SDateBox`・`STableComboBox` が使う。

**サイド ボタン**: 枠の内側の右端に、ボタン用の領域を確保できる。`STableComboBox` が一覧を開く矢印に、`SDateBox` がカレンダーのボタンに使っている。

| メンバー(protected) | 内容 |
| --- | --- |
| `GetSideButtonWidth(scale)` | 領域の幅を返す。既定は 0(領域なし)。入力部分はこの幅の分だけ狭くなる |
| `PaintSideButton(g, bounds, backColor)` | 領域を描く |
| `OnSideButtonClick(e)` | 領域を左ボタンで押したときに呼ばれる。フォーカスは先に移してある |
| `UpdateFrameLayout()` / `RedrawFrame()` | 幅を変えたとき / 見た目を変えたときに呼ぶ |

領域の上では、マウス カーソルが矢印になる。

**入力の確認**: 文字の種類だけでは決められない規則(数値の形になっているか、など)を持つコントロールのために、挿入する直前の文字列を調べられる。`SNumberBox` が使っている。

| メンバー(protected) | 内容 |
| --- | --- |
| `InputFilter`(internal) | 入力の選別の規則(文字の種類・空白・自動変換・最大文字数)。継承したクラスが、自分のプロパティや固定の規則を設定する。`STextBox` は `Format` などのプロパティをここへつないでいる。`SNumberBox` と `SDateBox` は、コンストラクターで固定の規則を設定している |
| `ProcessInput(input)` | 入力規則を通った文字列を、挿入の直前に受け取る。戻り値が挿入される。空文字列を返すと何も挿入しない。キー入力・IME の確定・貼り付けのどれでも呼ばれる |
| `OnInvalidInput(e)` | `InvalidInput` イベントを発生させる。`ProcessInput` で受け付けなかったときに呼ぶ |
| `HighlightText`(virtual) | 継承先で既定値を変えられる |

#### 2.2.5 作りの要点

- 枠と余白は、ウィンドウの非クライアント領域(標準の枠線が描かれる場所)に自前で描いている。文字を入力する部分は枠の内側に縮めてあり、標準のテキスト ボックスがそのまま描く。
- 文字の選別は、標準のテキスト ボックスへ文字(`WM_CHAR`)が渡る直前に行う。キー入力も IME の確定も最後はここを通る。IME で確定した文字は、Windows フォームがいったんキューに積んでから届けるので、`KeyPress` の段階で選別すると、1 文字が 2 文字になる変換(「ガ」→「ｶﾞ」)で順序が入れ替わる。
- 無効のときは、標準のテキスト ボックスが文字色を変えられない(必ず薄い灰色になる)ので、無効の間だけ入力部分も自前で描いている。
- `MaxLength` は標準のプロパティを同じ名前で置き換えている(標準は文字単位でしか数えられないため)。Windows 側の制限は、数え方が Char のときだけ同じ値にして、二重の制限にしている。
- フォーカスがあるときの背景色は、`BackColor` が返す値を切り替えて実現している。標準のテキスト ボックスは、入力部分の背景を `BackColor` の値で塗り(Windows フォームの中の処理)、ほかの色を渡す手段が無いため。切り替えるたびに `BackColorChanged` イベントが発生する。
- 選択した文字の色は、標準の処理が描いた直後に、選択した範囲だけを上から描き直して実現している。標準のテキスト ボックスは、選択した文字を必ず Windows の標準の色で描き、色を指定する手段が無いため。標準の処理は、キー入力やマウス操作のたびに直接描くので、表示が変わりうるメッセージを処理するたびに描き直している。
- プレースホルダーも、標準の処理が描いた上から自前で描いている(.NET Framework のテキスト ボックスにはプレースホルダーの機能が無く、Windows の機能 `EM_SETCUEBANNER` は色を指定できず、複数行では使えないため)。文字だけを重ねると縁が濃くなるので、背景から描き直している。
- 収まらない文字を縮めて表示する間(1.5)は、`WM_PAINT` / `WM_PRINTCLIENT` で入力部分を自前で描く。無効のときの自前の描画も、同じく縮めたフォントで描く。

### 2.3 GcTextBoxEx と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | `Text` への代入は選別しない。Gc は、書式に合わない文字を代入時に落とす | DB の値を黙って書き換えないため |
| 2 | `Format` を変えても `Text` を消さない。Gc は空にする | 設定の順序で値が消えるのを避けるため |
| 3 | `KeyPress` のハンドラーには、変換前の文字が渡る | 2.2.5 の順序の問題を避けるため |
| 4 | フォーカスがあるときは、枠線の色と太さが変わり、背景が Lavender になる。Gc は背景色だけを変えていた | どの入力欄を操作しているかを分かりやすくするため |
| 10 | 選択した文字は、RoyalBlue の背景に白い文字で表示する。Gc と標準のテキスト ボックスは、Windows の標準の色(濃い青に白い文字) | コンボ ボックスの一覧で指している行と、同じ色にそろえるため |
| 5 | フォーカス時の色は、アプリ全体の static 設定ではなく、コントロールごとのプロパティ | デザイナーで設定できるようにするため |
| 6 | 無効のときの文字色の既定は `ForeColor`。Gc は既定が灰色で、54 か所で黒を指定していた | 表示欄として使うことが多いため |
| 7 | `MaxLength` の既定は 32767(標準の `TextBox` と同じ)。Gc は 0(制限なし) | 標準に合わせた |
| 8 | `MaxLengthUnit` の既定は Char。Gc のルールは TextElement を指定していた | DB の文字数と一致する数え方を既定にした。TextElement も指定できる |
| 9 | Ctrl+BackSpace では何も起きない | 標準のテキスト ボックスが挿入する DEL 文字が、数字だけの欄などにも入ってしまうため |

推定で実装した箇所(InputMan のヘルプに定義が見つからなかったもの):

| 箇所 | 実装 | 根拠 |
| --- | --- | --- |
| 空白と `Format` の関係 | 空白は `AllowSpace` だけで決める | 既存のルールが `Format = "Aa9"` に `AllowSpace = Narrow` を、`Format = "9"` に None を組み合わせている |
| `@`(記号)の範囲 | ASCII の記号と、半角の句読点(｡ ｢ ｣ ､ ･) | 定義が無いため、一般的な範囲にした |
| 自動変換の中身 | 2.2.2 のとおり | ヘルプには「入力マスクに従って自動的に変換する」としか書かれていない |

### 2.4 入れていないもの

| 内容 | 理由 |
| --- | --- |
| `SetEnterHighlight()` / `SetLeaveHighlight()` | フォーカス時の見た目はコントロールの中で処理している。外から呼んでいる箇所も無い |
| GcTextBox のドロップダウン、サイド ボタン(Gc の機能としてのもの)、代替テキスト、ショートカット | 現在のフォームで使っていない |
| `Text` 代入時の選別 | 2.3 の 1 |
| IME の変換の開始・終了のイベント | 現在のフォームで使っていない。いったん作ったが削除した |

### 2.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実機の IME での入力。変換・確定の経路は、ウィンドウ メッセージを直接送る形でしか試していない |
| 2 | IME の読みの取得。取得の方式は GcTextBoxEx と同じ(IMM の `GCS_RESULTREADSTR`)なので、新しい Microsoft IME で読みが取れない場合があるという制約も同じはず |
| 3 | Visual Studio のデザイナーへの配置。既定値を Designer.cs に書き出さないことは、判定の処理をテストしただけ |
| 4 | 複数行でスクロール バーを表示した場合の見た目 |
| 5 | `FormFrame` の DPI 拡大(`scaleForDeviceDpi`)との組み合わせ |
| 6 | 親コントロールの背景が半透明(`SectionPanel` など)のときの、角丸の外側の見た目 |
| 7 | 表示倍率 120%(115 DPI)以外での見た目 |
| 8 | 実際の貼り付け(Ctrl+V、右クリック メニュー)。利用者のクリップボードを書き換えないよう、検証では貼り付けを行っていない。貼り付ける文字列を選別する処理そのものはテストした |
| 9 | 選択した文字の色の、実際の操作での見え方。マウスのドラッグでの範囲選択、Shift + 矢印での選択、IME での変換中に、標準の色が一瞬見えたり、描き残しが出たりしないか。確認したのは、プログラムから選択した状態のスクリーンショットだけ |

---

## 3. SComboBox

`GcComboBoxEx`(`GcComboBox`)の代わり。標準の `ComboBox` を継承しているので、`Items`・`SelectedIndex`・`SelectedIndexChanged`・`SelectedValueChanged`・`BeginUpdate()` / `EndUpdate()`・`DroppedDown`・`DataSource` などは標準のものがそのまま使える。

フォームに直接置いている 52 個(すべて「一覧から選ぶだけ」の 1 列表示)が対象。`UcTableComboBox` の置き換えは 4 章の `STableComboBox`。

### 3.1 対応

| GcComboBoxEx | SComboBox |
| --- | --- |
| `ExBeginUpdate()` / `ExEndUpdate()` | 標準の `BeginUpdate()` / `EndUpdate()` |
| `ExClearItem()` | 標準の `Items.Clear()` |
| `ExAddItem(表示, 値)` | `AddItem(表示, 値)` |
| `ExGetValue()` | `SelectedValue`(取得) |
| `ExGetValue(index)` | `GetValue(index)` |
| `ExSetSelectedIndexByValue(値)` | `SelectedValue = 値`、または `SelectByValue(値)`(見つかったかを返す) |
| `ExSetSelectedIndexByContent(文字列)` | `SelectByText(文字列)` |
| `ExGetContent()` / `ExGetContent(index)` | 標準の `Text` / `GetText(index)` |
| `ExDataSource` | 標準の `DataSource` |
| `ExCompareValue` / `ExCompareContent` | 標準の `ValueMember` / `DisplayMember` |
| `ReadOnly` | `ReadOnly` |
| `ExOpenBySpaceKey` | `OpenBySpaceKey` |
| F4 の抑止 | `OpenByF4Key`(既定 false) |
| `SingleBorderColor`、フォーカス時の色、`DisabledBackColor` など | 1.3 の共通プロパティ |
| `DropDownStyle = DropDownList` | 既定値(Designer.cs に書き出されない) |
| `FlatStyle = Flat`、`ListHeaderPane.Visible = false` | 不要 |
| `ExItems` | 標準の `Items`(中身は `SComboItem`) |

`AppCombo.SetComboBox` に当たる処理は、次の形になる。

```csharp
cmb.BeginUpdate();
cmb.Items.Clear();
cmb.AddItem("正会員", 1);
cmb.AddItem("準会員", 2);
cmb.EndUpdate();

cmb.SelectedValue = 2;          // 値で選択
object value = cmb.SelectedValue;   // 選択中の値
```

### 3.2 動作

| 機能 | 動作 |
| --- | --- |
| 項目 | `AddItem(表示, 値)` で、表示する文字列と内部の値の組(`SComboItem`)を追加する。追加した時点で一覧に入る |
| `SelectedValue` | 選択中の項目の値。未選択なら null。設定すると、その値の項目を選択する。`AddItem` の項目でも、`DataSource`(`ValueMember` の列の値)でも同じように使える |
| 値の比較 | 型が違っても、文字列にして同じなら一致とみなす(項目が int の 1 で、DB の値が short の 1 や文字列の "1" でも選択できる) |
| 該当なし | 該当する項目が無い値、null、`DBNull` を設定すると、未選択(`SelectedIndex = -1`、表示は空)になる |
| `ReadOnly`(既定 false) | 利用者が選択を変えられなくなる。クリック、↑ ↓ PageUp PageDown Home End、Space、F4、文字キーでの項目の検索、ホイールが効かず、一覧も開かない。プログラムからの設定はできる。フォーカスは受け取る。矢印を薄く表示する |
| `OpenBySpaceKey`(既定 true) | Space で一覧を開閉する。Space は項目の検索(文字キーでその文字から始まる項目を選ぶ、標準の動作)には使われなくなる |
| `OpenByF4Key`(既定 false) | false のとき、F4 で一覧が開かない。F4 を画面のファンクション キーに使うため |
| 高さ | `ItemHeight` で決まる(コントロールの高さは `ItemHeight` + 6)。既定は 22 で、高さ 28。DPI の拡大に追従する |
| 一覧 | 行の高さは `ItemHeight`。指している行を `SelectionColor`(文字は `SelectionForeColor`。空なら `ForeColor`)で描く。文字の左端は、閉じた状態の文字とそろえてある |
| 無効のとき | 背景を `DisabledBackColor`、文字を `DisabledForeColor`(空なら `ForeColor`)で描く。矢印を薄く表示する |
| 見た目の描き方 | `DropDownStyle` が DropDownList(既定)のとき、閉じた状態の枠・文字・矢印をすべて自前で描く |
| プレースホルダー(`PlaceholderText`。既定は空。色は `PlaceholderColor`、既定 (200, 210, 226)) | 何も選択していないとき、または、選択中の項目の文字列が空のとき(未指定を表す空欄の項目を選んでいるとき)に、閉じた状態の文字の位置へ、薄い色で表示する。フォントは入力欄のプレースホルダーと同じく 1.5 ポイント小さい(1.3)。フォーカスがある間も表示する。無効・読み取り専用のときは表示しない。選択の状態には影響しない。DropDownList のときだけ表示する(自前で描くのがその形式だけのため) |
| 収まらない文字を縮める(`ShrinkTextToFit`。既定 true) | 選択中の項目の文字列が、矢印を除いた幅に収まらないとき、収まる大きさまで縮めて描く(1.5)。フォーカスの有無によらず縮める。開いた一覧は元のフォントのまま。false にすると、縮めずに末尾を「…」にする |

### 3.3 GcComboBoxEx と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 項目は `AddItem` を呼んだ時点で一覧に入る。Gc は `ExBeginUpdate` の後でしか追加できず、`ExEndUpdate` で反映される | 標準の `Items` をそのまま使うため。`BeginUpdate` / `EndUpdate` は描き直しをまとめるためのもので、省略できる |
| 2 | `SelectedValue` を、`DataSource` が無くても使えるようにした(標準のプロパティを同じ名前で置き換え) | 値での取得・選択を、標準と同じ名前で書けるようにするため |
| 3 | `DrawMode` は固定(OwnerDrawFixed)で、変更できない | 項目を自前で描くため |
| 4 | フォーカスがあるときは、枠線の色と太さが変わる | `STextBox` と同じ |
| 5 | 値の比較で文字列にするとき、書式を固定(カルチャに依存しない)にしている。Gc は `ToString()` | PC の地域設定で結果が変わらないようにするため。整数・真偽値・文字列では結果は同じ |
| 6 | 未選択のときに、プレースホルダーを表示できる(`PlaceholderText`) | 必須の欄が未選択であることを、テキストの欄と同じ見た目で示すため(2026-11-26) |
| 7 | 選択中の項目の文字列が収まらないときは、フォントを縮めて表示する(既定) | 長い名称が欄の途中で切れるのを減らすため(2026-11-26。1.5) |

### 3.4 入れていないもの

| 内容 | 理由 |
| --- | --- |
| 複数列の一覧、列の見出し、入力による絞り込み | `STableComboBox`(4 章)にある |
| 文字を入力できる形式(`DropDownStyle` が DropDown / Simple)の見た目 | この形式にすると、標準のコンボ ボックスの見た目になる。フォームに直接置いた 52 個はすべて DropDownList |
| アイコン付きの項目(`ExAddItem(icon, …)`) | 現在どこでも使っていない |
| `Format` / `MaxLength` / `AllowSpace` / `HighlightText` | 文字を入力できる形式で使うもの。コンボの項目には入力ルールが 1 つも登録されていない |

### 3.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | Visual Studio のデザイナーへの配置(既定値を書き出さないことは、判定の処理をテスト済み) |
| 2 | `FormFrame` の DPI 拡大下での高さ。`ItemHeight` を拡大率に追従させる処理は `BorderedComboBox` と同じものを入れている |
| 3 | `DataSource` を使っているときの未選択(`SelectedIndex = -1`)。標準のコンボ ボックスには、フォームの表示時に先頭の行が選び直される癖があり、その対処は入れていない |
| 4 | 親コントロールの背景が半透明のときの見た目、表示倍率 120% 以外での見た目 |
| 5 | プレースホルダーを、初期値の入る欄(医療機関マスタの入力の病床有無・退会区分)で表示した状態。この 2 つは追加のときも初期値が入るので、画面では確かめていない(部品のテストでは確かめた) |

---

## 4. STableComboBox

`UcTableComboBox` の代わり。テーブルの行を、見出し付き・複数列の一覧から選ぶ。文字を入力すると、その文字を含む行だけに一覧を絞り込む。

`STextBox` と同じ土台(`SEditBase`)の上に作っているので、見た目、`ImeMode`、`ReadOnly`、無効のときの色は `STextBox` と同じ。右端の矢印は、土台のサイド ボタン(2.2.4)。入力した文字は検索に使うので、文字の種類を制限するプロパティ(`Format` など)は持たない。

### 4.1 対応

| UcTableComboBox | STableComboBox |
| --- | --- |
| `DBView` | `DataSource`(`DataTable` を渡す。専用のビューを内部で作る) |
| `SetColumn(列, 見出し, 幅, 位置)` | `AddColumn(列, 見出し, 幅, 位置)`。位置は `HorizontalAlignment` |
| `TextSubItemIndex`(表示中の n 番目の列) | `DisplayMember`(列の名前) |
| `CompareValue` | `ValueMember` |
| `Value` | `SelectedValue`(取得) |
| `ComboBox.ExSetSelectedIndexByValue(値)` | `SelectedValue = 値`、または `SelectByValue(値)` |
| `Text`(取得・設定) | `Text` |
| `Find` / `Search(列, 値)` | `SelectByText(文字列)` / `SelectByValue(値)` |
| `RowFilter` / `Sort` | 同じ名前 |
| `SelectedIndexNullLeave` | `FallbackSelectedIndex` |
| `DropDownSize` | `DropDownWidth`(幅)と `MaxDropDownItems`(行数) |
| `DroppedDown`(取得だけ) | `DroppedDown`(設定もできる) |
| `SelectIndexChanged` | `SelectedIndexChanged`、`SelectedValueChanged` |
| `TextChanged` | 標準の `TextChanged` |
| `ValidateComboBox` | なし。`DropDownClosed` と、標準の `Validated` を使う |
| `ComboBox`(中の `GcComboBoxEx`) | なし(コントロール自身が入力欄) |
| `ComboBox.ImeMode` / `EnterImeMode` | 標準の `ImeMode` |
| `HighlightText` | `HighlightText`(既定 true) |
| `ContentAlignment`、`ComboBorderStyle`、`DiabledBackColor`、`DisabledForeColor` | `ContentAlignment`、`BorderStyle`、`DisabledBackColor`、`DisabledForeColor` |
| `BeginUpdate()` / `EndUpdate()` | 不要 |
| `IconVisible`(情報アイコン) | なし(4.4) |
| `TextSubItemMatch` | なし。`SelectedRow != null` で判定できる |

`AppTableCombo.SetComboBox_Bank` に当たる設定は、次の形になる。

```csharp
cmb.DataSource = AppGlobal.Banks.DbView.DataTable;
cmb.Sort = t_bank.FBNK_Code;
cmb.AddColumn(t_bank.FBNK_Code, "銀行ｺｰﾄﾞ", 80, HorizontalAlignment.Right);
cmb.AddColumn(t_bank.FBNK_CodeShiten, "支店ｺｰﾄﾞ", 80, HorizontalAlignment.Right);
cmb.AddColumn(AppTableCombo.Fld_BankName, "銀行名", 310);
cmb.DisplayMember = AppTableCombo.Fld_BankName;
cmb.ValueMember = t_bank.FID_Bank;
cmb.ImeMode = ImeMode.Hiragana;
```

### 4.2 動作

#### 4.2.1 データと列

| 機能 | 動作 |
| --- | --- |
| `DataSource` | 一覧のもとになるテーブル。行の追加・変更・削除は、一覧に自動で反映される |
| `RowFilter` / `Sort` | 一覧に出す行の条件と並び順。`DataView` と同じ書き方。ほかの画面のビューには影響しない |
| 列(`AddColumn`) | 追加した順に左から並ぶ。幅は 96 DPI 基準のピクセル。幅の合計が一覧の幅より狭ければ、最後の列が広がる。テーブルに無い列は出さない。列を 1 つも追加しなければ、`DisplayMember` の列だけを出す。追加した列は `Columns`(`STableComboColumn` の一覧)で読め、`ClearColumns()` ですべて外せる |
| `DisplayMember` | 選択した行の、入力欄に表示する列。空なら一覧の最初の列 |
| `ValueMember` | `SelectedValue` で値として使う列 |
| 選べない行 | `DisplayMember` の列が空の行は、一覧に出さない |

#### 4.2.2 選択

| 機能 | 動作 |
| --- | --- |
| `SelectedValue` | 選択中の行の `ValueMember` の値。未選択なら null。設定すると、その値の行を選択して、入力欄をその行の文字列にする。値の比較は `SComboBox` と同じ |
| 該当なし | 該当する行が無い値、null、`DBNull` を設定すると、未選択にして入力欄を空にする |
| `Text` の設定 | 表示する文字列が一致する行を選択する。該当する行が無ければ、未選択にして、渡された文字列をそのまま表示する |
| `SelectedIndex` | 選択中の行の位置(絞り込んでいないときの、上からの順番)。設定もできる |
| `SelectedRow` / `RowCount` / `GetValue(index)` / `GetText(index)` | 選択中の行(`DataRow`)/ 選べる行の数 / 指定位置の値 / 指定位置の表示文字列 |
| 選択中の行が消えた | 行が削除された、または条件(`RowFilter`)から外れた場合は、選択が外れる。入力欄の文字列は残る |
| イベント | 選択している行が変わると、`SelectedIndexChanged`、`SelectedValueChanged` の順に発生する。利用者の操作でも、プログラムからの設定でも発生する |

プログラムから選択を設定したときは、一覧は開かない。

#### 4.2.3 入力による絞り込み

| 場面 | 動作 |
| --- | --- |
| 文字を入力した | 選択が外れる(`SelectedValue` が null になる)。一覧に出している列のどれかに当てはまる行だけを一覧に出し、先頭の行を指す。該当する行が無ければ一覧を閉じる |
| 当てはまる条件 | 文字列の列は、入力が 1 文字なら先頭の一致、2 文字以上なら途中の一致も含める。数値などの列は、常に先頭の一致 |
| 区別しないもの | ひらがなとカタカナ、全角と半角、英字の大文字と小文字 |
| 確定(Enter、行のクリック) | 指している行を選択し、入力欄をその行の文字列にして、全選択にする |
| フォーカスがほかのコントロールへ移った | 入力の途中なら、指している行(一覧が閉じていれば、絞り込んだ結果の先頭の行)を選択する。ほかのコントロールの `Validating` より前に確定する |
| 何も選択しないままフォーカスが移った | `FallbackSelectedIndex` が 0 以上ならその位置の行を選択する。-1(既定)なら入力欄を空にする |
| Esc | 入力を取り消して、入力を始める前の選択と文字列に戻す |
| ほかのアプリへ切り替えた | 一覧だけ閉じる。入力の途中の状態は残る |

#### 4.2.4 一覧の開閉とキー・マウス

| 操作 | 動作 |
| --- | --- |
| ↑ ↓ PageUp PageDown | 一覧が閉じていれば開く。開いていれば、指している行を移動する。入力欄の文字列は変えない |
| Alt+↓ / Alt+↑ | 一覧を開閉する |
| Enter | 一覧が開いているときだけ、確定に使う。このときの Enter は、フォームの処理(次の項目への移動など)へ渡さない |
| 右端の矢印のクリック | 一覧を開閉する。選択は変えない |
| 一覧の上でのマウス移動 / クリック | 指している行が追従する / その行で確定する |
| ホイール | 一覧が開いていれば、一覧をスクロールする。閉じていれば何もしない |
| 一覧の外側のクリック、親のフォームの移動・大きさの変更・非アクティブ化 | 一覧を閉じる |
| 読み取り専用・無効のとき | 一覧は開かない。矢印を薄く表示する。読み取り専用のときは、フォーカスが移っても選択を変えない |

一覧の見た目:

| 項目 | 内容 |
| --- | --- |
| 位置 | 入力欄のすぐ下。画面の下にはみ出す場合は上 |
| 幅(`DropDownWidth`。既定 0) | 0 なら、列の幅の合計とコントロールの幅の、広いほう |
| 高さ(`MaxDropDownItems`。既定 8) | 見出し + 行数分。行数が上限を超えるときはスクロール バーを出す |
| 見出し(`HeaderVisible`。既定 true) | 列の見出しを中央寄せで表示する |
| 指している行 | `SelectionColor` で塗る |
| 影 | あり |

一覧は、フォーカスを受け取らない別のウィンドウである。開いている間も、キー入力は入力欄に届く。

入力欄の表示:

| 項目 | 内容 |
| --- | --- |
| 収まらない文字(`ShrinkTextToFit`。既定 true) | フォーカスが無い間と無効の間は、選択した行の文字列が収まらなければ、フォントを縮めて表示する(1.5)。フォーカスがある間は、入力しやすいように元の大きさで表示する |
| プレースホルダー(`PlaceholderText`) | 入力欄が空のときに表示する(2.2.3)。必須の欄では「選択必須」を入れる(S画面仕様の 1.7) |

#### 4.2.5 行数の多いテーブルでの速さ

数万行のテーブルでも入力のたびに待たされないよう、次のものを一度だけ作って使い回す(`STableRowSource`)。どれも、最初に必要になった時点で作り、テーブルの行や設定(条件・並び順・列)が変わったら捨てる。

| 使い回すもの | 使う場面 |
| --- | --- |
| 選択できる行の一覧と、行から位置を引く表 | `RowCount`、`SelectedIndex`、選択中の行が一覧に残っているかの確認 |
| 値から行を引く表 | `SelectedValue` の設定、`SelectByValue` |
| 表示する文字列から行を引く表 | `Text` の設定、`SelectByText` |
| 絞り込み用に表記をそろえた、各行・各列の文字列 | 入力による絞り込み |

そのほかの工夫:

- 一覧は、表示している行(既定で 8 行)だけを描く。行数が増えても描画の時間は変わらない。
- テーブルを一括で読み込むと、変更の通知が 1 行ごとに届く。通知を受けた時点では使い回しているものを捨てるだけにして、作り直しは次に必要になったときに 1 回だけ行う。
- 絞り込み用の文字列は、行数 × 一覧の列数の分だけメモリに持つ。

開発 PC での実測(一覧の列は数値 1 + 文字列 2。最適化なしのビルド):

| 操作 | 3 万行 | 10 万行 |
| --- | ---: | ---: |
| 文字を入力(最初の 1 文字。絞り込み用の文字列を作る) | 66 ms | 161 ms |
| 文字を入力(2 文字目以降) | 4〜7 ms | 8〜11 ms |
| `SelectedValue` の設定(最初の 1 回。表を作る) | 4 ms | 18 ms |
| `SelectedValue` の設定(2 回目以降) | 0.1 ms | 0.1 ms |
| `SelectedValue` の取得 | 0.2 ms | 0.2 ms |
| 一覧を開く(全行) | 5 ms | 5 ms |
| `RowFilter` の設定 | 16 ms | 68 ms |
| 1 行を書き換えた後の、最初の `SelectedValue` の取得 | 6 ms | 15 ms |
| テーブルへの一括読み込み(コントロールを付けた状態) | 80 ms | 236 ms |

一括読み込みは、同じテーブルに通常の `DataView` を 1 つ付けただけの場合で 43 ms / 210 ms だった。コントロールが付いていることによる増分は小さい。

### 4.3 UcTableComboBox と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 入力中の一覧は、先頭の行を最初から指す。現行は「指さないが、閉じるときに未選択なら先頭を選ぶ」 | 確定される行は同じ。何が選ばれるかが見えるようにした |
| 2 | Esc で、入力を取り消して元の選択に戻す。現行は Esc でも先頭の行が選ばれる | 取り消しの手段が無かったため |
| 3 | 入力を始めた時点で選択が外れ、確定の操作をするまで選択しない。現行は、該当が 1 件だけになるとその時点で選択される | 入力の途中で値が変わるタイミングを一定にするため |
| 4 | ひらがな・カタカナに加えて、全角・半角と大文字・小文字も区別しない | 絞り込みの判定を、表記をそろえてから比べる方式にしたため。そろえ方は、Unicode の互換分解(全角の英数字 → 半角、半角カタカナ → 全角カタカナ、濁点付きの文字 → 文字 + 濁点)の後、ひらがなをカタカナに、英字を大文字にする |
| 5 | 半角記号の入力を禁止していない(現行は `Format = "^@"`) | 絞り込みを `RowFilter` の式ではなくコードで判定するので、`%` などが入っても問題ない |
| 6 | 列は `AddColumn` を呼んだ順に並ぶ。現行はテーブルの列順 | 列の順序を呼び出し側で決められるようにするため |
| 7 | 一覧を閉じているとき、ホイールでは何も起きない。現行は選択が変わるため、対策の処理(メッセージ フィルター)を入れていた | 入力欄が標準のテキスト ボックスなので、もともと起きない |
| 8 | 文字を入力していなくても、一覧を開いて矢印キーで行を指し、Tab などでフォーカスを移すと、指している行を選択する。一覧の外をマウスでクリックした場合は、一覧が先に閉じるので選択は変わらない | 標準のコンボ ボックスと同じ動きにするため |
| 9 | 入力の途中の内容を、フォーカスを動かさずに確定するメソッド `CommitEdit()` がある(`ISEditCommit`)。フォーカスが移ったときと同じ処理をする | 入力を始めると選択が外れるので(3)、ファンクション キーでの保存のようにフォーカスが動かない処理が、そのまま値を読むと「未選択」になってしまう。`SControlDB` の書き戻しは、読む前に自動で呼ぶ。`SControlDB` を使わずに値を読む処理は、読む前に呼ぶこと |
| 10 | 選択中の行が一覧から外れたとき(削除された、条件から外れた、表示する文字列が空になった)は、選択を外して、選択の変更イベントを発生させる。確かめるのは、テーブルの変更の通知が終わった後、または選択を読んだ時点 | テーブルが変更を通知している最中にイベントを発生させると、イベントを受けた側がテーブルを書き換えたときに、変更の途中のテーブルを触ることになるため |
| 8 | `Text` を空にしても、データの絞り込みや選択の検索をし直さない | 選択を行(`DataRow`)で覚えているので、絞り込みで位置がずれても選択を失わない |
| 9 | 一覧の大きさは、幅(`DropDownWidth`)と行数(`MaxDropDownItems`)で指定する。現行は幅と高さ(`DropDownSize`) | 行の高さがフォントと表示倍率で変わるため |

表示する行が無いときに一覧を開こうとした場合(矢印のクリック、↓ など)は、行の代わりに理由を表示した一覧を開く。現行は、何も起きない。

| 場面 | 表示する文字列 | プロパティ |
| --- | --- | --- |
| 選択できる行が 1 つも無い | 選択できるデータがありません | `NoDataText` |
| 入力した文字に当てはまる行が無い | 該当するデータがありません | `NoMatchText` |

文字を入力している間は、当てはまる行が無くなると一覧を閉じる(これは変えていない)。その状態で ↓ を押すと、上の文字列を表示する。この一覧では、Enter・Esc・一覧の外のクリックで閉じるだけで、何も選択されない。

### 4.4 入れていないもの

| 内容 | 理由・扱い |
| --- | --- |
| 一覧の大きさをマウスで変える機能(現行は `DropDown.AllowResize = true`) | 必要なら後から追加する |
| 情報アイコン(現行は `UcTableComboBox` の中にある `IconBox`) | 添付の画面イメージではラベルの横にあるので、フォーム側に `IconBox` を置く想定 |
| グリッドとの連携(`UcInputControl_Super.GridMovePermission`) | 入れていない。グリッドのセルに重ねて使うこと自体は、できる(10.7。`BorderStyle` を None にして、列の `InputControl` に指定する) |
| `ValidateComboBox` イベント | `DropDownClosed` と標準の `Validated` で代用できる |
| ドロップダウンの高さの記憶(現行もコメントアウトされている) | 使っていない |

### 4.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実機の IME での入力(ひらがなで入力して絞り込む操作は、文字を直接送る形で確認した) |
| 2 | Visual Studio のデザイナーへの配置 |
| 3 | `FormFrame` との組み合わせ。一覧の確定に使った Enter を「次の項目への移動」に渡さない処理は入れたが、実際のフォームでは試していない。`FormFrame.CheckEnabledFormKeyPreview` は `GcComboBoxEx` / `GcDateEx` しか見ていないので、S 系への対応が要る |
| 4 | (確認済み)グリッドのセルに重ねる使い方。医療機関マスタの入力の画面(S画面仕様の 6 章)で、見本のデータを使って確かめた |
| 5 | 行数の多いテーブルは、検証用のデータ(3 万行・10 万行)で速さを測っただけで(4.2.5)、実際のデータ(銀行コードの全件など)では試していない。テーブルを書き換えた直後の最初の 1 文字は、絞り込み用の文字列を作り直すので、その分の時間が掛かる |
| 6 | フォームの中に埋め込んだフォーム(`IsDialogForm = false`)での、一覧の位置と閉じ方 |
| 7 | 複数のディスプレイ、表示倍率 120% 以外 |
| 8 | 一覧を開いたままフォームを閉じた場合 |
| 9 | 収まらない文字を縮める表示を、グリッドのセルに重ねたテーブル コンボ(医療機関マスタの入力の標榜科目)で見た場合。画面では確かめていない |
| 10 | ▼ のある欄でのプレースホルダーの見た目は、会員の欄(年間会費徴収書類)を画像で確かめた。ほかの画面の欄での見た目は、確かめた記録が無い |

---

## 5. SNumberBox

`GcNumberEx`(`GcNumber`)の代わり。数値を入力する。値は `Value`(`decimal?`)で読み書きし、未入力は null。

`STextBox` と同じ土台(`SEditBase`)の上に作っているので、見た目、無効のときの色、`ReadOnly`、`BorderStyle` などは `STextBox` と同じ。数字以外を入力させない処理は、土台の入力の選別と確認(2.2.4)を使っている。文字の種類を指定するプロパティ(`Format` など)は持たない。

### 5.1 対応

| GcNumberEx | SNumberBox |
| --- | --- |
| `Value`(`decimal?`) | `Value` |
| `ValueChanged` | `ValueChanged` |
| `ExMaxValue` / `ExMinValue` | `MaxValue` / `MinValue` |
| 小数の桁数(`ExMaxValue` の小数部分の桁数から決まる) | `DecimalPlaces` |
| 入力ルールの「整数 n 桁・小数 m 桁」(comset が 9 を並べた値を作って `ExMaxValue` に設定) | `SetDigits(n, m, マイナスを許すか)` |
| `ExCommaDelimiter`(`","` / `""`) | `ThousandsSeparator`(true / false) |
| `ExAlternateBlank` | 当たるものは無い。名前は「空欄を許すか」だが、実際の働きは「値が 0 のときの表示」(false なら空欄で表示)だった。S 系では、0 は「0」と表示する。未入力を許すかどうかは `AllowBlank`(既定 true)で決める |
| 小数点のキーで 1000 倍(小数を持たない書式のとき) | `PeriodMultipliesByThousand`(既定 true) |
| `Text` の設定(`cost.ToString("#,##0")` を代入している箇所がある) | `Text` の設定(数値として読み取る) |
| `Clear()` | 標準の `Clear()`(未入力になる) |
| `HighlightText`、`ContentAlignment`、`ImeMode` | `STextBox` と同じ名前。既定値が数値向けになっている(5.2) |
| `DisabledBackColor` / `DisabledForeColor`、`SingleBorderColor`、`BorderStyle` | 1.3 の共通プロパティ |
| `Spin.SpinOnKeys = false`(カーソル キーでの増減を止める) | 不要(増減の機能が無い) |
| `AlternateText.Zero.Text = "0"` | 不要(0 は「0」と表示する) |
| `ExPrefix` / `ExSuffix`、`ExGetCurrencyField` | なし(5.4) |

### 5.2 動作

| 機能 | 動作 |
| --- | --- |
| 表示 | フォーカスが無いときは、桁区切りと決まった桁数の小数を付けて表示する(1,234,567、1,234.50)。フォーカスがある間は、桁区切りの無い形にして編集する(1234567、1234.50) |
| 入力できる文字 | 数字、マイナス、プラス、小数点。全角で入力しても半角になる。空白やそのほかの文字は入力されない |
| 桁数 | 整数の桁数は、`MaxValue` と `MinValue` のうち桁数の多いほうで決まる。小数の桁数は `DecimalPlaces`。超える入力は受け付けない |
| 範囲 | `MaxValue` を超える正の数、`MinValue` を下回る負の数になる入力は受け付けない。0 に近い側の境界(最小値が 10 のときの「5」など)は、入力の途中では判断できないので、フォーカスが移るときに範囲の端へ丸める |
| マイナス キー | 符号を反転する。全選択のとき・空欄のときは、入力し直しとして「-」だけにする。`MinValue` が 0 以上なら受け付けない |
| プラス キー | 正の数にする |
| 小数点のキー | `DecimalPlaces` が 1 以上なら小数点を入力する。0 のときは、値を 1000 倍する(`PeriodMultipliesByThousand` が true のとき。桁数や範囲を超える場合は何もしない) |
| `Value` と `ValueChanged` | 入力するたびに `Value` が変わり、`ValueChanged` が発生する。同じ値を設定し直しても発生しない |
| `Value` の設定 | 範囲の外の値は範囲の端に丸める。小数は `DecimalPlaces` の桁数に四捨五入する |
| 未入力(`AllowBlank`。既定 true) | 空欄は null。false にすると、空欄は 0 になる(編集中に空にしても、フォーカスが移ると「0」に戻る) |
| `Text` | 取得すると、表示中の文字列を返す。設定すると、桁区切り・空白・通貨記号を取り除いて数値として読み取る(全角の数字も読む)。数値として読めなければ未入力になる。デザイナーには出さない |
| フォーカスが移るとき | ほかのコントロールの `Validating` より前に、値を確定して表示用の形に戻す |
| 読み取り専用 | フォーカスがあっても、表示用の形(桁区切り付き)のままにする |
| 既定値 | 文字は右寄せ、フォーカス時に全選択(`HighlightText` = true)、IME は無効、範囲は ±9,999,999,999,999、小数 0 桁、桁区切りあり |
| 収まらない文字(`ShrinkTextToFit`。既定 false) | 既定では縮めない(ほかの入力欄と違う。1.5)。true にした欄は、フォーカスが無い間と無効の間、収まらない文字列を縮めて表示する |

`SetDigits(7, 0, true)` は、範囲を -9,999,999 〜 9,999,999、小数を 0 桁にする。現在のルール `Currency(7,0)` に当たる。

### 5.3 GcNumberEx と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | フォーカスがある間は、桁区切りを表示しない。GcNumberEx は、入力用の書式にも区切りを指定している(編集中も表示されると読めるが、実機では確かめていない) | 編集中に区切りを入れ直すと、カーソルの位置の扱いが複雑になるため |
| 2 | 入力の方式。Gc は、書式(整数部・小数部のフィールド)に沿って入力する。S は通常の文字入力で、入力した結果が数値の形にならないものを受け付けない。Delete や BackSpace の後の見え方など、細かい操作感は同じではない | 標準のテキスト ボックスの上に作るため |
| 3 | 桁数や範囲を超える入力は、挿入する前に調べて受け付けない。GcNumberEx は、入力された後で直前の文字列に戻していた。削除(Delete・BackSpace・切り取り)で桁数を超える形になった場合(「1234567.89」の小数点を消す、など)は、事前には調べられないので、削除する前の文字列に戻す | 入力欄がいったん書き換わるのを避けるため。削除の場合に戻すのは、戻さないと値が範囲の端に丸められて、入力していない数値に変わるため |
| 4 | マイナス キーは、常に符号の反転。GcNumberEx は、空欄・1 桁の値・全選択のときに「-0」にする処理を足していた | 動作を 1 つにまとめた |
| 5 | 小数の桁数を `DecimalPlaces` で指定する。Gc は最大値・最小値の小数部分から決めていた | 範囲と桁数を別々に指定できるようにするため |
| 6 | `AllowBlank` は、小数の桁数に連動しない。GcNumberEx は、小数を持つ書式にすると空欄を許さなくなる | 連動させる理由が見当たらないため。同じにしたい場合は `AllowBlank = false` を指定する |
| 7 | `Text` に数値として読めない文字列を設定すると、未入力になる | 値を数値で持つため |
| 8 | 右寄せ・全選択・IME 無効を既定にした。Gc では入力ルールが設定していた | ルールを適用しない欄(合計の表示など)でも同じ見た目にするため |

### 5.4 入れていないもの

| 内容 | 理由・扱い |
| --- | --- |
| `ExPrefix` / `ExSuffix`(前後に付ける文字列) | 全 18 個で使っていない |
| `ExGetCurrencyField`(書式文字列を作る static メソッド) | Gc の書式のためのもの |
| 編集中の桁区切り | 5.3 の 1 |
| カーソル キー・ホイールでの値の増減 | GcNumberEx でも止めていた |
| グリッドのセル編集でのキーの受け渡し(comset の `GGridDB` が `GcNumber` に値を直接設定している処理) | `SGridDB` では、グリッドで入力した文字を、メッセージで入力欄へ渡す(10.7.3 の 4)。数値の入力欄に特別な処理は無い。「-」「.」から入力を始めたときの動きは未確認(10.7.4 の 4) |

### 5.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実機のキーボードでの操作(テンキーの小数点・マイナス)。検証は文字を直接送る形で行った |
| 2 | 実際の貼り付け。経路は `STextBox` と同じ(2.5 の 8) |
| 3 | Visual Studio のデザイナーへの配置。`Value` などの既定値を書き出さないことは、判定の処理をテスト済み |
| 4 | (確認済み)入力ルールの適用(`SetDigits` などを呼ぶ側)。`SControlDB` のテストで確かめている(7.2.4、8.1) |
| 5 | 小数点のキーの 1000 倍が、実際の運用で使われているか。GcNumberEx に入っていた動作をそのまま既定にしている |

---

## 6. SDateBox

`UcDate`(中身は `GcDateEx`)の代わり。日付を入力する。値は `Value`(`DateTime?`)で読み書きし、未入力は null。

`STextBox` と同じ土台(`SEditBase`)の上に作っているので、見た目、無効のときの色、`ReadOnly`、`BorderStyle` などは `STextBox` と同じ。右端のカレンダーのボタンは、土台のサイド ボタン(2.2.4)。文字の種類を指定するプロパティ(`Format` など)は持たない。

### 6.1 対応

| UcDate | SDateBox |
| --- | --- |
| `UcValue`(`DateTime?`) | `Value` |
| `UcValueChanged` | `ValueChanged` |
| `UcFormat`(YYYYMMDD / YYYY_MM_DD / YYYYMM / YYYY) | `DateFormat`(YearMonthDay / YearMonthDaySlash / YearMonth / Year) |
| `UcDate.SetKoyomiSeirekiOn(bool)`(static) | `SDateBox.DefaultCalendarKind`(static。Japanese / Western) |
| `ChangedKoyomiSeirekiOn(bool)` | `CalendarKind`(コントロールごとの指定)、`RefreshDisplay()` |
| `UcSideButton` | `ShowCalendarButton` |
| `UcCalenderMinDate` / `UcCalenderMaxDate`、`GcDateEx.ExMinDate` / `ExMaxDate` | `MinDate` / `MaxDate`(入力とカレンダーで共通) |
| `UcDateCtl`(中の `GcDateEx`) | なし(コントロール自身が入力欄) |
| `UcDateCtl.DropDownCalendar.CalendarType = YearMonth` | 不要(`DateFormat` が年月なら月を選ぶカレンダー、年なら年を選ぶカレンダーになる) |
| `GcDateEx.DroppedDown` | `DroppedDown`(設定もできる) |
| `UcReadOnly` / `UcEnabled` | 標準の `ReadOnly` / `Enabled` |
| `UcBackColor` / `UcBorderStyle` / `SingleBorderColor` / `DisabledBackColor` / `DisabledForeColor` / `Font` | `BackColor` / `BorderStyle` / `BorderColor` / 同じ名前 |

`AppDate.SetDispSeireki` に当たる処理は、次の形になる。

```csharp
SDateBox.DefaultCalendarKind = val ? SDateCalendarKind.Western : SDateCalendarKind.Japanese;
```

### 6.2 動作

#### 6.2.1 表示の形

フォーカスが無いときは表示用の形、フォーカスがある間は編集用の形になる。

| `DateFormat` | 和暦: 表示 / 編集 | 西暦: 表示 / 編集 |
| --- | --- | --- |
| YearMonthDay | 令和 08年11月25日 / R08/11/25 | 2026年11月25日 / 2026/11/25 |
| YearMonthDaySlash | 令和 08/11/25 / R08/11/25 | 2026/11/25 / 2026/11/25 |
| YearMonth | 令和 08年11月 / R08/11 | 2026年11月 / 2026/11 |
| Year | 令和 08年 / R08 | 2026年 / 2026 |

表示用の形は `UcDate` と同じ。和暦・西暦は `CalendarKind` で決まり、Default(既定)ならアプリ全体の設定 `SDateBox.DefaultCalendarKind`(既定は和暦)に従う。

元号は `SDateText.cs` の表で持っている。

| 元号 | 略称 | 始まった日 |
| --- | --- | --- |
| 明治 | 明 / M | 1868/09/08 |
| 大正 | 大 / T | 1912/07/30 |
| 昭和 | 昭 / S | 1926/12/25 |
| 平成 | 平 / H | 1989/01/08 |
| 令和 | 令 / R | 2019/05/01 |

和暦の指定でも、明治の始まりより前の日付は西暦で表示する。

#### 6.2.2 入力の読み方

入力できる文字は、数字、区切り(`/ - .`)、元号のアルファベット、元号と「年月日」「元」の漢字。全角で入力しても半角になる。入力した文字列は、確定するときに日付として読む。

| 入力(年月日の形のとき) | 読み方 | 例(今日が 2026/11/25 の場合) |
| --- | --- | --- |
| 区切りで 3 つ | 年・月・日 | `2026/11/25`、`2026-1-5`、`2026.11.25`、`2026年11月25日` |
| 区切りで 2 つ | 月・日(年は今年) | `11/25` → 2026/11/25 |
| 数字 8 桁 | 年 4 桁 + 月 + 日 | `20261125` |
| 数字 6 桁 | 年 2 桁 + 月 + 日 | `081125` → 令和 8 年 11 月 25 日(和暦の表示のとき) |
| 数字 3〜4 桁 | 月 + 日(年は今年) | `1125`、`305` → 3 月 5 日 |
| 数字 1〜2 桁 | 日(今年の今月) | `7` → 2026/11/07 |
| 先頭に元号 | 年を和暦として読む | `R8/11/25`、`r08.11.25`、`R081125`、`令和8年11月25日`、`令和元年5月1日`、`S40/3/3` |

| 入力(年月の形のとき) | 読み方 | 例 |
| --- | --- | --- |
| 区切りで 2 つ以上 | 年・月(3 つ目の日は無視) | `2026/11`、`R8/11`、`令和8年11月` |
| 数字 6 桁 | 年 4 桁 + 月 | `202611` |
| 数字 3〜4 桁 | 年 1〜2 桁 + 月 | `0811` → 令和 8 年 11 月(和暦の表示のとき) |
| 数字 1〜2 桁 | 月(今年) | `3` → 2026/03 |

年だけの形では、4 桁までの数字を年として読む(`2026`、`R8`、`平成30年`)。

- 元号が無く、年が 1〜2 桁のときは、和暦の表示なら今の元号の年、西暦の表示なら西暦 2 桁の年(どの 100 年に当てるかは Windows の設定に従う)として読む。年が 3 桁以上なら西暦。
- 和暦として読んだ日付が、その元号の期間に入っていなければ、読めないものとして扱う(元の値に戻る)。`S70/1/1`(昭和は 64 年まで)、`H1/1/1`(平成は 1 月 8 日から)、`H31/5/1`(5 月 1 日からは令和)など。書類の和暦を写し間違えたときに、別の日付として黙って受け付けないようにするため。
- 年の形・年月の形では、その年・その月のうち 1 日でも元号の期間に入っていれば受け付ける。`R1` と `H31` はどちらも 2019 年、`H1/1` と `S64/1` はどちらも 1989 年 1 月として読む。`H32`、`R1/4`、`H31/5` は読めない。
- 年の形・年月の形の表示は、その年・その月の最後の日の元号にする。2019 年は「令和 01年」、2019 年 4 月は「平成 31年04月」、1989 年 1 月は「平成 01年01月」。年や月の途中で改元があったときに、値の「日」によって表示が変わらないようにするため(年・年月の形の値は、日を 1 日として持つ)。カレンダーの見出しも同じ決め方をする。
- 年月の形で入力した値は 1 日に、年の形で入力した値は 1 月 1 日になる。
- 存在しない日付(2 月 30 日など)や、日付として読めない文字列は受け付けない。

#### 6.2.3 確定と値

| 場面 | 動作 |
| --- | --- |
| Enter | 入力を確定する。Enter そのものは止めない(フォームが次の項目への移動などに使える) |
| フォーカスがほかのコントロールへ移った | 入力を確定して、表示用の形に戻す。ほかのコントロールの `Validating` より前に確定する |
| 日付として読めなかった | 入力する前の値に戻し、`InvalidInput` イベントを発生させる |
| 空にして確定した | 未入力(null)になる |
| Esc | 入力を取り消して、元の値に戻す |
| 範囲(`MinDate`〜`MaxDate`。既定は 1868/01/01〜2100/12/31) | 範囲の外の日付は、入力でもプログラムからの設定でも、範囲の端に丸める |
| `ValueChanged` | 値が変わったときに発生する。入力の途中では発生せず、確定したときに発生する |
| プログラムから設定した値 | 時刻や、年月の形での日も含めて、そのまま持つ。フォーカスが入って出ただけ(文字列を変えていない)なら、値は変わらない |
| `Text` | 取得すると、表示中の文字列を返す。設定すると、日付として読み取る(読めなければ未入力)。デザイナーには出さない |
| 和暦・西暦の切り替え | `CalendarKind` を変えると、すぐに表示が変わる。アプリ全体の設定(`DefaultCalendarKind`)を変えた場合は、表示済みのコントロールで `RefreshDisplay()` を呼ぶ |
| 既定値 | 文字は中央寄せ、フォーカス時に全選択、IME は無効 |
| 収まらない文字(`ShrinkTextToFit`。既定 true) | フォーカスが無い間と無効の間、表示用の形(「令和 08年12月15日」など)が収まらなければ、フォントを縮めて表示する(1.5)。フォーカスがある間は、編集用の形を元の大きさで表示する |

#### 6.2.4 カレンダー

| 操作 | 動作 |
| --- | --- |
| 右端のボタン、Space、↑ ↓、Alt+↓ | カレンダーを開く。入力の途中の文字列があれば、先に確定する |
| 開いている間の ← → ↑ ↓ | 指している日を、1 日・1 週間ずつ動かす(月を選ぶ表示では 1 か月・1 行分(4 か月)、年を選ぶ表示では 1 年・1 行分(4 年)) |
| PageUp / PageDown | 1 か月(月を選ぶ表示では 1 年、年を選ぶ表示では 12 年)動かす |
| Enter、日のクリック | その日を値にして閉じ、入力欄を全選択にする(月を選ぶ表示ではその月の 1 日、年を選ぶ表示ではその年の 1 月 1 日) |
| Esc、Space、Alt+↑ ↓ | 値を変えずに閉じる |
| 文字の入力 | カレンダーを閉じて、入力を続ける |
| 見出しの `‹` `›` | 1 か月(月を選ぶ表示では 1 年、年を選ぶ表示では 12 年)動かす |
| 見出しの `«` `»` | 1 年(月を選ぶ表示では 10 年、年を選ぶ表示では 60 年)動かす |
| 外側のクリック、親のフォームの移動など | 閉じる(`STableComboBox` の一覧と同じ) |

- 表示の種類は `DateFormat` で決まる(`SCalendarMode`)。年月日(YearMonthDay / YearMonthDaySlash)は日を選ぶ表示(1 か月分。7 列 × 6 行)、年月は月を選ぶ表示(1 年分。4 列 × 3 行)、年は年を選ぶ表示(12 年分。4 列 × 3 行)。年を選ぶ表示は 2026-11-26 に足した。それまでは、年の形でも月を選ぶ表示になっていた。
- 年を選ぶ表示の 1 画面は、年を 12 で割った商が同じ 12 年(2016〜2027 年など)。1 画面分動かしても、指しているマスの位置は変わらない。
- 見出しは、和暦の表示なら「令和 08年11月」、西暦なら「2026年11月」。月を選ぶ表示では年(「令和 08年」)、年を選ぶ表示では、表示している 12 年の範囲(「平成 28年 ～ 令和 09年」)。見出しの文字は太字。
- 見出しの左右の `«` `‹` `›` `»` は、線で描いた山形で、色は RoyalBlue(`SCalendarPopup` の `NavColor`)。
- 今日(今月・今年)を枠で囲み、指している日を塗って表示する。日曜は赤、土曜は青。範囲の外の日(月・年の表示では、選べる日を 1 日も含まない月・年)は薄く表示し、選べない。
- `ShowCalendarButton` を false にすると、ボタンが消え、キーでも開かなくなる。
- 読み取り専用・無効のときは開かない。ボタンを薄く表示する。

### 6.3 UcDate と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 入力の方式。Gc は、年・月・日の欄(フィールド)ごとに入力する。S は 1 つの文字列として自由に入力し、確定するときに日付として読む | 標準のテキスト ボックスの上に作るため。区切りなしの数字(`20261125`)や、月日だけの入力もできるようにした |
| 2 | 編集用の形は「R08/11/25」。UcDate は「R. 08/11/25」(書式 `g. ee/MM/dd`) | 入力する文字を減らすため。「R. 08/11/25」と入力しても読める |
| 3 | `ValueChanged` は、確定したときだけ発生する | 入力の途中の文字列は、日付として読めないことがあるため |
| 4 | 入力の範囲とカレンダーの範囲を、1 組(`MinDate` / `MaxDate`)にした。UcDate は別々のプロパティだった(Designer では全 30 か所で同じ値) | 別々にする使い方が無いため |
| 5 | 元号を、コントロール側の表で持つ | Windows・.NET・InputMan の元号の情報や設定に左右されないようにするため。改元のときは、表に 1 行足す |
| 6 | 明治の始まり(1868/09/08)より前の日付は、和暦の指定でも西暦で表示する | 当てはまる元号が表に無いため |
| 7 | `ShowCalendarButton` を false にすると、キーでもカレンダーが開かない | ボタンが無いのに開くのは分かりにくいため |
| 8 | 月を選ぶカレンダーにする指定(`CalendarType = YearMonth`)が不要 | `DateFormat` から決まるため。現在は 7 か所でフォーム側が個別に指定している |
| 14 | 年の形(`DateFormat` が Year)では、年を選ぶカレンダー(12 年分)を開く。元の画面の年だけの欄は、カレンダーのボタンを出さない設定だった(S 系へ移した 3 画面の年の欄は、ボタンを出すようにした) | 年だけを入力する欄で、年を直接選べるようにするため(2026-11-26 に決定) |
| 15 | 表示用の形が欄に収まらないときは、フォントを縮めて表示する(既定。1.5) | 和暦の年月日は幅を取るので、欄の幅が少し足りない場合でも、末尾が欠けないようにするため(2026-11-26) |
| 9 | 日付として読めない入力は、元の値に戻す | 自由入力にしたため、読めない文字列が入ることがある |
| 10 | Esc で入力を取り消せる | 自由入力にしたため |
| 11 | 入力の途中の内容を、フォーカスを動かさずに確定するメソッド `CommitEdit()` がある(`ISEditCommit`)。Enter・フォーカスの移動と同じ処理をする | 確定するまで `Value` は変わらないので(3)、ファンクション キーでの保存のようにフォーカスが動かない処理が、そのまま値を読むと、入力する前の値になってしまう。`SControlDB` の書き戻しは、読む前に自動で呼ぶ。`SControlDB` を使わずに値を読む処理(条件を入力する画面など)は、読む前に呼ぶこと |
| 12 | 元号の期間に入らない和暦の日付は、読めないものとして扱う(6.2.2) | 写し間違いに気づけるようにするため。GcDateEx が同じ入力をどう扱っていたかは確かめていない |
| 13 | 年・年月の形では、その年・その月の最後の日の元号で表示する(6.2.2) | 元年を入力したのに、前の元号で表示されるのを防ぐため(「R1」が「平成 31年」になっていた) |

### 6.4 入れていないもの

| 内容 | 理由・扱い |
| --- | --- |
| 欄(フィールド)ごとの入力 | 6.3 の 1 |
| 省略した部分を補う値の指定(Gc の `RecommendedValue`。UcDate は今年の 1 月 1 日を指定) | 補い方を固定にした(年は今年、月は 1 月、日は 1 日。年月日の形で日だけ入力したときは今月) |
| カレンダーの祝日の表示、「今日」ボタン、週の始まりの曜日の指定 | 現在も使っていない |
| ↑ ↓ やホイールでの日付の増減 | UcDate でも止めていた(↑ ↓ はカレンダーを開く) |
| 時刻の入力 | 現在も使っていない |

### 6.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実機のキーボード・マウスでの操作。検証は、キーと文字を直接送る形で行った。カレンダーの日のクリックと、見出しのボタンのクリックは検証していない(キーでの移動と選択は検証した)。年を選ぶ表示も同じで、キーでの移動と選択はテストで、表示は画像で確かめた |
| 2 | Visual Studio のデザイナーへの配置 |
| 3 | 必要な幅。カレンダーのボタン付きの和暦の年月日(「令和 08年12月15日」)は、表示倍率 120% で、幅 186 なら元の大きさで収まり、縮めない設定の幅 170 では「日」が欠けた(縮める設定なら、170 でも全部入る)。どちらも画像で確かめた。画面での欄の幅の決まりは S画面仕様の 1.7 にある。既存の `UcDate` の幅で足りるかは、画面ごとに確認が要る |
| 4 | `FormFrame` との組み合わせ。`FormFrame.CheckEnabledFormKeyPreview` は `GcDateEx` の `DroppedDown` と ↑ ↓ を見ているので、S 系への対応が要る |
| 5 | (確認済み)入力ルールの適用と、DB との値の受け渡し(`GControlDBDate` に当たる処理)。`SControlDB` のテストで、メモリ上の DataTable を使って確かめている(7.2.2、7.5 の 1) |
| 6 | グリッドのセルに重ねる使い方(会費調整の画面 `FormKaiin2_DlgChosei` の年月の欄)。実際の DB では確かめていない(10.7.4 の 4) |
| 7 | 西暦 2 桁の年の読み方は Windows の設定に従うので、PC によって結果が変わりうる |

---

## 7. SControlDB

`GControlDB`(comset)の代わり。画面のコントロールと、行(1 件分のデータ)の間で値を受け渡す。入力ルールの表(`GControlDBRule` の代わり)も含む。設計の方針は 1.4。

置き場所は `FMKaiin/Controls/Common/`(名前空間は `App`)。S 系のコントロールが FMKaiin にあり、comset からは参照できないので、FMKaiin に置いた。`SControlDBRuleConverter.cs` は、FMKaiin のプロジェクトには入れず、テスト用として `Test/` に置いている(7.2.4)。

| ファイル | 内容 |
| --- | --- |
| `SControlDB.cs` | 本体。登録、読み込み、書き戻し、読み取り専用の切り替え |
| `SControlDBItems.cs` | 項目とコントロールの対応(`SControlDBText` など 8 種類)と、その土台 `SControlDBItem` |
| `SControlDBRule.cs` | 入力ルール(`SFieldRule` / `SControlRule`)と、項目名との対応表 `SControlDBRule` |
| `SControlDBRuleConverter.cs`(`Test/`) | Gc 系の画面用のルールの定義(`GControlDBRule`)を S 系のルールに読み替える。テスト専用で、アプリからは使わない |
| `SRow.cs` | 行の入口 `ISRow` と、`DataRow` を包む `SDataRow` |

### 7.1 対応

| GControlDB | SControlDB |
| --- | --- |
| `new GControlDB(form, getDataRow)` / `(form, getDataRow, zipCode)` | `new SControlDB(form, getDataRow)` / `(form, getDataRow, zipCode)`。`getDataRow` は、`DataRow` を返す関数でも、`ISRow` を返す関数でもよい。引数は無い(GControlDB では、データ タグを受け取る引数が 1 つあった) |
| `Add(...)` / `EndAdd(rule)` | 同じ。ルールを設定しない `EndAdd()` もある |
| `GControlDBText(項目, コントロール)` | `SControlDBText(項目, STextBox)` |
| `GControlDBNumber(項目, コントロール)` | `SControlDBNumber(項目, SNumberBox)`、または `(項目, STextBox)` |
| `GControlDBDate(項目, uc.UcDateCtl)` | `SControlDBDate(項目, SDateBox)` |
| `GControlDBCombo(項目, cmb)` / `(項目, uc.ComboBox)` | `SControlDBCombo(項目, SComboBox)` / `(項目, STableComboBox)` |
| `GControlDBCheckBox(項目, YcCheckBoxEx)` | `SControlDBCheckBox(項目, CheckBox)`。`SCheckBox`(標準の `CheckBox` を継承している)を渡す |
| `GControlDBRuby(string[], Control[])` | `SControlDBRuby(string[], STextBox[])` |
| `GControlDBPostAddr(string[], Control[])` | `SControlDBPostAddr(string[], STextBox[])` |
| `GControlDBHyphenSplit(項目, Control[])` | `SControlDBHyphenSplit(項目, STextBox[])` |
| `FetchAll` / `FetchByField` / `FetchByControl` / `ClearAll` | 同じ |
| `UpdateAll` / `UpdateByControl` / `Update(container)` | 同じ。フォームを省略できる `Update()` もある |
| `Enabled` / `EnabledAll` / `ReadOnly` / `ReadOnlyAll` | 同じ |
| `GetControl(項目)` / `GetField(コントロール)` | 同じ |
| `GetDBBase(項目)` / `GetDBBase(コントロール)` | `GetItem(項目)` / `GetItem(コントロール)` |
| `ControlList` / `EndAddFlg` | 同じ |
| `FetchRunEx` / `UpdateRunEx`(引数は `DataRow`) | 同じ名前。引数は `ISRow` |
| `FetchRunExTag` / `UpdateRunExTag` | なし(7.4) |
| `GControlDBEnum.NoChange` | `SControlDBEnum.NoChange` |
| `FieldUpdated`(引数は `DataRow`) | 同じ名前。引数は `ISRow` |
| `SameFieldFetch` / `DataFillLeft` / `RubyZenkaku` | 同じ |
| データ タグ(`DataTag`、各クラスの最後の引数) | なし(7.4) |
| `GControlDBRule`(対応表) | `SControlDBRule` |
| `GControlDBRuleText(項目, n)` などのクラス | `SControlRule.Text(n)` などのメソッド(7.2.4) |
| `AppDbRule.Rule`(Gc 系の画面用の定義) | `AppSControlRule.Rule`(S 系の画面用の定義)。同じ内容を、S 系のルールで別に書いてある |
| `AppDbRule.SetControlByRule(コントロール, 項目)` | `rule.SetControlByRule(コントロール, 項目)` |

`FormMasterBasic.cs` の登録に当たる書き方は、次の形になる。

```csharp
sctl = new SControlDB(this, getDataRow, AppGlobal.DB.DBZipCode);

sctl.Add(new SControlDBRuby(
    new[] { t_basic.FBAS_Name, t_basic.FBAS_NameFurigana },
    new[] { iName, iNameFurigana }));
sctl.Add(new SControlDBText(t_basic.FBAS_NameDaihyo, iNameDaihyo));
sctl.Add(new SControlDBPostAddr(
    new[] { t_basic.FBAS_Post, t_basic.FBAS_Addr1 },
    new[] { iPost1, iPost2, iAddr1 }));
sctl.Add(new SControlDBHyphenSplit(t_basic.FBAS_Tel1, new[] { iTel1_1, iTel1_2, iTel1_3 }));
sctl.Add(new SControlDBCombo(t_basic.FID_Bank, iBank));
sctl.Add(new SControlDBCombo(t_basic.FBAS_DispSeireki, iDispSeireki));

sctl.EndAdd(AppSControlRule.Rule);
sctl.FetchAll();
```

### 7.2 動作

#### 7.2.1 登録と、読み込み・書き戻し

| 場面 | 動作 |
| --- | --- |
| `Add` | 項目とコントロールの対応を登録する。`EndAdd` の後は追加できない |
| `EndAdd` | 登録を終える。入力ルールを渡すと、登録したコントロールに、項目名で引いたルールを設定する。以後、フォーカスが外れたときの書き戻しが始まる |
| `FetchAll` | 行の値を、すべてのコントロールに表示する。行は、コンストラクターに渡した関数で、その都度取得する |
| フォーカスが外れた(`Validating`) | そのコントロールの値を行に書き戻す。チェック ボックスは、オン・オフを変えた時点で書き戻す |
| 書き戻した後 | 同じ項目を見ているほかのコントロールを、書き戻した値で表示し直す(`SameFieldFetch` が true のとき) |
| 値が変わっていない | 行を書き換えない。`FieldUpdated` も発生しない |
| 読み込みの最中 | 書き戻しをしない(読み込みでコントロールの値が変わったことをきっかけに、書き戻しが走るのを防ぐ) |
| 行が無い(関数が null を返した) | 読み込みでは、値が無いものとして表示する。書き戻しでは、何もしない |
| `Update()` / `Update(container)` | いまフォーカスがあるコントロールの値を書き戻す。保存の直前など、フォーカスを動かさずに入力中の値を確定させたいときに使う |
| 書き戻しの前の確定 | 書き戻すコントロールに入力の途中の内容があれば(日付を打っただけで確定していない、テーブル コンボで絞り込みの途中、など)、先に確定してから値を読む(`ISEditCommit.CommitEdit()`)。ファンクション キーでの保存はフォーカスが動かないので、保存の前に `Update()` を呼ぶこと。呼べば、画面に見えている内容が保存される |
| `ClearAll` | すべてのコントロールを空にする。コンボ ボックスは、項目があれば先頭を選択する。行には書き戻さない |
| 登録した入力欄にフォーカスが入った(`Enter`) | 全選択にしない入力欄(`HighlightText` が false の 1 行の入力欄。住所の欄など)は、カーソルを文字列の末尾に置く(続きを入力しやすいように) |

`SNumberBox`・`SDateBox`・`STableComboBox` は、フォーカスが移るときに自分の値を確定してから `Validating` に入るので、書き戻しには確定後の値が使われる。

#### 7.2.2 項目ごとの受け渡し

| 対応 | 読み込み(行 → コントロール) | 書き戻し(コントロール → 行) |
| --- | --- | --- |
| `SControlDBText` | 値を文字列にして表示する | 文字列を書く。空なら値なし。1 行の入力欄では改行を取り除く |
| `SControlDBNumber`(`SNumberBox`) | 数値として読めれば `Value` に入れる。読めなければ未入力 | `Value` を書く。未入力なら値なし |
| `SControlDBNumber`(`STextBox`) | 数値を文字列にして表示する | 桁区切りを除いた文字列を書く。数値として読めなければ値なし |
| `SControlDBDate` | 日付として読めれば `Value` に入れる。読めなければ未入力 | 日付を文字列にして書く(`DateType.Date` なら日付だけ、`DateTime` なら時刻付き)。未入力なら値なし |
| `SControlDBCombo` | 値が一致する項目を選択する。該当なし・値なしなら未選択 | `SelectedValue` を書く。未選択なら値なし |
| `SControlDBCheckBox` | 真偽値、または数値(0 以外がオン)として読む | オン・オフを真偽値で書く |
| `SControlDBHyphenSplit` | `-` で分けて各欄に入れる。区切りが足りないときは右の欄から埋める(`DataFillLeft` で左から) | 各欄を `-` でつないで書く。すべて空なら空文字列 |
| `SControlDBRuby` | 名称とふりがなを表示する | 名称を書く。IME の読み(`TakeImeReading()`)をふりがなの欄の末尾へ追記する。名称が空ならふりがなも空にする。どちらも、利用者が名称の欄で入力している間、名称が書き換わるたびにその場で行う(欄を離れるのを待たない。7.3 の 16)。名称の一部を消しても、ふりがなは変えない(読みは確定した 1 回分ごとにしか分からず、消した文字に当たる部分を決められないため) |
| `SControlDBPostAddr` | 郵便番号を 2 つの欄に分け、住所(と住所のふりがな)を表示する | 郵便番号をつないで書く。郵便番号の欄を操作した場合で、住所が空なら、郵便番号から住所を引いて入れる。住所は、郵便番号を入力している最中にも入れる(7.2.6) |

値なしは、行では `DBNull.Value` になる。

#### 7.2.3 読み取り専用と有効・無効

| メソッド | 動作 |
| --- | --- |
| `ReadOnly(コントロール, true)` / `ReadOnlyAll(true)` | 入力欄とコンボ ボックスは `ReadOnly` を true にし、背景をグレー(`SystemColors.Control`)にして、Tab キーでの移動先から外す。チェック ボックスは、クリックで切り替わらないようにする(背景は変えない) |
| `ReadOnly(コントロール, false)` / `ReadOnlyAll(false)` | 元に戻す。背景色と Tab の設定は、`EndAdd` の時点のものに戻る |
| `Enabled` / `EnabledAll` | `Enabled` を切り替える |

#### 7.2.4 入力ルール

入力ルールは、入力欄 1 つ分の設定 `SFieldRule` を、入力欄の数だけ持つ `SControlRule` で表す。`SControlDBRule` が、項目名との対応表になる。

| ルールを作るメソッド | 内容 | 当たる GControlDBRule |
| --- | --- | --- |
| `SControlRule.Text(n)` | n 文字まで。IME はひらがな、左寄せ | `GControlDBRuleText` |
| `SControlRule.Hankaku(n)` | 半角の英数字 n 文字まで。IME は無効 | `GControlDBRuleHankaku` |
| `SControlRule.Number(n, m)` | 整数 n 桁・小数 m 桁。桁区切りなし、右寄せ、IME は無効 | `GControlDBRuleNumber` |
| `SControlRule.Currency(n, m)` | 同上で、桁区切りあり | `GControlDBRuleCurrency` |
| `SControlRule.HyphenSplit(a, b, c)` | 欄ごとに a・b・c 文字までの数字 | `GControlDBRuleHyphenSplit` |
| `SControlRule.Ruby(a, b)` | 名称 a 文字(半角記号以外)、ふりがな b 文字(半角、IME は半角カタカナ) | `GControlDBRuleRuby` |
| `SControlRule.PostAddr(a, b)` | 郵便番号 3 桁・4 桁、住所 a 文字、住所のふりがな b 文字 | `GControlDBRulePostAddr` |
| `SControlRule.Date()` | 中央寄せ、IME は無効 | `GControlDBRuleDate` |

作ったルールは、`Parts` の値を変えて調整できる(例: `rule.Parts[0].AllowBlank = true;`)。

ルールを設定したときに変わるプロパティ:

| コントロール | 設定するもの |
| --- | --- |
| `STextBox` | `MaxLength`、`MaxLengthUnit`、`Format`、`AllowSpace`、`AcceptsCrLf`、`HighlightText`、`ImeMode`、`TextAlign` |
| `SNumberBox` | 数値のルールなら、桁数(`SetDigits`)、`ThousandsSeparator`、`AllowBlank`。ほかに `HighlightText`、`TextAlign` |
| `SDateBox` | `HighlightText`、`TextAlign` |
| `STableComboBox`、`SComboBox`、チェック ボックス | 何もしない |

数値のルールを `STextBox` に設定すると、「数字だけ・整数の桁数の文字数まで」になる(銀行コードなど、数値の項目を通常の入力欄で扱っている箇所のため)。

S 系の画面のルールは、S 系だけで書いた定義 `AppSControlRule`(`FMKaiin/UpdateForm/AppSControlRule.cs`)に持つ。使い方は `AppDbRule` と同じで、`SControlDB.EndAdd(AppSControlRule.Rule)` に渡すか、DB にバインドしない入力欄には `AppSControlRule.SetControlByRule(コントロール, 項目名)` を使う。ルール名の定数(`RuleCurrency` など)も同じ名前で持っている。Gc 系の画面用の定義 `AppDbRule` とは、同じ内容を 2 か所に書くことになる。ルールはほとんど変わらないので、読み替えの仕組みを間に入れるより、2 か所に書くほうが問題が起きにくい、という判断(2026-11-25)。すべての画面を S 系に移したら、`AppDbRule` をやめる。

`SControlDBRuleConverter`(`Test/`)は、`AppDbRule` の定義を S 系のルールに読み替えるもの。当初は S 系の画面からこれを通して `AppDbRule` を使う考えだったが、Gc 系の定義に依存することになるので、アプリからは外した。いまは、`AppSControlRule` が `AppDbRule` と同じ内容になっているかを項目ごとに比べるテスト(`Test/RuleHarness.cs`)が使っている。ルールを変えるときは、`AppDbRule.cs` と `AppSControlRule.cs` の両方を直してから、このテストを実行すること(片方だけ直すと、テストが違いを表示する)。

#### 7.2.5 フック

| フック | 使いどころ |
| --- | --- |
| `FetchRunEx(row, data)` | 行の値と、表示する値が違うとき(コードを名称に変える、など)。戻り値が表示される。null を返すと空欄 |
| `UpdateRunEx(row, data)` | 書き戻す値を変えたいとき、ほかの項目も一緒に書きたいとき。戻り値が書き込まれる |
| `SControlDBEnum.NoChange` | フックがこれを返すと、何も変えない(フックの中で自分で処理した場合) |
| `FieldUpdated(row, field, value)` | 行の値を書き換えたときの通知。対応ごと(`SControlDBItem.FieldUpdated`)と、全体(`SControlDB.FieldUpdated`)の両方にある |

`row` は `ISRow` で、`row[項目名]` で読み書きする。型付きの行クラス(`t_hikiotoshi` など)を使いたい場合は、`((SDataRow)row).Row` で `DataRow` を取り出せる(その箇所は DataTable に依存することになる)。

#### 7.2.6 郵便番号を入力している最中の、住所の取得

`SControlDBPostAddr` は、郵便番号の欄を離れるのを待たずに、入力している最中にも住所を入れる(2026-11-26)。画面の側に書くものは無い(`SControlDBPostAddr` を登録してある画面すべてに効く。基本情報と、医療機関マスタの入力)。

| 場面 | 動作 |
| --- | --- |
| 郵便番号の欄に、利用者が入力した | 2 つの欄に数字が 3 桁・4 桁そろっていて、住所の欄が空なら、入力が止まるのを待ち始める。待つ時間は `AutoLookupDelay`(既定 500 ミリ秒) |
| 待っている最中に、また入力した | 待ち直す(最後の入力から数える)。7 桁でなくなったら、待つのをやめる |
| 入力が止まった | 郵便番号から住所を引いて、住所の欄に入れる(住所のふりがなの欄があれば、ふりがなも足す)。フォーカスは、郵便番号の欄に残る。見つからなければ、何も変えない |
| 住所の欄に、何か入っている | 探さない。入っている住所は、書き換えない(郵便番号を打ち直しても、住所はそのまま) |
| 待っている最中に、郵便番号の欄を離れた | 待つのをやめる。これまでどおり、欄を離れたときに探す(7.2.2) |
| プログラムから値を入れた(読み込み・`ClearAll`) | 探さない |
| 行への書き戻し | これまでどおり、郵便番号の欄を離れたとき(または `Update()`)。入力の最中に入った住所も、そのときに行へ書かれる |
| `AutoLookupDelay` を 0 以下にした | 入力の最中には探さない(欄を離れたときだけ) |
| 住所を引く処理で例外が起きた | エラー ログに書いて、入力は続けられるようにする(欄を離れたときに、もう一度探す) |

住所を引く処理は、画面と同じスレッドで、そのまま呼ぶ(別のスレッドには回していない)。郵便番号の DB(`system\Post.db`。約 27 MB)の写しで測ったところ、1 回の検索は約 62 ミリ秒で、番号によらずほぼ一定だった(DB を開いた後の最初の 1 回だけ、約 93 ミリ秒。開発 PC での実測。200 回続けても同じ)。入力が止まってから 1 回だけ走るので、入力の邪魔にはならない。入力のたびに探す形に変える場合は、この時間が毎回掛かることになるので、別のスレッドに回すことを検討すること。

テスト(`Test/DbHarness.cs` の `PostTests`)は、郵便番号の DB の代わりに、番号と住所の対応を返す関数を差し込んで(`SControlDBPostAddr.AddressLookup`。テスト用)、探しに行く時機と回数を確かめている。

#### 7.2.7 新しいコードの採番と、桁数の上限

`AppDbID.GetNewCode(テーブル名または DBView, 項目名)`(`FMKaiin/AppDbID.cs`)は、項目の最大値に 1 を足した値を、新しいコードとして返す。そのとき、コードの桁数の上限を、`AppSControlRule`(7.2.4)の、その項目のルールから引く(2026-11-26。それまでは Gc 系の `AppDbRule` から引いていた)。これで `AppDbID.cs` は Gc 系を参照しない。

| 場面 | 動作 |
| --- | --- |
| ルールがあり、桁数(`Parts[0].MaxLength`)が 1 以上 | 新しいコードが 10 の桁数乗以上になったら、`AppLog` に「最大桁数を超えています。」と書いて、null を返す。比べるときは long を使う(10 桁以上のルールで int があふれないように) |
| ルールが無い、または桁数の指定が無い(0) | 上限を調べずに、新しいコードを返す |
| 最大値が取れなかった | これまでどおり、`ErrLog` に「新規CDの取得に失敗しました。」と書いて、null を返す |

いま採番に使っている項目と桁数:

| 項目 | ルール | 上限 |
| --- | --- | --- |
| 会員コード(`t_kaiin.FCD_Kaiin`) | `Code`(数字 6 桁) | 999999 |
| 担当者コード(`t_tantosha.FCD_Tanto`) | `Code`(数字 6 桁) | 999999 |
| 医療機関コード(`t_iryokikan.FIRK_Code`) | 数字 5 桁 | 99999 |

3 つとも、`AppDbRule` と同じ桁数なので、結果は以前と同じ。上限の手前では次の番号が返り、上限を超えると null が返ることを、確認用のプログラムで確かめた。

移行前の画面(`FormKaiin`・`FormMasterIryoKikan`・`FormMasterTantosha`)も同じメソッドを呼ぶので、S 系の画面と同じく、`AppSControlRule` のルールを見る。採番する項目のルールを変えるときは、`AppDbRule` と `AppSControlRule` の両方を直すこと(7.2.4)。

### 7.3 GControlDB と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | コントロールを、型を指定して渡す(`STextBox` など)。GControlDB は `Control` で受け取り、実行時に型を調べてエラー ログを出していた | 種類の違うコントロールを渡した誤りが、コンパイルの時点で分かるため |
| 2 | `EndAdd` で、コントロールの `Enabled` と `ReadOnly` を変えない。GControlDB は、すべて「有効・読み取り専用でない」に設定し直していた | デザイナーや、登録の前に設定した状態を、そのまま使えるようにするため |
| 3 | 読み取り専用のコントロールは、Tab の移動先から外すだけにした。GControlDB は、フォーカスが入ると次のコントロールへ移していた | クリックでフォーカスを置いて、内容をコピーできるようにするため |
| 4 | 日付型の項目は、日付として比べる。GControlDB は文字列にして比べるので、日付の欄からフォーカスが外れるたびに、値が同じでも行を書き換えていた(行が「変更あり」になる) | 変更していないのに、変更ありと判定されるのを防ぐため |
| 5 | `GetField` は、入力欄が複数ある項目(ハイフン区切り・郵便番号)でも正しい項目名を返す。GControlDB は、コントロールの番号をそのまま項目の番号に使っていた | 郵便番号の後半の欄から引くと「住所」が返るなど、結果が誤っていたため |
| 6 | 登録の誤り(コントロールが null、ふりがなの項目が 2 つでない、`EndAdd` の前に読み込みを呼んだ、同じ項目にルールを 2 回登録した、など)は、例外にする。GControlDB はエラー ログを出して続行していた | 画面を作っている段階で気づけるようにするため |
| 7 | 「利用者が操作したコントロール」を、名前ではなくコントロールそのもので判定する | 同じ名前のコントロールがあっても誤らないようにするため |
| 8 | ルールの文字の位置は、左右だけを設定する。GControlDBRule の定義は「左下」「右下」のように上下も指定している | S 系のコントロールは上下中央が基本で、定義どおり「下」にすると見た目が崩れるため |
| 9 | `FetchByField` に複数の項目を渡したとき、登録されていない項目があっても、残りの項目を処理する。GControlDB はそこで打ち切っていた | 一部だけ読み込まれた状態になるのを避けるため |
| 10 | 数値の項目の値が数値として読めないとき、未入力として表示する。GControlDB は、何もせず前の表示を残していた | 前の行の値が残って見えるのを防ぐため |
| 11 | フックと通知の引数は `ISRow`(1.4 の方針) | データの持ち方への依存を、項目名での読み書きだけに絞るため |
| 12 | 行を取得する関数(`getDataRow`)は、引数を取らない。GControlDB では、データ タグを受け取る引数があった | データ タグを使っている箇所が無いため(7.4)。既存のフォームを移すときは、`getDataRow` の引数を消す |
| 13 | 書き戻しの前に、入力の途中の内容を確定する(7.2.1) | 日付とテーブル コンボの入力欄は、フォーカスが移るときに値を確定する作りなので、フォーカスを動かさない保存で、入力した値が書き戻されなかったため |
| 14 | 数値のルールは、未入力(空欄)を許す。値が無い項目は空欄のまま表示し、フォーカスが通っても行を書き換えない。値が 0 の項目は「0」と表示する | GControlDBRule の `AlternateBlank` は、既定(false)で「0 を空欄で表示する」設定で、値が無い項目を 0 にするものではなかった。当初は「空欄を許さない」と読み替えていて、値が無い項目が 0 と表示され、フォーカスが外れると 0 が書き戻されていた |
| 15 | `SControlDBText` などの `STextBox` を受け取る対応に、数値・日付・テーブル コンボの入力欄を渡すと、コンパイルの時点でエラーになる | 渡せてしまうと、表示用の文字列(和暦など)が行に書かれ、型が合わずに値なしになるため。入力欄の土台を分けて(1.2)、型で防いだ |
| 16 | ふりがな(`SControlDBRuby`)は、名称が書き換わるたびに、その場で合わせる(読みの追記と、名称を全部消したときに空にすること)。GControlDB は、名称の欄を離れるときにまとめて行っていた | 名称を消して入力し直すときに、前のふりがなが残ったまま新しい読みが足されるのを、入力している最中に見て分かるようにするため(2026-11-25 に決定)。追記する位置(末尾)は変えていない。住所のふりがな(`SControlDBPostAddr`)は、これまでどおり欄を離れるとき |
| 17 | 郵便番号から住所を引くのを、郵便番号を入力している最中にも行う(7 桁そろって、入力が止まってから 0.5 秒後。7.2.6)。GControlDB は、郵便番号の欄を離れたときだけ | 欄を移る前に、住所が入るのを見て確かめられるようにするため(2026-11-26 に決定)。住所が入っているときに書き換えないのは、これまでと同じ |
| 18 | 対応(`SControlDBItem`)に、登録が終わったときに呼ばれる入口(`OnEndAdd`)を足した | 入力の最中に動く処理(17)が、登録先の `SControlDB` から、郵便番号の DB を借りるため |

### 7.4 入れていないもの

| 内容 | 理由・扱い |
| --- | --- |
| `GControlDBCheck`、`GControlDBDateSplit`(日付を 4 つの欄に分けて入力) | FMKaiin で使っていない |
| ルールの確認用の表示(`GControlDBRule.DebugMode`。設定したコントロールの背景色を変える) | 使っていない |
| コンボ ボックスへのルールの設定 | 現在も、コンボの項目にはルールが 1 つも登録されていない |
| グリッドとの連携(comset の `GGridDB`) | SControlDB の側に足したものは無い。グリッドのセルに重ねるコントロールは、「指している行」を返す関数を渡した SControlDB に、ふつうのコントロールと同じように登録する(10.7) |
| フックのうち、型付きの行クラスを直接受け取る形 | `((SDataRow)row).Row` で取り出せる(7.2.5) |
| データ タグ(1 つの SControlDB で複数の行を扱うための区別)と、タグ付きのフック(`FetchRunExTag` / `UpdateRunExTag`)、ルールのタグ付きの登録 | 使っている箇所が無い(現在のフォームは、行ごとに GControlDB を分けている)。いったん作ったが削除した。必要になったら追加する |

### 7.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のフォームでの使用。検証は、検証用のフォームと、メモリ上の DataTable で行った |
| 2 | (確認済み)郵便番号からの住所の自動入力。郵便番号の DB の写しを使って、医療機関マスタの入力の画面で確かめた(入力の最中に住所が入ること、欄を離れたときに行へ書かれること。7.2.6)。実際のキーボードでの入力と、住所のふりがなの欄がある画面(いまは無い)では、確かめていない |
| 3 | ふりがなの自動入力。IME の読みを、プログラムから `ImeReading` に入れた状態で検証した。実機の IME での確認は 2.5 のとおり未実施 |
| 4 | 値が 0 の数値の項目の表示。S 系は「0」と表示する。GcNumberEx は、ルールの `AlternateBlank` が false(既定。月額費用の 12 欄など)のとき、0 を空欄で表示する設定になっている(コードを読んだ結果。実際の表示は確かめていない)。同じにする必要があれば、表示の設定として追加する |
| 5 | 日付の項目が、日付型ではなく文字列型の場合の書き戻し。書き込む文字列は GControlDB と同じ(`ToShortDateString()`)だが、検証は日付型の項目で行った |
| 6 | (確認済み)`AppSControlRule` が `AppDbRule` と同じ内容になっていること。全 133 項目を、テスト(`Test/RuleHarness.cs`)で比べている |
| 7 | 表示する前にフォーカスが動くと、空のコントロールの値が行に書き戻される(GControlDB も同じ)。`EndAdd` の後、画面を表示する前に `FetchAll` を呼ぶこと |
| 8 | 実際の DB を使った画面からの採番(7.2.7)。確かめたのは、確認用のプログラムでの、上限の手前と上限を超えたときの結果だけ |

---

## 8. 確認した内容と、未確認の点のまとめ

### 8.1 確認した内容

| 確認 | 結果 |
| --- | --- |
| プロジェクト全体のビルド(MSBuild、Debug) | 成功。追加したファイルに警告なし |
| `STextBox` / 入力規則のテスト | 68 件合格。書式・空白・自動変換・改行・文字数の規則、キー入力と IME 確定の経路(ウィンドウ メッセージを直接送る)、変換で 2 文字になる場合の順序、`KeyPress` での取り消し、デザイナーの書き出し判定、収まらない文字を縮める表示 |
| `SComboBox` のテスト | 64 件合格。値での選択・取得(int / short / 文字列 / bool / null / `DBNull`)、`DataSource` での `SelectedValue`、キー操作(↓・F4・Space)、読み取り専用(キー・クリック・ホイール・文字キー)、デザイナーの書き出し判定、プレースホルダー、収まらない文字を縮める表示 |
| `STableComboBox` のテスト | 69 件合格。値・文字列・位置での選択、絞り込み(かな・数値の列・途中一致)、Enter・Esc・フォーカスの移動での確定と取り消し、該当なし、`FallbackSelectedIndex`、矢印のクリック、`RowFilter` と行の追加・削除の反映、読み取り専用、収まらない文字を縮める表示 |
| `SNumberBox` のテスト | 65 件合格。表示用と編集用の形の切り替え、桁数・範囲・小数の制限、符号の反転、小数点のキーでの 1000 倍、全角の数字、空欄の扱い、`Text` からの読み取り、読み取り専用、デザイナーの書き出し判定、収まらない文字を縮める表示(既定が false であること) |
| `SDateBox` のテスト | 133 件合格。表示用・編集用の形(和暦・西暦、4 つの形、元号の境目)、入力の読み方(区切りあり・なし、元号付き、省略形、全角、存在しない日付)、確定・取り消し・範囲、和暦と西暦の切り替え、カレンダーのキー操作(日・月・年)、読み取り専用、デザイナーの書き出し判定、収まらない文字を縮める表示 |
| `SControlDB` のテスト | 90 件合格。ルールの設定(種類ごと、コントロールの種類ごと)、既存のルール(GControlDBRule)の読み替え、対応 8 種類の読み込みと書き戻し、値なし・行なし、同じ項目の表示し直し、値が変わらないときに書き換えないこと、`ClearAll`、読み取り専用、項目・コントロールからの検索、フック、`Update()`、`ISRow` を返す関数 |
| `SToolTip` のテスト | 15 件合格。表示と非表示(`Hide`、表示時間、次の表示、フォームの移動・終了、指している入力欄の破棄)、表示する位置(コントロールの下・コントロールからの位置・画面の座標)、フォーカスが動かないこと |
| `SMsgBox` のテスト | 19 件合格。文字列の解釈(改行・色・太字)、ボタンの並びと最初のフォーカス、ボタンを押したときの結果、Enter・文字のキー(Y など)・Esc、Esc で閉じたときの結果(ボタンの組み合わせごと)、幅の下限と長い文の折り返し |
| `SGridDB` のテスト | 58 件合格。列の作成(見出し・文字の位置)、列の幅(割合・ピクセル・残り、グリッドの大きさを変えたとき)、表示する文字列(項目の値、プログラムで作る文字列)、指している行と DBView の現在の行の連動、右ボタンでの行の選択、行を指定しての移動、行の追加の反映、誤った使い方での例外、セルの中で入力する機能(10.7)、列の種類(数値・金額・日付・プログラムで作る列)と行ごとの見た目(`RowStyle`。画面の外では描かれないので、`DrawToBitmap` で描かせて確かめる) |
| 入力ルールの定義の比較 | 136 件合格。`AppSControlRule` の全 133 項目が `AppDbRule` と同じ内容であること、ルール名の定数と銀行の一覧の列名が同じであること |
| `FormFrame` との組み合わせのテスト | 16 件合格。`FormFrame` を継承した検証用のフォームで、表示倍率に合わせた拡大、Enter での項目の移動と値の確定・行への書き戻し、一覧・カレンダーを開いている間の Enter、ファンクション キーで実行する前の確定 |
| `STableComboBox` の速さ | 3 万行・10 万行の検証用データで、入力・選択・一覧の開閉・条件の変更・一括読み込みの時間を測った(4.2.5) |
| 見た目 | 各状態(通常・フォーカス・無効・読み取り専用・枠線なし・一覧を開いた状態・数値の編集中・カレンダーを開いた状態)を画面に表示して、スクリーンショットで確認。開発 PC(表示倍率 120%、115 DPI)での確認 |
| FormMasterBasic2 の見た目 | 画面を表示して、スクリーンショットで確認(配置、文字が欄に収まること、背景の上の白いパネル、銀行の一覧を開いた状態)。データベースにはつながず、入力欄に見本の値を入れた状態での確認(S画面仕様の 1.5) |
| 2026-11-26 に足した見た目 | 画面に表示して、スクリーンショットで確認した(開発 PC、表示倍率 120%)。プレースホルダーの色と大きさ(医会会費の追加の画面)、収まらない文字を縮める表示(コンボ・テーブル コンボ・日付。通常・フォーカス時・切り替え後)、カレンダーの見出しの太字と送りの記号の色(日・月・年の表示)、年を選ぶカレンダー、入力必須の印(8 画面。ほかの画面の項目名は、文字と印が幅に収まることを計算で確かめた)、タブ(会員の入力画面) |
| `AppDbID.GetNewCode` | 確認用のプログラムで、会員コード・医療機関コード・担当者コードの上限の手前で次の番号が返り、上限を超えると null が返ることを確かめた(7.2.7) |
| `Controls/Common/` のコンパイル | Gc 系の DLL を参照せずにコンパイルできる(8.4 の CommonCheck)。`STabControl` と、`SLabel` の `Required` を足した後の状態で確かめた |
| `SCheckBox` / `SRadioButton` / `SProgressBar`・`SBusyIndicator` のテスト | `CheckHarness` 20 件、`RadioHarness` 21 件、`ProgressHarness` 25 件が合格 |

テストは、検証用のプログラムからコントロールを実際のフォームに置き、ウィンドウ メッセージを送って結果を調べる方法で行った。検証用のプログラムは、リポジトリの `Test/` にある(8.4)。件数は、8.3 の修正の後のもの(修正した不具合ごとに、再発を防ぐテストを足してある)。件数は、2026-11-26 に、すべてのテストをまとめて実行したときのもの(`sh Test/run.sh`。合わせて 799 件が合格)。`SLabel`・`STabControl`・`SFileSelect` には、自動のテストが無い。

### 8.2 未確認の点(どのコントロールにも共通)

| # | 内容 | 確認できる段階 |
| ---: | --- | --- |
| 1 | 実機の IME での入力と、読みの取得 | 画面に置いて手で操作する |
| 2 | Visual Studio のデザイナーでの配置・プロパティの編集 | 同上 |
| 3 | (確認済み。8.1)`FormFrame` を継承したフォームでの DPI 拡大、Enter での項目移動との組み合わせ | — |
| 4 | (確認済み)半透明の親(`SectionPanel`)の上に置いたときの見た目。FormMasterBasic2 で、角丸の外側にパネルの色が見えることを確認した | — |
| 5 | (確認済み。8.1 の SControlDB のテスト)`GControlDB` に当たる処理との組み合わせ(値の読み書き、入力ルールの適用、読み取り専用の切り替え) | — |
| 6 | グリッドのセルに重ねる使い方。テーブル コンボとチェック ボックスは確かめた。数値・日付・ふつうの入力欄は 10.7.4 の 4 | 使う画面を、実際の DB で動かす |
| 7 | 表示倍率 100%・150% など、120% 以外での見た目(収まらない文字を縮める表示・入力必須の印・タブを含む) | 別の PC、または表示設定を変えて確認する |
| 8 | プレースホルダーを表示している入力欄で、実機の IME を使って入力を始めたときの表示(変換中の文字と重ならないこと) | 画面に置いて手で操作する |
| 9 | 収まらない文字を縮めて表示する欄の、実際の操作での表示(フォーカスの出入り、IME での変換の開始)。確かめたのは、スクリーンショットとテストだけ | 画面に置いて手で操作する |
| 10 | 2026-11-26 に足したプロパティ(`SLabel.Required`、`ShrinkTextToFit`、`SComboBox.PlaceholderText` など)と、`STabControl` を置いた画面を、Visual Studio のデザイナーで開くこと。デザイナーは Visual Studio 側のビルドを使うので、リビルドする前に開くと、新しいプロパティを知らず、エラーになるか、保存したときに設定が消える。基本情報の画面で `Required` の設定が消えたのは、おそらくこのため | Visual Studio でリビルドしてから、画面を開く |
| 11 | 実際の DB につないだ画面での動作(読み込み・登録) | 画面を実際の DB で動かす |

### 8.3 レビューでの指摘と対応(2026-11-25)

作成したコードを、書いた本人とは別の 5 つのエージェントで検証した(入力欄と入力の選別、コンボ、数値と日付、SControlDB、全体の作り方)。作り直しが必要という指摘は無く、作り方(枠の自前の描画、自作のポップアップ、自前の絞り込み、数値・日付の自前の実装、SControlDB を自前で持つこと)は、標準の部品へ寄せる案と比べたうえで「現状が妥当」という評価だった。

#### 8.3.1 直したもの

| # | 指摘 | 対応 | 書いた場所 |
| ---: | --- | --- | --- |
| 1 | 日付・テーブル コンボの入力欄で、入力の途中のままファンクション キーで保存すると、入力した値が保存されない | 確定用のメソッド `CommitEdit()`(`ISEditCommit`)を設け、`SControlDB` の書き戻しが読む前に呼ぶようにした | 4.3 の 9、6.3 の 11、7.2.1 |
| 2 | 数値のルールの読み替えで、値が無い項目が 0 になる | 未入力を許すのを既定にした | 5.1、7.3 の 14 |
| 3 | 数値の入力欄で、削除によって桁数を超えると、値が範囲の端に変わる | 削除する前の文字列に戻す | 5.3 の 3 |
| 4 | 元号の期間に入らない和暦の日付を、別の日付として受け付ける | 読めないものとして扱う | 6.2.2、6.3 の 12 |
| 5 | 年・年月の形で元年を入力すると、前の元号で表示される | その年・その月の最後の日の元号で表示する | 6.2.2、6.3 の 13 |
| 6 | 数字だけの欄に、貼り付けでタブ文字が入る | 1 行の入力欄ではタブを捨てる | 2.2.2 |
| 7 | Ctrl+BackSpace で、目に見えない文字(DEL)が入る | 入れない | 2.2.2、2.3 の 9 |
| 8 | テーブル コンボで、矢印キーで行を指して Tab で抜けても、その行が選ばれない | 選ぶようにした | 4.3 の 8 |
| 9 | テーブル コンボで、選択中の行が一覧から外れたときに、通知なしで選択が消える | 選択の変更イベントを発生させる | 4.3 の 10 |
| 10 | `STextBox` を受け取る対応に、数値・日付・テーブル コンボを渡せてしまう | 入力欄の土台(`SEditBase`)を分けて、型で防いだ | 1.2、7.3 の 15 |

あわせて、使っている箇所が無い機能を削除した: フォーカスがあるときの背景色(`FocusBackColor`)、IME の変換の開始・終了のイベント、データ タグとタグ付きのフック(2.4、7.4)。このうち、フォーカスがあるときの背景色は、FormMasterBasic2 を画面で見た結果、入れることになり、作り直した(2.2.3。指摘されていた不具合が起きない作りにしてある: 読み取り専用・無効の切り替えでも表示を合わせる、元の色を `NormalBackColor` で読めるようにする)。

#### 8.3.2 まだ直していないもの

影響が小さい、または起きる条件が限られるもの。直すかどうかは未定。

| 対象 | 内容 |
| --- | --- |
| `STextBox` | 最大文字数の数え方が Byte のとき、ぎりぎりの位置にサロゲート ペアの文字(「𠮷」など)を入力すると、後半だけが入る。既定の Char と、ルールが使う TextElement では起きない |
| `STextBox` | 複数行の入力欄で、数え方が Byte・TextElement のとき、Enter での改行が最大文字数を超えて入る |
| `STextBox` | 最大文字数が大きい欄に、数万文字の文字列を貼り付けると、切り詰めの計算に時間が掛かる |
| `STextBox` | `MaxLength` は標準のプロパティを同じ名前で隠しているので、`TextBox` 型の変数を通すと標準の側の値になる |
| `STextBox` | 書式 `Ｖ`(異体字セレクター)が、キー入力では通らない(貼り付けでは通る)。現在のルールでは使っていない |
| `SComboBox` | `SelectByValue(DBNull.Value)` が、値が空文字列の項目を選ぶ。`SControlDB` は `DBNull` を null に直して渡すので影響しない |
| `STableComboBox` | `DataSource` を差し替えたときに、条件(`RowFilter`)や並び順の式が新しいテーブルに合わず例外になると、一覧の内容が古いまま残る |
| `STableComboBox` | 絞り込みで、濁点の有無を区別しない(「カ」で「ガ」が当たる)。仕様としてどちらがよいかは未定 |
| `STableComboBox` | 前のコントロールの `Validating` がフォーカスの移動を取り消した場合でも、矢印のクリックで一覧が開く |
| `SNumberBox` | `Text` に数値でない文字列を設定すると、数字だけを拾う(「03-1234」が 31234 になる)。5.3 の 7 の記述(未入力になる)と合っていない |
| `SNumberBox` | 小数点のキーの 1000 倍で、最大値を `decimal` の上限近くに設定していると例外になる。値が 0 の欄で「0」の後ろから入力すると、先頭の 0 が桁数に数えられる |
| `SDateBox` | 時刻付きの文字列を `Text` に設定すると、未入力になる。カレンダーで選んだとき、`DropDownClosed` が値の更新より先に発生する。入力の途中で読み取り専用にされると、入力が捨てられる |
| `SCalendarPopup` | 日をダブルクリックすると、2 回目のクリックが、閉じたカレンダーの下にあるコントロールに届く可能性がある(未確認) |
| `SControlDB` | ハイフン区切りで、区切りの数が入力欄より多いデータは、前から残す(GControlDB は後ろから残していた) |
| `SControlDB` | `ReadOnly` / `ReadOnlyAll` は、デザイナーで読み取り専用にしてあった欄の状態を覚えていない(解除すると編集できるようになる)。現在のフォームは、このメソッドを使っていない |
| `SControlDB` | 名称の欄で入力した後、フォーカスを動かさずに別の行を読み込むと、ためてあった IME の読みが新しい行のふりがなに入る可能性がある |
| `SControlDB` | 読み込みのフック(`FetchRunEx`)の中で起きた例外は、画面まで上がる(GControlDB は握りつぶして空欄にしていた) |

#### 8.3.3 これからの作業に関わる指摘

| 内容 | 扱い |
| --- | --- |
| 新しいフォームには、Gc 系のコントロール・コードを入れない(将来 Gc 系を外しても動くようにする)。2026-11-25 に決定 | S 系だけで書いたルールの定義 `AppSControlRule` を用意した(7.2.4)。既存のフォームが残っている間は `AppDbRule` と並べて持ち、すべて移し終えたら `AppDbRule` をやめる。採番(`AppDbID.GetNewCode`)も `AppSControlRule` を見るようにした(7.2.7)。`Controls/Common/` が Gc 系を参照せずにコンパイルできることは、テストの実行のたびに確かめている(8.4)。S 系の画面に残っている間接の依存は、8.3.4 |
| `Controls/Common/` にあるものは、最終的に comset へ移す予定(2026-11-25) | S 系のコントロールが使っている共通の部品(`PaintHelper`・`LayoutScale`・`MenuIconPainter`・`MenuIconKind`・`FontFallback`)、`IconBox` と、SControlDB を `Controls/Common/` へ移した(1.2)。このアプリのロゴは、共通のアイコンから外して `IconLogo` に分けた |
| グリッド(`GGridDB`)の S 系版 `SGridDB` を作る。グリッド本体(ComponentOne の `C1TrueDBGrid`)は使い続ける。置き場所は `Controls/Common/`(2026-11-25 に決定) | 作った(10 章)。セルに重ねた入力欄へ最初のキーを渡す処理も、Gc 系を参照せずに作った(10.7) |
| `FormFrame` は、Enter をコントロールより先に処理して次の項目へ移す。カレンダーや一覧を開いている間の Enter(指している日・行の確定)が、コントロールに届かない可能性がある | 対応した(S画面仕様の 1.3)。コントロールが「このキーは自分が使う」と答えるインターフェース `ISKeyUser` を設け、`FormFrame` はそれを尋ねる |
| ファンクション キーでの処理の前に、フォーカスがあるコントロールの入力を確定させる処理が、`SControlDB` を使わない画面(条件を入力する画面)にも要る | 対応した(S画面仕様の 1.3)。ファンクション キーの処理(`FunctionKeyBinder`)が、メソッドを実行する前に、フォーカスがあるコントロールの入力を確定する |
| グリッドのセルに重ねる使い方は、FormMasterBasic には無い | 医療機関マスタの入力の画面で確かめた(10.7)。数値・日付の入力欄を重ねる使い方は、10.7.4 の 4 |
| 元号の表が、`SDateText`(画面)と comset の `DateParse`(帳票)の 2 か所にある | 改元のときは両方に足すこと |
| 検証用のプログラム(テスト)がリポジトリに入っていない | `Test/` に入れた(8.4) |

#### 8.3.4 S 系の画面に残っている Gc 系への依存の検査(2026-11-26)

`UpdateForm/` の全 44 画面(.cs・Designer.cs・.resx の 132 ファイル)に、Gc 系・Yc 系・Uc 系のコントロールや型、それに依存するコード(`GControlDB`・`GGridDB`・`AppCombo`・`AppTableCombo`・`AppDbRule`・`AppGridCommon` など)が残っていないかを、別のエージェントで調べた。直接の使用は無かった(画面の冒頭の、「Gc 系のコントロール・コードは使っていない」という説明のコメントだけ)。間接の依存として見つかったものと、その扱い:

| 内容 | 扱い |
| --- | --- |
| `AppDbID.GetNewCode` が、桁数の上限を Gc 系の `AppDbRule` から引いていた(会員・医療機関マスタ・担当者マスタの画面が呼ぶ) | 直した。`AppSControlRule` から引く(7.2.7) |
| `AppDate.cs` が、ファイルとしては `UcDate` と `GGridDBCommon` に依存している(`SetDispSeireki`) | 直していない。S 系の画面が使っているメソッド(`GetDate` など)は、Gc 系を通らない。Gc 系を外すときに直す |
| `FormFrame.CheckEnabledFormKeyPreview` に、`GcComboBoxEx` / `GcDateEx` の判定が残っている | 直していない。S 系の部品は、その前の `ISKeyUser` の判定で処理が終わる(S画面仕様の 1.3) |
| 帳票を出す画面から `ReportFlex*` を通して開く `FormReportPreview` | Gc 系は使っていないが、S 系の見た目でもない。移行の範囲に入れるかは未定 |
| 進行状況の画面 `FormBg_Progress` | Gc 系は使っていない。部品は S 系 |

### 8.4 テスト(Test フォルダー)

S 系のコントロールと SControlDB が仕様どおりに動くかを、自動で確かめるためのプログラム。ソリューションのフォルダーの `Test/` にある。FMKaiin のプロジェクトには含めていない(アプリの実行ファイルには入らない)。

| ファイル | 対象 |
| --- | --- |
| `TextHarness.cs` | `STextBox` と入力の選別(`SInputFilter` / `SInputFormat`) |
| `ComboHarness.cs` | `SComboBox` |
| `TableHarness.cs` | `STableComboBox`(`bench 行数` を付けて実行すると、速さを測る) |
| `NumberHarness.cs` | `SNumberBox` |
| `DateHarness.cs` | `SDateBox`、日付の文字列の変換(`SDateText`) |
| `DbHarness.cs` | `SControlDB`、入力ルール、ルールの読み替え |
| `ToolTipHarness.cs` | `SToolTip`。位置を確かめるので、フォームを画面の中に表示する(実行中、小さなウィンドウが一時的に見える) |
| `MsgBoxHarness.cs` | `SMsgBox`。モーダルではなく、ふつうのウィンドウとして表示して確かめる(実行中、小さなウィンドウが一時的に見える)。音は鳴らさない |
| `CheckHarness.cs` | `SCheckBox` |
| `RadioHarness.cs` | `SRadioButton` |
| `ProgressHarness.cs` | `SProgressBar`・`SBusyIndicator` |
| `GridHarness.cs` | `SGridDB`(一覧の表示、列の種類と行ごとの見た目、セルの中で入力する機能)。グリッドのライセンスの情報を、実行ファイルに埋め込んで実行する(`run.sh` が、ライセンス コンパイラー lc.exe で作る。Visual Studio がアプリをビルドするときと同じ仕組み) |
| `RuleHarness.cs` | 入力ルールの定義の比較(`AppSControlRule` と `AppDbRule`)。ビルドした FMKaiin.exe を使う |
| `FrameHarness.cs` | `FormFrame` と S 系のコントロールの組み合わせ。ビルドした FMKaiin.exe を使う |
| `SControlDBRuleConverter.cs` | Gc 系の画面用のルールの定義を、S 系のルールに読み替える(7.2.4)。テストからだけ使う |
| `run.sh` | すべてのテストをビルドして実行する |

実行の方法(Git Bash):

```sh
sh Test/run.sh                # すべて
sh Test/run.sh DateHarness    # 指定したものだけ
```

- 先に、FMKaiin を Debug でビルドしておく(`FMKaiin/bin/Debug` の comset.dll などを参照する)。`RuleHarness` と `FrameHarness` は、ビルドした FMKaiin.exe の中のクラスを使うので、FMKaiin.exe が古いと、古い内容を確かめることになる。
- どのテストも、データベースには接続しない。
- 各テストは 1 つの実行ファイルになる。画面の外にフォームを作ってコントロールを置き、キー入力を送ったり値を設定したりして、結果が期待どおりかを 1 件ずつ比べる。すべて通ると、テストごとに「○ 件合格」と表示する。
- あわせて、`Controls/Common/` が Gc 系(GrapeCity の DLL)を参照せずにコンパイルできることを確かめる(CommonCheck)。`Controls/Common/` のすべての .cs を、comset.dll とグリッドの DLL(`C1.Win.C1TrueDBGrid.4.dll`)だけを参照に入れて、ライブラリ(`Test/bin/CommonCheck.dll`)としてコンパイルする。結果は「Controls/Common: Gc 系を参照せずにコンパイルできる」と表示し、できなければ `Test/bin/CommonCheck.log` に理由を書く。テストの名前を指定したときも、毎回行う。
- `SLabel`・`STabControl`・`SFileSelect` のテストは無い(`SLabel` の入力必須の印と `STabControl` は、画面に表示して確かめた。8.1)。
- 実機の IME、実際のマウス操作、見た目は確かめられない(8.2)。
- コントロールを直したときは、これを実行して、ほかの動きを壊していないことを確かめる。不具合を直したときは、その不具合を再現するテストを足す。
- 実行するのは、変更した部分に関係するテストだけでよい(名前を指定する)。アイコンや色・配置だけの変更では、テストは実行せず、ビルドと画面の表示で確かめる。すべてを実行するのは、共通の土台(`SEditBase` など)や、複数のコントロールにまたがる変更のとき(2026-11-25 に決定)。
- テストは、見た目の値(色・高さ・フォントの大きさ・位置)を決め打ちの値と比べない書き方にしてある。画面を見ながら調整するたびに、失敗しないようにするため。
- フォーカスの移動を確かめるテストがあるので、実行中にほかのウィンドウを操作すると、失敗することがある。失敗したテストだけを実行し直す。

---

## 9. SToolTip

comset の `ComponentForm.ToolTip` の代わり。フォームの上に、アイコン・タイトル・本文の付いたツールチップを表示する。入力の誤りを、その入力欄のそばで知らせるのに使う(「入力必須項目 / 空欄にする事はできません。」など)。

`ComponentForm.ToolTip` は、標準の `System.Windows.Forms.ToolTip` を包んだもので、見た目は Windows が描く(色やフォントを変えられない)。`SToolTip` は、S 系のコントロールに合わせた見た目で、自前で描く。

### 9.1 ファイルと、使われ方

| ファイル | 内容 |
| --- | --- |
| `Controls/Common/SToolTip.cs` | 表示・非表示の管理。画面からは、このクラスを使う(public) |
| `Controls/Common/SToolTipPopup.cs` | ツールチップのウィンドウ(描画)。`SPopupForm` を継承。フォームから直接は使わない |

画面は、これまでどおり `appToolTip.Show(ctl, AppToolTipIndex.CannotUseBlank)` のように呼ぶ(`appToolTip` は `FormFrame` が持つ `AppToolTip`)。`AppToolTip` が中で使うクラスを、`ComponentForm.ToolTip` から `SToolTip` に差し替えた(2026-11-25)。画面のコードは変えていないが、**`AppToolTip` を使うすべての画面(30 ファイル)のツールチップが、新しい見た目になる**。

`AppToolTip.cs` は、`_doc/AppToolTip.xlsx` と `_doc/AppToolTip.cs.template` から自動で作られるファイルなので、テンプレートも同じように直してある(作り直しても元に戻らない)。差し替えたのは、クラス名の 4 か所だけ。元に戻すときは、両方のファイルで `App.Controls.SToolTip` を `ComponentForm.ToolTip` に戻す。

`UcImgButton`・`UcImgButton2`(以前のメイン画面のボタン)は、`ComponentForm.ToolTip` のまま(どの画面も、ツールチップを設定していない)。

### 9.2 対応

| ComponentForm.ToolTip | SToolTip |
| --- | --- |
| `new ToolTip(form)` | `new SToolTip(form)` |
| `Info` / `Warning` / `Error` / `Tips`(それぞれ、画面の座標・コントロールからの位置・コントロールの下、の 3 通り) | 同じ名前・同じ引数。`Warning` は警告の音、`Error` はエラーの音を鳴らす(同じ) |
| `Show`(3 通り) | 同じ名前・同じ引数。音は鳴らさない(同じ) |
| `Hide()` | 同じ |
| `AutoPopDelay` | 同じ(表示しておく時間。既定 5000 ミリ秒) |
| `Caption` / `Text` | 同じ(表示しているツールチップのタイトルと本文。表示していなければ空) |
| `Active` | 名前は同じだが、意味を変えた(9.4 の 3) |

### 9.3 動作

| 機能 | 動作 |
| --- | --- |
| 見た目 | 白い背景に、枠線(入力欄の枠線と同じ色)と影。左にアイコン、その右にタイトル(太字、見出しと同じ紺色)と本文。フォントは S 系の入力欄と同じ(10.5 pt) |
| アイコン | `ToolTipIcon.Info` は青い丸に白い i、`Warning` は橙色の三角に白い !、`Error` は赤い丸に白い ×、`None` はアイコンなし。`MenuIconKind` の `InfoFilled`・`WarningFilled`・`ErrorFilled` で描く |
| 角の丸み | Windows 11 では、角を小さく丸め、枠線も Windows に描かせる(ウィンドウの角をなめらかに丸くできるのは、Windows だけなので)。それより前の Windows では、角は四角で、枠線は自前で描く |
| 長い文 | 幅 420(96 DPI 基準)を超える文は、折り返す |
| 表示する位置(コントロールの下) | 上端は、コントロールの下端から 3 あける。左端は、コントロールの左右の中央(`ComponentForm.ToolTip` と同じ。すぐ下の行の、項目名と入力欄の先頭を隠さないため)。画面の右にはみ出すなら左へ寄せ、下にはみ出すなら、コントロールの上に表示する |
| 表示する位置(そのほか) | 画面の座標、または、コントロールの左上からの位置に、ツールチップの左上を合わせる |
| フォーカス | 表示しても、フォーカスは動かない。キー入力は、そのまま入力欄に届く |
| 消えるきっかけ | 表示時間(`AutoPopDelay`)が過ぎた / `Hide()` を呼んだ / 別のツールチップを表示した / ツールチップそのもの、または、指している入力欄以外の場所をクリックした / フォームが動いた・大きさが変わった・ほかのウィンドウへ切り替わった・閉じた / 指している入力欄が破棄された |
| 表示しない場合 | タイトルも本文も空のとき。フォームを閉じた後 |

### 9.4 ComponentForm.ToolTip と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 見た目を、自前で描く | 新しいデザインに合わせるため(Windows が描くツールチップは、色・フォント・アイコンを変えられない) |
| 2 | ツールチップそのものや、ほかの場所をクリックすると消える。フォームがほかのウィンドウへ切り替わったときも消える | 読み終わったら、すぐ消せるようにするため。一覧・カレンダーと同じ土台(`SPopupForm`)の動き |
| 3 | `Active` は、「いま表示しているか」を返す。`ComponentForm.ToolTip` は、標準の `ToolTip.Active`(ツールチップの機能が有効か。ふつうは常に true)をそのまま返していた | `AppToolTip.Active` の説明が「現在アクティブかどうか」なので、それに合う意味にした。このアプリで `Active` を読んでいる箇所は無い |
| 4 | 例外を握りつぶさない。`ComponentForm.ToolTip` は、表示の処理を try で囲み、例外をログに書いて続けていた | 例外が起きる条件(閉じたフォーム、破棄されたコントロール)は、先に調べて何もしないようにした。それ以外の例外は不具合なので、隠さない |
| 5 | 指している入力欄の破棄を見張る処理を、消すたびに外す。`ComponentForm.ToolTip` は、表示のたびに足すだけで、外していなかった | 同じ入力欄に何度も表示すると、見張りが増え続けるため |
| 6 | コントロールの位置を、枠を含めた全体で求める | S 系の入力欄は、枠の内側だけがクライアント領域なので、クライアント領域を基準にすると、枠の分だけずれる |

### 9.5 入れていないもの

| ComponentForm.ToolTip のメンバー | 内容 |
| --- | --- |
| `SetToolTip` / `RemoveAll` | マウスを載せたときに表示するツールチップ。このアプリでは使っていない |
| `AutomaticDelay` / `InitialDelay` / `ReshowDelay` | 上の機能のための、表示までの時間 |
| `ToolTipIcon` プロパティ | 表示のたびにアイコンを指定するので、不要 |

### 9.6 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | Windows 10 での見た目(角が四角で、枠線を自前で描く側の表示)。確認したのは、開発 PC(Windows 11)だけ |
| 2 | 既存の画面(Gc 系のコントロールを使う画面)での、表示する位置。確認したのは、FormMasterBasic2 と、検証用のフォームだけ。`UcDate`・`UcTableComboBox` のように、入力欄を中に持つコントロールを指した場合の位置は見ていない |
| 3 | 画面の端の近くで、上や左へ寄せて表示する場合 |
| 4 | 表示倍率 100%・150% での見た目 |

---

## 10. SGridDB

comset の `GGridDBCommon`(ファイルは GGridDB.cs)の代わり。グリッド(ComponentOne の `C1TrueDBGrid`)に、列の定義とデータ(`DBView`)を結び付ける。グリッドそのものは、これまでどおり `C1TrueDBGrid` を使う。

一覧の表示(10.1〜10.6)と、セルに入力用のコントロールを重ねて入力する機能(10.7)がある。`GGridDBCommon` を使う 17 画面のうち、13 画面は、一覧を表示するだけ(FormHikiotoshi、FormKaiin、FormKaiin_DlgHistory、FormLogView、FormMamisImport_DlgConfirm、FormMamisImport_DlgLog、FormMasterBank、FormMasterBankCode、FormMasterIryoKikan、FormMasterKaihi、FormMasterShinryoka、FormMasterTantosha、FormSelectorBankCode)。文字列以外の列(数値・金額・日付・プログラムで作る文字列)と、行ごとの見た目(`RowStyle`)は、2026-11-26 に足した(10.2)。

残りの 4 画面(FormHikiotoshi_DlgEntry、FormKaiin_DlgChosei、FormKaiin_DlgEntry、FormMasterIryoKikan_DlgEntry)は、セルに入力欄を重ねて入力する。この機能は、FormMasterIryoKikan_DlgEntry を移すときに足した(10.7。2026-11-26)。4 画面とも S 系の画面(名前の後ろに 2 が付くもの)を作った。見本のデータで確かめたのは、医療機関マスタの入力(テーブル コンボとチェック ボックス)と、会員の入力のセルでの入力。引落データ(FormHikiotoshi2_DlgEntry)と会費調整(FormKaiin2_DlgChosei)が使う、数値・日付・ふつうの入力欄を重ねる使い方は、実際の DB では確かめていない(10.7.4)。

`GGridDBCommon` が Gc 系を参照しているのは、セルの中で入力する機能の中だけ(入力を始めたキーを、重ねた入力欄へ渡す処理など)。`SGridDB` は、セルの中で入力する機能も含めて、Gc 系を参照しない。

### 10.1 ファイル

| ファイル | 内容 |
| --- | --- |
| `Controls/Common/SGridDB.cs` | 本体。列の登録、グリッドへの列の作成、データの結び付け、列の幅、セルのツールチップ |
| `Controls/Common/SGridDBInput.cs` | 本体の続き(同じクラスを 2 つのファイルに分けてある)。セルの中で入力する機能(10.7) |
| `Controls/Common/SGridDBColumns.cs` | 列の定義(土台の `SGridDBColumn` と、`SGridDBText`・`SGridDBNumber`・`SGridDBCurrency`・`SGridDBDate`・`SGridDBUnbound`)、文字の横位置(`SGridDBCellDisp`)、文字列を作る関数の型(`SGridDBUnboundFetch`)、行ごとの見た目を決める関数の型(`SGridDBRowStyle`) |
| `Controls/Common/SGridStyle.cs` | グリッドの既定の見た目と動き(10.3) |

名前空間は `App.Controls`。comset の `DBView`(`ComponentDB`)と、`C1.Win.C1TrueDBGrid.4.dll` を参照する。

### 10.2 対応

| GGridDB | SGridDB |
| --- | --- |
| `AppGridCommon.StyleSet(grid)` | 要らない(`SGridDB` を作ると、見た目と動きを設定する) |
| `new GGridDBCommon(grid, form)` | `new SGridDB(grid, form)` |
| `Add(new GGridDBText(項目, 見出し, 幅[, 位置]))` | `Add(new SGridDBText(項目, 見出し, 幅[, 位置]))`。登録した列を返す |
| `GGridDBCellDisp` | `SGridDBCellDisp`(Default / Left / Center / Right) |
| `SetUnboundColumnFetch(関数)`(直前に登録した列に設定) | 列の `UnboundFetch` プロパティ。`new SGridDBText(…) { UnboundFetch = 関数 }` |
| 関数の型 `string f(GGridDBBase col, UnboundColumnFetchEventArgs e)`(行は `e.Row`) | `string f(SGridDBColumn col, int row)` |
| `col.UnboundValue` | 同じ(その行の、列の項目の値。行が無い場合と、データにその項目が無い場合は null) |
| `GGridDBNumber` | `SGridDBNumber(項目, 見出し, 幅)`。右寄せで、値をそのまま表示する(桁区切りは付けない) |
| `GGridDBCurrency` | `SGridDBCurrency(項目, 見出し, 幅)`。右寄せで、書式 `###,###,###,##0`(`GGridDBCurrency` と同じ)で表示する |
| `GGridDBDate` | `SGridDBDate(項目, 見出し, 幅)`。中央寄せで、和暦なら「R08/11/26」、西暦なら「2026/11/26」 |
| `GGridDBUnbound` | `SGridDBUnbound(項目, 見出し, 関数, 幅[, 位置])`。位置を省くと左寄せ |
| `SetRowStyles` | `RowStyle` プロパティ。関数の型は `void f(int row, C1.Win.C1TrueDBGrid.Style style)`。`EndAdd` の前に設定する |
| (なし) | `SetView(dv)`(表示するデータの差し替え) |
| (なし) | `Columns`(登録した列の一覧)、`Grid`(結び付けているグリッド)、列の `Visible`(列を表示するか。既定 true) |
| `EndAdd(dv)` | 同じ |
| `PerformResize()` | 同じ |
| `AppGridCommon.SetRow(grid, row)` | `SetRow(row)`(その行を指して、グリッドの上下の中央あたりに表示する。行の数より大きい番号は、最後の行にする。行が無ければ何もしない) |
| `EnableRightClickSelect` | 同じ(既定 true) |

列のクラスは、どれも、幅をピクセル(int)で指定する形と、割合(float)で指定する形の 2 通りのコンストラクターを持つ。項目名(`dataField`)は、どの列でも空にできない(空や null を渡すと `ArgumentException`)。`SGridDBUnbound` の項目名は、データに無い名前でもよい(そのときは `UnboundValue` が null になる)。データに無い列を作るときは、空文字列ではなく、画面の中で決めた名前を渡す(会員の入力画面の異動情報の詳細の列は、`F_IdoDetail` などの名前を付けている)。

使い方(担当者マスタの一覧):

```csharp
dvTanto = new DBView(テーブル, this.BindingContext);

sgrid = new SGridDB(grid, this);
sgrid.Add(new SGridDBText(t_tantosha.FCD_Tanto, "コード", 0.15f, SGridDBCellDisp.Right));
sgrid.Add(new SGridDBText(t_tantosha.FTNT_Name, "氏名", 0.4f));
sgrid.Add(new SGridDBText(t_tantosha.FTNT_Auth, "権限", 0.0f) { UnboundFetch = ubAuth });
sgrid.EndAdd(dvTanto);
```

### 10.3 動作

| 機能 | 動作 |
| --- | --- |
| 列の作成 | `EndAdd` で、登録した順に左から列を作る。デザイナーで作った列があれば消す。列の幅は、利用者が変えられない |
| 列の幅(ピクセル) | 幅を整数で指定する。96 DPI 基準で、表示倍率に合わせて拡大する |
| 列の幅(割合) | 幅を小数(`0.15f` など)で指定する。グリッドの幅から縦スクロール バーの幅を引いた値に対する割合(残りの幅に対する割合ではない) |
| 列の幅(残り) | 割合を `0.0f` にすると、ここまでの列の残りの幅をすべて使う。いちばん右の列に指定する |
| グリッドの大きさが変わったとき | 列の幅を計算し直す |
| 文字の位置 | 横は `SGridDBCellDisp` に従う(`SGridDBText` は、指定しなければ左寄せ)。縦は中央。見出しは、縦横とも中央。収まらない文字は、末尾を「…」にする |
| 表示する文字列をプログラムで作る列 | 列の `UnboundFetch` に関数を指定すると、項目の値をそのまま表示せず、関数が返した文字列を表示する(区分の値を名称にする、など)。関数の中では、`col.UnboundValue` でその行の項目の値を読める。関数が null を返すと空欄。`SGridDBUnbound` は、関数を必ず受け取る(null なら `ArgumentNullException`) |
| 数値・金額の列 | 右寄せ。金額(`SGridDBCurrency`)は、グリッドの列の `NumberFormat` に `###,###,###,##0` を渡して、3 桁ごとに区切る。`UnboundFetch` を指定した列では、書式は使わない |
| 日付の列(`SGridDBDate`) | 中央寄せ。値を、`SDateBox` の編集用の形(年月日)で表示する。和暦・西暦は、表示するときのアプリ全体の設定(`SDateBox.DefaultCalendarKind`)に従う。値が空・`DBNull`・日付として読めない・`DateTime.MinValue` のときは、空欄にする。中では `UnboundFetch` を使っているので、自分で `UnboundFetch` を指定しないこと |
| 列を隠す | 列の `Visible` を false にすると、列は作るが表示しない |
| 行ごとの見た目(`RowStyle`) | `EndAdd` の時点で関数が設定してあれば、グリッドに行ごとの見た目を尋ねさせ(`FetchRowStyles`)、行ごとに関数を呼ぶ。関数は、渡された `Style` の文字の色(`ForeColor`)などを設定する(会員の一覧では、退会した会員を Firebrick の文字にしている)。`EndAdd` の時点で null なら、後から設定しても使われない。指している行を選択の色で塗る設定(既定。`MarqueeStyle` が HighlightRow / HighlightRowRaiseCell)のときは、指している行には適用しない(選択の色の上で文字が読めなくならないように) |
| データの差し替え(`SetView(dv)`) | 条件を変えて読み直したときなどに、表示するデータを別の `DBView` に替える。列は作り直さない。入力中なら入力を終え、セルのツールチップを消してから差し替える。`EndAdd` の前に呼ぶと例外 |
| データの結び付け | `EndAdd(dv)` で、`dv` の内容を表示する。`dv` をフォームの `BindingContext` を渡して作っておくと、グリッドの「指している行」と `dv` の現在の行(`CurrentRow`)が連動する。行の追加・削除・変更は、すぐグリッドに反映される |
| キー操作 | 独自の処理は無い(↑ ↓・PageUp / PageDown などは、グリッドに備わっている動き)。Enter で訂正の画面を開く、などは、画面の側で書く(`GGridDBCommon` でも同じ) |
| マウスの右ボタン | 押した行を、指している行にする(`EnableRightClickSelect`。既定 true) |
| セルに収まらない文字列 | マウスを載せると、全体をツールチップ(`SToolTip`)で、セルのすぐ下に表示する。ほかのセルへ移る・グリッドの外へ出ると消える |
| 既定の動き(`SGridStyle`) | 並べ替え・列の移動・列や行の選択・行の高さの変更・行の追加と更新・複数行の選択は、させない。指している行は、行の全体を色で示す。横スクロール バーは出さず、縦スクロール バーは常に出す。`AppGridCommon.StyleSet` と同じ設定 |
| 既定の見た目(`SGridStyle`) | フォントは S 系の入力欄と同じ(10.5 pt)。行の高さ 24、見出しの高さ 28(96 DPI 基準。表示倍率に合わせる)。見出しと罫線の色は、テーブル コンボの一覧(`STablePopup`)と同じ。外枠は、入力欄の枠線と同じ色。指している行は、選択の色(`SControlStyle.SelectionBackColor`。RoyalBlue)に白い文字 |

### 10.4 GGridDBCommon と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 見た目の設定を、`SGridDB` の中に入れた。`AppGridCommon.StyleSet` は、画面ごとに呼ぶ必要があった | 呼び忘れを防ぐため。新しいデザインの見た目を、1 か所で持つため |
| 2 | 表示する文字列を作る関数は、列のプロパティで指定する。`GGridDBCommon` は「直前に登録した列」に設定するメソッドだった | 呼ぶ順番を入れ替えると別の列に設定される、という誤りを防ぐため |
| 3 | その関数の引数から、グリッドのイベントの型(`UnboundColumnFetchEventArgs`)をなくし、行の番号を渡す | 画面のコードが使っていたのは、行の番号(`e.Row`)だけのため |
| 4 | ピクセルで指定した列の幅、行の高さ、見出しの高さを、表示倍率に合わせる。`GGridDBCommon` と `StyleSet` は、ピクセルのまま使っていた | 表示倍率が高い PC で、文字に対して列や行が狭くなるのを防ぐため |
| 5 | セルの文字の縦位置を、中央にした。`GGridDBCommon` の既定は下寄せ | 行の高さを広げたので、下寄せでは文字が下に寄って見えるため |
| 6 | セルに収まらない文字列のツールチップを、セルのすぐ下に出す。`GGridDBCommon` は、マウス カーソルの 48 ピクセル下に出していた | どのセルのものかが分かりやすいため。マウスがグリッドの外へ出たときにも消すようにした |
| 7 | 引数の誤り(グリッドやフォームが null、`EndAdd` の後の `Add`、`EndAdd` を 2 回)は、例外にする。`GGridDBCommon` は、フォームに null を渡すと、別の場所で例外になっていた | 画面を作っている段階で気づけるようにするため |
| 8 | グリッドが破棄されるときに、イベントの登録を外す。`GGridDBCommon` は、`Dispose()` を呼ばないと外れず、どの画面も呼んでいなかった | 登録が残り続けるのを防ぐため |
| 9 | 見出しをクリックしても、並べ替えない。`GGridDBCommon` には並べ替えの仕組みがあったが、`HeadClick` イベントを受け取る画面が無く、どの画面でも働いていなかった | 使われていないため(10.5) |
| 10 | 行ごとの見た目は、指している行には適用しない | 選択の色(RoyalBlue)の上に、行ごとの文字の色(Firebrick など)を重ねると、読めなくなるため |
| 11 | 列の項目名を空にすると、例外にする(データに無い列も、名前を付けて登録する)。元の会員の入力画面は、項目名を空にした `GGridDBUnbound` を使っていた | 登録の誤りに、画面を作っている段階で気づけるようにするため |
| 12 | 表示するデータを差し替えるメソッド `SetView` がある | 条件を変えて読み直す画面(引落データの一覧など)で、列を作り直さずにデータだけを替えるため |

### 10.5 入れていないもの

それを使う画面を移すときに足す。

| GGridDB の機能 | 使っている画面 |
| --- | --- |
| セルの中で入力する機能のうち、入力の開始・終了の通知(`InputEnter`・`InputLeave`・`InputDisabled`)、Enter で下の行へ移る設定(`EnterFocusMoveDown`)、セルに重ねないコントロールへ移す指定(`SetFocusControl`) | 使っている画面は無い |
| 列を書き換えられないようにする指定(`SetLocked`)、後から文字の位置を変える指定(`SetCellDisp`) | FormLogView、FormMasterKaihi など。S 系の画面では、`SetLocked` は要らない(グリッドそのものでセルを書き換えられない設定にしてある。10.7.1)。文字の位置は、列を作るときの引数で決める |
| 分割(スプリット)が 2 つ以上あるグリッド | 使っている画面は無い(どの画面も、分割は 1 つ) |
| 見出しのクリックでの並べ替え、チェック ボックスの列、年・年月・月日の列、列ごとの色・フォント、ボタンの列、複数行の選択、1 件を複数行で表示する機能 | 使っている画面は無い |

### 10.6 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | セルに収まらない文字列のツールチップ。グリッドが、実際のマウス カーソルの位置を見てイベントを起こすので、自動では確かめられなかった。長い氏名のセルにマウスを載せて、確かめる |
| 2 | 実際のデータベースにつないだ表示(S画面仕様の 2.4) |
| 3 | 表示倍率 100%・150% での見た目。確認したのは、開発 PC(120%)だけ |
| 4 | Enter を押したときの、グリッドに備わっている動き(行が動くかどうか)。`GGridDBCommon` も一覧の画面では設定していないので、これまでと同じはず |
| 5 | 数値・金額・日付の列と行ごとの見た目を、実際のデータで表示したとき(会員の一覧の退会者の赤い文字は、見本のデータで確かめた)。行ごとの見た目は、テストでは画面の外のグリッドに描かれないので、`DrawToBitmap` で描かせて確かめた |
| 6 | `SetView` で差し替えた後の、実際の DB での表示(引落データの一覧) |

### 10.7 セルの中で入力する機能

グリッドのセルに、入力用のコントロール(テーブル コンボ、チェック ボックスなど)を重ねて入力する。`GGridDBCommon` の `SetFocusControlInGrid` などに当たる。コードは `SGridDBInput.cs`。最初に使った画面は、医療機関マスタの入力(S画面仕様の 6 章)の、標榜科目のグリッド。

仕組みは、`GGridDBCommon` と同じ。列ごとに、入力に使うコントロールを 1 つ決めておく。コントロールは、ふだんは見えない位置に置いておき、入力を始めるときにフォーカスを移す。フォーカスが入った時点で、指している行のセルの位置と大きさに合わせて表示し、フォーカスが出た時点で、見えない位置へ戻す。

コントロールと行の値の受け渡しは、`SGridDB` では行わない。画面の側で、「グリッドが指している行」を返す関数を渡した `SControlDB` を用意して、入力用のコントロールを登録する(ふつうのコントロールと同じ)。フォーカスが出たときに、`SControlDB` が値を行へ書き戻す。指している行が変わったときの読み込み(`FetchAll`)は、画面の側で行う(`DBView` の `RowFetchDemand` イベント。元の画面と同じ)。

#### 10.7.1 対応

| GGridDB | SGridDB |
| --- | --- |
| `gcom.SetFocusControlInGrid(コントロール)`(直前に登録した列に設定) | 列の `InputControl` プロパティ |
| `gcom.SetTabIndex(n)`(同上) | 列の `InputTabIndex` プロパティ。指定しなければ、左の列から順 |
| `gcom.SetCellDisp(位置)`(同上) | 列を作るときの引数(10.2) |
| `gcom.SetLocked(true)` | 要らない(グリッドそのものでは、セルを書き換えられない設定にしてある) |
| `gcom.SelectInput()` | 同じ |
| `gcom.SelectNextControl(forward)` | `SelectNextInput(forward)` |
| `gcom.CheckFocusControl(コントロール)` | `IsInputControl(コントロール)` |
| `gcom.Select()` | 同じ |
| (なし) | `IsInputActive`(セルで入力している最中か) |
| `uc.ComboBox` を `GControlDBCombo` に登録 | `STableComboBox` そのものを `SControlDBCombo` に登録 |

使い方(医療機関マスタの入力の、標榜科目のグリッド):

```csharp
sgrid_shinryo = new SGridDB(grid_Sinryo, this);
sgrid_shinryo.Add(new SGridDBText(t_iryokikan_shinryoka.FID_Shinryoka, "名称", 220, SGridDBCellDisp.Left)
{
    UnboundFetch = ubShinryokaName,
    InputControl = iHyoboKamoku,          // STableComboBox
    InputTabIndex = 1,
});
sgrid_shinryo.Add(new SGridDBText(t_iryokikan_shinryoka.FChk_Shinryoka, "診療科目", 0.0f, SGridDBCellDisp.Center)
{
    UnboundFetch = ubShinryokaCheckbox,
    InputControl = iShinryoKamokuChk,     // CheckBox
    InputTabIndex = 2,
});
sgrid_shinryo.EndAdd(dvIryokikanShinryo);

// 入力用のコントロールと、グリッドが指している行の受け渡し
sctl_shinryo = new SControlDB(this, getDataRowShinryo);
sctl_shinryo.Add(new SControlDBCombo(t_iryokikan_shinryoka.FID_Shinryoka, iHyoboKamoku));
sctl_shinryo.Add(new SControlDBCheckBox(t_iryokikan_shinryoka.FChk_Shinryoka, iShinryoKamokuChk));
sctl_shinryo.EndAdd(AppSControlRule.Rule);
```

`FormFrame` を継承する画面は、フォームが Enter を先に受け取って、次の項目へ移る。グリッドと入力用のコントロールでの Enter は、画面の `FormFrame_KeyDown` で、先に処理する(元の画面と同じ書き方)。S 系では、一覧を開いている間の Enter をコントロールに任せる判定(`CheckEnabledFormKeyPreview`。S画面仕様の 1.3)を足す。

```csharp
protected override void FormFrame_KeyDown(object sender, KeyEventArgs e)
{
    switch (e.KeyCode)
    {
        case Keys.Enter:
            if (this.ActiveControl == grid_Sinryo)
            {
                sgrid_shinryo.SelectInput();          // 指しているセルで、入力を始める
                return;
            }
            else if (sgrid_shinryo.IsInputControl(this.ActiveControl))
            {
                // 一覧を開いている間の Enter は、コントロールが使う(指している行の確定)
                if (base.CheckEnabledFormKeyPreview(e.KeyCode) == true)
                {
                    return;
                }
                sgrid_shinryo.SelectNextInput(!e.Shift);   // 次の入力欄へ
                return;
            }
            break;
    }
    base.FormFrame_KeyDown(sender, e);
}
```

#### 10.7.2 動作

| 場面 | 動作 |
| --- | --- |
| `EndAdd` | 入力用のコントロールを、見えない位置(親の左上の外)へ移して、Tab での移動先から外す。グリッドは、Tab でセルの間を移り、Enter では何もしない設定にする。入力用のコントロールを持つ列が 1 つも無ければ、何も変えない |
| 入力を始める | `SelectInput()`、グリッドで Enter、グリッドで文字を入力、セルのダブルクリック。指しているセルの列の入力用のコントロールを、セルに重ねて表示して、フォーカスを移す |
| 表示する位置 | セルの位置と大きさ(右と下の罫線の 1 ピクセルを除く)。コントロールの親は、グリッドと違っていてもよい(画面上の位置で合わせる) |
| 入力を始めない場合 | 行が無い、その列に入力用のコントロールが無い、コントロールが無効(`Enabled = false`)。グリッドにフォーカスを置くだけ |
| 指している行が見えていない | 行が見える位置までスクロールしてから、入力を始める |
| グリッドで文字を入力して始めた | その文字を、入力用のコントロールへ渡す(キーボードから入力したのと同じ経路。テーブル コンボなら、一覧の絞り込みが始まる)。チェック ボックスは、Space でオン・オフを切り替える(ほかの文字では、フォーカスが移るだけ)。Ctrl・Alt との組み合わせは、対象外 |
| 次の入力欄へ移る(`SelectNextInput(true)`) | `InputTabIndex` の小さい順(同じ値なら左から)で、次の入力欄へ移る。行の最後の入力欄からは、次の行の最初の入力欄へ移る。無効のコントロールと、表示していない列は飛ばす。前へ移る(`false`)は、その逆 |
| 移る先が無い | 最後の行の最後の入力欄から次へ、最初の行の最初の入力欄から前へ移ろうとすると、false を返す。入力を終えて、フォーカスはグリッドにある |
| 移る前の書き戻し | 移る前に、いったんフォーカスをグリッドへ移す。これで、いまの入力欄の内容が行へ書き戻されてから(`SControlDB`)、行が移る |
| 移った先の行が端にある | 見えている範囲のいちばん上・いちばん下の行へ移ったときは、その行が上下の中央あたりに来るようにスクロールする |
| 入力用のコントロールでの Tab | 次の入力欄へ移る(Shift+Tab は前へ)。Enter(画面の側の処理)と同じ動き |
| チェック ボックスなど、入力欄でないコントロールでの矢印キー | 何もしない |
| 入力を終える | フォーカスがほかのコントロールへ移った、`Select()` を呼んだ(グリッドにフォーカスを移す。入力中だった列を、指している列にする)、グリッドの大きさ・位置が変わった、グリッドをスクロールした |
| 指しているセルの表示 | 指している行の全体を色で示すだけで、どの列を指しているかは表示しない(一覧の画面と同じ見た目。`GGridDBCommon` を使う画面でも同じ) |

#### 10.7.3 GGridDBCommon と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 入力用のコントロールと、移る順番は、列のプロパティで指定する | 10.4 の 2 と同じ |
| 2 | 入力用のコントロールで Tab を押すと、次の入力欄へ移る。`GGridDBCommon` は、Tab を効かなくしていた(押しても何も起きない) | 見えない位置に置いたコントロールなので、フォームの Tab の順番で移らせるわけにはいかないが、何も起きないよりは、Enter と同じ動きのほうが分かりやすいため |
| 3 | チェック ボックスなどでの矢印キーを、効かなくした。標準の動きでは、フォームの中の別のコントロールへフォーカスが移る | 入力中のセルから、脈絡のないところへフォーカスが飛ぶのを防ぐため |
| 4 | グリッドで入力した文字は、メッセージ(WM_CHAR)で入力用のコントロールへ渡す。`GGridDBCommon` は、キー入力を送り直す仕組み(SendKeys)と、コントロールの種類(GcNumber・GcTextBox)ごとの処理を使い分けていた | コントロールの種類ごとの分岐を持たないため。入力できる文字の選別は、受け取ったコントロールが行う |
| 5 | 入力を始めた直後に、続きの文字がグリッドへ届いた場合も、入力用のコントロールへ渡す。`GGridDBCommon` は、捨てていた | IME で確定した文字列は、1 文字ずつ続けて届く。2 文字目以降が、フォーカスが移る前のグリッドに届いた場合に備えたもの(実機の IME では未確認。10.7.4) |
| 6 | 見えない位置へ移すとき、コントロールの大きさは変えない。`GGridDBCommon` は、8 × 8 に縮めて (0, -20) に置いていた | S 系の入力欄は、高さを縮められない場合があり、(0, -20) では下端が見えてしまうため |
| 7 | 入力用のコントロールのフォントを、書き換えない。`GGridDBCommon` は、入力を始めるたびに、グリッドのフォントを設定していた | グリッドも S 系のコントロールも、同じ既定のフォント(`SControlStyle`)を使っているため |
| 8 | 移る順番(`InputTabIndex`)が同じ値の列は、左から順に移る。`GGridDBCommon` は、同じ値の列へは移れなかった | 順番を指定しなくても、左から順に移れるようにするため |
| 9 | 入力中かどうかを、入力用のコントロールにフォーカスがあるかどうかで判定する。`GGridDBCommon` は、入力を始めたときに立てる目印で持っていて、ほかのコントロールへフォーカスが移っても、次にグリッドへフォーカスが入るまで立ったままだった | 実際の状態と食い違わないようにするため |
| 10 | 指している行が見えていないときは、スクロールしてから入力を始める。`GGridDBCommon` は、エラー ログを出して、グリッドへフォーカスを戻していた | 入力を始められるようにするため |
| 11 | 同じコントロールを 2 つの列に指定した場合と、フォームに置いていないコントロールを指定した場合は、`EndAdd` で例外にする | 10.4 の 7 と同じ |

#### 10.7.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | セルのダブルクリックで入力を始める動き。グリッドが、マウスの左ボタンでは実際のマウス カーソルの位置を見るので、自動では確かめられなかった |
| 2 | 実機の IME での入力(グリッドにフォーカスがある状態で、ひらがなを入力して確定したときに、文字が入力用のコントロールへ渡るか) |
| 3 | 実際のキーボードでの Tab・Shift+Tab と、チェック ボックスでの矢印キー。テストでは、キーのメッセージをコントロールへ直接送っているので、その手前の判定(フォームがキーを先に処理するかどうか)は通っていない |
| 4 | 数値・日付の入力欄(`SNumberBox`・`SDateBox`)と、ふつうの入力欄(`STextBox`)を、セルに重ねる使い方。この使い方をしているのは、引落データの入力(FormHikiotoshi2_DlgEntry。金額・メモ)と会費調整(FormKaiin2_DlgChosei。年月・金額・メモなど)。実際の DB では確かめていない。医療機関マスタの入力で確かめたのは、テーブル コンボとチェック ボックス(テストでは、標準の `TextBox` と `CheckBox`)。数値の入力欄は、グリッドで「-」や「.」から入力を始めたときの動きを、使う画面を移すときに確かめること(`GGridDBCommon` には、GcNumber 用の特別な処理があった) |
| 5 | マウスのホイールなどでグリッドをスクロールしたときに、入力を終える動き |
| 6 | 表示倍率 100%・150% での、入力用のコントロールとセルの重なり方。確認したのは、開発 PC(120%)だけ |

---

## 11. SMsgBox

comset の `ComponentForm.MsgBox` の代わり。メッセージ ボックス(確認・警告・エラーなどを表示して、ボタンで答えてもらうダイアログ)。見た目は、S 系のコントロールに合わせて自前で描く。

### 11.1 ファイルと、使われ方

| ファイル | 内容 |
| --- | --- |
| `Controls/Common/SMsgBox.cs` | メッセージ ボックス(フォーム)。デザイナーのファイルは無く、配置はコードで計算する |
| `Controls/Common/SMsgBoxButton.cs` | メッセージ ボックスのボタン。フォームから直接は使わない |

画面は、これまでどおり `AppMsgBox.Show(AppMsgBoxIndex.CancelClose)` のように呼ぶ。`AppMsgBox` が中で使うクラスを、`ComponentForm.MsgBox` から `SMsgBox` に差し替えた(2026-11-25)。画面のコードは変えていないが、**`AppMsgBox` を使うすべての画面(114 か所)のメッセージ ボックスが、新しい見た目になる**。

`AppMsgBox.cs` は、`_doc/AppMsgBox.xlsx` と `_doc/AppMsgBox.cs.template` から自動で作られるファイルなので、テンプレートも同じように直してある。差し替えたのは、クラス名の 5 か所だけ。元に戻すときは、両方のファイルで `App.Controls.SMsgBox` を `MsgBox` に戻す。

`FormMasterImport.cs` の 3 か所は、`ComponentForm.MsgBox.Show(…)` を直接呼んでいるので、これまでの見た目のまま。

### 11.2 対応

| ComponentForm.MsgBox | SMsgBox |
| --- | --- |
| `new MsgBox(owner, text, caption, buttons, icons, defbutton)` | 同じ引数 |
| `MsgBox.Show(…)`(引数の組み合わせ 10 通り) | 同じ名前・同じ引数 |
| `Result`(押されたボタン) | 同じ |
| `CloseReason` / `MsgBox.eCloseReason`(None / Default / WindowCancel) | 同じ名前(`SMsgBox.eCloseReason`) |
| `StartPosition`・`ShowDialog()`(`AppMsgBox` が使う) | 同じ(フォームなので、そのまま使える) |
| — | `PlaySound`(表示したときに音を鳴らすか。既定 true。テストで音を止めるためのもの) |

### 11.3 動作

| 機能 | 動作 |
| --- | --- |
| 見た目 | 白い背景に、アイコンと文字。下に、ボタンを置く帯(グリッドの見出しと同じ色)。タイトル バーは Windows のもので、色は、画面と同じ濃い紺色にしている(`TitleBarColor.ApplyStandard`。色を指定できるのは Windows 11 以降)。フォントは S 系の入力欄と同じ(10.5 pt) |
| アイコン | `MessageBoxIcon.Question` は青い丸に白い ?、`Information` は青い丸に白い i、`Warning` は橙色の三角に白い !、`Error` は赤い丸に白い ×、`None` はアイコンなし。ツールチップと同じ図柄・色(`MenuIconKind` の `QuestionFilled` などで描く) |
| ボタン | 角丸。右寄せで並べる。フォーカスがあるボタンを、濃い青に白い文字で示す(Enter を押したときに働くボタンが分かるように)。色は、ファンクション キーのボタンと同じ |
| ボタンの文字 | はい(Y) / いいえ(N) / OK / キャンセル / 再試行(R) / 中止(A) / 無視(I)。並びは、標準の MessageBox と同じ |
| 最初のフォーカス | `defbutton` で指定したボタン。ボタンの数より後ろを指定した場合は、先頭 |
| キー操作 | Enter・Space は、フォーカスがあるボタンを押す。← → と Tab で、ボタンを移る。Y・N・R・A・I は、その文字のボタンを押す(Alt は要らない)。Esc は、右上の × と同じ |
| × や Esc で閉じたときの結果 | 「はい・いいえ」は いいえ、「OK・キャンセル」「再試行・キャンセル」「はい・いいえ・キャンセル」は キャンセル、「中止・再試行・無視」は 中止、「OK」だけのときは None。`CloseReason` は `WindowCancel` になる(ComponentForm.MsgBox と同じ) |
| 改行 | 文字列の中の `\n` で改行する。行数に制限は無い |
| 長い文 | 幅 600(96 DPI 基準。全角で 40 文字ほど)を超える行は、折り返す。短い文でも、幅は 360 より狭くしない |
| 文字の色・太字 | 行の先頭に `@c(色の名前)` を書くと、その行から後ろの文字の色を変える(「削除すると元には戻せません」の赤い文字に使っている)。行の先頭に `@b` を書くと、その行だけ太字にする |
| 音 | アイコンに合わせた Windows の音を鳴らす(ComponentForm.MsgBox と同じ) |
| 表示する位置 | 親のウィンドウの中央(`AppMsgBox` が指定する)。常に最前面に表示する |
| 背景色 | `BackColor` に関係なく、白と帯の色で描く(`AppMsgBox` は、親のフォームの色を `BackColor` に設定してくるため) |

### 11.4 ComponentForm.MsgBox と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 見た目を、自前で描く。アイコンは、画像ではなく図形で描く | 新しいデザインに合わせるため。表示倍率が高くても、ぼやけない |
| 2 | 行数の制限をなくし、長い行は折り返す。ComponentForm.MsgBox は、2 行まで(3 行目から後ろは表示されない)で、折り返さず、文の長さだけ横に広がっていた | 長いメッセージが画面からはみ出すのを防ぐため |
| 3 | ボタンを右寄せにし、フォーカスがあるボタンを色で示す。ComponentForm.MsgBox は、中央に並べていた | ファンクション キーのボタン(登録・キャンセルが右)と、並べ方をそろえるため |
| 4 | Esc で閉じられる。「はい・いいえ」では、いいえ になる | × と同じ結果なので、安全な側に倒れる。キーボードだけで取り消せるようにするため |
| 5 | `@c(色)` の色の名前を、その行の中から探す。ComponentForm.MsgBox は、文字列全体の最初の ( ) を見ていたので、2 行目に書くと 1 行目のかっこを読んでいた | 誤りを直した |
| 6 | 知らない色の名前は、無視する(色を変えない) | 文字が見えない色になるのを防ぐため |

### 11.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際の画面からモーダルで開いたときの表示(親のウィンドウの中央に出ること、最前面に出ること)。検証は、ふつうのウィンドウとして表示して行った |
| 2 | 右上の × で閉じたときの結果(Esc と同じ処理を通るので、同じ結果のはず) |
| 3 | 表示倍率 100%・150% での見た目。確認したのは、開発 PC(120%)だけ |

---

## 12. SButton

銀行コード マスタの画面を作るときに作った部品。その画面の仕様は、S画面仕様の 4 章にある。

### 12.1 SButton

角丸のボタン。画面の中に置く、ファンクション キー以外の操作に使う。標準の `Button` を継承していて、`Click`・`Text`・`Enabled`・`TabIndex` などは、そのまま使える。

| 機能 | 動作 |
| --- | --- |
| 見た目 | 角丸(半径は `CornerRadius`。既定 6)。白から薄い青への縦のグラデーションに、枠線。色は、ファンクション キーのボタンと同じ。フォントは S 系の入力欄と同じ |
| `Primary`(既定 false) | true にすると、主な操作のボタンとして、濃い青に白い文字で表示する |
| `HighlightWhenFocused`(既定 false) | true にすると、フォーカスがある間、濃い青で表示する。メッセージ ボックスのボタン(`SMsgBoxButton`)が使っている |
| フォーカス | 濃い青で表示していないボタンは、入力欄と同じ色・太さの枠線で示す |
| マウス | 載せると少し暗く、押すとさらに暗くなる |
| 無効のとき | 灰色の文字と枠線にする |
| アクセス キー | 文字の中の `&` の次の文字を、アクセス キーにする(「はい(&Y)」)。下線は、Alt を押すなどしてキーボードの操作を始めるまでは表示しない(Windows の設定に従う) |
| 角丸の外側 | 親コントロールと同じ見た目にする(半透明のパネルの上でも、角が浮かない) |

テストは、メッセージ ボックスのテスト(`MsgBoxHarness`)が、ボタンを押す・Enter・アクセス キーを確かめている。画面の中に置いたときの見た目は、銀行コード マスタの一覧で確かめた。

---

## 13. SCheckBox と SRadioButton

チェック ボックス。`YcCheckBoxEx` と、標準のチェック ボックスの代わり。標準のチェック ボックスは、チェックの四角の大きさや色を変えられないので、四角と文字をすべて自前で描いて、見た目を調整できるようにした(2026-11-26)。

標準の `CheckBox` を継承しているので、`Checked`・`CheckState`・`CheckedChanged`・`AutoCheck`・`ThreeState`・`CheckAlign`・`TextAlign`・`Text`・`Enabled`・`TabIndex` などは、そのまま使える。`SControlDBCheckBox`(7.1)と、グリッドのセルに重ねる入力用のコントロール(10.7)にも、そのまま使える。

最初に使った画面は、医療機関マスタの入力(S画面仕様の 6 章。併設施設の 2 つと、標榜科目のセルに重ねるもの)。

ラジオ ボタン `SRadioButton` は、同じ考え方で作った、標準の `RadioButton` の代わり(13.6)。13.1〜13.5 はチェック ボックスについて書き、ラジオ ボタンで違うところだけを 13.6 に書く。

### 13.1 ファイル

| ファイル | 内容 |
| --- | --- |
| `Controls/Common/SCheckBox.cs` | チェック ボックスの本体。名前空間は `App.Controls` |
| `Controls/Common/SRadioButton.cs` | ラジオ ボタンの本体(13.6) |
| `Controls/Common/SCheckStyle.cs` | 2 つに共通のもの。印(四角・丸)の大きさと色の既定値、印と文字の配置、背景と文字の描画、状態(フォーカス・マウス・無効)ごとの色の決め方。印の形を描く部分だけを、それぞれの本体が持つ |
| `Test/CheckHarness.cs` / `Test/RadioHarness.cs` | テスト(8.4) |

### 13.2 プロパティ

寸法は 96 DPI 基準のピクセルで、描くときに表示倍率を掛ける。どれも、既定値のままなら、デザイナーは Designer.cs に書き出さない。

| プロパティ | 既定値 | 内容 |
| --- | --- | --- |
| `BoxSize` | 17 | チェックの四角の大きさ(1 辺)。標準のチェック ボックスは 13 で、それより上下左右に 2 ずつ大きくしてある。7 より小さい値は、7 にする |
| `CornerRadius` | 3 | 四角の角の半径 |
| `BoxBorderColor` | (168, 184, 206) | オフのときの、四角の枠線の色(`SButton` の枠線と同じ) |
| `BoxBackColor` | 白 | オフのときの、四角の中の色 |
| `CheckedColor` | (34, 103, 182) | オンのとき(と、どちらでもない状態のとき)の、四角の色 |
| `CheckMarkColor` | 白 | チェックの印の色 |
| `FocusBorderColor` | (64, 144, 232) | フォーカスがあるときの、四角の枠線の色(S 系の入力欄と同じ) |
| `Font` / `ForeColor` | S 系の入力欄と同じ(1.3) | 文字のフォントと色 |
| `BackColor` | 透明 | 背景の色。透明・半透明なら、親コントロールと同じ見た目を先に描く(半透明のパネルの上でも浮かない)。グリッドのセルに重ねるときは、白を指定する |
| `CheckAlign`(標準のプロパティ) | MiddleLeft | 四角の位置。左・中央・右と、上・中央・下の組み合わせ |

色のプロパティは、空(`Color.Empty`)を設定すると、既定の色に戻る。

### 13.3 動作

| 機能 | 動作 |
| --- | --- |
| 切り替え | クリック、Space、アクセス キー(文字の中の `&` の次の文字)で、オン・オフを切り替える(標準のチェック ボックスの動き)。`AutoCheck` を false にすると、利用者は切り替えられない(`SControlDB` の読み取り専用が、これを使う) |
| 四角 | 角丸の四角。オフは、白地に枠線。オンは、`CheckedColor` で塗って、チェックの印を描く。どちらでもない状態(`ThreeState` を使うときだけ)は、横線を描く |
| フォーカス | 四角の枠線を、`FocusBorderColor` にして太くする(入力欄と同じ示し方。太さは、表示倍率 × 1.5 を四捨五入。最小 2) |
| マウス | 載せると四角が少し暗く、押すとさらに暗くなる(`SButton` と同じ割合) |
| 無効のとき | 四角を灰色(オフは `SControlStyle.DisabledBackColor`、オンは灰色の塗り)、文字を灰色にする |
| 文字 | 四角の右(四角が右にあるときは左)に、すき間 5 をあけて、1 行で描く。収まらない分は、末尾を「…」にする(折り返さない)。`&` の下線は、Alt を押すなどしてキーボードの操作を始めるまでは表示しない(Windows の設定に従う) |
| 四角を中央に置く(`CheckAlign` が MiddleCenter など) | 文字なしで使う(グリッドのセルに重ねる使い方)。文字があると、四角と重なる |
| 自動の大きさ(`AutoSize`) | 四角 + すき間 + 文字が、ちょうど収まる大きさにする |

### 13.4 標準のチェック ボックスと変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 四角の大きさを、13 から 17 にした(`BoxSize` で変えられる) | 標準の大きさは、10.5 pt の文字に対して小さく見えるため(2026-11-26 に決定) |
| 2 | 四角の色・角の丸みを、プロパティで変えられる。標準は、Windows の設定の色で描かれ、変えられない | 画面のデザインに合わせて調整できるようにするため |
| 3 | 背景の既定を、透明にした | S 系の画面では、半透明のパネルの上に置くため |
| 4 | フォーカスを、四角の枠線の色と太さで示す。標準は、文字のまわりに点線を表示する | S 系の入力欄・ボタンと、示し方をそろえるため |
| 5 | 文字は 1 行。標準は、幅に収まらないと折り返す | 画面では 1 行の項目名として使うため。折り返しが要るようになったら、足す |
| 6 | `FlatStyle`・`Appearance`(ボタンの形にする指定)・`Image` は、見た目に反映しない | 自前で描いているため。使っている画面は無い |

### 13.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | Visual Studio のデザイナーへの配置(既定値を書き出さないことは、判定の処理をテスト済み) |
| 2 | 表示倍率 100%・150% での見た目。確認したのは、開発 PC(120%)だけ |
| 3 | 実際のマウスでの、載せたとき・押したときの色の変化(テストでは、クリックのメッセージを送って、切り替わることだけを確かめた) |
| 4 | アクセス キー(`&`)での切り替えと、下線の表示 |
| 5 | `YcCheckBoxEx` にあった固有の機能を、使っている画面があるか。医療機関マスタの入力の画面では、使っていなかった。ほかの画面を移すときに、確かめる |

### 13.6 SRadioButton

ラジオ ボタン。標準の `RadioButton` を継承しているので、`Checked`・`CheckedChanged`・`AutoCheck`・`CheckAlign`・`Text`・`Enabled`・`TabIndex` などは、そのまま使える。大きさ・色・配置・フォーカスの示し方は、チェック ボックスとそろえてある(共通の処理 `SCheckStyle` を使う)。最初に使った画面は、医療機関マスタの印刷プレビュー(S画面仕様の 6.6)。

チェック ボックスと違うところ:

| 項目 | 内容 |
| --- | --- |
| 印の形 | 丸。選んでいないときは、白地に枠線。選んだときは、`CheckedColor` で塗って、中に白い点を描く(点の直径は、丸の 0.42 倍) |
| プロパティの名前 | `CircleSize`(丸の直径。既定 17。チェック ボックスの `BoxSize` に当たる)、`CircleBorderColor`、`CircleBackColor`、`CheckedColor`、`DotColor`(中の点の色。`CheckMarkColor` に当たる)、`FocusBorderColor`。角の半径(`CornerRadius`)は無い。既定の色は、チェック ボックスと同じ |
| 選択 | 同じ親コントロールに置いたものが 1 つの組になり、1 つを選ぶと、ほかの選択が外れる。選んであるものをもう一度押しても、外れない。↑ ↓ ← → で、組の中の次・前のものへ移って、それを選ぶ(どれも、標準のラジオ ボタンの動き) |
| Tab での移動 | 組の中で、選んであるものだけが、Tab の移動先になる(標準の動き) |

標準のラジオ ボタンと変えた点は、チェック ボックス(13.4)と同じ(丸の大きさ 13 → 17、色を変えられる、背景の既定が透明、フォーカスを枠線で示す、文字は 1 行)。

未確認の点は、チェック ボックス(13.5 の 1〜4)と同じ。↑ ↓ での切り替えは、印刷プレビューの画面で、キーをフォームの入口へ渡す形で確かめた(実際のキーボードでは未確認)。

---

## 14. SProgressBar と SBusyIndicator

進行状況の画面(`FormBg_Progress`)のために作った、2 つの部品。画面の構成と、使い方は、S画面仕様の 8 章にある。

### 14.1 SProgressBar

進み具合を表示するバー。両端が丸い帯(下地)の上に、進んだ分を色で塗る。

| プロパティ | 既定値 | 内容 |
| --- | --- | --- |
| `Minimum` / `Maximum` | 0 / 100 | 値の範囲。下限が上限より大きくなる設定をすると、もう一方も同じ値にする |
| `Value` | 0 | いまの値。範囲の外の値は、範囲の端に丸める |
| `TrackColor` | (226, 232, 240) | 下地の色 |
| `BarColor` | (34, 103, 182) | 進んだ分の色(チェック ボックスのオンの色と同じ) |
| `BackColor` | 透明 | 背景の色 |

値を設定すると、すぐにその位置まで塗る(ゆっくり伸びる動きは無い)。帯の太さは、コントロールの高さ。フォーカスは受け取らない。無効のときは、進んだ分を薄い色で描く。標準の `ProgressBar` にある、進み具合が分からないときの表示(`Style = Marquee`)と、`Step` / `PerformStep` は無い(使っている画面が無い)。

### 14.2 SBusyIndicator

処理中であることを示す、回る輪。薄い色の輪(下地)の上を、色の付いた弧が回る。線で描くので、どの大きさ・表示倍率でも、にじまない。

| プロパティ | 既定値 | 内容 |
| --- | --- | --- |
| `Active` | true | 回すか。false にすると、止まった状態で表示する |
| `LineWeight` | 0.11 | 線の太さ。輪の直径に対する割合 |
| `TrackColor` | (226, 232, 240) | 下地の輪の色 |
| `IndicatorColor` | (34, 103, 182) | 回る弧の色 |
| `BackColor` | 透明 | 背景の色 |
| `IsRunning`(読み取りだけ) | — | いま、回っているか |

回るのは、`Active` が true で、コントロールが表示されている間だけ(その間だけ、タイマーを動かす)。非表示にする・フォームを閉じると、止まる。デザイナーでは、回さない。弧は、50 ミリ秒ごとに 24 度ずつ進む(1 回転に、約 0.75 秒)。輪は、コントロールの中央に、幅と高さの小さいほうに合わせて描く。フォーカスは受け取らない。

---

## 15. SLabel

1 行の文字列を表示するラベル。以前の `SingleLineLabel`(`Controls/`)を、名前を変えて `Controls/Common/` へ移した(1.2)。S 系の画面の、見出し・項目名・入力欄の間の「－」・範囲の「～」・表示するだけの文字に使う。`Control` を継承している(標準の `Label` ではない)。

### 15.1 対応

| SingleLineLabel / 標準の Label | SLabel |
| --- | --- |
| `SingleLineLabel` | `SLabel`(中身は同じ。名前空間は `App.Controls`) |
| `Text`・`Font`・`ForeColor`・`Enabled`・`Padding` | 標準のものをそのまま使う |
| `TextAlign`(`ContentAlignment`) | `TextAlign`(既定 MiddleLeft) |
| 必須の項目名を橙色にする(元の画面のラベル) | `Required`(印を付ける。15.2) |

### 15.2 動作

| 機能 | 動作 |
| --- | --- |
| 配置 | 文字の高さだけを基準に、上下の位置を決める(行間が広く設定されたフォントでも、隣の入力欄と高さがそろう)。左右の余白は付けないので、文字は `Left` の位置から始まる。`Padding` の分は内側に寄せる |
| 長い文字列 | 折り返さない。幅に収まらない分は、末尾を「…」にする |
| 無効のとき | 文字を灰色(`SystemColors.GrayText`)で描く |
| 背景 | 透明の背景(`BackColor = Transparent`)に対応する。親の描いた内容の上に、文字だけを描ける |
| フォーカス | 受け取らない(Tab の移動先にならない) |
| 既定の大きさ | 100 × 23 |
| 入力必須の印(`Required`。既定 false) | true にすると、文字列の後ろに、半角の `*` を描く。色は `SControlStyle.RequiredMarkColor`(214, 48, 49)。無効のときは灰色。印は `SLabel` が描き、`Text` には入れない。文字列と印のすき間は、文字の高さの 0.12 倍(最小 1 ピクセル) |
| 印と `TextAlign` | 文字列と印をひとまとまりにして、`TextAlign` の位置(左・中央・右)に置く。上下の位置は、印の無いときと同じ決め方 |
| 印の分の幅が足りない | 文字列のほうを「…」で省略して、印を描く。ラベルの幅が、印とすき間の幅にも足りないときは、印は描かない |
| 文字列が空 | 印も描かない |
| 読み上げ | 読み上げソフトなどに伝える名前は、`Required` が true なら、後ろに「（必須）」を足す(印は絵として描いていて、文字列に入っていないため)。役割は、表示用の文字列(`StaticText`) |

印の形(`RequiredMark`)・すき間(`RequiredMarkGapRatio`)は `SLabel.cs` の定数、色は `SControlStyle.RequiredMarkColor` にある。変えると、すべての画面に効く。どの項目名に `Required` を付けるか(プレースホルダーが「入力必須」「選択必須」の欄と組になる項目名。範囲指定の行は項目名に 1 つだけ)は、画面の側の決まりで、S画面仕様の 1.7 にある。

### 15.3 変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 名前を `SingleLineLabel` から `SLabel` に変え、`Controls/Common/` へ移した | S 系の部品と一緒に comset へ移せるようにするため(1.2) |
| 2 | 入力必須の印を、プロパティ(`Required`)で付ける。元の画面は、必須の項目名を橙色の文字にしていた | 色だけに頼らず、形でも示すため。印を部品が描くので、印の形や色を 1 か所で変えられる(2026-11-26 に決定。最初は全角の「＊」だったが、半角の `*` に変えた) |

### 15.4 入れていないもの

| 内容 | 理由・扱い |
| --- | --- |
| 「* は入力必須」の凡例 | 入れていない。必要なら、置く場所を決めて画面の側に足す |
| 折り返し、複数行 | 1 行の項目名として使うため |

### 15.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 表示倍率 120% 以外での、印の位置と、文字と印が幅に収まるか。8 画面は画面に表示して確かめ、残りの画面の項目名は、120% での文字の幅を計算して確かめた |
| 2 | Visual Studio のデザイナーで、`Required` を設定すること。Visual Studio でリビルドする前に画面を開くと、設定が消えることがある(8.2 の 10) |
| 3 | 読み上げソフトでの読み上げ |

---

## 16. STabControl

タブでページを切り替えるコントロール。標準の `TabControl` を、S 系の見た目で描いたもの(2026-11-26)。最初に使った画面は、会員の入力(`FormKaiin2_DlgEntry`。情報1〜3 の 3 ページ)。

標準の `TabControl` を継承しているので、`TabPages`・`SelectedIndex`・`SelectedTab`・`SelectedIndexChanged` などは、そのまま使える。ページ(`TabPage`)の追加と、ページの中のコントロールの配置も、標準と同じ。変えているのは、描画(タブの見出しと、ページの枠)と、ページを置く範囲だけ。

### 16.1 対応

| 標準の TabControl | STabControl |
| --- | --- |
| `TabPages` / `SelectedIndex` / `SelectedIndexChanged` | 同じ |
| `Padding`(見出しの文字のまわりの余白) | 同じ。既定は (16, 6)(標準は (6, 3)) |
| `Font` | 同じ。既定は S 系の入力欄と同じフォント(`SControlStyle.Font`) |
| ページの `BackColor` | `PageColor` が自動で設定する(16.2) |
| (なし) | `PageColor`・`FrameColor`・`AccentColor` |

### 16.2 動作

| 機能 | 動作 |
| --- | --- |
| `PageColor`(既定 (246, 249, 253)) | ページの背景色。ページを足したときと、この値を変えたときに、各ページの `BackColor` をこの色にし、`UseVisualStyleBackColor` を false にする(標準の「テーマの背景」では、ページだけ白くなって枠と合わないため) |
| `FrameColor`(既定 (196, 206, 220)) | ページとタブの枠線の色 |
| `AccentColor`(既定 (32, 100, 161)) | 選んでいるタブの上に引く線の色。線の太さは 3(96 DPI 基準。最小 2) |
| ページの枠 | 角丸(半径 6。96 DPI 基準)の四角を、`PageColor` で塗って `FrameColor` の線で囲む |
| 選んでいるタブ | 上の 2 つの角を丸めた形を `PageColor` で塗り、下の辺は線を引かずにページとつなげる。文字は見出しの色(`SControlStyle.HeadingTextColor`) |
| 選んでいないタブ | 塗らない。文字は薄い色 (92, 108, 134)。マウスを載せると、半透明の白で塗る |
| 無効のとき | タブの文字と線を薄くする(コントロール全体が無効のとき、またはページが無効のとき) |
| 見出しの右の空いている部分、角丸の外側 | 親コントロールの見た目を写す(背景の画像の上に置いても、四角く抜けない) |
| ページを置く範囲(`DisplayRectangle`) | 見出しの下から、枠線 1 本の内側まで。標準は、立体の枠の分だけ内側に取る |
| 見出しの大きさ | フォントと `Padding` から決まる(表示倍率が変わっても、文字に合う) |
| キー操作 | 標準と同じ。Ctrl+Tab / Ctrl+Shift+Tab と、見出しにフォーカスがあるときの ← → で切り替える。見出しにフォーカスがあるときは、選んでいるタブの内側に点線の枠を描く(キーボードで操作を始めたとき。Windows の設定に従う) |
| アクセス キー | 見出しの文字の `&` の下線は、キーボードで操作を始めるまで表示しない |

### 16.3 入れていないもの

| 内容 | 理由・扱い |
| --- | --- |
| 見出しを下・左・右に置く形(`Alignment`)、ボタンの形(`Appearance`)、見出しの画像 | 描画は、見出しが上にある形だけを想定している。使っている画面は無い |

### 16.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | Visual Studio のデザイナーで、`STabControl` を置いた画面(会員の入力画面)を開くこと |
| 2 | 表示倍率 120% 以外での見た目 |
| 3 | 実際のキーボードとマウスでの切り替え。確かめたのは、見本のデータで会員の入力画面を表示して、タブを切り替えた結果 |

---

## 17. SFileSelect

ファイル・フォルダーのパスを入れる入力欄。`UcFileSelect`(Gc 系のテキスト ボックスと「...」のボタンを並べたもの)の代わり(2026-11-26)。右端のボタン(3 つの点)を押すと、ファイル・フォルダーを選ぶダイアログが開き、選んだパスが入る。最初に使った画面は、MAMIS の取込(`FormMamisImport2` の取込 CSV ファイルの欄)。

`STextBox` と同じ土台(`SEditBase`)の上に作っているので、見た目、フォーカスの示し方、無効・読み取り専用のときの色、プレースホルダー(2.2.3)、収まらない文字を縮める表示(1.5。既定 true)は、`STextBox` と同じ。右端のボタンは、土台のサイド ボタン(2.2.4)。入力できる文字は制限しない(パスを手で入力・貼り付けもできる)。パスは `Text` で読み書きする。

### 17.1 対応

| UcFileSelect | SFileSelect |
| --- | --- |
| `FileDialogType`(`eFileDialogType`: FolderSelect / FileOpen / FileSave) | `FileDialogType`(`SFileDialogType`。同じ 3 つ。既定 FileOpen) |
| `FileFilter`(既定 `すべてのファイル(*.*)\|*.*`) | `FileFilter`(同じ既定値。空や null を設定すると既定値に戻る) |
| `FileFilterIndex`(既定 0) | `FileFilterIndex`(既定 0) |
| `CheckFilePathMethod`(選んだパスを確かめる関数) | `CheckFilePath`(`Func<string, bool>`。デザイナーには出さない) |
| `UcText` | `Text` |
| `UcTxtCtl`(中の `GcTextBoxEx`) | なし(コントロール自身が入力欄) |
| 「...」のボタンのクリック | 右端のボタンのクリック、または `ShowDialog()` |

### 17.2 動作

| 機能 | 動作 |
| --- | --- |
| 右端のボタン / `ShowDialog()` | `FileDialogType` のダイアログを開く。選んだパスを `Text` に入れる。取り消したときは何もしない。読み取り専用・無効のときは開かない(ボタンの点を薄く描く) |
| `CheckFilePath` | ダイアログで選んだパスを、入れてよいかを決める関数。false を返すと入れない。指定しなければ、いつも入れる。手で入力したパスと、ドラッグして落としたパスには使わない(`UcFileSelect` と同じ) |
| フォルダーを選ぶ(FolderSelect) | 説明「フォルダを指定してください。」。一覧の一番上はデスクトップ。最初に選んでおくフォルダーは、入力欄のパス(空なら `C:\`)。新しいフォルダーを作るボタンは出さない |
| ファイルを開く(FileOpen) | 題名「開くファイルを選択してください」。最初のファイル名は入力欄のパス、最初のフォルダーは入力欄のパス(空ならデスクトップ) |
| ファイルを保存(FileSave) | 題名「保存するファイルを選択してください」。最初のファイル名は、入力欄のパスのファイル名の部分。最初のフォルダーは FileOpen と同じ |
| ファイルのダイアログの共通の設定 | `Filter` と `FilterIndex` は、`FileFilter` と `FileFilterIndex`。閉じた後にカレント ディレクトリを戻す(`RestoreDirectory`)。存在しないファイル・パスでも警告を出さない(存在の確認は、使う側で行う) |
| ファイルのドラッグ | エクスプローラーからファイル(フォルダー)を落とすと、そのパスを `Text` に入れる。複数を落とした場合は先頭の 1 つ。ファイル以外のデータと、読み取り専用のときは受け付けない |
| フォーカス | フォーカスが移ってきたときに、全体を選択する(`HighlightText` が true) |

`SControlDB` の対応(`SControlDBText` など)は `STextBox` を受け取るので、`SFileSelect` は登録できない。値は、画面の側で `Text` を読み書きする。

### 17.3 UcFileSelect と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 入力欄とボタンを 1 つのコントロールにした。ボタンは枠の内側の右端に、3 つの点で描く | ほかの S 系の入力欄(テーブル コンボ・日付)と同じ形にするため |
| 2 | 読み取り専用・無効のときは、ダイアログを開かない。読み取り専用のときは、ドラッグしたファイルも受け付けない。`UcFileSelect` は、ボタンとドラッグの処理で、読み取り専用かどうかを見ていなかった | 読み取り専用の欄の値が変わらないようにするため |
| 3 | `FileFilter` に空を設定すると、既定値に戻す | ダイアログに空の `Filter` を渡さないため |

### 17.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 自動のテスト(`Test/`)が無い |
| 2 | Visual Studio のデザイナーへの配置 |
| 3 | 表示倍率 120% 以外での見た目 |
