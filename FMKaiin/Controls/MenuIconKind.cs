namespace App.Controls
{
    /// <summary>
    /// <see cref="MenuIconPainter"/> が描画できるアイコンの種類。
    /// </summary>
    public enum MenuIconKind
    {
        /// <summary>アイコンなし。</summary>
        None = 0,
        /// <summary>聴診器（ロゴ）。</summary>
        Stethoscope,
        /// <summary>カレンダー。</summary>
        Calendar,
        /// <summary>複数の人物。</summary>
        Users,
        /// <summary>1 人の人物。</summary>
        User,
        /// <summary>ノート PC。</summary>
        Laptop,
        /// <summary>ファイルの取り込み（下向き矢印付きの書類）。</summary>
        FileDown,
        /// <summary>チェックリスト。</summary>
        ListChecks,
        /// <summary>電卓。</summary>
        Calculator,
        /// <summary>プリンター。</summary>
        Printer,
        /// <summary>銀行。</summary>
        Bank,
        /// <summary>外部への出力（枠から外へ向かう矢印）。</summary>
        ExternalLink,
        /// <summary>書類。</summary>
        Document,
        /// <summary>歯車。</summary>
        Gear,
        /// <summary>タグ。</summary>
        Tag,
        /// <summary>硬貨。</summary>
        Coins,
        /// <summary>税額表（TAX の文字が入った書類）。</summary>
        Tax,
        /// <summary>アップロード（上向き矢印とトレイ）。</summary>
        Upload,
        /// <summary>ログアウト（扉から外へ向かう矢印）。</summary>
        LogOut,
        /// <summary>木の葉（塗りつぶし。左上から右下へ向かって濃くなる）。</summary>
        Leaf,
        /// <summary>調整つまみ（つまみ付きの横線 3 本）。</summary>
        Sliders,
        /// <summary>木の葉（細い線の輪郭だけ）。</summary>
        LeafOutline,
        /// <summary>日付の点が入ったカレンダー。</summary>
        CalendarDays,
        /// <summary>右上が折れた書類（本文の線付き）。</summary>
        FileText,
        /// <summary>右向きの山形（次へ進む向きを示す矢印）。</summary>
        ChevronRight,
        /// <summary>ずらして重ねた 2 つの四角（奥は淡い色、手前ははっきりした輪郭線。見出しの飾り用）。</summary>
        LayeredSquares,
        /// <summary>右上がりの斜線 2 本（1 本目は指定色、2 本目は淡い色。見出しの飾り用）。</summary>
        DoubleSlash,
        /// <summary>団体のロゴ（F の字に、波と、塔のある島影を重ねた図柄。指定色 1 色の塗りつぶし）。</summary>
        OrganizationLogo,
        /// <summary>病院（十字と入口の付いた建物）。</summary>
        Hospital,
        /// <summary>建物と一覧（建物の右に横線 3 本）。</summary>
        BuildingList,
        /// <summary>データベース（円筒）。</summary>
        Database,
        /// <summary>棒グラフ（縦軸・横軸と棒 3 本）。</summary>
        BarChart,
        /// <summary>表（枠と罫線）。</summary>
        Table,
        /// <summary>領収書（下端がぎざぎざの紙）。</summary>
        Receipt,
        /// <summary>表計算の書類（右上が折れた書類に × 印）。</summary>
        FileSpreadsheet,
        /// <summary>新規作成の書類（右上が折れた書類に + 印）。</summary>
        FilePlus,
        /// <summary>書類と硬貨（書類の右下に硬貨を重ねる）。</summary>
        DocumentCoin,
        /// <summary>情報（丸の中に i の字）。</summary>
        Info
    }
}
