# S 画面仕様

S 系のコントロール(名前が S で始まる部品)で作り直した画面の仕様をまとめる。部品そのものの仕様は [Sコントロール仕様.md](Sコントロール仕様.md) にある。

- 画面の一覧: 進み具合と、メニューからの開き方
- 1 章: FormMasterBasic2(S 系のコントロールで作った最初の画面)と、画面を S 系へ移すときの手順(1.6)、項目の配置と色の決まり(1.7)
- 2 章: 担当者マスタ(FormMasterTantosha2 と、入力・パスワード変更の画面。グリッドのある最初の画面)
- 3 章: 銀行情報(FormMasterBasic2_Bank と、入力・銀行コード選択の画面)
- 4 章: 銀行コード マスタ(FormMasterBankCode2 と入力の画面)
- 5 章: 診療科マスタ(FormMasterShinryoka2 と入力の画面)
- 6 章: 医療機関マスタ(FormMasterIryoKikan2 と、入力・印刷プレビューの画面。グリッドのセルの中で入力する、最初の画面)
- 7 章: 医会会費マスタ(FormMasterKaihi2 と入力の画面。数値の入力欄 SNumberBox を使う、最初の画面)
- 8 章: 進行状況の画面(FormBg_Progress)
- 9 章: 会費納付書の Excel 出力(FormOutExcelNofusho2。日付の入力欄 SDateBox を使う、最初の画面)
- 10 章: 年間会費徴収明細書の印刷条件(FormOutChosyuMeisaiYear2)
- 11 章: Excel 出力の 5 画面(年間会費徴収書類・振込データ一覧・振込依頼書・各種医会等申請書・市民病院会費請求資料)
- 12 章: 帳票の印刷条件の 7 画面
- 13 章: FB データ作成・iFAX データ作成(FormCreateBankFile2・FormCreateIfaxFile2)
- 14 章: 実行結果の画面(FormLogView2)
- 15 章: 引落データ作成(FormHikiotoshi2 と入力の画面)
- 16 章: MAMIS データ取込(FormMamisImport2 と、確認・取込結果の画面)
- 17 章: 会員情報(FormKaiin2 と、入力・異動情報・会費調整・在籍期間の画面。タブ STabControl を使う、最初の画面)
- 18 章: ログイン(FormLogin2)
- 19 章: 画面全体に関わること(並べて開く仕組み、アプリのアイコン、Gc 系の残り、デザイナーで開く前の注意、S 系の部品の置き場所と名前空間)

章と節の番号は、2026-11-26 に、文書ごとに 1 から付け直した(それまでは、Sコントロール仕様.md と分ける前の、通しの番号だった)。この文書の中を指す参照は、「1.7」「6 章」のように番号だけで書く。Sコントロール仕様.md の章・節を指すときは、「Sコントロール仕様の 7.2.4」のように、文書の名前を付けて書く。章を足すときは、最後の章の次の番号にする。

画面ごとの章は、おおむね「ファイル」「画面の構成」「元の画面と変えた点」「未確認の点」の順で書く。

| 項目 | 内容 |
| --- | --- |
| 記載日 | 2026-11-26(Sコントロール仕様.md から分けた日)。同じ日に、その時点までに作った画面を反映して、番号を付け直した |
| 状態 | 作成済み。S 系の画面は、`FMKaiin/UpdateForm/` の 44 画面(下の「画面の一覧」)。進行状況の画面(8 章)は、名前を変えずに、見た目を作り替えた。S 系の画面を作っていない画面も、「画面の一覧」に書いた |
| 置き場所 | 新しい画面は `FMKaiin/UpdateForm/`。進行状況の画面(`FormBg_Progress`)だけは、名前と置き場所を変えていないので、`FMKaiin/` の直下。S 系の画面のために足した、アプリの側のコード(`AppSControlRule.cs`・`AppSTableCombo.cs`・`AppSCombo.cs`・`AppBankCodeFilter.cs`・`RowSnapshot.cs`)も、`FMKaiin/UpdateForm/` にある(2026-11-26 に、`FMKaiin/` の直下と `FormHelpers/` から移した。名前空間は `App` のまま)。S 系の部品そのものは、共通ライブラリー comset の `UI/` フォルダーにある(名前空間 `ComponentSUI`。19.5) |

---

## 画面の一覧

`FMKaiin/UpdateForm/` にある S 系の画面は 44(2026-11-26 時点。どれも .cs / .Designer.cs / .resx の 3 つがそろっていて、`FormFrame` を継承している)。下の表の「メニュー」は FormMain2 のボタンで、ボタンの名前を括弧に書く。

| 置き換え前 | 置き換え後(`UpdateForm/`) | 章 | 開く元 |
| --- | --- | --- | --- |
| `FormLogin` | `FormLogin2` | 18 章 | `Program.cs`(アプリの起動時)。`FormLogin` はプロジェクトに残してあるが、どこからも開かない |
| `FormMasterBasic` | `FormMasterBasic2` | 1 章 | メニュー(`btnMasterBasic`) |
| `FormMasterTantosha` / `_DlgEntry` / `_DlgPwd` | `FormMasterTantosha2` / `_DlgEntry` / `_DlgPwd` | 2 章 | 一覧はメニュー(`btnMasterTantosha`)。ほかの 2 つは一覧から |
| `FormMasterBank` / `FormMasterBank_DlgEntry` | `FormMasterBasic2_Bank` / `FormMasterBasic2_Bank_DlgEntry` | 3 章 | 一覧は FormMasterBasic2 の「銀行情報」(F5)。入力は一覧から |
| `FormSelectorBankCode` | `FormSelectorBankCode2` | 3 章 | S 系の 3 つの入力の画面(`FormMasterBasic2_Bank_DlgEntry`・`FormMasterKaihi2_DlgEntry`・`FormKaiin2_DlgEntry`)。これまでの画面(`FormMasterBank_DlgEntry`・`FormMasterKaihi_DlgEntry`・`FormKaiin_DlgEntry`)は、これまでどおり `FormSelectorBankCode` を開く |
| `FormMasterBankCode` / `_DlgEntry` | `FormMasterBankCode2` / `_DlgEntry` | 4 章 | 一覧はメニュー(`btnMasterBankCode`) |
| `FormMasterShinryoka` / `_DlgEntry` | `FormMasterShinryoka2` / `_DlgEntry` | 5 章 | 一覧はメニュー(`btnMasterShinryoka`) |
| `FormMasterIryoKikan` / `_DlgEntry` | `FormMasterIryoKikan2` / `_DlgEntry` | 6 章 | 一覧はメニュー(`btnMasterIryoKikan`) |
| `FormOutIryoKikan`(医療機関マスタの印刷プレビュー) | `FormMasterIryoKikan2_Out` | 6.6 | `FormMasterIryoKikan2` の印刷プレビュー(F9) |
| `FormMasterKaihi` / `_DlgEntry` | `FormMasterKaihi2` / `_DlgEntry` | 7 章 | 一覧はメニュー(`btnMasterIkaiKaihi`) |
| `FormOutExcelNofusho` | `FormOutExcelNofusho2` | 9 章 | メニュー(`btnXlsNofusho`) |
| `FormOutChosyuMeisaiYear` | `FormOutChosyuMeisaiYear2` | 10 章 | メニュー(`btnRepChosyuMeisaiYear`) |
| `FormOutExcelNenkanChoshu` / `FormOutExcelFurikomi` / `FormOutExcelFurikomiIrai` / `FormOutExcelKaihiShinsei` / `FormOutExcelShimingyoinKaihi` | 同じ名前に 2 を付けたもの | 11 章 | メニュー(11.1 の表) |
| `FormOutHikiotoshiKakunin` / `FormOutSeikyuMinyukin` / `FormOutKaihiList` / `FormOutKaihiUchiwakeMonth` / `FormOutKaihiUchiwakeYear` / `FormOutKaihiYoteiYear` | 同じ名前に 2 を付けたもの | 12 章 | メニュー(12.1 の表) |
| `FormOutKaihiListBank` | `FormOutKaihiListBank2` | 12 章 | どこからも開かない。FormMain2 にボタンが無い(元の画面は、古いメニュー `FormReportMenu1` からだけ開く) |
| `FormCreateBankFile` / `FormCreateIfaxFile` | `FormCreateBankFile2` / `FormCreateIfaxFile2` | 13 章 | メニュー(`btnCreateBankFile`・`btnCreateIfaxFile`) |
| `FormLogView` | `FormLogView2` | 14 章 | `FormCreateBankFile2`・`FormCreateIfaxFile2` の作成実行(F11)の後 |
| `FormHikiotoshi` / `_DlgEntry` | `FormHikiotoshi2` / `_DlgEntry` | 15 章 | 一覧はメニュー(`btnHikiotoshi`)。編集は一覧から |
| `FormMamisImport` / `_DlgConfirm` / `_DlgLog` | `FormMamisImport2` / `_DlgConfirm` / `_DlgLog` | 16 章 | `FormMamisImport2` は、メニュー(`btnMamisImport`)と、`FormKaiin2` の MAMIS取込(F7)。ほかの 2 つは `FormMamisImport2` から |
| `FormKaiin` / `_DlgEntry` / `_DlgEntry_Idoinfo` / `_DlgChosei` / `_DlgHistory` | `FormKaiin2` / `_DlgEntry` / `_DlgEntry_Idoinfo` / `_DlgChosei` / `_DlgHistory` | 17 章 | 一覧はメニュー(`btnKaiin`)。異動情報は入力の画面から、ほかは一覧から |
| `FormBg_Progress`(「しばらくお待ちください」の画面) | 同じ名前のまま、中身を作り替えた(`FMKaiin/` の直下) | 8 章 | 20 か所あまり。名前と使い方は変えていないので、これを使うすべての箇所が、新しい見た目になる |

S 系の画面を作っていない画面: `FormOutBlankSetting`(帳票の余白の設定)。印刷条件の画面ではなく、スクロール バーと画像で余白を合わせるダイアログで、S 系にスクロール バー(スライダー)に当たる部品が無いため。開く元は、移行していない `FormReportPreview` だけ。FormMain2 の `RegisterMenuActions` のコメントにある「開く画面がまだ無いもの」(会員情報・会員一覧表などの帳票のボタンと、会費マスターの `btnMasterKaihi`)は、元の画面も無い。

メニューからの開き方(2026-11-26 時点): FormMain2 の `RegisterMenuActions` は、登録しているすべてのボタンを `OpenSideBySideOnClick` で登録している。これまでの画面と S 系の画面を、左右に並べてモードレスで開く(見比べるための一時的な仕組み。19.1)。`needsBankCode: true` を付けたボタンは、開く前に銀行コードを読み込む(元の登録と同じ指定にしてある)。マスターの 6 つの登録は利用者が行い、ほかは利用者の指示で、2026-11-26 にこちらで書き換えた(`FormMain2.cs` は、利用者が編集しているファイルなので、指示のあった箇所だけを書き換えること)。移行が済んだら、`OpenOnClick(ボタン, () => new 新しい画面(), …)` に戻して、並べて開く処理の region を消す(`FormMain2.cs` のコメントに書いてある)。

---

## 1. FormMasterBasic2

基本情報の画面(`FormMasterBasic`)を、S 系のコントロールで作り直したもの。S 系のコントロールと SControlDB を使う最初の画面で、ほかの画面を移すときの手本にする。

### 1.1 ファイル

| ファイル | 内容 |
| --- | --- |
| `FMKaiin/UpdateForm/FormMasterBasic2.cs` | 処理。`FormMasterBasic.cs` と同じ構成(同じメソッド名)にしてある |
| `FMKaiin/UpdateForm/FormMasterBasic2.Designer.cs` | 画面の構成。Visual Studio のデザイナーで編集できる形にしてある |
| `FMKaiin/UpdateForm/FormMasterBasic2.resx` | 空(リソースは持っていない) |
| `FMKaiin/UpdateForm/AppSControlRule.cs` | 入力ルール(Sコントロール仕様の 7.2.4) |
| `FMKaiin/UpdateForm/AppSTableCombo.cs` | 銀行の一覧の内容(列・幅・並び順)。`AppTableCombo.SetComboBox_Bank` と同じ内容 |

Gc 系のコントロール・コードは使っていない(`GcTextBoxEx`・`GcComboBoxEx`・`UcTableComboBox`・`YcLabelEx`・`GControlDB`・`AppDbRule`・`AppTableCombo` のどれも参照しない)。

### 1.2 画面の構成

`FormFrame` を継承している(1.4)。座標は、`FormFrame` を継承するほかの画面と同じく 96 DPI 基準で、`FormFrame` がロード時に画面の DPI に合わせて拡大する。

| 領域 | コントロール | 内容 |
| --- | --- | --- |
| 背景 | — | `Resources.BackgroundWave` を全面に表示(FormMain2 は `BackgroundWave2`。マスターの画面は、`BackgroundWave` にする。1.7) |
| 見出し | `icoHeading`(`IconBox`)、`lblHeading`(`SLabel`) | 斜線 2 本の飾りと「基本情報」 |
| 入力欄のパネル | `secBody`(`SectionPanel`。見出し帯なし、半透明、角丸、影。色は FormMain2 のセクションと同じ) | 項目名(`SLabel`)と入力欄を、この中に置く |
| ファンクション キー | `funckey`(`FunctionKeyBar`) | F5 銀行情報 / F11 登録 / F12 キャンセル(`FuncMasterBasic`。FormMasterBasic と同じ定義) |

| 項目 | コントロール | 種類 | 行の項目 |
| --- | --- | --- | --- |
| 名称・フリガナ | `iName`・`iNameFurigana` | `STextBox` | `BAS_Name`・`BAS_NameFurigana`(`SControlDBRuby`。名称を入力すると、その場で読みをフリガナの末尾へ追記する。名称を全部消すと、フリガナも空にする) |
| 代表者名 | `iNameDaihyo` | `STextBox` | `BAS_NameDaihyo` |
| 郵便番号・住所 | `iPost1`・`iPost2`・`iAddr1` | `STextBox` | `BAS_Post`・`BAS_Addr1`(`SControlDBPostAddr`。郵便番号を入力すると、住所が空なら住所を入れる) |
| 住所(2 行目) | `iAddr2` | `STextBox` | `BAS_Addr2` |
| 電話番号 | `iTel1_1`〜`iTel1_3` | `STextBox` | `BAS_Tel1`(`SControlDBHyphenSplit`) |
| FAX番号 | `iFax_1`〜`iFax_3` | `STextBox` | `BAS_Fax`(`SControlDBHyphenSplit`) |
| 法人番号 | `iHojinNo` | `STextBox` | `BAS_HojinNo` |
| 日付表示 | `iDispSeireki` | `SComboBox`(西暦 = true、和暦 = false) | `BAS_DispSeireki` |
| 振込元銀行 | `iBank` | `STableComboBox` | `ID_Bank` |

振込元銀行の項目名の横には、情報のアイコン(`icoBankInfo`。クリックすると入力欄へフォーカスを移す)を置いていたが、利用者がデザイナーで消した(2026-11-26)。処理の側の参照(クリックの登録と処理)も取り除いてある。項目名 `lblBank` の幅は、アイコンを置いていたときの 98 のまま(1.7 の「項目名の横のアイコン」)。

名称とフリガナは入力必須(登録のときに、空欄なら登録しない)。この 2 つの入力欄には、プレースホルダー(`PlaceholderText`。Sコントロール仕様の 2.2.3)に「入力必須」を設定してあり、空のときに薄い色で表示する。項目名(`lblName`・`lblNameFurigana`)は `Required = true` にしてあり、後ろに赤い `*` を出す(1.7)。

入力欄の高さは 24(S 系の既定は 28)。添付の画面イメージに合わせて、行の間隔を詰めたため。

配置(行の間隔、パネルの大きさと色)と項目名の色は、作成後に Visual Studio のデザイナーで調整されている。Designer.cs は、デザイナーで編集する(スクリプトなどで作り直さない)。調整後の値は 1.7 にまとめてあり、これから作る画面も同じ値にする。

### 1.3 FormFrame などに足した対応

S 系のコントロールを `FormFrame` の画面で使うために、既存の 3 か所に足したもの。どれも、Gc 系には依存しない。Gc 系のコントロールを使う画面の動きは変わらない。

| 場所 | 足したもの | 理由 |
| --- | --- | --- |
| `FormFrame.CheckEnabledFormKeyPreview` | フォーカスがあるコントロールが `ISKeyUser` で「このキーは自分が使う」と答えたら、フォームはそのキーを処理しない | `FormFrame` は、Enter をコントロールより先に受け取って次の項目へ移す。一覧やカレンダーを開いている間の Enter(指している行・日の確定)を、コントロールへ渡すため。コントロールの種類ごとの判定(`is GcComboBoxEx` のような分岐)を足さなくて済むよう、インターフェースにした |
| `FunctionKeyBinder.Exec` | メソッドを実行する前に、フォーカスがあるコントロールの入力の途中の内容を確定する(`ISEditCommit`) | ファンクション キーはフォーカスを動かさないので、日付やテーブル コンボの入力欄で入力した直後に実行すると、メソッドが「入力する前の値」を読んでしまうため。SControlDB を使わない画面(条件を入力する画面)でも効く |
| `AppDate.SetDispSeireki` | `SDateBox.DefaultCalendarKind` にも同じ設定を伝える | 基本情報の「日付表示」(和暦・西暦)を、S 系の日付の入力欄にも反映するため |

`ISKeyUser` を実装しているのは、`SDateBox`(カレンダーを開いている間の Enter・Esc・矢印・PageUp / PageDown・Space)、`STableComboBox`(一覧を開いている間の Enter・Esc・↑ ↓・PageUp / PageDown、入力の途中の Esc)、`SComboBox`(一覧を開いている間の Enter・Esc・↑ ↓・PageUp / PageDown)。

### 1.4 FormMasterBasic と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | FAX 番号の入力欄を、電話番号とは別に持つ。FormMasterBasic は、「電話番号 2」(`BAS_Tel2`)と「FAX 番号」(`BAS_Fax`)の 2 項目を同じ 3 つの入力欄に結び付けていて、入力した番号が両方の項目に書かれていた | 添付の画面イメージに「電話番号」と「FAX番号」の 2 行があるため |
| 2 | 「電話番号 2」(`BAS_Tel2`)は、表示も変更もしない。DB の値は、そのまま残る | 画面イメージに無いため。帳票が使っているのは `BAS_Tel1` と `BAS_Fax` だけで、`BAS_Tel2` を読んでいる箇所は無い |
| 3 | 見た目を、新しいデザイン(背景の画像、白いパネル、項目名は帯ではなく文字だけ)にした | 添付の画面イメージに合わせた |
| 4 | 土台のクラスは `FormFrame` のまま | Enter での項目の移動、ツールチップ、ファンクション キー、画面の中央への表示、DPI に合わせた拡大を、ほかの画面と同じ仕組みで使うため。`FormFrame` の中には Gc 系のコントロール用の処理が残っているが、S 系のコントロールはそれを通らない(1.3 のインターフェースで受ける)ので、将来 Gc 系の処理を消しても、この画面は変わらない |
| 5 | 保存の前に呼ぶのは `sctl.Update()`。FormMasterBasic は `updateCurrentControlValue(gctl)` | `updateCurrentControlValue` は、Gc 系のコントロール用(`GControlDB` を受け取り、`UcTableComboBox`・`UcDate` の中身を取り出す)。S 系は中身を取り出す必要が無い |
| 6 | 行を返す関数 `getDataRow` は、引数なし | Sコントロール仕様の 7.3 の 12 |

### 1.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースにつないでの動作(読み込み、登録、銀行情報の画面から戻ったときの一覧の作り直し)。検証は、データベースにつながずに、画面の表示(見本の値)と、`FormFrame` を継承した検証用のフォームでのキー操作・書き戻しで行った |
| 2 | 実機の IME での入力と、名称からフリガナへの読みの追記 |
| 3 | 郵便番号からの住所の自動入力。この画面では確かめていない(同じ処理を、医療機関マスタの入力の画面で、郵便番号の DB の写しを使って確かめた。Sコントロール仕様の 7.2.6) |
| 4 | Visual Studio のデザイナーで開いたときの表示 |
| 5 | 表示倍率 100%・150% での見た目。確認したのは、開発 PC(120%)だけ |
| 6 | 空欄のチェックで表示するツールチップ(`appToolTip.Show`)の、S 系の入力欄に対する表示位置 |

この画面は、メニュー(FormMain2 の「基本情報」)から、これまでの画面(FormMasterBasic)と並べて開く(「画面の一覧」の注)。

### 1.6 画面を S 系へ移すときの手順

FormMasterBasic2 を作ったときの手順。ほかの画面を移すときも、同じ順で行う。

