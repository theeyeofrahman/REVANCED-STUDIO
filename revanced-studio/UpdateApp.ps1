$ErrorActionPreference = 'Stop'
$taskInstall = (Join-Path ([Environment]::GetFolderPath('UserProfile')) 'Downloads\REVANCED Studio')
$taskPackage = Join-Path $PSScriptRoot 'package'
$taskNames = @('REVANCED Studio.exe','REVANCED Studio.exe.config','REVANCED Studio.pdb','MadCard Maker REVANCED.dll','MadCard Maker REVANCED.pdb','README.md','THIRD_PARTY_NOTICES.md','verification.json')
$taskNames = @($taskNames + @(Get-ChildItem -LiteralPath $taskPackage -Filter '*.dll' -File | Select-Object -ExpandProperty Name) | Select-Object -Unique)
if (!(Test-Path -LiteralPath (Join-Path $taskInstall 'REVANCED Studio.exe'))) { throw 'Studio is not installed in the expected folder.' }
if (Get-Process -Name 'REVANCED Studio' -ErrorAction SilentlyContinue) { throw 'Close REVANCED Studio before installing this update. No windows were closed.' }
foreach ($taskName in $taskNames) { if (!(Test-Path -LiteralPath (Join-Path $taskPackage $taskName))) { throw "Missing package file: $taskName" } }
$taskChanges = [Collections.Generic.List[string]]::new()
$taskRemoved = [Collections.Generic.List[string]]::new()
try {
    foreach ($taskName in $taskNames) {
        $taskTarget = Join-Path $taskInstall $taskName
        $taskSource = Join-Path $taskPackage $taskName
        if ((Test-Path -LiteralPath $taskTarget) -and (Get-FileHash -LiteralPath $taskTarget).Hash -eq (Get-FileHash -LiteralPath $taskSource).Hash) { continue }
        $taskTemp = $taskTarget + '.app-update.tmp'
        Copy-Item -LiteralPath $taskSource -Destination $taskTemp -Force
        if (Test-Path -LiteralPath $taskTarget) {
            $taskBackup=$taskTarget+'.app-update.bak'
            if (Test-Path -LiteralPath $taskBackup) { $taskBackup=$taskTarget+'.app-update-'+(Get-Date -Format 'yyyyMMddHHmmssfff')+'.bak' }
            [IO.File]::Replace($taskTemp,$taskTarget,$taskBackup)
            $taskChanges.Add($taskTarget+'|'+$taskBackup)
        } else { [IO.File]::Move($taskTemp,$taskTarget); $taskChanges.Add($taskTarget+'|') }
        if ((Get-FileHash -LiteralPath $taskSource).Hash -ne (Get-FileHash -LiteralPath $taskTarget).Hash) { throw "Verification failed: $taskName" }
    }
    foreach ($taskName in @('MPNDossier.dll','MPNDossier.pdb')) {
        $taskTarget=Join-Path $taskInstall $taskName
        if (Test-Path -LiteralPath $taskTarget) {
            $taskBackup=$taskTarget+'.app-update.bak'
            if (Test-Path -LiteralPath $taskBackup) { $taskBackup=$taskTarget+'.app-update-'+(Get-Date -Format 'yyyyMMddHHmmss')+'.bak' }
            [IO.File]::Move($taskTarget,$taskBackup)
            $taskRemoved.Add($taskTarget+'|'+$taskBackup)
        }
    }
} catch {
    foreach ($taskRecord in $taskRemoved) { $taskParts=$taskRecord.Split('|',2); if (Test-Path -LiteralPath $taskParts[1]) { [IO.File]::Move($taskParts[1],$taskParts[0]) } }
    $taskRollback=$taskChanges.ToArray(); [array]::Reverse($taskRollback)
    foreach ($taskRecord in $taskRollback) { $taskParts=$taskRecord.Split('|',2); if ($taskParts[1] -and (Test-Path -LiteralPath $taskParts[1])) { Copy-Item -LiteralPath $taskParts[1] -Destination $taskParts[0] -Force } elseif (Test-Path -LiteralPath $taskParts[0]) { Remove-Item -LiteralPath $taskParts[0] -Force } }
    throw
}
Write-Output "Updated and verified $($taskChanges.Count) application files. Dossier integration binaries were moved to backups; project data was untouched and no windows were opened."
