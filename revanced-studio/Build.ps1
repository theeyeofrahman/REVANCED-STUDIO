param([string]$ReferenceData)
$ErrorActionPreference = 'Stop'
$taskProject = Join-Path $PSScriptRoot 'RevancedStudio.csproj'
& dotnet build $taskProject -c Release /m:1 /nodeReuse:false /p:UseSharedCompilation=false
if ($LASTEXITCODE -ne 0) { throw 'Studio build failed.' }
$taskOutput = Join-Path $PSScriptRoot 'bin/Release/net48'
$taskPackage = Join-Path $PSScriptRoot 'package'
[IO.Directory]::CreateDirectory($taskPackage) | Out-Null
foreach ($taskFile in Get-ChildItem -LiteralPath $taskOutput -File -Filter '*.dll' | Where-Object Name -ne 'MPNDossier.dll') { Copy-Item -LiteralPath $taskFile.FullName -Destination $taskPackage -Force }
foreach ($taskName in @('REVANCED Studio.exe','REVANCED Studio.exe.config','REVANCED Studio.pdb')) { Copy-Item -LiteralPath (Join-Path $taskOutput $taskName) -Destination $taskPackage -Force }
Copy-Item -LiteralPath (Join-Path $taskOutput 'MadCard Maker REVANCED.exe') -Destination (Join-Path $taskPackage 'MadCard Maker REVANCED.dll') -Force
Copy-Item -LiteralPath (Join-Path $taskOutput 'MadCard Maker REVANCED.pdb') -Destination $taskPackage -Force
foreach ($taskStale in @('MPNDossier.dll','MPNDossier.pdb')) { Remove-Item -LiteralPath (Join-Path $taskPackage $taskStale) -Force -ErrorAction SilentlyContinue }
Remove-Item -LiteralPath (Join-Path $taskPackage 'Data/Dossier') -Recurse -Force -ErrorAction SilentlyContinue
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'README.md') -Destination $taskPackage -Force
Copy-Item -LiteralPath (Join-Path $PSScriptRoot '../THIRD_PARTY_NOTICES.md') -Destination $taskPackage -Force
if ($ReferenceData) {
    $taskMadCardData = Join-Path $ReferenceData 'MadCard'
    if (!(Test-Path -LiteralPath (Join-Path $taskMadCardData 'db/base'))) { throw "Reference data is missing MadCard/db/base." }
    foreach($taskFile in Get-ChildItem -LiteralPath $taskMadCardData -Recurse -File){$taskRelative=[IO.Path]::GetRelativePath([IO.Path]::GetFullPath($taskMadCardData),$taskFile.FullName);$taskTarget=Join-Path (Join-Path $taskPackage 'Data/MadCard') $taskRelative;[IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($taskTarget))|Out-Null;Copy-Item -LiteralPath $taskFile.FullName -Destination $taskTarget -Force}
}
@{Built=(Get-Date).ToString('o');ReferenceDataBundled=(Test-Path -LiteralPath (Join-Path $taskPackage 'Data/MadCard/db/base'));DossierIntegrated=$false;GameRuntimeTested=$false} | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $taskPackage 'verification.json')
Write-Output "Packaged standalone MadCard Studio in $taskPackage. Reference data is supplied separately when omitted."
