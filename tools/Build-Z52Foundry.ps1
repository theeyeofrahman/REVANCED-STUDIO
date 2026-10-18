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
$taskVariantsBySource=@{};$taskVariants=@()
# Tier-3 deployment profiles retain each unit's appearance, voice and chatter while
# replacing the inherited late-game combat stats, perks and special attacks.
$taskProfiles=@(
    @{Id='Z52_01_Receiver';Level=4;Health=4;Head=0;Body=1;Tac=0;Attributes=@(8,8,8,4);Group='AAHW_Base';Speed=@(.9,.75,1);Behaviors=@('Runner','Personal','FreeAttacker')},
    @{Id='Z52_02_Enforcer';Level=5;Health=5;Head=1;Body=2;Tac=0;Attributes=@(9,9,8,6);Group='AAHW_Base';Speed=@(.9,.75,1);Behaviors=@('Personal','HoldCover','FreeAttacker')},
    @{Id='Z52_03_Breacher';Level=6;Health=8;Head=2;Body=3;Tac=0;Attributes=@(10,10,7,7);Group='AAHW_Base';Speed=@(.8,.75,1);Behaviors=@('Personal','FreeAttacker')},
    @{Id='Z52_04_Deadwire';Level=6;Health=5;Head=0;Body=1;Tac=0;Attributes=@(8,7,11,6);Group='AAHW_Base';Speed=@(1.05,.85,1);Behaviors=@('Personal','Focused','FreeAttacker')},
    @{Id='Z52_05_Surveyor';Level=8;Health=5;Head=0;Body=1;Tac=0;Attributes=@(8,8,10,11);Group='AAHW_Skilled';Speed=@(1,.8,1);Behaviors=@('Camper','Accurate','TargetShy','HoldCover')},
    @{Id='Z52_06_Suppressor';Level=10;Health=24;Head=3;Body=4;Tac=0;Attributes=@(16,14,6,11);Group='AAHW_Agent';Speed=@(.3,1,1);Behaviors=@('ItchyFinger','Personal','NoSprint','FreeAttacker')},
    @{Id='Z52_07_Spark';Level=5;Health=4;Head=0;Body=1;Tac=0;Attributes=@(7,8,9,7);Group='AAHW_Base';Speed=@(.9,.75,1);Behaviors=@('Personal','HoldCover','FreeAttacker')},
    @{Id='Z52_08_Warden';Level=9;Health=12;Head=2;Body=3;Tac=0;Attributes=@(13,12,5,8);Group='AAHW_Skilled';Speed=@(.55,.6,.8);Behaviors=@('Personal','NoSprint','FreeAttacker')},
    @{Id='Z52_09_Marshal';Level=9;Health=9;Head=1;Body=2;Tac=0;Attributes=@(11,10,10,11);Group='AAHW_Skilled';Speed=@(.85,.75,1);Behaviors=@('Tactical','HoldCover','FreeAttacker')},
    @{Id='Z52_10_Blackout';Level=12;Health=30;Head=3;Body=4;Tac=.5;Attributes=@(18,16,9,14);Group='AAHW_Agent';Speed=@(.7,.7,1);Behaviors=@('Tactical','HoldCover','FreeAttacker')}
)
foreach($taskProfile in $taskProfiles){
    if(!$taskCharacters.ContainsKey($taskProfile.Id)){throw "The active roster is missing $($taskProfile.Id)"}
    $taskVariant=$taskCharacters[$taskProfile.Id]|ConvertTo-Json -Depth 64|ConvertFrom-Json -AsHashtable
    $taskVariant.Filename=$taskProfile.Id.Replace('Z52_','Z52_Foundry_');$taskVariant.Priority=2
    $taskVariant.Description='A Z-52 '+$taskVariant.NameShort+' assigned to the Foundry containment detail.'
    $taskVariant.Level=[int]$taskProfile.Level;$taskVariant.Health=[double]$taskProfile.Health
    $taskVariant.ArmorHead=[double]$taskProfile.Head;$taskVariant.ArmorBody=[double]$taskProfile.Body
    $taskVariant.TacBar=[double]$taskProfile.Tac;$taskVariant.Attributes=$taskProfile.Attributes
    $taskVariant.CorpusBlocks=@('Basic');$taskVariant.SkillGroups=@($taskProfile.Group)
    foreach($taskField in @('Traits','Skills','SpecialSkills','RemovedSkills','Vocations','ExecutionsOffensive','ExecutionsDefensive')){$taskVariant[$taskField]=@('empty')}
    $taskVariant.Behaviors=$taskProfile.Behaviors
    $taskVariant.Speed=[ordered]@{x=[double]$taskProfile.Speed[0];y=[double]$taskProfile.Speed[1];z=[double]$taskProfile.Speed[2]}
    $taskVariant.SprintBonus=0.0;$taskVariant.Toughness=0.0;$taskVariant.CountWornArmor=$false
    $taskVariant.DefaultThrown='None';$taskVariant.DefaultOffHand='None';$taskVariant.DefaultSidearm='None'
    $taskVariant.Fist='U_Fist';$taskVariant.Kick='None';$taskVariant.Headbutt='None';$taskVariant.SlamExplode='None'
    if($taskVariant.Health -gt 30 -or $taskVariant.Level -gt 12 -or $taskVariant.CorpusBlocks.Count -ne 1 -or $taskVariant.TacBar -gt .5){throw 'Foundry combat profile exceeds its intended limits.'}
    $taskVariantsBySource[$taskProfile.Id]=$taskVariant.Filename
    $taskCharacters[$taskVariant.Filename]=$taskVariant;$taskVariants+=$taskVariant
}
function Unit([string]$id,[int]$count,[double]$scale,[double]$growth){
    if($taskVariantsBySource.ContainsKey($id)){$id=$taskVariantsBySource[$id]}
    if(!$taskCharacters.ContainsKey($id)){throw "The active Z-52 roster is missing $id"}
    [ordered]@{Type=$id;Total=$count;GenerationScale=$scale;AddUnitsPerWave=$growth}
}
function Boss([string]$id){
    if($taskVariantsBySource.ContainsKey($id)){$id=$taskVariantsBySource[$id]}
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
foreach($taskSequence in $taskAll){foreach($taskSpawn in @($taskSequence.Units)+@($taskSequence.Bosses)){if($taskSpawn -is [string]){continue};if(!$taskCharacters.ContainsKey($taskSpawn.Type)){throw "Unknown spawn $($taskSpawn.Type)"};if(!$taskSpawn.Type.StartsWith('Z52_Foundry_')){throw 'A full-strength character remains in a Foundry wave.'};[void]$taskSeen.Add($taskSpawn.Type)}}
if($taskSeen.Count -ne 10){throw 'Not all ten Z-52 units are represented.'}
for($taskIndex=0;$taskIndex -lt 3;$taskIndex++){
    $taskOld=$taskBase.WaveSequences[$taskIndex];$taskNew=$taskSequences[$taskIndex]
    if(($taskNew.Units|ForEach-Object {[double]$_.Total}|Measure-Object -Sum).Sum -ne ($taskOld.Units|ForEach-Object {[double]$_.Total}|Measure-Object -Sum).Sum){throw ("Sequence "+$taskIndex+" has "+(($taskNew.Units|ForEach-Object {[double]$_.Total}|Measure-Object -Sum).Sum)+" enemies; expected "+(($taskOld.Units|ForEach-Object {[double]$_.Total}|Measure-Object -Sum).Sum))}
    if($taskNew.TotalWaves -ne $taskOld.TotalWaves -or $taskNew.Cash -ne $taskOld.Cash -or $taskNew.XP -ne $taskOld.XP){throw 'Wave count or rewards changed unexpectedly.'}
}
if(($taskSequences|ForEach-Object {[int]$_.TotalWaves}|Measure-Object -Sum).Sum -ne 10){throw 'Expected ten finite waves.'}
$taskDocument=[ordered]@{Character=@($taskVariants);Stage_Arena=@($taskArena)}
[IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName([IO.Path]::GetFullPath($Output)))|Out-Null
[IO.File]::WriteAllText([IO.Path]::GetFullPath($Output),($taskDocument|ConvertTo-Json -Depth 64),[Text.UTF8Encoding]::new($false))
Write-Output 'Built Foundry-Z52.json: ten tier-3 character variants, ten finite waves and scaled endless enemies. Bosses have one corpus block; Blackout has 30 health and 0.5 TAC. Wave counts, enemy budgets and rewards retained.'
