<#
.SYNOPSIS
    Genera el CV corto en PDF (espanol e ingles) para cada perfil de build/pdf-profiles.fsx.

.DESCRIPTION
    1. `dotnet fsi build/short-cv.fsx` escribe un HTML por perfil e idioma con el contenido F# del sitio.
    2. Edge sin ventana los imprime a PDF, sin encabezados ni pies del navegador.
    El perfil General va a src/CvWeb/wwwroot/pdf (lo enlaza el sitio); los demas, a cv-perfiles/
    (para subir a vacantes; no se publican).

.EXAMPLE
    ./build/short-cv.ps1
#>
$ErrorActionPreference = 'Stop'
$root = Resolve-Path (Join-Path $PSScriptRoot '..')
$work = Join-Path $env:TEMP 'cv-short'
$siteDir = Join-Path $root 'src\CvWeb\wwwroot\pdf'
$profilesDir = Join-Path $root 'cv-perfiles'
$edge = @(
    "${env:ProgramFiles(x86)}\Microsoft\Edge\Application\msedge.exe",
    "$env:ProgramFiles\Microsoft\Edge\Application\msedge.exe",
    "$env:ProgramFiles\Google\Chrome\Application\chrome.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $edge) { throw 'Se necesita Edge o Chrome para imprimir el PDF.' }

# Sin HTML viejos: un perfil borrado no debe seguir generando su PDF.
Remove-Item $work -Recurse -Force -ErrorAction SilentlyContinue
dotnet fsi (Join-Path $PSScriptRoot 'short-cv.fsx') $work | Out-Host
if ($LASTEXITCODE -ne 0) { throw 'short-cv.fsx fallo.' }
New-Item -ItemType Directory -Force $siteDir, $profilesDir | Out-Null

foreach ($html in Get-ChildItem $work -Filter 'cv-*-*.html') {
    if ($html.BaseName -notmatch '^cv-(?<profile>.+)-(?<lang>es|en)$') { continue }
    $lang = $Matches.lang.ToUpperInvariant()
    $pdf = if ($Matches.profile -eq 'General') {
        Join-Path $siteDir "Eduardo_Hernandez_Vergara_CV_$lang.pdf"
    } else {
        Join-Path $profilesDir ("Eduardo_Hernandez_Vergara_CV_{0}_{1}.pdf" -f $Matches.profile, $lang)
    }
    $profileDir = Join-Path $env:TEMP "cv-short-profile-$($html.BaseName)"
    Remove-Item $pdf -ErrorAction SilentlyContinue
    # A veces Edge sin ventana termina el PDF pero no se cierra: se espera un minuto como maximo.
    $edgeArgs = '--headless=new', '--disable-gpu', '--no-first-run', "--user-data-dir=`"$profileDir`"",
        '--no-pdf-header-footer', '--generate-pdf-document-outline', "--print-to-pdf=`"$pdf`"", ([Uri]$html.FullName).AbsoluteUri
    $proc = Start-Process $edge -ArgumentList $edgeArgs -PassThru -WindowStyle Hidden
    if (-not $proc.WaitForExit(60000)) { Stop-Process -Id $proc.Id -Force -ErrorAction SilentlyContinue }
    if (-not (Test-Path $pdf)) { throw "No se genero $pdf." }
    Write-Host "PDF: $pdf"
}
