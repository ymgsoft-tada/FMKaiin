#!/bin/sh
# S 系コントロール（comset の UI フォルダー。以前は FMKaiin/Controls/Common）のテストを、ビルドして実行する。
#
# 使い方:
#   sh Test/run.sh               すべてのテストを実行する
#   sh Test/run.sh DateHarness   指定したテストだけを実行する（複数指定できる）
#
# テストの種類:
#   TextHarness・ComboHarness・TableHarness・NumberHarness・DateHarness・DbHarness・ToolTipHarness・MsgBoxHarness・GridHarness・CheckHarness・RadioHarness・ProgressHarness
#     comset の UI フォルダーのソースと一緒にコンパイルして、コントロールを単体で確かめる
#     （内部用のクラスも確かめるため。同じクラスが comset.dll にもあるので、ビルドの記録に CS0436 の警告が出るが、ソースのほうが使われる）。
#     GridHarness は、グリッド（C1TrueDBGrid）を使うので、そのライセンスの情報を実行ファイルに埋め込む
#     （Visual Studio がアプリをビルドするときと同じ仕組み。ライセンス コンパイラー lc.exe を使う）。
#   RuleHarness・FrameHarness
#     ビルドした FMKaiin.exe の中のクラスを使う（入力ルールの定義の比較、FormFrame との組み合わせ）。
#     FMKaiin.exe が古いとコンパイルできない、または古い内容を確かめることになるので、先にビルドし直すこと。
#
# 前提:
#   先に FMKaiin を Debug でビルドしてあること。その出力（FMKaiin/bin/Debug）にある comset.dll などを参照する。
#   別のフォルダーの出力を使う場合は、環境変数 FMKAIIN_OUT にそのフォルダーを指定する。
#   comset のソースは、FMKaiin.csproj が参照しているのと同じ場所（このリポジトリの 2 つ上の _Library/_All/comset/comset）にあること。
#   別の場所にある場合は、環境変数 COMSET_UI に、UI フォルダーを指定する。
#   Visual Studio 2022 の C# コンパイラーを使う。場所が違う場合は、環境変数 CSC に csc.exe のパスを指定する。
#   GridHarness は、ライセンス コンパイラーも使う。場所が違う場合は、環境変数 LC に lc.exe のパスを指定する。
#
# テストの中身:
#   各テストは 1 つの実行ファイルで、画面の外にフォームを作ってコントロールを置き、キー入力を送ったり値を設定したりして、
#   結果が期待どおりかを 1 件ずつ比べる。1 件ごとに ok / NG を表示し、すべて通ると ALL PASSED と表示する。
#   実機の IME・実際のマウス操作・見た目は確かめられない。データベースには接続しない。
#
# 出力先は Test/bin（Git の管理外）。実行中は、テスト用のウィンドウが一時的に前面になる。
# フォーカスの移動を確かめるテストがあるので、実行中にほかのウィンドウを操作すると、失敗することがある。
# 失敗したテストだけを、名前を指定して実行し直すこと。
#
# どれを実行するか:
#   変更した部分に関係するテストだけを、名前を指定して実行する。変更のたびに、すべてを実行する必要はない。
#   例: テーブル コンボの動作を変えた → TableHarness。入力ルールを変えた → RuleHarness。
#       アイコンや色・配置だけの変更 → テストは実行せず、ビルドと画面の表示で確かめる。
#   すべてを実行するのは、共通の土台（SEditBase など）や、複数のコントロールにまたがる変更のとき。

HERE="$(cd "$(dirname "$0")" && pwd)"
APP="$HERE/../FMKaiin"
UI="${COMSET_UI:-$HERE/../../../_Library/_All/comset/comset/UI}"
REF="${FMKAIIN_OUT:-$APP/bin/Debug}"
OUT="$HERE/bin"
CSC="${CSC:-/c/Program Files/Microsoft Visual Studio/2022/Community/MSBuild/Current/Bin/Roslyn/csc.exe}"
LC="${LC:-/c/Program Files (x86)/Microsoft SDKs/Windows/v10.0A/bin/NETFX 4.8 Tools/lc.exe}"

if [ ! -f "$REF/comset.dll" ]; then
  echo "comset.dll が見つかりません: $REF" >&2
  echo "先に FMKaiin を Debug でビルドするか、環境変数 FMKAIIN_OUT に出力フォルダーを指定してください。" >&2
  exit 2
fi
if [ ! -d "$UI" ]; then
  echo "comset の UI フォルダーが見つかりません: $UI" >&2
  echo "環境変数 COMSET_UI に、UI フォルダーを指定してください。" >&2
  exit 2
fi
if [ ! -f "$CSC" ]; then
  echo "C# コンパイラーが見つかりません: $CSC" >&2
  echo "環境変数 CSC に csc.exe のパスを指定してください。" >&2
  exit 2
fi

TESTS="$*"
if [ -z "$TESTS" ]; then
  TESTS="TextHarness ComboHarness TableHarness NumberHarness DateHarness DbHarness ToolTipHarness MsgBoxHarness GridHarness CheckHarness RadioHarness ProgressHarness RuleHarness FrameHarness"
fi

