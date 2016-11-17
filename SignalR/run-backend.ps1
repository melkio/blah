param(
    [String]$config="Debug"
)

$exe = Join-Path (Resolve-Path .) -ChildPath "Demo04.Backend\bin\$config\Demo04.Backend.exe"
& $exe