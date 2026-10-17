param([string]$ReferenceData)
$ErrorActionPreference = 'Stop'
$taskProject = Join-Path $PSScriptRoot 'RevancedStudio.csproj'
& dotnet build $taskProject -c Release /m:1 /nodeReuse:false /p:UseSharedCompilation=false
if ($LASTEXITCODE -ne 0) { throw 'Studio build failed.' }
$taskOutput = Join-Path $PSScriptRoot 'bin/Release/net48'
$taskPackage = Join-Path $PSScriptRoot 'package'
[IO.Directory]::CreateDirectory($taskPackage) | Out-Null
foreach ($taskFile in Get-ChildItem -LiteralPath $taskOutput -File -Filter '*.dll') { Copy-Item -LiteralPath $taskFile.FullName -Destination $taskPackage -Force }
foreach ($taskName in @('REVANCED Studio.exe','REVANCED Studio.exe.config','REVANCED Studio.pdb')) { Copy-Item -LiteralPath (Join-Path $taskOutput $taskName) -Destination $taskPackage -Force }
foreach ($taskName in @('MadCard Maker REVANCED','MPNDossier')) {
    Copy-Item -LiteralPath (Join-Path $taskOutput ($taskName+'.exe')) -Destination (Join-Path $taskPackage ($taskName+'.dll')) -Force
    Copy-Item -LiteralPath (Join-Path $taskOutput ($taskName+'.pdb')) -Destination $taskPackage -Force
}
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'README.md') -Destination $taskPackage -Force
Copy-Item -LiteralPath (Join-Path $PSScriptRoot '../THIRD_PARTY_NOTICES.md') -Destination $taskPackage -Force
if ($ReferenceData) {
    foreach ($taskName in @('MadCard','Dossier')) { if (!(Test-Path -LiteralPath (Join-Path $ReferenceData "$taskName/db/base"))) { throw "Reference data is missing $taskName/db/base." } }
    foreach($taskFile in Get-ChildItem -LiteralPath $ReferenceData -Recurse -File){$taskRelative=[IO.Path]::GetRelativePath([IO.Path]::GetFullPath($ReferenceData),$taskFile.FullName);$taskTarget=Join-Path (Join-Path $taskPackage 'Data') $taskRelative;[IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($taskTarget))|Out-Null;Copy-Item -LiteralPath $taskFile.FullName -Destination $taskTarget -Force}
}
@{Built=(Get-Date).ToString('o');ReferenceDataBundled=(Test-Path -LiteralPath (Join-Path $taskPackage 'Data'));GameRuntimeTested=$false} | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $taskPackage 'verification.json')
Write-Output "Packaged Studio in $taskPackage. Reference data is supplied separately when omitted."