| # | 作業 |
| ---: | --- |
| 1 | Designer.cs: `GcTextBoxEx` → `STextBox`、`GcNumberEx` → `SNumberBox`、`UcDate` → `SDateBox`、`GcComboBoxEx` → `SComboBox`、`UcTableComboBox` → `STableComboBox`。Gc 系に固有のプロパティの行(`ExFocusHighlight`・`ImeStr`・`SingleBorderColor` など)と、`ISupportInitialize` の `BeginInit` / `EndInit` の行は消す |
| 2 | `GControlDB` → `SControlDB`、`GControlDB○○` → `SControlDB○○`(Sコントロール仕様の 7.1)。コントロールの配列は `new STextBox[] { … }` にする。`uc.ComboBox`・`uc.UcDateCtl` のように中身を取り出している箇所は、コントロールそのものを渡す |
| 3 | `gctl.EndAdd(AppDbRule.Rule)` → `sctl.EndAdd(AppSControlRule.Rule)`。`AppDbRule.SetControlByRule` → `AppSControlRule.SetControlByRule` |
| 4 | `getDataRow(string tag)` → `getDataRow()`(Sコントロール仕様の 7.3 の 12) |
| 5 | `updateCurrentControlValue(gctl)` → `sctl.Update()`(1.4 の 5) |
| 6 | `AppTableCombo.SetComboBox_○○` を使っている場合は、同じ内容を `AppSTableCombo` に足して、そちらを呼ぶ |
| 7 | コンボ ボックスの項目の追加: `ExBeginUpdate` / `ExAddItem` / `ExEndUpdate` → `BeginUpdate` / `AddItem` / `EndUpdate` |
| 8 | フック(`FetchRunEx` / `UpdateRunEx`)の引数を `ISRow` にする(Sコントロール仕様の 7.2.5) |
| 9 | `sh Test/run.sh` を実行する。ルールを足した・変えた場合は、`AppDbRule.cs` と `AppSControlRule.cs` の両方を直す(Sコントロール仕様の 7.2.4) |
| 10 | 見た目を新しいデザインにする場合は、項目の配置と色を 1.7 の値にする |
| 11 | グリッド: `GGridDBCommon` → `SGridDB`、`GGridDBText` → `SGridDBText`、`GGridDBCellDisp` → `SGridDBCellDisp`。`AppGridCommon.StyleSet(grid)` の行は消す(`SGridDB` が設定する)。`gcom.SetUnboundColumnFetch(関数)` は、列のプロパティにする(`new SGridDBText(…) { UnboundFetch = 関数 }`)。関数の引数は `(SGridDBColumn col, int row)`(Sコントロール仕様の 10.2) |
| 12 | `AppCombo.SetComboBox` → `AppSCombo.SetComboBox`(引数は同じ) |
| 13 | Designer.cs のグリッド(`C1TrueDBGrid`)は、そのまま使う。新しく画面を作る場合は、元の画面の .resx にある `grid.Images`・`grid.PrintInfo.PageSettings`・`grid.PropBag` の 3 つを、新しい画面の .resx に写す |
| 14 | セルの中で入力するグリッド: `gcom.SetFocusControlInGrid(コントロール)` と `gcom.SetTabIndex(n)` は、列のプロパティにする(`new SGridDBText(…) { InputControl = コントロール, InputTabIndex = n }`)。`SetLocked` の行は消す。`CheckFocusControl` → `IsInputControl`、`SelectNextControl` → `SelectNextInput`、`AppGridCommon.SetRow(grid, 行)` → `sgrid.SetRow(行)`。`FormFrame_KeyDown` の Enter の処理には、一覧を開いている間の Enter をコントロールに任せる判定を足す(Sコントロール仕様の 10.7.1) |
| 15 | `YcCheckBoxEx` は、`SCheckBox` にする(Sコントロール仕様の 13 章)。`GControlDBCheckBox` → `SControlDBCheckBox` |
| 16 | 標準の `RadioButton` は、`SRadioButton` にする(Sコントロール仕様の 13.6)。Designer.cs の `BackColor`・`Font`・`ForeColor`・`UseVisualStyleBackColor` の行は、要らない(既定で、S 系の見た目になる) |
| 17 | `GroupBox`(項目のまとまり)は、置かない。ラジオ ボタンを囲んでいた `GroupBox` は、グループを示す小さな `SectionPanel` にして、ラジオ ボタンをその子にする(1.7 の「ラジオ ボタン」)。ほかの項目のまとまりは、まとまりの見出しを太字の文字(`SLabel`)で置く(1.7 の「項目のまとまり」) |
| 18 | 入力の画面を閉じるときの、破棄の確認(「入力内容をキャンセルします。編集した内容は破棄されますがよろしいですか？」)は、画面を表示してから、何か入力したときだけ出す。元の画面は、追加のときは、何も入力していなくても、必ず出していた(条件が「`Mode == eMode.Add` または `AppDb.CheckModified(srcrow, editrow)`」。追加のときは、元の行と編集する行が同じなので、この 2 つを比べても、入力があったかが分からないため)。S 系の画面では、`FormFrame_Shown` の最後で、編集する行の内容を控えて(`shownrow = new RowSnapshot(editrow)`。`UpdateForm/RowSnapshot.cs`)、閉じるときに `shownrow.IsModified(editrow)` で比べる。追加・訂正・コピー追加のどれも、同じ書き方。入力してから元の内容に戻したときは、出ない。2026-11-26 に、確認を出している 5 つの入力の画面(担当者・銀行情報・銀行コード・診療科・医会会費)を、この形にした。医会会費の入力の画面で、開いてすぐ閉じる・入力して閉じる(「いいえ」「はい」)・入力して元に戻す・金額を入れる・コンボを選ぶ、を確かめた。医療機関の入力の画面は、元の画面と同じく、確認そのものを出していない。そのあと、会員の 2 つの入力の画面(`FormKaiin2_DlgEntry`・`FormKaiin2_DlgEntry_Idoinfo`)も同じ形にした。会員の入力の画面は、子の一覧(医師会の会費・診療科目・所属学会・参加医会・異動情報)の行も比べる(17.3)。子の一覧の行は、追加のときなどに、呼び出し側(一覧の画面)が、この画面を開く前に足しているので、テーブルの変更の有無(`HasChanges`)では、入力があったかが分からないため。行を編集するだけの画面(`FormHikiotoshi2_DlgEntry`・`FormKaiin2_DlgChosei`)は、元の画面と同じく、ビューの `HasChanges` で調べる |
| 19 | 入力必須の欄には、プレースホルダー(`PlaceholderText`)を付ける。テキスト・日付・数値・ファイルの欄は「入力必須」、コンボ(`SComboBox`・`STableComboBox`)は「選択必須」。その欄の項目名(`SLabel`)は `Required = true` にする(1.7)。元の画面で、項目名の色で必須を示していた欄が対象。範囲指定(○○ ～ ○○)の行は、項目名に 1 つだけ付ける |
| 20 | 値の範囲で指定する区分のコンボ(在籍区分・支払方法・会費区分の From ～ To)は、`AppSCombo.SetComboBoxWithCode` で、「1：在籍」のように、値と名称を並べて表示する(帳票へ渡す値は、値そのもの。元の画面と同じ)。範囲に何が入るかを、値の並びで読めるようにするため(2026-11-26 に利用者と決めた) |
| 21 | 部品にプロパティを足したあと(たとえば `SLabel.Required`)に、その画面を Visual Studio のデザイナーで開くときは、先に Visual Studio でリビルドする(19.4) |

### 1.7 項目の配置と色(これから作る画面も同じにする)

FormMasterBasic2 で、Visual Studio のデザイナーを使って決めた値(2026-11-25)。S 系のコントロールで作る画面は、項目の余白と項目名の色を、この値に合わせる。座標と大きさは 96 DPI 基準。元の値は `FormMasterBasic2.Designer.cs` にある。

| 対象 | 値 |
| --- | --- |
| 見出し | 飾り(`IconBox`、`DoubleSlash`)は (16, 13) に 24 × 24、色 (32, 100, 161)、`LineWeight` 0.9。文字(`SLabel`)は (46, 10) に高さ 30、BIZ UDPゴシック 15 pt 太字、色 (17, 42, 74) |
| 入力欄のパネル(`SectionPanel`) | 左右はフォームの端から 10、上は y = 46。下端とファンクション キーの間は 20。`HeaderHeight` 0、`CornerRadius` 8、`ShadowSize` 6。色は、`FillColor` ARGB(108, 245, 245, 255)、`BorderColor` ARGB(235, 255, 255, 255)、`BorderGradientColor` ARGB(150, 196, 208, 222)、`ShadowColor` (60, 84, 120)、`BackColor` は透明 |
| 項目名(`SLabel`) | パネルの中の x = 26、幅 118、高さ 24。文字の色は (38, 86, 134)。BIZ UDPゴシック 10.5 pt。`BackColor` は透明 |
| 入力欄 | パネルの中の x = 150(項目名の右端から 6)。高さ 24。右端は、パネルの右端から 70 あける(幅 570 のパネルで、右端が 500) |
| 行 | 1 行目は、パネルの中の y = 24。行の間隔は 31(高さ 24 + すき間 7)。同じ項目の 2 行目(フリガナ、住所の 2 行目)は、間隔を 28〜29 に詰める。最後の行の下は 32 あける |
| 入力欄の間の「－」(`SLabel`) | 幅 20、高さ 24、中央寄せ。入力欄とすき間なしで並べる。文字の色は (31, 38, 62)(項目名の色にはしない) |
| 範囲を指定する 2 つの入力欄(～) | 2 つの入力欄の間に、「～」(`SLabel`)を置く。置き方は、上の「－」と同じ(幅 20、高さ 24、中央寄せ、入力欄とすき間なし、文字の色 (31, 38, 62))。範囲の行が続くときは、入力欄を同じ幅にして、「～」と右の欄の位置を、行の間でそろえる(10 章) |
| 日付の入力欄(`SDateBox`)の幅 | 年月日は 186、年月は 150、年は 116。どれもカレンダーのボタンを出す(年の欄は、年を選ぶカレンダーを開く)。和暦の表示(「令和 08年12月15日」)と、カレンダーのボタンが収まる幅。はじめは 170・140・90(年はボタンなし)にしていたが、170 では、年月日の末尾の「日」がボタンに隠れたので、2026-11-26 に広げた(会費納付書の画面の、表示倍率 120% の画像で確かめた) |
| 項目名の横のアイコン(`IconBox`) | 20 × 20、`Padding` 2。入力欄の左 24(x = 126)、行の y + 2。色は (30, 110, 190)。アイコンを置く行は、項目名の幅を 98 にする。この置き方をしていた FormMasterBasic2 のアイコンは、利用者が消した(1.2)ので、いまは、この置き方をしている画面は無い |

担当者マスタの 3 画面(2 章)を作ったときに、この表に足した決まり:

| 対象 | 値 |
| --- | --- |
| 項目名が長い画面 | 項目名の幅 118 に収まらない項目名がある画面は、その画面のすべての項目名の幅を広げ、入力欄の x もその分ずらす(パスワード変更の画面は、項目名の幅 140、入力欄の x = 172)。項目名と入力欄の間の 6 は変えない |
| 項目を左右 2 列に並べる画面 | 左の列は、これまでどおり(項目名 x = 26・幅 118、入力欄 x = 150)。右の列は、左の列の入力欄の右端から 24 あけて項目名を置く(銀行情報の入力は、項目名 x = 374・幅 84、入力欄 x = 464)。右の列の入力欄の右端から、パネルの右端までは 26(左の余白と同じ) |
| 検索欄のある一覧の画面 | 検索欄は、パネルの中の y = 20 に、項目名と入力欄を、左から横に並べる。グリッドは、その下に 12 あけて置く(y = 56)。項目名は、自分の入力欄に寄せて置く: 項目名の枠の幅は「文字数 × 14 + 4」(全角 1 文字の幅が 14。2 文字なら 32、3 文字 46、4 文字 60、6 文字 88)、項目名の枠と入力欄の間は 6(文字の右端から入力欄まで 10)、入力欄から次の項目名まで 32、最後の入力欄から検索クリアのボタンまで 18。項目名を必須の印付き(`Required`)にするときは、印の分だけ枠を広げる(FormHikiotoshi2 の「処理月度」は 60 → 74。15.2)。項目名の前と後ろのすき間が同じくらいだと、その項目名が、左の欄のものか右の欄のものか、見分けにくい。そのため、前のすき間を、後ろの 3 倍にしている。ボタンは、幅 100 で、右端をグリッドの右端にそろえる。余った幅は、名称の入力欄を広げて使う(幅を決めていない入力欄が 2 つあるときは、等分する)。項目名の枠を、文字数にかかわらず同じ幅にすると、文字数の少ない項目名(名称・コード)だけ、入力欄との間が広く見えるので、枠の幅は、文字数で決める。2026-11-26 に、4 つの一覧(銀行コード・診療科・医療機関・医会会費)を、この決まりで並べ直した。それまでは、文字の右端から入力欄まで 20、次の項目名まで 24 で、ほぼ同じだった。そのあと作った会員の一覧(17.2)も、この決まり。銀行コード選択の画面(`FormSelectorBankCode2`)の検索欄は、並べ直していない(項目名の幅 70) |
| 表示するだけの欄(入力できない欄) | `STextBox` を、無効(`Enabled = false`)にして使う。背景は灰色になる。設定は、Designer ではなく処理の側(`FormFrame_Shown`)で行う |
| 一覧(グリッド)の画面 | グリッドは、入力欄のパネル(`SectionPanel`)の中に、上下左右 16 あけて置く。グリッドの色・フォント・行の高さは、`SGridDB` が設定する(Sコントロール仕様の 10.3) |
| 項目名の文字 | 幅をそろえるための空白は入れない(「氏　名」→「氏名」)。左寄せで並べるので、要らない |
| フォームの背景の画像 | マスターの画面(`FormMaster～`)は、`Resources.BackgroundWave`(2026-11-25 に決定)。`BackgroundImageLayout` は `Stretch` |
| フォームの名前(クラス名・ファイル名) | メニューなどから直接開く画面は、元の画面の名前に 2 を付ける(`FormMasterTantosha` → `FormMasterTantosha2`、`FormSelectorBankCode` → `FormSelectorBankCode2`)。ほかの画面から開く画面は、開く元の画面の名前(2 を付けたもの)の後ろに、`_` と内容を表す名前を続ける。入力の画面は `_DlgEntry`(`FormMasterTantosha_DlgEntry` → `FormMasterTantosha2_DlgEntry`)。元の名前が、開く元と結び付いていない画面も、この形に直す(銀行情報の一覧 `FormMasterBank` は、基本情報から開くので `FormMasterBasic2_Bank`。その入力の画面は `FormMasterBasic2_Bank_DlgEntry`)。置き場所は `FMKaiin/UpdateForm/`(2026-11-26 に決定) |
| 画面の中のボタン | ファンクション キーに割り当てない操作(検索クリアなど)は、`SButton` を置く。高さは入力欄と同じ 24 |
| チェック ボックス | `SCheckBox`(Sコントロール仕様の 13 章)を使う。高さ 24。背景・フォント・文字の色は、既定のまま(透明、S 系の入力欄と同じフォントと色)。横に並べるときは、すき間を 6 あける |
| ラジオ ボタン | `SRadioButton`(Sコントロール仕様の 13.6)を使う。高さ 24。背景・フォント・文字の色は、既定のまま。1 つのグループのラジオ ボタンは、グループを示す小さな `SectionPanel` の子にする(同じパネルの中で 1 つだけ選べる。デザイナーで、グループごと動かせる)。パネルは、項目名の右(入力欄と同じ x = 150)に、行の y に合わせて置く。パネルの設定は、`HeaderHeight` 0、`CornerRadius` 6、`FillColor` ARGB(120, 255, 255, 255)、`BorderColor` (205, 214, 226)、`BackColor` は透明、影なし(この値は作業者が決めた。2026-11-26)。ラジオ ボタンは、パネルの中の (10, 5) から並べる。パネルの高さは、ラジオ ボタンの上下に 5 ずつ(1 行なら 34)。ラジオ ボタンがパネルの余白の分だけ下がるので、項目名も 5 下げる(行の y + 5)。パネルの後ろの行・パネルの下端は、ラジオ ボタンの下端を行の下端とみなして決める(パネルが最後の行なら、パネルの下端からパネル(`secBody`)の下端まで 27) |
| ラジオ ボタンを縦に並べる | 書類タイプ(どの書類を出すか)のラジオ ボタンは、すべての画面で縦に並べる(2026-11-26 に利用者が決めた)。行の間隔は 31(ふつうの行と同じ)。幅は、グループの中でそろえる。2 つならパネルの高さ 65、5 つなら 158(11 章)。医療機関マスタの印刷範囲(6.6)も縦 |
| ラジオ ボタンを横に並べる | 書類タイプでないもの(会費納付書の医師会、市民病院会費請求資料の医会)は、横に並べたまま。パネルの中で、すき間 6 で並べる。幅は、文字が 2 文字なら 66(9 章・11 章) |
| 入力必須の欄 | 入力欄に、プレースホルダー(`PlaceholderText`)を付ける。テキスト・日付・数値・ファイルの欄は「入力必須」、コンボ(`SComboBox`・`STableComboBox`)は「選択必須」(2026-11-26 に利用者が決めた)。色と大きさは、部品の既定(Sコントロール仕様.md のプレースホルダーの記述)。プレースホルダーは入力すると消えるので、これとは別に、項目名(`SLabel`)を `Required = true` にして、後ろに赤い半角の `*` を出す(印は `SLabel` が描く。項目名の文字(`Text`)には入れない。Sコントロール仕様の 15 章)。印は入力した後も残る。範囲指定の行は、項目名に 1 つだけ。2026-11-26 に、24 画面・57 個の項目名に設定した。印の分の幅が足りなかった 3 つだけ、項目名を広げた(FormHikiotoshi2 の `lblDateYM` 60 → 74・入力欄を右へ 14、FormMamisImport2 の `lblFile` 118 → 128・入力欄を右へ 10(右端はそのまま)、FormMasterBankCode2_DlgEntry の `lblNameKanaShiten` 84 → 88)。印を全角の「＊」から半角の `*` に変えた後は、広げなくても収まる可能性があるが、そのままにしてある。「* は入力必須」の凡例は、画面に出していない |
| 項目の多い入力の画面の行の間隔 | 会員の入力の画面(`FormKaiin2_DlgEntry`。17.3)は、項目が多いので、行の間隔を 28 に詰める。同じ項目の 2 行目(住所の 2 行目)と、2 つ並べる行(フリガナと氏名)は 26。ほかの画面は、これまでどおり 31 |
| 標準のコントロール | 画面に、標準のコントロール(`CheckBox`・`RadioButton` など)を直接は置かない。S 系の部品が無い種類は、S 系の部品を作ってから使う(見た目を、後から 1 か所で調整できるようにするため)。チェック ボックスとラジオ ボタンについて、利用者から指示があった(2026-11-26)。ほかの種類にも当てはめるのは、作業者の解釈 |
| 入力欄の横にグリッドを置く入力の画面 | グリッドは右の列に置く(左の列の入力欄の右端から 24 あける。右端からパネルの右端までは 26)。1 行目に、グリッドの見出し(項目名)と、行の追加・削除のボタン(`SButton`。幅 56、右寄せ、すき間 6、Tab の移動先にしない)を置く。グリッドは、2 行目から、左の列の最後の行の下端まで |
| グリッドのセルに重ねる入力用のコントロール(Sコントロール仕様の 10.7) | 入力欄・コンボは、`BorderStyle` を None にする(枠線と角丸をなくす)。チェック ボックス(`SCheckBox`)は、文字なし、`CheckAlign` を MiddleCenter、`BackColor` を白にする。デザイナーでは、グリッドの手前に置いておく(位置と大きさは、実行時に `SGridDB` がセルに合わせる) |
| 表示するだけの文字(最終更新の日時・担当者など) | `SLabel`。見出しの右(フォームの右端から 10 あける)に、右寄せで置く。BIZ UDPゴシック 9 pt、高さ 18。見出しに当たる文字(「最終更新:」)は項目名の色、値は (31, 38, 62) |
| 項目のまとまり(元の画面で `GroupBox` に入っていた項目) | 枠は置かず、まとまりの見出しを置く(`SLabel`。BIZ UDPゴシック 10.5 pt 太字、色は画面の見出しと同じ (17, 42, 74)、高さ 24)。まとまりの中の項目は、見出しの 31 下から始めて、見出しより 16 右へ下げる。次のまとまりの見出しは、前のまとまりの最後の行の下端から 17 あける(7 章)。会員の入力の画面(17.3)の「銀行情報」「処理区分」は、見出しの下に、ラジオ ボタンのグループと同じ設定の `SectionPanel`(`secBank`・`secShori`)を置いて、項目をその中に置いている。ラジオ ボタンを囲んでいた `GroupBox` は、上の「ラジオ ボタン」のとおり |
| 数値の入力欄を並べる(月ごとの金額など) | 項目名(幅 44)と入力欄(`SNumberBox`。幅 100)を、すき間 6 で並べる。合計の欄は、無効(`Enabled = false`)にした `SNumberBox`(表示するだけの欄と同じ)。合計は、月ごとの欄より桁が増えるので、幅は、合計の最大の桁数で決める(「数字を入れる欄の幅」。7 桁の欄が 12 個なら、合計は 9 桁で、幅 130)。右端を、月ごとの欄の右端にそろえて、左へ広げる |
| 数字を入れる欄の幅 | 入力できる桁数が収まる幅にする。最小の幅は「11 × 桁数 + 17」(2 桁 39、3 桁 50、4 桁 61、5 桁 72、7 桁 94、10 桁 127)。これに 3 ほど余裕を足す。数字 1 つが約 10.9、枠・余白・カーソルの分が約 16.7(表示倍率 120% での実測。数字は、どれも同じ幅)。これより狭いと、右寄せの欄では先頭の数字が欠ける。桁区切りを表示する欄(`SNumberBox`)は、区切り 1 つにつき 5 を足す。2026-11-26 に、幅が足りない欄があったので、実測して決めた(銀行コード 4 桁の欄が 56 で、先頭の数字が欠けていた) |
| フォームのタイトル(ウィンドウの上端の文字) | 「メイン名：サブ名」の形にする(区切りは全角のコロン)。例: 「担当者マスタ：追加」「担当者マスタ：訂正」「担当者マスタ：パスワード変更」。サブ名が無い画面は、メイン名だけ(「担当者マスタ」「基本情報」)。【 】や( )でサブ名を囲む書き方は使わない(2026-11-25 に決定。これから作る画面は、すべてこの形にする) |
| タイトル バーの色 | メイン画面(FormMain2)と同じ、濃い紺色(32, 64, 99)に白い文字。`FormFrame` が、ウィンドウを作るときに設定するので(`TitleBarColor.ApplyStandard`)、画面ごとの設定は要らない。`FormFrame` を継承する画面は、これまでの画面(Gc 系)も含めて、すべてこの色になる。`FormFrame` を継承しないメッセージ ボックス(`SMsgBox`)と進行状況の画面(`FormBg_Progress`)は、それぞれが同じ設定をしている。色を指定できるのは Windows 11 以降で、それより前では、標準の色のまま(2026-11-26) |

