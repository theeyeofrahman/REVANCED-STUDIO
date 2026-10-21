param([Parameter(Mandatory)][ValidateRange(1,10)][int]$Through)
$ErrorActionPreference='Stop'
$taskRoot=Split-Path $PSScriptRoot -Parent
$taskContent=Join-Path $taskRoot 'content/poop scoop box of toys'
$taskSource=Get-Content -LiteralPath (Join-Path $taskContent 'Zayden-Crew-Arena.json') -Raw|ConvertFrom-Json -AsHashtable
if($taskSource.Origin.Count -lt 8){throw 'The eight balanced Arena origin prototypes are missing.'}
$taskDefs=@(
 @{Key='Kiln';Faction='Kiln Union';Name='Foundry Hand';RootIndex=0;Bonuses=@(0,0,-1,1);Description='You grew up on the foundry floor. You know the safe side of a hot door and how to turn scrap into a working tool. Your union colors and borrowed face shield mark the crew that taught you to work together.'},
 @{Key='Violet';Faction='Violet Relay';Name='Signal Broker';RootIndex=1;Bonuses=@(0,1,0,-1);Description='You traded route reports between the Violet Relay rooftops. Your plum field coat and pale lens mark a courier trained to notice details and leave before trouble closes in.'},
 @{Key='Ashline';Faction='Ashline Mutual';Name='Road Escort';RootIndex=2;Bonuses=@(0,1,-1,0);Description='You rode the Ashline convoys as a guard and navigator. Dust-colored travel gear carries the faded teal mark of a crew that saves room for one more passenger.'},
 @{Key='Latchkey';Faction='Latchkey Syndicate';Name='Quiet Entry';RootIndex=3;Bonuses=@(0,1,0,-1);Description='You learned to open sealed doors for the Latchkey crew. Charcoal clothes and brass details help you blend into old service corridors; your dry humor keeps jobs from getting tense.'},
 @{Key='Harbor';Faction='Glass Harbor';Name='Dock Salvager';RootIndex=4;Bonuses=@(-1,0,1,0);Description='You pulled useful parts from flooded warehouses with Glass Harbor. Slate-blue gear and sea-glass lenses still smell like salt and machine oil.'},
 @{Key='Quarry';Faction='Red Quarry Combine';Name='Quarry Spotter';RootIndex=5;Bonuses=@(0,1,0,-1);Description='You called safe routes through the Red Quarry pits. Oxide panels and amber safety glass are the colors of a crew that never starts a blast without checking every person.'},
 @{Key='Siltwater';Faction='Siltwater Wardens';Name='Pump Tender';RootIndex=6;Bonuses=@(-1,0,1,0);Description='You kept the Siltwater pumps running through dry seasons. River-green weather gear and sealed blue kit mark a careful worker who checks the water before anyone drinks.'},
 @{Key='Rail';Faction='Black Rail Compact';Name='Line Runner';RootIndex=7;Bonuses=@(0,1,-1,0);Description='You carried messages between Black Rail platforms. Burgundy panels on blackened gear identify a quick courier who always knows the next stop.'},
 @{Key='Afterlight';Faction='Afterlight Clinic';Name='Ward Volunteer';RootIndex=0;Bonuses=@(-1,0,0,1);Description='You helped the Afterlight Clinic turn abandoned rooms into safe wards. Weathered light panels and clinic-green trim mark a calm volunteer who makes time for one more patient.'},
 @{Key='Cinder';Faction='Cinder Circuit';Name='Relay Tinkerer';RootIndex=4;Bonuses=@(0,0,1,-1);Description='You kept Cinder Circuit radios and relay towers alive with scavenged parts. Graphite coveralls, copper panels, and live-cyan lenses show your repair crew. Cinder still answers when Zayden calls.'}
)
function Rgb([string]$hex){$hex=$hex.TrimStart('#');[ordered]@{r=[math]::Round(([Convert]::ToInt32($hex.Substring(0,2),16)/255),6);g=[math]::Round(([Convert]::ToInt32($hex.Substring(2,2),16)/255),6);b=[math]::Round(([Convert]::ToInt32($hex.Substring(4,2),16)/255),6);a=1.0}}
$taskPalettes=@{
 Kiln=@('#4F5358','#B45B2B','#F7A138');Violet=@('#30283A','#7A5B91','#C7A0EA');Ashline=@('#85745D','#4E7773','#EBCB83');Latchkey=@('#292925','#96713A','#E7C46C');Harbor=@('#344A5A','#3C7B83','#91E1D7');Quarry=@('#51403A','#A55438','#E6B05A');Siltwater=@('#30474A','#70866A','#B8D79C');Rail=@('#303137','#713E4A','#DF7867');Afterlight=@('#D6D0BA','#5D806D','#B5E1BD');Cinder=@('#343A3B','#B9633B','#69D8CD')
}
$taskOut=@()
for($i=0;$i -lt $Through;$i++){
 $taskDef=$taskDefs[$i];$taskOrigin=($taskSource.Origin[$taskDef.RootIndex]|ConvertTo-Json -Depth 64 -Compress|ConvertFrom-Json -AsHashtable)
 $taskOrigin.Filename='Origin_Overhaul_'+$taskDef.Key;$taskOrigin.Name=$taskDef.Name;$taskOrigin.NameShort=$taskDef.Name
 $taskOrigin.Description=$taskDef.Description;$taskOrigin.AttributeBonuses=$taskDef.Bonuses;$taskOrigin.Priority=1
 $taskPalette=$taskPalettes[$taskDef.Key]
 foreach($taskUniform in $taskOrigin.DefaultUniforms){$taskUniform.TintPrimary=Rgb $taskPalette[0];$taskUniform.TintSecondary=Rgb $taskPalette[1];$taskUniform.TintGlass=Rgb $taskPalette[2]}
 foreach($taskSwatch in $taskOrigin.Swatch_Pairs){$taskSwatch.Color1=Rgb $taskPalette[0];$taskSwatch.Color2=Rgb $taskPalette[1];$taskSwatch.Name=$taskDef.Faction+' colors'}
 foreach($taskSwatch in $taskOrigin.Swatch_Solos){$taskSwatch.Color1=Rgb $taskPalette[2];$taskSwatch.Name=$taskDef.Faction+' signal'}
 $taskOut+=$taskOrigin
}
$json=[ordered]@{Origin=$taskOut}|ConvertTo-Json -Depth 64
[IO.File]::WriteAllText((Join-Path $taskContent 'Overhaul-Origins.json'),$json,[Text.UTF8Encoding]::new($false))
Write-Output "Built $Through faction-themed Arena Origins with inherited balanced perks."
