$ErrorActionPreference = 'Stop'
$taskInstall = (Join-Path ([Environment]::GetFolderPath('UserProfile')) 'Downloads\REVANCED Studio')
$taskPackage = Join-Path $PSScriptRoot 'package'
$taskNames = @('REVANCED Studio.exe','REVANCED Studio.exe.config','REVANCED Studio.pdb','MadCard Maker REVANCED.dll','MadCard Maker REVANCED.pdb','MPNDossier.dll','MPNDossier.pdb','README.md','THIRD_PARTY_NOTICES.md','verification.json')
$taskNames = @($taskNames + @(Get-ChildItem -LiteralPath $taskPackage -Filter '*.dll' -File | Select-Object -ExpandProperty Name) | Select-Object -Unique)
if (!(Test-Path -LiteralPath (Join-Path $taskInstall 'REVANCED Studio.exe'))) { throw 'Studio is not installed in the expected folder.' }
if (Get-Process -Name 'REVANCED Studio' -ErrorAction SilentlyContinue) { throw 'Close REVANCED Studio before installing this update. No windows were closed.' }
foreach ($taskName in $taskNames) { if (!(Test-Path -LiteralPath (Join-Path $taskPackage $taskName))) { throw "Missing package file: $taskName" } }
$taskApplied = [Collections.Generic.List[string]]::new()
try {
    foreach ($taskName in $taskNames) {
        $taskTarget = Join-Path $taskInstall $taskName
        $taskSource = Join-Path $taskPackage $taskName
        if ((Test-Path -LiteralPath $taskTarget) -and (Get-FileHash -LiteralPath $taskTarget).Hash -eq (Get-FileHash -LiteralPath $taskSource).Hash) { continue }
        $taskTemp = $taskTarget + '.app-update.tmp'
        Copy-Item -LiteralPath $taskSource -Destination $taskTemp -Force
        if (Test-Path -LiteralPath $taskTarget) { [IO.File]::Replace($taskTemp,$taskTarget,$taskTarget+'.app-update.bak') }
        else { [IO.File]::Move($taskTemp,$taskTarget) }
        $taskApplied.Add($taskName)
        if ((Get-FileHash -LiteralPath $taskSource).Hash -ne (Get-FileHash -LiteralPath $taskTarget).Hash) { throw "Verification failed: $taskName" }
    }
} catch {
    foreach ($taskName in $taskApplied) { $taskTarget=Join-Path $taskInstall $taskName; if (Test-Path -LiteralPath ($taskTarget+'.app-update.bak')) { Copy-Item -LiteralPath ($taskTarget+'.app-update.bak') -Destination $taskTarget -Force } }
    throw
}
Write-Output "Updated and verified $($taskApplied.Count) application files. Project data was untouched; no windows were opened."