---

## 2. 担当者マスタ(FormMasterTantosha2)

担当者マスタの 3 つの画面を、S 系の部品で作り直したもの。グリッドのある画面を移した、最初の例。

### 2.1 ファイル

| ファイル(`FMKaiin/UpdateForm/`) | 元の画面 | 内容 |
| --- | --- | --- |
| `FormMasterTantosha2`(.cs / .Designer.cs / .resx) | `FormMasterTantosha` | 担当者の一覧。追加(F2)・訂正(F3、Enter、ダブルクリック)・削除(F4)・閉じる(F12) |
| `FormMasterTantosha2_DlgEntry`(同上) | `FormMasterTantosha_DlgEntry` | 担当者 1 件の入力。パスワード変更(F5。訂正のときだけ)・登録(F11)・キャンセル(F12) |
| `FormMasterTantosha2_DlgPwd`(同上) | `FormMasterTantosha_DlgPwd` | パスワードの変更。登録(F11)・キャンセル(F12) |
| `FMKaiin/UpdateForm/AppSCombo.cs` | `AppCombo` | 区分の辞書(値と表示名)から、`SComboBox` の項目を作る。権限の一覧に使う |

処理は、元の画面と同じ構成(同じメソッド名)にしてある。ファンクション キーの定義(`FuncMasterTanto` など)と、入力ルール(`AppSControlRule`。コード・氏名・パスワード)は、すでにあるものを使う。3 つとも、Gc 系のコントロール・コードは使っていない。

一覧の .resx は、元の画面のものを写した(グリッドの初期状態 `grid.PropBag` などを持っている)。入力の 2 画面の .resx は、空。

### 2.2 画面の構成

配置と色は、1.7 の値。3 画面とも、見出し・入力欄のパネル・ファンクション キーの構成は FormMasterBasic2 と同じ。

| 画面 | 項目 | コントロール | 行の項目 |
| --- | --- | --- | --- |
| 一覧 | グリッド(コード・氏名・権限) | `grid`(`C1TrueDBGrid`。`SGridDB` で結び付ける) | `CD_Tanto`・`TNT_Name`・`TNT_Auth`(権限は、区分の値を名称にして表示) |
| 入力 | コード | `iCode`(`STextBox`。入力必須) | `CD_Tanto` |
| 入力 | 氏名 | `iName`(`STextBox`。入力必須) | `TNT_Name` |
| 入力 | パスワード | `iPwd`(`STextBox`。入力必須。伏せ字。訂正のときは無効にして、F5 の画面で変える) | `TNT_Password` |
| 入力 | 権限 | `iAuth`(`SComboBox`。「なし」と「SuperUser」は出さない) | `TNT_Auth` |
| パスワード変更 | 現在のパスワード | `iPwd_Old`(`STextBox`。入力必須。伏せ字にしない) | — |
| パスワード変更 | 新パスワード・新パスワード(確認) | `iPwd_New`・`iPwd_New2`(`STextBox`。入力必須。伏せ字) | — |

入力必須の欄には、プレースホルダー「入力必須」を設定してあり、その項目名は `Required = true`(後ろに赤い `*`。1.7)。元の画面は、項目名の色(赤)で示していた。入力の画面とパスワード変更の画面を閉じるときの破棄の確認は、入力があったときだけ出す(1.6 の 18。パスワード変更の画面は、元の画面と同じく、確認を出さない)。

### 2.3 元の画面と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 入力の画面に、「診療所」の欄を置いていない。元の画面は、欄はあるが、非表示にしてあり、保存もしていなかった(該当の行がコメントになっている) | 使われていないため |
| 2 | 項目名と一覧の見出しの「氏　名」「権　限」を、「氏名」「権限」にした | 1.7 |
| 3 | 入力必須を、プレースホルダーで示す | 1.2 と同じ |
| 4 | 閉じるときの「入力した内容を行へ書き戻す」処理は、`sctl.Update()` の 1 行。元の画面は、フォーカスがあるコントロールの種類(`UcDate`・`UcTableComboBox`)ごとに分けていた | 1.4 の 5 と同じ |
| 5 | 一覧の、使っていない変数(`login_id`)をなくした | 使われていないため |
| 6 | タイトルは、「担当者マスタ：追加」「担当者マスタ：訂正」「担当者マスタ：パスワード変更」。元の画面は「担当者マスタ【追加】」「担当者マスタ【訂正】」「担当者マスタ(パスワード変更)」 | フォームのタイトルは「メイン名：サブ名」にする決まり(1.7) |

現在のパスワードの欄が伏せ字でないのは、元の画面と同じ(元の画面の Designer に、この欄だけ `PasswordChar` の指定が無い)。意図したものかは未確認。

### 2.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースにつないでの動作(一覧の読み込み、追加・訂正・削除と保存、追加した行への移動、パスワードの変更)。検証は、データベースにつながずに、見本のデータで行った。入力の画面は、読み込み・表示までは見本の行で動かして確かめた |
| 2 | 登録のときの確認(空欄・コードの重複)と、キャンセルのときの確認メッセージ |
| 3 | 一覧での Enter・ダブルクリックでの訂正 |
| 4 | Visual Studio のデザイナーで開いたときの表示 |
| 5 | 表示倍率 100%・150% での見た目 |

一覧の画面は、メニュー(FormMain2 の担当者マスターのボタン)から、これまでの画面と並べて開く(「画面の一覧」の注)。

---

## 3. 銀行情報(FormMasterBasic2_Bank)

銀行情報の 3 つの画面を、S 系の部品で作り直したもの。

### 3.1 ファイル

| ファイル(`FMKaiin/UpdateForm/`) | 元の画面 | 内容 |
| --- | --- | --- |
| `FormMasterBasic2_Bank`(.cs / .Designer.cs / .resx) | `FormMasterBank` | 銀行情報の一覧。追加(F2)・訂正(F3、Enter、ダブルクリック)・削除(F4)・閉じる(F12) |
| `FormMasterBasic2_Bank_DlgEntry`(同上) | `FormMasterBank_DlgEntry` | 銀行情報 1 件の入力。銀行コード選択(F5、コードの欄のダブルクリック)・登録(F11)・キャンセル(F12) |
| `FormSelectorBankCode2`(同上) | `FormSelectorBankCode` | 銀行コードの選択。検索クリア(F2)・選択実行(F11、Enter、ダブルクリック)・キャンセル(F12) |

処理は、元の画面と同じ構成(同じメソッド名)にしてある。ファンクション キーの定義(`FuncMasterBank` など)と、入力ルール(`AppSControlRule`)は、すでにあるものを使う。3 つとも、Gc 系のコントロール・コードは使っていない。

グリッドのある 2 画面の .resx は、元の画面のものを写した。入力の画面の .resx は、空。

FormMasterBasic2 の「銀行情報」(F5)は、`FormMasterBasic2_Bank` を開くようにした。

### 3.2 画面の構成

配置と色は、1.7 の値。入力の画面は、項目を左右 2 列に並べている。

| 画面 | 項目 | コントロール | 内容 |
| --- | --- | --- | --- |
| 一覧 | グリッド(銀行名・区分・口座番号) | `grid`(`SGridDB`) | 銀行名は、銀行コード・支店コードから引いた「銀行名　支店名支店」。区分は、口座区分の値を名称にしたもの |
| 入力 | 銀行コード・支店コード | `iBankCode`・`iShitenCode`(`STextBox`。入力必須) | `BNK_Code`・`BNK_CodeShiten`。入力するたびに、下の銀行名・支店名を引き直す |
| 入力 | 銀行名・銀行名カナ・支店名・支店名カナ | `iNameBank`・`iNameKanaBank`・`iNameShiten`・`iNameKanaShiten`(`STextBox`。無効) | 銀行コードの表から引いて、表示するだけ。行には書かない |
| 入力 | 口座区分 | `iKozaType`(`SComboBox`。「なし」は出さない) | `BNK_KozaType` |
| 入力 | 口座番号・会社コード | `iKozaNo`・`iCodeCompany`(`STextBox`。入力必須) | `BNK_KozaNo`・`BNK_CodeCompany` |
| 銀行コード選択 | 銀行検索・支店検索 | `iBank`・`iShiten`(`STextBox`) | 入力するたびに、一覧を絞り込む。数字だけならコードが一致するもの、1 文字なら名称かフリガナがその文字で始まるもの、2 文字以上なら含むもの |
| 銀行コード選択 | グリッド(銀行コード・銀行名・支店コード・支店名) | `grid`(`SGridDB`) | 開いたときは、選択されている銀行コードの行を指して、中央あたりに表示する。最初のフォーカスは、グリッド |

入力の画面のタイトルは、「銀行情報：追加」「銀行情報：訂正」「銀行情報：コピー追加」(1.7)。

登録のときに、銀行コード・支店コード・銀行名・支店名・口座番号・会社コードが空でないことを確かめる。銀行名・支店名が空なのは、入力したコードに当たる銀行・支店が無いとき(元の画面と同じ)。

### 3.3 元の画面と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 入力必須を、プレースホルダーと、項目名の後ろの `*`(`Required`)で示す。元の画面は、項目名の色で示していた | 1.2 と同じ |
| 2 | タイトルを「メイン名：サブ名」にした。元の画面は「銀行情報【追加】」など | 1.7 |
| 3 | 閉じるときの「入力した内容を行へ書き戻す」処理は、`sctl.Update()` の 1 行 | 1.4 の 5 と同じ |
| 4 | 絞り込みの条件を作る処理を、`AppBankCodeFilter.Create`(`FMKaiin/UpdateForm/AppBankCodeFilter.cs`)にまとめた。銀行コード選択と、銀行コード マスタ(4 章)が使う。元の画面は、2 つの画面のそれぞれで、銀行と支店について同じ処理を書いていた(計 4 か所)。作られる条件は同じ | 直すときに、一部だけ直す誤りを防ぐため |
| 5 | 入力の画面は、銀行コード選択の画面を閉じた後で、その画面を破棄する。元の画面は、破棄していなかった | 使い終わったフォームを残さないため |

### 3.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースにつないでの動作(一覧の読み込み、追加・訂正・削除と保存)。検証は、データベースにつながずに、見本のデータで行った。入力の画面と銀行コード選択の画面は、画面の処理をそのまま動かして、銀行名の表示・絞り込み・選択中の行の表示を確かめた。一覧は、同じ手順を検証用のプログラムから行った |
| 2 | 銀行コード選択の画面で選んだ結果が、入力の画面に入ること。登録のときの確認と、キャンセルのときの確認メッセージ |
| 3 | 実際の銀行コードの表(件数が多い)での、絞り込みの速さ |
| 4 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目 |

一覧の画面は、メニューにはつないでいない(基本情報の画面から開く)。

---

## 4. 銀行コード マスタ(FormMasterBankCode2)

銀行コード マスタの 2 つの画面を、S 系の部品で作り直したもの。メニュー(FormMain2 の `btnMasterBankCode`)が開く `FormMasterBankCode` に当たる。

銀行情報(3 章。`FormMasterBasic2_Bank`)とは、別の画面。銀行情報は、このアプリが使う口座(振込元)の一覧で、基本情報の画面から開く。銀行コード マスタは、全国の銀行・支店のコードの一覧。

### 4.1 ファイル

| ファイル | 元の画面 | 内容 |
| --- | --- | --- |
| `UpdateForm/FormMasterBankCode2`(.cs / .Designer.cs / .resx) | `FormMasterBankCode` | 銀行コードの一覧。検索欄(銀行・支店)、検索クリアのボタン、追加(F2)・訂正(F3、Enter、ダブルクリック)・削除(F4)・閉じる(F12) |
| `UpdateForm/FormMasterBankCode2_DlgEntry`(同上) | `FormMasterBankCode_DlgEntry` | 銀行コード 1 件の入力。登録(F11)・キャンセル(F12) |
| `UpdateForm/AppBankCodeFilter.cs` | (各画面の中の `viewFilter`) | 検索欄の内容から、絞り込みの条件を作る。銀行コード選択(`FormSelectorBankCode2`)と共用 |
| `comset/UI/SButton.cs` | 標準の `Button` | 角丸のボタン(Sコントロール仕様の 12.1) |

処理は、元の画面と同じ構成(同じメソッド名)にしてある。ファンクション キーの定義(`FuncMasterBankCode` など)と、入力ルール(`AppSControlRule`)は、すでにあるものを使う。2 つとも、Gc 系のコントロール・コードは使っていない。

一覧は、開くときに、進行状況の画面(`FormBg_Progress`。Gc 系は使っていない既存の画面)を出して、データを読み込む(元の画面と同じ)。

### 4.2 画面の構成

配置と色は、1.7 の値。

| 画面 | 項目 | コントロール | 内容 |
| --- | --- | --- | --- |
| 一覧 | 銀行検索・支店検索 | `iBank`・`iShiten`(`STextBox`) | 入力するたびに、一覧を絞り込む(条件は 3.2 の銀行コード選択と同じ) |
| 一覧 | 検索クリア | `btnClear`(`SButton`) | 検索欄を空にして、すべての行を表示する。フォーカスは、銀行検索の欄へ移る |
| 一覧 | グリッド(銀行コード・銀行名(正式名称)・支店コード・支店名) | `grid`(`SGridDB`) | 銀行コードは 4 桁、支店コードは 3 桁に、ゼロ埋めで表示する。最初のフォーカスは、グリッド |
| 入力 | 銀行コード・支店コード | `iBankCode`・`iShitenCode`(`STextBox`) | `BCD_Code`・`BCD_CodeShiten` |
| 入力 | 銀行名・銀行名カナ | `iNameBank`・`iNameKanaBank`(`STextBox`) | `BCD_Name`・`BCD_NameFurigana`(`SControlDBRuby`。銀行名を入力すると、読みをカナの欄へ追記する) |
| 入力 | 正式名称 | `iFullNameBank`(`STextBox`) | `BCD_FullName` |
| 入力 | 支店名・支店名カナ | `iNameShiten`・`iNameKanaShiten`(`STextBox`) | `BCD_NameShiten`・`BCD_NameShitenFurigana`(`SControlDBRuby`) |

入力の画面は、7 つの欄すべてが入力必須(プレースホルダー「入力必須」と、項目名の `*`。支店名カナの項目名 `lblNameKanaShiten` は、印の分だけ、幅を 84 から 88 に広げた)。登録のときに、同じ銀行コード・支店コードの行がほかに無いことも確かめる。フォーカスの順は、元の画面と同じ(左の列を上から下へ、次に右の列)。

タイトルは、一覧が「銀行コードマスタ」、入力が「銀行コードマスタ：追加」「銀行コードマスタ：訂正」「銀行コードマスタ：コピー追加」。

### 4.3 元の画面と変えた点・未確認の点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 一覧の、コードの列の幅を、0.10 から 0.12 にした(銀行名の列は 0.45 から 0.42) | フォントが大きくなったので、見出し(銀行ｺｰﾄﾞ・支店ｺｰﾄﾞ)が 2 行に折り返されたため |
| 2 | 絞り込みの条件を作る処理を、`AppBankCodeFilter` に移した | 3.3 の 4 |
| 3 | 検索クリアのボタンを、`SButton` にした | 新しいデザインに合わせるため |
| 4 | 入力必須をプレースホルダーで示す、タイトルを「メイン名：サブ名」にする、閉じるときの書き戻しを `sctl.Update()` にする | 3.3 の 1〜3 と同じ |

未確認の点:

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースにつないでの動作(読み込みと進行状況の画面、追加・訂正・削除と保存)。検証は、見本の銀行コード(40 件)を入れて、画面の処理をそのまま動かして行った(ゼロ埋めの表示、絞り込み、検索クリア、訂正での読み込み) |
| 2 | 実際の件数(数万件)での、絞り込みの速さ |
| 3 | 実機の IME での、銀行名・支店名からカナへの読みの追記 |
| 4 | 登録のときの確認(空欄・コードの重複)、削除の 2 回の確認 |
| 5 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目 |

---

## 5. 診療科マスタ(FormMasterShinryoka2)

診療科マスタの 2 つの画面を、S 系の部品で作り直したもの。メニュー(FormMain2 の `btnMasterShinryoka`)が開く `FormMasterShinryoka` に当たる。

### 5.1 ファイル

| ファイル(`FMKaiin/UpdateForm/`) | 元の画面 | 内容 |
| --- | --- | --- |
| `FormMasterShinryoka2`(.cs / .Designer.cs / .resx) | `FormMasterShinryoka` | 診療科の一覧。検索欄(コード・名称)、検索クリアのボタン、追加(F2)・訂正(F3、Enter、ダブルクリック)・削除(F4)・閉じる(F12) |
| `FormMasterShinryoka2_DlgEntry`(同上) | `FormMasterShinryoka_DlgEntry` | 診療科 1 件の入力。登録(F11)・キャンセル(F12) |

処理は、元の画面と同じ構成(同じメソッド名)にしてある。ファンクション キーの定義(`FuncMasterShinryokamoku`・`FuncMasterShinryokamoku_DlgEntry`)と、入力ルール(`AppSControlRule`。コードは数字 2 桁、名称は 30 文字)は、すでにあるものを使う。2 つとも、Gc 系のコントロール・コードは使っていない。`SGridDB` に足したものは無い(列は、どちらも文字列の列)。

一覧の .resx は、元の画面のものを写した。入力の画面の .resx は、空。

### 5.2 画面の構成

配置と色は、1.7 の値。

| 画面 | 項目 | コントロール | 内容 |
| --- | --- | --- | --- |
| 一覧 | コード | `iCodeShinryoka`(`STextBox`) | 入力するたびに、一覧を絞り込む。コードがその数字で始まるもの(前方一致)。全角の数字でもよい。数字以外が入っているときは、コードでは絞り込まない。入力ルールは設定していない(元の画面と同じ) |
| 一覧 | 名称 | `iNameShinryoka`(`STextBox`) | 入力するたびに、一覧を絞り込む。名称にその文字を含むもの(部分一致。ひらがな・カタカナの違いは区別しない)。コードと両方を入力したときは、両方に当たるもの |
| 一覧 | 検索クリア | `btnClear`(`SButton`) | 検索欄を空にして、すべての行を表示する。フォーカスは動かさない(元の画面と同じ) |
| 一覧 | グリッド(コード・名称) | `grid`(`SGridDB`) | コードの列は幅 75 で右寄せ、名称の列は残りの幅。並びはコードの順。最初のフォーカスは、グリッド |
| 入力 | 診療科コード | `iCode`(`STextBox`。入力必須) | `SRK_Code` |
| 入力 | 診療科名称 | `iName`(`STextBox`。入力必須) | `SRK_Name` |

タイトルは、一覧が「診療科マスタ」、入力が「診療科マスタ：追加」「診療科マスタ：訂正」。

登録のときに、2 つの欄が空でないことと、同じコードの行がほかに無いことを確かめる。削除のときは、ほかの表で使われている診療科なら、削除しない(どちらも元の画面と同じ)。

### 5.3 元の画面と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 入力の画面の、名称の入力欄の名前を `iName` にした。元の画面は `iShisetsuName`(施設名) | 中身と合っていない名前だったため |
| 2 | 一覧の、使っていない処理(コメントにしてあった、会費区分の絞り込みと「検索時表示」の列)は、写していない | 使われていないため |
| 3 | 検索クリアのボタンを、`SButton` にした | 4.3 の 3 と同じ |
| 4 | 入力必須をプレースホルダーで示す、タイトルを「メイン名：サブ名」にする、閉じるときの書き戻しを `sctl.Update()` にする | 3.3 の 1〜3 と同じ |

### 5.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースにつないでの動作(一覧の読み込み、追加・訂正・削除と保存、追加した行への移動、ほかの表で使われている診療科の削除の確認)。検証は、データベースにつながずに、見本の診療科(24 件)で行った。一覧は、読み込みの後の手順を検証用のプログラムから行い、絞り込みと検索クリアは画面の処理をそのまま動かした。入力の画面は、画面の処理をそのまま動かして、読み込み・入力ルール(数字 2 桁)・空欄と重複の確認・行への書き戻しを確かめた |
| 2 | キャンセルのときの確認メッセージ。一覧での Enter・ダブルクリックでの訂正 |
| 3 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目 |
| 4 | 名称の検索欄に半角の `'` を入力すると、絞り込みの条件が正しく作られず、一覧は直前の絞り込みのままになる(例外にはならない)。条件を作っているのは comset の `DBQuery.CreateLikeKanaString` で、元の画面も同じ呼び出しをしている。元の画面でも同じ動きになるかは、確かめていない |

一覧の画面は、メニュー(FormMain2 の `btnMasterShinryoka`)から、これまでの画面と並べて開く(「画面の一覧」の注)。

---

## 6. 医療機関マスタ(FormMasterIryoKikan2)

医療機関マスタの 3 つの画面(一覧・入力・印刷プレビュー)を、S 系の部品で作り直したもの。メニュー(FormMain2 の `btnMasterIryoKikan`)が開く `FormMasterIryoKikan` に当たる。入力の画面は、グリッドのセルの中で入力する機能(Sコントロール仕様の 10.7)を使う、最初の画面。

印刷プレビューの画面は `FormMasterIryoKikan2_Out`(6.6)。一覧の印刷プレビュー(F9)から開く。旧画面 `FormMasterIryoKikan` からは、これまでどおり `FormOutIryoKikan` を開く。

### 6.1 ファイル

