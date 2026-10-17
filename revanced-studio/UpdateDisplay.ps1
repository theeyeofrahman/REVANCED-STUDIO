$ErrorActionPreference = 'Stop'
$taskInstall = (Join-Path ([Environment]::GetFolderPath('UserProfile')) 'Downloads\REVANCED Studio')
if (!(Test-Path -LiteralPath (Join-Path $taskInstall 'REVANCED Studio.exe'))) { throw 'Studio is not installed in the expected folder.' }
foreach ($taskName in @('REVANCED Studio.exe.config','README.md','verification.json')) {
    $taskSource = Join-Path (Join-Path $PSScriptRoot 'package') $taskName
    $taskTarget = Join-Path $taskInstall $taskName
    $taskTemporary = $taskTarget + '.display-update.tmp'
    Copy-Item -LiteralPath $taskSource -Destination $taskTemporary -Force
    if (Test-Path -LiteralPath $taskTarget) { [IO.File]::Replace($taskTemporary,$taskTarget,$taskTarget+'.display-update.bak') }
    else { [IO.File]::Move($taskTemporary,$taskTarget) }
    if ((Get-FileHash -LiteralPath $taskSource).Hash -ne (Get-FileHash -LiteralPath $taskTarget).Hash) { throw "Verification failed: $taskName" }
}
Write-Output 'High-DPI update installed and verified. No application was opened or closed.'
