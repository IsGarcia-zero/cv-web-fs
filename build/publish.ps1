<#
.SYNOPSIS
    Publica el CV como sitio estatico listo para GitHub Pages, con el HTML pre-renderizado.

.DESCRIPTION
    1. `dotnet publish` en Release (el build regenera el CV corto en PDF si cambio el contenido).
    2. Sirve el resultado en un puerto local y lo abre con Edge sin ventana para que Blazor lo dibuje.
    3. Copia ese HTML dentro de `#main` del index.html publicado: el contenido se ve al instante
       (y los buscadores lo leen) mientras se descarga WebAssembly, que despues lo reemplaza.
    4. Agrega `.nojekyll` (GitHub Pages ignoraria la carpeta `_framework` sin el).

.EXAMPLE
    ./build/publish.ps1            # deja el sitio en ./publish/wwwroot
#>
param(
    [string]$Output = (Join-Path $PSScriptRoot '..\publish'),
    [int]$Port = 5199
)

$ErrorActionPreference = 'Stop'
$project = Join-Path $PSScriptRoot '..\src\CvWeb\CvWeb.fsproj'
$edge = @(
    "${env:ProgramFiles(x86)}\Microsoft\Edge\Application\msedge.exe",
    "$env:ProgramFiles\Microsoft\Edge\Application\msedge.exe",
    "$env:ProgramFiles\Google\Chrome\Application\chrome.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $edge) { throw 'Se necesita Edge o Chrome para pre-renderizar.' }

Write-Host '1/4 Publicando...'
dotnet publish $project -c Release -o $Output --nologo | Out-Host
if ($LASTEXITCODE -ne 0) { throw 'dotnet publish fallo.' }
$site = Resolve-Path (Join-Path $Output 'wwwroot')
$index = Join-Path $site 'index.html'

Write-Host '2/4 Sirviendo el sitio y dibujandolo con un navegador sin ventana...'
$server = Start-Process python -ArgumentList '-m', 'http.server', $Port, '--bind', '127.0.0.1', '--directory', "`"$site`"" `
    -PassThru -WindowStyle Hidden
try {
    # Espera a que el servidor responda antes de abrir el navegador.
    $ready = $false
    foreach ($i in 1..30) {
        try { Invoke-WebRequest "http://127.0.0.1:$Port/" -UseBasicParsing -TimeoutSec 2 | Out-Null; $ready = $true; break }
        catch { Start-Sleep -Milliseconds 500 }
    }
    if (-not $ready) { throw "El servidor local no respondio en el puerto $Port." }
    $profileDir = Join-Path $env:TEMP "cv-prerender-$Port"
    # Edge escribe avisos inofensivos en stderr; en Windows PowerShell eso detendria el script.
    $ErrorActionPreference = 'Continue'
    $dom = & $edge --headless=new --disable-gpu --no-first-run --user-data-dir="$profileDir" `
        --virtual-time-budget=20000 --dump-dom "http://127.0.0.1:$Port/" 2>$null | Out-String
    $ErrorActionPreference = 'Stop'
}
finally {
    Stop-Process -Id $server.Id -Force -ErrorAction SilentlyContinue
}

Write-Host '3/4 Insertando el HTML pre-renderizado...'
$match = [regex]::Match($dom, '(?s)<div id="main">(.*?)</div>\s*<script')
if (-not $match.Success -or $match.Groups[1].Value -notmatch 'class="shell"') {
    throw 'Blazor no termino de dibujar en el navegador sin ventana; no se pre-renderizo nada.'
}
$body = $match.Groups[1].Value
# Quita lo que los scripts agregan en tiempo de ejecucion, para que vuelvan a iniciarse sobre el HTML estatico.
$body = $body -replace ' data-(started|running|tabs-ready|left|right)(="[^"]*")?', ''
$body = $body -replace ' aria-current="true"', ''
$body = $body -replace ' class="block reveal visible"', ' class="block reveal"'

$html = Get-Content $index -Raw -Encoding UTF8
$html = [regex]::Replace($html, '(?s)<div id="main">.*?</div>(\s*<script)', "<div id=`"main`">$body</div>`$1", 1)
$html = $html -replace '<html lang="es" data-theme="dark">', '<html lang="es" data-theme="dark" data-prerendered="">'
Set-Content $index $html -Encoding UTF8 -NoNewline

Write-Host '4/4 Archivos de GitHub Pages...'
New-Item (Join-Path $site '.nojekyll') -ItemType File -Force | Out-Null
Write-Host "Listo: $site"
