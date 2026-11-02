<#
.SYNOPSIS
    Genera el CV corto en PDF (espanol e ingles) dentro de src/CvWeb/wwwroot/pdf.

.DESCRIPTION
    1. `dotnet fsi build/short-cv.fsx` escribe el HTML con el mismo contenido F# del sitio.
    2. Edge sin ventana lo imprime a PDF, sin encabezados ni pies del navegador.

.EXAMPLE
    ./build/short-cv.ps1
#>
$ErrorActionPreference = 'Stop'
$root = Resolve-Path (Join-Path $PSScriptRoot '..')
$work = Join-Path $env:TEMP 'cv-short'
$pdfDir = Join-Path $root 'src\CvWeb\wwwroot\pdf'
$edge = @(
    "${env:ProgramFiles(x86)}\Microsoft\Edge\Application\msedge.exe",
    "$env:ProgramFiles\Microsoft\Edge\Application\msedge.exe",
    "$env:ProgramFiles\Google\Chrome\Application\chrome.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $edge) { throw 'Se necesita Edge o Chrome para imprimir el PDF.' }

dotnet fsi (Join-Path $PSScriptRoot 'short-cv.fsx') $work | Out-Host
if ($LASTEXITCODE -ne 0) { throw 'short-cv.fsx fallo.' }
New-Item -ItemType Directory -Force $pdfDir | Out-Null

foreach ($lang in 'es', 'en') {
    $html = Join-Path $work "cv-$lang.html"
    $pdf = Join-Path $pdfDir ("Eduardo_Hernandez_Vergara_CV_{0}.pdf" -f $lang.ToUpperInvariant())
    $profileDir = Join-Path $env:TEMP "cv-short-profile-$lang"
    Remove-Item $pdf -ErrorAction SilentlyContinue
    # A veces Edge sin ventana termina el PDF pero no se cierra: se espera un minuto como maximo.
    $edgeArgs = '--headless=new', '--disable-gpu', '--no-first-run', "--user-data-dir=`"$profileDir`"",
        '--no-pdf-header-footer', "--print-to-pdf=`"$pdf`"", ([Uri]$html).AbsoluteUri
    $proc = Start-Process $edge -ArgumentList $edgeArgs -PassThru -WindowStyle Hidden
    if (-not $proc.WaitForExit(60000)) { Stop-Process -Id $proc.Id -Force -ErrorAction SilentlyContinue }
    if (-not (Test-Path $pdf)) { throw "No se genero $pdf." }
    Write-Host "PDF: $pdf"
}