| ファイル | 元の画面 | 内容 |
| --- | --- | --- |
| `UpdateForm/FormMasterIryoKikan2`(.cs / .Designer.cs / .resx) | `FormMasterIryoKikan` | 医療機関の一覧。検索欄(コード・名称・組コード・退会区分)、検索クリアのボタン、追加(F2)・訂正(F3、Enter、ダブルクリック)・削除(F4)・印刷プレビュー(F9)・閉じる(F12) |
| `UpdateForm/FormMasterIryoKikan2_DlgEntry`(同上) | `FormMasterIryoKikan_DlgEntry` | 医療機関 1 件の入力と、その標榜科目(診療科)の入力。登録(F11)・キャンセル(F12) |
| `UpdateForm/FormMasterIryoKikan2_Out`(同上) | `FormOutIryoKikan` | 印刷プレビュー(出力する帳票の選択)。印刷プレビュー(F9)・閉じる(F12)。6.6 |
| `UpdateForm/AppSTableCombo.cs` の `SetComboBox_KumiCode`・`SetComboBox_Kaisetsushutai`・`SetComboBox_Shinryoka` | `AppTableCombo` の同じ名前のメソッド | 組コード・開設主体・診療科の一覧の内容(どれも、コード・名称の 2 列、コードの順、選択後は名称を表示、値は ID)。引数は、元と同じく、コントロールと、表のビュー |
| `comset/UI/SGridDBInput.cs` | (comset の `GGridDBCommon`) | グリッドのセルの中で入力する機能(Sコントロール仕様の 10.7) |

処理は、元の画面と同じ構成(同じメソッド名)にしてある。ファンクション キーの定義(`FuncMasterIryoKikan`・`FuncMasterIryoKikan_DlgEntry`・`FuncOutIryoKikan`)と、入力ルール(`AppSControlRule`)は、すでにあるものを使う。3 つとも、Gc 系のコントロール・コードは使っていない。

一覧と入力の画面の .resx は、元の画面のものを写した(グリッドの初期状態を持っている)。印刷プレビューの画面の .resx は、空。入力の画面のものからは、元の画面にだけあった部品(`gcShortcut1`)の情報を取り除いた。

### 6.2 一覧の構成

配置と色は、1.7 の値。検索欄は 4 つあり、項目と項目の間を 32 あけて横に並べている(1.7)。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| コード | `iCodeIryo`(`STextBox`) | 入力するたびに、一覧を絞り込む。コードがその数字で始まるもの(前方一致)。全角の数字でもよい。数字以外が入っているときは、コードでは絞り込まない。入力ルールは設定していない(元の画面と同じ) |
| 名称 | `iNameIryo`(`STextBox`) | 入力するたびに、一覧を絞り込む。施設正式名・施設カナ・通称のどれかに、その文字を含むもの(ひらがな・カタカナ、全角・半角のカナの違いは区別しない) |
| 組コード | `iKumiCode`(`STableComboBox`) | 選んだ組の医療機関だけにする。文字を入力し始めると、選択が外れて、組では絞り込まなくなる。確定(Enter、行のクリック、フォーカスの移動)すると、その組で絞り込む |
| 退会区分 | `iKbnTaikai`(`SComboBox`。空欄・会員・退会) | 会員か退会を選ぶと、その行だけにする。空欄と、開いた直後(未選択)は、絞り込まない |
| 検索クリア | `btnClear`(`SButton`) | 4 つの検索欄を空にして(退会区分は空欄の項目、組コードは未選択)、すべての行を表示する。フォーカスは動かさない(元の画面と同じ) |
| グリッド | `grid`(`SGridDB`) | コード(幅 0.10、右寄せ)・施設正式名(0.5)・組コード(0.15。ID を名称にして表示)・開設主体(0.15。ID を名称にして表示)・退会区分(残り、中央寄せ。会員 / 退会)。並びはコードの順。最初のフォーカスは、グリッド |

4 つの検索欄は、すべてを満たす行だけを表示する(AND)。

フォームの幅は 1016(元の画面は 925)。フォントが大きくなった分、検索欄を広げたため。検索欄の間隔は、1.7 の決まり。余った幅は、名称の入力欄(幅 222)に回している。

### 6.3 入力の画面の構成

左の列に医療機関の項目、右の列に標榜科目のグリッドを置いている(配置は 1.7)。フォームの大きさは 890 × 670(元の画面は 905 × 714)。

| 項目 | コントロール | 行の項目 |
| --- | --- | --- |
| コード | `iCode`(`STextBox`。入力必須。数字 5 桁) | `IRK_Code`。追加のときは、空いている番号が入っている |
| 施設正式名・施設カナ | `iShisetsuName`・`iShisetsuKana`(`STextBox`) | `IRK_Name`・`IRK_Kana`(`SControlDBRuby`。名称を入力すると、読みをカナの欄へ追記する) |
| 施設漢字通称・施設通称カナ | `iShisetsuTsusho`・`iShisetsuTsushoKana`(`STextBox`) | `IRK_Tsusho`・`IRK_TsushoKana`(`SControlDBRuby`) |
| 郵便番号・住所 | `iPost1`・`iPost2`・`iAddr1`(`STextBox`) | `IRK_Post`・`IRK_Addr1`(`SControlDBPostAddr`) |
| 住所(2 行目) | `iAddr2`(`STextBox`) | `IRK_Addr2` |
| 電話番号・FAX | `iTel1_1`〜`iTel1_3`・`iTel2_1`〜`iTel2_3`(`STextBox`) | `IRK_Tel1`・`IRK_Fax1`(`SControlDBHyphenSplit`) |
| 開設主体 | `iKaisetsushutai`(`STableComboBox`) | `IRK_KaisetsuShutai` |
| 病床有無 | `iByoshoUmu`(`SComboBox`。有 / 無。入力必須) | `IRK_ByoshoUmu`。「無」にすると、許可病床数を空にして(行の値も空にする)、入力できなくする |
| 許可病床数 | `iKyokabyosho`(`STextBox`。数字 4 桁) | `IRK_Kyoka` |
| 併設施設 | `chkKaigo`(介護施設)・`chkEtc`(その他)(`SCheckBox`)、`iHeisetsu`(`STextBox`) | `IRK_Kaigo`・`IRK_Etc`・`IRK_Memo`。「その他」がオフの間は、`iHeisetsu` に入力できない。オフのまま登録すると、`IRK_Memo` を空にする |
| 組コード | `iKumiCode`(`STableComboBox`) | `IRK_KumiCode` |
| 退会区分 | `iTaikaiKbn`(`SComboBox`。会員 / 退会。入力必須) | `IRK_TaikaiKbn` |
| 最終更新 | `lblLastUpdate`・`oLastUpdate`・`oLastUpdateUser`(`SLabel`) | `LastUpdate`・`LastUpdateUser`(担当者の氏名にして表示)。訂正のときだけ表示する。見出しの右に置いている |

標榜科目(`t_iryokikan_shinryoka` のうち、この医療機関の行):

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| グリッド | `grid_Sinryo`(`SGridDB`) | 名称(幅 220。診療科の ID を名称にして表示)・診療科目(残り、中央寄せ。オンなら「○」) |
| 名称の入力 | `iHyoboKamoku`(`STableComboBox`。枠線なし) | セルに重ねて、診療科を選ぶ。`ID_Shinryoka` |
| 診療科目の入力 | `iShinryoKamokuChk`(`SCheckBox`。文字なし) | セルに重ねて、オン・オフを切り替える。`Chk_Shinryoka`。切り替えた時点で、行に書かれる |
| 追加・削除 | `btnKamokuAdd`・`btnKamokuDel`(`SButton`) | 追加: 行を足して、その行の名称のセルで入力を始める。削除: 確認のうえ、指している行を消す |

標榜科目の操作(Sコントロール仕様の 10.7.2 の動きを、この画面に当てはめたもの):

- グリッドで Enter、文字の入力、セルのダブルクリックで、指しているセルの入力を始める。
- 名称のセルでは、文字を入力すると診療科の一覧を絞り込む。一覧を開いている間の Enter は、指している診療科の確定に使う。
- Enter(または Tab)で、名称 → 診療科目 → 次の行の名称、の順に移る。最後の行の診療科目からは、グリッドへ戻る。Shift を押していれば、逆へ移る。
- 入力した内容は、セルを離れた時点で行へ書かれて、グリッドの表示に反映される。

標榜科目の行は、一覧の画面から渡されたビュー(`DvIryokikanShinryo`)に書く。データベースへの書き込みと、登録しなかったときの取り消し(`RejectChanges`)は、一覧の画面が行う(元の画面と同じ)。

入力必須は、コード・病床有無・退会区分(登録のときに、空なら登録しない)。コードにはプレースホルダー「入力必須」、病床有無と退会区分には「選択必須」を設定してあり、3 つの項目名は `Required = true`(1.7)。病床有無と退会区分のコンボは、「選択必須」の 4 文字が収まるよう、幅を 80 から 100 に広げた(右隣に何も無い行なので、ほかの配置は変えていない)。病床有無と退会区分は、追加のときも初期値(有・会員)が入るので、プレースホルダーが見えるのは、選択を外したときだけ(画面では確かめていない。部品のテストでは確かめた)。登録のときに、同じコードの医療機関がほかに無いことも確かめる。

タイトルは、一覧が「医療機関マスタ」、入力が「医療機関マスタ：追加」「医療機関マスタ：訂正」。

フォーカスの順は、元の画面と同じ(左の列を上から下へ、最後に標榜科目のグリッド。追加・削除のボタンは、Tab の移動先にしない)。

### 6.4 元の画面と変えた点

一覧:

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | フォーカスの順を、グリッド → コード → 名称 → 組コード → 退会区分 → 検索クリア(画面の左から右)にした。元の画面は、グリッド → 退会区分 → コード → 検索クリア → 組コード → 名称 | 元の画面の順は、項目を後から足した順のままになっていて、並びと合っていなかったため |
| 2 | 組コードを変えたときの処理は、絞り込みのやり直しだけ。元の画面は、その前に、選ばれた値をコンボに設定し直していた(`iKumiCode.ComboBox.ExSetSelectedIndexByValue(iKumiCode.Value)`) | `UcTableComboBox` の中のコンボに、選択を伝え直すための処理。`STableComboBox` は、コントロール自身が選択を持っているので要らない |
| 3 | 組コードの欄に文字を入力している間は、組では絞り込まない(選択が外れるため)。元の画面は、該当が 1 件だけになった時点で、その組で絞り込んでいた | `STableComboBox` の動き(Sコントロール仕様の 4.3 の 3) |
| 4 | 検索クリアのボタンを、`SButton` にした | 4.3 の 3 と同じ |
| 5 | 使っていないコード(コメントにしてあった行)は、写していない | 使われていないため |

入力:

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 標榜科目の行を足すときに、診療科目をオフにしておく(`Chk_Shinryoka = false`)。元の画面は、値を入れていなかった | S 系のチェック ボックスの受け渡しは、切り替えたときにだけ行へ書く(Sコントロール仕様の 7.2.1)。入れておかないと、切り替えなかった行の値が、値なしのまま残るため(画面ではオフに見える) |
| 2 | 標榜科目の名称のセルで、診療科の一覧を開いている間の Enter は、診療科の確定だけを行う(もう一度 Enter で、次のセルへ移る)。元の画面は、一覧を開いているかどうかを見ずに、次のセルへ移す処理になっている(コードを読んだ結果。実際の動きは確かめていない) | ほかの S 系の画面での、テーブル コンボの Enter と同じ動きにするため(1.3) |
| 3 | 標榜科目のセルでの Tab と、チェック ボックスでの矢印キーの動き | Sコントロール仕様の 10.7.3 の 2・3 |
| 4 | チェック ボックスを、`SCheckBox` にした(Sコントロール仕様の 13 章)。元の画面は `YcCheckBoxEx` | S 系の部品にそろえるため。当初は標準の `CheckBox` を置いていたが、四角の大きさなどを調整できるよう、`SCheckBox` を作って置き換えた(2026-11-26) |
| 5 | 項目名を「住所」の 1 つにした(2 行目の入力欄には、項目名を付けない)。元の画面は「住所１」「住所２」 | FormMasterBasic2 と同じ形にするため |
| 6 | 入力必須は、コード・病床有無・退会区分の 3 つを、プレースホルダー(「入力必須」「選択必須」)と、項目名の `*` で示す。元の画面は、この 3 つの項目名を、色で示していた | 6.3 のとおり。当初は、コンボにプレースホルダーが無かったので、コードだけに付けていた。2026-11-26 に、コンボにもプレースホルダーを足して、そろえた |
| 7 | タイトルを「メイン名：サブ名」にした。保存の前の書き戻しを、`sctl.Update()` と `sctl_shinryo.Update()` にした | 3.3 の 2・3 と同じ |
| 8 | 元の画面にあった、使っていない部品(`gcShortcut1`)と、コメントにしてあった行は、写していない | 使われていないため |

元の画面と同じにしてある点: キャンセル(F12)と × では、確認のメッセージを出さずに閉じる(ほかの入力の画面は、変更があれば確認する)。意図したものかは、未確認。

### 6.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースにつないでの動作(一覧の読み込み、追加・訂正・削除と保存、追加した行への移動、標榜科目の保存と取り消し、印刷プレビュー)。検証は、データベースにつながずに、見本のデータで行った。一覧は、読み込みの後の手順を検証用のプログラムから行い、列の文字列・4 つの検索欄での絞り込み・検索クリアは、画面の処理をそのまま動かした。入力の画面は、アプリのデータベース管理クラスに見本の表を入れて、画面の処理をそのまま動かした(表示、標榜科目のセルでの入力・行の追加、病床有無と併設施設の連動、登録の前の確認) |
| 2 | 一覧から入力の画面を開いて、登録・キャンセルで戻ったときの動作。開く側の処理は元の画面と同じだが、実際には開いていない(データベースを読むため) |
| 3 | 標榜科目の削除(確認のメッセージが出るので、自動では確かめられなかった) |
| 4 | 実機の IME での入力(名称からカナへの読みの追記、組コード・開設主体・診療科の欄への入力)。郵便番号からの住所の自動入力は、郵便番号の DB の写しを使って確かめた(Sコントロール仕様の 7.2.6) |
| 5 | グリッドのセルの中で入力する機能の、未確認の点(Sコントロール仕様の 10.7.4) |
| 6 | 一覧での Enter・ダブルクリックでの訂正 |
| 7 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(幅 1016 の一覧が、画面に収まるか) |
| 8 | 名称の検索欄に半角の `'` を入力したときの動き(5.4 の 4 と同じ。条件を作る処理が同じため) |

一覧の画面は、メニュー(FormMain2 の `btnMasterIryoKikan`)から、これまでの画面と並べて開く(「画面の一覧」の注)。

### 6.6 印刷プレビューの画面(FormMasterIryoKikan2_Out)

出力する帳票を選ぶ画面。一覧の印刷プレビュー(F9)から、一覧のビュー(絞り込んだ状態のまま)を受け取って開く。処理は、元の画面(`FormOutIryoKikan`)と同じ構成(同じメソッド名)。帳票(`ReportFlexIryoKikan`・`ReportFlexIryoKikanJoho`)は、元の画面と同じものを使う。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 印刷範囲 | `rdoList`(医療機関一覧表)・`rdoJoho`(医療機関情報)(`SRadioButton`。グループのパネル `secHani` の中に、縦に並べる。1.7) | 開いたときは「医療機関一覧表」を選んで、フォーカスもそこに置く。↑ ↓ で切り替える |
| 印刷プレビュー(F9) | — | 医療機関一覧表: 一覧に表示している医療機関を出力する。医療機関情報: 一覧で指している 1 件だけを出力する |

見出しは「印刷条件」、タイトルは「医療機関マスタ：印刷プレビュー」。フォームの大きさは 400 × 248(グループのパネルを置いたときに、380 × 243 から広げた)。

元の画面と変えた点:

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 見出し・項目名・パネル・背景を、1.7 の見た目にした。ラジオ ボタンを、`SRadioButton` にした | 1.7 |
| 2 | タイトルを「医療機関出力」から「医療機関マスタ：印刷プレビュー」にした | 1.7 のタイトルの決まり |
| 3 | フォームの大きさを 346 × 229 から 400 × 248 にした | 1.7 の余白と、ラジオ ボタンのグループのパネルに合わせたため |
| 4 | 最大化・最小化のボタンをなくした。元の画面は、最大化のボタンが表示されていた | ほかの S 系の画面と同じにした(大きさを変えられない画面なので、最大化しても意味が無い) |

未確認の点:

| # | 内容 |
| ---: | --- |
| 1 | 帳票の実行(F9)。データベースとプリンターが要るので、実行していない。検証は、空のビューを渡して画面を表示し、最初の選択とフォーカス、↑ ↓ での切り替えを確かめた |
| 2 | 一覧が 0 件のとき(絞り込みで 1 件も無いときなど)に「医療機関情報」を出力すると、指している行が無いので、例外になるおそれがある(コードを読んでの推測。元の画面と同じ処理なので、直していない) |
| 3 | 実際の一覧の画面から開いたときの動き。Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目 |

---

## 7. 医会会費マスタ(FormMasterKaihi2)

医会会費マスタの 2 つの画面を、S 系の部品で作り直したもの。メニュー(FormMain2 の `btnMasterIkaiKaihi`)が開く `FormMasterKaihi` に当たる。入力の画面は、数値の入力欄 `SNumberBox`(Sコントロール仕様の 5 章)を画面で使う、最初の例。

### 7.1 ファイル

| ファイル | 元の画面 | 内容 |
| --- | --- | --- |
| `UpdateForm/FormMasterKaihi2`(.cs / .Designer.cs / .resx) | `FormMasterKaihi` | 会費の一覧。検索欄(区分・コード・名称・検索表示)、検索クリアのボタン、追加(F2)・訂正(F3、Enter、ダブルクリック)・削除(F4)・コピー追加(F5)・閉じる(F12) |
| `UpdateForm/FormMasterKaihi2_DlgEntry`(同上) | `FormMasterKaihi_DlgEntry` | 会費 1 件の入力(会費の項目、月額費用の 12 か月分、銀行情報)。登録(F11)・キャンセル(F12) |
| `UpdateForm/AppSTableCombo.cs` の `SetComboBox_IfaxGroup` | `AppTableCombo.SetComboBox_IfaxGroup` | iFAX グループの一覧の内容(コード・名称の 2 列、コードの順、選択後は名称を表示、値は ID) |

処理は、元の画面と同じ構成(同じメソッド名)にしてある。ファンクション キーの定義(`FuncMasterKaihi`・`FuncMasterKaihi_DlgEntry`)と、入力ルール(`AppSControlRule`)は、すでにあるものを使う。2 つとも、Gc 系のコントロール・コードは使っていない。銀行コードの選択は、S 系の画面(`FormSelectorBankCode2`。3 章)を開く。

一覧の .resx は、元の画面のものを写した。入力の画面の .resx は、空。

一覧の銀行名・支店名の列と、入力の画面の銀行名・支店名の欄は、銀行コードの表(`AppGlobal.BankCodeMg`)から引く。メニューは、元の画面と同じく、銀行コードを読み込んでから開くこと(`needsBankCode: true`)。

### 7.2 一覧の構成

配置と色は、1.7 の値。検索欄は 4 つを横に並べている(項目と項目の間は 32。1.7)。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 区分 | `iKbnKaihi`(`SComboBox`。会費区分。先頭は空欄) | 選んだ区分の会費だけにする。空欄と、開いた直後(未選択)は、絞り込まない |
| コード | `iCodeKaihi`(`STextBox`。数字 5 桁) | 入力するたびに、一覧を絞り込む。コードがその数字で始まるもの(前方一致) |
| 名称 | `iNameKaihi`(`STextBox`) | 入力するたびに、一覧を絞り込む。名称にその文字を含むもの(ひらがな・カタカナの違いは区別しない) |
| 検索表示 | `iKbnUsed`(`SComboBox`。空欄・表示・非表示) | 表示か非表示を選ぶと、その行だけにする |
| 検索クリア | `btnClear`(`SButton`) | 4 つの検索欄を空にして、すべての行を表示する。フォーカスは動かさない(元の画面と同じ) |
| グリッド | `grid`(`SGridDB`) | 区分(幅 100。値を名称にして表示)・コード(70、右寄せ)・名称(200)・検索表示(70、中央寄せ。表示 / 非表示)・銀行名(130)・支店名(130。どちらも、銀行コード・支店コードから引いた名称)・備考(残り)。並びはコードの順。最初のフォーカスは、グリッド |

4 つの検索欄は、すべてを満たす行だけを表示する(AND)。フォーカスの順は、元の画面と同じ(検索欄を左から右へ、検索クリア、グリッド)。

フォームの幅は 986(元の画面は 925)。フォントが大きくなった分、検索欄を広げたため。検索欄の間隔は、1.7 の決まり。余った幅は、名称の入力欄(幅 230)に回している。

### 7.3 入力の画面の構成

左の列に会費の項目、右の列に月額費用(上)と銀行情報(下)を置いている(元の画面と同じ並び)。フォームの大きさは 760 × 654(元の画面は 907 × 605)。

