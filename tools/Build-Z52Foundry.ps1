param(
    [Parameter(Mandatory)][string]$Database,
    [Parameter(Mandatory)][string]$Roster,
    [string]$Output=(Join-Path $PSScriptRoot '../content/poop scoop box of toys/Foundry-Z52.json')
)
$ErrorActionPreference='Stop'
$taskBase=Get-Content -LiteralPath (Join-Path $Database 'base/Stage_Arena/Arena_Battle1.json') -Raw|ConvertFrom-Json -AsHashtable
if($taskBase.Name -ne 'The Foundry' -or $taskBase.SceneName -ne 'Arena_Foundry'){throw 'Unexpected Foundry reference data.'}
$taskRoster=Get-Content -LiteralPath $Roster -Raw|ConvertFrom-Json -AsHashtable
$taskCharacters=@{};foreach($taskCard in $taskRoster.Character){$taskCharacters[$taskCard.Filename]=$taskCard}
function Unit([string]$id,[int]$count,[double]$scale,[double]$growth){
    if(!$taskCharacters.ContainsKey($id)){throw "The active Z-52 roster is missing $id"}
    [ordered]@{Type=$id;Total=$count;GenerationScale=$scale;AddUnitsPerWave=$growth}
}
function Boss([string]$id){
    if(!$taskCharacters.ContainsKey($id)){throw "The active Z-52 roster is missing boss $id"}
    $taskCard=$taskCharacters[$id];$taskWeapon=$taskCard.DefaultWeapon
    if(!$taskWeapon){$taskRoot=Get-Content -LiteralPath (Join-Path $Database ('base/Character/'+$taskCard.Root+'.json')) -Raw|ConvertFrom-Json -AsHashtable;$taskWeapon=$taskRoot.DefaultWeapon}
    if(!$taskWeapon -or $taskWeapon -eq 'None'){throw "Boss $id has no resolved primary weapon."}
    [ordered]@{Type=$id;Weapon=$taskWeapon;Sidearm='None';Throwable='None';Key='None';SerialNumber='';Gear=@('empty')}
}
function New-FoundrySequence($baseline,$units,$bosses){
    [ordered]@{
        TotalWaves=[int]$baseline.TotalWaves;Cash=[int]$baseline.Cash;XP=[int]$baseline.XP
        WaveMult_Cash=[double]$baseline.WaveMult_Cash;WaveMult_XP=[double]$baseline.WaveMult_XP
        Units=@($units);Bosses=@(if(@($bosses).Count){$bosses}else{'empty'})
    }
}
$taskFirst=@(
    (Unit 'Z52_01_Receiver' 10 .7 0)
    (Unit 'Z52_02_Enforcer' 5 .624 1)
    (Unit 'Z52_04_Deadwire' 1 .459 1)
    (Unit 'Z52_07_Spark' 1 .55 1)
)
$taskSecond=@(
    (Unit 'Z52_01_Receiver' 8 .7 1)
    (Unit 'Z52_02_Enforcer' 9 .656 1)
    (Unit 'Z52_03_Breacher' 5 .459 1)
    (Unit 'Z52_07_Spark' 3 .55 .5)
)
$taskThird=@(
    (Unit 'Z52_02_Enforcer' 9 .7 .1)
    (Unit 'Z52_03_Breacher' 7 .656 .5)
    (Unit 'Z52_04_Deadwire' 5 .459 .5)
    (Unit 'Z52_05_Surveyor' 5 .671 1)
    (Unit 'Z52_07_Spark' 4 .55 .5)
    (Unit 'Z52_08_Warden' 2 .50 .5)
    (Unit 'Z52_09_Marshal' 3 .60 1)
)
$taskEndless=@(
    (Unit 'Z52_01_Receiver' 4 .7 -.5)
    (Unit 'Z52_02_Enforcer' 8 .572 1.5)
    (Unit 'Z52_03_Breacher' 8 .525 1.5)
    (Unit 'Z52_04_Deadwire' 4 .60 .5)
    (Unit 'Z52_05_Surveyor' 3 .671 .5)
    (Unit 'Z52_06_Suppressor' 3 .562 .5)
    (Unit 'Z52_07_Spark' 4 .60 .5)
    (Unit 'Z52_08_Warden' 2 .546 .5)
)
$taskSequences=@(
    (New-FoundrySequence $taskBase.WaveSequences[0] $taskFirst @())
    (New-FoundrySequence $taskBase.WaveSequences[1] $taskSecond @((Boss 'Z52_06_Suppressor')))
    (New-FoundrySequence $taskBase.WaveSequences[2] $taskThird @((Boss 'Z52_10_Blackout')))
)
$taskInfinite=New-FoundrySequence $taskBase.InfiniteSequence $taskEndless @((Boss 'Z52_09_Marshal'),(Boss 'Z52_10_Blackout'))
$taskArena=[ordered]@{
    Filename='Arena_Battle1';Root='Arena_Battle1';Priority=1
    Description='Z-52 has sealed the Foundry and turned its engineering platform into a containment trial. Break through the patrol line, survive the breach teams, and confront Director Blackout. Push beyond the final wave to face escalating Z-52 reinforcements.'
    WaveName='Containment Wave';WaveSequences=$taskSequences;InfiniteSequence=$taskInfinite
}
$taskSeen=[Collections.Generic.HashSet[string]]::new()
$taskAll=@($taskSequences)+@($taskInfinite)
foreach($taskSequence in $taskAll){foreach($taskSpawn in @($taskSequence.Units)+@($taskSequence.Bosses)){if($taskSpawn -is [string]){continue};if(!$taskCharacters.ContainsKey($taskSpawn.Type)){throw "Unknown spawn $($taskSpawn.Type)"};[void]$taskSeen.Add($taskSpawn.Type)}}
if($taskSeen.Count -ne 10){throw 'Not all ten Z-52 units are represented.'}
for($taskIndex=0;$taskIndex -lt 3;$taskIndex++){
    $taskOld=$taskBase.WaveSequences[$taskIndex];$taskNew=$taskSequences[$taskIndex]
    if(($taskNew.Units|ForEach-Object {[double]$_.Total}|Measure-Object -Sum).Sum -ne ($taskOld.Units|ForEach-Object {[double]$_.Total}|Measure-Object -Sum).Sum){throw ("Sequence "+$taskIndex+" has "+(($taskNew.Units|ForEach-Object {[double]$_.Total}|Measure-Object -Sum).Sum)+" enemies; expected "+(($taskOld.Units|ForEach-Object {[double]$_.Total}|Measure-Object -Sum).Sum))}
    if($taskNew.TotalWaves -ne $taskOld.TotalWaves -or $taskNew.Cash -ne $taskOld.Cash -or $taskNew.XP -ne $taskOld.XP){throw 'Wave count or rewards changed unexpectedly.'}
}
if(($taskSequences|ForEach-Object {[int]$_.TotalWaves}|Measure-Object -Sum).Sum -ne 10){throw 'Expected ten finite waves.'}
$taskDocument=[ordered]@{Stage_Arena=@($taskArena)}
[IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName([IO.Path]::GetFullPath($Output)))|Out-Null
[IO.File]::WriteAllText([IO.Path]::GetFullPath($Output),($taskDocument|ConvertTo-Json -Depth 64),[Text.UTF8Encoding]::new($false))
Write-Output 'Built Foundry-Z52.json: ten finite waves, all ten Z-52 roles, Suppressor and Blackout bosses, and an endless Z-52 sequence. Original finite wave counts, initial enemy budgets and rewards retained.'
