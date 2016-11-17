param(
    [Int32]$port=8080
)

$iis = "C:\Program Files\IIS Express\iisexpress.exe"
$path = Join-Path (Resolve-Path .) -ChildPath "Demo04.Frontend"

& $iis /path:$path /port:$port