| 項目 | コントロール | 行の項目 |
| --- | --- | --- |
| 会費区分 | `iKbnKaihi`(`SComboBox`。入力必須) | `Kaihi_KaihiType` |
| 会費コード | `iCode`(`STextBox`。入力必須。数字 5 桁) | `CD_Kaihi` |
| 会費印刷用名称・会費略称 | `iName`・`iShortName`(`STextBox`。どちらも入力必須) | `Kaihi_Name`・`Kaihi_ShortName`(`SControlDBRuby`。名称を入力すると、読みを略称の欄へ追記する。元の画面と同じ) |
| 備考 | `iBiko`(`STextBox`) | `Kaihi_Biko` |
| 会費群 | `iKaihiGunCode`(`STextBox`。数字 4 桁) | `Kaihi_GunCode`(`SControlDBNumber`) |
| 検索時表示区分 | `iSearchUsed`(`SComboBox`。表示する / 表示しない) | `Kaihi_SearchUsed` |
| 会費属性 | `iKaihiZokusei`(`SComboBox`。入力必須) | `Kaihi_Zokusei` |
| 月額費用(1 月〜12 月) | `iCost1`〜`iCost12`(`SNumberBox`。7 桁、桁区切りあり) | `Kaihi_GetsugakuCost1`〜`12`(`SControlDBNumber`)。入力するたびに、行へ書いて、合計を直す |
| 月額費用の合計 | `oCostAll`(`SNumberBox`。無効。幅 130) | 12 か月分の合計を表示するだけ。行には書かない。幅は、最大の合計(9,999,999 × 12 = 119,999,988。9 桁)が収まる大きさ。右端を 12 月の欄にそろえているので、項目名「合計」は、7〜12 月の項目名より 30 左にある |
| 銀行コード・支店コード | `iBankCD`・`iShitenCD`(`STextBox`。数字 4 桁・3 桁) | `Kaihi_BankCode`・`Kaihi_BankCodeShiten`。入力するたびに、銀行名・支店名を引き直す。欄をダブルクリックすると、銀行コードの選択の画面を開く |
| 銀行名・支店名 | `iBankName`・`iShitenName`(`STextBox`。無効) | 銀行コードの表から引いて、表示するだけ(支店名は、後ろに「支店」を付ける) |
| 口座区分・口座番号 | `iKozaType`(`SComboBox`)・`iKozaNo`(`STextBox`。数字 7 桁) | `Kaihi_BankKozaType`・`Kaihi_BankKozaNo` |
| 口座名義・カナ | `iKozaName`・`iKozaNameKana`(`STextBox`) | `Kaihi_BankKozaName`・`Kaihi_BankKozaNameKana`(`SControlDBRuby`) |
| iFaxグループ | `iBankIfax`(`STableComboBox`) | `Kaihi_BankIfax` |

登録のときに、会費区分・会費コード・会費印刷用名称・会費略称・会費属性が空でないことと、同じ会費コードの行がほかに無いことを確かめる(元の画面と同じ)。この 5 つの項目名は `Required = true`。キャンセル(F12)と × では、画面を表示してから内容を変えていたときだけ、確認のメッセージを出す(1.6 の 18。元の画面は、追加・コピー追加のときは、変えていなくても出していた)。

タイトルは、一覧が「医会会費マスタ」、入力が「医会会費マスタ：追加」「医会会費マスタ：訂正」「医会会費マスタ：コピー追加」。

フォーカスの順は、元の画面と同じ(左の列を上から下へ → 月額費用の 1〜6 月、7〜12 月 → 銀行情報)。

### 7.4 元の画面と変えた点

一覧:

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 区分の列の幅を、80 から 100 にした | フォントが大きくなったので、いちばん長い名称(グループ会費)が、80 では収まらなかったため |
| 2 | 検索クリアのボタンを、`SButton` にした。使っていないコード(コメントにしてあった行)は、写していない | 4.3 の 3、6.4 の 5 と同じ |

入力:

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 「月額費用」「銀行情報」の枠(`GroupBox`)をやめて、太字の見出しにした | 1.7(項目のまとまり) |
| 2 | 入力必須の 5 つを、プレースホルダー(会費コード・会費印刷用名称・会費略称は「入力必須」、会費区分と会費属性のコンボは「選択必須」)と、項目名の `*` で示す。元の画面は、5 つの項目名を、色で示していた | 1.7。当初は、コンボにプレースホルダーが無かったので、コンボには付けていなかった。2026-11-26 に、そろえた |
| 3 | 項目名「合  計」を「合計」にした | 1.7(幅をそろえる空白は入れない) |
| 4 | 月額費用が 1 つも入っていないとき、合計は「0」と表示する。元の画面(GcNumberEx)では、空欄で表示される設定だった可能性がある(Sコントロール仕様の 7.5 の 4。実際の表示は確かめていない) | `SNumberBox` は、0 を「0」と表示するため(Sコントロール仕様の 5.1) |
| 5 | 銀行コードの選択の画面を、閉じた後で破棄する。元の画面は、破棄していなかった | 3.3 の 5 と同じ |
| 6 | タイトルを「メイン名：サブ名」にした。閉じるとき・登録の前の書き戻しを、`sctl.Update()` にした | 3.3 の 2・3 と同じ |
| 7 | 口座区分のコンボの幅を詰めて、口座番号の欄を広げた(同じ行に並べるのは、元の画面と同じ) | フォントが大きくなったので、7 桁の口座番号が、元の比率の幅では収まらなかったため |
| 8 | 数字の欄(会費コード・銀行コード・支店コード・口座番号)の幅は、桁数が収まる幅にしてある(1.7 の「数字を入れる欄の幅」)。当初は、銀行コード(4 桁)の欄が 56 で、先頭の数字が欠けていた(2026-11-26 に修正)。合計の欄は、当初は幅 100 で、合計が 8 桁以上(1,000 万以上)になると、先頭が欠けた。最大の合計(9 桁)が収まる 130 に広げた(2026-11-26) | 1.7 |

### 7.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースにつないでの動作(一覧の読み込み、追加・訂正・コピー追加・削除と保存、追加した行への移動)。検証は、データベースにつながずに、見本の会費(6 件)・銀行コード・iFAX グループで行った。一覧は、読み込みの後の手順を検証用のプログラムから行い、列の文字列・4 つの検索欄での絞り込み・検索クリアは、画面の処理をそのまま動かした。入力の画面は、アプリのデータベース管理クラスに見本の表を入れて、画面の処理をそのまま動かした(訂正・追加・コピー追加の表示、月額費用の入力と合計、銀行コード・支店コードからの名称の表示、登録の前の確認) |
| 2 | 一覧から入力の画面を開いて、登録・キャンセルで戻ったときの動作(一覧は、データベースを読むので、実際には開いていない) |
| 3 | 銀行コード・支店コードの欄のダブルクリックで、銀行コードの選択の画面を開いて、選んだ結果が入ること |
| 4 | 実機の IME での入力(名称から略称へ、口座名義からカナへの、読みの追記) |
| 5 | 月額費用の欄での、実際のキーボードの操作(小数点のキーでの 1000 倍、マイナス キーなど。Sコントロール仕様の 5.2 の動きを、この画面では確かめていない) |
| 6 | キャンセルのときの確認メッセージ、削除の確認、一覧での Enter・ダブルクリックでの訂正 |
| 7 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目 |
| 8 | 名称の検索欄に半角の `'` を入力したときの動き(5.4 の 4 と同じ) |

一覧の画面は、メニュー(FormMain2 の `btnMasterIkaiKaihi`)から、これまでの画面と並べて開く(「画面の一覧」の注。銀行コードを読み込んでから開く指定 `needsBankCode: true` も付いている)。

---

## 8. 進行状況の画面(FormBg_Progress)

時間の掛かる処理の間に表示する、「しばらくお待ちください」の画面。処理を別のスレッドで実行して、終わると自分で閉じる。銀行コードの読み込み(FormMain2・銀行コード マスタ)、取り込み、帳票の出力など、20 か所あまりで使っている。

名前(`FormBg_Progress`)・置き場所(`FMKaiin/` の直下)・使い方は変えずに、見た目だけを S 系の画面に合わせて作り替えた(2026-11-26)。呼び出している側は、どこも変えていない。Gc 系・Yc 系は、もともと使っていない。

### 8.1 ファイル

| ファイル | 内容 |
| --- | --- |
| `FMKaiin/FormBg_Progress.cs` | 処理。文字コードは Shift-JIS、改行は LF(元のまま。書き換えるときは、文字コードを保つこと) |
| `FMKaiin/FormBg_Progress.Designer.cs` | 画面の構成。BOM 付き UTF-8、改行は LF(元のまま) |
| `FMKaiin/FormBg_Progress.resx` | 回転アイコンの画像(13 枚)を持っていたが、使わなくなったので、取り除いた |
| `comset/UI/SProgressBar.cs` | 進み具合のバー(Sコントロール仕様の 14.1) |
| `comset/UI/SBusyIndicator.cs` | 回る輪(Sコントロール仕様の 14.2) |
| `Test/ProgressHarness.cs` | `SProgressBar`・`SBusyIndicator` のテスト(Sコントロール仕様の 8.4) |

### 8.2 画面の構成と、使い方

大きさは 380 × 102(96 DPI 基準。`AutoScaleMode` は Dpi なので、表示倍率に合わせて拡大される)。元の画面は 324 × 79。

| 領域 | コントロール | 内容 |
| --- | --- | --- |
| 背景 | — | `Resources.BackgroundWave` を全面に表示(S 系のマスターの画面と同じ) |
| パネル | `secBody`(`SectionPanel`。色は 1.7 と同じ) | フォームの中に、上下左右 10 あけて置く |
| 回る輪 | `IconIndicator`(`SBusyIndicator`。36 × 36) | 処理中であることを示す。`IconVisible` を false にすると、消える |
| メッセージ | `LblInfo`(`SLabel`。BIZ UDPゴシック 10.5 pt) | `LabelText` の文字列 |
| 進み具合 | `ProgressBar`(`SProgressBar`。高さ 8) | `SetProgress`・`AdvanceProgress`・`FinishProgress` で進める。`ProgressBarVisible` を false にすると消えて、メッセージを上下の中央に表示する |
| タイトル | (ウィンドウの上端の文字) | `TitleText` の文字列。閉じるボタン(×)は、押せない(元の画面と同じ)。タイトル バーの色は、ほかの画面と同じ(1.7) |

使い方(変えていない):

```csharp
FormBg_Progress prog = new FormBg_Progress();
prog.DoWorkEvent += prog_DoWorkEvent;      // 別のスレッドで実行する処理
prog.TitleText = "しばらくお待ちください。";
prog.LabelText = "データを取得しています。";
prog.ShowDialog();                          // 処理が終わると、自分で閉じる
prog.Dispose();
```

元の画面と変えた点:

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 背景を、波の画像と半透明のパネルにした。フォントと文字の色を、S 系に合わせた。大きさを、324 × 79 から 380 × 102 にした | ほかの S 系の画面と、見た目をそろえるため |
| 2 | 進み具合のバーを、`SProgressBar` にした。元は、標準の `ProgressBar` | 1.7(標準のコントロールを直接は置かない) |
| 3 | 回転アイコンを、線で描く `SBusyIndicator` にした。元は、12 枚の画像を 80 ミリ秒ごとに切り替えていた | 画像は、拡大すると、にじむ。色も、S 系の色に合わせられないため |
| 4 | 進み具合を表示するときの、「値をいったん 1 つ大きくしてから戻す」手順をなくした | 標準の `ProgressBar` が、ゆっくり伸びる動きをするのを避けるための手順だった。`SProgressBar` は、すぐにその位置まで塗る |
| 5 | 公開している部品の型が変わった(`LblInfo` は `SLabel`、`ProgressBar` は `SProgressBar`、`IconIndicator` は `SBusyIndicator`)。背景用の部品 `Picture` は、なくした | 画面の作り替えに伴うもの。フォームの外から、これらを直接使っている箇所は無い(確認済み) |
| 6 | `AutoScaleMode` を、Font から Dpi にした | 座標を 96 DPI 基準で書いて、表示倍率に合わせて拡大するため(自前で描く部品の拡大率と、一致させる) |

変えていない点: 多くの呼び出し側は、処理の最初に `AdvanceProgress(100)` を呼んでいる。このため、バーは、開いた直後から最後まで、いっぱいのまま表示される(元の画面でも同じ)。また、別のスレッドの処理で例外が起きても、この画面は、それを記録も通知もせずに閉じる(`bgWorker_RunWorkerCompleted` は `e.Error` を見ていない。元の画面と同じ)。呼び出し側は、結果が入っていないまま先へ進む。2 つの画面を並べて開いたときに落ちた件(19.1)は、この動きが関わっている。

未確認の点:

| # | 内容 |
| ---: | --- |
| 1 | 実際の処理(銀行コードの読み込み、取り込み、帳票の出力など)の中での表示。検証は、検証用の処理(待つだけの処理)を別のスレッドで動かして、バーあり・バーなし・輪なしの 3 通りを表示して行った |
| 2 | `NoBgWorker`(別のスレッドを使わない設定)での表示。いまは、どこも使っていない(`AppDb.cs` に、コメントにした行があるだけ) |
| 3 | 処理が長いとき(数十秒以上)の、輪の動きのなめらかさ。表示倍率 100%・150% での見た目 |
| 4 | Visual Studio のデザイナーで開いたときの表示 |

---

## 9. 会費納付書の Excel 出力(FormOutExcelNofusho2)

会費納付書を Excel に出力する画面(`FormOutExcelNofusho`)を、S 系の部品で作り直したもの(2026-11-26)。メニュー(FormMain2 の `btnXlsNofusho`)が開く画面に当たる。

### 9.1 ファイル

| ファイル | 元の画面 | 内容 |
| --- | --- | --- |
| `UpdateForm/FormOutExcelNofusho2`(.cs / .Designer.cs / .resx) | `FormOutExcelNofusho` | 月分・処理日・医師会を指定して、Excel 出力(F11)・閉じる(F12) |

出力の処理(`AppExcelNofusho`)と、ファンクション キーの定義(`FuncOutExcelNofusho`)は、元の画面と同じものを使う。Gc 系・Yc 系のコントロール・コードは使っていない。

### 9.2 画面の構成

配置と色は、1.7 の値。フォームの大きさは 470 × 279。見出しは「会費納付書」、タイトルは「Excel出力：会費納付書」。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 月分 | `iDateYM`(`SDateBox`。年月。幅 150。入力必須) | 初期値は、今月。確定すると(Enter・フォーカスの移動・カレンダーでの選択)、処理日を決め直す |
| 処理日 | `iDate`(`SDateBox`。年月日。幅 186。入力必須) | 初期値は、月分の翌月 15 日。土曜・日曜なら、次の月曜。祝日は判定しないので、手で直す(元の画面と同じ) |
| 医師会 | `rdoNichii`・`rdoKeni`・`rdoShii`(`SRadioButton`。グループのパネル `secIkai` の中に、横に並べる) | 日医・県医・市医から 1 つ。初期値は日医 |

月分と処理日には、プレースホルダー「入力必須」を設定してあり、項目名は `Required = true`(1.7)。初期値が入るので、プレースホルダーが見えるのは、利用者が消したときだけ。

月分・処理日が空欄のまま出力しようとすると、その欄へフォーカスを移して、ツールチップ(「空欄にする事はできません」)を出す。

### 9.3 元の画面と変えた点

| # | 変えた点 | 理由 |
| ---: | --- | --- |
| 1 | タイトルを「Excel出力 - 会費納付書」から「Excel出力：会費納付書」にした | 1.7 のタイトルの決まり |
| 2 | ラジオ ボタンを囲む `GroupBox` を置かない。3 つのラジオ ボタンは、グループを示す小さな `SectionPanel`(`secIkai`)の子にする(同じパネルの中のラジオ ボタンは、1 つだけ選べる)。当初は、入力欄のパネルに直接置いていたが、グループを示すため、2026-11-26 に、利用者の指示でパネルを置いた(フォームは、高さ 5・幅 20 広げた) | 1.6 の 17 |
| 3 | 月分を変えたときに処理日を決め直すのは、月分を確定したとき(`SDateBox.ValueChanged`)。入力の途中では、決め直さない。元の画面(`UcDate.UcValueChanged`)が、入力の途中でも発生していたかは、確かめていない | `SDateBox` の動き(Sコントロール仕様の 6 章) |
| 4 | 作成のあと(2026-11-26)、月分の欄の幅を 140 から 150 に、処理日の欄の幅を 170 から 186 に広げた | 170 では、「日」がカレンダーのボタンに隠れたため(1.7) |

### 9.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際の Excel 出力(`AppExcelNofusho.Execute`)と、その後のメッセージ。検証は、データベースにつながずに、表示・月分からの処理日の計算(土曜・日曜・平日)・キーでの月分の入力・ラジオ ボタンの切り替え・空欄のときの動きを、画面の処理をそのまま動かして行った。グループのパネルと、広げた処理日の欄(「令和 08年12月15日」が収まること)は、画像で確かめた |
| 2 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |

メニュー(FormMain2 の `btnXlsNofusho`)から、これまでの画面と並べて開く(「画面の一覧」の注)。

---

## 10. 年間会費徴収明細書の印刷条件(FormOutChosyuMeisaiYear2)

年間会費徴収明細書の印刷条件の画面(`FormOutChosyuMeisaiYear`)を、S 系の部品で作り直したもの(2026-11-26)。メニュー(FormMain2 の `btnRepChosyuMeisaiYear`)が開く画面に当たる。

### 10.1 ファイル

| ファイル | 元の画面 | 内容 |
| --- | --- | --- |
| `UpdateForm/FormOutChosyuMeisaiYear2`(.cs / .Designer.cs / .resx) | `FormOutChosyuMeisaiYear` | 対象年と、会員・在籍区分・医療機関の範囲を指定して、印刷プレビュー(F9)・閉じる(F12) |
| `UpdateForm/AppSTableCombo.cs` | `AppTableCombo` | 会員の一覧(`SetComboBox_Kaiin`。コード・氏名・フリガナ。選択後は氏名を表示、値は会員の ID)と、医療機関の一覧(`SetComboBox_IryoKikan`。コード・名称。選択後はコードを表示、値は医療機関の ID)を足した。どちらも、元の設定と同じ列・幅・並び順 |

帳票(`ReportFlexChoshuMeisaiYear`)と、ファンクション キーの定義(`FuncOutChosyuMeisaiYear`)は、元の画面と同じものを使う。Gc 系・Yc 系のコントロール・コードは使っていない。

### 10.2 画面の構成

配置と色は、1.7 の値。フォームの大きさは 580 × 305。見出しは「出力条件」、タイトルは「年間会費徴収明細書」。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 対象年 | `iTaishoYear`(`SDateBox`。年。幅 116。カレンダーのボタンで、年を選ぶカレンダーを開く) | 入力必須(プレースホルダー「入力必須」、項目名に `*`)。初期値は、今年 |
| 会員 | `iKaiinFrom` ～ `iKaiinTo`(`STableComboBox`) | 会員の範囲。空欄の側は、絞り込まない。帳票へは、会員の ID(`SelectedValue`)を渡す |
| 在籍区分 | `iZaisekiFrom` ～ `iZaisekiTo`(`SComboBox`) | 在籍区分の範囲。「1：在籍」「2：異動」「3：退会」のように、値と名称を並べて表示する(`AppSCombo.SetComboBoxWithCode`。1.6 の 20)。未選択と、空欄の項目は、絞り込まない。帳票へは、区分の値(`GetValue(SelectedIndex)`)を渡す。値の範囲なので、「1：在籍 ～ 3：退会」は異動も含む。「在籍と退会だけ」のような指定はできない(元の画面と同じ) |
| 医療機関 | `iIryoFrom` ～ `iIryoTo`(`STableComboBox`) | 医療機関の範囲。空欄の側は、絞り込まない。帳票へは、医療機関の ID を渡す |

範囲を逆に指定したとき(大きいほうを左に入れたとき)の入れ替えは、帳票の側が行う(元の画面と同じ)。対象年が空欄のままプレビューしようとすると、その欄へフォーカスを移して、ツールチップを出す。

### 10.3 元の画面と変えた点

| # | 変えた点 | 理由 |
| ---: | --- | --- |
| 1 | 範囲の 2 つの入力欄の間に、「～」を置いた。元の画面は、間をあけているだけで、文字は無かった | 2 つの欄が、範囲の始まりと終わりであることを示すため(1.7) |
| 2 | 範囲の入力欄は、3 行とも同じ幅(160)にした。元の画面は、会員・医療機関が 138、在籍区分が 71 | 「～」と右の欄の位置を、3 行でそろえるため |
| 3 | 対象年の欄の形(年)を、デザイナーで設定した。元の画面は、表示のときに、処理で設定していた。作成時は、元の画面に合わせてカレンダーのボタンを出していなかった(幅 90)が、2026-11-26 に、`SDateBox` に年を選ぶカレンダーができたので、ボタンを出して、幅を 116 にした(利用者の指示) | デザイナーで開いたときも、実際と同じ形で見えるようにするため |
| 4 | 入力の確認(`validateRow`)は、対象年だけを調べる書き方にした。元の画面は、コントロールの種類で分ける書き方だったが、調べる対象は対象年だけだった | 使っていない分岐を持たないため。動きは同じ |
| 5 | 在籍区分のコンボを、「1：在籍」の形で表示する。元の画面は、名称だけ(「在籍」) | 「在籍 ～ 退会」が異動を含むことが、名称だけでは分からなかったため(利用者の提案。2026-11-26) |

### 10.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際の帳票の出力(`ReportFlexChoshuMeisaiYear`)。検証は、データベースにつながずに、アプリのデータベース管理クラスに見本の会員(5 件)・医療機関(3 件)を入れて、表示・一覧の内容・選んだときの表示と帳票へ渡す値・会員の欄での入力による絞り込み・対象年が空欄のときの動き・キーでの対象年の入力を、画面の処理をそのまま動かして行った |
| 2 | 会員・医療機関の件数が多いときの、一覧の表示の速さ |
| 3 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |
| 4 | 在籍区分のコンボで、数字のキー(「1」〜「3」)を押して区分を選べること(表示は画像で確かめた) |

メニュー(FormMain2 の `btnRepChosyuMeisaiYear`)から、これまでの画面と並べて開く(「画面の一覧」の注。銀行コードを読み込んでから開く指定 `needsBankCode: true` も、元の登録と同じ)。

---

## 11. Excel 出力の 5 画面