mkdir -p "$OUT"
# 実行時に読み込まれる DLL と、アプリの実行ファイルを、テストの実行ファイルと同じ場所に置く
cp -u "$REF"/*.dll "$OUT"/
if [ -f "$REF/FMKaiin.exe" ]; then
  cp -u "$REF/FMKaiin.exe" "$OUT"/
fi

REFS="-r:System.dll -r:System.Core.dll -r:System.Data.dll -r:System.Xml.dll -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:Microsoft.VisualBasic.dll"
COMSET="-r:$(cygpath -w "$REF/comset.dll")"
# グリッド（C1TrueDBGrid）。SGridDB が使う。Gc 系ではないので、UI フォルダーから参照してよい
C1GRID="-r:$(cygpath -w "$REF/C1.Win.C1TrueDBGrid.4.dll")"
# 既存のルールの読み替え（SControlDBRuleConverter）は Gc 系の定義を使うので、テストでは GrapeCity の DLL も参照に入れる
GRAPECITY="-r:$(cygpath -w "$REF/GrapeCity.Win.Editors.v60.dll")"
CONVERTER="$(cygpath -w "$HERE/SControlDBRuleConverter.cs")"

SRC=""
for f in "$UI"/*.cs; do
  SRC="$SRC $(cygpath -w "$f")"
done

FAILED=0

# comset の UI フォルダーが、Gc 系（GrapeCity の DLL）を参照せずにコンパイルできることを確かめる。
# このフォルダーのものは、将来 Gc 系を外しても動くようにしておく決まりなので、ここで見張る
if "$CSC" -nologo -langversion:7.3 -target:library "-out:$(cygpath -w "$OUT/CommonCheck.dll")" $REFS "$COMSET" "$C1GRID" $SRC > "$OUT/CommonCheck.log" 2>&1; then
  echo "comset/UI: Gc 系を参照せずにコンパイルできる"
else
  echo "comset/UI: Gc 系を参照せずにコンパイルできない（$OUT/CommonCheck.log）"
  FAILED=1
fi

TOTAL=0
for t in $TESTS; do
  if [ ! -f "$HERE/$t.cs" ]; then
    echo "$t: テストがありません"
    FAILED=1
    continue
  fi

  case "$t" in
    RuleHarness|FrameHarness)
      # ビルドした FMKaiin.exe の中のクラスを使う。FMKaiin.exe は 32 ビットなので、テストも 32 ビットにする
      "$CSC" -nologo -langversion:7.3 -platform:x86 -target:exe "-out:$(cygpath -w "$OUT/$t.exe")" $REFS "$COMSET" "$GRAPECITY" \
        "-r:$(cygpath -w "$OUT/FMKaiin.exe")" "$(cygpath -w "$HERE/$t.cs")" "$CONVERTER" > "$OUT/$t.build.log" 2>&1
      ;;
    GridHarness)
      # グリッドのライセンスの情報を作って、実行ファイルに埋め込む（無いと、実行時にグリッドが評価版の表示を出して止まる）。
      # ライセンス コンパイラーは、まれに異常終了するので、できるまで 3 回まで試す
      if [ ! -f "$LC" ]; then
        echo "$t: ライセンス コンパイラーが見つかりません: $LC（環境変数 LC に lc.exe のパスを指定してください）"
        FAILED=1
        continue
      fi
      printf 'C1.Win.C1TrueDBGrid.C1TrueDBGrid, C1.Win.C1TrueDBGrid.4\r\n' > "$OUT/$t.licx"
      LICENSED=0
      for try in 1 2 3; do
        if (cd "$OUT" && "$LC" -nologo "-target:$t.exe" "-complist:$t.licx" -outdir:. -i:C1.Win.C1TrueDBGrid.4.dll) > "$OUT/$t.build.log" 2>&1; then
          LICENSED=1
          break
        fi
      done
      if [ $LICENSED -ne 1 ] || [ ! -f "$OUT/$t.exe.licenses" ]; then
        echo "$t: グリッドのライセンスの情報を作れない（$OUT/$t.build.log）"
        FAILED=1
        continue
      fi
      "$CSC" -nologo -langversion:7.3 -target:exe "-out:$(cygpath -w "$OUT/$t.exe")" "-resource:$(cygpath -w "$OUT/$t.exe.licenses")" \
        $REFS "$COMSET" "$C1GRID" "$GRAPECITY" "$(cygpath -w "$HERE/$t.cs")" "$CONVERTER" $SRC > "$OUT/$t.build.log" 2>&1
      ;;
    *)
      "$CSC" -nologo -langversion:7.3 -target:exe "-out:$(cygpath -w "$OUT/$t.exe")" $REFS "$COMSET" "$C1GRID" "$GRAPECITY" \
        "$(cygpath -w "$HERE/$t.cs")" "$CONVERTER" $SRC > "$OUT/$t.build.log" 2>&1
      ;;
  esac
  if [ $? -ne 0 ]; then
    echo "$t: コンパイルできない（$OUT/$t.build.log）"
    FAILED=1
    continue
  fi

  "$OUT/$t.exe" > "$OUT/$t.log" 2>&1
  PASSED=$(grep -c "^ok" "$OUT/$t.log")
  TOTAL=$((TOTAL + PASSED))
  if grep -q "^ALL PASSED" "$OUT/$t.log"; then
    echo "$t: $PASSED 件合格"
  else
    echo "$t: 失敗あり（$OUT/$t.log）"
    grep "^NG" "$OUT/$t.log"
    FAILED=1
  fi
done

echo "合格 $TOTAL 件"
if [ $FAILED -ne 0 ]; then
  echo "失敗があります"
fi
exit $FAILED
