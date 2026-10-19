$ErrorActionPreference = 'Stop'
$packagePath = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot 'package'))
$installPath = [IO.Path]::GetFullPath((Join-Path ([Environment]::GetFolderPath('UserProfile')) 'Downloads\REVANCED Studio'))
if (!(Test-Path -LiteralPath (Join-Path $packagePath 'REVANCED Studio.exe'))) { throw 'Build the Studio package first.' }
$files = @(Get-ChildItem -LiteralPath $packagePath -Recurse -File)
$databasePath = Join-Path $packagePath 'Data\MadCard\db\base'
$databaseFiles = @(Get-ChildItem -LiteralPath $databasePath -Filter '*.json' -Recurse -File -ErrorAction SilentlyContinue)
if ($databaseFiles.Count -lt 1000) { throw 'The packaged MadCard reference database is missing or incomplete.' }
# This installer only creates a new app folder; it never replaces an existing project or mod.
if (Test-Path -LiteralPath $installPath) { throw 'The destination already exists. Inspect it before updating to protect local work.' }
New-Item -ItemType Directory -Path $installPath | Out-Null
foreach ($item in $files) {
    $relative = [IO.Path]::GetRelativePath($packagePath, $item.FullName)
    $target = [IO.Path]::GetFullPath((Join-Path $installPath $relative))
    if (!$target.StartsWith($installPath + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) { throw 'Invalid package path.' }
    [IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($target)) | Out-Null
    Copy-Item -LiteralPath $item.FullName -Destination $target
    if ((Get-FileHash -LiteralPath $item.FullName).Hash -ne (Get-FileHash -LiteralPath $target).Hash) { throw "Copy verification failed: $relative" }
}
Write-Output "Installed and verified $($files.Count) files in $installPath. No application was launched."