Excel へ出力する画面のうち、会費納付書(9 章)のほかの 5 つを、S 系の部品で作り直したもの(2026-11-26)。作り方は、どれも会費納付書と同じ。出力の処理(`AppExcel～`)と、ファンクション キーの定義(`FuncOutExcel～`。Excel 出力 F11・閉じる F12)は、元の画面と同じものを使う。Gc 系・Yc 系のコントロール・コードは使っていない。

### 11.1 ファイルと、画面の構成

ファイルは、どれも `UpdateForm/` の .cs / .Designer.cs / .resx。配置と色は、1.7 の値。タイトルは「Excel出力：(見出しの文字)」(市民病院会費請求資料だけは、元の画面と同じく「Excel出力：市民病院会費請求」)。

| 画面(元の画面は、末尾の 2 が無い名前) | 見出し | 項目 | メニューのボタン | 大きさ |
| --- | --- | --- | --- | --- |
| `FormOutExcelNenkanChoshu2` | 年間会費徴収書類 | 年(`iYear`。年。幅 116。初期値は今年。入力必須)、会員(`iKaiinFrom`。`STableComboBox`。入力必須)、書類タイプ(`rdoYotei` 予定通知書・`rdoJisseki` 徴収明細書(実績)。グループのパネル `secType` の中に縦に並べる。初期値は予定通知書) | `btnXlsNenkanChoshu` | 460 × 310 |
| `FormOutExcelFurikomi2` | 医師信用組合振込データ一覧 | 月分(`iDateYM`。年月。幅 150。初期値は今月)、処理日(`iDate`。年月日。幅 186。初期値は、月分の翌月 15 日。土曜・日曜なら次の月曜)。どちらも入力必須 | `btnXlsFurikomi` | 426 × 243 |
| `FormOutExcelFurikomiIrai2` | 振込依頼書 | 月分・処理日(上と同じ。処理日の計算は、`AppExcelFurikomiIrai.GetDefaultShoribi` を使う。元の画面と同じ) | `btnXlsFurikomiIrai` | 426 × 243 |
| `FormOutExcelKaihiShinsei2` | 各種医会等申請書 | 会員(`iKaiinFrom`。入力必須)、書類タイプ(新入会・新入会(研修医)・変更・変更(高齢用)・変更(4条C)の 5 つを、グループのパネル `secType` の中に縦に並べる。初期値は新入会) | `btnXlsKaihiShinsei` | 460 × 372 |
| `FormOutExcelShimingyoinKaihi2` | 市民病院会費請求資料 | 出力期間(年 `iYear`(幅 116。入力必須)と、期 `iPeriod`(`SComboBox`。Ⅰ期・Ⅱ期・Ⅲ期。年の欄の右に 6 あけて置く)。初期値は今年のⅠ期)、医会(`rdoKen` 県医・`rdoShi` 市医。グループのパネル `secIkai` の中に横に並べる。初期値は県医) | `btnXlsShiminByoin` | 442 × 248 |

入力必須の欄には、プレースホルダー(日付は「入力必須」、会員は「選択必須」)を設定してあり、項目名は `Required = true`(1.7。出力期間は、項目名 `lblPeriod` に付けて、年の欄にプレースホルダーを付ける)。入力必須の欄が空のまま出力しようとすると、その欄へフォーカスを移して、ツールチップ(「空欄にする事はできません」)を出す。年の欄は、カレンダーのボタンで、年を選ぶカレンダーを開く。年間会費徴収書類と振込データ一覧は、出力の前に、銀行コードを読み込んでいなければ読み込む(進行状況の画面を出す。元の画面と同じ)。

### 11.2 元の画面と変えた点

| # | 変えた点 | 理由 |
| ---: | --- | --- |
| 1 | タイトルの「Excel出力 - ○○」を、「Excel出力：○○」にした | 1.7 のタイトルの決まり |
| 2 | ラジオ ボタンを囲む `GroupBox` を置かず、グループを示す小さな `SectionPanel` の子にする(9.3 の 2 と同じ。パネルを置いたときに、フォームの高さを 5 広げた) | 1.6 の 17 |
| 3 | 月分を変えたときに処理日を決め直すのは、月分を確定したとき(9.3 の 3 と同じ) | `SDateBox` の動き(Sコントロール仕様の 6 章) |
| 4 | 各種医会等申請書の書類タイプは、行の間隔を 31 にした(元の画面は 26) | 1.7(ラジオ ボタンを縦に並べるとき) |
| 5 | 市民病院会費請求資料の、期のコンボの名前を `iPeriod`、市医のラジオ ボタンの名前を `rdoShi` にした(元の画面は `iPiriod`・`radioButton1`) | つづりの誤りと、意味の分からない名前を、新しい画面には持ち込まないため |
| 6 | 会員を選ぶ欄の値は `SelectedValue`、期の値は `SelectedValue` で取る(元の画面は `Value`・`ExGetValue()`) | S 系の部品での書き方(Sコントロール仕様の 3 章・4 章の対応の表) |
| 7 | 年間会費徴収書類の書類タイプを、横から縦の並びにした(作成時は、元の画面と同じく横。フォームは 518 × 274 から幅を詰めた) | 書類タイプは、すべての画面で縦に並べる(利用者の指示。1.7) |
| 8 | 年の欄(年間会費徴収書類・市民病院会費請求資料)に、カレンダーのボタンを出して、幅を 90 から 116 にした。市民病院会費請求資料は、期のコンボと医会のパネル、フォームの幅を、その分(26)右へ広げた | `SDateBox` に年を選ぶカレンダーができたため(利用者の指示。2026-11-26) |
| 9 | 振込データ一覧と振込依頼書の、月分の欄を 140 から 150、処理日の欄を 170 から 186 にした(フォームの幅も 16 広げた) | 9.3 の 4 と同じ |

### 11.3 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際の Excel 出力(`AppExcel～.Execute`)と、その後のメッセージ。銀行コードの読み込み(年間会費徴収書類・振込データ一覧)。検証は、データベースにつながずに、見本の会員(5 件)を入れて、表示・初期値・月分からの処理日の計算・キーでの月分の入力・ラジオ ボタンの切り替えと書類タイプの値・会員と期の選択・入力必須の欄が空のときの動きを、画面の処理をそのまま動かして行った |
| 2 | メニューから開いたときの動き(これまでの画面と並べて開く)。`FormMain2.cs` の登録の行を書き換えて、ビルドが通ることまでを確かめた |
| 3 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |

---

## 12. 帳票の印刷条件の 7 画面

帳票の印刷条件の画面のうち、年間会費徴収明細書(10 章)のほかの 7 つを、S 系の部品で作り直したもの(2026-11-26)。作り方は、どれも年間会費徴収明細書と同じ。帳票(`ReportFlex～`)と、ファンクション キーの定義(`FuncOut～`。印刷プレビュー F9・閉じる F12)は、元の画面と同じものを使い、帳票へは元の画面と同じ値を渡す。Gc 系・Yc 系のコントロール・コードは使っていない。

### 12.1 ファイルと、画面の構成

ファイルは、どれも `UpdateForm/` の .cs / .Designer.cs / .resx。配置と色は、1.7 の値。見出しは、どれも「出力条件」、タイトルは元の画面と同じ(帳票の名前)。

| 画面(元の画面は、末尾の 2 が無い名前) | タイトル | 項目(上から) | 帳票 | メニューのボタン | 大きさ(元の画面) |
| --- | --- | --- | --- | --- | --- |
| `FormOutHikiotoshiKakunin2` | 引落金額確認表 | 対象年月、会員、支払方法 | `ReportFlexHikiotoshiKakunin` | `btnRepHikiotoshiKakunin` | 580 × 274(491 × 255) |
| `FormOutSeikyuMinyukin2` | 未入金者用請求書 | 対象年月、会員、支払方法 | `ReportFlexSeikyuMinyukin` | `btnRepSeikyuMinyukin` | 580 × 274(491 × 247) |
| `FormOutKaihiList2` | 会費種別一覧表 | 会費区分、会費 | `ReportFlexKaihiList` | `btnRepKaihiList` | 580 × 243(491 × 288) |
| `FormOutKaihiListBank2` | 会費種別一覧表(銀行有) | 会費区分、会費 | `ReportFlexKaihiListBank` | なし(「画面の一覧」) | 580 × 243(491 × 288) |
| `FormOutKaihiUchiwakeMonth2` | 会費月別内訳 | 対象年月、会員、在籍区分、月別会費内訳 | `ReportFlexKaihiUchiwakeMonth` | `btnRepKaihiUchiwakeMonth` | 580 × 305(491 × 300) |
| `FormOutKaihiUchiwakeYear2` | 年会費内訳書 | 対象年(右に「※未実装」)、会員、在籍区分、月別会費内訳 | `ReportFlexKaihiUchiwakeYear` | `btnRepKaihiUchiwakeYear` | 580 × 305(491 × 300) |
| `FormOutKaihiYoteiYear2` | 年間会費徴収予定通知書 | 対象年月、会員、在籍区分、医療機関 | `ReportFlexKaihiYoteiYear` | `btnRepKaihiYoteiYear` | 580 × 305(491 × 303) |

メニューの登録は、どれも `needsBankCode: true`(元の登録と同じ)。

項目ごとの作り:

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 対象年月 | `iTaishoYM`(`SDateBox`。年月。幅 150) | 入力必須(プレースホルダー「入力必須」、項目名に `*`)。初期値は、今月の 1 日(`AppDate.ParseDateFirstDay`。元の画面と同じ) |
| 対象年 | `iTaishoYear`(`SDateBox`。年。幅 116。年を選ぶカレンダーを開く) | 入力必須。初期値は、今日。元の画面と同じく、帳票の絞り込みには使われていない(帳票へは渡すが、帳票の側で使っていない)。そのことを示す元の画面の「※未実装」(`lblTaishoYearNote`)を、欄の右に残した |
| 会員 | `iKaiinFrom` ～ `iKaiinTo`(`STableComboBox`。幅 160) | 会員の範囲。一覧は `AppSTableCombo.SetComboBox_Kaiin`(選択後は氏名を表示)。年会費内訳書だけは `SetComboBox_KaiinCode`(選択後は会員コードを表示。元の画面と同じ使い分け)。帳票へは、会員の ID(`SelectedValue`)を渡す |
| 支払方法・在籍区分・会費区分 | `iShiharaiFrom` ～ `iShiharaiTo`・`iZaisekiFrom` ～ `iZaisekiTo`・`iTypeKaihiFrom` ～ `iTypeKaihiTo`(`SComboBox`。幅 160) | 区分の値の範囲。「1：在籍」の形で表示する(`AppSCombo.SetComboBoxWithCode`。1.6 の 20)。帳票へは、区分の値(`GetValue(SelectedIndex)`)を渡す |
| 会費 | `iKaihiFrom` ～ `iKaihiTo`(`STableComboBox`。幅 160) | 会費の範囲(`AppSTableCombo.SetComboBox_Kaihi`)。長い会費名は、欄の幅に収まるよう、文字を縮めて表示する(部品の `ShrinkTextToFit`。既定でオン。8 pt まで縮めても収まらなければ、末尾を「…」にする) |
| 医療機関 | `iIryoFrom` ～ `iIryoTo`(`STableComboBox`。幅 160) | 医療機関の範囲(`SetComboBox_IryoKikan`) |
| 月別会費内訳 | `iUchiwakeFrom`(`SComboBox`。幅 160) | 空欄・不要・必要の 3 つ(値は null・true・false。元の画面と同じ)。範囲ではなく、1 つの欄 |

範囲の行は、2 つの欄の間に「～」を置く(10 章と同じ置き方)。範囲の空欄の側は、絞り込まない。入力必須は、対象年月と対象年だけで、空欄のままプレビューしようとすると、その欄へフォーカスを移して、ツールチップ(「空欄にする事はできません」)を出す。会費種別一覧表の 2 つは、入力必須の欄が無い(どの欄も、空欄なら絞り込まない)。

### 12.2 元の画面と変えた点

| # | 変えた点 | 理由 |
| ---: | --- | --- |
| 1 | 範囲の 2 つの欄の間に「～」を置き、範囲の欄の幅を 160 にそろえた(元の画面は、会員などが 138、区分が 71) | 10.3 の 1・2 と同じ |
| 2 | 支払方法・在籍区分・会費区分のコンボを、「1：在籍」の形で表示する。元の画面は、名称だけ | 10.3 の 5 と同じ |
| 3 | 対象年月・対象年に、プレースホルダーと、項目名の `*` を付けた。対象年の欄は、カレンダーのボタンを出す | 1.7 |
| 4 | フォームの大きさを、1.7 の余白に合わせた(12.1 の表) | 1.7 |
| 5 | 会費の欄の幅は、元の画面より広い 160 にしたが、これでも長い会費名は入らない。幅は広げずに、文字を縮めて表示することにした(利用者の判断。2026-11-26) | 部品の `ShrinkTextToFit`(Sコントロール仕様.md) |

### 12.3 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際の帳票の出力(印刷プレビュー)。検証は、データベースにつながずに、見本のデータで、表示・選択・入力までを、画面の処理をそのまま動かして行った |
| 2 | メニューから開いたときの動き(これまでの画面と並べて開く) |
| 3 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |
| 4 | 区分のコンボで、数字のキーを押して区分を選べること(10.4 の 4 と同じ) |

---

## 13. FB データ作成・iFAX データ作成(FormCreateBankFile2・FormCreateIfaxFile2)

銀行へ渡す引落データ(FB データ)と、iFAX の宛先リストを作る 2 つの画面を、S 系の部品で作り直したもの(2026-11-26)。作成の処理(`AppBankFile`・`AppIfaxFile`)と、ファンクション キーの定義(`FuncCreateBankFile`・`FuncCreateIfaxFile`。作成実行 F11・閉じる F12)は、元の画面と同じものを使う。作成の後の結果は、S 系の結果の画面(`FormLogView2`。14 章)で表示する。Gc 系・Yc 系のコントロール・コードは使っていない。

### 13.1 ファイル

| ファイル(`FMKaiin/UpdateForm/`) | 元の画面 | 内容 |
| --- | --- | --- |
| `FormCreateBankFile2`(.cs / .Designer.cs / .resx) | `FormCreateBankFile` | 月度・引落元銀行・引落日を指定して、FB データを作る |
| `FormCreateIfaxFile2`(同上) | `FormCreateIfaxFile` | iFAX グループを指定して、宛先リストを作る |

### 13.2 画面の構成

配置と色は、1.7 の値。

| 画面 | 項目 | コントロール | 内容 |
| --- | --- | --- | --- |
| FB データ作成 | 月度 | `iDateYM`(`SDateBox`。年月。幅 150。入力必須) | 初期値は、今月 |
| FB データ作成 | 引落元銀行 | `iBank`(`STableComboBox`。幅 300。入力必須。プレースホルダー「選択必須」) | 銀行情報の一覧(`AppSTableCombo.SetComboBox_Bank`)。初期値は、基本情報の振込元銀行(`AppGlobal.Basic.ID_Bank`) |
| FB データ作成 | 引落日 | `iDate`(`SDateBox`。年月日。幅 186。入力必須) | 初期値は、今月の 25 日。入力ルールは `t_basic.FBAS_DateYM` のもの(元の画面と同じ)。開いたときのフォーカスは、この欄 |
| iFAX データ作成 | iFAXグループ | `iIfax`(`STableComboBox`。幅 300) | 空欄なら、すべてのグループを出力する。開いたときのフォーカスは、この欄 |

| 画面 | 見出し | タイトル | 大きさ(元の画面) |
| --- | --- | --- | --- |
| FB データ作成 | 銀行用引落データ作成 | FBデータ作成 | 540 × 274(525 × 270) |
| iFAX データ作成 | iFAXデータ作成 | iFAXデータ作成 | 540 × 212(525 × 188) |

作成実行(F11)の流れは、元の画面と同じ。FB データ作成は、引落元銀行・月度・引落日の順に空欄を調べて(空欄なら、その欄へフォーカスを移して、ツールチップを出す)、保存先を尋ねてから作る。作成の結果が「データなし」「成功」「保存できない」「データの不一致」のどれかなら、結果の画面を開く(タイトルの見出しは「FBデータ作成結果」「iFAXデータ作成結果」)。

### 13.3 元の画面と変えた点・未確認の点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 結果の画面を、`FormLogView` から `FormLogView2` にした | 2 系の画面から、移行前の画面を開かないため |
| 2 | 入力必須の欄に、プレースホルダーと、項目名の `*` を付けた | 1.7 |
| 3 | 引落元銀行の値は `SelectedValue` で取る(元の画面は `Value`) | S 系の部品での書き方 |

未確認の点:

| # | 内容 |
| ---: | --- |
| 1 | 実際のファイルの作成(保存先の指定、FB データ・宛先リストの中身)と、結果の画面の表示。検証は、データベースにつながずに、見本のデータで画面を表示して行った |
| 2 | メニューから開いたときの動き(これまでの画面と並べて開く。FB データ作成は、銀行コードを読み込んでから開く) |
| 3 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |

---

## 14. 実行結果の画面(FormLogView2)

処理の結果(1 行に 1 件のメッセージ)を一覧で見せる画面(`FormLogView`)を、S 系の部品で作り直したもの(2026-11-26)。FB データ作成・iFAX データ作成(13 章)の後に開く。元の画面と同じく、ファンクション キーのバーは無く、下のボタンで操作する。Gc 系・Yc 系のコントロール・コードは使っていない。

### 14.1 ファイルと、画面の構成

ファイルは `UpdateForm/FormLogView2`(.cs / .Designer.cs / .resx)。フォームの大きさは 901 × 562(元の画面は 878 × 528)。タイトルは「実行結果」、見出しは、呼び出し側が `TitleText` で決める(既定は「結果」)。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 一覧 | `grid`(`SGridDB`) | No(幅 50)・状況(50、中央寄せ。成功は「〇」、警告は「△」、エラーは「×」)・内容(残りの幅)。成功の行は青、エラーの行は赤の文字。見るだけの一覧なので、指している行を選択の色で塗らない(`MarqueeStyle` は `NoMarquee`。元の画面と同じ) |
| 閉じる | `btnClose`(`SButton`。強調の色) | 閉じる |
| 実行・キャンセル | `btnExec`(強調の色)・`btnCancel`(`SButton`) | 確認のための表示(`LogType = TestResult`)のときだけ出す(このときは、閉じるのボタンを出さない)。実行は、確認のメッセージ(`AppMsgBoxIndex.ExecImportCSV`)で「はい」なら、`FormCloseReason.Exec` で閉じる。キャンセルと × は、閉じる前に確認のメッセージ(`AppMsgBoxIndex.CancelImportCSV`)を出し、「いいえ」なら閉じない |
| ログ出力 | `btnLog`(`SButton`) | 一覧を CSV(Shift-JIS)に保存する。初めのファイル名は「FBログ_(年月)分」(元の画面と同じ。iFAX データ作成から開いたときも、この名前) |

呼び出し側が渡すもの: `Table`(結果の表。`LogTable`)、`TitleText`、`SelectedYM`(ファイル名に使う年月)、`LogType`(既定は `Result`。このときは、実行とキャンセルのボタンを出さない)。いま 2 系の画面から開くのは、どれも `Result`。

### 14.2 元の画面と変えた点・未確認の点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 内容の列を、残りの幅をすべて使う列にした。元の画面は幅 500 で、横スクロール バーを出していた | 一覧の幅に合わせるため |
| 2 | ボタンを `SButton` にした。行の色は、`SGridDB.RowStyle` で付ける(元の画面は `FetchRowStyle` の処理) | S 系の部品にそろえるため |
| 3 | 見出しを、1.7 の見た目にした | 1.7 |

未確認の点:

| # | 内容 |
| ---: | --- |
| 1 | 実際の処理の結果での表示と、ログ出力(ファイルの保存) |
| 2 | `LogType = TestResult` での動き(実行・キャンセルのボタン、閉じるときの確認)。いま、2 系の画面からは使っていない |
| 3 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |

---

## 15. 引落データ作成(FormHikiotoshi2)

引落データの一覧の画面と、会員 1 人分の引落データの編集の画面を、S 系の部品で作り直したもの(2026-11-26)。メニュー(`btnHikiotoshi`)が開く `FormHikiotoshi` に当たる。Gc 系・Yc 系のコントロール・コードは使っていない。

### 15.1 ファイル

| ファイル(`FMKaiin/UpdateForm/`) | 元の画面 | 内容 |
| --- | --- | --- |
| `FormHikiotoshi2`(.cs / .Designer.cs / .resx) | `FormHikiotoshi` | 処理月度の引落データの一覧(会員ごとの合計)。作成実行(F5)・個別再作成(F6)・編集(F8、Enter、ダブルクリック)・閉じる(F12) |
| `FormHikiotoshi2_DlgEntry`(同上) | `FormHikiotoshi_DlgEntry` | 会員 1 人の、その月度の引落データの編集(セルの中で入力する)。追加(F2)・削除(F4)・登録(F11)・キャンセル(F12) |

ファンクション キーの定義(`FuncHikiotoshi`・`FuncHikiotoshi_DlgEntry`)と、データの作り方(会員の所属会費と調整金額から、引落データの行を作る)は、元の画面と同じ。

### 15.2 一覧の構成

配置と色は、1.7 の値。フォームの大きさは 612 × 717(元の画面は 579 × 690)。見出し・タイトルは「引落データ作成」。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 処理月度 | `iDateYM`(`SDateBox`。年月。幅 150。入力必須) | 初期値は、今月。確定すると(`ValueChanged`)、その月度の一覧を読み込み直す。項目名(幅 74)は、検索の行の決まり(1.7)で置き、印の分だけ幅を 60 から広げた |
| 一覧 | `grid`(`SGridDB`) | 会員コード(幅 120)・氏名(200)・引落金額(残り。桁区切り。`SGridDBCurrency`) |
| 合計人数・月額計 | `iTotalCount`・`iTotalCost`(`SNumberBox`。無効) | 一覧の件数と、引落金額の合計。表示するだけ |
| 最終更新 | `lblCreateDate`・`lblUpdateUser`(`SLabel`) | 見出しの右。その月度の引落データのうち、最後に更新した日時と担当者 |

