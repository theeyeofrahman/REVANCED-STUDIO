param(
    [Parameter(Mandatory)][string]$PrototypeDeck,
    [Parameter(Mandatory)][string]$CharacterDirectory,
    [Parameter(Mandatory)][string]$ChatterDirectory,
    [Parameter(Mandatory)][string]$Database,
    [string]$Output=(Join-Path $PSScriptRoot '../content/poop scoop box of toys')
)
$ErrorActionPreference='Stop'
$taskDeck=Get-Content -LiteralPath $PrototypeDeck -Raw|ConvertFrom-Json -AsHashtable
$taskCharacters=@();$taskChatters=@();$taskIds=[Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
function Require-Stock([string]$type,[string]$id){
    if(!$id -or $id -in @('None','empty')){return}
    if(!(Test-Path -LiteralPath (Join-Path $Database "base/$type/$id.json"))){throw "Missing stock reference: $type/$id"}
}
foreach($taskCard in $taskDeck.Character){
    $taskId=$taskCard.Filename
    if(!$taskIds.Add($taskId)){throw "Duplicate character: $taskId"}
    $taskNative=Get-Content -LiteralPath (Join-Path $CharacterDirectory ($taskId+'.json')) -Raw|ConvertFrom-Json -AsHashtable
    $taskChatterId='CC_'+$taskId
    $taskChatter=Get-Content -LiteralPath (Join-Path $ChatterDirectory ($taskChatterId+'.json')) -Raw|ConvertFrom-Json -AsHashtable
    $taskChatterRoot=$taskCard.TextChatter
    $taskCard.Name=$taskNative.FullName;$taskCard.NameShort=$taskNative.ShortName
    foreach($taskField in @('Description','Title','Level','Health','TacBar','Scale','Attributes','Speed','Toughness','VoiceSet')){$taskCard[$taskField]=$taskNative[$taskField]}
    $taskCard.CorpusBlocks=$taskNative.CorpusBlocks
    $taskCard.ArmorHead=[double]$taskNative.ArmorHead.x;$taskCard.ArmorBody=[double]$taskNative.ArmorBody.x
    $taskCard.CountWornArmor=[bool]$taskNative.CountWornArmor
    $taskCard.Uniform=@(foreach($taskPiece in $taskNative.Uniform){[ordered]@{
        ArmorPicks=@($taskPiece.ArmorPicks.Armor);TintPrimary=$taskPiece.Tint;TintSecondary=$taskPiece.Tint2;TintGlass=$taskPiece.Glass
        AltMaterial=[int]$taskPiece.AltMaterialIndex;AltMesh=[int]$taskPiece.AltMeshIndex
    }})
    # Keep the original MadCard stock loadouts; NEM-only OH_* dependencies are not copied.
    $taskCard.TextChatter=$taskChatterId
    $taskChatter.Name=$taskChatter.FullName;$taskChatter.NameShort=$taskChatter.ShortName
    $taskChatter.Remove('FullName');$taskChatter.Remove('ShortName')
    $taskChatter.Filename=$taskChatterId;$taskChatter.Root=$taskChatterRoot;$taskChatter.Priority=1
    # NEM uses a font alias; MadCard uses the actual bundled font name.
    if($taskChatter.Font -eq 'NEXUS'){$taskChatter.Font='BlackOpsOne-Regular'}
    foreach($taskPair in @(@('Character',$taskCard.Root),@('VoiceSet',$taskCard.VoiceSet),@('AnimationProfile',$taskCard.AnimationSet),@('Chatter',$taskChatterRoot))){Require-Stock $taskPair[0] $taskPair[1]}
    foreach($taskPiece in $taskCard.Uniform){foreach($taskArmor in $taskPiece.ArmorPicks){Require-Stock 'Armor' $taskArmor}}
    foreach($taskCorpus in $taskCard.CorpusBlocks){Require-Stock 'Corpus' $taskCorpus}
    foreach($taskField in @('DefaultWeapon','DefaultSidearm','DefaultOffHand','DefaultThrown','UnarmedReplacement')){
        $taskWeapon=$taskCard[$taskField];if(!$taskWeapon -or $taskWeapon -in @('None','empty')){continue}
        $taskFound=$false;foreach($taskType in @('Melee','Ranged','Thrown')){if(Test-Path -LiteralPath (Join-Path $Database "base/$taskType/$taskWeapon.json")){$taskFound=$true}}
        if(!$taskFound){throw "Missing weapon: $taskWeapon"}
    }
    if($taskCard.Contains('FullName') -or $taskCard.Contains('DefaultPrimary') -or $taskCard.Contains('DefaultFaction')){throw 'Legacy NEM fields remain in a character.'}
    # Sparse overrides keep unchanged stock effects and materials inherited from their root.
    foreach($taskEntry in @(@{Card=$taskCard;Type='Character'},@{Card=$taskChatter;Type='Chatter'})){
        $taskRoot=Get-Content -LiteralPath (Join-Path $Database ('base/'+$taskEntry.Type+'/'+$taskEntry.Card.Root+'.json')) -Raw|ConvertFrom-Json -AsHashtable
        foreach($taskField in @($taskEntry.Card.Keys)){
            if($taskField -in @('Filename','Root','Priority','VoiceSet')){continue}
            if($taskRoot.Contains($taskField) -and (($taskEntry.Card[$taskField]|ConvertTo-Json -Depth 64 -Compress) -ceq ($taskRoot[$taskField]|ConvertTo-Json -Depth 64 -Compress))){$taskEntry.Card.Remove($taskField)}
        }
    }
    $taskCharacters+=$taskCard;$taskChatters+=$taskChatter
}
if($taskCharacters.Count -ne 10 -or $taskChatters.Count -ne 10){throw 'Expected ten characters and ten chatter cards.'}
$taskJson=([ordered]@{Chatter=$taskChatters;Character=$taskCharacters}|ConvertTo-Json -Depth 64)
$taskJson=[regex]::Replace($taskJson,'\[\s*\]','["empty"]')
[IO.Directory]::CreateDirectory([IO.Path]::GetFullPath($Output))|Out-Null
[IO.File]::WriteAllText((Join-Path $Output 'Z-52.json'),$taskJson,[Text.UTF8Encoding]::new($false))
if(!(Test-Path -LiteralPath (Join-Path $Output 'info.txt'))){
    [ordered]@{Name='poop scoop box of toys';Description='Z-52: ten original units and their individual combat chatter, in typed MadCard format.';Author='Zayden';Tags=@('Characters','Chatter')}|ConvertTo-Json -Depth 5|Set-Content -LiteralPath (Join-Path $Output 'info.txt') -Encoding utf8NoBOM
}
Write-Output 'Converted 10 Z-52 characters and 10 distinct chatter cards. Stock roots, voices, uniforms and weapons resolved.'
