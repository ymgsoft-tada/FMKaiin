# MenuIconKind の全アイコンを PNG に書き出す（「コントロール・フォーム仕様.md」のアイコン一覧用）。
#
# 使い方:
#   1. FMKaiin を Debug でビルドする
#   2. このスクリプトを実行する（引数なしで可）
#        powershell -ExecutionPolicy Bypass -File _doc\spec\export-icons.ps1
#
# 出力先は img\icons\<アイコン名>.png。アイコンを追加・変更したら実行し直して、仕様書の一覧にも行を足すこと。
# FMKaiin.exe は 32 ビット用なので、64 ビットの PowerShell から実行した場合は 32 ビットの PowerShell で実行し直す。
param(
    [string]$ExePath = (Join-Path $PSScriptRoot '..\..\FMKaiin\bin\Debug\FMKaiin.exe'),
    [string]$OutDir = (Join-Path $PSScriptRoot 'img\icons'),
    [int]$Size = 40
)

$ErrorActionPreference = 'Stop'

if ([Environment]::Is64BitProcess) {
    $powershell32 = Join-Path $env:SystemRoot 'SysWOW64\WindowsPowerShell\v1.0\powershell.exe'
    & $powershell32 -NoProfile -ExecutionPolicy Bypass -File $PSCommandPath -ExePath $ExePath -OutDir $OutDir -Size $Size
    exit $LASTEXITCODE
}

Add-Type -AssemblyName System.Drawing

$ExePath = (Resolve-Path $ExePath).Path
if (-not (Test-Path $OutDir)) {
    New-Item -ItemType Directory -Path $OutDir | Out-Null
}
$OutDir = (Resolve-Path $OutDir).Path

$assembly = [Reflection.Assembly]::LoadFrom($ExePath)
$kindType = $assembly.GetType('App.Controls.MenuIconKind', $true)
$painterType = $assembly.GetType('App.Controls.MenuIconPainter', $true)
$draw = $painterType.GetMethod('Draw', [Type[]]@([Drawing.Graphics], $kindType, [Drawing.RectangleF], [Drawing.Color], [single]))

# 画面のボタンと同じ青で、白地に描く（仕様書を暗い配色で見ても形が分かるようにするため）
$iconColor = [Drawing.Color]::FromArgb(16, 86, 150)
$padding = [single]($Size * 0.15)
$iconBounds = New-Object Drawing.RectangleF $padding, $padding, ($Size - $padding * 2), ($Size - $padding * 2)

$count = 0
foreach ($kind in [Enum]::GetValues($kindType)) {
    if ([int]$kind -eq 0) {
        continue   # None（アイコンなし）
    }

    $bitmap = New-Object Drawing.Bitmap $Size, $Size
    $graphics = [Drawing.Graphics]::FromImage($bitmap)
    try {
        $graphics.Clear([Drawing.Color]::White)
        $draw.Invoke($null, @($graphics, $kind, $iconBounds, $iconColor, [single]0.8)) | Out-Null
        $bitmap.Save((Join-Path $OutDir "$kind.png"), [Drawing.Imaging.ImageFormat]::Png)
        $count++
    }
    finally {
        $graphics.Dispose()
        $bitmap.Dispose()
    }
}

Write-Output "$count 個のアイコンを書き出しました: $OutDir"