読み込みは、進行状況の画面(`FormBg_Progress`)を出して、別のスレッドで集計の問い合わせを行う(元の画面と同じ)。読み込むたびに新しいビューができるので、最初は列と一緒に `SGridDB.EndAdd` で結び付け、2 回目からは `SGridDB.SetView` で、データだけを差し替える。一覧が 0 件のときは、個別再作成と編集のキーを使えなくする。

作成実行(F5)は、その月度のデータが無ければ作り、あれば 2 回確認してから消して作り直す。個別再作成(F6)は、指している会員の分だけを作り直す。どちらも、作り直した後に、一覧の表示位置を元に戻す(元の画面と同じ)。

### 15.3 編集の画面の構成

フォームの大きさは 664 × 494(元の画面は 636 × 410)。見出しは「引落データ編集」、タイトルは「引落データ作成：編集」(元の画面は「引落データ編集」)。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 処理月度・会員 | `iDateYM`・`iKaiin`(`STextBox`。無効) | 表示するだけ |
| 一覧 | `grid`(`SGridDB`) | 参加医会(幅 180。会費の ID を名称にして表示)・金額(150。桁区切り)・備考(残り)。会員の欄の右に「グリッドで直接編集できます。」(`lblNote`)と表示する |
| セルの入力 | `iKaihi`(`STableComboBox`。この会員の会費だけ)・`iCost`(`SNumberBox`)・`iMemo`(`STextBox`) | セルに重ねて入力する(Sコントロール仕様の 10.7)。結び付けは `SControlDB`。会費を選ぶと、その会費の支払方法を、行に入れる(`UpdateRunEx`)。金額を変えると、すぐに合計を直す |
| 引落金額 | `iTotalCost`(`SNumberBox`。無効) | 一覧の金額の合計 |

登録(F11)のときに、会費が空の行があれば、その行の会費のセルで入力を始めて、ツールチップを出す。登録すると、テーブルを保存する。キャンセル(F12)と × では、内容を変えていれば(`HasChanges`)確認を出し、閉じるときは変更を取り消す(`RejectChanges`)。どちらも元の画面と同じ。

### 15.4 元の画面と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 合計人数・月額計・引落金額を、無効にした `SNumberBox` にした(元の画面は、文字を組み立てて `GcNumberEx` の `Text` に入れていた) | 1.7(表示するだけの欄) |
| 2 | 処理月度の項目名を、印の分だけ広げた(幅 60 → 74、入力欄を右へ 14) | 1.7 |
| 3 | 編集の画面のタイトルを「引落データ作成：編集」にした | 1.7 のタイトルの決まり |
| 4 | 「グリッドで直接編集できます。」の文字を、名前の合う `lblNote` に置いた(元の画面は `lblCreateDate` という名前の部品に入れていた) | 中身と合う名前にするため |

### 15.5 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースでの読み込み・作成・個別再作成・編集の保存。検証は、データベースにつながずに、見本のデータを一覧に結び付けて、画面を表示して行った。検証の環境にはログインした担当者(`AppGlobal.LoginUser`)が無いので、登録の前の確認(`validateRow`)は通していない |
| 2 | この画面を、1 つだけで実際のデータベースにつないで開くこと |
| 3 | メニューから開いたときの動き。2 つの画面を並べて開いたときに落ちた件は、並べて開く処理で直した(19.1)。直した後の、実際のデータベースでの確認はしていない |
| 4 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |

最終更新の日時は、`yyyy/MM/dd hh:mm`(12 時間制の時)で表示する。元の画面と同じ書式で、意図したものかは未確認。

---

## 16. MAMIS データ取込(FormMamisImport2)

MAMIS の CSV を取り込む 3 つの画面(ファイルの指定・取込確認・取込結果と取込履歴)を、S 系の部品で作り直したもの(2026-11-26)。メニュー(`btnMamisImport`)と、会員の一覧の MAMIS取込(F7)から開く。読込・照合・取込の処理(`AppMamisImport`)と、ファンクション キーの定義は、元の画面と同じものを使う。Gc 系・Yc 系のコントロール・コードは使っていない。

### 16.1 ファイル

| ファイル(`FMKaiin/UpdateForm/`) | 元の画面 | 内容 |
| --- | --- | --- |
| `FormMamisImport2`(.cs / .Designer.cs / .resx) | `FormMamisImport` | 取込 CSV の指定。履歴(F9)・取込開始(F11)・閉じる(F12) |
| `FormMamisImport2_DlgConfirm`(同上) | `FormMamisImport_DlgConfirm` | 取込確認。CSV の会員ごとの照合の結果と、選んだ会員の取込内容。実行(F11)・キャンセル(F12) |
| `FormMamisImport2_DlgLog`(同上) | `FormMamisImport_DlgLog` | 取込結果(取込の直後)と取込履歴(F9)。閉じる(F12) |

### 16.2 画面の構成

配置と色は、1.7 の値。

| 画面 | 大きさ(元の画面) | タイトル | 構成 |
| --- | --- | --- | --- |
| ファイルの指定 | 620 × 212(602 × 202) | MAMISデータ取込 | 取込CSVファイル(`iFile`。ファイルを選ぶ入力欄 `SFileSelect`。開く用のダイアログ、`FileFilter` は「CSVファイル\|*.csv」。入力必須。項目名は幅 128) |
| 取込確認 | 1100 × 732(1100 × 691) | MAMISデータ取込：取込確認 | 上に件数の要約(`lblSummary`。CSV の会員数・取込できる・変更なし・取込できない)、その下に会員の一覧(`gridHead`)、下に、選んだ会員の取込内容(`gridDetail`) |
| 取込結果・取込履歴 | 1100 × 728(1100 × 691) | MAMISデータ取込：取込結果 / 取込履歴 | 上に取込の一覧(`gridHead`。新しい順)、下に、選んだ取込の内容(`gridDetail`)。取込の直後は `LogID` を渡して開き、その取込だけを「取込結果」として出す。履歴(F9)は全件を「取込履歴」として出す |

一覧の列:

| 画面 | 一覧 | 列(幅) |
| --- | --- | --- |
| 取込確認 | 会員(`gridHead`) | No(50)・状況(165、中央)・会員コード(80、右)・氏名(130)・医籍登録番号(110、右)・申請(110、中央)・MAMIS更新日時(150、中央)・項目数(55、右)・内容(残り)。変更なしの行は灰色、照合できない行(会員が見つからない・一致しない)は赤の文字 |
| 取込確認 | 取込内容(`gridDetail`) | No(50)・項目(170)・区分(60、中央)・現在の登録内容(370)・MAMISの内容(取込後)(残り) |
| 取込結果・取込履歴 | 取込(`gridHead`) | No(50)・処理日時(150、中央)・処理者(150)・取込会員数(100)・取込項目数(100)・取込ファイル(残り) |
| 取込結果・取込履歴 | 内容(`gridDetail`) | No(50)・会員コード(90、右)・氏名(140)・項目(160)・区分(60、中央)・変更前(260)・変更後(残り) |

下の一覧(`gridDetail`)は、見るだけなので、指している行を選択の色で塗らない。上の一覧で指す行を変えると(`RowColChange`)、下の一覧と、その見出し(`lblDetail`。「選択中の会員の取込内容：　(コード)　(氏名)」など)を作り直す。

取込開始(F11)の流れは、元の画面と同じ: ファイルの欄が空ならツールチップ、ファイルが無ければメッセージ → 読込と照合(この時点ではデータベースを更新しない。CSV でない・データが無い・開けないときはメッセージ) → 取込確認の画面 → 実行で閉じたときだけ取込 → 取込結果の画面。

### 16.3 元の画面と変えた点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 取込 CSV の欄を、`UcFileSelect`(Gc 系)から `SFileSelect` にした | Gc 系を使わないため(部品は Sコントロール仕様の 17 章) |
| 2 | 取込確認の一覧の、医籍登録番号の列を 95 から 110、MAMIS更新日時の列を 125 から 150 にした | フォントが大きくなり、見出しと値が収まらなかったため |
| 3 | タイトルの「MAMISデータ取込 - ○○」を、「MAMISデータ取込：○○」にした | 1.7 のタイトルの決まり |
| 4 | 取込 CSV の項目名を、印の分だけ広げた(幅 118 → 128、入力欄を右へ 10。右端はそのまま) | 1.7 |
| 5 | 行の文字の色は、`SGridDB.RowStyle` で付ける | S 系の部品での書き方 |

### 16.4 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際の MAMIS の CSV での読込・照合・取込と、データベースからの履歴の読み込み。検証は、データベースにつながずに、見本のデータで 3 つの画面を表示して行った(取込確認の列の幅は、このときの画像を見て広げた) |
| 2 | ファイルを選ぶ入力欄での、ダイアログからの選択と、ファイルのドラッグ アンド ドロップ |
| 3 | メニューと、会員の一覧から開いたときの動き |
| 4 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |

---

## 17. 会員情報(FormKaiin2)

会員情報の 5 つの画面(一覧・入力・異動情報・会費調整・在籍期間)を、S 系の部品で作り直したもの(2026-11-26)。メニュー(`btnKaiin`。`needsBankCode: true`)が開く `FormKaiin` に当たる。5 つとも、Gc 系・Yc 系のコントロール・コードは使っていない。

入力の画面(`FormKaiin_DlgEntry`)は、元の画面が約 2,400 行・約 200 個のコントロールある。作るときは、元のソースを決まった規則(1.6 の書き換え)で S 系へ書き換えるスクリプトを使い、そのあと配置と、下の 17.7 の点を直した。Designer.cs は、その後はデザイナーで直す(1.2 の最後の段落と同じ)。

### 17.1 ファイル

| ファイル(`FMKaiin/UpdateForm/`) | 元の画面 | 内容 |
| --- | --- | --- |
| `FormKaiin2`(.cs / .Designer.cs / .resx) | `FormKaiin` | 会員の一覧。追加(F2)・訂正(F3、Enter、ダブルクリック)・削除(F4)・コピー追加(F5)・再入会(F6)・MAMIS取込(F7)・会費調整(F8)・印刷プレビュー(F9)・在籍期間表示(F10)・閉じる(F12) |
| `FormKaiin2_DlgEntry`(同上) | `FormKaiin_DlgEntry` | 会員 1 人の入力。情報1表示(F5)・情報2表示(F6)・情報3表示(F7)・登録(F11)・キャンセル(F12) |
| `FormKaiin2_DlgEntry_Idoinfo`(同上) | `FormKaiin_DlgEntry_Idoinfo` | 異動情報 1 件の入力。登録(F11)・キャンセル(F12) |
| `FormKaiin2_DlgChosei`(同上) | `FormKaiin_DlgChosei` | 会費予定額の調整(セルの中で入力する)。追加(F2)・削除(F4)・登録(F11)・キャンセル(F12) |
| `FormKaiin2_DlgHistory`(同上) | `FormKaiin_DlgHistory` | 在籍期間の表示。閉じる(F12) |
| `UpdateForm/AppSTableCombo.cs` の `SetComboBox_Kaihi2`・`SetComboBox_Ishikai`・`SetComboBox_Gakkai`・`SetComboBox_Shisetsugyomu` | `AppTableCombo` の同じ名前のメソッド | 会費(区分つき)・医師会・学会・施設業務の一覧の内容。元と同じ列・幅・並び順 |
| `comset/UI/STabControl.cs` | 標準の `TabControl` | 入力の画面の、情報1 ～ 情報3 の切り替え(Sコントロール仕様の 16 章) |

ファンクション キーの定義(`FuncKaiin`・`FuncKaiin_DlgEntry`・`FuncKaiin_DlgEntry_Idoinfo`・`FuncKaiin_DlgChosei`・`FuncKaiin_DlgHistory`)と、入力ルール(`AppSControlRule`)は、すでにあるものを使う。処理は、元の画面と同じ構成(同じメソッド名)にしてある。

### 17.2 一覧の構成

配置と色は、1.7 の値。フォームの大きさは 1098 × 637(元の画面は 1084 × 607)。タイトルは「会員情報」、見出しは「会員一覧」。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 検索 | `iSearch`(`STextBox`。幅 250。プレースホルダー「コード、氏名、フリガナで検索」) | 入力するたびに絞り込む。数字だけなら会員コードが一致するもの、1 文字なら氏名かフリガナがその文字で始まるもの、2 文字以上なら含むもの(元の画面と同じ) |
| 生年月日 | `iSearchBirthday`(`SDateBox`。年月日。幅 186) | 確定すると、生年月日が一致するものに絞り込む |
| 医籍登録番号 | `iSearchIsekiTorokuNo`(`STextBox`。幅 100) | 数字だけのとき、番号が一致するものに絞り込む |
| 検索クリア | `btnClear`(`SButton`) | 3 つの検索欄を空にして、絞り込み直す(退会者を含めるかどうかは、そのまま) |
| グリッド | `grid`(`SGridDB`) | コード(80、右寄せ)・氏　名(200)・性別(60、中央)・生年月日(120、中央)・医籍登録番号(110、右寄せ)・在籍区分(80、中央)・会員区分(残り、中央)。退会した会員の行は、文字を Firebrick にする(`SGridDB.RowStyle`)。開いたときに、会員コードの順で読み込む。最初のフォーカスは、グリッド |
| 退会者を含める | `chkUnUsed`(`SCheckBox`。グリッドの下) | オフの間は、退会した会員を出さない |

検索の行は、1.7 の決まり(項目名の幅は「文字数 × 14 + 4」、項目名と入力欄の間 6、項目と項目の間 32、最後の入力欄とボタンの間 18)で並べてある。検索の条件は、すべてを満たす行だけを表示する(AND)。

追加・訂正・コピー追加・再入会・削除の処理(子の表の行の準備と、入力の画面から戻ったときの保存・取り消し)は、元の画面のまま。新しい会員コードは `AppDbID.GetNewCode` で採番する(19.3)。MAMIS取込(F7)は `FormMamisImport2`(16 章)を開き、閉じたら一覧を描き直す。印刷プレビュー(F9)は、帳票 `ReportFlexKaiinList` に、いまの一覧(絞り込んだまま)を渡す。

### 17.3 入力の画面の構成

フォームの大きさは 1298 × 970(元の画面は 1224 × 910)。タイトルは「会員情報：追加」「会員情報：訂正」「会員情報：コピー追加」「会員情報：追加(再入会)」(元の画面は「会員情報【追加】」など)。見出しは「会員情報」で、訂正のときだけ、見出しの右に最終更新の日時と担当者を出す。項目が多いので、行の間隔は 28(2 つ並べる行と、住所の 2 行目は 26。1.7)。

領域の構成:

| 領域 | 置き場所 | 主な項目 |
| --- | --- | --- |
| 左の列 | 入力欄のパネル(`secBody`)の左。項目名 x = 26・幅 118、入力欄 x = 150 | 入会年月日・承認日・申請年月日、会員コード、メールアドレス、医籍登録番号・医籍登録年月日、フリガナ・氏名(右に旧氏名のフリガナ・旧氏名)、郵便番号・住所(2 行)、携帯電話番号・電話番号・FAX、生年月日・性別、出身校・卒業年月・出身校(大学院)・修了年月・学位取得年月、会員区分・在籍区分、異動前医師会・会費減免事由、医師会の会費の一覧(`grid_KaihiIshikai`。日医・県医・市医の 3 行) |
| 右の列 | `tabInfo`(`STabControl`。(606, 16) に 656 × 776)。ページの中は、項目名 x = 14・幅 110、入力欄 x = 130 | 下の 3 ページ |
| 情報1(`tabPage1`) | | 診療科目(主)、診療科目(担当)の一覧(`grid_Sinryo`。追加・削除のボタン)、医療機関コード(`iIryoKikan`)と、その医療機関の情報(施設名通称・カナ・郵便番号・住所・電話・FAX・開設主体・病床有無・許可病床数・併設の施設・組コード。どれも無効にした、表示するだけの欄)、施設・業務、指定医(10 個の `SCheckBox` と、その他の欄) |
| 情報2(`tabPage2`) | | 参加医会の一覧(`grid_Kaihi`。追加・削除のボタン)、銀行情報(見出し「銀行情報」と、`SectionPanel`(`secBank`)の中に、指定口座①〜③。銀行コード・支店コード・銀行名・支店名(無効)・口座区分・口座番号・口座名義) |
| 情報3(`tabPage3`) | | 所属学会の一覧(`grid_Gakkai`。追加・削除のボタン)、異動情報の一覧(`grid_Ido`。追加・訂正・削除のボタン)、退会年月日・退会事由・異動先医師会・その他、処理区分(見出し「処理区分」と、`SectionPanel`(`secShori`)の中に、文書送付先・月別会費内訳・FAX送付先) |
| 右の列の下 | タブの下、右寄せ | 登録日(`iTorokuDate`) |

入力必須(登録のときに、空なら登録しない。元の画面と同じ 7 つ): 会員コード・氏名・フリガナ・入会年月日・在籍区分・登録日・文書送付先。7 つの項目名は `Required = true` で、入力欄にはプレースホルダー(在籍区分と文書送付先は「選択必須」、ほかは「入力必須」)を付けてある。登録のときは、ほかの会員と会員コードが重ならないことも確かめる。

仕組み:

| 仕組み | 内容 |
| --- | --- |
| 入力欄と行の結び付け | `SControlDB` を 6 つ使う。`sctl`(会員の表 `t_kaiin` の行。郵便番号からの住所の入力のため、郵便番号の DB を渡している)、`sctl_iryo`(選んだ医療機関の行。表示するだけ)、`sctl_kaihiIshikai`・`sctl_shinryo`・`sctl_gakkai`・`sctl_kaihi`(4 つの一覧の、指している行。セルに重ねた入力欄と結び付ける)。登録・閉じるときは、6 つすべての `Update()` を呼んで、フォーカスがある入力欄の内容を、行へ書き戻す(`updateFocusedInput`) |
| 一覧 | `SGridDB` を 5 つ使う(医師会の会費・診療科目・参加医会・所属学会・異動情報)。異動情報のほかの 4 つは、セルに入力欄を重ねて、その場で入力する(Sコントロール仕様の 10.7)。異動情報は、行の追加・訂正を、異動情報の画面(17.4)で行う(一覧で Enter・ダブルクリックでも訂正)。異動情報の「詳細」「その他」の列は、異動事由によって使う項目が変わるので、データに無い名前の列(`F_IdoDetail`・`F_IdoDetailMemo`)を `SGridDBUnbound` で作って、表示する文字列を組み立てる |
| 子の一覧の行 | 一覧の行(`DvKaiinKaihiIshikai`・`DvKaiinShinryo`・`DvKaiinGakkai`・`DvKaiinKaihi`・`DvKaiinIdo`)は、一覧の画面から渡されたビューに書く。データベースへの書き込みと、登録しなかったときの取り消しは、一覧の画面が行う(元の画面と同じ) |
| タブの切り替え | F5・F6・F7 で、情報1・情報2・情報3 のページを出す。Enter での移動は、ページをまたぐところで、ページを切り替えてから次の欄へ移す(医師会の会費の一覧の最後のセルから、情報1 の診療科目(主)へ) |
| エラーのときのタブ | 入力の誤りを出す欄が、表示していないページにあるときは、先にそのページへ切り替えてから、フォーカスを移して、ツールチップを出す(`showInputError`) |
| 医療機関の情報 | 医療機関コードの欄で医療機関を選ぶと(`SelectedValueChanged`)、その医療機関の行に絞り込んで、情報1 の表示するだけの欄を出し直す |
| 銀行コード | 銀行コード・支店コードの欄を入力するたびに、銀行名・支店名を引き直す。欄をダブルクリックすると、銀行コード選択の画面(`FormSelectorBankCode2`。3 章)を開き、閉じた後で破棄する |
| 破棄の確認 | キャンセル(F12)と × では、画面を表示してから、会員の行か、5 つの一覧の行のどれかが変わっていたときだけ、確認を出す。会員の行は `RowSnapshot`、一覧の行は、表示したときの全行の値をつないだ文字列(`childRowsText`)と比べる(1.6 の 18) |
| 区分の一覧 | 文書送付先・FAX送付先(施設 / 自宅)と、月別会費内訳(不要 / 必要)は、true / false の 2 つの項目を、処理で作る(`setBoolCombo`) |

### 17.4 異動情報の画面(FormKaiin2_DlgEntry_Idoinfo)

入力の画面の、異動情報の追加・訂正のボタンから開く。フォームの大きさは 500 × 305(元の画面は 531 × 237)。タイトルは「会員情報：異動情報：追加」「会員情報：異動情報：訂正」(デザイナーのタイトル「会員情報：異動情報」の後ろに、開くときに「：追加」「：訂正」を足す。元の画面は「会員-異動情報【追加】」など)。見出しは「異動情報」。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 異動年月日 | `iIdoDate`(`SDateBox`。年月日) | `IdoDate`。開いたときのフォーカスは、この欄 |
| 異動事由 | `iIdoKbn`(`SComboBox`) | `IdoJiyuKbn`。選んだ異動事由の詳細の欄だけを出す |
| 詳細 | 異動事由ごとのパネル(`pnl_Ido`・`pnl_Kaiin`・`pnl_Shozai`・`pnl_Address`・`pnl_Etc`)。同じ位置((150, 86))に重ねて置く | それぞれ、詳細のコンボと、その他の欄(`STextBox`)。その他の欄は、詳細のコンボで「その他」を選んでいるときだけ入力できる(選んでいないときは、欄と行の値を空にする)。異動事由が未選択のときは、項目名「詳細」も消す |

