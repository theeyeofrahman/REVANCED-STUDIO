param([Parameter(Mandatory)][ValidateRange(1,100)][int]$Through)
$ErrorActionPreference='Stop'
$taskRoot=Split-Path $PSScriptRoot -Parent
$taskContent=Join-Path $taskRoot 'content/poop scoop box of toys'
$taskZ52=Get-Content -LiteralPath (Join-Path $taskContent 'Z-52.json') -Raw|ConvertFrom-Json -AsHashtable
$taskFoundry=Get-Content -LiteralPath (Join-Path $taskContent 'Foundry-Z52.json') -Raw|ConvertFrom-Json -AsHashtable
$taskTemplates=@($taskFoundry.Character|Where-Object {$_.Filename -like 'Z52_Foundry_*'}|Sort-Object { [int](($_.Filename -replace '^Z52_Foundry_','') -replace '_.*$','') })
$taskChatter=@($taskZ52.Chatter|Sort-Object { [int](($_.Filename -replace '^CC_Z52_','') -replace '_.*$','') })
if($taskTemplates.Count -ne 10 -or $taskChatter.Count -ne 10){throw 'Expected ten validated Foundry character templates and ten Z-52 chatter templates.'}

$taskFactions=@(
 [ordered]@{Key='Kiln';Name='Kiln Union';Territory='the abandoned foundry belt';Look='soot-dark work armor with ember-orange face glass';Motto='finish the shift, bring everyone home';Primary='#4F5358';Secondary='#B45B2B';Glass='#F7A138';Calls='Bricker,Spall,Rivet,Gauge,Cinderjack,Fuse,Trowel,Slag,Vent,Anvil';Style='plainspoken shop-floor radio calls; they explain the job and never hide behind riddles.'},
 [ordered]@{Key='Violet';Name='Violet Relay';Territory='the rooftops and old signal exchanges';Look='deep plum coats over pale plates with violet glass';Motto='information is a way out';Primary='#30283A';Secondary='#7A5B91';Glass='#C7A0EA';Calls='Vesper,Lilt,Halide,Morrow,Quill,Sable,Wink,Aperture,Dialtone,Serein';Style='cool, precise intelligence trades; every call includes a useful detail.'},
 [ordered]@{Key='Ashline';Name='Ashline Mutual';Territory='the convoy roads beyond the city';Look='dust-tan travel gear with faded teal markings';Motto='a spare seat is a promise';Primary='#85745D';Secondary='#4E7773';Glass='#EBCB83';Calls='Crosswind,Wayfarer,Rations,Beacon,Lowbeam,Stow,Gull,Thread,Mica,Lantern';Style='protective convoy calls, practical route checks, and reassurance for civilians.'},
 [ordered]@{Key='Latchkey';Name='Latchkey Syndicate';Territory='sealed apartments and corporate vaults';Look='matte charcoal kits with brass hardware and gold lenses';Motto='leave the lock better than you found it';Primary='#292925';Secondary='#96713A';Glass='#E7C46C';Calls='Keyhole,Wedge,Deadbolt,Chime,Shim,Pinfall,Tumbler,Blank,Click,Hinge';Style='dry heist banter and clear entry instructions; they are thieves, not mystics.'},
 [ordered]@{Key='Harbor';Name='Glass Harbor';Territory='the flooded docks and container yards';Look='slate-blue salvage armor with sea-glass lenses';Motto='the tide leaves something for us';Primary='#344A5A';Secondary='#3C7B83';Glass='#91E1D7';Calls='Brine,Wake,Buoy,Sounder,Kelp,Breaker,Netter,Tide,Docklight,Sluice';Style='dockside shorthand, tide calls, and blunt warnings about bad water.'},
 [ordered]@{Key='Quarry';Name='Red Quarry Combine';Territory='the cut stone pits and freight lifts';Look='oxide-red plates over quarry gray with amber safety glass';Motto='no one works a blast alone';Primary='#51403A';Secondary='#A55438';Glass='#E6B05A';Calls='Powderline,Core,Drillbit,Shale,Scrip,Pick,Cobalt,Seam,Blastcap,Dredge';Style='crew-first callouts, measured blast warnings, and hard-earned mine humor.'},
 [ordered]@{Key='Siltwater';Name='Siltwater Wardens';Territory='the reservoir pumps and drainage tunnels';Look='river-green rain gear with deep-blue seals';Motto='keep the pumps clean and the people breathing';Primary='#30474A';Secondary='#70866A';Glass='#B8D79C';Calls='Mire,Floodgate,Clarity,Cistern,Sump,Oxbow,Rill,Spillway,Current,Wader';Style='calm water-system reports, contamination checks, and clear evacuation directions.'},
 [ordered]@{Key='Rail';Name='Black Rail Compact';Territory='the freight lines under the industrial quarter';Look='blackened rail armor with burgundy panels and signal-red lenses';Motto='the line stays open';Primary='#303137';Secondary='#713E4A';Glass='#DF7867';Calls='Switchman,Coupler,Sleeper,Brake,Crosstie,Ballast,Spur,Axle,Signalman,Nightcar';Style='short dispatch calls with platform, track, and timing details.'},
 [ordered]@{Key='Afterlight';Name='Afterlight Clinic';Territory='the mobile wards and ruined triage stations';Look='weathered bone-white panels with clinic-green markings';Motto='treat first, argue later';Primary='#D6D0BA';Secondary='#5D806D';Glass='#B5E1BD';Calls='Lumen,Hemlock,Gauze,Sterile,Stitch,Anodyne,Belladonna,Suture,Vial,Bandage';Style='medic-first calls with calm triage, status reports, and direct safety instructions.'},
 [ordered]@{Key='Cinder';Name='Cinder Circuit';Territory='the switching rooms and scavenged relay towers';Look='graphite mechanic suits with copper panels and live-cyan glass';Motto='nothing is dead until the wire is cut';Primary='#343A3B';Secondary='#B9633B';Glass='#69D8CD';Calls='Cinder,Arc,Splice,Fanout,Patchbay,Relay,Capacitor,Loopback,Jumper,Ground';Style='technician banter, quick diagnostics, and clear electrical hazards; Cinder keeps a line open to Zayden.'}
)
$taskRoleNames=@('Field lead','Quartermaster','Pathfinder','Riflehand','Bulwark','Mechanic','Medic','Marksman','Runner','Trainee')
$taskRoleDetails=@(
 'sets the crew plan and gives every member a clear way out',
 'keeps the crew supplied and knows which kit can be repaired in the field',
 'reads routes, sightlines, and exits before the rest of the crew arrives',
 'holds a steady lane and calls out changing enemy positions',
 'takes the exposed approach so lighter crew members can reposition',
 'keeps radios, doors, lifts, and improvised tools working under pressure',
 'stabilizes injured crew and refuses to leave anyone behind',
 'spots threats early and shares a clean shot with the rest of the team',
 'moves messages and supplies through gaps the larger crew cannot use',
 'is the newest member, learning the route from people who trust them'
)
$taskStats=@(
 @{Level=8;Health=10;Attributes=@(12,11,11,10);Group='AAHW_Skilled'},
 @{Level=6;Health=7;Attributes=@(9,9,10,11);Group='AAHW_Base'},
 @{Level=5;Health=6;Attributes=@(8,12,9,9);Group='AAHW_Base'},
 @{Level=5;Health=7;Attributes=@(10,10,9,9);Group='AAHW_Base'},
 @{Level=6;Health=9;Attributes=@(11,9,11,8);Group='AAHW_Base'},
 @{Level=5;Health=7;Attributes=@(8,9,10,11);Group='AAHW_Base'},
 @{Level=5;Health=6;Attributes=@(8,10,11,10);Group='AAHW_Base'},
 @{Level=6;Health=7;Attributes=@(9,12,9,11);Group='AAHW_Skilled'},
 @{Level=4;Health=5;Attributes=@(8,12,8,8);Group='AAHW_Base'},
 @{Level=4;Health=5;Attributes=@(8,9,9,8);Group='AAHW_Base'}
)
function Rgb([string]$hex){
 $hex=$hex.TrimStart('#');[ordered]@{r=[math]::Round(([Convert]::ToInt32($hex.Substring(0,2),16)/255),6);g=[math]::Round(([Convert]::ToInt32($hex.Substring(2,2),16)/255),6);b=[math]::Round(([Convert]::ToInt32($hex.Substring(4,2),16)/255),6);a=1.0}
}
$taskLines=[ordered]@{}
for($f=0;$f -lt $taskFactions.Count;$f++){
 $taskFaction=$taskFactions[$f];$taskCards=@();$taskVoices=@()
 $taskNames=$taskFaction.Calls.Split(',')
 for($i=0;$i -lt $taskNames.Count;$i++){
  $taskOrdinal=($f*10)+$i+1;if($taskOrdinal -gt $Through){break}
  $taskName=$taskNames[$i];$taskRole=$taskRoleNames[$i];$taskStat=$taskStats[$i];$taskTemplate=$taskTemplates[$i]
  $taskCard=($taskTemplate|ConvertTo-Json -Depth 64 -Compress|ConvertFrom-Json -AsHashtable)
  $taskChatterTemplate=$taskChatter[$i];$taskChatterId='CC_OVR_'+$taskFaction.Key+'_'+$taskName
  $taskCard.Filename='OVR_'+$taskFaction.Key+'_'+$taskName;$taskCard.Name=$taskName;$taskCard.NameShort=$taskName
  $taskCard.Description="$taskName is the $taskRole of the $($taskFaction.Name), working $($taskFaction.Territory). $($taskRoleDetails[$i]). Their field kit shows the crew's colors: $($taskFaction.Look)."
  $taskCard.Title=$taskRole;$taskCard.Faction=$taskFaction.Name;$taskCard.Level=$taskStat.Level;$taskCard.Health=$taskStat.Health;$taskCard.TacBar=0
  $taskCard.Attributes=$taskStat.Attributes;$taskCard.SkillGroups=@($taskStat.Group);$taskCard.CorpusBlocks=@('Basic');$taskCard.TextChatter=$taskChatterId
  foreach($taskUniform in $taskCard.Uniform){$taskUniform.TintPrimary=Rgb $taskFaction.Primary;$taskUniform.TintSecondary=Rgb $taskFaction.Secondary;$taskUniform.TintGlass=Rgb $taskFaction.Glass}
  $taskCards+=$taskCard
  $taskVoice=($taskChatterTemplate|ConvertTo-Json -Depth 64 -Compress|ConvertFrom-Json -AsHashtable)
  $taskVoice.Filename=$taskChatterId;$taskVoice.Name="$taskName - $($taskFaction.Name) combat chatter";$taskVoice.NameShort=$taskName;$taskVoice.Priority=1
  $taskVoice.Description="Custom event chatter for $taskName. $($taskFaction.Style)"
  $taskVoice.Spawned=@("$taskName, $taskRole, reporting from $($taskFaction.Territory).")
  $taskVoice.WakeUp=@("$taskName, eyes up. $($taskFaction.Name) still has work to finish.")
  $taskVoice.Attacking=@("$taskName taking the $($taskRole.ToLower()) lane. $($taskFaction.Motto).")
  $taskVoice.Chasing=@("$taskName on the pursuit. Keep my signal as I push them toward the $($taskFaction.Territory).")
  $taskVoice.Waiting=@("$taskName holding at the $($taskFaction.Key) staging point. Check your route before you move.")
  $taskVoice.FriendDied=@("$taskName here. We lost one of ours; I'll cover the crew while you mark the spot.")
  $taskVoice.EnemyDown=@("$taskName confirms one hostile down. Watch the next angle.")
  $taskVoice.Wounded=@("$taskName is hit! Still moving; get me behind cover!")
  $taskVoice.Died=@("$taskName signing off. Tell the $($taskFaction.Name) crew I kept my word.")
  $taskVoices+=$taskVoice
 }
 if($taskCards.Count -gt 0){$taskLines[$taskFaction.Key]=[ordered]@{Character=$taskCards;Chatter=$taskVoices}}
}

foreach($faction in $taskFactions){
 $file=Join-Path $taskContent ('Overhaul-'+$faction.Key+'.json')
 if($taskLines.Contains($faction.Key)){$json=$taskLines[$faction.Key]|ConvertTo-Json -Depth 64}else{$json=''}
 if($json){[IO.File]::WriteAllText($file,$json,[Text.UTF8Encoding]::new($false))}elseif(Test-Path -LiteralPath $file){Remove-Item -LiteralPath $file -Force}
}
Write-Output "Built faction roster through member $Through."
