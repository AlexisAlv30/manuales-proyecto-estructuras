# Genera los PDF de los manuales a partir de los archivos HTML usando Microsoft Edge.
# Uso: clic derecho → "Ejecutar con PowerShell", o en una terminal:
#   powershell -ExecutionPolicy Bypass -File docs\fuentes\generar_pdf.ps1

$edge = "${env:ProgramFiles(x86)}\Microsoft\Edge\Application\msedge.exe"
$aqui = Split-Path -Parent $MyInvocation.MyCommand.Path
$docs = Split-Path -Parent $aqui
$perfil = Join-Path $env:TEMP "edge-manuales-logistica"

$manuales = @(
    @{ Html = "manual_tecnico.html"; Pdf = "Manual_Tecnico.pdf" },
    @{ Html = "manual_usuario.html"; Pdf = "Manual_Usuario.pdf" }
)

foreach ($manual in $manuales) {
    $html = Join-Path $aqui $manual.Html
    $pdf = Join-Path $docs $manual.Pdf
    $url = "file:///" + ($html -replace '\\', '/')

    & $edge --headless=new --disable-gpu --no-pdf-header-footer --user-data-dir="$perfil" --print-to-pdf="$pdf" $url | Out-Null
    Write-Host "Generado: $pdf"
}