登録(F11)のときに、選んでいる異動事由のもの以外の詳細の値を消す(元の画面と同じ)。キャンセル(F12)と × では、入力があったときだけ確認を出す(1.6 の 18)。

元の画面と変えた点: 詳細の欄を、異動事由の右から、下の行へ移して、項目名「詳細」を付けた(元の画面は、項目名なし)。タイトルを「メイン名：サブ名」の形にした。

### 17.5 会費調整の画面(FormKaiin2_DlgChosei)

一覧の会費調整(F8)から、指している会員を渡して開く。フォームの大きさは 840 × 521(元の画面は 848 × 492)。タイトルは「会員情報：会費調整」(元の画面は「会費調整」)、見出しは「会費予定額調整」。

| 項目 | コントロール | 内容 |
| --- | --- | --- |
| 会員 | `iKaiin`(`STextBox`。無効) | 会員コードと氏名。右に「グリッドで直接編集できます。」(`lblNote`) |
| 一覧 | `grid`(`SGridDB`) | 月分(130、中央)・会費(200)・金額(120、桁区切り)・支払方法(100、中央)・備考(残り)。会費調整の表(`t_kaiin_chosei`)のうち、この会員の行を、月分の順に出す |
| セルの入力 | `iDateYM`(`SDateBox`。年月)・`iKaihi`(`STableComboBox`)・`iCost`(`SNumberBox`)・`iShiharai`(`SComboBox`)・`iMemo`(`STextBox`) | セルに重ねて入力する(Sコントロール仕様の 10.7)。結び付けは `SControlDB` |

追加(F2)は、支払方法を `eShiharai.Koza1` にした行を足して、その行の月分のセルで入力を始める。登録(F11)のときに、月分・会費・金額の空欄を調べて、空ならそのセルで入力を始めて、ツールチップを出す。キャンセル(F12)は、内容を変えていれば(`HasChanges`)確認を出す(元の画面と同じ)。

### 17.6 在籍期間の画面(FormKaiin2_DlgHistory)

一覧の在籍期間表示(F10)から、指している会員を渡して開く。フォームの大きさは 597 × 414(元の画面は 597 × 344)。タイトルは「会員情報：在籍期間」(元の画面は「在籍期間表示」)、見出しは「在籍期間」。

会員名・医籍登録番号・在籍期間(合計)を、無効にした `STextBox` で表示し、その下の一覧(`SGridDB`。No(50)・会員コード(100)・期間(残り))に、再入会の前の分を含めた在籍の期間を、入会日の順に出す。期間は「入会日 ～ 退会日(退会していなければ今日)」の後ろに、在籍した期間(`Kaiin.GetMembershipYearMonth`)を全角の括弧で付けた形。見るだけの画面。

### 17.7 元の画面と変えた点

一覧:

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 検索欄の上にあった説明の文字(「コード、氏名、フリガナで検索」)を、検索欄のプレースホルダーにした | 欄の中に出せば、説明の文字の行が要らないため |
| 2 | 生年月日の列を 90 から 120、医籍登録番号の列を 90 から 110 にした | フォントが大きくなり、値が切れたため |
| 3 | 退会した会員の行の色は、`SGridDB.RowStyle` で付ける | S 系の部品での書き方 |
| 4 | 開く画面を、すべて 2 系の画面にした(入力・会費調整・在籍期間・MAMIS データ取込) | 2 系の画面から、移行前の画面を開かないため |

グリッドの見出しの「氏　名」は、元の画面のまま(幅をそろえる空白が入っている。1.7 の「項目名の文字」の決まりには合っていない)。

入力の画面:

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 大きさを 1298 × 970 にして、行の間隔を 28 にした | 項目が多く、フォントが大きくなったため(1.7) |
| 2 | Enter での移動: 診療科目(担当)の一覧の最後の行からは、医療機関コードへ移る。所属学会の一覧の最後の行からは、異動情報の一覧へ移る | 元の画面は、別のページにある欄を移動先にしていて、移れないはずの指定だったため |
| 3 | 入力必須の欄が空のときに、その欄が表示していないページにあれば、そのページへ切り替えてから、メッセージを出す | 切り替えないと、フォーカスを移せず、メッセージも見えないため |
| 4 | 破棄の確認は、入力があったときだけ出す(一覧の行の変更も、入力として数える) | 1.6 の 18 |
| 5 | 医療機関の情報は、医療機関コードの一覧で選んだ医療機関のものを表示する。元の画面は、入力の途中の文字(`TextChanged`)を、コードとして検索していた | `STableComboBox` は、選択を値で持つため(6.4 の 2 と同じ) |
| 6 | 情報1 ～ 情報3 の切り替えを、`STabControl` にした。「銀行情報」と「処理区分」の `GroupBox` を、見出しと `SectionPanel` にした | 標準のコントロールを直接は置かない(1.7)。1.6 の 17 |
| 7 | 銀行コード選択の画面を `FormSelectorBankCode2` にして、閉じた後で破棄する | 3.3 の 5 と同じ |
| 8 | タイトルを「会員情報：追加」などにした | 1.7 のタイトルの決まり |
| 9 | 入力必須を、プレースホルダーと、項目名の `*` で示す | 1.7 |

フリガナと旧氏名のフリガナの項目名は、文字の前に全角の空白が入っている(「　フリガナ」)。意図したものかは未確認。

### 17.8 未確認の点

| # | 内容 |
| ---: | --- |
| 1 | 実際のデータベースにつないでの動作(一覧と会費調整の読み込み、追加・訂正・コピー追加・再入会・削除と保存、子の表の保存と取り消し、会費調整の保存、印刷プレビュー)。検証は、データベースにつながずに、見本のデータで 5 つの画面を表示して行った。入力の画面は、初期表示・タブの切り替え・セルでの入力・医療機関の切り替え・入力必須の確認・破棄の確認の判定を、一覧は検索と退会者の色を、会費調整は行の追加と登録の前の確認を確かめた |
| 2 | 一覧から入力の画面を開いて、登録・キャンセルで戻ったときの動作(一覧は、データベースを読むので、実際には開いていない) |
| 3 | 実機の IME での入力(氏名からフリガナへの読みの追記など)と、郵便番号からの住所の入力 |
| 4 | 銀行コード・支店コードの欄のダブルクリックで、銀行コード選択の画面を開いて、選んだ結果が入ること |
| 5 | メニューから開いたときの動き(これまでの画面と並べて開く) |
| 6 | Visual Studio のデザイナーで開いたときの表示(とくに、`STabControl` を置いた入力の画面)。表示倍率 100%・150% での見た目(検証は 120%) |

---

## 18. ログイン(FormLogin2)

ログインの画面(`FormLogin`)を、利用者から渡された画面のイメージに合わせて、S 系の部品で作り直したもの(2026-11-26)。`Program.cs` は、起動時に `FormLogin2` を開く(`FormLogin2 login = new FormLogin2();`)。元の `FormLogin` は、プロジェクトに残してあるが、どこからも開かない。Gc 系・Yc 系のコントロール・コードは使っていない。

### 18.1 ファイルと、画面の構成

ファイルは `UpdateForm/FormLogin2`(.cs / .Designer.cs / .resx)。フォームの大きさは 648 × 396(元の画面は 391 × 158)。タイトルは、元の画面と同じく、処理で「(アプリの名前) ＜ログイン＞」にする(`AppConst.AppTitle + " ＜ログイン＞"`。1.7 のタイトルの決まりの外)。ファンクション キーのバーは無い。

| 領域 | コントロール | 内容 |
| --- | --- | --- |
| 団体の名前 | `iconLogo`(`IconLogo`)・`lblCorporateType`「公益社団法人」・`lblOrganization`「藤沢市医師会」・`sepHeader`(縦線)・`lblAppTitle`「会員管理システム」 | メイン画面(FormMain2)の見出しと同じ部品・同じ色 |
| 見出し | `icoHeading`・`lblHeading`「ログイン」(BIZ UDPゴシック 18 pt 太字)・`pnlHeadingLine`(横線) | |
| 入力欄のパネル | `secBody`(`SectionPanel`) | 担当者コード(`iCode`。幅 150)・パスワード(`iPwd`。幅 340。`PasswordChar` は「●」)・ログインのボタン(`btnLogin`。`SButton`。強調の色)。項目名と入力欄は 12 pt、高さ 30 |

ほかの画面と違い、文字を大きくしてある(項目名・入力欄 12 pt)。値は、画面のイメージに合わせて決めたもの。

処理は元の画面と同じ: 開いたときに、前回ログインした担当者のコードを入れる(入っていれば、パスワードの欄から入力を始める)。担当者コードの入力ルールは `t_tantosha.FCD_Tanto` のもの(数字だけ)。ログインのボタンで、担当者コードが空なら担当者コード、パスワードが空ならパスワードの欄にツールチップを出す。担当者マスタを読み直して照合し、合えば、担当者コードを設定に保存して閉じる(`FormCloseReason.Exec`)。合わなければ、パスワードの不一致か、担当者が見つからないことを、ツールチップで示す。

### 18.2 元の画面と変えた点・未確認の点

| # | 内容 | 理由 |
| ---: | --- | --- |
| 1 | 見た目を、画面のイメージに合わせた(団体の名前、見出し、パネル) | 利用者の指示 |
| 2 | 動作確認用に、担当者コード「0」とパスワードをあらかじめ入れる行を、Debug ビルドのときだけ働くようにした(`#if DEBUG`)。元の画面は、Release ビルドでも働く状態だった | Release ビルドで、パスワードが入った状態で開かないようにするため。Release でも入れたい場合は、`#if DEBUG` を外す |

未確認の点:

| # | 内容 |
| ---: | --- |
| 1 | 実際のログイン(担当者マスタの読み込みと、パスワードの照合)。検証は、見本の表示で、画面のイメージとほぼ同じ見た目になること、空欄のときに止まってメッセージが出ること、担当者コードに数字以外が入らないことを確かめた |
| 2 | Enter キーでの操作(パスワードの欄からログインのボタンへの移動と、ボタンの実行) |
| 3 | Visual Studio のデザイナーで開いたときの表示。表示倍率 100%・150% での見た目(検証は 120%) |

---

## 19. 画面全体に関わること

### 19.1 並べて開く仕組みと、その直し(2026-11-26)

FormMain2 のメニューは、これまでの画面と S 系の画面を、左右に並べてモードレスで開く(`OpenSideBySideOnClick`。「画面の一覧」の注)。見比べるための一時的な仕組みで、いずれ無くす(利用者の判断)。処理は、`FormMain2.cs` の region「【一時対応】従来の画面と新しい画面を並べて開く」にまとめてあり、移行が済んだら、region ごと消す。

| 動き | 内容 |
| --- | --- |
| 開く順 | これまでの画面を先に開き、その画面の初回描画(`Shown` の処理)が終わってから、S 系の画面を開く。これまでの画面が、`Load` か `Shown` の中で閉じられたときは、S 系の画面を開かずに破棄する |
| 位置 | 2 つを、メニューの画面がある作業領域の中央に、左右に並べる。収まらないときは、右の画面を作業領域の右端に合わせて、左の画面に重ねる |
| もう一度押したとき | 開いている画面を前面に出す(もう 1 組は開かない)。両方を閉じた後に押すと、開き直す |

直した不具合: 口座振替の画面(引落データ作成。`FormHikiotoshi` と `FormHikiotoshi2`)を並べて開くと、アプリが落ちた(`bin\Debug\err.log` に 2 回記録があった)。

| 段階 | 起きていたこと |
| --- | --- |
| 1 | メニューが、2 つの画面を続けて開く(どちらもモードレス) |
| 2 | これまでの画面の初回描画が、読み込みの処理を呼び、進行状況の画面(`FormBg_Progress`)を `ShowDialog()` で出して待つ |
| 3 | その待ちの間に、S 系の画面の初回描画が割り込み、同じ読み込みを始める |
| 4 | 2 つの読み込みが、別々のスレッドで同時に、1 本の DB 接続(`OleDbConnection`。共有していて、使うたびに開いて閉じる)へ問い合わせ、片方が `InvalidOperationException` で失敗する。小さな .mdb で再現したところ、閉じてある 1 本の接続へ 2 つのスレッドから同時に `Fill` すると、200 回中 200 回、片方が失敗した(順に行えば 50 回すべて成功) |
| 5 | 失敗は捨てられ(下の弱い点)、一覧のビューが null のまま進み、`SGridDB.EndAdd(null)` の `ArgumentNullException` で落ちる |

原因は、2 つの画面を並べて開く登録にしたこと(どちらの画面の誤りでもない)。直し方は、上の表の「開く順」のとおり、「元の画面の表示が終わってから、新しい画面を開く」順にした(`FormMain2.cs` の、並べて開く処理だけを直した。利用者が選んだ案)。2 つの画面を開くのに見本の画面を使って、読み込みが順に走ることを確かめた(直す前は 2 つが同時に、直した後は 1 つずつ)。並べて開く画面のうち、初回描画で別のスレッドの読み込みをするのは、引落データ作成の組だけ(銀行コード マスタは `Load` の中で読むので順に走る。Excel 出力の画面は、実行のキーを押したときに読む)。

直していない、根本の弱い点(元からあるもの。2 つの画面を 1 つずつモーダルで開いていた間は、表に出なかった):

| 弱い点 | 内容 |
| --- | --- |
| 作業スレッドのエラーを捨てる | `FormBg_Progress` は、別のスレッドの処理が失敗しても、記録せずに閉じる(8.2) |
| 捕まえる例外が狭い | comset の `DBFill.Fill` は `OleDbException` しか捕まえない。今回の `InvalidOperationException` は、素通りする |
| DB 接続に排他が無い | DB 接続は 1 本で、別のスレッドから同時に使うことへの備え(排他)が無い |

未確認の点: 直した後に、実際のデータベースで、引落データ作成を並べて開くこと。

### 19.2 アプリのアイコン

`FMKaiin/Resources/icon.ico` を、`Resources/icon.png`(256 × 256)から作った、複数の大きさ入り(16・20・24・32・40・48・64・256。8 つ)のものにした(2026-11-26)。256 は `icon.png` をそのまま入れ、ほかは縮めて作った。

| 使われる場所 | 内容 |
| --- | --- |
| exe のアイコン | `FMKaiin.csproj` の `ApplicationIcon` が `Resources\icon.ico` |
| フォームのタイトル バーのアイコン | `icon.ico` を直接は読まない。`FormFrame.resx`(`FormFrame` を継承する画面すべて)と `FormMain2.resx`(メニューの画面)の `$this.Icon` に、`icon.ico` と同じ内容を埋め込んである |
| `Properties\Resources.resx` の `icon` | `icon.ico` をファイルの参照で読む(ビルドし直せば、新しい内容になる) |

アイコンを差し替えるときは、`icon.ico` と、2 つの resx の `$this.Icon` の両方を差し替える。`icon.png` は、プロジェクトに登録していない(アイコンを作る元としてだけ使った)。

未確認の点: 表示倍率ごとの、タイトル バー・タスク バーでの見え方(ログインの画面のタイトル バーに新しいアイコンが出ること、メニューの画面が例外なく開くことまでを確かめた)。

### 19.3 Gc 系の残り(2026-11-26 に検査)

`UpdateForm` の 44 画面(.cs・Designer.cs・resx の 132 ファイル)を検査した。結果:

| 対象 | 結果 |
| --- | --- |
| Gc 系・Yc 系・Uc 系のコントロールや型の直接の使用 | 無い。using は `ComponentSUI`(S 系の部品の名前空間。検査の時点では `App.Controls` だった。19.5)・`C1.Win.C1TrueDBGrid`・`ComponentDB`・`ComponentDebug`・`ComponentIO`・`System.*` だけ。Designer.cs の部品は、S 系と `SectionPanel`・`IconBox`・`IconLogo`・`FunctionKeyBar`・`BufferedPanel`・`C1TrueDBGrid`・`Panel`・`TabPage` だけ。resx の埋め込みのデータにも、Gc 系・Yc 系の型は無い |
| 標準のコントロール(TextBox・ComboBox・Label・Button・CheckBox・RadioButton など)の直接の配置 | 無い(`FormLogView2` が、保存先を尋ねるのに `SaveFileDialog` を使っているだけ) |
| 2 系の画面から開く、移行前の画面 | `FormBg_Progress`(S 系の見た目に作り替え済み。8 章)と、帳票クラス(`ReportFlex～`)を経由して開く `FormReportPreview`(Gc 系ではないが、S 系の見た目でもない。C1 のビューアと標準の ToolStrip)だけ。`FormSelector～` などは開かない |

共通のコードに残っている Gc 依存:

| 場所 | 内容 | 2 系の画面への影響 |
| --- | --- | --- |
| `AppDate.cs` の `SetDispSeireki` | Gc 系の `UcDate.SetKoyomiSeirekiOn` と `GGridDBCommon.KoyomiSeirekiOn` を呼ぶ(起動時に `AppGlobal` が呼ぶ。S 系の `SDateBox.DefaultCalendarKind` も、ここで設定する。1.3)。ファイルとして `ComponentGGridDB` を参照している | 2 系の画面が呼ぶ `AppDate` のメソッド(`GetDate`・`GetDateSeirekiSlash`・`GetDateSlash`・`GetYearMonth`・`ParseDateFirstDay`)は、Gc 系を通らない |
| `FormFrame.cs` の `CheckEnabledFormKeyPreview` | `GcComboBoxEx`・`GcDateEx` の判定が残っている | S 系の部品は、その前の `ISKeyUser` の判定(1.3)で終わる |

`AppDbID.GetNewCode`(新しいコードの採番。会員・医療機関・担当者の一覧の追加で使う)は、コードの桁数の上限を Gc 系の `AppDbRule` から引いていた。2026-11-26 に、`AppSControlRule` から引くように直した。採番に使う 3 項目(会員コード 6 桁、医療機関コード 5 桁、担当者コード 6 桁)は、新旧のルールで桁数が同じなので、結果は変わらない(上限の手前では次の番号、上限を超えると null を返すことを、確認用のプログラムで確かめた)。移行前の画面(`FormKaiin`・`FormMasterIryoKikan`・`FormMasterTantosha`)も同じメソッドを使うので、こちらも S 系のルールを見る。あわせて、桁数の指定が無いルール(0)は「上限なし」として扱い、上限の比較は long で行うようにした(10 桁以上のルールで int があふれるため)。実際のデータベースでの、画面からの採番は試していない。

### 19.4 デザイナーで開く前の注意

部品にプロパティを足したあと(たとえば `SLabel.Required`)は、Visual Studio でリビルドしてから、その部品を使う画面をデザイナーで開く。デザイナーは、Visual Studio 自身のビルドの結果(作業で使うビルドとは別のもの)を使うので、それが古いままだと、新しいプロパティの行を読めない。その状態で開くと、エラーになるか、保存したときに、その行が消える(FormMasterBasic2 で、作業者が足した項目名の `Required` の行が、デザイナーの保存で消えたことがある。原因は、これと考えられる)。

作業者が Designer.cs を書き換えた画面を、Visual Studio のデザイナーで開いたままにしていると、デザイナーが古い内容で上書きすることがある。Designer.cs を書き換えたときは、利用者に、その画面を読み込み直してもらう。`FMKaiin.csproj` を書き換えたときは、Visual Studio の再読み込みの確認で「破棄」を選んでもらう。

### 19.5 S 系の部品の置き場所と名前空間(2026-11-26)

S 系の部品は、2026-11-26 に、FMKaiin の `Controls/Common/` から、共通ライブラリー comset の `UI/` フォルダーへ移し、名前空間を `ComponentSUI` に変えた(移したものの一覧と経過は、Sコントロール仕様の 1.6)。画面の側に関わることを、ここに書く。

| 項目 | 内容 |
| --- | --- |
| 画面の .cs | `using ComponentSUI;` を書く。S 系のコントロールのほか、`SControlDB`・`SGridDB`・`FontFallback`・`FunctionKeyBinder`・`FuncKeyDefine` なども、この名前空間にある。以前の `using App.Controls;` は要らない(ロゴの `IconLogo` を使う画面だけ、残してある) |
| Designer.cs | 型を `ComponentSUI.STextBox` のように書く(以前は `App.Controls.STextBox`)。Visual Studio のデザイナーで保存すると、この形で書き出される |
| FMKaiin に残っている部品 | `FMKaiin/UI/` の `IconLogo`・`OrganizationLogoShape`(このアプリのロゴ)と、使っていない `GradientForm`・`StepHeaderLabel`。名前空間は `App.Controls` のまま |
| アプリの側のコード | `AppSControlRule`・`AppSTableCombo`・`AppSCombo`・`AppBankCodeFilter`・`RowSnapshot` は、`FMKaiin/UpdateForm/` にある(名前空間 `App`)。comset へは移していない(テーブルの項目名など、このアプリに固有のものを使うため) |
| 部品を直すとき | comset(別のリポジトリ Library)のファイルを直すことになる。FMKaiin.sln に comset のプロジェクトが入っているので、Visual Studio では、これまでと同じようにビルドできる。コミットは、FMKaiin と Library の 2 つのリポジトリで、対にして行う |
| デザイナー | 部品は comset.dll から読まれる。部品を直したあとは、Visual Studio でリビルドしてから、画面をデザイナーで開く(19.4) |

名前空間を変えたときに書き換えたのは、FMKaiin の 213 ファイル(`App.Controls.○○` を `ComponentSUI.○○` に 2,026 か所、`using` の置き換え 62 か所、`using ComponentSUI;` の追加 52 ファイル)と、`Test/` のファイル、自動で作られる `AppFormFuncKey.cs`・`AppMsgBox.cs`・`AppToolTip.cs` のテンプレート(`_doc/` の `.template`)。

未確認の点: Visual Studio のデザイナーで、画面を開いて保存すること(名前空間を変えた後)。実際のデータベースでの動作。
